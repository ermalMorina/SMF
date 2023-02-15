import React from "react";
import Footer from "../Footer/footer";
import Navigation from "../Navigation/navigation";
import './home.css';

function Home() {
    return (
        <div>
            <Navigation />
            <div className="home__container">
                <div className="home__header" >
                    <h3>Welcome to</h3>
                    <h1>Spartan Manage Football</h1>
                </div>
            </div>
            <br />
            <Footer />     
        </div>
    );
}

export default Home;