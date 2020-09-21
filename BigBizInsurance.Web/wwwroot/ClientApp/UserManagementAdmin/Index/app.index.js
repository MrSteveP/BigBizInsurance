$app.index = (function () {
    function Init(initialData) {
        $(document).ready(function () {

            init_daterangepicker('div_userPerDayReport', GetReportData, initialData.dashboardDataBaseUrl);
            GetReportData(initialData.dashboardDataBaseUrl,'', '');

        });
    }

    function GetReportData(baseUrl, from, to) {
        ajaxRequests.doAjax_Get(
            baseUrl + '&from=' + from + '&to=' + to,
            function (data) {

                var chart_plot_01_settings_test = {
                    series: {
                        lines: {
                            show: false,
                            fill: true
                        },
                        splines: {
                            show: true,
                            tension: 0.4,
                            lineWidth: 1,
                            fill: 0.4
                        },
                        points: {
                            radius: 0,
                            show: true
                        },
                        shadowSize: 2
                    },
                    grid: {
                        verticalLines: true,
                        hoverable: true,
                        clickable: true,
                        tickColor: "#d5d5d5",
                        borderWidth: 1,
                        color: '#fff'
                    },
                    colors: ["rgba(38, 185, 154, 0.38)", "rgba(3, 88, 106, 0.38)"],
                    xaxis: {
                        tickColor: "rgba(51, 51, 51, 0.06)",
                        mode: "time",
                        tickSize: [1, "day"],
                        //tickLength: 10,
                        axisLabel: "Date",
                        axisLabelUseCanvas: true,
                        axisLabelFontSizePixels: 12,
                        axisLabelFontFamily: 'Verdana, Arial',
                        axisLabelPadding: 10
                    },
                    yaxis: {
                        ticks: 8,
                        tickColor: "rgba(51, 51, 51, 0.06)",
                    },
                    tooltip: false
                };

                var arr_data1_test = [];
                for (var i = 0; i < data.data.length; i++) {
                    var record = data.data[i];
                    arr_data1_test.push([gd(record.year, record.month, record.day), record.count]);
                }


                if ($("#chart_plot_01_test").length) {
                    console.log('Plot1');

                    $.plot($("#chart_plot_01_test"), [arr_data1_test], chart_plot_01_settings_test);
                }


            }, function (error) { });
    }

    function gd(year, month, day) {
        return new Date(year, month - 1, day).getTime();
    }

    return { Init };
}());




