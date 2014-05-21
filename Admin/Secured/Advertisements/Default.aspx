<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Advertisements.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
     <div style="float: left; display: block; width: 350px; min-height: 500px; border:1px solid; border-radius:5px;margin-top :8px;">
        <div><u><label style="font-size:large;font-variant: small-caps;padding:4px;">Post an advertisement</label></u></div> 
       <div  style="padding:8px;text-align:center;">
           <input type="text" id="txtCompanie" placeholder="Enter companie" class="NormalText"/>
       </div>
          <div  style="padding:8px;text-align:center;">
          <input type="text" id="txtAdStartDate" placeholder="Select advertisement start-date" class="NormalText"/>
       </div>
           <div  style="padding:8px;text-align:center;">
          <input type="text" id="txtEndtDate" placeholder="Select advertisement end-date" class="NormalText"/>
       </div>
           <div  style="padding:8px;text-align:center;">
          <input type="text" id="txtAdvertisementUrl" placeholder="Enter advertisement link url" class="NormalText"/>
       </div>
          <div  style="padding:8px;text-align:center;">
          <select class="NormalText">
              <option>Choose advertisement page </option>
              <option>Home page</option>
              <option>Photos page</option>
              <option>Videos page</option>
          </select>
       </div>
         <div  style="padding:8px;text-align:center;">
          <select class="NormalText">
              <option>Choose advertisement display position</option>
              <option>Right side</option>
              <option>Left side</option>
              <option>Top</option>
          </select>
       </div>
          <div  style="padding:8px;text-align:center;">
          <select class="NormalText">
              <option>Choose file type</option>
              <option>Video file</option>
              <option>Audio file</option>
              <option>Image file</option>
          </select>
       </div>
         <div  style="padding:8px;text-align:center;">
             <input type="file" class="NormalText" />
             </div>
    <div  style="padding:8px;text-align:center;">
        <input type="button" id="btnSubmit" value="Post advertisement" class="NormalText"/>
        </div>
    </div>
</asp:Content>
