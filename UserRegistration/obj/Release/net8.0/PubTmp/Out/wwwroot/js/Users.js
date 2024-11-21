$(document).ready(function () {
    loadUserData();
});

function loadUserData() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url": '/UserManagement/GetTableData',
            "method": "GET",
            "datatype": "json",
            "async": true,
            "dataSrc": '',
        },
        "columns": [
            {
                "render": function (data, type, row) {
                    return `<input type="checkbox" value="${row.email}"/>`;
                }
            },
            {
                data: 'name',
                name: "Name",
                render: function (data, type, row) {
                    return `
                        <div>
                            <div style="font-weight: bold;">${data}</div>
                            
                            <span style="font-size: smaller; color: gray;font-style: italic;">${row.address}</span> 
                        </div>
                    `;
                }
            },
            { data: 'email', name: "Email" },
            {
                data: 'lastLogin',
                name: "LastLogin",
                render: function (data, type, row) {
                    var formattedDate = new Date(data);
                    return formattedDate.toLocaleString();
                }
            },
            {
                data: 'lockoutEnd',
                name: "Status",
                render: function (data, type, row) {
                    if (data && new Date(data) > new Date()) {
                        return '<span class="text-danger">Locked</span>';
                    } else {
                        return '<span class="text-success">Active</span>';
                    }
                }
            }
        ]
    });
}

function toggleSelectAll() {
    var isChecked = $('#select-all').prop('checked');
    $('#myTable tbody input[type="checkbox"]').prop('checked', isChecked);
}

$('#deleteBtn').on('click', function () {
    let val = [];

    $('#myTable tbody :checkbox:checked').each(function (i) {
        val[i] = $(this).val();
    });

    $.ajax({
        type: 'POST',
        url: '/usermanagement/DeleteUser',
        data: { 'email': val },
        success: function () {
            dataTable.ajax.reload();
        },
        error: function () {
            console.log('Error deleting users');
        }
    });
});

$('#BlockBtn').on('click', function () {
    let val = [];

    $('#myTable tbody :checkbox:checked').each(function (i) {
        val[i] = $(this).val();
    });

    $.ajax({
        type: 'POST',
        url: '/usermanagement/BlockUser',
        data: { 'email': val },
        success: function () {
            dataTable.ajax.reload();
        },
        error: function () {
            console.log('Error blocking users');
        }
    });
});

$('#UnBlockBtn').on('click', function () {
    let val = [];

    $('#myTable tbody :checkbox:checked').each(function (i) {
        val[i] = $(this).val();
    });

    $.ajax({
        type: 'POST',
        url: '/usermanagement/UnBlockUser',
        data: { 'email': val },
        success: function () {
            dataTable.ajax.reload();
        },
        error: function () {
            console.log('Error unblocking users');
        }
    });
});
