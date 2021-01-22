using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Buvette
{
    public partial class Form1 : Form
    {
        DataClasses1DataContext co = new DataClasses1DataContext();
        List<String> panier = new List<String>();
        double prix_totale;

        public Form1()
        {
            InitializeComponent();
            var produits = from c in co.Produit select c.nom;
            var i = 0;
            var j = 0;
            foreach (var obj in produits)
            {
                if (j == 3) { j = 0; i++; }
                Button Mybutton = new Button();
                Mybutton.Location = new Point(250 * j + 150, 150 * i + 50);
                Mybutton.Size = new Size(200, 100);
                Mybutton.Text = obj;
                Mybutton.TextAlign = ContentAlignment.MiddleCenter;
                Mybutton.FlatStyle = FlatStyle.Flat;
                Mybutton.BackColor = Color.CornflowerBlue;
                Mybutton.ForeColor = Color.White;
                Mybutton.Font = new Font("Cambria", 18);
                Mybutton.Click += selectcategories;
                // Adding this button to form 
                categories.Controls.Add(Mybutton);
                j++;

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }


        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void selectcategories(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            Button produit = sender as Button;

            groupBox1.Text = produit.Text;

            var y = (from h in co.Produit
                     join d in co.Sous_Produit
                        on h.ref_produit equals d.ref_produit
                     where h.nom == produit.Text
                     select new { d.nom , d.prix_unitaire });
            var i = 0;
            var j = 0;
            foreach (var obj in y)
            {
                if (j == 3) { j = 0; i++; }
                Button Mybutton = new Button();
                Mybutton.Location = new Point(250 * j + 150, 150 * i + 100);
                Mybutton.Size = new Size(165, 165);
                Mybutton.Text = obj.nom +'\n' +obj.prix_unitaire +"Dh";
                Mybutton.FlatStyle = FlatStyle.Flat;
                Mybutton.BackColor = Color.CornflowerBlue;
                Mybutton.ForeColor = Color.White;
                Mybutton.Padding = new Padding(20);
                Mybutton.Font = new Font("Cambria", 18);
                Mybutton.Click += addPanier;
                // Adding this button to form 
                groupBox1.Controls.Add(Mybutton);
                j++;

            }
        }
        public void addPanier(object sender, EventArgs e)
        {
            Button b = sender as Button;
            var StringPrix = b.Text.Substring(b.Text.IndexOf('\n') + 1);
            double prix = Convert.ToDouble(StringPrix.Substring(0, StringPrix.Length - 2));
            p.Rows.Add(b.Text.Substring(0, b.Text.IndexOf('\n')),prix);

            prix_totale = 0;
            for (int i = 0; i < p.Rows.Count; ++i)

            {

                prix_totale += Convert.ToDouble(p.Rows[i].Cells[1].Value);

            }
            prixT.Text = prix_totale + " Dh";

        }

        private void p_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(p.Columns[e.ColumnIndex].Name == "Supprimer" && e.RowIndex != p.Rows.Count-1)
            {
                prix_totale -= Convert.ToDouble(p.Rows[e.RowIndex].Cells[1].Value);
                p.Rows.RemoveAt(e.RowIndex);
                prixT.Text = prix_totale + " Dh";

            }
        }

        private void categories_Enter(object sender, EventArgs e)
        {

        }
    } 
}
