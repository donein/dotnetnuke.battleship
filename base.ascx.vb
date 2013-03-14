Imports DotNetNuke
Imports DotNetNuke.Common.Globals
Imports System.Web.HttpContext

Namespace DONEIN_NET.Battleship

	Public Class Base
		Inherits DotNetNuke.Entities.Modules.PortalModuleBase
		Implements Entities.Modules.IActionable
		'Implements Entities.Modules.IPortable
		Implements Entities.Modules.ISearchable



		#Region " Declare: Shared Classes "

		#End Region



		#Region " Declare: Local Objects "

			Protected WithEvents btn_restart As System.Web.UI.WebControls.LinkButton
			Protected WithEvents ltr_script As System.Web.UI.WebControls.Literal	
			Protected str_script As String
			 
		#End Region



		#Region " Page: Load "

  			Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
				If Not IsPostBack Then
					create_script()	
				End If
			End Sub

		#End Region



		#Region " Handle: Fleet "

			Private Function create_script() As String
				Dim str_image_path As String = DotNetNuke.Common.ApplicationPath + "/DesktopModules/DONEIN_NET/Battleship/"
				
				Dim tmp_minesweeper As String = DotNetNuke.Services.Localization.Localization.GetString("pl_minesweeper.Text", LocalResourceFile)
				Dim tmp_frigate As String = DotNetNuke.Services.Localization.Localization.GetString("pl_frigate.Text", LocalResourceFile)
				Dim tmp_cruiser As String = DotNetNuke.Services.Localization.Localization.GetString("pl_cruiser.Text", LocalResourceFile)
				Dim tmp_battleship As String = DotNetNuke.Services.Localization.Localization.GetString("pl_battleship.Text", LocalResourceFile)
				
				Dim tmp_preload As String = DotNetNuke.Services.Localization.Localization.GetString("pl_preload.Text", LocalResourceFile)
				Dim tmp_you_sank_my As String = DotNetNuke.Services.Localization.Localization.GetString("pl_you_sank_my.Text", LocalResourceFile)
				Dim tmp_I_sank_your As String = DotNetNuke.Services.Localization.Localization.GetString("pl_I_sank_your.Text", LocalResourceFile)
				Dim tmp_you_win As String = DotNetNuke.Services.Localization.Localization.GetString("pl_you_win.Text", LocalResourceFile)
				Dim tmp_I_win As String = DotNetNuke.Services.Localization.Localization.GetString("pl_I_win.Text", LocalResourceFile)
				Dim tmp_computer_has As String = DotNetNuke.Services.Localization.Localization.GetString("pl_computer_has.Text", LocalResourceFile)
				Dim tmp_nothing_left As String = DotNetNuke.Services.Localization.Localization.GetString("pl_nothing_left.Text", LocalResourceFile)
				
				str_script = "" + vbcrlf
				str_script += "<SCRIPT LANGUAGE=""JavaScript"">" + vbcrlf
				str_script += "<!--" + vbcrlf 
				str_script += "" + vbcrlf
				str_script += "var alert_box = document.getElementById('div_alert');" + vbcrlf
				str_script += "/* LIVE SHIPS */" + vbcrlf
				str_script += "var ship =  [[[1,5], [1,2,5], [1,2,3,5], [1,2,3,4,5]], [[6,10], [6,7,10], [6,7,8,10], [6,7,8,9,10]]];" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* DEAD SHIPS */" + vbcrlf
				str_script += "var dead = [[[201,203], [201,202,203], [201,202,202,203], [201,202,202,202,203]], [[204,206], [204,205,206], [204,205,205,206], [204,205,205,205,206]]];" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* SHIP DESCRIPTIONS */" + vbcrlf
				str_script += "var shiptypes = [[""" + tmp_minesweeper + """,2,4],[""" + tmp_frigate + """,3,4],[ """ + tmp_cruiser + """,4,2],[ """ + tmp_battleship + """,5,1]];" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "var gridx = 16, gridy = 16;" + vbcrlf
				str_script += "var player = [], computer = [], playersships = [], computersships = [];" + vbcrlf
				str_script += "var playerlives = 0, computerlives = 0, playflag=true, statusmsg="""";" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* PRELOAD IMAGES */" + vbcrlf
				str_script += "var preloaded = [];" + vbcrlf
				str_script += "function imagePreload()" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	var i,ids = [1,2,3,4,5,6,7,8,9,10,100,101,102,103,201,202,203,204,205,206];" + vbcrlf
				str_script += "	window.status = """ + tmp_preload + """;" + vbcrlf
				str_script += "	for (i=0;i<ids.length;++i)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		var img = new Image, name = """ + str_image_path + "images/""+ids[i]+"".gif"";" + vbcrlf
				str_script += "		img.src = name;" + vbcrlf
				str_script += "		preloaded[i] = img;" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "	window.status = """";" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* PLACE SHIPS ON GRID */" + vbcrlf
				str_script += "function setupPlayer(ispc)" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	var y,x;" + vbcrlf
				str_script += "	grid = [];" + vbcrlf
				str_script += "	for (y=0;y<gridx;++y)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		grid[y] = [];" + vbcrlf
				str_script += "		for (x=0;x<gridx;++x)" + vbcrlf
				str_script += "		grid[y][x] = [100,-1,0];" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "	var shipno = 0;" + vbcrlf
				str_script += "	var s;" + vbcrlf
				str_script += "	for (s=shiptypes.length-1;s>=0;--s)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		var i;" + vbcrlf
				str_script += "		for (i=0;i<shiptypes[s][2];++i)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			var d = Math.floor(Math.random()*2);" + vbcrlf
				str_script += "			var len = shiptypes[s][1], lx=gridx, ly=gridy, dx=0, dy=0;" + vbcrlf
				str_script += "			if ( d==0)" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				lx = gridx-len;" + vbcrlf
				str_script += "				dx=1;" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "			else" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				ly = gridy-len;" + vbcrlf
				str_script += "				dy=1;" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "			var x,y,ok;" + vbcrlf
				str_script += "			do" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				y = Math.floor(Math.random()*ly);" + vbcrlf
				str_script += "				x = Math.floor(Math.random()*lx);" + vbcrlf
				str_script += "				var j,cx=x,cy=y;" + vbcrlf
				str_script += "				ok = true;" + vbcrlf
				str_script += "				for (j=0;j<len;++j)" + vbcrlf 
				str_script += "				{" + vbcrlf
				str_script += "					if (grid[cy][cx][0] < 100)" + vbcrlf 
				str_script += "					{" + vbcrlf
				str_script += "						ok=false;" + vbcrlf
				str_script += "						break;" + vbcrlf
				str_script += "					}" + vbcrlf
				str_script += "					cx+=dx;" + vbcrlf
				str_script += "					cy+=dy;" + vbcrlf
				str_script += "				}" + vbcrlf
				str_script += "			}" + vbcrlf 
				str_script += "			while(!ok);" + vbcrlf
				str_script += "			var j,cx=x,cy=y;" + vbcrlf
				str_script += "			for (j=0;j<len;++j)" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				grid[cy][cx][0] = ship[d][s][j];" + vbcrlf
				str_script += "				grid[cy][cx][1] = shipno;" + vbcrlf
				str_script += "				grid[cy][cx][2] = dead[d][s][j];" + vbcrlf
				str_script += "				cx+=dx;" + vbcrlf
				str_script += "				cy+=dy;" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "			if (ispc)" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				computersships[shipno] = [s,shiptypes[s][1]];" + vbcrlf
				str_script += "				computerlives++;" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "			else" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				playersships[shipno] = [s,shiptypes[s][1]];" + vbcrlf
				str_script += "				playerlives++;" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "			shipno++;" + vbcrlf
				str_script += "		}" + vbcrlf	
				str_script += "	}" + vbcrlf
				str_script += "	return grid;" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* CHANGE IMAGE */" + vbcrlf
				str_script += "function set_image(y,x,id,ispc)" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	if ( ispc )" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		computer[y][x][0] = id;" + vbcrlf
				str_script += "		document.images[""pc""+y+""_""+x].src = """ + str_image_path + "images/""+id+"".gif"";" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "	else" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		player[y][x][0] = id;" + vbcrlf
				str_script += "		document.images[""ply""+y+""_""+x].src = """ + str_image_path + "images/""+id+"".gif"";" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* BUILD GRID */" + vbcrlf
				str_script += "function showGrid(ispc)" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	var y,x;" + vbcrlf
				str_script += "	for (y=0;y<gridy;++y)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		for (x=0;x<gridx;++x)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			if ( ispc )" + vbcrlf
				str_script += "			{" + vbcrlf
				str_script += "				document.write ('<A HREF=""javascript:gridClick('+y+','+x+');""><IMG SRC=""" + str_image_path + "images/100.gif"" NAME=""pc'+y+'_'+x+'"" WIDTH=""16"" HEIGHT=""16"" STYLE=""border-top: 0px; border-left: 0px; border-right: #88A7FF 1px solid; border-bottom: #88A7FF 1px solid;""></A>');" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "			else" + vbcrlf
				str_script += "			{" + vbcrlf
				str_script += "				document.write ('<A HREF=""javascript:void(0);""><IMG SRC=""" + str_image_path + "images/'+player[y][x][0]+'.gif"" NAME=""ply'+y+'_'+x+'"" WIDTH=""16"" HEIGHT=""16"" STYLE=""border-top: 0px; border-left: 0px; border-right: #88A7FF 1px solid; border-bottom: #88A7FF 1px solid;""></A>');" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "		document.write('<BR />');" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* HANDLE CLICKS */" + vbcrlf
				str_script += "function gridClick(y,x)" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	if ( playflag )" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		if (computer[y][x][0] < 100)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			set_image(y,x,103,true);" + vbcrlf
				str_script += "			var shipno = computer[y][x][1];" + vbcrlf
				str_script += "			if ( --computersships[shipno][1] == 0 )" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				sinkShip(computer,shipno,true);" + vbcrlf
				str_script += "				if (alert_box) alert_box.innerHTML = ""<SPAN STYLE=&quot;COLOR: #00DD00;&quot;>" + tmp_you_sank_my + " ""+shiptypes[computersships[shipno][0]][0]+""!</SPAN>"";" + vbcrlf
				str_script += "				updateStatus();" + vbcrlf
				str_script += "				if ( --computerlives == 0 )" + vbcrlf 
				str_script += "				{" + vbcrlf
				str_script += "					if (alert_box) alert_box.innerHTML = ""<SPAN STYLE=&quot;COLOR: #00DD00;&quot;>" + tmp_you_win + "</SPAN>"";" + vbcrlf
				str_script += "					playflag = false;" + vbcrlf
				str_script += "				}" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "			if ( playflag ) computerMove();" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "		else if (computer[y][x][0] == 100)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			set_image(y,x,102,true);" + vbcrlf
				str_script += "			computerMove();" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* MOVE COMPUTER PLAYER */" + vbcrlf
				str_script += "function computerMove()" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	var x,y,pass;" + vbcrlf
				str_script += "	var sx,sy;" + vbcrlf
				str_script += "	var selected = false;" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "	for (pass=0;pass<2;++pass)" + vbcrlf
				str_script += "	{" + vbcrlf
				str_script += "		for (y=0;y<gridy && !selected;++y)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			for (x=0;x<gridx && !selected;++x)" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "
				str_script += "				if (player[y][x][0]==103)" + vbcrlf 
				str_script += "				{" + vbcrlf
				str_script += "					sx=x; sy=y;" + vbcrlf
				str_script += "					var nup=(y>0 && player[y-1][x][0]<=100);" + vbcrlf
				str_script += "					var ndn=(y<gridy-1 && player[y+1][x][0]<=100);" + vbcrlf
				str_script += "					var nlt=(x>0 && player[y][x-1][0]<=100);" + vbcrlf
				str_script += "					var nrt=(x<gridx-1 && player[y][x+1][0]<=100);" + vbcrlf
				str_script += "					if ( pass == 0 )" + vbcrlf 
				str_script += "					{" + vbcrlf
				str_script += "						var yup=(y>0 && player[y-1][x][0]==103);" + vbcrlf
				str_script += "						var ydn=(y<gridy-1 && player[y+1][x][0]==103);" + vbcrlf
				str_script += "						var ylt=(x>0 && player[y][x-1][0]==103);" + vbcrlf
				str_script += "						var yrt=(x<gridx-1 && player[y][x+1][0]==103);" + vbcrlf
				str_script += "						if ( nlt && yrt) { sx = x-1; selected=true; }" + vbcrlf
				str_script += "						else if ( nrt && ylt) { sx = x+1; selected=true; }" + vbcrlf
				str_script += "						else if ( nup && ydn) { sy = y-1; selected=true; }" + vbcrlf
				str_script += "						else if ( ndn && yup) { sy = y+1; selected=true; }" + vbcrlf
				str_script += "					}" + vbcrlf
				str_script += "					else" + vbcrlf 
				str_script += "					{" + vbcrlf
				str_script += "						if ( nlt ) { sx=x-1; selected=true; }" + vbcrlf
				str_script += "						else if ( nrt ) { sx=x+1; selected=true; }" + vbcrlf
				str_script += "						else if ( nup ) { sy=y-1; selected=true; }" + vbcrlf
				str_script += "						else if ( ndn ) { sy=y+1; selected=true; }" + vbcrlf
				str_script += "					}" + vbcrlf
				str_script += "				}" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "	if ( !selected )" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		do" + vbcrlf
				str_script += "		{" + vbcrlf
				str_script += "			sy = Math.floor(Math.random() * gridy);" + vbcrlf
				str_script += "			sx = Math.floor(Math.random() * gridx/2)*2+sy%2;" + vbcrlf
				str_script += "		}" + vbcrlf 
				str_script += "		while( player[sy][sx][0]>100 );" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "	if (player[sy][sx][0] < 100)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		set_image(sy,sx,103,false);" + vbcrlf
				str_script += "		var shipno = player[sy][sx][1];" + vbcrlf
				str_script += "		if ( --playersships[shipno][1] == 0 )" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			sinkShip(player,shipno,false);" + vbcrlf
				str_script += "			if (alert_box) alert_box.innerHTML = ""<SPAN STYLE=&quot;COLOR: #DD0000;&quot;>" + tmp_I_sank_your + " ""+shiptypes[playersships[shipno][0]][0]+""!</SPAN>"";" + vbcrlf
				str_script += "			if ( --playerlives == 0 )" + vbcrlf 
				str_script += "			{" + vbcrlf
				str_script += "				knowYourEnemy();" + vbcrlf
				str_script += "				if (alert_box) alert_box.innerHTML = ""<SPAN STYLE=&quot;COLOR: #DD0000;&quot;>" + tmp_I_win + "</SPAN>"";" + vbcrlf
				str_script += "				playflag = false;" + vbcrlf
				str_script += "			}" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "	else" + vbcrlf
				str_script += "	{" + vbcrlf
				str_script += "		set_image(sy,sx,102,false);" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* WE'RE GOING DOWN! */" + vbcrlf
				str_script += "function sinkShip(grid,shipno,ispc)" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	var y,x;" + vbcrlf
				str_script += "	for (y=0;y<gridx;++y)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		for (x=0;x<gridx;++x)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			if ( grid[y][x][1] == shipno )" + vbcrlf
				str_script += "			if (ispc) set_image(y,x,computer[y][x][2],true);" + vbcrlf
				str_script += "			else set_image(y,x,player[y][x][2],false);" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "	" + vbcrlf
				str_script += "/* SHOW ALL ENEMY SHIPS */" + vbcrlf
				str_script += "function knowYourEnemy()" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	var y,x;" + vbcrlf
				str_script += "	for (y=0;y<gridx;++y)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		for (x=0;x<gridx;++x)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			if ( computer[y][x][0] == 103 )" + vbcrlf
				str_script += "			set_image(y,x,computer[y][x][2],true);" + vbcrlf
				str_script += "			else if ( computer[y][x][0] < 100 )" + vbcrlf
				str_script += "			set_image(y,x,computer[y][x][0],true);" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "/* COUNT THE NUMBER OF BOGIES REMAINING */" + vbcrlf
				str_script += "function updateStatus()" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	var f=false,i,s = """ + tmp_computer_has + " "";" + vbcrlf
				str_script += "	for (i=0;i<computersships.length;++i)" + vbcrlf 
				str_script += "	{" + vbcrlf
				str_script += "		if (computersships[i][1] > 0)" + vbcrlf 
				str_script += "		{" + vbcrlf
				str_script += "			if (f) s=s+"", ""; else f=true;" + vbcrlf
				str_script += "			s = s + shiptypes[computersships[i][0]][0];" + vbcrlf
				str_script += "		}" + vbcrlf
				str_script += "	}" + vbcrlf
				str_script += "	if (!f) s = s + """ + tmp_nothing_left + """;" + vbcrlf
				str_script += "	statusmsg = s;" + vbcrlf
				str_script += "	window.status = statusmsg;" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "	" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "function setStatus()" + vbcrlf 
				str_script += "{" + vbcrlf
				str_script += "	window.status = statusmsg;" + vbcrlf
				str_script += "}" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "	" + vbcrlf
				str_script += "/* GAME STARTUP */" + vbcrlf
				str_script += "imagePreload();" + vbcrlf
				str_script += "player = setupPlayer(false);" + vbcrlf
				str_script += "computer = setupPlayer(true);" + vbcrlf
				str_script += "document.write(""<TABLE BORDER=0><TR><TD ALIGN=CENTER><SPAN CLASS=SubHead>COMPUTER FLEET</SPAN></TD><TD ALIGN=CENTER><SPAN CLASS=SubHead>PLAYER FLEET</SPAN></TD></TR><TR><TD>"");" + vbcrlf
				str_script += "showGrid(true);" + vbcrlf
				str_script += "document.write(""</TD><TD>"");" + vbcrlf
				str_script += "showGrid(false);" + vbcrlf
				str_script += "document.write(""</TD></TR></TABLE>"");" + vbcrlf
				str_script += "updateStatus();" + vbcrlf
				str_script += "setInterval(""setStatus();"", 500);" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "" + vbcrlf
				str_script += "// -->" + vbcrlf
				str_script += "</SCRIPT>" + vbcrlf

				ltr_script.Text = str_script
				str_script = NOTHING

			End Function

			
			Private Sub btn_restart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_restart.Click
				create_script()	
			End Sub

		#End Region



		#Region " Web Form Designer Generated Code "

			'This call is required by the Web Form Designer.
			<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

			End Sub

			'NOTE: The following placeholder declaration is required by the Web Form Designer.
			'Do not delete or move it.
			Private designerPlaceholderDeclaration As System.Object

			Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
				'CODEGEN: This method call is required by the Web Form Designer
				'Do not modify it using the code editor.
				InitializeComponent()
			End Sub

		#End Region



		#Region "Optional Interfaces"

			Public ReadOnly Property ModuleActions() As Entities.Modules.Actions.ModuleActionCollection Implements Entities.Modules.IActionable.ModuleActions
				Get
					Dim Actions As New Entities.Modules.Actions.ModuleActionCollection
						'Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString("pl_action_update.Text", LocalResourceFile), "", "", "", get_update_url("DONEIN.NET Battleship"), False, DotNetNuke.Security.SecurityAccessLevel.Host, True, True) '// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
						'Actions.Add(GetNextActionID, DotNetNuke.Services.Localization.Localization.GetString(Entities.Modules.Actions.ModuleActionType.ContentOptions, LocalResourceFile), Entities.Modules.Actions.ModuleActionType.ContentOptions, "", "", EditUrl(), False, Security.SecurityAccessLevel.Edit, True, False)
					Return Actions
				End Get
			End Property

			'// DNNUPDATE SEEMS TO HAVE BEEN RETIRED
			'Private Function get_update_url(ByVal module_name As String) As String
			'	Dim obj_tab As DotNetNuke.Entities.Tabs.TabInfo
			'	With New DotNetNuke.Entities.Tabs.TabController 
			'		obj_tab = .GetTabByName("DNN Update", DotNetNuke.Common.Utilities.Null.NullInteger)
			'	End With

			'	If obj_tab Is Nothing Then
			'		Return "http://www.dnnupdate.com/module-intro.content?module=" + module_name
			'	Else
			'		Return obj_tab.Url + "?tabid=" + obj_tab.TabID.ToString + "&module=" + module_name
			'	End If
			'End Function


			'Public Function ExportModule(ByVal ModuleID As Integer) As String Implements Entities.Modules.IPortable.ExportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Function

			'Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserId As Integer) Implements Entities.Modules.IPortable.ImportModule
			'	' included as a stub only so that the core knows this module Implements Entities.Modules.IPortable
			'End Sub

			Public Function GetSearchItems(ByVal ModInfo As Entities.Modules.ModuleInfo) As Services.Search.SearchItemInfoCollection Implements Entities.Modules.ISearchable.GetSearchItems
				' included as a stub only so that the core knows this module Implements Entities.Modules.ISearchable
			End Function

		#End Region



	End Class

End NameSpace


