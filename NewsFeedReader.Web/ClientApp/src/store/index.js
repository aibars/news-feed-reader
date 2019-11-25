let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? {
  loggedIn: true,
  user
} : {};

const constants = {
  LOGIN_REQUEST: 'LOGIN_REQUEST',
  LOGIN_SUCCESS: 'LOGIN_SUCCESS',
  LOGIN_FAILURE: 'LOGIN_FAILURE',
  GET_FEEDS_SUCCESS: 'GET_FEEDS_SUCCESS',
  GET_FEEDS_FAILURE: 'GET_FEEDS_FAILURE',
  GET_FEEDS_REQUEST: 'GET_FEEDS_REQUEST'
};

export function authentication(state = initialState, action) {
  switch (action.type) {
    case constants.LOGIN_REQUEST:
      return {
        ...initialState,
        loggingIn: true,
        user: action.user
      };
    case constants.LOGIN_SUCCESS:
      return {
        ...initialState,
        loggedIn: true,
        user: action.user
      };
    case constants.LOGIN_FAILURE:
      return {};
    default:
      return state
  }
}

export function feeds(state = [], action) {
  switch (action.type) {
    case constants.GET_FEEDS_REQUEST:
      return state;
    case constants.GET_FEEDS_SUCCESS:
      return action.feeds;
    case constants.GET_FEEDS_FAILURE:
      return state;
    default:
      return state;
  }
}