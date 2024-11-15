//Andrew Munroe
#ifndef heap_h
#define heap_h 1
#include "util.h"
#include "main.h"
#include "data_structures.h"
#include "graph.h"
#include "main.h"


int heapInsert(HEAP*, pVERTEX*, int);
HEAP * initHeap(HEAP *, int);
void decreaseKey(HEAP*, pVERTEX*,  int, double);
int extractMin(HEAP*);



#endif
