function FollowCamera::onLevelLoaded(%this, %scenegraph)
{
	$camera = %this ;
	$spawn.spawn();
	//%this.enableUpdateCallback() ;
	//%this.mount(MainCamera, "0 0", %this.trackForce, false);
	
}
function FollowCamera::reset(%this)
{
	%y = $player.getPositionY() - 25;
	%x = $player.getPositionX();
	
	%this.setPosition(%x,%y);
	
	%this.enableUpdateCallback() ;
}

function FollowCamera::onUpdate(%this)
{	
	%y = $player.getLinearVelocityY();
	%x = $player.getLinearVelocityX();
	
	if (%x == 0 && %y == 0){
		%this.reset();
		
	}
	
	%this.setLinearVelocity(%x,%y);
	
}