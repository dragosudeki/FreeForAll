 function charSelect::onLevelLoaded(%this, %scenegraph)
{
	$charSel = %this ;
	$charSel.levelNum = 1 ;
	$charSel.isDone = false ;
	$charSel.levelNum2 = 2 ;
	$charSel.isDone2 = false ;
	%this.AniFin = false ;
	
	canvas.pushDialog(CharSelGUI);
	
	moveMap.bindCmd(keyboard, "left", "charL();", "");
	moveMap.bindCmd(keyboard, "right", "charR();", "");
	moveMap.bindCmd(keyboard, "up", "charU();", "");
	moveMap.bindCmd(keyboard, "down" , "charD();","");
	
	moveMap.bindCmd(keyboard, "A", "charL2();", "");
	moveMap.bindCmd(keyboard, "D", "charR2();", "");
	moveMap.bindCmd(keyboard, "W", "charU2();", "");
	moveMap.bindCmd(keyboard, "S" , "charD2();","");
	
	moveMap.bindCmd(keyboard, "numpad1" , "charChoose();","");
	moveMap.bindCmd(keyboard, "numpad2" , "undo1();","");
	moveMap.bindCmd(keyboard, "numpad3" , "undo1();","");
	moveMap.bindCmd(keyboard, "H" , "charChoose2();","");
	moveMap.bindCmd(keyboard, "J" , "undo2();","");
	
	moveMap.bindCmd(keyboard, "K" , "undo2();","");
	
	%this.updateSelection();
	%this.updateSelection2();
	
	%this.enableUpdateCallback() ;
	%this.bx1 = namebox1.getPositionX();
	%this.bx2 = namebox2.getPositionX();

	%this.cy = charbox.getPositionY();
	
	namebox1.setPositionX(%this.bx1 - 100);
	namebox2.setPositionX(%this.bx2 + 100);

	namebox1.setLinearVelocityX(100);
	namebox2.setLinearVelocityX(-100);
	charbox.setPositionY(%this.cy + 200);
	//setRes(500,500);
	
	%this.alpha = 0 ;
	
	name1.setBlendAlpha(0);
	name2.setBlendAlpha(0);
	move1Select.setBlendAlpha(0);
	move2Select.setBlendAlpha(0);
	move3Select.setBlendAlpha(0);
	Bmove1Select.setBlendAlpha(0);
	Bmove2Select.setBlendAlpha(0);
	Bmove3Select.setBlendAlpha(0);
	%this.setBlendAlpha(0);
	charSelect2.setBlendAlpha(0);
	M1.setBlendAlpha(0);
	M2.setBlendAlpha(0);
	M3.setBlendAlpha(0);
	M4.setBlendAlpha(0);
	M5.setBlendAlpha(0);
	M6.setBlendAlpha(0);
	C1.setBlendAlpha(0);
	C2.setBlendAlpha(0);
	C3.setBlendAlpha(0);
	C4.setBlendAlpha(0);
	player1Select.setBlendAlpha(0);
	player2Select.setBlendAlpha(0);
	B1.setBlendAlpha(%this.alpha);
	B2.setBlendAlpha(%this.alpha);
	B3.setBlendAlpha(%this.alpha);
	B4.setBlendAlpha(%this.alpha);
	B5.setBlendAlpha(%this.alpha);
	B6.setBlendAlpha(%this.alpha);
	B7.setBlendAlpha(%this.alpha);
	B8.setBlendAlpha(%this.alpha);
	B9.setBlendAlpha(%this.alpha);
	B10.setBlendAlpha(%this.alpha);
	A1.setBlendAlpha(%this.alpha);
	A2.setBlendAlpha(%this.alpha);
	A3.setBlendAlpha(%this.alpha);
	A4.setBlendAlpha(%this.alpha);
	A5.setBlendAlpha(%this.alpha);
	A6.setBlendAlpha(%this.alpha);
	A7.setBlendAlpha(%this.alpha);
	A8.setBlendAlpha(%this.alpha);
	A9.setBlendAlpha(%this.alpha);
	A10.setBlendAlpha(%this.alpha);
	Z1.setBlendAlpha(%this.alpha);
	Z2.setBlendAlpha(%this.alpha);
	charback1.setBlendAlpha(%this.alpha);
	charback2.setBlendAlpha(%this.alpha);
	alxPlay("character");	
}
function charSelect::onUpdate(%this){
		
		if (namebox1.getPositionX() >= %this.bx1 && namebox2.getPositionX() <= %this.bx2){
			
			if (namebox1.getPositionX() >= %this.bx1){
				namebox1.setLinearVelocityX(0);
				namebox1.setPositionX(%this.bx1);
			}
			if (namebox2.getPositionX() <= %this.bx2){
				namebox2.setLinearVelocityX(0);
				namebox2.setPositionX(%this.bx2);
				charbox.setLinearVelocityY(-200);
			}
			if (charbox.getPositionY() <= %this.cy){
				charbox.setLinearVelocityY(0);
				charbox.setPositionY(%this.cy);	
				%this.fadeAll();
			}
			
		}
		
}
function charSelect::fadeAll(%this){
		if (%this.alpha < 1){
			%this.alpha += 0.1 ;
			
			name1.setBlendAlpha(%this.alpha);
			name2.setBlendAlpha(%this.alpha);
			move1Select.setBlendAlpha(%this.alpha);
			move2Select.setBlendAlpha(%this.alpha);
			move3Select.setBlendAlpha(%this.alpha);
			Bmove1Select.setBlendAlpha(%this.alpha);
			Bmove2Select.setBlendAlpha(%this.alpha);
			Bmove3Select.setBlendAlpha(%this.alpha);
			%this.setBlendAlpha(%this.alpha);
			charSelect2.setBlendAlpha(%this.alpha);
			M1.setBlendAlpha(%this.alpha);
			M2.setBlendAlpha(%this.alpha);
			M3.setBlendAlpha(%this.alpha);
			M4.setBlendAlpha(%this.alpha);
			M5.setBlendAlpha(%this.alpha);
			M6.setBlendAlpha(%this.alpha);
			C1.setBlendAlpha(%this.alpha);
			C2.setBlendAlpha(%this.alpha);
			C3.setBlendAlpha(%this.alpha);
			C4.setBlendAlpha(%this.alpha);
			player1Select.setBlendAlpha(%this.alpha);
			player2Select.setBlendAlpha(%this.alpha);
			if (%this.alpha > 0.4){
				B1.setBlendAlpha(0.4);
				B2.setBlendAlpha(0.4);
				B3.setBlendAlpha(0.4);
				B4.setBlendAlpha(0.4);
				B5.setBlendAlpha(0.4);
				B6.setBlendAlpha(0.4);
				B7.setBlendAlpha(0.4);
				B8.setBlendAlpha(0.4);
				B9.setBlendAlpha(0.4);
				B10.setBlendAlpha(0.4);
			}
			else {
				B1.setBlendAlpha(%this.alpha);
				B2.setBlendAlpha(%this.alpha);
				B3.setBlendAlpha(%this.alpha);
				B4.setBlendAlpha(%this.alpha);
				B5.setBlendAlpha(%this.alpha);
				B6.setBlendAlpha(%this.alpha);
				B7.setBlendAlpha(%this.alpha);
				B8.setBlendAlpha(%this.alpha);
				B9.setBlendAlpha(%this.alpha);
				B10.setBlendAlpha(%this.alpha);
			}
			charback1.setBlendAlpha(%this.alpha);
			charback2.setBlendAlpha(%this.alpha);
			A1.setBlendAlpha(%this.alpha);
			A2.setBlendAlpha(%this.alpha);
			A3.setBlendAlpha(%this.alpha);
			A4.setBlendAlpha(%this.alpha);
			A5.setBlendAlpha(%this.alpha);
			A6.setBlendAlpha(%this.alpha);
			A7.setBlendAlpha(%this.alpha);
			A8.setBlendAlpha(%this.alpha);
			A9.setBlendAlpha(%this.alpha);
			A10.setBlendAlpha(%this.alpha);
			Z1.setBlendAlpha(%this.alpha);
			Z2.setBlendAlpha(%this.alpha);
			
			%this.schedule(200,"fadeAll");
		
		}
		else if (!%this.AniFin) {
			error("Finished");
			%this.AniFin = true ;	
		}
}
function undo1()
{
	if ($charSel.AniFin)
	$charSel.unselect1();	
	
}
function undo2()
{
	if ($charSel.AniFin)
	$charSel.unselect2();	
	
}
function charD()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum + 2 <= 10 && $charSel.isDone == false){
		$charSel.levelNum = $charSel.levelNum + 2 ;
		$charSel.updateSelection();
	}
}
function charL()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum - 1 >= 1 && $charSel.isDone == false){
		$charSel.levelNum = $charSel.levelNum - 1 ;
		$charSel.updateSelection();
	}
}
function charR()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum + 1 <= 10 && $charSel.isDone == false){
		$charSel.levelNum = $charSel.levelNum + 1 ;
		$charSel.updateSelection();
	}	
}
function charU()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum - 2 >= 1 && $charSel.isDone == false){
		$charSel.levelNum = $charSel.levelNum - 2 ;
		$charSel.updateSelection();
	}
}
function charChoose()
{
	if ($charSel.AniFin)
	if ($charSel.isDone == false)
		$charSel.charSelect();	
}

