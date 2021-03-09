const { merge } = require('webpack-merge');
const common = require('./webpack.common.js');

// @ts-ignore
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