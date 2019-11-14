interface PlacePosition {
    x: number,
    y: number
}

interface Movie {
    Title: string,
    Description: string,
    Runtime: number,
    PosterUrl: string,
}

interface Showing {
    Movie: Movie,
    Time: Date,
    Is2D: boolean,
    IsSubtitles: boolean,
}

export { PlacePosition, Movie, Showing };