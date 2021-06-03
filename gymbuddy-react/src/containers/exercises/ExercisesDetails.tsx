import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom"
import Alert, { EAlertClass } from "../../components/Alert";
import { IExercise, InitialExercise } from "../../domain/IExercise";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const ExercisesDetails = () => {
    const { id } = useParams() as IRouteId;
    const [errorMessage, setErrorMessage] = useState('');
    const [exercise, setExercise] = useState(InitialExercise);

    const loadData = async (id: string) => {
        const response = await BaseService.get<IExercise>("/exercises/" + id);
        if (response.ok) {
            setExercise(response.data!)
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

            <h1>{exercise.name}</h1>

            <div>
                <h4>{exercise.description}</h4>
                <hr />
                <dl className="row">
                    <dt className="col-sm-2">
                        Difficulty
                    </dt>
                    <dd className="col-sm-10">
                        {exercise.difficulty?.name ?? ''}
                    </dd>
                </dl>
            </div>
            {exercise.musclesTrainedInExercise.length !== 0
                ?
                <h3 >This exercise trains:</h3>
                :
                <h3>No trained muscles added yet</h3>}

            <div className="list-group">
                {exercise.musclesTrainedInExercise.map((muscle, index) =>
                    <li key={index} className="list-group-item flex-column align-items-start">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{muscle.everydayName}</h5>
                            <small>{muscle.relevance}</small>
                        </div>
                        <p className="mb-1">{muscle.medicalName}</p>
                    </li>
                )}
            </div>

            <div className="mt-5">
                | <Link to="/exercises">Back to List</Link>
            </div>
        </>
    )
}

export default ExercisesDetails
