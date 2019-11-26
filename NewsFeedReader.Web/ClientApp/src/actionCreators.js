import { service } from './services';
import { history } from './history';

const constants = {
    LOGIN_REQUEST: 'LOGIN_REQUEST',
    LOGIN_SUCCESS: 'LOGIN_SUCCESS',
    LOGIN_FAILURE: 'LOGIN_FAILURE',
    GET_FEEDS_REQUEST: 'GET_FEEDS_REQUEST',
    GET_FEEDS_SUCCESS: 'GET_FEEDS_SUCCESS',
    GET_FEEDS_FAILURE: 'GET_FEEDS_FAILURE',
};

export function login(username, password) {
    return dispatch => {
        dispatch(request({ username }));

        service.login(username, password)
            .then(
                user => {
                    dispatch(success(user));
                    history.push('/');
                },
                error => {
                    dispatch(failure(error.toString()));
                }
            );

        function request(user) {
            return { type: constants.LOGIN_REQUEST, user }
        }

        function success(user) {
            return { type: constants.LOGIN_SUCCESS, user }
        }

        function failure(error) {
            return { type: constants.LOGIN_FAILURE, error }
        }
    };
}

export function getFeeds() {
    return dispatch => {
        dispatch(request());

        service.getFeeds()
            .then(
                data => dispatch(success(data)),
                error => dispatch(failure(error.toString()))
            );
    };

    function request() {
        return { type: constants.GET_FEEDS_REQUEST }
    }

    function success(feeds) {
        return { type: constants.GET_FEEDS_SUCCESS, feeds }
    }

    function failure(error) {
        return { type: constants.GET_FEEDS_FAILURE, error }
    }
}

export function subscribeToFeed(url) {
    return dispatch => {
        dispatch(request());

        service.subscribeToFeed(url)
            .then(
                () => this.getFeeds(),
                error => dispatch(failure(error.toString()))
            );
    };

    function success() {
        return { type: 'GET_FEEDS_REQUEST' }
    }

    function request() {
        return { type: 'SUBSCRIBE_REQUEST' }
    }

    function failure(error) {
        return { type: 'SUBSCRIBE_FAILURE', error }
    }
}

export function filterFeeds(feeds) {
    return dispatch =>
        dispatch({ type: 'FILTER_FEEDS', feeds });
};