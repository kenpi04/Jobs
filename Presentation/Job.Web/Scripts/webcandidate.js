var intValidate = 0;
var isShowWebCam = false;
var image = [];
var canvas = document.getElementById('imgdisplay');
var saveCB;
var ctx=null;
var pos = 0;
$(function () {

  
    if (canvas.toDataURL) {

        ctx = canvas.getContext("2d");
        image = ctx.getImageData(0, 0, 320, 240);

        saveCB = function (data) {

            var col = data.split(";");
            var img = image;

            for (var i = 0; i < 320; i++) {
                var tmp = parseInt(col[i]);
                img.data[pos + 0] = (tmp >> 16) & 0xff;
                img.data[pos + 1] = (tmp >> 8) & 0xff;
                img.data[pos + 2] = tmp & 0xff;
                img.data[pos + 3] = 0xff;
                pos += 4;
            }

            if (pos >= 4 * 320 * 240) {
                ctx.putImageData(img, 0, 0);
                //$.post("/upload.php", { type: "data", image: canvas.toDataURL("image/png") });
                pos = 0;
            }
        };

    } else {

        saveCB = function (data) {
            image.push(data);

            pos += 4 * 320;

            if (pos >= 4 * 320 * 240) {
                // $.post("/upload.php", { type: "pixel", image: image.join('|') });
                pos = 0;
            }
        };
    }

});
function ShowWebcam() {
    debugger;
    jQuery('#camera-main').fadeIn();
    //jQuery("#webcam").show();            
    if (!isShowWebCam) {
        var pageUrl = '/Recuiment';
        jQuery(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "callback",
                swffile: '/Scripts/jscam.swf',
                debug: function (type, status) {
                    jQuery('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    if (isShowWebCam) {
                        isShowWebCam = false;
                        saveCB(data);
                        $("#camera-main").hide();
                        //  $('#avatar-webcam').attr("src", "data:image/jpg;base64,"+data);
                        $('#camera-main').hide();
                        $('.wait-submit-camera').hide();
                        $('.btnCapture').show();
                        $('.btnCloseCapture').show();
                        $('.divUpdateProgress').css('display', 'none');
                        $("#imgdisplay").show();
                    }
                },
                onCapture: function () {
                    webcam.save();
                    isShowWebCam = true;
                }
            });
        });
    }
}

function Capture() {
    jQuery('.wait-submit-camera').show();
    jQuery('.btnCapture').hide();
    jQuery('.btnCloseCapture').hide();
    jQuery('.divUpdateProgress').css('display', 'block');
    webcam.capture();
    return false;
}

function CloseCapture() {
    jQuery('#camera-main').hide();
}

