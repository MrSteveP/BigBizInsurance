
var ajaxRequests = {

    doAjax_Get: function (url, onSuccess, onError) {
        $.ajax({
            method: "GET",
            url: url
        }).done(function (result) {
            if (onSuccess)
                onSuccess(result);
        }).fail(function (err) {
            if (onError) {
                onError(err);
            }
        });
    },
    doAjax_Post: function (url, data, onSuccess, onError) {
        $.ajax({
            method: "POST",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            //contentType: "application/json;",// charset=utf-8",
            data: data,
            url: url
        }).done(function (result) {
            if (onSuccess)
                onSuccess(result);
        }).fail(function (err) {
            if (onError) {
                onError(err);
            }
        });
    },
    doAjax_PostForPdf: function (url, data, onSuccess, onError) {
        $.ajax({
            method: "POST",
            headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },
            contentType: "application/json; charset=utf-8",
            data: data,
            url: url
        }).done(function (result) {
            if (onSuccess)
                onSuccess(result);
        }).fail(function (err) {
            if (onError) {
                onError(err);
            }
        });
    },
    doAjax_PostWithUploadFile: function (formId, pageHandlerUrl, onComplete) {

        if ($('#' + formId).valid()) {

            var frmData = new FormData(document.getElementById(formId));

            var xhr = new XMLHttpRequest();
            xhr.onreadystatechange = function () {
                if (xhr.readyState === 4 && xhr.status === 200) {
                    var jsonResponse = JSON.parse(xhr.response);
                    onComplete(jsonResponse);

                    hideOverlay();
                }
            }
            xhr.open('post', pageHandlerUrl, true);
            xhr.send(frmData);

            showOverlay();
        }
        else {
            hideOverlay();
        }
    }

};



////**************  Date range picker *************///

