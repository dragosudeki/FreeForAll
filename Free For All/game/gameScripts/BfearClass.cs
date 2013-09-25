function BfearClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 60 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.collision = 0.80;
	$Bplayer.collision2 = 0.643;
	%this.sideCollision = 0.32;
	
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
	
	moveMap.bindCmd(keyboard, "A", "BfeplayerLeft2();", "BfeplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "BfeplayerRight2();", "BfeplayerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "BfeplayerJump2();", "");
	moveMap.bindCmd(keyboard, "S" , "BfeplayerDown2();","BfeplayerDownStop2();");
	moveMap.bindCmd(keyboard, "H", "bfearHit1();", "");
	moveMap.bindCmd(keyboard, "J", "bfearCycle2();", "");
	moveMap.bindCmd(keyboard, "K", "bfearFire3();", "");
	
	%this.setCollisionPolyCustom(4,"-0.320 0.800","0.320 0.800","0.320 -0.643","-0.320 -0.643");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(27.167,28.556);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(fearStand);
	
}
function BfearClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}

function fegamePause2(){
	
	$Bplayer.checkPause();	
	
}
function BfeplayerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function BfeplayerDownStop2(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function BfeplayerLeft2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveLeft = true ;
	}
}
function BfeplayerLeftStop2()
{
	$Bplayer.moveLeft = false ;
}
function BfeplayerRight2()
{
	if (!$Bplayer.isDead)
	{
		$Bplayer.moveRight = true ;	
	}
}
function BfeplayerRightStop2()
{
	$Bplayer.moveRight = false ;	
}
function BfeplayerJump2()
{
	if (!$Bplayer.inCycle && !$Bplayer.onMove1)
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
function bfearHit1()
{
	if (!$Bplayer.inCycle && !$Bplayer.onMove3)
	$Bplayer.attack1();	
}
function bfearCycle2()
{
	if (!$Bplayer.onMove1 && !$Bplayer.onMove3)
	$Bplayer.attackCycle();	
}
function bfearFire3()
{
	if (!$Bplayer.onMove1 && !$Bplayer.inCycle)
	$Bplayer.fearAttack3();	
}

function BfearClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BfearClass::updateH(%this)
{
	if (!$Bplayer.onMove3){
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
					   %this.setLinearVelocityX(-%this.movementSpeed + $Bplayer.moveX) ;
					}
				}
				if (%this.moveRight)
				{
					if (!%this.againstRightWall && $Bplayer.smashX == 0)
					{
						$Bplayer.isLeft = false ;
						%this.againstLeftWall = false ;
						%this.setLinearVelocityX(%this.movementSpeed + $Bplayer.moveX) ;
					}
				}
				if (%this.moveLeft == %this.moveRight || %this.onMove1 || %this.inCycle || %this.onMove3)
				{
					if ($Bplayer.smashX == 0)
						%this.setLinearVelocityX($Bplayer.moveX) ;
					
				}
			}
			else if ($Bplayer.smashX != 0){
				
				%this.setLinearVelocityX($Bplayer.smashX) ;
				return ;
			}
	}else{
		if ($Bplayer.isLeft == false){
			%this.setLinearVelocityX(100);
		}else{
			%this.setLinearVelocityX(-100);
		}
		if(%this.againstLeftWall || %this.againstRightWall){
			%this.outtaMove3Charge();
		}	
			
	}
		
}

