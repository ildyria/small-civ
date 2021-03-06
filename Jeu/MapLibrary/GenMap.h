#pragma once
#include <sstream>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <vector>
#include <algorithm>
#include <list>
#include <map>
#include <tuple>
#include <iostream>



#ifdef WANTDLLEXP
	#define DLL _declspec(dllexport)
	#define EXTERNC extern "C"
#else
	#define DLL
	#define EXTERNC
#endif

#define FIRST_ASCII_LETTER 'A'
#define LOW_LIFE 1
#define BLOODSHED_POINTS 2 
#define DANGEROUS 2 
#define ENEMY 1


class DLL GenMap
{


#ifdef UNIT_TEST
		friend UnitTest;
#endif

private:
	int _sizeX;
	int _sizeY;
	std::vector<int> _mapCreated; // we keep it, so we are able to use it without having to pass it as parameter;
	inline char toCharacter(int i);
public:
	GenMap();
	GenMap(int sizeX, int sizeY);
	GenMap(int sizeX, int sizeY, std::vector<int> tilelist);
	~GenMap();
	int getX();
	int getY();
	std::vector<int> generate(int nbElementDiff);
	std::pair<std::pair<int, int>, std::pair<int, int>> placePlayer(std::list<int> unwanted);
	std::vector<std::pair<int, int>> possibleMoves(int posX, int posY, int nbMoves, std::vector<std::pair<int, int>> costMatrix, int** map);
	std::vector<int> bestMoves(int nbMovesWanted, std::tuple<int, int, int> u, std::list<int> movesPossibles, std::map<int, std::pair<int, int>> terrainData, std::map<int, int> opponents);
	
	struct comparator {
		bool operator() (std::pair<int, int> i, std::pair<int, int> j) { return i.second < j.second; }
	} comparator;

};


EXTERNC DLL GenMap* GenMap_new();
EXTERNC DLL GenMap* GenMap_new(int sizeX, int sizeY);
EXTERNC DLL GenMap* GenMap_new(int sizeX, int sizeY, std::vector<int> tilelist);
EXTERNC DLL void GenMap_delete(GenMap* genmap);
EXTERNC DLL std::vector<int> GenMap_generate(GenMap* genmap, int nbElementDiff);
EXTERNC DLL std::pair<std::pair<int, int>, std::pair<int, int>> GenMap_placePlayer(GenMap* genmap, std::list<int> unwanted);
EXTERNC DLL std::vector<int> GenMap_bestMoves(GenMap* genmap, int nbMovesWanted, std::tuple<int, int, int> u, std::list<int> movesPossibles, std::map<int, std::pair<int, int>> terrainData, std::map<int, int> opponents);
