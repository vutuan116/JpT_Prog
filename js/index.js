var levelJson = [];
var tuVungJson = [];
var kanjiJson = [];

$(document).ready(function () {
    tuVung.forEach(item => {
        tuVungJson.push(JSON.parse(item));
    });

    kanji.forEach(item => {
        kanjiJson.push(JSON.parse(item));
    });

    setting();
    viewListLevel();
    viewListLesson($("#level_select").val());
});

function viewListLevel() {
    levelJson = [];
    tuVungJson.forEach(x => {
        if (!levelJson.includes(x.Level)) {
            levelJson.push(x.Level);
        }
    });
    $("#level_select").empty();
    levelJson.forEach(x => {
        $("#level_select").append(
            `<option value=${x}>${x}</option>`
        );
    })
}

function viewListLesson(level) {
    let indexWb = 0;
    $("#wordbook_lesson_div").empty();
    $("#kanji_lesson_div").empty();
    tuVungJson.forEach(x => {
        indexWb++;
        if (level == x.Level) {
            $("#wordbook_lesson_div").append(
                '<div>'
                + '<input class="cursor_pointer" type="checkbox" value="' + x.Lesson + '" id="wb_lesson_' + indexWb + '">'
                + '<label class="cursor_pointer" for="wb_lesson_' + indexWb + '">&nbsp;' + x.Lesson + '</label>'
                + '</div>');
        }
    });

    // indexWb = 0;
    kanjiJson.forEach(x => {
        indexWb++;
        if (level == x.Level) {
            $("#kanji_lesson_div").append(
                '<div>'
                + '<input class="cursor_pointer" type="checkbox" value="' + x.Lesson + '" id="wb_lesson_' + indexWb + '">'
                + '<label class="cursor_pointer" for="wb_lesson_' + indexWb + '">&nbsp;' + x.Lesson + '</label>'
                + '</div>');
        }
    });

}

function start() {
    // let listLesson = $("input[type=checkbox]:checked");
    // let level = $("#level_select").val();
    // let listWordbook = [];
    // if (!listLesson || listLesson.length == 0) {
    //     alert("Hãy chọn ít nhất 1 bài học");
    //     return;
    // }
    // listLesson.each(x => {
    //     lesson = listLesson[x];
    //     tuVungJson.filter(y => y.Level == level && y.Lesson == lesson.value).forEach(z => {
    //         listWordbook = listWordbook.concat(z.Data);
    //     });
    // });

    // listWordbook = derangeArray(listWordbook);

    $("#menu_div").hide();
    $("#test_div").show();
}

function derangeArray(listItem) {
    let result = [];
    console.log(listItem);

    while (listItem.length > 0) {
        let index = getRandomInt(0, listItem.length - 1);
        result.push(listItem[index]);
        listItem.splice(index, 1);
    }

    console.log(result);
    return result;
}





// help function 
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function setting() {
}