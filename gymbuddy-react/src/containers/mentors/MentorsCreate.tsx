import React, { useState, useContext } from "react";
import { Link, useHistory } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { ICreateMentor, IMentor } from "../../domain/IMentor";
import { BaseService } from "../../services/base-service";

const MentorsCreate = () => {
    const [model, setModel] = useState({} as ICreateMentor);
    const [error, setError] = useState('')
    const history = useHistory();
    const appState = useContext(AppContext)

    const createClicked = async () => {
        if (!model.lastname ||
            !model.description ||
            !model.specialty ||
            !model.firstname ||
            !model.email ||
            !model.password
        ) {
            setError('All fields are required!')
            return
        }
        model.fullName = model.firstname + " " + model.lastname
        const response = await BaseService.create<IMentor, ICreateMentor>(model, "/account/registermentor", appState.token!);
        if (response.ok) {
            history.push('/mentors')
        } else {
            setError(response.message!);
        }
    }

    return (
        <>
            <h1>Create</h1>

            <h4>Mentor and user account for the mentor</h4>
            <hr />

            <h5>Mentor details</h5>
            <div className="row">
                <div className="col-md-6">
                    <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />
                    <div className="form-group">
                        <label className="control-label" htmlFor="Description">Description</label>
                        <textarea className="form-control" id="Description" name="Description"
                            onChange={e => setModel({ ...model, description: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="Specialty">Specialty</label>
                        <input className="form-control" type="text" id="Specialty" name="Specialty"
                            onChange={e => setModel({ ...model, specialty: e.target.value })} />
                    </div>
                </div>
            </div>

            <h5>Account details</h5>

            <div className="row">
                <div className="col-md-6">
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input className="form-control" type="email" id="email" onChange={e => setModel({ ...model, email: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="register-firstname">Firstname</label>
                        <input className="form-control" id="register-firstname" onChange={e => setModel({ ...model, firstname: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="register-lastname">Lastname</label>
                        <input className="form-control" id="register-lastname" onChange={e => setModel({ ...model, lastname: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input className="form-control" type="password" id="password" onChange={e => setModel({ ...model, password: e.target.value })} />
                    </div>
                </div>
            </div>

            <div className="form-group">
                <input type='submit' onClick={createClicked} value="Create" className="btn btn-primary" />
            </div>

            <div>
                <Link to="/mentors">Back to List</Link>
            </div>
        </>
    )
}

export default MentorsCreate
