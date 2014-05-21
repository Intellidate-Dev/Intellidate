<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_mailbox.ascx.cs" Inherits="IntellidateUser.web.uc.uc_mailbox" %>

<div class=" col-lg-3 rounded-corners nav" style="background-color: #217e7c; width: 230px;">
    <!--LHS Column-->
    <div>
        <ul class="nav" runat="server" id="nav">
            <li><a class="line-horizontal-LHS" href="Compose"><i class="glyphicon glyphicon-edit" style="opacity: 0.75;"></i>&nbsp; &nbsp; &nbsp; Compose</a></li>
            <li><a class="line-horizontal-LHS" href="Mailbox"><i class="glyphicon glyphicon-save" style="opacity: 0.75;"></i>&nbsp; &nbsp; &nbsp; Inbox</a></li>
            <li><a class="line-horizontal-LHS" href="Sent"><i class="glyphicon glyphicon-open" style="opacity: 0.75;"></i>&nbsp; &nbsp; &nbsp; Sent</a></li>
            <li><a class="line-horizontal-LHS" href="Trash"><i class="glyphicon glyphicon-trash" style="opacity: 0.75;"></i>&nbsp; &nbsp; &nbsp; Trash</a></li>
            <li><a class="line-horizontal-LHS" href="Filtered"><i class="glyphicon glyphicon-filter" style="opacity: 0.75;"></i>&nbsp; &nbsp; &nbsp; Filtered</a></li>
            <li><a href="InstantMessaging"><i class="glyphicon glyphicon-comment" style="opacity: 0.75;"></i>&nbsp; &nbsp; &nbsp; Instant Messaging</a></li>
        </ul>
    </div>
</div>
