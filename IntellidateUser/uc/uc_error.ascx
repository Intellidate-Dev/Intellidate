<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uc_error.ascx.cs" Inherits="IntellidateUser.uc.uc_error" %>
<div class="container">
    
    	<div class="row"> <!--formContainer row-->
		<div class="col-lg-8  col-xs-offset-2 bottom-rounded-corners top-rounded-corners"  style="background-color:#e43f18; height:auto; min-height:100px; margin-top:75px; padding-top:15px; "> <!--Parent column-->

        	<div class="row"> <!--nested row for forgot password-->
                <div class="col-lg-3 " style="padding:10 40 10 40;">
                    <span class="glyphicon glyphicon-warning-sign glyphicon-oops"></span>
                </div>
                <div class="col-lg-9 " style="padding:10 40 10 40;">
                    <h1 style="color:white;">404 - Ooops, page does not exist <h1> <h3 style="color:white;">We agree its weird to be on this page...</h3>
                </div>
                
            </div> <!--end of first row-->

		</div> <!--end of Parent column-->
        
      </div> <!--end of form container row-->
      
    	<div class="row" > <!--back journey msg-->
            <div class="col-lg-4" style="margin-top:10px;"><h5 style="text-align:right;"><a href="#" style="color:#032e86;"><< Return to previous page</a></h5></div>			
            <div class="col-lg-6" style="margin-top:10px;"><h4 style="color:#707070; font-size:22px; text-align:center;">OR choose any of the following to get back in action.</h4></div>

		</div> <!--end of back journey msg-->      


		<div class="row"> <!--formContainer row-->
			<div class="col-lg-8  col-xs-offset-2 bottom-rounded-corners top-rounded-corners"  style="background-color:#eaeff3; height:auto; min-height:100px; margin-top:10px; padding-top:15px; "> <!--Parent column-->
            	<form role="form">
 
                <div class="row"> <!--nested row for submit button-->
                	
                	<div class="col-lg-4 " style="margin-top:15px;">
					<button type="button" class="btn btn-default" style="width:225px; letter-spacing:1.2px; "> <span class="glyphicon glyphicon-log-in"></span> <span class="oops-button">Login in Now </span></button>
                    </div>

                    <div class="col-lg-4 " style="margin-top:15px;">
	                <button type="button" class="btn btn-default" style="width:225px; letter-spacing:1.2px; "> <span class="glyphicon glyphicon-search"> </span> <span class="oops-button">Search Profiles </span></button>
                    </div>
                    
                	<div class="col-lg-4 " style="margin-top:15px;">
					<button type="button" class="btn btn-default" style="width:225px; letter-spacing:1.2px; "> <span class="glyphicon glyphicon-home"></span> <span class="oops-button">Take me Home </span></button>
                    </div>

                </div> <!--end of nested row for forgot submit button-->
			</form>
            </div> <!--end of Parent column-->
		</div> <!--end of formContainer row-->



    	<div class="row" > <!--signout msg-->
			<div class="col-lg-12  " style="text-align:center; margin-top:50px;"><h2 style="color:#a3bd96;">Statement Needed (Site promotion or App related)</h2></div>
		</div> <!--end of Signout msg-->
	
    </div>