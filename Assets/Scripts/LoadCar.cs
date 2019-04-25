using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class LoadCar : MonoBehaviour {

    public int[] player1Parts = new int[4];
    public int[] player2Parts = new int[4];
    public int[] player3Parts = new int[4];
    public int[] player4Parts = new int[4];
    public int[] visibleCar = new int[4]; 

    int CantPlayers;
    int player= 1;

    string mapa;

    tempSave tp = new tempSave();
    ModGatherer mg = new ModGatherer();
    void Awake ()
    {
         DontDestroyOnLoad(transform.gameObject);
         
    }
    
    public void GetValues()
    {
        switch (player)
        {
            case 1:
                {
                    if (player <= CantPlayers)
                    {

                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.Load("cars.xml");

                            XmlElement root = xmlDoc.DocumentElement;
                            foreach (XmlNode childAsset in root.ChildNodes)
                            {
                                XmlElement id = childAsset["ID"];
                                if (id != null)
                                {
                                    if (id.InnerText.Equals(tp.f.ToString()))
                                    {
                                        player1Parts[0] = Convert.ToInt32(childAsset["Chasis"].InnerText);
                                        player1Parts[1] = Convert.ToInt32(childAsset["Wheels"].InnerText);
                                        player1Parts[2] = Convert.ToInt32(childAsset["Front_Component"].InnerText);
                                        player1Parts[3] = Convert.ToInt32(childAsset["Back_Component"].InnerText);
                                    }
                                }
                            }
                        player++;
                        }
                    else
                    {
                        Application.LoadLevel("building"); //gameScene
                    }
                }
                break;
            case 2:
                {
                    if (player <= CantPlayers)
                    {

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load("cars.xml");

                        XmlElement root = xmlDoc.DocumentElement;
                        foreach (XmlNode childAsset in root.ChildNodes)
                        {
                            XmlElement id = childAsset["ID"];
                            if (id != null)
                            {
                                if (id.InnerText.Equals(tp.f.ToString()))
                                {
                                    player2Parts[0] = Convert.ToInt32(childAsset["Chasis"].InnerText);
                                    player2Parts[1] = Convert.ToInt32(childAsset["Wheels"].InnerText);
                                    player2Parts[2] = Convert.ToInt32(childAsset["Front_Component"].InnerText);
                                    player2Parts[3] = Convert.ToInt32(childAsset["Back_Component"].InnerText);
                                }
                            }
                        }
                        player++;
                    }
                    else
                    {
                        Application.LoadLevel(1); //gameScene
                    }
                }
                break;
            case 3:
                {
                    if (player <= CantPlayers)
                    {

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load("cars.xml");

                        XmlElement root = xmlDoc.DocumentElement;
                        foreach (XmlNode childAsset in root.ChildNodes)
                        {
                            XmlElement id = childAsset["ID"];
                            if (id != null)
                            {
                                if (id.InnerText.Equals(tp.f.ToString()))
                                {
                                    player3Parts[0] = Convert.ToInt32(childAsset["Chasis"].InnerText);
                                    player3Parts[1] = Convert.ToInt32(childAsset["Wheels"].InnerText);
                                    player3Parts[2] = Convert.ToInt32(childAsset["Front_Component"].InnerText);
                                    player3Parts[3] = Convert.ToInt32(childAsset["Back_Component"].InnerText);
                                }
                            }
                        }
                        player++;
                    }
                    else
                    {
                        Application.LoadLevel(1); //gameScene
                    }
                }
                break;
            case 4:
                {
                    if (player <= CantPlayers)
                    {

                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load("cars.xml");

                        XmlElement root = xmlDoc.DocumentElement;
                        foreach (XmlNode childAsset in root.ChildNodes)
                        {
                            XmlElement id = childAsset["ID"];
                            if (id != null)
                            {
                                if (id.InnerText.Equals(tp.f.ToString()))
                                {
                                    player4Parts[0] = Convert.ToInt32(childAsset["Chasis"].InnerText);
                                    player4Parts[1] = Convert.ToInt32(childAsset["Wheels"].InnerText);
                                    player4Parts[2] = Convert.ToInt32(childAsset["Front_Component"].InnerText);
                                    player4Parts[3] = Convert.ToInt32(childAsset["Back_Component"].InnerText);
                                }
                            }
                        }
                        player++;
                    }
                    else
                    {
                        Application.LoadLevel(1); //gameScene
                    }
                }
                break;
            default: break;
        }
    }

    public void NameThePlayers (int num)
    {
        CantPlayers = num;
		Debug.Log (CantPlayers);
    }
    public void GetMap (string map)
    {
        mapa = map;
    }
    public void VisibleChasis (int c)
    {
        visibleCar[0] = c;
    }
    public void VisibleRuedas(int g)
    {
        visibleCar[1] = g;
    }
    public void VisibleF(int bsus)
    {
        visibleCar[2] = bsus;
    }
    public void VisibleB(int dgadd)
    {
        visibleCar[3] = dgadd;
    }

}
