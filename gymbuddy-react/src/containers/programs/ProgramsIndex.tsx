import React, { useState, useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import ProgramListItem from "../../components/ProgramListItem";
import Search from "../../components/Search";
import { AppContext } from "../../context/AppContext";
import { IProgram } from "../../domain/IProgram";
import { IUserProgram } from "../../domain/IUserProgram";
import { BaseService } from "../../services/base-service";

const ProgramsIndex = () => {
    const [errorMessage, setErrorMessage] = useState('')
    const [deleteMessage, setDeleteMessage] = useState('')
    const [programs, setPrograms] = useState([] as IProgram[])
    const [programsToShow, setProgramsToShow] = useState([] as IProgram[])
    const [userPrograms, setUserPrograms] = useState([] as IUserProgram[])
    const appState = useContext(AppContext);

    const loadData = async () => {
        const response = await BaseService.getAll<IProgram>('/fullprograms')
        if (response.ok) {
            setPrograms(response.data!)
            setProgramsToShow(response.data!)
        } else {
            setErrorMessage(response.message!)
        }
    }

    useEffect(() => {
        const loadUserPrograms = async () => {
            const response = await BaseService.getAll<IUserProgram>('/userprograms/all', appState.token!)
            if (response.data?.length ?? -1 > 0) {
                setUserPrograms(response.data!)
            }
        }

        loadData();

        if (appState.token) {
            loadUserPrograms();
        }

    }, [appState.token]);

    const confirmDelete = (id: number) => {
        const deleteButton = document.getElementById(`${id}-delete`)
        const confirmDeleteButton = document.getElementById(`${id}-confirm-delete`)

        deleteButton!.style.display = 'none';
        confirmDeleteButton!.style.display = 'block';
    }

    const onDelete = async (id: number) => {
        const response = await BaseService.delete("/fullprograms/" + id, appState.token!)
        if (response.ok) {
            setDeleteMessage('Deleted!');
            setPrograms([...programs.filter(x => x.id !== id)])
            setProgramsToShow([...programsToShow.filter(x => x.id !== id)])
        } else {
            setErrorMessage(response.message!);
        }
    }

    const addToMyPrograms = async (id: number) => {
        const response = await BaseService.create({}, 'userprograms/' + id, appState.token!)
        if (response.ok) {
            setDeleteMessage('Added to my programs! Navigate to My Programs to see it!');
            const newUserProgram: IUserProgram = {
                fullProgramId: id
            }
            setUserPrograms([...userPrograms, newUserProgram])
        } else {
            setErrorMessage(response.message!)
        }
    }

    const onSearch = (search: string) => {
        setProgramsToShow([...programs.filter(x => x.name.toLowerCase().includes(search.toLowerCase()))])
    }

    const onClear = () => {
        setProgramsToShow([...programs])
    }

    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">Programs</h1>
                        <p className="lead text-light">Learn about different programs.</p>
                        {appState.isAdmin ?
                            <p>
                                <Link to='/programs/create' className="btn btn-primary my-2">Create new</Link>
                            </p>
                            : null}
                    </div>
                </div>
            </section>

            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />
            <Alert show={deleteMessage !== ''} message={deleteMessage} alertClass={EAlertClass.Success} />

            <Search onClear={onClear} onSearch={onSearch} />
            
            <div className="album py-5">
                <div className="container">

                    <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3">

                        {
                            programsToShow.map(program => <ProgramListItem isInUserPrograms={userPrograms.find(x => x.fullProgramId === program.id) ? true : false} addToMyPrograms={addToMyPrograms} onConfirmDelete={confirmDelete} onDelete={onDelete} program={program} key={program.id} />)
                        }

                    </div>
                </div>
            </div>
        </>
    )
}

export default ProgramsIndex
