import React from 'react';
import './Signup.css';
import {
    Button,
    FormLabel,
    LinearProgress,
    Grid,
    FormGroup
  } from '@material-ui/core';
import { useHistory } from "react-router-dom";
import { Field, Form, Formik, FormikHelpers } from 'formik';
import { TextField, Checkbox } from 'formik-material-ui';
import * as Yup from 'yup';
import { NewCustomer } from '../payload';
import { CustomerService } from '../services/api';
import { DatePickerField } from './DatePickerField';
import { PhoneInputField } from './PhoneInputField';
import { useSnackbar } from 'notistack';

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/

const userSchema = Yup.object().shape({
    FirstName : Yup.string().required('Please enter a first name'),
    LastName : Yup.string().required('Please enter a last name'),
    Email : Yup.string().email().required('Please enter a valid email.'),
    PhoneNumber : Yup.string().matches(phoneRegExp, "Phone number is not valid").required(), // change this to required
    BirthDate : Yup.date().notRequired(),
    NewsletterSubscription : Yup.boolean().default(false)
})

function Signup() {
    const history = useHistory();
    const { enqueueSnackbar } = useSnackbar();
    const initialValues = {
        FirstName: '',
        LastName : '',
        Email: '',
        PhoneNumber : '',
        BirthDate : new Date(),
        NewsletterSubscription : false
      };
    
    const handleFormSubmit = (values : NewCustomer, formikHelpers : FormikHelpers<NewCustomer>) => {
        console.log(`Submitting : ${values}`)
        const promise = CustomerService.postNewCustomer(values);
        promise.then( _response => {
            formikHelpers.setSubmitting(false);
            // Notify success
            enqueueSnackbar(`Sign up successful. Thank you ${_response.data.FirstName} !`, {
                variant: 'success',
            });
            history.push('/');
        }).catch( error => {
            formikHelpers.setSubmitting(false);
            // Notify alert
            console.log(error);
            enqueueSnackbar('An error occured. Please try again in a moment.', {
                variant: 'error',
            });
        });
    }

    return <>
        <Grid
            container
            direction='column'
            justify='center'
            alignItems='center'
        >
            <h2> Signup </h2>
            <Formik
                initialValues={initialValues}
                validationSchema={userSchema}
                onSubmit={handleFormSubmit}
            >
                {({ isSubmitting, handleSubmit }) => 
                    <Form className='form-box' onSubmit={handleSubmit}>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="FirstName">First Name</FormLabel>
                            <Field name="FirstName" type="text" component={TextField}/>
                        </FormGroup>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="LastName">Last Name</FormLabel>
                            <Field name="LastName" type="text" component={TextField}/>
                        </FormGroup>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="Email">Email</FormLabel>
                            <Field name="Email" type="text" component={TextField}/>
                        </FormGroup>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="Birthdate">Birthdate</FormLabel>
                            <DatePickerField name="Birthdate" />
                        </FormGroup >
                        <FormGroup className='form-group'>
                            <PhoneInputField name="PhoneNumber" />
                        </FormGroup>
                        <div className='form-group'>
                            <FormLabel htmlFor="NewsletterSubscription">Subscribe to our newsletter</FormLabel>
                            <Field name="NewsletterSubscription" type="checkbox" component={Checkbox}/>
                        </div>
                        <div className='submit-button-group'>
                            <Button type="reset" variant='outlined' color='secondary' disabled={isSubmitting}>Reset</Button>
                            <Button type="submit" variant='outlined' color='primary' disabled={isSubmitting}>Submit</Button>
                        </div>
                        {isSubmitting && <LinearProgress color='primary' />}
                    </Form>
                }
                
            </Formik>
        </Grid>
    </>
}

export default Signup;