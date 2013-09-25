function BbioClass::start(%this)
{
	$Bplayer = %this ;
	$Bplayer.startX = %this.getPositionX() ;
	$Bplayer.startY = %this.getPositionY() ;
	$Bplayer.movementSpeed = 60 ;
	$Bplayer.airborne = false ;
	
	$Bplayer.collision = 0.805;
	$Bplayer.collision2 = 0.530;
	%this.sideCollision = 0.270;
	
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
	
	moveMap.bindCmd(keyboard, "A", "BplayerLeft2();", "BplayerLeftStop2();");
	moveMap.bindCmd(keyboard, "D", "BplayerRight2();", "BplayerRightStop2();");
	moveMap.bindCmd(keyboard, "W", "BplayerJump2();", "");
	moveMap.bindCmd(keyboard, "S" , "BioBplayerDown2();","BioBplayerDownStop2();");
	moveMap.bindCmd(keyboard, "H", "Bbiomove1();", "");
	moveMap.bindCmd(keyboard, "J", "Bbiomove2();", "");
	moveMap.bindCmd(keyboard, "K", "Bbiomove3();", "");
	
	%this.setCollisionPolyCustom(4,"-0.270 0.805","0.270 0.805","0.270 -0.530","-0.270 -0.530");
	%this.setPosition($Bplayer.startX,$Bplayer.startY);
	%this.setSize(29.904,31.433);
	%this.setLayer(2);
	%this.setGraviticConstantForce(1);
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 15 16 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	BplayerGUI.playAnimation(BioStand);
	
}
function BbioClass::solo(%this)
{
	%force = 20;
    sceneWindow2D.mount($Bplayer, "0 0", %force, true);
}

