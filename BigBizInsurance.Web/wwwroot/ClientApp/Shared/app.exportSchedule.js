$app.exportSchedule = (function () {
    function Init() {
        $(document).ready(function () {
            $('.ExportToPdf').click(function () {
                var tableHTML = document.getElementById('div_schedule').outerHTML;

                exportHtml.exportToPdf($scheduleResources.ScheduleFileName, exportJTable.pdfTitle($scheduleResources.ScheduleFileName) + tableHTML);
            });

            $('.ExportToExcel').click(function () {
                var tableHTML = document.getElementById('div_schedule').outerHTML;

                exportHtml.exportToExcel($scheduleResources.ScheduleFileName, tableHTML);
            });

        });
    }


    return { Init };
}());

