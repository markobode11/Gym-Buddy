import React, { useContext, useEffect, useState } from "react"
import Alert, { EAlertClass } from "../../components/Alert";
import { AppContext } from "../../context/AppContext";
import { EGender } from "../../domain/Enums/EGender";
import { ICalculateMacros } from "../../domain/ICalculateMacros";
import { IMacros } from "../../domain/IMacros"
import { BaseService } from "../../services/base-service";

const MacrosIndex = () => {
    const [userMacros, setUserMacros] = useState(undefined as IMacros | null | undefined);
    const [calculateModel, setCalculateModel] = useState({} as ICalculateMacros)
    const [errorMessage, setErrorMessage] = useState('')
    const appState = useContext(AppContext);

    const calculateClicked = async () => {
        if (!calculateModel.age || !calculateModel.gender || !calculateModel.height || !calculateModel.morGorL || !calculateModel.weight) {
            setErrorMessage('All fields are required!');
            return
        }
        setErrorMessage('')
        const response = await BaseService.create<IMacros, ICalculateMacros>(calculateModel, '/macros', appState.token!);
        if (response.ok) {
            setUserMacros(response.data as IMacros)
        } else {
            setErrorMessage(response.message!);
        }
    }

    const calculateAgainClicked = () => {
        setUserMacros(null)
    }

    useEffect(() => {
        const loadData = async () => {
            const response = await BaseService.get<IMacros>('/macros', appState.token!)
            if (response.ok) {
                setUserMacros(response.data!)
            } else {
                setUserMacros(null)
            }
        }
        loadData()
    }, [appState.token])

    return userMacros === undefined ? <></> : (
        <>
            {!userMacros ? // user does not have macros yet
                <div className="col-sm-10 col-md-10">
                    <div>
                        <h3>To find out your nutritional needs</h3>
                        <h3>Insert details</h3>
                    </div>

                    <Alert show={errorMessage !== ''} message={errorMessage} alertClass={EAlertClass.Danger} />

                    <div className="form-group mt-5">
                        <label htmlFor="age">Age</label>
                        <input
                            className="form-control"
                            id="age"
                            type="number"
                            onChange={(e) => setCalculateModel({ ...calculateModel, age: parseInt(e.target.value) })}
                        />
                    </div>
                    <div className="form-group mt">
                        <label htmlFor="weight">Weight</label>
                        <input
                            className="form-control"
                            id="weight"
                            type="number"
                            onChange={(e) => setCalculateModel({ ...calculateModel, weight: parseInt(e.target.value) })}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="height">Height</label>
                        <input
                            className="form-control"
                            id="height"
                            type="number"
                            onChange={(e) => setCalculateModel({ ...calculateModel, height: parseInt(e.target.value) })}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="goal">Your goal</label>
                        <select className="form-control" id="goal" onChange={(e) => setCalculateModel({ ...calculateModel, morGorL: e.target.value })}>
                            <option value="">Choose your goal...</option>
                            <option value="M">Maintain weight</option>
                            <option value="G">Gain weight 0.5 kg per week</option>
                            <option value="L">Lose weight 0.5 kg per week</option>
                        </select>
                    </div>
                    <div className="form-group">
                        <label htmlFor="goal">Gender</label>
                        <select className="form-control" id="gender" onChange={(e) => setCalculateModel({ ...calculateModel, gender: parseInt(e.target.value) })}>
                            <option value=''>Choose you gender...</option>
                            <option value={EGender.Male}>Male</option>
                            <option value={EGender.Female}>Female</option>
                        </select>
                    </div>

                    <button
                        type="submit"
                        className="btn btn-info"
                        onClick={calculateClicked}
                    >
                        <i className="fas fa-calculator"></i> Calculate
                    </button>
                </div>

                : // else show this

                <div className="col-sm-10 col-md-6">
                    <h3>Your nutritional needs</h3>

                    <div className='alert alert-info col-8 mt-5'>{userMacros.kcal} kcal</div>
                    <div className='alert alert-info col-8'>{userMacros.protein}g of protein</div>
                    <div className='alert alert-info col-8'>{userMacros.carbs}g of carbs</div>
                    <div className='alert alert-info col-8'>{userMacros.fat}g of fat</div>

                    <div className='alert alert-warning'>Note that all these numbers are rough estimates and may vary on your activity level</div>

                    <button className="btn btn-info" onClick={calculateAgainClicked}>
                    <i className="fas fa-calculator"></i> Calculate again
                    </button>
                </div>
            }
        </>
    ) 
}

export default MacrosIndex