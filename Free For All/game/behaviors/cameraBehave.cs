//-----------------------------------------------------------------------------
// Torque Game Builder
// Copyright (C) GarageGames.com, Inc.
// Behavior by Mike Lilligreen
//-----------------------------------------------------------------------------

if (!isObject(PlatformerCameraBehavior))
{
   %template = new BehaviorTemplate(PlatformerCameraBehavior);
   
   %template.friendlyName = "Platformer Camera";
   %template.behaviorType = "Camera";
   %template.description  = "Attach to a scene object for platformer camera controls";
   
   %template.addBehaviorField(target, "The object that the camera will track", object, "", t2dAnimatedSprite);
   %template.addBehaviorField(trackForce, "How the camera will follow target, 0 is rigid otherwise higher values mean less lag.", int, 0);
   %template.addBehaviorField(viewLimit, "Turn on camera view limit as defined by CLAMP world limit of object this behavior is attached to.", bool, false);
   %template.addBehaviorField(trackHeight, "Check to follow target in both X and Y, otherwise camera follows only in X.", bool, false);
   %template.addBehaviorField(facePlayer, "Camera will offset itself in direction player is facing if checked", bool, false);
   %template.addBehaviorField(xOffset, "Distance the target object should be from the center of the screen in X", int, 0);
   %template.addBehaviorField(yOffset, "Distance the target object should be from the center of the screen in Y", int, 0);
   %template.addBehaviorField(autoScroll, "Turn on auto scrolling of camera in X", bool, false);
   %template.addBehaviorField(trapObject, "Target object not allowed outside of scrolling camera view", bool, false);
   %template.addBehaviorField(scrollSpeed, "Velocity of auto scrolling", int, 5);
}

function PlatformerCameraBehavior::onAddToScene(%this, %scenegraph)
{
   // attaching the camera needs to be done via a schedule instead of directly here (TGB bug)
   %this.schedule(100, "attachCamera");
   
   // calls startAutoScroll function if box is checked in behavior
   if (%this.autoScroll)
      %this.schedule(1000, "startAutoScroll");
   
   // calls setViewLimit function if box is checked in behavior
   if (%this.viewLimit)
      %this.schedule(100, "setViewLimit");
}

function PlatformerCameraBehavior::attachCamera(%this)
{
   // mount the camera to the scene object and start the onUpdate function
   sceneWindow2D.mount(%this.owner, "0 0", %this.trackForce, false);
   FollowCamera.reset();
   %this.owner.enableUpdateCallback();
}

function PlatformerCameraBehavior::detachCamera(%this)
{
   sceneWindow2D.dismount();
   %this.owner.disableUpdateCallback();
}

function PlatformerCameraBehavior::onUpdate(%this)
{
   // if autoScroll and trapObject boxes are checked, confine the player within the camera view limits
   if (%this.autoScroll)
   {
      if (%this.trapObject)
      {
         %area = sceneWindow2D.getcurrentCameraArea();
         %this.target.setWorldLimit(CLAMP, %area);
      }
   }else
   {
      %newLoc = %this.target.getPositionX() + %this.xOffset;
      
      if (%this.trackHeight)
         %this.owner.setPositionY(%this.target.getPositionY() + %this.yOffset);

      if (%this.facePlayer)
      {
         // The $player.moveRight and moveLeft fields are taken from the playerClass.cs
         // file used in the basic platformer mechanics tutorial, you may have to change
         // this to get it to work with the PSK or your own movement code.
         %move = $player.moveRight - $player.moveLeft;
         if ((%move * %this.xOffset) < 0)
            %this.xOffset = -%this.xOffset;
            
         if (%this.xOffset > 0 || %this.xOffset < 0)
         {
            if ($player.moveRight || $player.moveLeft)
            {
               // This section creates a smooth acceleration of camera speed to prevent
               // a type of camera jitter during sudden left/right movements of the player
               $smoothCam++;
            
               if ($smoothCam > 10)
                  $smoothCam = 10;
               
               // the max %speed value should be larger than the player movement speed to
               // allow the camera to catch up to and pass the player
               %speed = $smoothCam * 5;
               %this.owner.moveTo(%newLoc, %this.owner.getPositionY(), %speed);
            }else
            {
               // if the player stops moving, so does the camera
               %this.owner.setAtRest();
               $smoothCam = 0;
            }
         }else
         {
            %this.owner.setPositionX(%this.target.getPositionX());
         }
      }else
      {
         %this.owner.setPositionX(%this.target.getPositionX() + %this.xOffset);
      }
   }
}

function PlatformerCameraBehavior::startAutoScroll(%this)
{
   // scrolling will start as soon as the player moves
   if ($player.moveRight || $player.moveLeft)
   {
      %this.owner.setLinearVelocityX(%this.scrollSpeed);
   }else
   {
      %this.schedule(100, "startAutoScroll");
   }
}

function PlatformerCameraBehavior::stopAutoScroll(%this)
{
   %this.owner.setAtRest();
}

function PlatformerCameraBehavior::setViewLimit(%this)
{
   // check the world limit mode of this object
   %limit = %this.owner.getWorldLimit();
   %mode = getWord(%limit, 0);
   
   // if CLAMP then set the camera view limit with the same values
   if (%mode $= "CLAMP")
   {
      %minX = getWord(%limit, 1);
      %minY = getWord(%limit, 2);
      %maxX = getWord(%limit, 3);
      %maxY = getWord(%limit, 4);
      
      sceneWindow2D.setViewLimitOn(%minX SPC %minY SPC %maxX SPC %maxY);
   }else
   {
      error("Wrong Limit Mode set - needs to be CLAMP");
   }
}