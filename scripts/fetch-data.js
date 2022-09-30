class FetchData {
    constructor() {
        this.windDirectionsChart = new WindDirectionsChart();
        this.windChart = new WindChart();
        this.rainChart = new RainChart();
        this.temperatureChart = new TemperatureChart();
        this.periodStrategyFactory = new PeriodStrategyFactory();
        this.currentData = null;
    }

    fetch(url) {
        return fetch(url)
            .then(response => {
                return this.parseJson(response);
            })
            .then(jsonResponse => {
                this.buildCharts(jsonResponse);
                return jsonResponse;
            })
            .catch(e => {
                alert(e);
            });
    }

    parseJson(response) {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response.json();
    }

    buildCharts(data) {
        this.currentData = data;
        this.temperatureChart.build(data);
        this.rainChart.build(data);
        this.windChart.build(data);
        this.windDirectionsChart.build(data);

        this.setTitle(data);
    }

    setTitle(data) {
        document.getElementById('title').innerHTML = this.periodStrategyFactory.create(data).getTitle(data);
    }
}