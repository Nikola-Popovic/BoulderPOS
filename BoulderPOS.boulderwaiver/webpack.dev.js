const { merge } = require('webpack-merge');
const common = require('./webpack.common.js');

// @ts-ignore
module.exports = merge(common, {
    mode: 'development',
    devtool: 'inline-source-map',
    devServer: {
        hot : true,
        contentBase: './dist',
        historyApiFallback: true,
    }
});