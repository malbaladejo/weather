
class UrlBuilder {
    constructor() {

    }

    buildDayUrl(date) {
        return this.getBaseUrl() + 'day-' + this.dateToString(date) + this.getExtension();
    }

    buildWeekUrl(firstDay) {
        let lastDay = new Date(firstDay);
        lastDay.setDate(lastDay.getDate() + 6);
        return this.getBaseUrl() + 'week-' + this.dateToString(firstDay) + '-' + this.dateToString(lastDay) + this.getExtension();
    }

    getBaseUrl() {
        return config.baseUrl;
    }

    getExtension() {
        return config.jsonExtension;
    }

    dateToString(date) {
        // date is a date
        if (date.getFullYear) {
            return date.getFullYear() + '-' + this.padDateNumber(date.getMonth() + 1) + '-' + this.padDateNumber(date.getDate());
        }

        // date is a string
        return date;
    }

    padDateNumber(value) {
        return value.toString().padStart(2, '0')
    }
}

