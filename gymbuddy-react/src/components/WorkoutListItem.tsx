import { Link } from "react-router-dom";
import { useContext } from "react";
import { AppContext } from "../context/AppContext";
import { IWorkout } from "../domain/IWorkout";

const WorkoutListItem = (props: { workout: IWorkout, onDelete: (id: number) => void, onConfirmDelete: (id: number) => void }) => {
    const appState = useContext(AppContext);

    return (
        <>
            <div className="col mb-3 flex-fill">
                <div className="card shadow-sm">
                    <div className="card-body workout-item">
                        <p className="card-text"><b>{props.workout.name}</b></p>
                        <p className="card-text">{props.workout.description}</p>
                        <div className="d-flex justify-content-between align-items-center">
                            <div className="btn-group">
                                <Link className="btn btn-sm btn-outline-secondary"
                                    to={'/workouts/' + props.workout.id}><i className="fas fa-eye"></i> View</Link>
                                {appState.isAdmin ?
                                    <Link className="btn btn-sm btn-outline-secondary" to={'/workouts/edit/' + props.workout.id}>Edit</Link>
                                    : null}
                            </div>
                            <small className="text-muted">{props.workout.difficulty!.name}</small>
                        </div>
                    </div>
                    {appState.isAdmin ?
                        <>
                            <button onClick={() => props.onConfirmDelete(props.workout.id)} id={props.workout.id + '-delete'} className="btn btn-sm btn-delete"><i className="fas fa-trash-alt"></i> Delete</button>
                            <button onClick={() => props.onDelete(props.workout.id)} id={props.workout.id + '-confirm-delete'} style={{ display: 'none' }} className="btn btn-sm btn-danger">
                            <i className="fas fa-check-circle"></i> Are you sure?
                            </button>
                        </>
                        : null}
                </div>
            </div>
        </>
    )
}

export default WorkoutListItem