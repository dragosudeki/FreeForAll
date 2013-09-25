function elTontaClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 60 ;
	$player.airborne = false ;
	
	$player.collision = 0.800;
	$player.collision2 = 0.800;
	%this.sideCollision = 0.6;
	
	$player.moveX = 0 ;
	
	$player.onCool1 = false ;
	$player.onCool2 = false ;
	$player.onCool3 = false ;
	$player.doubleJ = false ;
	
	$player.pooNum = 5;
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = 3 ;
	$player.isDead = false ;
	$player.note3Hit = false ;
	
	moveMap.bindCmd(keyboard, "left", "elTontaplayerLeft2();", "elTontaplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "right", "elTontaplayerRight2();", "elTontaplayerRightStop2();");
	moveMap.bindCmd(keyboard, "up", "elTontaplayerJump2();", "");
	moveMap.bindCmd(keyboard, "down" , "elTontaplayerDown2();","playerDownStop2();");
	moveMap.bindCmd(keyboard, "numpad1", "poop1();", "");
	moveMap.bindCmd(keyboard, "numpad2", "elDash();", "");
	moveMap.bindCmd(keyboard, "numpad3", "dynamoPoo();", "");
	moveMap.bindCmd(keyboard, "P", "gamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.600 0.800","0.600 0.800","0.600 -0.800","-0.600 -0.800");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(21.3,21.3);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(eltontaStand);
	
}
function elTontaClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($player, "0 0", %force, true);
}

