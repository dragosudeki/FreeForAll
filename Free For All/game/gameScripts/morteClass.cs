function morteClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 65 ;
	$player.airborne = false ;
	
	$player.collision = 1;
	$player.collision2 = 1;
	%this.sideCollision = 0.643;
	
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
	moveMap.bindCmd(keyboard, "numpad1", "note1();", "");
	moveMap.bindCmd(keyboard, "numpad2", "note2();", "");
	moveMap.bindCmd(keyboard, "numpad3", "note3();", "");
	moveMap.bindCmd(keyboard, "P", "gamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.643 1.000","0.643 1.000","0.643 -1.000","-0.643 -1.000");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(23.824,21.691);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(morteStand);
	
}
function morteClass::solo(%this)
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
function note1()
{
	$player.fireMissile();	
}
function note2()
{
	$player.fireNote2();	
}
function note3()
{
	$player.fireNote3();	
}

function morteClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function morteClass::updateH(%this)
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
			if (%this.moveLeft == %this.moveRight)
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
function morteClass::updateV(%this)
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
function morteClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function morteClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "morteDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(morteDeath) ;
			}
		
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "morteJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(morteJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "morteFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(morteFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "morteWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(morteWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(morteWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "morteStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(morteStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(morteStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function morteClass::die(%this)
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
function morteClass::spawn(%this)
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
function morteClass::updateVertical(%this)
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
function morteClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function morteClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function morteClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function morteClass::fireMissile(%this){
	
	
	if ($player.onCool1 == false && !$player.isDead){
		if ($player.isLeft == false){
		
			%this.note1Class = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = note1Class ;
				bomb = %this ;
			} ;
			%this.note1Class.fire();
			%this.note1Class.setLinearVelocityX(25 + ($player.moveX / 1));
			
		}else{
				
			%this.note1Class = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = note1Class ;
				bomb = %this ;
			} ;
			%this.note1Class.fire();
			%this.note1Class.setLinearVelocityX(-25 + ($player.moveX / 1));		
				
		}
		
		$player.onCool1 = true ;
		%this.schedule(500,"cooldown1");
	
	}
	
}
function morteClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function morteClass::fireNote2(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.note2Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note2Class ;
			bomb = %this ;
		} ;
		%this.note2Class.fire();
		%this.note2Class.setLinearVelocityX(30 + ($player.moveX / 1));
		
		}else{
			
		%this.note2Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note2Class ;
			bomb = %this ;
		} ;
		%this.note2Class.fire();
		%this.note2Class.setLinearVelocityX(-30 + ($player.moveX / 1));		
			
	}
	
	$player.onCool2 = true ;
	%this.schedule(850,"cooldown2");
	
	}
	
}
function morteClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function morteClass::fireNote3(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.note3Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note3Class ;
			bomb = %this ;
		} ;
		%this.note3Class.fire();
		%this.note3Class.setPositionX(%this.note3Class.getPositionX() + (%this.note3Class.getWidth() / 2));
		%this.note3Class.setLinearVelocityX(30);
		
		}else{
			
		%this.note3Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note3Class ;
			bomb = %this ;
		} ;
		%this.note3Class.fire();
		%this.note3Class.setPositionX(%this.note3Class.getPositionX() - (%this.note3Class.getWidth() / 2));
		%this.note3Class.setLinearVelocityX(-30);		
			
	}
	
	$player.onCool3 = true ;
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function morteClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function morteClass::note3Hit(%this){
	
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
function morteClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}