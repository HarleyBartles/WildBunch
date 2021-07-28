import React from 'react'
import { useSelector } from 'react-redux'
import { Redirect } from 'react-router'
import { Field, Formik, ErrorMessage } from 'formik'
import { Form, FormGroup, Input, Label, Row, Col, Button } from 'reactstrap'

const Profile = () => {
    const { isAuthenticated, email } = useSelector(state => state.account)
    const errors = useSelector(state => state.account.errors.profile)

    function validate(values) {
        const errors = {};
        if (!values.emailAddress) {
            errors.emailAddress = 'Required';
        } else if (
            !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.emailAddress)
        ) {
            errors.emailAddress = 'Invalid email address';
        }

        return errors;
    }

    function handleSubmit(values, setSubmitting) {
        setSubmitting(false)
    }

    if (!isAuthenticated)
        return <Redirect to='/account/login' />

    return (
        <Col md="6">
            <Formik
                initialValues={{ emailAddress: email, phoneNumber: '' }}
                validate={(values) => validate(values)}
                onSubmit={(values, { setSubmitting }) => handleSubmit(values, setSubmitting)}
            >
                {({ isSubmitting, handleChange }) => (
                    <Form>
                        { !!errors && !!errors.length &&
                            errors.map((err, index) => <div className="text-danger" key={index}>{err}</div>)
                        }
                        <FormGroup>
                            <Label for="emailAddress">Email Address</Label>
                            <Input name="emailAddress" id="emailAddress"  disabled value={ email } />
                            
                            <ErrorMessage name="emailAddress" component="div" />
                        </FormGroup>
                        <FormGroup>
                            <Label for="phoneNumber">Phone Number</Label>
                            <Field name="phoneNumber" id="phoneNumber" component={Input} onChange={handleChange} />
                            <ErrorMessage name="phoneNumber" component="div" />
                        </FormGroup>
                        <Button type="submit" className="btn btn-primary" disabled={ isSubmitting }>Save</Button>
                    </Form>
                )}
            </Formik>
        </Col>
    )
}

export default Profile