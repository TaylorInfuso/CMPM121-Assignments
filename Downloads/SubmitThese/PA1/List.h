//Name: Taylor Infuso
//Date: 27 January, 2019
//File: list.h
//Description:
//This is the header file for list.c. Not much here, but contains 
//initializations for functions and is later fed into ListClient.c.

#ifndef  _LIST_H_INCLUDE_
#define _LIST_H_INCLUDE_
//#ifndef _NODE_H_INCLUDE_
//#define _NODE_H_INCLUDE_

#include<stdio.h>
#include<stdlib.h>

typedef struct ListObj* List;
typedef struct NodeObj* Node;

List newList(void);
void freeList(List* pL);

//These are functions that return infromation regarding the contents of
//supplied lists and nodes.
int length(List L);
int frontValue(List L);
int backValue(List L);
int getValue(Node N);
int equals(List A, List B);

//These funcction manipulate lists and nodes by changing the contents or 
//order of nodes.
void clear(List L);
Node getFront(List L);
Node getBack(List L);
Node getPrevNode(Node N);
Node getNextNode(Node N);
void prepend(List L, int data);
void append(List L, int data);
void insertBefore(List L, Node N, int data);
void insertAfter(List L, Node N, int data);
void deleteFront(List L);
void deleteBack(List L);

//This function is simply used to print the contents of the lists.
void printList(FILE* out, List L);

//This is my own personal function. Just frees up memory and removes
//specified nodes.
void erase(Node N);

#endif
