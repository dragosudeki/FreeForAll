function platform::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
	%this.setCollisionLayers("30");
}
function platform::onUpdate(%this)
{	
	%height1 = $player.getHeight() / 2 * $player.collision;
	%py = $player.getPositionY() + %height1 ;
	
	
	if ($onSolo == 0){
		%height2 = $Bplayer.getHeight() / 2 * $Bplayer.collision;
		%py2 = $Bplayer.getPositionY() + %height2 ;
	}
	else {
		
		%py2 = 0 ;	
		
	}
	
	
	%y = %this.getPositionY() - (%this.getHeight() / 2);
	
	if (%py < %y && %py2 < %y){
		%this.setLayer(15);
	
	}
	if (%py > %y && %py2 < %y){
	
		%this.setLayer(16);
	}
	if (%py < %y && %py2 > %y){
		%this.setLayer(17);
	
	}
	if (%py > %y && %py2 > %y ){
		%this.setLayer(18);
	}	
}
//////////////////////////////////////////////////////
function platformTemp::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
	%this.setCollisionLayers("30");
	%this.y = %this.getPositionY();
	%this.setBlendAlpha(1);
	%this.setBlendingStatus(true);
	
}
function platformTemp::onUpdate(%this)
{	
	%height1 = $player.getHeight() / 2 * $player.collision;
	%py = $player.getPositionY() + %height1 ;
	
	if ($onSolo == 0){
		%height2 = $Bplayer.getHeight() / 2 * $Bplayer.collision;
		%py2 = $Bplayer.getPositionY() + %height2 ;
	}
	else {
		
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
	
	if (%this.isGoing){
		
		%alpha = %this.getBlendAlpha();
		%this.setBlendAlpha(%alpha - 0.01);
		
		if (%alpha == 0){
			%this.schedule(2000,"reset");	
		}
		if (%alpha <= 0){
			%this.setLayer(18);	
		}
		
	}
}

function platformTemp::reset(%this){
	
	%this.isGoing = false ;
	%this.setBlendAlpha(1);
	
}
function platformTemp::fall(%this){
	
	%this.isGoing = true;
	
}
function platformTemp::onCollision( %srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts )
{
	
	if (%dstObj == $Bplayer || %dstObj == $player){
		
		%srcObj.schedule(1,"fall");
	
	}
}
///////////////////////////////////
function seeThru::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
	%this.setCollisionLayers("30");
}
function seeThru::onUpdate(%this)
{	
	%height1 = $player.getHeight() / 2 * $player.collision;
	%py = $player.getPositionY();
	%width1 = $player.getWidth() / 2;
	%px = $player.getPositionX();
	
	if ($onSolo == 0){
		%height2 = $Bplayer.getHeight() / 2 * $Bplayer.collision;
		%py2 = $Bplayer.getPositionY();
		%width2 = $Bplayer.getWidth() / 2;
		%px2 = $Bplayer.getPositionX();
	}
	else {
		%py2 = 0 ;
		%px2 = 0 ;	
	}
	
	%h = (%this.getHeight() / 2);
	%y = %this.getPositionY();
	
	%w = (%this.getWidth() / 2);
	%x = %this.getPositionX();
	
	if (%py + %height1 > %y - %h && %py - %height < %y + %h && %px> %x - %w && %px < %x + %w){
		
		%this.setBlendAlpha(0);
	
	}
	else if (%py2 + %height2 > %y - %h && %py2 - %height < %y + %h && %px2 > %x - %w && %px2 < %x + %w){
			
		%this.setBlendAlpha(0);
			
	}
	else {
		
		%this.setBlendAlpha(1);
		
	}
}
/////////////////////////////////////////
function platformBot::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
	%this.setCollisionLayers("30");
}
function platformBot::onUpdate(%this)
{	
	%height1 = $player.getHeight() / 2 * $player.collision2;
	%height2 = $Bplayer.getHeight() / 2 * $Bplayer.collision2;
	%py = $player.getPositionY() - %height1 ;
	%py2 = $Bplayer.getPositionY() - %height2 ;
	%y = %this.getPositionY() + (%this.getHeight() / 2);
	
	if (%py < %y && %py2 < %y){
		%this.setLayer(18);
	
	}
	else if (%py > %y && %py2 < %y){
	
		%this.setLayer(17);
	}
	else if (%py < %y && %py2 > %y){
		%this.setLayer(16);
	}
	else if (%py > %y && %py2 > %y ){
		%this.setLayer(15);
	}	
}
/////////////////////////////////////////
function platformDamage::onLevelLoaded(%this,%scenegraph){
	%this.sizex = (%this.getSizeX()+1)/2;
	%this.sizey = (%this.getSizeY()+1)/2;
	%this.x = %this.getPositionX();
	%this.y = %this.getPositionY();
	%this.damage1 = true ;
	%this.damage2 = true ;
	%this.enableUpdateCallback() ;
}
function platformDamage::onUpdate(%this){
	%height1 = $player.getHeight() / 2;
	%py = $player.getPositionY();
	%width1 = $player.getWidth() * $player.sideCollision / 2;
	%px = $player.getPositionX();
	
	if (%px-%width1<=%this.x+%this.sizex && %px+%width1>=%this.x-%this.sizex){
		if (%py-%height1<=%this.y+%this.sizey && %py+%height1>=%this.y-%this.sizey){
			if (%this.damage1){
				
				$player.health = $player.health - 1 ;
				playerHealth.text = $player.health ;
				if ($player.health <= 0 && !$player.isDead){
					$player.die() ;	
				}
				
				%this.damage1 = false ;
				%this.schedule(500,"resetDamage1");
			}
		}
	}
	
	if ($onSolo == 0){
		%height1 = $Bplayer.getHeight() / 2;
		%py = $Bplayer.getPositionY();
		%width1 = $Bplayer.getWidth() * $player.sideCollision / 2;
		%px = $Bplayer.getPositionX();
		
		if (%px-%width1<=%this.x+%this.sizex && %px+%width1>=%this.x-%this.sizex){
			if (%py-%height1<=%this.y+%this.sizey && %py+%height1>=%this.y-%this.sizey){
				if (%this.damage2){
					
					if ($Bplayer.health > 0)
					$Bplayer.health = $Bplayer.health - 1 ;
					player2Health.text = $Bplayer.health ;
					if ($Bplayer.health <= 0 && !$Bplayer.isDead){
						$Bplayer.die() ;	
					}
					
					%this.damage2 = false ;
					%this.schedule(500,"resetDamage2");
				}
			}
		}
	}	
}
function platformDamage::resetDamage1(%this){
	%this.damage1 = true ;
}
function platformDAmage::resetDamage2(%this){
	%this.damage2 = true ;
}
/////////////////////////////////////////////////////
function waterWave::onLevelLoaded(%this,%scenegraph){
	%this.up = true ;
	%this.Y = %this.getPositionY();
	error ("WORKING");
	%this.enableUpdateCallback();	
	%this.setLinearVelocityY(-5);

}
function waterWave::onUpdate(%this){
	if (%this.up){
		if (%this.getPositionY() <= 9){
			%this.up = false ;
			%this.speed = getRandom(1,5);
			%this.setLinearVelocityY(%this.speed);
				
		}
	}else {
		if (%this.getPositionY() >= 20){
			%this.up = true ;
			%this.speed = getRandom(-5,-1);
			%this.setLinearVelocityY(%this.speed);	
		}
	}
}
