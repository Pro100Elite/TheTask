function ReplaceButton(empNo) {
    var btn = document.createElement('button');
    btn.setAttribute('class', "btn");
    btn.setAttribute('id', empNo);
    $(btn).text("-");

    $('#Replace-' + empNo).parent().replaceWith(btn);
}

$(document).on('click', "button", function () {
    var id = $(this).attr('id');
    var btn = $(this).text();
    if (btn != "+") {
        $("[id = '" + id + "'][class = parent]").hide(300);
        $(this).text("+");
    } else {
        $("[id = '" + id + "'][class = parent]").show(300);
        $(this).text("-");
    }
});