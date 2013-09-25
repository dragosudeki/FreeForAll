////////////////////////////////////////////////////////////////
function plagueBallClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(BioPlagueBall) ;
	
	%this.num = 4;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.570 0.77","-0.57 -0.77","0.57 -0.77","0.57 0.77");
	%this.setPositionY(%this.source.getPositionY() - 3) ;
	%this.setSize(11.4,6.0);
	%this.setLinearVelocityX(0) ;	
	
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
	%this.schedule(500,"plague");
	%this.schedule(3000,"safeDelete");
}
function plagueBallClass::plague(%this){
	if (%this.num >0){
		%this.num += -1;
		
		%this.plagueCloudClass = new t2dAnimatedSprite()
		{
			scenegraph = %this.scenegraph ;
			class = plagueCloudClass ;
			source = %this ;
		} ;
		if (%this.source == $player){
			%this.plagueCloudClass.fire();
		}else{
			%this.plagueCloudClass.fire2();
		}
		
		%this.plagueCloudClass.setPositionX(%this.getPositionX()-3);
		%this.plagueCloudClass.setLinearVelocityX(0);	
		
		%this.schedule(500,"plague");	
	}
}

// Missile deleted
function plagueBallClass::delete(%this)
{
	%this.safeDelete();
}
////////////////////////////////////////////////////////////////
function plagueCloudClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(BioPlagueCloud) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.351 0.285","-0.351 -0.285","0.351 -0.285","0.351 0.285");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(5.400,11.500);
	%this.setLinearVelocity(0,0);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
	
	
	%this.schedule(5000,"delete");
}

function plagueCloudClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(BioPlagueCloud) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.351 0.285","-0.351 -0.285","0.351 -0.285","0.351 0.285");
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(5.400,11.500);
	%this.setLinearVelocity(0,0);	
	%this.setCollisionLayers(" 1 ");
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	
	
	%this.schedule(5000,"delete");
		
}
// Missile collision
function plagueCloudClass::onCollision(%srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts)
{
	if (%dstObj == $player)
	{
		%srcObj.safeDelete() ;
			
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
		
	}
	else if (%dstObj == $Bplayer)
	{
		%srcObj.safeDelete() ;
				
		$Bplayer.health = $Bplayer.health - 10;
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
function slimePillarClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(SlimePillar) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"1.000 1.000","-1.000 1.000","-1.000 -0.442","1.000 -0.442");
	%this.setPositionY(%this.source.getPositionY() - 6.6) ;
	%this.setSize(18.8,40);
	%this.setLinearVelocity(0,0);
	
	//Turn on collision but turn off physics
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 ");
	
	%this.enableUpdateCallback() ;
}

function slimePillarClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(SlimePillar) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"1.000 1.000","-1.000 1.000","-1.000 -0.442","1.000 -0.442");
	%this.setPositionY(%this.source.getPositionY() - 6.6) ;
	%this.setSize(18.8,40);
	%this.setLinearVelocity(0,0);	
	%this.setCollisionLayers(" 1 ");
	%this.damage = 8;
	//Turn on collision but turn off physics
	
	%this.setLayer(11);
	
	
	%this.enableUpdateCallback() ;
		
}
function slimePillarClass::onUpdate(%this){
	if (%this.getAnimationFrame() == 4 && !%this.on){
		%this.setCollisionActive (true, true);
		%this.setCollisionPhysics(false, false);
		%this.setCollisionCallback(true);
		%this.on = true ;	
	}
	else if (%this.getAnimationFrame() == 10){
		%this.setCollisionActive (false, false);
	}
	if (%this.getAnimationName() $= "SlimePillar"){
		if (%this.getIsAnimationFinished()){
			%this.safeDelete();
			%this.source.fire2 = false ;
			%this.source.onCool2 = true ;
			%this.source.schedule(850,"cooldown2");
			return;
		}
	}
}
// Missile collision
function slimePillarClass::onCollision(%srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts){
	if (%dstObj == $player)
	{
		%srcObj.setCollisionActive(false,false) ;
			
		$player.health = $player.health - 8;
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
		%srcObj.setCollisionActive(false,false) ;
				
		$Bplayer.health = $Bplayer.health - 8;
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
function svirSoulClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(soulShot) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.380 0.490","0.380 0.490","0.380 -0.470","-0.380 -0.470");
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setSize(11.0,12.0);
	%this.setLinearVelocityY(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
}

function svirSoulClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(soulShot) ;
	
	//Missile position
	%this.setCollisionPolyCustom(4,"-0.380 0.490","0.380 0.490","0.380 -0.470","-0.380 -0.470");
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setPositionX(%this.source.getPositionX()) ;
	%this.setSize(11.0,12.0);
	%this.setLinearVelocityY(0) ;	
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
		
}
// Missile collision
function svirSoulClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
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
function svirSoulClass::delete(%this)
{
	%this.safeDelete();
}
////////////////////////////////////////////////////////////////
function svirMeleeClass::fire(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(12.00,26.00);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	%this.setLayer(10);
	%this.setCollisionLayers(" 2 11");
	%this.enableUpdateCallback() ;
	%this.schedule(500,"collision");		
}
function svirMeleeClass::collision(%this){
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
}
function svirMeleeClass::fire2(%this)
{
	%this.setBlendAlpha(0);
	//Pick image and set size
	%this.playAnimation(blank) ;
	
	//Missile position
	%this.setPositionY(%this.source.getPositionY()) ;
	%this.setSize(12.00,26.00);
	%this.setLinearVelocityX(%this.source.moveX) ;	
	
	%this.setLayer(11);
	%this.setCollisionLayers(" 1 10");
	%this.enableUpdateCallback() ;
	%this.schedule(500,"collision");		
}
function svirMeleeClass::onUpdate(%this){
	if (%this.left)
		%this.setPositionY(%this.source.getPositionY());
	else
		%this.setPositionY(%this.source.getPositionY());
}
// Missile collision
function svirMeleeClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts ){
	if (%dstObj == $player ){	
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.delete();
			%dstObj.smashX += -36;	
		}else{
			%srcObj.delete();
			%dstObj.smashX += 36;
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
			%srcObj.delete();
			%dstObj.smashX += -36;	
		}else{
			%srcObj.delete();
			%dstObj.smashX += 36;
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
function svirMeleeClass::delete(%this)
{
	%this.setEnabled(false);
}
///////////////////////////////////////////////////
function svriBoltClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(soulBolt) ;
	%this.setSize(68.872,7.843);
	
	//Missile position
	%this.setLinearVelocity(0,0) ;	
	%this.setCollisionPolyCustom(4,"-1.000 1.000","-1.000 -1.000","0.825 -1.000","0.825 1.000");
	%this.setPositionY(%this.source.getPositionY() - 7.238);
	%this.setLinearVelocity(0,0);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(10);
	%this.setCollisionLayers(" 3 ");
	
	%this.schedule(1000,"delete2");
}

function svriBoltClass::fire2(%this)
{
	//Pick image and set size
	%this.playAnimation(soulBolt) ;
	%this.setSize(68.872,7.843);
	
	//Missile position
	%this.setLinearVelocity(0,0) ;	
	%this.setCollisionPolyCustom(4,"-1.000 1.000","-1.000 -1.000","0.825 -1.000","0.825 1.000");
	%this.setPositionY(%this.source.getPositionY() - 7.238);
	%this.setLinearVelocity(0,0);
	
	//Turn on collision but turn off physics
	%this.setCollisionActive (true, true) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(true) ;
	%this.setLayer(11);
	%this.setCollisionLayers(" 3 ");
	
	%this.schedule(1000,"delete2");
}
// Missile collision
function svriBoltClass::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "wall" )
	{
		%srcObj.delete() ;
	}	
	else if (%dstObj == $player ){	
		if (%srcObj.getPositionX() > %dstObj.getPositionX())
		{	
			%srcObj.delete();
			%dstObj.jumpL();	
		}else{
			%srcObj.delete();
			%dstObj.jumpR();
		}
		$player.health = $player.health - 15 ;
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
			%srcObj.delete();
			%dstObj.jumpL();	
		}else{
			%srcObj.delete();
			%dstObj.jumpR();
		}
		$Bplayer.health = $Bplayer.health - 15 ;
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
function svriBoltClass::delete(%this)
{
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(false) ;
	%this.schedule(100,"delete2");
}
function svriBoltClass::delete2(%this)
{
	%this.safeDelete();
	%this.source.inBolt = false ;
	%this.source.schedule(4000,"cooldown3");
}	
