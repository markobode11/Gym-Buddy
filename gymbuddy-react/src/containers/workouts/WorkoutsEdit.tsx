import React, { useState, useContext, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IDifficulty } from "../../domain/IDifficulty";
import { IExercise } from "../../domain/IExercise";
import { IExerciseInWorkout } from "../../domain/IExerciseInWorkout";
import { InitialWorkout, IWorkout } from "../../domain/IWorkout";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const WorkoutsEdit = () => {
    const { id } = useParams() as IRouteId;

    const [model, setModel] = useState(InitialWorkout);
    const [difficulties, setDifficulties] = useState([] as IDifficulty[])
    const [exercises, setExercises] = useState([] as IExercise[])
    const [error, setError] = useState('')
    const [message, setMessage] = useState('')
    const appState = useContext(AppContext)
    const [exerciseInWorkout, setExerciseInWorkout] = useState({} as IExerciseInWorkout)

    const loadDifficulties = async () => {
        const response = await BaseService.getAll<IDifficulty>("/difficulties");
        if (response.ok) {
            setDifficulties(response.data!)
        } else {
            setError(response.message!)
        }
    }

    const saveClicked = async () => {
        if (!model.name || !model.description || !model.difficultyId || !model.duration) {
            setError('All fields are required!')
            setMessage('')
            return
        }
        setError('')
        model.difficulty = null
        const response = await BaseService.edit<IWorkout>(model, "/workouts/" + id, appState.token!);
        if (response.ok) {
            setMessage("Saved!")
        } else {
            setError(response.message!);
        }
    }

    const loadData = async (id: string) => {
        const response = await BaseService.get<IWorkout>("/workouts/" + id);
        if (response.ok) {
            setModel(response.data!)
        } else {
            setError(response.message!);
        }
    }

    const loadExercises = async () => {
        const response = await BaseService.getAll<IExercise>("/exercises");
        if (response.ok) {
            setExercises(response.data!)
        } else {
            setError(response.message!);
        }
    }

    useEffect(() => {
        loadDifficulties()
        loadData(id)
        loadExercises()
    }, [id])

    const addExercise = async () => {
        if (!exerciseInWorkout.exerciseId) {
            setError("To add an exercise please select one!")
            setMessage('')
            return;
        }
        setError('')
        exerciseInWorkout.workoutId = model.id;
        const response = await BaseService.create(exerciseInWorkout, "/exerciseInWorkout", appState.token!)
        if (response.ok) {
            setMessage('Exercise added!')
            const addedExercise = exercises.find(x => x.id.toString() === exerciseInWorkout.exerciseId)
            setModel({ ...model, exercisesInWorkout: [...model.exercisesInWorkout, addedExercise!] })
        } else {
            setError(response.message!)
        }
    }

    const removeExercise = async (exerciseId: number, index: number) => {
        const response = await BaseService.delete("/exerciseInWorkout/" + model.id + "/" + exerciseId, appState.token!)
        if (response.ok) {
            setMessage('Exercise removed!')
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
                <h5><Link to="/workouts">Back to List</Link></h5>
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
                        <label className="control-label" htmlFor="duration">Duration in minutes</label>
                        <input className="form-control" type="text" id="duration" name="duration"
                            value={model.duration} onChange={e => setModel({ ...model, duration: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label className="control-label">Difficulty</label>
                        <select className="form-control" onChange={e => setModel({ ...model, difficultyId: parseInt(e.target.value) })} value={model.difficultyId}>
                            {difficulties.map(diff =>
                                <option value={diff.id} key={diff.id}>{diff.name}</option>)}
                        </select>
                    </div>
                    <div className="form-group">
                        <input type='submit' onClick={saveClicked} value="Save" className="btn btn-primary" />
                    </div>
                </div>
            </div>

            <div className="form-group justify-content-between  mb-3">
                <select className="form-control" onChange={e => setExerciseInWorkout({ ...exerciseInWorkout, exerciseId: e.target.value })}>
                    <option value="">Select exercise...</option>
                    {exercises.map(exercise => <option key={exercise.id} value={exercise.id} >{exercise.name} - {exercise.description}</option>)}

                </select>
                <button onClick={addExercise} className='align-self-center btn btn-info mt-3 mb-3'>Add exercise to this workout</button>
            </div>

            {model.exercisesInWorkout.length !== 0
                ?
                <h3 >Exercises in this workout:</h3>
                :
                <h3>No exercises added yet</h3>}

            <div className="list-group">
                {model.exercisesInWorkout.map((exercise, index) =>
                    <li id={index.toString()} key={index} className="list-group-item flex-column align-items-start">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{exercise.name}</h5>
                        </div>
                        <p className="mb-1">{exercise.description}</p>
                        <button onClick={() => removeExercise(exercise.id, index)} className='btn btn-info'>Remove exercise</button>
                    </li>
                )}
            </div>

        </>
    )
}

export default WorkoutsEdit
