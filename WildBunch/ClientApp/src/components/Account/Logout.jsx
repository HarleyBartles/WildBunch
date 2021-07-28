import React from 'react'
import { useDispatch } from 'react-redux';
import { Redirect } from 'react-router';
import { logoutUser } from '../../actionhandlers/account';

const Logout = () => {
    const dispatch = useDispatch()

    localStorage.removeItem("WildBunchAuthToken")

    dispatch(logoutUser());

    return <Redirect to="/account/login" />
}

export default Logout