// Html Sample
//<div id="div_userPerDayReport" class="pull-right" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc">
//    <i class="glyphicon glyphicon-calendar fa fa-calendar"></i>
//    <span>December 30, 2014 - January 28, 2015</span> <b class="caret"></b>
//</div>
function init_daterangepicker(divId, applyFunction, baseUrl) {

    if (typeof ($.fn.daterangepicker) === 'undefined') { return; }
    console.log('init_daterangepicker');

    var cb = function (start, end, label) {
        console.log(start.toISOString(), end.toISOString(), label);
        $('#' + divId + ' span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    };

    var optionSet1 = {
        startDate: moment().subtract(29, 'days'),
        endDate: moment(),
        //minDate: '01/01/2012',
        //maxDate: '12/31/2015',
        dateLimit: {
            days: 60
        },
        showDropdowns: true,
        showWeekNumbers: true,
        timePicker: false,
        timePickerIncrement: 1,
        timePicker12Hour: true,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        },
        opens: 'left',
        buttonClasses: ['btn btn-default'],
        applyClass: 'btn-small btn-primary',
        cancelClass: 'btn-small',
        format: 'MM/DD/YYYY',
        separator: ' to ',
        locale: {
            applyLabel: 'Submit',
            cancelLabel: 'Clear',
            fromLabel: 'From',
            toLabel: 'To',
            customRangeLabel: 'Custom',
            daysOfWeek: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            firstDay: 1
        }
    };

    $('#' + divId + ' span').html(moment().subtract(29, 'days').format('MMMM D, YYYY') + ' - ' + moment().format('MMMM D, YYYY'));
    $('#' + divId + '').daterangepicker(optionSet1, cb);
    $('#' + divId + '').on('show.daterangepicker', function () {
        console.log("show event fired");
    });
    $('#' + divId + '').on('hide.daterangepicker', function () {
        console.log("hide event fired");
    });
    $('#' + divId + '').on('apply.daterangepicker', function (ev, picker) {
        // console.log("apply event fired, start/end dates are " + picker.startDate.format('MMMM D, YYYY') + " to " + picker.endDate.format('MMMM D, YYYY'));
        applyFunction(baseUrl, picker.startDate.format('MM/DD/YYYY'), picker.endDate.format('MM/DD/YYYY'))
    });
    $('#' + divId + '').on('cancel.daterangepicker', function (ev, picker) {
        console.log("cancel event fired");
    });
    $('#options1').click(function () {
        $('#' + divId + '').data('daterangepicker').setOptions(optionSet1, cb);
    });
    $('#options2').click(function () {
        $('#' + divId + '').data('daterangepicker').setOptions(optionSet2, cb);
    });
    $('#destroy').click(function () {
        $('#' + divId + '').data('daterangepicker').remove();
    });

}




var exportHtml = {
    exportToExcel: function (filename = '', tableHTML) {

        tableHTML = "<head><meta charset='UTF-8'></head>" + tableHTML;
        var downloadLink;
        var dataType = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';// 'application/vnd.ms-excel';

        // Specify file name
        filename = filename ? filename + '.xls' : 'excel_data.xls';

        // Create download link element
        downloadLink = document.createElement("a");
        document.body.appendChild(downloadLink);

        //if (navigator.msSaveOrOpenBlob) {
        //    var blob = new Blob(['\ufeff', tableHTML], {
        //        type: dataType
        //    });
        //    navigator.msSaveOrOpenBlob(blob, filename);
        //} else {
        //    // Create a link to the file
        //    downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

        //    // Setting the file name
        //    downloadLink.download = filename;

        //    //triggering the function
        //    downloadLink.click();
        //    $('#div_exportLoading').remove();
        //}

        var blob = new Blob(['\ufeff', tableHTML], {
            type: dataType//"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=utf-8"
        });
        saveAs(blob, filename);

        $('#div_exportLoading').remove();
    },
    exportToPdf: function (filename = '', html) {


        //$('body').append('<div id="tempDiv" style="display:none;">' + html + '</div>');
        //$(html).tableHTMLExport({ type: 'pdf', filename: filename+'.pdf' });


        // Generate the PDF.
        html2pdf().from(html).set({
            margin: 0.5,
            filename: filename + '.pdf',
            html2canvas: { scale: 1.5 },
            jsPDF: { orientation: 'landscape', unit: 'in', format: 'a3', compressPDF: true }
        }).save();
    }
}


var exportResources = {
    ExportToExcel: $shared.ExportToExcel,
    ExportToExcelAll: $shared.ExportToExcelAll,
    ExportToPdf: $shared.ExportToPdf,
    ExportToPdfAll: $shared.ExportToPdfAll,
    ExportLoadingText: $shared.WaitForPreparingData,
}
/* Export JTable */
var exportJTable = {
    th_td_stylePDF: 'border: 1px solid black;padding-bottom:10px;padding-right:10px;font-family: mainFont;max-width:150px;',
    tableWidthPDF: '100%',
    exportType: { excel: 'excel', pdf: 'pdf ' },
    generateHtmlTable: function (ar_theaders, ar_fields, records, exportType) {

        var th_td_style = '';
        var tableWidth = '';
        var replaceOld = '', replaceNew = '';

        if (exportType === exportJTable.exportType.pdf) {
            th_td_style = exportJTable.th_td_stylePDF;
            tableWidth = exportJTable.tableWidthPDF;
        } else if (exportType === exportJTable.exportType.excel) {
            // replaceOld = / /g;
            //  replaceNew = '%20';
        }

        var table = '<table width="' + tableWidth + '">';

        //Thead
        table = table + '<thead> <tr>';
        for (var i = 0; i < ar_theaders.length; i++) {
            var headerText = insertSpaceBetweenWords(ar_theaders[i]);
            table = table + '<th style="' + th_td_style + '">' + headerText + '</th>';
        }
        table = table + '</tr></thead> ';

        //tbody
        table = table + '<tbody>';
        for (var j = 0; j < records.length; j++) {
            var record = records[j];
            table = table + '<tr>';
            for (var f = 0; f < ar_fields.length; f++) {
                var field = ar_fields[f];
                var fieldData = record[field] !== null ? record[field] : '';
                fieldData = insertSpaceBetweenWords(fieldData);
                table = table + '<td style="' + th_td_style + '">' + fieldData + '</td>';
            }
            table = table + '</tr>';
        }
        table = table + '</tbody>';

        table = table + '</table>';

        return table.replace(replaceOld, replaceNew);
    },
    exportToExcel: function (filename = '') {

        var tableSelect = $('.jtable').clone();

        // th - remove attributes and header divs from jTable
        tableSelect.find('th').each(function (pos, el) {

            val = $(this).find('.jtable-column-header-text').text();

            if (val !== '' && val !== $jtableConstants.Title_Options && !$(this).hasClass($jtableConstants.ignoredFieldClass))
                $(this).html(val);
            else
                $(this).remove();
            $(this).removeAttr('width');
            $(this).removeAttr('class');
            $(this).removeAttr('style');
        });

        tableSelect.find('td').each(function (pos, el) {

            if ($(this).hasClass($jtableConstants.ignoredFieldClass))
                $(this).remove();


            $(this).removeAttr('class');
            $(this).removeAttr('style');
            $(this).removeAttr('width');
            if ($(this).find('img').length > 0)
                $(this).html("");
            if ($(this).find('button').length > 0)
                $(this).html("");
            if ($(this).find('.dropdown').length > 0)
                $(this).html("");


        });

        var tableHTML = tableSelect[0].outerHTML;//.replace(/ /g, '%20');

        exportHtml.exportToExcel(filename, tableHTML);

    },
    exportToExcelAllData: function (filename, urlListAction, httpMethod, ar_theaders, ar_fields) {
        httpMethod = httpMethod.toLowerCase();
        var exportType = exportJTable.exportType.excel;
        debugger;
        if (httpMethod === 'get') {
            ajaxRequests.doAjax_Get(urlListAction + '&jtPageSize=0',
                function (res) {

                    exportJTable.exportPatchs(filename, res.records, ar_theaders, ar_fields, exportType);

                });
        } else if (httpMethod === 'post') {
            ajaxRequests.doAjax_Post(urlListAction, { jtPageSize: 0 },
                function (res) {
                    debugger;
                    exportJTable.exportPatchs(filename, res.records, ar_theaders, ar_fields, exportType);

                });
        }


    },
    pdfTitle: function (filename = '') {
        return '<center><h1>' + filename + '</h1></center></br>' + '<div style="height:10px;"></div>';
    },
    exportToPdf: function (filename = '') {
        var tableSelect = $('.jtable').clone();
        tableSelect.attr('width', exportJTable.tableWidthPDF);
        tableSelect.removeAttr("class");

        // th - remove attributes and header divs from jTable
        tableSelect.find('th').each(function (pos, el) {
            $(this).attr('style', exportJTable.th_td_stylePDF);

            val = $(this).find('.jtable-column-header-text').text();
            if (val !== '' && val !== $jtableConstants.Title_Options && !$(this).hasClass($jtableConstants.ignoredFieldClass)) {

                val = insertSpaceBetweenWords(val);
                $(this).html(val);

            }
            else
                $(this).remove();

        });

        tableSelect.find('td').each(function (pos, el) {


            if ($(this).hasClass($jtableConstants.ignoredFieldClass))
                $(this).remove();

            $(this).removeAttr('class');
            $(this).removeAttr('style');
            $(this).removeAttr('width');

            $(this).attr('style', exportJTable.th_td_stylePDF);
            if ($(this).find('img').length > 0)
                $(this).remove();//.html("");
            else if ($(this).find('button').length > 0)
                $(this).remove();//.html("");
            else if ($(this).find('.dropdown').length > 0)
                $(this).remove();//.html("");
            else {
                val = $(this).text();
                val = insertSpaceBetweenWords(val);
                $(this).html(val);
            }

        });

        var tableHTML = tableSelect[0].outerHTML;

        exportHtml.exportToPdf(filename, exportJTable.pdfTitle(filename) + tableHTML);
    },
    exportToPdfAllData: function (filename, urlListAction, httpMethod, ar_theaders, ar_fields) {
        httpMethod = httpMethod.toLowerCase();
        var exportType = exportJTable.exportType.pdf;


        if (httpMethod === 'get') {
            ajaxRequests.doAjax_Get(urlListAction + '&jtPageSize=0',
                function (res) {

                    exportJTable.exportPatchs(filename, res.records, ar_theaders, ar_fields, exportType);

                });
        } else if (httpMethod === 'post') {
            ajaxRequests.doAjax_Post(urlListAction, { jtPageSize: 0 },
                function (res) {
                    exportJTable.exportPatchs(filename, res.records, ar_theaders, ar_fields, exportType);

                });
        }
    },
    getExportToolbar: function (table_Title, urlListAction, httpMethod,
        exportAllData_theaders, exportAllData_fields) {

        var items = [
            {
                tooltip: exportResources.ExportToExcel,
                icon: $jtableConstants.ExcelIcon,
                click: function () {
                    exportJTable.exportToExcel(table_Title);
                }
            }, {
                tooltip: exportResources.ExportToExcelAll,
                icon: $jtableConstants.ExcelAllDataIcon,
                click: function () {
                    debugger;
                    exportJTable.exportToExcelAllData(table_Title,
                        urlListAction,
                        httpMethod,
                        exportAllData_theaders,
                        exportAllData_fields);
                }
            }, {
                tooltip: exportResources.ExportToPdf,
                icon: $jtableConstants.PdfIcon,
                click: function () {
                    exportJTable.exportToPdf(table_Title);
                }
            }, {
                tooltip: exportResources.ExportToPdfAll,
                icon: $jtableConstants.PdfAllDataIcon,
                click: function () {

                    exportJTable.exportToPdfAllData(table_Title,
                        urlListAction,
                        httpMethod,
                        exportAllData_theaders,
                        exportAllData_fields);
                }
            }
        ];


        return items;
    },
    createExportToolbarDropdownList: function (divId, table_Title, urlListAction, httpMethod,
        exportAllData_theaders, exportAllData_fields) {

        var exportElements_excel = '<div class="dropdown d-inline-block mb-1">' +
            '<button class="btn btn-success dropdown-toggle" type="button" id ="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" data-display="static" >' +
            $shared.ExportToExcel +
            '</button >' +
            '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
            '<a class="dropdown-item ExportToExcel">' + exportResources.ExportToExcel + '</a>' +
            '<a class="dropdown-item ExportToExcelAll">' + exportResources.ExportToExcelAll + '</a>' +
            '</div>' +
            '</div > &nbsp;';


        var exportElements_pdf = '<div class="dropdown d-inline-block mb-1">' +
            '<button class="btn btn-secondary dropdown-toggle" type="button" id ="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" data-display="static" >' +
            $shared.ExportToPdf +
            '</button >' +
            '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
            '<a class="dropdown-item ExportToPdf">' + exportResources.ExportToPdf + '</a>' +
            '<a class="dropdown-item ExportToPdfAll">' + exportResources.ExportToPdfAll + '</a>' +
            '</div>' +
            '</div > &nbsp;';

     
        var exportElements = exportElements_excel + exportElements_pdf;
        //var exportElements = '<div class="exportContainer"> <div class="dropdown export">' +
        //    '<button class="dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="true">' +
        //    '<img src="' + $jtableConstants.ExcelIcon + '" alt="" >' +
        //    '</button>' +
        //    '<ul class="dropdown-menu">' +
        //    '<li>' +
        //    '<a  class="ExportToExcel"> ' + exportResources.ExportToExcel + '</a>' +
        //    '</li>' +
        //    '<li>' +
        //    '<a  class="ExportToExcelAll"> ' + exportResources.ExportToExcelAll + '</a>' +
        //    '</li>' +
        //    '</ul>' +
        //    '</div>' +

        //    '<div class="dropdown export">' +
        //    '<button class="dropdown-toggle" type="button" data-toggle="dropdown" aria-expanded="true">' +
        //    '<img src="' + $jtableConstants.PdfIcon + '" alt="" >' +
        //    '</button>' +
        //    '<ul class="dropdown-menu">' +
        //    '<li>' +
        //    '<a  class="ExportToPdf"> ' + exportResources.ExportToPdf + '</a>' +
        //    '</li>' +
        //    '<li>' +
        //    '<a  class="ExportToPdfAll"> ' + exportResources.ExportToPdfAll + '</a>' +
        //    '</li>' +
        //    '</ul>' +
        //    '</div> </div>';

        function registerExportClick() {
            $('.ExportToExcel').click(function () {
                exportJTable.exportToExcel(table_Title);
            });

            $('.ExportToExcelAll').click(function () {
                debugger;
                exportJTable.exportToExcelAllData(table_Title,
                    urlListAction,
                    httpMethod,
                    exportAllData_theaders,
                    exportAllData_fields);
            })

            $('.ExportToPdf').click(function () {
                exportJTable.exportToPdf(table_Title);
            });

            $('.ExportToPdfAll').click(function () {

                exportJTable.exportToPdfAllData(table_Title,
                    urlListAction,
                    httpMethod,
                    exportAllData_theaders,
                    exportAllData_fields);
            });
        }

        $('#' + divId).prepend(exportElements);
        registerExportClick();


    },
    exportPatchs: function (filename, records, ar_theaders, ar_fields, exportType) {

        showExportLoadingText();

        if (exportType == exportJTable.exportType.pdf && records.length > 1200) {

            var sampleRecords = [];
            for (var i = 0; i < records.length; i++) {

                sampleRecords.push(records[i]);

                var MaxRecordsCount = 0;
                if (exportType == exportJTable.exportType.pdf)
                    MaxRecordsCount = 1200;
                else
                    MaxRecordsCount = 5000;

                if (i % MaxRecordsCount === 0) {//export just 1200 record per file
                    exportJTable.exportPatchsPrivateHelper(sampleRecords, filename, ar_theaders, ar_fields, exportType);
                    sampleRecords = [];//Reset list
                }

            }

            if (sampleRecords.length > 0) {
                exportJTable.exportPatchsPrivateHelper(sampleRecords, filename, ar_theaders, ar_fields, exportType);
                sampleRecords = [];//Reset list
            }

            //  exportJTable.exportPatchsPrivateHelper(records, filename, ar_theaders, ar_fields, exportType);

        }
        else
            exportJTable.exportPatchsPrivateHelper(records, filename, ar_theaders, ar_fields, exportType);



    },
    exportPatchsPrivateHelper: function (sampleRecords, filename, ar_theaders, ar_fields, exportType) {

        var table = exportJTable.generateHtmlTable(ar_theaders, ar_fields, sampleRecords, exportType);

        if (exportType == exportJTable.exportType.pdf)
            exportHtml.exportToPdf(filename, exportJTable.pdfTitle(filename) + table);
        else
            exportHtml.exportToExcel(filename, table);

    }
};

function showExportLoadingText() {
    $('.exportContainer').prepend('<div id="div_exportLoading"> ' + exportResources.ExportLoadingText + ' </div>');
}

function hideExportLoadingText() {
    $('#div_exportLoading').remove();
}

/*JTable*/
var jtableHelpers = {
    icons: {
        view: '<i class="fa fa-eye" aria-hidden="true"></i>',
        details: '<i class="fa fa-eye" aria-hidden="true"></i>',
        add: '<i class="fa fa-plus-circle" aria-hidden="true"></i>',
        edit: '<i class="far fa-edit" aria-hidden="true"></i>',
        delete: '<i class="fa fa-trash" aria-hidden="true"></i>',
        password: '<i class="fas fa-user-lock" aria-hidden="true"></i>',
        download: '<i class="fa fa-download" aria-hidden="true"></i>',
        users: '<i class="fa fa-users"></i>',
        copy: '<i class="fa fa-copy"></i>',
        cancel: '<i class="fa fa-times-circle"></i>',
        exchange: '<i class="fas fa-exchange-alt"></i>',
        reNew: '<i class="fas fa-sync-alt"></i>',
        map: '<i class="fas fa-map-marker-alt"></i>',
        transfer: '<i class="fas fa-recycle"></i>',
        license: ' <i class="fas fa-id-badge"></i>',
        star: ' <i class="fas fa-star"></i>'
    },
    getActions: function (ar_actionsLinks) {

        var actions = '';
        for (var i = 0; i < ar_actionsLinks.length; i++) {
            var action = ar_actionsLinks[i];
            actions = actions + action;
        }

        return '<div class="dropdown d-inline-block mb-1">' +
            '<button class="btn btn-info dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" data-display="static" >' +
            $shared.Options +
            '</button > ' +
            '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
            actions +

            '</div>' +
            '</div >';
        //return '<div class="btn-group">' +
        //    '<button type="button" class="btn btn-danger dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
        //    '<button type = "button" class="btn btn-danger" > ' + $shared.Options + '</button >' +
        //    '<span class="sr-only">Toggle Dropdown</span>' +
        //    '</button>' +
        //    '<div class="dropdown-menu">' +
        //    actions +
        //    '</div>' +
        //    '</div >';

        //return '<div class="dropdown">' +
        //    '<i class="option" aria-hidden="true" data-toggle="dropdown" aria-haspopup="true" id="dLabel"></i>' +
        //    '<ul class="dropdown-menu" aria-labelledby="dLabel"> ' +
        //    actions +
        //    '</ul>' +
        //    '</div>';
    }
};

function insertSpaceBetweenWords(text) {
    try {
        return text.split(' ').join('&nbsp;');
    }
    catch (ex) {
        return text;
    }
}

jQuery.fn.serializeJSON = function () {
    var json = {};
    jQuery.map(jQuery(this).serializeArray(), function (n, i) {
        var _ = n.name.indexOf('[');
        if (_ > -1) {
            var o = json;
            _name = n.name.replace(/\]/gi, '').split('[');
            for (var j = 0, len = _name.length; j < len; i++) {
                if (j === len - 1) {
                    if (o[_name[i]]) {
                        if (typeof o[_name[i]] === 'string') {
                            o[_name[j]] = [o[_name[j]]];
                        }
                        o[_name[j]].push(n.value);
                    }
                    else o[_name[j]] = n.value || '';
                }
                else o = o[_name[j]] = o[_name[j]] || {};
            }
        }
        else {
            if (json[n.name] !== undefined) {
                if (!json[n.name].push) {
                    json[n.name] = [json[n.name]];
                }
                json[n.name].push(n.value || '');
            }
            else json[n.name] = n.value || '';
        }
    });
    return json;
};

