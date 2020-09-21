$app.lookups = (function () {

    /***********************Load class rooms BEGIN******************************/
    function loadClassRoomsByGradeId(ddl_grades, ddl_classRooms,
        currentClassRoomId, classRoomsUrl, ddl_classRoomsLabel ) {

        var initialData = { ddl_grades, ddl_classRooms, currentClassRoomId, classRoomsUrl, ddl_classRoomsLabel };
       
        bindClassRooms(initialData);

        $(ddl_grades).change(function () {
            bindClassRooms(initialData);
        });
    }

    function bindClassRooms(initialData) {
        var gradeId = $(ddl_grades).val();
        if (gradeId == '') {

            dropdownlist.fillDropDownListWithOptions($(ddl_classRooms),
                [],
                initialData.currentClassRoomId, initialData.ddl_classRoomsLabel);
        }
        else {

            ajaxRequests.doAjax_Get(initialData.classRoomsUrl + '/' + gradeId, function (data) {

                dropdownlist.fillDropDownListWithOptions($(ddl_classRooms),
                    data.options,
                    initialData.currentClassRoomId, initialData.ddl_classRoomsLabel);

            }, function () { });
        }
    }
/***********************Load class rooms END******************************/



    return { loadClassRoomsByGradeId };
}());




