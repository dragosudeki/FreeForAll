function timer::onLevelLoaded(%this, %scenegraph)
{
	$timer = %this ;
	%this.win = 0 ;
	%this.enableUpdateCallback() ;
	$timer.start = mRound(getSimTime() / 1000);
	
	end1.setPositionX(end1.getPositionX() + 1000);
	end2.setPositionX(end2.getPositionX() + 1000);
	endtext.setPositionX(endtext.getPositionX() + 1000);
}
function timer::onUpdate(%this){
	TimerDis.text = 300 + $timer.start - mRound(getSimTime() / 1000);
	if (%this.win != 0){
		if (end1.getPositionX() <= 0){
			end1.setLinearVelocityX(0);
			end2.setLinearVelocityX(0);
			endtext.setLinearVelocityX(0);
			end1.setPositionX(0);
			end2.setPositionX(0);
			endtext.setPositionX(0);
			canvas.pushDialog(RoundEnd);
			%this.win = 0;
			$timescale = 0;
		}
	}
}
function timer::endGame1(%this){
	error("ENDGAME2");
	%this.win = 3 ;
	end1.setLinearVelocityX(-800);
	end2.setLinearVelocityX(-800);
	endtext.setLinearVelocityX(-800);
	if (endtext.getImageMap() $= "BOMBImageMap"){
		endtext.setImageMap(player1wins);
	}
	
}
function timer::endGame2(%this){
	%this.win = 2 ;
	end1.setLinearVelocityX(-800);
	end2.setLinearVelocityX(-800);
	endtext.setLinearVelocityX(-800);
	if (endtext.getImageMap() $= "BOMBImageMap"){
		endtext.setImageMap(player2wins);
	}
}

function timer::goBackToSelection(%this){
	
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/LevelSelection.t2d");
    sceneWindow2D.loadLevel($newLevel); 
	canvas.popDialog(PauseTest);
	canvas.popDialog(RoundEnd);
	alxStopAll(); 
	$timescale = 1 ;
	
}

function timer::goBackToChar(%this){
	
	sceneWindow2D.endLevel(); 
	if ($onSolo == 0){ 
		$newLevel = expandFileName("~/data/levels/CharacterSelection.t2d");
		sceneWindow2D.loadLevel($newLevel); 
	}else{
		$newLevel = expandFileName("~/data/levels/SoloSelection.t2d");
    	sceneWindow2D.loadLevel($newLevel); 
	}
	alxStopAll();
	canvas.popDialog(PauseTest);
	canvas.popDialog(RoundEnd);
	$timescale = 1 ;
	
}

function timer::goBackToMain(%this){
	
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/MainMenu.t2d");
    sceneWindow2D.loadLevel($newLevel); 	
	canvas.popDialog(PauseTest);
	canvas.popDialog(RoundEnd);
	alxStopAll();
	$timescale = 1 ;
	
}

function timer::restartLevel(%this){
	
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName($LastLevel);
    sceneWindow2D.loadLevel($newLevel); 	
	canvas.popDialog(PauseTest);
	canvas.popDialog(RoundEnd);
	$timescale = 1 ;
	
}
////////////////////////////////////////////////////
function move1::onLevelLoaded(%this, %scenegraph)
{
	$warning1 = %this;
	%this.enableUpdateCallback() ;
}
function move1::onUpdate(%this){
	if ($player.onCool1){
		
		%this.setBlendColour(0,0,0,255);
		
	}
	else{
		
		%this.setBlendColour(255,255,255,255);
		
	}
	if ($player.health <= 40 && !%this.happened){
		%this.happened = true;
		%this.schedule(1000,"changeColour");
	}
}
function move1::changeColour(%this){
	%blending = $player.getBlendColour();
	%red = getWord(%blending,1);
	error(%blending);
	if ($player.health>0 && $player.health<=40){
		if (%red > 0){
			$player.setBlendColour(1,0,0,1);
		}else{
			$player.setBlendColour(1,1,1,1);
		}
		%this.schedule(500,"changeColour");
	}
}
function move2::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
}
function move2::onUpdate(%this){
	if ($player.onCool2){
		
		%this.setBlendColour(0,0,0,255);
		
	}
	else{
		
		%this.setBlendColour(255,255,255,255);
		
	}
}
function move3::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
}
function move3::onUpdate(%this){
	if ($player.onCool3){
		
		%this.setBlendColour(0,0,0,255);
		
	}
	else{
		
		%this.setBlendColour(255,255,255,255);
		
	}
}
////////////////////////////////////////////////////
function Bmove1::onLevelLoaded(%this, %scenegraph)
{
	$warning2 = %this ;
	%this.enableUpdateCallback() ;
}
function Bmove1::onUpdate(%this){
	if ($Bplayer.onCool1){
		
		%this.setBlendColour(0,0,0,255);
		
	}
	else{
		
		%this.setBlendColour(255,255,255,255);
		
	}
	if ($Bplayer.health <= 40 && !%this.happened){
		%this.happened = true;
		%this.schedule(1000,"changeColour");
	}
}
function Bmove1::changeColour(%this){
	%blending = $Bplayer.getBlendColour();
	%red = getWord(%blending,1);
	error(%blending);
	if ($Bplayer.health>0 && $Bplayer.health<=40){
		if (%red > 0){
			$Bplayer.setBlendColour(1,0,0,1);
		}else{
			$Bplayer.setBlendColour(1,1,1,1);
		}
		%this.schedule(500,"changeColour");
	}
}
function Bmove2::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
}
function Bmove2::onUpdate(%this){
	if ($Bplayer.onCool2){
		
		%this.setBlendColour(0,0,0,255);
		
	}
	else{
		
		%this.setBlendColour(255,255,255,255);
		
	}
}
function Bmove3::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
}
function Bmove3::onUpdate(%this){
	if ($Bplayer.onCool3){
		
		%this.setBlendColour(0,0,0,255);
		
	}
	else{
		
		%this.setBlendColour(255,255,255,255);
		
	}
}
////////////////////////////////////////////////////
function timerShow::onLevelLoaded(%this, %scenegraph)
{
	%this.enableUpdateCallback() ;
	%this.start = mRound(getSimTime() / 1000);
}
function timerShow::onUpdate(%this){
	%this.text = 300 + %this.start - mRound(getSimTime() / 1000);
}