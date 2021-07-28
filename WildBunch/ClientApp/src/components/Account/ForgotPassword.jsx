import React, { Fragment } from "react"
import { useSelector } from "react-redux";
import { ErrorMessage, Field, Formik } from "formik";
import { Button, Col, Form, FormGroup, Row, Label, Input } from "reactstrap"

const ForgotPassword = (props) => {
    const errors = useSelector(state => state.account.errors.forgotPassword)

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
        setSubmitting(false)
    }

    return (
        <Fragment>
            <h1>Forgot your password?</h1>
            <Row>
                <Col md='4'>
                    <section>
                        <h4>Enter your email.</h4>
                        <hr />
                        <Formik
                            initialValues={{ email: '' }}
                            validate={(values) => validate(values)}
                            onSubmit={(values, { setSubmitting }) => handleSubmit(values, setSubmitting)}
                        >
                            {({ isSubmitting, handleChange }) => (
                                <Form>
                                    { !!errors && !!errors.length &&
                                        errors.map((err, index) => <div className="text-danger" key={index}>{err}</div>)
                                    }
                                    <FormGroup>
                                        <Label asp-for="email"></Label>
                                        <Field name="email" id="email" type="email" component={Input} onChange={handleChange} />
                                        <ErrorMessage name="email" component="div" className="text-danger" />
                                    </FormGroup>
                                    <Button type="submit" color="primary" disabled={ isSubmitting }>Go</Button>
                                </Form>)}
                        </Formik>
                    </section>
                </Col>
            </Row>
        </Fragment>
    )
}

export default ForgotPassword