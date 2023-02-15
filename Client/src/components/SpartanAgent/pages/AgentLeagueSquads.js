import React, { useState, useEffect } from "react";
import { render } from "react-dom";
import { Form, Field } from "react-final-form";
import arrayMutators from "final-form-arrays";
import { FieldArray } from "react-final-form-arrays";
import { Button } from "react-bootstrap";
import axios from "axios";
import SidebarAgent from "../SidebarAgent";
import "./AgentLeagueSquads.css";
import { appendErrors } from "react-hook-form";


const sleep = (ms) => new Promise((resolve) => setTimeout(resolve, ms));

const onSubmit = async (values) => {

  await sleep(100);
  var data = JSON.stringify(values, 0, 2);
  console.log(data);
  var data = JSON.stringify(values, 0, 2);
  var data = JSON.stringify(values, 0, 2);

  await axios({
    method: "POST",
    url: "https://localhost:7122/api/League/addSquadsToLeague",
    data: data,
    headers: {
      "Content-Type": "application/json",
    }
  })
 };
// TESTINGG=================================================
  // await axios.post("https://localhost:7122/api/League/addSquadsToLeague", 
  // data,
  // headers,{
  //   "Content-Type": "application/json",
  // }).then((res)=>{console.log("RESSSSSS987654321")}).error((errors)=>{console.log("ERRORS987654321")})

  // =======================================================
function AgentLeaguesSquads() {

  const [league, setLeague] = useState([]);
  useEffect(() => {
    fetch("https://localhost:7122/api/League/getLeagues", {
      method: "get",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((resp) => resp.json())
      .then((resp) => setLeague(resp));
  }, []);

  const [squads, setSquads] = useState([]);
  useEffect(() => {
    fetch("https://localhost:7122/api/Squad", {
      method: "get",
      headers: {
        "Content-Type": "application/json",
      },
    })
      .then((resp) => resp.json())
      .then((resp) => setSquads(resp));
  }, []);

  return (
    <>
      <SidebarAgent />
      <Form
        onSubmit={onSubmit}
        mutators={{
          ...arrayMutators,
        }}
        render={({
          handleSubmit,
          form: {
            mutators: { push, pop },
          },
          pristine,
          form,
          submitting,
          values,
        }) => {
          return (
            <form onSubmit={handleSubmit} className='form__container'>
              <h2>Add squads to the league</h2>
              <hr></hr>
              <div className='select__league'>
                <label>League</label>
                <Field
                  name='LeaguesLeagueId'
                  component='select'
                  placeholder='League'
                >
                  <option selected>Select league</option>
                  {league.map((x) => {
                    return <option value={x.leagueId}>{x.leagueName}</option>;
                  })}
                </Field>
              </div>
              <div className='buttons'>
                <Button
                  variant='dark'
                  type='button'
                  onClick={() => push("SquadsTeamId", undefined)}
                >
                  Add Squad
                </Button>
                <Button
                  style={{ width: "auto" }}
                  variant='dark'
                  type='button'
                  onClick={() => pop("SquadsTeamId")}
                >
                  Remove Squad
                </Button>
              </div>
              <FieldArray name='SquadsTeamId'>
                {({ fields }) =>
                  fields.map((name, index) => (
                    <div key={name} className='select__squad'>
                      <label>Squad #{index + 1}</label>
                      <Field
                        name={`${name}.teamId`}
                        component='select'
                        placeholder='Choose an existed squad ID'
                      >
                        <option selected>Select squad</option>
                        {squads.map((x) => {
                          return (
                            <option value={x.teamId}>
                              {x.name}: (
                              {x.isVerified ? "Verified" : "Unverifed"})
                            </option>
                          );
                        })}
                      </Field>
                      <span
                        onClick={() => fields.remove(index)}
                        style={{ cursor: "pointer" }}
                      ></span>
                    </div>
                  ))
                }
              </FieldArray>

              <div className='buttons'>
                <Button
                  variant='success'
                  className='submit__btn'
                  type='submit'
                  disabled={submitting || pristine}
                >
                  Submit
                </Button>
                <Button
                  variant='danger'
                  type='button'
                  onClick={form.reset}
                  disabled={submitting || pristine}
                >
                  Reset
                </Button>
              </div>
            </form>
          );
        }}
      />
    </>
  );
}

export default AgentLeaguesSquads;
