function collisionSplash::appear(%this)
{
	//Pick image and set size
	%this.playAnimation(SPLASH) ;
	
	//Missile position
	//%this.setPositionX(%this.source.getPositionX() - 1) ;
	//%this.setPositionY(%this.source.getPositionY()) ;	
	
	//Turn on collisionSplash but turn off physics
	%this.setCollisionActive (false, false) ;
	%this.setCollisionPhysics(false, false) ;
	%this.setCollisionCallback(false) ;
	%this.schedule(100,"delete");
	%this.setLayer(0);
	
	%randNum = getRandom(0,360);
	%this.setRotation(%randNum);
	
	%this.setBlendColour(255,172,0,255);
		
}

// Missile deleted
function collisionSplash::delete(%this)
{
	%this.safeDelete();
}
								   