import React from 'react';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import '../styles/Feeds.css';
import { getFeeds, subscribeToFeed, filterFeeds } from '../actionCreators';
import FeedItem from './FeedItem';

class Feeds extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      feedUrl: '',
      searchText: '',
    };

    this.registerFeed = this.registerFeed.bind(this);
    this.handleKeyPress = this.handleKeyPress.bind(this);
    this.searchFeeds = this.searchFeeds.bind(this);
  }

  componentDidMount() {
    this.props.getFeeds();
  }

  handleKeyPress = (e) => {
    if (e.charCode === 13) {
      this.registerFeed();
    }
  }

  searchFeeds = (val) => {
    this.setState({ searchText: val });
    var filtered = this.props.feeds.filter(f =>
      f.title.toLowerCase().includes(val.toLowerCase())
    );
    this.props.filterFeeds(filtered);
  };

  registerFeed = () => {
    if (this.state.feedUrl === '') return;
    this.props.subscribeToFeed(this.state.feedUrl);
  };

  render() {
    let feeds = this.props.visibleItems;
    return (
      <div className="feeds-box">
        <div id="status-line">
          <input className="login-input"
            type="text"
            onKeyPress={this.handleKeyPress}
            value={this.state.feedUrl}
            onChange={e => this.setState({ feedUrl: e.target.value })}
          />

          <button className="login-btn subscribe-btn" onClick={this.registerFeed}>Subscribe to RSS</button>
          <span className="login-label">Logged in as: <label>{this.props.user.userName}</label>
            <Link onClick={() => localStorage.removeItem('user')} className="logout-label" to="/login">Logout</Link>
          </span>

          <br></br>
          <input className="login-input"
            type="text"
            value={this.state.searchText}
            placeholder="Search..."
            onChange={e => this.searchFeeds(e.target.value)}
          />

          <button className="button" id="refresh-btn"
            onClick={() => {
              this.setState({ searchText: '' });
              this.props.getFeeds()
            }}>
            Refresh
          </button>

        </div>
        {feeds.map((item, index) => <FeedItem
          key={index}
          id={index}
          item={item}>
        </FeedItem>)}
      </div>
    );
  }
}

const mapDispatchToProps = {
  getFeeds,
  subscribeToFeed,
  filterFeeds
};

const connectedFeeds = connect((state) => {
  const { authentication, items } = state;
  const { user } = authentication;
  const { feeds, visibleItems } = items;
  return { user, feeds, visibleItems };
}, mapDispatchToProps)(Feeds);

export { connectedFeeds as Feeds };