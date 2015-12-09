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
         OpenFileDialog open_File = new OpenFileDialog();
         open_File.InitialDirectory = "E:\\";
         if (open_File.ShowDialog() == DialogResult.OK)
         {
            richTextBox1.Text = "";
            str = File.ReadAllText(open_File.FileName, Encoding.Default);
            richTextBox1.AppendText(str);
         }
      }
      public struct Variable
      {
         public bool Is_local;
         public int count;
         public string name;
      }
      string control_of_symbols = "+-*/()[];.:,<>='";
      private List<Variable> Spen(List<String> text, out int count_of_procedure)
      {
         count_of_procedure = 0;
         List<Variable> allVariables = new List<Variable>();
         bool Is_procedure = false;
         bool Is_record = false;
         bool Is_str = false;
         int nesting_of_Levels = 0;
         bool Is_cantInitialize = false;
         for (int i = 0; i < text.Count; i++)
         {
            if (text[i].CompareTo("'") == 0)
               Is_str = !Is_str;
            int j;
            if ((text[i].ToLower().CompareTo("procedure") == 0) || (text[i].ToLower().CompareTo("function") == 0))
            {
               Is_procedure = true;
               count_of_procedure++;
            }
            if (text[i].ToLower().CompareTo("record") == 0) Is_record = true;
            if (text[i].ToLower().CompareTo("case") == 0) Is_cantInitialize = true;
            if (text[i].ToLower().CompareTo("begin") == 0) nesting_of_Levels++;
            if (text[i].ToLower().CompareTo("end") == 0)
            {
               nesting_of_Levels--;
               if (nesting_of_Levels < 0)
               {
                  Is_cantInitialize = false;

               }
               if (nesting_of_Levels == 0)
                  Is_procedure = false;
            }
            if (((":").CompareTo(text[i]) == 0) && (!Is_record) && (!Is_cantInitialize) && (!Is_procedure))
            {
               if (text[i + 1].CompareTo("=") != 0)
               {
                  if (i > 0)
                  {
                     {
                        j = i - 1;
                        bool count_of_Variables = true;
                        while (count_of_Variables)
                        {
                           if ((text[j].CompareTo(";") == 0) || (text[j].ToLower().CompareTo("var") == 0) ||
                               (text[j].CompareTo(")") == 0) || (text[j].CompareTo("(") == 0))
                              count_of_Variables = false;
                           for (int k = 0; k < control_of_symbols.Length; k++)
                           {
                              if (text[j][0] == control_of_symbols[k])
                                 count_of_Variables = false;
                           }
                           if (text[j].CompareTo(",") == 0) count_of_Variables = true;
                           if (count_of_Variables && !Is_str)
                           {
                              if (text[j].CompareTo(",") != 0)
                              {
                                 Variable variable;
                                 variable.count = 0;
                                 variable.name = text[j];
                                 variable.Is_local = true;
                                 allVariables.Add(variable);
                              }
                           }
                           j--;
                           if (j < 0) count_of_Variables = false;
                        }
                     }
                  }
               }
            }
            for (int k = 0; k < allVariables.Count; k++)
            {
               if ((allVariables[k].name.CompareTo(text[i]) == 0) && (!Is_record) && (!Is_str))
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
         bool large_of_Comments = false;
         for (int i = 0; i < richTextBox1.Lines.Count(); i++)
         {
            string beginning_of_comment = richTextBox1.Lines[i];
            int j = 0;
            int symbCount = 0;
            for (j = 0; j < richTextBox1.Lines[i].Length; j++)
            {
               if ((beginning_of_comment[j] == '/') && (beginning_of_comment[j + 1] == '/'))
               {
                  withoutComments[i, symbCount++] = '\n';
                  break;
               }
               if (beginning_of_comment[j] == '{')
                  large_of_Comments = true;
               if (beginning_of_comment[j] == '}')
                  large_of_Comments = false;
               if (!large_of_Comments)
               {
                  withoutComments[i, symbCount++] = beginning_of_comment[j];
               }

            }
            withoutComments[i, symbCount++] = '\n';
            if (symbCount > maxStrLength) maxStrLength = symbCount;

         }
         return withoutComments;
      }

      private List<String> Parse(char[,] text, int maxStrLength)
      {
         List<string> result = new List<string>();

         for (int i = 0; i < text.GetLength(0); i++)
         {

            char[] control_of_gaps = new char[512];
            int size = 0;
            for (int j = 0; j < text.GetLength(1); j++)
            {
               bool check_for_next_element = false;
               for (int k = 0; k < control_of_symbols.Length; k++)
                  if (text[i, j] == control_of_symbols[k])
                     check_for_next_element = true;
               if (((text[i, j] != ' ') && (!check_for_next_element)) && (text[i, j] != '\n'))
               {
                  if (text[i, j] != '\n')
                     control_of_gaps[size++] = text[i, j];
               }
               else
               {
                  if (size != 0)
                     result.Add(new string(control_of_gaps));
                  for (int r = 0; r < size; r++)
                     control_of_gaps[r] = '\0';
                  size = 0;
                  if (check_for_next_element)
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
         char[,] clear_text = DeleteComments(ref maxStrLength);
         List<string> parsed = Parse(clear_text, maxStrLength);
         clear_text = null;
         richTextBox2.Text = "";
         int count_of_procedure;
         List<Variable> variables = Spen(parsed, out count_of_procedure);

         int count_of_call = 0;
         foreach (Variable variable in variables)
         {
            count_of_call = count_of_call + variable.count;
            string symbol_name = variable.name;
            richTextBox2.AppendText(symbol_name);
            richTextBox2.AppendText(" " + variable.count.ToString());
            richTextBox2.AppendText("\n");

         }
         label2.Visible = true;
         label1.Text = "Количество вызовов: " + count_of_call.ToString();
         label2.Text = "Количество переменных: " + variables.Count.ToString();
         label3.Text = "Количество процедур: " + count_of_procedure.ToString();


      }
   }
}
