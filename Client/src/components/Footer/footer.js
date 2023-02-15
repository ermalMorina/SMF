import React from "react"
import { Container } from "react-bootstrap";
import spartanlogo from "../../images/textlogo.svg"
import './footer.css';
import { BsFacebook } from 'react-icons/bs';
import { BsInstagram } from 'react-icons/bs';
import { BsTwitter } from 'react-icons/bs';
import { BsLinkedin } from 'react-icons/bs';

function Footer() {

    return (
        < div className="footer__container container-fluid text-center text-md-left" >
            <Container>
                <div className="row">
                    <div className="col-md-6 mt-md-0 mt-3">
                        <img src={spartanlogo} width='20%' />
                        <h5 className="text-uppercase">Manage Football</h5>
                        <p>You can see all the fixtures and leagues on our platform where we display International Football History and Statistics. We inform you about sports by making special posts for football clubs.</p>
                    </div>

                    <hr className="clearfix w-100 d-md-none pb-0" />

                    <div className="col-md-3 mb-md-0 mb-3">
                        <h5 className="text-uppercase">Quick Links</h5>
                        <ul className="list-unstyled">
                            <li><a href="#!">Home</a></li>
                            <li><a href="#!">Matches</a></li>
                            <li><a href="#!">Results</a></li>
                            <li><a href="#!">Teams</a></li>
                        </ul>
                    </div>

                    <div className="col-md-3 mb-md-0 mb-3">
                        <h5 className="text-uppercase">Contact Us</h5>
                        <ul className="list-unstyled">
                            <li><a href="#!"><BsFacebook /></a></li>
                            <li><a href="#!"><BsInstagram /></a></li>
                            <li><a href="#!"><BsTwitter /></a></li>
                            <li><a href="#!"><BsLinkedin /></a></li>
                        </ul>
                    </div>
                </div>
            </Container>
        </div >
    );
};

export default Footer;