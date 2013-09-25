function note1Class::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(musicNote1) ;
	
	//Missile position
	%this.setPositionX(%this.bomb.getPositionX() - 1) ;
	%this.setPositionY(%this.bomb.getPositionY()) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	%this.setConstantForceY(50);
	%this.setLinearVelocityY(-45);
	%this.setAutoRotation(100);
		
}

function note1Class::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(musicNote1) ;
	
	//Missile position
	%this.setPositionX(%this.bomb.getPositionX() - 1) ;
	%this.setPositionY(%this.bomb.getPositionY()) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
	%this.setConstantForceY(50);
	%this.setLinearVelocityY(-45);
	%this.setAutoRotation(100);
		
}
// Missile collision
function note1Class::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" )
	{
		%srcObj.delete() ;
	}	
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		error(%srcObj.deg);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = 45;
		
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}			
	}
	else if (%dstObj == $player ){	
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$player.health = $player.health - 2 ;
		if ($player.health <= 0 && $player.isDead == false){	
			%dstObj.die();	
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
	}
	else if (%dstObj == $Bplayer ){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$Bplayer.health = $Bplayer.health - 2 ;
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){	
			%dstObj.die();	
		}	
		Player2Health.text = $Bplayer.health ;
			
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
// Missile deleted
function note1Class::delete(%this)
{
	%this.safeDelete();
}
/////////////////////////////////////////
function note2Class::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(musicNote2) ;
	
	//Missile position
	%this.setPositionX(%this.bomb.getPositionX()) ;
	%this.setPositionY(%this.bomb.getPositionY() - 10) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	%this.setConstantForceY(50);
	%this.setLinearVelocityY(-55);
	//%this.setAutoRotation(100);
	
	%this.alpha = 0 ;
	%this.setBlendAlpha(0);
	%this.schedule(40,"fadeIn");
		
}

function note2Class::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(musicNote2) ;
	
	//Missile position
	%this.setPositionX(%this.bomb.getPositionX()) ;
	%this.setPositionY(%this.bomb.getPositionY() - 10) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
	%this.setConstantForceY(50);
	%this.setLinearVelocityY(-55);
	//%this.setAutoRotation(100);
	
	%this.alpha = 0 ;
	%this.setBlendAlpha(0);
	%this.schedule(40,"fadeIn");
		
}
function note2Class::fadeIn(%this){
	
	%this.alpha += 0.1 ;
	%this.setBlendAlpha(%this.alpha);
	
	if (%this.alpha < 1)
		%this.schedule(40,"fadeIn");
	else
		%this.setCollisionActive (true, true) ;
	
}
// Missile collision
function note2Class::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "wall" )
	{
		%srcObj.delete() ;
	}
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}	
	}	
	else if (%dstObj == $player ){	
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$player.health = $player.health - 4 ;
		if ($player.health <= 0 && $player.isDead == false){	
			%dstObj.die();	
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
	}
	else if (%dstObj == $Bplayer ){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$Bplayer.health = $Bplayer.health - 4 ;
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){	
			%dstObj.die();	
		}	
		Player2Health.text = $Bplayer.health ;
			
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
// Missile deleted
function note2Class::delete(%this)
{
	%this.safeDelete();
}
/////////////////////////////////////////
function note3Class::fire(%this)
{
	%this.player1 = true ;
	//Pick image and set size
	%this.playAnimation(note3) ;
	%this.setSize(89.001,10.667);
	
	//Missile position
	%this.setPositionX(%this.bomb.getPositionX() - 1) ;
	%this.setPositionY(%this.bomb.getPositionY()) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(12);
	%this.setCollisionLayers(" 0 ");
	//%this.setAutoRotation(100);
	
	%this.enableUpdateCallback() ;
		
}

function note3Class::fire2(%this)
{
	%this.player1 = false ;
	//Pick image and set size
	%this.playAnimation(note3) ;
	%this.setSize(89.001,10.667);
	
	//Missile position
	%this.setPositionX(%this.bomb.getPositionX() - 1) ;
	%this.setPositionY(%this.bomb.getPositionY()) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(12);
	%this.setCollisionLayers(" 0 ");
	//%this.setAutoRotation(100);
	
	%this.enableUpdateCallback() ;
		
}
function note3Class::onUpdate(%this){
	
	%x = %this.getPositionX();
	%width = %this.getWidth() / 2;
	%y = %this.getPositionY();
	%height = %this.getHeight() / 2;
	
	
	
	if (%this.player1 == false){
	
		%x1 = $player.getPositionX();
		%y1 = $player.getPositionY();
		%w1 = $player.getWidth() * $player.collision / 2;
		%h1 = $player.getHeight() * $player.collision / 2;	
		
		if (%x - %width <= %x1 + %w1 && %x + %width >= %x1 - %w1)
			if (%y - %height <= %y1 + %h1 && %y - %height >= %y1 - %h1)
				$player.note3Hit();
		
	}
	
	if (%this.player1 == true){
		
		%x2 = $Bplayer.getPositionX();
		%y2 = $Bplayer.getPositionY();
		%w2 = $Bplayer.getWidth() / 2;
		%h2 = $Bplayer.getHeight() / 2;		
		
		if (%x - %width <= %x2 + %w2 && %x + %width >= %x2 - %w2)
			if (%y - %height <= %y2 + %h2 && %y - %height >= %y2 - %h2)
				$Bplayer.note3Hit();
		
		
	}
	
}
// Missile collision
function note3Class::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" )
	{
		%srcObj.delete() ;
	}	
}
// Missile deleted
function note3Class::delete(%this)
{
	%this.safeDelete();
}
////////////////////////////////////////////////////////////////
function leafClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(leafAnimation) ;
	
	//Missile position
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY() - 5) ;
	%this.setSize(10.3,5.1);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
		
}

function leafClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(leafAnimation) ;
	
	//Missile position
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY() - 5) ;
	%this.setSize(10.3,5.1);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
		
}
// Missile collision
function leafClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" )
	{
		%srcObj.delete() ;
	}	
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%degrees = %srcObj.deg;
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}	
		if (%srcObj.getLinearVelocityX()<0)
			%srcObj.setFlip(true,false);
		else
			%srcObj.setFlip(true,true);
		%srcObj.setRotation(%degrees);	
		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		%degrees = %srcObj.deg;
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		if (%srcObj.getLinearVelocityX()<0)
			%srcObj.setFlip(true,false);
		else
			%srcObj.setFlip(true,true);
		%srcObj.setRotation(%degrees);	
	}
	else if (%dstObj == $player ){	
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$player.health = $player.health - 4 ;
		if ($player.health <= 0 && $player.isDead == false){	
			%dstObj.die();	
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
	}
	else if (%dstObj == $Bplayer ){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$Bplayer.health = $Bplayer.health - 4 ;
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){	
			%dstObj.die();	
		}	
		Player2Health.text = $Bplayer.health ;
			
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
// Missile deleted
function leafClass::delete(%this)
{
	%this.safeDelete();
}	
////////////////////////////////////////////////////////////////
function swordClass::fire(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(12.17,24.34);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 11");
	%this.enableUpdateCallback() ;		
}

function swordClass::fire2(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(12.17,24.34);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 10");
	%this.enableUpdateCallback() ;	
}
function swordClass::onUpdate(%this){
	if (%this.left)
		%this.setPositionY(%this.source.getPositionY());
	else
		%this.setPositionY(%this.source.getPositionY());
}
// Missile collision
function swordClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player ){	
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$player.health = $player.health - 8 ;
		if ($player.health <= 0 && $player.isDead == false){	
			%dstObj.die();	
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
	}
	else if (%dstObj == $Bplayer ){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$Bplayer.health = $Bplayer.health - 8 ;
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){	
			%dstObj.die();	
		}	
		Player2Health.text = $Bplayer.health ;
			
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
// Missile deleted
function swordClass::delete(%this)
{
	%this.safeDelete();
}								   						   							   						   
////////////////////////////////////////////////////////////////
function nukeClass::halfsecond(%this){
	%this.s += 0.5 ;
	%this.schedule(500,"halfsecond");
}
function nukeClass::fire(%this)
{
	%this.s = 0 ;
	%this.schedule(500,"halfsecond");
	%this.inc = 0;
	
	%this.setCollisionPolyCustom(4,"-0.516 -0.388","0.614 -0.388","0.614 0.280","-0.516 0.280");
	//Pick image and set size
	%this.playAnimation(nuke) ;
	
	//Missile position
	%this.setPositionY($maxy) ;
	%this.setSize(54.0,54.0);
	%this.setLinearVelocityY(50) ;
	%this.schedule(500,"increaseFall");
		
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
		
}

function nukeClass::fire2(%this)
{
	%this.inc = 0;
	
	%this.setCollisionPolyCustom(4,"-0.516 -0.388","0.614 -0.388","0.614 0.280","-0.516 0.280");
	//Pick image and set size
	%this.playAnimation(nuke2) ;
	
	//Missile position
	%this.setPositionY($maxy) ;
	%this.setSize(54.0,54.0);
	%this.setLinearVelocityY(50) ;
	%this.schedule(500,"increaseFall");		
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 ");
		
}
function nukeClass::increaseFall(%this){
	
	
	if (%this.inc <= 10){
		%this.setLinearVelocityY(%this.getLinearVelocityY() + 15);
		%this.schedule(500,"increaseFall");
		%this.inc += 1;
	}
	
}
// Missile collision
function nukeClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "pit" )
	{
		%srcObj.delete() ;
	}
}
// Missile deleted
function nukeClass::delete(%this)
{
	%this.safeDelete();
}	
////////////////////////////////////////////////////////////////
function casperDeathClass::fire(%this)
{
	%this.inc = 0;
	
	%this.setCollisionPolyCustom(4,"-0.516 -0.388","0.614 -0.388","0.614 0.280","-0.516 0.280");
	//Pick image and set size
	%this.playAnimation(nuke) ;
	
	//Missile position
	%this.setPositionY($maxy) ;
	%this.setSize(54.0,54.0);
	%this.setLinearVelocityY(50) ;
	%this.schedule(500,"increaseFall");
		
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.enableUpdateCallback();
	%this.setLayer(10);		
}

function casperDeathClass::fire2(%this)
{
	%this.inc = 0;
	
	%this.setCollisionPolyCustom(4,"-0.516 -0.388","0.614 -0.388","0.614 0.280","-0.516 0.280");
	//Pick image and set size
	%this.playAnimation(nuke2) ;
	
	//Missile position
	%this.setPositionY($maxy) ;
	%this.setSize(54.0,54.0);
	%this.setLinearVelocityY(50) ;
	%this.schedule(500,"increaseFall");		
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.enableUpdateCallback();
	%this.setLayer(11);
		
}
function casperDeathClass::increaseFall(%this){
	
	
	if (%this.inc <= 10){
		%this.setLinearVelocityY(%this.getLinearVelocityY() + 15);
		%this.schedule(500,"increaseFall");
		%this.inc += 1;
	}
	
}
// Missile collision
function casperDeathClass::onUpdate(%this)
{
	if (%this.getPositionY() >= %this.source.getPositionY()){
		
		%this.delete();
		%this.source.respawn();	
	}
}
// Missile deleted
function casperDeathClass::delete(%this)
{
	%this.safeDelete();
}	
////////////////////////////////////////////////////////////////
function cardClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(belshazzarCard) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.570 0.77","-0.57 -0.77","0.57 -0.77","0.57 0.77");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(10.3,5.1);
	%this.setLinearVelocityX(0) ;	
	%this.setAutoRotation(180);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
}

function cardClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(belshazzarCard) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.570 0.77","-0.57 -0.77","0.57 -0.77","0.57 0.77");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(10.3,5.1);
	%this.setLinearVelocityX(0) ;	
	%this.setAutoRotation(180);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
		
}
// Missile collision
function cardClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" || %dstObj.class $= "pit")
	{
		%srcObj.delete() ;
	}	
	if (%dstObj == $player)
	{
		if (%dstObj.getPositionX() > %srcObj.getPositionX())
		{
			
			%srcObj.safeDelete();
			%dstObj.jumpR();
			
		}
		else {
			
			%srcObj.safeDelete();
			%dstObj.jumpL();
			
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	if (%dstObj == $Bplayer)
	{
		if (%dstObj.getPositionX() > %srcObj.getPositionX())
		{
			
			%srcObj.safeDelete();
			%dstObj.jumpR();
			
		}
		else {
			
			%srcObj.safeDelete();
			%dstObj.jumpL();
			
		}
				
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
		
		if ($Bplayer.health <= 0 && !$Bplayer.isDead){
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
}
// Missile deleted
function cardClass::delete(%this)
{
	%this.safeDelete();
}	
////////////////////////////////////////////////////////////////
function waveClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(diamond) ;
	%this.setCollisionPolyCustom(4,"-1.000 0","0 -1.000","1.000 0","0 1.000");
	
	//Missile position
	%this.setPositionX(%this.source.getPositionX()) ;
	if (%this.number == 3){
		%this.startY = %this.source.getPositionY() + 31.670/2;
	}
	%this.setPositionY(%this.startY) ;
	%this.setSize(20.118,20.118);
	%this.setLinearVelocityX(0) ;	
	%this.setLinearVelocityY(-70) ;
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
	
	%this.schedule(250,"delete");		
}

function waveClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(diamond) ;
	%this.setCollisionPolyCustom(4,"-1.000 0","0 -1.000","1.000 0","0 1.000");
	//Missile position
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.startY) ;
	%this.setSize(20.118,20.118);
	%this.setLinearVelocityX(0) ;	
	%this.setLinearVelocityY(-50) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 ");
	%this.schedule(250,"delete");
		
}
// Missile collision
function waveClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" )
	{
		%srcObj.delete() ;
	}	
	if (%dstObj == $player)
	{
		if (%srcObj.goLeft)
		{
			
			%srcObj.setEnabled(false);
			if (%srcObj.number <= 0){
				%dstObj.setPositionY(%dstObj.getPositionY() + 10);	
			}else{
				%dstObj.setPositionX(%dstObj.getPositionX() - 15);
			}
			
		}
		else if (!%srcObj.goLeft){
			
			%srcObj.setEnabled(false);
			if (%srcObj.number <= 0){
				%dstObj.setPositionY(%dstObj.getPositionY() + 10);	
			}else{
				%dstObj.setPositionX(%dstObj.getPositionX() + 15);
			}
			
		}
		
				
		$player.health = $player.health - 5 ;
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	if (%dstObj == $Bplayer)
	{
		if (%srcObj.goLeft)
		{
			
			%srcObj.setEnabled(false);
			%dstObj.setPositionX(%dstObj.getPositionX() - 15);
			
		}
		else if (!%srcObj.goLeft){
			
			%srcObj.setEnabled(false);
			%dstObj.setPositionX(%dstObj.getPositionX() + 15);
			
		}
				
		$Bplayer.health = $Bplayer.health - 5 ;
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
			
			%dstObj.die() ;	
			
		}	
		
	}
}
// Missile deleted
function waveClass::delete(%this)
{
	if (%this.number > 0){
		
		%this.waveClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = waveClass ;
				source = %this.source ;
				startY = %this.startY;
				number = %this.number - 1;
				goLeft = %this.goLeft;
			} ;
		if (%this.source == $player){
			%this.waveClass.fire();
		}else{
			%this.waveClass.fire2();
		}
		if (%this.goLeft){
			%this.waveClass.setPositionX(%this.getPositionX() - 17.5);
		}else{
			%this.waveClass.setPositionX(%this.getPositionX() + 17.5);
		}
	}
		
	%this.safeDelete();
}
////////////////////////////////////////////////////////////////
function poopClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(poop) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.351 0.285","-0.351 -0.285","0.351 -0.285","0.351 0.285");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(18.779,19.739);
	%this.setLinearVelocityX(%this.speed);	
	%this.setLinearVelocityY(0);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
	
	
	%this.schedule(5000,"delete");
}

function poopClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(poop) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.351 0.285","-0.351 -0.285","0.351 -0.285","0.351 0.285");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(18.779,19.739);
	%this.setLinearVelocityX(%this.speed) ;
	%this.setLinearVelocityY(0);	
	%this.setCollisionLayers(" 1 ");
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	
	
	%this.schedule(5000,"delete");
		
}
// Missile collision
function poopClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "pit")
	{
		%srcObj.delete() ;
	}	
	else if (%dstObj == $player)
	{
		
		if ($player.smashx > 0){
			$player.smashx += 8 ;
		}else if ($player.smashx < 0){
			$player.smashx += -8 ;
		}
		
		%srcObj.delete() ;
			
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj == $Bplayer)
	{
		if ($Bplayer.smashx > 0){
			$Bplayer.smashx += 8 ;
		}else if ($Bplayer.smashx < 0){
			$Bplayer.smashx += -8 ;
		}
		
		%srcObj.delete() ;
				
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
		
		if ($Bplayer.health <= 0 && !$Bplayer.isDead){
			
			%dstObj.die() ;	
			
		}	
		
	}
}
// Missile deleted
function poopClass::delete(%this)
{
	%this.safeDelete();
	
	if (%this.source == $player){
		$player.pooNum += 1 ;
	}else{
		$Bplayer.pooNum += 1 ;
	}
}
////////////////////////////////////////////////////////////////
function dynamiteClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(dynamite) ;
	
	//Missile position
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(8.900,9.350);
	%this.setLinearVelocityX(%this.speed);	
	%this.setLinearVelocityY(0);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
	%this.schedule(5000,"delete");
}

function dynamiteClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(dynamite) ;
	
	//Missile position
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(8.900,9.350);
	%this.setLinearVelocityX(%this.speed);	
	%this.setLinearVelocityY(0);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
	
	%this.schedule(5000,"delete");
		
}
// Missile deleted
function dynamiteClass::delete(%this)
{
	%this.dynaSplosion = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph	;
		class = dynaSplosion ;
		source = %this ;
	} ;
	
	if (%this.source == $player){
		%this.dynaSplosion.fire();
		$player.liveBomb = false;
		$player.onCool3 = true ;
		$player.schedule(2000,"cooldown3");
	}else if (%this.source == $Bplayer){
		%this.dynaSplosion.fire2();
		$Bplayer.liveBomb = false;
		$Bplayer.onCool3 = true ;
		$Bplayer.schedule(2000,"cooldown3");
	}
	
	%this.safeDelete();
	
}	
////////////////////////////////////////////////////////////////
function dynaSplosion::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(SPLASH) ;
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.537 0.452","-0.537 -0.452","0.537 -0.452","0.537 0.452");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(49.510,52.420);
	%this.setLinearVelocityX(0);	
	%this.setLinearVelocityY(0);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
	
	%this.schedule(500,"delete");
	
	%this.sizex = 49.510 ;
	%this.sizey = 52.420 ;
	
	%this.schedule(50,"shrink");
	%this.setBlendColor(1,1,0,1);
}

function dynaSplosion::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(SPLASH) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.537 0.452","-0.537 -0.452","0.537 -0.452","0.537 0.452");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(49.510,52.420);
	%this.setLinearVelocityX(0) ;
	%this.setLinearVelocityY(0);	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 ");
	
	%this.schedule(500,"delete");
	
	%this.sizex = 49.510 ;
	%this.sizey = 52.420 ;
	
	%this.schedule(50,"shrink");
	%this.setBlendColor(1,1,0,1);
		
}
function dynaSplosion::shrink(%this){
		%this.sizex += -5 ;
		%this.sizey += -5 ;
		
		if (%this.sizex < 1){
			%this.sizex = 1 ;
		}
		if (%this.sizey < 1){
			%this.sizey = 1 ;
		}
		
		%this.setSize(%this.sizex,%this.sizey);
		
		
		
		%this.schedule(50,"shrink");
}
// Missile collision
function dynaSplosion::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player)
	{
				
		%srcObj.setCollisionActive (false, false) ;
			
		$player.health = $player.health - 20 ;
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj == $Bplayer)
	{
		
		%srcObj.setCollisionActive (false, false) ;
				
		$Bplayer.health = $Bplayer.health - 20 ;
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
			
			%dstObj.die() ;	
			
		}	
		
	}
}
// Missile deleted
function dynaSplosion::delete(%this)
{
	%this.safeDelete();
}	
/////////////////////////////////////////
function energyBoltClass::fire(%this){
	//Pick image and set size
	%this.playAnimation(energybolt) ;
	%this.setSize(10.3,10.3);
	%this.setCollisionPolyCustom(4,"-0.32 -0.50","-0.32 0.28","0.382 0.280","0.382 -0.50");
	%this.setAutoRotation(getRandom(100,200));
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY() - 0.2) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
	%this.alpha = 0 ;
	%this.setBlendAlpha(0);
	%this.schedule(10,"fadeIn");
	%this.damage = 2;	
	
	%this.schedule(1500,"fadeOut");
}

