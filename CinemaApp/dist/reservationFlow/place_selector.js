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
var show_alert_1 = require("../show_alert");
var Seat = /** @class */ (function (_super) {
    __extends(Seat, _super);
    function Seat(props) {
        return _super.call(this, props) || this;
    }
    Seat.prototype.render = function () {
        var styles = "room-seat ";
        styles += this.props.taken ? "seat-taken" : (this.props.selected ? "seat-selected" : "seat-free");
        var seat_number = this.props.pos.x + 1;
        var seat_row = this.props.pos.x === 0 ? String.fromCharCode(97 + this.props.pos.y).toUpperCase() : "";
        return (React.createElement("div", { className: styles, "seat-number": seat_number, "seat-row": seat_row, onClick: this.props.handler }));
    };
    return Seat;
}(React.Component));
var Room = /** @class */ (function (_super) {
    __extends(Room, _super);
    function Room(props) {
        var _this = _super.call(this, props) || this;
        _this.state = { SelectedPlaces: Array.from(_this.props.SelectedPlacesStart) };
        return _this;
    }
    Room.prototype.handleClick = function (pos) {
        var found = this.state.SelectedPlaces.find(function (p) { return p.x === pos.x && p.y === pos.y; });
        if (found) {
            this.setState(function (prev) { return ({
                SelectedPlaces: prev.SelectedPlaces.filter(function (p) { return p.x !== pos.x || p.y !== pos.y; })
            }); });
        }
        else if (this.state.SelectedPlaces.length < this.props.MaxTickets) {
            this.setState(function (prev) { return ({
                SelectedPlaces: prev.SelectedPlaces.concat([pos])
            }); });
        }
        else {
            show_alert_1.show_alert("Nie mo\u017Cesz zarezerwowa\u0107 wi\u0119cej ni\u017C " + this.props.MaxTickets + " miejsc naraz");
        }
    };
    Room.prototype.createRow = function (index) {
        var _this = this;
        var row = [];
        var _loop_1 = function (i) {
            var pos = {
                x: i,
                y: index,
            };
            var filter = (function (p) { return p.x === pos.x && p.y === pos.y; });
            var taken = Boolean(this_1.props.TakenPlaces.find(filter));
            var selected = Boolean(this_1.state.SelectedPlaces.find(filter));
            row.push(React.createElement(Seat, { key: pos.x + "-" + pos.y, pos: pos, taken: taken, selected: selected, handler: taken ? undefined : function () { return _this.handleClick(pos); } }));
        };
        var this_1 = this;
        for (var i = 0; i < this.props.RoomWidth; i++) {
            _loop_1(i);
        }
        return (React.createElement("div", { key: index, className: "room-row" }, row));
    };
    Room.prototype.createRoom = function () {
        var room = [];
        for (var i = 0; i < this.props.RoomHeight; i++) {
            room.push(this.createRow(i));
        }
        return room;
    };
    Room.prototype.render = function () {
        var _this = this;
        return (React.createElement("div", null,
            React.createElement("div", { className: "room" }, this.createRoom()),
            React.createElement("div", { className: "d-flex justify-content-center" },
                React.createElement("input", { type: "button", className: "p-2 mt-3 btn btn-lg btn-primary", onClick: function () { return _this.props.submit(_this.state.SelectedPlaces); }, value: "Kontynuuj" }))));
    };
    return Room;
}(React.Component));
exports.Room = Room;
//# sourceMappingURL=place_selector.js.map