(function ($) {
    
    var intCompanyID = $('.txtCompanyID').val();
    if (intCompanyID == 2) {
        $('.box-title .underline').css('border-bottom', '2px solid #01A1E6');
        $('.button div.submit').css('background', '#01A1E6');
        $('.box-title .background').css('background', '#01A1E6');
        $('.divWebCandidate .c-btn').css('background', '#01A1E6');
    }

    var availableTags = ParseArray();

    function split(val) {
        return val.split(/,\s*/);
    }
    function extractLast(term) {
        return split(term).pop();
    }

    $(".txtWordplace2")
      // don't navigate away from the field on tab when selecting an item
      .bind("keydown", function (event) {
          if (event.keyCode === $.ui.keyCode.TAB &&
              $(this).autocomplete("instance").menu.active) {
              event.preventDefault();
          }
      })
      .autocomplete({
          minLength: 0,
          source: function (request, response) {
              // delegate back to autocomplete, but extract the last term
              response($.ui.autocomplete.filter(
                availableTags, extractLast(request.term)));
          },
          focus: function () {
              // prevent value inserted on focus
              return false;
          },
          select: function (event, ui) {
              var terms = split(this.value);
              // remove the current input
              terms.pop();
              // add the selected item
              terms.push(ui.item.label);
              // add placeholder to get the comma-and-space at the end
              terms.push("");
              this.value = terms.join(", ");

              var termsValue = split($('.hdfWordplaceSelected').val());
              termsValue.pop();
              termsValue.push(ui.item.value);
              termsValue.push("");
              $('.hdfWordplaceSelected').val(termsValue.join(","));
              return false;
          }
      });

    // New script
    $('#ddlRecruitmentProvince').on('change', function () {
        //alert($(this).val());
        LoadRecruitmentNew($(this).val(), 0);
    });

    $('.list-type-job input[type=radio]').on('click', function () {
        //alert($(this).val());
        LoadRecruitmentNew(0, $(this).val());
    });

    var TGDDLIB = function () { };
    TGDDLIB.HideShowLoading = function (isShow) {
        var model = $('#overlay').length;
        if (model == "0") {
            $('body').append("<div id='overlay'><span>Chờ một chút...</span></div>");
        }

        if (isShow) {
            $('#overlay').css("display", 'block');
        }
        else {
            $('#overlay').css("display", 'none');
        }
    }

    function LoadRecruitmentNew(ProvinceID, TypeID) {
        //TGDDLIB.HideShowLoading(true);  
        $('#overlay-2').show();
        setTimeout(function () {
            $.ajax({
                url: "/Recruitment/ReruitmentListPartial",
                type: "GET",
                data: {
                    ProvinceID: ProvinceID,
                    TypeID: TypeID
                },
                success: function (data) {
                    if (data != null) {
                        $("#divRecruitmentNew").html('');
                        $("#divRecruitmentNew").html(data);
                    }
                },
                error: function () {
                    alert("Server error");
                }
            }).always(function (data) {
                //TGDDLIB.HideShowLoading(false);
                $('#overlay-2').hide();
            });
        }, 1000);
    }

    // Event select work place
    $('#selWorkPlace').on('click', function () {
        $('.hdfListWordPlace').val();
        $('.txtWorkPlace').val();
        var itemValueChecked = '';
        var itemTextChecked = '';

        $('.list-province-wrap input[type="checkbox"]').each(function (index) {            
            if ($(this).prop('checked') == true) {                
                itemValueChecked += $(this).val() + ',';
                itemTextChecked += $(this).attr('name') + ', ';
            }
        });

        $('.hdfListWordPlace').val(itemValueChecked);
        $('.txtWorkPlace').val(itemTextChecked);
        $('.divListProvince').hide();
    });
    $('#unselWorkPlace').on('click', function () {
        $('.hdfListWordPlace').val('');
        $('.txtWorkPlace').val('');

        $('.list-province-wrap input[type="checkbox"]').each(function (index) {
            $(this).prop('checked', false);
        });

        $('.divListProvince').hide();
    });

    function isDate(txtDate)
    {
        var currVal = txtDate;
        if(currVal == '')
            return false;
  
        //Declare Regex  
        var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
        var dtArray = currVal.match(rxDatePattern); // is format OK?
 
        if (dtArray == null)
            return false;

        //Checks for dd/mm/yyyy format.
        dtDay = dtArray[1];
        dtMonth= dtArray[3];
        dtYear = dtArray[5];  
 
        if (dtMonth < 1 || dtMonth > 12)
            return false;
        else if (dtDay < 1 || dtDay> 31)
            return false;
        else if ((dtMonth==4 || dtMonth==6 || dtMonth==9 || dtMonth==11) && dtDay ==31)
            return false;
        else if (dtMonth == 2)
        {
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
            if (dtDay> 29 || (dtDay ==29 && !isleap))
                return false;
        }
        return true;
    }    

    function CheckValidate() {
        var isValid = true;
        $(".divWebCandidate .required123456").each(function( index ) {
            //alert($(this).val() );
            var value = $(this).val();
            if (value == '') {
                $(this).focus();
                $(this).addClass('parsley-error');
                isValid = false;
                return false;
            }
        });

        return isValid;
    }

    $(".divWebCandidate .required123456").change(function () {
        var value = $(this).val();
        if (value == '') {
            $(this).focus();
            $(this).addClass('parsley-error');
            return false;
        }
        else {
            $(this).removeClass('parsley-error');
        }
    });

    $('#btnSave').on('click', function () {
        // Kiem tra form nhap rong
        if (CheckValidate() == false) {
            return false;
        }

        // Kiem tra field ngay gio
        if (!isDate($('#txtBirthDay').val())) {
            $('#txtBirthDay').focus();
            $('#txtBirthDay').addClass('parsley-error');
            isValid = false;
            return false;
        }

       // $('#submitCaptcha').click();
        //if (tokenReCaptcha == '') {
        //    alert('Hãy xác nhận bạn không phải là robot?');
        //    return false;
        //}

        var fuAvatar;
        if ($('#avatar-webcam').attr('src') == '/Content/themes/Recruitment/Images/camera.jpg') {
            if ($('#isUploadControl').val() == '1') {
                fuAvatar = document.getElementById("imgdisplayHide").toDataURL("image/jpeg", 0.75);
                fuAvatar = fuAvatar.replace(/^data:image\/(png|jpeg|jpg);base64,/, "");
            }
            else {
                alert('Bạn chưa chọn ảnh đại diện');
                return false;
            }
        }
        TGDDLIB.HideShowLoading(true);
        uploadFile(function(){
            var fuFileAttach = $('#hiddenFileAttach').val();
       
            var model = new Object();

            //  model.TokenCaptcha = tokenReCaptcha;

            model.RecruitmentRequestDetailID = $('#rrdID').val();        
            model.CateId = $('#ddlRecruitmentPosition').val();
            model.WorkPlace = $('.hdfListWordPlace').val();

            model.Fullname = $('#txtFullname').val();
            model.Gender = $('.gender input[name=gender]:checked').val();
            model.BirthDay = $('#txtBirthDay').val();
            model.BirthPlace = $('#ddlBirthPlace').val();
            model.IDNo = $('#txtIDNo').val();

            model.MarriedStatus = $('#radMarriedStatus input[name=radMarriedStatus]:checked').val();
            model.Address = $('#txtResident').val();
            model.StateProvinceId = $('#ddlResidentProvince').val();

            model.DistrictId = $('#ddlResidentDistrict').val();
            model.Email = $('#txtEmail').val();
            model.Mobile = $('#txtMobile').val();
            model.Height = $('#txtHeight').val();
            model.Weight = $('#txtWeight').val();
            model.DesiredIncome = $('#txtDesiredIncome').val();
            model.PersonalInformation = $('#txtPersonalInformation').val();

            model.Shool = $('#txtShoolName').val();
            model.MajorContent = $('#txtMajorContent').val();
            model.QualificationType = $('#ddlQualificationType').val();
            model.Certificate = $('#txtCertificate').val();

            model.StudyScheduleMorning = $('#hdfSchedule_Morning').val();
            model.StudyScheduleAfternoon = $('#hdfSchedule_Afternoon').val();
            model.StudyScheduleEvening = $('#hdfSchedule_Evening').val();

            // Them danh sach qua trinh lam viec
            if ($('#txtCompany').val() != '' && $('#txtPosition').val() != '' && $('#txtNote').val() != '') {
                var objWorkExperience = new Object();
                objWorkExperience.Company = $('#txtCompany').val();
                objWorkExperience.Position = $('#txtPosition').val();
                objWorkExperience.Description = $('#txtNote').val();

                lstWorkExperience.push(objWorkExperience);
            }
            model.WokedCompany = lstWorkExperience;

            // Gen file upload avatar
        

            // Gen file upload attachment
    

        
            model.Image = fuAvatar;
            model.CvFileName = fuFileAttach;
            var jsonModel = JSON.stringify(model);

            $('.btnCancel').hide();
            $('.btnSave').hide();
            $('.wait-submit').show();
           
            $.ajax({
                url: "/Recuiment/Post",
                type: "POST",
                data: jsonModel,  
                contentType: "application/json; charset=utf-8",
                dataType:'JSON',
                success: function (data) {
                    if (data != null) {
                        if (data == 'success') {
                            alert('Cảm ơn bạn đã đăng ký ứng tuyển tại Thế Giới Di Động. Hồ sơ của bạn đã được lưu vào hệ thống. Xem chi tiết quy trình tuyển dụng tại Thế Giới Di Động.');
                            window.location.href = '/process';
                        }
                        else {
                            alert(data);
                        }

                        $('.btnCancel').show();
                        $('.btnSave').show();
                        $('.wait-submit').hide();
                    }
                },
                error: function () {
                    alert("Website đang trong quá trình nâng cấp, vui lòng quay lại sau. Rất xin lỗi và mong bạn thông cảm vì sự bất tiện này");
                },
                complete: function () {
                    TGDDLIB.HideShowLoading(false);
                }
            }).always(function (data) {
                
            });
        });
        return false;
    });

    function uploadFile(callback) {
        
  
        var files = document.getElementById("fuFileAttach").files;
        if (files.length > 0) {
            if (window.FormData !== undefined) {
                var data = new FormData();
                for (var x = 0; x < files.length; x++) {
                    data.append("file" + x, files[x]);
                }

                $.ajax({
                    type: "POST",
                    url: '/Recuiment/UploadFile',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (result) {
                        console.log(result);
                        $('#hiddenFileAttach').val(result);
                        callback();
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        if (xhr.responseText && xhr.responseText[0] == "{")
                            err = JSON.parse(xhr.responseText).Message;
                        console.log(err);
                    }
                }).always(function (data) {
                    TGDDLIB.HideShowLoading(false);
                });
            } else {
                alert("This browser doesn't support HTML5 file uploads!"); 
            }
        }
        else {
            callback();
        }
        
    }

    var lstWorkExperience = [];    
    $('#btnAddWork').on('click', function () {
        if ($('#txtCompany').val() == '' | $('#txtPosition').val() == '' | $('#txtNote').val() == '') {
            alert('Xin vui lòng nhập đầy đủ thông tin QUÁ TRÌNH LÀM VIỆC');
            if ($('#txtCompany').val() == '')
                $('#txtCompany').focus();
            else if ($('#txtPosition').val() == '')
                $('#txtPosition').focus();
            else if ($('#txtNote').val() == '')
                $('#txtNote').focus();
            return false;
        }

        var objWorkExperience = new Object();
        objWorkExperience.Company = $('#txtCompany').val();
        objWorkExperience.Position = $('#txtPosition').val();
        objWorkExperience.Description = $('#txtNote').val();
        
        lstWorkExperience.push(objWorkExperience);

        var strTemp = '';
        $('#box-workexperience').html('');
        strTemp += '<table class="formcontrol" border="0">';
        $.each(lstWorkExperience, function (i, val) {            
            strTemp += '<tr>';
            strTemp += '<td style="width: 240px"><div><b>Công ty:</b></div></td>';
            strTemp += '<td><input type="text" class="textinput" placeholder="Công ty gần đây đã làm" style="width: 323px;" value="' + val.Company + '">';
            strTemp += '&nbsp;&nbsp;&nbsp;&nbsp;';
            strTemp += '<span>Vị trí: </span>';
            strTemp += '<input name="txtPosition" type="text"  class="textinput" placeholder="Vị trí" style="width: 300px;" value="' + val.Position + '">';
            strTemp += '</td></tr>';
            strTemp += '<tr><td style="vertical-align: top">Mô tả chi tiết công việc đã làm:</td>';
            strTemp += '<td>';
            strTemp += '<textarea name="txtNote" rows="6" class="textinput textarea" style="width: 96%" >' + val.Description + '</textarea>';
            strTemp += '</td></tr>';
        });
        strTemp += '</table>';
        $('#box-workexperience').html(strTemp);

        // Clear text
        $('#txtCompany').val('');
        $('#txtPosition').val('');
        $('#txtNote').val('Công việc cụ thể: \nThời gian làm việc: \nNguyên nhân nghỉ việc: \nThông tin người tham khảo:');
    });

    var maxWidth = 600;
    var maxHeight = 600;

   
   
    if (canvas != null) {
        ctx = canvas.getContext('2d');
        var canvasHide = document.getElementById('imgdisplayHide');
        var ctxHide = canvasHide.getContext('2d');
        image = ctx.getImageData(0, 0, 320, 240);


        // Avatar
        var imageLoader = document.getElementById('fuAvatar');
        imageLoader.addEventListener('change', loadImage, false);

        // File Attachment
        // Upload file ma hoa binary (Tam thoi khong dung)
        //var fileAttachLoader = document.getElementById('fuFileAttach');
        //fileAttachLoader.addEventListener('change', loadFileAttach, false);
    }
   
    function loadImage(e) {
        var reader = new FileReader();

        /*Xóa dữ liệu của hình ảnh cũ*/
        ctx.clearRect(0, 0, canvas.width, canvas.height);
        ctxHide.clearRect(0, 0, canvasHide.width, canvasHide.height);

        reader.onload = function (event) {

            img = new Image();
            img.onload = function () {
                var ratio = 1;

                if (img.width > maxWidth)
                    ratio = maxWidth / img.width;
                else if (img.height > maxHeight)
                    ratio = maxHeight / img.height;

                var width = img.width * ratio;
                var height = img.height * ratio;

                var ratioShow = 1;
                if (img.width > 200)
                    ratioShow = 200 / img.width;
                else if (img.height > 200)
                    ratioShow = 200 / img.height;

                var widthShow = img.width * ratioShow;
                var heightShow = img.height * ratioShow;

                $('#imgdisplay').attr("width", widthShow);
                $('#imgdisplay').attr("height", "170px");

                //var strShootingtime = getDateTime();

                ctx.drawImage(img, 0, 0, img.width, img.height, 0, 0, widthShow, heightShow);
                ctx.fillStyle = 'red';
                ctx.font = '10pt tahoma';
                ctx.lineWidth = 2;
                //ctx.fillText(strShootingtime, 20, 20);


                $('#imgdisplayHide').attr("width", width);
                $('#imgdisplayHide').attr("height", height);

                ctxHide.drawImage(img, 0, 0, img.width, img.height, 0, 0, width, height);
                ctxHide.fillStyle = 'red';
                ctxHide.font = '20pt tahoma';
                ctxHide.lineWidth = 2;
                //ctxHide.fillText(strShootingtime, 30, 30);
                $("#btnUpload").show();
                $("#imgdisplay").show();
            }
            img.src = event.target.result;
            $('#isUploadControl').val('1');
            //alert(event.target.result);
        }
        reader.readAsDataURL(e.target.files[0]);
        return false;
    }

    function loadFileAttach(e) {
        var reader = new FileReader();

        reader.onload = function (event) {            
            alert(event.target.result);
            $('#hiddenFileAttach').val(event.target.result);
        }
        reader.readAsDataURL(e.target.files[0]);
        return false;
    }

    $('#ddlResidentProvince').on('change', function () {
        var id = $(this).val();

        TGDDLIB.HideShowLoading(true);  
        setTimeout(function () {
            $.ajax({
                url: "/Common/GetDistrict",
                type: "GET",
                data: {
                    ProvinceID: id
                },
                success: function (data) {
                    if (data != null) {
                        $('#ddlResidentDistrict').html('');
                        $('#ddlResidentDistrict').append('<option value="0">Chọn Quận/Huyện</option>');
                        $.each(data, function (key, value) {
                            $('#ddlResidentDistrict').append('<option value="' + value.Id + '">' + value.Name + '</option>');
                        });
                    }
                },
                error: function () {
                    alert("Server error");
                }
            }).always(function (data) {
                TGDDLIB.HideShowLoading(false);
            });
        }, 1000);
    });
    
    $('#total-position-2').html($('#total-position').html());

    $('#spanResetCaptcha').on('click', function () {
        $('#resetCaptcha').click();
    });
    $("#txtNote").val("Công việc cụ thể: \nThời gian làm việc: \nNguyên nhân nghỉ việc: \nThông tin người tham khảo:")
})(jQuery);


