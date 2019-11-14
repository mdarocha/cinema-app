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
var DayButton = /** @class */ (function (_super) {
    __extends(DayButton, _super);
    function DayButton(props) {
        return _super.call(this, props) || this;
    }
    DayButton.prototype.render = function () {
        var styles = "showing-date-button btn btn-outline-info mr-1 ml-1 mt-1 ";
        styles += this.props.active ? "active " : "";
        styles += this.props.pos == 0 ? "ml-md-3 " : "";
        styles += this.props.pos == 6 ? "mr-md-3 " : "";
        return (React.createElement("button", { disabled: !this.props.enabled, onClick: this.props.enabled ? this.props.handler : null, className: styles },
            React.createElement("span", { className: "font-weight-bold mb-1" }, Moment(this.props.date).locale('pl').format('dddd')),
            React.createElement("br", null),
            React.createElement("span", null, Moment(this.props.date).locale('pl').format('D.MM'))));
    };
    return DayButton;
}(React.Component));
exports.DayButton = DayButton;
var WeekChangeButton = /** @class */ (function (_super) {
    __extends(WeekChangeButton, _super);
    function WeekChangeButton(props) {
        return _super.call(this, props) || this;
    }
    WeekChangeButton.prototype.render = function () {
        return (React.createElement("button", { disabled: !this.props.enabled, onClick: this.props.enabled ? this.props.handler : null, className: "btn btn-sm btn-outline-info mr-1 ml-1" }, this.props.forward ? ">>" : "<<"));
    };
    return WeekChangeButton;
}(React.Component));
exports.WeekChangeButton = WeekChangeButton;
//# sourceMappingURL=day_buttons.js.map