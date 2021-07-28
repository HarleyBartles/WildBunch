import { Field } from 'formik';
import React, { Fragment } from 'react'
import { Label } from 'reactstrap';

const ButtonGroupField = (props) => {
    const styles = {
        display: "flex",
        marginBottom: "20px",
        overflow: "hidden"
    }

    return <div className="button-group-field">{ props.children }</div>
}

const RadioButtonGroup = (props) => {
    const { label, name, options, ...rest } = props;
    
    return (

        <Fragment>
            <Label for={name}>{label}</Label>
            <ButtonGroupField>
                <Field name={name} id={name} {...rest} >
                    {
                        ({ field }) => {
                            return options.map(opt => {
                                const optId = `${name}-radio_${opt.key}`
                                return (
                                    <Fragment key={opt.key}>
                                        <input
                                            {...field}
                                            type="radio"
                                            id={optId}
                                            value={opt.value}
                                            checked={field.value == opt.value}
                                        />
                                        <Label for={optId}>{opt.key}</Label>
                                    </Fragment>
                                )
                            })
                        }
                    }
                    </Field>
            </ButtonGroupField>
        </Fragment>
    )
}

export default RadioButtonGroup