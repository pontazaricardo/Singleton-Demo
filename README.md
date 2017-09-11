# Singleton-Demo

This is an example of a DMC that allows multiple users to access a centralized data management entity in order to insert new objects or update the existing ones. 

![demo](/images/demo.gif)

## Design patterns

For this project:
1. A [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) dessign pattern was used, so any users using it can access the same instance of the DMC. 
2. A [double-checked locking](https://en.wikipedia.org/wiki/Double-checked_locking) dessign pattern was implemented in the data structure holding the data in order to make the DMC multithread and thread-safe.

## Usage

Just import this project to Visual studio.

## Interfaces

Because the idea of the DMC is to manage objects that are somewhat unknown for it, we use the *IComparable* interface. From this, the clients implement their own *CompareTo* function, allowing us not only to compare but to sort objects of the same type.

## Data structure

We implemented a dictionary that looks for the type of objects the DMC gets, and:
1. If the type is already recorded in the dictionary, we use the *CompareTo* function to scan a list of objects of that type in the dictionary.
    1. If we do not find the object, we insert it.
	2. If we find the same object, we update.
2. If the type is a new type, we create the entry in the dictionary and create a list associated with it with the given object.

## Searches

Because per type we use a *List* structure. We can either scan the entire list or perform a smart search. Scanning the list will require *O(n)* time, so in order to speed the search, when inserting, we insert the object in a sorted way, so we can perform a *binary search* when looking for the object. This way allows us to reduce the search time to *O(ln n)*.