function ParseArray() {
    var availableTags = jQuery('.hdfListWordPlace').val();
    var arr = Array();
    if (availableTags != undefined) {
        var tempArr = availableTags.split(',');
        for (i = 0; i < tempArr.length - 1; i++) {
            var obj = new Object();
            var tmp = tempArr[i].split('-');
            obj.value = tmp[0].trim();
            obj.label = tmp[1].trim();

            //arr.push(tempArr[i]);
            arr.push(obj);
        }
    }
    return arr;
}

jQuery(function () {
    // Kiem tra hidden field study schedule khi trinh duyet postback     

    var hdfScheduleMorning = jQuery('#hdfSchedule_Morning').val();
    var hdfScheduleAfternoon = jQuery('#hdfSchedule_Afternoon').val();
    var hdfScheduleEvening = jQuery('#hdfSchedule_Evening').val();
    if (hdfScheduleMorning != undefined && hdfScheduleMorning != "") {
        var arrTmp = hdfScheduleMorning.split(",");
        for (var i = 0; i < arrTmp.length; i++) {
            if (arrTmp[i] != '')
                jQuery('#divStudySchedule input[name=chk_group_morning][id=chk' + arrTmp[i] + ']').prop('checked', true);
        }
    }
    if (hdfScheduleAfternoon != undefined && hdfScheduleAfternoon != "") {
        var arrTmp = hdfScheduleAfternoon.split(",");
        for (var i = 0; i < arrTmp.length; i++) {
            if (arrTmp[i] != '')
                jQuery('#divStudySchedule input[name=chk_group_afternoon][id=chk' + arrTmp[i] + ']').prop('checked', true);
        }
    }
    if (hdfScheduleEvening != null && hdfScheduleEvening != "") {
        var arrTmp = hdfScheduleEvening.split(",");
        for (var i = 0; i < arrTmp.length; i++) {
            if (arrTmp[i] != '')
                jQuery('#divStudySchedule input[name=chk_group_evening][id=chk' + arrTmp[i] + ']').prop('checked', true);
        }
    }

    // Ngay bay dau lam viec khong duoc nho hon ngay hien tai
    jQuery(".txtBeginDate").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '1970:2030',
        dateFormat: 'dd/mm/yy',
        minDate: 0,
        defaultDate: null
    });

    // Nhung ngay nay khong duoc lon hon ngay hien tai
    var d = new Date();
    var n = d.getFullYear();
    jQuery(".txtBirthDay, .txtIDIssuedDate, .txtStartDate, .txtEndDate").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '1950:' + (n - 18),
        dateFormat: 'dd/mm/yy',
        maxDate: new Date(),
        defaultDate: '05/03/1995'
    });

    // Lay lich hoc
    jQuery('#divStudySchedule input').on('change', function () {
        var strTemp = '';

        // Sang
        jQuery('#divStudySchedule input[name=chk_group_morning]').each(function () {
            if (this.checked) {
                strTemp += jQuery(this).val() + ',';
            }
            jQuery('#hdfSchedule_Morning').val(strTemp);
        });

        // Chieu
        strTemp = '';
        jQuery('#divStudySchedule input[name=chk_group_afternoon]').each(function () {
            if (this.checked) {
                strTemp += jQuery(this).val() + ',';
            }
            jQuery('#hdfSchedule_Afternoon').val(strTemp);
        });

        // Toi
        strTemp = '';
        jQuery('#divStudySchedule input[name=chk_group_evening]').each(function () {
            if (this.checked) {
                strTemp += jQuery(this).val() + ',';
            }
            jQuery('#hdfSchedule_Evening').val(strTemp);
        });
    });

    // Control upload file
    jQuery('.marskupload').click(function () {
        jQuery('.controlupload').click();
    });
    //jQuery('.avatar-marsk').click(function () {
    //    jQuery('.avatar-control').click();
    //});

    // Close nhap vi tri tuyen dung            
    jQuery('.x-close').click(function () {
        jQuery('.txtRecruitmentPosition').hide();
        jQuery('.x-close').hide();
        jQuery('.ddlRecruitmentPosition').show();
        jQuery('.ddlRecruitmentPosition').val('');
    });
});

