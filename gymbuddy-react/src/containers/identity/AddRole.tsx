import React, { useContext, useState, useEffect } from "react";
import { useHistory, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IRole } from "../../domain/IRole";
import { IUserWithRole } from "../../domain/IUser";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const AddRole = () => {
    const appState = useContext(AppContext);
    const [user, setUser] = useState({} as IUserWithRole);
    const [roles, setRoles] = useState([] as IRole[]);
    const [error, setError] = useState('');
    const { id } = useParams() as IRouteId;
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

        const loadRoles = async () => {
            const response = await BaseService.getAll<IRole>('/account/getroles', appState.token!);
            if (response.ok) {
                setRoles(response.data!)
            } else {
                setError(response.message!)
            }
        }

        loadUser();
        loadRoles();
    }, [appState.token, id]);

    const addClicked = async () => {
        if (!user.roleName) {
            setError('Select role!');
            return
        }
        const response = await BaseService.create(user, '/account/addrole', appState.token!);
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
                        <h1 className="fw-light">Add user to role</h1>
                    </div>
                </div>
            </section>

            <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />

            <div className='row col-10 text-center'>
                <div className='col alert alert-secondary m-3'>{user.email}</div>
                <select className='col m-3 form-control' onChange={(e) => setUser({ ...user, roleName: e.target.value })}>
                    <option>Please select...</option>
                    {roles.map(role => <option key={role.id} value={role.name}>{role.name}</option>)}
                </select>
                <div onClick={addClicked} className='col m-3 form-control btn btn-primary'>Add</div>
            </div>
        </>
    )
}

export default AddRole
