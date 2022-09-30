class NavigationStrategyFactory {
    constructor() {

    }

    create(fetchData) {
        if (fetchData.currentData.type === dayPeriod) {
            return new DayNavigationStrategy(fetchData);
        }

        if (fetchData.currentData.type === weekPeriod) {
            return new WeekNavigationStrategy(fetchData);
        }

        throw ('NavigationStrategyFactory: type [' + input.type + '] is not supported.');
    }
}