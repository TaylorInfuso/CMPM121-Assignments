//Taylor Infuso

//This file is for creating a list object


#include<stdio.h>
#include<stdlib.h>
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

typpdedef NodeObj* List;

List newList(){
	List L = malloc(sizeof(List));
	L->head = NULL;
	L->tail = NULL;
	L->cursor = NULL;
	L->length = 0;
	return L;
}

void freeList(List* pL){
	//return to this later
	return NULL;
}

//returns the length
private int length(List L){
	return L->length;
}

//returns the head value. Return nothing if list is empty
private int frontValue(List L){
	if(L->length > 0)
		return L->head->index;
	else
		return null;
}

//returns the tail value. Returns nothing if list is empty
private int backValue(List L){
	if (L->length > 0)
		return L->tail->index;
	else
		return null;
}

//returns the index of the supplied node
private int getValue(Node N){
	if(N->index != null)
		return N->index;
	else
		return null;
}

//compared the elements in two lists
private int equals(List A, List B){
	A->cursor = A->head;
	B->cursor = B->head;
	if(A->length == B->length){
		for(int i = 0; i < A->length; i++){
			if(A->cursor->index == B->cursor->index){
				A->cursor = A->cursor->next;
				B->cursor = B->cursor->next;
				if(A->cursor->index == null && B->cursor->index == null)
					return true;
			}
		}
	}
	return false;
}

private void clear(List L){
	L->head = null;
	L->tail = null;
	L->cursor = null;
	L->length = 0;
}

private Node getFront(List L){
	if(L->length > 0)
		return L->head;
	else
		return null;
}

private Node getBack(List L){
	if(L->length > 0)
		return L->tail;
	else
		return null;
}

private Node getPrevNode(Node N){
	if(N->prev != null)
		return N->prev;
	else
		return null;
}

private Node getNextNode(Node N){
	if(N->next != null)
		return N->next;
	else
		return null;
}

private void prepend(List L, int data){
	if(L->length > 0){
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp.index = data;
		temp->next = L->head;
		L->head->back = temp;
		L->head = temp;
		L->length++;
	}
	else{
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp.index = data;
		L->head = temp;
		L->tail = temp;
		L->length++;
	}
}

private void append(List L, int data){
	if(L->length > 0){
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp.index = data;
		temp->prev = L.tail;
		L->tail->next = temp;
		temp = tail;
		L->length++;
	}
	else{
                NodeObj* temp = malloc(sizeof(NodeObj));
                temp.index = data;
		L->head = temp;
		L->tail = temp;
		L->length++;
	}
}

void deleteFront(List L){
	if(L->length > 1){
		L->head = L->head->next;
		erase(L->head->back);
		L->length--;
	}
	else(L->length == 0{
		erase(L->head);
		erase(L->tail);
		erase(L->cursor);
		L->length--;
	}
}

void deleteBack(List  L){
        if(L->length > 1){
                L->tail = L->tail->prev;
		erase(L->tail->next);
                L->length--;
        }
        else(L->length == 1){
                erase(L->head);
                erase(L->tail);
                erase(L->cursor);
                L->length--;
        }

}

void insertBefore(List L, Node N, int data){
	if(L->length > 0){
		if(N == L->head){
                        NodeObj* temp = malloc(sizeof(NodeObj));
                        temp.index = data;
			L->head->prev = temp;
			temp->next = L->head;
			temp = L->head;
			if(L->length == 1)
				tail = temp->next;
			L->length++;
		}
		else{
                        NodeObj* temp = malloc(sizeof(NodeObj));
                        temp.index = data;
			N->prev = temp;
			temp->next = N;
			L->length++;
		}
	}
}

void insertAfter(List L, Node N, int data){
	if(L->length > 0){
		if(N == L->tail){
                        NodeObj* temp = malloc(sizeof(NodeObj));
			temp.index = data;
                        L->tail->next = temp;
                        temp->prev = L->tail;
                        temp = L->tail;
			if(L->length == 1)
				head == temp->prev;
                        L->length++;
		}
		else{
			NodeObj* temp = malloc(sizeof(NodeObj));
                        temp.index = data;
			N->next = temp;
			temp->prev = N;
			L->length++;
		}
	}
}

void deleteFront(List L){
	if(L->length == 1){
		erase(L->head);
		erase(L->tail);
		erase(L->cursor);
		L->length--;
	}
	else(L->length > 1){
		L->head.next = L->head;
		erase(L->head.prev);
		L->length--;
	}
}

void deleteBack(List L){
	        if(L->length == 1){
                erase(L->head);
                erase(L->tail);
                erase(L->cursor);
                L->length--;
        }
        else(L->length > 1){
                L->tail->prev = L->tail;
                erase(L->tail->next);
                L->length--;
        }
}

void erase(Node N){
	if(N != NULL){
		free(N);
		N = NULL;
	}
}

void printList(FILE* out, List L){
	if (L->head == NULL){
		return;
	}
	else{
		L->cursor = L->head;
		for(int i = 0; i < L->length; i++){
			fprintf(out, "%d " , l->cursor->index);
			if(L->cursor->next != NULL)
				cursor = cursor->next;
	}
}
