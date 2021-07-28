import React from 'react'
import { useSelector } from 'react-redux'
import { Route, Redirect, useLocation } from 'react-router-dom'

const AuthenticatedRoute = (props) => {
    const location = useLocation()
    const { isAuthenticated } = useSelector(state => state.account)    

    if (isAuthenticated || props.isAuthenticated) {
        return <Route {...props} />
    }

    return <Redirect to="/account/login" redirectUrl={ location.pathname } />
}

export default AuthenticatedRoute