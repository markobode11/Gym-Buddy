import React, { useContext, useEffect, useState } from "react"
import { Link, useParams } from "react-router-dom"
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IMentor, InitialMentor } from "../../domain/IMentor";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const MentorsEdit = () => {
    const { id } = useParams() as IRouteId;

    const [model, setModel] = useState(InitialMentor);
    const [error, setError] = useState('')
    const [message, setMessage] = useState('')
    const appState = useContext(AppContext)

    const saveClicked = async () => {
        if (!model.fullName || !model.description || !model.specialty) {
            setMessage('')
            setError('All fields are required!')
            return
        }
        setError('')
        const response = await BaseService.edit<IMentor>(model, "/mentors/" + id, appState.token!);
        if (response.ok) {
            setMessage("Saved!")
        } else {
            setError(response.message!);
        }
    }

    const loadData = async (id: string) => {
        const response = await BaseService.get<IMentor>("/mentors/" + id);
        if (response.ok) {
            setModel(response.data!)
        } else {
            setError(response.message!);
        }
    }

    useEffect(() => {
        loadData(id)
    }, [id])

    return (
        <>
            <h1>Edit</h1>

            <h4>{model.fullName}</h4>

            <div className='mt-3'>
                <h5><Link to="/mentors">Back to List</Link></h5>
            </div>

            <hr />
            <div className="row">
                <div className="col-md-4">
                    <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />
                    <Alert show={message !== ''} message={message} alertClass={EAlertClass.Success} />
                    <div className="form-group">
                        <label className="control-label" htmlFor="Name">Name</label>
                        <input className="form-control" type="text" name="Name" id='Name' value={model.fullName} onChange={e => setModel({ ...model, fullName: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="Description">Description</label>
                        <textarea className="form-control" id="Description" name="Description"
                            value={model.description} onChange={e => setModel({ ...model, description: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="Specialty">Specialty</label>
                        <input className="form-control" type="text" id="Specialty" name="Specialty"
                            value={model.specialty} onChange={e => setModel({ ...model, specialty: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <input type='submit' onClick={saveClicked} value="Save" className="btn btn-primary" />
                    </div>
                </div>
            </div>
        </>
    )
}

export default MentorsEdit
