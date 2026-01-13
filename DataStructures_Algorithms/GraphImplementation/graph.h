#ifndef _graph_h
#define _graph_h 1
#include "data_structures.h"
#include "heap.h"
#include "stack.h"

pNODE * readGraph(FILE*, int, char*);
void printADJ(pNODE*);
pVERTEX * initGraph(pVERTEX*);
void dijkstra(HEAP*, pVERTEX*, int, pNODE*);
void dijsktraSP(HEAP *, pVERTEX *, int , int , pNODE* );

extern pVERTEX* V;
extern pNODE* ADJ;
extern int n, m;
#endif