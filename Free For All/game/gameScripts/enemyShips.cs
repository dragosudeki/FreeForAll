function leftBounds::onLevelLoaded(%this,%scenegraph){
	$basicships = 0;
	$coastships = 0;
	$explodeships = 0 ;
}
///////////////////////////////////////////////////////////////////////////////////////////////////////
function basicShip::onLevelLoaded(%this,%scenegraph){
	%this.startX = %this.getPositionX();
	%this.startY = getRandom(-35,35);
	%this.setLinearVelocityX(-50);
	
	%this.max = 10;
	
	%this.setCollisionPolyCustom(4,"-0.643 1.000","0.643 1.000","0.643 -1.000","-0.643 -1.000");
	%this.setPosition(%this.startX,%this.startY);
	%this.setSize(8.1,8.1);
	%this.setLayer(5);
	%this.setCollisionLayers("0 1 2 3 4 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;	
	%this.speedX = getRandom(-50,-30);
	%this.setLinearVelocityX(%this.speedX);
}
//Updates the player's x and y as well as animation onto screen
function basicShip::onUpdate(%this){
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function basicShip::setCurrentAnimation(%this){
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

// When player will die
function basicShip::respawn(%this){	
	$basicships += 1 ;
	if ($basicships < %this.max){
		%this.speedX = getRandom(-80,-45);
		%this.setLinearVelocityX(%this.speedX);
		%this.startY = getRandom(-35,35);
			
		error("Basic ships created: " SPC $basicships);
	}else
		error("Basic ships depleted" SPC $basicships);
}

function basicShip::onWorldLimit(%this, %mode, %limit){
    if (%limit $= "left" || %limit $= "right") {
       %this.setLinearVelocityX(0);
    }
	if (%limit $= "top" || %limit $= "bottom") {
       %this.setLinearVelocityY(0);
    }
}
// When playercollides with ghost
function basicShip::onCollision( %this, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts){
	%this.spawntime = getRandom(2000,3000);
	if (%dstObj.class $= "leftBounds"){
		%this.setLinearVelocityX(0);
		%this.setPosition(%this.startX,%this.startY);
		
		%this.schedule(%this.spawntime,"respawn");
			
		
	}else if (%dstObj.class $= "flyingClass"){
		%this.setLinearVelocityX(0);
		%this.setPosition(%this.startX,%this.startY);
		%this.schedule(%this.spawntime,"respawn");
		
	}
}
///////////////////////////////////////////////////////////////////////////////////////////////////////
function coastShip::onLevelLoaded(%this,%scenegraph){
	%this.startX = %this.getPositionX();
	%this.startY = getRandom(-35,35);
	%this.coast = false ;
	error("Coast start");
	%this.max = 10;
	
	%this.setCollisionPolyCustom(4,"-0.643 1.000","0.643 1.000","0.643 -1.000","-0.643 -1.000");
	%this.setPosition(%this.startX,%this.startY);
	%this.setSize(8.1,8.1);
	%this.setLayer(5);
	%this.setCollisionLayers("0 1 2 3 4 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;	
	%this.speedX = getRandom(-130,-90);
	%this.setLinearVelocityX(%this.speedX);
}
//Updates the player's x and y as well as animation onto screen
function coastShip::onUpdate(%this){
	if (%this.getPositionX() <= 0 && !%this.coast){
		%this.coast = true;
		%this.speedX = getRandom(-30,-15);
		%this.setLinearVelocityX(%this.speedX);
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function coastShip::setCurrentAnimation(%this){
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

// When player will die
function coastShip::respawn(%this){
	
	$coastships += 1 ;
	if ($coastships < %this.max){
		%this.speedX = getRandom(-130,-90);
		%this.setLinearVelocityX(%this.speedX);
		%this.startY = getRandom(-35,35);
		%this.coast = false ;
			
		error("Coast ships created: " SPC $coastships);
	}else
		error("Coast ships depleted" SPC $coastships);
}

function coastShip::onWorldLimit(%this, %mode, %limit){
    if (%limit $= "left" || %limit $= "right") {
       %this.setLinearVelocityX(0);
    }
	if (%limit $= "top" || %limit $= "bottom") {
       %this.setLinearVelocityY(0);
    }
}

// When playercollides with ghost
function coastShip::onCollision( %this, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts ){
	%this.spawntime = getRandom(2000,3000);
	if (%dstObj.class $= "leftBounds"){
		%this.setLinearVelocityX(0);
		%this.setPosition(%this.startX,%this.startY);
		
		%this.schedule(%this.spawntime,"respawn");
			
		
	}else if (%dstObj.class $= "flyingClass"){
		%this.setLinearVelocityX(0);
		%this.setPosition(%this.startX,%this.startY);
		%this.schedule(%this.spawntime,"respawn");
		
	}
}
///////////////////////////////////////////////////////////////////////////////////////////////////////
function explodeShip::onLevelLoaded(%this,%scenegraph){
	%this.startX = %this.getPositionX();
	%this.startY = getRandom(-35,35);
	%this.setLinearVelocityX(-50);
	
	%this.max = 10;
	
	%this.setCollisionPolyCustom(4,"-0.643 1.000","0.643 1.000","0.643 -1.000","-0.643 -1.000");
	%this.setPosition(%this.startX,%this.startY);
	%this.setSize(8.1,8.1);
	%this.setLayer(5);
	%this.setCollisionLayers("0 1 2 3 4 6 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.enableUpdateCallback() ;	
	%this.speedX = getRandom(-50,-30);
	%this.setLinearVelocityX(%this.speedX);
}
//Updates the player's x and y as well as animation onto screen
function explodeShip::onUpdate(%this){
	if (%this.getPositionX() <= 0){
		
		
		%this.explodeDebris = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = explodeDebris ;
			source = %this ;
		} ;
		%this.explodeDebris.create();
		%this.explodeDebris.setLinearVelocityX(-50);
		
		%this.explodeDebris = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = explodeDebris ;
			source = %this ;
		} ;
		%this.explodeDebris.create();
		%this.explodeDebris.setLinearVelocityX(-50);
		%this.explodeDebris.setLinearVelocityY(-50);
		
		%this.explodeDebris = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = explodeDebris ;
			source = %this ;
		} ;
		%this.explodeDebris.create();
		%this.explodeDebris.setLinearVelocityX(-50);
		%this.explodeDebris.setLinearVelocityY(50);
		
		%this.setLinearVelocityX(0);
		%this.setPosition(%this.startX,%this.startY);
		%this.spawntime = getRandom(2000,3000);
		%this.schedule(%this.spawntime,"respawn");
	}
	%this.setCurrentAnimation();
	
}
//Creating the animations when user does certain actions
function explodeShip::setCurrentAnimation(%this){
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

// When player will die
function explodeShip::respawn(%this){
	
	$explodeships += 1 ;
	if ($explodeships < %this.max){
		%this.speedX = getRandom(-80,-45);
		%this.setLinearVelocityX(%this.speedX);
		%this.startY = getRandom(-35,35);
			
		error("Explode ships created: " SPC $explodeships);
	}else
		error("Explode ships depleted" SPC $explodeships);
}

function explodeShip::onWorldLimit(%this, %mode, %limit){
    if (%limit $= "left" || %limit $= "right") {
       %this.setLinearVelocityX(0);
    }
	if (%limit $= "top" || %limit $= "bottom") {
       %this.setLinearVelocityY(0);
    }
}

// When playercollides with ghost
function explodeShip::onCollision( %this, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts ){
	%this.spawntime = getRandom(2000,3000);
	if (%dstObj.class $= "leftBounds"){
		%this.setLinearVelocityX(0);
		%this.setPosition(%this.startX,%this.startY);
		
		%this.schedule(%this.spawntime,"respawn");
			
		
	}else if (%dstObj.class $= "flyingClass"){
		%this.setLinearVelocityX(0);
		%this.setPosition(%this.startX,%this.startY);
		%this.schedule(%this.spawntime,"respawn");
		
	}
		
	
}
/////////////////////////////////////////////////////
function explodeDebris::create(%this){
	error("Debris");
	%this.playAnimation(bulletAnimation);
	%this.setPosition(%this.source.getPositionX(),%this.source.getPositionY());
	%this.setSize(5.1,5.1);
	%this.setLayer(6);
	%this.setCollisionLayers("0 1 2 3 4 5 7 8 9 11 13 14 15 17 19 20 21 22 23 24 25");
	%this.setCollisionPolyCustom(4,"-0.643 1.000","0.643 1.000","0.643 -1.000","-0.643 -1.000");
	%this.setCollisionActive(true,true);
	%this.setCollisionPhysics(true,true);
	%this.setCollisionCallback(true);
	%this.schedule(10000,"safeDelete");
	
}
function explodeDebris::onCollision( %this, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts ){
	if (%dstObj.class $= "flyingClass"){
		%this.safeDelete();
		
	}
	else if (%dstObj.class $= "leftBounds") {
		%this.safeDelete();
	}
	else {
		%this.safeDelete();
		
		%dstObj.spawntime = getRandom(2000,3000);
		%dstObj.setLinearVelocityX(0);
		error(%dstObj.startX SPC " " SPC %dstObj.startY);
		%dstObj.setPosition(%dstObj.startX,%dstObj.startY);
		%dstObj.schedule(%dstObj.spawntime,"respawn");
	}
}