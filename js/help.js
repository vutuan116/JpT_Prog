var menuSetting = {};
var lessonHistory = [];
var wordHardHistory = [];
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function setting() {
    menuSetting = JSON.parse(localStorage.getItem("menuSetting") ?? "{}");
    lessonHistory = JSON.parse(localStorage.getItem("lessonHistory") ?? "[]");
    wordHardHistory = JSON.parse(localStorage.getItem("wordHardHistory") ?? "[]");

    if (menuSetting.Level) {
        $("#level_select").val(menuSetting.Level);
    }
    if (menuSetting.Type) {
        $("#type_select").val(menuSetting.Type);
    }
    if (menuSetting.WordType) {
        $("#wordtype_select").val(menuSetting.WordType);
    }
    if (menuSetting.WordSelect) {
        $("#word_select").val(menuSetting.WordSelect);
    } else {
        $("#word_select").val("wordbook");
    }
    if ($("#word_select").val() == "wordbook") {
        $("#wordbook_lesson_div").removeClass("hide");
        $("#kanji_lesson_div").addClass("hide");
    } else {
        $("#wordbook_lesson_div").addClass("hide");
        $("#kanji_lesson_div").removeClass("hide");
    }
}

function saveSetting() {
    menuSetting.Level = $("#level_select").val();
    menuSetting.Type = $("#type_select").val();
    menuSetting.WordType = $("#wordtype_select").val();
    menuSetting.WordSelect = $("#word_select").val();

    localStorage.setItem("menuSetting", JSON.stringify(menuSetting));
}

function saveLessonHistory() {
    let listLessonSelected = $("input[type=checkbox]:checked");
    listLessonSelected.each(x => {
        let ls = lessonHistory.find(lsItem => lsItem.Name == listLessonSelected[x].value);
        if (ls) {
            ls.Time = new Date().yyyyMMdd();
        } else {
            lessonHistory.push({ "Name": listLessonSelected[x].value, "Time": new Date().yyyyMMdd() })
        }
    });

    localStorage.setItem("lessonHistory", JSON.stringify(lessonHistory));
}

function saveWordHard() {
    let listWb = $(".btn_wordhard.on");
    listWordbook.forEach(x => {
        let index = wordHardHistory.indexOf(x.Id.toString());
        if (index >= 0) {
            wordHardHistory.splice(index, 1);
        }
    });
    listWb.each(x => {
        wordHardHistory.push(listWb[x].getAttribute("value"));
    });
    localStorage.setItem("wordHardHistory", JSON.stringify(wordHardHistory));
}

function derangeArray(listItem) {
    let result = [];
    while (listItem.length > 0) {
        let index = getRandomInt(0, listItem.length - 1);
        result.push(listItem[index]);
        listItem.splice(index, 1);
    }
    return result;
}

function goHome() {
    $(".div_main").addClass("hide");
    $(".menu").removeClass("hide");
}

function resetHideStatus() {
    $(".th_col_index").addClass("hide");
    $(".th_col_repeat").addClass("hide");
    $(".th_col_hard").addClass("hide");
    $(".th_col_hard").addClass("hide");
}

Date.prototype.hhmmss = function () {
    var hh = this.getHours();
    var mm = this.getMinutes();
    var ss = this.getSeconds();

    return [hh < 10 ? '0' + hh : hh, mm < 10 ? '0' + mm : mm, ss < 10 ? '0' + ss : ss].join(':');
};

Date.prototype.yyyyMMdd = function () {
    var yy = this.getFullYear();
    var MM = this.getMonth() + 1;
    var dd = this.getDate();

    return [yy < 10 ? '0' + yy : yy, MM < 10 ? '0' + MM : MM, dd < 10 ? '0' + dd : dd].join('-');
};
