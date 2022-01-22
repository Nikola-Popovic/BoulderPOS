const path = require("path");
const marked = require("marked");
const renderer = new marked.Renderer;
const HtmlWebpackPlugin = require("html-webpack-plugin");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const InterpolateHtmlPlugin = require('react-dev-utils/InterpolateHtmlPlugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const { webpack, HotModuleReplacementPlugin } = require("webpack");

module.exports = {
    entry: "./src/index.tsx",
    resolve: {
      extensions: [".js", ".jsx", ".json", ".ts", ".tsx"],
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
      new HotModuleReplacementPlugin(),
      new CleanWebpackPlugin({ cleanStaleWebpackAssets: false }),
      new HtmlWebpackPlugin({
        template : './public/index.html',
        filename: './index.html',
        manifest: './public/manifest.json',
        favicon: './public/favicon.ico'
      }),
      new MiniCssExtractPlugin({
        filename: '[name]-[fullhash].css'
      })
    ],
    output: {
        path: path.resolve(__dirname, 'dist'),
        publicPath: '/',
        filename: 'bundle-[fullhash].js',
      }
  };