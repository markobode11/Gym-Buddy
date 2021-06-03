import React, { useState, useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import Search from "../../components/Search";
import WorkoutListItem from "../../components/WorkoutListItem";
import { AppContext } from "../../context/AppContext";
import { IWorkout } from "../../domain/IWorkout";
import { BaseService } from "../../services/base-service";

const WorkoutsIndex = () => {
    const [errorMessage, setErrorMessage] = useState('')
    const [deleteMessage, setDeleteMessage] = useState('')
    const [workouts, setWorkouts] = useState([] as IWorkout[])
    const [workoutsToShow, setWorkoutsToShow] = useState([] as IWorkout[])
    const appState = useContext(AppContext);

    const loadData = async () => {
        const response = await BaseService.getAll<IWorkout>('/workouts')
        if (response.ok) {
            setWorkouts(response.data!)
            setWorkoutsToShow(response.data!)
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
        const response = await BaseService.delete("/workouts/" + id, appState.token!)
        if (response.ok) {
            setDeleteMessage('Deleted!');
            setWorkouts([...workouts.filter(x => x.id !== id)])
            setWorkoutsToShow([...workoutsToShow.filter(x => x.id !== id)])
        } else {
            setErrorMessage(response.message!);
        }
    }

    const onSearch = (search: string) => {
        setWorkoutsToShow([...workouts.filter(x => x.name.toLowerCase().includes(search.toLowerCase()))])
    }

    const onClear = () => {
        setWorkoutsToShow([...workouts])
    }
    
    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">Workouts</h1>
                        <p className="lead text-light">Learn about different workouts.</p>
                        {appState.isAdmin ?
                            <p>
                                <Link to='/workouts/create' className="btn btn-primary my-2">Create new</Link>
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

                        {workoutsToShow.map(workout => <WorkoutListItem onConfirmDelete={confirmDelete} onDelete={onDelete} workout={workout} key={workout.id} />)}

                    </div>
                </div>
            </div>
        </>
    )
}

export default WorkoutsIndex
