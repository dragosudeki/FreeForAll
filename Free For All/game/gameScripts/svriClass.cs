function svriClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 70 ;
	$player.airborne = false ;
	
	$player.collision = 0.700;
	$player.collision2 = 0.700;
	%this.sideCollision = 0.200;
	
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
	moveMap.bindCmd(keyboard, "down" , "playerDown2();","svrplayerDownStop2();");
	moveMap.bindCmd(keyboard, "numpad1", "svrimove1();", "");
	moveMap.bindCmd(keyboard, "numpad2", "svrimove2();", "");
	moveMap.bindCmd(keyboard, "numpad3", "svrimove3();", "");
	moveMap.bindCmd(keyboard, "P", "gamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.200 0.700","0.200 0.700","0.200 -0.700","-0.200 -0.700");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(31.250,31.250);
	%this.setLayer(1); 
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(svriStand);
	
}
function svriClass::solo(%this)
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
function svrplayerDownStop2(){
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
function playerJump2(){
	if (!$player.inBolt)
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
function svrimove1()
{
	if (!$player.inBolt && !$player.onPunch)
		$player.svriSoulShot();	
}
function svrimove2()
{
	if (!$player.throwSoul && !$player.inBolt)
		$player.svriPunch();	
}
function svrimove3()
{
	if (!$player.throwSoul && !$player.onPunch && !$player.onCool3){
		$player.onCool3 = true ;
		$player.inBolt = true ;
	}
}

function svriClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function svriClass::updateH(%this)
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
			if (%this.moveLeft == %this.moveRight || %this.throwSoul || %this.onPunch || %this.inBolt)
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
function svriClass::updateV(%this)
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
function svriClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function svriClass::setCurrentAnimation(%this)
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
	else if (%this.inBolt){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "svriMove3")
			{
				%this.setLinearVelocityY(0);
				if (%this.getIsAnimationFinished())
				{
					%this.svriSoulBolt();
					return;
				}
			}else
			{
				%this.playAnimation(svriMove3) ;
			}
		}
	}
	else if (%this.onPunch){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "svriMove2")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.onPunch = false ;
					%this.svirMeleeClass.safeDelete();
					return;
				}
			}else
			{
				%this.playAnimation(svriMove2) ;
			}
		}
	}
	else if (%this.throwSoul){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "svriMove1")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.throwSoul=false;
					return;
				}
			}else
			{
				%this.playAnimation(svriMove1) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "svriJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(svriJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "svriFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(svriFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "svriWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(svriWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(svriWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "svriStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(svriStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(svriStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function svriClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	%this.setBlendAlpha(0);
	
	%this.smokeSourceClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = smokeSourceClass ;
		source = %this ;
		soul = true ;
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
function svriClass::spawn(%this)
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
function svriClass::updateVertical(%this)
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
function svriClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function svriClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function svriClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function svriClass::shootSoul(%this){
	
	if ($player.isLeft == false){
		
			%this.svirSoulClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = svirSoulClass ;
				source = %this ;
			} ;
			%this.svirSoulClass.fire();
			%this.svirSoulClass.setLinearVelocityX(75);
			
		}else{
				
			%this.svirSoulClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = svirSoulClass ;
				source = %this ;
			} ;
			%this.svirSoulClass.fire();
			%this.svirSoulClass.setLinearVelocityX(-75);		
				
		}	
}
function svriClass::svriSoulShot(%this){
	
	
	if ($player.onCool1 == false && !$player.isDead){
		
		$player.throwSoul = true ;
		$player.onCool1 = true ;
		%this.schedule(400,"shootSoul");
		%this.schedule(1000,"cooldown1");
	
	}
	
}
function svriClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function svriClass::svriPunch(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.svirMeleeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svirMeleeClass ;
			source = %this ;
		} ;
		%this.svirMeleeClass.fire();
		%this.svirMeleeClass.setPositionX(%this.getPositionX() + 5);
		
		}else{
			
		%this.svirMeleeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svirMeleeClass ;
			source = %this ;
		} ;
		%this.svirMeleeClass.fire();
		%this.svirMeleeClass.setPositionX(%this.getPositionX() - 5);
			
	}
	
	$player.onPunch = true ;
	$player.onCool2 = true ;
	%this.schedule(1500,"cooldown2");
	
	}
	
}
function svriClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function svriClass::svriSoulBolt(%this){
	
	
	if (!$player.isDead && !$player.shot){
	if ($player.isLeft == false){
	
		%this.svriBoltClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svriBoltClass ;
			source = %this ;
		} ;
		%this.svriBoltClass.fire();
		%this.svriBoltClass.setPositionX(%this.getPositionX() + 42.166);
		%this.svriBoltClass.setLinearVelocity(0,0);
		
		}else{
			
		%this.svriBoltClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svriBoltClass ;
			source = %this ;
		} ;
		%this.svriBoltClass.fire();
		%this.svriBoltClass.setPositionX(%this.getPositionX() - 42.166);
		%this.svriBoltClass.setFlip(true,false);
		%this.svriBoltClass.setLinearVelocity(0,0);
		
			
	}
	$player.shot = true;
	}
	
}
function svriClass::cooldown3(%this){

	$player.onCool3 = false ;
	$player.shot = false;
		
	
}
function svriClass::note3Hit(%this){
	
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
function svriClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}