const path = require("path");

module.exports = {
    mode: 'development',

    entry: {
        reservationFlow: './Scripts/reservationFlow/expose.ts',
        dayPicker: './Scripts/dayPicker/expose.ts',
        app: './Scripts/app.ts',
        jquery: [
            'jquery',
            'jquery-ajax',
            'jquery-ajax-unobtrusive',
            'jquery-validation',
            'jquery-validation-unobtrusive',
        ],
    },

    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: [
                    'ts-loader'
                ],
                exclude: '/node_modules/'
            },
            {
                test: /\.s?css$/,
                use: [
                    'style-loader',
                    'css-loader',
                    {
                        loader: 'postcss-loader',
                        options: {
                            plugins: function () {
                                return [
                                    require('precss'),
                                    require('autoprefixer')
                                ];
                            }
                        }
                    },
                    'sass-loader',
                ]
            },
            {
                test: require.resolve('jquery'),
                use: [{
                    loader: 'expose-loader', options: '$'
                },
                {
                    loader: 'expose-loader', options: 'jQuery'
                }]
            },
        ]
    },

    resolve: {
        extensions: ['.tsx', '.ts', '.js', '.json']
    },

    plugins: [
    ],

    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: '[name].bundle.js'
    }
}