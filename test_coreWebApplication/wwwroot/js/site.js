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
$(document).bind("ajaxSend", function () {
    $('.loader').addClass('d-block').removeClass('d-none');
}).bind("ajaxComplete", function () {
    $('.loader').addClass('d-none').removeClass('d-block');
});

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
        lengthMenu: [[10, 25, 50, 1000], [10, 25, 50, "All"]],
        ordering: true,
        searching: true,
        columnDefs: [
            { orderable: false, targets: [4, 5, 6] }
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
        columns: [
            { data: "employeeId", name: "EmployeeID", searchable: true, search: { search: "" } },
            { data: "emailID", name: "EmailID", searchable: true, search: { search: "" } },
            { data: "employeeName", name: "EmployeeName", searchable: true, search: { search: "" } },
            { data: "address", name: "Address", searchable: true, search: { search: "" } },
            { data: "mobileno", name: "Mobileno", searchable: true, search: { search: "" } },
            {
                data: "dateOfBirth", name: "DateOfBirth", searchable: false, search: { search: "" }, "render": function (data, type, full) {
                    var trimmedDate = data.substring(0, 10);
                    return trimmedDate;
                } },
            {
                render: function (data, type, full) {
                    return "<a type='button' class='btn btn-primary' href='/AddEmployee/" + full.employeeId + "'>" +
                        "Update" +
                        "</a >" +
                        "<button type='button' class='btn btn-danger' data-bs-toggle='modal' data-bs-target='#deleteModal_" + full.employeeId + "'>" +
                        "Delete" +
                        "</button>";
                },searchable : false
            },
        ],
        initComplete: function () {
            var api = this.api();

            // Add search input for each searchable column
            api.columns().every(function () {
                var column = this;

                if (column.settings()[0].aoColumns[column.index()].searchable) {
                    var input = $('<input type="text" class="form-control form-control-sm" placeholder="Search">')
                        .appendTo($(column.header()))
                        .on('click', function (e) {
                            e.stopPropagation(); // Prevent sorting when clicking on the search input
                        })
                        .on('keyup change clear', function () {
                            if (column.search() !== this.value) {
                                column.search(this.value).draw();
                            }
                        });

                    // Set the default search value
                    input.val(column.search());
                }
            });
        }
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