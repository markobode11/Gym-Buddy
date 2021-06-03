import React, { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IDifficulty } from "../../domain/IDifficulty";
import { InitialExercise, IExercise } from "../../domain/IExercise";
import { IMuscle } from "../../domain/IMuscle";
import { IMuscleInExercise } from "../../domain/IMuscleInExercise";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const ExercisesEdit = () => {
    const { id } = useParams() as IRouteId;

    const [model, setModel] = useState(InitialExercise);
    const [difficulties, setDifficulties] = useState([] as IDifficulty[])
    const [muscles, setMuscles] = useState([] as IMuscle[])
    const [error, setError] = useState('')
    const [message, setMessage] = useState('')
    const appState = useContext(AppContext)
    const [muscleInExercise, setMuscleInExercise] = useState({} as IMuscleInExercise)

    const loadDifficulties = async () => {
        const response = await BaseService.getAll<IDifficulty>("/difficulties");
        if (response.ok) {
            setDifficulties(response.data!)
        } else {
            setError(response.message!)
        }
    }

    const saveClicked = async () => {
        if (!model.name || !model.description || !model.difficultyId) {
            setError('All fields are required!')
            setMessage('')
            return
        }
        setError('')
        model.difficulty = null
        const response = await BaseService.edit<IExercise>(model, "/exercises/" + id, appState.token!);
        if (response.ok) {
            setMessage("Saved!")
        } else {
            setError(response.message!);
        }
    }

    const loadData = async (id: string) => {
        const response = await BaseService.get<IExercise>("/exercises/" + id);
        if (response.ok) {
            setModel(response.data!)
        } else {
            setError(response.message!);
        }
    }

    const loadMuscles = async () => {
        const response = await BaseService.getAll<IMuscle>("/muscles");
        if (response.ok) {
            setMuscles(response.data!)
        } else {
            setError(response.message!);
        }
    }

    useEffect(() => {
        loadDifficulties()
        loadData(id)
        loadMuscles()
    }, [id])

    const addMuscle = async () => {
        if (!muscleInExercise.relevance || !muscleInExercise.muscleId) {
            setError("To add a muscle both muscle and relevance are required!")
            setMessage('')
            return;
        }
        setError('')
        muscleInExercise.exerciseId = model.id;
        const response = await BaseService.create(muscleInExercise, "/muscleinexercise", appState.token!)
        if (response.ok) {
            setMessage('Muscle added!')
            const addedMuscle = muscles.find(x => x.id.toString() === muscleInExercise.muscleId)
            addedMuscle!.relevance = muscleInExercise.relevance
            setModel({...model, musclesTrainedInExercise: [...model.musclesTrainedInExercise, addedMuscle!]})
        } else {
            setError(response.message!)
        }
    }

    const removeMuscle = async (muscleId: number, index: number) => {
        const response = await BaseService.delete("/muscleinexercise/" + muscleId + "/" + model.id, appState.token!)
        if (response.ok) {
            setMessage('Muscle removed!')
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
                <h5><Link to="/exercises">Back to List</Link></h5>
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
                <select className="form-control" onChange={e => setMuscleInExercise({ ...muscleInExercise, muscleId: e.target.value })}>
                    <option value="">Select muscle...</option>
                    {muscles.map(muscle => <option key={muscle.id} value={muscle.id} >{muscle.everydayName} ({muscle.medicalName})</option>)}

                </select>
                <label className="control-label mt-3">Muscle relevance</label>
                <input className="form-control" type="text" id="relevance" name="relevance" onChange={e => setMuscleInExercise({...muscleInExercise, relevance: e.target.value})} />
                <button onClick={addMuscle} className='align-self-center btn btn-info mt-3 mb-3'>Add trained muscle</button>
            </div>

            {model.musclesTrainedInExercise.length !== 0
                ?
                <h3 >This exercise trains:</h3>
                :
                <h3>No trained muscles added yet</h3>}

            <div className="list-group">
                {model.musclesTrainedInExercise.map((muscle, index) =>
                    <li id={index.toString()} key={index} className="list-group-item flex-column align-items-start">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{muscle.everydayName}</h5>
                            <small>{muscle.relevance}</small>
                        </div>
                        <p className="mb-1">{muscle.medicalName}</p>
                        <button onClick={() => removeMuscle(muscle.id, index)} className='btn btn-info'>Remove muscle</button>
                    </li>
                )}
            </div>

        </>
    )
}

export default ExercisesEdit