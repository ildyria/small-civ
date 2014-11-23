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
			KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>^ placePlayer(List<int>^ unwanted) {
				//cliext::list<int>^ unwantedStdList = gcnew cliext::list<int>(unwanted);
				std::list<int> unwantedStdList;
				for each(int i in unwanted) { unwantedStdList.push_back(i); }
				std::pair<std::pair<int, int>, std::pair<int, int>> result = GenMap_placePlayer(_genMap, unwantedStdList);
				KeyValuePair<int, int> p1(result.first.first, result.first.second), p2(result.second.first, result.second.second);
				return gcnew KeyValuePair<KeyValuePair<int, int>, KeyValuePair<int, int>>(p1, p2);
			}
		protected:
			!WrapperGenMap(){ GenMap_delete(_genMap); }
		};

}
#endif