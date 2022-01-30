import React from 'react';

export const WIZARD_STEPS = [
    React.lazy(() => import('../sections/GettingStarted')),
    React.lazy(() => import('../sections/Waiver')),
    React.lazy(() => import('../sections/Signup'))
];