function mainScreen::onLevelLoaded(%this, %scenegraph)
{	
	$main = %this ;
	canvas.pushDialog(mainGUI);
	playSound();
	alxPlay("main");
}

function mainScreen::goTo1P(%this){
	
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/SoloSelection.t2d");
    sceneWindow2D.loadLevel($newLevel); 
	canvas.popDialog(mainGUI);
	$timescale = 1 ;
	alxStopAll();
}
function mainScreen::goTo2P(%this){
	
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/CharacterSelection.t2d");
    sceneWindow2D.loadLevel($newLevel); 	
	canvas.popDialog(mainGUI);
	$timescale = 1 ;
	alxStopAll();
}
function mainScreen::goToCon(%this){
	
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/Controls.t2d");
    sceneWindow2D.loadLevel($newLevel); 	
	canvas.popDialog(mainGUI);
	$timescale = 1 ;
}
/////////////////////////////////////////////////////////////
function controls::onLevelLoaded(%this, %scenegraph)
{	
	$control = %this ;
	canvas.pushDialog(controlsGUI2);
}

function controls::goToMain(%this){
	
	sceneWindow2D.endLevel();  
	$newLevel = expandFileName("~/data/levels/MainMenu.t2d");
    sceneWindow2D.loadLevel($newLevel); 
	canvas.popDialog(controlsGUI2);
	$timescale = 1 ;
	
}
/////////////////////////////////////////////////////////////
//getRandom();
function mainEvent1::onLevelLoaded(%this, %scenegraph){
	
	%this.max = 4 ;
	
	// 0 - Rupes charge
	// 1 - El Tonta poop & run
	// 2 - Casper throw bomb and die
	// 3 - Belshazzar wave and card down
	%this.turn = 0 ;
	// -157,-27
	%this.x0 = %this.getPositionX();
	%this.y0 = %this.getPositionY();
	// -157,25
	%this.x1 = eltontaEvent.getPositionX();
	%this.y1 = eltontaEvent.getPositionY();
	// 80,24
	%this.x2 = casperEvent.getPositionX();
	%this.y2 = casperEvent.getPositionY();
	// -75,-12.50
	%this.x3 = belshazzarEvent.getPositionX();
	%this.y3 = belshazzarEvent.getPositionY();
	// 100,2.50
	%this.x4 = yukiEvent.getPositionX();
	%this.y4 = yukiEvent.getPositionY();
	
	%this.enableUpdateCallback();
	
	%this.previous = -1 ;
	
	%this.resetEvent();
	
}
function mainEvent1::resetEvent(%this){
	%this.turn = getRandom(0,%this.max);
	if (%this.previous != -1){
		%this.checkPreviousEvent();	
	}
	%this.previous = %this.turn ;
	
	
	
	if (%this.turn == 0){
		%this.setPosition(%this.x0, %this.y0);
		%this.setLinearVelocityX(80);
		%this.setFlip(false,false);
	}
	else if (%this.turn == 1){
		%this.hasPoop = false ;
		eltontaEvent.setLinearVelocityX(50);
		eltontaEvent.setLinearVelocityY(0);
		eltontaEvent.setPosition(%this.x1,%this.y1);
		eltontaEvent.playAnimation(eltontaWalk);	
	}
	else if (%this.turn == 2){
		%this.part2 = 0 ;
		casperEvent.setLinearVelocityX(-40);
		casperEvent.setPosition(%this.x2,%this.y2);
		casperEvent.setFlip(false,false);
		casperEvent.playAnimation(casperWalk);
		
	}
	else if(%this.turn == 3){
		%this.part2 = 0 ;
		belshazzarEvent.setLinearVelocityX(40);
		belshazzarEvent.setPosition(%this.x3,%this.y3);
		belshazzarEvent.setFlip(false,false);
		belshazzarEvent.playAnimation(belshazzarRun);	
	}
	else if(%this.turn == 4){
		%this.part2 = 0 ;
		yukiEvent.setLinearVelocityX(-40);
		yukiEvent.setPosition(%this.x4,%this.y4);
		yukiEvent.setFlip(false,false);
		yukiEvent.playAnimation(yukiStand);	
	}
	
}
function mainEvent1::checkPreviousEvent(%this){
	%this.turn = getRandom(0,%this.max);
	warn(%this.turn SPC %this.previous);
	if (%this.turn == %this.previous){
		%this.checkPreviousEvent();	
	}
}
function mainEvent1::onUpdate(%this){
	
	if (%this.turn == 1){
		if (eltontaEvent.getPositionX() >= -20 && %this.hasPoop == false){
			%this.poop = new t2dAnimatedSprite()
			{
				scenegraph = %this.scenegraph ;
			} ;
			%this.poop.playAnimation(poop);
			%this.poop.setLayer(10);
			%this.poop.setSize(10.3,10.3);
			%this.poop.setPosition(eltontaEvent.getPositionX(),28.5);
			%this.hasPoop = true ;
		}
		if (eltontaEvent.getPositionX() >= 0){
			//Stand and die
			eltontaEvent.setLinearVelocityX(0);
			
			if (eltontaEvent.getAnimationName() $= "eltontaDeath")
			{
				if (eltontaEvent.getIsAnimationFinished())
				{
					eltontaEvent.setLinearVelocityY(50);
					%this.poop.safeDelete();
					%this.resetEvent();
				}
			}
			else{
				eltontaEvent.playAnimation(elTontaDeath);
			}
			
		}
		
	}
	else if (%this.turn == 2){
		if (casperEvent.getPositionX() <= 40){
			if (%this.part2 == 0){
				casperEvent.setLinearVelocityX(0);
				
				if (casperEvent.getAnimationName() $= "casperThrow")
				{
					if (casperEvent.getIsAnimationFinished())
					{
						casperEvent.playAnimation(casperStand);
						%this.casperFire();
					}
				}
				else if (casperEvent.getAnimationName() $= "casperWalk"){
					casperEvent.playAnimation(casperThrow);
				}
				
			}
			else if (%this.part2 == 1){
				%this.bomb = new t2dAnimatedSprite()
				{
					scenegraph = %this.scenegraph ;
				} ;
				%this.bomb.playAnimation(nuke);
				%this.bomb.setSize(31.250,31.250);
				%this.bomb.setPosition(casperEvent.getPositionX(),casperEvent.getPositionY() - 200);
				%this.bomb.setLinearVelocityY(90);
				
				casperEvent.playAnimation(casperDeath);	
				%this.nextPart2();
			}
			else if (%this.part2 == 2){
				if (%this.bomb.getPositionY() >= casperEvent.getPositionY()){
					%this.collisionSplash = new t2dAnimatedSprite()
					{
						scenegraph = %this.scenegraph	;
						class = collisionSplash ;
					} ;
					%this.collisionSplash.setPosition(casperEvent.getPositionX(),casperEvent.getPositionY());
					%this.collisionSplash.appear();
					%this.collisionSplash.setSize(30,30);
					casperEvent.setPosition(%this.x2, %this.y2);
					%this.bomb.safeDelete();
					%this.resetEvent();
				}
			}
		}
	}
	else if(%this.turn == 3){
		if(belshazzarEvent.getPositionX() >= -39 && %this.part2 == 0){
			belshazzarEvent.setLinearVelocityX(0);
			
			if (belshazzarEvent.getAnimationName() $= "belshazzarThrow")
			{
				if (belshazzarEvent.getIsAnimationFinished())
				{
					belshazzarEvent.playAnimation(belshazzarStand);
					%this.schedule(750,"nextPart2");	
				}
			}
			else if (belshazzarEvent.getAnimationName() $= "belshazzarRun"){
				belshazzarEvent.playAnimation(belshazzarThrow);
				%this.belshFire();
			}
		}
		if(%this.part2 == 1){
			belshazzarEvent.setLinearVelocityY(100);
			belshazzarEvent.playAnimation(belshazzarCard);
			
			if (belshazzarEvent.getPositionY() >= 50){
				belshazzarEvent.setLinearVelocityY(0);
				%this.resetEvent();
			}		
		}
	}
	else if(%this.turn == 4){
		if (yukiEvent.getPositionX() <= 42){
			if (%this.part2 == 0){
				yukiEvent.setLinearVelocityX(0);
				yukiEvent.playAnimation(yukiAttack);
				%this.yukiFire();
			}
			else if (%this.part2==1){
				if (yukiEvent.getAnimationName() $= "yukiAttack")
					if (yukiEvent.getIsAnimationFinished()){
						%this.schedule(750,"yukiMove");
					}
			}
			else if (%this.part2>1){
				yukiEvent.setFlip(true,false);
				yukiEvent.setLinearVelocityX(40);
			}
		}
		else if (yukiEvent.getPositionX() > 100){
			yukiEvent.setLinearVelocityX(0);
			%this.resetEvent();	
		}
	}
}
function mainEvent1::yukiMove(%this){
	yukiEvent.playAnimation(yukiStand);
	%this.nextPart2();
}
function mainEvent1::yukiFire(%this){
	yukiEvent.isLeft = true ;
	%this.starClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = starClass ;
		source = yukiEvent ;
		number = 5 ;
		start = yukiEvent.getPositionX() - 3.7 ;
	} ;
	%this.starClass.fire();
	%this.starClass.setSize(4.632,4.632);
	%this.nextPart2();
}
function mainEvent1::belshFire(%this){
	%this.cardClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = cardClass ;
		source = %this ;
	} ;
	%this.cardClass.fire();
	%this.cardClass.setPosition(belshazzarEvent.getPositionX(),belshazzarEvent.getPositionY());
	%this.cardClass.setLayer(5);
	%this.cardClass.setSize(5,5);
	%this.cardClass.setLinearVelocityX(70);	
	
}
function mainEvent1::casperFire(%this){
	
	%this.bulletClass = new t2dAnimatedSprite()
	{
		scenegraph = %this.scenegraph ;
		class = bulletClass ;
		enemy1 = %this ;
	} ;
	%this.bulletClass.fire1();
	%this.bulletClass.setSize(5.600,5.600);
	%this.bulletClass.setPositionX(casperEvent.getPositionX() + 8) ;
	%this.bulletClass.setPositionY(casperEvent.getPositionY() - 7) ;
	%this.bulletClass.setLinearVelocityX(-30);
	
	%this.schedule(750,"nextPart2");
	
}
function mainEvent1::nextPart2(%this){
	%this.part2 += 1 ;	
}
function mainEvent1::onCollision(%srcObj, %dstObj, %srcRef, %dstRef, %time, %normal, %contactCount, %contacts ){
	
	// Rupes running around
	if (%srcObj.turn == 0){
		if (%dstObj.class $= "EndTurn1"){
			%srcObj.setLinearVelocityX(0);
			%srcObj.resetEvent();
		}
		if (%dstObj.class $= "TurnAround1"){
			%srcObj.setLinearVelocityX(-80);
			%srcObj.setFlip(true,false);
			%srcObj.isLeft = true ;
			
		}
	}
	
}