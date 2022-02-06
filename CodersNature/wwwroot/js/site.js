const txt = document.querySelector('.myTextarea');

function setNewSize() {
    this.style.height = "1px";
    this.style.height = this.scrollHeight + "px";
}
txt.addEventListener('keyup', setNewSize);

function startChecker() {
    var startCheck = document.getElementById("StartDateCheckbx");
    if (startCheck.checked == true) {
        document.getElementById("startInput").disabled = true;
        document.getElementById("startInput").style.color = "gray";
    }
    else {
        document.getElementById("startInput").disabled = false;
        document.getElementById("startInput").style.color = "#552017";
    }
}
function finishChecker() {
    var finishCheck = document.getElementById("DeadlineCheckbx");
    if (finishCheck.checked == true) {
        document.getElementById("deadlineInput").disabled = true;
        document.getElementById("deadlineInput").style.color = "gray";
    }
    else {
        document.getElementById("deadlineInput").disabled = false;
        document.getElementById("deadlineInput").style.color = "#552017";
    }
}


