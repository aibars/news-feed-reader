import React from 'react';
import '../styles/Login.css';
import { connect } from 'react-redux';
import { register } from '../actionCreators';

class Register extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            username: '',
            password: '',
            submitted: false
        };

        this.submitRegister = this.submitRegister.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    submitRegister(e) {
        e.preventDefault();
        this.setState({ submitted: true });
        const { username, password } = this.state;
        if (username && password) {
            this.props.register(username, password);
        }
    }

    handleChange(e) {
        const { name, value } = e.target;
        this.setState({ [name]: value });
    }

    render() {
        return (
            <div className="box-container">
                <div className="inner-container">
                    <div className="header">
                        Register
                    </div>
                    <div className="box">
                        <div className="input-group">
                            <label htmlFor="username">Username</label>
                            <input
                                type="text"
                                name="username"
                                className="login-input"
                                placeholder="Username"
                                value={this.state.username}
                                onChange={this.handleChange} />
                        </div>
                        <div className="input-group">
                            <label htmlFor="password">Password</label>
                            <input
                                type="password"
                                name="password"
                                className="login-input"
                                placeholder="Password"
                                value={this.state.password}
                                onChange={this.handleChange} />
                        </div>
                        <button
                            type="button"
                            className="login-btn"
                            onClick={this.submitRegister}>Create New Account</button>
                        <br />
                        {this.props.errors &&
                            <span className="danger-error">{this.props.errors}</span>}
                    </div>
                </div>
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    const { authentication } = state;
    const { errors } = authentication;

    return { errors };
};
const actionCreators = {
    register: register,
};

const connectedLoginPage = connect(mapStateToProps, actionCreators)(Register);
export { connectedLoginPage as Register };