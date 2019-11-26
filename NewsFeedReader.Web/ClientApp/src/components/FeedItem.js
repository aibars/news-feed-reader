import React from 'react';
import moment from 'moment';
import '../styles/Feeds.css';

class FeedItem extends React.Component {
    render() {
        let index = this.props.id;
        let item = this.props.item;
        return (
            <div>
                <a className="item-link" href={item.link}>
                    <span className={"feed-line-" + (index % 2 === 0 ? "even" : "odd")}>{item.title + " "} 
                        <label className="send-date">Published on: {isToday(item.sendDate) ? moment(item.sendDate).format('hh:mm') : moment(item.sendDate).format('YYYY/DD/MM hh:mm')}</label>
                    </span>
                </a>
            </div>
        );
    }
}

export default FeedItem;

const isToday = (input) => {
    const today = new Date();
    const dateInput = new Date(input);
    return dateInput.getDate() === today.getDate() &&
        dateInput.getMonth() === today.getMonth() &&
        dateInput.getFullYear() === today.getFullYear();
}