import * as React from 'react';
import * as Moment from 'moment';
import { DayButton, WeekChangeButton } from "./day_buttons";

interface IDayPicker {
    sourceUrl: string,
}

interface IDayPickerState {
    currentWeekOffset: number,
    currentDayOffset: number,
}

class DayPicker extends React.Component<IDayPicker, IDayPickerState> {
    constructor(props) {
        super(props);
        this.state = { currentWeekOffset: 0, currentDayOffset: 0 };
    }

    handleDayClick(dayNumber: number) {
        console.log(dayNumber);
        $("#movieList").load(this.props.sourceUrl, { day: dayNumber }, () => {
            console.log("loading complete");
            this.setState(prev => ({
                currentDayOffset: dayNumber
            }));
        });
    }

    handleWeekChange(change: number) {
        this.setState(prev => ({
            currentWeekOffset: prev.currentWeekOffset + change,
        }));
    }

    createDayButtons() {
        let week_start = Moment().locale('pl').add(this.state.currentWeekOffset, 'week').startOf('week');
        let today = Moment().locale('pl');

        let buttons = [];
        for (let i = 0; i < 7; i++) {
            let button_day = Moment(week_start).add(i, 'd');
            let offset = button_day.diff(today, 'd') + (button_day >= today ? 1 : 0); //ugly fix to zero-indexed dates

            buttons.push(
                <DayButton
                    key={i}
                    pos={i}
                    enabled={offset >= 0}
                    active={offset == this.state.currentDayOffset}
                    handler={() => this.handleDayClick(offset)}
                    date={button_day.toDate()} />
            );
        }

        return (<div>{buttons}</div>)
    }

    render() {
        return (
            <div className="showing-date-list justify-content-center d-flex mb-3 mb-md-5 mr-1 ml-1">
                <WeekChangeButton
                    forward={false}
                    handler={() => this.handleWeekChange(-1)}
                    enabled={this.state.currentWeekOffset > 0} />
                {this.createDayButtons()}
                <WeekChangeButton
                    forward={true}
                    handler={() => this.handleWeekChange(1)}
                    enabled={true} />
            </div>
        )
    }
}

export { DayPicker };