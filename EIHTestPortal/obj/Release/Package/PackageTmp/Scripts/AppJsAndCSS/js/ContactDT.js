
let delete_id = "";
function Contact() { }
$(document).ready(function () {
    $('#contact_tble').DataTable({
        "columns": [
            {},
            {},
            {},
            {},
            {
                "render": function (data, type, full, meta) {
                    if (data == 1)
                        return "Active";
                    return "Inactive";
                }},
            {
            }
        ]

    });
    
});


function edit_contact(id) {
    

    window.location = '/Contact/Edit' + '?strId='+$(id).attr("model-param");
}

function add_btn_clicked() {
    window.location = '/Contact/Edit';
}

function delete_btn_clicked(thisptr) {
    delete_id = $(thisptr).attr("model-param");
    
    $("#popupModalDelete").modal('show');
}

function yes_clicked() {
    
    sendToServer();
}
function get_formdata() {
    var t = new Contact();
    t.id = delete_id;
    return t;

}
function sendToServer() {
    

    var url = "/Contact/Delete";
    var t = get_formdata();
    

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