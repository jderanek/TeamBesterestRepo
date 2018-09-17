UI ASSETS YAAAA
----------------------------------------------------------------------------------------------

Contents:
----------------------------------------------------------------------------------------------
Clock Folder
	
	Assets for the clock at the top center of the screen.
	
	Buttons beneath the clock itself have .pngs for when they are clicked on, so they can
	turn a darker grey for a moment as a sort of feedback that the player is interacting
	with the button. 
	
	Fast Forward has two settings, labled FF1 and FF2. Both take up the same button space.
	The Play button similarly shares a space where the Pause button is, as the player
	toggles whether or not the game is playing in real time. 
	
	The hands are saved individually as well, so that the game can rotate them appropriately 
	when time is passing. The minute hand shouldn't move much unless the game is running
	in real time, in which case the minute hand will move as a kind of indicator as to
	when an hour is about to pass.
----------------------------------------------------------------------------------------------
Pause Menu Folder

	MenuClipboard.png is the base pause menu of the game that pops up in the very center
	of the screen when the menu button is clicked. 
	
	ManuBox1.png and MenuBox2.png are both ideas I had for what the menu selections
	could look like in game. I personally think MenuBox2.png would be easier to understand,
	but I'll leave that decision up to what looks best when paired with an actual font.
	
	The example in the main folder that shows this menu demonstrates a basic darkenening 
	of the rest of the screen. I believe this should happen when the pause menu is opened,
	as it makes it clearer that the game is, in fact, paused.
----------------------------------------------------------------------------------------------
Side Panel Folder

	MonsterBox_Closed.png is the monster box that the player would see in either the
	applications menu or the monster list. This asset is for when the player has not
	clicked on it yet.

	MonsterBox_Open.png is the monster box when it has been clicked on. 

	Please note that in both cases, I do not have access to whatever image we're going
	to use for the monsters we have available, so you may need to either send those to
	me to edit onto the photos for individual renders, or you can edit them yourself
	in the PSD. To do so:
		>Open the PSD
		>Drop the image (with transparency hopefully) into the PSD, ideally with it's 
		own layer
		>Move that image layer into the MonsterBox group (looks like a folder icon)
		>Make sure the image is under Paperclip, but above MonsterBoxOpened.
		>Crop the MonsterBox appropriately
		>Save As "[monster_name]_MonsterBox.png"
----------------------------------------------------------------------------------------------
Top Bar Folder
	
	Each tab has it's own asset for when they are selected. For example, ApplicantsTab.png
	is for when no tabs are currently selected, or when the Applicants menu is open. 
	ApplicantsTabDarkened.png is for when any other tab is currently opened. This follows
	for every other tab.

	There are two types of Menu Tabs present as well. There's MenuTabOld.png, which is the
	original Sandwich icon that I came up with, and MenuTab.png, which is the newest
	iteration, with a Gear symbol. I was told the Sandwich icon could be confusing
	(especially since this isn't a mobile game), so the Gears may be more visually clear.
	Just in case, I included them both anyway, but I believe we want to stick with the
	Gear symbol for the time being, hence why I kept it as MenuTab.png
----------------------------------------------------------------------------------------------

If you have any questions about where to put any of these assets, how to implement them, or if
more are needed, please either refer to the included examples or @ me on the discord channel 
and I'll hop to it!

NOTE: I need to briefly discuss the interview screen on Tuesday. I believe the UI elements for
that screen will be VERY easy to produce (since it's mostly art of the monster, and text) but
I don't recall the exact layout off the top of my head.
