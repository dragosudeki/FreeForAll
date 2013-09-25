new AudioDescription(SoundLoop)
{
   volume		= 1.0;
   type		   = 0;
   isLooping	= true;
   is3D		   = false;
};

new AudioProfile(main)
{
   filename	   = "~/data/audio/MainMenu.wav";
   description = "SoundLoop";
   preload 	   = true;
};
new AudioProfile(character)
{
   filename	   = "~/data/audio/CharacterSelection.wav";
   description = "SoundLoop";
   preload 	   = true;
};
new AudioProfile(rupes)
{
   filename	   = "~/data/audio/RupesCity.wav";
   description = "SoundLoop";
   preload 	   = true;
};
new AudioProfile(desert)
{
   filename	   = "~/data/audio/DesertMap.wav";
   description = "SoundLoop";
   preload 	   = true;
};
new AudioProfile(fear)
{
   filename	   = "~/data/audio/FearMap.wav";
   description = "SoundLoop";
   preload 	   = true;
};
new AudioProfile(pirate)
{
   filename	   = "~/data/audio/PirateMap.wav";
   description = "SoundLoop";
   preload 	   = true;
};