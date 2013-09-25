function dominioClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 65 ;
	$player.airborne = false ;
	
	$player.collision = 0.584;
	$player.collision2 = 0.650;
	%this.sideCollision = 0.2;
	
	$player.moveX = 0 ;
	
	$player.onCool1 = false ;
	$player.onCool2 = false ;
	$player.onCool3 = false ;
	$player.doubleJ = false ;
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = 3 ;
	$player.isDead = false ;
	$player.note3Hit = false ;
	
	moveMap.bindCmd(keyboard, "left", "domiplayerLeft2();", "domiplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "right", "domiplayerRight2();", "domiplayerRightStop2();");
	moveMap.bindCmd(keyboard, "up", "domiplayerJump2();", "");
	moveMap.bindCmd(keyboard, "down" , "domiplayerDown2();","domiplayerDownStop2();");
	moveMap.bindCmd(keyboard, "numpad1", "dompunch1();", "");
	moveMap.bindCmd(keyboard, "numpad2", "domkick();", "");
	moveMap.bindCmd(keyboard, "numpad3", "domupper();", "");
	moveMap.bindCmd(keyboard, "P", "domigamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.200 0.584","0.200 0.584","0.200 -0.650","-0.200 -0.650");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(35.537,35.537);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(dominioStand);
	
}
function dominioClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($player, "0 0", %force, true);
}

