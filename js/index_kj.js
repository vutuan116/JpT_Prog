
function genHtmlForKanji(word, isShowHira, isShowKanji, isShowMean, index) {
    let resultHtml = `<tr>`;

    if (index){
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
            `<td>
                <ruby class="wb text-center">${word.Kanji}
                    <rt>
                        <p class="text-center m-0">${word.Hira}</p>
                    </rt>
                </ruby>
                <p class="cnvi m-0 text-end">${word.CnVi}</p>
            </td>`;
    } else {
        resultHtml = resultHtml +
            `<td>
                <ruby class="wb text-center hide">${word.Kanji}
                    <rt>
                        <p class="text-center m-0">${word.Hira}</p>
                    </rt>
                </ruby>
                <p class="cnvi m-0 text-end">${word.CnVi}</p>
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
                <span class="wb hide gre_color">${word.Mean}</span>
            </td>`;
    }

    resultHtml = resultHtml +
        `<td class="th_col_repeat text-center hide bd_r_0">
            <i class="fas fa-repeat btn_repeat" onclick="this.classList.toggle('on')" index="${index}"></i>
        </td>`;
    return resultHtml;
}


function viewKanji(){
    resetHideStatus();
    var html = "";
    var index = 1;
    var typeSelected = $('#type_select').val();
    switch (typeSelected) {
        case "test":
            // listWordbook.forEach(x => {
            //     var isShowHira = getRandomInt(0, 100) % 2 == 0;
            //     if (isShowHira) {
            //         html = html + genHtmlForKanji(x, true, false, index);
            //     } else {
            //         html = html + genHtmlForWordBook(x, false, true, index);
            //     }
            //     index++;
            // });
            // $(".th_col_index").removeClass("hide");
            // $(".wb_btn.checkWb").removeClass("hide");
            // $(".wb_btn.againCheckWb").addClass("hide");
            break;
        default:
            listWordbook.forEach(x => {
                html = html + genHtmlForKanji(x, true, true,true);
                index++;
            });
            $(".wb_btn.checkWb").addClass("hide");
            $(".wb_btn.againCheckWb").removeClass("hide");
            $(".th_col_hard").removeClass("hide");
            html = html.replaceAll('th_col_hard wordhard hide', 'th_col_hard wordhard');
    }

    $("#tbl_body_list_wordbook").html(html);
}