function gamePause2(){
	
	$Bplayer.checkPause();	
	
}
function BioBplayerDown2()
{
	$Bplayer.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 10 13 14 19 20 21 22 23 24 25");
}
function BioBplayerDownStop2(){
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
function Bbiomove1()
{
	if (!$Bplayer.onCool1 && !$Bplayer.fire3 && !$Bplayer.fire2)
		$Bplayer.fire1 = true;	
}
function Bbiomove2()
{
	if (!$Bplayer.onCool2 && !$Bplayer.fire1 && !$Bplayer.fire3)
		$Bplayer.fireNote2();	
}
function Bbiomove3()
{
	if (!$Bplayer.onCool3 && !$Bplayer.fire1 && !$Bplayer.fire2)
		$Bplayer.fire3 = true;	
}

function BbioClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function BbioClass::updateH(%this)
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
			if (%this.moveLeft == %this.moveRight || %this.fire1 || %this.fire2 || %this.fire3)
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
function BbioClass::updateV(%this)
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
function BbioClass::onUpdate(%this)
{
	if (!$Bplayer.isDead){
		%this.updateH();
		%this.updateV();
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function BbioClass::setCurrentAnimation(%this)
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
		
		if (%this.getAnimationName() $= "BioDeath")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(BioDeath) ;
			}
		
	}
	else if (%this.fire3){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioMove3")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.fireNote3();
					return;
				}
			}else
			{
				%this.playAnimation(BioMove3) ;
			}
		}
	}
	else if (%this.fire2){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioMove2")
			{
				%this.setLinearVelocityY(0);
				if (%this.getIsAnimationFinished())
				{
					
					return;
				}
			}else
			{
				%this.playAnimation(BioMove2) ;
			}
		}
	}
	else if (%this.fire1){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioMove1")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.fireMissile();
					return;
				}
			}else
			{
				%this.playAnimation(BioMove1) ;
			}
		}
	}
	else if (%yVelocity < 0){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioJump")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(BioJump) ;
			}
		}
	}
	else if (%yVelocity > 0){
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioFall")
			{
				if (%this.getIsAnimationFinished())
				{
					return;
				}
			}else
			{
				%this.playAnimation(BioFall) ;
			}
		}
	}
	// If moving
	else if ($Bplayer.moveLeft == true || $Bplayer.moveRight == true)
	{
		// If he isnt invunerable then he will walk normally
		if ($Bplayer.cantDie == false)
		{
			if (%this.getAnimationName() $= "BioWalk1")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(BioWalk1) ;
					return;
				}
			}else
			{
				%this.playAnimation(BioWalk1) ;
			}
		}
	}
	else {
		if (%this.getAnimationName() $= "BioStand")
			{
				if (%this.getIsAnimationFinished())
				{
					%this.playAnimation(BioStand) ;
					return;
				}
			}else
			{
				%this.playAnimation(BioStand) ;
			}
		
	}
	
		
		
		
}
//When user loses all health or jumps off map
function BbioClass::die(%this)
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
function BbioClass::spawn(%this)
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
function BbioClass::updateVertical(%this)
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
function BbioClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function BbioClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function BbioClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function BbioClass::fireMissile(%this){
	if ($Bplayer.onCool1 == false && !$Bplayer.isDead){
		if ($Bplayer.isLeft == false){
		
			%this.slimeBallClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = slimeBallClass ;
				source = %this ;
			} ;
			%this.slimeBallClass.fire2();
			%this.slimeBallClass.setPositionX(%this.getPositionX()+5);
			%this.slimeBallClass.setLinearVelocityX(80);
			
		}else{
				
			%this.slimeBallClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = slimeBallClass ;
				source = %this ;
			} ;
			%this.slimeBallClass.fire2();
			%this.slimeBallClass.setPositionX(%this.getPositionX()-5);
			%this.slimeBallClass.setLinearVelocityX(-80);
			%this.slimeBallClass.setFlip(true,false);		
				
		}
		
		$Bplayer.fire1 = false ;
		$Bplayer.onCool1 = true ;
		%this.schedule(600,"cooldown1");
	
	}
	
}
function BbioClass::cooldown1(%this){

	$Bplayer.onCool1 = false ;	
	
}
function BbioClass::fireNote2(%this){
	
	
	if ($Bplayer.onCool2 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.slimePillarClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = slimePillarClass ;
			source = %this ;
		} ;
		%this.slimePillarClass.fire2();
		%this.slimePillarClass.setPositionX(%this.getPositionX() + 19);
		
		}else{
			
		%this.slimePillarClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = slimePillarClass ;
			source = %this ;
		} ;
		%this.slimePillarClass.fire2();
		%this.slimePillarClass.setPositionX(%this.getPositionX() - 19);	
			
	}
	
	$Bplayer.fire2 = true ;
	$Bplayer.onCool2 = true ;
	
	}
	
}
function BbioClass::cooldown2(%this){

	$Bplayer.onCool2 = false ;	
	
}
function BbioClass::fireNote3(%this){
	
	
	if ($Bplayer.onCool3 == false && !$Bplayer.isDead){
	if ($Bplayer.isLeft == false){
	
		%this.plagueBallClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = plagueBallClass ;
			source = %this ;
		} ;
		%this.plagueBallClass.fire();
		%this.plagueBallClass.setPositionX(%this.getPositionX()+3);
		%this.plagueBallClass.setLinearVelocityX(60);
		
		}else{
			
		%this.plagueBallClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = plagueBallClass ;
			source = %this ;
		} ;
		%this.plagueBallClass.fire();
		%this.plagueBallClass.setPositionX(%this.getPositionX()-3);
		%this.plagueBallClass.setLinearVelocityX(-60);	
		%this.plagueBallClass.setFlip(true,false);
			
	}
	
	$Bplayer.fire3 = false ;
	$Bplayer.onCool3 = true ;
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function BbioClass::cooldown3(%this){

	$Bplayer.onCool3 = false ;
		
	
}
function BbioClass::note3Hit(%this){
	
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
function BbioClass::note3CD(%this){
	
	$Bplayer.note3Hit = false ;
	
}