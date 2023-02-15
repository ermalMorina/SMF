import React, { useEffect, useState } from "react";
import SidebarAdmin from "../SidebarAdmin";
import axios from "axios";
import { Form, Button } from "react-bootstrap";

export default function AdminAddSquad() {
  const [stadiumId, setStadiumId] = useState("");
  const [name, setName] = useState("");
  const [city, setCity] = useState("");
  const [isVerified, setIsVerified] = useState(false);
  const [file, setFile] = useState(null);
  const [logo, setLogo] = useState(null);

  const [token, setToken] = useState("");
  const [submitedRegister, setSubmitedRegister] = useState(false);
  const [registered, setRegistered] = useState(false);

  async function registerSquad(e) {
    e.preventDefault();
    const userId = localStorage.getItem("userId");
    console.log("USERID", userId);
    const t = localStorage.getItem("token");
    setToken(t);
    console.log("stadiumId",stadiumId)
    console.log("dataa123123", stadiumId)
    await axios({
      method: "POST",
      url: "https://localhost:7122/api/Squad/addSquad",
      data: {
        stadiumId: stadiumId,
        logo: logo,
        name: name,
        city: city,
        isVerified: false,
        file: file,
        RegisterUserId: userId,
      },
      headers: {
        Authorization: `bearer ${t}`,
        "Content-Type": "multipart/form-data",
      },
    }).then(
      (response) => {console.log(response)
        setSubmitedRegister(true);
      },
      (error) => {
        setRegistered(true);
      }
    );
  }

  const [stadium, setStadium] = useState([]);
  useEffect(() => {
    fetch("https://localhost:7122/api/Stadium/get-stadium", {
      method: "get",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((resp) => resp.json())
      .then((resp) => setStadium(resp));
  }, []);

  const handleFileSelect = (event) => {
    setFile(event.target.files[0]);
  };

  return (
    <div>
      <SidebarAdmin />
      <div
        style={{
          position: "absolute",
          top: "50%",
          left: "55%",
          width: "700px",
          backgroundColor: "#fff",
          padding: "20px",
          transform: "translate(-50%, -50%)",
        }}
      >
        <Form>
          <h2>Register Squad</h2>
          <hr></hr>

          
          <Form.Group className="mb-3">
            <Form.Label>Stadium</Form.Label>
            <Form.Select onChange={(e)=> setStadiumId(e.target.value)}>
              
              <option unselectable="true">Choose Stadium</option>
              {stadium.map((x) => {
                return <option value={x.id}>{x.name}</option>;
              })}
            </Form.Select>
          </Form.Group>

          <Form.Group className="mb-3" controlId="formFile">
            <Form.Label>Upload logo</Form.Label>
            <Form.Control
              type="file"
              name="file"
              onChange={(e) => setLogo(e.target.files[0])}
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formLastName">
            <Form.Label>Squad Name</Form.Label>
            <Form.Control
              onChange={(e) => setName(e.target.value)}
              type="text"
              placeholder="Enter Squad Name"
            />
          </Form.Group>

          <Form.Group className="mb-3" controlId="formExperience">
            <Form.Label>Squad City</Form.Label>
            <Form.Control
              onChange={(e) => setCity(e.target.value)}
              type="text"
              placeholder="Enter Squad City"
            />
          </Form.Group>

          

          <Form.Group className="mb-3" controlId="formFile">
            <Form.Label>Upload certificate</Form.Label>
            <Form.Control
              type="file"
              name="file"
              onChange={(e) => setFile(e.target.files[0])}
            />
          </Form.Group>

          <Button
            variant="primary"
            type="submit"
            onClick={registerSquad}
            style={{ width: "auto", backgroundColor: "#35AD79" }}
          >
            Register Squad
          </Button>
        </Form>
      </div>
    </div>
  );
}