function charD2()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum2 + 2 <= 10 && $charSel.isDone2 == false){
		$charSel.levelNum2 = $charSel.levelNum2 + 2 ;
		$charSel.updateSelection2();
	}
}
function charL2()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum2 - 1 >= 1 && $charSel.isDone2 == false){
		$charSel.levelNum2 = $charSel.levelNum2 - 1 ;
		$charSel.updateSelection2();
	}
}
function charR2()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum2 + 1 <= 10 && $charSel.isDone2 == false){
		$charSel.levelNum2 = $charSel.levelNum2 + 1 ;
		$charSel.updateSelection2();
	}	
}
function charU2()
{
	if ($charSel.AniFin)
	if ($charSel.levelNum2 - 2 >= 1 && $charSel.isDone2 == false){
		$charSel.levelNum2 = $charSel.levelNum2 - 2 ;
		$charSel.updateSelection2();
	}
}
function charChoose2()
{
	if ($charSel.AniFin)
	if ($charSel.isDone2 == false)
		$charSel.charSelect2();	
}

function charSelect::updateSelection(%this){
	if ($charSel.levelNum == 1){
		%this.setPosition(-6.816,-22.316);
		player1Select.playAnimation(dominioStand) ;
		player1Select.setSize(25,25);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(dominioSymbol1);
		move2Select.playAnimation(dominioSymbol2);
		move3Select.playAnimation(dominioSymbol3);
		charback1.setFlip(true,false);
		charback1.setImageMap(dominioCover);
		name1.text = "Dominio";
		$playerAA = "dominio";
	}
	else if ($charSel.levelNum == 2){
		%this.setPosition(5.723,-22.316);
		player1Select.setSize(22.0,22.0);
		player1Select.setFlip(false,false);
		player1Select.playAnimation(svriStand) ;
		move1Select.playAnimation(soulShot);
		move2Select.playAnimation(svriSymbol2);
		move3Select.playAnimation(svriMove31);
		charback1.setImageMap(svriCover);
		charback1.setFlip(false,false);
		name1.text = "Svri";
		$playerAA = "svri";
	}
	
	else if ($charSel.levelNum == 3){
		%this.setPosition(-6.816,-9.783);
		player1Select.playAnimation(morteSelect) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(musicNote1);
		move2Select.playAnimation(musicNote2);
		move3Select.playAnimation(note3Symbol);
		charback1.setImageMap(morteCover);
		charback1.setFlip(false,false);
		name1.text = "Mort";
		$playerAA = "morte";
	}
	else if ($charSel.levelNum == 4){
		%this.setPosition(5.723,-9.783);
		player1Select.playAnimation(rupesStandAnimation) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(leafAnimation);
		move2Select.playAnimation(rupePunchSymbol);
		move3Select.playAnimation(rupeChargeSymbol);
		charback1.setImageMap(rupescover);
		charback1.setFlip(false,false);
		name1.text = "Rupes";
		$playerAA = "rupes";
	}
	
	else if ($charSel.levelNum == 5){
		%this.setPosition(-6.816,2.750);
		player1Select.playAnimation(casperStand) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(true,false);
		move1Select.playAnimation(BOMBAnimation);
		move2Select.playAnimation(casperSymbol2);
		move3Select.playAnimation(nuke);
		charback1.setImageMap(casperbackground);
		charback1.setFlip(true,false);
		name1.text = "Casper";
		$playerAA = "casper";
	}
	else if ($charSel.levelNum == 6){
		%this.setPosition(5.723,2.750);
		player1Select.playAnimation(belshazzarStand) ;
		player1Select.setSize(27,27);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(cardThrowSymbol);
		move2Select.playAnimation(belshazzarCard);
		move3Select.playAnimation(diamond);
		charback1.setImageMap(belzzcover);
		name1.text = "Belshazzar";
		$playerAA = "belshazzar";
	}
		
	else if ($charSel.levelNum == 7){
		%this.setPosition(-6.816,15.283);
		player1Select.playAnimation(eltontaStand) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(poop);
		move2Select.playAnimation(eltontaDash);
		move3Select.playAnimation(dynamite);
		charback1.setImageMap(eltontacover);
		charback1.setFlip(false,false);
		name1.text = "El Tonta";
		$playerAA = "eltonta";
	}
	else if ($charSel.levelNum == 8){
		%this.setPosition(5.723,15.283);
		player1Select.setSize(22.911,22.911);
		player1Select.setFlip(false,false);
		player1Select.playAnimation(fearStand) ;
		move1Select.playAnimation(fearSymbol1);
		move2Select.playAnimation(fearSymbol2);
		move3Select.playAnimation(fearSymbol3);
		charback1.setImageMap(fearcover);
		charback1.setFlip(false,false);
		name1.text = "Subject F34R";
		$playerAA = "fear";
	}
	else if ($charSel.levelNum == 9){
		%this.setPosition(B9.getPositionX(),B9.getPositionY());
		player1Select.setSize(22.911,22.911);
		player1Select.setFlip(false,false);
		player1Select.playAnimation(BioStand) ;
		move1Select.playAnimation(BioSymbol1);
		move2Select.playAnimation(BioSymbol2);
		move3Select.playAnimation(BioSymbol3);
		charback1.setImageMap(bioCover);
		charback1.setFlip(false,false);
		name1.text = "Arkaedes";
		$playerAA = "arkaedes";
	}
	else if ($charSel.levelNum == 10){
		%this.setPosition(B10.getPositionX(),B10.getPositionY());
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(true,false);
		player1Select.playAnimation(yukiSelect) ;
		move1Select.playAnimation(energybolt);
		move2Select.playAnimation(yukiSymbol2);
		move3Select.playAnimation(starEffect);
		charback1.setImageMap(yukicover);
		charback1.setFlip(true,false);
		name1.text = "Yuki";
		$playerAA = "yuki";
	}
}
function charSelect::updateSelection2(%this){
	
	if ($charSel.levelNum2 == 1){
		charSelect2.setPosition(-6.816,-22.316);
		player2Select.playAnimation(dominioStand) ;
		player2Select.setSize(25,25);
		player2Select.setFlip(true,false);
		Bmove1Select.playAnimation(dominioSymbol1);
		Bmove2Select.playAnimation(dominioSymbol2);
		Bmove3Select.playAnimation(dominioSymbol3);
		charback2.setImageMap(dominioCover);
		charback2.setFlip(false,false);
		name2.text = "Dominio";
		$playerBB = "dominio";
	}
	else if ($charSel.levelNum2 == 2){
		charSelect2.setPosition(5.723,-22.316);
		player2Select.setSize(22.0,22.0);
		player2Select.setFlip(true,false);
		player2Select.playAnimation(svriStand) ;
		Bmove1Select.playAnimation(soulShot);
		Bmove2Select.playAnimation(svriSymbol2);
		Bmove3Select.playAnimation(svriMove31);
		charback2.setImageMap(svriCover);
		charback2.setFlip(true,false);
		name2.text = "Svri";
		$playerBB = "svri";

	}
	
	else if ($charSel.levelNum2 == 3){
		charSelect2.setPosition(-6.816,-9.783);
		player2Select.playAnimation(morteSelect) ;
		player2Select.setSize(19.9,19.9);
		player2Select.setFlip(true,false);
		Bmove1Select.playAnimation(musicNote1);
		Bmove2Select.playAnimation(musicNote2);
		Bmove3Select.playAnimation(note3Symbol);
		charback2.setImageMap(morteCover);
		charback2.setFlip(true,false);
		name2.text = "Mort";
		$playerBB = "morte";
	}
	else if ($charSel.levelNum2 == 4){
		charSelect2.setPosition(5.723,-9.783);
		player2Select.playAnimation(rupesStandAnimation) ;
		player2Select.setSize(19.9,19.9);
		player2Select.setFlip(true,false);
		Bmove1Select.playAnimation(leafAnimation);
		Bmove2Select.playAnimation(rupePunchSymbol);
		Bmove3Select.playAnimation(rupeChargeSymbol);
		charback2.setImageMap(rupescover);
		charback2.setFlip(true,false);
		name2.text = "Rupes";
		$playerBB = "rupes";
	}
	
	else if ($charSel.levelNum2 == 5){
		charSelect2.setPosition(-6.816,2.750);
		player2Select.playAnimation(casperStand) ;
		player2Select.setFlip(false,false);
		player2Select.setSize(19.9,19.9);
		Bmove1Select.playAnimation(BOMBAnimation);
		Bmove2Select.playAnimation(casperSymbol2);
		Bmove3Select.playAnimation(nuke);
		charback2.setImageMap(casperbackground);
		charback2.setFlip(false,false);
		name2.text = "Casper";
		$playerBB = "casper";
	}
	else if ($charSel.levelNum2 == 6){
		charSelect2.setPosition(5.723,2.750);
		player2Select.playAnimation(belshazzarStand) ;
		player2Select.setSize(27,27);
		player2Select.setFlip(true,false);
		Bmove1Select.playAnimation(cardThrowSymbol);
		Bmove2Select.playAnimation(belshazzarCard);
		Bmove3Select.playAnimation(diamond);
		charback2.setImageMap(belzzcover);
		name2.text = "Belshazzar";
		$playerBB = "belshazzar";
	}
	
	else if ($charSel.levelNum2 == 7){
		charSelect2.setPosition(-6.816,15.283);
		player2Select.playAnimation(eltontaStand) ;
		player2Select.setSize(19.9,19.9);
		player2Select.setFlip(true,false);
		Bmove1Select.playAnimation(poop);
		Bmove2Select.playAnimation(eltontaDash);
		Bmove3Select.playAnimation(dynamite);
		charback2.setImageMap(eltontacover);
		charback2.setFlip(true,false);
		name2.text = "El Tonta";
		$playerBB = "eltonta";
	}
	else if ($charSel.levelNum2 == 8){
		charSelect2.setPosition(5.723,15.283);
		player2Select.playAnimation(fearStand) ;
		player2Select.setSize(22.911,22.911);
		player2Select.setFlip(true,false);
		Bmove1Select.playAnimation(fearSymbol1);
		Bmove2Select.playAnimation(fearSymbol2);
		Bmove3Select.playAnimation(fearSymbol3);
		charback2.setImageMap(fearcover);
		name2.text = "Subject F34R";
		$playerBB = "fear";
	}
	
	else if ($charSel.levelNum2 == 9){
		charSelect2.setPosition(B9.getPositionX(),B9.getPositionY());
		player2Select.setSize(22.911,22.911);
		player2Select.setFlip(true,false);
		player2Select.playAnimation(BioStand) ;
		Bmove1Select.playAnimation(BioSymbol1);
		Bmove2Select.playAnimation(BioSymbol2);
		Bmove3Select.playAnimation(BioSymbol3);
		charback2.setImageMap(bioCover);
		charback2.setFlip(true,false);
		name2.text = "Arkaedes";
		$playerBB = "arkaedes";
	}
	else if ($charSel.levelNum2 == 10){
		charSelect2.setPosition(B10.getPositionX(),B10.getPositionY());
		player2Select.setSize(19.9,19.9);
		player2Select.setFlip(true,false);
		player2Select.playAnimation(yukiSelect) ;
		Bmove1Select.playAnimation(energybolt);
		Bmove2Select.playAnimation(yukiSymbol2);
		Bmove3Select.playAnimation(starEffect);
		charback2.setImageMap(yukicover);
		charback2.setFlip(true,false);
		name2.text = "Yuki";
		$playerBB = "yuki";
	}
}

