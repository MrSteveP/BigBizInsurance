$app.policyIndex = (function () {
    function Init() {
        $(document).ready(function () {
            var TRs = $('#tbl_pagesTableBody').find('tr');

            for (var i = 0; i < TRs.length; i++) {
                var tr = TRs[i];

                var select = $(tr).find('select');
                var hdnf_policy = $(tr).find('.PolicyName');
                var policyName = $(hdnf_policy).val();

                if (policyName != '')
                    $(select).val(policyName);
            }
        });
    }


    return { Init };
}());




