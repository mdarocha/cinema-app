"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function pos_to_string(pos) {
    return String.fromCharCode(97 + pos.y).toUpperCase() + (pos.x + 1);
}
exports.pos_to_string = pos_to_string;
function pos_list_to_string(position, roomw) {
    return position.sort(function (a, b) { return (((a.y * roomw) + a.x) - ((b.y * roomw) + b.x)); })
        .map(function (pos) { return pos_to_string(pos); }).join(" ");
}
exports.pos_list_to_string = pos_list_to_string;
//# sourceMappingURL=utils.js.map