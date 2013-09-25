function BdominioClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 65 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.collision = 0.584;
	$Bplayer.collision2 = 0.650;
	%this.sideCollision = 0.2;
	
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
	
	moveMap.bindCmd(keyboard, "A", "BdomiplayerLeft2();", "BdomiplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "BdomiplayerRight2();", "BdomiplayerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "BdomiplayerJump2();", "");
	moveMap.bindCmd(keyboard, "S" , "BdomiplayerDown2();","BdomiplayerDownStop2();");
	moveMap.bindCmd(keyboard, "H", "Bdompunch1();", "");
	moveMap.bindCmd(keyboard, "J", "Bdomkick();", "");
	moveMap.bindCmd(keyboard, "K", "Bdomupper();", "");
	
	%this.setCollisionPolyCustom(4,"-0.200 0.584","0.200 0.584","0.200 -0.650","-0.200 -0.650");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(35.537,35.537);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(dominioStand);
	
}
function BdominioClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}

function domigamePause2(){
	
	$Bplayer.checkPause();	
	
}
function BdomiplayerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function BdomiplayerDownStop2(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function BdomiplayerLeft2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveLeft = true ;
	}
}
function BdomiplayerLeftStop2()
{
	$Bplayer.moveLeft = false ;
}
function BdomiplayerRight2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveRight = true ;	
	}
}
function BdomiplayerRightStop2()
{
	$Bplayer.moveRight = false ;	
}
function BdomiplayerJump2()
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
function Bdompunch1()
{
	if (!$Bplayer.onPunch2 && !$Bplayer.onKick)
		$Bplayer.soulPunch1();	
}
function Bdomkick()
{
	if (!$Bplayer.onPunch2 && !$Bplayer.onPunch1)
		$Bplayer.soulKick1();	
}
function Bdomupper()
{
	if (!$Bplayer.onPunch1 && !$Bplayer.onKick)
		$Bplayer.dominioUpperCut();	
}

function BdominioClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BdominioClass::updateH(%this)
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
			if (%this.moveLeft == %this.moveRight || %this.onPunch1 || %this.onKick || %this.onPunch2)
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
function BdominioClass::updateV(%this)
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
function BdominioClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BdominioClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "dominioDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.setBlendAlpha(0);
					return;
				}
			}else
			{
				%this.playAnimation(dominioDeath) ;
			}
		
	}
	else if (%this.onPunch2){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioUpperCut")
			{
				%this.setLinearVelocityY(0);
				if (%this.getIsAnimationFinished())
				{
					%this.onPunch2 = false ;
					%this.soulPunchClass.safeDelete();
					%this.schedule(3000,"cooldown3");
					return;
				}
			}else
			{
				%this.playAnimation(dominioUpperCut) ;
			}
		}
	}
	else if (%this.onKick){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioKick")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.onKick = false ;
					%this.soulPunchClass.safeDelete();
					%this.schedule(800,"cooldown2");
					return;
				}
			}else
			{
				%this.playAnimation(dominioKick) ;
			}
		}
	}
	else if (%this.onPunch1){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioPunch")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.onPunch1 = false ;
					%this.soulPunchClass.safeDelete();
					%this.schedule(500,"cooldown1");
					return;
				}
			}else
			{
				%this.playAnimation(dominioPunch) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioJump")
			{
				if (%this.getIsAnimationFinished()){
				}
			}else
			{
				%this.playAnimation(dominioJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioFall")
			{
				if (%this.getIsAnimationFinished()){
				}
			}else
			{
				%this.playAnimation(dominioFall) ;
			}
		}
	}
	// If moving
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "dominioWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(dominioWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(dominioWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "dominioStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(dominioStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(dominioStand) ;
			}
		
	}	
}
//When user loses all health or jumps off map
function BdominioClass::die(%this)
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
		$timer.endGame2();
	}
}

// When player will die
function BdominioClass::spawn(%this)
{
	$Bplayer.isDead = false ;
	$Bplayer.moveLeft = false ;
	$Bplayer.moveRight = false ;
	$Bplayer.isAirborne = false ;
	$Bplayer.doubleJ = false ;
	$warning1.happened = false;
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
function BdominioClass::updateVertical(%this)
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
function BdominioClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function BdominioClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BdominioClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function BdominioClass::soulPunch1(%this){
	
	
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft == false){
		
			%this.soulPunchClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = soulPunchClass ;
				source = %this ;
				damage = 5;
			} ;
			%this.soulPunchClass.fire2();
			%this.soulPunchClass.setPositionX(%this.getPositionX() + 8.885);
			%this.soulPunchClass.setLinearVelocityX(%this.moveX);
			
		}else{
				
			%this.soulPunchClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = soulPunchClass ;
				source = %this ;
				damage = 5;
			} ;
			%this.soulPunchClass.fire2();
			%this.soulPunchClass.setPositionX(%this.getPositionX() - 8.885);
			%this.soulPunchClass.setLinearVelocityX(%this.moveX);		
				
		}
		$Bplayer.onPunch1 = true ;
		$Bplayer.onCool1 = true ;
	
	}
	
}
function BdominioClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BdominioClass::soulKick1(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 8;
		} ;
		%this.soulPunchClass.fire2();
		%this.soulPunchClass.setPositionX(%this.getPositionX() + 8.885);
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);
		
		}else{
			
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 8;
		} ;
		%this.soulPunchClass.fire2();
		%this.soulPunchClass.setPositionX(%this.getPositionX() - 8.885);
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);	
			
	}
	
	$Bplayer.onKick = true ;
	$Bplayer.onCool2 = true ;
	%this.schedule(850,"cooldown2");
	
	}
	
}
function BdominioClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BdominioClass::dominioUpperCut(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 10;
			left = false;
		} ;
		%this.soulPunchClass.setPositionX(%this.getPositionX() + 8.88);
		%this.soulPunchClass.fire2();
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);
		
		}else{
			
		%this.soulPunchClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = soulPunchClass ;
			source = %this ;
			damage = 10;
			left = true;
		} ;
		%this.soulPunchClass.setPositionX(%this.getPositionX() - 8.88);
		%this.soulPunchClass.fire2();
		%this.soulPunchClass.setLinearVelocityX(%this.moveX);		
			
	}
	
	$Bplayer.onPunch2 = true ;
	$Bplayer.onCool3 = true ;
	
	}
}
function BdominioClass::cooldown3(%this){
	$Bplayer.onCool3 = false ;	
}
function BdominioClass::note3Hit(%this){
	
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
function BdominioClass::note3CD(%this){
	$Bplayer.note3Hit = false ;
}