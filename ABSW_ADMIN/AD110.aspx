<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AD110.aspx.vb" Inherits="ABSW_ADMIN.AD110" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <!--
    <base target="fra_content_01X" runat="server" />
    -->
    
    <title>Telephone Bill</title>
	<link rel="stylesheet" href="calendar.css" />
    <link rel="stylesheet" type="text/css" href="StyleAdmin.css" />
    <script src="jquery.simplemodal.js" type="text/javascript"></script>
    <script src="jquery-1.11.0.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>

    <script language="vbscript" type="text/vbscript" src="Script/ScriptVB.vbs">
    </script>


<script type="text/javascript">
    // calling jquery functions once document is ready
    $(document).ready(function() {
        $("#txtClassCod").css("display", "none");
        $("#txtTransNum").on('focusout', function(e) {
            e.preventDefault();
            if ($("#txtTransNum").val() != "") {
                $("#txtClassCod").val("006")
                LoadAdminCode();
            }
            return false;
        });

        function LoadAdminCode() {
            var classCode = "006";
            //alert("This is the class code: " + document.getElementById('txtClassCod').value + " and Item code :" + document.getElementById('txtTransNum').value);
            $.ajax({
                type: "POST",
                url: "AD111.aspx/GetMiscAdminInfo",
                data: JSON.stringify({ _classcode: classCode, _itemcode: document.getElementById('txtTransNum').value }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess_LoadAdminCode,
                failure: OnFailure,
                error: OnError_LoadAdminCodeInfo
            });
            // this avoids page refresh on button click
            return false;
        }
        function OnSuccess_LoadAdminCode(response) {
            //debugger;

            var xmlDoc = $.parseXML(response.d);
            var xml = $(xmlDoc);
            var admobjects = xml.find("Table");
            retrieve_AdminCodeInfoValues(admobjects);

        }
        // retrieve the values for branch
        function retrieve_AdminCodeInfoValues(admobjects) {
            //debugger;
            $.each(admobjects, function() {
                var admobject = $(this);
                document.getElementById('txtTelUsersName').value = $(this).find("sItemDesc").text()
                $('#ddlBraNum').val($(this).find("sBranch").text())
                $('#ddlDeptNum').val($(this).find("sDept").text())

            });

        }
        function OnError_LoadAdminCodeInfo(response) {
            //debugger;
            var errorText = response.responseText;

            alert('Error! Telephone Does Not Exist ');
            $('#txtTransNum').focus();

        }

        function OnFailure(response) {
            //debugger;
            var errorText = response.responseText;

            alert('Failure!!!' + '<br/>' + errorText);
        }

    });
</script>




</head>
<body>
    <form id="AD110" runat="server" submitdisabledcontrols="true">
    
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
                            Telephone Bill Data Entry
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="Label2" Text="Trans Class:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlTransClass" Width="300" runat="server" AutoPostBack="True"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtClassID" Visible="false" Enabled="false" Width="60" MaxLength="4" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtClassName" Visible="false" Enabled="false" Width="60" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="Label1" Text="Trans ID:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlTransID" Width="300" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtTransID" Visible="false" Enabled="false" Width="60" MaxLength="4" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtTransName" Visible="false" Enabled="false" Width="60" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransNum" Text="Telephone No:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTransNum" MaxLength="15" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransDate" Text="Transaction Date:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTransDate" MaxLength="10" runat="server"></asp:TextBox>
                        <script language="JavaScript" type="text/javascript">
                            new tcal({ 'formname': 'AD110', 'controlname': 'txtTransDate' });</script>&nbsp;&nbsp;dd/mm/yyyy</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblBraNum" Text="Branch:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlBraNum" Width="300" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtBraNum" Visible="false" Enabled="false" Width="60" MaxLength="4" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtBraName" Visible="false" Enabled="false" Width="60" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblDeptNum" Text="Department:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlDeptNum" Width="300" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtDeptNum" Visible="false" Enabled="false" Width="60" MaxLength="5" runat="server"></asp:TextBox>
                            &nbsp;<asp:TextBox ID="txtDeptName" Visible="false" Enabled="false" Width="60" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransAmt" Text="Amount =N=:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTransAmt" MaxLength="15" runat="server"></asp:TextBox>&nbsp;</td>
                    </tr>

                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblUserName" Text="User's Name:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTelUsersName" runat="server"></asp:TextBox>&nbsp;</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransDescr" Text="Description:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTransDescr" MaxLength="250" runat="server"></asp:TextBox>&nbsp;</td>                       
                    </tr>

                    <tr>
                        <td colspan="2" valign="top">&nbsp; <asp:TextBox ID="txtClassCod" MaxLength="15" runat="server" Text="006" Visible=true></asp:TextBox></td>
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
                                <asp:GridView id="grdData" Font-Size="Small" BackColor="White" bordercolor="Silver" borderstyle="Solid" runat="server"
                                    horizontalalign="Left" DataSourceID="ods"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    selectedindex="0" PagerSettings-Position="TopAndBottom" PagerSettings-Mode="NextPreviousFirstLast"
                                    PagerSettings-FirstPageText="First" PagerSettings-NextPageText="Next"
                                    PagerSettings-PreviousPageText="Previous" PagerSettings-LastPageText="Last"
                                    EmptyDataText="No data available." Width="100%">
                        
	                                <RowStyle></RowStyle>
	                                <PagerStyle></PagerStyle>
	                                <HeaderStyle BackColor="#9ACD32" ForeColor="White"></HeaderStyle>
                                    <selectedrowstyle backcolor="LightCyan" forecolor="DarkBlue" font-bold="true"/>  
                        
                                    <Columns>
                                        <asp:TemplateField>
        			                        <ItemTemplate>
        						                <asp:CheckBox id="chkSel" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:HyperLinkField DataTextField="tbId" DataNavigateUrlFields="tbId,TelephoneNo"
                                            DataNavigateUrlFormatString="~/AD110.aspx?idd={0},{1}," HeaderText="ID">
                                        </asp:HyperLinkField>
                            
                                        <asp:HyperLinkField DataTextField="TelephoneNo" DataNavigateUrlFields="tbId,TelephoneNo"
                                            DataNavigateUrlFormatString="~/AD110.aspx?idd={0},{1}," HeaderText="Telephone #">
                                        </asp:HyperLinkField>
                            
                                        <asp:BoundField DataField="TransDate" HeaderText="Trans Date" DataFormatString="{0:d}"/>
                                        <asp:TemplateField  HeaderText="Trans. Amt">
                                          <ItemTemplate>
                                           <asp:Label ID="lblTransAmt" runat="server" DataFormatString="{0:N2}" Text='<%#Eval("TransAmount") %>'/>
                                          </ItemTemplate>
                                             <FooterTemplate>

                                            <asp:Label ID="lbltxtTotal" runat="server" Text="0.00" />

                                            </FooterTemplate>

                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Description" HeaderText="Trans Description"/>
                                        <asp:BoundField DataField="UserName" HeaderText="Phone User"/>
                            
                                    </Columns>
  
                                </asp:GridView>
				                </td>
			            </tr>

                        <tr>
                            <td colspan="4" valign="top" style="width:auto;">
                                &nbsp;</td>
                        </tr>
                    </table>

                    </td>    
                </tr>


                <tr>
                    <td colspan="7" valign="top">&nbsp;</td>
                    
                </tr>    
        
    </table>
    <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="TelephoneBills" TypeName="CustodianAdmin.Data.TelephoneBillRepository">
    </asp:ObjectDataSource>  
    
    </div>
    </form>
</body>
</html>
