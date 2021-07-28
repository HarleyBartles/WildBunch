import * as React from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import LoginPartial from './LoginPartial';
import { useState } from 'react';
import { useSelector } from 'react-redux';

const NavMenu = (props) => {
    const [isOpen, setIsOpen] = useState(false)
    const { isAuthenticated, activeGameId } = useSelector(state => state.account)

    const homeUrl = isAuthenticated
        ? !!activeGameId
            ? `/game/${activeGameId}/map`
            : "/game/create"
        : "/"

    function toggle() {
        setIsOpen(!isOpen);
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3 main-nav" light>
                <Container>
                    <NavbarBrand tag={Link} to={homeUrl}>WildBunch</NavbarBrand>
                    <NavbarToggler onClick={toggle} className="mr-2" />
                    <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={isOpen} navbar>
                        <ul className="navbar-nav flex-grow">
                            <NavItem>
                                <NavLink tag={Link} className="text-dark" to={homeUrl}>Home</NavLink>
                            </NavItem>
                            <LoginPartial />
                        </ul>
                    </Collapse>
                </Container>
            </Navbar>
        </header>
    );
}

export default NavMenu
