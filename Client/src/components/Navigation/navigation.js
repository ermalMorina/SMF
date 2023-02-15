import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container, Nav, Navbar, NavbarBrand, Image } from 'react-bootstrap';
import Logo from '../../images/textlogo.svg';
import { Link } from 'react-router-dom';
import './navigation.css'

function Navigation() {

    function MouseOver(event) {
        event.target.style.color = '#35AD79';
    }

    function MouseOut(event) {
        event.target.style.color = '';
    }

    return (
        <Navbar className='nav__container' variant='dark'>
            <Container className='d-flex justify-content-between navbar'>
                <NavbarBrand>
                    <Link to='/'>
                        <Image src={Logo} width='150px' />
                    </Link>
                </NavbarBrand>

                <Nav>
                    <Nav.Link onMouseOver={MouseOver} onMouseOut={MouseOut} href='/'>Home</Nav.Link>
                    <Nav.Link onMouseOver={MouseOver} onMouseOut={MouseOut} href='/matches'>Matches</Nav.Link>
                    <Nav.Link onMouseOver={MouseOver} onMouseOut={MouseOut} href='/results'>Results</Nav.Link>
                    <Nav.Link onMouseOver={MouseOver} onMouseOut={MouseOut} href='/teams'>Teams</Nav.Link>
                    <Nav.Link onMouseOver={MouseOver} onMouseOut={MouseOut} href='/about'>About</Nav.Link>
                    <Nav.Link onMouseOver={MouseOver} onMouseOut={MouseOut} href='/register'>Register</Nav.Link>
                    <Nav.Link onMouseOver={MouseOver} onMouseOut={MouseOut} href='/login'>LogIn</Nav.Link>
                </Nav>
            </Container>
        </Navbar>
    );
};

export default Navigation;