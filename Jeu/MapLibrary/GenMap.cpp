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

//interdire l'eau sur les cases du diamètre ? ou deplacement si impossible ?
std::pair<std::pair<int, int>, std::pair<int, int>> GenMap::placePlayer(std::list<int> unwanted) {
	std::pair<int, int> j1, j2;
	for (int i = 0; i < _sizeX * _sizeX; i++) {
		int id = 1;
		std::list<int>::iterator tileJ1 = std::find(unwanted.begin(), unwanted.end(), _mapCreated[id]);
		std::list<int>::iterator tileJ2 = std::find(unwanted.begin(), unwanted.end(), _mapCreated[_sizeX * _sizeX - 1 - id]);
		if (tileJ1 != unwanted.end() && tileJ2 != unwanted.end()) {
			//Use of var ? NO http://weknowgifs.com/wp-content/uploads/2013/04/fuck-this-thing-cat.gif
			j1 = std::make_pair(id / _sizeX, id % _sizeX);
			j2 = std::make_pair((_sizeX * _sizeX - 1 - id) / _sizeX, (_sizeX * _sizeX - 1 - id) % _sizeX);
			break;
		}
	}
	std::pair<std::pair<int, int>, std::pair<int, int>> res(j1, j2);
	return res;
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


/*int main() {
	GenMap g(5, 5);
	std::string s = g.generate(5);
	std::cout << s << std::endl;
}*/