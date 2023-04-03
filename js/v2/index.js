function start() {
    _listWordbook = [];
    let listLesson = $("input[type=checkbox]:checked");
    $(".btn_ontop")[0].click();
    if (!listLesson || listLesson.length == 0) {
        alert("Hãy chọn ít nhất 1 bài học");
        return;
    }

    let level = $("#level_sel").val();
    let wordType = $("#word_type_sel").val();

    listLesson.each(x => {
        var lesson = listLesson[x];
        var tempWb = [];
        if ($("#wb_kan_sel").val() == "wordbook") {
            tempWb = _tuVungJson.filter(y => y.Level == level && y.Lesson == lesson.value)[0];
        } else {
            tempWb = _kanjiJson.filter(y => y.Level == level && y.Lesson == lesson.value)[0];
        }
        _listWordbook = _listWordbook.concat(tempWb.Data);
    });

    _listWordbook.forEach(wb1 => {
        wb1.IsHard = wordHardHistory.includes(wb1.Id.toString());
    });

    if (wordType == "hard") {
        _listWordbook = _listWordbook.filter(wb => wb.IsHard);
    }

    if (!_listWordbook || _listWordbook.length == 0) {
        alert("Không có từ phù hợp");
        return;
    }

    if ($("#wb_kan_sel").val() == "wordbook") {
        viewWordbook();
    } else {
        viewKanji();
    }

    $(".div_main").addClass("hide");
    $(".test_wb").removeClass("hide");

    saveSetting();
}

function viewWordbook() {
    var html = "";
    var index = 1;
    var typeSelected = $('#view_type_sel').val();
    switch (typeSelected) {
        case "show-random":
            _listWordbook.forEach(word => {
                html += genHtmlForWordBook(index, word, true);
                index++;
            });
            break;
        default:
            _listWordbook.forEach(word => {
                html += genHtmlForWordBook(index, word, false);
                index++;
            });
            break;
    }

    $("#tbl_show_wordbook > tbody").html(html);
}


function genHtmlForWordBook(index, word, isShowRandom) {
    let resultHtml = `<tr>`;
    var isShowHira = isShowRandom ? getRandomInt(0, 100) % 2 == 0 : true;
    resultHtml +=
        `<td class="td_wHard pr-0 bd_r_0">
            <label class="lb_no">${index}</label>
            <i value="${word.Id}" class="fas fa-star btn_wordhard ${word.IsHard ? "on" : ""}" value="${word.Id}" onclick="this.classList.toggle('on')"></i>
        </td>`;

    resultHtml +=
        `<td class="bd_l_0" onclick="toggleHideEle(this)">
            <ruby class="wb td_wordbook ${isShowHira ? "hi_de" : "hide"}">${word.Hira}
                <rt>${word.Kanji}</rt>
            </ruby>
        </td>`;

    resultHtml +=
        `<td class="lineh-1" onclick="toggleHideEle(this)">
            <span class="td_mean ${isShowRandom && isShowHira ? "hide" : "hi_de"}">${word.Mean}</span>
        </td>`;

    return resultHtml;
}

function mixWb() {
    saveWordHard();
    _listWordbook = derangeArray(_listWordbook);

    if ($("#wb_kan_sel").val() == "wordbook") {
        viewWordbook();
    } else {
        viewKanji();
    }
    $(".btn_ontop")[0].click();
}

function mixOnlyHardWb() {
    var listHard = $(".btn_wordhard.on");
    if (!listHard || listHard.length == 0) {
        alert("Không có từ phù hợp");
        return;
    }
    saveWordHard();
    var listWbHard = [];
    listHard.each(x => {
        listWbHard.push(_listWordbook.filter(y => y.Id == Number(listHard[x].getAttribute("value")))[0]);
    });

    _listWordbook = derangeArray(listWbHard);

    if ($("#wb_kan_sel").val() == "wordbook") {
        viewWordbook();
    } else {
        viewKanji();
    }
    $(".btn_ontop")[0].click();
}

function saveAndBack() {
    saveLessonHistory();
    saveWordHard();
    goHome();
    viewListLesson();
}

function showAll(elementClassId) {
    var colElement = $("."+elementClassId);
    if (colElement[0].outerHTML.includes("hide")) {
        colElement.each(x => {
            $(colElement[x]).removeClass("hide");
            $(colElement[x]).addClass("hi_de");
        });
    } else {
        colElement.each(x => {
            $(colElement[x]).removeClass("hi_de");
            $(colElement[x]).addClass("hide");
        });
    }
}