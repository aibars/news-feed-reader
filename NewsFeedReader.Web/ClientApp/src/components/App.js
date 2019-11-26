import * as React from 'react';
import { Router, Route, Redirect, Switch } from 'react-router';
import PrivateRoute from 'react-private-route';
import { Feeds } from './Feeds';
import { Login } from './Login';
import { Register } from './Register';
import { history } from '../history';
import '../styles/custom.css'


class App extends React.Component {
    isLoggedIn() {
        return localStorage.user ? true : false;
    }

    render() {
        return (
            <Router history={history}>
                <Switch>
                    <PrivateRoute exact path="/" component={Feeds}
                        isAuthenticated={this.isLoggedIn()} />
                    <Route path="/login" component={Login} />
                    <Route path="/register" component={Register} />
                    <Redirect from="*" to="/" />
                </Switch>
            </Router>
        );
    }

}

export default App;