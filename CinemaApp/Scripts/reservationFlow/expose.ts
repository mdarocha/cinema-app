import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { ReservationFlow } from "./reservation_flow";

//expose react components and library as global so
//reactjs.net can use them properly

declare global {
    namespace NodeJS {
        interface Global {
            ReservationFlow: any,

            React: any,
            ReactDOM: any,
        }
    }
}

global.React = React;
global.ReactDOM = ReactDOM;

global.ReservationFlow = ReservationFlow;