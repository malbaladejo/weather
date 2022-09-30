const dayPeriod = 'day';
const weekPeriod = 'week';

let controller = null;

window.onload = function () {
    controller = new Controller();
    controller.start();
}

function showDay() {
    controller.showDay();
}

function showWeek() {
    controller.showWeek();
}

function previous() {
    controller.previous();
}

function next() {
    controller.next();
}