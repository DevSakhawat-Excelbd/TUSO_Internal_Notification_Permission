$(document).ready(function () {
   ReadApplicationPermissions();
   getRole();
   checkAll();
   createCheck();
   readCheck();
   editCheck();
   deleteCheck();
   moduleCheck();
});

function checkAll() {
   $("#selectAll").click(function () {
      $('.check').not(this).prop('checked', this.checked);
   });
   $(".check").click(function () {
      $("#selectAll").prop('checked', false)
   })
}

function createCheck() {
   $("#create").click(function () {
      $('.create').not(this).prop('checked', this.checked);
   })
   $(".create").click(function () {
      $("#create").prop('checked', false)
   })
}

function readCheck() {
   $("#read").click(function () {
      $('.read').not(this).prop('checked', this.checked);
   })
   $(".read").click(function () {
      $("#read").prop('checked', false)
   })
}

function editCheck() {
   $("#edit").click(function () {
      $('.edit').not(this).prop('checked', this.checked);
   })
   $(".edit").click(function () {
      $("#edit").prop('checked', false)
   })
}

function deleteCheck() {
   $("#delete").click(function () {
      $('.delete').not(this).prop('checked', this.checked);
   })
   $(".delete").click(function () {
      $("#delete").prop('checked', false)
   })
}

function moduleCheck(e) {
   var data = e.name;
   var create = ".row" + data;
   var findClass = e.classList[1];
   var classFind = "." + findClass;

   //$(classFind).not(this).click(function () {
   //   $(create).prop('checked', this.checked);
   //});
   //$(create).not(this).click(function () {
   //   $(classFind).prop('checked', false);
   //});

   if (e.checked == true) {
      $(create).prop("checked", true);
   }
   else {
      $(create).prop("checked", false);
   }

   $(create).click(function (x) {
      $(classFind).prop('checked', false);
      if (x.checked == true) {
         x.checked == false;
      }
      else {
         x.checked == true;
      }
      //if (x.checked == false) {
      //   x.checked == true;
      //}
   })
}

function getRole() {
   $('#role option').remove();
   $.ajax({
      url: "https://localhost:7026/tuso-api/user-roles",
      type: "GET",
      dataType: "json",
      contentType: 'application/json',
      success: function (res) {
         $('#role').append($('<option>').text('Select').attr({ 'value': '' }));
         $.each(res, function (index, v) {
            $('#role').append($('<option>').text(v.roleName).attr({ 'value': v.oid }));
         });
      },
      error: function (xhr) {
         console.log(xhr);
      }
   });
}

function ReadApplicationPermissions() {
   appPermissionData = [];
   $.ajax({
      url: "https://localhost:7026/tuso-api/application-permissions",
      type: "GET",
      dataType: "json",
      contentType: 'application/json',
      success: function (res) {
         for (var i = 0; i < res.length; i++) {
            var appPermission = { 'oid': res[i].oid, 'readPermission': res[i].readPermission, 'createPermission': res[i].createPermission, 'editPermission': res[i].editPermission, 'deletePermission': res[i].deletePermission, 'roleID': res[i].roleID, 'roleName': res[i].roleName, 'moduleID': res[i].moduleID, 'moduleName': res[i].moduleName, 'allData': res[i].allData };
            appPermissionData.push(appPermission);
         }

         var data = "";
         var trNo = 0;
         var no = 0;
         var id = 1;
         if (appPermissionData.length > 0) {
            for (var i = 0; i < appPermissionData.length; i++) {
               data += '<tr class="TrNo_' + trNo + '">'
               data += '<td class="Module_' + no + '"><input type="checkbox" onclick="moduleCheck(this)"  class="check" id="Module_' + no + '" ><span>' + appPermissionData[i].moduleName + '</span><span hidden id="ModuleID_' + no + '" >' + appPermissionData[i].moduleID + '</span><span hidden id="oid_' + no + '" >' + appPermissionData[i].oid + '</span></td>'

               if (appPermissionData[i].readPermission == true) {
                  data += '<td class="Read_' + no + '"><input type="checkbox" class="check read" id="Read_' + no + '" checked></td>'
               }
               else {
                  data += '<td class="Read_' + no + '"><input type="checkbox" class="check read" id="Read_' + no + '" ></td>'
               }

               if (appPermissionData[i].createPermission == true) {
                  data += '<td class="Create_' + no + '"><input type="checkbox" class="check create" id="Create_' + no + '" checked ></td>'
               }
               else {
                  data += '<td class="Create_' + no + '"><input type="checkbox" class="check create" id="Create_' + no + '" ></td>'
               }

               if (appPermissionData[i].editPermission == true) {
                  data += '<td class="Update_' + no + '"><input type="checkbox" class="check edit" id="Edit_' + no + '" checked ></td>'
               }
               else {
                  data += '<td class="Update_' + no + '"><input type="checkbox" class="check edit" id="Edit_' + no + '" ></td>'
               }

               if (appPermissionData[i].deletePermission == true) {
                  data += '<td class="Delete_' + no + '"><input type="checkbox" class="check delete" id="Delete_' + no + '" checked ></td></tr >'
               }
               else {
                  data += '<td class="Delete_' + no + '"><input type="checkbox" class="check delete" id="Delete_' + no + '" ></td></tr >'
               }

               trNo++;
               no++;
            }
         }
         $("#LoadAppPermission").html(data);
      },
      error: function (xhr) {
         console.log(xhr);
      }
   });
}

