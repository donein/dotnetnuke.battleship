<%@ Control Language="vb" AutoEventWireup="false" Codebehind="base.ascx.vb" Inherits="DONEIN_NET.Battleship.Base" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<STYLE>
	.alert_box
	{
		BACKGROUND: #F3F3F3;
		BORDER: 1px solid #8484FF;
		COLOR: #8484FF;
		FONT-SIZE: 9pt;
		FONT-FAMILY: Arial;
		TEXT-ALIGN: center;
		VERTICAL-ALIGN: middle;
		PADDING-TOP: 1px;
		PADDING-LEFT: 5px;
		PADDING-RIGHT: 5px;
		WIDTH: 548px;
		HEIGHT: 20px;
	}
</STYLE>


<BR />
<TABLE BORDER="0" WIDTH="548" CELLPADDING="0" CELLSPACING="0">
		<TD ALIGN="center" VALIGN="top">
			<DIV ID="div_alert" CLASS="alert_box"></DIV>
			<ASP:LITERAL RUNAT="server" ID="ltr_script" />
		</TD>
	</TR>
	<TR HEIGHT="30">
		<TD HEIGHT="30" ALIGN="left" VALIGN="top">
			<ASP:LINKBUTTON RUNAT="server" ID="btn_restart" TEXT="Restart Game" CSSCLASS="CommandButton" STYLE="padding-left: 2px;"/>
		</TD>
	</TR>
</TABLE>


