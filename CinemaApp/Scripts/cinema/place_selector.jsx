class Seat extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        let styles = "room-seat ";
        styles += this.props.taken ? "seat-taken" : (this.props.selected ? "seat-selected" : "seat-free");
        return (
            <div
                className={styles}
                onClick={this.props.handler}
            >{this.props.pos.x}</div>);
    }
}

class Room extends React.Component {
    constructor(props) {
        super(props);

        this.state = { selected: new Array() };
    }

    handleClick(pos) {
        if (this.state.selected.length < this.props.MaxTickets) {
            if (!this.state.selected.find(p => p.x === pos.x && p.y === pos.y)) {
                this.setState(prev => ({
                    selected: [...prev.selected, pos]
                }));
            } else {
                this.setState(prev => ({
                    selected: prev.selected.filter(p => p.x !== pos.x || p.y !== pos.y)
                }));
            }
        }
    }

    handleSubmit() {
        if (this.state.selected.length > 0) {
            const url = this.props.PostUrl;

            const model = this.props
            const places = this.state.selected;

            $.post(url, { 'model': model, 'places': places }, (data) => console.log(data));
        }
    }

    createRow(index, width) {
        let row = []
        for (let i = 0; i < this.props.RoomWidth; i++) {
            let pos = {
                x: i,
                y: index,
            };

            const filter = (p => p.x === pos.x && p.y === pos.y);
            let taken = Boolean(this.props.TakenPlaces.find(filter));
            let selected = Boolean(this.state.selected.find(filter));

            row.push(<Seat key={`${pos.x}-${pos.y}`}
                pos={pos}
                taken={taken}
                selected={selected}
                handler={taken ? undefined : () =>this.handleClick(pos)} />)
        }
        return (<div key={index} className="room-row">{row}</div>);
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
                        className="p-2 mt-3 btn btn-lg btn-primary room-submit-button"
                        onClick={(e) => this.handleSubmit()}
                        value="Kontynuuj" />
                </div>
            </div>
        );
    }
}