function charSelect::charSelect(%this){
	
	
	
	if ($charSel.isDone2 == true){
		
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/LevelSelection.t2d");
        sceneWindow2D.loadLevel($newLevel); 	
		canvas.popDialog(CharSelGUI);
		$charSel.levelNum = 0 ;
		
	}
	else {
		
		 $charSel.isDone = true ;
		 %this.setBlendAlpha(0.5);
		
	}
}
function charSelect::mainMenu(%this){
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/MainMenu.t2d");
    sceneWindow2D.loadLevel($newLevel); 
	canvas.popDialog(CharSelGUI);
	alxStopAll();
}
function charSelect::unselect1(%this){
	
	if (!$charSel.isDone && !$charSel.isDone2){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/MainMenu.t2d");
        sceneWindow2D.loadLevel($newLevel);
		canvas.popDialog(CharSelGUI); 	
		alxStopAll();
	}
	else if ($charSel.isDone == true){
		
		$charSel.isDone = false ;
		 %this.setBlendAlpha(1.0);
		
	}
	
}
function charSelect::unselect2(%this){
	
	if (!$charSel.isDone && !$charSel.isDone2){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/MainMenu.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		canvas.popDialog(CharSelGUI);	
		alxStopAll();
	}
	else if ($charSel.isDone2 == true){
		
		$charSel.isDone2 = false ;
		 charSelect2.setBlendAlpha(1.0);
		
	}
	
}

