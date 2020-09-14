$(document).ready(function () {
    $(":input[date-picker]").datepicker({
        dateFormat: "mm/dd/yyyy",
    });
    $(":input[datetime-picker]").datetimepicker({
        dateFormat: "yy/mm/dd",
        timeFormat: "HH:mm:ss"
    });
    $(":input[time-picker]").timepicker({
        timeFormat: "HH:mm:ss"
    });
})