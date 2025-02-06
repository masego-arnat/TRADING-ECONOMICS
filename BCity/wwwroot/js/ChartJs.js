$(document).ready(function () {

    let chartInstance; // Variable to hold the chart instance
    $('#countryForm').on('submit', function (e) {
        e.preventDefault(); // Prevent the default form submission
        if (chartInstance) {
            chartInstance.destroy(); // Destroy the existing chart instance if it exists
        }
        var countryName = $('.single').val();
        $('#Search').hide();
        $('#Searchloader').show();

        console.log("this is to heck wahts in dis ting ", countryName);
        // AJAX call to fetch economic data based on the country name
        $.ajax({
            url: 'Forecast/GetEconomicData',
            // url: '@Url.Action("GetEconomicData", "EconomicData")',
            type: 'POST',
            data: { country: countryName },
            success: function (data) {

                $('#Search').show();
                $('#Searchloader').hide();
                console.log("thid id the data ", data);
                // loadChart(data);
                // Check if data is empty or does not contain expected properties
                if (!data || data.length === 0) {
                    // Display a message indicating no data is available
                    // $('#message').text('No data available for the selected country.').show();
                    alert('Sorry, data is not available for the selected country. Please choose from the supported countries.');
                    $('#economicChart').hide(); // Hide the chart if it exists
                    if (chartInstance) {
                        chartInstance.destroy(); // Destroy the existing chart instance if it exists
                    }
                } else {
                    $('#message').hide(); // Hide the message if data is available
                    $('#economicChart').show(); // Show the chart
                    loadChart(data); // Load the new chart
                }
            },
            error: function (error) {


                console.error("Error fetching data:", error);

                if (chartInstance) {
                    chartInstance.destroy(); // Destroy the existing chart instance if it exists
                }
            }
        });
    });

    // Function to load chart
    function loadChart(data) {
        var ctx = document.getElementById('economicChart').getContext('2d');
        var labels = ['Q1', 'Q2', 'Q3', 'Q4']; // Quarterly labels
        var datasets = [];

        // Prepare datasets for each indicator
        data.forEach(item => {
            datasets.push({
                label: item.title,
                data: [item.q1, item.q2, item.q3, item.q4],
                borderColor: getRandomColor(),
                borderWidth: 2,
                fill: false
            });
        });

        // Create the chart
        chartInstance = new Chart(ctx, {
            type: 'line',
            // type: 'doughnut',
            // type: 'pie',

            data: {
                labels: labels,
                datasets: datasets
            },
            options: {
                responsive: true,
                animations: {
                    tension: {
                        duration: 1000,
                        easing: 'linear',
                        from: 1,
                        to: 0,
                        loop: false
                    }
                },
                scales: {
                    x: {
                        title: {
                            display: true,
                            text: 'Quarters'
                        }
                    },
                    y: {
                        title: {
                            display: true,
                            text: 'Value'
                        }
                    }
                }
            }
        });
    }

    // Function to generate random colors for datasets
    function getRandomColor() {
     
        const hue = Math.floor(Math.random() * 360); // Random hue between 0 and 360
        const saturation = '70%'; // Fixed saturation for vibrancy
        const lightness = '50%'; // Fixed lightness for consistency
        return `hsl(${hue}, ${saturation}, ${lightness})`;
    }
});