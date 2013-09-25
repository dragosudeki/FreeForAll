function yukiClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 80 ;
	$player.airborne = false ;
	
	$player.collision = 1;
	$player.collision2 = 0.8;
	%this.sideCollision = 0.5;
	
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
	
	moveMap.bindCmd(keyboard, "left", "yuplayerLeft2();", "yuplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "right", "yuplayerRight2();", "yuplayerRightStop2();");
	moveMap.bindCmd(keyboard, "up", "yuplayerJump2();", "");
	moveMap.bindCmd(keyboard, "down" , "yuplayerDown2();","yuplayerDownStop2();");
	moveMap.bindCmd(keyboard, "numpad1", "fireEnergyBolt();", "");
	moveMap.bindCmd(keyboard, "numpad2", "move2();", "");
	moveMap.bindCmd(keyboard, "numpad3", "star3();", "");
	moveMap.bindCmd(keyboard, "P", "yugamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.5 1.000","0.5 1.000","0.5 -0.8","-0.5 -0.8");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(23.56,24.76);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(yukiSelect);
	
}
function yukiClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($player, "0 0", %force, true);
}

function yugamePause2(){
	
	$player.checkPause();	
	
}
function yuplayerDown2()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function yuplayerDownStop2(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function yuplayerLeft2()
{
	if (!$player.isDead)
	{
		$player.moveLeft = true ;
	}
}
function yuplayerLeftStop2()
{
	$player.moveLeft = false ;
}
function yuplayerRight2()
{
	if (!$player.isDead)
	{
		$player.moveRight = true ;	
	}
}
function yuplayerRightStop2()
{
	$player.moveRight = false ;	
}
function yuplayerJump2()
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
function fireEnergyBolt()
{
	if (!$player.shootBolt && !$player.starFire && !$player.spinBroom){
		$player.shootBolt = true;
		$player.movementSpeed = 70 ;
	}
}
function move2()
{
	if (!$player.shootBolt && !$player.starFire && !$player.onCool2 && !$player.spinBroom){
		$player.useMove2();	
	}
}
function star3()
{
	if (!$player.shootBolt && !$player.starFire && !$player.onCool3 && !$player.spinBroom){
		$player.movementSpeed = 60 ;
		$player.starFire = true ;	
	}
}

function yukiClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function yukiClass::updateH(%this)
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
				   if (!$player.rotation)
				   $player.isLeft = true ;
				   %this.setLinearVelocityX(-%this.movementSpeed + ($player.moveX/2)) ;
				}
			}
			if (%this.moveRight)
			{
				if (!%this.againstRightWall && $player.smashX == 0)
				{
					if (!$player.rotation)
					$player.isLeft = false ;
					%this.againstLeftWall = false ;
					%this.setLinearVelocityX(%this.movementSpeed + ($player.moveX/2)) ;
				}
			}
			if (%this.moveLeft == %this.moveRight || $player.rotation)
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
function yukiClass::updateV(%this)
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
function yukiClass::onUpdate(%this)
{
	if (!$player.isDead){
		
		%this.updateH();
		%this.updateV();
		if ($player.rotation){
			if (%this.getRotation() >= 350 && $player.isLeft){
				%this.broomClass.safeDelete();
				%this.setAutoRotation(0);
				%this.setRotation(0);
				$player.spinBroom = false ;
				%this.schedule(1500,"cooldown2");	
			}
			if (%this.getRotation() <= -350 && !$player.isLeft){
				%this.broomClass.safeDelete();
				%this.setAutoRotation(0);
				%this.setRotation(0);
				$player.spinBroom = false ;
				%this.schedule(1500,"cooldown2");	
			}
			%this.setLinearVelocity(0,-1);
		}
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function yukiClass::setCurrentAnimation(%this)
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
		
		%this.setBlendAlpha(0);
		
	}
	else if ($player.starFire){
		if (%this.getAnimationName() $= "yukiAttack")
		{
			if (%this.getIsAnimationFinished())
			{
				%this.startStar() ;
				return;
			}
		}else{
			%this.playAnimation(yukiAttack) ;
		}	
	}
	else if ($player.shootBolt){
		if (%this.getAnimationName() $= "yukiAttack")
		{
			if (%this.getIsAnimationFinished())
			{
				%this.fireBolt() ;
				$player.movementSpeed = 80;
				$player.shootBolt = false ;
				return;
			}
		}else{
			%this.playAnimation(yukiAttack) ;
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "yukiFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(yukiFall) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "yukiJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(yukiJump) ;
			}
		}
	}
	
	else {
		if (%this.getAnimationName() $= "yukiStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(yukiStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(yukiStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function yukiClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	%this.smokeSourceClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = smokeSourceClass ;
		source = %this ;
	} ;
	%this.smokeSourceClass.start();
	
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
function yukiClass::spawn(%this)
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
function yukiClass::updateVertical(%this)
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
function yukiClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function yukiClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function yukiClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function yukiClass::fireBolt(%this){
	if ($player.onCool1 == false && !$player.isDead){
		if ($player.isLeft == false){
		
			%this.energyBoltClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = energyBoltClass ;
				source = %this ;
			} ;
			%this.energyBoltClass.fire();
			%this.energyBoltClass.setPositionX(%this.getPositionX() + 8.3);
			%this.energyBoltClass.setLinearVelocityX(82);
			
		}else{
				
			%this.energyBoltClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = energyBoltClass ;
				source = %this ;
			} ;
			%this.energyBoltClass.fire();
			%this.energyBoltClass.setPositionX(%this.getPositionX() - 8.3);
			%this.energyBoltClass.setLinearVelocityX(-82);			
		}
		$player.onCool1 = true ;
		%this.schedule(500,"cooldown1");
	}
}
function yukiClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function yukiClass::useMove2(%this){
	
	if (!$player.onCool2){
		if ($player.isLeft)
			%this.setAutoRotation(700);
		else
			%this.setAutoRotation(-700);
		%this.broomClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = broomClass ;
			source = %this ;
			number = 5 ;
			start = %this.startxxx ;
		} ;
		$player.spinBroom = true ;
		%this.broomClass.fire();
		%this.broomClass.setAutoRotation(%this.getAutoRotation());
		$player.rotation = true ;
		$player.onCool2 = true;
	}
	
	
}
function yukiClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function yukiClass::startStar(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
		if ($player.isLeft){
			%this.startxxx = %this.getPositionX() - 8.3 ;
			error(%this.startxxx);
			%this.starClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = starClass ;
				source = %this ;
				number = 5 ;
				start = %this.startxxx ;
			} ;
		
		}else{
			%this.startxxx = %this.getPositionX() + 8.3 ;
			%this.starClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = starClass ;
				source = %this ;
				number = 5 ;
				start = %this.startxxx ;
			} ;
		}
		%this.starClass.fire();
		$player.onCool3 = true ;
	
	}
	
}
function yukiClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function yukiClass::note3Hit(%this){
	
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
function yukiClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}