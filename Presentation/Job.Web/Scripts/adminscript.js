$(function () {
    $("input[role=date]").datepicker({
            dateFormat:"dd/mm/yy"
    })
    $("#frmCreateNews #StateProvince").change(function () {
        getDistrict(this, "ddlDistrictId");
    })
    $("#frmUpdateShop select[name=State]").change(function () {
        getDistrict(this, "ddlDistrictId");
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
function getDistrict(e,elDistrictId)
{
    var selectedValue = $(e).val();
    $("#" + elDistrictId).html("<option>Chọn quận/huyện</option>");
    var defaultValue= $("#" + elDistrictId).data("val");
    $.getJSON("/common/GetDistrict", { provinceId: selectedValue }, function (data) {
        var selected = "";
        if (defaultValue != "")
            selected = "selected=selected"
        for (var d in data)
            $("#" + elDistrictId).append("<option " + selected + " value='" + data[d].Id + "'>" + data[d].Name + "</option>");
    });
}