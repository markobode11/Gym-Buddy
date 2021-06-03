import React, { useContext, useEffect, useState } from "react"
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import ExerciseListItem from "../../components/ExerciseListItem";
import Search from "../../components/Search";
import { AppContext } from "../../context/AppContext";
import { IExercise } from "../../domain/IExercise";
import { BaseService } from "../../services/base-service";

const ExercisesIndex = () => {
    const [errorMessage, setErrorMessage] = useState('')
    const [deleteMessage, setDeleteMessage] = useState('')
    const [exercises, setExercises] = useState([] as IExercise[])
    const [exercisesToShow, setExercisesToShow] = useState([] as IExercise[])
    const appState = useContext(AppContext);

    const loadData = async () => {
        const response = await BaseService.getAll<IExercise>('/exercises')
        if (response.ok) {
            setExercises(response.data!)
            setExercisesToShow(response.data!)
        } else {
            setErrorMessage(response.message!)
        }
    }

    useEffect(() => {
        loadData();
    }, []);

    const confirmDelete = (id: number) => {
        const deleteButton = document.getElementById(`${id}-delete`)
        const confirmDeleteButton = document.getElementById(`${id}-confirm-delete`)

        deleteButton!.style.display = 'none';
        confirmDeleteButton!.style.display = 'block';
    }

    const onDelete = async (id: number) => {
        const response = await BaseService.delete("/exercises/" + id, appState.token!)
        if (response.ok) {
            setDeleteMessage('Deleted!');
            setExercises([...exercises.filter(x => x.id !== id)])
            setExercisesToShow([...exercisesToShow.filter(x => x.id !== id)])
        } else {
            setErrorMessage(response.message!);
        }
    }

    const onSearch = (search: string) => {
        setExercisesToShow([...exercises.filter(x => x.name.toLowerCase().includes(search.toLowerCase()))])
    }

    const onClear = () => {
        setExercisesToShow([...exercises])
    }

    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">Exercises</h1>
                        <p className="lead text-light">Learn about different exercises.</p>
                        {appState.isAdmin ?
                            <p>
                                <Link to='/exercises/create' className="btn btn-primary my-2">Create new</Link>
                            </p>
                            : null}
                    </div>
                </div>
            </section>

            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />
            <Alert show={deleteMessage !== ''} message={deleteMessage} alertClass={EAlertClass.Success} />

            <Search onSearch={onSearch} onClear={onClear}/>
            <div className="album py-5">
                <div className="container">

                    <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3">

                        {exercisesToShow.map(exercise => <ExerciseListItem onConfirmDelete={confirmDelete} onDelete={onDelete} exercise={exercise} key={exercise.id} />)}

                    </div>
                </div>
            </div>
        </>
    )
}

export default ExercisesIndex
