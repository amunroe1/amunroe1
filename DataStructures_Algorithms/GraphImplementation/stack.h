//Andrew Munroe
#ifndef stack_h
#define stack_h 1
#include "data_structures.h"
#include "graph.h"
#include "heap.h"


typedef struct TAG_STACK{
    int     *data;      // Pointer to store stack elements
    int     top;        // Index of the top element
    int     maxSize;    // Maximum size of the stack
}STACK;

STACK * initStack(STACK*, int);


int pop(STACK *);
void push(STACK* pstack, int val);
void clearStack(STACK *);
int peek(STACK*);



extern STACK* search;

#endif