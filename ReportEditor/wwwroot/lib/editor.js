
// シートの左上のスクリーン座標を取得する
window.getPaperOffset = (sheetID) => {
    var sheet = document.getElementById(sheetID);
    var rect = sheet.getBoundingClientRect();
    return new Array(rect.left, rect.top);
}


// 指定したIDの要素の情報を取得する
window.getComponentInfo = (id) => {

    var element = document.getElementById(id);
    var rect = element.getBoundingClientRect();
    var obj = {
        ID: element.id,
        X: rect.left,
        Y: rect.top,
        Height: rect.height,
        Width: rect.width,
    };
    return obj;
}

window.getComponents = (sheetID) => {

    var sheet = document.getElementById(sheetID);
    if (sheet == null) {
        return null;
    }
    var elements = Array.from(sheet.children);
    var components = [];
    // 1つ目にシートを入れておく
    components.push(getComponentInfo(sheet.id));

    elements.forEach((element) => {
        if (element.id == undefined) {
            return null;
        }
        var info = getComponentInfo(element.id)
        components.push(info);
    });

    return Array.from(components);
}

// デバッグ用
window.onDebugButtonClick = () => {

    var sheetID = "omponent8-group";
    console.log(getComponents(sheetID));

}

// 指定したIDの要素のVisibilityを変更する
window.setVisibility = (id, visible) => {
    var element = document.getElementById(id);

    if (visible) {
        element.classList.remove("hidden");
    } else {
        element.classList.add("hidden");
    }

}


