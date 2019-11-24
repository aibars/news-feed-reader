import * as React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import '../styles/Feeds.css';
import { service } from '../services';
import moment from 'moment';

class Feeds extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      username: '',
      feeds: [],
      feedUrl: ''
    };

    this.registerFeed = this.registerFeed.bind(this);
    this.handleKeyPress = this.handleKeyPress.bind(this);
  }
 
  handleKeyPress = (e) => {
    if (e.charCode === 13) {
      this.registerFeed();
    }
  }

  registerFeed = () => {
    if (this.state.message === '') return;
    alert('registered url:', this.state.feedUrl)
  };

  render() {
    return (
      <div className="feeds-box">
        <div id="status-line">
          <input className="login-input"
            type="text"
            onKeyPress={this.handleKeyPress}
            value={this.state.feedUrl}
            onChange={e => this.setState({ feedUrl: e.target.value })}
          />

          <button className="login-btn subscribe-btn" onClick={this.registerFeed}>Subscribe</button>
          <span className="login-label">Logged in as: <label>{this.props.user.userName}</label>
            <Link onClick={() => localStorage.removeItem('user')} className="logout-label" to="/login">Logout</Link>
          </span>
        </div>
        {/* create new component */}
        <div> 
          {this.state.feeds.map((item, index) => (
            <span className={"feed-line-" + (index % 2 === 0 ? "even" : "odd")}
              key={index}>{item.userName}: {item.text}  <label className="send-date">{isToday(item.sendDate) ? moment(item.sendDate).format('hh:mm') : moment(item.sendDate).format('YYYY/DD/MM hh:mm')}</label>
            </span>
          ))}
        </div>
        {/* ------- */}
      </div>
    );
  }
}

const connectedFeeds = connect((state) => {
  const { authentication, messages } = state;
  const { user } = authentication;
  return { user, messages };
})(Feeds);

export { connectedFeeds as Feeds };

const isToday = (input) => {
  const today = new Date();
  const dateInput = new Date(input);
  return dateInput.getDate() === today.getDate() &&
    dateInput.getMonth() === today.getMonth() &&
    dateInput.getFullYear() === today.getFullYear();
}