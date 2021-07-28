import React, { Fragment } from 'react'
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { useSelector } from 'react-redux';

const LoginPartial = (props) => {
    const { isAuthenticated } = useSelector(state => state.account)

    return isAuthenticated
        ? (
            <Fragment>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/account/manage">Manage Account</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/account/logout">Logout</NavLink>
                </NavItem>
            </Fragment>)
        : (
            <Fragment>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/account/login">Login</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/account/register">Register</NavLink>
                </NavItem>
            </Fragment>)
}

export default LoginPartial