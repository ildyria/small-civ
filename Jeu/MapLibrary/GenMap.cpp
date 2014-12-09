#include "GenMap.h"


GenMap::GenMap()
{
}

GenMap::GenMap(int sizeX, int sizeY) : _sizeX(sizeX), _sizeY(sizeY)
{
}

int GenMap::getX(){
	return _sizeX;
}
int GenMap::getY(){
	return _sizeX;
}


int* GenMap::generate(int nbElementDiff) {
	// should check if you can go from point a to point b
	int* result = new int[_sizeX*_sizeY];
	int character;
	srand(time(NULL));
	std::vector<int> nbelem(nbElementDiff);
	for (int i = 0; i < nbElementDiff; i++){
		nbelem[i] = _sizeX * _sizeY / nbElementDiff;
	}
		
	for (int i = 0; i < _sizeX * _sizeY; i++){
		bool unAssoc = true;
		//should report to the next instead, should be less time consuming
		int z = rand() % nbElementDiff;
		while (unAssoc) {
			if (nbelem[z] != 0){
				character = z;
				nbelem[z]--;
				unAssoc = false;
			}
			else {
				z = (z + 1) % nbElementDiff;
			}
		}
		result[i] = character;
	}
	_mapCreated = result;
	return result;
}

// Is not actually the farthest.
std::pair<std::pair<int, int>, std::pair<int, int>> GenMap::placePlayer(std::list<int> unwanted) {
	std::pair<int, int> j1, j2;
	bool j1Found = false, j2Found = false;
	for (int i = 0; i < _sizeX * _sizeX && !j1Found && !j2Found; i++) {
		std::list<int>::iterator tileJ1 = std::find(unwanted.begin(), unwanted.end(), _mapCreated[i]);
		std::list<int>::iterator tileJ2 = std::find(unwanted.begin(), unwanted.end(), _mapCreated[_sizeX * _sizeX - 1 - i]);
		if (tileJ1 == unwanted.end()) {
			j1 = std::make_pair(i / _sizeX, i % _sizeX);
			j1Found = true;
		}
		if (tileJ2 == unwanted.end()){
			//Use of var ? NO http://weknowgifs.com/wp-content/uploads/2013/04/fuck-this-thing-cat.gif
			j2 = std::make_pair((_sizeX * _sizeX - 1 - i) / _sizeX, (_sizeX * _sizeX - 1 - i) % _sizeX);
			j2Found = true;
		}
	}
	std::pair<std::pair<int, int>, std::pair<int, int>> res(j1, j2);
	return res;
}

std::list<int> GenMap::bestMoves(int nbMovesWanted, std::tuple<int, int, int> u, std::list<int> movesPossibles, std::map<int, std::pair<int, int>> terrainData, std::list<std::tuple<int, int, int>> opponents) {
	std::map<int, int> possibilities;

	for each (int move in movesPossibles)
	{
		if (terrainData.find(_mapCreated[move]) != terrainData.end()) {
			possibilities[move] = terrainData[_mapCreated[move]].second;
		}
		else {
			possibilities[move] = 1;
		}
		
	}
	
	//delete moves where opponent has more life than me => probability that i will die
	//Could be enhance to delete move that are to close
	//find low life opponent
	for each (std::tuple<int, int, int> adv in opponents)
	{
		int pos = std::get<0>(adv) * _sizeX + std::get<1>(adv);
		if (possibilities.find(pos) != possibilities.end()) {
			if (std::get<2>(adv) > std::get<2>(u)) {
				possibilities[pos] -= DANGEROUS;
			}
			else if (std::get<2>(adv) == LOW_LIFE) {
				possibilities[pos] += BLOODSHED_POINTS;
			}
			else {
				possibilities[pos] -= ENEMY;
			}
		}
	}
	std::list<int> result;
	//for (int i = 0 ; i < nbMovesWanted ; i++)
	for (int i = 0; i < 3; i++)
	{
		std::map<int, int>::iterator it = std::max_element(possibilities.begin(), possibilities.end());
		if (it != possibilities.end()) {
			result.push_back((*it).first);
			possibilities.erase(it);
		}
		else {
			break;
		}
	}
	//return movesPossibles;
	return result;
}

char GenMap::toCharacter(int i) {
	return FIRST_ASCII_LETTER + i;
}

GenMap::~GenMap()
{
}

//can be solved with a matrix as parameter [type] = movecost :: pointgained
//use a damn pointer bitch !
std::vector<std::pair<int, int>> GenMap::possibleMoves(int posX, int posY, int nbMoves, std::vector<std::pair<int, int>> costMatrix, int** map) {
	std::vector<std::pair<int, int>> result;
	for (int i = -1; i < 2; i++) {
		for (int j = -1; j < 2; j++) {
			std::pair<int, int> v = costMatrix[map[posX + i][posY + j]];
			if (v.first <= nbMoves) {
				//do again possibleMoves(posX + i, posY + j, nbMoves - v.first(), costMatrix, map)
				result.push_back(std::pair<int, int>(posX + i, posY + j));
			}
		}
	}
	return result;
}

GenMap* GenMap_new() { return new GenMap(); }
GenMap* GenMap_new(int sizeX, int sizeY) { return new GenMap(sizeX, sizeY); }
void GenMap_delete(GenMap* genmap) { delete genmap; }
int* GenMap_generate(GenMap* genmap, int nbElementDiff) { return genmap->generate(nbElementDiff); }
std::pair<std::pair<int, int>, std::pair<int, int>> GenMap_placePlayer(GenMap* genmap, std::list<int> unwanted) { return genmap->placePlayer(unwanted); }
std::list<int> GenMap_bestMoves(GenMap* genmap, int nbMovesWanted, std::tuple<int, int, int> u, std::list<int> movesPossibles, std::map<int, std::pair<int, int>> terrainData, std::list<std::tuple<int, int, int>> opponents) {
	return genmap->bestMoves(nbMovesWanted, u, movesPossibles, terrainData, opponents);
}

/*int main() {
	GenMap g(5, 5);
	std::string s = g.generate(5);
	std::cout << s << std::endl;
}*/