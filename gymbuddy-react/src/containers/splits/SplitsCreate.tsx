import React, { useState, useContext } from "react";
import { Link, useHistory } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { InitialSplit, ISplit } from "../../domain/ISplit";
import { BaseService } from "../../services/base-service";

const SplitsCreate = () => {
    const [model, setModel] = useState(InitialSplit);
    const [error, setError] = useState('')
    const history = useHistory();
    const appState = useContext(AppContext)

    const createClicked = async () => {
        if (!model.name || !model.description) {
            setError('All fields are required!')
            return
        }
        const response = await BaseService.create<ISplit>(model, "/splits", appState.token!);
        if (response.ok) {
            history.push('/splits/edit/' + response.data?.id)
        } else {
            setError(response.message!);
        }
    }

    return (
        <>
            <h1>Create</h1>

            <h4>Split</h4>
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
                        <input type='submit' onClick={createClicked} value="Create" className="btn btn-primary" />
                    </div>
                </div>
            </div>

            <div>
                <Link to="/splits">Back to List</Link>
            </div>
        </>
    )
}

export default SplitsCreate
