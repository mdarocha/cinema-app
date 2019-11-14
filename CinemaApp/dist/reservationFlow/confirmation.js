"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var Moment = require("moment");
var Confirmation = /** @class */ (function (_super) {
    __extends(Confirmation, _super);
    function Confirmation(props) {
        return _super.call(this, props) || this;
    }
    Confirmation.prototype.render = function () {
        return (React.createElement("div", null,
            React.createElement("h3", null, "Rezerwacja"),
            React.createElement("div", { className: "reservation-confirm-movie row mt-md-4 ml-md-4 mr-md-4 mb-2 mt-2" },
                React.createElement("div", { className: "col-sm-4 pl-5 pr-5 pl-sm-0 pr-sm-0" },
                    React.createElement("img", { className: "movie-poster img-fluid", src: this.props.Movie.PosterUrl })),
                React.createElement("div", { className: "col mt-3 mt-sm-0 ml-sm-3" },
                    React.createElement("h4", { className: "font-weight-bold mb-1 mb-sm-3 mb-md-5" }, this.props.Movie.Title),
                    React.createElement("h4", null, Moment(this.props.Showing.Time).locale('pl').
                        format('HH:mm, dddd D MMM')),
                    React.createElement("h4", null,
                        this.props.Showing.IsSubtitles ? "Napisy" : "Dubbing",
                        ",",
                        this.props.Showing.Is2D ? " 2D" : " 3D"),
                    React.createElement("h3", null, this.props.SelectedPlacesList))),
            React.createElement("div", { className: "d-flex justify-content-center" },
                React.createElement("input", { type: "button", className: "mr-3 mt-3 btn btn-lg btn-primary", onClick: this.props.back, value: "Anuluj" }),
                React.createElement("input", { type: "button", className: "ml-3 mt-3 btn btn-lg btn-primary", onClick: this.props.confirm, value: "Potwierd\u017A" }))));
    };
    return Confirmation;
}(React.Component));
exports.Confirmation = Confirmation;
//# sourceMappingURL=confirmation.js.map