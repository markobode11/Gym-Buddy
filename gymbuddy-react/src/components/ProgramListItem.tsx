import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { AppContext } from "../context/AppContext";
import { IProgram } from "../domain/IProgram";

const ProgramListItem = (props:
    {
        program: IProgram,
        isInUserPrograms: boolean
        onDelete: (id: number) => void,
        onConfirmDelete: (id: number) => void,
        addToMyPrograms: (id: number) => void
    }) => {
    const appState = useContext(AppContext);

    return (
        <>
            <div className="col mb-3 flex-fill">
                <div className="card shadow-sm">
                    <div className="card-body workout-item">
                        <p className="card-text"><b>{props.program.name}</b></p>
                        <p className="card-text">{props.program.goal}</p>
                        <div className="d-flex justify-content-between align-items-center">
                            <div className="btn-group">
                                <Link className="btn btn-sm btn-outline-secondary"
                                    to={'/programs/' + props.program.id}><i className="fas fa-eye"></i> View</Link>
                                {appState.isAdmin ?
                                    <Link className="btn btn-sm btn-outline-secondary" to={'/programs/edit/' + props.program.id}>Edit</Link>
                                    : null}
                            </div>
                        </div>
                    </div>
                    {appState.token && !props.isInUserPrograms && !appState.isAdmin && !appState.isMentor ? <button className='btn btn-secondary' onClick={() => props.addToMyPrograms(props.program.id)}><i className="fas fa-plus"></i> Add to my programs</button> : null}
                    {appState.token && props.isInUserPrograms && !appState.isAdmin ? <span className='list-item text-center bg-info'><i className="fas fa-check-circle"></i> Already in My Programs</span>: null}
                    {appState.isAdmin ?
                        <>
                            <button onClick={() => props.onConfirmDelete(props.program.id)} id={props.program.id + '-delete'} className="btn btn-delete"><i className="fas fa-trash-alt"></i> Delete</button>
                            <button onClick={() => props.onDelete(props.program.id)} id={props.program.id + '-confirm-delete'} style={{ display: 'none' }} className="btn btn-danger">
                            <i className="fas fa-check-circle"></i> Are you sure?
                            </button>
                        </>
                        : null}
                </div>
            </div>
        </>
    )
}

export default ProgramListItem
