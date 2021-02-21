function Contact() { }

$(document).ready(function () {
    disable_back_btn();
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
});

function back_clicked() {
    
 
}

function yes_clicked() {
    
    window.location = "/Contact/Index";
}

function save_clicked() {
    

    if (validateData()) {
        send_data();
    }

}

function get_formdata() {
    var t = new Contact();

   
    t.id = $("#idstr").val();
    t.firstName = $("#firstName").val();;
    t.lastName = $("#lastName").val();;
    t.PhoneNumber = $("#phonenumber").val();;
    t.Email = $("#email").val();;
    t.Status = $("#status").val();

    console.log(t);
    return t;

}

function send_data() {
    var t = get_formdata();
    var url = "/Contact/Save"

    $.ajax({
        url: url,
        type: 'POST',
        data: JSON.stringify(t),
        contentType: "application/json; charset=utf-8", 
        datatype: 'json',
        success: function (data) {
            
            if (data.result) {
                //show popup and 

                $("#popupModalSucess").modal('show');
            }
            else {

                $("#errorTxt").text("Error! something went wrong, Please try later..");
                //show error message here
                if (t.id === "") {
                    
                    $("#errorTxt").text("Resource is already exists!");
                }
                $("#popupModalError").modal('show');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.error(textStatus);
        },
        complete: function () {
        }
    });
}

$(document).on({
    ajaxStart: function () {
        
        $("body").addClass("loading");
        $('#overlay').show();
    },
    ajaxStop: function () {
        $("body").removeClass("loading");
        $('#overlay').hide();
    }
});

function validateData() {
    if ($("#email").val() == "") {
        toastr.error('Please enter email address, It is required field')
        return false;
    }

    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if ($("#email").val().match(validRegex)) {
        return true;
    }
    else {
        toastr.error('Please enter valid email address');
        return false;
    }
}
function enable_back_btn() {
    console.log("enable backbtn");
    $("#back_btn").removeAttr("disabled");
}

function disable_back_btn() {
    $("#back_btn").attr("disabled", true);
    
}