function BrupesClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 50 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.collision = 0.727;
	$Bplayer.collision2 = 0.727;
	%this.sideCollision = 0.584;
	
	$Bplayer.moveX = 0 ;
	
	$Bplayer.onCharge = false ;
	
	
	
	$Bplayer.onCool2 = false ;
	$Bplayer.onCool1 = false ;
	$Bplayer.onCool3 = false ;
	$Bplayer.doubleJ = false ;
	
	$Bplayer.health = 200 ;
	player2Health.text = $Bplayer.health ;
	$Bplayer.lives = 3 ;
	$Bplayer.isDead = false ;
	$Bplayer.note3Hit = false ;
	
	moveMap.bindCmd(keyboard, "A", "BplayerLeft23();", "BplayerLeftStop23();");
	moveMap.bindCmd(keyboard, "D", "BplayerRight23();", "BplayerRightStop23();");
	moveMap.bindCmd(keyboard, "W", "BplayerJump23();", "");
	moveMap.bindCmd(keyboard, "S" , "BplayerDown23();","BplayerDownStop23();");
	moveMap.bindCmd(keyboard, "J", "Brpunch();", "");
	moveMap.bindCmd(keyboard, "K", "Brcharge();", "");
	moveMap.bindCmd(keyboard, "H", "Brshoot();", "");
	
	%this.setCollisionPolyCustom(4,"-0.584 0.727","0.584 0.727","0.584 -0.727","-0.584 -0.727");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(27.652,27.652);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(rupesStandAnimation);
	
}
function BplayerDown23()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function BplayerDownStop23(){
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
}
// All keyboard inputs
function BplayerLeft23()
{
	if (!$Bplayer.isDead && !$Bplayer.onCharge && !$Bplayer.isPunching)
	{
		$Bplayer.moveLeft = true ;
	}
}
function BplayerLeftStop23()
{
	$Bplayer.moveLeft = false ;
}
function BplayerRight23()
{
	if (!$Bplayer.isDead && !$Bplayer.onCharge && !$Bplayer.isPunching)
	{
		$Bplayer.moveRight = true ;	
	}
}
function BplayerRightStop23()
{
	$Bplayer.moveRight = false ;	
}
function BplayerJump23()
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
function Brpunch() {
	if (!$Bplayer.onCharge){
		$Bplayer.punch() ;
	}
}
function Brcharge() {
	if (!$Bplayer.onCharge){
		$Bplayer.charge() ;
	}
	
}
function Brshoot() {
	if (!$Bplayer.onCharge){
		$Bplayer.blowLeaf() ;
	}
}

function BrupesClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BrupesClass::updateH(%this)
{
		
		if ($Bplayer.onCharge == true){
			
			if ($Bplayer.isLeft == false){
				
				%this.setLinearVelocityX(100);
				
			}else{
				
				%this.setLinearVelocityX(-100);
				
			}
			if(%this.againstLeftWall || %this.againstRightWall){
				
				$Bplayer.onCharge = false ;	
				
			}
		}else{
			
			if ($Bplayer.smashX > 0){
			
				$Bplayer.smashX += -2;
			}
			else if ($Bplayer.smashX < 0){
				
				$Bplayer.smashX += 2;
			}
			
			if (!$Bplayer.isPunching){
				if ($Bplayer.smashX == 0){
					if (%this.moveLeft == %this.moveRight)
					{
						if ($Bplayer.smashX == 0)
							%this.setLinearVelocityX($Bplayer.moveX) ;
					}
				
				
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
				}
				else {
					
					%this.setLinearVelocityX($Bplayer.smashX) ;
				}
			}
			
		}
}

