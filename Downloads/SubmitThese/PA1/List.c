//Name: Taylor Infuso
//Date: 27 January, 2019
//File: List.c
//Description:
//This file is for creating list and node objects and supplying 
//functions for alterations of their contents like addition and
//removal of elements.



#include <stdio.h>
#include <stdlib.h>
#include "List.h" 
 
typedef struct NodeObj{
	int index;
	struct NodeObj* next;
	struct NodeObj* back;
}NodeObj;

typedef NodeObj* Node;

Node newNode(int data){
	struct NodeObj* Node = malloc(sizeof(data));
	Node->index = data;
	Node->next = NULL;
	Node->back = NULL;
	return Node;
}

typedef struct ListObj{
	struct NodeObj* head;
	struct NodeObj* tail;
	struct NodeObj* cursor;
	int length;
}ListObj;

typedef ListObj* List;

List newList(){
	List L = malloc(sizeof(List));
	L->head = NULL;
	L->tail = NULL;
	L->cursor = NULL;
	L->length = 0;
	return L;
}

//empties the list
void freeList(List *pL){
	List temp = *pL;
	while(temp->cursor->next != NULL){
		temp->cursor = temp->cursor->next;
		erase(temp->cursor->back);
		temp->length--;
	}
	erase(temp->cursor);
	temp->length--;
	*pL = temp;
}

//returns the length
int length(List L){
	return L->length;
}

//returns the head value. Return nothing if list is empty
int frontValue(List L){
	if(L->length > 0)
		return L->head->index;
	else
		return 0;
}

//returns the tail value. Returns nothing if list is empty
int backValue(List L){
	if (L->length > 0)
		return L->tail->index;
	else
		return 0;
}

//returns the index of the supplied node
int getValue(Node N){
	if(N != NULL)
		return N->index;
	else
		return 0;
}

//compared the elements in two lists
int equals(List A, List B){
	A->cursor = A->head;
	B->cursor = B->head;
	if(A->length == B->length){
		for(int i = 0; i < A->length; i++){
			if(A->cursor->index == B->cursor->index){
				if(A->cursor->next != NULL && B->cursor->next != NULL){
					A->cursor = A->cursor->next;
	                                B->cursor = B->cursor->next;
				}
				else
					return 1;
			}
		}
	}
	return 0;
}

//clears the list out
void clear(List L){
	L->head = NULL;
	L->tail = NULL;
	L->cursor = NULL;
	L->length = 0;
}

//returns the front element of the list
Node getFront(List L){
	if(L->length > 0)
		return L->head;
	else
		return 0;
}

//returns the last element of the list
Node getBack(List L){
	if(L->length > 0)
		return L->tail;
	else
		return 0;
}

//returns the element before the supplied one
Node getPrevNode(Node N){
	if(N->back != NULL)
		return N->back;
	else
		return 0;
}

//returns the element after the supplied one
Node getNextNode(Node N){
	if(N->next != NULL)
		return N->next;
	else
		return 0;
}

//adds a new element to the list at the head. If the length of the list
//is 0, this element becomes the head and the tail.
void prepend(List L, int data){
	if(L->length > 0){
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp->index = data;
		temp->next = L->head;
		L->head->back = temp;
		L->head = temp;
		L->length++;
	}
	else{
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp->index = data;
		L->head = temp;
		L->tail = temp;
		L->length++;
	}
}

//adds a new element to the end of the list, making it the tail. If the
//list is empty, the new element will become the head and tail.
void append(List L, int data){
	if(L->length > 0){
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp->index = data;
		temp->back = L->tail;
		L->tail->next = temp;
		L->tail = temp;
		L->length++;
	}
	else{
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp->index = data;
		L->head = temp;
		L->tail = temp;
		L->length++;
	}
}

//deletes the head of the list
void deleteFront(List L){
	if(L->length > 1){
		L->head = L->head->next;
		erase(L->head->back);
		L->length--;
	}
}

//deletes the tail of the list
void deleteBack(List L){
        if(L->length > 1){
                L->tail = L->tail->back;
		erase(L->tail->next);
                L->length--;
        }
}

//inserts a new element before the supplied one. Becomes the head if
//the new node goes before the head.
void insertBefore(List L, Node N, int data){
	if(L->length > 0){
		if(N == L->head){
                        NodeObj* temp = malloc(sizeof(NodeObj));
                        temp->index = data;
			L->head->back = temp;
			temp->next = L->head;
			L->head = temp;
			if(L->length == 1)
				L->tail = temp->next;
			L->length++;
		}
		else{
                        NodeObj* temp = malloc(sizeof(NodeObj));
                        temp->index = data;
			temp->next = N;
			temp->back = N->back;
			N->back = temp;
			temp->back->next = temp;
			L->length++;
		}
	}
}

//inserts a new element after the supplied node. If the supplied node is
//the tail, the new node becomes the tail
void insertAfter(List L, Node N, int data){
	if(L->length > 0){
		if(N == L->tail){
                        NodeObj* temp = malloc(sizeof(NodeObj));
			temp->index = data;
                        L->tail->next = temp;
                        temp->back = L->tail;
                        L->tail = temp;
			if(L->length == 1)
				L->head = temp->back;
                        L->length++;
		}
		else{
			NodeObj* temp = malloc(sizeof(NodeObj));
                        temp->index = data;
			temp->back = N;
			temp->next = N->next;
			N->next = temp;
			temp->next->back = temp;
			L->length++;
		}
	}
}

//erases a supplied node
void erase(Node N){
	if(N != NULL){
		free(N);
		N = NULL;
	}
}

//prints the entire list assuming there are elements
void printList(FILE* out, List L){
	if (L->head == NULL){
		return;
	}
	else{
		L->cursor = L->head;
		for(int i = 0; i < L->length; i++){
			fprintf(out, "%d " , L->cursor->index);
			if(L->cursor->next != NULL)
				L->cursor = L->cursor->next;
		}
	}
}
