import { useState } from 'react'
import { useLocalAuth } from './hooks/useLocalAuth'

const AuthService = (props) => {
    const [authToken, setAuthToken] = useState("")
    const authenticated = useLocalAuth(authToken)

    function updateAuthToken(token) {
        setAuthToken(token)
    }

    function isAuthenticated() {
        return authenticated
    }

    function authenticate() {

    }    
}

export default AuthService