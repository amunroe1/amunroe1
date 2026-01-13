//Andrew Munroe
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "main.h"

int main(int argc, char **argv){
    FILE  *fp1, *fp2;
    int x1, x2, returnV, flag, temp2;
    HEAP *  Q;
    char  Word[100];
    char *graphType;

    fp1 = NULL;
    fp2 = NULL;
    Q = NULL;
    
    STACK * pStack;

    
    

    // Check commandline arguments
    if (argc < 4){
        fprintf(stderr, "Usage: %s <InputFile> <GraphType> <Flag>\n", argv[0]);
        exit(0);
    }

    flag  = atoi(argv[3]);
    graphType = argv[2];

    //Check for valid Graph Type
    if (strcmp("UndirectedGraph", graphType)!=0 && strcmp("DirectedGraph", graphType)!=0){
        fprintf(stderr, "Error: Graph type must be 'DirectedGraph' or 'UndirectedGraph'\n");
        exit(0);
        } 

   
    // File opened for reading...
    fp1 = fopen(argv[1], "r");

    // If File can't be opened...
    if (!fp1){
        fprintf(stderr, "Error: cannot open file %s\n", argv[1]);
        exit(0);
        }    

    
    readGraph(fp1, flag, graphType);

    //Q = initHeap(Q, n);
    V = initGraph(V);
    pStack = initStack(pStack, n);

    
    

    // close the input file
    fclose(fp1);

   
    // open the output file
    /*
    fp2 = fopen(argv[2], "w");
    if (!fp2){
        fprintf(stderr, "Error: cannot open file %s\n", argv[2]);
        exit(0);
    }
    */

    //Loop over instructions
    while (1){
        returnV = nextInstruction(Word, &x1, &x2);
        

        if (returnV == 0){
            fprintf(stderr, "Warning: Invalid instruction: %s\n", Word);
            continue;

        }

        if (strcmp(Word, "Stop")==0){
            
            if (fp2){
                fclose(fp2);
            }
            return 0;
        }

        if (strcmp(Word, "PrintADJ")==0){
            //fprintf(stderr, "Instruction: PrintADJ\n");
            printADJ(ADJ);
        }

        if (strcmp(Word, "SinglePair")==0){
            //fprintf(stderr, "Instruction: SinglePair %d %d\n", x1, x2);
            if (x1>=1 && x1<=n && x2>=1 && x2<=8){
                dijsktraSP(Q, V, x1, x2, ADJ);
                temp2 = x2;
            }
            else{
                fprintf(stderr, "Error: index '%d' is out of bounds. Index must be between 1 and %d\n", x1, n);
            }
        }

        if (strcmp(Word, "SingleSource")==0){
            //fprintf(stderr, "Instruction: SingleSource %d\n", x1);
            if (x1>=1 && x1<=n){
                dijkstra(Q, V, x1, ADJ);
                temp2 = 0;
            }
            else{
                fprintf(stderr, "Error: index '%d' is out of bounds. Index must be between 1 and %d\n", x1, n);
            }   
        }

        if (strcmp(Word, "PrintLength")==0){
            //fprintf(stderr, "Instruction: PrintLength %d %d\n", x1, x2);
            bool present1 = false;
            bool present2 = false;

            for (int j = 0; j <= search->top; j++) {
                if (search->data[j] == x1) {
                    present1 = true;
                    break;
                }
            }
            for (int j = 0; j <= search->top; j++) {
                if (search->data[j] == x2) {
                    present2 = true;
                    break;
                }
            }
            if (!present1 || !present2 || V[x2]->key == __DBL_MAX__){
                fprintf(stdout, "There is no path from %d to %d.\n", x1, x2);
                continue;
            }
            else{
                double length = 0;
                int j = x2;
                while (j != x1){
                    length = length + (V[j]->key - V[V[j]->pi]->key);
                    j = V[j]->pi;
                }
                
                fprintf(stdout, "The length of the shortest path from %d to %d is: %8.2lf\n", x1, x2, length);
            }
        }
                
        if (strcmp(Word, "PrintPath")==0){
            //fprintf(stderr, "Instruction: PrintPath %d %d\n", x1, x2);
            bool present1 = false;
            bool present2 = false;
            

            if (x1 == search->data[0]) {
                present1 = true;
            }

            if (temp2 == x2 || temp2 == 0){
                present2 = true;
            }

            if (!present1 || !present2){
                fprintf(stderr, "Invalid Instruction for 'PrintPath'\n");
                continue;
            }

            if (V[x2]->key != __DBL_MAX__){
                
                fprintf(stdout, "The shortest path from %d to %d is:\n",x1, x2);

                int j = x2;
                clearStack(pStack);
                pStack = initStack(pStack, n);
                while (j != x1){
                    push(pStack, V[j]->pi);
                    j = V[j]->pi;
                }
                
                int k = pop(pStack);
                
                fprintf(stdout, "[%d:%8.2lf]", V[k]->index, V[k]->key);
                
                for(int j=pStack->top; j>-1; j--){
                    fprintf(stdout, "-->[%d:%8.2lf]", V[pStack->data[j]]->index, V[pStack->data[j]]->key);
                }

                fprintf(stdout, "-->[%d:%8.2lf].\n", V[x2]->index, V[x2]->key);
            }
            else{
                fprintf(stdout, "There is no path from %d to %d.\n", x1, x2);
                continue;
            }
        }

        if (strcmp(Word, "Test")==0){
            //fprintf(stderr, "v.d v.pi\n");
            /*
            for (int j=1; j<=n; j++){
                if(V[j]->key == __DBL_MAX__){
                    fprintf(stderr, "No path to vertex %d\n", j);
                }
                else{
                    fprintf(stderr, "%4.2lf %d\n", V[j]->key, V[j]->pi);
                }
                
            }
            */
           pNODE current = NULL;   
            for (int j = 1; j <= 5; j++){
                current = ADJ[j];

                fprintf(stdout, "ADJ[%d]:", j);
                while (current){
                    fprintf(stdout, "-->[%d %d: %4.2lf]", current->u, current->v, current->w);
                    current = current->next;
            }
            fprintf(stdout, "\n");
    }





            }
        }
}



