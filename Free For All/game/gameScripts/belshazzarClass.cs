function belshazzarClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 70 ;
	$player.airborne = false ;
	
	$player.collision = 0.573;
	$player.collision2 = 0.573;
	%this.sideCollision = 0.340;
	
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
	
	moveMap.bindCmd(keyboard, "left", "belshplayerLeft2();", "belshplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "right", "belshplayerRight2();", "belshplayerRightStop2();");
	moveMap.bindCmd(keyboard, "up", "belshplayerJump2();", "");
	moveMap.bindCmd(keyboard, "down" , "belshplayerDown2();","belshplayerDownStop2();");
	moveMap.bindCmd(keyboard, "numpad1", "card();", "");
	moveMap.bindCmd(keyboard, "numpad2", "cardFall();", "");
	moveMap.bindCmd(keyboard, "numpad3", "cardWave();", "");
	moveMap.bindCmd(keyboard, "P", "belshgamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.340 0.573","0.340 0.573","0.340 -0.573","-0.340 -0.573");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(30.163,31.670);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(belshazzarStand);
	
}
function belshazzarClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($player, "0 0", %force, true);
}

function belshgamePause2(){
	
	$player.checkPause();	
	
}
function belshplayerDown2()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function belshplayerDownStop2(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function belshplayerLeft2()
{
	if (!$player.isDead)
	{
		$player.moveLeft = true ;
	}
}
function belshplayerLeftStop2()
{
	$player.moveLeft = false ;
}
function belshplayerRight2()
{
	if (!$player.isDead)
	{
		$player.moveRight = true ;	
	}
}
function belshplayerRightStop2()
{
	$player.moveRight = false ;	
}
function belshplayerJump2()
{
	if (!$player.inCard){
		if (!$player.airborne && !$player.isDead)
		{
			$player.setLinearVelocityY(-70) ;
			$player.airborne = true ;
			$player.doubleJ = false ;
		}
		else if (!$player.doubleJ && !$player.isDead){
			
			$player.doubleJ = true ;
			$player.setLinearVelocityY(-70);
			
		}
	}
}
function card()
{
	if (!$player.isWave && !$player.inCard)
	$player.fireMissile();	
}
function cardFall()
{
	if (!$player.isWave && !$player.throwCard)
	$player.fallingCard();	
}
function cardWave()
{
	if (!$player.onCool3 && !$player.inCard && !$player.throwCard){
		$player.isWave = true ;
		$player.setLinearVelocityX(0);
	}
}

function belshazzarClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function belshazzarClass::updateH(%this)
{
		if ($player.smashX > 0){
			$player.smashX += -1;
		}
		else if ($player.smashX < 0){
			$player.smashX += 1;
		}
		if (!$player.inCard && !$player.isWave){
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
		
}

// When player is moving up or down
function belshazzarClass::updateV(%this)
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
function belshazzarClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function belshazzarClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "belshDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(belshDeath) ;
			}
		
	}
	else if ($player.isWave){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarSmack")
			{
				if (%this.getIsAnimationFinished())
				{
					$player.groundWave();
				}
			}else
			{
				%this.playAnimation(belshazzarSmack) ;
			}
		}
	}
	else if ($player.inCard){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarCard")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(belshazzarCard) ;
			}
		}
	}
	else if ($player.throwCard){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarThrow")
			{
				if (%this.getIsAnimationFinished())
				{
					$player.throwCard = false ;
				}
			}else
			{
				%this.playAnimation(belshazzarThrow) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(belshazzarJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(belshazzarFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarRun")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(belshazzarRun) ;
					return;
				}
			}else
			{
				%this.playAnimation(belshazzarRun) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "belshazzarStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(belshazzarStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(belshazzarStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function belshazzarClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	$player.airborne = false ;
	$player.isDead = true ;
	$player.inCard = false;
	$player.isWave=false;
	$player.throwCard=false;
	%this.setCurrentAnimation() ;
	
	if ($player.lives > 0)
	%this.schedule( 2000, "spawn") ;
	else {
		$timer.endGame2();
	}
}

// When player will die
function belshazzarClass::spawn(%this)
{
	$player.isDead = false ;
	$player.moveLeft = false ;
	$player.moveRight = false ;
	$player.isAirborne = false ;
	$player.doubleJ = false ;
	
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = $player.lives - 1;
	playerLives.text = $player.lives ;
	
	%this.setEnabled(true) ;
}

// When player is moving up or down
function belshazzarClass::updateVertical(%this)
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
function belshazzarClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $Bplayer){
		
		if ($player.inCard){
			if (%dstObj.getPositionX() < %srcObj.getPositionX())
			{
				
			%dstObj.jumpL();
				
			}
			else {
				
			%dstObj.jumpR();
				
			}
					
			$Bplayer.health = $Bplayer.health - 8 ;
			player2Health.text = $Bplayer.health ;
			
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
			
			if ($Bplayer.health <= 0 && !$Bplayer.isDead){
				
				%dstObj.die() ;	
				
			}
			
			$player.inCard = false ;
			$player.onCool2 = true ;
			%srcObj.schedule(2000,"cooldown2");
			
		}
		
	}
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
function belshazzarClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function belshazzarClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function belshazzarClass::fireMissile(%this){
	
	
	if ($player.onCool1 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.cardClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = cardClass ;
			source = %this ;
		} ;
		%this.cardClass.fire();
		%this.cardClass.setLinearVelocityX(70 + ($player.moveX / 1));
		
		}else{
			
		%this.cardClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = cardClass ;
			source = %this ;
		} ;
		%this.cardClass.fire();
		%this.cardClass.setLinearVelocityX(-70 + ($player.moveX / 1));		
			
	}
	
	$player.throwCard = true ;
	$player.onCool1 = true ;
	%this.schedule(500,"cooldown1");
	
	}
	
}
function belshazzarClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function belshazzarClass::fallingCard(%this){
	
	if ($player.inCard == false){
		if ($player.onCool2 == false && !$player.isDead){
			
			$player.inCard = true ;
			%this.setLinearVelocityY(0);
			%this.setLinearVelocityX(0);
			%this.schedule(300,"fall");
			
		}
	}else{
		$player.inCard = false ;
		$player.onCool2 = true ;
		%this.schedule(2000,"cooldown2");
	}
	
}
function belshazzarClass::fall(%this){

	%this.setLinearVelocityY(200);	
	
}
function belshazzarClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function belshazzarClass::groundWave(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.waveClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = waveClass ;
			source = %this ;
			goLeft = false;
			number = 3;
			startY = %this.getPositionY() + 31.670/2;
		} ;
		%this.waveClass.fire();
		%this.waveClass.setPositionX(%this.getPositionX() + 17.5);
		
		}else{
			
		%this.waveClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = waveClass ;
			source = %this ;
			goLeft = true;
			number = 3;
			startY = %this.getPositionY() + 31.670/2;
		} ;
		%this.waveClass.fire();
		%this.waveClass.setPositionX(%this.getPositionX() - 17.5);
			
	}
	$player.isWave = false ;
	$player.onCool3 = true ;
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function belshazzarClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function belshazzarClass::note3Hit(%this){
	
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
function belshazzarClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}