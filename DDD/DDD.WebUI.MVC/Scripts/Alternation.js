﻿$(document).ready(function () {
    $(".table-style tr").mouseover(function () {
        $(this).addClass("over");
    }).mouseout(function () {
        $(this).removeClass("over");
    })
    $(".table-style tr:even").addClass("alt");
});   
 