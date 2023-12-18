import { Outlet, Link } from "react-router-dom"
import "./MainLayout.css"

const MainLayout = (props) => {

    return (
        <div className="mainLayout">
            <div className="layout-header">
                <Link to="/" className="button header-button">Item List</Link>
                <br />
                <Link to="/cart" className="button header-button">Cart</Link>     
            </div>
            <div className="body">
                <Outlet />
            </div>
        </div>
    )
}

export default MainLayout;