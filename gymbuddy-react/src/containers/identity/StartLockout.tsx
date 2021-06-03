import React, { useContext, useEffect, useState } from "react"
import { useHistory, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert"
import { AppContext } from "../../context/AppContext";
import { IUserWithRole } from "../../domain/IUser";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const StartLockout = () => {
    const [error, setError] = useState('');
    const [end, setEnd] = useState(new Date());
    const [user, setUser] = useState({} as IUserWithRole)
    const { id } = useParams() as IRouteId;
    const appState = useContext(AppContext);
    const history = useHistory();

    useEffect(() => {
        const loadUser = async () => {
            const response = await BaseService.get<IUserWithRole>('/account/getuserbyid/' + id, appState.token!);
            if (response.ok) {
                setUser(response.data!)
            } else {
                setError(response.message!)
            }
        }

        loadUser();
    }, [appState.token, id])

    const startClicked = async () => {
        const response = await BaseService.create({ id: user.id, lockDownEnd: end }, '/account/startlockdown', appState.token!);
        if (response.ok) {
            history.push('/usermanagement')
        } else {
            setError(response.message!)
        }
    }

    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">Start Lockdown</h1>
                    </div>
                </div>
            </section>

            <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />

            <div className="row">
                <div className="col-md-6">
                    <div className="form-group">
                        <div className='col alert alert-secondary'><b>User:</b> {user.email}</div>
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="end">Lockdown end</label>
                        <input className="form-control" type="date" id="end" name="end"
                            onChange={e => setEnd(new Date(e.target.value))} />
                    </div>
                    <div onClick={startClicked} className='col form-control btn btn-primary'>Start Lockdown</div>
                </div>
            </div>
        </>
    )
}

export default StartLockout
