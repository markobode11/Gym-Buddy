import React, { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { InitialProgram, IProgram } from "../../domain/IProgram";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const ProgramsDetails = () => {
    const { id } = useParams() as IRouteId;
    const [errorMessage, setErrorMessage] = useState('');
    const [program, setProgram] = useState(InitialProgram);

    const loadData = async (id: string) => {
        const response = await BaseService.get<IProgram>("/fullprograms/" + id);
        if (response.ok) {
            setProgram(response.data!)
        } else {
            setErrorMessage(response.message!);
        }
    }

    useEffect(() => {
        loadData(id);
    }, [id])

    return (
        <>
            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />

            <h1>{program.name}</h1>
            <hr />

            <h4 className='mt-2'>{program.description}</h4>
            <hr />

            <h4 className='mt-2'>Goal: {program.goal}</h4>
            <hr />

            {program.splitsInProgram.length !== 0
                ?
                <h3 className='mt-2'>Splits in this program</h3>
                :
                <h3 className='mt-2'>No splits added yet</h3>}

            <div className="list-group">
                {program.splitsInProgram.map((split, index) =>
                    <Link to={'/splits/' + split.id} key={index} className="list-group-item list-group-item-action flex-column align-items-start hover-shadow">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{split.name}</h5>
                        </div>
                        <p className="mb-1">{split.description}</p>
                    </Link>
                )}
            </div>

            <div className="mt-5">
                | <Link to="/programs">Back to List</Link>
            </div>
        </>
    )
}

export default ProgramsDetails
