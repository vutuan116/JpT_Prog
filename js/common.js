var menuSetting={};
var lessonHistory= [];
var wordHardHistory= [];
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function setting() {
    menuSetting = JSON.parse(localStorage.getItem("menuSetting")??"{}");
    lessonHistory = JSON.parse(localStorage.getItem("lessonHistory")??"[]");
    wordHardHistory = JSON.parse(localStorage.getItem("wordHardHistory")??"[]");

    if (menuSetting.Level){
        $("#level_select").val(menuSetting.Level);
    }
    if (menuSetting.Type){
        $("#type_select").val(menuSetting.Type);
    }
    if (menuSetting.WordType){
        $("#wordtype_select").val(menuSetting.WordType);
    }
}

function saveSetting(){
    menuSetting.Level = $("#level_select").val();
    menuSetting.Type = $("#type_select").val();
    menuSetting.WordType = $("#wordtype_select").val();

    localStorage.setItem("menuSetting", JSON.stringify(menuSetting));
}

function saveLessonHistory(){
    let listLessonSelected = $("input[type=checkbox]:checked");
    listLessonSelected.each(x => {
        let ls = lessonHistory.find(lsItem=> lsItem.Name == listLessonSelected[x]);
        if (ls){
            ls.Time = new Date().yyyyMMdd();
        }else{
            lessonHistory.push({"Name":listLessonSelected[x].value, "Time":new Date().yyyyMMdd()})
        }
    });

    localStorage.setItem("lessonHistory", JSON.stringify(lessonHistory));
}

function saveWordHard(){

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

function goTop() {
    $("html").scrollTop(0);
}

function goHome() {
    $(".div_main").addClass("hide");
    $(".menu").removeClass("hide");
}

Date.prototype.hhmmss = function () {
    var hh = this.getHours();
    var mm = this.getMinutes();
    var ss = this.getSeconds();

    return [hh < 10 ? '0' + hh : hh, mm < 10 ? '0' + mm : mm, ss < 10 ? '0' + ss : ss].join(':');
};

Date.prototype.yyyyMMdd = function () {
    var hh = this.getFullYear();
    var MM = this.getMonth();
    var ss = this.getDay();

    return [hh < 10 ? '0' + hh : hh, MM < 10 ? '0' + MM : MM, ss < 10 ? '0' + ss : ss].join('-');
};