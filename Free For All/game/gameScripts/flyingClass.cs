function flyingClass::onLevelLoaded(%this,%scenegraph)
{
	$plane = %this ;
	$plane.startX = %this.getPositionX() ;
	$plane.startY = %this.getPositionY() ;
	$plane.movementSpeed = 40 ;
	
	$plane.health = 200 ;
	$plane.lives = 3 ;
	error("PLANE CREATED");
	moveMap.bindCmd(keyboard, "left", "planeLeft();", "planeLeftS();");
	moveMap.bindCmd(keyboard, "right", "planeRight();", "planeRightS();");
	moveMap.bindCmd(keyboard, "up", "planeUp();", "planeUpS();");
	moveMap.bindCmd(keyboard, "down" , "planeDown();","planeDownS();");
	moveMap.bindCmd(keyboard, "numpad1", "note1();", "");
	moveMap.bindCmd(keyboard, "numpad2", "note2();", "");
	moveMap.bindCmd(keyboard, "numpad3", "note3();", "");
	moveMap.bindCmd(keyboard, "P", "gamePause2();", "");
	
	%this.setCollisionPolyCustom(4,"-0.643 1.000","0.643 1.000","0.643 -1.000","-0.643 -1.000");
	%this.setPosition($plane.startX,$plane.startY);
	%this.setSize(8.1,8.1);
	%this.setLayer(1);
	%this.setCollisionLayers("0 1 2 3 4 5 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;
	////
	
}

function gamePause2(){
	$plane.checkPause();	
}
function planeLeft(){
	$plane.left = true;	
	$plane.updateMovement();
}
function planeRight(){	
	$plane.right = true;
	$plane.updateMovement();
}
function planeUp(){
	$plane.up = true ;
	$plane.updateMovement();
}
function planeDown(){
	$plane.down = true ;
	$plane.updateMovement();
}
function planeLeftS(){
	$plane.left = false;
	$plane.updateMovement();	
}
function planeRightS(){	
	$plane.right = false;
	$plane.updateMovement();
}
function planeUpS(){
	$plane.up = false ;
	$plane.updateMovement();
}
function planeDownS(){
	$plane.down = false ;
	$plane.updateMovement();
}



function flyingClass::checkPause(%this){

	if ($timescale == 1){
		$timescale = 0 ;
		canvas.pushDialog(PauseTest);
	}
	
}

//When player is moving right or left
function flyingClass::updateMovement(%this)
{
	if (%this.up){
		%this.setLinearVelocityY(-%this.movementSpeed);
	}
	if (%this.left){
		%this.setLinearVelocityX((%this.movementSpeed * -0.9));
	}
	if (%this.right){
		%this.setLinearVelocityX(%this.movementSpeed);
	}
	if (%this.down){
		%this.setLinearVelocityY(%this.movementSpeed);
	}
	if (%this.up == %this.down){
		%this.setLinearVelocityY(0);
	}
	if (%this.left == %this.right){
		%this.setLinearVelocityX(0);
	}
		
}

//Updates the player's x and y as well as animation onto screen
function flyingClass::onUpdate(%this)
{
	%this.updateMovement();
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function flyingClass::setCurrentAnimation(%this)
{
	%xVelocity = %this.getLinearVelocityX();
	
		if (%this.getAnimationName() $= "_1")
		{
			if (%this.getIsAnimationFinished())
			{
				return;
			}
		}else
		{
			%this.playAnimation(_1) ;
		}	
}
//When user loses all health or jumps off map
function flyingClass::die(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0) ;
	%this.setConstantForceY(0) ;
	
	$plane.airborne = false ;
	$plane.isDead = true ;
	%this.setCurrentAnimation() ;
	
	if ($plane.lives > 0)
	%this.schedule( 2000, "spawn") ;
	else {
		$timescale = 0;
		canvas.pushDialog(RoundEnd);
		if (end.text $= "Player 1 Wins"){
			end.text = "Tie";
		}
		else
		end.text = "Player 2 Wins";
	}
}

// When player will die
function flyingClass::spawn(%this)
{
	$plane.isDead = false ;
	$plane.moveLeft = false ;
	$plane.moveRight = false ;
	$plane.isAirborne = false ;
	$plane.doubleJ = false ;
	
	%this.setCollisionActive (true, true) ;
	%this.setPosition(%this.startX, %this.startY) ;
	
	$plane.health = 200 ;
	playerHealth.text = $plane.health ;
	$plane.lives = $plane.lives - 1;
	playerLives.text = $plane.lives ;
	
	%this.setEnabled(true) ;
}

function flyingClass::onWorldLimit(%this, %mode, %limit)
{
    if (%limit $= "left" || %limit $= "right") {
       %this.setLinearVelocityX(0);
    }
	if (%limit $= "top" || %limit $= "bottom") {
       %this.setLinearVelocityY(0);
    }
}

// When playercollides with ghost
function flyingClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
}

