import { PlacePosition } from "./reservation_flow_view_model";

interface ISeat {
    taken: boolean,
    selected: boolean,
    pos: PlacePosition,
    handler(): void,
}

interface IRoom {
    readonly TakenPlaces: Array<PlacePosition>,
    readonly MaxTickets: number,
    readonly RoomWidth: number,
    readonly RoomHeight: number,

    SelectedPlacesStart: Array<PlacePosition>,

    submit(selected: Array<PlacePosition>): void,
}

interface IRoomState {
    SelectedPlaces: Array<PlacePosition>
}


export { ISeat, IRoom, IRoomState }
