function ByukiClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 80 ;
	$Bplayer.airborne = false ;

	$Bplayer.collision = 1;
	$Bplayer.collision2 = 0.8;
	%this.sideCollision = 0.5;
	
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
	
	moveMap.bindCmd(keyboard, "A", "ByuplayerLeft2();", "ByuplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "ByuplayerRight2();", "ByuplayerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "ByuplayerJump2();", "");
	moveMap.bindCmd(keyboard, "S" , "ByuplayerDown2();","ByuplayerDownStop2();");
	moveMap.bindCmd(keyboard, "H", "BfireEnergyBolt();", "");
	moveMap.bindCmd(keyboard, "J", "Bmove2();", "");
	moveMap.bindCmd(keyboard, "K", "Bstar3();", "");
	
	%this.setCollisionPolyCustom(4,"-0.5 1.000","0.5 1.000","0.5 -0.8","-0.5 -0.8");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(23.56,24.76);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(yukiSelect);
	
}
function ByukiClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}	
function ByuplayerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function ByuplayerDownStop2(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function ByuplayerLeft2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveLeft = true ;
	}
}
function ByuplayerLeftStop2()
{
	$Bplayer.moveLeft = false ;
}
function ByuplayerRight2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveRight = true ;	
	}
}
function ByuplayerRightStop2()
{
	$Bplayer.moveRight = false ;	
}
function ByuplayerJump2()
{
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
function BfireEnergyBolt()
{
	if (!$Bplayer.shootBolt && !$Bplayer.starFire && !$Bplayer.spinBroom){
		$Bplayer.shootBolt = true;
		$Bplayer.movementSpeed = 70 ;
	}
}
function Bmove2()
{
	if (!$Bplayer.shootBolt && !$Bplayer.starFire && !$Bplayer.onCool2 && !$Bplayer.spinBroom){
		$Bplayer.useMove2();	
	}
}
function Bstar3()
{
	if (!$Bplayer.shootBolt && !$Bplayer.starFire && !$Bplayer.onCool3 && !$Bplayer.spinBroom){
		$Bplayer.movementSpeed = 60 ;
		$Bplayer.starFire = true ;	
	}
}

function ByukiClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function ByukiClass::updateH(%this)
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
				   if (!$Bplayer.rotation)
				   $Bplayer.isLeft = true ;
				   %this.setLinearVelocityX(-%this.movementSpeed + ($Bplayer.moveX/2)) ;
				}
			}
			if (%this.moveRight)
			{
				if (!%this.againstRightWall && $Bplayer.smashX == 0)
				{
					if (!$Bplayer.rotation)
					$Bplayer.isLeft = false ;
					%this.againstLeftWall = false ;
					%this.setLinearVelocityX(%this.movementSpeed + ($Bplayer.moveX/2)) ;
				}
			}
			if (%this.moveLeft == %this.moveRight || $Bplayer.rotation)
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
function ByukiClass::updateV(%this)
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
function ByukiClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		
		%this.updateH();
		%this.updateV();
		if ($Bplayer.rotation){
			if (%this.getRotation() >= 350 && $Bplayer.isLeft){
				%this.broomClass.safeDelete();
				%this.setAutoRotation(0);
				%this.setRotation(0);
				$Bplayer.spinBroom = false ;
				%this.schedule(1500,"cooldown2");	
			}
			if (%this.getRotation() <= -350 && !$Bplayer.isLeft){
				%this.broomClass.safeDelete();
				%this.setAutoRotation(0);
				%this.setRotation(0);
				$Bplayer.spinBroom = false ;
				%this.schedule(1500,"cooldown2");	
			}
			%this.setLinearVelocity(0,-1);
		}
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function ByukiClass::setCurrentAnimation(%this)
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
		
		%this.setBlendAlpha(0);
		
	}
	else if ($Bplayer.starFire){
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
	else if ($Bplayer.shootBolt){
		if (%this.getAnimationName() $= "yukiAttack")
		{
			if (%this.getIsAnimationFinished())
			{
				%this.fireBolt() ;
				$Bplayer.movementSpeed = 80;
				$Bplayer.shootBolt = false ;
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
function ByukiClass::die(%this)
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
function ByukiClass::spawn(%this)
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
function ByukiClass::updateVertical(%this)
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
function ByukiClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function ByukiClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function ByukiClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function ByukiClass::fireBolt(%this){
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft == false){
		
			%this.energyBoltClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = energyBoltClass ;
				source = %this ;
			} ;
			%this.energyBoltClass.fire2();
			%this.energyBoltClass.setPositionX(%this.getPositionX() + 8.3);
			%this.energyBoltClass.setLinearVelocityX(82);
			
		}else{
				
			%this.energyBoltClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = energyBoltClass ;
				source = %this ;
			} ;
			%this.energyBoltClass.fire2();
			%this.energyBoltClass.setPositionX(%this.getPositionX() - 8.3);
			%this.energyBoltClass.setLinearVelocityX(-82);			
		}
		$Bplayer.onCool1 = true ;
		%this.schedule(500,"cooldown1");
	}
}
function ByukiClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function ByukiClass::useMove2(%this){
	
	if (!$Bplayer.onCool2){
		if ($Bplayer.isLeft)
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
		$Bplayer.spinBroom = true ;
		%this.broomClass.fire2();
		$Bplayer.rotation = true ;
		$Bplayer.onCool2 = true;
	}
	
	
}
function ByukiClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function ByukiClass::startStar(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft){
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
		%this.starClass.fire2();
		$Bplayer.onCool3 = true ;
	
	}
	
}
function ByukiClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
		
	
}
function ByukiClass::note3Hit(%this){
	
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
function ByukiClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}