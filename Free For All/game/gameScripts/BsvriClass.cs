function BsvriClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 70 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.collision = 0.700;
	$Bplayer.collision2 = 0.700;
	%this.sideCollision = 0.200;
	
	$Bplayer.moveX = 0 ;
	
	$Bplayer.onCool1 = false ;
	$Bplayer.onCool2 = false ;
	$Bplayer.onCool3 = false ;
	$Bplayer.doubleJ = false ;
	
	$Bplayer.health = 200 ;
	player2Health.text = $Bplayer.health ;
	$Bplayer.lives = 3 ;
	$Bplayer.isDead = false ;
	$Bplayer.note3Hit = false ;
	
	moveMap.bindCmd(keyboard, "A", "BSplayerLeft2();", "BSplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "BSplayerRight2();", "BSplayerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "BSplayerJump2();", "");
	moveMap.bindCmd(keyboard, "S", "BSplayerDown2();","BplayerDownStop24();");
	moveMap.bindCmd(keyboard, "H", "Bsvrimove1();", "");
	moveMap.bindCmd(keyboard, "J", "Bsvrimove2();", "");
	moveMap.bindCmd(keyboard, "K", "Bsvrimove3();", "");
	
	%this.setCollisionPolyCustom(4,"-0.200 0.700","0.200 0.700","0.200 -0.700","-0.200 -0.700");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(31.250,31.250);
	%this.setLayer(2); 
	%this.setGraviticConstantForce(1);
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(svriStand);
	
}
function BsvriClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}

function gamePause2(){
	
	$Bplayer.checkPause();	
	
}
function BSplayerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function playerDownStop2(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function BSplayerLeft2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveLeft = true ;
	}
}
function BSplayerLeftStop2()
{
	$Bplayer.moveLeft = false ;
}
function BSplayerRight2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveRight = true ;	
	}
}
function BSplayerRightStop2()
{
	$Bplayer.moveRight = false ;	
}
function BSplayerJump2(){
	if (!$Bplayer.inBolt)
	if (!$Bplayer.airborne && !$Bplayer.isDead)
	{
		$Bplayer.setLinearVelocityY(-65) ;
		$Bplayer.airborne = true ;
		$Bplayer.doubleJ = false ;
	}
	else if (!$Bplayer.doubleJ && !$Bplayer.isDead){
		
		$Bplayer.doubleJ = true ;
		$Bplayer.setLinearVelocityY(-75);
		
	}
}
function Bsvrimove1()
{
	if (!$Bplayer.inBolt && !$Bplayer.onPunch)
		$Bplayer.svriSoulShot();	
}
function Bsvrimove2()
{
	if (!$Bplayer.throwSoul && !$Bplayer.inBolt)
		$Bplayer.svriPunch();	
}
function Bsvrimove3()
{
	if (!$Bplayer.throwSoul && !$Bplayer.onPunch && !$Bplayer.onCool3){
		$Bplayer.onCool3 = true ;
		$Bplayer.inBolt = true ;
	}
}

function BsvriClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BsvriClass::updateH(%this)
{
		if ($Bplayer.smashX > 0){
			$Bplayer.smashX += -1;
		}
		else if ($Bplayer.smashX < 0){
			$Bplayer.smashX += 1;
		}
		
		if ($Bplayer.smashX == 0){ 
			if (%this.moveLeft)
			{
				if(!%this.againstLeftWall && $Bplayer.smashX == 0)
				{
				   %this.againstRightWall = false ;
				   $Bplayer.isLeft = true ;
				   %this.setLinearVelocityX(-%this.movementSpeed + ($Bplayer.moveX/2)) ;
				}
			}
			if (%this.moveRight)
			{
				if (!%this.againstRightWall && $Bplayer.smashX == 0)
				{
					$Bplayer.isLeft = false ;
					%this.againstLeftWall = false ;
					%this.setLinearVelocityX(%this.movementSpeed + ($Bplayer.moveX/2)) ;
				}
			}
			if (%this.moveLeft == %this.moveRight || %this.throwSoul || %this.onPunch || %this.inBolt)
			{
				if ($Bplayer.smashX == 0)
					%this.setLinearVelocityX($Bplayer.moveX) ;
				
			}
		}
		else if ($Bplayer.smashX != 0){
			
			%this.setLinearVelocityX($Bplayer.smashX) ;
			return ;
		}
		
}

