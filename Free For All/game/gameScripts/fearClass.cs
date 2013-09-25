function fearClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 60 ;
	$player.airborne = false ;
	
	$player.collision = 0.80;
	$player.collision2 = 0.643;
	%this.sideCollision = 0.320;
	
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
	
	moveMap.bindCmd(keyboard, "left", "feplayerLeft2();", "feplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "right", "feplayerRight2();", "feplayerRightStop2();");
	moveMap.bindCmd(keyboard, "up", "feplayerJump2();", "");
	moveMap.bindCmd(keyboard, "down" , "feplayerDown2();","feplayerDownStop2();");
	moveMap.bindCmd(keyboard, "numpad1", "fearHit1();", "");
	moveMap.bindCmd(keyboard, "numpad2", "fearCycle2();", "");
	moveMap.bindCmd(keyboard, "numpad3", "fearFire3();", "");
	moveMap.bindCmd(keyboard, "P", "fegamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.320 0.800","0.320 0.800","0.320 -0.643","-0.320 -0.643");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(27.167,28.556);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(fearStand);
	
}
function fearClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($player, "0 0", %force, true);
}

function fegamePause2(){
	
	$player.checkPause();	
	
}
function feplayerDown2()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function feplayerDownStop2(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function feplayerLeft2()
{
	if (!$player.isDead)
	{
		$player.moveLeft = true ;
	}
}
function feplayerLeftStop2()
{
	$player.moveLeft = false ;
}
function feplayerRight2()
{
	if (!$player.isDead)
	{
		$player.moveRight = true ;	
	}
}
function feplayerRightStop2()
{
	$player.moveRight = false ;	
}
function feplayerJump2()
{
	if (!$player.inCycle && !$player.onMove1)
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
function fearHit1()
{
	if (!$player.inCycle && !$player.onMove3)
	$player.attack1();	
}
function fearCycle2()
{
	if (!$player.onMove1 && !$player.onMove3)
	$player.attackCycle();	
}
function fearFire3()
{
	if (!$player.onMove1 && !$player.inCycle)
	$player.fearAttack3();	
}

function fearClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function fearClass::updateH(%this)
{
	if (!$player.onMove3){
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
					   %this.setLinearVelocityX(-%this.movementSpeed + $player.moveX) ;
					}
				}
				if (%this.moveRight)
				{
					if (!%this.againstRightWall && $player.smashX == 0)
					{
						$player.isLeft = false ;
						%this.againstLeftWall = false ;
						%this.setLinearVelocityX(%this.movementSpeed + $player.moveX) ;
					}
				}
				if (%this.moveLeft == %this.moveRight || %this.onMove1 || %this.inCycle || %this.onMove3)
				{
					if ($player.smashX == 0)
						%this.setLinearVelocityX($player.moveX) ;
					
				}
			}
			else if ($player.smashX != 0){
				
				%this.setLinearVelocityX($player.smashX) ;
				return ;
			}
	}else{
		if ($player.isLeft == false){
			%this.setLinearVelocityX(100);
		}else{
			%this.setLinearVelocityX(-100);
		}
		if(%this.againstLeftWall || %this.againstRightWall){
			%this.outtaMove3Charge();
		}	
			
	}
		
}

