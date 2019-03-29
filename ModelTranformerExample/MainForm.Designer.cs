namespace ModelTranformerExample
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.InputTextPanel = new System.Windows.Forms.Panel();
            this.InputTestRichTextBox = new System.Windows.Forms.RichTextBox();
            this.InputRulesPanel = new System.Windows.Forms.Panel();
            this.RulesInputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.OutputPanel = new System.Windows.Forms.Panel();
            this.OutputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.InputTextLabel = new System.Windows.Forms.Label();
            this.InputRulesLabel = new System.Windows.Forms.Label();
            this.OutputLabel = new System.Windows.Forms.Label();
            this.TextOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.RulesOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OutputSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.TargetLangTextBox = new System.Windows.Forms.TextBox();
            this.SourceLangTextBox = new System.Windows.Forms.TextBox();
            this.ParseRulesButton = new System.Windows.Forms.Button();
            this.SourceLangLabel = new System.Windows.Forms.Label();
            this.AllInputTextPanel = new System.Windows.Forms.Panel();
            this.AllInputRulesPanel = new System.Windows.Forms.Panel();
            this.TranslationPanel = new System.Windows.Forms.Panel();
            this.TransformStartButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TargetLangLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.InputTextPanel.SuspendLayout();
            this.InputRulesPanel.SuspendLayout();
            this.OutputPanel.SuspendLayout();
            this.AllInputTextPanel.SuspendLayout();
            this.AllInputRulesPanel.SuspendLayout();
            this.TranslationPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputTextPanel
            // 
            this.InputTextPanel.Controls.Add(this.InputTestRichTextBox);
            this.InputTextPanel.Location = new System.Drawing.Point(-4, 63);
            this.InputTextPanel.Name = "InputTextPanel";
            this.InputTextPanel.Size = new System.Drawing.Size(300, 506);
            this.InputTextPanel.TabIndex = 0;
            // 
            // InputTestRichTextBox
            // 
            this.InputTestRichTextBox.Location = new System.Drawing.Point(4, 0);
            this.InputTestRichTextBox.Name = "InputTestRichTextBox";
            this.InputTestRichTextBox.Size = new System.Drawing.Size(293, 503);
            this.InputTestRichTextBox.TabIndex = 0;
            this.InputTestRichTextBox.Text = "";
            this.InputTestRichTextBox.TextChanged += new System.EventHandler(this.InputTestRichTextBox_TextChanged);
            // 
            // InputRulesPanel
            // 
            this.InputRulesPanel.Controls.Add(this.RulesInputRichTextBox);
            this.InputRulesPanel.Location = new System.Drawing.Point(0, 64);
            this.InputRulesPanel.Name = "InputRulesPanel";
            this.InputRulesPanel.Size = new System.Drawing.Size(331, 505);
            this.InputRulesPanel.TabIndex = 1;
            // 
            // RulesInputRichTextBox
            // 
            this.RulesInputRichTextBox.Location = new System.Drawing.Point(3, 1);
            this.RulesInputRichTextBox.Name = "RulesInputRichTextBox";
            this.RulesInputRichTextBox.Size = new System.Drawing.Size(325, 501);
            this.RulesInputRichTextBox.TabIndex = 1;
            this.RulesInputRichTextBox.Text = "";
            // 
            // OutputPanel
            // 
            this.OutputPanel.Controls.Add(this.OutputRichTextBox);
            this.OutputPanel.Location = new System.Drawing.Point(4, 61);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(271, 511);
            this.OutputPanel.TabIndex = 2;
            // 
            // OutputRichTextBox
            // 
            this.OutputRichTextBox.Location = new System.Drawing.Point(3, 4);
            this.OutputRichTextBox.Name = "OutputRichTextBox";
            this.OutputRichTextBox.ReadOnly = true;
            this.OutputRichTextBox.Size = new System.Drawing.Size(265, 501);
            this.OutputRichTextBox.TabIndex = 1;
            this.OutputRichTextBox.Text = "";
            // 
            // InputTextLabel
            // 
            this.InputTextLabel.AutoSize = true;
            this.InputTextLabel.Location = new System.Drawing.Point(12, 10);
            this.InputTextLabel.Name = "InputTextLabel";
            this.InputTextLabel.Size = new System.Drawing.Size(89, 13);
            this.InputTextLabel.TabIndex = 3;
            this.InputTextLabel.Text = "Исходный текст";
            // 
            // InputRulesLabel
            // 
            this.InputRulesLabel.AutoSize = true;
            this.InputRulesLabel.Location = new System.Drawing.Point(1, 10);
            this.InputRulesLabel.Name = "InputRulesLabel";
            this.InputRulesLabel.Size = new System.Drawing.Size(135, 13);
            this.InputRulesLabel.TabIndex = 4;
            this.InputRulesLabel.Text = "Правила трансформации";
            // 
            // OutputLabel
            // 
            this.OutputLabel.AutoSize = true;
            this.OutputLabel.Location = new System.Drawing.Point(15, 10);
            this.OutputLabel.Name = "OutputLabel";
            this.OutputLabel.Size = new System.Drawing.Size(40, 13);
            this.OutputLabel.TabIndex = 5;
            this.OutputLabel.Text = "Вывод";
            // 
            // TextOpenFileDialog
            // 
            this.TextOpenFileDialog.FileName = "openFileDialog1";
            // 
            // RulesOpenFileDialog
            // 
            this.RulesOpenFileDialog.FileName = "openFileDialog2";
            // 
            // TargetLangTextBox
            // 
            this.TargetLangTextBox.Location = new System.Drawing.Point(101, 36);
            this.TargetLangTextBox.Name = "TargetLangTextBox";
            this.TargetLangTextBox.Size = new System.Drawing.Size(100, 20);
            this.TargetLangTextBox.TabIndex = 10;
            // 
            // SourceLangTextBox
            // 
            this.SourceLangTextBox.Location = new System.Drawing.Point(111, 36);
            this.SourceLangTextBox.Name = "SourceLangTextBox";
            this.SourceLangTextBox.Size = new System.Drawing.Size(100, 20);
            this.SourceLangTextBox.TabIndex = 11;
            // 
            // ParseRulesButton
            // 
            this.ParseRulesButton.Location = new System.Drawing.Point(4, 36);
            this.ParseRulesButton.Name = "ParseRulesButton";
            this.ParseRulesButton.Size = new System.Drawing.Size(149, 23);
            this.ParseRulesButton.TabIndex = 12;
            this.ParseRulesButton.Text = "Парсинг текста правил";
            this.ParseRulesButton.UseVisualStyleBackColor = true;
            this.ParseRulesButton.Click += new System.EventHandler(this.ParseRulesButton_Click);
            // 
            // SourceLangLabel
            // 
            this.SourceLangLabel.AutoSize = true;
            this.SourceLangLabel.Location = new System.Drawing.Point(18, 34);
            this.SourceLangLabel.Name = "SourceLangLabel";
            this.SourceLangLabel.Size = new System.Drawing.Size(87, 13);
            this.SourceLangLabel.TabIndex = 13;
            this.SourceLangLabel.Text = "Исходный язык";
            // 
            // AllInputTextPanel
            // 
            this.AllInputTextPanel.Controls.Add(this.SourceLangLabel);
            this.AllInputTextPanel.Controls.Add(this.InputTextPanel);
            this.AllInputTextPanel.Controls.Add(this.InputTextLabel);
            this.AllInputTextPanel.Controls.Add(this.SourceLangTextBox);
            this.AllInputTextPanel.Location = new System.Drawing.Point(3, 3);
            this.AllInputTextPanel.Name = "AllInputTextPanel";
            this.AllInputTextPanel.Size = new System.Drawing.Size(278, 572);
            this.AllInputTextPanel.TabIndex = 14;
            // 
            // AllInputRulesPanel
            // 
            this.AllInputRulesPanel.Controls.Add(this.InputRulesLabel);
            this.AllInputRulesPanel.Controls.Add(this.InputRulesPanel);
            this.AllInputRulesPanel.Controls.Add(this.ParseRulesButton);
            this.AllInputRulesPanel.Location = new System.Drawing.Point(287, 3);
            this.AllInputRulesPanel.Name = "AllInputRulesPanel";
            this.AllInputRulesPanel.Size = new System.Drawing.Size(329, 572);
            this.AllInputRulesPanel.TabIndex = 15;
            // 
            // TranslationPanel
            // 
            this.TranslationPanel.Controls.Add(this.TransformStartButton);
            this.TranslationPanel.Location = new System.Drawing.Point(622, 3);
            this.TranslationPanel.Name = "TranslationPanel";
            this.TranslationPanel.Size = new System.Drawing.Size(70, 572);
            this.TranslationPanel.TabIndex = 16;
            // 
            // TransformStartButton
            // 
            this.TransformStartButton.Location = new System.Drawing.Point(3, 218);
            this.TransformStartButton.Name = "TransformStartButton";
            this.TransformStartButton.Size = new System.Drawing.Size(62, 26);
            this.TransformStartButton.TabIndex = 6;
            this.TransformStartButton.Text = "Go";
            this.TransformStartButton.UseVisualStyleBackColor = true;
            this.TransformStartButton.Click += new System.EventHandler(this.TransformStartButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TargetLangLabel);
            this.panel1.Controls.Add(this.OutputLabel);
            this.panel1.Controls.Add(this.OutputPanel);
            this.panel1.Controls.Add(this.TargetLangTextBox);
            this.panel1.Location = new System.Drawing.Point(698, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(275, 572);
            this.panel1.TabIndex = 17;
            // 
            // TargetLangLabel
            // 
            this.TargetLangLabel.AutoSize = true;
            this.TargetLangLabel.Location = new System.Drawing.Point(15, 34);
            this.TargetLangLabel.Name = "TargetLangLabel";
            this.TargetLangLabel.Size = new System.Drawing.Size(80, 13);
            this.TargetLangLabel.TabIndex = 14;
            this.TargetLangLabel.Text = "Целевой язык";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.87706F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.12294F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.Controls.Add(this.AllInputTextPanel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.AllInputRulesPanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TranslationPanel, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1026, 578);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 602);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Визуальный интерфейс компонента трансформации моделей";
            this.InputTextPanel.ResumeLayout(false);
            this.InputRulesPanel.ResumeLayout(false);
            this.OutputPanel.ResumeLayout(false);
            this.AllInputTextPanel.ResumeLayout(false);
            this.AllInputTextPanel.PerformLayout();
            this.AllInputRulesPanel.ResumeLayout(false);
            this.AllInputRulesPanel.PerformLayout();
            this.TranslationPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InputTextPanel;
        private System.Windows.Forms.Panel InputRulesPanel;
        private System.Windows.Forms.Panel OutputPanel;
        private System.Windows.Forms.Label InputTextLabel;
        private System.Windows.Forms.Label InputRulesLabel;
        private System.Windows.Forms.Label OutputLabel;
        private System.Windows.Forms.RichTextBox InputTestRichTextBox;
        private System.Windows.Forms.RichTextBox RulesInputRichTextBox;
        private System.Windows.Forms.RichTextBox OutputRichTextBox;
        private System.Windows.Forms.OpenFileDialog TextOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog RulesOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog OutputSaveFileDialog;
        private System.Windows.Forms.TextBox TargetLangTextBox;
        private System.Windows.Forms.TextBox SourceLangTextBox;
        private System.Windows.Forms.Button ParseRulesButton;
        private System.Windows.Forms.Label SourceLangLabel;
        private System.Windows.Forms.Panel AllInputTextPanel;
        private System.Windows.Forms.Panel AllInputRulesPanel;
        private System.Windows.Forms.Panel TranslationPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button TransformStartButton;
        private System.Windows.Forms.Label TargetLangLabel;
    }
}

