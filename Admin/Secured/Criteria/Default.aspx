<%@ Page Title="" Language="C#" MasterPageFile="~/Secured/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdminModule.Secured.Criteria.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="objHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="objContentPlaceHolder" runat="server">
    <div style="height: 10px;"></div>
    <div style="display: block; float: left; width: 580px; min-height: 160px; max-height: 490px; overflow-y: auto; border: 1px solid #bbbbbb;">
        <div style="font-size: 24px; padding: 6px;" class="Transformer">Manage Criterias</div>
        <div style="padding: 6px;">
            <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Criteria Name :&nbsp;</div>
            <div style="float: left; width: 400px;">
                <input type="text" id="txtCriteriaName" style="width: 380px;" />
            </div>
        </div>
        <div style="clear: both;"></div>
        <div style="padding: 6px;">
            <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Criteria Type :&nbsp;</div>
            <div style="float: left; width: 400px;">
                <select id="cboCriteriaType" style="width: 384px;">
                    <option value="">Please Select</option>
                    <option value="1">Independent List</option>
                    <option value="2">Ordered List</option>
                    <option value="3">Range List</option> 
                </select>
            </div>
        </div>
        <div style="clear: both;"></div>
        <!-- 
        <div style="padding: 6px;">
            <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">User Question :&nbsp;</div>
            <div style="float: left; width: 400px;">
                <textarea id="txtCriteriaUserQuestion" style="width: 378px; height: 40px; font-family: Arial; resize: none;"></textarea>
            </div>
        </div>
        <div style="clear: both;"></div>
        -->
        <div style="padding: 6px;">
            <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Preference Question :&nbsp;</div>
            <div style="float: left; width: 400px;">
                <textarea id="txtCriteriaPreferenceQuestion" style="width: 378px; height: 40px; font-family: Arial; resize: none;">Answer(s) you accept.</textarea>
            </div>
        </div>
        <div style="clear: both;"></div>
        <section class="ListOfOptions displaynone">
            <div style="clear: both;"></div>
            <div style="padding: 6px;">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Options :&nbsp;</div>
                <div style="float: left; width: 400px;">
                    <div id="divTextAreaSection">
                        <textarea id="txtPasteOptions" placeholder="Paste the Options copied from Excel here" style="width: 378px; height: 18px; font-family: Arial; resize: none;"></textarea>
                    </div>
                    <div id="divOptionsSection" class="displaynone" data-bind="template: { name: 'template-options', foreach: AllOptions }"></div>
                    <div id="divAddMore">
                        <input type="button" value="Add New Option" id="btnAddMore" />
                    </div>
                </div>
            </div>
            <script type="text/html" id="template-options">
                <div style="display: block;">
                    <div style="float: left; width: 300px;">
                        <input type="text" data-bind="value: OptionText, valueUpdate: 'afterkeydown'" style="width: 290px;" />
                    </div>
                    <div style="float: left;">
                        <input type="button" value="Delete" style="width: 84px;" data-bind="event: { click: DeleteThis }" />
                    </div>
                </div>
                <div style="clear: both;"></div>
            </script>
            <div style="clear: both;"></div>
            <div style="padding: 6px;">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">&nbsp;</div>
                <div style="float: left; width: 400px;">
                    <input type="checkbox" id="chkIncludePreferAll" />
                    <label for="chkIncludePreferAll" style="cursor: pointer;">Include "All" preferences</label>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div style="padding: 6px;" class="displaynone" id="divIncludeAllText">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Include All Text :&nbsp;</div>
                <div style="float: left; width: 400px;">
                    <textarea id="txtIncludeAllText" style="width: 378px; height: 40px; font-family: Arial; resize: none;">Any of the above</textarea>
                </div>
            </div>
        </section>

        <section class="ListOfOrderedOptions displaynone">
            <div style="clear: both;"></div>
            <div style="padding: 6px;">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Options :&nbsp;</div>
                <div style="float: left; width: 400px;">
                    <div id="divTextAreaSection_2">
                        <textarea id="txtPasteOptions_2" placeholder="Paste the Options copied from Excel here" style="width: 378px; height: 18px; font-family: Arial; resize: none;"></textarea>
                    </div>
                    <div id="divOptionsSection_2" class="displaynone" data-bind="template: { name: 'template-options', foreach: AllOptions }"></div>
                    <div id="divAddMore_2">
                        <input type="button" value="Add New Option" id="btnAddMore_2" />
                    </div>
                </div>
            </div>
            <script type="text/html" id="template-options_2">
                <div style="display: block;">
                    <div style="float: left; width: 300px;">
                        <input type="text" data-bind="value: OptionText, valueUpdate: 'afterkeydown'" style="width: 290px;" />
                    </div>
                    <div style="float: left;">
                        <input type="button" value="Delete" style="width: 84px;" data-bind="event: { click: DeleteThis }" />
                    </div>
                </div>
                <div style="clear: both;"></div>
            </script>
            <div style="clear: both;"></div>
            <div style="padding: 6px;">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">&nbsp;</div>
                <div style="float: left; width: 400px;">
                    <input type="checkbox" id="chkIncludePreferAll_2" />
                    <label for="chkIncludePreferAll_2" style="cursor: pointer;">Include "All" preferences</label>
                </div>
            </div>
        </section>


        <section class="RangeOptions displaynone">
            <div style="clear: both;"></div>

            <div style="padding: 6px;">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">&nbsp;</div>
                <div style="float: left; width: 400px;">
                    <select id="cboRangeType">
                        <option value="1">Salary (Text boxes)</option>
                        <option value="2">Distance (Text boxes)</option>
                        <option value="3">Age (Dropdowns)</option>
                        <option value="4">Height (Dropdowns)</option>
                    </select>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div style="padding: 6px;">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Minimum Value&nbsp;:</div>
                <div style="float: left; width: 400px;">
                    <input type="text" id="txtMinValue" placeholder="1000" /><span class="units">&nbsp;$</span>
                </div>
            </div>
            <div style="clear: both;"></div>
            <div style="padding: 6px;">
                <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Maximum Value&nbsp;:</div>
                <div style="float: left; width: 400px;">
                    <input type="text" id="txtMaxValue" placeholder="1000" /><span class="units">&nbsp;$</span>
                </div>
            </div>

            <div style="clear: both;"></div>

        </section>

        <div style="clear: both;"></div>
        <div style="padding: 6px;">
            <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">Mismatch Text :&nbsp;</div>
            <div style="float: left; width: 400px;">
                <textarea id="txtMismatchText" style="width: 378px; height: 40px; font-family: Arial; resize: none;">Not your desired answer</textarea>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div style="padding: 6px;">
            <div style="float: left; width: 140px; padding-top: 4px; text-align: right;">&nbsp;</div>
            <div style="float: left; width: 400px;">
                <input type="checkbox" id="chkShowMatch" value="1" /><label for="chkShowMatch" style="cursor: pointer;">Show Match Value</label>
            </div>
        </div>

        <div style="clear: both;"></div>
        <div style="padding: 6px; color: red; height: 20px; text-align: center;" id="lblError">&nbsp;</div>
        <input type="hidden" id="hdnCriteriaID" />
        <div style="padding: 6px;">
            <div style="float: left;">
                <input type="button" id="btnReset" value="Reset" />
            </div>
            <div style="float: left;">
                <input type="button" id="btnCreate" value="Create Criteria" />
            </div>
        </div>
    </div>
    <div style="display: block; float: left; margin-left: 10px; width: 400px; min-height: 160px; max-height: 490px; overflow-y: auto; border: 1px solid #bbbbbb;">
        <div style="font-size: 24px; padding: 6px;" class="Transformer">Criteria Previews</div>
        <div style="padding: 10px;" id="divPreviews" data-bind="template: { name: 'template-criteria', foreach: AllCriterias }"></div>
    </div>
    <script type="text/html" id="template-criteria">

        <div style="border: 1px solid #d0d0d0;">
            <div style="position:absolute;margin-top:4px;margin-left:276px;">
                <div style="float:left;">
                    <input type="button" value="Edit" data-bind="event: { click: SetCriteriaForEdit }" />
                </div>
                <div style="float:left;">
                    <input type="button" value="Delete" />
                </div>
            </div>
            <div data-bind="text: CriteriaName" style="font-weight: bold;"></div>
            <div data-bind="text: OptionQuestion"></div>

            <!--ko ifnot: CriteriaType() == '3' -->
            <div>
                <!--ko foreach:PreferenceOptions-->
                <div>
                    <input type="radio" name="rdoOption" /><label data-bind="text: OptionText"></label>
                </div>
                <!--/ko-->
            </div>
            <!--/ko-->

            <!--ko if: CriteriaType() == '3' -->
            <div>
                <input type="text" />
            </div>
            <!--/ko-->

            <div data-bind="text: PreferenceQuestion"></div>
            <div>
                <!--ko ifnot: CriteriaType() == '3' -->
                <!--ko foreach:PreferenceOptions-->
                <div>
                    <input type="checkbox" name="chkPreference" /><label data-bind="text: OptionText"></label>
                </div>
                <!--/ko-->
                <!--ko if:IncludeAllInPreference-->
                <div>
                    <input type="checkbox" name="chkPreference" /><label data-bind="text: IncludeAllInPreferenceText"></label>
                </div>
                <!--/ko-->
                <!--/ko-->

                <!--ko if: CriteriaType() == '3' -->
                <input type="range" aria-multiselectable="true" />
                <!--/ko-->
            </div>
        </div>
        <div>&nbsp;</div>
    </script>
    <script src="Script.js" type="text/javascript"></script>
</asp:Content>
