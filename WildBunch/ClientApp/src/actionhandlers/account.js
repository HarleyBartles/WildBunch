import requestManager from '../lib/requestManager'
import { actions } from '../store/account'

const registerUser = (user) => {

    return (dispatch) => {
        const model = {
            email: user.email,
            password: user.password,
        }

        dispatch({ type: actions.REGISTER_USER_REQUEST, payload: model })

        requestManager.localPostRequest('/api/account/register', model)
            .then(response => {
                dispatch({ type: actions.REGISTER_USER_RESPONSE, payload: response })
            })
            .catch(ex => {
                dispatch({ type: actions.REGISTER_USER_RESPONSE, payload: { messages: ex , success: false } })
            })
    }    
}

const setUser = (user) => {
    return (dispatch) => {
        dispatch({ type: actions.SET_USER, payload: user })
    }
}

const loginUser = (user) => {

    return (dispatch) => {
        const model = {
            email: user.email,
            rememberMe: user.rememberMe
        }

        dispatch({ type: actions.LOGIN_USER_REQUEST, payload: { model } })

        model.password = user.password

        requestManager.localPostRequest('/api/account/login', model)
            .then(response => {
                dispatch({ type: actions.LOGIN_USER_RESPONSE, payload: response })
            })
            .catch(ex => {
                dispatch({ type: actions.LOGIN_USER_RESPONSE, payload: { ...ex, success: false } })
            })
    }
}

const logoutUser = () => {
    return (dispatch) => {
        dispatch({ type: actions.CLEAR_USER })
    }
}

export {
    registerUser,
    loginUser,
    logoutUser,
    setUser,
}