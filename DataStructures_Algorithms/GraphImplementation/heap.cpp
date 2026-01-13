//Andrew Munroe
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <cmath>
#include "heap.h"


int V_size;



//Helper functions
int right(int i){
    return (2*i)+1;
}
int left (int i){
    return 2*i;
}
void swapPos(VERTEX* a, VERTEX* b) {
    VERTEX *temp = new VERTEX;
    //temp->pos = a->pos;
    temp->key = a->key;
    temp->index = a->index;

    //a->pos = b->pos;
    a->key = b->key;
    a->index = b->index;
    
    //b->pos = temp->pos;
    b->key = temp->key;
    b->index = temp->index;

    delete temp;
}


// Function to heapify a subtree with the root at given index i
void minHeapify(HEAP* Q, int i) {
    int smallest = i;           
    int l = left(i); 
    int r = right(i);
    int heapSize = Q->size;

    

    // If left child is smaller than root
    if (l < heapSize+1 &&  Q->H[l]->key < Q->H[smallest]->key ){

        smallest = l;
    }

    // If right child is smaller than the smallest so far
    if (r < heapSize+1 && Q->H[r]->key < Q->H[smallest]->key){
        smallest = r;
    }

    // If the smallest is not the root
    if (smallest != i) {
        // Swap the root with the smallest
        swapPos(Q->H[i], Q->H[smallest]);

        /*
        int temp1;
        temp1 = pHeap->H[i];
        pHeap->H[i] = pHeap->H[smallest];
        pHeap->H[smallest] = temp1;
        */
        

        // Recursively heapify the affected subtree
        minHeapify(Q, smallest);
    }
}

//Builds a heap in the pHeap object using array V
HEAP * initHeap(HEAP *Q, int n){

    Q = (pHEAP)calloc(1, sizeof(HEAP));

    Q->H = (pVERTEX*)calloc(n, sizeof(pVERTEX));
    for (int j=1; j<=n; j++){
        Q->H[j] = new VERTEX;
        Q->H[j]->color = WHITE;
        Q->H[j]->index =j;
        Q->H[j]->key = 0;
        Q->H[j]->pi = 0;
        Q->H[j]->pos = 0;
    }

    Q->capacity = n;
    Q->size = 0;
    Q->H = NULL;
    
    return Q;
}


//Function to insert element V[index] into Q
int heapInsert(HEAP *Q, pVERTEX *V, int index){
    
    if (!Q->H){
        Q->H = (pVERTEX*)calloc(n, sizeof(pVERTEX));
    }

    Q->size = Q->size +1;
    
    
    if (Q->size > Q->capacity) {
        fprintf(stderr, "Heap capacity isn't large enough\n");
        exit(0);
    }
    
    //Q->H[Q->size] = new VERTEX;
    
    
    Q->H[Q->size] = new VERTEX;
    
    Q->H[Q->size]->key = V[index]->key;
    Q->H[Q->size]->index = V[index]->index;
    Q->H[Q->size]->pos = Q->size;
    
    for (int i=(floor((Q->size)/2)); i>0; i--){
        minHeapify(Q, i);
    }
    
    return index;

}

//Function to decrease the key-value of an index
void decreaseKey(HEAP *Q,pVERTEX*V, int index, double x){
    //fprintf(stdout, "Going to change V[%d]: %lf -> %lf\n", index, V[index]->key, x);
    int temp;
    for (int i = 1; i<=Q->size; i++){
        if (Q->H[i]->index == index){
            temp = i;
        }
    }
    
    Q->H[temp]->key = x;
    //look for where H->index == index and modify the key





    for (int i=(floor((Q->size)/2)); i>0; i--){
        minHeapify(Q, i);
    }
}



int extractMin(pHEAP Q){
    int min = Q->H[1]->index;

    //Q->H[1]->pos = 0;

    swapPos(Q->H[1], Q->H[Q->size]);

    Q->size = Q->size - 1;
    
    minHeapify(Q, 1);

    return min;
}