#ifndef  _LIST_H_INCLUDE_
#define _LIST_H_INCLUDE_

typedef struct ListObj* List;

List newList(void);
void freeList(List* pL);


int length(List L);
int frontValue(List L);
int backValue(List L);
int getValue(Node N);
int equals(List A, List B);

void clear(List L);
void getFront(List L);
void getBack(List L);
void getPrevNode(Node N);
void getNextNode(Node N);
void prepend(List L, int data);
void append(List L, int data);
void insertBefore(List L, Node N, int data);
void insertAfter(List L, Node N, int data);
void deleteFront(List L);
void deleteBack(List L);

void printList(FILE* out, List L);
void erase(Node N);

#endif
