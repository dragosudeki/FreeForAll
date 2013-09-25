function BbelshazzarClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 70 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.collision = 0.573;
	$Bplayer.collision2 = 0.573;
	%this.sideCollision = 0.340;
	
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
	
	moveMap.bindCmd(keyboard, "A", "belsh2playerLeft2();", "belsh2playerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "belsh2playerRight2();", "belsh2playerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "belsh2playerJump2();", "");
	moveMap.bindCmd(keyboard, "S" , "belsh2playerDown2();","belsh2playerDownStop2();");
	moveMap.bindCmd(keyboard, "H", "bcard();", "");
	moveMap.bindCmd(keyboard, "J", "bcardFall();", "");
	moveMap.bindCmd(keyboard, "K", "bcardWave();", "");
	
	%this.setCollisionPolyCustom(4,"-0.340 0.573","0.340 0.573","0.340 -0.573","-0.340 -0.573");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(30.163,31.670);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(belshazzarStand);
	
}
function BbelshazzarClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}

function belshgamePause2(){
	
	$Bplayer.checkPause();	
	
}
function belsh2playerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function belsh2playerDownStop2(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function belsh2playerLeft2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveLeft = true ;
	}
}
function belsh2playerLeftStop2()
{
	$Bplayer.moveLeft = false ;
}
function belsh2playerRight2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveRight = true ;	
	}
}
function belsh2playerRightStop2()
{
	$Bplayer.moveRight = false ;	
}
function belsh2playerJump2()
{
	if (!$Bplayer.inCard){
		if (!$Bplayer.airborne && !$Bplayer.isDead)
		{
			$Bplayer.setLinearVelocityY(-70) ;
			$Bplayer.airborne = true ;
			$Bplayer.doubleJ = false ;
		}
		else if (!$Bplayer.doubleJ && !$Bplayer.isDead){
			
			$Bplayer.doubleJ = true ;
			$Bplayer.setLinearVelocityY(-70);
			
		}
	}
}
function bcard()
{
	if (!$Bplayer.isWave && !$Bplayer.inCard)
	$Bplayer.fireMissile();	
}
function bcardFall()
{
	if (!$Bplayer.isWave && !$Bplayer.throwCard)
	$Bplayer.fallingCard();	
}
function bcardWave()
{
	if (!$Bplayer.onCool3 && !$Bplayer.inCard && !$Bplayer.throwCard){
		$Bplayer.isWave = true ;
		$Bplayer.setLinearVelocityX(0);
	}
}

function BbelshazzarClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BbelshazzarClass::updateH(%this)
{
		if ($Bplayer.smashX > 0){
			$Bplayer.smashX += -1;
		}
		else if ($Bplayer.smashX < 0){
			$Bplayer.smashX += 1;
		}
		if (!$Bplayer.inCard && !$Bplayer.isWave){
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
				if (%this.moveLeft == %this.moveRight)
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
		
}

// When player is moving up or down
function BbelshazzarClass::updateV(%this)
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
function BbelshazzarClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BbelshazzarClass::setCurrentAnimation(%this)
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
	else if ($Bplayer.isWave){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarSmack")
			{
				if (%this.getIsAnimationFinished())
				{
					$Bplayer.groundWave();
				}
			}else
			{
				%this.playAnimation(belshazzarSmack) ;
			}
		}
	}
	else if ($Bplayer.inCard){
		if ($Bplayer.cantDie == false)
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
	else if ($Bplayer.throwCard){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "belshazzarThrow")
			{
				if (%this.getIsAnimationFinished())
				{
					$Bplayer.throwCard = false ;
				}
			}else
			{
				%this.playAnimation(belshazzarThrow) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($Bplayer.cantDie == false)
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
		if ($Bplayer.cantDie == false)
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
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
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
function BbelshazzarClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	$Bplayer.airborne = false ;
	$Bplayer.isDead = true ;
	$Bplayer.inCard = false;
	$Bplayer.isWave=false;
	$Bplayer.throwCard=false;
	%this.setCurrentAnimation() ;
	
	if ($Bplayer.lives > 0)
	%this.schedule( 2000, "spawn") ;
	else {
		$timer.endGame1();
	}
}

// When player will die
function BbelshazzarClass::spawn(%this)
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
function BbelshazzarClass::updateVertical(%this)
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
function BbelshazzarClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player){
		
		if ($Bplayer.inCard){
			if (%dstObj.getPositionX() < %srcObj.getPositionX())
			{
				
			%dstObj.jumpL();
				
			}
			else {
				
			%dstObj.jumpR();
				
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
				
				%dstObj.die() ;	
				
			}
			
			$Bplayer.inCard = false ;
			$Bplayer.onCool2 = true ;
			%srcObj.schedule(2000,"cooldown2");
			
		}
		
	}
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
function BbelshazzarClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BbelshazzarClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function BbelshazzarClass::fireMissile(%this){
	
	
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.cardClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = cardClass ;
			source = %this ;
		} ;
		%this.cardClass.fire2();
		%this.cardClass.setLinearVelocityX(70 + ($Bplayer.moveX / 1));
		
		}else{
			
		%this.cardClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = cardClass ;
			source = %this ;
		} ;
		%this.cardClass.fire2();
		%this.cardClass.setLinearVelocityX(-70 + ($Bplayer.moveX / 1));		
			
	}
	
	$Bplayer.throwCard = true ;
	$Bplayer.onCool1 = true ;
	%this.schedule(500,"cooldown1");
	
	}
	
}
function BbelshazzarClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BbelshazzarClass::fallingCard(%this){
	
	if ($Bplayer.inCard == false){
		if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
			
			$Bplayer.inCard = true ;
			%this.setLinearVelocityY(0);
			%this.setLinearVelocityX(0);
			%this.schedule(300,"fall");
			
		}
	}else{
		$Bplayer.inCard = false ;
		$Bplayer.onCool2 = true ;
		%this.schedule(2000,"cooldown2");
	}
	
}
function BbelshazzarClass::fall(%this){

	%this.setLinearVelocityY(200);	
	
}
function BbelshazzarClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BbelshazzarClass::groundWave(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.waveClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = waveClass ;
			source = %this ;
			goLeft = false;
			number = 3;
			startY = %this.getPositionY() + 31.670/2;
		} ;
		%this.waveClass.fire2();
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
		%this.waveClass.fire2();
		%this.waveClass.setPositionX(%this.getPositionX() - 17.5);
			
	}
	$Bplayer.isWave = false ;
	$Bplayer.onCool3 = true ;
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function BbelshazzarClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
		
	
}
function BbelshazzarClass::note3Hit(%this){
	
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
function BbelshazzarClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}