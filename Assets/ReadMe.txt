* Menu System
	- Designed to be flexible and should be adaptable to whatever game or application that you're making
	- Essentially we're just laying out some UI elements with buttons that help us jump from one menu to the next
	- When we enable UI we disable the previous one and though some screens may look similar to one another or just have some of the same functionality 
	  we will always have them set up as completely separate objects
	- Create a custom based class menu class that will give all of the menus a certain level of uniformity
	- Create a manager class called menu manager to manage these menus
	
* Canvas 
	- In order to create UI elements, we need a canvas objects
	- We're going to structure the UI system so that each screen or menu has its own separate canvas and that will help us make each UI screen 
	  self-contained as will be turning them on and off
	- Render Mode: By default, the canvas comes in using render mode screen space overlay which shows the graphics always on top of everything else 
	  in the scene
	- Canvas scaler: will help us keep our layout intact as the target build changes from resolution or resolution. It works best if you choose a 
	  reference resolution that will be closest to your final target build
	- Keeping important stuff in the center of the screen is helpful when dealing with multiple target resolutions
	
* Main Menu 
	- Start by dividing the screen into header and body in order to arrange texts and buttons more effectively 
	- The header area, which holds a text or logo image, will not be clickable and we need to block clicks and touches in this area
	- The body area will hold the main buttons or UI's. This area must be interactable
	
* UI 
	- Prevent UI from being interactable or be able to pass raycasts by creating a CANVAS GROUP component, attach it to the selected UI and remove
	  checkbox under INTERACTABLE & BLOCK RAYCASTS. To make an UI clickable, use the CANVAS GROUP with the checkboxes selected
	  
* Arrangment 
	 - Vertical layout group component
	  