// When player is moving up or down
function BfearClass::updateV(%this)
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
function BfearClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BfearClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "fearDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.setBlendAlpha(0);
					return;
				}
			}else
			{
				%this.playAnimation(fearDeath) ;
			}
	}
	else if ($Bplayer.onMove3){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearFireAttack")
			{
				if (%this.getIsAnimationFinished()){
					return;
				}
			}else
			{
				%this.playAnimation(fearFireAttack) ;
			}
		}
	}
	else if ($Bplayer.inCycle){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearAttack2Animation")
			{
				if (%this.getIsAnimationFinished()){
					$Bplayer.cycleCount += 1 ;
					if ($Bplayer.cycleCount >= 2){
						%this.outtaCycle();
					}else{
						%this.playAnimation(fearAttack2Animation) ;	
					}
					return;
				}
			}else
			{
				%this.playAnimation(fearAttack2Animation) ;
			}
		}
	}
	else if ($Bplayer.onMove1){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearAttack")
			{
				if (%this.getIsAnimationFinished()){
					$Bplayer.onMove1=false;
					%this.fearStaffClass.safeDelete();
					return;
				}
			}else
			{
				%this.playAnimation(fearAttack) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(fearJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(fearFall) ;
			}
		}
	}
	// If moving
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "fearWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(fearWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(fearWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "fearStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(fearStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(fearStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function BfearClass::die(%this)
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
function BfearClass::spawn(%this)
{
	$Bplayer.isDead = false ;
	$Bplayer.moveLeft = false ;
	$Bplayer.moveRight = false ;
	$Bplayer.isAirborne = false ;
	$Bplayer.doubleJ = false ;
	$warning2.happened = false;
	
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	%this.setBlendColour(1,1,1,1);
	
	$Bplayer.health = 200 ;
	player2Health.text = $Bplayer.health ;
	$Bplayer.lives = $Bplayer.lives - 1;
	player2Lives.text = $Bplayer.lives ;
	
	%this.setEnabled(true) ;
}

// When player is moving up or down
function BfearClass::updateVertical(%this)
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
function BfearClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player){
		if ($Bplayer.onMove3){
			if (%dstObj.getPositionX() > %srcObj.getPositionX()){
				%dstObj.smashX += 20;
			}else {
				%dstObj.smashX += -20;
			}
						
			%dstObj.health = %dstObj.health - 15 ;
			playerHealth.text = %dstObj.health ;
				
			if (%dstObj.health <= 0 && !$player.isDead){
				
				%dstObj.die() ;	
				
			}
			
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
			%srcObj.outtaMove3Charge();
		}
	}
	else if (%dstObj.class $= "pit")
	{
		$Bplayer.health = 0 ;
		player2Health.text = $Bplayer.health ;
		%srcObj.die();
	}
	else if (%dstObj.class $= "bulletClass")
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
	else if (%dstObj.class $= "bulletClass2")
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
	else if (%dstObj.class $= "nukeClass")
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
function BfearClass::jumpL(%this){
	if (!$Bplayer.inCycle || !$Bplayer.onMove3)
		%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BfearClass::jumpR(%this){
	if (!$Bplayer.inCycle || !$Bplayer.onMove3)
		%this.setPositionX(%this.getPositionX() + 6);
	
}

function BfearClass::attack1(%this){
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
		
		%this.schedule(100,"createStaffClass");
		$Bplayer.onMove1 = true ;
		$Bplayer.onCool1 = true ;
		%this.schedule(500,"cooldown1");
		
	}
	
}
function BfearClass::createStaffClass(%this){
	if ($Bplayer.isLeft == false && !$Bplayer.isDead){	
			%this.fearStaffClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = fearStaffClass ;
				source = %this ;
			} ;
			%this.fearStaffClass.fire2();
			%this.fearStaffClass.setPositionX(%this.getPositionX() + 5.89);
				
		}else{
					
			%this.fearStaffClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = fearStaffClass ;
				source = %this ;
			} ;	
			%this.fearStaffClass.fire2();
			%this.fearStaffClass.setPositionX(%this.getPositionX() - 5.89);	
					
		}
}
function BfearClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BfearClass::attackCycle(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.fearCircleClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = fearCircleClass ;
			source = %this ;
		} ;
		%this.fearCircleClass.fire2();
		%this.fearCircleClass.setPositionX(%this.getPositionX() + 5.89);	
		
		}else{
			
		%this.fearCircleClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = fearCircleClass ;
			source = %this ;
		} ;
		%this.fearCircleClass.fire2();
		%this.fearCircleClass.setPositionX(%this.getPositionX() - 5.89);			
			
	}
	
	$Bplayer.onCool2 = true ;
	$Bplayer.inCycle = true ;
	$Bplayer.cycleCount = 0 ;
	
	}
	
}
function BfearClass::outtaCycle(%this){
	if ($Bplayer.inCycle && !$Bplayer.isDead){
		$Bplayer.inCycle = false ;
		%this.fearCircleClass.safeDelete();
		%this.schedule(850,"cooldown2");
	}
}
function BfearClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BfearClass::fearAttack3(%this){
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
		
		$Bplayer.onMove3 = true ;
		$Bplayer.onCool3 = true ;
		%this.schedule(500,"outtaMove3Charge");
		
	}
}
function BfearClass::outtaMove3Charge(%this){
	if (%this.onMove3){
		%this.onMove3 = false ;
		%this.schedule(4500,"cooldown3");
	}
}
function BfearClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
		
	
}
function BfearClass::note3Hit(%this){
	
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
function BfearClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}
