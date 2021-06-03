import React, { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { InitialWorkout, IWorkout } from "../../domain/IWorkout";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const WorkoutsDetails = () => {
    const { id } = useParams() as IRouteId;
    const [errorMessage, setErrorMessage] = useState('');
    const [workout, setWorkout] = useState(InitialWorkout);

    const loadData = async (id: string) => {
        const response = await BaseService.get<IWorkout>("/workouts/" + id);
        if (response.ok) {
            setWorkout(response.data!)
        } else {
            setErrorMessage(response.message!);
        }
    }

    useEffect(() => {
        loadData(id);
    }, [id])

    return (
        <>
            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />

            <h1>{workout.name}</h1>
            <hr />

            <div>
                <h4>{workout.description}</h4>
                <hr />
                <h4>Duration {workout.duration}</h4>
                <hr />
                <dl className="row">
                    <dt className="col-sm-2">
                        Difficulty
                    </dt>
                    <dd className="col-sm-10">
                        {workout.difficulty?.name ?? ''}
                    </dd>
                </dl>
            </div>
            {workout.exercisesInWorkout.length !== 0
                ?
                <h3 >Exercises in this workout:</h3>
                :
                <h3>No exercises added yet</h3>}

            <div className="list-group">
                {workout.exercisesInWorkout.map((exercise, index) =>
                    <Link to={'/exercises/' + exercise.id} key={index} className="list-group-item list-group-item-action flex-column align-items-start">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{exercise.name}</h5>
                        </div>
                        <h6 className="mb-1">{exercise.description}</h6>
                    </Link>
                )}
            </div>

            <div className="mt-5">
                | <Link to="/workouts">Back to List</Link>
            </div>
        </>
    )
}

export default WorkoutsDetails
