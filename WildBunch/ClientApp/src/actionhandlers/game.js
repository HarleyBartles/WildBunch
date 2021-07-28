import requestManager from '../lib/requestManager'
import { actions } from '../store/game'

const getGame = (gameId) => {
    return (dispatch) => {
        dispatch({ type: actions.GET_GAME, payload: { gameId } })

        requestManager.localGetRequest("/api/game/get-game", { gameId } )
            .then(response => {
                dispatch({ type: actions.SET_GAME, payload: response })
            })
    }
}

const createGame = (characterName, difficulty) => {
    return (dispatch) => {
        dispatch({ type: actions.CREATE_GAME, payload: { characterName, difficulty: parseInt(difficulty) } })

        requestManager.localPostRequest("api/game/create-game", { characterName, difficulty: parseInt(difficulty) })
            .then(response => {
                dispatch({ type: actions.GAME_CREATED, payload: response })
            })
    }
}

export {
    getGame,
    createGame
}