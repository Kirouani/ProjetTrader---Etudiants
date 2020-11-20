using MySql.Data.MySqlClient;
using System;
using MetierTrader;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        private MySqlConnection cnx;
        private MySqlCommand cmd;
        private MySqlDataReader dr;


        // Constructeur
        public GstBdd()
        {
            string chaine = "Server=localhost;Database=trader;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        public List<Trader> getAllTraders()
        {
            List<Trader> mesTraders = new List<Trader>();
            cmd = new MySqlCommand("select idTrader, nomTrader from trader", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Trader unTrader = new Trader(Convert.ToInt16(dr[0].ToString()), dr[1].ToString());
                mesTraders.Add(unTrader);
            }
            dr.Close();
            return mesTraders;
        }
        public List<ActionPerso> getAllActionsByTrader(int numTrader)
        {
            List<ActionPerso> lesActions = new List<ActionPerso>();
            cmd = new MySqlCommand("select numAction, nomAction, prixAchat, quantite, prixAchat*quantite from action a inner join acheter ac on a.IdAction = ac.numAction where numTrader = "+numTrader, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ActionPerso uneActionPerso = new ActionPerso(Convert.ToInt32(dr[0].ToString()), (dr[1].ToString()), Convert.ToDouble(dr[2].ToString()), Convert.ToInt32(dr[3].ToString()), Convert.ToDouble(dr[4].ToString()));
                lesActions.Add(uneActionPerso);
            }
            dr.Close();
            return lesActions;
        }
          
       public List<MetierTrader.Action> getAllActionsNonPossedees(int numTrader)
        {
            List<MetierTrader.Action> lesActionsNonPosseder = new List<MetierTrader.Action>();
            cmd = new MySqlCommand("select idAction, nomAction from action where idAction not in (select numAction from action a inner join acheter ac on a.idAction = ac.numAction where numTrader =" + numTrader+")", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MetierTrader.Action uneActionNonPosseder = new MetierTrader.Action(Convert.ToInt32(dr[0].ToString()), (dr[1].ToString()));
                lesActionsNonPosseder.Add(uneActionNonPosseder);
            }
            dr.Close();
            return lesActionsNonPosseder;

        }

        public void SupprimerActionAcheter(int numAction, int numTrader)
        {
           // cmd = new MySqlCommand("DELETE numAction, numTrader from acheter where numAction = " + numAction "and where numTrader =" + numTrader, cnx);
        }

        public void UpdateQuantite(int numAction, int numTrader, int quantite)
        {
            
        }

        public double getCoursReel(int numAction)
        {
            return 0;
        }
        public void AcheterAction(int numAction, int numTrader, double prix, int quantite)
        {

        }
        public double getTotalPortefeuille(int numTrader)
        {
            double total = 0;

            cmd = new MySqlCommand("select sum(prixAchat*quantite) numTrader from action a inner join acheter ac on a.IdAction = ac.numAction where numTrader = "+numTrader, cnx);
            dr = cmd.ExecuteReader();
            dr.Read();           
            total =(Convert.ToDouble(dr[1]));         
            dr.Close();
            return total;
        }
    }
}
