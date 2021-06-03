import { Link } from "react-router-dom"

const Footer = () => {
    return (
        <footer className="border-top footer text-light">
            <div className="container">
                &copy; 2021 - GymBuddy - <Link to="/privacy"><i className="fas fa-user-secret"></i> Privacy</Link>
            </div>
        </footer>
    )
}

export default Footer
