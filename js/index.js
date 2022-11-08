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
    viewListLesson();

    goHome();
    // ==================== Test Start ====================
    $('#wb_lesson_1').attr('checked', 'checked');
    //start();
    // ==================== Test end ====================


    $(".wb_lesson").on('change', function () {
        let listLesson = $(".wb_lesson[type=checkbox]:checked");
        if (!listLesson || listLesson.length == 0) {
            return;
        }
        var listLs = "";
        listLesson.each(x => {
            listLs = listLs + (x==0?'':', ') + listLesson[x].value;
        });
        $(".wb_selected").html(listLs);
    });

    $(".kj_lesson").on('change', function () {
        let listLesson = $(".kj_lesson[type=checkbox]:checked");
        if (!listLesson || listLesson.length == 0) {
            return;
        }
        var listLs = "";
        listLesson.each(x => {
            listLs = listLs + (x==0?'':', ') + listLesson[x].value;
        });
        $(".kj_selected").html(listLs);
    });
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

function viewListLesson() {
    let indexWb = 0;
    let htmlWb = "";
    let htmlKj = "";
    let level = $("#level_select").val();

    $("#wordbook_lesson_div").empty();
    $("#kanji_lesson_div").empty();
    tuVungJson.forEach(x => {
        indexWb++;
        if (level == x.Level) {
            let historyLs = lessonHistory.find(lsItem => lsItem.Name == x.Lesson);
            htmlWb = htmlWb +
                `<tr>
                    <td>
                        <input class="cursor_pointer wb_lesson" type="checkbox" value="${x.Lesson}" id="wb_lesson_${indexWb}">
                        <label class="cursor_pointer" for="wb_lesson_${indexWb}">&nbsp;${x.Lesson}</label>
                    </td>
                    <td class="text-end">${historyLs ? historyLs.Time : ''}</td>
                </tr>`;
        }
    });

    $("#wordbook_lesson_div").html(htmlWb);

    kanjiJson.forEach(x => {
        indexWb++;
        if (level == x.Level) {
            let historyLs = lessonHistory.find((lsItem) => { return lsItem.Name == x.Lesson });
            htmlKj = htmlKj +
                `<tr>
                    <td>
                        <input class="cursor_pointer kj_lesson" type="checkbox" value="${x.Lesson}" id="wb_lesson_${indexWb}">
                        <label class="cursor_pointer" for="wb_lesson_${indexWb}">&nbsp;${x.Lesson}</label>
                    </td>
                    <td class="text-end">${historyLs ? historyLs.Time : ''}</td>
                </tr>`;
        }
    });
    $("#kanji_lesson_div").html(htmlKj);
}

function start() {
    let listLesson = $("input[type=checkbox]:checked");
    goTop();
    if (!listLesson || listLesson.length == 0) {
        alert("Hãy chọn ít nhất 1 bài học");
        return;
    }

    let level = $("#level_select").val();
    var i = 0;
    let wordType = $("#wordtype_select").val();

    listLesson.each(x => {
        lesson = listLesson[x];
        tuVungJson.filter(y => y.Level == level && y.Lesson == lesson.value).forEach(z => {
            listWbTemp = z.Data;
            switch (wordType) {
                case "NVA":
                    listWbTemp = listWbTemp.filter(wb => wb.Type == "N" || wb.Type == "V" || wb.Type == "A");
                    break;
                case "N":
                    listWbTemp = listWbTemp.filter(wb => wb.Type == "N");
                    break;
                case "V":
                    listWbTemp = listWbTemp.filter(wb => wb.Type == "V");
                    break;
                case "A":
                    listWbTemp = listWbTemp.filter(wb => wb.Type == "A");
                    break;
                case "O":
                    listWbTemp = listWbTemp.filter(wb => wb.Type == "O");
                    break;
            }
            listWordbook = listWordbook.concat(listWbTemp);
            i++;
        });
    });

    listWordbook = derangeArray(listWordbook);
    viewListWordbook();

    $(".div_main").addClass("hide");
    $(".test_wb").removeClass("hide");

    saveSetting();
}

function viewListWordbook() {
    var html = "";
    var id = 0;
    // ==================== Test Start ====================
    // var typeSelected = "view";
    var typeSelected = $('#type_select').val();
    // ==================== Test End ====================
    switch (typeSelected) {
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
                html = html + viewWord(x, id);
                id++;
            });
            $(".wb_btn.checkWb").addClass("hide");
            $(".wb_btn.againCheckWb").removeClass("hide");
    }

    $("#tbl_body_list_wordbook").html(html);
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
        if ($("#type_select").val() != "view") {
            saveLessonHistory();
            viewListLesson();
        }
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

function hideWord(word, id) {
    return "" +
        `<tr">
            <td class="text-center tbl-no-custom">${id}</td>
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
            <td class="text-center tbl-no-custom">${id}</td>
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

function viewWord(word, id) {
    return "" +
        `<tr">
            <td class="text-center tbl-no-custom">${id}</td>
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