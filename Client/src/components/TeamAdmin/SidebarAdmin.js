import React, { useState, useEffect } from 'react'
import * as RiIcons from "react-icons/ri"
import { Link } from 'react-router-dom';
import { AdminPages } from './AdminPages';
import './SidebarAdmin.css';
import Logo from '../../images/textlogo.svg';
import { Dropdown, Modal, Button } from "react-bootstrap";
import * as FaIcons from "react-icons/fa"
import axios from 'axios';
import ChangePassword from './TeamAdmin';


function SidebarAdmin() {

    const [sidebar, setSidebar] = useState(false);
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [show, setShow] = useState(false);

    useEffect(() => {
        setUsername(localStorage.getItem("username"));
    }, []);

    function handleLogout() {
        localStorage.removeItem("username");
        localStorage.removeItem("email");
        localStorage.removeItem("token");
        localStorage.removeItem("role");
        localStorage.removeItem("userId");
        localStorage.removeItem("teamId");
        window.location.reload();
    }
    const showSidebar = () => setSidebar(!sidebar)

    return (
        <div>
            <div className='sidebar__admin'>
                <div>
                    <img src={Logo} width='130px' />
                </div>
                <div style={{
                    display: "flex",
                    alignItems: "center",
                }}>
                    {/* <h4 style={{ color: "white", margin: "0 1rem" }}>{username}</h4> */}
                    <Dropdown>
                        <Dropdown.Toggle style={{ backgroundColor: "#009444", width: "auto" }}>
                            {username}
                            <FaIcons.FaUserCircle style={{
                                fontSize: "1.5rem",
                                color: "#fff",
                                cursor: "pointer",
                                margin: "5px"
                            }} />
                        </Dropdown.Toggle>
                        <Dropdown.Menu variant="dark">
                            <Dropdown.Item onClick={ChangePassword} href="/reset-password">Change password</Dropdown.Item>

                            <Dropdown.Divider />
                            <Dropdown.Item onClick={handleLogout} href="/login">Logout</Dropdown.Item>
                        </Dropdown.Menu>
                    </Dropdown>
                </div>
            </div>
            <nav className={sidebar ? 'nav-menu active' : 'nav-menu'}>
                <ul className='nav-menu-items'>
                    <li className='navbar-toggle'>
                        <Link to="#" className='menu-bars'>
                            
                        </Link>
                    </li>
                    {AdminPages.map((item, index) => {
                        return (
                            <li key={index} className={item.cName}> 
                            
                                    <Link to={item.path}>
                                    {item.icon}
                                    <span>{item.title}</span>
                                </Link> 
                            </li>
                        )
                    })}
                </ul>
            </nav>
        </div>
    )
}

export default SidebarAdmin;