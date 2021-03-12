var project = {
    urlDomain: "/project",
    init: function () {
        project.registerEven();
        project.GetListEmployee();
    },
    registerEven: function () {
        $("#add-employee").off("click").on("click", function () {
            project.openFormAddEmployee();
        })

        $('#modal-form22').find('#btnSave').off("click").on('click', function (e) {
            e.preventDefault();
            console.log("a")
            project.addEmployeeToProject();
        });
        $('.job-do').off('click').on('click', function () {
            var jobId = $(this).attr("data-id");
            $.ajax({
                url: project.urlDomain + '/Update?jobId=' + jobId,
                method: 'Get',
                success: function (result) {
                    showContentModal(result);
                }
            })
        });
        $('.job-InProgress').off('click').on('click', function () {
            var jobId = $(this).attr("data-id");
            $.ajax({
                url: project.urlDomain + '/UpdateJobInProgress?jobId=' + jobId,
                method: 'Get',
                success: function (result) {
                    showContentModal(result);
                }
            })
        });
        $('.job-done').off('click').on('click', function () {
            var jobId = $(this).attr("data-id");
            $.ajax({
                url: project.urlDomain + '/UpdateJobdone?jobId=' + jobId,
                method: 'Get',
                success: function (result) {
                    showContentModal(result);
                }
            })
        });
    },
    openFormAddEmployee: function () {
        $.ajax({
            url: project.urlDomain + "/AddEmployeetoProject",
            method: "get",
            success: function (result) {
                
                showContentModal22(result, "Thêm mới thành viên");
            }
        })
    },
    
    addEmployeeToProject: function () {
        var val = $("#cbEmployee").val();
        var model = [];
        if (val.length > 0) {
            $.each(val, function (i, item) {
                var a = {
                    EmployId: item,
                    ProjectId: $("#txtId").val()
                }
                model.push(a);
            })
        }
        var data = JSON.stringify(model);
        console.log(model)
        $.ajax({
            url: project.urlDomain + "/AddEmployeetoProject",
            method: "Post",
            contentType: 'application/json',
            data: data,
            success: function () {
                hideContentModal22();
            }
        })
    },
    GetListEmployee: function () {
        var ListEmployee = [];
        $.ajax({
            url: project.urlDomain + '/listEmployee?id=' + $("#txtId").val(),
            method: "GET",
            success: function (data) {
                ListEmployee = data;
            }
        }).then(function () {
            if (ListEmployee != null) {
                var html = '';
                $.each(ListEmployee, function (i, item) {
                    $.ajax({

                        url: project.urlDomain + "/Employee?id=" + item.Id,
                        async: false,
                        method: 'GET',
                        success: function (data) {
                            html += data
                        }
                    })
                });
                $("#list-employee").html(html)
            }
        })
    },
    
   
}