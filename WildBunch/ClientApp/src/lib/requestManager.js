import Promise from 'bluebird'
import qs from 'qs'
//import { actions as authActions } from '../Store/authManager'
//import { dialogTypes } from '../Components/DialogContainer'

const INTERNAL_SERVER_ERROR_STATUS_CODE = 500
const UNAUTHORISED_STATUS_CODE = 401
const FORBIDDEN_STATUS_CODE = 403
const NOT_FOUND_STATUS_CODE = 404
const BAD_REQUEST_STATUS_CODE = 400
const OK_STATUS_CODE = 200

const responseHandler = ({ response, request, ...requestArgs }) => {
    const contentType = response.headers.get("Content-Type")
    const reject = Promise.reject

    if (response.status == FORBIDDEN_STATUS_CODE) {
        requestManager.pauseRequests()
        // todo trigger reauth?
        return request(...requestArgs)
    }
    else if (contentType != null && contentType.includes('application/json')) {
        return response.json().then(responseData => {
            if (response.status !== OK_STATUS_CODE) {
                requestManager.triggerErrorDialog(responseData.Message)

                return reject(new Error(responseData.Message))
            }
            else {
                return responseData
            }
        })
    }
    else if (response.status == INTERNAL_SERVER_ERROR_STATUS_CODE) {
        const message = "An internal server error occured. If the problem persists please contact IT support."
        requestManager.triggerErrorDialog(message)

        return reject(message)
    }
    else if (response.status == UNAUTHORISED_STATUS_CODE) {
        const message = "You are not authorised to access the requested resource."
        requestManager.triggerErrorDialog(message)

        return reject(message)
    }
    else if (response.status == NOT_FOUND_STATUS_CODE) {
        const message = "The requested resource was not found. If the problem persists please contact IT support."
        requestManager.triggerErrorDialog(message)

        return reject(message)
    }
    else if (response.status == BAD_REQUEST_STATUS_CODE) {

        const message = "The server received a bad request. If the problem persists please contact IT support."
        requestManager.triggerErrorDialog(message)

        return reject(message)
    }
    else if (response.status !== OK_STATUS_CODE) {
        const message = `The server responded with error code ${response.status}. If the problem persists please contact IT support, quoting this code.`
        requestManager.triggerErrorDialog(message)

        return reject(message)
    }
    else {
        requestManager.triggerErrorDialog('An unknown error occured. If the problem persists please contact IT support.')

        return response
    }
}

// should have
// request id as the unique id ' class.method params'
// request status
// 

const localGetRequest = (url, token, params = {}, headers = {}) => {

    const fullUrl = `${url}?${qs.stringify(params)}`

    return fetch(fullUrl, {
        method: 'get',
        headers: new Headers({
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
            ...headers
        })
    })
        .then(response => responseHandler({ response, request: requestManager.localGetRequest, url, params, headers }))
}

const localPostRequest = (url, token, body, headers = {}, isJsonResponse = true, otherProps = {}) =>
    fetch(url, {
        method: 'POST',
        body: body,
        headers: new Headers({
            'Authorization': 'Bearer ' + token,
            ...headers
        }),
        ...otherProps
    })
        .then(response => responseHandler({ response, request: requestManager.localPostRequest, url, body, headers, otherProps }))

const newGuid = () => {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

class Request {
    constructor(parameters) {
        this._parameters = parameters
        this.id = newGuid()
    }

    build(promiseRetreiver) {
        const self = this
        this._promiseRetreiver = promiseRetreiver

        this._promise = new Promise((resolve, reject) => {
            return self._resolve = () => {
                let requestPromise = this._promiseRetreiver(self._parameters)
                return resolve(requestPromise)
            }
        });

        return this._promise
    }

    resolve() {
        if (this._resolve)
            this._resolve()
    }
}

class RequestManager {
    constructor() {
        this._pendingRequests = []
        this._accessToken = null
        this._store = null
    }

    connectStore(store) {
        this._store = store
    }

    pauseRequests() {
        this._accessToken = null
    }

    updateAccessToken(accessToken) {
        this._accessToken = accessToken

        this._pendingRequests.forEach(r => r.resolve())
        this._pendingRequests = []
    }

    localGetRequest(url, param, header, requireAuth = true) {
        const self = this

        if (this._accessToken != null || !requireAuth) {
            return localGetRequest(url, this._accessToken, param, header)
        }
        else {
            let requestParams = {
                url,
                param,
                header
            }

            let request = new Request(requestParams);
            let promise = request
                .build((result) => {
                    return localGetRequest(result.url, self._accessToken, result.param, result.header)
                })

            this._pendingRequests.push(request)
            return promise
        }
    }

    localPostRequest(url, body, headers, requireAuth = true, otherProps = {}) {
        const jsonHeaders = {
            ...headers,
            'Content-Type': 'application/json'
        }

        return this.postRawRequest(url, JSON.stringify(body), jsonHeaders, requireAuth, true, otherProps)
    }

    postRawRequest(url, body, headers, requireAuth = true, isJsonResponse = false, otherProps = {}) {
        const self = this

        if (this._accessToken != null || requireAuth) {
            return localPostRequest(url, this._accessToken, body, headers, isJsonResponse, otherProps)
        }
        else {
            let requestParams = {
                url,
                body,
                headers,
                isJsonResponse,
                otherProps
            }

            let request = new Request(requestParams);
            let promise = request
                .build((result) => {
                    return localPostRequest(result.url, self._accessToken, result.body, result.headers, result.isJsonResponse, result.otherProps)
                })

            this._pendingRequests.push(request)
            return promise
        }
    }

    triggerErrorDialog(message) {
        if (!this._store)
            return

        //this._store.dispatch({ type: 'SHOW_DIALOG', modalType: dialogTypes.REQUEST_ERROR, data: { message }, options: { isClosable: true } })
    }
}

const requestManager = new RequestManager()

export default requestManager