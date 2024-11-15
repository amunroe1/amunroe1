#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "stack.h"


STACK *search;

STACK * initStack(STACK *pstack, int size){

   pstack = (STACK*)malloc(sizeof(STACK));
   pstack -> data = (int*)malloc(size*sizeof(int));
   pstack -> top = -1;
   pstack -> maxSize = size;

   return pstack;

}

void push(STACK* pstack, int value) {
    if (pstack->top == pstack->maxSize - 1) {
        fprintf(stdout, "Stack overflow\n");
        return;
    }

    pstack->top = pstack->top + 1;
    pstack->data[pstack->top] = value;
}


void clearStack(STACK* pstack) {
    free(pstack->data); 
    free(pstack);       
}

int pop(STACK* pstack) {
    if (pstack->top == -1) {
        fprintf(stdout, "Stack underflow\n");
        return -1;
    }

    int temp = pstack->data[pstack->top];
    pstack->top = pstack->top - 1;
    return temp;
}

int peek(STACK* pstack) {
    if (pstack->top == -1) {
        printf("Stack is empty\n");
        return -1;
    }
    for (int i = pstack->top; i>-1; i--){
      fprintf(stderr, "%d\n", pstack->data[i]);
    }
    return pstack->data[pstack->top];
}


