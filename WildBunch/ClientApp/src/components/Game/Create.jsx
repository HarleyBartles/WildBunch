import React, { Fragment, useEffect } from 'react'
import { ButtonGroup, Button, Col, FormGroup, Input, Label, Row } from 'reactstrap'
import { Formik, Form, Field, ErrorMessage } from 'formik'
import { useDispatch, useSelector } from 'react-redux'
import { useHistory } from 'react-router'
import { createGame } from '../../actionhandlers/game'
import RadioButtonGroup from '../Common/RadioButtonGroup'

const CreateGame = () => {
    const dispatch = useDispatch()
    const history = useHistory()
    const { activeGame, errors: { create:errors } } = useSelector(state => state.game)

    useEffect(() => {
        if (!!activeGame) {
            history.push(`/game/${activeGame.gameId}/map`)
        }
        
    }, [activeGame])

    function handleSubmit(values, setSubmitting) {
        const { characterName, difficulty } = values

        dispatch(createGame(characterName, difficulty))

        setSubmitting(false)
    }

    function validate(values) {
        const errors = {};

        if (!values.characterName) {
            errors.characterName = 'Required';
        }

        if (values.difficulty < 1 || values.difficulty > 3) {
            errors.difficulty = 'Select a valid difficulty level'
        }

        return errors;
    }

    const radioOptions = [
        { key: "Easy", value: 1 },
        { key: "Medium", value: 2 },
        { key: "Hard", value: 3 },
    ]

    return (
        <Fragment>
            <h1>New Game</h1>
            <Row>
                <Col md="4">
                    <Formik
                        initialValues={{ characterName: '', difficulty: 0 }}
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
                                    <Label for="characterName">Character Name</Label>
                                    <Field type="text" name="characterName" id="characterName" component={Input} onChange={handleChange} />
                                    <ErrorMessage name="characterName" component="div" className="text-danger" />
                                </FormGroup>
                                <FormGroup>
                                    <RadioButtonGroup
                                        label="Difficulty"
                                        name="difficulty"
                                        options={radioOptions}
                                    />
                                    <ErrorMessage name="difficulty" component="div" className="text-danger" />
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

export default CreateGame