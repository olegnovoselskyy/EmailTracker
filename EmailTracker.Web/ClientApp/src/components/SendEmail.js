import React, { Component } from 'react';
import {
    InputGroup,
    InputGroupAddon,
    Input,
    Button
} from 'reactstrap';
import axios from 'axios'

export class SendEmail extends Component {

    constructor(props) {
        super(props);
        this.state = { emailAddress: '' };
    }
    sendEmail = () => {
        //TODO: Secure API call with tokens
        var emailAddress = this.state.emailAddress;
        var url = "https://localhost:44324/api/Trackers/SendEmail?emailAddress=" + emailAddress;
        var self = this;
        axios.post(url).then(
            function (response) {
                console.log("$(\"[id*='imgTrack']\").src = \"https://localhost:44324/api/TrackerEvents/" + response.data.externalID + "\"");
                self.props.onSendEmail();
            });
    }

    setEmailAddress = (event) => {
        this.setState({ emailAddress: event.target.value });
    }

    render() {
        return (
            <InputGroup>
                <Input id="txtEmailAddress" onKeyUp={this.setEmailAddress} placeholder="email@example.com" />
                <InputGroupAddon addonType="append"><Button color="primary" onClick={this.sendEmail}>Send Email</Button></InputGroupAddon>
            </InputGroup>
        );
    }
}
