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

    $(".wb_lesson").on('change', () => {
        var listLs = "";
        let listLesson = $(".wb_lesson[type=checkbox]:checked");
        if (!listLesson || listLesson.length == 0) {
            $(".wb_selected").html(listLs);
            return;
        }
        listLesson.each(x => {
            listLs = listLs + (x == 0 ? '' : ', ') + listLesson[x].value;
        });
        $(".wb_selected").html(listLs);
    });

    $(".kj_lesson").on('change', () => {
        var listLs = "";
        let listLesson = $(".kj_lesson[type=checkbox]:checked");
        if (!listLesson || listLesson.length == 0) {
            $(".kj_selected").html(listLs);
            return;
        }
        listLesson.each(x => {
            listLs = listLs + (x == 0 ? '' : ', ') + listLesson[x].value;
        });
        $(".kj_selected").html(listLs);
    });

    $(".btn_repeat").on('click', () => {
        this.toggleClass("on");
    });
    $(".btn_wordhard").on('click', () => {
        this.toggleClass("on");
    });

    // ==================== Test Start ====================
    $('#wb_lesson_1').attr('checked', 'checked');
    //start();
    // ==================== Test end ====================
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
    resetHideStatus();

    let listLesson = $("input[type=checkbox]:checked");
    $("btn_ontop").click();
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
                case "OnLyHard":
                    listWbTemp.forEach(wb1=>{
                        if (wordHardHistory.includes(wb1.Id.toString())){
                            wb1.IsWordHard = true;
                        }
                    })
                    listWbTemp = listWbTemp.filter(wb => wb.IsWordHard);
                    break;
            }
            listWordbook = listWordbook.concat(listWbTemp);
            i++;
        });
    });
    listWordbook.forEach(x => {
        if (wordHardHistory.includes(x.Id.toString())) {
            x.IsWordHard = true;
        }
    });
    if ($('#type_select').val()!="view"){
        listWordbook = derangeArray(listWordbook);
    }
    
    viewListWordbook();

    $(".div_main").addClass("hide");
    $(".test_wb").removeClass("hide");

    saveSetting();
}
