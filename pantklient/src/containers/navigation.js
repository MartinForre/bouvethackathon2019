import React from 'react';
import { Route, Switch } from 'react-router-dom';

// Pages
import ProfileComponent from '../components/ProfileComponent/ProfileComponent';
import RegisterBagComponent from '../components/RegisterBagComponent/RegisterBagComponent';
import QrScannerComponent from '../components/QrScannerComponent/QrScannerComponent';
import Login from '../components/Login/Login';
import RegisterUser from '../components/RegisterUser/RegisterUser';
import Home from '../components/Home/Home';

export default () => (
  <Switch>
    <Route
      exact
      path='/'
      component={QrScannerComponent}
    />
    <Route
      path='/profile'
      component={ProfileComponent}
    />
    <Route
      path='/login'
      component={Login}
    />
    <Route
      path='/registerBag/:id'
      component={RegisterBagComponent}
    />
    <Route
      path='/registerUser'
      component={RegisterUser}
    />
    <Route
      path='/home'
      component={Home}
    />
  </Switch>
);
