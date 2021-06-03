import React, { useContext, useEffect, useState } from "react"
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IUserWithRoleAndLockOut } from "../../domain/IUser";
import { BaseService } from "../../services/base-service";
import { IdentityService } from "../../services/identity-service";

const UserManagement = () => {
    const [users, setUsers] = useState([] as IUserWithRoleAndLockOut[]);
    const [error, setError] = useState('');
    const appState = useContext(AppContext);

    useEffect(() => {
        const loadUsers = async () => {
            const response = await BaseService.getAll<IUserWithRoleAndLockOut>('/account/getallusers', appState.token!);
            if (response.ok) {
                setUsers(response.data!)
            } else {
                setError(response.message!)
            }
        }

        loadUsers();
    }, [appState.token]);

    const endLockout = async (userId: number) => {
        const response = await IdentityService.RemoveRole('/account/endlockdown/' + userId, appState.token!)
        if (response.ok) {
            var changedUser = users.find(x => x.id === userId);
            var index = users.indexOf(changedUser!);
            users[index].lockoutEndDateTime = null
            users[index].isLockedOut = false
            setUsers([...users])
        } else {
            setError(response.message!)
        }
    }

    const removeRole = async (userId: number) => {
        const response = await IdentityService.RemoveRole('/account/removerole/' + userId, appState.token!)
        if (response.ok) {
            var changedUser = users.find(x => x.id === userId);
            var index = users.indexOf(changedUser!);
            users[index].roleName = null
            setUsers([...users])
        } else {
            setError(response.message!)
        }
    }

    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">User management</h1>
                        <p className="lead text-light">Ban and unban users, add and remove users from roles.</p>
                    </div>
                </div>
            </section>

            <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />

            <table className="table">
                <thead>
                    <tr>
                        <th>Email</th>
                        <th>Full name</th>
                        <th>Role</th>
                        <th></th>
                        <th>Locked out</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {users.map(user =>
                        <tr key={user.id}>
                            <td>{user.email}</td>
                            <td>{user.firstname} {user.lastname}</td>
                            <td>{user.roleName ? user.roleName : ''}</td>
                            <td>
                                {user.roleName ?
                                    <button onClick={() => removeRole(user.id)} className='btn btn-danger'>Remove from role</button>
                                    :
                                    <Link to={'/addrole/' + user.id} className='btn btn-info'>Add to role</Link>}

                            </td>
                            <td>{user.isLockedOut ? 'Until ' + new Date(user.lockoutEndDateTime!).toDateString() : 'No'}</td>
                            <td>
                                {user.isLockedOut ?
                                    <button onClick={() => endLockout(user.id)} className='btn btn-secondary'>End lockout</button>
                                    :
                                    <Link to={'/startlockout/' + user.id} className='btn btn-warning'>Start lockout</Link>
                                }
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </>
    )
}

export default UserManagement
