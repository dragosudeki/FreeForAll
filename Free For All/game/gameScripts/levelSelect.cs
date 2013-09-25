function levelSelect::onLevelLoaded(%this, %scenegraph)
{
	$levelSel = %this ;
	$levelSel.levelNum = 1 ;
		
	moveMap.bindCmd(keyboard, "left", "levelL();", "");
	moveMap.bindCmd(keyboard, "right", "levelR();", "");
	moveMap.bindCmd(keyboard, "up", "levelU();", "");
	moveMap.bindCmd(keyboard, "down" , "levelD();","");
	moveMap.bindCmd(keyboard, "A", "levelL();", "");
	moveMap.bindCmd(keyboard, "D", "levelR();", "");
	moveMap.bindCmd(keyboard, "W", "levelU();", "");
	moveMap.bindCmd(keyboard, "S" , "levelD();","");
	
	moveMap.bindCmd(keyboard, "H" , "levelChoose();","");
	moveMap.bindCmd(keyboard, "numpad1" , "levelChoose();","");
	moveMap.bindCmd(keyboard, "J" , "goBack();","");
	moveMap.bindCmd(keyboard, "numpad2" , "goBack();","");
	
	%this.updateSelection();
	$lives = 3;	
}
function goBack(){
	$levelSel.goBackToMain();	
}
function levelD()
{
	if ($levelSel.levelNum < 3 ){
		$levelSel.levelNum = $levelSel.levelNum + 3 ;
		$levelSel.updateSelection();
	}
	else {
		$levelSel.levelNum = 10;
		$levelSel.updateSelection();
	}
}
function levelU()
{
	if ($levelSel.levelNum >= 4 && $levelSel.levelNum != 10 ){
		$levelSel.levelNum = $levelSel.levelNum - 3 ;
		$levelSel.updateSelection();
	}
	else if ($levelSel.levelNum == 10){
		$levelSel.levelNum = 5 ;
		$levelSel.updateSelection();	
	}
}
function levelL()
{
	if ($levelSel.levelNum - 1 >= 1 && $levelSel.levelNum != 10){
		$levelSel.levelNum = $levelSel.levelNum - 1 ;
		$levelSel.updateSelection();
	}
	else if ($levelSel.levelNum == 10){
		$lives += -1 ;
		if ($lives < 1)
			$lives = 1 ;
		lnum.text = $lives;
		$levelSel.updateSelection();
	}
}
function levelR()
{
	if ($levelSel.levelNum + 1 <= 5 && $levelSel.levelNum != 10){
		$levelSel.levelNum = $levelSel.levelNum + 1 ;
		$levelSel.updateSelection();
	}
	else if ($levelSel.levelNum == 10){
		$lives += 1 ;
		if ($lives > 10)
			$lives = 10;
		lnum.text = $lives;
		$levelSel.updateSelection();
	}
	
}

function levelChoose()
{
	$levelSel.levelSelect();	
}

function levelSelect::updateSelection(%this){	
	if ($levelSel.levelNum == 1){
		%this.setPosition(l1.getPositionX(),l1.getPositionY());
	}
	else if ($levelSel.levelNum == 4){
		%this.setBlendAlpha(1);
		%this.setPosition(l4.getPositionX(),l4.getPositionY());
	}
	else if ($levelSel.levelNum == 2){
		%this.setBlendAlpha(1);
		%this.setPosition(l2.getPositionX(),l2.getPositionY());
	}
	else if ($levelSel.levelNum == 5){
		%this.setBlendAlpha(1);
		%this.setPosition(l5.getPositionX(),l5.getPositionY());
	}
	else if ($levelSel.levelNum == 3){
		%this.setBlendAlpha(1);
		%this.setPosition(l3.getPositionX(),l3.getPositionY());
	}
	else if ($levelSel.levelNum == 6){
		%this.setBlendAlpha(1);
		%this.setPosition(l6.getPositionX(),l6.getPositionY());
	}
	if ($levelSel.levelNum == 10){
		
		%this.setBlendAlpha(0);
		lname.setBlendColour(1,0,0);
		if ($lives == 1)
			ll.setBlendColour(0,0,0);
		else
			ll.setBlendColour(1,0,0);
		lnum.setBlendColour(1,0,0);
		if ($lives == 10)
			lr.setBlendColour(0,0,0);
		else
			lr.setBlendColour(1,0,0);
	}else{
		%this.setBlendAlpha(1);
		lname.setBlendColour(1,1,1);
		if ($lives != 1)
		ll.setBlendColour(1,1,1);
		lnum.setBlendColour(1,1,1);
		if ($lives != 10)
		lr.setBlendColour(1,1,1);
	}	
}
function levelSelect::goBackToMain(%this){
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/CharacterSelection.t2d");
    sceneWindow2D.loadLevel($newLevel);
}
function levelSelect::levelSelect(%this){
	
	if ($levelSel.levelNum == 1){
		
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Loading.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		$level = 1 ;	
		$levelSel.levelNum = 0 ;
		
	}
	else if ($levelSel.levelNum == 2){
		
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Loading.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		$level = 2 ;	
		$levelSel.levelNum = 0 ;
		
	}
	else if ($levelSel.levelNum == 3){
		
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Loading.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		$level = 3 ;	
		$levelSel.levelNum = 0 ;
		
	}
	else if ($levelSel.levelNum == 4){
		
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Loading.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		$level = 4 ;	
		$levelSel.levelNum = 0 ;
		
	}
	else if ($levelSel.levelNum == 5){
		
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/Loading.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		$level = 5 ;	
		$levelSel.levelNum = 0 ;
		
	}
}

