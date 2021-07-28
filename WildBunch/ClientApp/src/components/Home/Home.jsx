import React from 'react';
import { useSelector } from 'react-redux';
import { Redirect } from 'react-router-dom';

const Home = () => {
    const { isAuthenticated, activeGameId } = useSelector(state => state.account)

    if (isAuthenticated && !!activeGameId) {
        return <Redirect to={`/game/${activeGameId}/map`} />
    } else if (isAuthenticated) {
        return <Redirect to="/game/create" />
    }

    return <Redirect to="/account/login" />
}

export default (Home);
