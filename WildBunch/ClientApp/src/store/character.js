import { actions as gameActions } from './game'

export const actions = {
    GET_CHARACTER: "GET_CHARACTER",
    SET_CHARACTER: "SET_CHARACTER"
}

const initialState = {    
    characterId: null,
    name: null,
    healthPoints: 0,
    dollars: 0,
    bag: {
        bagId: null,
        bagItems: []
    }
}

export const reducer = (state = initialState, action) => {
    switch (action.type) {
        case gameActions.GAME_CREATED:
            return gameCreated(state, action.payload)
        
        default:
            return state
    }
}

const gameCreated = (state, payload) => {
    const { success, result } = payload

    if (!success) {
        return state
    }

    const { character } = result
    const { characterId, name, healthPoints, dollars, bag } = character

    return {
        ...state,
        characterId,
        name,
        healthPoints,
        dollars,
        bag: {
            ...state.bag,
            bagId: bag.bagId,
            bagItems: bag.itemsCarried
        }
    }
}