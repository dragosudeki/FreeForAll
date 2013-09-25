function spawn1::onLevelLoaded(%this, %scenegraph)
{
	$maxy = %this.getPositionY();
	$onSolo = 0 ;
	$spawn1 = %this ;	
	%this.spawn();
}

function spawn1::spawn(%this){
	if ($playerAA $= "player1"){
		%this.playerClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = playerClass ;
			spawn1 = %this ;
		} ;
		%this.playerClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.playerClass.start() ;
	}
	else if ($playerAA $= "player2"){
		%this.playerClass2 = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = playerClass2 ;
			spawn1 = %this ;
		} ;
		%this.playerClass2.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.playerClass2.start();
	}
	else if ($playerAA $= "morte"){
		%this.morteClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = morteClass ;
			spawn1 = %this ;
		} ;
		%this.morteClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.morteClass.start();
	}
	else if ($playerAA $= "rupes"){
		%this.rupesClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = rupesClass ;
			spawn1 = %this ;
		} ;
		%this.rupesClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.rupesClass.start();
	}
	else if ($playerAA $= "casper"){
		%this.casperClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = casperClass ;
			spawn1 = %this ;
		} ;
		%this.casperClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.casperClass.start();
	}
	else if ($playerAA $= "belshazzar"){
		%this.belshazzarClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = belshazzarClass ;
			spawn1 = %this ;
		} ;
		%this.belshazzarClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.belshazzarClass.start();
	}
	else if ($playerAA $= "eltonta"){
		%this.elTontaClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = elTontaClass ;
			spawn1 = %this ;
		} ;
		%this.elTontaClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.elTontaClass.start();
	}
	else if ($playerAA $= "yuki"){
		%this.yukiClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = yukiClass ;
			spawn1 = %this ;
		} ;
		%this.yukiClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.yukiClass.start();
	}
	else if ($playerAA $= "fear"){
		%this.fearClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = fearClass ;
			spawn1 = %this ;
		} ;
		%this.fearClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.fearClass.start();
	}
	else if ($playerAA $= "dominio"){
		%this.dominioClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = dominioClass ;
			spawn1 = %this ;
		} ;
		%this.dominioClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.dominioClass.start();
	}
	else if ($playerAA $= "arkaedes"){
		%this.bioClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = bioClass ;
			spawn1 = %this ;
		} ;
		%this.bioClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.bioClass.start();
	}
	else {
		%this.svriClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = svriClass ;
			spawn1 = %this ;
		} ;
		%this.svriClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.svriClass.start();
	}
	if ($lives == 0)
		$lives = 4 ;
	$player.lives = $lives-1;
	playerLives.text = $lives-1;
}

function spawn1::reset(%this){
	
	if ($playerAA $= "player1"){
		%this.playerClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
	}
	if ($playerAA $= "player2"){
		%this.playerClass2.setPosition(%this.getPositionX(),%this.getPositionY()) ;
	}
	
}

function spawn2::reset(%this){
	
	if ($playerBB $= "player1"){
		%this.BplayerClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
	}
	if ($playerBB $= "player2"){
		%this.BplayerClass2.setPosition(%this.getPositionX(),%this.getPositionY()) ;
	}
	
}
/////////////////////////////////////////////////////////
function spawn2::onLevelLoaded(%this, %scenegraph)
{
	$spawn2 = %this ;
	%this.spawn();
}

