# Super Adventure

![Super Adventure Screenshot](Captura%20de%20pantalla%202024-06-20%20215613.png)

## Overview
**Super Adventure** is a decision-based RPG where you will have to perform great feats and complete certain tasks to improve. But be careful, as you will face fearsome creatures. Remember, you can always return to your sweet home.

## Some Technical Details
This game, although it may seem simple at first glance, integrates several quite interesting concepts that I wanted to implement and refine in C# and through the use of certain design patterns.

### Key Features
- **Decision-Based Gameplay**: Your choices determine the outcome of the game.
- **Dynamic Quests**: Complete various tasks and quests to progress.
- **Fearsome Creatures**: Encounter and battle various monsters.
- **Home Base**: Return to your sweet home anytime for respite.

### Technical Implementation
- **JSON Object Generation**: 
  - All game objects (weapons, characters, places, items, missions) are generated from a JSON file storing essential attributes.
  - This approach, using the Factory design pattern, allows for easy creation and instantiation of objects, simplifying the addition of new missions, places, items, and weapons without modifying the code. Only the JSON file needs to be updated.

- **INotifyPropertyChanged Interface**:
  - Used for updating the visual section of the game.
  - Similar to the Observer pattern in Java, this ensures that when a model class undergoes changes, the ViewModel classes get updated automatically. This decouples the model classes from the view classes, enhancing modularity and maintainability.

- **MessageBroker Class**:
  - Created to facilitate moving the combat logic into its own class.
  - Acts as a "Message Broker" in the Observer pattern, separating the publisher and subscribers.
  - This Singleton class handles messages to the UI, ensuring only one instance exists that all other objects in the program can share.
  - The UI subscribes to this single MessageBroker, eliminating the need to create individual events and subscriptions for each type of class.

## Installation
To run the application, follow these steps:

1. **Clone the repository**:
   ```sh
   git clone https://github.com/yourusername/super-adventure.git

