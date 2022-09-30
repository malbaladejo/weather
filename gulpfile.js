const { src, dest, series } = require('gulp');
const minify = require('gulp-minify');
const concat = require('gulp-concat');
const htmlreplace = require('gulp-html-replace');
const version = require('./version');
const rename = require("gulp-rename");
const replace = require('gulp-replace');
const htmlmin = require('gulp-htmlmin');
const cleancss = require('gulp-clean-css');

function buildJs() {
    return src(['version.js', 'config/config-prod.js', 'scripts/*.js', 'scripts/**/*.js',])
        .pipe(concat('script-' + version() + '.js'))
        .pipe(replace('env: \'dev\'', 'env: \'prod\''))
        .pipe(minify())
        .pipe(dest('dist/' + version()));
}
exports.buildJs = buildJs;

function buildCss() {
    return src('styles.css')
        .pipe(rename('styles-' + version() + '.css'))
        .pipe(dest('dist/' + version()));
}
exports.buildCss = buildCss;

function buildCssMin() {
    return src('styles.css')
        .pipe(rename('styles-' + version() + '-min.css'))
        .pipe(cleancss({ compatibility: 'ie8' }))
        .pipe(dest('dist/' + version()));
}
exports.buildCssMin = buildCssMin;

function buildHtml() {
    return src('index.html')
        .pipe(htmlreplace({
            'js': 'script-' + version() + '-min.js',
            'css': 'styles-' + version() + '-min.css'
        }))
        .pipe(htmlmin({ collapseWhitespace: true }))
        .pipe(dest('dist/' + version()));
}
exports.buildHtml = buildHtml;

function buildHtmlDebug() {
    return src('index.html')
        .pipe(rename('index-debug.html'))
        .pipe(htmlreplace({
            'js': 'script-' + version() + '.js',
            'css': 'styles-' + version() + '.css'
        }))
        .pipe(dest('dist/' + version()));
}
exports.buildHtmlDebug = buildHtmlDebug;

exports.default = series(buildJs, buildCssMin, buildCss, buildHtml, buildHtmlDebug);