import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { DayPicker } from "./day_picker";

//expose react components and library as global so
//reactjs.net can use them properly

declare global {
    namespace NodeJS {
        interface Global {
            DayPicker: any,

            React: any,
            ReactDOM: any,
        }
    }
}

global.React = React;
global.ReactDOM = ReactDOM;

global.DayPicker = DayPicker;