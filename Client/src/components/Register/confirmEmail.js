import React from 'react';
import { Button } from 'react-bootstrap'
import './confirmEmail.css';

export default function confirmEmail() {
    return (
        <div className='confirm__container'>
            <h1>Email has been confirmed! 🥳🙌</h1>
            <Button className='redirect' type='submit' href='/login'>
                Redirect to login page
            </Button>
        </div>
    )
}