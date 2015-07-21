<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AD170.aspx.vb" Inherits="ABSW_ADMIN.AD170" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Insurance Records</title>
    <link rel="stylesheet" type="text/css" href="StyleAdmin.css" />
    <script src="jquery-1.11.0.js" type="text/javascript"></script>
    <script src="jquery.simplemodal.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="Script/ScriptJS.js">
    </script>

<script type="text/javascript">
    // calling jquery functions once document is ready
    $(document).ready(function() {
        $("#cmbBrokerName").on('focusout', function(e) {
            e.preventDefault();
            $("#txtBrokerName").attr("value", $("#cmbBrokerName option:selected").val())

            //    return false;
        });

    });     //End of JQuery Document ready 
</script>

</head>
<body>
    <form id="Form1" name="Form1" runat="server">

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
                            &nbsp;|&nbsp;&nbsp;<a id="PageAnchor_Return_Link" runat="server" href="ADOTH.aspx" style="font-size:large; font-weight:bold;">CLOSE PAGE</a>
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
                        <td align="left" colspan="2" valign="top" class="tbl_caption">Insurance Records Data Entry</td>
                    </tr>

                    <tr>
                        <td colspan="2" valign="top"></td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransNum" Text="Transaction No:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTransNum" MaxLength="15" AutoPostBack="false" runat="server"></asp:TextBox></td>
                    </tr>
                                        <tr>
                        <td align="right" valign="top"><asp:Label ID="lbltranstype" Text="Transaction Type:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbTransType"  Width="150" runat="server">
                            <asp:ListItem Text="Fire" Value="Fire"></asp:ListItem>
                            <asp:ListItem Text="Motor" Value="Motor"></asp:ListItem>
                            <asp:ListItem Text="Accident" Value="Accident"></asp:ListItem>
                        </asp:DropDownList>                       
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransDate" Text="Trans Date:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTransDate" MaxLength="10" runat="server"></asp:TextBox>&nbsp;&nbsp;dd/mm/yyyy</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblBraNum" Text="Branch:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="ddlBraNum" AutoPostBack="false" Width="300" runat="server"></asp:DropDownList>                       
                            &nbsp;<asp:TextBox ID="txtBraNum" Enabled="false" Visible=false Width="60" MaxLength="4" runat="server"></asp:TextBox>
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
                        <td align="right" valign="top"><asp:Label ID="lblPolicyNo" Text="Policy No:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtPolicyNo"  runat="server" Width=300px></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblBroker" Text="Broker Name:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="bottom"><asp:TextBox ID="txtBrokerName"  Width="150"  runat="server" MaxLength=15 ></asp:TextBox>
                                           &nbsp;Find:<asp:TextBox ID="txtBrokerName_Search" Width="120px" runat="server"></asp:TextBox>
    		        &nbsp;<asp:Button ID="cmdBroker_Search" Text="Search" runat="server" OnClick="DoProc_BrokerName_Search" />
                    &nbsp;<asp:DropDownList id="cmbBrokerName" AutoPostBack="true" runat="server" Width="220px" OnTextChanged="DoProc_Broker_Change"></asp:DropDownList>

                        
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblInsurerName" Text="Insurer Name:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbInsurerName" AutoPostBack="false" Width="300" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtInsurerName"  runat="server" Width=300px Visible=false></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblCoInsurer1" Text="Co-Insurer 1:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbCoInsurerName1" AutoPostBack="false" Width="300" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtCoInsurerName1"  runat="server" Width=300px Visible=false></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblCoInsurer2" Text="Co-Insurer 2:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbCoInsurerName2" AutoPostBack="false" Width="300" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtCoInsurerName2"  runat="server" Width=300px Visible=false></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblCoInsurer3" Text="Co-Insurer 3:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbCoInsurerName3" AutoPostBack="false" Width="300" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtCoInsurerName3"  runat="server" Width=300px Visible=false></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblCoInsurer4" Text="Co-Insurer 4:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbCoInsurerName4" AutoPostBack="false" Width="300" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtCoInsurerName4"  runat="server" Width=300px Visible=false></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblCoInsurer5" Text="Co-Insurer 5:" runat="server" ></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:DropDownList ID="cmbCoInsurerName5" AutoPostBack="false" Width="300" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="txtCoInsurerName5"  runat="server" Width=300px Visible=false></asp:TextBox>
                        </td>
                    </tr>
                    
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblTransDescr" Text="Description:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtTransDescr" runat="server" Width="300px"></asp:TextBox>&nbsp;</td>
                    </tr>

                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblStartDate" Text="Start Date:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtStartDate" MaxLength="10" runat="server"></asp:TextBox>&nbsp;&nbsp;dd/mm/yyyy</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblEndDate" Text="End Date:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtEndDate" MaxLength="10" runat="server"></asp:TextBox>&nbsp;&nbsp;dd/mm/yyyy</td>                       
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblSumInsured" Text="Sum Insured =N=:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtSumInsured" MaxLength="15" runat="server"></asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" valign="top"><asp:Label ID="lblPremAmt" Text="Premium Amt =N=:" runat="server"></asp:Label>&nbsp;</td>
                        <td valign="top"><asp:TextBox ID="txtPremAmt" MaxLength="15" runat="server"></asp:TextBox>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">&nbsp;</td>
                    </tr>

                </table>
            </td>

        </tr>

                <tr>
                    <td colspan="7" valign="top">
                    <table align="center" width="95%" style="border-style:groove;">
                        <tr>
                            <td align="left" colspan="4" valign="top" class="tbl_caption">Transaction Details</td>
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

                                        <asp:HyperLinkField DataTextField="ibId" DataNavigateUrlFields="ibId,TransNo"
                                            DataNavigateUrlFormatString="~/AD170.aspx?idd={0},{1}" HeaderText="ID">
                                        </asp:HyperLinkField>
                            
                                        <asp:HyperLinkField DataTextField="TransNo" DataNavigateUrlFields="ibId,TransNo"
                                            DataNavigateUrlFormatString="~/AD170.aspx?idd={0},{1}" HeaderText="Trans #">
                                        </asp:HyperLinkField>
                            
                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:d}"/>
                                        <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:d}"/>
                                        <asp:TemplateField  HeaderText="Sum Insured">
                                          <ItemTemplate>
                                           <asp:Label ID="lblSumIns" runat="server" DataFormatString="{0:N2}" Text='<%#Eval("SumInsured") %>' />
                                          </ItemTemplate>
                                          <FooterTemplate>

                                            <asp:Label ID="lbltxtTotalSumIns" runat="server" Text="0.00" />

                                            </FooterTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Premium">
                                          <ItemTemplate>
                                           <asp:Label ID="lblPrem" runat="server" DataFormatString="{0:N2}" Text='<%#Eval("PremiumAmt") %>' />
                                          </ItemTemplate>
                                           <FooterTemplate>

                                            <asp:Label ID="lbltxtTotalPrem" runat="server" Text="0.00" />

                                            </FooterTemplate>

                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BrokerName" HeaderText="Broker"/>
                            
                                    </Columns>
  
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
    <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="InsurancePremBills" TypeName="CustodianAdmin.Data.InsurancePremRepository">
    </asp:ObjectDataSource>  

                    </td>    
                </tr>

                <tr>
                    <td colspan="7" valign="top">&nbsp;</td>
                </tr>    

        </table>

        </div>
                                    
    </form>
</body>
</html>
