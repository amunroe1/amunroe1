#ifndef _data_structures_h
#define _data_structures_h 1

enum COLOR{
    WHITE, GRAY, BLACK
};


typedef struct TAG_VERTEX{
    int     index;
    COLOR   color;
    double  key;
    int     pi;
    int     pos;
}VERTEX;


typedef VERTEX *pVERTEX;

typedef struct TAG_HEAP{

    int         capacity;
    int         size;
    pVERTEX    *H;

}HEAP;
typedef HEAP *pHEAP;

typedef struct TAG_NODE{
    int         index;
    int         u;
    int         v;
    double      w;
    TAG_NODE    *next;
}NODE;

typedef NODE *pNODE;




#endif