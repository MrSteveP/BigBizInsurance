$app.authentication = (function () {
    function Init() {
        $(document).ready(function () {
            OnThrottleCheckChaned();

            $('#txt_AllowThrottleAuthentication').click(function () {
                OnThrottleCheckChaned()
            });
        });
    }

    function OnThrottleCheckChaned() {
        if ($('#txt_AllowThrottleAuthentication').is(":checked")) {

            $('#txt_LockoutTime').prop("disabled", false);
            $('#txt_MaximumNumberOfAttempts').prop("disabled", false);

        }
        else {
            $('#txt_LockoutTime').prop("disabled", true);
            $('#txt_MaximumNumberOfAttempts').prop("disabled", true);
        }
    }

    return { Init };
}());




