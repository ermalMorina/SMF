import React, { useState, useEffect } from "react";
import SidebarAgent from "../SidebarAgent";
import axios from "axios";
import { Form, Button, Table, Modal } from "react-bootstrap";
import { Link } from "react-router-dom";

function AgentStadium() {
  const [id, setId] = useState(null);
  const [name, setName] = useState("");
  const [location, setLocation] = useState("");
  const [capacity, setCapacity] = useState("");
  const [submitedRegister, setSubmitedRegister] = useState(false);
  const [registered, setRegistered] = useState(false);

  const [selectedItem, setSelectedItem] = useState({});

  // Checking if the user is logged in
  const [logged, setlogged] = useState(false);

  // Showing the modal to add the stadium
  const [show, setShow] = useState(false);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  // Showing the modal to delete the stadium
  const [deleteShow, setDeleteShow] = useState(false);
  const handleDeleteClose = () => setDeleteShow(false);
  const handleDeleteShow = () => setDeleteShow(true);

  // Creating the state which contain the incomming data
  const [APIData, setAPIData] = useState([]);

  // Checking if the user is looged in and getting their email
  useEffect(() => {
    var items = null;
    items = localStorage.getItem("email");
    if (items != null) {
      setlogged(true);
    }
  });

  // Function to register the stadium
  async function registerStadium() {
    await axios({
      method: "POST",
      url: "https://localhost:7122/api/Stadium/create-stadium",
      data: {
        name: name,
        location: location,
        capacity: capacity,
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

  // Sending the get request
  useEffect(() => {
    axios
      .get(`https://localhost:7122/api/Stadium/get-stadium`)
      .then((response) => {
        setAPIData(response.data);
      });
  }, []);

  // Sending the delete request to the respective stadium id
  const onDelete = (id) => {
    axios
      .delete(`https://localhost:7122/api/Stadium/${id}`)
      .then(window.location.reload());
  };

  // Edit
  const setData = (data) => {
    let { id, name, location, capacity } = data;
    localStorage.setItem("ID", id);
    localStorage.setItem("Name", name);
    localStorage.setItem("Location", location);
    localStorage.setItem("Capacity", capacity);
  };

  return (
    <div>
      {logged ? (
        <>
          <SidebarAgent />

          <Modal show={show} onHide={handleClose} animation={false} size='lg'>
            <Modal.Header closeButton>
              <Modal.Title>Create League</Modal.Title>
            </Modal.Header>

            <Modal.Body>
              <Form>
                <Form.Group className='mb-3'>
                  <Form.Label>Stadium Name</Form.Label>
                  <Form.Control
                    onChange={(e) => setName(e.target.value)}
                    type='text'
                    placeholder='Enter Stadium Name'
                  />
                </Form.Group>

                <Form.Group className='mb-3'>
                  <Form.Label>Stadium Location</Form.Label>
                  <Form.Control
                    onChange={(e) => setLocation(e.target.value)}
                    type='text'
                    placeholder='Enter Stadium Location'
                  />
                </Form.Group>

                <Form.Group className='mb-3'>
                  <Form.Label>Stadium Capacity</Form.Label>
                  <Form.Control
                    onChange={(e) => setCapacity(e.target.value)}
                    type='number'
                    placeholder='Enter Stadium Capacity'
                  />
                </Form.Group>

                <Button
                  style={{ width: "auto" }}
                  variant='success'
                  type='submit'
                  onClick={registerStadium}
                >
                  Create Stadium
                </Button>
              </Form>
            </Modal.Body>
          </Modal>

          <Modal show={deleteShow} onHide={handleDeleteClose}>
            <Modal.Header closeButton>
              <Modal.Title>Delete</Modal.Title>
            </Modal.Header>
            <Modal.Body>Are you sure you want to delete</Modal.Body>
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
              Create Stadium
            </Button>

            <Table striped bordered size='sm' style={{ textAlign: "center" }}>
              <thead>
                <tr>
                  <th>Stadium Name</th>
                  <th>Stadium Location</th>
                  <th>Stadium Capacity</th>
                  <th>Edit</th>
                  <th>Delete</th>
                </tr>
              </thead>

              <tbody>
                {APIData.map((data) => {
                  return (
                    <>
                      <tr>
                        <td>{data.name}</td>
                        <td>{data.location}</td>
                        <td>{data.capacity}</td>
                        <td>
                          <Link to='/agent-edit-stadium'>
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

export default AgentStadium;
