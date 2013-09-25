function casperClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 40 ;
	$player.airborne = false ;
	$player.collision = 0.869;
	$player.collision2 = 0.673;
	$player.sideCollision = 0.555;
	
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
	
	moveMap.bindCmd(keyboard, "left", "playerLeft24();", "playerLeftStop24();");
	moveMap.bindCmd(keyboard, "right", "playerRight24();", "playerRightStop24();");
	moveMap.bindCmd(keyboard, "up", "playerJump24();", "");
	moveMap.bindCmd(keyboard, "down" , "playerDown24();","playerDownStop24();");
	moveMap.bindCmd(keyboard, "numpad1", "bomb();", "");
	moveMap.bindCmd(keyboard, "numpad2", "note2();", "");
	moveMap.bindCmd(keyboard, "numpad3", "nuke();", "");
	moveMap.bindCmd(keyboard, "P", "gamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.555 -0.673","0.555 -0.673","0.555 0.869","-0.555 0.869");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(24.34,24.34);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(casperStand);
	
}
function casperClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($player, "0 0", %force, true);
}

function gamePause24(){
	
	$player.checkPause();	
	
}
function playerDown24()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function playerDownStop24(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function playerLeft24()
{
	if (!$player.isDead)
	{
		$player.moveLeft = true ;
	}
}
function playerLeftStop24()
{
	$player.moveLeft = false ;
}
function playerRight24()
{
	if (!$player.isDead)
	{
		$player.moveRight = true ;	
	}
}
function playerRightStop24()
{
	$player.moveRight = false ;	
}
function playerJump24()
{
	if (!$player.airborne && !$player.isDead)
	{
		$player.setLinearVelocityY(-75) ;
		$player.airborne = true ;
		$player.doubleJ = false ;
	}
	else if (!$player.doubleJ && !$player.isDead){
		
		$player.doubleJ = true ;
		$player.setLinearVelocityY(-65);
		
	}
}
function bomb()
{
	if (!$player.isNuke && !$player.onSword)
	$player.fireBomb();	
}
function note2()
{
	if (!$player.isNuke && !$player.isThrowing)
	$player.fireNote2();	
}
function nuke()
{
	if (!$player.isThrowing && !$player.onSword)
	$player.callNuke();	
}

function casperClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function casperClass::updateH(%this)
{
		if ($player.smashX > 0){
			$player.smashX += -2;
		}
		else if ($player.smashX < 0){
			$player.smashX += 2;
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
			if (%this.moveLeft == %this.moveRight || $player.isThrowing || $player.onSword || $player.isNuke)
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
function casperClass::updateV(%this)
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
function casperClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function casperClass::setCurrentAnimation(%this)
{
	%yVelocity = %this.getLinearVelocityY();
	
	if ($player.isLeft == false )
	{
		%this.setFlip(true,false) ;
	}
	if ($player.isLeft == true )
	{
		%this.setFlip(false,false) ;
	}
	
	if ($player.isDead){
		
		if (%this.getAnimationName() $= "casperDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(casperDeath) ;
			}
		
	}
	else if ($player.isNuke){	
		if (%this.getAnimationName() $= "casperNuke")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(casperNuke) ;
					return;
				}
			}else
			{
				%this.playAnimation(casperNuke) ;
			}
	}
	else if ($player.onSword){
		
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperSword")
			{
				if (%this.getIsAnimationFinished())
				{
					$player.onSword = false ;
					%this.swordClass.delete();
				}
			}else
			{
				%this.playAnimation(casperSword) ;
			}
		}
		
	}
	else if ($player.isThrowing){
		
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperThrow")
			{
				if (%this.getIsAnimationFinished())
				{
					$player.isThrowing = false ;
				}
			}else
			{
				%this.playAnimation(casperThrow) ;
			}
		}
		
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(casperJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(casperFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(casperWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(casperWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "casperStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(casperStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(casperStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function casperClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	$player.airborne = false ;
	$player.isDead = true ;
	
	%this.casperDeathClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = casperDeathClass ;
		source = %this ;
	} ;
	%this.casperDeathClass.fire();
	%this.casperDeathClass.setPositionX(%this.getPositionX());
	
	%this.setCurrentAnimation() ;
	
}
function casperClass::die2(%this)
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
function casperClass::respawn(%this){
	if ($player.lives > 0)
		%this.schedule( 2000, "spawn") ;
	else {
		$timescale = 0;
		canvas.pushDialog(RoundEnd);
		if (end.text $= "Player 1 Wins"){
			end.text = "Tie";
		}
		else
		end.text = "Player 2 Wins";
	}	
	%this.setEnabled(false);
}

// When player will die
function casperClass::spawn(%this)
{
	$player.isDead = false ;
	$player.moveLeft = false ;
	$player.moveRight = false ;
	$player.isAirborne = false ;
	$player.doubleJ = false ;
	$warning1.happened = false;
	%this.setBlendColour(1,1,1,1);
	
	%this.setEnabled(true);
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = $player.lives - 1;
	playerLives.text = $player.lives ;
	
	%this.setEnabled(true) ;
}

// When player is moving up or down
function casperClass::updateVertical(%this)
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
function casperClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
		if (%dstObj.class $= "pit")
		{
			$player.health = 0 ;
			playerHealth.text = $player.health ;
			%srcObj.die2();
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
		if (%dstObj.class $= "swordClass")
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
function casperClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function casperClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function casperClass::fireBomb(%this){
	
	
	if ($player.onCool1 == false && !$player.isDead){
		if ($player.isLeft == false){
		
				%this.bulletClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = bulletClass ;
					enemy1 = %this ;
				} ;
				%this.bulletClass.fire1();
				%this.bulletClass.setPositionX(%this.getPositionX() - 8) ;
				%this.bulletClass.setPositionY(%this.getPositionY() - 7) ;
				%this.bulletClass.setLinearVelocityX(30 + ($player.moveX / 1));
			
			}else{
				
				%this.bulletClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = bulletClass ;
					enemy1 = %this ;
				} ;
				%this.bulletClass.fire1();
				%this.bulletClass.setPositionX(%this.getPositionX() + 8) ;
				%this.bulletClass.setPositionY(%this.getPositionY() - 7) ;
				%this.bulletClass.setLinearVelocityX(-30 + ($player.moveX / 1));		
				
		}
	
	$player.onCool1 = true ;
	$player.isThrowing = true ;
	%this.schedule(600,"cooldown1");
	
	}
	
}
function casperClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function casperClass::fireNote2(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead){
		if ($player.isLeft == false){
				
				
				%this.swordClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = swordClass ;
					source = %this ;
				} ;
				%this.swordClass.fire();
				%this.swordClass.setPositionX(%this.getPositionX() + 6.085);
				
			}else{
					
				%this.swordClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = swordClass ;
					source = %this ;
				} ;	
				%this.swordClass.fire();
				%this.swordClass.setPositionX(%this.getPositionX() - 6.085);	
					
			}
			
			$player.onSword = true ;
			$player.isThrowing = false ;
			$player.onCool2 = true ;
			%this.schedule(750,"cooldown2");
		
		}
	
}
function casperClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function casperClass::callNuke(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.nukeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = nukeClass ;
			source = %this ;
		} ;
		%this.nukeClass.fire();
		%this.nukeClass.setFlip(true,false);
		%this.nukeClass.setPositionX(%this.getPositionX() + 40);
		
		}else{
			
		%this.nukeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = nukeClass ;
			source = %this ;
		} ;
		%this.nukeClass.fire();
		%this.nukeClass.setPositionX(%this.getPositionX() - 40);
			
	}
	
	$player.isNuke = true ;
	$player.onCool3 = true ;
	%this.schedule(500,"outtaNuke");
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function casperClass::outtaNuke(%this){

	$player.isNuke = false ;
		
	
}
function casperClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function casperClass::note3Hit(%this){
	
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
function casperClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}