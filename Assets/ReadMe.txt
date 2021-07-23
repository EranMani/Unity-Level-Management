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

* Singleton Pattern
	- The singleton pattern will provide global access to the single instnace of our class, and this will eliminate the calls for
	  methods such as FindObjectOfType
	- The singleton pattern will ensure there is only one instnace of the class in the scene. The singleton will be a class and will 
	  have only one representative object in the game
	- The most common way to implement a singleton is with static properties
	- The instance of the singleton will be called using a property with a GET which points to the private instance of the manager
	- The call to a manager will look like this: Manager.Instance
	- The basic structure for a singleton is:
		* Use static field and public property to refer to global instance
		* In Awake method, either self-destruct or assign self a global instance of the manager
	- Manager scripts, especially ones that persists across more then one scene, are better suited for this pattern

* Generics
	- Instead of defining a new method or class for each data type, we can define a method or class using a generic type
	- Generic type method will look like this:
		* public bool Compare<T>(T a, T b) { return a.Equals(b);  }
	- We define the method using a standalone data type shown as a capital T and with the angle brackets. We substitue the data
	  type for T and then pass in the parameters
	- This allows to write one method or class using generics and then use that for a whole variety of data types,
	  rather than having a separate snippet of code for each thing that you want to apply that to
	- Generic data type can be limited by using the "where" keyword after the definition of the generic method
		* must be a value type -> where T: struct
		* must be a reference type -> where T: class
		* must have a public parameterless constructor -> where T: new()
		* must derive from specified base class -> where T: <base class>
		* must implement specified interface -> where T: <interface>
	- GetComponent() method for example is a generic method
		* T GetComponent<T>() { }

* Menu Subclasses 
	- The goal is to get the menu set up as a singleton, but singleton permits only one instance of each class
	- To fix the issue, the menus can be derived from a common base class and then give each menu screen its own unique subclass
	- This will give the ability to assign unique methods to its corresponding menu subclass
	- We don't instantiate any objects directly from the base Menu class when making a new menu screen
	- The MENU class will be modified to be an abstract, which means that the class is not meant to be a standalone object
	- To create a new menu, simply create a new subclass and use that subclass. New menu subclasses will simply use the menu
	  base class as a template
	- We can keep methods in our menu based class but they need to apply to any menu subclasses that derive from it.
	  Anything else probably doesn't belong here.
	- For example, the back button pressed is the only method which all menus have in common
	- Anything commonly defined in the menu base class also applies to the subclasses (Inheritance)
	- VIRTUAL Keyword: allows to make a special version of an back pressed functionality for any of the menu subclasses
	- OVERRIDE Keyword: allows to change the method behaviour from the base class. The base class method must contain VIRTUAL
	  keyword in order to override the method in the subclass
	
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
	- Unity event system: responsible for processing and handling events in a Unity scene. A scene should only contain one EventSystem.
	                      the event system is needed in order for UI elements to listen for the user input
* Transition
	- Set-up a fader which will apply to UI elements
	- Maskable Graphics: A base clas for both UI images and texts elements
	- Each graphic has a canvas renderer attached which renders a graphical UI object contained within a canvas
	- CrossFadeAlpha: tweens the alpha of the CanvasRenderer color associated with this Graphic

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


* Dont destroy on load
	- Loading a scene in unity by default destroys everything from the old scene and then loads up a whole new set of objects in the hierarchy
	- In order to make the menus active during game play they need to exist in more than just the main menu scene.
	  We need them to persist from the main menu
	- When marking an object with don't destroy on load, it's made persistent across scenes. When loading a new scene, the object
	  is not destroyed by default

* System
	- System.Reflection: a way to look at all the fields of the menu manager and figure out which ones were a menu prefabs
	                     This will allow to automatically generate the array of the menu fields.
						 Reflection works on a type, for example we need to get the system type menu manager
						 GetFields() - returns information about each field in the system type. GetFields() is used in conjunction
						               with "binding flags". These are special numeration that control how to search through the
									   reflection. We can chain binding flags, using bitwise operator OR (|) to search for the proper
									   menu fields
						 GetValue()- We can read what is stored in each field by using the GetValue(). We always run this method on a specific object
						 so we will need to pass in THIS which relates to the menu manager object for example. By using the THIS keyword
						 we say we are only interseted in the menu component, and so we need to cast this field value into a MENU type
						 to get the prefab of the specific field
