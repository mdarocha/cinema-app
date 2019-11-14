import * as React from 'react';
import * as Moment from 'moment';

import { ReservationFlowViewModel, PlacePosition } from "./reservation_flow_view_model";
import { IRoom } from "./place_selector_interfaces";

import { Room } from "./place_selector";
import { Confirmation, IComfirmation } from "./confirmation";

import { pos_list_to_string } from './utils';
import { show_alert } from '../show_alert';

enum ReservationFlowState {
    PlaceSelection,
    Confirmation
}

interface IReservationFlowState {
    Status: ReservationFlowState,
    SelectedPlaces: Array<PlacePosition>,
}

class ReservationFlow extends React.Component<ReservationFlowViewModel, IReservationFlowState> {
    constructor(props) {
        super(props);
        this.state = { Status: ReservationFlowState.PlaceSelection, SelectedPlaces: [] };
    }

    handleContinue(selected: Array<PlacePosition>) {
        if (selected.length > 0) {
            this.setState(prev => ({
                SelectedPlaces: selected,
                Status: ReservationFlowState.Confirmation
            }));
        }
    }

    handleBack() {
        this.setState(prev => ({
            Status: ReservationFlowState.PlaceSelection
        }));
    }

    handleSubmit() {
        const url = this.props.PostUrl;

        let model: ReservationFlowViewModel = { ...this.props };
        model.SelectedPlaces = this.state.SelectedPlaces;

        $.post(url, { 'model': model }, (data) => {
            if (data.success) {
                window.location.replace(data.redirect_url);
            } else {
                show_alert("Wystąpił błąd");
            }
        });
    }

    render() {
        let view;
        switch (this.state.Status) {
            case ReservationFlowState.PlaceSelection:
                const room: IRoom = {
                    TakenPlaces: this.props.TakenPlaces,
                    MaxTickets: this.props.MaxTickets,
                    RoomWidth: this.props.RoomWidth,
                    RoomHeight: this.props.RoomHeight,

                    SelectedPlacesStart: this.state.SelectedPlaces,

                    submit: (s) => this.handleContinue(s),
                };

                view = (
                    <div>
                        <h5 className="text-center">
                            {this.props.Movie.Title} - {Moment(this.props.Showing.Time).locale('pl').format('HH:mm, dddd')}
                        </h5>
                        <h6 className="text-center">{this.props.Showing.IsSubtitles ? "Napisy" : "Dubbing"}, 
                                                    {this.props.Showing.Is2D ? " 2D" : " 3D"}
                        </h6>
                        <Room {...room} />
                    </div>
                );
                break;

            case ReservationFlowState.Confirmation:
                const selected_string = pos_list_to_string(this.state.SelectedPlaces, this.props.RoomWidth)

                const confirmation: IComfirmation = {
                    Showing: this.props.Showing,
                    Movie: this.props.Movie,
                    SelectedPlacesList: selected_string,

                    confirm: () => this.handleSubmit(),
                    back: () => this.handleBack(),
                };

                view = (<Confirmation {...confirmation} />);
        }

        return (
            <div>
                {view}
            </div>);
    }
}

export { ReservationFlow };