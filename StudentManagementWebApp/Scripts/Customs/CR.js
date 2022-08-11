//InQueue
var confirmQ = function (subjectId) {
    console.log('InQueue');
    $('#confirmModal').modal('show');
    var id = $('#id_SV').text();
    $('#additionalInfo').text("");
    $('document').ready(function () {
        $('#submitBtn').click(function (e) {
            e.preventDefault();
            console.log(subjectId);
            inQ(id, subjectId, '/Student/DKHP');
        });
    })
}
var inQ = function (id, subId, main) {
    var _url = '/Student' + '/' + 'InQueue' + '/';//Locate action InQueue in Student controller
    $.ajax({
        type: "POST",
        url: _url,
        data: {id: id, subId: subId},//Post data into server (example: /Student/InQueue/SV001&MH01)
        success: function (result) {
            console.log("Done");
            location.href = '' + main + '/' + id;
        },
        error: function () {
            console.log("Failed");
            location.href = '' + main + '/' + id;
        }

    })
}
//DeQueue
var confirmDQ = function (subjectId) {
    console.log('DeQueue');
    $('#confirmModal').modal('show');
    var id = $('#id_SV').text();
    $('#additionalInfo').text("");
    $('document').ready(function () {
        $('#submitBtn').click(function (e) {
            e.preventDefault();
            console.log(subjectId);
            deQ(id, subjectId, '/Student/DKHP');
        });
    })
}
var deQ = function (id, subId, main) {
    var _url = '/Student' + '/' + 'DeQueue' + '/';//Locate action InQueue in Student controller
    $.ajax({
        type: "POST",
        url: _url,
        data: { id: id, subId: subId },//Post data into server (example: /Student/InQueue/SV001&MH01)
        success: function (result) {
            console.log("Done");
            location.href = '' + main + '/' + id;
        },
        error: function () {
            console.log("Failed");
            location.href = '' + main + '/' + id;
        }

    })
}
//Commit
var confirmDKHP = function (id) {
    $('#confirmModal').modal("show");
    var id = $('#id_SV').text();
    $('#additionalInfo').text("Hoàn tất Đăng Ký Học Phần cho sinh viên có mã " + id);
    $('document').ready(function () {
        $('#submitBtn').click(function (e) {
            e.preventDefault();
            commitDKHP(id, '/Student/DKHP');
        });
    })
}
var commitDKHP = function (id, main) {
    var _url = '/Student' + '/' + 'CommitDKHP' + '/';
    $.ajax({
        type: "POST",
        url: _url,
        data: { id: id },//Post data into server (example: /Student/CommitDKHP/SV001&MH01)
        success: function (result) {
            console.log("Done");
            location.href = '' + main + '/' + id;
        },
        error: function () {
            console.log("Failed");
            location.href = '' + main + '/' + id;
        }

    })
}