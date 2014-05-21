<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc _compose.ascx.cs" Inherits="IntellidateUser.web.uc.uc__compose" %>

<div id="dvMsgBox" style="margin-top: 8px; display: block; width: 645px;padding:4px; min-height: 150px;background:#fff; float: left; border: 1px solid; border-radius: 8px 6px; padding-left: 10px; margin-left: 15px;">
    <div style="font-size: 20px; padding: 8px;float:left;width:100%" class="Transformer">
      <div style="float:left;width:400px;"> Send a message</div> 
      <div id="dvClose" style="float:right;width:44px;">
          <img alt="close" id="objClose" src="../../images/close.png" />
      </div>
    </div>
    <div class="row">
                    <!--editor tabs-->
                    <div class="col-lg-12">
                        <a href="#" class="btn btn-default" id="aBold" style="height: 34px;"><b>B</b></a>
                        <a href="#" class="btn btn-default" id="aItalic" style="height: 34px;"><b><em>I</em></b></a>
                        <a href="#" class="btn btn-default" id="aUnderline" style="height: 34px;"><b>U</b></a>
                        <a href="#" class="btn btn-default" id="bColorP" style="height: 34px;">
                            <select id="colorselector">
                                <option value="#000000" data-color="#000000">block</option>
                                <option value="#FFFFFF" data-color="#FFFFFF">white </option>
                                <option value="#FFFF00" data-color="#FFFF00">yellow</option>
                                <option value="#FFD700" data-color="#FFD700">gold </option>
                                <option value="#FF6347" data-color="#FF6347">tomato </option>
                                <option value="#808080" data-color="#808080">gray </option>
                                <option value="#008000" data-color="#008000">green</option>
                                <option value="#006400" data-color="#006400">drakgreen</option>
                                <option value="#DAA520" data-color="#DAA520">goldenrod </option>
                                <option value="#FF0000" data-color="#FF0000">red</option>
                                <option value="#FF00FF" data-color="#FF00FF">fuchsia </option>
                                <option value="#B22222" data-color="#B22222">firebrick </option>
                            </select>
                        </a>
                       
                        <a class="btn btn-default" href="#" style="height: 34px;"> 
                        <select id="emotion-icons">
						<option value="images/emoticons/happy.png" data-color="#fff">happy</option>
						<option value="images/emoticons/angry.png" data-color="#fff">angry</option>
						<option value="images/emoticons/confused.png" data-color="#fff">confused</option>
						<option value="images/emoticons/cool.png" data-color="#fff">cool</option>
						<option value="images/emoticons/evil.png" data-color="#fff">evil</option>
						<option value="images/emoticons/grin.png" data-color="#fff">grin</option>
						<option value="images/emoticons/neutral.png" data-color="#fff">neutral</option>
						<option value="images/emoticons/sad.png" data-color="#fff">sad</option>
						<option value="images/emoticons/shocked.png" data-color="#fff">shocked</option>
						<option value="images/emoticons/smiley.png" data-color="#fff">smiley</option>
						<option value="images/emoticons/tongue.png" data-color="#fff">tongue</option>
						<option value="images/emoticons/wink.png" data-color="#fff">wink</option>
						<option value="images/emoticons/wondering.png" data-color="#fff">wondering</option>

                    </select> </a>
                    </div>

                </div>
                <!--End editor tabs-->
                <div class="row" style="margin-top: 10px;">
                    <!--Reply text area-->
                    <div class="col-lg-12">
                        <div id="dvContent" style="width: 620px; height: auto;padding:4px; min-height: 200px; resize: none; font: normal; font-weight: normal; border: 1px solid #ccc;border-radius:6px;" contenteditable="true">
                        </div>
                       

                    </div>
                </div>
                <!--End Reply text area-->
                <div class="row" style="padding-top: 10px; padding-bottom: 5px;">
                    <!--RHS main row-->
                    <div class="col-lg-1" style="margin-top: 5px; top: 0px; left: 0px; width: 90px;">
                        <input type="button" id="btnSend" class="btn btn-primary" value="Send" />
                    </div>
                    <div class="col-lg-1" style="margin-top: 5px; top: 0px; left: 0px; width: 98px;">
                        <input type="button" id="btnDiscard" class="btn btn-default" value="Discard" />
                    </div>
                      <div class="col-lg-1" style="margin-top: 8px; top: 0px; left: 0px; width: 440px;padding-left: 0px;padding-right: 0px;">
                       <div class="alert alert-danger alert-dismissable" id="dvMessage" style="margin-top: 0px; width:auto;padding-right:4px;padding-left:4px;height:30px;">                   
                           </div>    
                      </div>
                </div>
         


</div>

<div >


</div>