import { useContext } from "react"
import { Link } from "react-router-dom"
import { AppContext } from "../context/AppContext"
import pic from './../gymbuddy.jpg'

const Home = () => {
    const appState = useContext(AppContext)

    return (
        <>
        <img alt='gymbuddy' src={pic} className='img-fluid'></img>
            <div
                className="position-relative overflow-hidden p-3 p-md-5 m-md-3 text-center text-light bg-dark"
            >
                <div className="col-md-9 p-lg-9 mx-auto my-5">
                    <p className="lead font-weight-normal">
                        Learn how to train for your needs
            </p>
                    {!appState.token ?
                        <Link className="btn btn-outline-secondary" to="/register">
                            Create account now!
                    </Link> : null}
                </div>
            </div>
        </>
    )
}

export default Home
