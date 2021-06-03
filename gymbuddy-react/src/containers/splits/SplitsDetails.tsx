import React, { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { InitialSplit, ISplit } from "../../domain/ISplit";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const SplitsDetails = () => {
    const { id } = useParams() as IRouteId;
    const [errorMessage, setErrorMessage] = useState('');
    const [split, setSplit] = useState(InitialSplit);

    const loadData = async (id: string) => {
        const response = await BaseService.get<ISplit>("/splits/" + id);
        if (response.ok) {
            setSplit(response.data!)
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

            <h1>{split.name}</h1>
            <hr />

            <h4 className='mt-2'>{split.description}</h4>
            <hr />


            {split.workoutsInSplit.length !== 0
                ?
                <h3 className='mt-2'>Workouts in this split:</h3>
                :
                <h3 className='mt-2'>No workouts added yet</h3>}

            <div className="list-group">
                {split.workoutsInSplit.map((workout, index) =>
                    <Link to={'/workouts/' + workout.id} key={index} className="list-group-item list-group-item-action flex-column align-items-start hover-shadow">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{workout.name}</h5>
                        </div>
                        <p className="mb-1">{workout.description}</p>
                    </Link>
                )}
            </div>

            <div className="mt-5">
                | <Link to="/splits">Back to List</Link>
            </div>
        </>
    )
}

export default SplitsDetails
