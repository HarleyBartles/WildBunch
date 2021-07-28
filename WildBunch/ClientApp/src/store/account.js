import { actions as gameActions } from './game'

export const actions = {
    REGISTER_USER_REQUEST: "REGISTER_USER_REQUEST",
    REGISTER_USER_RESPONSE: "REGISTER_USER_RESPONSE",
    LOGIN_USER_REQUEST: "LOGIN_USER_REQUEST",
    LOGIN_USER_RESPONSE: "LOGIN_USER_RESPONSE",
    SET_USER: "SET_USER",
    CLEAR_USER: "CLEAR_USER",
}

const initialState = {
    userId: null,
    email: null,
    isAuthenticated: false,
    authToken: null,
    activeGameId: null,
    errors: {
        login: [],
        register: [],
        forgotPassword: []
    }
}

export const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actions.REGISTER_USER_REQUEST:
            return RegisterUserRequest(state, action.payload)
        case actions.REGISTER_USER_RESPONSE:
            return RegisterUserResponse(state, action.payload)
        case actions.LOGIN_USER_REQUEST:
            return LoginUserRequest(state, action.payload)
        case actions.LOGIN_USER_RESPONSE:
            return LoginUserResponse(state, action.payload)
        case actions.SET_USER:
            return SetUser(state, action.payload)
        case actions.CLEAR_USER:
            return ClearUser(state)
        case gameActions.GAME_CREATED:
            return onGameCreated(state, action.payload)
        default:
            return state
    }    
}

const RegisterUserRequest = (state, payload) => {
    return state
}

const RegisterUserResponse = (state, payload) => {
    var { success, result, messages } = payload

    if (!success) {
        return {
            ...state,
            errors: {
                ...state.errors,
                register: messages
            }
        }
    }

    const { userId, email } = result

    return {
        ...state,
        userId: userId,
        isAuthenticated: !!userId
    }
}

const LoginUserRequest = (state, payload) => {
    return state
}

const LoginUserResponse = (state, payload) => {
    var { success, result, messages } = payload

    if (!success) {
        return {
            ...state,
            errors: {
                ...state.errors,
                login: messages
            }
        }
    }

    const { userId, email, rememberMe, token, activeGameId } = result

    if (rememberMe)
        localStorage.setItem("WildBunchAuthToken", token)
        
    return {
        ...state,
        userId: userId,
        isAuthenticated: !!userId,
        email,
        authToken: token,
        activeGameId
    }
}

const SetUser = (state, payload) => {
    const { userId, email, authToken, activeGameId } = payload

    return {
        ...state,
        userId: userId || state.userId,
        isAuthenticated: !!userId || !!state.userId,
        email: email || state.email,
        authToken: authToken || state.authToken,
        activeGameId: activeGameId || state.activeGameId
    }
}

const ClearUser = (state) => {
    return {...initialState}
}

const onGameCreated = (state, payload) => {
    const { success, result } = payload

    if (!success) {
        return state
    }

    const { gameId } = result

    return {
        ...state,
        activeGameId: gameId
    }
}