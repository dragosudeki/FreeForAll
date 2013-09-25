function BmorteClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 65 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.onCool1 = false ;
	$Bplayer.onCool2 = false ;
	$Bplayer.onCool3 = false ;
	$Bplayer.doubleJ = false ;
	
	$Bplayer.moveX = 0 ;
	
	$Bplayer.collision = 1 ;
	$Bplayer.collision2 = 1;
	%this.sideCollision = 0.643;
	
	$Bplayer.health = 200 ;
	player2Health.text = $Bplayer.health ;
	$Bplayer.lives = 3 ;
	$Bplayer.isDead = false ;
	
	$Bplayer.note3Hit = false ;
	
	moveMap.bindCmd(keyboard, "A", "BplayerLeft2();", "BplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "BplayerRight2();", "BplayerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "BplayerJump2();", "");
	moveMap.bindCmd(keyboard, "S" , "BplayerDown2();","BplayerDownStop2();");
	moveMap.bindCmd(keyboard, "H", "Bnote1();", "");
	moveMap.bindCmd(keyboard, "J", "Bnote2();", "");
	moveMap.bindCmd(keyboard, "K", "Bnote3();", "");
	
	%this.setCollisionPolyCustom(4,"-0.643 1.000","0.643 1.000","0.643 -1.000","-0.643 -1.000");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(23.824,21.691);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(morteStand);
	
}

function BplayerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function BplayerDownStop2(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function BplayerLeft2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveLeft = true ;
	}
}
function BplayerLeftStop2()
{
	$Bplayer.moveLeft = false ;
}
function BplayerRight2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveRight = true ;	
	}
}
function BplayerRightStop2()
{
	$Bplayer.moveRight = false ;	
}
function BplayerJump2()
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
function Bnote1()
{
	$Bplayer.fireMissile();	
}
function Bnote2()
{
	$Bplayer.fireNote2();	
}
function Bnote3()
{
	$Bplayer.fireNote3();	
}

function BmorteClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BmorteClass::updateH(%this)
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
			if (%this.moveLeft == %this.moveRight)
			{
				if ($Bplayer.smashX == 0)
					%this.setLinearVelocityX($Bplayer.moveX) ;
					return ;
			}
			
		}
		else if ($Bplayer.smashX != 0)
			%this.setLinearVelocityX($Bplayer.smashX) ;
			
}

// When player is moving up or down
function BmorteClass::updateV(%this)
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
		$Bplayer.smashX = false ;
		%this.setLinearVelocityX(0) ;
		%this.setLinearVelocityY(%yVelocity) ;
		
		return ;
	}
	// collide with wall to the right
	if (%normalX == -1 && %normalY == 0)
	{
		$Bplayer.smashX = false ;
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
function BmorteClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BmorteClass::setCurrentAnimation(%this)
{
	%xVelocity = %this.getLinearVelocityX();
	%yVelocity = %this.getLinearVelocityY();
	if ($Bplayer.isLeft == false)
	{
		%this.setFlip(false,false) ;
	}
	if ($Bplayer.isLeft == true)
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
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "morteJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(morteJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "morteFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(morteFall) ;
			}
		}
	}
	// If moving
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "morteWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(morteWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(morteWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "morteStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(morteStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(morteStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function BmorteClass::die(%this)
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
		$timer.endGame1();
	}
}

// When player will die
function BmorteClass::spawn(%this)
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
function BmorteClass::updateVertical(%this)
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
function BmorteClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function BmorteClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BmorteClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function BmorteClass::fireMissile(%this){
	
	
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.note1Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note1Class ;
			bomb = %this ;
		} ;
		%this.note1Class.fire2();
		%this.note1Class.setLinearVelocityX(25 + ($Bplayer.moveX / 1));
		
		}else{
			
		%this.note1Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note1Class ;
			bomb = %this ;
		} ;
		%this.note1Class.fire2();
		%this.note1Class.setLinearVelocityX(-25 + ($Bplayer.moveX / 1));		
			
	}
	
	$Bplayer.onCool1 = true ;
	%this.schedule(500,"cooldown1");
	
	}
	
}
function BmorteClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BmorteClass::fireNote2(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.note2Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note2Class ;
			bomb = %this ;
		} ;
		%this.note2Class.fire2();
		%this.note2Class.setLinearVelocityX(35 + ($Bplayer.moveX / 1));
		
		}else{
			
		%this.note2Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note2Class ;
			bomb = %this ;
		} ;
		%this.note2Class.fire2();
		%this.note2Class.setLinearVelocityX(-35 + ($Bplayer.moveX / 1));		
			
	}
	
	$Bplayer.onCool2 = true ;
	%this.schedule(850,"cooldown2");
	
	}
	
}
function BmorteClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BmorteClass::fireNote3(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.note3Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note3Class ;
			bomb = %this ;
		} ;
		%this.note3Class.fire2();
		%this.note3Class.setPositionX(%this.note3Class.getPositionX() + (%this.note3Class.getWidth() / 2));
		%this.note3Class.setLinearVelocityX(30);
		
		}else{
			
		%this.note3Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note3Class ;
			bomb = %this ;
		} ;
		%this.note3Class.fire2();
		%this.note3Class.setPositionX(%this.note3Class.getPositionX() - (%this.note3Class.getWidth() / 2));
		%this.note3Class.setLinearVelocityX(-30);		
			
	}
	
	$Bplayer.onCool3 = true ;
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function BmorteClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;	
	
}
function BmorteClass::note3Hit(%this){
	
	if ($Bplayer.note3Hit == false){
		
		$Bplayer.health = $Bplayer.health - 2 ;
		
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){
			
			%this.die();
			
		}
		
		Player2Health.text = $Bplayer.health ;
		$Bplayer.note3Hit = true ;
		
		%this.schedule(300,"note3CD");
}
	
}
function BmorteClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}
