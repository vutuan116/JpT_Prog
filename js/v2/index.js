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
        listWbTemp = listWbTemp.filter(wb => wb.IsHard);
    }

    if (!_listWordbook || _listWordbook.length == 0) {
        alert("Không có từ phù hợp");
        return;
    }

    if ($('#view_type_sel').val() != "show-all") {
        _listWordbook = derangeArray(_listWordbook);
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
    var index = 0;
    var typeSelected = $('#view_type_sel').val();
    switch (typeSelected) {
        case "show-all":
            // _listWordbook.forEach(x => {
            //     var isShowHira = getRandomInt(0, 100) % 2 == 0;
            //     if (isShowHira) {
            //         html = html + genHtmlForWordBook(index, x, true);
            //     } else {
            //         html = html + genHtmlForWordBook(index, x, false);
            //     }
            //     index++;
            // });
            // $(".th_col_index").removeClass("hide");
            // $(".wb_btn.checkWb").removeClass("hide");
            // $(".wb_btn.againCheckWb").addClass("hide");
            // $(".fa-eye").each(function () {
            //     $(this).addClass("hide");
            // });

            _listWordbook.forEach(word => {
                html += genHtmlForWordBook(index, word, false);
                index++;
            });
            break;
        default:
            // _listWordbook.forEach(x => {
            //     html += genHtmlForWordBook(x, true, true);
            //     index++;
            // });
            // $(".wb_btn.checkWb").addClass("hide");
            // $(".wb_btn.againCheckWb").removeClass("hide");
            // $(".th_col_hard").removeClass("hide");
            // html = html.replaceAll('th_col_hard wordhard hide', 'th_col_hard wordhard');
            // $(".fa-eye").each(function () {
            //     $(this).removeClass("hide");
            // });
    }

    $("#tbl_show_wordbook > tbody").html(html);
}


function genHtmlForWordBook(index, word, isShowRandom) {
    let resultHtml = `<tr>`;
    resultHtml += `<td class="text-center">${index}</td>`;
    var isShowHira = isShowRandom ? getRandomInt(0, 100) % 2 == 0 : true;
    resultHtml +=
        `<td class="td_wHard">
            <i id="btn_isHard_${word.Id}" class="fas fa-star btn_wordhard ${word.IsHard ? "on" : ""}" value="${word.Id}" onclick="this.classList.toggle('on')"></i>
        </td>`;

    resultHtml +=
        `<td onclick="toggleHideEle(this)">
            <ruby class="wb wordbook ${isShowHira ? "hi_de" : "hide"}">${word.Hira}
                <rt>${word.Kanji}</rt>
            </ruby>
        </td>`;

    resultHtml +=
        `<td onclick="toggleHideEle(this)">
            <span class="mean ${isShowRandom && isShowHira ? "hide" : "hi_de"}">${word.Mean}</span>
        </td>`;

    resultHtml +=
        `<td class="th_col_repeat text-center">
            <i class="fad fa-repeat-1 btn_repeat" onclick="this.classList.toggle('on')" index="${index}"></i>
        </td>`;

    return resultHtml;
}