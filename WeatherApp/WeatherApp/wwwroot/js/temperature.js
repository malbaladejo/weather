﻿window.addEventListener('load', () => {
    buildTemperatureChart(data);
});

function buildTemperatureChart(data) {
    const chartCtx = document.getElementById('chart').getContext('2d');

    new Chart(chartCtx, {
        data: {
            labels: data.map(x => x.label),
            datasets: buildTemperatureDatasets(data)
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom'
                }
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function buildTemperatureDatasets(data) {

    return [{
        type: 'line',
        label: inLabel,
        data: data.map(x => x.inTemperature),
        elements: {
            point: {
                radius: 0
            }
        },
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)'
        ],
        borderColor: [
            'rgba(255, 99, 132, 1)'
        ],
        borderWidth: 1
    },
    {
        type: 'line',
        label: outLabel,
        data: data.map(x => x.outTemperature),
        elements: {
            point: {
                radius: 0
            }
        },
        backgroundColor: [
            'rgba(255, 206, 86, 0.2)'
        ],
        borderColor: [
            'rgba(255, 206, 86, 1)'
        ],
        borderWidth: 1
    }];
}