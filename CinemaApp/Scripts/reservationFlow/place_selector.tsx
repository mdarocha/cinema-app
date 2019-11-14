import * as React from "react";
import { ISeat, IRoom, IRoomState } from "./place_selector_interfaces";
import { show_alert } from "../show_alert";

class Seat extends React.Component<ISeat> {
    constructor(props: ISeat) {
        super(props);
    }

    render() {
        let styles = "room-seat ";
        styles += this.props.taken ? "seat-taken" : (this.props.selected ? "seat-selected" : "seat-free");

        const seat_number = this.props.pos.x + 1;
        const seat_row = this.props.pos.x === 0 ? String.fromCharCode(97 + this.props.pos.y).toUpperCase() : "";

        return (
            <div
                className={styles}
                seat-number={seat_number}
                seat-row={seat_row}
                onClick={this.props.handler}>
            </div>);
    }
}

class Room extends React.Component<IRoom, IRoomState> {
    constructor(props) {
        super(props);
        this.state = { SelectedPlaces: Array.from(this.props.SelectedPlacesStart) }
    }

    handleClick(pos) {
        const found = this.state.SelectedPlaces.find(p => p.x === pos.x && p.y === pos.y);

        if (found) {
            this.setState(prev => ({
                SelectedPlaces: prev.SelectedPlaces.filter(p => p.x !== pos.x || p.y !== pos.y)
            }));
        } else if (this.state.SelectedPlaces.length < this.props.MaxTickets) {
            this.setState(prev => ({
                SelectedPlaces: [...prev.SelectedPlaces, pos]
            }));
        } else {
            show_alert(`Nie możesz zarezerwować więcej niż ${this.props.MaxTickets} miejsc naraz`);
        }
    }

    createRow(index: number) {
        let row = []
        for (let i = 0; i < this.props.RoomWidth; i++) {
            let pos = {
                x: i,
                y: index,
            };

            const filter = (p => p.x === pos.x && p.y === pos.y);
            let taken = Boolean(this.props.TakenPlaces.find(filter));
            let selected = Boolean(this.state.SelectedPlaces.find(filter));

            row.push(<Seat key={`${pos.x}-${pos.y}`}
                pos={pos}
                taken={taken}
                selected={selected}
                handler={taken ? undefined : () =>this.handleClick(pos)} />)
        }
        return (
            <div key={index} className="room-row">
                {row}
            </div>);
    }

    createRoom() {
        let room = []
        for (let i = 0; i < this.props.RoomHeight; i++) {
            room.push(this.createRow(i));
        }
        return room;
    }

    render() {
        return (
            <div>
                <div className="room">
                    {this.createRoom()}
                </div>
                <div className="d-flex justify-content-center">
                    <input type="button"
                        className="p-2 mt-3 btn btn-lg btn-primary"
                        onClick={() => this.props.submit(this.state.SelectedPlaces)}
                        value="Kontynuuj" />
                </div>
            </div>
        );
    }
}

export { Room };