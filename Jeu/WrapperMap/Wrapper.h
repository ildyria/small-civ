#pragma once
#ifndef __WRAPPER__
#define __WRAPPER__
//le chemin de la lib
//#using <System.dll>
#include "../MapLibrary/GenMap.h"
//#pragma comment(lib, "../Debug/MapLibrary.lib")
#pragma comment(lib, "MapLibrary.lib")
#include <msclr/marshal.h>
#include <cliext\list>
#include <tuple>
using namespace System;
using namespace System::Collections::Generic;


using namespace msclr::interop;

namespace Wrapper {
	public ref class WrapperGenMap {
		private:
			GenMap* _genMap;
		public:
			WrapperGenMap(){ _genMap = GenMap_new(); }
			WrapperGenMap(int sizeX, int sizeY) { _genMap = GenMap_new(sizeX, sizeY); }
			WrapperGenMap(int sizeX, int sizeY, List<int>^ tilelist) {
				int* tilelistConv = (int*) malloc(sizeof(int) * tilelist->Count);
				for (int i = 0; i < tilelist->Count; i++)
				{
					tilelistConv[i] = tilelist[i];
				}
				_genMap = GenMap_new(sizeX, sizeY, tilelistConv);
			}
			~WrapperGenMap(){ GenMap_delete(_genMap); }
			List<int>^ generateMap(int nbElementDiff) {
				int* cases = GenMap_generate(_genMap, nbElementDiff);
					//_genMap->generate(nbElementDiff);
				List<int>^ lCases = gcnew List<int>();
				for (int i = 0; i< _genMap->getX() * _genMap->getY(); i++){
					lCases->Add(cases[i]);
				}
				delete[] cases;
				return lCases;
			}
			// I expect to go to hell for this <3
			List<Tuple<int, int>^>^ placePlayer(List<int>^ unwanted) {
				std::list<int> unwantedStdList;
				for each(int i in unwanted) { unwantedStdList.push_back(i); }
				std::pair<std::pair<int, int>, std::pair<int, int>> result = GenMap_placePlayer(_genMap, unwantedStdList);
				Tuple<int, int>^ p1 = gcnew Tuple<int, int>(result.first.first, result.first.second);
				Tuple<int, int>^ p2 = gcnew Tuple<int, int>(result.second.first, result.second.second);
				List<Tuple<int, int>^>^ listPos = gcnew List<Tuple<int, int>^>();
				listPos->Add(p1);
				listPos->Add(p2);
				return listPos;
			}

			List<int>^ bestMoves(int nbMovesWanted, Tuple<int, int, int>^ u, List<int>^ movesPossibles, Dictionary<int, Tuple<int, int>^>^ terrainData, List<Tuple<int, int, int>^>^ opponents) {
				List<int>^ result = gcnew List<int>();
				std::tuple<int, int, int> uConv(u->Item1, u->Item2, u->Item3);
				std::list<int> movesPossiblesConv;
				for each (int move in movesPossibles)
				{
					movesPossiblesConv.push_back(move);
				}
				std::list<std::tuple<int, int, int>> opponentsConv;
				for each (Tuple<int, int, int>^ adv in opponents)
				{
					opponentsConv.push_back(std::tuple<int, int, int>(adv->Item1, adv->Item2, adv->Item3));
				}
				std::map<int, std::pair<int, int>> terrainDataConv;
				for each (KeyValuePair<int, Tuple<int, int>^> ind in terrainData)
				{
					terrainDataConv[ind.Key] = std::make_pair<int, int>(ind.Value->Item1, ind.Value->Item2);
				}

				std::list<int> stdRes = GenMap_bestMoves(_genMap, nbMovesWanted, uConv, movesPossiblesConv, terrainDataConv, opponentsConv);
				for each (int pos in stdRes)
				{
					result->Add(pos);
				}
				
				return result;
			}
			



		protected:
			!WrapperGenMap(){ GenMap_delete(_genMap); }
		};

}
#endif