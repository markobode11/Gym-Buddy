import { useContext, useState } from "react";
import { Redirect } from "react-router-dom";
import { AppContext } from "../../context/AppContext";
import Alert, { EAlertClass } from "../../components/Alert";
import { IdentityService } from "../../services/identity-service";

const Login = () => {
    const appState = useContext(AppContext);

    const [loginData, setLoginData] = useState({ email: '', password: '' });
    const [error, setError] = useState('');

    const loginClicked = async () => {
        if (loginData.email === '' || loginData.password === '') {
            setError('Empty email or password!');
            return
        };
        const response = await IdentityService.Login('/account/login', loginData)
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
                <div className="col-sm-1 col-md-3"></div>
                <div className="col-sm-10 col-md-6">
                    <h4>Use your account to log in.</h4>
                    <hr />
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input className="form-control" type="email" id="email" onChange={e => setLoginData({ ...loginData, email: e.target.value })}/>
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input className="form-control" type="password" id="password" onChange={e => setLoginData({ ...loginData, password: e.target.value })}/>
                    </div>
                    <div className="form-group">
                        <button onClick={loginClicked} type="submit" className="btn btn-dark">Log in</button>
                    </div>
                    <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />
                </div>
                <div className="col-sm-1 col-md-3"></div>
            </div>
        </>
    )
}

export default Login
