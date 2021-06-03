import React, { useContext } from "react";
import { Link } from "react-router-dom";
import { AppContext } from "../context/AppContext";
import { IMentor } from "../domain/IMentor";

const MentorListItem = (props:
    {
        mentor: IMentor,
        userHasMentor: boolean
        onDelete: (id: number) => void,
        onConfirmDelete: (id: number) => void,
        chooseAsMentor: (id: number) => void
    }) => {
    const appState = useContext(AppContext);
    const muteClicked = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        (e.target as HTMLButtonElement).innerHTML = 'You already have a mentor!';
        (e.target as HTMLButtonElement).style.color = '#F00';
    }

    return (
        <>
            <div className="col mb-3 flex-fill">
                <div className="card shadow-sm">
                    <div className="card-body workout-item">
                        <p className="card-text"><b>{props.mentor.fullName}</b></p>
                        <p className="card-text"><b>Specialty:</b> {props.mentor.specialty}</p>
                        <div className="d-flex justify-content-between align-items-center">
                            <div className="btn-group">
                                <Link className="btn btn-sm btn-outline-secondary"
                                    to={'/mentors/' + props.mentor.id}><i className="fas fa-eye"></i> View</Link>
                                {appState.isAdmin ?
                                    <Link className="btn btn-sm btn-outline-secondary" to={'/mentors/edit/' + props.mentor.id}>Edit</Link>
                                    : null}
                            </div>
                        </div>
                    </div>
                    {appState.token && !props.userHasMentor && !appState.isAdmin && !appState.isMentor ? <button className='btn btn-secondary' onClick={() => props.chooseAsMentor(props.mentor.id)}><i className="fas fa-crosshairs"></i> Choose as mentor</button> : null}
                    {appState.token && props.userHasMentor && !appState.isAdmin && !appState.isMentor ? <button style={{cursor: 'help'}} className='btn btn-secondary disabled' onClick={(e) => muteClicked(e)}>Choose as mentor</button> : null}
                    {appState.isAdmin ?
                        <>
                            <button onClick={() => props.onConfirmDelete(props.mentor.id)} id={props.mentor.id + '-delete'} className="btn btn-delete"><i className="fas fa-trash-alt"></i> Delete</button>
                            <button onClick={() => props.onDelete(props.mentor.id)} id={props.mentor.id + '-confirm-delete'} style={{ display: 'none' }} className="btn btn-danger">
                            <i className="fas fa-check-circle"></i> Are you sure?
                            </button>
                        </>
                        : null}
                </div>
            </div>
        </>
    )
}

export default MentorListItem
