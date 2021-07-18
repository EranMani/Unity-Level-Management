* Menu System
	- Designed to be flexible and should be adaptable to whatever game or application that you're making
	- Essentially we're just laying out some UI elements with buttons that help us jump from one menu to the next
	- When we enable UI we disable the previous one and though some screens may look similar to one another or just have some of the same functionality 
	  we will always have them set up as completely separate objects
	- Create a custom based class menu class that will give all of the menus a certain level of uniformity. In this class we will add
	  methods to run the desired scene or menu upon certain button click.
	- Create a manager class called menu manager to manage these menus. The manager will instantiate the menus at runtime, while
	  keeping only the main menu active and the others not.
	- Since each menu prefab has its own custom class, we can assign its methods to the buttons and these connections will be saved
	  with the prefab, since the class is part of the prefab as well (the canvas menu parent object). To keep the connections
	  within a prefab, the scripts and methods must sit inside the prefab.
	- Since each menu has a back button, we need to keep track of where we came from and we can do this by using a STACK, which
	  is a collection that stores its elements last in first out. We will always start with one element, which is the main menu.
	  If we click a button, a new menu gets added to the top of the stack and we disable the menus underneath. Clicking another button
	  will repeat the process - another menu gets added to the top of the stack and we disable any menus below it.
	  The stack always keeps track of where weve been at any time.
	  If we hit the back button on the currently active menu, we pop off the top menu and then figure out which menu is revealed
	  underneath, and then we enable whatever is left at the top of the stack.
	
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
	- There will be always one menu availble at a time, so the main menu will be active and the rest will be turned off
	
* UI 
	- Prevent UI from being interactable or be able to pass raycasts by creating a CANVAS GROUP component, attach it to the selected UI and remove
	  checkbox under INTERACTABLE & BLOCK RAYCASTS. To make an UI clickable, use the CANVAS GROUP with the checkboxes selected
	  
* Arrangment 
	 - Vertical layout group component
	 
* Buttons
	- Each button can have a normal (or default) color
	- Each button can change its color when highlighted
	- Each button can change its color when pressed
	- Each button has an onClick unity event assigned, which trigger something when the button is pressed. The onClick property allows to set  
      up more methods to invoke when the button is clicked.	We can call these methods from any object in the current scene or project itself.

* Switching menus
	- One option is to use the unity events in onClick to create an event of set-active to activate or deactivate menus. This 
	  option is not fit for scalable projects with great amount of screens
	  
