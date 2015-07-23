<%@ Page Language="vb" AutoEventWireup="false" EnableEventValidation="false" CodeBehind="AD111.aspx.vb" Inherits="ABSW_ADMIN.AD111" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <!--
    <base target="fra_content_01X" runat="server" />
    -->
    
    <title>Admin Codes Setup</title>
	<link rel="stylesheet" href="calendar.css" />
    <link rel="stylesheet" type="text/css" href="StyleAdmin.css" />
    <script src="AD_HOME.aspx" type="text/javascript"></script>
    <script src="jquery.simplemodal.js" type="text/javascript"></script>
    <script src="jquery-1.11.0.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>

    <script language="vbscript" type="text/vbscript" src="Script/ScriptVB.vbs">
    </script>


    <script type="text/javascript">
        // calling jquery functions once document is ready
        $(document).ready(function() {
            function GeneralRefresh() {
                switch ($('#cmbCodeClass').val()) {
                    case "001": //vehicle
                        $('#lblItemCode').text('Vehicle No');
                        $('#lblItemDesc').text('Name Of User');
                        $('#type1').show();
                        $('#BranchRow').show();
                        $('#DeptRow').show();
                       // LoadMotorTypes();
                        $('#TransClassRow').hide();
                        break;
                    case "002": //supplier
                        $('#lblItemCode').text('Supplier Code');
                        $('#lblItemDesc').text('Supplier Name');
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        break;
                    case "003": //Equipment
                        $('#lblItemCode').text('Equipment Code');
                        $('#lblItemDesc').text('Equipment Name');
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        break;
                    case "004": //insurance
                        $('#lblItemCode').text('Company Code');
                        $('#lblItemDesc').text('Company Name');
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        break;
                    case "005": //Brokers
                        $('#lblItemCode').text('Broker Code');
                        $('#lblItemDesc').text('Broke Name');
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        break;
                    case "006": //telephone user
                        $('#lblItemCode').text('User Code');
                        $('#lblItemDesc').text('User Name');
                        $('#type1').hide();
                        $('#BranchRow').show();
                        $('#DeptRow').show();
                        $('#TransClassRow').hide();
                        break;
                    case "007": //meter type
                        $('#lblItemCode').text('Meter No');
                        $('#lblItemDesc').text('Account No/Info');
                        $('#type1').show();
                        $('#BranchRow').show();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        LoadMeterTypes();
                        break;
                    case "008": //Department
                        $('#lblItemCode').text('Dept Code');
                        $('#lblItemDesc').text('Dept Name');
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        break;
                    case "009": //Branch
                        $('#lblItemCode').text('Branch Code');
                        $('#lblItemDesc').text('Branch Name');
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        break;
                    case "010": //Trans Class
                        $('#lblItemCode').text('Trans Class');
                        $('#lblItemDesc').text('Class Name');
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#TransClassRow').hide();
                        $('#DeptRow').hide();
                        break;
                    case "011": //Trans ID
                        $('#lblItemCode').text('Trans ID');
                        $('#lblItemDesc').text('ID Name');
                        //$('#lblBraNum').text('Trans Class');
                        
                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        break;
                    case "012": //Vehicle Types
                        $('#lblItemCode').text('Type ID');
                        $('#lblItemDesc').text('Type Name');

                        $('#type1').hide();
                        $('#BranchRow').hide();
                        $('#DeptRow').hide();
                        $('#TransClassRow').hide();
                        break;
                    default:
                        $('#BranchRow').show();
                        $('#DeptRow').show();
                }
            }

            //screen responds according to selection
            $("#cmbCodeClass").on('focusout', function(e) {
                e.preventDefault();
                GeneralRefresh();

            });

            //load data into type combo according to selection

            function LoadMeterTypes() {
                $.ajax({
                    type: "POST",
                    url: "AD111.aspx/GetMeterTypeList",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        $("#cmbCodeType").empty().append($("<option></option>").val("[-]").html("Please select"));
                        $.each(response.d, function() {
                            $("#cmbCodeType").append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                    },
                    error: function() {
                        alert("An error has occurred loading the meter types.");
                    }
                });


            }

            // ajax call to load motor types information
            function LoadMotorTypes() {
                $.ajax({
                    type: "POST",
                    url: "AD111.aspx/GetMotorTypesInfo",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {
                        $("#cmbCodeType").empty().append($("<option></option>").val("[-]").html("Please select"));
                        $.each(response.d, function(key, value) {
                            $("#cmbCodeType").append($("<option></option>").val(value.mId).html(value.Make + ',  ' + value.Type));
                        });
                    },
                    failure: OnFailure,
                    error: OnError_LoadMotorTypes
                });
                // this avoids page refresh on button click
                return false;
            }

            GeneralRefresh();

            function OnFailure(response) {
                //debugger;
                alert('Failure!!!' + '<br/>' + response.reponseText);
            }

            function OnError_LoadMotorTypes(response) {
                //debugger;
                var errorText = response.responseText;
                alert('Error!!!' + '\n\n' + errorText);
            }
        });
</script>
</head>
<body>
    <form id="AD111" runat="server">
    <div class="div_container_02">
    <br />
    <table align="center" border="0" class="tbl_main">
        <tr>
            <td align="left" colspan="7" valign="top">
                <table border="0" class="tbl_butt">
                    <tr>
                        <td colspan="5" valign="top">
                            &nbsp;<asp:Button ID="cmdNew" CssClass="cmd_butt" Text="New Data" runat="server" />
                            &nbsp;<asp:Button ID="cmdSave" CssClass="cmd_butt"  Text="Save Data" runat="server" Visible="false" />
                            &nbsp;<asp:Button id="cmdSaveN" CssClass="cmd_butt" Text="Save Data" runat="server" OnClientClick="JavaSave_Rtn()" />
                            &nbsp;<asp:Button ID="cmdDel" CssClass="cmd_butt" Text="Delete Data" Enabled="false" runat="server" Visible="false" />
                            &nbsp;<asp:Button ID="cmdDelN" CssClass="cmd_butt" Text="Delete Data" runat="server" OnClientClick="JavaDel_Rtn()" />
                            &nbsp;<asp:Button ID="cmdPrint" CssClass="cmd_butt" Enabled="false" Text="Print" runat="server" />
                            &nbsp;
                        </td>
                        <td id="td_Return_Link" colspan="1" valign="top" runat="server">
                            &nbsp;|&nbsp;&nbsp;<a id="PageAnchor_Return_Link" runat="server" href="Blankpg.aspx" style="font-size:large; font-weight:bold;">CLOSE PAGE</a>
                            <%=PageLinks%>&nbsp;&nbsp;|
                        </td>
                        <td colspan="1" valign="top">    
        	                <div style="display: none;">
        	                    &nbsp;&nbsp;Status:&nbsp;<asp:textbox id="txtAction" Visible="true" runat="server" EnableViewState="false" Width="50px"></asp:textbox>&nbsp;
        	                </div>
                            
        	            </td>
                    </tr>                    
                </table>
            </td>
        </tr>

                <tr>
                    <td colspan="7" valign="top"><asp:Label ID="lblMessage" Text="Status:" ForeColor="Red" runat="server"></asp:Label></td>
                </tr>    

        <tr>
            <td colspan="7" valign="top">
                <table align="left" width="100%">
                    <tr>
                        <td align="left" colspan="2" valign="top" class="tbl_caption">
                            Miscellanous Codes Data Setup
                        </td>
                    </tr>

                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblCodeClass" Text="Code Class:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbCodeClass" Width="150" runat="server"  AutoPostBack="true">
                            <asp:ListItem Text="Vehicle" Value="001"></asp:ListItem>
                            <asp:ListItem Text="Supplier" Value="002"></asp:ListItem>
                            <asp:ListItem Text="Equipment" Value="003"></asp:ListItem>
                            <asp:ListItem Text="Insurance Company" Value="004"></asp:ListItem>
                            <asp:ListItem Text="Broker" Value="005"></asp:ListItem>
                            <asp:ListItem Text="Telephone User" Value="006"></asp:ListItem>
                            <asp:ListItem Text="Meter No" Value="007"></asp:ListItem>
                            <asp:ListItem Text="Department" Value="008"></asp:ListItem>
                            <asp:ListItem Text="Branch" Value="009"></asp:ListItem>
                            <asp:ListItem Text="Trans Class" Value="010"></asp:ListItem>
                            <asp:ListItem Text="Trans IDs" Value="011"></asp:ListItem>
                            <asp:ListItem Text="Vehicle Type" Value="012"></asp:ListItem>
                        </asp:DropDownList><asp:TextBox ID="txtCodeClass" Visible="false" Enabled="false" Width="60" MaxLength="4" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblItemCode" Text="Code:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtCode" Width="150" runat="server"></asp:TextBox>&nbsp;</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblItemDesc" Text="Description:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtCodeDescr" runat="server" Width="300"></asp:TextBox>&nbsp;</td>                       
                    </tr>
                    <tr id="type1">
                        <td align="right" valign="top"><asp:Label ID="lblType" Text="Code Type:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top">
                           <asp:DropDownList ID="cmbCodeType" Width="300" runat="server" >
                            <asp:ListItem Text="Insurance" Value="Insurance"></asp:ListItem>
                            <asp:ListItem Text="Supplier" Value="Supplier"></asp:ListItem>
                            <asp:ListItem Text="Services" Value="Services"></asp:ListItem>
                        </asp:DropDownList>
                      </td>
                   </tr>
                    <tr id="TransClassRow">
                        <td align="right" valign="top"><asp:Label ID="lblTransClass" Text="TransClass:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlTransClass" Width="300" runat="server" AutoPostBack="true"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtTransClass" Visible="false" Enabled="false" Width="60" MaxLength="4" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtTransclassName" Visible="false" Enabled="false" Width="60" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr id="BranchRow">
                        <td align="right" valign="top"><asp:Label ID="lblBraNum" Text="Branch:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlBraNum" Width="300" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtBraNum" Visible="false" Enabled="false" Width="60" MaxLength="4" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtBraName" Visible="false" Enabled="false" Width="60" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr id="DeptRow">
                        <td align="right" valign="top"><asp:Label ID="lblDeptNum" Text="Department:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlDeptNum" Width="300" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtDeptNum" Visible="false" Enabled="false" Width="60" MaxLength="5" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtDeptName" Visible="false" Enabled="false" Width="60" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2" valign="top">&nbsp;</td>
                    </tr>

                </table>
            </td>                
        </tr>

                <tr>
                    <td colspan="7" valign="top">
                    <table align="center" width="90%" style="border-style:groove;">
                        <tr>
                            <td align="left" valign="top" colspan="4" class="tbl_caption">
                                Transaction-Codes Details
                            </td>
                        </tr>
  			           <tr>
                            <td align="left" colspan="4" valign="top" style=" background-color: #ccccee; height: 26px">
					            &nbsp;&nbsp;<asp:Button ID="cmdDelItem_ASP" Enabled="true" Font-Bold="true" Text="Delete Item" runat="server" />
					            &nbsp;&nbsp;<asp:Button ID="cmdDelItem" Enabled="false" Font-Bold="true" Visible="false" Text="Delete Item" runat="server" />
				                </td>
			            </tr>

                        <tr>
                            <td colspan="4" valign="top" style="width:auto;">
                                <asp:GridView id="grdData" Font-Size="Small" BackColor="White" bordercolor="Silver" borderstyle="Solid" runat="server"
                                    horizontalalign="Left" DataSourceID="ods"
                                    AutoGenerateColumns="False" AllowPaging="True"
                                    selectedindex="0" PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                    EmptyDataText="No data available." Width="100%">
                        
	                                <RowStyle></RowStyle>
	                                <PagerStyle></PagerStyle>
	                                <HeaderStyle BackColor="#9ACD32" ForeColor="White"></HeaderStyle>
                                    <selectedrowstyle backcolor="LightCyan" forecolor="DarkBlue" font-bold="true"/>  
                        
<PagerSettings FirstPageText="First" LastPageText="Last" Mode="NextPreviousFirstLast" NextPageText="Next" Position="TopAndBottom" PreviousPageText="Previous"></PagerSettings>
                        
                                    <Columns>
                                        <asp:TemplateField>
        			                        <ItemTemplate>
        						                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:HyperLinkField DataTextField="icId" DataNavigateUrlFields="icId,ClassCode"
                                            DataNavigateUrlFormatString="~/AD111.aspx?idd={0},{1}," HeaderText="ID">
                                        </asp:HyperLinkField>
                            
                                        <asp:HyperLinkField DataTextField="ClassCode" DataNavigateUrlFields="icId,ClassCode"
                                            DataNavigateUrlFormatString="~/AD111.aspx?idd={0},{1}," HeaderText="Class">
                                        </asp:HyperLinkField>
                                        <asp:BoundField DataField="ItemCode" HeaderText="Code"/>
                                        <asp:BoundField DataField="ItemDesc" HeaderText="Description"/>
                            
                                    </Columns>
  
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>

                    </td>    
                </tr>


                <tr>
                    <td colspan="7" valign="top">&nbsp;</td>
                </tr>    
        
    </table>
    <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="GetAdminCodes" TypeName="CustodianAdmin.Data.AdminCodeRepository">
        <SelectParameters>
            <asp:ControlParameter ControlID="cmbCodeClass" DefaultValue="002" Name="_classcode" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>  
    
    </div>
    </form>
</body>
</html>
