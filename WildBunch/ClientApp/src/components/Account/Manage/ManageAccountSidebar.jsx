import React from 'react'
import { Link } from 'react-router-dom'
import { Col, Nav, Navbar, NavItem, NavLink } from 'reactstrap'

const ManageAccountSidebar = () => {
    return (
        <Col md="3">
            <Navbar color="light" light className="sidebar-sticky">
                <Nav className="mr-auto" navbar>
                    <NavItem>
                        <NavLink tag={Link} to="/account/manage/profile">Profile</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={ Link } to="#">Change Password</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} to="#">Confirm Email</NavLink>
                    </NavItem>
                </Nav>
            </Navbar>
        </Col>
    )
}

export default ManageAccountSidebar