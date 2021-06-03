import React, { useState, useContext, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { InitialSplit, ISplit } from "../../domain/ISplit";
import { IWorkout } from "../../domain/IWorkout";
import { IWorkoutInSplit } from "../../domain/IWorkoutInSplit";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const SplitsEdit = () => {
    const { id } = useParams() as IRouteId;

    const [model, setModel] = useState(InitialSplit);
    const [workouts, setWorkouts] = useState([] as IWorkout[])
    const [error, setError] = useState('')
    const [message, setMessage] = useState('')
    const appState = useContext(AppContext)
    const [workoutInSplit, setWorkoutInSplti] = useState({} as IWorkoutInSplit)

    const saveClicked = async () => {
        if (!model.name || !model.description) {
            setError('All fields are required!')
            setMessage('')
            return
        }
        setError('')
        const response = await BaseService.edit<ISplit>(model, "/splits/" + id, appState.token!);
        if (response.ok) {
            setMessage("Saved!")
        } else {
            setError(response.message!);
        }
    }

    const loadData = async (id: string) => {
        const response = await BaseService.get<ISplit>("/splits/" + id);
        if (response.ok) {
            setModel(response.data!)
        } else {
            setError(response.message!);
        }
    }

    const loadWorkouts = async () => {
        const response = await BaseService.getAll<IWorkout>("/workouts");
        if (response.ok) {
            setWorkouts(response.data!)
        } else {
            setError(response.message!);
        }
    }

    useEffect(() => {
        loadData(id)
        loadWorkouts()
    }, [id])

    const addWorkout = async () => {
        if (!workoutInSplit.workoutId) {
            setError("To add a workout please select one first")
            setMessage('')
            return;
        }
        setError('')
        workoutInSplit.splitId = model.id;
        const response = await BaseService.create(workoutInSplit, "/workoutinsplit", appState.token!)
        if (response.ok) {
            setMessage('Workout added!')
            const addedWorkout = workouts.find(x => x.id.toString() === workoutInSplit.workoutId)
            setModel({ ...model, workoutsInSplit: [...model.workoutsInSplit, addedWorkout!] })
        } else {
            setError(response.message!)
        }
    }

    const removeWorkout = async (workoutId: number, index: number) => {
        const response = await BaseService.delete("/workoutinsplit/" + workoutId + "/" + model.id, appState.token!)
        if (response.ok) {
            setMessage('Workout removed!')
            document.getElementById(index.toString())!.remove()
        } else {
            setError(response.message!)
        }
    }

    return (
        <>
            <h1>Edit</h1>

            <h4>{model.name}</h4>

            <div className='mt-3'>
                <h5><Link to="/splits">Back to List</Link></h5>
            </div>

            <hr />
            <div className="row">
                <div className="col-md-4">
                    <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />
                    <Alert show={message !== ''} message={message} alertClass={EAlertClass.Success} />
                    <div className="form-group">
                        <label className="control-label" htmlFor="Name">Name</label>
                        <input className="form-control" type="text" name="Name" id='Name' value={model.name} onChange={e => setModel({ ...model, name: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="Description">Description</label>
                        <textarea className="form-control" id="Description" name="Description"
                            value={model.description} onChange={e => setModel({ ...model, description: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <input type='submit' onClick={saveClicked} value="Save" className="btn btn-primary" />
                    </div>
                </div>
            </div>

            <div className="form-group justify-content-between  mb-3">
                <select className="form-control" onChange={e => setWorkoutInSplti({ ...workoutInSplit, workoutId: e.target.value })}>
                    <option value="">Select workout...</option>
                    {workouts.map(workout => <option key={workout.id} value={workout.id} >{workout.name} - {workout.description}</option>)}

                </select>
                <button onClick={addWorkout} className='align-self-center btn btn-info mt-3 mb-3'>Add workout to this split</button>
            </div>

            {model.workoutsInSplit.length !== 0
                ?
                <h3 >Workouts in this split:</h3>
                :
                <h3>No workouts added yet</h3>}

            <div className="list-group">
                {model.workoutsInSplit.map((workout, index) =>
                    <li id={index.toString()} key={index} className="list-group-item flex-column align-items-start">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{workout.name}</h5>
                        </div>
                        <p className="mb-1">{workout.description}</p>
                        <button onClick={() => removeWorkout(workout.id, index)} className='btn btn-info'>Remove workout</button>
                    </li>
                )}
            </div>

        </>
    )
}

export default SplitsEdit
