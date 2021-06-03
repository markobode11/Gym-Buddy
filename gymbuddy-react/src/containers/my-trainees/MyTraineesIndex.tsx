import React, { useContext, useEffect, useState } from "react"
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IUser } from "../../domain/IUser";
import { BaseService } from "../../services/base-service";

const MyTraineesIndex = () => {
    const appState = useContext(AppContext);
    const [trainees, setTrainees] = useState([] as IUser[])
    const [errorMessage, setErrorMessage] = useState('');

    useEffect(() => {
        const loadUsers = async () => {
            const response = await BaseService.getAll<IUser>('/usermentor/trainees', appState.token!);
            if (response.ok) {
                setTrainees(response.data!)
            } else {
                setErrorMessage(response.message!)
            }
        }
        loadUsers();
    }, [appState.token]);



    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">My trainees</h1>
                        <p className="lead text-light">View and contact trainees.</p>
                    </div>
                </div>
            </section>

            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />

            {trainees.length === 0 ? <div className='alert alert-info'>No trainees yet!</div>
                :
                <div className='container list-group'>
                    {trainees.map(trainee =>
                        <div className='alert alert-info row align-items-start' key={trainee.id}>
                            <div className='col'>{trainee.firstname} {trainee.lastname}</div>
                            <div className='col'>{trainee.gender === 0 ? 'Male' : 'Female'}</div>
                            <div className='col'>{trainee.email}</div>
                        </div>)}
                </div>
            }
        </>
    )
}

export default MyTraineesIndex