// When player is moving up or down
function BsvriClass::updateV(%this)
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
		$Bplayer.airborne = true ;
		$Bplayer.moveX = 0 ;
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
		$Bplayer.airborne = false ;
		$Bplayer.doubleJ = false ;
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
function BsvriClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BsvriClass::setCurrentAnimation(%this)
{
	%yVelocity = %this.getLinearVelocityY();
	if ($Bplayer.isLeft == false )
	{
		%this.setFlip(false,false) ;
	}
	if ($Bplayer.isLeft == true )
	{
		%this.setFlip(true,false) ;
	}
	
	if ($Bplayer.isDead){
		
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
		if ($Bplayer.cantDie == false)
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
		if ($Bplayer.cantDie == false)
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
		if ($Bplayer.cantDie == false)
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
		if ($Bplayer.cantDie == false)
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
		if ($Bplayer.cantDie == false)
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
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
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
function BsvriClass::die(%this)
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
	
	$Bplayer.airborne = false ;
	$Bplayer.isDead = true ;
	%this.setCurrentAnimation() ;
	
	if ($Bplayer.lives > 0)
	%this.schedule( 2000, "spawn") ;
	else {
		$timer.endGame2();
	}
}

// When player will die
function BsvriClass::spawn(%this)
{
	$Bplayer.isDead = false ;
	$Bplayer.moveLeft = false ;
	$Bplayer.moveRight = false ;
	$Bplayer.isAirborne = false ;
	$Bplayer.doubleJ = false ;
	$warning2.happened = false;
	%this.setBlendColour(1,1,1,1);
	
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	$Bplayer.health = 200 ;
	player2Health.text = $Bplayer.health ;
	$Bplayer.lives = $Bplayer.lives - 1;
	player2Lives.text = $Bplayer.lives ;
	
	%this.setEnabled(true) ;
}

// When player is moving up or down
function BsvriClass::updateVertical(%this)
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
function BsvriClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "pit")
	{
		$Bplayer.health = 0 ;
		player2Health.text = $Bplayer.health ;
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
				
		$Bplayer.health = $Bplayer.health - 2 ;
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
				
		$Bplayer.health = $Bplayer.health - 25 ;
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
			
			%srcObj.die() ;	
			
		}	
		
	}
}

// Jumps Left when hit
function BsvriClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BsvriClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function BsvriClass::shootSoul(%this){
	
	if ($Bplayer.isLeft == false){
		
			%this.svirSoulClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = svirSoulClass ;
				source = %this ;
			} ;
			%this.svirSoulClass.fire2();
			%this.svirSoulClass.setLinearVelocityX(75);
			
		}else{
				
			%this.svirSoulClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = svirSoulClass ;
				source = %this ;
			} ;
			%this.svirSoulClass.fire2();
			%this.svirSoulClass.setLinearVelocityX(-75);		
				
		}	
}
function BsvriClass::svriSoulShot(%this){
	
	
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
		
		$Bplayer.throwSoul = true ;
		$Bplayer.onCool1 = true ;
		%this.schedule(400,"shootSoul");
		%this.schedule(1000,"cooldown1");
	
	}
	
}
function BsvriClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BsvriClass::svriPunch(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.svirMeleeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svirMeleeClass ;
			source = %this ;
		} ;
		%this.svirMeleeClass.fire2();
		%this.svirMeleeClass.setPositionX(%this.getPositionX() + 5);
		
		}else{
			
		%this.svirMeleeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svirMeleeClass ;
			source = %this ;
		} ;
		%this.svirMeleeClass.fire2();
		%this.svirMeleeClass.setPositionX(%this.getPositionX() - 5);
			
	}
	
	$Bplayer.onPunch = true ;
	$Bplayer.onCool2 = true ;
	%this.schedule(1500,"cooldown2");
	
	}
	
}
function BsvriClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BsvriClass::svriSoulBolt(%this){
	
	
	if (!$Bplayer.isDead && !$Bplayer.shot){
	if ($Bplayer.isLeft == false){
	
		%this.svriBoltClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svriBoltClass ;
			source = %this ;
		} ;
		%this.svriBoltClass.fire2();
		%this.svriBoltClass.setPositionX(%this.getPositionX() + 42.166);
		%this.svriBoltClass.setLinearVelocity(0,0);
		
		}else{
			
		%this.svriBoltClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = svriBoltClass ;
			source = %this ;
		} ;
		%this.svriBoltClass.fire2();
		%this.svriBoltClass.setPositionX(%this.getPositionX() - 42.166);
		%this.svriBoltClass.setFlip(true,false);
		%this.svriBoltClass.setLinearVelocity(0,0);
		
			
	}
	$Bplayer.shot = true;
	}
	
}
function BsvriClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
	$Bplayer.shot = false;
		
	
}
function BsvriClass::note3Hit(%this){
	
	if ($Bplayer.note3Hit == false){
		
		$Bplayer.health = $Bplayer.health - 2 ;
		
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){
			
			%this.die();
			
		}
		
		PlayerHealth.text = $Bplayer.health ;
		$Bplayer.note3Hit = true ;
		
		%this.schedule(300,"note3CD");
}
	
}
function BsvriClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}