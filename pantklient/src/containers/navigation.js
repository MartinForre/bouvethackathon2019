import React from 'react';
import { Route, Switch } from 'react-router-dom';

// Pages
import AboutComponent from '../components/AboutComponent';
import RegisterBagComponent from '../components/RegisterBagComponent';
import QrScannerComponent from '../components/QrScannerComponent';

export default () => (
  <Switch>
    <Route
      exact
      path='/'
      component={QrScannerComponent}
    />
    <Route
      path='/about'
      component={AboutComponent}
    />
    <Route
      path='/registerBag/:id'
      component={RegisterBagComponent}
    />
  </Switch>
);
