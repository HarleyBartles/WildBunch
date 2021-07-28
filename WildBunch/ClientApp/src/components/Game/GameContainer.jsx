import React from 'react';
import { useSelector } from 'react-redux';
import { Redirect } from 'react-router';
import { Route } from 'react-router';
import Create from "./Create";
import Map from './Map'

const GameContainer = (props) => {
    return (
        <div>
            <Route path={`${props.match.path}/create`} component={Create} />
            <Route path={`${props.match.path}/:gameId/map`} component={Map} />
        </div>
    )
};

export default GameContainer