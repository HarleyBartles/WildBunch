import React from 'react';
import { Route } from 'react-router';
import { Row } from 'reactstrap';
import Sidebar from './ManageAccountSidebar'
import Profile from './Profile'

const ManageAccountLayout = (props) => {
    return (
        <Row>
            <Sidebar />
            <Route path={`${props.match.path}/profile`} component={Profile} />
        </Row>
    )
};

export default ManageAccountLayout