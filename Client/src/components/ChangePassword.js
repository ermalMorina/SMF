import { Form, Button, Modal } from 'react-bootstrap';
import React, {Route, useState, useEffect, Redirect } from 'react'
import { useLocation, useNavigate, Link } from "react-router-dom";
import axios from 'axios';
import login from './Login/login';


function AgentChangePassword() {
    const [msg, setMsg] = useState('');
    const [errors, setErrors] = useState([]);
    const [errorback, seterrorback] = useState(false)
    const [res, setRes] = useState(false)
    const [password, setPassword] = useState('');
    const [confPass, setConfPass] = useState('');
    const [currpass, setCurrPass] = useState('');
    const [username, setUsername] = useState('');
    const [message, setmessage] = useState('');
    const navigate = useNavigate();
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    
    function returntrue(){
        console.log("returnii");
        var role=localStorage.getItem("role");

        navigate(`/${role}`)
    }


    function handleLogout() {
        localStorage.removeItem("username");
        localStorage.removeItem("email");
        localStorage.removeItem("token");
        localStorage.removeItem("role");
    

    }

    useEffect(() => {
        setUsername(localStorage.getItem("username"));
        console.log(username)
    }, []);
  
    async function ChangePasswordAPI(e) {
       e.preventDefault();
        await axios({
            method: "post",
            url: `https://localhost:7122/api/User/ChangePassword`,
            data: {
                userName: username,
                CurrentPassword: currpass,
                NewPassword: password,
                ConfirmPassword: confPass
            },
        }).then(
            (response) => {
                console.log("RRRESPONSEEE", response.data);
                setRes(true);
                setmessage(response.data);
                handleLogout();
                navigate("/login");
                
            },
            (e) => {
                try { 
                    console.log(e)
                    var a = e.response.data.message
                    setMsg(a) 
                    var b =e.response.data.errors.errors
                    setErrors(b) 
                    
                    console.log("msgaaaaaaaaaaaaaa",msg)
                    seterrorback(true);  
                  } catch (err) {
                    console.log("trycatchworking", err); 
                  } 
            }
        );
    }

    return (
        <div>
            
            <div style={{
                position: "absolute",
                top: "50%",
                left: "50%",
                transform: "translate(-50%, -50%)",
                backgroundColor: "#fff",
                padding: "20px"
            }}>
                <h1>Change your password</h1>
                <Form style={{ width: "500px" }}>
               
                    <Form.Group className="mb-3">
                    { errorback ?
                      errors.map((error) => {
                        return ( 
                            <p style={{ color: "red" }}>{error}</p>  
                        ) 
                      }
                      ) : null
                    } 
                        <p style={{ color: "red" }}>{msg}</p>
                        { res ?

                            <p style={{ color: "green" }}>{message}</p>
                            

                          : null
                          }
                        <Form.Label>Username</Form.Label>
                        <Form.Control
                            value={username}
                            type="text"
                            placeholder="Enter Username"
                            
                        />
                    </Form.Group>

                     <Form.Group className="mb-3">
                        <Form.Label>Current password</Form.Label>
                        <Form.Control
                            onChange={(e) => setCurrPass(e.target.value)}
                            type="password"
                            placeholder="Enter current password"
                        />
                    </Form.Group>
                   
                    <Form.Group className="mb-3">
                        <Form.Label>New password</Form.Label>
                        <Form.Control
                            onChange={(e) => setPassword(e.target.value)}
                            type="password"
                            placeholder="Enter new password"
                        />
                    </Form.Group>

                    <Form.Group className="mb-3">
                        <Form.Label>Confirm Password</Form.Label>
                        <Form.Control
                            onChange={(e) => setConfPass(e.target.value)}
                            type="password"
                            placeholder="Please confirm the password"
                        />
                    </Form.Group>

                    <Button style={{ backgroundColor: "#009444", width: "auto" }} type="submit" onClick={ChangePasswordAPI}>
                        Change Password
                    </Button>

                    <Button style={{ backgroundColor: "#009444", width: "auto" }} type="submit" onClick={returntrue}>
                        Return to dashboard
                    </Button>
                </Form>

            </div>
        </div>
    )
}

export default AgentChangePassword;