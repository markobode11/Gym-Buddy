import React, { useState, useContext, useEffect } from "react";
import { Link, useHistory } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IDifficulty } from "../../domain/IDifficulty";
import { InitialWorkout, IWorkout } from "../../domain/IWorkout";
import { BaseService } from "../../services/base-service";

const WorkoutsCreate = () => {
    const [model, setModel] = useState(InitialWorkout);
    const [difficulties, setDifficulties] = useState([] as IDifficulty[])
    const [error, setError] = useState('')
    const history = useHistory();
    const appState = useContext(AppContext)

    const loadDifficulties = async () => {
        const response = await BaseService.getAll<IDifficulty>("/difficulties");
        if (response.ok) {
            setDifficulties(response.data!)
        } else {
            setError(response.message!)
        }
    }

    const createClicked = async () => {
        if (!model.name || !model.description || !model.difficultyId || !model.duration) {
            setError('All fields are required!')
            return
        }
        const response = await BaseService.create<IWorkout>(model, "/workouts", appState.token!);
        if (response.ok) {
            history.push('/workouts/edit/' + response.data?.id)
        } else {
            setError(response.message!);
        }
    }

    useEffect(() => {
        loadDifficulties()
    }, [])

    return (
        <>
            <h1>Create</h1>

            <h4>Workout</h4>
            <hr />
            <div className="row">
                <div className="col-md-4">
                    <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />
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
                        <select className="form-control" onChange={e => setModel({ ...model, difficultyId: parseInt(e.target.value) })}>
                            <option>--- Please select ---</option>
                            {difficulties.map(diff => <option value={diff.id} key={diff.id}>{diff.name}</option>)}
                        </select>
                    </div>
                    <div className="form-group">
                        <input type='submit' onClick={createClicked} value="Create" className="btn btn-primary" />
                    </div>
                </div>
            </div>

            <div>
                <Link to="/workouts">Back to List</Link>
            </div>
        </>
    )
}

export default WorkoutsCreate