function charSelect::charSelect2(%this){
	
	if ($charSel.isDone == true){
		
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/LevelSelection.t2d");
        sceneWindow2D.loadLevel($newLevel); 
		canvas.popDialog(CharSelGUI);	
		$charSel.levelNum = 0 ;
		
	}
	else {
		
		 $charSel.isDone2 = true ;
		 charSelect2.setBlendAlpha(0.5);
		
	}
	
}
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
function soloSelect::onLevelLoaded(%this, %scenegraph)
{
	$soloSel = %this ;
	$soloSel.levelNum = 1 ;
	$soloSel.isDone = false ;
	%this.AniFin = false ;
	moveMap.bindCmd(keyboard, "left", "soloL();", "");
	moveMap.bindCmd(keyboard, "right", "soloR();", "");
	moveMap.bindCmd(keyboard, "up", "soloU();", "");
	moveMap.bindCmd(keyboard, "down" , "soloD();","");
	
	moveMap.bindCmd(keyboard, "numpad1" , "soloChoose();","");
	moveMap.bindCmd(keyboard, "numpad2" , "undo();","");
	moveMap.bindCmd(keyboard, "numpad3" , "undo();","");
	
	%this.updateSelection();
	
	%this.alpha = 0 ;
	
	name1.setBlendAlpha(0);
	move1Select.setBlendAlpha(0);
	move2Select.setBlendAlpha(0);
	move3Select.setBlendAlpha(0);
	%this.setBlendAlpha(0);
	M1.setBlendAlpha(0);
	M2.setBlendAlpha(0);
	M3.setBlendAlpha(0);
	C1.setBlendAlpha(0);
	C2.setBlendAlpha(0);
	player1Select.setBlendAlpha(0);
	B1.setBlendAlpha(%this.alpha);
	B2.setBlendAlpha(%this.alpha);
	B3.setBlendAlpha(%this.alpha);
	B4.setBlendAlpha(%this.alpha);
	B5.setBlendAlpha(%this.alpha);
	B6.setBlendAlpha(%this.alpha);
	B7.setBlendAlpha(%this.alpha);
	B8.setBlendAlpha(%this.alpha);
	A2.setBlendAlpha(%this.alpha);
	A3.setBlendAlpha(%this.alpha);
	A4.setBlendAlpha(%this.alpha);
	A5.setBlendAlpha(%this.alpha);
	A6.setBlendAlpha(%this.alpha);
	A7.setBlendAlpha(%this.alpha);
	A8.setBlendAlpha(%this.alpha);
	charback1.setBlendAlpha(%this.alpha);
	
	%this.enableUpdateCallback();
	
	%this.bx1 = namebox1.getPositionX();
	%this.cy = charbox.getPositionY();
	namebox1.setPositionX(%this.bx1 - 100);
	namebox1.setLinearVelocityX(100);
	charbox.setPositionY(%this.cy + 200);
}
function undo()
{
if ($soloSel.AniFin)	
	$soloSel.unselect1();	
}
function soloD()
{
	if ($soloSel.AniFin)
	if ($soloSel.levelNum + 2 <= 8 && $soloSel.isDone == false){
		$soloSel.levelNum = $soloSel.levelNum + 2 ;
		$soloSel.updateSelection();
	}
}
function soloL()
{
	if ($soloSel.AniFin)
	if ($soloSel.levelNum - 1 >= 1 && $soloSel.isDone == false){
		$soloSel.levelNum = $soloSel.levelNum - 1 ;
		$soloSel.updateSelection();
	}
}
function soloR()
{
	if ($soloSel.AniFin)
	if ($soloSel.levelNum + 1 <= 8 && $soloSel.isDone == false){
		$soloSel.levelNum = $soloSel.levelNum + 1 ;
		$soloSel.updateSelection();
	}	
}
function soloU()
{
	if ($soloSel.AniFin)
	if ($soloSel.levelNum - 2 >= 1 && $soloSel.isDone == false){
		$soloSel.levelNum = $soloSel.levelNum - 2 ;
		$soloSel.updateSelection();
	}
}
function soloChoose()
{
	if ($soloSel.AniFin)
	if ($soloSel.isDone == false)
		$soloSel.soloSelect();	
}

