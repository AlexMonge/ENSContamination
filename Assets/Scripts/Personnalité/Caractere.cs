using UnityEngine;
using System;

namespace UnityEngine {

	public class Caractere
	{
		private int[] caractere;

		public Caractere ()
		{
            // INFO - BIBLI - COURS - PAUSE
            caractere = new int[4];

			int randMax = 101;
			randMax -= caractere[0] = Random.Range(0, randMax);
			randMax -= caractere[1] = Random.Range(0, randMax);
			randMax -= caractere[2] = Random.Range(0, randMax);
			caractere[3] = randMax - 1;
		}

		public bool inRandomRange(int val, ECaractere car) 
		{
			if (val < calculValueCarac((int) car))
				return true;
			return false;
		}

		private int calculValueCarac(int index)
		{
			int total = 0;

			for (int i = 0; i < index + 1; ++i)
				total += caractere [i];

			return total;
		}
	}
}