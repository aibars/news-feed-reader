let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? {
  loggedIn: true,
  user,
  errors: ''
} : {};

const constants = {
  LOGIN_REQUEST: 'LOGIN_REQUEST',
  LOGIN_SUCCESS: 'LOGIN_SUCCESS',
  LOGIN_FAILURE: 'LOGIN_FAILURE',
  REGISTER_SUCCESS: 'REGISTER_SUCCESS',
  REGISTER_FAILURE: 'REGISTER_FAILURE',
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
    case constants.REGISTER_SUCCESS:
      return {
        ...initialState,
        loggedIn: true,
        user: action.user
      };
    case constants.REGISTER_FAILURE:
      return {
        ...state,
        errors: action.error
      };
    default:
      return state
  }
}
const initialFeeds = { feeds: [], visibleItems: [] };

export function items(state = initialFeeds, action) {
  switch (action.type) {
    case constants.GET_FEEDS_REQUEST:
      return state;
    case constants.GET_FEEDS_SUCCESS:
      return {
        feeds: action.feeds,
        visibleItems: action.feeds
      };
    case constants.GET_FEEDS_FAILURE:
      return state;
    case 'FILTER_FEEDS':
      return {
        ...state,
        visibleItems: action.feeds
      };
    default:
      return state;
  }
}