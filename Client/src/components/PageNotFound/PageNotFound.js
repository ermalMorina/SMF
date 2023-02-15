import React from 'react'
import NotFound from "../../images/404.jpg"
import "./PageNotFound.css";
import { Link } from "react-router-dom";
import Navigation from "../Navigation/navigation";

function PageNotFound() {
    return (
        <>
            {/* <Navigation /> */}
            <div className='notfound'>
                <div>
                    <img src={NotFound} width="70%" />
                </div>

                <div id="info">
                    <h1>This page could not be found</h1>
                    <p className='paragraph'>Something went wrong or the page you requested does not exist. Please go back to the home page!</p>
                    <Link className='button' to="/">
                        Back to home page
                    </Link>
                </div>
            </div >
        </>

    )
}

export default PageNotFound