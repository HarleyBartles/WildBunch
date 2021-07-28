import * as React from 'react';
import { Route } from 'react-router';
import AuthenticatedRoute from './components/AuthenticatedRoute';
import { Layout, Home } from './components/Home';
import { AccountLayout } from './components/Account';
import { useLocalAuth } from './lib/hooks/useLocalAuth';
import GameContainer from './components/Game/GameContainer';
import './styles/custom.css'
import './styles/custom.scss'
import { useLocalGameId } from './lib/hooks/useLocalGameId';

export default () => {
    const token = localStorage.getItem("WildBunchAuthToken")
    
    const { isAuthenticated, activeGameId }= useLocalAuth(token)

    return (
        <Layout>
            <Route exact path='/' component={Home} />
            <Route path='/account' component={AccountLayout} />
            <AuthenticatedRoute path='/game' component={GameContainer} isAuthenticated={ isAuthenticated } />
        </Layout>
    )
};
