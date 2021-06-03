import React, { useContext, useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import UserProgramListItem from "../../components/UserProgramListItem";
import { AppContext } from "../../context/AppContext";
import { IProgram } from "../../domain/IProgram";
import { BaseService } from "../../services/base-service";

const MyProgramsIndex = () => {
    const [programs, setPrograms] = useState(undefined as IProgram[] | undefined);
    const [errorMessage, setErrorMessage] = useState('')
    const [deleteMessage, setDeleteMessage] = useState('')
    const appState = useContext(AppContext);

    useEffect(() => {
        const loadUserPrograms = async () => {
            const response = await BaseService.getAll<IProgram>('userprograms', appState.token!);
            if (response.ok) {
                setPrograms(response.data ? response.data : [])
            } else {
                setErrorMessage(response.message!)
            }
        }
        loadUserPrograms();
    }, [appState.token]);

    const confirmDelete = (id: number) => {
        const deleteButton = document.getElementById(`${id}-delete`)
        const confirmDeleteButton = document.getElementById(`${id}-confirm-delete`)

        deleteButton!.style.display = 'none';
        confirmDeleteButton!.style.display = 'block';
    }

    const onDelete = async (programId: number) => {
        const response = await BaseService.delete("/userprograms/" + programId, appState.token!)
        if (response.ok) {
            setDeleteMessage('Removed!');
            setPrograms([...programs!.filter(x => x.id !== programId)])
        } else {
            setErrorMessage(response.message!);
        }
    }

    return programs === undefined ? <></> : (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">My programs</h1>
                        <p className="lead text-light">Visit the programs page to choose yourself a program.</p>
                        <p>
                            <Link to='/programs' className="btn btn-primary my-2">Go to program selection</Link>
                        </p>
                    </div>
                </div>
            </section>

            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />
            <Alert show={deleteMessage !== ''} message={deleteMessage} alertClass={EAlertClass.Success} />

            {programs?.length !== 0 ?
                <div className="album py-5">
                    <div className="container">

                        <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3">

                            {
                                programs.map(program => <UserProgramListItem program={program} onConfirmDelete={confirmDelete} onDelete={onDelete} key={program.id}/>)
                            }

                        </div>
                    </div>
                </div>
                : <div className='alert alert-info'>You have not chosen any programs yet!</div>}

        </>
    )
}

export default MyProgramsIndex