function energyBoltClass::fire2(%this){
	//Pick image and set size
	%this.playAnimation(energybolt) ;
	%this.setSize(10.3,10.3);
	%this.setCollisionPolyCustom(4,"-0.32 -0.50","-0.32 0.28","0.382 0.280","0.382 -0.50");
	%this.setAutoRotation(getRandom(100,200));
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY() - 0.2) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
	
	%this.alpha = 0 ;
	%this.setBlendAlpha(0);
	%this.schedule(10,"fadeIn");
	%this.damage = 2;		
	
	%this.schedule(1500,"fadeOut");
}
function energyBoltClass::fadeIn(%this){
	
	%this.alpha += 0.1 ;
	%this.setBlendAlpha(%this.alpha);
	
	if (%this.alpha < 1)
		%this.schedule(20,"fadeIn");
	else{
		%this.setCollisionActive (true, true) ;
		%this.damage = 4;	
	}
}
function energyBoltClass::fadeOut(%this){
	
	%this.alpha -= 0.1 ;
	%this.setBlendAlpha(%this.alpha);
	
	if (%this.alpha > 0)
		%this.schedule(40,"fadeOut");
	else
		%this.safeDelete();
	
}
// Missile collision
function energyBoltClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "wall" || %dstObj.class $= "pit")
	{
		%srcObj.delete() ;
	}	
	else if (%dstObj == $Bplayer)
	{
		%srcObj.delete() ;
		
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){	
			%srcObj.safeDelete();
			%dstObj.jumpL();
		}else {
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
				
		$Bplayer.health = $Bplayer.health - %srcObj.damage ;
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj == $player)
	{
		%srcObj.delete() ;
		
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){	
			%srcObj.safeDelete();
			%dstObj.jumpL();
		}else {
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		
		$player.health = $player.health - %srcObj.damage ;
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
	
}
// Missile deleted
function energyBoltClass::delete(%this)
{
	%this.safeDelete();
}	
/////////////////////////////////////////
function starClass::fire(%this){
	//Pick image and set size
	%this.playAnimation(starEffect) ;
	%this.setSize(11.0,11.6);
	%this.setCollisionPolyCustom(4,"-0.32 -0.50","-0.32 0.28","0.382 0.280","0.382 -0.50");
	%this.setAutoRotation(getRandom(100,200));
	
	//Missile position
	if (%this.source.isLeft){
		%this.setPositionX(%this.source.getPositionX() - 8.3);
		%this.deg = getRandom(200,340);
	}else{
		%this.setPositionX(%this.source.getPositionX() + 8.3);
		%this.deg = getRandom(20,160);
	}
	%this.setPositionY(%this.source.getPositionY() - 0.2) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
	%this.alpha = 0 ;
	%this.setBlendAlpha(0);
	%this.schedule(10,"fadeIn");
	
	%this.deg+= -90 ;
	%this.deg = mDegToRad(%this.deg);
	
	%this.setLinearVelocityX(80 * mCos(%this.deg));
	%this.setLinearVelocityY(80 * mSin(%this.deg));
	
	%this.schedule(100,"shoot");
	%this.hasShot = false ;
}
function starClass::fire2(%this){
	//Pick image and set size
	%this.playAnimation(starEffect) ;
	%this.setSize(11.0,11.6);
	%this.setCollisionPolyCustom(4,"-0.32 -0.50","-0.32 0.28","0.382 0.280","0.382 -0.50");
	%this.setAutoRotation(getRandom(100,200));
	
	//Missile position
	if (%this.source.isLeft){
		%this.setPositionX(%this.source.getPositionX() - 8.3);
		%this.deg = getRandom(200,340);
	}else{
		%this.setPositionX(%this.source.getPositionX() + 8.3);
		%this.deg = getRandom(20,160);
	}
	%this.setPositionY(%this.source.getPositionY() - 0.2) ;
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
	
	%this.alpha = 0 ;
	%this.setBlendAlpha(0);
	%this.schedule(10,"fadeIn");	
	
	%this.deg += -90 ;
	%this.deg = mDegToRad(%this.deg);
	
	%this.setLinearVelocityX(80 * mCos(%this.deg));
	%this.setLinearVelocityY(80 * mSin(%this.deg));
	
	%this.schedule(100,"shoot");
	%this.hasShot = false ;
}
function starClass::fadeIn(%this){
	%this.alpha += 0.1 ;
	%this.setBlendAlpha(%this.alpha);
	
	if (%this.alpha < 1)
		%this.schedule(20,"fadeIn");
	else {
		%this.setCollisionActive (true, true) ;
		%this.schedule(750,"fadeOut");	
	}
}
function starClass::fadeOut(%this){
	%this.alpha += -0.1 ;
	%this.setBlendAlpha(%this.alpha);
	
	if (%this.alpha > 0)
		%this.schedule(20,"fadeOut");
	else {
		%this.setCollisionActive(false,false);
		%this.delete();
	}
}

