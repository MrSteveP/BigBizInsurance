$app.auditIndex= (function () {
    function Init(initialData) {

        $(document).ready(function () {

            registerJTable(initialData);
           // reloadJTable();
            registerSearch();
            registerReset();
            $('#btn_Search').click();

        });
       
    }

    function registerJTable(initialData) {
        var dataForExportAllData = getDataForExportAllData();

        var toolbarExportItems = exportJTable.createExportToolbarDropdownList('TableContainer',
            $auditResources.Index_ListTitle,
            initialData.urlListAction,
            'post',
            dataForExportAllData.ar_theaders,
            dataForExportAllData.ar_fields);


        $('#TableContainer').jtable({
            title: $auditResources.Index_ListTitle,
            paging: true,
            sorting: false,
            defaultSorting: 'name desc',
            pageSize: 10,
            actions: {
                listAction: initialData.urlListAction,
            },
            toolbar: {
                items: toolbarExportItems
            },
            fields: {
                id: {
                    key: true,
                    list: false
                },
                userName: {
                    width: "20%",
                    title: $auditResources.Index_List_UserName
                },
                dateString: {
                    width: "20%",
                    title: $auditResources.Index_List_Date
                },
                entityName: {
                    width: "20%",
                    title: $auditResources.Index_List_EntityName
                },
               audit_ActionCode: {
                    width: "20%",
                   title: $auditResources.Index_List_ActionCode
                },
                //actionData: {
                //    width: "20%",
                //    title: $auditResources.Index_List_ActionData
                //},
                actions: {
                    width: '6%',
                    title: $shared.Options,
                    listClass: 'ignore',
                    sorting: false,
                    display: function (data) {
                        var links = [];
                        links.push('<a class="dropdown-item" href="' + initialData.detailsBaseUrl + '/' + data.record.id + '">' + $shared.Display +'</a>');

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

            $('#txt_name').val('');
            $('#txt_location').val('');
            $('#ddl_gradeId').val('');

            $('#btn_Search').click();
        });
    }

    function getDataForExportAllData() {
        var ar_theaders = [
            $auditResources.Index_List_UserName,
            $auditResources.Index_List_Date,
            $auditResources.Index_List_EntityName,
            $auditResources.Index_List_ActionCode,
            $auditResources.Index_List_ActionData
        ];

        var ar_fields = [
            'userName',
            'dateString',
            'entityName',
            'audit_ActionCode',
            'actionData'
        ];

        return { ar_theaders, ar_fields };
    }

    return { Init };
}());