function gamePause2(){
	
	$player.checkPause();	
	
}
function elTontaplayerDown2()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function elTontaplayerDownStop2(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function elTontaplayerLeft2()
{
	if (!$player.isDead && !$player.inDash)
	{
		$player.moveLeft = true ;
	}
}
function elTontaplayerLeftStop2()
{
	$player.moveLeft = false ;
}
function elTontaplayerRight2()
{
	if (!$player.isDead && !$player.inDash)
	{
		$player.moveRight = true ;	
	}
}
function elTontaplayerRightStop2()
{
	$player.moveRight = false ;	
}
function elTontaplayerJump2()
{
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
function poop1()
{
	if (!$player.airborne && !$player.inDash)
		$player.pooPoop();	
}
function elDash()
{
	$player.donkeyDash();	
}
function dynamoPoo()
{
	$player.dynamoPoo();	
}

function elTontaClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function elTontaClass::updateH(%this)
{
	if ($player.inDash){
		
			if ($player.isLeft == false){
				
				%this.setLinearVelocityX(100);
				
			}else{
				
				%this.setLinearVelocityX(-100);
				
			}
			if(%this.againstLeftWall || %this.againstRightWall){
				
				$player.inDash = false ;	
				
			}
			
	}
	else if (!$player.inDash){
		
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
function elTontaClass::updateV(%this)
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
function elTontaClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function elTontaClass::setCurrentAnimation(%this)
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
	//$player.inDash
	if ($player.isDead){
		
		if (%this.getAnimationName() $= "eltontaDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(eltontaDeath) ;
			}
		
	}
	else if ($player.inDash){
		
		if (%this.getAnimationName() $= "eltontaDash")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(eltontaDash) ;
			}
		
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "eltontaJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(eltontaJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "eltontaFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(eltontaFall) ;
			}
		}
	}
	// If moving
	else if ($player.moveLeft == true || $player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "eltontaWalk")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(eltontaWalk) ;
					return;
				}
			}else
			{
				%this.playAnimation(eltontaWalk) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "eltontaStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(eltontaStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(eltontaStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function elTontaClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
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
function elTontaClass::spawn(%this)
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
function elTontaClass::updateVertical(%this)
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
// When player collides with anything
function elTontaClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $Bplayer)
	{
		
		if ($player.inDash == true){
			
			if (%dstObj.getPositionX() > %srcObj.getPositionX())
			{
				
				$Bplayer.smashX += 60;
				
			}
			else {
				
				$Bplayer.smashX += -60;
				
			}
					
			$Bplayer.health = $Bplayer.health - 8 ;
			player2Health.text = $Bplayer.health ;
			
			if ($Bplayer.health <= 0 && !$Bplayer.isDead){
			
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
			$player.inDash = false ;
			
		}
	}
	else if (%dstObj.class $= "pit")
	{
		$player.health = 0 ;
		playerHealth.text = $player.health ;
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
function elTontaClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function elTontaClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function elTontaClass::pooPoop(%this){
	
	
	if ($player.onCool1 == false && !$player.isDead && $player.pooNum > 0){
	if ($player.isLeft == false){
	
		%this.poopClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = poopClass ;
			source = %this ;
			speed = %this.moveX ;
		} ;
		%this.poopClass.fire();
		%this.poopClass.setPositionX(%this.getPositionX() - 8.322);
		%this.poopClass.setPositionY(%this.getPositionY() + 6.200);
		
		}else{
			
		%this.poopClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = poopClass ;
			source = %this ;
			speed = %this.moveX ;
		} ;
		%this.poopClass.fire();
		%this.poopClass.setPositionX(%this.getPositionX() + 8.322);
		%this.poopClass.setPositionY(%this.getPositionY() + 6.200);
			
	}
	
	$player.pooNum += -1 ;
	$player.onCool1 = true ;
	%this.schedule(500,"cooldown1");
	
	}
	
}
function elTontaClass::cooldown1(%this){

	$player.onCool1 = false ;	
	
}
function elTontaClass::donkeyDash(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead){
		if ($player.isLeft == false){
		
				%this.setLinearVelocityX(80);
			
			}else{
			
				%this.setLinearVelocityX(-80);	
			
		}
		
		$player.onCool2 = true ;
		$player.inDash = true ;
		%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 13 14 15 17 19 20 21 22 23 24 25");
		%this.schedule(800,"outtaDash");
		%this.schedule(2000,"cooldown2");
	
	}
	
}
function elTontaClass::outtaDash(%this){

	$player.inDash = false ;	
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	
}
function elTontaClass::cooldown2(%this){

	$player.onCool2 = false ;	
	
}
function elTontaClass::dynamoPoo(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
		if (!$player.liveBomb & !$player.inDash && !$player.airborne){
				if ($player.isLeft == false){
				
					%this.dynamiteClass = new t2dAnimatedSprite()
					{
						scenegraph = %this.scenegraph ;
						class = dynamiteClass ;
						source = %this ;
						speed = %this.moveX ;
					} ;
					%this.dynamiteClass.fire();
					%this.dynamiteClass.setPositionX(%this.getPositionX() - 8.322);
					%this.dynamiteClass.setPositionY(%this.getPositionY() + 6.800);
					
					}else{
						
					%this.dynamiteClass = new t2dAnimatedSprite()
					{
						scenegraph = %this.scenegraph ;
						class = dynamiteClass ;
						source = %this ;
						speed = %this.moveX ;
					} ;
					%this.dynamiteClass.fire();
					%this.dynamiteClass.setPositionX(%this.getPositionX() + 8.322);
					%this.dynamiteClass.setPositionY(%this.getPositionY() + 6.800);
						
				}
			
			$player.liveBomb = true ;
		}
		else if ($player.liveBomb){
		
			%this.dynamiteClass.delete();
		
		}
	
	}
	
	
}
function elTontaClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function elTontaClass::note3Hit(%this){
	
	if ($player.note3Hit == false && !%this.inDash){
		
		$player.health = $player.health - 2 ;
		
		if ($player.health <= 0 && $player.isDead == false){
			
			%this.die();
			
		}
		
		PlayerHealth.text = $player.health ;
		$player.note3Hit = true ;
		
		%this.schedule(300,"note3CD");
}
	
}
function elTontaClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}