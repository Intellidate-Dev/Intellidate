<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Photos.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    
    
    <!--Jquery popup supported files -->
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="scripts/jquery-1.9.1.js"></script>
    <script src="scripts/jquery-ui.js"></script>
    <!--End Jquery popup-->
    <script src="/Scripts/jquery.signalR-1.1.4.min.js"></script>
    <script src="/signalr/hubs"></script>


    <script type="text/html" id="PhotoView">
        <div>       
     <div style="float:left;width:98%;padding-left:2px;padding-right:2px;"><div style="float:left; font-family:Arial;font-variant:normal;font-size:small;"><label> <input type="checkbox" data-bind="checked: SelectAll" /> Select all</label></div>
        <div style="float:right;width:auto;"> 
                <input type="button" id="btnApproveSelected" value="Approve selected" data-bind=" event: { click: ApproveSelected }"/> 
                <input type="button" id="btnRejectSelected" value="Reject selected" data-bind=" event: { click: RejectSelected }"/>
        </div></div>  
              <div data-bind=" foreach: AllPhotos">
        <div style="float: left; width: 100px; height: 120px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;">
         
             <div style="float:left;"> <input type="checkbox" data-bind="checked: Selected"/></div>
          <div style="width:100%;">
                <img alt="" style="float: left; width: 100px; height: 100px;" data-bind="attr: { src: AttachmentPath }, click: open" />
                </div>
            <div title="Photo"  data-bind="dialog: { autoOpen: false, modal: true, title: 'Photo' }, dialogVisible: isOpen" >
        <div style="float: left; width: 100%;">
            <!--ko if: isApproved-->
            <div style="float: left; padding-left: 5px">
                <input id="btnApprove" type="button" value="Approve" data-bind=" event: { click: ApprovePhoto }"/></div>
            <!--/ko-->
             <!--ko if: isRejected-->
            <div style="float: right; padding-right: 10px">
                <input type="button" value="Reject" data-bind=" event: { click: RejectPhoto }"/></div>
             <!--/ko-->
        </div>
        <br />
        <img  data-bind="attr: { src: AttachmentPath } " alt="" />
        </div>
      
        </div>
             </div>
            </div>
    </script>


    <div style="float: left;">
        <div style="float: left; display: block; height: 20px; width: 100%;"></div>
        <div style="float: left; display: block; width: 300px; min-height: 565px; border:1px solid #000;border-radius: 6px;margin-right:2px;">
            <!--Manage Photos -->
            <div style="display: block; height: 10px;">&nbsp;</div>
            <div style="display: block; padding: 4px; font-size: 22px; color: #888;" class="Transformer">Manage Photos</div>
            <div style="display: block; padding: 8px;">
                <div style="display: block; width: 350px; float: left;">
                    <div style="font-size:medium;font-weight:bold;padding:4px;color:#888;font-variant: small-caps;">Search Photos </div>
                    <div style="width: 330px; resize: none;">
                        <div >
                            <div  style="font-size:medium;font-weight:bold;padding:4px;color:#888;font-variant: small-caps;">
                            <input type="radio" id="rdoRequested" name="rdoPhotos" value="A" />
                            <label for="rdoRequested">Requested photos</label><label id="lblRequested" style="font-variant:normal;font-weight:normal;font-size:smaller;"></label>  <br />
                            <input type="radio" id="rdoReported" name="rdoPhotos" value="B" />
                            <label for="rdoReported">Reported photos</label><label id="lblReported" style="font-variant:normal;font-weight:normal;font-size:smaller;"></label> <br />
                            <input type="radio" id="rdoRejected" name="rdoPhotos" value="C" />
                            <label for="rdoRejected">Rejected photos</label><label id="lblRejected" style="font-variant:normal;font-weight:normal;font-size:smaller;"></label> <br />
                            <input type="radio" id="rdoPending" name="rdoPhotos" value="D" />
                            <label for="rdoPending">Pending photos</label><label id="lblPending" style="font-variant:normal;font-weight:normal;font-size:smaller;"></label> <br />
                            <input type="radio" id="rdoApproved" name="rdoPhotos" value="E" />
                            <label for="rdoApproved">Approved photos</label><label id="lblApproved" style="font-variant:normal;font-weight:normal;font-size:smaller;"></label> <br />
                            <input type="radio" id="rdoAllPhotos" name="rdoPhotos" value="F" />
                            <label for="rdoAllPhotos">Search by user</label><br />
                                <div id="dvSearch">
                                   <input type="text" id="txtUser" placeholder="Enter username" /><input type="button" id="btnGo" value="Search" />
                                </div>

                            </div>
                            <div style="font-size:medium;font-weight:bold;padding:4px;color:#888;font-variant: small-caps;">
                 <div>
                <div style="width: 300px;">
                       <span style="color: red; background: #f9edbe;font-size:large; width: auto;" class="Transformer">
                    <label id="lblMsg"></label>
</span>
                </div>
                     
                 <div id="dvOneImg" style="width:50%;height:auto;text-align:center;padding-left:80px;"> 
                      <div style="float: left; width: 100px; height: 100px; border: 1px solid #ccc; border-radius: 6px; margin-left: 2px; text-align: center; padding: 5px;">
            <img alt="" style="float: left; width: 100px; height: 100px;" id="imgLast" />          
        </div></div>   
 <div id="dvImgs" style="width:88%;overflow-y:auto;overflow-x:hidden;"> 


 </div>   

<label id="lblalready"></label>
  <div id="dvalreadyImgs" style="width:88%;overflow-y:auto;overflow-x:hidden;"> 


 </div>  
        </div>
                </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div style="float: left; display: block; width: 730px;height:auto; padding-left: 10px;border:1px solid #000;border-radius:6px;">
            <div style="display: block; padding: 4px; font-size: 22px; color: #888;"" class="Transformer">
                <span style="float: left; font-weight: bold;">Photos List </span>
                
                <div id="imgPleaseWait" style="position:absolute;" class="displaynone"><img src="../../Images/wait.gif" /></div>
                <div id="dvPhotosList" style="display: block; width: 700px; height: 520px;margin-bottom:8px; float: left; border: 1px solid #ccc; border-radius: 6px; overflow-y: auto; margin-top: 4px; padding: 4px;" data-bind="template: { name: 'PhotoView' }">

                </div>              
            </div>
        </div>


    </div>
   

    <script src="Script.js"></script>

    <script src="../../Scripts/Snippets.js"></script>

</asp:Content>
