function checkExpand() {
    ToggleVisibility(event.srcElement.id);
};
function ToggleVisibility(id) {
    if ("" != id) {
        var elOut = document.all[id];
        var elIn = document.all[id + "_"];
        if (null != elIn) {
            if (elIn.style.display == "none") {
                elIn.style.display = "";
                elOut.innerText = "-";
            }
            else {
                elIn.style.display = "none";
                elOut.innerText = "+";
            }
        }
    }
};
function ToggleVisibility2(id) {
    if ("" != id) {
        var elOutP = document.all[id + "P"];
        var elOutM = document.all[id + "M"];
        var elIn = document.all[id + "_"];
        if (null != elIn) {
            if (elIn.style.display == "none") {
                elIn.style.display = "";
                elOutP.display = "none";
                elOutM.display = "";
            }
            else {
                elIn.style.display = "none";
                elOutP.display = "";
                elOutM.display = "none";
            }
        }
    }
};
function SetVisibility(id, sd) {
    if ("" != id) {
        var elOut = document.all[id];
        var elIn = document.all[id + "_"];
        if (null != elIn) {
            if (sd == 1) {
                elIn.style.display = "";
                elOut.innerText = "-";
            }
            else {
                elIn.style.display = "none";
                elOut.innerText = "+";
            }
        }
    }
};