class DayDto {
    constructor() {
        this.type = "";
        this.date = null;
        this.firstDay = null;
        this.lastDay = null;
        this.data = new DayData();
    }
}

class DayData {
    constructor() {
        this.hour = null;
        this.OutHumidity = 0;
        this.InHumidity = 0;
        this.OutTemperature = 0;
        this.InTemperature = 0;
        this.Rain = 0.0;
        this.Wind = null;
        this.WindDirection = "";
    }
}

class WindDirection {
    constructor() {
        this.direction = "";
        this.percent = "";
    }
}
class WeekDto {
    constructor() {
        this.type = "";
        this.firstDayOfWeek = null;
        this.lastDayOfWeek = null;
        this.data = [];
        this.windDirections = [];
    }
}

class WeekData {
    constructor() {
        this.date = null;
        this.min = new DayData();
        this.max = new DayData();
        this.rain = 0.0;
    }
}

class DataConverter {

    mapDayData(item) {

    }

    mapDayData(item) {

    }

    mapWindDirection(item) {

    }

    mapWeekDto(item) {

    }

    mapWeekData(item) {

    }
}