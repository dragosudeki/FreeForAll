function platformMoveX::onLevelLoaded(%this, %scenegraph)
{
	%this.speed = getRandom(0,20) + 25;
	%this.setLinearVelocityX(%this.speed);
	%this.enableUpdateCallback() ;
	%this.setCollisionLayers("30");
}
function platformMoveX::onUpdate(%this)
{
	%height1 = $player.getHeight() / 2 * $player.collision;
	%py = $player.getPositionY() + %height1 ;
	
	if ($onSolo == 0){
		%height2 = $Bplayer.getHeight() / 2 * $Bplayer.collision;
		%py2 = $Bplayer.getPositionY() + %height2 ;
	}
	else{
		%py2 = 0 ;	
	}
	
	%y = %this.getPositionY() - (%this.getHeight() / 2);
	
	if (%py < %y && %py2 < %y){
		%this.setLayer(15);
	}
	else if (%py > %y && %py2 < %y){
		%this.setLayer(16);
	}
	else if (%py < %y && %py2 > %y){
		%this.setLayer(17);
	}
	else if (%py > %y && %py2 > %y ){
		%this.setLayer(18);
	}
}
function platformMoveX::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj.class $= "pit"){
		
		%srcObj.speed = getRandom(0,20) + 30;
		if (%srcObj.getLinearVelocityX() > 0){		
			%srcObj.setLinearVelocityX(-(%srcObj.speed));
		}else if (%srcObj.getLinearVelocityX() < 0){
			%srcObj.setLinearVelocityX((%srcObj.speed));
		}
		
	}
	
	else if (%dstObj == $player){
		
		$player.moveX = %srcObj.getLinearVelocityX();
		
	}	
	
	else if (%dstObj == $Bplayer){
		
		$Bplayer.moveX = %srcObj.getLinearVelocityX();
		
	}
	
	
	
}