import React, { useContext, useEffect, useState } from "react"
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IUpdateUser } from "../../domain/IUser";
import { BaseService } from "../../services/base-service";
import { IdentityService } from "../../services/identity-service";

const Account = () => {
    const [user, setUser] = useState({} as IUpdateUser);
    const [error, setError] = useState('');
    const [message, setMessage] = useState('');
    const [confirm, setConfirm] = useState('');
    const appState = useContext(AppContext);

    useEffect(() => {
        const loadUser = async () => {
            const response = await BaseService.get<IUpdateUser>('/account/getuser', appState.token!);
            if (response.ok) {
                setUser(response.data!)
            } else {
                setError(response.message!)
            }
        }

        loadUser();
    }, [appState.token]);

    const saveClicked = async () => {
        if (!user.email || !user.firstname || !user.lastname) {
            setError('Email, firstname and lastname are required!')
            return
        }
        if (user.newPassWord && user.newPassWord !== confirm) {
            setError('New password and confirm password must be the same!');
            return
        }
        if((!user.newPassWord && user.oldPassword) || (user.newPassWord && !user.oldPassword)) {
            setError('To change password all password fields are required!')
            return
        }
        setError('')
        const response = await IdentityService.Update('/account/changeuserinfo', user, appState.token!);
        if (response.ok) {
            setMessage("Saved");
            setError('')
        } else {
            setError(response.message!)
            setMessage('')
        }
    }

    return (
        <>
            <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />
            <Alert show={message !== ''} message={message} alertClass={EAlertClass.Success} />

            <div className="row">
                <div className="col-sm-10 col-md-6">
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input value={user.email ? user.email : ''} className="form-control" type="email" id="email" onChange={e => setUser({ ...user, email: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="firstname">Firstname</label>
                        <input value={user.firstname ? user.firstname : ''} className="form-control" id="firstname" onChange={e => setUser({ ...user, firstname: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="lastname">Lastname</label>
                        <input value={user.lastname ? user.lastname : ''} className="form-control" id="lastname" onChange={e => setUser({ ...user, lastname: e.target.value })} />
                    </div>
                    <hr />
                    <h5>Change password</h5>
                    <div className="form-group">
                        <label htmlFor="password">Old Password</label>
                        <input className="form-control" type="password" id="password" onChange={e => setUser({ ...user, oldPassword: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="new">New Password</label>
                        <input className="form-control" type="password" id="new" onChange={e => setUser({ ...user, newPassWord: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label htmlFor="confirm">Confirm New Password</label>
                        <input className="form-control" type="password" id="confirm" onChange={e => setConfirm(e.target.value)} />
                    </div>
                    <div className="form-group">
                        <button onClick={saveClicked} type="submit" className="btn btn-dark">Save info</button>
                    </div>
                </div>
            </div>
        </>
    )
}

export default Account
