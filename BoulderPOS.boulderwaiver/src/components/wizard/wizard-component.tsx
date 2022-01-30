import React, { Suspense, useMemo, useReducer } from 'react';
import { useTranslation } from 'react-i18next';
import { stepReducer, initialState, IWizardSectionAction } from './';
import i18n from '../../i18next';
import { WIZARD_STEPS } from './wizard-steps';

const sections = WIZARD_STEPS;

export interface IWizardSectionProps {
    dispatch: React.Dispatch<IWizardSectionAction>
} 

export function WizardComponent() {
    const { t } = useTranslation(undefined, { i18n });
    const [state, dispatch] = useReducer(stepReducer, initialState);
    
    const WizardStep = useMemo(() => sections[state.step], [sections[state.step]]);

    return <Suspense fallback={<div>{t('loading')}...</div>}>
        <WizardStep dispatch={dispatch}/>
    </Suspense>
}