//function PostMultipleRow() {
//   var PostDataArray = [];
//   var trNo = $("table").find("tr").length;
//   for (var i = 2; i < trNo; i++) {
//      var readTrue, createTrue, editTrue, delTrue = false;

//      var moduleID = $("table").find("tr").eq(i).find("td").eq(0).find("span").eq(1).attr('id');
//      var read = $("table").find("tr").eq(i).find("td").eq(1).find("input").attr('id');
//      var create = $("table").find("tr").eq(i).find("td").eq(2).find("input").attr('id');
//      var edit = $("table").find("tr").eq(i).find("td").eq(3).find("input").attr('id');
//      var del = $("table").find("tr").eq(i).find("td").eq(4).find("input").attr('id');

//      ////for edit applicationpermission oid
//      var appOid = $("table").find("tr").eq(i).find("td").eq(0).find("span").eq(2).attr('id');
//      var appOidNo = $("#" + appOid).text();

//      console.log(appOidNo);

//      if ($("#" + read).prop("checked")) {
//         readTrue = true;
//      }
//      else {
//         readTrue = false;
//      }

//      if ($("#" + create).prop("checked")) {
//         createTrue = true;
//      }
//      else {
//         createTrue = false;
//      }

//      if ($("#" + edit).prop("checked")) {
//         editTrue = true;
//      }
//      else {
//         editTrue = false;
//      }

//      if ($("#" + del).prop("checked")) {
//         delTrue = true
//      }
//      else {
//         delTrue = false;
//      }

//      var roleValueId = $("#role").val();
//      //var roleValueText = $("#role").text();
//      var moduleIdNo = $("#" + moduleID).text();

//      var postData =
//      {
//         'oid': 0,
//         'moduleID': moduleIdNo,
//         'roleID': roleValueId,
//         'readPermission': readTrue,
//         'createPermission': createTrue,
//         'editPermission': editTrue,
//         'deletePermission': delTrue
//      };
//      PostDataArray.push(postData);
//   }

//   console.log(PostDataArray);
//   $.ajax({
//      url: "https://localhost:7026/tuso-api/application-permission",
//      type: "POST",
//      dataType: "json",
//      contentType: 'application/json',
//      /*data: JSON.stringify({ 'PostDataArray': PostDataArray }),*/
//      data: JSON.stringify(PostDataArray),
//      success: function (res) {
//         alert("success");
//         location.reload();
//      },
//      error: function (xhr) {
//         console.log(xhr); 
//      }
//   });
//}

function EditMultipleRow(postValue) {
   var ValueInteger = parseInt(postValue);
   var PostDataArray = [];
   var EditDataArray = [];
   var trNo = $("table").find("tr").length;
   for (var i = 2; i < trNo; i++) {
      var readTrue, createTrue, editTrue, delTrue = false;
      // module id
      var moduleID = $("table").find("tr").eq(i).find("td").eq(0).find("span").eq(1).attr('id');
      var moduleIdNo = $("#" + moduleID).text();

      var read = $("table").find("tr").eq(i).find("td").eq(1).find("input").attr('id');
      var create = $("table").find("tr").eq(i).find("td").eq(2).find("input").attr('id');
      var edit = $("table").find("tr").eq(i).find("td").eq(3).find("input").attr('id');
      var del = $("table").find("tr").eq(i).find("td").eq(4).find("input").attr('id');

      //for edit applicationpermission oid
      var appOid = $("table").find("tr").eq(i).find("td").eq(0).find("span").eq(2).attr('id');
      var appOidNo = $("#" + appOid).text();

      if ($("#" + read).prop("checked")) {
         readTrue = true;
      }
      else {
         readTrue = false;
      }

      if ($("#" + create).prop("checked")) {
         createTrue = true;
      }
      else {
         createTrue = false;
      }

      if ($("#" + edit).prop("checked")) {
         editTrue = true;
      }
      else {
         editTrue = false;
      }

      if ($("#" + del).prop("checked")) {
         delTrue = true
      }
      else {
         delTrue = false;
      }

      var roleId = $("#role").val();

      if (ValueInteger == 0) {
         var Data =
         {
            'oid': 0,
            'moduleID': moduleIdNo,
            'roleID': roleId,
            'readPermission': readTrue,
            'createPermission': createTrue,
            'editPermission': editTrue,
            'deletePermission': delTrue
         };
         PostDataArray.push(Data);
      }
      else {
         var Data =
         {
            'oid': appOidNo,
            'moduleID': moduleIdNo,
            'roleID': roleId,
            'readPermission': readTrue,
            'createPermission': createTrue,
            'editPermission': editTrue,
            'deletePermission': delTrue
         };
      }
      EditDataArray.push(Data);
   }

   if (ValueInteger == 0) {
      $.ajax({
         url: "https://localhost:7026/tuso-api/application-permission",
         type: "POST",
         dataType: "json",
         contentType: 'application/json',
         data: JSON.stringify(PostDataArray),
         success: function (res) {
            location.reload();
         },
         error: function (xhr) {
            console.log(xhr);
         }
      });
   }
   else {
      $.ajax({
         url: "https://localhost:7026/tuso-api/application-permission-update",
         type: "POST",
         dataType: "json",
         contentType: 'application/json',
         data: JSON.stringify(EditDataArray),
         success: function (res) {
            location.reload();
         },
         error: function (xhr) {
            console.log(xhr);
         }
      });
   }
}