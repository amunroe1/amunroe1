#include <stdio.h>
#include <string.h>
#include "util.h"

int nextInstruction(char *Word,int * X1, int *X2)
{
    int  returnV;

    fscanf(stdin, "%s", Word);

    if (strcmp(Word, "Stop")==0)        return 1;
    if (strcmp(Word, "PrintADJ")==0)    return 1;
    if (strcmp(Word, "Test")==0)        return 1;



    if (strcmp(Word, "SinglePair")==0){
        returnV = fscanf(stdin, "%d %d", X1, X2);
        if (returnV == 2){
            return 1;
        }else{
            return 0;
        }
    }
    
    if (strcmp(Word, "SingleSource")==0){
        returnV = fscanf(stdin, "%d", X1);
        if (returnV == 1){
            return 1;
        }else{
            return 0;
        }
    }
    
    if (strcmp(Word, "PrintLength")==0){
        returnV = fscanf(stdin, "%d %d", X1, X2);
        if (returnV == 2){
            return 1;
        }
        else{
            return 0;
        }
    }

    
    if (strcmp(Word, "PrintPath")==0){
        returnV = fscanf(stdin, "%d %d", X1, X2);
        if (returnV == 2){
            return 1;
        }else{
            return 0;
        }
    }

    return 0;
}
