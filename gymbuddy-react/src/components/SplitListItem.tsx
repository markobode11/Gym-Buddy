import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { AppContext } from "../context/AppContext";
import { ISplit } from "../domain/ISplit";

const SplitListItem = (props: { split: ISplit, onDelete: (id: number) => void, onConfirmDelete: (id: number) => void }) => {
    const appState = useContext(AppContext);

    return (
        <>
            <div className="col mb-3 flex-fill">
                <div className="card shadow-sm">
                    <div className="card-body workout-item">
                        <p className="card-text"><b>{props.split.name}</b></p>
                        <p className="card-text">{props.split.description}</p>
                        <div className="d-flex justify-content-between align-items-center">
                            <div className="btn-group">
                                <Link className="btn btn-sm btn-outline-secondary"
                                    to={'/splits/' + props.split.id}><i className="fas fa-eye"></i> View</Link>
                                {appState.isAdmin ?
                                    <Link className="btn btn-sm btn-outline-secondary" to={'/splits/edit/' + props.split.id}>Edit</Link>
                                    : null}
                            </div>
                        </div>
                    </div>
                    {appState.isAdmin ?
                        <>
                            <button onClick={() => props.onConfirmDelete(props.split.id)} id={props.split.id + '-delete'} className="btn btn-sm btn-delete"><i className="fas fa-trash-alt"></i> Delete</button>
                            <button onClick={() => props.onDelete(props.split.id)} id={props.split.id + '-confirm-delete'} style={{ display: 'none' }} className="btn btn-sm btn-danger">
                            <i className="fas fa-check-circle"></i> Are you sure?
                            </button>
                        </>
                        : null}
                </div>
            </div>
        </>
    )
}

export default SplitListItem
