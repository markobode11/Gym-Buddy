import { useContext } from "react"
import { NavLink } from "react-router-dom"
import { AppContext } from "../context/AppContext"

const Header = () => {
    const appState = useContext(AppContext)

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark bg-dark border-bottom box-shadow mb-3 nav">
                <div className="container">
                    <NavLink className="navbar-brand" to="/"><i className="fab fa-guilded"></i> Gym Buddy</NavLink>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">

                        <ul className="navbar-nav flex-grow-1">
                            <li className="nav-item">
                                <NavLink className="nav-link text-light" to="/exercises"><i className="fas fa-dumbbell"></i> Exercises</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link text-light" to="/workouts">Workouts</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link text-light" to="/splits">Splits</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link text-light" to="/programs">Programs</NavLink>
                            </li>
                            <li className="nav-item">
                                <NavLink className="nav-link text-light" to="/mentors"><i className="fas fa-user-alt"></i> Our Mentors</NavLink>
                            </li>
                            {appState.token && !appState.isMentor ?
                                <li className="nav-item">
                                    <NavLink className="nav-link text-light" to="/nutrition"><i className="fas fa-utensils"></i> Nutrition</NavLink>
                                </li>
                                : null}
                        </ul>

                        <ul className="navbar-nav">
                            {appState.token === null ?
                                <>
                                    <li className="nav-item">
                                        <NavLink className="nav-link text-light" to="/register"><i className="fas fa-registered"></i> Register</NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink className="nav-link text-light" to="/login"><i className="fas fa-sign-in-alt"></i> Login</NavLink>
                                    </li>
                                </>
                                :
                                appState.isMentor ?
                                    <li className="nav-item">
                                        <NavLink className="nav-link text-light" to='/mytrainees'>My trainees</NavLink>
                                    </li>
                                    :
                                    !appState.isAdmin ?
                                        <>
                                            <li className="nav-item">
                                                <NavLink className="nav-link text-light" to='/myprograms'>My Programs</NavLink>
                                            </li>
                                            <li className="nav-item">
                                                <NavLink className="nav-link text-light" to='/mymentor'>My mentor</NavLink>
                                            </li>
                                        </>
                                        : null
                            }
                            {appState.isAdmin ?
                                <li className="nav-item">
                                    <NavLink className="nav-link text-light" to='/usermanagement'>Manage users</NavLink>
                                </li>
                                :
                                null}
                            {appState.token !== null ?
                                <>
                                    <li className="nav-item">
                                        <NavLink className="nav-link text-light" to='/account'><i className="far fa-user"></i> My account</NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink to='/' onClick={appState.logOut} className="nav-link text-light"><i className="fas fa-sign-out-alt"></i> Logout</NavLink>
                                    </li>
                                </> : null
                            }
                        </ul>

                    </div>
                </div>
            </nav>
        </header>
    )
}

export default Header
