<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_whosavedme.ascx.cs" Inherits="IntellidateUser.uc.uc_whosavedme" %>
<link href="css/bootstrap.css" type="text/css" rel="stylesheet">
<link href="css/bootstrap-theme.css" type="text/css" rel="stylesheet">
<link href="css/myStyle.css" type="text/css" rel="stylesheet">
<link href='http://fonts.googleapis.com/css?family=Lobster' rel='stylesheet' type='text/css'>
<link href='http://fonts.googleapis.com/css?family=Open+Sans:300' rel='stylesheet' type='text/css'>
<script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
<div class="col-lg-9 rounded-corners RHS-background" style="width: 822px; min-height: 450px; margin-left: 10px;">
    <!--RHS Column-->
    <div class="row bottom-border-for-heading" style="padding-top: 10px; padding-bottom: 10px; background-color: #f1f5e0">
        <!--RHS main row-->

        <div class="col-lg-9" style="margin-top: 5px; color: #888;">
            <b>
                <label id="countsaved"></label>
                <% 
                    if (this.Name == "WhoViewed")
                    {  %> users have Viewed my profile <%}
                                                                           else if (this.Name == "WhoSaved")
                                                                           { %>users have Saved my profile
            <%}
                                                                           else
                                                                           { %>
                 profile has been saved by you
                <%} %>
            </b>
        </div>
        <div class="col-lg-3" style="margin-top: 5px; color: #888;"><b>Saved Date /Time </b></div>
    </div>
    <asp:DataList ID="lstSavedProfile" runat="server">
        <ItemTemplate>
            <!-- End RHS main row-->
            <div class="row" style="padding-bottom: 5px;">
                <!--RHS content row-->
                <div class="col-lg-1" style="margin: 10px 0;">
                    <img src="../images/image-mail.png" height="54" width="54" class="rounded-corners" />
                </div>
                <div class="col-lg-8" style="margin-top: 5px;">
                    <span style="font-size: 17px; color: #217e7c; font-weight: bold;" id="FullName"><%# Eval("FullName") %><% 
                                                                                                                               if (this.Name == "WhoViewed")
                                                                                                                               {  %>
                        <span style="color: #217e7c; font-size: 12px; font-weight: 700; margin-left: 10px;">x viewed: </span><span style="color: #8F8F8F; font-size: 12px; font-weight: 600;"><%# Eval("ViewedTimes") %></span></span>
                    <%} %></span>
                    <br>


                    <%# Eval("Age") %>&nbsp;,&nbsp;<%# Eval("Height") %>&nbsp;Cms&nbsp;,&nbsp;<%# Eval("Religion") %>&nbsp;,&nbsp;<%# Eval("Location") %>

                    <br>
                    <span style="color: #217e7c; font-size: 12px; font-weight: 700;">Match percentage:</span> <span style="color: #A82729; font-size: 18px; font-weight: 600;"><span id="MatchPercentage"><%# Eval("MatchPercentage") %></span>%&nbsp; Compatible</span>
                    <span class="glyphicon glyphicon-heart" style="color: #217e7c; margin-left: 40px;"></span>
                    <span style="color: #217e7c; font-size: 12px; font-weight: 700; margin-left: 45px;">Last Seen:</span><span style="color: #8F8F8F; font-size: 12px; font-weight: 600;"><time class="timeago" title='<%# Eval("LastOnlineTime") %>'></time></span>
                    <input type="hidden" id="hdncount" value="'<%# Eval("CountOfSaved") %>'" />
                </div>
                <div class="col-lg-2" style="margin-top: 10px; color: #666;">
                    <span style="vertical-align: -18px;">
                        <time class="timeago" title='<%# Eval("SavedTime") %>'></time>
                    </span>
                </div>
                <div class="col-lg-1" style="margin-top: 10px;"><span style="vertical-align: -18px; color: #c02727;" class="glyphicon glyphicon-trash"></span></div>
            </div>
        </ItemTemplate>
    </asp:DataList>
    <div class="row">
        <!--RHS main seperator row-->
        <div class="col-lg-12 bottom-border-for-list"></div>
    </div>
    <!-- End RHS main seperator row-->
</div>
<!--end of RHS column-->
<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
<script src="../Scripts/jquery-ui-1.10.4.js"></script>
<script src="../Scripts/jquery-ui-1.10.4.min.js"></script>
<script src="../../js/jquery.timeago.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="js/bootstrap.min.js"></script>
<script src="http://knockoutjs.com/downloads/knockout-3.0.0.js"></script>
<script src="http://cdnjs.cloudflare.com/ajax/libs/knockout/3.1.0/knockout-min.js"></script>
<script src="../js/jsfun.js"></script>
<script src="../js/jsconst.js"></script>

