using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace ModelTranformerExample
{
    /// <summary>
    /// Форма графического клиента
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Компонент трансформации моделей
        /// </summary>
        ModelTransformationComponent.ITransformationComponent transformationComponent;

        /// <summary>
        /// Правила трансформации
        /// </summary>
        ModelTransformationComponent.AllRules allRules;

        /// <summary>
        /// Хэш входной строки правил трансформации
        /// </summary>
        int hash;

        public MainForm()
        {
            InitializeComponent();
            transformationComponent = new ModelTransformationComponent.TransformationComponent();
            Debug.WriteLine("transformationComponent initialized");
        }

        /// <summary>
        /// Нажатие кнопки "Открыть файл" для текстовой модели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTextButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("OpenTextButton clicked");
            if (TextOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("Dialog result ok");
                var text = File.ReadAllText(TextOpenFileDialog.FileName);
                Debug.WriteLine("----------Got text----------");
                Debug.WriteLine(text);
                Debug.WriteLine("----------------------------");
                InputTestRichTextBox.Text = text;
            }
        }

        /// <summary>
        /// Нажатие кнопки "Открыть файл" для правил трансформации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputRulesButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("InputRulesButton clicked");
            if (RulesOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("Dialog result ok");
                var rules = File.ReadAllText(RulesOpenFileDialog.FileName);
                Debug.WriteLine("----------Got text----------");
                Debug.WriteLine(rules);
                Debug.WriteLine("----------------------------");
                RulesInputRichTextBox.Text = rules;
            }
        }

        /// <summary>
        /// Нажатие кнопки "Сохранить как" 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OutputButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("InputRulesButton clicked");
            if (OutputSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Debug.WriteLine("Dialog result ok");
                Debug.WriteLine("Writing to: " + OutputSaveFileDialog.FileName);
                File.WriteAllText(OutputSaveFileDialog.FileName, OutputRichTextBox.Text);
            }
        }


        /// <summary>
        /// Нажатие кнопки "стрелочка" - трансформация 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransformStartButton_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("TransformStartButton clicked");
            try
            {
                if (allRules == null)
                {
                    Debug.WriteLine("allRules was null");
                    hash = RulesInputRichTextBox.Text.GetHashCode();
                    Debug.WriteLine("got hash: " + hash.ToString());
                    allRules = transformationComponent.TransformToRules(RulesInputRichTextBox.Text);
                    Debug.WriteLine("parsed rules succesfully");
                }
                var text = transformationComponent.Transform(InputTestRichTextBox.Text,
                        allRules,
                        SourceLangTextBox.Text,
                        TargetLangTextBox.Text);
                OutputRichTextBox.Text = text;
                Debug.WriteLine("parsed model succesfully");
                Debug.WriteLine("----------Result------------");
                Debug.WriteLine(text);
                Debug.WriteLine("----------------------------");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                OutputRichTextBox.AppendException(ex);
            }
        }

        /// <summary>
        /// Кнопка "Парсинг текста правил"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParseRulesButton_Click(object sender, EventArgs e)
        {
            try
            {
                int gothash = RulesInputRichTextBox.Text.GetHashCode();
                if (hash != gothash)
                {
                    Debug.WriteLineIf(allRules == null, "allRules was null");
                    Debug.WriteLineIf(hash != RulesInputRichTextBox.Text.GetHashCode(), "hash was:" + hash + " got:" + gothash);
                    hash = gothash;
                    Debug.WriteLine("new hash:" + hash);
                    allRules = transformationComponent.TransformToRules(RulesInputRichTextBox.Text);
                    Debug.WriteLine("rules parsed succesful");
                    AutoCompleteStringCollection names = new AutoCompleteStringCollection();
                    names.AddRange(allRules.GetLanguages.ToArray());
                    SourceLangTextBox.AutoCompleteCustomSource = names;
                    SourceLangTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    SourceLangTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    TargetLangTextBox.AutoCompleteCustomSource = names;
                    TargetLangTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    TargetLangTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                else
                {
                    Debug.WriteLine("rules where ready and same hash");
                    if (MessageBox.Show(
                    "Похоже, входная строка не изменилась. Вы уверены, что хотите повторить парсинг?",
                    "",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Debug.WriteLine("got yes");
                        hash = RulesInputRichTextBox.Text.GetHashCode();
                        Debug.WriteLine("new hash:" + hash);
                        allRules = transformationComponent.TransformToRules(RulesInputRichTextBox.Text);
                        Debug.WriteLine("rules parsed succesful");
                    }
                    else
                    {
                        Debug.WriteLine("got not yes");
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                OutputRichTextBox.AppendException(ex);
            }
        }

        private void InputTestRichTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
