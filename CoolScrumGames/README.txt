Steps to adding a new game:

1. Duplicate the GamePageTemplate file for each game on the website. Rename it to the new game.
2. Place the game's JavaScript (.js) file in a new folder under wwwroot/js/<GameFolder>. (It'll be easier if we keep the names consistent for each folder in every spot)
3. Place the game's images/assets in a new folder under  wwwroot/images/game_images/<NewGameFolder>
3. Reference it in the GameTemplate.cshtml "script src" field.
4. Under the onclick event for the start button, call the method in the game's JavaScript file that initilizes the game. 

**NOTE** 
_________
Some modifications to the game's JS will be necessary. The game's start/initilize functions will need to accept the universal game canvas as a parameter.
The game will need to draw its graphics to the this canvas that gets passed. Look at Jake's Asteroid game's startGame() function to see how we handled that.