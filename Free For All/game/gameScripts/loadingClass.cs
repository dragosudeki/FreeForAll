function loadingClass::onLevelLoaded(%this, %scenegraph){
	%this.px = play1.getPositionX();
	play1.setPositionX(%this.px - 100);
	%this.px2 = play2.getPositionX();
	play2.setPositionX(%this.px2 + 100);
	vstext.setBlendAlpha(0);
	play1.setSize(24,48);
	play2.setSize(24,48);
	
	if ($playerAA $= "fear"){
		play1.setImageMap(fearcover);
		play1.setFlip(false,false);
	}
	else if ($playerAA $= "morte"){
		play1.setImageMap(morteCover);
		play1.setFlip(false,false);
	}
	else if ($playerAA $= "rupes"){
		play1.setImageMap(rupescover);
		play1.setFlip(false,false);
	}
	else if ($playerAA $= "casper"){
		play1.setImageMap(casperbackground);
		play1.setFlip(true,false);
	}
	else if ($playerAA $= "belshazzar"){
		play1.setImageMap(belzzcover);
		play1.setFlip(false,false);
	}
	else if ($playerAA $= "eltonta"){
		play1.setImageMap(eltontacover);
		play1.setFlip(false,false);
	}
	else if ($playerAA $= "yuki"){
		play1.setImageMap(yukicover);
		play1.setFlip(true,false);
	}
	else if ($playerAA $= "dominio"){
		play1.setImageMap(dominioCover);
		play1.setFlip(true,false);
	}
	else if ($playerAA $= "arkaedes"){
		play1.setImageMap(bioCover);
		play1.setFlip(false,false);
	}
	else {
		play1.setImageMap(svriCover);
		play1.setFlip(false,false);
	}
	/////////
	if ($playerBB $= "fear"){
		play2.setImageMap(fearcover);
		play2.setFlip(true,false);
	}
	else if ($playerBB $= "morte"){
		play2.setImageMap(morteCover);
		play2.setFlip(true,false);
	}
	else if ($playerBB $= "rupes"){
		play2.setImageMap(rupescover);
		play2.setFlip(true,false);
	}
	else if ($playerBB $= "casper"){
		play2.setImageMap(casperbackground);
		play2.setFlip(false,false);
	}
	else if ($playerBB $= "belshazzar"){
		play2.setImageMap(belzzcover);
		play2.setFlip(true,false);
	}
	else if ($playerBB $= "eltonta"){
		play2.setImageMap(eltontacover);
		play2.setFlip(true,false);
	}
	else if ($playerBB $= "yuki"){
		play2.setImageMap(yukicover);
		play2.setFlip(false,false);
	}
	else if ($playerAA $= "dominio"){
		play2.setImageMap(dominioCover);
		play2.setFlip(false,false);
	}
	else if ($playerAA $= "arkaedes"){
		play2.setImageMap(bioCover);
		play2.setFlip(true,false);
	}
	else {
		play2.setImageMap(svriCover);
		play2.setFlip(true,false);
	}
	
	
	
	loadtext.text = "Loading";
	%this.loadt = 0 ;
	%this.schedule(500,"changeDot");
	
	%this.setSizeX(0);
	
	%this.loading = true ;
	%this.enableUpdateCallback() ;
}
function loadingClass::changeDot(%this){
	if (%this.loading){
		%this.loadt += 1;
		if (%this.loadt > 3)
			%this.loadt=0;
		
		if (%this.loadt == 0){
			loadtext.text = "Loading";
		}
		else if (%this.loadt == 1){
			loadtext.text = "Loading.";
		}
		else if (%this.loadt == 2){
			loadtext.text = "Loading..";
		}
		else if (%this.loadt == 3){
			loadtext.text = "Loading...";
		}
		
		%this.schedule(500,"changeDot");
	}else
		loadtext.text = "Loading Complete";
}
function loadingClass::onUpdate(%this){
	if (%this.loading){
		%sizex = %this.getSizeX();
		if (%sizex < 100){
			%this.setSizeX(%sizex + 1);
			%this.setPositionX((%sizex/2) - 50);
		}
		else{
			%this.setSizeX(100);
			%this.setPositionX(0);
			%this.loading = false ;
			%this.part = 0;
		}
	}else{
		if (%this.part == 0){
			%px = play1.getPositionX();
			%px2 = play2.getPositionX();
			if (%px <= -12 || %px2 >= 12){
				play1.setLinearVelocityX(125);
				play2.setLinearVelocityX(-125);	
			}
			if (%px >= -12 && %px2 <= 12){
				play1.setLinearVelocityX(0);
				play1.setPositionX(-12);
				play2.setLinearVelocityX(0);
				play2.setPositionX(12);
				
				%this.collisionSplash = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph	;
					class = collisionSplash ;
					source = %this ;
				} ;
				%this.collisionSplash.setPosition(0,-6);
				%this.collisionSplash.appear();
				%this.collisionSplash.setSize(40,40);
				vstext.setBlendAlpha(1.0);
				%this.part = 1 ;
			}
		}
		else if (%this.part == 1){
			%px = play1.getPositionX();
			%px2 = play2.getPositionX();
			if (%px >= -28 && %px2 <= 28){
				play1.setLinearVelocityX(-125);
				play2.setLinearVelocityX(125);	
			}
			if (%px <= -28 && %px2 >= 28){
				play1.setLinearVelocityX(0);
				play1.setPositionX(-28);
				play2.setLinearVelocityX(0);
				play2.setPositionX(28);	
				%this.part = 2 ;
				%this.schedule(1000,"loadLevel");
			}
		}
		
	}
}
function loadingClass::loadLevel(%this){
	if ($level == 1){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Test1.t2d");
        sceneWindow2D.loadLevel($newLevel);
		alxStopAll(); 
		alxPlay("fear");
	}
	else if ($level == 2){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Level2.t2d");
        sceneWindow2D.loadLevel($newLevel);
		alxStopAll(); 
		alxPlay("rupes"); 
	}
	else if ($level == 3){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Test3.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		alxStopAll(); 
		alxPlay("fear");
	}
	else if ($level == 4){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Test4.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		alxStopAll(); 
		alxPlay("pirate");
	}
	else if ($level == 5){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Test5.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		alxStopAll(); 
		alxPlay("desert");
	}
}