function soloSelect::updateSelection(%this){
	
	if ($soloSel.levelNum == 1){
		%this.setPosition(B1.getPositionX(),B1.getPositionY());
		//player1Select.playAnimation(rupesStandAnimation) ;
		//move1Select.playAnimation(leafAnimation);
		//move2Select.playAnimation(musicNote2);
		//move3Select.playAnimation(note3Symbol);
		//name1.text = "Rupes";
		//$playerAA = "rupes";
	}
	else if ($soloSel.levelNum == 2){
		%this.setPosition(B2.getPositionX(),B2.getPositionY());
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(true,false);
		player1Select.playAnimation(yukiSelect) ;
		move1Select.playAnimation(energybolt);
		//move2Select.playAnimation(musicNote2);
		move3Select.playAnimation(starEffect);
		charback1.setImageMap(blankImageMap);
		name1.text = "Yuki";
		$playerAA = "yuki";
	}
	
	else if ($soloSel.levelNum == 3){
		%this.setPosition(B3.getPositionX(),B3.getPositionY());
		player1Select.playAnimation(morteSelect) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(musicNote1);
		move2Select.playAnimation(musicNote2);
		move3Select.playAnimation(note3Symbol);
		charback1.setImageMap(blankImageMap);
		name1.text = "Mort";
		$playerAA = "morte";
	}
	else if ($soloSel.levelNum == 4){
		%this.setPosition(B4.getPositionX(),B4.getPositionY());
		player1Select.playAnimation(rupesStandAnimation) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(leafAnimation);
		//move2Select.playAnimation(musicNote2);
		//move3Select.playAnimation(note3Symbol);
		charback1.setImageMap(blankImageMap);
		name1.text = "Rupes";
		$playerAA = "rupes";
	}
	
	else if ($soloSel.levelNum == 5){
		%this.setPosition(B5.getPositionX(),B5.getPositionY());
		player1Select.playAnimation(casperStand) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(true,false);
		move1Select.playAnimation(BOMBAnimation);
		//move2Select.playAnimation(musicNote2);
		move3Select.playAnimation(nuke);
		charback1.setImageMap(casperbackground);
		name1.text = "Casper";
		$playerAA = "casper";
	}
	else if ($soloSel.levelNum == 6){
		%this.setPosition(B6.getPositionX(),B6.getPositionY());
		player1Select.playAnimation(belshazzarStand) ;
		player1Select.setSize(27,27);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(cardThrowSymbol);
		move2Select.playAnimation(belshazzarCard);
		move3Select.playAnimation(diamond);
		charback1.setImageMap(blankImageMap);
		name1.text = "Belshazzar";
		$playerAA = "belshazzar";
	}
	
	else if ($soloSel.levelNum == 7){
		%this.setPosition(B7.getPositionX(),B7.getPositionY());
		player1Select.playAnimation(eltontaStand) ;
		player1Select.setSize(19.9,19.9);
		player1Select.setFlip(false,false);
		move1Select.playAnimation(poop);
		move2Select.playAnimation(eltontaDash);
		move3Select.playAnimation(dynamite);
		charback1.setImageMap(blankImageMap);
		name1.text = "El Tonta";
		$playerAA = "eltonta";
	}
	else if ($soloSel.levelNum == 8){
		%this.setPosition(B8.getPositionX(),B8.getPositionY());
		player1Select.playAnimation(fearStand) ;
		player1Select.setSize(22.911,22.911);
		player1Select.setFlip(false,false);
		//move1Select.playAnimation(poop);
		//move2Select.playAnimation(eltontaDash);
		//move3Select.playAnimation(dynamite);
		charback1.setImageMap(blankImageMap);
		name1.text = "Subject F34R";
		$playerAA = "fear";
	}
}

