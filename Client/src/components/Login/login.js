import React, { useState } from "react";
import { Form, Button, Container } from "react-bootstrap";
import background from "../../images/homeimage.jpg";
import { Spinner } from "react-bootstrap";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "./login.css";
import registerImage from "../../images/access.svg";
import Navigation from "../Navigation/navigation";
import Footer from "../Footer/footer";

function Login() {
  const navigate = useNavigate();
  const [clickedLoggin, setClickedLoggin] = useState(false);
  const [errors, setErrors] = useState(false);
  const [username, setUsername] = useState(false);
  const [password, setPassword] = useState(false);
  const [response, setResponse] = useState("");
  async function handleLogin(e) {
    e.preventDefault();
    console.log("CLICKED", clickedLoggin);

    if (username == "" || password == "") {
      setResponse("The fields can't be empty");
      setErrors(true);
      return;
    }
    setClickedLoggin(true);
    await axios({
      method: "POST",
      url: "https://localhost:7122/api/User/login",
      data: {
        email: username,
        password: password,
      },
    }).then(
      (response) => {
        var result = response.data;
        localStorage.setItem("username", result.userName);
        localStorage.setItem("email", result.email);
        localStorage.setItem("token", result.token);
        localStorage.setItem("role", result.role);
        localStorage.setItem("userId", result.userId);

        console.log(result);
        if (response.data.role == "agent") {
          navigate("/agent");
        } else {
          navigate("/admin");
        }
      },
      (error) => {
        setResponse(error.response.data);
        console.log(response, "dsfdf");
        console.log(error, "qen");
        setErrors(true);
      }
    );
    setClickedLoggin(false);
  }
  return (
    <div>
      <Navigation />
      <Container className='login__container'>
        <div className='login__header'>
          <div>
            <Form
              className='login__form'
              style={{
                width: "500px",
              }}
            >
              <div>
                <div>
                  <h2>Login</h2>
                </div>

                <Form.Group className='mb-3'>
                  <Form.Label>Username</Form.Label>
                  <Form.Control
                    type='text'
                    placeholder='Enter your username'
                    onChange={(e) => setUsername(e.target.value)}
                  />
                </Form.Group>

                <Form.Group className='mb-3'>
                  <Form.Label>Password</Form.Label>
                  <Form.Control
                    type='password'
                    placeholder='Enter your password'
                    onChange={(e) => setPassword(e.target.value)}
                  />
                </Form.Group>
                <div>
                  <p style={{ color: "red" }}>{response}</p>
                </div>
                {clickedLoggin ? (
                  <label>Logging in...</label>
                ) : (
                  <Button type='submit' onClick={handleLogin}>
                    Log in
                  </Button>
                )}
              </div>
            </Form>
          </div>

          <div>
            <img src={registerImage} width='60%' />
          </div>
        </div>
      </Container>
    </div>
  );
}

export default Login;
