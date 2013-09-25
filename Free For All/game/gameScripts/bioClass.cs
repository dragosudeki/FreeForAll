function bioClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 60 ;
	$player.airborne = false ;
	
	$player.collision = 0.805;
	$player.collision2 = 0.530;
	%this.sideCollision = 0.270;
	
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
	
	moveMap.bindCmd(keyboard, "left", "playerLeft2();", "playerLeftStop2();");
	moveMap.bindCmd(keyboard, "right", "playerRight2();", "playerRightStop2();");
	moveMap.bindCmd(keyboard, "up", "playerJump2();", "");
	moveMap.bindCmd(keyboard, "down" , "playerDown2();","playerDownStop2();");
	moveMap.bindCmd(keyboard, "numpad1", "biomove1();", "");
	moveMap.bindCmd(keyboard, "numpad2", "biomove2();", "");
	moveMap.bindCmd(keyboard, "numpad3", "biomove3();", "");
	moveMap.bindCmd(keyboard, "P", "gamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.270 0.805","0.270 0.805","0.270 -0.530","-0.270 -0.530");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(29.904,31.433);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(BioStand);
	
}
function bioClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($player, "0 0", %force, true);
}

function gamePause2(){
	
	$player.checkPause();	
	
}
function playerDown2()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function playerDownStop2(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function playerLeft2()
{
	if (!$player.isDead)
	{
		$player.moveLeft = true ;
	}
}
function playerLeftStop2()
{
	$player.moveLeft = false ;
}
function playerRight2()
{
	if (!$player.isDead)
	{
		$player.moveRight = true ;	
	}
}
function playerRightStop2()
{
	$player.moveRight = false ;	
}
function playerJump2()
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
function biomove1()
{
	if (!$player.onCool1 && !$player.fire3 && !$player.fire2)
		$player.fire1 = true;	
}
function biomove2()
{
	if (!$player.onCool2 && !$player.fire1 && !$player.fire3)
		$player.fireNote2();	
}
function biomove3()
{
	if (!$player.onCool3 && !$player.fire1 && !$player.fire2)
		$player.fire3 = true;	
}

function bioClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function bioClass::updateH(%this)
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
			if (%this.moveLeft == %this.moveRight || %this.fire1 || %this.fire2 || %this.fire3)
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
function bioClass::updateV(%this)
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
function bioClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function bioClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "BioDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(BioDeath) ;
			}
		
	}
	else if (%this.fire3){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioMove3")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.fireNote3();
					return;
				}
			}else
			{
				%this.playAnimation(BioMove3) ;
			}
		}
	}
	else if (%this.fire2){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioMove2")
			{
				%this.setLinearVelocityY(0);
				if (%this.getIsAnimationFinished())
				{
					
					return;
				}
			}else
			{
				%this.playAnimation(BioMove2) ;
			}
		}
	}
	else if (%this.fire1){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioMove1")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.fireMissile();
					return;
				}
			}else
			{
				%this.playAnimation(BioMove1) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioJump")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(BioJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioFall")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(BioFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioWalk1")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(BioWalk1) ;
					return;
				}
			}else
			{
				%this.playAnimation(BioWalk1) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "BioStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(BioStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(BioStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function bioClass::die(%this)
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
function bioClass::spawn(%this)
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
function bioClass::updateVertical(%this)
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
function bioClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function bioClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function bioClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function bioClass::fireMissile(%this){
	if ($player.onCool1 == false && !$player.isDead){
		if ($player.isLeft == false){
		
			%this.slimeBallClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = slimeBallClass ;
				source = %this ;
			} ;
			%this.slimeBallClass.fire();
			%this.slimeBallClass.setPositionX(%this.getPositionX()+5);
			%this.slimeBallClass.setLinearVelocityX(80);
			
		}else{
				
			%this.slimeBallClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = slimeBallClass ;
				source = %this ;
			} ;
			%this.slimeBallClass.fire();
			%this.slimeBallClass.setPositionX(%this.getPositionX()-5);
			%this.slimeBallClass.setLinearVelocityX(-80);	
			%this.slimeBallClass.setFlip(true,false);	
				
		}
		
		$player.fire1 = false ;
		$player.onCool1 = true ;
		%this.schedule(600,"cooldown1");
	
	}
	
}
function bioClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function bioClass::fireNote2(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.slimePillarClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = slimePillarClass ;
			source = %this ;
		} ;
		%this.slimePillarClass.fire();
		%this.slimePillarClass.setPositionX(%this.getPositionX() + 19);
		
		}else{
			
		%this.slimePillarClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = slimePillarClass ;
			source = %this ;
		} ;
		%this.slimePillarClass.fire();
		%this.slimePillarClass.setPositionX(%this.getPositionX() - 19);	
			
	}
	
	$player.fire2 = true ;
	$player.onCool2 = true ;
	
	}
	
}
function bioClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function bioClass::fireNote3(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.plagueBallClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = plagueBallClass ;
			source = %this ;
		} ;
		%this.plagueBallClass.fire();
		%this.plagueBallClass.setPositionX(%this.getPositionX()+3);
		%this.plagueBallClass.setLinearVelocityX(60);
		
		}else{
			
		%this.plagueBallClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = plagueBallClass ;
			source = %this ;
		} ;
		%this.plagueBallClass.fire();
		%this.plagueBallClass.setPositionX(%this.getPositionX()-3);
		%this.plagueBallClass.setLinearVelocityX(-60);	
		%this.plagueBallClass.setFlip(true,false);	
			
	}
	
	$player.fire3 = false ;
	$player.onCool3 = true ;
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function bioClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function bioClass::note3Hit(%this){
	
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
function bioClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}