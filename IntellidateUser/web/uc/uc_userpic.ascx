<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_userpic.ascx.cs" Inherits="IntellidateUser.web.uc.uc_userpic" %>

<!-- Profile Pic Div Begin -->
<div style="width:340px;float:right;">
      <ul class="nav navbar-nav navbar-right" style="width:340px">
		<li><a href="#"><span class="badge-link-color">Notifications </span> <span class="badge my-badge"></span></a>
		</li>
		<li><a href="#"><span class="badge-link-color">Messages </span><span class="badge my-badge" id="spanMsg"></span></a></li>
        <li class="dropdown" style="margin-top:8px;">
          <span href="#" class="dropdown-toggle" data-toggle="dropdown" style="margin-right:0px;">
                <img src="../../images/defaultUser.gif" style="height:50px;width:50px;" />
          </span>
          <ul class="dropdown-menu">
            <li><a href="#">Profile</a></li>
              <li><a href="#">Change photo</a></li>
            <li><a href="#">Settings</a></li>
            <li class="divider"></li>
            <li><a href="LogOut">Logout</a></li>
          </ul>
        </li>
      </ul>
</div>



 <!-- Profile Pic Div End -->
