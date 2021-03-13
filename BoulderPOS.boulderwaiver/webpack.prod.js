const { merge } = require('webpack-merge');
const common = require('./webpack.common.js');

// @ts-ignore
module.exports = merge(common, {
  mode: 'production',
  devtool: 'source-map'
});