
function derangeArray(listItem) {
    let result = [];
    while (listItem.length > 0) {
        let index = getRandomInt(0, listItem.length - 1);
        result.push(listItem[index]);
        listItem.splice(index, 1);
    }
    return result;
}

Date.prototype.hhmmss = function () {
    var hh = this.getHours();
    var mm = this.getMinutes();
    var ss = this.getSeconds();

    return [hh < 10 ? '0' + hh : hh, mm < 10 ? '0' + mm : mm, ss < 10 ? '0' + ss : ss].join(':');
};

Date.prototype.yyyyMMdd = function () {
    var yy = this.getFullYear();
    var MM = this.getMonth() + 1;
    var dd = this.getDate();

    return [yy < 10 ? '0' + yy : yy, MM < 10 ? '0' + MM : MM, dd < 10 ? '0' + dd : dd].join('-');
};

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}