// When player is moving up or down
function fearClass::updateV(%this)
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
function fearClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function fearClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "fearDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.setBlendAlpha(0);
					return;
				}
			}else
			{
				%this.playAnimation(fearDeath) ;
			}
	}
	else if ($player.onMove3){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearFireAttack")
			{
				if (%this.getIsAnimationFinished()){
					return;
				}
			}else
			{
				%this.playAnimation(fearFireAttack) ;
			}
		}
	}
	else if ($player.inCycle){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearAttack2Animation")
			{
				if (%this.getIsAnimationFinished()){
					$player.cycleCount += 1 ;
					if ($player.cycleCount >= 2){
						%this.outtaCycle();
					}else{
						%this.playAnimation(fearAttack2Animation) ;	
					}
					return;
				}
			}else
			{
				%this.playAnimation(fearAttack2Animation) ;
			}
		}
	}
	else if ($player.onMove1){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearAttack")
			{
				if (%this.getIsAnimationFinished()){
					$player.onMove1=false;
					%this.fearStaffClass.safeDelete();
					return;
				}
			}else
			{
				%this.playAnimation(fearAttack) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(fearJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(fearFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(fearWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(fearWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "fearStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(fearStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(fearStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function fearClass::die(%this)
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
function fearClass::spawn(%this)
{
	$player.isDead = false ;
	$player.moveLeft = false ;
	$player.moveRight = false ;
	$player.isAirborne = false ;
	$player.doubleJ = false ;
	$warning1.happened = false;
	
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	%this.setBlendColour(1,1,1,1);
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = $player.lives - 1;
	playerLives.text = $player.lives ;
	
	%this.setEnabled(true) ;
}

// When player is moving up or down
function fearClass::updateVertical(%this)
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
function fearClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $Bplayer){
		if ($player.onMove3){
			if (%dstObj.getPositionX() > %srcObj.getPositionX()){
				%dstObj.smashX += 20;
			}else {
				%dstObj.smashX += -20;
			}
						
			%dstObj.health = %dstObj.health - 15 ;
			player2Health.text = %dstObj.health ;
				
			if (%dstObj.health <= 0 && !$Bplayer.isDead){
				
				%dstObj.die() ;	
				
			}
			
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
			%srcObj.outtaMove3Charge();
		}
	}
	else if (%dstObj.class $= "pit")
	{
		$player.health = 0 ;
		playerHealth.text = $player.health ;
		%srcObj.die();
	}
	else if (%dstObj.class $= "bulletClass")
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
	else if (%dstObj.class $= "bulletClass2")
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
	else if (%dstObj.class $= "nukeClass")
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
function fearClass::jumpL(%this){
	if (!$player.inCycle || !$player.onMove3)
		%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function fearClass::jumpR(%this){
	if (!$player.inCycle || !$player.onMove3)
		%this.setPositionX(%this.getPositionX() + 6);
	
}

function fearClass::attack1(%this){
	if ($player.onCool1 == false && !$player.isDead){
		
		%this.schedule(100,"createStaffClass");
		$player.onMove1 = true ;
		$player.onCool1 = true ;
		%this.schedule(500,"cooldown1");
		
	}
	
}
function fearClass::createStaffClass(%this){
	if ($player.isLeft == false && !$player.isDead){	
			%this.fearStaffClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = fearStaffClass ;
				source = %this ;
			} ;
			%this.fearStaffClass.fire();
			%this.fearStaffClass.setPositionX(%this.getPositionX() + 5.89);
				
		}else{
					
			%this.fearStaffClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = fearStaffClass ;
				source = %this ;
			} ;	
			%this.fearStaffClass.fire();
			%this.fearStaffClass.setPositionX(%this.getPositionX() - 5.89);	
					
		}
}
function fearClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function fearClass::attackCycle(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.fearCircleClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = fearCircleClass ;
			source = %this ;
		} ;
		%this.fearCircleClass.fire();
		%this.fearCircleClass.setPositionX(%this.getPositionX() + 5.89);	
		
		}else{
			
		%this.fearCircleClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = fearCircleClass ;
			source = %this ;
		} ;
		%this.fearCircleClass.fire();
		%this.fearCircleClass.setPositionX(%this.getPositionX() - 5.89);			
			
	}
	
	$player.onCool2 = true ;
	$player.inCycle = true ;
	$player.cycleCount = 0 ;
	
	}
	
}
function fearClass::outtaCycle(%this){
	if ($player.inCycle && !$player.isDead){
		$player.inCycle = false ;
		%this.fearCircleClass.safeDelete();
		%this.schedule(850,"cooldown2");
	}
}
function fearClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function fearClass::fearAttack3(%this){
	if ($player.onCool3 == false && !$player.isDead){
		
		$player.onMove3 = true ;
		$player.onCool3 = true ;
		%this.schedule(500,"outtaMove3Charge");
		
	}
}
function fearClass::outtaMove3Charge(%this){
	if (%this.onMove3){
		%this.onMove3 = false ;
		%this.schedule(4500,"cooldown3");
	}
}
function fearClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function fearClass::note3Hit(%this){
	
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
function fearClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}
