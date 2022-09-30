class DayNavigationStrategy {
    constructor(fetchData) {
        this.dayEnabled = false;
        this.weekEnabled = true;

        this.fetchData = fetchData;
        this.urlBuilder = new UrlBuilder();
    }

    showDay() {
        // nothing to do
    }

    showWeek() {
        console.debug('showWeek');
        return this.fetchData.fetch(this.urlBuilder.buildWeekUrl(this.fetchData.currentData.firstDay));
    }

    previous() {
        console.debug('previous');
        return this.fetchData.fetch(this.getPreviousUrl());
    }

    next() {
        console.debug('next');
        return this.fetchData.fetch(this.getNextUrl());
    }

    getPreviousUrl() {
        return this.urlBuilder.buildDayUrl(this.getPreviousDay());
    }

    getNextUrl() {
        return this.urlBuilder.buildDayUrl(this.getNextDay());
    }

    getPreviousDay() {
        let previousDate = new Date(this.fetchData.currentData.date);
        previousDate.setDate(previousDate.getDate() - 1);

        return previousDate;
    }

    getNextDay() {
        let nextDate = new Date(this.fetchData.currentData.date);
        nextDate.setDate(nextDate.getDate() + 1);

        return nextDate;
    }
}