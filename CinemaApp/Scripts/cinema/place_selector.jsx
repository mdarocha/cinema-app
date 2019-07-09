const seats_y = 12;
const seats_x = 18;

class Seat extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="room-seat seat-free" >
            </div>
        );
    }
}

class Room extends React.Component {
    constructor(props) {
        super(props);
    }

    createRow(index, width) {
        let row = []
        for (let i = 0; i < width; i++) {
            let pos = {
                x: i,
                y: index,
            };

            row.push(<Seat key={`${pos.x}-${pos.y}`} pos={pos} />)
        }
        return (<div key={index} className="room-row">{row}</div>);
    }

    createRoom() {
        let room = []
        for (let i = 0; i < seats_y; i++) {
            room.push(this.createRow(i, seats_x));
        }
        return room;
    }

    render() {
        return (
            <div className="room">
                {this.createRoom()}
            </div>
        );
    }
}

ReactDOM.render(
    <Room />,
    $('#place-selector')[0]
);