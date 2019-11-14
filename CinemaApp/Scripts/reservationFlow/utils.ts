import { PlacePosition } from "./reservation_flow_view_model";

function pos_to_string(pos: PlacePosition) {
    return String.fromCharCode(97 + pos.y).toUpperCase() + (pos.x + 1);
}

function pos_list_to_string(position: Array<PlacePosition>, roomw: number) {
    return position.sort((a, b) => (((a.y * roomw) + a.x) - ((b.y * roomw) + b.x)))
                    .map(pos => pos_to_string(pos)).join(" ");
}
export { pos_to_string, pos_list_to_string };