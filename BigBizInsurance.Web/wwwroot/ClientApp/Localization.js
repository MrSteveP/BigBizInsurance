

var localResources = {
    ar: {
        CharactersRemaining: "أحرف متبقية",
        DeleteConfirm: "هل أنت متأكد انك تريد الحذف؟",
        ErrorOccured: "لقد حدث خطأ",
        UnExpectedError: "لقد حدث خطأ غير متوقع برجاء المحاولة مرة أخرى",
        ChangeLangDescription: "حول إلى اللغة العربية",
        Arabic: "عربي",
        Yes: 'نعم',
        No: 'لا',
        English: "إنجليزي",
        LoadingText: "جارى تحميل البيانات الخاصة بك.",
        Ok: "موافق",
        Cancel: "إلغاء",
        Close: "إغلاق",
        InvalidHours: "قيمة غير صالحة للساعات: ",
        InvalidMinutes: "قيمة غير صالحة للدقائق: ",
        InvalidTime: "تنسيق الوقت غير صالح: ",
        AM: "ص",
        PM: "م",
        Notification: "تنبيه"
    },
    en: {
        CharactersRemaining: "characters remaining",
        DeleteConfirm: "Are you sure you want to delete?",
        ErrorOccured: "Error Occured",
        UnExpectedError: "Unexpected Error Occured. Please try again later.",
        ChangeLangDescription: "Switch to English language",
        Arabic: "Arabic",
        English: "English",
        LoadingText: "Loading ...",
        Yes: 'Yes',
        No: 'No',
        Ok: "Ok",
        Cancel: "Cancel",
        Close: "Close",
        InvalidHours: "Invalid value for hours: ",
        InvalidMinutes: "Invalid value for minutes: ",
        InvalidTime: "Invalid time format: ",
        AM: "AM",
        PM: "PM",
        Notification: "Notification"
    }
};


function initLangSwitcher(elementId) {

    elementId = elementId || "lang-switcher";
    var langSwitcherAnchor = $("#" + elementId);
    var isIcon = false;

    if (!langSwitcherAnchor.length) {
        elementId = "lang-switcher-icon";
        langSwitcherAnchor = $("#" + elementId);
        isIcon = langSwitcherAnchor.length !== 0;
    }

    langSwitcherAnchor.attr("title",
        isArabic()
            ? localResources.en.ChangeLangDescription
            : localResources.ar.ChangeLangDescription);

    if (!isIcon) {
        langSwitcherAnchor.find('#spn_lang').text(isArabic()
            ? localResources.en.English
            : localResources.ar.Arabic);
    }

    langSwitcherAnchor.click(function (e) {
        if (isArabic()) {
            setCookie(cultureCookieName, "c=en-GB|uic=en-GB");
        } else {
            setCookie(cultureCookieName, "c=ar-SA|uic=ar-SA");
        }

        window.location = window.location.href;
        e.preventDefault();
    });
};

function setCookie(name, value, days) {
    var expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/; secure;";
};

function readCookie(name) {
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEq) === 0) return c.substring(nameEq.length, c.length);
    }
    return null;
};
// ------------------------------- Localization and Culture Related Code -----------------------------

function isArabic() {
    return getCurrentLang() === 'ar';
}

function isRtl() {
    return isArabic();
}

var cultureCookieName = '.AspNetCore.Culture';

function getCurrentLang() {
    var lang = 'ar';
    var cultureFromCookie = decodeURIComponent(readCookie(cultureCookieName));
    var langFromHtmlTag = $('html').attr('lang');

    if (cultureFromCookie !== undefined && cultureFromCookie !== "" && cultureFromCookie !== null) {// && cultureFromCookie != null because of error in pubish event button close
        lang = (cultureFromCookie === 'ar-SA' || cultureFromCookie === 'ar' || cultureFromCookie.indexOf('ar-SA') > -1) ? 'ar' : 'en';
    } else if (langFromHtmlTag !== undefined && langFromHtmlTag !== "") {
        lang = langFromHtmlTag;
    }

    return lang;
};

function getLocalString(key) {
    return localResources[getCurrentLang()][key];
};

function getTimeSuffix(time) {

    if (isArabic())
        return time.replace(/am/g, " ص ").replace(/pm/g, " م ");
    else
        return time;
};