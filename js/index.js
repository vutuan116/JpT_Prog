var levelJson = [];
var tuVungJson = [];
var kanjiJson = [];
var listWordbook = [];

$(document).ready(function () {
    tuVung.forEach(item => {
        tuVungJson.push(item);
    });

    kanji.forEach(item => {
        kanjiJson.push(item);
    });

    setting();
    viewListLevel();
    viewListLesson($("#level_select").val());

    goHome();
    // $('#wb_lesson_12').attr('checked', 'checked');
    // start();
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
                `<div>
                    <input class="cursor_pointer" type="checkbox" value="${x.Lesson}" id="wb_lesson_${indexWb}">
                    <label class="cursor_pointer" for="wb_lesson_${indexWb}">&nbsp;${x.Lesson}</label>
                </div>`);
        }
    });

    kanjiJson.forEach(x => {
        indexWb++;
        if (level == x.Level) {
            $("#kanji_lesson_div").append(
                `<div>
                    <input class="cursor_pointer" type="checkbox" value="${x.Lesson}" id="wb_lesson_${indexWb}">
                    <label class="cursor_pointer" for="wb_lesson_${indexWb}">&nbsp;${x.Lesson}</label>
                </div>`);
        }
    });

}

function start() {
    let listLesson = $("input[type=checkbox]:checked");
    let level = $("#level_select").val();
    goTop();
    if (!listLesson || listLesson.length == 0) {
        alert("Hãy chọn ít nhất 1 bài học");
        return;
    }
    var i = 0;
    listLesson.each(x => {
        lesson = listLesson[x];
        tuVungJson.filter(y => y.Level == level && y.Lesson == lesson.value).forEach(z => {
            listWordbook = listWordbook.concat(z.Data);
            listWordbook[listWordbook.length - 1].Id = i;
            i++;
        });
    });

    listWordbook = derangeArray(listWordbook);
    viewListWordbook();

    $(".div_main").addClass("hide");
    $(".test_wb").removeClass("hide");
}

function viewListWordbook() {
    var html = "";
    var id = 0;
    switch ($('#type_select').find(":selected").val()) {
        case "test":
            listWordbook.forEach(x => {
                var isShowHira = getRandomInt(0, 100) % 2 == 0;
                if (isShowHira) {
                    html = html + hideMean(x, id);
                } else {
                    html = html + hideWord(x, id);
                }
                id++;
            });
            break;
        case "hideWord":
            listWordbook.forEach(x => {
                html = html + hideWord(x, id);
                id++;
            });
            break;
        case "hideMean":
            listWordbook.forEach(x => {
                html = html + hideMean(x, id);
                id++;
            });
            break;
        default:
            listWordbook.forEach(x => {
                html = html + viewWord(x);
            });
            $(".wb_btn.checkWb").addClass("hide");
            $(".wb_btn.againCheckWb").removeClass("hide");
    }

    $("#tbl_body_list_wordbook").html(html);
}

// help function 
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function setting() {
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

function checkWordbook() {
    $(".wb.hide").removeClass("hide");
    $(".wb_btn.checkWb").addClass("hide");
    $(".wb_btn.againCheckWb").removeClass("hide");
    goTop();
}

function againTestWb() {
    var listWbChecked = $(".word_repeat:checked");

    if (!listWbChecked || listWbChecked.length == 0) {
        goHome();
    }

    var listWbRepeat = [];
    listWbChecked.each(x => {
        let id = listWbChecked[x].id.replace("word_repeat_", "");
        listWbRepeat.push(listWordbook[Number(id)]);
    });

    listWordbook = listWbRepeat;
    viewListWordbook();

    $(".wb_btn.checkWb").removeClass("hide");
    $(".wb_btn.againCheckWb").addClass("hide");
    goTop();
}

function goTop() {
    $("html").scrollTop(0);
}

function goHome() {
    $(".div_main").addClass("hide");
    $(".menu").removeClass("hide");
}

function hideWord(word, id) {
    return "" +
        `<tr">
            <td>
                <ruby class="wb hide">${word.Hira}
                    <rt>${word.Kanji}</rt>
                </ruby>
                <input class="w-100">
            </td>
            <td>
                <span>${word.Mean}</span>
            </td>
            <td class="td_btn_repeat wb hide">
                <input class="cursor_pointer word_repeat" type="checkbox" id="word_repeat_${id}">
                <label class="cursor_pointer" for="word_repeat_${id}"><i class="fa-solid fa-repeat"></i></label>
            </td>
        </tr>`;
}

function hideMean(word, id) {
    return "" +
        `<tr>
            <td>
                <ruby>${word.Hira}
                    <rt>${word.Kanji}</rt>
                </ruby>
            </td>
            <td>
                <input class="w-100">
                <span class="wb hide">${word.Mean}</span>
            </td>
            <td class="td_btn_repeat wb hide">
                <input class="cursor_pointer word_repeat" type="checkbox" id="word_repeat_${id}">
                <label class="cursor_pointer" for="word_repeat_${id}"><i class="fa-solid fa-repeat"></i></label>
            </td>
        </tr>`;
}

function viewWord(word) {
    return "" +
        `<tr">
            <td>
                <ruby class="wb">${word.Hira}
                    <rt>${word.Kanji}</rt>
                </ruby>
            </td>
            <td>
                <span>${word.Mean}</span>
            </td>
        </tr>`;
}