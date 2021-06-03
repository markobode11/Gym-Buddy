import React, { useState, useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import Alert, { EAlertClass } from "../../components/Alert";
import Search from "../../components/Search";
import SplitListItem from "../../components/SplitListItem";
import { AppContext } from "../../context/AppContext";
import { ISplit } from "../../domain/ISplit";
import { BaseService } from "../../services/base-service";

const SplitsIndex = () => {
    const [errorMessage, setErrorMessage] = useState('')
    const [deleteMessage, setDeleteMessage] = useState('')
    const [splits, setSplits] = useState([] as ISplit[])
    const [splitsToSHow, setSplitsToShow] = useState([] as ISplit[])
    const appState = useContext(AppContext);

    const loadData = async () => {
        const response = await BaseService.getAll<ISplit>('/splits')
        if (response.ok) {
            setSplits(response.data!)
            setSplitsToShow(response.data!)
        } else {
            setErrorMessage(response.message!)
        }
    }

    useEffect(() => {
        loadData();
    }, []);

    const confirmDelete = (id: number) => {
        const deleteButton = document.getElementById(`${id}-delete`)
        const confirmDeleteButton = document.getElementById(`${id}-confirm-delete`)

        deleteButton!.style.display = 'none';
        confirmDeleteButton!.style.display = 'block';
    }

    const onDelete = async (id: number) => {
        const response = await BaseService.delete("/splits/" + id, appState.token!)
        if (response.ok) {
            setDeleteMessage('Deleted!');
            setSplits([...splits.filter(x => x.id !== id)])
            setSplitsToShow([...splitsToSHow.filter(x => x.id !== id)])
        } else {
            setErrorMessage(response.message!);
        }
    }

    const onSearch = (search: string) => {
        setSplitsToShow([...splits.filter(x => x.name.toLowerCase().includes(search.toLowerCase()))])
    }

    const onClear = () => {
        setSplitsToShow([...splits])
    }

    return (
        <>
            <section className="py-5 text-center container">
                <div className="row py-lg-5">
                    <div className="col-lg-6 col-md-8 mx-auto">
                        <h1 className="fw-light">Splits</h1>
                        <p className="lead text-light">Learn about different splits.</p>
                        {appState.isAdmin ?
                            <p>
                                <Link to='/splits/create' className="btn btn-primary my-2">Create new</Link>
                            </p>
                            : null}
                    </div>
                </div>
            </section>

            <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />
            <Alert show={deleteMessage !== ''} message={deleteMessage} alertClass={EAlertClass.Success} />

            <Search onClear={onClear} onSearch={onSearch} />

            <div className="album py-5">
                <div className="container">

                    <div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 g-3">

                        {splitsToSHow.map(split => <SplitListItem onConfirmDelete={confirmDelete} onDelete={onDelete} split={split} key={split.id} />)}

                    </div>
                </div>
            </div>
        </>
    )
}

export default SplitsIndex
