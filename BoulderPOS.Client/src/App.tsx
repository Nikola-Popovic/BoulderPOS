import React from 'react';
import './App.css';
import NavBar from './components/navbar/navbar';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { SnackbarProvider } from 'notistack';
import Home from './components/home';
import { Shop } from './components/shop';
import { CheckIn } from './components/checkin/checkin';
import { Administration } from './components/administration/administration';

function App() {
  return (
    <Router>
      <SnackbarProvider maxSnack={2}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'center'
        }}>
        <NavBar/>
        <Switch>
          <Route path='/' exact component={Home}/>
          <Route exact path='/shop/:clientId' component={Shop}/>
          <Route path='/shop' component={Shop}/>
          <Route path='/checkin' component={CheckIn}/>
          <Route path='/administration' component={Administration}/>
        </Switch>
      </SnackbarProvider>
    </Router>
  );
}

export default App;
