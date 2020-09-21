$app.sampleIndex = (function () {
    function Init(initialData) {

        $(document).ready(function () {

            registerJTable(initialData);
            //reloadJTable();
            registerSearch();
            registerReset();
            $('#btn_Search').click();

        });

    }

    function registerJTable(initialData) {
        var dataForExportAllData = getDataForExportAllData();

        var toolbarExportItems = exportJTable.createExportToolbarDropdownList('TableContainer',
            $sampleResources.Index_ListTitle,
            initialData.urlListAction,
            'post',
            dataForExportAllData.ar_theaders,
            dataForExportAllData.ar_fields);


        $('#TableContainer').jtable({
            title: $sampleResources.Index_ListTitle,
            paging: true,
            sorting: false,
            defaultSorting: 'name desc',
            pageSize: 10,
            actions: {
                listAction: initialData.urlListAction,
                deleteAction:initialData.deleteBaseUrl
            },
            toolbar: {
                items: toolbarExportItems
            },
            fields: {
                id: {
                    key: true,
                    list: false
                },
                name: {
                    width: "20%",
                    title: $sampleResources.Index_List_Name
                },
              
                actions: {
                    width: '6%',
                    title: $shared.Options,
                    listClass: 'ignore',
                    sorting: false,
                    display: function (data) {
                        var links = [];
                        links.push('<a class="dropdown-item" href="' + initialData.detailsBaseUrl + '/' + data.record.id + '">' + $shared.Display + '</a>');
                        links.push('<a class="dropdown-item" href="' + initialData.editBaseUrl + '/' + data.record.id + '?' + initialData.editQueryString+'">' + $shared.Edit + '</a>');

                        return jtableHelpers.getActions(links);

                    }
                }
            }
        });
    }



    function reloadJTable() {
        $('#TableContainer').jtable('load', $('#frm_Search').serializeJSON());
    }

    function registerSearch() {
        $('#btn_Search').click(function (e) {
            e.preventDefault();
            if (formValidation.valid('frm_Search')) {
                reloadJTable();
            }
        });
    }
    function registerReset() {
        $('#btn_Reset').click(function (e) {
            document.getElementById('frm_Search').reset();

            $('#txt_keywork').val('');

            $('#btn_Search').click();
        });
    }

    function getDataForExportAllData() {
        var ar_theaders = [
            $sampleResources.Index_List_Name
        ];

        var ar_fields = [
            'name'
        ];

        return { ar_theaders, ar_fields };
    }

    return { Init };
}());