/***************** Validation summary **********************/
formValidation = {
    valid: function (formId) {

        if ($('#' + formId).valid()) {
            $('.validation-summary-valid ul li').remove();
            $('.validation-summary-errors ul li').remove();
            $('.validation-summary-errors').addClass('validation-summary-valid').removeClass('validation-summary-errors');
            return true;
        } else
            return false;
    },
    disableValidation: function () {
        //Set the attribute to false
        $("[data-val='true']").attr('data-val', 'false');

        //Reset validation message
        $('.field-validation-error')
            .removeClass('field-validation-error')
            .addClass('field-validation-valid');

        //Reset input, select and textarea style
        $('.input-validation-error')
            .removeClass('input-validation-error')
            .addClass('valid');

        //Reset validation summary
        $(".validation-summary-errors")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid");

        //To reenable lazy validation (no validation until you submit the form)
        $('form').removeData('unobtrusiveValidation');
        $('form').removeData('validator');
        $.validator.unobtrusive.parse($('form'));
    }
};


var messages = {
    showSuccessMessage: function (message) {
        // var toaster = $('#toaster');
        toastr.options = {
            closeButton: true,
            debug: false,
            newestOnTop: false,
            progressBar: true,
            positionClass: (document.dir != "rtl") ? "toast-top-right" : "toast-top-left",
            preventDuplicates: false,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            timeOut: "5000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut"
        };
        toastr.success(message, "success!");
        //var elements = messageBasics.generateMessageElements(message, "alert alert-success alert-dismissible");
        //messageBasics.appendMessagEelements(elements);
        //messageBasics.removeAfterSecondes();
    },
    showInfoMessage: function (message) {
        toastr.options = {
            closeButton: true,
            debug: false,
            newestOnTop: false,
            progressBar: true,
            positionClass: (document.dir != "rtl") ? "toast-top-right" : "toast-top-left",
            preventDuplicates: false,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            timeOut: "5000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut"
        };
        toastr.success(message, "Info!");

        //var elements = messageBasics.generateMessageElements(message, "alert alert-info alert-dismissible");
        //messageBasics.appendMessagEelements(elements);
        //messageBasics.removeAfterSecondes();
    },
    showWarningMessage: function (message) {

        toastr.options = {
            closeButton: true,
            debug: false,
            newestOnTop: false,
            progressBar: true,
            positionClass: (document.dir != "rtl") ? "toast-top-right" : "toast-top-left",
            preventDuplicates: false,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            timeOut: "5000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut"
        };
        toastr.warning(message, "warning!");

        //var elements = messageBasics.generateMessageElements(message, "alert alert-warning alert-dismissible");
        //messageBasics.appendMessagEelements(elements);
        //messageBasics.removeAfterSecondes();
    },
    showErrorMessage: function (message) {
        toastr.options = {
            closeButton: true,
            debug: false,
            newestOnTop: false,
            progressBar: true,
            positionClass: (document.dir != "rtl") ? "toast-top-right" : "toast-top-left",
            preventDuplicates: false,
            onclick: null,
            showDuration: "300",
            hideDuration: "1000",
            timeOut: "5000",
            extendedTimeOut: "1000",
            showEasing: "swing",
            hideEasing: "linear",
            showMethod: "fadeIn",
            hideMethod: "fadeOut"
        };
        toastr.error(message, "error!");

        //var elements = messageBasics.generateMessageElements(message, "alert alert-danger alert-dismissible");
        //messageBasics.appendMessagEelements(elements);
        //messageBasics.removeAfterSecondes();
    },

    //showMessage: function (message, divName, onSuccess, onError) {
    //    if (message !== null) {
    //        switch (message.result) {
    //            case "error":
    //                $("#divMessageinfo").addClass("alert-danger");
    //                if (isFunction(onError)) {
    //                    onError();
    //                }
    //                break;
    //            case "success":
    //                $("#divMessageinfo").addClass("alert-success");
    //                if (isFunction(onSuccess)) {
    //                    onSuccess();
    //                }
    //                break;
    //            case "Info":
    //                $("#divMessageinfo").addClass("alert-info");
    //                break;
    //            case "Warning":
    //                $("#divMessageinfo").addClass("alert-warning");
    //                break;
    //        }
    //        $("#lblMessageInfo").html(message.message.replace("\n", "<br/>"));
    //        $("#divshowMessage").addClass("show");
    //        var element = $("#divshowMessage");
    //        var divToAppend = ".leftBlock";

    //        if (divName !== null) {
    //            divToAppend = "#" + divName;
    //            $('html, body').animate({ scrollTop: $(divToAppend).offset().top }, 1000);
    //            $(divToAppend).append(element);
    //        }
    //        else {
    //            $('html, body').animate({ scrollTop: $(".btns").offset().top }, 1000);
    //            $(divToAppend).append(element);
    //        }
    //        messageBasics.removeAfterSecondes();
    //    }
    //    else {
    //        $("#divshowMessage").addClass("hide");

    //    }
    //}
};

