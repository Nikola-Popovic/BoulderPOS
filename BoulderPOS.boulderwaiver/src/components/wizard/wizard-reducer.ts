import { WizardActions, IWizardSectionState, IWizardSectionAction } from './';

export function stepReducer(state : IWizardSectionState, action : IWizardSectionAction) {
    switch (action.type) {
        case WizardActions.NEXT :
            return {
                ...state,
                step: state.step + 1,
                isValid: false
            }
        case WizardActions.PREC :
            return {
                ...state,
                step: state.step - 1,
                isValid: true
            }
        default:
            return state;
    }
}