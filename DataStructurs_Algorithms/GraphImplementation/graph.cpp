#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "graph.h"


pVERTEX* V;
pNODE* ADJ;

int n, m, i, u, v, tempv;
double w, tempw, newKey;

//Node insertion helper func

void listInsert(pNODE *ADJ, int index, int u, int v, double w){
    NODE *pNODE;
    pNODE = NULL;
    //allocate memory for newNode
    NODE *newNode = new NODE;
    newNode->index = index;
    newNode->u = u;
    newNode->v = v;
    newNode->w = w;
    newNode->next = NULL;

    if (ADJ[u] == NULL){
        ADJ[u] = newNode;
    }

    else{
        pNODE = ADJ[u];
        ADJ[u] = newNode;
        ADJ[u] -> next = pNODE;
    }
}

void listAppend(pNODE *ADJ, int index, int u, int v, double w){
    NODE *pNODE;
    pNODE = NULL;
    //allocate memory for newNode
    NODE *newNode = new NODE;
    newNode->index = index;
    newNode->u = u;
    newNode->v = v;
    newNode->w = w;
    newNode->next = NULL;

    if(ADJ[u] == NULL){
        ADJ[u] = newNode;
    }

    else{
        int condition = true;
        pNODE = ADJ[u];
        while (condition == true){
            if (pNODE->next){
                pNODE = pNODE->next;
            }
            else{
                pNODE->next = newNode;
                condition = false;
            }
        }
    }
}

void initSS(pVERTEX* V, int source){
    for (int j = 1; j <= n; j++){
        V[j]->key = __DBL_MAX__;
        V[j]->pi = 0;
        V[j]->color = WHITE;
    }
    V[source]->key = 0;
    V[source]->color = GRAY;
}

double Relax(HEAP *Q, pVERTEX* V, int u, int v, double w){
    if (V[v]->key > (V[u]->key + w)){

        V[v]->key = (V[u]->key + w);
        V[v]->pi = u;
        
    }
    return (V[u]->key + w);
}

//Function to read graph from file
pNODE * readGraph(FILE *fp, int flag, char* graphType){
    // read first line to get "n" vertices and "m" edges
    
    fscanf(fp,"%d %d", &n, &m);
    V = (VERTEX **)malloc(n * sizeof(pVERTEX));
    ADJ = (NODE**)malloc(n * sizeof(pNODE));
    //fprintf(stdout, "m=%d n=%d\n", m, n);
    search = initStack(search, n);

     
    while (fscanf(fp, "%d %d %d %lf", &i, &u, &v, &w) == 4){
        if (flag == 1){
            listAppend(ADJ, i, u, v, w);
            if (strcmp("UndirectedGraph", graphType)==0){
                listAppend(ADJ, v, v, u, w);
                }
        }
        else if (flag == 0){
            listInsert(ADJ, i , u, v, w);
            if (strcmp("UndirectedGraph", graphType)==0){
                listInsert(ADJ, v, v, u, w);
            }
        }
    }
    
    return ADJ;
}

pVERTEX * initGraph(pVERTEX *){
    for (int j=1; j< n+1; j++){
        //fprintf(stdout, "Working: ADJ[%d]->index = %d\n", j, ADJ[j]->index);

        V[j] = (VERTEX *)malloc(sizeof(VERTEX));
        V[j]->color = WHITE;
        V[j]->index = j;
        V[j]->key = 0;
        V[j]->pi = 0;
        V[j]->pos = 0;

        
    }
    //fprintf(stdout, "V[1]->color = %d\nV[1]->index = %d\nV[j]->key = %4.2lf\nV[j]->pi = %d\nV[j]->pos = %d\n", V[1]->color, V[1]->index, V[1]->key, V[1]->pi, V[1]->pos);
    return V;
}

void printADJ(pNODE * ADJ){
    pNODE current = NULL;   
    for (int j = 1; j <= n; j++){
        current = ADJ[j];

        fprintf(stdout, "ADJ[%d]:", j);
        while (current){
            fprintf(stdout, "-->[%d %d: %4.2lf]", current->u, current->v, current->w);
            current = current->next;
        }
        fprintf(stdout, "\n");
    }
}

void dijkstra(HEAP *Q,  pVERTEX *G, int source, pNODE *ADJ){
    

    initSS(G, source);
    Q = NULL;
    Q = initHeap(Q, n);
    

    clearStack(search);
    search = initStack(search, n);
    
    heapInsert(Q, G, source);
    
    
 
        //fprintf(stderr, "Inserted node %d\n", j);
    
    while (Q->size > 0)
    {
        u = extractMin(Q);
        //fprintf(stderr, "min extracted: %d\n", u);

        push(search, u);

        pNODE current = ADJ[u];
        while (current){
            if (G[current->v]->color == WHITE){

                G[current->v]->color = GRAY;
                tempv = current -> v;
                tempw = current -> w;
                Relax(Q, G, u, tempv, tempw);
                heapInsert(Q, G, current->v);

            }
            else if (G[current->v]->color == GRAY){

                tempv = current -> v;
                tempw = current -> w;
                newKey = Relax(Q, G, u, tempv, tempw);
                decreaseKey(Q, G, tempv, newKey);
            }
            current = current->next;
        }
        G[u]->color = BLACK;
    }
}

void dijsktraSP(HEAP *Q, pVERTEX *G, int source, int dest, pNODE* ADJ){
    initSS(G, source);

    Q = NULL;
    Q = initHeap(Q, n);

    clearStack(search);
    search = initStack(search, n);

    heapInsert(Q, G, source);

    while (Q->size > 0)
    {
        u = extractMin(Q);
        push(search, u);

        if (u==dest){
            Q->size = 0;
            continue;
        }

        pNODE current = ADJ[u];
        while (current){
            if (G[current->v]->color == WHITE){

                G[current->v]->color = GRAY;
                tempv = current -> v;
                tempw = current -> w;
                Relax(Q, G, u, tempv, tempw);
                heapInsert(Q, G, current->v);

            }
            else if(G[current->v]->color == GRAY){

                tempv = current -> v;
                tempw = current -> w;
                newKey = Relax(Q, G, u, tempv, tempw);
                decreaseKey(Q, G, tempv, newKey);
            }
            current = current->next;
        }
        G[u]->color = BLACK;
    }
}