function domigamePause2(){
	
	$player.checkPause();	
	
}
function domiplayerDown2()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function domiplayerDownStop2(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function domiplayerLeft2()
{
	if (!$player.isDead)
	{
		$player.moveLeft = true ;
	}
}
function domiplayerLeftStop2()
{
	$player.moveLeft = false ;
}
function domiplayerRight2()
{
	if (!$player.isDead)
	{
		$player.moveRight = true ;	
	}
}
function domiplayerRightStop2()
{
	$player.moveRight = false ;	
}
function domiplayerJump2()
{
	if (!$player.airborne && !$player.isDead)
	{
		$player.setLinearVelocityY(-65) ;
		$player.airborne = true ;
		$player.doubleJ = false ;
	}
	else if (!$player.doubleJ && !$player.isDead){
		$player.doubleJ = true ;
		$player.setLinearVelocityY(-75);
	}
}
function dompunch1()
{
	if (!$player.onPunch2 && !$player.onKick)
		$player.soulPunch1();	
}
function domkick()
{
	if (!$player.onPunch2 && !$player.onPunch1)
		$player.soulKick1();	
}
function domupper()
{
	if (!$player.onPunch1 && !$player.onKick)
		$player.dominioUpperCut();	
}

function dominioClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function dominioClass::updateH(%this)
{
		if ($player.smashX > 0){
			$player.smashX += -1;
		}
		else if ($player.smashX < 0){
			$player.smashX += 1;
		}
		
		if ($player.smashX == 0){ 
			if (%this.moveLeft)
			{
				if(!%this.againstLeftWall && $player.smashX == 0)
				{
				   %this.againstRightWall = false ;
				   $player.isLeft = true ;
				   %this.setLinearVelocityX(-%this.movementSpeed + ($player.moveX/2)) ;
				}
			}
			if (%this.moveRight)
			{
				if (!%this.againstRightWall && $player.smashX == 0)
				{
					$player.isLeft = false ;
					%this.againstLeftWall = false ;
					%this.setLinearVelocityX(%this.movementSpeed + ($player.moveX/2)) ;
				}
			}
			if (%this.moveLeft == %this.moveRight || %this.onPunch1 || %this.onKick || %this.onPunch2)
			{
				if ($player.smashX == 0)
					%this.setLinearVelocityX($player.moveX) ;
				
			}
		}
		else if ($player.smashX != 0){
			
			%this.setLinearVelocityX($player.smashX) ;
			return ;
		}
		
}

// When player is moving up or down
function dominioClass::updateV(%this)
{
	%yVelocity = %this.getLinearVelocityY() ;
	
	%this.setLinearVelocityY(5) ;
	%collision = %this.castCollision(0.005) ;
	%normalX = getWord(%collision, 4) ;
	%normalY = getWord(%collision, 5) ;
	
	if (%normalX != 1){
	
	%this.againstRightWall = false;	
		
	}
	if (%normalX != -1){
		
	%this.againstLeftWall = false ;	
		
	}
	
	// No collision
	if (%collision $= "")
	{
		$player.airborne = true ;
		$player.moveX = 0 ;
		%this.setConstantForceY(100);
		%this.setLinearVelocityY(%yVelocity) ;
		return;
	}
	// collide with wall to the left
	if (%normalX == 1 && %normalY == 0)
	{
		%this.againstLeftWall = true ;
		%this.setLinearVelocityX(0) ;
		%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	// collide with wall to the right
	if (%normalX == -1 && %normalY == 0)
	{
		%this.againstRightWall = true ;
		%this.setLinearVelocityX(0) ;
		%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	// on ground with no wall collisions
	if (%normalY == -1)
	{
		$player.airborne = false ;
		$player.doubleJ = false ;
		%this.againstLeftWall = false ;
		%this.againstRightWall = false ;
		%this.setConstantForceY(0) ;
		%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	// in air and hits platform with its head
	if (%normalY == 1)
	{
		%this.airborne = true ;
		//%this.setLinearVelocityX(0) ;
		//%this.setConstantForceY(100) ;
		//%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	//in case another type of collision normal was detected
	%this.airborne = false ;
	%this.againstLeftWall = false ;
	%this.againstRightWall = false ;
}
//Updates the player's x and y as well as animation onto screen
function dominioClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function dominioClass::setCurrentAnimation(%this)
{
	%yVelocity = %this.getLinearVelocityY();
	if ($player.isLeft == false )
	{
		%this.setFlip(false,false) ;
	}
	if ($player.isLeft == true )
	{
		%this.setFlip(true,false) ;
	}
	
	if ($player.isDead){
		
		if (%this.getAnimationName() $= "dominioDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.setBlendAlpha(0);
					return;
				}
			}else
			{
				%this.playAnimation(dominioDeath) ;
			}
		
	}
	else if (%this.onPunch2){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioUpperCut")
			{
				%this.setLinearVelocityY(0);
				if (%this.getIsAnimationFinished())
				{
					%this.onPunch2 = false ;
					%this.soulPunchClass.safeDelete();
					%this.schedule(3000,"cooldown3");
					return;
				}
			}else
			{
				%this.playAnimation(dominioUpperCut) ;
			}
		}
	}
	else if (%this.onKick){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioKick")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.onKick = false ;
					%this.soulPunchClass.safeDelete();
					%this.schedule(800,"cooldown2");
					return;
				}
			}else
			{
				%this.playAnimation(dominioKick) ;
			}
		}
	}
	else if (%this.onPunch1){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioPunch")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.onPunch1 = false ;
					%this.soulPunchClass.safeDelete();
					%this.schedule(500,"cooldown1");
					return;
				}
			}else
			{
				%this.playAnimation(dominioPunch) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioJump")
			{
				if (%this.getIsAnimationFinished()){
				}
			}else
			{
				%this.playAnimation(dominioJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioFall")
			{
				if (%this.getIsAnimationFinished()){
				}
			}else
			{
				%this.playAnimation(dominioFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(dominioWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(dominioWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "dominioStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(dominioStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(dominioStand) ;
			}
		
	}	
}
//When user loses all health or jumps off map
function dominioClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	$player.airborne = false ;
	$player.isDead = true ;
	%this.setCurrentAnimation() ;
	
	if ($player.lives > 0)
	%this.schedule( 2000, "spawn") ;
	else {
		$timer.endGame2();
	}
}

// When player will die
function dominioClass::spawn(%this)
{
	$player.isDead = false ;
	$player.moveLeft = false ;
	$player.moveRight = false ;
	$player.isAirborne = false ;
	$player.doubleJ = false ;
	$warning1.happened = false;
	%this.setBlendColour(1,1,1,1);
	
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = $player.lives - 1;
	playerLives.text = $player.lives ;
	
	%this.setEnabled(true) ;
}

// When player is moving up or down
function dominioClass::updateVertical(%this)
{
	%yVelocity = %this.getLinearVelocityY() ;
	
	%this.setLinearVelocityY(5) ;
	%collision = %this.castCollision(0.005) ;
	%normalX = getWord(%collision, 4) ;
	%normalY = getWord(%collision, 5) ;
	
	
	if (%normalX != 1){
	
	%this.againstRightWall = false;	
		
	}
	if (%normalX != -1){
		
	%this.againstLeftWall = false ;	
		
	}
	
	// No collision
	if (%collision $= "")
	{
		%this.airborne = true ;
		%this.setConstantForceY(100);
		%this.setLinearVelocityY(%yVelocity) ;
		return;
	}
	// collide with wall to the left
	if (%normalX == 1 && %normalY == 0)
	{
		%this.againstLeftWall = true ;
		//%this.setLinearVelocityX(0) ;
		%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	// collide with wall to the right
	if (%normalX == -1 && %normalY == 0)
	{
		%this.againstRightWall = true ;
		//%this.setLinearVelocityX(0) ;
		%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	// on ground with no wall collisions
	if (%normalY == -1)
	{
		%this.airborne = false ;
		%this.againstLeftWall = false ;
		%this.againstRightWall = false ;
		%this.setConstantForceY(0) ;
		%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	// in air and hits platform with its head
	if (%normalY == 1)
	{
		%this.airborne = true ;
		//%this.setLinearVelocityX(0) ;
		//%this.setConstantForceY(100) ;
		//%this.setLinearVelocityY(%yVelocity) ;
		return ;
	}
	//in case another type of collision normal was detected
	%this.airborne = false ;
	%this.againstLeftWall = false ;
	%this.againstRightWall = false ;
}
// When playercollides with ghost
function dominioClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "pit")
	{
		$player.health = 0 ;
		playerHealth.text = $player.health ;
		%srcObj.die();
	}
	if (%dstObj.class $= "bulletClass")
	{
		if (%dstObj.getPositionX() > %srcObj.getPositionX())
		{
			
		%dstObj.safeDelete();
		%srcObj.jumpL();
			
		}
		else {
			
		%dstObj.safeDelete();
		%srcObj.jumpR();
			
		}
				
		$player.health = $player.health - 8 ;
		playerHealth.text = $player.health ;
		
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
		
		if ($player.health <= 0 && !$player.isDead){
			
			%srcObj.die() ;	
			
		}	
		
	}
	if (%dstObj.class $= "bulletClass2")
	{
		if (%dstObj.getPositionX() > %srcObj.getPositionX())
		{
			
		%dstObj.safeDelete();
		%srcObj.jumpL();
			
		}
		else {
			
		%dstObj.safeDelete();
		%srcObj.jumpR();
			
		}
				
		$player.health = $player.health - 2 ;
		playerHealth.text = $player.health ;
		
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
		
		if ($player.health <= 0 && !$player.isDead){
			
			%srcObj.die() ;	
			
		}	
		
	}
	if (%dstObj.class $= "nukeClass")
	{
		if (%dstObj.getPositionX() > %srcObj.getPositionX())
		{
			
			%dstObj.safeDelete();
			%srcObj.jumpL();
			
		}
		else {
			
			%dstObj.safeDelete();
			%srcObj.jumpR();
			
		}
				
		$player.health = $player.health - 25 ;
		playerHealth.text = $player.health ;
		
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
		
		if ($player.health <= 0 && !$player.isDead){
			
			%srcObj.die() ;	
			
		}	
		
	}
}

// Jumps Left when hit
function dominioClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function dominioClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function dominioClass::soulPunch1(%this){
	
	
	if ($player.onCool1 == false && !$player.isDead){
		if ($player.isLeft == false){
		
			%this.soulPunchClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = soulPunchClass ;
				source = %this ;
				damage = 5;
				left = false ;
			} ;
			%this.soulPunchClass.fire();
			%this.soulPunchClass.setPositionX(%this.getPositionX() + 8.885);
			%this.soulPunchClass.setLinearVelocityX(%this.moveX);
			
		}else{
				
			%this.soulPunchClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = soulPunchClass ;
				source = %this ;
				damage = 5;
				left = true ;
			} ;
			%this.soulPunchClass.fire();
			%this.soulPunchClass.setPositionX(%this.getPositionX() - 8.885);
			%this.soulPunchClass.setLinearVelocityX(%this.moveX);		
				
		}
		$player.onPunch1 = true ;
		$player.onCool1 = true ;
	
	}
	
}
function dominioClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function dominioClass::soulKick1(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 8;
			left = false ;
		} ;
		%this.soulPunchClass.fire();
		%this.soulPunchClass.setPositionX(%this.getPositionX() + 8.885);
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);
		
		}else{
			
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 8;
			left = true ;
		} ;
		%this.soulPunchClass.fire();
		%this.soulPunchClass.setPositionX(%this.getPositionX() - 8.885);
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);	
			
	}
	
	$player.onKick = true ;
	$player.onCool2 = true ;
	%this.schedule(850,"cooldown2");
	
	}
	
}
function dominioClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function dominioClass::dominioUpperCut(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 10;
			left = false;
		} ;
		%this.soulPunchClass.setPositionX(%this.getPositionX() + 8.88);
		%this.soulPunchClass.fire();
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);
		
		}else{
			
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 10;
			left = true;
		} ;
		%this.soulPunchClass.setPositionX(%this.getPositionX() - 8.88);
		%this.soulPunchClass.fire();
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);		
			
	}
	
	$player.onPunch2 = true ;
	$player.onCool3 = true ;
	
	}
}
function dominioClass::cooldown3(%this){
	$player.onCool3 = false ;	
}
function dominioClass::note3Hit(%this){
	
	if ($player.note3Hit == false){
		
		$player.health = $player.health - 2 ;
		
		if ($player.health <= 0 && $player.isDead == false){
			%this.die();
		}
		
		PlayerHealth.text = $player.health ;
		$player.note3Hit = true ;
		
		%this.schedule(300,"note3CD");
	}
}
function dominioClass::note3CD(%this){
	$player.note3Hit = false ;
}