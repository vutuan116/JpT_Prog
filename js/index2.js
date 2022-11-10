
function viewListWordbook() {
    var html = "";
    var index = 1;
    // ==================== Test Start ====================
    // var typeSelected = "view";
    var typeSelected = $('#type_select').val();
    // ==================== Test End ====================
    switch (typeSelected) {
        case "test":
            listWordbook.forEach(x => {
                var isShowHira = getRandomInt(0, 100) % 2 == 0;
                if (isShowHira) {
                    html = html + genHtmlForWordBook(x, true, false);
                } else {
                    html = html + genHtmlForWordBook(x, false, true);
                }
                index++;
            });
            break;
        case "hideWord":
            listWordbook.forEach(x => {
                html = html + genHtmlForWordBook(x, false, true);
                index++;
            });
            break;
        case "hideMean":
            listWordbook.forEach(x => {
                html = html + genHtmlForWordBook(x, true, false);
                index++;
            });
            break;
        default:
            listWordbook.forEach(x => {
                html = html + genHtmlForWordBook(x, true, true);
                index++;
            });
            $(".wb_btn.checkWb").addClass("hide");
            $(".wb_btn.againCheckWb").removeClass("hide");
            $(".th_col_index").removeClass("hide");
            $(".th_btn_support").removeClass("hide");
            html = html.replaceAll('th_btn_support wordhard hide', 'th_btn_support wordhard');
    }

    $("#tbl_body_list_wordbook").html(html);
}

function checkWordbook() {
    $(".wb.hide").removeClass("hide");
    $(".wb_btn.checkWb").addClass("hide");
    $(".th_btn_support").removeClass("hide");
    goTop();
}

function againTestWb() {
    var listWbChecked = $(".word_repeat:checked");

    if (!listWbChecked || listWbChecked.length == 0) {
        saveLessonHistory();
        saveWordHard();
        goHome();
        viewListLesson();
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

function genHtmlForWordBook(word, isShowHira, isShowMean) {
    let resultHtml = `<tr>`;
    if (word.IsWordHard) {
        resultHtml = resultHtml +
            `<td class="th_btn_support wordhard hide text-center bd_l_0">
                <i class="fas fa-star btn_wordhard on" value="${word.Id}" onclick="this.classList.toggle('on')"></i>
            </td>`;
    } else {
        resultHtml = resultHtml +
            `<td class="th_btn_support wordhard hide text-center bd_l_0">
                <i class="fas fa-star btn_wordhard" value="${word.Id}" onclick="this.classList.toggle('on')"></i>
            </td>`;
    }

    if (isShowHira) {
        resultHtml = resultHtml +
            `<td>
                <ruby class="wb">${word.Hira}
                    <rt>${word.Kanji}</rt>
                </ruby>
            </td>`;
    } else {
        resultHtml = resultHtml +
            `<td>
                <ruby class="wb hide">${word.Hira}
                    <rt>${word.Kanji}</rt>
                </ruby>
                <input class="w-100">
            </td>`;
    }

    if (isShowMean) {
        resultHtml = resultHtml +
            `<td>
                <span>${word.Mean}</span>
            </td>`;
    } else {
        resultHtml = resultHtml +
            `<td>
                <input class="w-100">
                <span class="wb hide">${word.Mean}</span>
            </td>`;
    }

    resultHtml = resultHtml +
        `<td class="th_btn_support text-center hide bd_r_0">
            <i class="fas fa-repeat btn_repeat" onclick="this.classList.toggle('on')"></i>
        </td>`;
    return resultHtml;
}
