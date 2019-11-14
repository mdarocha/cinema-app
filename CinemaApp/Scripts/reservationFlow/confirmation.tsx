import * as React from 'react';
import * as Moment from 'moment';
import { Showing, Movie } from "../models";

interface IComfirmation {
    Showing: Showing,
    Movie: Movie,
    SelectedPlacesList: string,

    confirm(): void,
    back(): void,
}

class Confirmation extends React.Component<IComfirmation> {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <h3>Rezerwacja</h3>
                <div className="reservation-confirm-movie row mt-md-4 ml-md-4 mr-md-4 mb-2 mt-2">
                    <div className="col-sm-4 pl-5 pr-5 pl-sm-0 pr-sm-0">
                        <img className="movie-poster img-fluid" src={this.props.Movie.PosterUrl} />
                    </div>
                    <div className="col mt-3 mt-sm-0 ml-sm-3">
                        <h4 className="font-weight-bold mb-1 mb-sm-3 mb-md-5">
                            {this.props.Movie.Title}
                        </h4>
                        <h4>
                            {Moment(this.props.Showing.Time).locale('pl').
                                format('HH:mm, dddd D MMM')}
                        </h4>
                        <h4>
                            {this.props.Showing.IsSubtitles ? "Napisy" : "Dubbing"},
                            {this.props.Showing.Is2D ? " 2D" : " 3D"}
                        </h4>
                        <h3>
                            {this.props.SelectedPlacesList}
                        </h3>
                    </div>
                </div>
                <div className="d-flex justify-content-center">
                    <input type="button"
                        className="mr-3 mt-3 btn btn-lg btn-primary"
                        onClick={this.props.back}
                        value="Anuluj" />
                    <input type="button"
                        className="ml-3 mt-3 btn btn-lg btn-primary"
                        onClick={this.props.confirm}
                        value="Potwierdź" />
                </div>
            </div>
        );
    }
}

export { Confirmation, IComfirmation };