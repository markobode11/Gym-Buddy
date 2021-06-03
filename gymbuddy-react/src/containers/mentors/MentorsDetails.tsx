import React, { useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom"
import Alert, { EAlertClass } from "../../components/Alert";
import { IMentor, InitialMentor } from "../../domain/IMentor";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const MentorsDetails = () => {
    const { id } = useParams() as IRouteId;
    const [errorMessage, setErrorMessage] = useState('');
    const [mentor, setMentor] = useState(InitialMentor);

    const loadData = async (id: string) => {
        const response = await BaseService.get<IMentor>("/mentors/" + id);
        if (response.ok) {
            setMentor(response.data!)
        } else {
            setErrorMessage(response.message!);
        }
    }

    useEffect(() => {
        loadData(id);
    }, [id])
    
    return (
        <div className="row">
        <div className="col-sm-10 col-md-6">
            <h3>Mentor { mentor.fullName }</h3>
            <div>
                <h5>{mentor.description}</h5>
                <hr />
                <dl className="row">
                    <dt className="col-sm-5">Specialty</dt>
                    <dd className="col-sm-7">{mentor.specialty}</dd>
                </dl>
                <dl className="row">
                    <dt className="col-sm-5">Mentor since</dt>
                    <dd className="col-sm-7">{new Date(mentor.since).toDateString()}</dd>
                </dl>
            </div>
            <div className="mt-5">
                <Link to="/mentors">Back to List</Link>
            </div>
            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />
        </div>
    </div>
    )
}

export default MentorsDetails
