//---------------------------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
//---------------------------------------------------------------------------------------------

//---------------------------------------------------------------------------------------------
// initializeProject
// Perform game initialization here.
//---------------------------------------------------------------------------------------------
function initializeProject()
{
	// Load up the in game gui.
   exec("~/gui/mainScreen.gui");
   
   // Exec game scripts.
   exec("./gameScripts/game.cs");
   exec("./gameScripts/audioDatablocks.cs");
   
   exec("./gameScripts/spawn.cs");
   exec("./gameScripts/flyingClass.cs");
   exec("./gameScripts/enemyShips.cs");
   
   exec("./gameScripts/playerClass.cs");
   exec("./gameScripts/playerClass2.cs");
   exec("./gameScripts/morteClass.cs");
   exec("./gameScripts/rupesClass.cs");
   exec("./gameScripts/casperClass.cs");
   exec("./gameScripts/belshazzarClass.cs");
   exec("./gameScripts/elTontaClass.cs");
   exec("./gameScripts/yukiClass.cs");
   exec("./gameScripts/fearClass.cs");
   exec("./gameScripts/dominioClass.cs");
   exec("./gameScripts/bioClass.cs");
   exec("./gameScripts/svriClass.cs");
   
   exec("./gameScripts/bulletClass2.cs");
   exec("./gameScripts/note1Class.cs");
   exec("./gameScripts/projectileClass1.cs");
   
   exec("./gameScripts/BplayerClass.cs");
   exec("./gameScripts/BplayerClass2.cs");
   exec("./gameScripts/BmorteClass.cs");
   exec("./gameScripts/BrupesClass.cs");
   exec("./gameScripts/BcasperClass.cs");
   exec("./gameScripts/BbelshazzarClass.cs");
   exec("./gameScripts/BelTontaClass.cs");
   exec("./gameScripts/ByukiClass.cs");
   exec("./gameScripts/BfearClass.cs");
   exec("./gameScripts/BdominioClass.cs");
   exec("./gameScripts/BbioClass.cs");
   exec("./gameScripts/BsvriClass.cs");
   
   exec("./gameScripts/enemyThrow.cs");
   
   exec("./gameScripts/timer.cs");
   exec("./gameScripts/collision.cs");
   
   exec("./gameScripts/platform.cs");
   exec("./gameScripts/platformMove.cs");
   
   exec("./gameScripts/camera.cs");
   
   exec("./behaviors/cameraBehave.cs");
   
   exec("./gameScripts/levelSelect.cs");
   exec("./gameScripts/charSelect.cs");
   exec("./gameScripts/mainScreen.cs");
   exec("./gameScripts/loadingClass.cs");
   
   exec("./gui/PauseTest.gui");
   exec("./gui/RoundEnd.gui");
   exec("~/gui/mainGUI.gui");
   exec("~/gui/controlsGUI.gui");
   exec("~/gui/CharSelGUI.gui");
   
   // This is where the game starts. Right now, we are just starting the first level. You will
   // want to expand this to load up a splash screen followed by a main menu depending on the
   // specific needs of your game. Most likely, a menu button will start the actual game, which
   // is where startGame should be called from.
   startGame( expandFilename($Game::DefaultScene) );
}

//---------------------------------------------------------------------------------------------
// shutdownProject
// Clean up your game objects here.
//---------------------------------------------------------------------------------------------
function shutdownProject()
{
   endGame();
}

//---------------------------------------------------------------------------------------------
// setupKeybinds
// Bind keys to actions here..
//---------------------------------------------------------------------------------------------
function setupKeybinds()
{
   new ActionMap(moveMap);
   //moveMap.bind("keyboard", "a", "doAction", "Action Description");
}
