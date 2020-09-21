$app.userIndex = (function () {
    function Init(initialData) {

        $(document).ready(function () {
            registerJTable(initialData);
            reloadJTable();
            registerSearch();
            registerReset();
            $('#btn_Search').click();

        });

    }

    function registerJTable(initialData) {
        var dataForExportAllData = getDataForExportAllData();

        var toolbarExportItems = exportJTable.createExportToolbarDropdownList('TableContainer',
            $usersManagement.Index_ListTitle,
            initialData.urlListAction,
            'post',
            dataForExportAllData.ar_theaders,
            dataForExportAllData.ar_fields);


        $('#TableContainer').jtable({
            title: $usersManagement.Index_ListTitle,
            paging: true,
            sorting: false,
            defaultSorting: 'name desc',
            pageSize: 10,
            actions: {
                listAction: initialData.urlListAction,
                deleteAction: initialData.deleteBaseUrl

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

                    title: $usersManagement.Index_List_Name,
                    display: function (data) {
                        return data.record.firstName + ' ' + data.record.middleName + ' ' + data.record.lastName;
                    }
                },
                userName: {
                    width: "20%",
                    title: $usersManagement.Index_List_UserName
                },
                email: {
                    width: "20%",
                    title: $usersManagement.Index_List_Email
                },
                phoneNumber: {
                    width: "20%",
                    title: $usersManagement.Index_List_Phone
                },
                active: {
                    width: "20%",
                    title: $usersManagement.Index_List_Status,
                    display: function (data) {

                        if (data.record.active == true)
                            return '<center><span class="btn btn-success"> ' + $shared.Active+' </span></center>';
                        else
                            return '<center><span class="btn btn-danger"> ' + $shared.Inactive +'</span></center>';
                    }
                },
                actions: {
                    width: '6%',
                    title: $shared.Options,
                    listClass: 'ignore',
                    sorting: false,
                    display: function (data) {
                        var links = [];
                        links.push('<a class="dropdown-item" href="' + initialData.detailsBaseUrl + '/' + data.record.id + '">' + $shared.Display + '</a>');
                        links.push('<a class="dropdown-item" href="' + initialData.editBaseUrl + '/' + data.record.id + '">' + $shared.Edit + '</a>');
                        links.push('<a class="dropdown-item" href="' + initialData.changePasswordBaseUrl + '/' + data.record.id + '">' + $usersResources.ChangePassword + '</a>');

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

            $('#txt_Name').val('');
            $('#txt_UserName').val('');
            $('#txt_Email').val('');
            $('#txt_Phone').val('');

            $('#btn_Search').click();
        });
    }

    function getDataForExportAllData() {
        var ar_theaders = [
            $usersManagement.Index_List_Name,
            $usersManagement.Index_List_UserName,
            $usersManagement.Index_List_Email,
            $usersManagement.Index_List_Phone,
        ];

        var ar_fields = [
            'name',
            'userName',
            'email',
            'phoneNumber'
        ];

        return { ar_theaders, ar_fields };
    }


    return { Init, reloadJTable };
}());




