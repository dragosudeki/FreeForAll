function enemyThrow::onLevelLoaded(%this)
{
	%this.startX = %this.getPositionX();
	%this.startY = %this.getPositionY();
	
	%this.setLayer(2);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 17 19 20 21 22 23 24 25");
	
	if (%this.getAnimationName() $= "rupesStand"){
		%this.anime = "rupes";	
		%this.setSize(27.652,27.652);
		%this.setCollisionPolyCustom(4,"-0.521 0.663","0.521 0.663","0.521 -0.663","-0.521 -0.663");
	}else{
		%this.anime = "rupes";	
	}	
	%this.fireMissile();
	
	%this.enableUpdateCallback();
}
function enemyThrow::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}
//Updates the player's x and y as well as animation onto screen
function enemyThrow::onUpdate(%this)
{
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function enemyThrow::setCurrentAnimation(%this)
{
	if (%this.anime $= "rupes"){
		if (%this.death){
			// Shoot
			if (%this.getAnimationName() $= "rupesDeath"){
				if (%this.getIsAnimationFinished()){
					%this.setEnabled(false);
					return;
				}
			}else{
				%this.playAnimation(rupesDeath) ;
			}
		}
		else if (%this.fireNow){
			// Shoot
			if (%this.getAnimationName() $= "rupesShootAnimation"){
				if (%this.getIsAnimationFinished()){
					%this.fireMissile();
					return;
				}
			}else{
				%this.playAnimation(rupesShootAnimation) ;
			}
		}
		else {
			// Stand
			if (%this.getAnimationName() $= "rupesStandAnimation"){
				if (%this.getIsAnimationFinished()){
					%this.playAnimation(rupesStandAnimation) ;
					return;
				}
			}else{
				%this.playAnimation(rupesStandAnimation) ;
			}
		}
	}
				
}
//When user loses all health or jumps off map
function enemyThrow::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	%this.death = true ;
	%this.setCurrentAnimation() ;
	
	//%this.schedule( 60000, "spawn") ;
}

// When player will die
function enemyThrow::spawn(%this)
{
	%this.setEnabled(true);
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	%this.setEnabled(true) ;
}
// When person gets hit
function enemyThrow::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "pit")
	{
		%srcObj.die();
	}
	if (%dstObj.class $= "bulletClass")
	{
		%srcObj.collisionSplash = new t2dAnimatedSprite()
		{
			scenegraph = %srcObj.scenegraph	;
			class = collisionSplash ;
			source = %srcObj ;
		} ;
		%x = getWord(%contacts,0);
		%y = getWord(%contacts,1);
		%srcObj.collisionSplash.setPosition(%x,%y);
		%srcObj.collisionSplash.appear();
		
		%dstObj.delete();
		%srcObj.die();	
		
	}
	if (%dstObj.class $= "bulletClass2")
	{
		%srcObj.collisionSplash = new t2dAnimatedSprite()
		{
			scenegraph = %srcObj.scenegraph	;
			class = collisionSplash ;
			source = %srcObj ;
		} ;
		%x = getWord(%contacts,0);
		%y = getWord(%contacts,1);
		%srcObj.collisionSplash.setPosition(%x,%y);
		%srcObj.collisionSplash.appear();
		
		%dstObj.delete();
		%srcObj.die();
		
	}
	else if (%dstObj.class $= "note1Class" )
	{
		
		%srcObj.collisionSplash = new t2dAnimatedSprite()
		{
			scenegraph = %srcObj.scenegraph	;
			class = collisionSplash ;
			source = %srcObj ;
		} ;
		%x = getWord(%contacts,0);
		%y = getWord(%contacts,1);
		%srcObj.collisionSplash.setPosition(%x,%y);
		%srcObj.collisionSplash.appear();
		
		%dstObj.delete();
		%srcObj.die();
	}
	else if (%dstObj.class $= "note2Class" )
	{
		
		%srcObj.collisionSplash = new t2dAnimatedSprite()
		{
			scenegraph = %srcObj.scenegraph	;
			class = collisionSplash ;
			source = %srcObj ;
		} ;
		%x = getWord(%contacts,0);
		%y = getWord(%contacts,1);
		%srcObj.collisionSplash.setPosition(%x,%y);
		%srcObj.collisionSplash.appear();
		
		%dstObj.delete();
		%srcObj.die();
	}
	if (%dstObj.class $= "leafClass")
	{
		%srcObj.collisionSplash = new t2dAnimatedSprite()
		{
			scenegraph = %srcObj.scenegraph	;
			class = collisionSplash ;
			source = %srcObj ;
		} ;
		%x = getWord(%contacts,0);
		%y = getWord(%contacts,1);
		%srcObj.collisionSplash.setPosition(%x,%y);
		%srcObj.collisionSplash.appear();
		
		%dstObj.delete();
		%srcObj.die();
		
	}
	if (%dstObj.class $= "swordClass")
	{	
		%srcObj.collisionSplash = new t2dAnimatedSprite()
		{
			scenegraph = %srcObj.scenegraph	;
			class = collisionSplash ;
			source = %srcObj ;
		} ;
		%x = getWord(%contacts,0);
		%y = getWord(%contacts,1);
		%srcObj.collisionSplash.setPosition(%x,%y);
		%srcObj.collisionSplash.appear();
		
		%dstObj.delete();
		%srcObj.die();
		
	}
	if (%dstObj.class $= "nukeClass")
	{
		%srcObj.collisionSplash = new t2dAnimatedSprite()
		{
			scenegraph = %srcObj.scenegraph	;
			class = collisionSplash ;
			source = %srcObj ;
		} ;
		%x = getWord(%contacts,0);
		%y = getWord(%contacts,1);
		%srcObj.collisionSplash.setPosition(%x,%y);
		%srcObj.collisionSplash.appear();
		
		%dstObj.delete();
		%srcObj.die();	
		
	}
}

// Jumps Left when hit
function enemyThrow::jumpL(%this){
	
	//%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function enemyThrow::jumpR(%this){
	
	//%this.setPositionX(%this.getPositionX() + 6);
	
}

function enemyThrow::fireMissile(%this){
	
	if (%this.anime $= "rupes"){
		%this.leafClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = leafClass ;
			source = %this ;
		} ;
		%this.leafClass.fire2();	
		%px = $player.getPositionX();
		
		if (%px > %this.startX){
			%this.leafClass.setLinearVelocityX(30);
			%this.setFlip(false,false);
		}else{
			%this.leafClass.setLinearVelocityX(-30);
			%this.setFlip(true,false);
			%this.leafClass.setFlip(true,false);
		}
		
		%this.fireNow = false ;
		%randNum = getRandom(2500,3000);
		%this.schedule(%randNum,"fireAgain");
	}
	
}
function enemyThrow::fireAgain(%this){
	%this.fireNow = true;
	%this.setCurrentAnimation();	
}
function enemyThrow::note3Hit(%this){
	%this.die();
}