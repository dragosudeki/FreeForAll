function rupesClass::start(%this)
{
	$player = %this ;
	$player.startX = %this.getPositionX() ;
	$player.startY = %this.getPositionY() ;
	$player.movementSpeed = 40 ;
	$player.airborne = false ;
	
	$player.collision = 0.727;
	
	$player.moveX = 0 ;
	
	$player.onCharge = false ;
	
	
	
	$player.onCool2 = false ;
	$player.onCool1 = false ;
	$player.onCool3 = false ;
	$player.doubleJ = false ;
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = 3 ;
	$player.isDead = false ;
	$player.note3Hit = false ;
	
	moveMap.bindCmd(keyboard, "left", "playerLeft23();", "playerLeftStop23();");
	moveMap.bindCmd(keyboard, "right", "playerRight23();", "playerRightStop23();");
	moveMap.bindCmd(keyboard, "up", "playerJump23();", "");
	moveMap.bindCmd(keyboard, "down" , "playerDown23();","playerDownStop23();");
	moveMap.bindCmd(keyboard, "comma", "rpunch();", "");
	moveMap.bindCmd(keyboard, "period", "rcharge();", "");
	moveMap.bindCmd(keyboard, "M", "rshoot();", "");
	moveMap.bindCmd(keyboard, "P", "gamePause23();", "");
	
	%this.setCollisionPolyCustom(4,"-0.584 0.727","0.584 0.727","0.584 -0.727","-0.584 -0.727");
	%this.setPosition($player.startX,$player.startY);
	%this.setSize(27.652,27.652);
	%this.setLayer(1);
	%this.setGraviticConstantForce(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	playerGUI.playAnimation(rupesStandAnimation);
	
	
}

function gamePause23(){
	
	$player.checkPause();	
	
}
function playerDown23()
{
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 19 20 21 22 23 24 25");
}
function playerDownStop23(){
	$player.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
}
// All keyboard inputs
function playerLeft23()
{
	if (!$player.isDead && !$player.onCharge && !$player.isPunching)
	{
		$player.moveLeft = true ;
	}
}
function playerLeftStop23()
{
	$player.moveLeft = false ;
}
function playerRight23()
{
	if (!$player.isDead && !$player.onCharge && !$player.isPunching)
	{
		$player.moveRight = true ;	
	}
}
function playerRightStop23()
{
	$player.moveRight = false ;	
}
function playerJump23()
{
	if (!$player.airborne && !$player.isDead)
	{
		$player.setLinearVelocityY(-75) ;
		$player.airborne = true ;
		$player.doubleJ = false ;
	}
	else if (!$player.doubleJ && !$player.isDead){
		
		$player.doubleJ = true ;
		$player.setLinearVelocityY(-65);
		
	}
}
function rpunch() {
	if (!$player.onCharge){
		$player.punch() ;
	}
}
function rcharge() {
	if (!$player.onCharge){
		$player.charge() ;
	}
	
}
function rshoot() {
	if (!$player.onCharge){
		$player.blowLeaf() ;
	}
}

function rupesClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function rupesClass::updateH(%this)
{
		
		if ($player.onCharge == true){
			
			if ($player.isLeft == false){
				
				%this.setLinearVelocityX(100);
				
			}else{
				
				%this.setLinearVelocityX(-100);
				
			}
			if(%this.againstLeftWall || %this.againstRightWall){
				
				$player.onCharge = false ;	
				
			}
		}else{
			
			if ($player.smashX > 0){
			
				$player.smashX += -2;
			}
			else if ($player.smashX < 0){
				
				$player.smashX += 2;
			}
			
			if (!$player.isPunching){
				if ($player.smashX == 0){
					if (%this.moveLeft == %this.moveRight)
					{
						if ($player.smashX == 0)
							%this.setLinearVelocityX($Bplayer.moveX) ;
					}
				
				
					if (%this.moveLeft)
					{
						if(!%this.againstLeftWall && $Bplayer.smashX == 0)
						{
						   %this.againstRightWall = false ;
						   $player.isLeft = true ;
						   %this.setLinearVelocityX(-%this.movementSpeed + $Bplayer.moveX + $Bplayer.smashX) ;
						}
					}
					if (%this.moveRight)
					{
						if (!%this.againstRightWall && $Bplayer.smashX == 0)
						{
							$player.isLeft = false ;
							%this.againstLeftWall = false ;
							%this.setLinearVelocityX(%this.movementSpeed + $Bplayer.moveX + $Bplayer.smashX) ;
						}
					}
				}
				else {
					
					%this.setLinearVelocityX($player.smashX) ;
				}
			}			
		}
}

// When player is moving up or down
function rupesClass::updateV(%this)
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
function rupesClass::onUpdate(%this)
{
	if (!$player.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function rupesClass::setCurrentAnimation(%this)
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
	
	if ($player.isDead){
		
		if (%this.getAnimationName() $= "lawyerDeathAnimation")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(lawyerDeathAnimation) ;
					return;
				}
			}else
			{
				%this.playAnimation(lawyerDeathAnimation) ;
			}
		
	}
	// Charging
	else if ($player.onCharge == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
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
	else if ($player.isBlowing == true)
	{
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesShootAnimation")
			{
				if (%this.getIsAnimationFinished())
				{
					$player.isBlowing = false ;
					return;
				}
			}else
			{
				%this.playAnimation(rupesShootAnimation) ;
			}
		}
	}
	else if ($player.isPunching){
		
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesPunchAnimation")
			{
				if (%this.getIsAnimationFinished())
				{
					$player.isPunching = false ;
					
					return;
				}
			}else
			{
				%this.playAnimation(rupesPunchAnimation) ;
			}
		}
		
	}
	// If moving
	else if ($player.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
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
	else if ($player.moveLeft == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($player.cantDie == false)
		{
			if (%this.getAnimationName() $= "rupesWalkL")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(rupesWalkL) ;
					return;
				}
			}else
			{
				%this.playAnimation(rupesWalkL) ;
			}
		}
	}
	// Standing
	else {
		if ($player.isLeft == false){
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
		else if ($player.isLeft == true){
			
			if (%this.getAnimationName() $= "rupesStandL")
				{
					if (%this.getIsAnimationFinished())
					{
						%this.playAnimation(rupesStandL) ;
						return;
					}
				}else
				{
					%this.playAnimation(rupesStandL) ;
				}
			
		}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function rupesClass::die(%this)
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
		$timescale = 0;
		canvas.pushDialog(RoundEnd);
		if (end.text $= "Player 2 Wins"){
			end.text = "Tie";
		}
		else
		end.text = "Player 1 Wins";
	}
}

