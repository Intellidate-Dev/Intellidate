<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.UserMessages.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <script src="/Scripts/jquery.signalR-1.1.4.min.js"></script>
    <script src="/signalr/hubs"></script>
    <script type="text/html" id="MessagesView">
        <div style="float:left;width:98%;">
         <div style="float:left;width:50%;padding-left:10px;">
              <label><input type="checkbox" data-bind="checked: SelectAll"/>Select all </label>
         </div> 
         <div  style="float:left;width:48%;">
             <input type="button" id="btnSreject" style="float:right;" value="Reject selected" data-bind=" event: { click: RejectSelected }" /> 
         </div> 
       </div>

         <div style="height:500px;overflow-x:hidden;overflow-y:auto;float:left;" data-bind="foreach: AllMessages">
       
               <div style="float:left;width:700px;margin:8px;border:1px dotted #ccc;border-radius:6px;height:130px;padding:2px;">
        <div style="float:left;width:100%;">  
             <div style="float:left;"> <label><input type="checkbox" data-bind="checked: Selected" /> </label></div>  
            <div style="float:right;height:30px;">
                <input type="button" value="Reject this message" data-bind=" event: { click: RejectThis }" />
                <input type="button" value="View Reports" data-bind=" event: { click: open}"/>
           </div></div>
          <div style="height:95px;overflow-x:hidden;overflow-y:auto;padding:2px;border:1px solid #f6dba4;border-radius:6px;" data-bind="text: MessageText">
        
          </div>
           
       </div>    
            
             <div data-bind="dialog: { autoOpen: false, modal: true, title: 'Photo' }, dialogVisible: isOpen">
                 <label data-bind="text: GetUserMessageReport"></label> 
             </div>
         
             </div>

    </script>

    <div style="width: 100%;">
        <div style="float: left; display: block; width: 300px; min-height: 565px; border: 1px solid #000; border-radius: 6px; margin-right: 2px; margin-top: 5px;">
            <div style="font-size: medium; font-weight: bold; padding: 4px; color: #888; font-variant: small-caps;">
                Manage Messages 
            </div>
            <div style="font-size: medium; font-weight: bold; padding: 4px; color: #888; font-variant: small-caps;">
                <div>
                    <input type="radio" id="rdoMReported" name="rdoMessages" value="A" />
                    <label for="rdoMReported">This Mounth Reported Messages</label>
                    <label id="lblMReported" style="font-variant: normal; font-weight: normal; font-size: smaller;"></label>
                </div>

                <div>
                    <input type="radio" id="rdoWReported" name="rdoMessages" value="B" />
                    <label for="rdoWReported">This Week Reported Messages</label>
                    <label id="lblWReported" style="font-variant: normal; font-weight: normal; font-size: smaller;"></label>
                </div>
                <div>
                    <input type="radio" id="rdoDReported" name="rdoMessages" value="C" />
                    <label for="rdoDReported">Today Reported Messages</label>
                    <label id="lblDReported" style="font-variant: normal; font-weight: normal; font-size: smaller;"></label>
                </div>


                <div>
                    <label style="float: left; width: 100%;">
                        <input type="radio" id="rdoBReported" name="rdoMessages" value="D" style="float: left;" />
                        <span style="float: left; width: 250px;">Get Reported Messages Between Two dates</span></label>

                </div>
                <div id="dvTwodates" style="padding: 5px;">
                    <input type="text" id="txtFrom" placeholder="Select from date" style="width: 100px; margin-left: 15px;" />-<input type="text" id="txtTo" style="width: 100px;" placeholder="Select to date" />
                    <input type="button" id="btnGo" value="Go" />
                </div>
                <div>
                    <input type="radio" id="rdoRejected" name="rdoMessages" value="E" />
                    <label for="rdoRejected">Rejected Messages</label>
                    <label id="lblRejected" style="font-variant: normal; font-weight: normal; font-size: smaller;"></label>
                </div>

            </div>
        </div>
        <div style="float: left; display: block; width: 730px; height: 565px; padding-left: 10px; border: 1px solid #000; border-radius: 6px; margin-top: 5px;">
            <div style="font-size: medium; font-weight: bold; padding: 4px; color: #888; font-variant: small-caps;">
                Messages List
            </div>
            <div id="imgPleaseWait" style="position: absolute;" class="displaynone">
                <img src="../../Images/wait.gif" /></div>
            <div id="dvKnockOut" data-bind="template: { name: 'MessagesView' }"></div>

        </div>
    </div>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">


        function ShowWait() {
            $("#imgPleaseWait").removeClass("displaynone");
        }

        function HideWait() {
            $("#imgPleaseWait").addClass("displaynone");
        }

        //Knockout model
        function MessagesVM(_obj) {
            var _self = this;
            _self._id = ko.observable(_obj._id);
            _self.MessageRefID = ko.observable(_obj.MessageRefID);
            _self.SenderRefID = ko.observable(_obj.SenderRefID);
            _self.Sender = ko.observable(_obj.Sender);
            _self.RecipientRefID = ko.observable(_obj.RecipientRefID);
            _self.Recipient = ko.observable(_obj.Recipient);
            _self.MessageText = ko.observable(_obj.MessageText);
            _self.SentTime = ko.observable(_obj.SentTime);
            _self.IsMessageViewed = ko.observable(_obj.IsMessageViewed);
            _self.MessageViewedTime = ko.observable(_obj.MessageViewedTime);
            _self.IsMessageEdited = ko.observable(_obj.IsMessageEdited);
            _self.MessageLastEditedTime = ko.observable(_obj.MessageLastEditedTime);
            _self.MessageDeletedBySender = ko.observable(_obj.MessageDeletedBySender);
            _self.MessageDeletedByRecipient = ko.observable(_obj.MessageDeletedByRecipient);
            _self.Status = ko.observable(_obj.Status);
            _self.GetUserMessageReport = ko.observableArray(_obj.GetUserMessageReport);
            _self.Selected = ko.observable(false);
            _self.isOpen = ko.observable(false);
        }


        //jquery popup with knockout binding
        ko.bindingHandlers.dialog = {
            init: function (element, valueAccessor, allBindingsAccessor) {
                var options = ko.utils.unwrapObservable(valueAccessor()) || {};
                //do in a setTimeout, so the applyBindings doesn't bind twice from element being copied and moved to bottom
                setTimeout(function () {
                    options.close = function () {
                        allBindingsAccessor().dialogVisible(false);
                    };

                    $(element).dialog(options);
                }, 0);

                //handle disposal (not strictly necessary in this scenario)
                ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                    $(element).dialog("destroy");
                });
            },
            update: function (element, valueAccessor, allBindingsAccessor) {
                var shouldBeOpen = ko.utils.unwrapObservable(allBindingsAccessor().dialogVisible),
                    $el = $(element),
                    dialog = $el.data("uiDialog") || $el.data("dialog");

                //don't call open/close before initilization
                if (dialog) {
                    $el.dialog(shouldBeOpen ? "open" : "close");
                }
            }
        };


        function ListMessagesVM(_list) {
            var self = this;
            self.AllMessages = ko.observableArray();
            for (var i = 0; i < _list.length; i++) {
                self.AllMessages.push(new MessagesVM(_list[i]));
            }

            SelectAll = ko.computed({
                read: function () {
                    var item = ko.utils.arrayFirst(self.AllMessages(), function (item) {
                        return !item.Selected();
                    });
                    return item == null;
                },
                write: function (value) {
                    ko.utils.arrayForEach(self.AllMessages(), function (item1) {
                        item1.Selected(value);
                    });
                }
            });

            RejectThis = function (data) {

            }
            ViewReports = function (data) {

            }
            RejectSelected = function () {

            }
            RemoveAndReBind = function (data) {
                self.AllMessages.removeAll();
                for (var i = 0; i < data.length; i++) {
                    self.AllMessages.push(new MessagesVM(data[i]));
                }
            }
            RemoveAll = function () {
                self.AllMessages.removeAll();
            }
        }

        $(document).ready(function () {
            $("#dvTwodates").hide();
            $('input:radio[name="rdoMessages"]').change(function () {
                //Requested
                if ($(this).val() == 'D') {
                    RemoveAll();
                    $("#dvTwodates").show();
                    $(function () {
                        $("#txtFrom").datepicker({
                            defaultDate: "+1w",
                            changeMonth: true,
                            changeYear: true,
                            numberOfMonths: 1,
                            onClose: function (selectedDate) {
                                $("#txtTo").datepicker("option", "minDate", selectedDate);
                            }
                        });
                        $("#txtTo").datepicker({
                            defaultDate: "+1w",
                            changeMonth: true,
                            changeYear: true,
                            numberOfMonths: 1,
                            onClose: function (selectedDate) {
                                $("#txtFrom").datepicker("option", "maxDate", selectedDate);
                            }
                        });
                    });
                }
                else {
                    $("#dvTwodates").hide();
                }
                if ($(this).val() == 'A') {
                    //bind this mounth reported messagea list
                    ShowWait();
                    $.postDATA("Service.ashx?f=GTMRM", "{}", function (_data) {
                        RemoveAndReBind(_data);
                        HideWait();
                    });
                }
                if ($(this).val() == 'B') {
                    //bind this week reported messagea list
                    ShowWait();
                    $.postDATA("Service.ashx?f=GTWRM", "{}", function (_data) {
                        RemoveAndReBind(_data);
                        HideWait();
                    });
                }
                if ($(this).val() == 'C') {
                    //bind today reported messagea list
                    ShowWait();
                    $.postDATA("Service.ashx?f=GTDRM", "{}", function (_data) {
                        RemoveAndReBind(_data);
                        HideWait();
                    });
                }
                if ($(this).val() == 'D') {
                  
                }
                if ($(this).val() == 'E') {
                    ShowWait();

                    $.postDATA("Service.ashx?f=GRMBTD", "{}", function (_data) {
                        RemoveAndReBind(_data);
                        HideWait();
                    });
                }
            });

            $("#btnGo").click(function () {
                var _DateObj = new Object();
                var _Todate = $("#txtTo").val();
                var _Fromdate = $("#txtFrom").val();
                _DateObj.ToDate = _Todate.split('/')[1] + "/" + _Todate.split('/')[0] + "/" + _Todate.split('/')[2];
                _DateObj.FromDate = _Fromdate.split('/')[1] + "/" + _Fromdate.split('/')[0] + "/" + _Fromdate.split('/')[2];
                ShowWait();
                $.postJSON("Service.ashx?f=GRMBTD", _DateObj, function (_data) {
                    RemoveAndReBind(_data);
                    HideWait();
                });
            });

            var _self = new Object();
            _self._id = "";
            _self.MessageRefID = "";
            _self.SenderRefID = "";
            _self.Sender = "";
            _self.RecipientRefID = "";
            _self.Recipient = "";
            _self.MessageText = "hello world";
            _self.SentTime = "";
            _self.IsMessageViewed = "";
            _self.MessageViewedTime = "";
            _self.IsMessageEdited = "";
            _self.RecipientRefID = "";
            _self.MessageLastEditedTime = "";
            _self.MessageDeletedBySender = "";
            _self.MessageDeletedByRecipient = "";
            _self.Status = "";
            _self.GetUserMessageReport = "";
            var _dummyArray = new Array();
            _dummyArray.push(_self);
            //bind ko 
            ko.applyBindings(new ListMessagesVM(_dummyArray), document.getElementById('dvKnockOut'));

        });

    </script>

</asp:Content>
