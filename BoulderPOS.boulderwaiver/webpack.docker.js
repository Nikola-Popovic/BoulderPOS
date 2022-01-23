/* eslint-disable no-undef */
/* eslint-disable @typescript-eslint/no-var-requires */
const { merge } = require('webpack-merge');
const common = require('./webpack.common.js');

module.exports = merge(common, {
    mode: 'development',
    devtool: 'inline-source-map',
    devServer: {
        hot : true,
        disableHostCheck : true,
        host: '0.0.0.0',
        contentBase: './dist',
        historyApiFallback: true,
        port: 3000,
        watchOptions: {
            poll: true 
        }
    }
});