import { IExercise } from "./../domain/IExercise";
import { Link } from "react-router-dom";
import { useContext } from "react";
import { AppContext } from "../context/AppContext";

const ExerciseListItem = (props: { exercise: IExercise, onDelete: (id: number) => void, onConfirmDelete: (id: number) => void }) => {
    const appState = useContext(AppContext);

    return (
        <>
            <div className="col mb-3">
                <div className="card shadow-sm">
                    <div className="card-body bg-app">
                        <p className="card-text"><b>{props.exercise.name}</b></p>
                        <p className="card-text">{props.exercise.description}</p>
                        <div className="d-flex justify-content-between align-items-center">
                            <div className="btn-group">
                                <Link className="btn btn-sm btn-outline-secondary"
                                    to={'/exercises/' + props.exercise.id}><i className="fas fa-eye"></i> View</Link>
                                {appState.isAdmin ?
                                    <Link className="btn btn-sm btn-outline-secondary" to={'/exercises/edit/' + props.exercise.id}>Edit</Link>
                                    : null}
                            </div>
                            <small className="text-muted">{props.exercise.difficulty!.name}</small>
                        </div>
                    </div>
                    {appState.isAdmin ?
                        <>
                            <button onClick={() => props.onConfirmDelete(props.exercise.id)} id={props.exercise.id + '-delete'} className="btn btn-sm btn-delete"><i className="fas fa-trash-alt"></i> Delete</button>
                            <button onClick={() => props.onDelete(props.exercise.id)} id={props.exercise.id + '-confirm-delete'} style={{ display: 'none' }} className="btn btn-sm btn-danger">
                            <i className="fas fa-check-circle"></i> Are you sure?
                            </button>
                        </>
                        : null}
                </div>
            </div>
        </>
    )
}

export default ExerciseListItem
