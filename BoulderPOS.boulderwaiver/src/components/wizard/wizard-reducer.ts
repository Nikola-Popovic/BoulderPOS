enum WizardActions {
    NEXT = 'NEXT',
    PREC = 'PREC'
}

interface IWizardSectionAction {
    type : WizardActions
}

interface IWizardSectionState {
    isLoading: boolean,
    isValid: boolean,
    step : number,
}

const initialState : IWizardSectionState = {
    isLoading: true,
    isValid: false,
    step : 0
}


function stepReducer(state : IWizardSectionState, action : IWizardSectionAction) {
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

export { stepReducer, WizardActions, initialState };
export type { IWizardSectionState, IWizardSectionAction };
