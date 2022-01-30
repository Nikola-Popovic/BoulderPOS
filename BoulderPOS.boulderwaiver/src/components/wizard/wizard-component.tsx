import Button from '@mui/material/Button';
import React, { Suspense, useEffect, useMemo, useReducer } from 'react';
import { useTranslation } from 'react-i18next';
import { stepReducer, WizardActions, initialState, IWizardSectionAction } from './wizard-reducer';
import i18n from '../../i18next';

const sections = [
    React.lazy(() => import('./wizard-pages/step1-getting-started')),
    React.lazy(() => import('./wizard-pages/step2-terms-and-cond')),
    React.lazy(() => import('./wizard-pages/step3-signup')),
];

export interface IWizardSectionProps {
    dispatch: React.Dispatch<IWizardSectionAction>
} 

export function WizardComponent() {
    const { t } = useTranslation(undefined, { i18n });
    const [state, dispatch] = useReducer(stepReducer, initialState);
    
    const WizardStep = useMemo(() => sections[state.step], [sections[state.step]]);

    const submit = () => {
        return;
    }
    
    const canPrev = () => state.step > 0;
    const nextStep = () => dispatch({type : WizardActions.NEXT});
    const prevStep = () => dispatch({type : WizardActions.PREC});
    const canNext = () => state.step < sections.length && state.isValid;

    return <Suspense fallback={<div>Chargement...</div>}>
        <div className="wizard-box">
            <WizardStep dispatch={dispatch}/>
            { state.step !==0 &&
            <div className='wizard-bottom'>
                <Button onClick={prevStep} disabled={!canPrev()} variant='outlined'> 
                    {t('prev')}
                </Button>
                {
                    state.step === sections.length - 1 ? 
                    <Button onClick={nextStep} disabled={!canNext()} color='primary' variant='outlined'> 
                        {t('next')}
                    </Button>
                    :   
                    <Button onClick={submit} disabled={!canNext()} color='primary' variant='outlined' >
                        {t('submit')}
                    </Button>
                }
            </div>
            }
        </div>
    </Suspense>
}
