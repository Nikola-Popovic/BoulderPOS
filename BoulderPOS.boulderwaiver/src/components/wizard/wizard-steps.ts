import React from 'react';

export const WIZARD_STEPS = {
    1 : React.lazy(() => import('./wizard-pages/step1-getting-started')),
};