import React, { PureComponent } from 'react';
import { Link } from 'react-router-dom';
import { connect } from 'react-redux';

import {
    registerRequest
} from './register.actions.js';

import './register.scss';

class RegisterContainer extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            phone: '',
            password: '',
        }

        this.userPhoneChange = this.userPhoneChange.bind(this);
        this.userPasswordChange = this.userPasswordChange.bind(this);
        this.userPasswordConfirmationChange = this.userPasswordConfirmationChange.bind(this);
        this.Clear = this.Clear.bind(this);
        this.registerRequest = this.registerRequest.bind(this);
    }

    userPhoneChange(e) {
         const value = e.target.value;
         /*
        if(!true) {
            this.userPhone.value = this.state.phone;
            return;
        } */

        this.setState({
            phone: value,
        })
    }

    userPasswordChange(e) {
        this.setState({
            password: e.target.value,
        })
    }

    userPasswordConfirmationChange(e) {
        this.userPasswordConfirmNode.innerHTML = "";

        if(this.state.password !== e.target.value)
            this.userPasswordConfirmNode.innerHTML = "The password isn't match.";
    }

    Clear() {
        this.userPhone.value = "";
        this.userPassword.value = "";
        this.userPasswordConfirm.value = "";
        this.userPasswordConfirmNode.innerHTML = "";

        this.setState({
            phone: '',
            password: '',
        })
    }

    registerRequest() {
        
        const userData = {
            phone: this.state.phone,
            password: this.state.password,
        }
        this.props.registerRequest(userData);
    }

    render() {
        return (
            <div className='register__page'>
            <div className='container register'>
                <div className='register__title'><h2>Register</h2></div>
                <div className='register__form'> 
                    
                        <div className='register__block'>
                            <input className='register__block-input'
                                type='text' name='register'
                                ref={curr => this.userPhone = curr}
                                onChange={this.userPhoneChange} 
                                placeholder='Phone'
                                />
                        </div>
                        <div className='register__message'>
                            <label className='register__message-label'
                                ref={curr => this.userPhoneNode = curr}>
                            </label>
                        </div>
                            
                        <div className='register__block'>
                            <input className='register__block-input'
                                type='password' name='password'
                                ref={curr => this.userPassword = curr}
                                onChange={this.userPasswordChange} 
                                placeholder='Password'
                                />
                        </div>
                        <div className='register__message'>
                            <label className='register__message-label'
                                ref={curr => this.userPasswordNode = curr}>
                            </label>
                        </div>

                        <div className='register__block'>
                            <input className='register__block-input'
                                type='password' name='confirmPassword'
                                ref={curr => this.userPasswordConfirm = curr}
                                onChange={this.userPasswordConfirmationChange} 
                                placeholder='Confirm password'
                                />
                        </div>
                        <div className='register__message'>
                            <label className='register__message-label'
                                ref={curr => this.userPasswordConfirmNode = curr}>
                            </label>
                        </div>

                        

                    </div> 
                    <div className='register__buttons'>
                    <button className="register__block-btn" onClick={this.registerRequest}>
                        Register
                    </button> 
                    <Link className="register__button__link" to='/login'> Have an account </Link> 
                    </div>  
                     
                </div>
            </div>
        );
    }
}

const mapDispatchToProps = dispatch => {
    return {
        registerRequest: (data) => {
            dispatch(registerRequest(data));
        }
    }
}


export default RegisterContainer = connect(
    null, mapDispatchToProps)(RegisterContainer);