jQuery(document).on('click', function (e) {
    var target = jQuery(e.target);
    if (target.is('.txtWorkPlace ') || target.parent().parent().is('#divListProvince') || target.parent().parent().parent().is('#divListProvince')) {
        return true;
    }
    else {
        jQuery('#divListProvince').hide();
    }
});


// Lay lai datepicker control khi postback
function ReloadDatapicker() {
    jQuery(".txtStartDate, .txtEndDate").datepicker({
        changeMonth: true,
        changeYear: true,
        yearRange: '1970:2030',
        dateFormat: 'dd/mm/yy',
        maxDate: new Date(),
        defaultDate: null
    });
}

function OtherRecruitmentPosition(that) {
    //alert(that.val());
    if (that.val() == '99999') {
        jQuery('.txtRecruitmentPosition').show();
        jQuery('.x-close').show();
        that.hide();

        jQuery('.txtRecruitmentPosition').focus();
    }
    else {
        jQuery('.txtRecruitmentPosition').hide();
        jQuery('.x-close').hide();
        that.show();
    }
}

function CheckSave() {
    // Kiem tra nhap vi tri ung tuyen
    //if (jQuery('.txtRecruitmentPosition').css('display') == 'inline-block' && jQuery('.txtRecruitmentPosition').val() == '') {
    //    alert('Nhập vị trí tuyển dụng');
    //    jQuery('.txtRecruitmentPosition').focus();
    //    return false;
    //}

    jQuery('.btnCancel').hide();
    jQuery('.btnSave').hide();
    jQuery('.wait-submit').show();

    // Kiem tra validate
    setTimeout(function () {
        jQuery(".tryvalidate").each(function (index) {
            var visible = jQuery(this).css('visibility');
            if (visible == 'visible') {
                jQuery('html, body').animate({
                    scrollTop: jQuery(this).offset().top - 20
                }, 500);

                jQuery(this).focus();
                jQuery('.btnCancel').show();
                jQuery('.btnSave').show();
                jQuery('.wait-submit').hide();

                return false;
                //alert(index + ": " + jQuery(this).text());
            }
        });
    }, 1000);
}

// Box nguyen vong lam viec
// Click show list danh sach tinh thanh
function ReloadShowListProvince() {
    jQuery('.divListProvince').show();
}

// Check numberic CMND
function InputNumberOnly(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);

    if (key != 50 || key != 73) {

        var regex = /[0-9]/; //input only numbers and * character
        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
        }
    }
}