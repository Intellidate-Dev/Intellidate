<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Messages.Default_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">

   
    <script src="../../Scripts/knockout-3.0.0.debug.js"></script>
    <script src="../../Scripts/knockout.mapping-latest.js"></script>
    <script src="../../Scripts/jquery-1.9.1.js"></script>
    <script src="../../Scripts/jquery-2.1.0.min.js"></script>
    <link href="../../StyleSheet/jquery-ui.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.10.2.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <link href="scripts/jquery-te-1.4.0.css" rel="stylesheet" />
    <script src="scripts/jquery-te-1.4.0.min.js"></script>
    <script src="/Scripts/jquery.signalR-1.1.4.min.js"></script>
    <script src="/signalr/hubs"></script>
    <link href="css/token-input.css" rel="stylesheet" />
    <script src="../../Scripts/jquery.tokeninput.js"></script>
    <script src="../../Scripts/jquery.elastic.js"></script>
    <script src="../../Scripts/jquery.events.input.js"></script>


    <div style="float: left;">
        <div style="float: left; display: block; height: 20px; width: 100%;"></div>

        <div style="float: left; display: block; width: 350px; min-height: 500px; border-bottom-style: solid; border-bottom-color: inherit; border-bottom-width: 0px;">
            <!--Send Message -->
            <div style="display: block; height: 10px;">&nbsp;</div>
            <div style="display: block; padding: 4px; font-size: 22px; color: #888;" class="Transformer">Send A Message</div>

            <div style="display: block; padding: 8px;">
                <div style="display: block; width: 350px; float: left;">
                    <div style="font-weight: bold;">Search Users: </div>
                    <div style="width: 330px; resize: none;">

                        <div>
                            <input type="radio" id="rdoAllUsers" name="rdomsg" value="A" />
                            <label for="rdoAllUsers">Send to all</label>
                            <input type="radio" id="rdoMulty" name="rdomsg" value="B" />
                            <label for="rdoMulty">Send specific users</label>
                            <input type="radio" id="rdoSearch" name="rdomsg" value="C" />
                            <label for="rdoSearch">Send search by</label>
                        </div>

                        <div id="dvText" style="height: 450px; overflow-y: auto; overflow-x: hidden;">
                            <input type="text" id="txtUser" />
                        </div>

                        <div id="dvSearch" style="border: 1px dotted #000; border-radius: 6px; height: auto; padding: 4px; margin: 0px; height: 450px; overflow-y: auto; overflow-x: hidden;">

                            <div>
                                <input type="checkbox" id="chkAge" value="Age" name="chkAgetype" />
                                <label for="chkAge">Age</label>
                            </div>

                            <div id="dvAge">
                                <div id="dvslider"></div>
                                Min  :
                                <label id="lblminAge"></label>
                                Years&nbsp; Max :
                                <label id="lblmaxAge"></label>
                                Years
                            </div>

                            <div>
                                <input type="checkbox" id="chkLocation" value="Location" name="Locationchk" />
                                <label for="chkLocation">Location</label><br />
                                <div id="dvLocation" data-bind="foreach: AllLocations" style="display: block; float: left; border: 1px solid #ccc">
                                    <div style="float: left; width: 150px;">
                                        <label>
                                            <input type="checkbox" name="LocationList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID, id: _RefID" /><span data-bind="    text: $data.LocationName"></span></label>
                                    </div>
                                </div>
                            </div>

                            <div>
                                <input type="checkbox" id="chkPremium" value="Premium users" name="PremiumList" />
                                <label for="chkPremium">Premium users</label>
                                <input type="checkbox" id="chkFree" value="Free users" name="FreeList" />
                                <label for="chkFree">Free users</label>
                            </div>

                            <div>
                                <input type="checkbox" id="chkEthnicity" value="Ethnicity" name="Ethnicitychk" />
                                <label for="chkEthnicity">Ethnicity</label>
                            </div>

                            <div id="dvEthnicity" data-bind="foreach: AllEthnicites" style="display: block; float: left; border: 1px solid #ccc">
                                <div style="float: left; width: 150px;">
                                    <label>
                                        <input type="checkbox" name="EthnicityList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.EthnicityName"></span></label>
                                </div>
                            </div>

                            <div>
                                <input type="checkbox" id="chkReligion" value="Religion" name="Religionchk" />
                                <label for="chkReligion">Religion</label>
                            </div>

                            <div id="dvReligion" data-bind="foreach: AllReligions" style="display: block; float: left; border: 1px solid #ccc">
                                <div style="float: left; width: 150px;">
                                    <label>
                                        <input type="checkbox" name="ReligionList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.ReligionType"></span></label>
                                </div>
                            </div>

                            <div>
                                <input type="checkbox" id="chkEducation" value="Education" name="Educationchk" />
                                <label for="chkEducation">Education</label>
                            </div>

                            <div id="dvEducation" data-bind="foreach: AllQualifications " style="display: block; float: left; border: 1px solid #ccc">
                                <div style="float: left; width: 150px;">
                                    <label>
                                        <input type="checkbox" name="EducationList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.Qualification"></span></label>
                                </div>
                            </div>

                            <div>
                                <input type="checkbox" id="chkHavechildren" value="Have children" name="Childrenchk" />
                                <label for="chkHavechildren">Have children</label>
                            </div>

                            <div id="dvHavechildren" data-bind="foreach: AllHaveChildrens " style="display: block; float: left; border: 1px solid #ccc">
                                <div style="float: left; width: 150px;">
                                    <label>
                                        <input type="checkbox" name="ChildrenList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.Description"></span></label>
                                </div>
                            </div>

                            <div>
                                <input type="checkbox" id="chkDrink" value="Drink" name="Drinkchk" />
                                <label for="chkDrink">Drink</label>
                            </div>

                            <div id="dvDrink" data-bind="foreach: AllDrinkTypes " style="display: block; float: left; border: 1px solid #ccc">
                                <div style="float: left; width: 150px;">
                                    <label>
                                        <input type="checkbox" name="DrinkList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.Description"></span></label>
                                </div>
                            </div>

                            <div>
                                <input type="checkbox" id="chkSmoke" value="Smoke" name="Smokechk" />
                                <label for="chkSmoke">Smoke</label>
                            </div>

                            <div id="dvSmoke" data-bind="foreach: AllSmokers" style="display: block; float: left; border: 1px solid #ccc">
                                <div style="float: left; width: 150px;">
                                    <label>
                                        <input type="checkbox" name="SmokeList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.SmokeDetails"></span></label>
                                </div>
                            </div>

                            <div>
                                <input type="checkbox" id="chkBodytype" value="Body type" name="Bodychk" />
                                <label for="chkBodytype">Body type</label>
                            </div>

                            <div id="dvBodytype" data-bind="foreach: AllBodyTypes " style="display: block; float: left; border: 1px solid #ccc">
                                <div style="float: left; width: 150px;">
                                    <label>
                                        <input type="checkbox" name="BodyList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.Description"></span></label>
                                </div>
                            </div>

                            <div style="float: left; padding-bottom: 5px;">
                                <div>
                                    <input type="checkbox" id="chkHoroscope" value="Horoscope" name="Horoscopechk" />
                                    <label for="chkHoroscope">Horoscope</label>
                                </div>
                                <div id="dvHoroscope" data-bind="foreach: AllHoroscope " style="display: block; float: left; border: 1px solid #ccc">
                                    <div style="float: left; width: 150px;">
                                        <label>
                                            <input type="checkbox" name="HoroscopeList" style="float: left;" data-bind="checkedValue: _RefID, value: _RefID" /><span data-bind="    text: $data.Description"></span></label>
                                    </div>
                                </div>
                            </div>

                            <div style="float: right; width: 100%; height: 40px;">
                                <input type="button" style="float: right;" id="btnGo" value="Go and search" /><br />
                            </div>

                            <br />
                        </div>

                    </div>
                </div>
            </div>

        </div>
     
        <div style="float: left; display: block; width: 680px; min-height: 500px; border-bottom-style: solid; border-bottom-color: inherit; border-bottom-width: 0px;">
             
            <div id="dvMsgBox" style="margin-top: 8px; display: block; width: 680px; min-height: 150px; float: left; border: 1px solid; border-radius: 8px 6px; padding-left: 10px; margin-left: 15px;">
                <div style="font-size: 20px; padding: 8px;" class="Transformer">New Message</div>
                <div id="dvUsersList" style="display: block; width: 650px; height: auto; border: 1px solid #ccc; margin-left: 10px; overflow-y: auto;">
                    <span style="float: left; font-weight: bold;">
                        <label id="lblUsers"></label>
                    </span>
                    <br />
                </div>
                <div style="padding: 8px;">
                    <span style="color: red; background: #f9edbe; font-size: large; width: auto;" class="Transformer">
                        <label id="lblMsg"></label>
                    </span>
                </div>
                <div style="padding: 8px;">
                    <input id="txtSubject" type="text" style="width: 650px;" placeholder="Subject" />
                </div>
                <div style="padding: 8px;">
                    <textarea id="txtMessage" style="width: 620px; font-family: Arial; resize: none;" class="jqte-test" placeholder="Enter your message"></textarea>
                </div>
                <div style="padding: 8px;">
                    <div style="float: left; width: 33%;">
                    </div>

                    <div style="float: left; width: 100%; text-align: right;">
                        <input type="button" id="btnSendMessage" value="Send Message" style="font-family: Arial; font-size: 14px; float: right; margin-right: 8px;" />
                        <input type="hidden" id="hdnUserIds" />
                    </div>
                </div>
                <div style="clear: both; height: 10px;"></div>

            </div>

            <div style="display: block;">&nbsp;</div>

        </div>

    </div>


    <div style="clear: both;"></div>
       <div id="imgPleaseWait" style="position:absolute;" class="displaynone"><img src="../../Images/wait.gif" /></div>
    <script src="../../Scripts/Snippets.js"></script>
    <script src="Script.js"></script>
    
</asp:Content>
