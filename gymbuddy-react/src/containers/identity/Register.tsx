import React, { useContext, useState } from "react";
import { Redirect } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IdentityService } from "../../services/identity-service";


const Register = () => {
    const appState = useContext(AppContext);

    const [registerData, setRegsterData] = useState(
        {
            email: '',
            password: '',
            firstname: '',
            lastname: ''
        });
    const [error, setError] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');

    const registerClicked = async () => {
        if (registerData.email === '' ||
            registerData.password === '' ||
            registerData.firstname === '' ||
            registerData.lastname === '' ||
            confirmPassword === '') {
            setError('All fields are required!');
            return
        };
        if (registerData.password !== confirmPassword) {
            setError('Password and confirm password do not match!');
            return
        }
        const response = await IdentityService.Login('/account/register', registerData)
        if (response.ok) {
            appState.setAuthInfo(response.data!.token, response.data!.firstname, response.data!.lastname);
        } else {
            setError(response.message!)
        }
    }

    return (
        <>
            { appState.token !== null ? <Redirect to="/" /> : null}
            <div className="row">
                <div className="col-sm-10 col-md-6">
                    <h4>Create new account.</h4>
                    <hr />
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input className="form-control" type="email" id="email" onChange={e => setRegsterData({ ...registerData, email: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="register-firstname">Firstname</label>
                        <input className="form-control" id="register-firstname" onChange={e => setRegsterData({ ...registerData, firstname: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="register-lastname">Lastname</label>
                        <input className="form-control" id="register-lastname" onChange={e => setRegsterData({ ...registerData, lastname: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input className="form-control" type="password" id="password" onChange={e => setRegsterData({ ...registerData, password: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="confirm-password">Confirm Password</label>
                        <input className="form-control" type="password" id="confirm-password" onChange={e => setConfirmPassword(e.target.value)} />
                    </div>
                    <div className="form-group">
                        <button onClick={registerClicked} type="submit" className="btn btn-dark">Register</button>
                    </div>

                    <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />

                </div>
                <div className="col-sm-1 col-md-3"></div>
            </div>
        </>
    )
}

export default Register
