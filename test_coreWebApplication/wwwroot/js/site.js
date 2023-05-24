//for mobilenumber input validation
//preventing the user from entering any non number data
function FilterInput(event) {
    var keyCode = ('which' in event) ? event.which : event.keyCode;

    isNotWanted = (keyCode == 69 || keyCode == 101);
    return !isNotWanted;
};
function handlePaste(e) {
    var clipboardData, pastedData;

    // Get pasted data via clipboard API
    clipboardData = e.clipboardData || window.clipboardData;
    pastedData = clipboardData.getData('Text').toUpperCase();

    if (pastedData.indexOf('E') > -1) {
        //alert('found an E');
        e.stopPropagation();
        e.preventDefault();
    }
};

var showPasswordTimeout;

function showPassword() {
    var passwordInput = document.getElementById("password");
    passwordInput.type = "text";
    $("#togglePassword").addClass("bi-eye-slash").removeClass("bi-eye")
}

function hidePassword() {
    var passwordInput = document.getElementById("password");
    passwordInput.type = "password";
    $("#togglePassword").addClass("bi-eye").removeClass("bi-eye-slash")
}
function showConfirmPassword() {
    var passwordInput = document.getElementById("confirmpassword");
    $("#toggleConfirmPassword").addClass("bi-eye-slash").removeClass("bi-eye")
    passwordInput.type = "text";
}

function hideConfirmPassword() {
    var passwordInput = document.getElementById("confirmpassword");
    passwordInput.type = "password";
    $("#toggleConfirmPassword").addClass("bi-eye").removeClass("bi-eye-slash")
}

$(document).ready(function () {
    $('#Employees-table').DataTable({
        processing: true,
        serverSide: true,
        paging: true,
        "lengthMenu": [[10, 25, 50, 2147483647 ], [10, 25, 50, "All"]],
        "ordering": true,
        "searching": true,
        columnDefs: [
            { orderable: false, targets: [4,5,6] }
        ],
        dom: 'Blfrtip',
        buttons: [
            'csv'
        ],
        ajax: {
            url: '/Employee?handler=GetEmployees',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            type: 'POST',
            dataType: 'json'
        },
        "columns": [
            {"data":"employeeId","name": "EmployeeID"},
            { "data": "emailID", "name": "EmailID" },
            { "data": "employeeName","name" : "EmployeeName" },
            { "data": "address","name": "Address" },
            { "data": "mobileno","name" : "Mobileno" },
            { "data": "password", "name": "Password" },
            {
                "render": function (data, type, full) {
                    return "<a type='button' class='btn btn-primary' href='/AddEmployee/" + full.employeeId + "'>" +
                    "Update" +
                    "</a >" +
                        "<button type='button' class='btn btn-danger' data-bs-toggle='modal' data-bs-target='#deleteModal_" + full.employeeId + "'>" +
                            "Delete" +
                        "</button>" }
            },
        ]
    });
});

$(function () {
    $("#CountryId").on("change", function () {
        var CountryId = $(this).val();
        $("#CityId").empty();
        $("#CityId").append("<option value=''>Select City</option>");
        $.getJSON(`?handler=Cities&countryId=${CountryId}`, (data) => {
            $.each(data, function (i, item) {
                $("#CityId").append(`<option value="${item.cityId}">${item.cityName}</option>`);
            });
        });
    });
});