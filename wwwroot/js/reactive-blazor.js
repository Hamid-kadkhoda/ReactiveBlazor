

async function RenderNewCanvasChart(identifier, labels, dataset,label = "") {

    await setTimeout(() => { }, 0);

    const ctx = document.getElementById(`${identifier}`);

    let chart = new Chart(ctx, {
        type: 'radar',
        data: {
            labels: labels,
            datasets: [{
                label: label, 
                data: dataset,
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    console.log(chart)
} 