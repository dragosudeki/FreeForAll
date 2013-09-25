///////////////////////////////////////////////////
function svriBoltClass::fire(%this)
{
	//Pick image and set size
	%this.playAnimation(svriBolt) ;
	
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
	%this.playAnimation(svriBolt) ;
	
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
	%this.setCollisionActive(false,false);
}
function svriBoltClass::delete2(%this)
{
	%this.safeDelete();
	%this.source.inBolt = false ;
	%this.source.schedule(5000,"cooldown3");
}