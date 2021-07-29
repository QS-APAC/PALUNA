initForm($("#userInfor"));
var listDetailItem = [];
initModal();
function initModal(parameters) {
    $("#thanhToan").on("click", () => {
        if (!formValidation($("#userInfor"))) {
            return;
        }
        var userInfor = getUserInfor();
        var postData = JSON.stringify(userInfor);
        $.ajax({
            url: "/GioHang/CartPayment",
            type: "post",
            data: {
                cartModel: {
                    CartUser: { Hoten: userInfor.Hoten, MaNguoiDung: userInfor.MaNguoiDung, Email: userInfor.Email, Dienthoai: userInfor.Dienthoai, Diachi: userInfor.Diachi },
                    ListItem: listDetailItem
                }

            },
            success: (data, textSts, xhr) => {
                if (xhr.status == 200) {
                    window.location.reload();
                } else {

                }
                console.log(data);
            },
            error: (xhr, textStatus, errorThrown) => {
                if (xhr.status == 401) {
                    var bool = confirm("Bạn chưa đăng nhập hoặc đã hết phiên làm việc! Hãy đăng nhập lại để tiếp tục.");
                    if (bool) {
                        window.location.href = `${window.location.origin}/User/Dangnhap`;
                    }
                    var closeButton = $("#close-modal-button")
                    if (closeButton && closeButton.length > 0) {
                        $(closeButton).click();
                    }
                }
            }
        })
    });
}
function getUserInfor() {
    var userInfor = {};
    $("#userInfor input").each((i, v) => {
        var attrName = $(v).attr("attrName");
        var value = $(v).val();
        if (attrName) {
            userInfor[attrName] = value || "";
        }
    });
    return userInfor;
}
function getBuyItem() {
    var isHasItem = false;
    var listItem = [];
    $(".divGioHang input[type='checkbox']:checked").each((i, e) => {
        isHasItem = true;
        var itemEl = $(e).closest("tr.detailItem");
        var item = {};
        $(itemEl).find("td").each((j, el) => {

            var attrName = $(el).attr("attrName");
            if (attrName) {
                var value = $(el).attr("value");
                item[attrName] = value || "";
            }

        })
        listItem.push(item);
        console.log(listItem);
    })
    return listItem;
}
$("#payment-button").on("click", () => {
    var listItem = getBuyItem();
    var listDetail = $("#listDetailArea");
    $(listDetail).empty();
    var templateDetail = '<p pId="{{Id}}"><a href="#">{{productName}}</a> <span class="price">{{Price}}</span></p>';
    var total = 0;
    if (listItem && listItem.length > 0) {
        listDetailItem = listItem;
        // buildModalTemplate();
        $(listItem).each((i, v) => {
            $(listDetail).append(templateDetail.replace("{{Id}}", v.iMasp).replace("{{productName}}", v.sTensp).replace("{{Price}}", v.ThanhTien));
            total += Number(v.ThanhTien);
        });
        $("#totalText").empty();
        var totalTemplate = `Total <span class="price" style="color:black"><b>{{Total}}</b></span>`;
        $("#totalText").append(totalTemplate.replace("{{Total}}", total));
        $("#showModal").click();
    } else {
        alert("Bạn chưa chọn món đồ nào?")
    }
});