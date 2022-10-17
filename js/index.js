var levelJson = [];
var tuVungJson = [];
var kanjiJson = [];

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

    $('#wb_lesson_12').attr('checked', 'checked');
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
                    <label class="cursor_pointer" for="wb_lesson_'${indexWb}">&nbsp;${x.Lesson}</label>
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

    $("html").scrollTop(0);

    let listWordbook = [];
    if (!listLesson || listLesson.length == 0) {
        alert("Hãy chọn ít nhất 1 bài học");
        return;
    }
    listLesson.each(x => {
        lesson = listLesson[x];
        tuVungJson.filter(y => y.Level == level && y.Lesson == lesson.value).forEach(z => {
            listWordbook = listWordbook.concat(z.Data);
        });
    });

    listWordbook = derangeArray(listWordbook);
    viewListWordbook(listWordbook);

    $("#menu_div").hide();
    $("#test_div").show();
}

function viewListWordbook(listWb, type) {
    var html = "";
    if (type == null) {
        var index = 0;
        listWb.forEach(x => {
            var isMod = getRandomInt(0, 100) % 2 == 0;
            var hiraShow = 'hide';
            var meanShow = '';
            if (isMod) {
                var hiraShow = '';
                var meanShow = 'hide';
            }

            html = html +
                `<tr id="word_${index}">
                    <td>
                        <ruby class="${meanShow}">${x.Hira}
                            <rt>${x.Kanji}</rt>
                        </ruby>
                        <input class="w-100 ${hiraShow}">
                    </td>
                    <td>
                        <input class="${meanShow}">
                        <span class="${hiraShow}">${x.Mean}</span>
                    </td>
                    <td class="td_btn_repeat hide">
                        <input class="cursor_pointer" type="checkbox" id="word_repeat_${index}">
                        <label class="cursor_pointer" for="word_repeat_${index}"><i class="fa-solid fa-repeat"></i></label>
                    </td>
                </tr>`;

            index++;
        })
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
