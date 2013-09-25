function BelTontaClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 60 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.collision = 0.800;
	$Bplayer.collision2 = 0.800;
	%this.sideCollision = 0.6;
	
	$Bplayer.moveX = 0 ;
	
	$Bplayer.onCool1 = false ;
	$Bplayer.onCool2 = false ;
	$Bplayer.onCool3 = false ;
	$Bplayer.doubleJ = false ;
	
	$Bplayer.pooNum = 5;
	
	$Bplayer.health = 200 ;
	player2Health.text = $Bplayer.health ;
	$Bplayer.lives = 3 ;
	$Bplayer.isDead = false ;
	$Bplayer.note3Hit = false ;
	
	moveMap.bindCmd(keyboard, "A", "BelTontaplayerLeft2();", "BelTontaplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "BelTontaplayerRight2();", "BelTontaplayerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "BelTontaplayerJump2();", "");
	moveMap.bindCmd(keyboard, "S" , "BelTontaplayerDown2();","BplayerDownStop2();");
	moveMap.bindCmd(keyboard, "H", "Bpoop1();", "");
	moveMap.bindCmd(keyboard, "J", "BelDash();", "");
	moveMap.bindCmd(keyboard, "K", "BdynamoPoo();", "");
	
	%this.setCollisionPolyCustom(4,"-0.600 0.800","0.600 0.800","0.600 -0.800","-0.600 -0.800");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(21.3,21.3);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(eltontaStand);
	
}
function BelTontaClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}

function BelTontaplayerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function BelTontaplayerDownStop2(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function BelTontaplayerLeft2()
{
	if (!$Bplayer.isDead && !$Bplayer.inDash)
	{
		$Bplayer.moveLeft = true ;
	}
}
function BelTontaplayerLeftStop2()
{
	$Bplayer.moveLeft = false ;
}
function BelTontaplayerRight2()
{
	if (!$Bplayer.isDead && !$Bplayer.inDash)
	{
		$Bplayer.moveRight = true ;	
	}
}
function BelTontaplayerRightStop2()
{
	$Bplayer.moveRight = false ;	
}
function BelTontaplayerJump2()
{
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
function Bpoop1()
{
	if (!$Bplayer.airborne && !$Bplayer.inDash)
		$Bplayer.pooPoop();	
}
function BelDash()
{
	$Bplayer.donkeyDash();	
}
function BdynamoPoo()
{
	$Bplayer.dynamoPoo();	
}

function BelTontaClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BelTontaClass::updateH(%this)
{
	if ($Bplayer.inDash){
		
			if ($Bplayer.isLeft == false){
				
				%this.setLinearVelocityX(100);
				
			}else{
				
				%this.setLinearVelocityX(-100);
				
			}
			if(%this.againstLeftWall || %this.againstRightWall){
				
				$Bplayer.inDash = false ;	
				
			}
			
	}
	else if (!$Bplayer.inDash){
		
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
				
			}
		}
		else if ($Bplayer.smashX != 0){
			
			%this.setLinearVelocityX($Bplayer.smashX) ;
			return ;
		}
	}
		
}

// When player is moving up or down
function BelTontaClass::updateV(%this)
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
function BelTontaClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BelTontaClass::setCurrentAnimation(%this)
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
	//$Bplayer.inDash
	if ($Bplayer.isDead){
		
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
	else if ($Bplayer.inDash){
		
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
		if ($Bplayer.cantDie == false)
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
		if ($Bplayer.cantDie == false)
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
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
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
function BelTontaClass::die(%this)
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
function BelTontaClass::spawn(%this)
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
function BelTontaClass::updateVertical(%this)
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
function BelTontaClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player)
	{
		
		if ($Bplayer.inDash == true){
			
			if (%dstObj.getPositionX() > %srcObj.getPositionX())
			{
				
				$player.smashX += 40;
				
			}
			else {
				
				$player.smashX += -40;
				
			}
					
			$player.health = $player.health - 8 ;
			playerHealth.text = $player.health ;
			
			if ($player.health <= 0 && !$player.isDead){
			
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
			$Bplayer.inDash = false ;
			
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
function BelTontaClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BelTontaClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function BelTontaClass::pooPoop(%this){
	
	
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead && $Bplayer.pooNum > 0){
	if ($Bplayer.isLeft == false){
	
		%this.poopClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = poopClass ;
			source = %this ;
			speed = %this.moveX ;
		} ;
		%this.poopClass.fire2();
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
		%this.poopClass.fire2();
		%this.poopClass.setPositionX(%this.getPositionX() + 8.322);
		%this.poopClass.setPositionY(%this.getPositionY() + 6.200);
			
	}
	
	$Bplayer.pooNum += -1 ;
	$Bplayer.onCool1 = true ;
	%this.schedule(500,"cooldown1");
	
	}
	
}
function BelTontaClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BelTontaClass::donkeyDash(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft == false){
		
				%this.setLinearVelocityX(80);
			
			}else{
			
				%this.setLinearVelocityX(-80);	
			
		}
		
		$Bplayer.onCool2 = true ;
		$Bplayer.inDash = true ;
		%this.schedule(600,"outtaDash");
		%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 13 14 15 16 19 20 21 22 23 24 25");
		%this.schedule(2000,"cooldown2");
	
	}
	
}
function BelTontaClass::outtaDash(%this){

	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	$Bplayer.inDash = false ;	
	
}
function BelTontaClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BelTontaClass::dynamoPoo(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
		if (!$Bplayer.liveBomb && !$Bplayer.inDash && !$Bplayer.airborne){
				if ($Bplayer.isLeft == false){
				
					%this.dynamiteClass = new t2dAnimatedSprite()
					{
						scenegraph = %this.scenegraph ;
						class = dynamiteClass ;
						source = %this ;
						speed = %this.moveX ;
					} ;
					%this.dynamiteClass.fire2();
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
					%this.dynamiteClass.fire2();
					%this.dynamiteClass.setPositionX(%this.getPositionX() + 8.322);
					%this.dynamiteClass.setPositionY(%this.getPositionY() + 6.800);
						
				}
			
			$Bplayer.liveBomb = true ;
		}
		else if ($Bplayer.liveBomb){
		
			%this.dynamiteClass.delete();
		
		}
	
	}
	
}
function BelTontaClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
		
	
}
function BelTontaClass::note3Hit(%this){
	
	if ($Bplayer.note3Hit == false && !%this.inDash){
		
		$Bplayer.health = $Bplayer.health - 2 ;
		
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){
			
			%this.die();
			
		}
		
		Player2Health.text = $Bplayer.health ;
		$Bplayer.note3Hit = true ;
		
		%this.schedule(300,"note3CD");
}
	
}
function BelTontaClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}