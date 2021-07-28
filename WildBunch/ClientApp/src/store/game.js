
export const actions = {
    GET_GAME: "GET_GAME",
    SET_GAME: "SET_GAME",
    CREATE_GAME: "CREATE_GAME",
    GAME_CREATED: "GAME_CREATED"
}

const initialState = {
    activeGame: null,
    errors: {
        create: []
    }
}

export const reducer = (state = initialState, action) => {
    switch (action.type) {
        case actions.GET_GAME:
            return getGame(state, action.payload)
        case actions.SET_GAME:
            return setGame(state, action.payload)
        case actions.CREATE_GAME:
            return createGameRequested(state, action.payload)
        case actions.GAME_CREATED:
            return gameCreated(state, action.payload)
        default:
            return state
    }
}

const getGame = (state, payload) => {
    return state
}

const setGame = (state, payload) => {
    const {success, errors, result} = payload

    if (!success) {
        return {
            ...state,
            errors
        }
    }

    const { game } = result

    return {
        ...state,
        activeGame: game
    }
}

const createGameRequested = (state, payload) => {
    return state
}

const gameCreated = (state, payload) => {
    const { success, messages, result } = payload

    if (!success) {
        return {
            ...state,
            errors: {
                ...state.errors,
                create: messages
            }
        }
    }
    const { game } = result

    return {
        ...state,
        activeGame: game
    }
}