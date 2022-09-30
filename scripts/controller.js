class Controller {
    constructor() {
        this.fetchData = new FetchData();
        this.urlBuilder = new UrlBuilder();
        this.navigationStrategyFactory = new NavigationStrategyFactory();
    }

    start() {
        this.fetchData.fetch(this.urlBuilder.buildDayUrl('2022-07-26'));
    }

    showDay() {
        if (!this.getNavigationStrategy().dayEnabled) {
            return;
        }

        this.getNavigationStrategy().showDay();
    }

    showWeek() {
        if (!this.getNavigationStrategy().weekEnabled) {
            return;
        }
        this.getNavigationStrategy().showWeek();
    }

    previous() {
        this.getNavigationStrategy().previous();
    }

    next() {
        this.getNavigationStrategy().next();
    }

    getNavigationStrategy() {
        return this.navigationStrategyFactory.create(this.fetchData);
    }
}