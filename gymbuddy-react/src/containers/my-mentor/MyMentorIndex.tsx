import React, { useContext, useEffect, useState } from "react"
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { IMentor } from "../../domain/IMentor"
import { BaseService } from "../../services/base-service"

const MyMentorIndex = () => {
    const appState = useContext(AppContext);
    const [mentor, setMentor] = useState(undefined as IMentor | null | undefined)
    const [errorMessage, setErrorMessage] = useState('')
    const [showDelete, setShowDelete] = useState(false)

    useEffect(() => {
        const loadMentor = async () => {
            const response = await BaseService.get<IMentor>('/usermentor/mentor', appState.token!)
            if (response.ok) {
                setMentor(response.data!)
            }
            else if (response.statusCode !== 404) {
                setErrorMessage(response.message!)
            } else {
                setMentor(null)
            }
        }

        loadMentor()
    }, [appState.token])

    const removeClicked = () => {
        setShowDelete(true);
    }

    const deleteClicked = async () => {
        const response = await BaseService.delete('/usermentor', appState.token!)
        if (response.ok) {
            setMentor(null)
        } else {
            setErrorMessage(response.message!)
        }
        setShowDelete(false)
    }

    return mentor === undefined ? <></> : (
        <>
            { mentor !== null ?
                <div className="row">

                    <div className="col-sm-10 col-md-10">
                        <h3><b>My Mentor</b></h3>
                        <hr />

                        <h4>{mentor.fullName}</h4>
                        <div>
                            <h5>{mentor.description}</h5>
                            <hr />
                            <dl className="row">
                                <dt className="col-sm-5">Specialty</dt>
                                <dd className="col-sm-7">{mentor.specialty}</dd>
                            </dl>
                            <dl className="row">
                                <dt className="col-sm-5">Mentor with GymBuddy since</dt>
                                <dd className="col-sm-7">{new Date(mentor.since).toDateString()}</dd>
                            </dl>
                        </div>

                        <div className='alert alert-secondary'>Contact mentor by email: <b>{mentor.email}</b></div>

                        {!showDelete ?
                            <button onClick={removeClicked} className='btn btn-warning float-right'><i className="fas fa-user-minus"></i> Remove mentor</button>
                            :
                            <button onClick={deleteClicked} className='btn btn-danger float-right'>Are you sure?</button>
                        }
                    </div>

                    <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />
                </div >
                : <>
                    <div className='alert alert-info text-center'>You have not chosen a mentor yet!</div>
                    <div className='d-flex justify-content-center'>
                        <Link to='mentors' className='btn btn-info'>Find yourself a mentor!</Link>
                    </div>
                </>
            }
        </>
    )
}

export default MyMentorIndex
