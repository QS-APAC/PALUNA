
$(document).ready(() => {

});
var listValidate = ["required", "email", "phone"];
function initForm(formEl) {
    var requireError = `<div class="row error required">
                                <label class="col-lg-3" for="adr"> </label>
                                <span class="field-validation-error text-danger" >
                                    Bạn không được để trống trường này!
                                </span>
                            </div>`;
    var validError = `<div class="row error {{errClass}}">
                                <label class="col-lg-3" for="adr"> </label>
                                <span class="field-validation-error text-danger" >
                                    Bạn nhập không đúng định dạng!
                                </span>
                            </div>`;

    $(listValidate).each((i, v) => {
        var err = validError;
        if (v == "required") {
            err = requireError;
        } else if (v == "email") {
            err = err.replace("{{errClass}}", "email");
        } else {
            err = err.replace("{{errClass}}", "phone");
        }

        $(formEl).find(`input[validate*='${v}']`).each((j, el) => {
            var fieldArea = $(el).closest(".fieldArea");
            var html = fieldArea.html();
            fieldArea.empty();
            fieldArea.append(err + html);
        });
    });

}

function formValidation(formEl) {
    var result = true;
    $(formEl).find(`input[validate]`).each((j, el) => {
        var validate = $(el).attr("validate");
        var fieldArea = $(el).closest(".fieldArea");
        var inputVal = $(el).val();
        if (validate != null) {
            var listInputValidate = validate.split(",");
            var hasErr = false;
            $(listInputValidate).each((i, typeValid) => {
                if (!hasErr) {
                    var errEl = $(fieldArea).find(`.${typeValid}`);
                    if (!validateInput(inputVal, typeValid)) {
                        result = false;
                        if (errEl && errEl.length > 0) {
                            $(errEl).addClass("is-invalid");
                            hasErr = true;
                        }
                    } else {
                        if ($(errEl).hasClass("is-invalid")) {
                            $(errEl).removeClass("is-invalid");
                        }
                    }
                }

            });
        }
    });
    return result;
}
//$(formEl).find("input[validate*='email']").each((i, v) => {
//    var inputVal = $(v).val();
//    var fieldArea = $(v).closest(".fieldArea");
//    if (!fieldArea.hasClass("has-error")) {
//        $(fieldArea).addClass("has-error");
//    } else if (validateEmail(inputVal)) { }

//});
function validateInput(val, type) {
    switch (type) {
        case "required":
            return val != null && val != "";
        case "email":
            return validateEmail(val);
        case "phone":
            return validatePhone(val);
        default:
            return true;
    }
}
function validateEmail(email) {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}

function validatePhone(phone) {
    const re = /^(03|05|07|08|09)([0-9]{8})$/;
    return re.test(String(phone));
}
//function numberPhoneValidation(formEl) {
//    $(formEl).find("input[validate='numberphone']").each((i, v) => {


//    });
//}
//function requiredValidation(formEl) {
//    var result = true;
//    $(formEl).find("input[validate='required']").each((i, v) => {
//        var value = $(v).val();
//        var fieldArea = $(v).closest(".fieldArea");
//        if (value == null || value == "") {
//            result = false;
//            console.log("count");

//            if (!$(fieldArea).hasClass("has-error")) {
//                $(fieldArea).addClass("has-error");
//            }
//        } else {
//            if ($(fieldArea).hasClass("has-error")) {
//                $(fieldArea).remove("has-error")
//            }
//        }
//    });
//    return result;
//}