import { useState } from "react";
import { Route, Switch } from "react-router-dom";
import Footer from "./components/Footer";
import Header from "./components/Header";
import ExercisesCreate from "./containers/exercises/ExercisesCreate";
import ExercisesDetails from "./containers/exercises/ExercisesDetails";
import ExercisesEdit from "./containers/exercises/ExercisesEdit";
import ExercisesIndex from "./containers/exercises/ExercisesIndex";
import Home from "./containers/Home";
import Login from "./containers/identity/Login";
import Register from "./containers/identity/Register";
import Page404 from "./containers/Page404";
import Privacy from "./containers/Privacy";
import { AppContextProvider, initialAppState } from "./context/AppContext";
import jwt_decode from "jwt-decode";
import SplitsCreate from "./containers/splits/SplitsCreate";
import SplitsDetails from "./containers/splits/SplitsDetails";
import SplitsEdit from "./containers/splits/SplitsEdit";
import SplitsIndex from "./containers/splits/SplitsIndex";
import WorkoutsCreate from "./containers/workouts/WorkoutsCreate";
import WorkoutsDetails from "./containers/workouts/WorkoutsDetails";
import WorkoutsEdit from "./containers/workouts/WorkoutsEdit";
import WorkoutsIndex from "./containers/workouts/WorkoutsIndex";
import ProgramsCreate from "./containers/programs/ProgramsCreate";
import ProgramsEdit from "./containers/programs/ProgramsEdit";
import ProgramsDetails from "./containers/programs/ProgramsDetails";
import ProgramsIndex from "./containers/programs/ProgramsIndex";
import MentorsIndex from "./containers/mentors/MentorsIndex";
import MentorsDetails from "./containers/mentors/MentorsDetails";
import MentorsEdit from "./containers/mentors/MentorsEdit";
import MentorsCreate from "./containers/mentors/MentorsCreate";
import MacrosIndex from "./containers/nutrition/MacrosIndex";
import MyProgramsIndex from "./containers/my-programs/MyProgramsIndex";
import MyMentorIndex from "./containers/my-mentor/MyMentorIndex";
import MyTraineesIndex from "./containers/my-trainees/MyTraineesIndex";
import Account from "./containers/identity/Account";
import UserManagement from "./containers/identity/UserManagement";
import StartLockout from "./containers/identity/StartLockout";
import AddRole from "./containers/identity/AddRole";

function App() {
    const setAuthInfo = (token: string, firstname: string, lastname: string): void => {
        var decodedToken = jwt_decode(token)
        var role = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]
        var isAdmin = false;
        var isMentor = false;
        if (role === "Admin") {
            isAdmin = true;
            document.body.style.backgroundColor = '#587ccc';
        }
        if (role === "Mentor") {
            isMentor = true;
            document.body.style.backgroundColor = '#587c77';
        }
        setAppState({ token, firstname, lastname, isAdmin, isMentor, setAuthInfo, logOut });
    }

    const logOut = (): void => {
        document.body.style.backgroundColor = '#587c94';
        setAppState({ ...initialAppState, setAuthInfo, logOut });
    }

    const [appState, setAppState] = useState({ ...initialAppState, setAuthInfo });

    return (
        <>
            <AppContextProvider value={appState} >
                <Header />

                <div className="container">
                    <main role="main" className="pb-3">

                        <Switch>
                            <Route exact path='/' component={Home} />
                            <Route path='/privacy' component={Privacy} />

                            <Route path='/login' component={Login} />
                            <Route path='/register' component={Register} />
                            {/* <Route path='/account' component={Account} /> */}

                            <Route path='/exercises/create' component={appState.isAdmin ? ExercisesCreate : Page404} />
                            <Route path='/exercises/edit/:id' component={appState.isAdmin ? ExercisesEdit : Page404} />
                            <Route path='/exercises/:id' component={ExercisesDetails} />
                            <Route path='/exercises' component={ExercisesIndex} />

                            <Route path='/workouts/create' component={appState.isAdmin ? WorkoutsCreate : Page404} />
                            <Route path='/workouts/edit/:id' component={appState.isAdmin ? WorkoutsEdit : Page404} />
                            <Route path='/workouts/:id' component={WorkoutsDetails} />
                            <Route path='/workouts' component={WorkoutsIndex} />

                            <Route path='/splits/create' component={appState.isAdmin ? SplitsCreate : Page404} />
                            <Route path='/splits/edit/:id' component={appState.isAdmin ? SplitsEdit : Page404} />
                            <Route path='/splits/:id' component={SplitsDetails} />
                            <Route path='/splits' component={SplitsIndex} />

                            <Route path='/programs/create' component={appState.isAdmin ? ProgramsCreate : Page404} />
                            <Route path='/programs/edit/:id' component={appState.isAdmin ? ProgramsEdit : Page404} />
                            <Route path='/programs/:id' component={ProgramsDetails} />
                            <Route path='/programs' component={ProgramsIndex} />

                            <Route path='/mentors/create' component={appState.isAdmin ? MentorsCreate : Page404} />
                            <Route path='/mentors/edit/:id' component={appState.isAdmin ? MentorsEdit : Page404} />
                            <Route path='/mentors/:id' component={MentorsDetails} />
                            <Route path='/mentors' component={MentorsIndex} />

                            <Route path='/nutrition' component={appState.token && !appState.isMentor ? MacrosIndex : Page404} />

                            <Route path='/myprograms' component={appState.token ? MyProgramsIndex : Page404} />

                            <Route path='/mymentor' component={appState.token ? MyMentorIndex : Page404} />

                            <Route path='/mytrainees' component={appState.isMentor ? MyTraineesIndex : Page404} />

                            <Route path='/account' component={appState.token ? Account : Page404} />

                            <Route path='/usermanagement' component={appState.isAdmin ? UserManagement : Page404} />
                            <Route path='/startlockout/:id' component={appState.isAdmin ? StartLockout : Page404} />
                            <Route path='/addrole/:id' component={appState.isAdmin ? AddRole : Page404} />

                            <Route component={Page404} />
                        </Switch>

                    </main>
                </div>

                <Footer />
            </AppContextProvider>
        </>
    );
}

export default App;
