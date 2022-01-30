/* eslint-disable no-undef */
/* eslint-disable @typescript-eslint/no-var-requires */
const path = require("path");
const marked = require("marked");
const webpack = require("webpack");
const renderer = new marked.Renderer;
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const i18nextPlugin = require("ya-i18next-webpack-plugin").default;
const ASSET_PATH = process.env.ASSET_PATH || '/';
const PUBLIC_PATH = process.env.PUBLIC_PATH || '/public';

module.exports = {
    entry: "./src/index.tsx",
    resolve: {
      extensions: [".js", ".jsx", ".ts", ".tsx", ".json"],
    },
    module: {
      rules: [
        {
          test: /\.(ts|tsx)$/,
          loader: 'awesome-typescript-loader',
          exclude: /node_modules/,
        },
        {
          enforce: "pre",
          test: /\.js$/,
          loader: "source-map-loader",
        },
        {
          test: /\.(js|jsx)$/, 
          exclude: /node_modules/,
          use: ['babel-loader'],
        },
        { 
          test: /\.json$/, 
          exclude: /(node_modules|public)/, 
          type: 'asset/resource' 
        },
        {
          test: /\.css$/,
          use: ["style-loader", "css-loader"]
        },
        {
            test: /\.(md)$/,
            use: [
            {
              loader : 'html-loader'
            },
            {
                loader: 'markdown-loader',
                options: {
                  pedantic : true,
                  renderer
                },
            },
            ],
        },
        {
            test: /\.(png|jpg|jpeg|gif|mp4|svg|ico)(\?.*$|$)/,
            exclude: /node_modules/,
            use: ['file-loader?name=[name].[ext]'] 
        },
        {
            test: /\.(sass|scss)$/,
            use: [
              {
                loader: MiniCssExtractPlugin.loader,
              },
              'style-loader',
              'css-loader',
              'sass-loader'
            ]
          },
      ],
    },
    plugins: [
      new CleanWebpackPlugin({ cleanStaleWebpackAssets: false }),
      new HtmlWebpackPlugin({
        template : './public/index.html',
        filename: './index.html',
        manifest: './public/manifest.json',
        favicon: './public/favicon.ico'
      }),
      new MiniCssExtractPlugin({
        filename: '[name]-[fullhash].css'
      }),
      new i18nextPlugin({
        defaultLanguage: "fr",
        languages: ["fr", "en"],
        functionName: "t",
        resourcePath: "./src/lang/{{lng}}/{{ns}}.json",
        pathToSaveMissing: ".src/lang/{{lang}}//{{ns}}-missing.json"
      }),
      new webpack.DefinePlugin({
        'process.env.ASSET_PATH': JSON.stringify(ASSET_PATH),
        'process.env.PUBLIC_PATH': JSON.stringify(PUBLIC_PATH),
      }),
      new webpack.HotModuleReplacementPlugin()
    ],
    output: {
        path: path.resolve(__dirname, 'dist'),
        publicPath: 'auto',
        filename: 'bundle-[fullhash].js',
      }
  };