import React, { useState, useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import MentorListItem from "../../components/MentorListItem";
import Search from "../../components/Search";
import { AppContext } from "../../context/AppContext";
import { IMentor } from "../../domain/IMentor";
import { BaseService } from "../../services/base-service";

const MentorsIndex = () => {
    const [errorMessage, setErrorMessage] = useState('')
    const [deleteMessage, setDeleteMessage] = useState('')
    const [mentors, setMentors] = useState([] as IMentor[])
    const [mentorsToShow, setMentorsToShow] = useState([] as IMentor[])
    const appState = useContext(AppContext);
    const [userHasMentor, setUsetHasMentor] = useState(true)

    const loadData = async () => {
        const response = await BaseService.getAll<IMentor>('/mentors')
        if (response.ok) {
            setMentors(response.data!)
            setMentorsToShow(response.data!)
        } else {
            setErrorMessage(response.message!)
        }
    }

    useEffect(() => {
        loadData();
        const loadMentor = async () => {
            const response = await BaseService.get<IMentor>('/usermentor/mentor', appState.token!)
            if (!response.ok && response.statusCode === 404) {
                setUsetHasMentor(false)
            }
            else if (!response.ok && response.statusCode !== 404) {
                setErrorMessage("Error loading users mentor! " + response.message!)
            }
        }
        if (appState.token) loadMentor();
    }, [appState.token]);

    const confirmDelete = (id: number) => {
        const deleteButton = document.getElementById(`${id}-delete`)
        const confirmDeleteButton = document.getElementById(`${id}-confirm-delete`)

        deleteButton!.style.display = 'none';
        confirmDeleteButton!.style.display = 'block';
    }

    const onDelete = async (id: number) => {
        const response = await BaseService.delete("/mentors/" + id, appState.token!)
        if (response.ok) {
            setDeleteMessage('Deleted!');
            setMentors([...mentors.filter(x => x.id !== id)])
            setMentorsToShow([...mentorsToShow.filter(x => x.id !== id)])
        } else {
            setErrorMessage(response.message!);
        }
    }

    const chooseAsMentor = async (id: number) => {
        const response = await BaseService.create({}, 'usermentor/' + id, appState.token!)
        if (response.ok) {
            setDeleteMessage('Added as a mentor! Navigate to My Mentors to see it!');
            setUsetHasMentor(true)
        } else {
            setErrorMessage(response.message!)
        }
    }

    const onSearch = (search: string) => {
        setMentorsToShow([...mentors.filter(x => x.fullName.toLowerCase().includes(search.toLowerCase()))])
    }

    const onClear = () => {
        setMentorsToShow([...mentors])
    }

    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">Our Mentors</h1>
                        <p className="lead text-light">Learn about our Mentors.</p>
                        {appState.isAdmin ?
                            <p>
                                <Link to='/mentors/create' className="btn btn-primary my-2">Create new</Link>
                            </p>
                            : null}
                    </div>
                </div>
            </section>

            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />
            <Alert show={deleteMessage !== ''} message={deleteMessage} alertClass={EAlertClass.Success} />

            <Search onClear={onClear} onSearch={onSearch}/>
            <div className="album py-5">
                <div className="container">

                    <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3">

                        {mentorsToShow.map(mentor => <MentorListItem userHasMentor={userHasMentor} chooseAsMentor={chooseAsMentor} onConfirmDelete={confirmDelete} onDelete={onDelete} mentor={mentor} key={mentor.id} />)}

                    </div>
                </div>
            </div>
        </>
    )
}

export default MentorsIndex
