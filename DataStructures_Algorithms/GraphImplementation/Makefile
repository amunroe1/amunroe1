EXEC = PJ3
CC = g++
CFLAGS = -c -Wall

$(EXEC) :main.o util.o stack.o heap.o graph.o
	$(CC) -o $(EXEC) main.o util.o heap.o graph.o stack.o

main.o :main.cpp main.h data_structures.h util.h stack.h heap.h graph.h
	$(CC) $(CFLAGS) main.cpp

util.o :util.cpp util.h data_structures.h
	$(CC) $(CFLAGS) util.cpp

stack.o :stack.cpp stack.h data_structures.h
	$(CC) $(CFLAGS) stack.cpp

heap.o :heap.cpp heap.h data_structures.h
	$(CC) $(CFLAGS) heap.cpp

graph.o :graph.cpp graph.h data_structures.h stack.h heap.h
	$(CC) $(CFLAGS) graph.cpp

clean :
	rm *.o $(EXEC)