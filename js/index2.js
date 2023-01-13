
function viewListWordbook() {
    resetHideStatus();
    var html = "";
    var index = 1;
    var typeSelected = $('#type_select').val();
    switch (typeSelected) {
        case "test":
            listWordbook.forEach(x => {
                var isShowHira = getRandomInt(0, 100) % 2 == 0;
                if (isShowHira) {
                    html = html + genHtmlForWordBook(x, true, false, index);
                } else {
                    html = html + genHtmlForWordBook(x, false, true, index);
                }
                index++;
            });
            $(".th_col_index").removeClass("hide");
            $(".wb_btn.checkWb").removeClass("hide");
            $(".wb_btn.againCheckWb").addClass("hide");
            $(".fa-eye").each(function () {
                $(this).addClass("hide");
            });
            break;
        default:
            listWordbook.forEach(x => {
                html = html + genHtmlForWordBook(x, true, true);
                index++;
            });
            $(".wb_btn.checkWb").addClass("hide");
            $(".wb_btn.againCheckWb").removeClass("hide");
            $(".th_col_hard").removeClass("hide");
            html = html.replaceAll('th_col_hard wordhard hide', 'th_col_hard wordhard');
            $(".fa-eye").each(function () {
                $(this).removeClass("hide");
            });
    }

    $("#tbl_body_list_wordbook").html(html);
}

function checkWordbook() {
    $(".wb.hide").removeClass("hide");
    $(".wb_btn.checkWb").addClass("hide");
    $(".th_col_hard").removeClass("hide");
    $(".th_col_repeat").removeClass("hide");
    $(".wb_btn.againCheckWb").removeClass("hide");
    $(".btn_ontop").click();
}

function againTestWb() {
    var listWbChecked = $(".btn_repeat.on");

    if (!listWbChecked || listWbChecked.length == 0) {
        saveLessonHistory();
        saveWordHard();
        goHome();
        viewListLesson();
        return;
    }

    var listWbRepeat = [];
    listWbChecked.each(x => {
        listWbRepeat.push(listWordbook[Number(listWbChecked[x].getAttribute("index")) - 1]);
    });

    listWordbook = listWbRepeat;
    viewListWordbook();

    $(".wb_btn.checkWb").removeClass("hide");
    $(".wb_btn.againCheckWb").addClass("hide");
    $("btn_ontop").click();
}

function genHtmlForWordBook(word, isShowHira, isShowMean, index) {
    let resultHtml = `<tr>`;

    if (index) {
        resultHtml = resultHtml +
            `<td class="text-center">${index}</td>`;
    }

    if (word.IsWordHard) {
        resultHtml = resultHtml +
            `<td class="th_col_hard wordhard hide text-center bd_l_0">
                <i class="fas fa-star btn_wordhard on" value="${word.Id}" onclick="this.classList.toggle('on')"></i>
            </td>`;
    } else {
        resultHtml = resultHtml +
            `<td class="th_col_hard wordhard hide text-center bd_l_0">
                <i class="fas fa-star btn_wordhard" value="${word.Id}" onclick="this.classList.toggle('on')"></i>
            </td>`;
    }

    if (isShowHira) {
        resultHtml = resultHtml +
            `<td onclick="show(this)">
                <ruby class="wb wordbook">${word.Hira}
                    <rt>${word.Kanji}</rt>
                </ruby>
            </td>`;
    } else {
        resultHtml = resultHtml +
            `<td onclick="show(this)">
                <ruby class="wb wordbook hide gre_color">${word.Hira}
                    <rt>${word.Kanji}</rt>
                </ruby>
            </td>`;
    }

    if (isShowMean) {
        resultHtml = resultHtml +
            `<td onclick="show(this)">
                <span class="mean">${word.Mean}</span>
            </td>`;
    } else {
        resultHtml = resultHtml +
            `<td onclick="show(this)">
                <span class="mean wb hide gre_color">${word.Mean}</span>
            </td>`;
    }

    resultHtml = resultHtml +
        `<td class="th_col_repeat text-center hide bd_r_0">
            <i class="fas fa-repeat btn_repeat" onclick="this.classList.toggle('on')" index="${index}"></i>
        </td>`;
    return resultHtml;
}

function randomTestWb() {
    saveWordHard();
    var html = "";
    var index = 1;

    listWordbook = derangeArray(listWordbook);

    listWordbook.forEach(x => {
        html = html + genHtmlForWordBook(x, true, true);
        index++;
    });

    $(".th_col_hard").removeClass("hide");
    html = html.replaceAll('th_col_hard wordhard hide', 'th_col_hard wordhard');
    $(".fa-eye").each(function () {
        $(this).removeClass("hide");
    });
    $("#tbl_body_list_wordbook").html(html);
    $(".btn_ontop")[0].click();
}