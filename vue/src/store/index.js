import { createStore as _createStore } from 'vuex';
import axios from 'axios';
const NOTIFICATION_TIMEOUT = 6000;
export function createStore(currentToken, currentUser) {
  let store = _createStore({
    state: {
      token: currentToken || '',
      user: currentUser || {},
      notification: null,
    },
    mutations: {
      SET_AUTH_TOKEN(state, token) {
        state.token = token;
        localStorage.setItem('token', token);
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
      },
      SET_USER(state, user) {
        state.user = user;
        localStorage.setItem('user', JSON.stringify(user));
      },
      LOGOUT(state) {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
        state.token = '';
        state.user = {};
        axios.defaults.headers.common = {};
      },
      SET_NOTIFICATION(state, notification) {
        // Lets clear the current notification if one exists
        if (state.notification) {
          this.commit('CLEAR_NOTIFICATION');
        }
        // If String is only sent, we can create a notification object with default values
        if (typeof notification === 'string') {
          notification = {
            message: notification,
            type: 'error',
            timeout: NOTIFICATION_TIMEOUT
          }
        } else {
          // Else add default values if needed
          notification.type = notification.type || 'error';
          notification.timeout = notification.timeout || NOTIFICATION_TIMEOUT;
        }

        // Set the notification in state
        state.notification = notification;

        // Clear the message after timeout
        notification.timer = window.setTimeout(() =>{
          this.commit('CLEAR_NOTIFICATION');
        }, notification.timeout);
      },
      CLEAR_NOTIFICATION(state) {
        if (state.notification && state.notification.timer) {
          window.clearTimeout(state.notification.timer);
        }
        state.notification = null;
      },
    },
  });
  return store;
}