// When player is moving up or down
function BrupesClass::updateV(%this)
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
function BrupesClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BrupesClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "rupesDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(rupesDeath) ;
			}
		
	}
	// Charging
	else if ($Bplayer.onCharge == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesChargeAnimation")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(rupesChargeAnimation) ;
					return;
				}
			}else
			{
				%this.playAnimation(rupesChargeAnimation) ;
			}
		}
	}
	else if ($Bplayer.isBlowing == true)
	{
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesShootAnimation")
			{
				if (%this.getIsAnimationFinished())
				{
					$Bplayer.isBlowing = false ;
					return;
				}
			}else
			{
				%this.playAnimation(rupesShootAnimation) ;
			}
		}
	}
	else if ($Bplayer.isPunching){
		
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesPunchAnimation")
			{
				if (%this.getIsAnimationFinished())
				{
					$Bplayer.isPunching = false ;
					
					return;
				}
			}else
			{
				%this.playAnimation(rupesPunchAnimation) ;
			}
		}
		
	}
	else if (%yVelocity < 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesJump")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(rupesJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesFall")
			{
				if (%this.getIsAnimationFinished())
				{
				}
			}else
			{
				%this.playAnimation(rupesFall) ;
			}
		}
	}
	// If moving
	else if ($Bplayer.moveRight == true || $Bplayer.moveLeft == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesWalkAnimation")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(rupesWalkAnimation) ;
					return;
				}
			}else
			{
				%this.playAnimation(rupesWalkAnimation) ;
			}
		}
	}
	// Standing
	else {
			if (%this.getAnimationName() $= "rupesStandAnimation")
				{
					if (%this.getIsAnimationFinished())
					{
						%this.playAnimation(rupesStandAnimation) ;
						return;
					}
				}else
				{
					%this.playAnimation(rupesStandAnimation) ;
				}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function BrupesClass::die(%this)
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
function BrupesClass::spawn(%this)
{
	$Bplayer.isDead = false ;
	$Bplayer.moveLeft = false ;
	$Bplayer.moveRight = false ;
	$Bplayer.isAirborne = false ;
	$Bplayer.onCharge = false ;
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

// When playercollides with ghost
function BrupesClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player)
	{
		
		if ($Bplayer.onCharge == true){
			
			if (%dstObj.getPositionX() > %srcObj.getPositionX())
			{
				
				$player.smashX += 60;
				
			}
			else {
				
				$player.smashX += -60;
				
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
			$Bplayer.onCharge = false ;
			
		}
		else if ($Bplayer.isPunching && !$Bplayer.punchHasDam){
			
			$Bplayer.punchHasDam = true ;
			$player.setLinearVelocityY(-50);
			
					
			$player.health = $player.health - 4 ;
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
function BrupesClass::jumpL(%this){
	
	if (!$Bplayer.onCharge)
		%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BrupesClass::jumpR(%this){
	
	if (!$Bplayer.onCharge)
		%this.setPositionX(%this.getPositionX() + 6);
	
}

function BrupesClass::punch(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead && $Bplayer.smashX == 0){
		if ($Bplayer.isLeft == false){
		
			%this.setLinearVelocityX(50);
			
		}else{
				
			%this.setLinearVelocityX(-50);	
				
		}
		
		$Bplayer.isPunching = true ;
		$Bplayer.onCool2 = true ;
		$Bplayer.punchHasDam = false ;
		%this.schedule(1000,"cooldown1");
	
	}
	
}
function BrupesClass::cooldown1(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BrupesClass::cooldown2(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BrupesClass::blowLeaf(%this){
	
	
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft == false){
		
			%this.leafClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = leafClass ;
				source = %this ;
			} ;
			%this.leafClass.fire2();
			%this.leafClass.setFlip(false,false);
			%this.leafClass.setLinearVelocityX(65);
			
		}else{
				
			%this.leafClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = leafClass ;
				source = %this ;
			} ;
			%this.leafClass.fire2();
			%this.leafClass.setFlip(true,false);
			%this.leafClass.setLinearVelocityX(-65);		
				
		}
		
		$Bplayer.onCool1 = true ;
		$Bplayer.isBlowing = true ;
		$Bplayer.isPunching = false ;
		%this.schedule(750,"cooldown2");
	
	}
	
}
function BrupesClass::cooldown2(%this){

	$Bplayer.onCool1 = false ;	
	
}function BrupesClass::charge(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
		
		$Bplayer.onCharge = true ;
		$Bplayer.isPunching = false ;
	
		$Bplayer.onCool3 = true ;
		%this.schedule(750,"outtaCharge");
		%this.schedule(5000,"cooldown3");
	
	}
	
}
function BrupesClass::outtaCharge(%this){

	$Bplayer.onCharge = false ;
		
	
}
function BrupesClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
		
	
}
function BrupesClass::note3Hit(%this){
	
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
function BrupesClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}