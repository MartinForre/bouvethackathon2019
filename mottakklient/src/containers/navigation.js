import React from 'react';
import { Route, Switch } from 'react-router-dom';

// Pages
import QrScannerComponent from '../components/QrScannerComponent/QrScannerComponent';
import BagControlComponent from "../components/BagControlComponent/BagControlComponent";

export default () => (
    <Switch>
        <Route
            exact
            path='/'
            component={QrScannerComponent}
        />
        <Route
            exact
            path='/control/:id'
            component={BagControlComponent}
        />
    </Switch>
);
