var rateColumnIndex = 0,
    templateColumnIndex = 1,
    barColumnIndex = 3;

function gridInit(s, e) {
    var batchApi = grid.batchEditApi;
    var visibleIndices = batchApi.GetRowVisibleIndices();

    for (var i = 0; i < visibleIndices.length; i++) {
        var visibleIndex = visibleIndices[i];
        var rateColumn = grid.GetColumn(rateColumnIndex);
        var barColumn = grid.GetColumn(barColumnIndex);
        var barRowValue = batchApi.GetCellValue(visibleIndex, barColumn);
        var rowValue = batchApi.GetCellValue(visibleIndex, rateColumn);
        SetColValue(visibleIndex, rateColumn, rowValue);
        SetBarColValue(visibleIndex, barColumn, barRowValue);
    }
}

function batchEndEdit(s, e) {
    var visibleIndex = e.visibleIndex;
    var rowValues = e.rowValues;
    var rateColumn = grid.GetColumn(rateColumnIndex);
    var barColumn = grid.GetColumn(barColumnIndex);
    var templateColumn = grid.GetColumn(templateColumnIndex);
    SetColValue(visibleIndex, rateColumn, rowValues[rateColumnIndex].value);
    SetBarColValue(visibleIndex, barColumn, rowValues[barColumnIndex].value);
    SetHtmlTemplateColumnValue(visibleIndex, templateColumn, rowValues[templateColumnIndex]);
}

function onBatchEditChangesCanceling(s, e) {
    var updValues = e.updatedValues;
    for (var rowIndex in updValues) {
        for (var columnIndex in updValues[rowIndex]) {
            var visibleIndex = parseInt(rowIndex);
            var columnId = parseInt(columnIndex);
            var initialVal = grid.batchEditApi.GetCellValue(visibleIndex, columnId, true);
            var rateColumn, barColumn, templateColumn;
            if (columnId == rateColumnIndex) {
                rateColumn = grid.GetColumn(rateColumnIndex);
                SetColValue(visibleIndex, rateColumn, initialVal);
            }
            else if (columnId == barColumnIndex) {
                barColumn = grid.GetColumn(barColumnIndex);
                SetBarColValue(visibleIndex, barColumn, initialVal);
            }
            else if (columnId == templateColumnIndex) {
                templateColumn = grid.GetColumn(templateColumnIndex);
                SetHtmlTemplateColumnValue(visibleIndex, templateColumn, initialVal);
            }
        }
    }
}

function GetControl(visibleIndex, column) {
    return ASPx.GetControlCollection().GetControlsByPredicate(function (c) {
        var parent = grid.batchEditApi.GetCellTextContainer(visibleIndex, column);
        return ASPx.GetIsParent(parent, c.GetMainElement());
    })[0];
}

function SetColValue(visibleIndex, column, rowValue) {
    var columnControl = GetControl(visibleIndex, column);
    columnControl && columnControl.SetValue(rowValue);
}

function SetBarColValue(visibleIndex, column, rowValue) {
    var columnControl = GetControl(visibleIndex, column);
    columnControl && columnControl.SetPosition(rowValue);
}

function SetHtmlTemplateColumnValue(visibleIndex, column, rowValue) {
    if (ASPx.IsExists(rowValue)) {
        var templateCell = grid.batchEditApi.GetCellTextContainer(visibleIndex, column);
        if (rowValue.value)
            templateCell.firstElementChild.innerHTML = rowValue.value;
        else
            templateCell.firstElementChild.innerHTML = rowValue;
    }
}