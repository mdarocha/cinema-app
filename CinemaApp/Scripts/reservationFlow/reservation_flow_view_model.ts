import { Showing, Movie, PlacePosition } from "../models";

interface ReservationFlowViewModel {
    Showing: Showing,
    Movie: Movie,

    SelectedPlaces: Array<PlacePosition>,
    TakenPlaces: Array<PlacePosition>,

    PostUrl: string,

    readonly RoomWidth: number,
    readonly RoomHeight: number,
    readonly MaxTickets: number,
}

export { PlacePosition, ReservationFlowViewModel }