import { authHeader } from './auth-header';

export const service = {
    login,
    getFeeds: getFeeds,
    subscribeToFeed: subscribeToFeed,
};

function login(username, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, password })
    };

    return fetch('/api/Account/login', requestOptions)
        .then(handleResponse)
        .then(user => {
            localStorage.setItem('user', JSON.stringify(user));

            return user;
        });
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                window.location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}

function getFeeds() {
    const requestOptions = {
        method: 'GET',
        headers: authHeader(),
    };

    return fetch('/api/feeds/user', requestOptions)
        .then(handleResponse);
}

function subscribeToFeed(url) {
    const requestOptions = {
        method: 'POST',
        headers:
            Object.assign(authHeader(),
                { 'Content-Type': 'application/json' }),
        body: JSON.stringify({ url })
    };

    return fetch('/api/feeds/subscribe', requestOptions)
        .then(handleResponse);
}