var messageBasics = {
    generateMessageElements: function (message, divClasses) {
        return '<div id="div_Message" class="' + divClasses + '" role="alert">' +
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close" >' +
            '<span aria-hidden="true">×</span>' +
            '</button >' + message +
            '</div >';
    },
    appendMessagEelements: function (elements) {
        $('#div_Message').remove();
        $('#div_MessageParent').append(elements);
        $('html, body').animate({ scrollTop: $("#div_Message").offset().top }, 1000);
    },
    removeAfterSecondes: function () {
        setTimeout(function () { $('#div_Message').fadeOut(); }, 90000);

    }
};


function removeUnAuthorizedLinksMenu() {
    var child_menus = $('#sidebar-menu').find('li.has-sub');
    for (var i = 0; i < child_menus.length; i++) {
        var child_menu = child_menus[i];
        var child_menu_LIs = $(child_menu).find('.actionLink');
        if (child_menu_LIs.length == 0) {
            $(child_menu).remove();
        }
    }
}

/********dropdownlist functions******* */
var dropdownlist = {
    fillDropDownListWithOptions: function ($select, options, value, label) {
        $select.empty();

        if (label != '' && label != undefined)
            $('<option >' + label + '</option>').val('').appendTo($select);

        for (var i = 0; i < options.length; i++) {
            $('<option' +
                (options[i].value === value ? ' selected="selected"' : '') +
                '>' +
                options[i].text +
                '</option>')
                .val(options[i].value)
                .appendTo($select);
        }
    }
};

/*************************Attachment file validation**********************/
$('.upload_video').change(function (e) {
    debugger;

    var fileUpload = e.target;

    var lblError = $(fileUpload).parent().find('.spn_uploadError');
    var allowedExtensions = $(fileUpload).parent().find('.hdnf_allowedExtensions').val();
    var allowedFiles = allowedExtensions.split(',');

    var fileExtension = getFileExtension(fileUpload.value.toLowerCase());
    if (allowedFiles.indexOf(fileExtension) > -1) {
        $(lblError).text("");
    }
    else {
        e.target.value = '';
        $(lblError).text($shared.InvalidFileExtension);

    }

});

function getFileExtension(filename) {
    return (/[.]/.exec(filename)) ? (/[^.]+$/.exec(filename)[0]) : undefined;
}
