<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_messagelist.ascx.cs" Inherits="IntellidateUser.web.uc.uc_messagelist" %>


<div class="col-lg-9 rounded-corners RHS-background" style="width: 822px; min-height: 450px; margin-left: 10px;">
    <!--RHS Column-->
    <div class="row bottom-border-for-heading" style="padding-top: 10px; padding-bottom: 10px; background-color: #f1f5e0">
        <!--RHS main row-->
        <div class="col-lg-1">
            <input type="checkbox" class="my-checkbox">
        </div>
        <div class="col-lg-2" style="margin-top: 5px; color: #888;"><b>From </b></div>
        <div class="col-lg-2 col-xs-offset-5" style="margin-top: 5px; color: #888;"><b>Overall match</b> </div>
        <div class="col-lg-2" style="margin-top: 5px; color: #888;"><b>Date /Time </b></div>
    </div>
    <div class="row" style="padding-bottom: 5px;">
        <!--RHS content row-->
        <div class="col-lg-1">
            <input type="checkbox" class="my-checkbox" style="vertical-align: -30px;">
        </div>
        <div class="col-lg-2" style="margin: 10px 0;"> 
            <img src="images/image-mail.png" height="54" width="54" class="rounded-corners" />
        </div>
        <div class="col-lg-5" style="margin-top: 5px;">
            <span style="font-size: 17px; color: #217e7c; font-weight: bold;">Gwyneth Graham</span>
            <br>
            Message content goes here. Message content goes here. Message content goes here. 
        </div>
        <div class="col-lg-2" style="margin-top: 10px; color: #666;"><span class="percentage-title">72</span>% </div>
        <div class="col-lg-1" style="margin-top: 10px; color: #666;"><span style="vertical-align: -18px;">8:42am</span> </div>
        <div class="col-lg-1" style="margin-top: 10px;">
            <span style="vertical-align: -18px; color: #c02727;" class="glyphicon glyphicon-remove"></span>
        </div>

    </div>
</div>
