class WeekNavigationStrategy {
    constructor(fetchData) {
        this.dayEnabled = true;
        this.weekEnabled = false;

        this.fetchData = fetchData;
        this.urlBuilder = new UrlBuilder();
    }

    showDay() {
        return this.fetchData.fetch(this.urlBuilder.buildDayUrl(this.fetchData.currentData.firstDay));
    }

    showWeek() {
        // Nothing to do
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
        return this.urlBuilder.buildWeekUrl(this.getPreviousWeek());
    }

    getNextUrl() {
        return this.urlBuilder.buildWeekUrl(this.getNextWeek());
    }

    getPreviousWeek() {
        let previousDate = new Date(this.fetchData.currentData.firstDay);
        previousDate.setDate(previousDate.getDate() - 7);

        return previousDate;
    }

    getNextWeek() {
        let nextDate = new Date(this.fetchData.currentData.lastDay);
        nextDate.setDate(nextDate.getDate() + 1);

        return nextDate;
    }
}