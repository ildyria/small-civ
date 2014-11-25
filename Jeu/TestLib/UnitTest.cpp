#include "stdafx.h"

using namespace System;
using namespace System::Text;
using namespace System::Diagnostics;
using namespace System::Collections::Generic;
using namespace Microsoft::VisualStudio::TestTools::UnitTesting;

namespace TestLib
{
	[TestClass]
	public ref class UnitTest
	{
	private:
		TestContext^ testContextInstance;

	public: 
		/// <summary>
		///Obtient ou définit le contexte de test qui fournit
		///des informations sur la série de tests active ainsi que ses fonctionnalités.
		///</summary>
		property Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^ TestContext
		{
			Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^ get()
			{
				return testContextInstance;
			}
			System::Void set(Microsoft::VisualStudio::TestTools::UnitTesting::TestContext^ value)
			{
				testContextInstance = value;
			}
		};

		#pragma region Additional test attributes
		//
		//Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
		//
		//Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
		//[ClassInitialize()]
		//static void MyClassInitialize(TestContext^ testContext) {};
		//
		//Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
		//[ClassCleanup()]
		//static void MyClassCleanup() {};
		//
		//Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
		//[TestInitialize()]
		//void MyTestInitialize() {};
		//
		//Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
		//[TestCleanup()]
		//void MyTestCleanup() {};
		//
		#pragma endregion 

		[TestMethod]
		void CPP_generateMap()
		{
			//Assert::AreEqual('D', FIRST_ASCII_LETTER + 3));
			/*GenMap g(5, 5);
			int* c = g.generate(5);
			int i;*/
			/*bool bornes = true;
			for (i = 0; c[i] != '\0'; i++){
				if (c[i] > 5 && c[i] < 0) {
					bornes = false;
				}
			}
			Assert::AreEqual(25, i);
			Assert::IsTrue(bornes);*/
			Assert::Fail();
		};

		[TestMethod]
		void CPP_placePlayers()
		{
			Assert::Fail();
		};

		[TestMethod]
		void CPP_bestMoves()
		{
			Assert::Fail();
		};
	};
	
}
