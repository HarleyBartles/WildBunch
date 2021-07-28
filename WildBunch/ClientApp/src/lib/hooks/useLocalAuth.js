import React, { useEffect, useState } from 'react'
import jwt_decode from "jwt-decode"
import requestManager from '../requestManager'
import { setUser } from '../../actionhandlers/account'
import { getGame } from '../../actionhandlers/game'
import { useDispatch, useSelector } from 'react-redux'

const useLocalAuth = (authToken) => {
    const dispatch = useDispatch()
    const [isAuthenticated, setIsAuthenticated] = useState(!!authToken)    
    const [activeGameId, setActiveGameId] = useState(null)

    useEffect(() => {        
        if (!!authToken) {
            var decodedToken = jwt_decode(authToken)

            if (Date.now() < decodedToken.exp * 1000) {
                requestManager.updateAccessToken(authToken)

                setIsAuthenticated(true)

                var user = {
                    userId: decodedToken.UserId,
                    email: decodedToken.Email,
                    authToken,
                    activeGameId: decodedToken.ActiveGameId
                }

                dispatch(setUser(user))
                setActiveGameId(decodedToken.ActiveGameId)
            }
        }
        else {
            setIsAuthenticated(false)
            setActiveGameId(null)
        }
    }, [authToken])

    return {
        isAuthenticated,
        activeGameId
    }
}

export {
    useLocalAuth
}