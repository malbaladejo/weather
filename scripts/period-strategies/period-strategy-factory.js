class PeriodStrategyFactory {
    constructor() { }

    create(input) {
        if (input.type === dayPeriod) {
            return new DayStrategy();
        }

        if (input.type === weekPeriod) {
            return new WeekStrategy();
        }

        throw ('PeriodStrategyFactory: type [' + input.type + '] is not supported.');
    }
}