// Missile collision
function starClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "wall" || %dstObj.class $= "pit"){
		%srcObj.delete() ;
	}	
	else if (%dstObj == $Bplayer){
		%srcObj.delete() ;
		
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){	
			%srcObj.safeDelete();
			%dstObj.jumpL();
		}else {
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
				
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
		
		if ($Bplayer.health <= 0 && !$Bplayer.isDead){
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj == $player){
		%srcObj.delete() ;
		
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){	
			%srcObj.safeDelete();
			%dstObj.jumpL();
		}else {
			%srcObj.safeDelete();
			%dstObj.jumpR();
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
}
function starClass::shoot(%this){
	
	if (!%this.hasShot){
	%this.hasShot = true ;
	if (%this.number > 0){
			%this.starClass = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
				class = starClass ;
				source = %this.source ;
				number = %this.number - 1 ;
				start = %this.start;
			} ;
			if (%this.source == $player)
				%this.starClass.fire();
			else
				%this.starClass.fire2();
			if (%this.getSizeX() == 4.632){
				%this.starClass.setSize(4.632,4.632);
				%this.starClass.setPositionX(%this.source.getPositionX() - 3.7);
			}
		}else{
			%this.source.starFire = false ;
			%this.source.movementSpeed = 80 ;
			%this.source.schedule(3000,"cooldown3");
		}
	}	
}
// Missile deleted
function starClass::delete(%this)
{
	if (!%this.hasShot){
		%this.shoot();	
	}
	%this.safeDelete();
}		
////////////////////////////////////////////////////////////////
function broomClass::fire(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionX(%this.source.getPositionX());
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(23.56,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
			
}

function broomClass::fire2(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionX(%this.source.getPositionX());
	%this.setPositionY(%this.source.getPositionY());
	%this.setSize(23.56,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 ");	
}
// Missile collision
function broomClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts ){
	if (%dstObj == $player){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{
			
			%srcObj.delete();
			%dstObj.jumpL();
			
		}
		else {
			
			%srcObj.delete();
			%dstObj.jumpR();
			
		}
				
		$player.health = $player.health - 5 ;
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
			
			%dstObj.die() ;	
			
		}	
	}else if (%dstObj == $Bplayer){
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){
			%srcObj.delete();
			%dstObj.jumpL();
		}else{
			%srcObj.delete();
			%dstObj.jumpR();
		}
				
		$Bplayer.health = $Bplayer.health - 5 ;
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
			
			%dstObj.die() ;	
			
		}
	}
}
// Missile deleted
function broomClass::delete(%this)
{
	%this.setCollisionActive(false,false);
}	
////////////////////////////////////////////////////////////////
function fearStaffClass::fire(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(11.78,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
	%this.enableUpdateCallback() ;		
}

function fearStaffClass::fire2(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(11.78,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 ");
	%this.enableUpdateCallback() ;	
}
function fearStaffClass::onUpdate(%this){
	if (%this.left)
		%this.setPositionY(%this.source.getPositionY());
	else
		%this.setPositionY(%this.source.getPositionY());
}
// Missile collision
function fearStaffClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{
			
			%srcObj.delete();
			%dstObj.jumpL();
			
		}
		else {
			
			%srcObj.delete();
			%dstObj.jumpR();
			
		}
				
		$player.health = $player.health - 5 ;
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
			
			%dstObj.die() ;	
			
		}	
	}else if (%dstObj == $Bplayer){
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){
			%srcObj.delete();
			%dstObj.jumpL();
		}else{
			%srcObj.delete();
			%dstObj.jumpR();
		}
				
		$Bplayer.health = $Bplayer.health - 5 ;
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
			
			%dstObj.die() ;	
			
		}
	}
}
// Missile deleted
function fearStaffClass::delete(%this)
{
	%this.setCollisionActive(false,false);
}
////////////////////////////////////////////////////////////////
function fearCircleClass::fire(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(11.78,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 11");
	%this.enableUpdateCallback() ;		
}

function fearCircleClass::fire2(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(11.78,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 10");
	%this.enableUpdateCallback() ;	
}
function fearCircleClass::onUpdate(%this){
	if (%this.left)
		%this.setPositionY(%this.source.getPositionY());
	else
		%this.setPositionY(%this.source.getPositionY());
}
// Missile collision
function fearCircleClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{
			
			%dstObj.setPositionX(%dstObj.getPositionX() - 18);
			
		}
		else {
			
			%dstObj.setPositionX(%dstObj.getPositionX() + 18);
			
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
			
			%dstObj.die() ;	
			
		}	
	}else if (%dstObj == $Bplayer){
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){
			%dstObj.setPositionX(%dstObj.getPositionX() - 15);
		}else{
			%dstObj.setPositionX(%dstObj.getPositionX() + 15);
		}
				
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
		
		if ($Bplayer.health <= 0 && !$Bplayer.isDead){
			
			%dstObj.die() ;	
			
		}
	}
}	
////////////////////////////////////////////////////////////////
function fearFireStaffClass::fire(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(11.78,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
	%this.enableUpdateCallback() ;		
}

function fearFireStaffClass::fire2(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(11.78,24.76);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 ");
	%this.enableUpdateCallback() ;	
}
function fearFireStaffClass::onUpdate(%this){
	
	if (%this.left)
		%this.setPositionY(%this.source.getPositionY());
	else
		%this.setPositionY(%this.source.getPositionY());
}
// Missile collision
function fearFireStaffClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player){
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{
			
			%srcObj.delete();
			%dstObj.smashx += -20;
			
		}
		else {
			
			%srcObj.delete();
			%dstObj.smashx += 20;
			
		}
				
		$player.health = $player.health - 10 ;
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
			
			%dstObj.die() ;	
			
		}	
	}else if (%dstObj == $Bplayer){
		if (%srcObj.getPositionX() > %dstObj.getPositionX()){
			%srcObj.delete();
			%dstObj.smashx += -20;
		}else{
			%srcObj.delete();
			%dstObj.smashx += 20;
		}
				
		$Bplayer.health = $Bplayer.health - 10 ;
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
			
			%dstObj.die() ;	
			
		}
	}
}
// Missile deleted
function fearFireStaffClass::delete(%this)
{
	%this.setCollisionActive(false,false);
}	
////////////////////////////////////////////////////////////////
function bulletClass::fire1(%this)
{
	//Pick image and set size
	%this.playAnimation(BOMBAnimation) ;
	
	//Missile position
	%this.setLinearVelocityX(0) ;	
	%this.setLayer(10);
	%this.setCollisionLayers("3");
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	//%this.setLayer(18);
	//%this.setCollisionLayers(" 20");
	%this.setConstantForceY(50);
	%this.setLinearVelocityY(-50);
	%this.setAutoRotation(100);
		
}
function bulletClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(BOMBAnimation) ;
	
	//Missile position
	%this.setLinearVelocityX(0) ;	
	
	%this.setLayer(11);
	%this.setCollisionLayers("3");
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	
	%this.setConstantForceY(50);
	%this.setLinearVelocityY(-50);
	%this.setAutoRotation(100);
		
}
// Missile collision
function bulletClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" )
	{
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
		
		%srcObj.delete() ;
	}	
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}	
		if (%srcObj.getLinearVelocityX()>0)
			%srcObj.setFlip(true,false);
		else
			%srcObj.setFlip(false,false);
		%srcObj.setRotation(%srcObj.deg);	
		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		if (%srcObj.getLinearVelocityX()>0)
			%srcObj.setFlip(true,false);
		else
			%srcObj.setFlip(false,false);
		%srcObj.setRotation(%srcObj.deg);
		
	}
}
// Missile deleted
function bulletClass::delete(%this)
{
	
	%this.safeDelete();
}
////////////////////////////////////////////////////////////////
function soulPunchClass::fire(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(17.77,23.1);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 11");
	%this.schedule(200,"allowCollision");
	if (%this.damage == 10)
		%this.fireShoot();
	
}
function soulPunchClass::fireShoot(%this){
	%this.soulThrowClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = soulThrowClass ;
		source = %this.source ;
	} ;
	%this.soulThrowClass.fire();
	if (%this.left)
		%this.soulThrowClass.setLinearVelocityX(-65);	
	else if (!%this.left)
		%this.soulThrowClass.setLinearVelocityX(65);
	%this.soulThrowClass.setPosition(%this.getPositionX(),%this.getPositionY());
	echo(%this.getPositionX() SPC %this.soulThrowClass.getPositionX());	
	
	%this.soulThrowClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = soulThrowClass ;
		source = %this.source ;
	} ;
	%this.soulThrowClass.fire();
	if (%this.left)
		%this.soulThrowClass.setLinearVelocityX(-65);	
	else if (!%this.left)
		%this.soulThrowClass.setLinearVelocityX(65);
	%this.soulThrowClass.setLinearVelocityY(40);
	%this.soulThrowClass.setPosition(%this.getPositionX(),%this.getPositionY());
	echo(%this.getPositionX() SPC %this.soulThrowClass.getPositionX());	
	
	%this.soulThrowClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = soulThrowClass ;
		source = %this.source ;
	} ;
	%this.soulThrowClass.fire();
	if (%this.left)
		%this.soulThrowClass.setLinearVelocityX(-65);	
	else if (!%this.left)
		%this.soulThrowClass.setLinearVelocityX(65);
	%this.soulThrowClass.setLinearVelocityY(-40);
	%this.soulThrowClass.setPosition(%this.getPositionX(),%this.getPositionY());
	echo(%this.getPositionX() SPC %this.soulThrowClass.getPositionX());	
}
function soulPunchClass::fireShoot2(%this){
	%this.soulThrowClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = soulThrowClass ;
		source = %this.source ;
	} ;
	%this.soulThrowClass.fire2();
	if (%this.left)
		%this.soulThrowClass.setLinearVelocityX(-65);	
	else if (!%this.left)
		%this.soulThrowClass.setLinearVelocityX(65);
	%this.soulThrowClass.setPosition(%this.getPositionX(),%this.getPositionY());
	
	%this.soulThrowClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = soulThrowClass ;
		source = %this.source ;
	} ;
	%this.soulThrowClass.fire2();
	if (%this.left)
		%this.soulThrowClass.setLinearVelocityX(-65);	
	else if (!%this.left)
		%this.soulThrowClass.setLinearVelocityX(65);
	%this.soulThrowClass.setLinearVelocityY(-40);
	%this.soulThrowClass.setPosition(%this.getPositionX(),%this.getPositionY());
	
	%this.soulThrowClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = soulThrowClass ;
		source = %this.source ;
	} ;
	%this.soulThrowClass.fire2();
	if (%this.left)
		%this.soulThrowClass.setLinearVelocityX(-65);	
	else if (!%this.left)
		%this.soulThrowClass.setLinearVelocityX(65);
	%this.soulThrowClass.setLinearVelocityY(40);
	%this.soulThrowClass.setPosition(%this.getPositionX(),%this.getPositionY());
}
function soulPunchClass::allowCollision(%this){
	if (%this.damage != 10){
		%this.setCollisionActive (true, true) ;
		%this.setCollisionPhysics(false, false) ;
		%this.setCollisionCallback(true) ;
		%this.enableUpdateCallback() ;
	}
}
function soulPunchClass::onUpdate(%this){
	if (%this.left)
		%this.setPositionY(%this.source.getPositionY());
	else
		%this.setPositionY(%this.source.getPositionY());
}
function soulPunchClass::fire2(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(17.77,23.1);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 10");
	%this.schedule(200,"allowCollision");
	if (%this.damage == 10)
		%this.fireShoot2();
}
// Missile collision
function soulPunchClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj == $player ){
		if (%srcObj.left)
		{	
			%srcObj.delete();
			%dstObj.jumpL();	
		}else{
			%srcObj.delete();
			%dstObj.jumpR();
		}
		$player.health = $player.health - %srcObj.damage ;
		if ($player.health <= 0 && $player.isDead == false){	
			%dstObj.die();	
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
	}
	else if (%dstObj == $Bplayer ){
		if (%srcObj.left)
		{	
			%srcObj.delete();
			%dstObj.jumpL();	
		}else{
			%srcObj.delete();
			%dstObj.jumpR();
		}
		$Bplayer.health = $Bplayer.health - %srcObj.damage ;
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){	
			%dstObj.die();	
		}	
		Player2Health.text = $Bplayer.health ;
			
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
// Missile deleted
function soulPunchClass::delete(%this)
{
	%this.setEnabled(false);
}	
////////////////////////////////////////////////////////////////
function soulThrowClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(soulShot) ;
	
	//Missile position
	%this.setSize(8.25,8.25);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");	
	%this.setCollisionPolyCustom(4,"-0.380 0.490","0.380 0.490","0.380 -0.470","-0.380 -0.470");
}
function soulThrowClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(soulShot) ;
	
	//Missile position
	%this.setSize(8.25,8.25);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
		
}
// Missile collision
function soulThrowClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" )
	{
		%srcObj.delete() ;
	}	
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		error(%srcObj.deg);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = 45;
		
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}			
	}
	else if (%dstObj == $player ){	
		if (%srcObj.getLinearVelocityX() < 0)
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$player.health = $player.health - 10 ;
		if ($player.health <= 0 && $player.isDead == false){	
			%dstObj.die();	
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
	}
	else if (%dstObj == $Bplayer ){
		if (%srcObj.getLinearVelocityX() < 0)
		{	
			%srcObj.safeDelete();
			%dstObj.jumpL();	
		}else{
			%srcObj.safeDelete();
			%dstObj.jumpR();
		}
		$Bplayer.health = $Bplayer.health - 10 ;
		if ($Bplayer.health <= 0 && $Bplayer.isDead == false){	
			%dstObj.die();	
		}	
		Player2Health.text = $Bplayer.health ;
			
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
// Missile deleted
function soulThrowClass::delete(%this)
{
	%this.safeDelete();
}
////////////////////////////////////////////////////////////////
function smokeSourceClass::start(%this){
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	%this.num = 6;
	%this.setPosition(%this.source.getPositionX(),%this.source.getPositionY());
	//Missile position
	%this.setLinearVelocity(0,0) ;
	
	%this.createSmoke();
}
function smokeSourceClass::createSmoke(%this){
	if (%this.num > 0){
		%this.num += -1 ;
		
		
		%this.smokeClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = smokeClass ;
			source = %this ;
		} ;
		%this.smokeClass.fire();
		
		%x = getRandom(-10,10);
		%y = getRandom(-10,10);
		%this.smokeClass.setPosition(%this.getPositionX() + %x, %this.getPositionY() + %y);
		if (%this.soul)
			%this.smokeClass.playAnimation(soulShot);
		
		%this.schedule(100,"createSmoke");
	}else
		%this.safeDelete();
}
////////////////////////////////////////////////////////////////
function smokeClass::fire(%this){
	%this.playAnimation(pinkSmoke);
	%this.alpha = 1;
	%this.schedule(500,"fadeOut");
	%f = getRandom(0,100);
	
	%this.setSize(25,25);
	
	if (%f <= 50)
		%this.setFlip(false,false);
	else
		%this.setFlip(true,false);
		
	%this.deg = getRandom(0,360);
	%this.deg += -90 ;
	%this.deg = mDegToRad(%this.deg);
	
	%this.setLinearVelocityX(60 * mCos(%this.deg));
	%this.setLinearVelocityY(60 * mSin(%this.deg));
}

function smokeClass::fadeOut(%this){
	
	%this.alpha -= 0.1 ;
	%this.setBlendAlpha(%this.alpha);
	
	if (%this.alpha > 0)
		%this.schedule(40,"fadeOut");
	else
		%this.safeDelete();
}	
////////////////////////////////////////////////////////////////
function slimeBallClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(BioSlimeBall) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.570 0.77","-0.57 -0.77","0.57 -0.77","0.57 0.77");
	%this.setPositionY(%this.source.getPositionY() - 3) ;
	%this.setSize(8.0,4.0);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
}

function slimeBallClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(BioSlimeBall) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.570 0.77","-0.57 -0.77","0.57 -0.77","0.57 0.77");
	%this.setPositionY(%this.source.getPositionY() - 3) ;
	%this.setSize(8.0,4.0);
	%this.setLinearVelocityX(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
		
}
// Missile collision
function slimeBallClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	if (%dstObj.class $= "wall" || %dstObj.class $= "pit")
	{
		%srcObj.delete() ;
	}	
	if (%dstObj == $player)
	{
		if (%dstObj.getPositionX() > %srcObj.getPositionX())
		{
			
			%srcObj.safeDelete();
			%dstObj.jumpR();
			
		}
		else {
			
			%srcObj.safeDelete();
			%dstObj.jumpL();
			
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
			
			%dstObj.die() ;	
			
		}	
		
	}
	if (%dstObj == $Bplayer)
	{
		if (%dstObj.getPositionX() > %srcObj.getPositionX())
		{
			
			%srcObj.safeDelete();
			%dstObj.jumpR();
			
		}
		else {
			
			%srcObj.safeDelete();
			%dstObj.jumpL();
			
		}
				
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
		
		if ($Bplayer.health <= 0 && !$Bplayer.isDead){
			
			%dstObj.die() ;	
			
		}	
		
	}
	else if (%dstObj.class $= "fearCircleClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-40,40);
		else 
		%srcObj.deg = getRandom(140,220);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
	else if (%dstObj.class $= "swordClass"){
		%xs = %srcObj.getLinearVelocityX();
		%ys = %srcObj.getLinearVelocityY();
		if (%srcObj.getPositionX() < %dstObj.getPositionX())
		%srcObj.deg = getRandom(-10,70);
		else 
		%srcObj.deg = getRandom(110,190);
		%srcObj.deg = mDegToRad(%srcObj.deg);
		
		%srcObj.speed = mPow((%xs*%xs)+(%ys*%ys),0.5);
		%srcObj.speed = -%srcObj.speed; 
		%srcObj.setLinearVelocityX(%srcObj.speed * mCos(%srcObj.deg));
		%srcObj.setLinearVelocityY(%srcObj.speed * mSin(%srcObj.deg));
		if (%srcObj.getLayer()==10){
			%srcObj.setLayer(11);
		}else{
			%srcObj.setLayer(10);
		}		
		
	}
}
// Missile deleted
function slimeBallClass::delete(%this)
{
	%this.safeDelete();
}			