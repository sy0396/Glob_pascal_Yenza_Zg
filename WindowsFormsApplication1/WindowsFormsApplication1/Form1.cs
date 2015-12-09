using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }            
        

        private void button1_Click(object sender, EventArgs e)
        {            
            String str = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "E:\\";
            openFileDialog1.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = "";
                str = File.ReadAllText(openFileDialog1.FileName, Encoding.Default);
                richTextBox1.AppendText(str);
            }
        }
        public struct Variable
        {
            public bool local;
            public int count;
            public string name;
        }
        string control = "+-*/()[];.:,<>='";
        private List<Variable> Spen(List<String> text,out int procCount)
        {
           procCount = 0;
            List<Variable> allVariables = new List<Variable>();
            bool procedure = false;
            bool record = false;
            bool str = false;
            int nestingLevel = 0;
            bool cantInitialize = false;
            for (int i = 0; i < text.Count;i++ )
            {
                if (text[i].CompareTo("'") == 0)
                    str = !str;
                int j;
                if ((text[i].ToLower().CompareTo("procedure") == 0) || (text[i].ToLower().CompareTo("function") == 0))
                {
                   procedure = true;
                   procCount++;
                }
                if (text[i].ToLower().CompareTo("record") == 0) record = true;
                if (text[i].ToLower().CompareTo("case") == 0) cantInitialize = true;
                if (text[i].ToLower().CompareTo("begin") == 0) nestingLevel++;
                if (text[i].ToLower().CompareTo("end") == 0)
                {
                    nestingLevel--;
                    if (nestingLevel < 0)
                    {
                       cantInitialize = false;
                       
                    }
                    if (nestingLevel == 0)
                       procedure = false;
                }
                if (((":").CompareTo(text[i]) == 0)&&(!record)&&(!cantInitialize)&&(!procedure))
                {                    
                        if (text[i + 1].CompareTo("=") != 0)
                        {
                            if (i > 0)
                            {
                                {
                                    j = i - 1;
                                    bool moreVariables = true;
                                    while (moreVariables)
                                    {
                                        if ((text[j].CompareTo(";") == 0) || (text[j].ToLower().CompareTo("var") == 0) || 
                                            (text[j].CompareTo(")") == 0) || (text[j].CompareTo("(") == 0)) moreVariables = false;
                                        for (int k=0;k<control.Length;k++)
                                        {
                                            if (text[j][0]==control[k])
                                                moreVariables=false;
                                        }
                                        if (text[j].CompareTo(",") == 0) moreVariables = true;
                                        if (moreVariables&&!str)
                                        
                                        {   if (text[j].CompareTo(",")!=0)
                                            {
                                                       
                                                Variable variable;
                                                variable.count = 0;                                                
                                                variable.name = text[j];
                                                variable.local = true;
                                                allVariables.Add(variable);
                                            }
                                        }
                                        j--;
                                        if (j < 0) moreVariables = false;
                                    }
                                }
                            }
                        }
                }
                for (int k=0;k<allVariables.Count;k++)
                {
                    if ((allVariables[k].name.CompareTo(text[i])==0)&&(!record)&&(!str))
                    {
                        Variable variable = allVariables[k];
                        variable.count++;
                        allVariables.Remove(allVariables[k]);
                        allVariables.Add(variable);
                        break;
                    }
                }
            }

            return allVariables;
        }

        private char[,] DeleteComments(ref int maxStrLength)
        {
            char[,] withoutComments = new char[richTextBox1.Lines.Count(), 500];
            bool largeComment = false;
            for (int i=0;i<richTextBox1.Lines.Count();i++)
            {
                string r=richTextBox1.Lines[i];
                int j=0;
                int symbCount = 0;
                for (j=0;j<richTextBox1.Lines[i].Length;j++)
                {
                    
                    if ((r[j] == '/') && (r[j + 1] == '/'))
                    {
                         withoutComments[i,symbCount++] = '\n';
                         break;
                    }
                    if (r[j] == '{')
                        largeComment = true;
                    if (r[j] == '}')
                        largeComment = false;
                    if (!largeComment)
                    {
                        withoutComments[i, symbCount++] = r[j];                        
                    }
                                        
                }
                withoutComments[i, symbCount++] = '\n';    
                if (symbCount > maxStrLength) maxStrLength = symbCount;
                
            }
            return withoutComments;        
        }
        
        private List<String> Parse(char[,] text,int maxStrLength)
        {
            List<string> result=new List<string>();
            
            for (int i = 0; i < text.GetLength(0);i++ )
            {
                
                char[] tmp=new char [512];
                int size = 0;
                for (int j=0;j<text.GetLength(1);j++)
                {
                    bool controloflength=false;
                    for (int k=0;k<control.Length;k++)
                        if (text[i,j]==control[k])
                           controloflength = true;
                    if (((text[i, j] != ' ') && (!controloflength)) && (text[i, j] != '\n'))
                    {
                        if (text[i, j] != '\n')
                            tmp[size++] = text[i, j];
                    }
                    else
                    {                        
                        if (size != 0)
                            result.Add(new string(tmp));
                        for (int r = 0; r < size; r++)
                            tmp[r] = '\0';
                            size = 0;
                            if (controloflength)
                            result.Add(Convert.ToString(text[i, j]));
                    }
                }
            }
            return result;
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
            int maxStrLength = 0;            
            char[,] tempText = DeleteComments(ref maxStrLength);
            List<string> parsed=Parse(tempText,maxStrLength);
            tempText = null;
            richTextBox2.Text = "";
           int procCount;
            List < Variable > variables= Spen(parsed,out procCount);

            int countofcall = 0; 
           foreach (Variable variable in variables)
            {
               countofcall = countofcall + variable.count;
                string s = variable.name;                
                richTextBox2.AppendText(s);
                richTextBox2.AppendText(" "+variable.count.ToString());
                richTextBox2.AppendText("\n");
                
            }
           label2.Visible = true;
           label1.Text = "Количество вызовов: " + countofcall.ToString();
           label2.Text = "Количество переменных: " + variables.Count.ToString();
           label3.Text = "Количество процедур: " + procCount.ToString();

            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
