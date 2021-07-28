import React, { useEffect, useState } from 'react'
import jwt_decode from "jwt-decode"
import requestManager from '../requestManager'
import { setUser } from '../../actionhandlers/account'
import { getGame } from '../../actionhandlers/game'
import { useDispatch, useSelector } from 'react-redux'

const useLocalGameId = (activeGameId) => {
    const dispatch = useDispatch()
    const [isActive, setIsActive] = useState(!!activeGameId)

    useEffect(() => {
        if (!!activeGameId) {
            setIsActive(true)

            var user = {
                activeGameId
            }

            dispatch(setUser(user))
        }
        else {
            setIsActive(false)
        }
    }, [activeGameId])

    return isActive
}

export {
    useLocalGameId
}