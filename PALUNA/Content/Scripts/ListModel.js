var typeId = null;

$(document).ready(() => {
    var typeIdPos = window.location.href.toLocaleLowerCase().indexOf("typeid");
    if (typeIdPos > -1) {
        var queryStringSplit = window.location.href.toLocaleLowerCase().slice(typeIdPos + 6).split("&");
        if (queryStringSplit.length > 0) {
            typeId = Number(queryStringSplit[0].replace("=", ""));
        }
    }

    $("input[type='checkbox']").on("change",
        (e) => {
            var checkbox = $(e.target);
            var skip = Number(checkbox.attr("latestSkip"));
            if (checkbox.prop("checked") === true) {
                onFilterModel(checkbox.attr("modelId"), skip);
                skip += 4;
                checkbox.attr("latestSkip", skip);

            } else {
                skip = 0;
                checkbox.attr("latestSkip", 0);
                if ($("input[type='checkbox']:checked").length === 0) {

                    $("input[type='checkbox']").each((i, e) => {
                        onFilterModel($(e).attr("modelId"), skip, true);
                    });
                    showHideModelProduct(true);
                    return;
                }
            }
            showHideModelProduct(false);

        });
    var filterModelEl = $(".filter-model");
    if (filterModelEl && filterModelEl.length > 0) {
        filterModelEl.on("click", (e) => {
            onFilterModel($(e.target));
        })
    }
});

function buildProductTemplate(data) {
    if (!data || data.length < 1) return "";
    var rowModelProductTemplate = $("#row-model");
    var rowModelProductHtml = "";
    if (rowModelProductTemplate && rowModelProductTemplate.length > 0) {
        rowModelProductHtml = rowModelProductTemplate.html();

    } else {
        return "";
    }
    var productModelTemplateEl = $("#product-model");
    var productModelTemplate = "";
    if (productModelTemplateEl && productModelTemplateEl.length > 0) {
        productModelTemplate = $(productModelTemplateEl).html();
    } else {
        return "";
    }

    var listProductHtml = "";

    for (var i = 0; i < data.length; i++) {
        var product = data[i];
        var productModelHtml = productModelTemplate.replaceAll("{{productCode}}", product.Masp)
            .replaceAll("{{productImage}}", product.Anhbia)
            .replaceAll("{{productName}}", product.Tensp)
            .replaceAll("{{productPrice}}", product.Giatien);
        listProductHtml += productModelHtml;
    }

    rowModelProductHtml = rowModelProductHtml.replace("{{listProduct}}", listProductHtml);
    return listProductHtml;
}

function appendAllData(rowId, data, isShowAll) {
    var currentModelHtml = $(`.list-product[modelId=${rowId}]`);
    if (currentModelHtml && currentModelHtml.length > 0) {
        var allProduct = buildProductTemplate(data);
        if (isShowAll) {
            $(currentModelHtml).empty();
        }
        $(currentModelHtml).append(allProduct);
    }

}
function showHideModelProduct(isShowAll) {
    var listModelRows = $("div.row");
    if (!listModelRows || listModelRows.length <= 0) {
        return;
    }
    $("input[type='checkbox']").each((i, e) => {
        var modelId = $(e).attr("modelId");
        if ($(e).prop("checked") === false && !isShowAll) {
            $(`div.row[modelId=${modelId}] .list-product`).empty()
            $(`div.row[modelId=${modelId}]`).hide();
            $(`input[modelId=${modelId}]`).attr("latestSkip", 0);
        } else {
            $(`div.row[modelId=${modelId}]`).show();
        }
    })
}
function onFilterModel(filterModelId, skip, isShowAll) {
    {

        var filterData = { typeId: typeId, modelId: filterModelId, skip: skip, take: (isShowAll ? 4 : null) };
        $.ajax({
            url: "sanpham/filter",
            type: "post",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(filterData),
            success: function (data) {
                if (data) {
                    var result = $.parseJSON(data);
                    $(`input[modelId=${filterModelId}]`).attr("latestSkip", skip + result.length);
                    appendAllData(filterModelId, result, isShowAll);
                }
            }
        });
    }
}
