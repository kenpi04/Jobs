$(function () {
    $("input[role=date]").datepicker({
            dateFormat:"dd/mm/yy"
    })

})
function setDatePicker(id)
{
    $(id).datepicker({
        dateFormat:"dd/mm/yy"

    })
}
function deleteNews(id)
{
    if (confirm("Bạn có chắc xóa tin này!")) {
        $.post("/admin/news/deletenews", { id: id }, function (d) {
            alert(d);
            window.location.reload();
        })
    }
    return false;
}
function deleteCarrerNews(id) {
    if (confirm("Bạn có chắc xóa tin này!")) {
        $.post("/admin/news/deleteCarrerNews", { id: id }, function (d) {
            alert(d);
            window.location.reload();
        })
    }
    return false;
}