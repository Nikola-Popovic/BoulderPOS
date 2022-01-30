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

export { WizardActions, initialState };
export type { IWizardSectionState, IWizardSectionAction };
