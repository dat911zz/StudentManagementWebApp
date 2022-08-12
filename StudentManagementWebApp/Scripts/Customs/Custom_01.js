
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
        $('#abortBtn').click(function (e) {
            e.preventDefault();
            console.log("abort!");
            $('#submitBtn').off();//Bắt buộc hủy event nếu không sẽ bị nạp chồng các môn với nhau khi bấm ok
            location.href = "#";
        });
    })
}
var inQ = function (id, subId, main) {
    console.log('InQueue: OK');
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
        $('#abortBtn').click(function (e) {
            e.preventDefault();
            console.log("abort!");
            $('#submitBtn').off();//Bắt buộc hủy event nếu không sẽ bị nạp chồng các môn với nhau khi bấm ok
            location.href = "#";
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
        $('#abortBtn').click(function (e) {
            e.preventDefault();
            console.log("abort!");
            $('#submitBtn').off();//Bắt buộc hủy event nếu không sẽ bị nạp chồng các môn với nhau khi bấm ok
            location.href = "#";
        });
    })
}
var commitDKHP = function (id, main) {
    var _url = '/Student/CommitDKHP/';
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
//ScoreUpdater
var confirmScoreUpdate = function (id) {
    var modalC = $("#confirmModal");
    modalC.modal("show");
    var optionSelected = $('#subjectList option:selected').text();
    var mh_id = $('#subjectList option:selected').val();
    var dqt = $('#dqt').val();
    var dtp = $('#dtp').val();

    if (dqt < 0 || dqt > 10) {
        $('#errDqt').text("Điểm vừa nhập không hợp lệ");
        modalC.modal("hide");
    }
    else {
        $('#errDqt').text("");
    }
    if (dtp < 0 || dtp > 10) {
        $('#errDtp').text("Điểm vừa nhập không hợp lệ");
        modalC.modal("hide");
    }
    else {
        $('#errDtp').text("");
    }

    $('#additionalInfo').text(" Cập nhật điểm môn " + optionSelected + "( Điểm quá trình: " + dqt + "; Điểm thành phần: " + dtp + " )");
    $('document').ready(function () {
        $('#submitBtn').click(function (e) {
            e.preventDefault();
            commitScoreUpdate(id, '/Student/ScoreUpdater', mh_id, dqt, dtp);
        });
        $('#abortBtn').click(function (e) {
            e.preventDefault();
            console.log("abort!");
            $('#submitBtn').off();//Bắt buộc hủy event nếu không sẽ bị nạp chồng các môn với nhau khi bấm ok
            location.href = "#";
        });
    })
}

var commitScoreUpdate = function (id, main, mh_id, dqt, dtp) {
    var _url = '/Student/ScoreUpdater/';
    $.ajax({
        type: "POST",
        url: _url,
        data: { id: id, mmh: mh_id, dqt: dqt, dtp: dtp },
        success: function (result) {
            console.log("Done");
            location.href = '' + main + '/' + id;
        },
        error: function () {
            console.log("Failed");
            alert("failed");
            location.href = '#';
        }
    })
}