// Jumps Left when hit
function flyingClass::jumpL(%this){
	
	%this.setPositionX(%this.getPositionX() - 6);
	
}
// Jumps right when hit
function flyingClass::jumpR(%this){
	
	%this.setPositionX(%this.getPositionX() + 6);
	
}

function flyingClass::fireMissile(%this){
	
	
	if ($plane.onCool1 == false && !$plane.isDead){
		if ($plane.isLeft == false){
		
			%this.note1Class = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = note1Class ;
				bomb = %this ;
			} ;
			%this.note1Class.fire();
			%this.note1Class.setLinearVelocityX(25 + ($plane.moveX / 1));
			
		}else{
				
			%this.note1Class = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = note1Class ;
				bomb = %this ;
			} ;
			%this.note1Class.fire();
			%this.note1Class.setLinearVelocityX(-25 + ($plane.moveX / 1));		
				
		}
		
		$plane.onCool1 = true ;
		%this.schedule(500,"cooldown1");
	
	}
	
}
function flyingClass::cooldown1(%this){

	$plane.onCool1 = false ;	
	
}
function flyingClass::fireNote2(%this){
	
	
	if ($plane.onCool2 == false && !$plane.isDead){
	if ($plane.isLeft == false){
	
		%this.note2Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note2Class ;
			bomb = %this ;
		} ;
		%this.note2Class.fire();
		%this.note2Class.setLinearVelocityX(30 + ($plane.moveX / 1));
		
		}else{
			
		%this.note2Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note2Class ;
			bomb = %this ;
		} ;
		%this.note2Class.fire();
		%this.note2Class.setLinearVelocityX(-30 + ($plane.moveX / 1));		
			
	}
	
	$plane.onCool2 = true ;
	%this.schedule(850,"cooldown2");
	
	}
	
}
function flyingClass::cooldown2(%this){

	$plane.onCool2 = false ;	
	
}
function flyingClass::fireNote3(%this){
	
	
	if ($plane.onCool3 == false && !$plane.isDead){
	if ($plane.isLeft == false){
	
		%this.note3Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note3Class ;
			bomb = %this ;
		} ;
		%this.note3Class.fire();
		%this.note3Class.setPositionX(%this.note3Class.getPositionX() + (%this.note3Class.getWidth() / 2));
		%this.note3Class.setLinearVelocityX(30);
		
		}else{
			
		%this.note3Class = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = note3Class ;
			bomb = %this ;
		} ;
		%this.note3Class.fire();
		%this.note3Class.setPositionX(%this.note3Class.getPositionX() - (%this.note3Class.getWidth() / 2));
		%this.note3Class.setLinearVelocityX(-30);		
			
	}
	
	$plane.onCool3 = true ;
	%this.schedule(5000,"cooldown3");
	
	}
	
}
function flyingClass::cooldown3(%this){

	$plane.onCool3 = false ;
		
	
}