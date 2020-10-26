import React, { Component } from 'react';
import { Button, Form, FormGroup, Label, Input, Card, CardTitle, CardBody } from 'reactstrap';
import { Link } from 'react-router-dom';

// TODO: Create separate component for login

export class Home extends Component {
  static displayName = Home.name;

  render () {
      return (
          <div className="text-center">
              <h1>Email Tracker Admin</h1>
              <div className="row mt-3">
                  <div className="col-4 offset-4">
                      <Card>
                          <CardBody>
                              <CardTitle><h4>Login</h4></CardTitle>
                              <Form>
                                  <FormGroup>
                                      <Label for="txtUsername">Username</Label>
                                      <Input type="username" name="username" id="txtUsername" value="User" />
                                  </FormGroup>
                                  <FormGroup>
                                      <Label for="txtPassword">Password</Label>
                                      <Input type="password" name="password" id="txtPassword" value="password" />
                                  </FormGroup>
                                  <FormGroup>
                                      <Link to="/trackers">
                                          <Button className="btn btn-block btn-primary">Log In</Button>
                                      </Link>
                                      <Link to="/"> 
                                          <Button className="btn btn-block btn-default my-2">Forgot Password</Button>
                                      </Link>
                                  </FormGroup>
                              </Form>
                          </CardBody>
                      </Card>
                  </div>
              </div>
          </div>
    );
  }
}
