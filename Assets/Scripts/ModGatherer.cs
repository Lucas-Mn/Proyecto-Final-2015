using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using System.Collections.Generic;

public class ModGatherer : MonoBehaviour {

    public int newChasis = 0;  public int newWheels = 0;
    public int newFC = 0;      public int newBC = 0;
    private int id = 0;


    public void GetChasis(int c)
    {
        newChasis = c;

    }
    public void GetWheels(int y)
    {
        newWheels = y;

    }
    public void GetFC(int r)
    {
        newFC = r;

    }
    public void GetBC(int f)
    {
        newBC = f;

    }
    public void perform ()
    {
        id = (int)GameObject.Find("Slider").GetComponent<Slider>().value;
        if (File.Exists("cars.xml"))
        {
            OnClickModify(newChasis, newWheels, newFC, newBC, id);
        }
        else
        {
            write();
        }
    }

    private void OnClickModify(int a, int b, int x, int z, int cc)
    {
		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("cars.xml");

		XmlElement root = xmlDoc.DocumentElement;

		//XmlElement assets = root["Cars"];
		
		//Loop though each child
		foreach(XmlNode childAsset in root.ChildNodes) {
			XmlElement id = childAsset["ID"];
			if(id != null) {
				if(id.InnerText.Equals(cc.ToString())) {
					childAsset["Chasis"].InnerText = a.ToString();
					childAsset["Wheels"].InnerText = b.ToString();
					childAsset["Front_Component"].InnerText = x.ToString();
					childAsset["Back_Component"].InnerText = z.ToString();
				}
			}
		}

		/*foreach(XmlElement element in xmlDoc.ChildNodes[0].ChildNodes) {
			if(element.GetAttribute("ID") == cc.ToString()) {
				Debug.Log("ACA ESTOY");
			}
		}*/

        /*var query = from n in xmlDoc.Descendants("Custom_Cars")
                    where n.Attribute("ID").Value == cc.ToString()
                    select n;
        foreach (XElement n in query)
        {
            n.SetElementValue("Chasis", a.ToString());
            n.SetElementValue("Wheels", b.ToString());
            n.SetElementValue("Front_Component", x.ToString());
            n.SetElementValue("Back_Component", z.ToString());
        }*/

        xmlDoc.Save("cars.xml");
    }
    public static void write()
    {
        C_Cars[] customArray = new C_Cars[6];
        customArray[0] = new C_Cars(0, 0, 0, 0, 0);
        customArray[1] = new C_Cars(1, 0, 0, 0, 0);
        customArray[2] = new C_Cars(2, 0, 0, 0, 0);
        customArray[3] = new C_Cars(3, 0, 0, 0, 0);
        customArray[4] = new C_Cars(4, 0, 0, 0, 0);
        customArray[5] = new C_Cars(5, 0, 0, 0, 0);


        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;

        using (XmlWriter wr = XmlWriter.Create("cars.xml", settings))
        {
            wr.WriteStartDocument();
            wr.WriteStartElement("Cars");

            foreach (C_Cars cars in customArray)
            {
                wr.WriteStartElement("Custom_Cars");
                wr.WriteElementString("ID", cars.id.ToString());
                wr.WriteElementString("Chasis", cars.chasis.ToString());
                wr.WriteElementString("Wheels", cars.wheels.ToString());
                wr.WriteElementString("Front_Component", cars.f_component.ToString());
                wr.WriteElementString("Back_Component", cars.b_component.ToString());
                wr.WriteEndElement();
            }
            wr.WriteEndElement();
            wr.WriteEndDocument();
        }

    }

   public class C_Cars
    {
        int _id;
        int _Chasis;
        int _Wheels;
        int _F_Component;
        int _B_Component;

        public C_Cars(int Id, int Chasis, int Wheels, int F_Component, int B_Component)
        {
            this._id = Id;
            this._Chasis = Chasis;
            this._Wheels = Wheels;
            this._F_Component = F_Component;
            this._B_Component = B_Component;
        }
        public int id { get { return _id; } }
        public int chasis { get { return _Chasis; } }
        public int wheels { get { return _Wheels; } }
        public int f_component { get { return _F_Component; } }
        public int b_component { get { return _B_Component; } }
    }
}