function soloSelect::soloSelect(%this){
			
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/Solo1.t2d");
    sceneWindow2D.loadLevel($newLevel); 
	canvas.popDialog(CharSelGUI);	
	$soloSel.levelNum = 0 ;
	
			
}
function soloSelect::unselect1(%this){
	if (!$soloSel.isDone){
		sceneWindow2D.endLevel();  
		$newLevel = expandFileName("~/data/levels/MainMenu.t2d");
        sceneWindow2D.loadLevel($newLevel); 	
		canvas.popDialog(CharSelGUI);
	}
}
function soloSelect::onUpdate(%this){
		
		if (namebox1.getPositionX() >= %this.bx1){
			
			if (namebox1.getPositionX() >= %this.bx1){
				namebox1.setLinearVelocityX(0);
				namebox1.setPositionX(%this.bx1);
				charbox.setLinearVelocityY(-200);
			}
			
			if (charbox.getPositionY() <= %this.cy){
				charbox.setLinearVelocityY(0);
				charbox.setPositionY(%this.cy);	
				%this.fadeAll();
			}
			
		}
		
}
function soloSelect::fadeAll(%this){
		if (%this.alpha < 1){
			%this.alpha += 0.1 ;
			
			name1.setBlendAlpha(%this.alpha);
			move1Select.setBlendAlpha(%this.alpha);
			move2Select.setBlendAlpha(%this.alpha);
			move3Select.setBlendAlpha(%this.alpha);
			%this.setBlendAlpha(%this.alpha);
			M1.setBlendAlpha(%this.alpha);
			M2.setBlendAlpha(%this.alpha);
			M3.setBlendAlpha(%this.alpha);
			C1.setBlendAlpha(%this.alpha);
			C2.setBlendAlpha(%this.alpha);
			player1Select.setBlendAlpha(%this.alpha);
			charback1.setBlendAlpha(%this.alpha);
			if (%this.alpha > 0.4){
				B1.setBlendAlpha(0.4);
				B2.setBlendAlpha(0.4);
				B3.setBlendAlpha(0.4);
				B4.setBlendAlpha(0.4);
				B5.setBlendAlpha(0.4);
				B6.setBlendAlpha(0.4);
				B7.setBlendAlpha(0.4);
				B8.setBlendAlpha(0.4);
			}
			else {
				B1.setBlendAlpha(%this.alpha);
				B2.setBlendAlpha(%this.alpha);
				B3.setBlendAlpha(%this.alpha);
				B4.setBlendAlpha(%this.alpha);
				B5.setBlendAlpha(%this.alpha);
				B6.setBlendAlpha(%this.alpha);
				B7.setBlendAlpha(%this.alpha);
				B8.setBlendAlpha(%this.alpha);
			}
			A2.setBlendAlpha(%this.alpha);
			A3.setBlendAlpha(%this.alpha);
			A4.setBlendAlpha(%this.alpha);
			A5.setBlendAlpha(%this.alpha);
			A6.setBlendAlpha(%this.alpha);
			A7.setBlendAlpha(%this.alpha);
			A8.setBlendAlpha(%this.alpha);
			%this.schedule(200,"fadeAll");
		
		}
		else if (!%this.AniFin)
		%this.AniFin = true ;	
}