import React from 'react';
import {
    Button,
    FormLabel,
    LinearProgress,
    Grid,
    FormGroup
  } from '@mui/material';
import { useHistory } from "react-router-dom";
import { Field, Form, Formik, FormikHelpers } from 'formik';
import { TextField, Checkbox } from 'formik-mui';
import * as Yup from 'yup';
import { NewCustomer } from '../../../payload';
import { CustomerService } from '../../../services/api';
import { DatePickerField } from '../../DatePickerField';
import { PhoneInputField } from '../../PhoneInputField';
import { useSnackbar } from 'notistack';
import { useTranslation } from 'react-i18next';
import i18n from '../../../i18next';
import { IWizardSectionProps } from '../wizard-component';

const phoneRegExp = /^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/


function Signup(props : IWizardSectionProps) {
    const { t } = useTranslation('translation', { i18n });
    const userSchema = Yup.object().shape({
        FirstName : Yup.string().required(t("enterValue")),
        LastName : Yup.string().required(t("enterValue")),
        Email : Yup.string().email().required(t("emailInvalid")),
        PhoneNumber : Yup.string().matches(phoneRegExp, t("phoneInvalid")).required(), 
        BirthDate : Yup.date().notRequired(),
        NewsletterSubscription : Yup.boolean().default(false)
    });

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
        // console.log(`Submitting : ${values}`)
        const promise = CustomerService.postNewCustomer(values);
        promise.then( _response => {
            formikHelpers.setSubmitting(false);
            // Notify success
            enqueueSnackbar(t('signupComplete'), {
                variant: 'success',
            });
            history.push('/');
        }).catch( error => {
            formikHelpers.setSubmitting(false);
            // Notify alert
            console.log(error);
            enqueueSnackbar(t('errorTryAgain'), {
                variant: 'error',
            });
        });
    }

    return <>
        <Grid
            container
            direction='column'
            justifyContent='center'
            alignItems='center'
        >
            <h2> {t("signup")} </h2>
            <Formik
                initialValues={initialValues}
                validationSchema={userSchema}
                onSubmit={handleFormSubmit}
            >
                {({ isSubmitting, handleSubmit }) => 
                    <Form className='form-box' onSubmit={handleSubmit}>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="FirstName">{t("firstName")}</FormLabel>
                            <Field name="FirstName" type="text" component={TextField}/>
                        </FormGroup>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="LastName">{t("lastName")}</FormLabel>
                            <Field name="LastName" type="text" component={TextField}/>
                        </FormGroup>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="Email">{t("email")}</FormLabel>
                            <Field name="Email" type="text" component={TextField}/>
                        </FormGroup>
                        <FormGroup className='form-group'>
                            <FormLabel htmlFor="Birthdate">{t("birthDate")}</FormLabel>
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
                            <Button type="reset" variant='outlined' disabled={isSubmitting}>{t("reset")}</Button>
                            <Button type="submit" variant='outlined' color='primary' disabled={isSubmitting}>{t("submit")}</Button>
                        </div>
                        {isSubmitting && <LinearProgress color='primary' />}
                    </Form>
                }
                
            </Formik>
        </Grid>
    </>;
}

export default Signup;