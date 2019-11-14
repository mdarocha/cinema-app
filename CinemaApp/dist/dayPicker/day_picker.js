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
var day_buttons_1 = require("./day_buttons");
var DayPicker = /** @class */ (function (_super) {
    __extends(DayPicker, _super);
    function DayPicker(props) {
        var _this = _super.call(this, props) || this;
        _this.state = { currentWeekOffset: 0, currentDayOffset: 0 };
        return _this;
    }
    DayPicker.prototype.handleDayClick = function (dayNumber) {
        var _this = this;
        console.log(dayNumber);
        $("#movieList").load(this.props.sourceUrl, { day: dayNumber }, function () {
            console.log("loading complete");
            _this.setState(function (prev) { return ({
                currentDayOffset: dayNumber
            }); });
        });
    };
    DayPicker.prototype.handleWeekChange = function (change) {
        this.setState(function (prev) { return ({
            currentWeekOffset: prev.currentWeekOffset + change,
        }); });
    };
    DayPicker.prototype.createDayButtons = function () {
        var _this = this;
        var week_start = Moment().locale('pl').add(this.state.currentWeekOffset, 'week').startOf('week');
        var today = Moment().locale('pl');
        var buttons = [];
        var _loop_1 = function (i) {
            var button_day = Moment(week_start).add(i, 'd');
            var offset = button_day.diff(today, 'd') + (button_day >= today ? 1 : 0); //ugly fix to zero-indexed dates
            buttons.push(React.createElement(day_buttons_1.DayButton, { key: i, pos: i, enabled: offset >= 0, active: offset == this_1.state.currentDayOffset, handler: function () { return _this.handleDayClick(offset); }, date: button_day.toDate() }));
        };
        var this_1 = this;
        for (var i = 0; i < 7; i++) {
            _loop_1(i);
        }
        return (React.createElement("div", null, buttons));
    };
    DayPicker.prototype.render = function () {
        var _this = this;
        return (React.createElement("div", { className: "showing-date-list justify-content-center d-flex mb-3 mb-md-5 mr-1 ml-1" },
            React.createElement(day_buttons_1.WeekChangeButton, { forward: false, handler: function () { return _this.handleWeekChange(-1); }, enabled: this.state.currentWeekOffset > 0 }),
            this.createDayButtons(),
            React.createElement(day_buttons_1.WeekChangeButton, { forward: true, handler: function () { return _this.handleWeekChange(1); }, enabled: true })));
    };
    return DayPicker;
}(React.Component));
exports.DayPicker = DayPicker;
//# sourceMappingURL=day_picker.js.map