import * as React from 'react';
import * as Moment from 'moment';

interface IDayButton {
    handler(): void,
    date: Date,
    enabled: boolean,
    active: boolean,
    pos: number,
}

class DayButton extends React.Component<IDayButton> {
    constructor(props) {
        super(props);
    }

    render() {
        let styles = "showing-date-button btn btn-outline-info mr-1 ml-1 mt-1 ";
        styles += this.props.active ? "active " : "";
        styles += this.props.pos == 0 ? "ml-md-3 " : "";
        styles += this.props.pos == 6 ? "mr-md-3 " : "";

        return (
            <button
                disabled={!this.props.enabled}
                onClick={this.props.enabled ? this.props.handler : null}
                className={styles}>
                <span className="font-weight-bold mb-1">{Moment(this.props.date).locale('pl').format('dddd')}</span><br />
                <span>{Moment(this.props.date).locale('pl').format('D.MM')}</span>
            </button>
        )
    }
}

interface IWeekChangeButton {
    handler(): void,
    forward: boolean,
    enabled: boolean,
}

class WeekChangeButton extends React.Component<IWeekChangeButton> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <button
                disabled={!this.props.enabled}
                onClick={this.props.enabled ? this.props.handler : null}
                className="btn btn-sm btn-outline-info mr-1 ml-1">
                {this.props.forward ? ">>" : "<<"}
            </button>
        )
    }
}

export { DayButton, WeekChangeButton };