function spawn2::spawn(%this){
	
	if ($playerBB $= "player1"){
		%this.BplayerClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BplayerClass ;
			spawn2 = %this ;
		} ;
		%this.BplayerClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BplayerClass.start();
	}
	else if ($playerBB $= "player2"){
		%this.BplayerClass2 = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BplayerClass2 ;
			spawn2 = %this ;
		} ;
		%this.BplayerClass2.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BplayerClass2.start();
	}
	else if ($playerBB $= "morte"){
		%this.BmorteClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BmorteClass ;
			spawn2 = %this ;
		} ;
		%this.BmorteClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BmorteClass.start();
	}
	else if ($playerBB $= "rupes"){
		%this.BrupesClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BrupesClass ;
			spawn2 = %this ;
		} ;
		%this.BrupesClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BrupesClass.start();
	}
	else if ($playerBB $= "casper"){
		%this.BcasperClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BcasperClass ;
			spawn2 = %this ;
		} ;
		%this.BcasperClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BcasperClass.start();
	}
	else if ($playerBB $= "belshazzar") {
		%this.BbelshazzarClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BbelshazzarClass ;
			spawn2 = %this ;
		} ;
		%this.BbelshazzarClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BbelshazzarClass.start();
	}
	else if ($playerBB $= "eltonta"){
		%this.BelTontaClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BelTontaClass ;
			spawn2 = %this ;
		} ;
		%this.BelTontaClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BelTontaClass.start();
	}
	else if ($playerBB $= "yuki"){
		%this.ByukiClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = ByukiClass ;
			spawn2 = %this ;
		} ;
		%this.ByukiClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.ByukiClass.start();
	}
	else if ($playerBB $= "fear"){
		%this.BfearClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BfearClass ;
			spawn2 = %this ;
		} ;
		%this.BfearClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BfearClass.start();
	}
	else if ($playerBB $= "dominio"){
		%this.BdominioClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BdominioClass ;
			spawn2 = %this ;
		} ;
		%this.BdominioClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BdominioClass.start();
	}
	else if ($playerBB $= "arkaedes"){
		%this.BbioClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BbioClass ;
			spawn2 = %this ;
		} ;
		%this.BbioClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BbioClass.start();
	}
	else {
		%this.BsvriClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = BsvriClass ;
			spawn2 = %this ;
		} ;
		%this.BsvriClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.BsvriClass.start();
	}
	if ($lives == 0)
		$lives = 4 ;
	$Bplayer.lives = $lives - 1;
	player2Lives.text = $lives - 1;
	
}
//////////////////////////////////////////////////////////////
function soloSpawn::onLevelLoaded(%this, %scenegraph)
{
	$onSolo = 1 ;
	$maxy = %this.getPositionY();
	$spawn = %this ;	
}
function soloSpawn::spawn(%this){
	if ($playerAA $= "player1"){
		%this.playerClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = playerClass ;
			spawn1 = %this ;
		} ;
		%this.playerClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.playerClass.start() ;
	}
	else if ($playerAA $= "player2"){
		%this.playerClass2 = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = playerClass2 ;
			spawn1 = %this ;
		} ;
		%this.playerClass2.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.playerClass2.start();
	}
	else if ($playerAA $= "morte"){
		%this.morteClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = morteClass ;
			spawn1 = %this ;
		} ;
		%this.morteClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.morteClass.start();
	}
	else if ($playerAA $= "rupes"){
		%this.rupesClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = rupesClass ;
			spawn1 = %this ;
		} ;
		%this.rupesClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.rupesClass.start();
	}
	else if ($playerAA $= "casper"){
		%this.casperClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = casperClass ;
			spawn1 = %this ;
		} ;
		%this.casperClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.casperClass.start();
	}
	else if ($playerAA $= "belshazzar"){
		%this.belshazzarClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = belshazzarClass ;
			spawn1 = %this ;
		} ;
		%this.belshazzarClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.belshazzarClass.start();
	}

	else if ($playerAA $= "eltonta"){
		%this.elTontaClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = elTontaClass ;
			spawn1 = %this ;
		} ;
		%this.elTontaClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.elTontaClass.start();
	}
	else {
		%this.elTontaClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph	;
			class = elTontaClass ;
			spawn1 = %this ;
		} ;
		%this.elTontaClass.setPosition(%this.getPositionX(),%this.getPositionY()) ;
		%this.elTontaClass.start();
	}

	
	Camera.mount($player,"0 0", 0,false);
}