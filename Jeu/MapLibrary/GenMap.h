#pragma once
#include <sstream>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <vector>
#include <algorithm>

#define FIRST_ASCII_LETTER 'A'

#ifdef WANTDLLEXP
	#define DLL _declspec(dllexport)
	#define EXTERNC extern "C"
#else
	#define DLL
	#define EXTERNC
#endif

class DLL GenMap
{


#ifdef UNIT_TEST
		friend UnitTest;
#endif

private:
	int _sizeX;
	int _sizeY;
	inline char toCharacter(int i);
public:
	GenMap();
	GenMap(int sizeX, int sizeY);
	~GenMap();
	int getX();
	int getY();
	int* generate(int nbElementDiff);
	std::pair<std::pair<int, int>, std::pair<int, int>> placePlayer();
	std::vector<std::pair<int, int>> possibleMoves(int posX, int posY, int nbMoves, std::vector<std::pair<int, int>> costMatrix, int** map);


};

EXTERNC DLL GenMap* GenMap_new();
EXTERNC DLL GenMap* GenMap_new(int sizeX, int sizeY);
EXTERNC DLL void GenMap_delete(GenMap* genmap);
EXTERNC DLL int* GenMap_generate(GenMap* genmap, int nbElementDiff);
