using UnityEngine;
using System;
using System.Text;

namespace UnityEngine {

	public class Caractere
	{
        /// <summary>
        /// Tableau définissant le caractère propre à une personne.
        /// Dans la version actuelle :
        ///     - Premier élément   -> préfère les salles info
        ///     - Deuxième élément  -> préfère le foyer
        ///     - Troisième élément -> préfère les salles de cours
        ///     - Quatrième élément -> préfère le patio
        /// </summary>
		private int[] caractere;

        #region Constructeurs

        /// <summary>
        /// Constructeur par défaut de Caractere
        /// </summary>
        public Caractere ()
		{
            caractere = new int[4];

            // Génération des caractères en %age
			int randMax = 101;
			randMax -= caractere[(int) ECaractere.INFORMATIQUE] = Random.Range(0, randMax);
			randMax -= caractere[(int)ECaractere.FOYER] = Random.Range(0, randMax);
			randMax -= caractere[(int)ECaractere.SALLECOURS] = Random.Range(0, randMax);
			caractere[(int)ECaractere.PAUSE] = randMax - 1;

            StringBuilder sb = new StringBuilder();
            sb.Append("INFO : ");
            sb.Append(caractere[0]);
            sb.Append(" | FOYER : ");
            sb.Append(caractere[1]);
            sb.Append(" | COURS : ");
            sb.Append(caractere[2]);
            sb.Append(" | PAUSE : ");
            sb.Append(caractere[3]);
        }

        #endregion

        #region Méthodes 

        /// <summary>
        /// Permet de savoir si <strong>val</strong> appartient à un caractère spécifié
        /// </summary>
        /// <param name="val">Une valeur comprise entre 0 et 100</param>
        /// <param name="car">Un caractère</param>
        /// <returns></returns>
        public bool inRandomRange(int val, ECaractere car) 
		{
			if (val < calculValueCarac((int) car))
				return true;
			return false;
		}

        /// <summary>
        /// Permet de calculer la somme des pourcentages pour un caractère en fonction de sa position dans le tableau.
        /// Ex : [0] = 12 et [1] = 50. Alors la somme arrive à 12% pour [0] et 62% pour [1] (100% pour la dernière valeur).
        /// Cela permet de savoir où se range un nombre aléatoire entre 0 et 100.
        /// </summary>
        /// <param name="index">Identifiant du caractère</param>
        /// <returns>Retourne la somme des pourcentages pour un caractère en fonction de sa position dans le tableau</returns>
		private int calculValueCarac(int index)
		{
			int total = 0;

			for (int i = 0; i < index + 1; ++i)
				total += caractere [i];

			return total;
		}

        #endregion
    }
}