import React, { Fragment, useEffect } from 'react'
import { Link, useHistory } from "react-router-dom"
import { useDispatch, useSelector } from 'react-redux'
import { Button, Col, FormGroup, Input, Label, Row } from 'reactstrap'
import { Formik, Form, Field, ErrorMessage } from 'formik'
import { loginUser } from '../../actionhandlers/account'

const Login = (props) => {
    const dispatch = useDispatch()
    const history = useHistory()
    const [errors, isAuthenticated] = useSelector(state => [state.account.errors.login, state.account.isAuthenticated])

    const { redirectUrl } = props

    useEffect(() => {
        if (isAuthenticated) {
            history.push(redirectUrl || "/")
        }
    }, [isAuthenticated, history])

    function validate(values) {
        const errors = {};
        if (!values.email) {
            errors.email = 'Required';
        } else if (
            !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)
        ) {
            errors.email = 'Invalid email address';
        }

        return errors;
    }

    function handleSubmit(values, setSubmitting) {
        dispatch(loginUser(values))

        setSubmitting(false)
    }

    return (
        <Fragment>
            <h1>Login</h1>
            <Row>
                <Col md='4'>
                    <section>
                        <Formik
                            initialValues={{ email: '', password: '', rememberMe: false }}
                            validate={(values) => validate(values)}
                            onSubmit={(values, { setSubmitting }) => handleSubmit(values, setSubmitting)}
                        >
                            {({ isSubmitting, handleChange }) => (
                                <Form>
                                    <h4>Howdy pardner. What name do you go by?</h4>
                                    <hr />
                                    { !!errors && !!errors.length &&
                                        errors.map((err, index) => <div className="text-danger" key={index}>{err}</div>)
                                    }

                                    <FormGroup>
                                        <Label for="email">Email</Label>
                                        <Field type="email" name="email" id="email" component={Input} onChange={handleChange} />
                                        <ErrorMessage name="email" component="div" className="text-danger" />
                                    </FormGroup>
                                    <FormGroup>
                                        <Label for="password">Password</Label>
                                        <Field type="password" name="password" id="password" component={Input} onChange={handleChange} />
                                        <ErrorMessage name="password" component="div" className="text-danger" />
                                    </FormGroup>
                                    <FormGroup>
                                        <Label check>
                                            <Field type="checkbox" name="rememberMe" />{' '}
                                            Remember Me
                                    </Label>
                                    </FormGroup>
                                    <Button type="submit" color="primary" disabled={isSubmitting}>Go</Button>

                                    <FormGroup>
                                        <p>
                                            <Link id="forgot-password" to="/account/forgot-password">Forgot your password?</Link>
                                        </p>
                                        <p>
                                            <Link to="/account/register">Register as a new user</Link>
                                        </p>
                                    </FormGroup>
                                </Form>)}
                        </Formik>
                    </section>
                </Col>
            </Row>
        </Fragment>
    )
}

export default Login