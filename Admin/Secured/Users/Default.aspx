<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Users.ManageUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">


    <link href="../../StyleSheet/UserCss.css" rel="stylesheet" />
    <script src="../../Scripts/knockout-3.0.0.debug.js"></script>
    <script src="../../Scripts/knockout.mapping-latest.js"></script>
    <script src="../../Scripts/jquery-1.9.1.js"></script>
    <script src="../../Scripts/jquery-2.1.0.min.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <link href="../../StyleSheet/jquery-ui.css" rel="stylesheet" />
    <link rel="stylesheet" href="/resources/demos/style.css">


    <script type="text/html" id="UserView">

        <div style="margin-left: auto; margin-right: auto;">
         
            <div style="padding: 4px; float: left;">
             <div style="float: left;"> 
                  <strong style="float: left; font-size: x-large; padding-top: 4px; color: #14493e;">Search By :</strong>

             </div> 
             <div style="width: 180px; height: 30px; float: left;">  <select style="padding: 2px; width: 180px; height: 30px; float: left;" id="ddlSearch" data-bind="event: { change: SearchRecentUsers }">
                    <option value="0">Choose..</option>
                    <option value="1">Month</option>
                    <option value="2">Week</option>
                    <option value="3">Day</option>
                </select>
             </div> 
            </div>
      
            <div style="padding: 4px; float: right;padding-right: 24px;">
             <div style=" width: 180px; height: 22px; float: left; ">  
                   <input type="text" style="padding: 2px; width: 180px; height: 22px; float: left; " id="txtSearchKey" placeholder="Search Key" />
              </div>
             <div style="height: 30px; width: 35px;float: left; ">
                    <input id="btnSearch" class="confirmButton" type="button" style="padding: 2px; height: 30px; width: 35px;float: left; " value="Go" data-bind="event: { click: SearchUsers }" />
                 </div>
             <div style="float: left;height: 30px; "> 
                    <input type="button" style="float: left;height: 30px; " class="confirmButton" data-bind="event: { click: AddNewUser }" value="Add New User" id="btnAddUser" />
                 </div>
            </div>

            <div>
            <table class="tableCss" style="display: none">
             
                <thead>
                    <tr>
                        <th style="width:40px;">S.No</th>
                        <th style="width:88px;">Login Name</th>
                        <th style="width:83px;">Full Name</th>
                        <th style="width:150px;">Email Address</th>
                        <th style="width:65px;">Gender</th>
                        <th style="width:110px;">Date Of Birth</th>
                        <th style="width:40px;">Age</th>
                        <th style="width:115px;">Created Date</th>
                        <th style="width:110px;">Last Online Time</th>
                        <th style="width:70px;">Edit</th>
                        <th style="width:145px;">Status</th>
                    </tr>
                </thead>

                <tbody data-bind="foreach: AllUsers, event: { scroll: scrolled }">
                    <tr>
                        <td style="width:40px;"><span style="float: left; margin-left: 4px;" data-bind="text: $index() + 1"></span></td>
                        <td style="width:80px;">
                            <div style="display: block; padding: 4px; float: left;">
                                <p style="float: left; margin-left: 4px" data-bind="text: LoginName"></p>
                                <input type="hidden" id="hdnuserid" data-bind="value: _RefID" />
                            </div>
                        </td>
                        <td style="width:80px;">
                            <p style="float: left;width:80px;" data-bind="text: FullName"></p>
                        </td>
                        <td style="width:150px;">
                            <s  style="float: left;width:150px;" data-bind="text: EmailAddress"></s>
                          
                        </td>
                        <td style="width:65px;">
                            <div style="float: left;width:65px;" data-bind="text: Gender"></div>
                        </td>
                        <td  style="width:110px;">
                            <div style="float: left;" data-bind="text: dob"></div>
                        </td>
                        <td style="width:40px;">
                            <div style="float: left;width:40px;" data-bind="text: UserAgeInYears"></div>
                        </td>
                        <td  style="width:110px;">
                            <div style="float: left;width:110px;" data-bind="text: userCreated "></div>
                        </td>
                        <td  style="width:110px;">
                            <div style="float: left;width:110px;" data-bind="text: LastOnline"></div>
                        </td>
                        <td style="width:70px;">
                            <input type="button" class="confirmButton" style="width:65px;" value="Edit" data-bind="event: { click: EditUser }" id="btnEdit" />
                        </td>
                        <td style="width:140px;">
                            <!--ko if: StatusCheck-->
                            <input type="button" class="confirmButton" style="width:140px;" data-bind="event: { click: DeleteUser }" value="Inactivate" />
                            <!--/ko-->
                            <!--ko ifnot: StatusCheck-->
                            <input type="button" class="confirmButton" style="width:140px;" data-bind="event: { click: ActivateUser }" value="Activate" />
                            <!--/ko-->

                            <!--ko if:ShowPopup-->
                            <div class="popup">

                                <div class="UserBox">

                                    <div class="UserTitle" style="height: 32px;">
                                        <span style="float: left;">Update User</span>
                                        <input type="button" value="X" style="font-family: Arial; float: right; height: 30px; width: 30px;" class="confirmButton" data-bind="event: { click: ClosePopup }" />
                                    </div>

                                    <div class="SmallGap">

                                    </div>

                                    <div style="margin: 0px auto; width: 300px; font-size: 15px;">
                                   
                                        <div style="padding: 0px;"><strong>Login Name:</strong>
                                            <input type="text" id="txtLogin" data-bind="value: LoginName" class="NormalText" />
                                        </div>
                                      
                                        <div style="padding: 0px;"><strong>Full Name:</strong>
                                            <input type="text" id="txtFullName"  data-bind="value: FullName" class="NormalText" />
                                        </div>
                                      
                                        <div style="padding: 0px;"><strong>Email Address:</strong><input type="text" id="txtEmail" data-bind="value: EmailAddress, event: { change: CheckEmailId }" class="NormalText" />
                                        <input type="hidden" data-bind="value: _RefID" id="hdnRefId" />
                                             </div>

                                        <div style="padding: 0px;"><strong>Password:</strong>
                                            <input type="text" id="txtPassword" data-bind="value: Password" class="NormalText" />
                                        </div>

                                        <div style="padding: 0px;"><strong>Gender:</strong>
                                            <select class="NormalText" id="ddlGender" data-bind="options: availableOptions, value: Gender" style="height: 28px; width: 268px;"></select>
                                        </div>

                                        <div style="padding: 0px;"><strong>Date Of Birth:</strong>
                                            <input type="text" id="txtDob" data-bind="value: dob, datepicker: dob, datepickerOptions: { maxDate: new Date(), changeMonth: true, changeYear: true }" class="NormalText" />
                                        </div>
                                        <div style="padding: 0px; margin-top: 5px;">
                                            <input type="button" id="cmdUpdateUser" style="height: 30px; width: 268px;" class="NormalText" value="Update User" data-bind="event: { click: UpdateDetails }" />
                                        </div>

                                        <div style="text-align: center; color: red; font-family: Arial; font-size: 14px;;height: 30px;" id="dvMsg">

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!--/ko-->
                        </td>
                    </tr>
                </tbody>

            </table>
            </div>

        </div>


    </script>

    <div id="dvUsers" data-bind="template: { name: 'UserView' }"></div>

    
    <script src="../../Scripts/Snippets.js"></script>
    <script src="Script.js"></script>
    

</asp:Content>
