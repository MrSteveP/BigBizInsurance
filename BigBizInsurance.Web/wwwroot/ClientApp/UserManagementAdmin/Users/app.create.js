$app.create = (function () {
    function Init() {
        $(document).ready(function () {
            $('.dropify').dropify();

            $('#txt_birthDate').daterangepicker({
                singleDatePicker: true,
                singleClasses: "picker_4",
                locale: {
                    format: 'MM/DD/YYYY'
                }
            }, function (start, end, label) {
                console.log(start.toISOString(), end.toISOString(), label);
            });

        });
    }

  

    return { Init };
}());




