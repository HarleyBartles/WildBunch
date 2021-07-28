import React, { Fragment, useEffect } from 'react'
import { Button, Col, FormGroup, Input, Label, Row } from 'reactstrap'
import { Formik, Form, Field, ErrorMessage } from 'formik'
import { registerUser } from '../../actionhandlers/account'
import { useDispatch, useSelector } from 'react-redux'
import { useHistory } from 'react-router'

const Register = (props) => {
    const dispatch = useDispatch()
    const history = useHistory()
    const [errors, isAuthenticated] = useSelector(state => [state.account.errors.register, state.account.isAuthenticated])

    useEffect(() => {
        if (isAuthenticated === true)
            history.push("/")
    }, [isAuthenticated, history])

    function handleSubmit(values, setSubmitting) {
        dispatch(registerUser(values))

        setSubmitting(false)
    }

    function validate(values) {
        const errors = {};

        if (!values.email) {
            errors.email = 'Required';
        }
        else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)) {
            errors.email = 'Invalid email address';
        }

        if (values.password !== values.confirmPassword) {
            errors.password = 'Passwords do not match'
            errors.confirmPassword = 'Passwords do not match'
        }

        return errors;
    }

    return (
        <Fragment>
            <h1>Register</h1>
            <Row>
                <Col md="4">
                    <Formik
                        initialValues={{ email: '', password: '', confirmPassword: '' }}
                        validate={(values) => validate(values)}
                        onSubmit={(values, { setSubmitting }) => handleSubmit(values, setSubmitting)}
                    >
                        {({ isSubmitting, handleChange }) => (
                            <Form>
                                <h4>Glad to make your aquaintance friend.</h4>
                                <p>Stop a spell and tell me a little about yourself</p>
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
                                    <Label for="confirm-password">Confirm Password</Label>
                                    <Field type="password" name="confirm-password" id="confirmPassword" component={Input} onChange={handleChange} />
                                    <ErrorMessage name="confirmPassword" component="div" className="text-danger"/>
                                </FormGroup>
                                <Button type="submit" color="primary" disabled={isSubmitting}>Go</Button>
                            </Form>
                        )}
                    </Formik>
                </Col>
            </Row>
        </Fragment>
    )
}

export default Register