import React, { Component } from 'react';
import { SendEmail } from './SendEmail';
import { TrackersTable } from './TrackersTable';
import axios from 'axios'

export class Trackers extends Component {
    constructor(props) {
        super();

        this.state = {
            trackers: []
        };
        this.getTrackers = this.getTrackers.bind(this)
        this.getTrackers();
    }

    getTrackers = () => {
        //TODO: Secure API call with tokens
        var url = "https://localhost:44324/api/Trackers";
        axios.get(url).then(r => this.setState({ trackers: r.data }))

    }
  
    render() {
        return (
            <div>
                <div className="row">
                    <div className="col-4 mt-2">
                        <h1>Trackers</h1>
                    </div>
                    <div className="col-4 offset-4">
                        <SendEmail onSendEmail={this.getTrackers} />
                    </div>
                </div>
                <div className="row">
                    <div className="col">
                        <TrackersTable data={this.state.trackers} />
                    </div>
                </div>
            </div>
        );
    }
}