// When player will die
function rupesClass::spawn(%this)
{
	$player.isDead = false ;
	$player.moveLeft = false ;
	$player.moveRight = false ;
	$player.isAirborne = false ;
	$player.onCharge = false ;
	
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	$player.health = 200 ;
	playerHealth.text = $player.health ;
	$player.lives = $player.lives - 1;
	playerLives.text = $player.lives ;
	
	%this.setEnabled(true) ;
}

// When playercollides with ghost
function rupesClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $Bplayer)
	{
		
		if ($player.onCharge == true){
			
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
			$player.onCharge = false ;
			
		}
		else if ($player.isPunching && !$player.punchHasDam){
			
			$player.punchHasDam = true ;
			$Bplayer.setLinearVelocityY(-50);
			
					
			$Bplayer.health = $Bplayer.health - 4 ;
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
			
		}
		
	}
	if (%dstObj.class $= "pit")
	{
		$player.health = 0 ;
		playerHealth.text = $player.health ;
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
	else if (%dstObj.class $= "note1Class" )
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
		
		if ($player.health <= 0 && $player.isDead == false){
			
			%srcObj.die();
			
		}
		
		PlayerHealth.text = $player.health ;
		
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
		
		%dstObj.safeDelete();
	}
	else if (%dstObj.class $= "note2Class" )
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
		
		$player.health = $player.health - 4 ;
		
		if ($player.health <= 0 && $player.isDead == false){
			
			%srcObj.die();
			
		}
		
		PlayerHealth.text = $player.health ;
		
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
		
		%dstObj.safeDelete();
	}
	if (%dstObj.class $= "leafClass")
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
				
		$player.health = $player.health - 4 ;
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
function rupesClass::jumpL(%this){
	
	if (!$player.onCharge)
		%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function rupesClass::jumpR(%this){
	
	if (!$player.onCharge)
		%this.setPositionX(%this.getPositionX() + 6);
	
}

function rupesClass::punch(%this){
	
	
	if ($player.onCool2 == false && !$player.isDead && $player.smashX == 0){
		if ($player.isLeft == false){
		
			%this.setLinearVelocityX(50);
			
		}else{
				
			%this.setLinearVelocityX(-50);	
				
		}
		
		$player.isPunching = true ;
		$player.onCool2 = true ;
		$player.punchHasDam = false ;
		%this.schedule(1000,"cooldown1");
	
	}
	
}
function rupesClass::cooldown1(%this){

	$player.onCool2 = false ;	
	
}
function rupesClass::cooldown2(%this){

	$player.onCool1 = false ;	
	echo("TEST2");
	
}
function rupesClass::blowLeaf(%this){
	
	
	if ($player.onCool1 == false && !$player.isDead){
	if ($player.isLeft == false){
	
		%this.leafClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = leafClass ;
			source = %this ;
		} ;
		%this.leafClass.fire();
		%this.leafClass.setFlip(false,false);
		%this.leafClass.setLinearVelocityX(65);
		
		}else{
			
		%this.leafClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = leafClass ;
			source = %this ;
		} ;
		%this.leafClass.fire();
		%this.leafClass.setFlip(true,false);
		%this.leafClass.setLinearVelocityX(-65);		
			
	}
	$player.isPunching = false ;
	$player.onCool1 = true ;
	$player.isBlowing = true ;
	%this.schedule(500,"cooldown2");
	
	}
	
}
function rupesClass::charge(%this){
	
	
	if ($player.onCool3 == false && !$player.isDead){
		
		$player.onCharge = true ;
		$player.isPunching = false ;
		$player.onCool3 = true ;
		%this.schedule(750,"outtaCharge");
		%this.schedule(5000,"cooldown3");
	
	}
	
}
function rupesClass::outtaCharge(%this){

	$player.onCharge = false ;
		
	
}
function rupesClass::cooldown3(%this){

	$player.onCool3 = false ;
		
	
}
function rupesClass::note3Hit(%this){
	
	if ($player.note3Hit == false){
		
		$player.health = $player.health - 2 ;
		
		if ($player.health <= 0 && $player.isDead == false){
			
			%this.die();
			
		}
		
		PlayerHealth.text = $player.health ;
		$player.note3Hit = true ;
		
		%this.schedule(200,"note3CD");
		
	}
	
}
function rupesClass::note3CD(%this){
	
	$player.note3Hit = false ;
	
}