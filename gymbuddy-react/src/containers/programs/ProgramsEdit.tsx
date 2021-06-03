import React, { useState, useContext, useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { InitialProgram, IProgram } from "../../domain/IProgram";
import { ISplit } from "../../domain/ISplit";
import { ISplitInProgram } from "../../domain/ISplitInProgram";
import { BaseService } from "../../services/base-service";
import { IRouteId } from "../../types/IRouteId";

const ProgramsEdit = () => {
    const { id } = useParams() as IRouteId;

    const [model, setModel] = useState(InitialProgram);
    const [splits, setSplits] = useState([] as ISplit[])
    const [error, setError] = useState('')
    const [message, setMessage] = useState('')
    const appState = useContext(AppContext)
    const [splitInProgram, setSplitInProgram] = useState({} as ISplitInProgram)

    const saveClicked = async () => {
        if (!model.name || !model.description || !model.goal) {
            setError('All fields are required!')
            setMessage('')
            return
        }
        setError('')
        const response = await BaseService.edit<IProgram>(model, "/fullprograms/" + id, appState.token!);
        if (response.ok) {
            setMessage("Saved!")
        } else {
            setError(response.message!);
        }
    }

    const loadData = async (id: string) => {
        const response = await BaseService.get<IProgram>("/fullprograms/" + id);
        if (response.ok) {
            setModel(response.data!)
        } else {
            setError(response.message!);
        }
    }

    const loadSplits = async () => {
        const response = await BaseService.getAll<ISplit>("/splits");
        if (response.ok) {
            setSplits(response.data!)
        } else {
            setError(response.message!);
        }
    }

    useEffect(() => {
        loadData(id)
        loadSplits()
    }, [id])

    const addSplit = async () => {
        if (!splitInProgram.splitId) {
            setError("To add a split please select one first")
            setMessage('')
            return;
        }
        setError('')
        splitInProgram.fullProgramId = model.id;
        const response = await BaseService.create(splitInProgram, "/splitinprogram", appState.token!)
        if (response.ok) {
            setMessage('Split added!')
            const addedSplit = splits.find(x => x.id.toString() === splitInProgram.splitId)
            setModel({ ...model, splitsInProgram: [...model.splitsInProgram, addedSplit!] })
        } else {
            setError(response.message!)
        }
    }

    const removeSplit = async (splitId: number, index: number) => {
        const response = await BaseService.delete("/splitinprogram/" + model.id + "/" + splitId, appState.token!)
        if (response.ok) {
            setMessage('Split removed!')
            document.getElementById(index.toString())!.remove()
        } else {
            setError(response.message!)
        }
    }

    return (
        <>
            <h1>Edit</h1>

            <h4>{model.name}</h4>

            <div className='mt-3'>
                <h5><Link to="/programs">Back to List</Link></h5>
            </div>

            <hr />
            <div className="row">
                <div className="col-md-4">
                    <Alert show={error !== ''} message={error} alertClass={EAlertClass.Danger} />
                    <Alert show={message !== ''} message={message} alertClass={EAlertClass.Success} />
                    <div className="form-group">
                        <label className="control-label" htmlFor="Name">Name</label>
                        <input className="form-control" type="text" name="Name" id='Name' value={model.name} onChange={e => setModel({ ...model, name: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="Description">Description</label>
                        <textarea className="form-control" id="Description" name="Description"
                            value={model.description} onChange={e => setModel({ ...model, description: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <label className="control-label" htmlFor="Goal">Goal</label>
                        <input className="form-control" type="text" id="Goal" name="Goal"
                            value={model.goal} onChange={e => setModel({ ...model, goal: e.target.value })} />
                    </div>
                    <div className="form-group">
                        <input type='submit' onClick={saveClicked} value="Save" className="btn btn-primary" />
                    </div>
                </div>
            </div>

            <div className="form-group justify-content-between  mb-3">
                <select className="form-control" onChange={e => setSplitInProgram({ ...splitInProgram, splitId: e.target.value })}>
                    <option value="">Select split...</option>
                    {splits.map(split => <option key={split.id} value={split.id} >{split.name} - {split.description}</option>)}

                </select>
                <button onClick={addSplit} className='align-self-center btn btn-info mt-3 mb-3'>Add split to this program</button>
            </div>

            {model.splitsInProgram.length !== 0
                ?
                <h3 >Splits in this program:</h3>
                :
                <h3>No splits added yet</h3>}

            <div className="list-group">
                {model.splitsInProgram.map((split, index) =>
                    <li id={index.toString()} key={index} className="list-group-item flex-column align-items-start">
                        <div className="d-flex w-100 justify-content-between">
                            <h5 className="mb-1">{split.name}</h5>
                        </div>
                        <p className="mb-1">{split.description}</p>
                        <button onClick={() => removeSplit(split.id, index)} className='btn btn-info'>Remove split</button>
                    </li>
                )}
            </div>

        </>
    )
}

export default ProgramsEdit
