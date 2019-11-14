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
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var Moment = require("moment");
var place_selector_1 = require("./place_selector");
var confirmation_1 = require("./confirmation");
var utils_1 = require("./utils");
var show_alert_1 = require("../show_alert");
var ReservationFlowState;
(function (ReservationFlowState) {
    ReservationFlowState[ReservationFlowState["PlaceSelection"] = 0] = "PlaceSelection";
    ReservationFlowState[ReservationFlowState["Confirmation"] = 1] = "Confirmation";
})(ReservationFlowState || (ReservationFlowState = {}));
var ReservationFlow = /** @class */ (function (_super) {
    __extends(ReservationFlow, _super);
    function ReservationFlow(props) {
        var _this = _super.call(this, props) || this;
        _this.state = { Status: ReservationFlowState.PlaceSelection, SelectedPlaces: [] };
        return _this;
    }
    ReservationFlow.prototype.handleContinue = function (selected) {
        if (selected.length > 0) {
            this.setState(function (prev) { return ({
                SelectedPlaces: selected,
                Status: ReservationFlowState.Confirmation
            }); });
        }
    };
    ReservationFlow.prototype.handleBack = function () {
        this.setState(function (prev) { return ({
            Status: ReservationFlowState.PlaceSelection
        }); });
    };
    ReservationFlow.prototype.handleSubmit = function () {
        var url = this.props.PostUrl;
        var model = __assign({}, this.props);
        model.SelectedPlaces = this.state.SelectedPlaces;
        $.post(url, { 'model': model }, function (data) {
            if (data.success) {
                window.location.replace(data.redirect_url);
            }
            else {
                show_alert_1.show_alert("Wystąpił błąd");
            }
        });
    };
    ReservationFlow.prototype.render = function () {
        var _this = this;
        var view;
        switch (this.state.Status) {
            case ReservationFlowState.PlaceSelection:
                var room = {
                    TakenPlaces: this.props.TakenPlaces,
                    MaxTickets: this.props.MaxTickets,
                    RoomWidth: this.props.RoomWidth,
                    RoomHeight: this.props.RoomHeight,
                    SelectedPlacesStart: this.state.SelectedPlaces,
                    submit: function (s) { return _this.handleContinue(s); },
                };
                view = (React.createElement("div", null,
                    React.createElement("h5", { className: "text-center" },
                        this.props.Movie.Title,
                        " - ",
                        Moment(this.props.Showing.Time).locale('pl').format('HH:mm, dddd')),
                    React.createElement("h6", { className: "text-center" },
                        this.props.Showing.IsSubtitles ? "Napisy" : "Dubbing",
                        ",",
                        this.props.Showing.Is2D ? " 2D" : " 3D"),
                    React.createElement(place_selector_1.Room, __assign({}, room))));
                break;
            case ReservationFlowState.Confirmation:
                var selected_string = utils_1.pos_list_to_string(this.state.SelectedPlaces, this.props.RoomWidth);
                var confirmation = {
                    Showing: this.props.Showing,
                    Movie: this.props.Movie,
                    SelectedPlacesList: selected_string,
                    confirm: function () { return _this.handleSubmit(); },
                    back: function () { return _this.handleBack(); },
                };
                view = (React.createElement(confirmation_1.Confirmation, __assign({}, confirmation)));
        }
        return (React.createElement("div", null, view));
    };
    return ReservationFlow;
}(React.Component));
exports.ReservationFlow = ReservationFlow;
//# sourceMappingURL=reservation_flow.js.map