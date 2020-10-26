import React, { Component } from 'react';

export class TrackersTable extends Component {

    constructor(props) {
        super();
    }

    render() {
        return (
            <div>
                <header>
                    <div>
                        <table className="table table-hover">
                            <tbody>
                                <tr>
                                    <th>Tracker ID</th>
                                    <th>External ID</th>
                                    <th>Status</th>
                                    <th>IP Address</th>
                                </tr>
                                {this.props.data.map((tracker) => (
                                    <tr key={tracker.trackerID}>
                                        <td>{tracker.trackerID}</td>
                                        <td>{tracker.externalID}</td>
                                        <td>
                                            {tracker.ipAddress == null ? "Delivered" : "Opened"}
                                        </td>
                                        <td>{tracker.ipAddress}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </header>
            </div>
        );
    }
}

