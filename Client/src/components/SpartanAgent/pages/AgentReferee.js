import React, { useState, useEffect } from "react";
import SidebarAgent from "../SidebarAgent";
import axios from "axios";
import { Form, Button, Table, Modal } from "react-bootstrap";
import { Link } from "react-router-dom";

function AgentReferee() {
  const [id, setId] = useState(null);
  const [name, setName] = useState("");
  const [lastName, setLastName] = useState("");
  const [experience, setExperience] = useState("");
  const [city, setCity] = useState("");
  const [position, setPosition] = useState("");
  const [submitedRegister, setSubmitedRegister] = useState(false);
  const [registered, setRegistered] = useState(false);

  const [selectedItem, setSelectedItem] = useState({});

  const [logged, setlogged] = useState(false);

  const [RefereeData, setRefereeData] = useState([]);

  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const [deleteShow, setDeleteShow] = useState(false);
  const handleDeleteClose = () => setDeleteShow(false);
  const handleDeleteShow = () => setDeleteShow(true);

  const [token, setToken] = useState("");

  useEffect(() => {
    var items = null;
    items = localStorage.getItem("email");
    if (items != null) {
      setlogged(true);
    }
  });

  // GET request
  useEffect(() => {
    axios.get(`https://localhost:7122/api/Referees`).then((response) => {
      setRefereeData(response.data);
    });
  }, []);

  // Function to register referee
  async function registerReferee() {
    const t = localStorage.getItem("token");
    setToken(t);

    await axios({
      method: "POST",
      url: "https://localhost:7122/api/Referees/addReferee",
      data: {
        name: name,
        lastName: lastName,
        experience: experience,
        city: city,
        position: position,
      },
      headers: {
        Authorization: `bearer ${t}`,
      },
    }).then(
      (response) => {
        setSubmitedRegister(true);
      },
      (error) => {
        setRegistered(true);
      }
    );
  }

  // Edit
  const setData = (data) => {
    let { id, name, lastName, experience, city, position } = data;
    localStorage.setItem("RId", id);
    localStorage.setItem("RName", name);
    localStorage.setItem("RLastName", lastName);
    localStorage.setItem("RExperience", experience);
    localStorage.setItem("RCity", city);
    localStorage.setItem("RPosition", position);
  };

  // Delete request
  async function onDelete(id) {
    const t = localStorage.getItem("token");
    setToken(t);

    await axios({
      method: "DELETE",
      url: `https://localhost:7122/api/Referees/${id}`,
      headers: {
        Authorization: `bearer ${t}`,
      },
    }).then(window.location.reload());
  }

  return (
    <div>
      {logged ? (
        <>
          <SidebarAgent />

          <Modal show={show} onHide={handleClose} animation={false} size='lg'>
            <Modal.Header closeButton>
              <Modal.Title>Register Referee</Modal.Title>
            </Modal.Header>

            <Modal.Body>
              <Form>
                <Form.Group className='mb-3'>
                  <Form.Label>Referee Name</Form.Label>
                  <Form.Control
                    onChange={(e) => setName(e.target.value)}
                    type='text'
                    placeholder='Enter Referee Name'
                  />
                </Form.Group>

                <Form.Group className='mb-3'>
                  <Form.Label>Referee Last Name</Form.Label>
                  <Form.Control
                    onChange={(e) => setLastName(e.target.value)}
                    type='text'
                    placeholder='Enter Referee Last Names'
                  />
                </Form.Group>

                <Form.Group className='mb-3'>
                  <Form.Label>Referee Experience</Form.Label>
                  <Form.Control
                    onChange={(e) => setExperience(e.target.value)}
                    type='text'
                    placeholder='Enter Referee Experience'
                  />
                </Form.Group>

                <Form.Group className='mb-3'>
                  <Form.Label>Referee City</Form.Label>
                  <Form.Control
                    onChange={(e) => setCity(e.target.value)}
                    type='text'
                    placeholder='Enter Referee City'
                  />
                </Form.Group>

                <Form.Group className='mb-3'>
                  <Form.Label>Referee Position</Form.Label>
                  <Form.Control
                    onChange={(e) => setPosition(e.target.value)}
                    type='text'
                    placeholder='Enter Referee Position'
                  />
                </Form.Group>

                <Button
                  style={{ width: "auto" }}
                  variant='success'
                  type='submit'
                  onClick={registerReferee}
                >
                  Register Referee
                </Button>
              </Form>
            </Modal.Body>
          </Modal>

          <Modal show={deleteShow} onHide={handleDeleteClose}>
            <Modal.Header closeButton>
              <Modal.Title>Delete</Modal.Title>
            </Modal.Header>
            <Modal.Body>Are you sure you want to delete?</Modal.Body>
            <Modal.Footer>
              <Button
                variant='danger'
                onClick={() => onDelete(selectedItem.id)}
              >
                Delete
              </Button>
            </Modal.Footer>
          </Modal>

          <div
            style={{
              position: "absolute",
              top: "10%",
              left: "25%",
              width: "1100px",
              backgroundColor: "#fff",
              padding: "30px",
            }}
          >
            <Button
              style={{ width: "auto" }}
              variant='success'
              onClick={handleShow}
            >
              Create Referee
            </Button>

            <Table striped bordered size='sm' style={{ textAlign: "center" }}>
              <thead>
                <tr>
                  <th>Id</th>
                  <th>Name</th>
                  <th>Lastname</th>
                  <th>Experience</th>
                  <th>City</th>
                  <th>Position</th>
                  <th>Edit</th>
                  <th>Delete</th>
                </tr>
              </thead>
              <tbody>
                {RefereeData.map((data) => {
                  return (
                    <>
                      <tr>
                        <td>{data.id}</td>
                        <td>{data.name}</td>
                        <td>{data.lastName}</td>
                        <td>{data.experience}</td>
                        <td>{data.city}</td>
                        <td>{data.position}</td>
                        <td>
                          <Link to='/agent-edit-referee'>
                            <Button
                              variant='success'
                              onClick={() => setData(data)}
                            >
                              Edit
                            </Button>
                          </Link>
                        </td>
                        <td>
                          <Button
                            variant='dark'
                            onClick={() => {
                              handleDeleteShow(true);
                              setSelectedItem(data);
                            }}
                          >
                            Delete
                          </Button>
                        </td>
                      </tr>
                    </>
                  );
                })}
              </tbody>
            </Table>
          </div>
        </>
      ) : (
        <div>
          <h4>Agent Stadium</h4>
          <h1>Sorry you are Unauthorized for this page </h1>
        </div>
      )}
    </div>
  );
}

export default AgentReferee;
