import React from 'react';
import { Route } from 'react-router';
import ForgotPassword from './ForgotPassword';
import Login from "./Login";
import Logout from "./Logout";
import Register from './Register';
import ManageAccountLayout from './Manage/ManageAccountLayout'
import AuthenticatedRoute from '../AuthenticatedRoute';

const AccountLayout = (props) => {
    return (
        <div>
            <Route exact path={`${props.match.path}/login`} component={Login} />
            <Route exact path={`${props.match.path}/logout`} component={Logout} />
            <Route exact path={`${props.match.path}/forgot-password`} component={ForgotPassword} />
            <Route exact path={`${props.match.path}/register`} component={Register} />
            <AuthenticatedRoute path={`${props.match.path}/manage`} component={ManageAccountLayout} />
        </div>
    )
};

export default AccountLayout