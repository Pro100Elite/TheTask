$("#searchDept").on("keyup", function () {
    var value = $(this).val();

    $("#Dept tr").each(function (index) {
        if (index !== 0) {

            $row = $(this);

            var id = $row.find("#DeptName").text();

            if (id.indexOf(value) !== 0) {
                $row.hide();
            }
            else {
                $row.show();
            }
        }
    });
});

function ReplaceSplitRight() {

    $("#searchEmp").on("keyup", function () {
        var value = $(this).val();

        $("#Emp tr").each(function (index) {
            if (index !== 0) {

                $row = $(this);

                var id = $row.find("#EName").text();

                if (id.indexOf(value) !== 0) {
                    $row.hide();
                }
                else {
                    $row.show();
                }
            }
        });
    });
}

$("table").delegate("tr[data-id]", "click", function () {
    var dept = $(this);
    $.ajax({
        url: "/MasterDetail/DetailData?dept=" + $(this).attr("data-id"),
        type: 'GET',
        success: function (data) {
            $('#rp').html(data);
            $("#dName").text('Department Selected: ' + dept.children().first().text() + '');
            $("#searchEmp").on("keyup", function () {
                var value = $(this).val();

                $("#Emp tr").each(function (index) {
                    if (index !== 0) {

                        $row = $(this);

                        var id = $row.find("#EName").text();

                        if (id.indexOf(value) !== 0) {
                            $row.hide();
                        }
                        else {
                            $row.show();
                        }
                    }
                });
            });

            $(".active").removeClass("active");
            dept.addClass("active");
        }
    });
});