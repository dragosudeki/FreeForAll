function BcasperClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 40 ;
	$Bplayer.airborne = false ;
	$Bplayer.collision = 0.869;
	$Bplayer.collision2 = 0.673;
	%this.sideCollision = 0.555;

	
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
	
	moveMap.bindCmd(keyboard, "A", "BplayerLeft24();", "BplayerLeftStop24();");
	moveMap.bindCmd(keyboard, "D", "BplayerRight24();", "BplayerRightStop24();");
	moveMap.bindCmd(keyboard, "W", "BplayerJump24();", "");
	moveMap.bindCmd(keyboard, "S" , "BplayerDown24();","BplayerDownStop24();");
	moveMap.bindCmd(keyboard, "H", "Bbomb();", "");
	moveMap.bindCmd(keyboard, "J", "Bnote2();", "");
	moveMap.bindCmd(keyboard, "K", "Bnuke();", "");
	
	%this.setCollisionPolyCustom(4,"-0.555 -0.673","0.555 -0.673","0.555 0.869","-0.555 0.869");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(24.34,24.34);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(casperStand);
	
}
function BcasperClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}

function gamePause24(){
	
	$Bplayer.checkPause();	
	
}
function BplayerDown24()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function BplayerDownStop24(){
	error("TEST");
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function BplayerLeft24()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveLeft = true ;
	}
}
function BplayerLeftStop24()
{
	$Bplayer.moveLeft = false ;
}
function BplayerRight24()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveRight = true ;	
	}
}
function BplayerRightStop24()
{
	$Bplayer.moveRight = false ;	
}
function BplayerJump24()
{
	if (!$Bplayer.airborne && !$Bplayer.isDead)
	{
		$Bplayer.setLinearVelocityY(-75) ;
		$Bplayer.airborne = true ;
		$Bplayer.doubleJ = false ;
	}
	else if (!$Bplayer.doubleJ && !$Bplayer.isDead){
		
		$Bplayer.doubleJ = true ;
		$Bplayer.setLinearVelocityY(-65);
		
	}
}
function Bbomb()
{
	if (!$Bplayer.isNuke && !$Bplayer.onSword)
	$Bplayer.fireBomb();	
}
function Bnote2()
{
	if (!$Bplayer.isNuke && !$Bplayer.isThrowing)
	$Bplayer.fireNote2();	
}
function Bnuke()
{
	if (!$Bplayer.onSword && !$Bplayer.isThrowing)
	$Bplayer.callNuke();	
}

function BcasperClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BcasperClass::updateH(%this)
{
		if ($Bplayer.smashX > 0){
			$Bplayer.smashX += -2;
		}
		else if ($Bplayer.smashX < 0){
			$Bplayer.smashX += 2;
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
			if (%this.moveLeft == %this.moveRight || $Bplayer.isThrowing || $Bplayer.onSword || $Bplayer.isNuke)
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
function BcasperClass::updateV(%this)
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
function BcasperClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BcasperClass::setCurrentAnimation(%this)
{
	%yVelocity = %this.getLinearVelocityY();
	
	if ($Bplayer.isLeft == false )
	{
		%this.setFlip(true,false) ;
	}
	if ($Bplayer.isLeft == true )
	{
		%this.setFlip(false,false) ;
	}
	
	if ($Bplayer.isDead){
		
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
	else if ($Bplayer.isNuke){
		
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperNuke")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(casperNuke) ;
			}
		}
		
	}
	else if ($Bplayer.onSword){
		
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperSword")
			{
				if (%this.getIsAnimationFinished())
				{
					$Bplayer.onSword = false ;
					%this.swordClass.delete();
				}
			}else
			{
				%this.playAnimation(casperSword) ;
			}
		}
		
	}
	else if ($Bplayer.isThrowing){
		
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "casperThrow")
			{
				if (%this.getIsAnimationFinished())
				{
					$Bplayer.isThrowing = false ;
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
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
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
function BcasperClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	$Bplayer.airborne = false ;
	$Bplayer.isDead = true ;
	%this.setCurrentAnimation() ;
	
	%this.casperDeathClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = casperDeathClass ;
		source = %this ;
	} ;
	%this.casperDeathClass.fire2();
	%this.casperDeathClass.setPositionX(%this.getPositionX());
	
	%this.setCurrentAnimation() ;
	
}
function BcasperClass::die2(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	$Bplayer.airborne = false ;
	$Bplayer.isDead = true ;
	%this.setCurrentAnimation() ;
	
	if ($Bplayer.lives > 0)
	%this.schedule( 2000, "spawn") ;
	else {
		endbox.setPosition($endx,$endy);
		$timescale = 0;
		canvas.pushDialog(RoundEnd);
		endname.setImageMap();
	}
}
function BcasperClass::respawn(%this){
	if ($Bplayer.lives > 0)
		%this.schedule( 2000, "spawn") ;
	else {
		$timer.endGame1();
	}	
	%this.setEnabled(false);
}

// When player will die
function BcasperClass::spawn(%this)
{
	$Bplayer.isDead = false ;
	$Bplayer.moveLeft = false ;
	$Bplayer.moveRight = false ;
	$Bplayer.isAirborne = false ;
	$Bplayer.doubleJ = false ;
	$warning2.happened = false;
	%this.setBlendColour(1,1,1,1);
	
	%this.setEnabled(true);
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	$Bplayer.health = 200 ;
	player2Health.text = $Bplayer.health ;
	$Bplayer.lives = $Bplayer.lives - 1;
	player2Lives.text = $Bplayer.lives ;
	
	%this.setEnabled(true) ;
}

// When player is moving up or down
function BcasperClass::updateVertical(%this)
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
function BcasperClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "pit")
	{
		$Bplayer.health = 0 ;
		player2Health.text = $Bplayer.health ;
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
function BcasperClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BcasperClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function BcasperClass::fireBomb(%this){
	
	
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft == false){
		
				%this.bulletClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = bulletClass ;
					enemy1 = %this ;
				} ;
				%this.bulletClass.fire2();
				%this.bulletClass.setPositionX(%this.getPositionX() - 8) ;
				%this.bulletClass.setPositionY(%this.getPositionY() - 7) ;
				%this.bulletClass.setLinearVelocityX(30 + ($Bplayer.moveX / 1));
			
			}else{
				
				%this.bulletClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = bulletClass ;
					enemy1 = %this ;
				} ;
				%this.bulletClass.fire2();
				%this.bulletClass.setPositionX(%this.getPositionX() + 8) ;
				%this.bulletClass.setPositionY(%this.getPositionY() - 7) ;
				%this.bulletClass.setLinearVelocityX(-30 + ($Bplayer.moveX / 1));		
				
		}
	
	$Bplayer.onCool1 = true ;
	$Bplayer.isThrowing = true ;
	%this.schedule(600,"cooldown1");
	
	}
	
}
function BcasperClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BcasperClass::fireNote2(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft == false){
				
				
				%this.swordClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = swordClass ;
					source = %this ;
				} ;
				%this.swordClass.fire2();
				%this.swordClass.setPositionX(%this.getPositionX() + 6.085);
				
			}else{
					
				%this.swordClass = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
					class = swordClass ;
					source = %this ;
				} ;	
				%this.swordClass.fire2();
				%this.swordClass.setPositionX(%this.getPositionX() - 6.085);	
					
			}
			
			$Bplayer.onSword = true ;
			$Bplayer.isThrowing = false ;
			$Bplayer.onCool2 = true ;
			%this.schedule(750,"cooldown2");
		
		}
	
}
function BcasperClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BcasperClass::callNuke(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.nukeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = nukeClass ;
			source = %this ;
		} ;
		%this.nukeClass.fire2();
		%this.nukeClass.setFlip(true,false);
		%this.nukeClass.setPositionX(%this.getPositionX() + 40);
		
		}else{
			
		%this.nukeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = nukeClass ;
			source = %this ;
		} ;
		%this.nukeClass.fire2();
		%this.nukeClass.setPositionX(%this.getPositionX() - 40);
			
	}
	
	$Bplayer.onCool3 = true ;
	$Bplayer.isNuke = true ;
	%this.schedule(500,"outtaNuke");
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function BcasperClass::outtaNuke(%this){

	$Bplayer.isNuke = false ;
		
	
}
function BcasperClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
		
	
}
function BcasperClass::note3Hit(%this){
	
	if ($Bplayer.note3Hit == false){
		
		$Bplayer.health = $Bplayer.health - 2 ;
		
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){
			
			%this.die();
			
		}
		
		player2Health.text = $Bplayer.health ;
		$Bplayer.note3Hit = true ;
		
		%this.schedule(300,"note3CD");
}
	
}
function BcasperClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}