namespace ImageRecoveryFromConv
{
    partial class MainWindow
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
            this.button_LoadImage = new System.Windows.Forms.Button();
            this.button_Recovery = new System.Windows.Forms.Button();
            this.groupBoxGaussFuncParameters = new System.Windows.Forms.GroupBox();
            this.numUpDownSlice = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numUpDownSigma = new System.Windows.Forms.NumericUpDown();
            this.numUpDownDiameter = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelDiameter = new System.Windows.Forms.Label();
            this.groupBoxRecoveryParameters = new System.Windows.Forms.GroupBox();
            this.numUpDownEpsRecovery = new System.Windows.Forms.NumericUpDown();
            this.numUpDownMaxIt = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numUpDownEpsSignal = new System.Windows.Forms.NumericUpDown();
            this.labelProcentSlice = new System.Windows.Forms.Label();
            this.progressBarImageRecovery = new System.Windows.Forms.ProgressBar();
            this.buttonShowRecoveryImage = new System.Windows.Forms.Button();
            this.groupBoxGaussFuncParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSlice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDiameter)).BeginInit();
            this.groupBoxRecoveryParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEpsRecovery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownMaxIt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEpsSignal)).BeginInit();
            this.SuspendLayout();
            // 
            // button_LoadImage
            // 
            this.button_LoadImage.Location = new System.Drawing.Point(12, 131);
            this.button_LoadImage.Name = "button_LoadImage";
            this.button_LoadImage.Size = new System.Drawing.Size(164, 54);
            this.button_LoadImage.TabIndex = 0;
            this.button_LoadImage.Text = "Загрузить изображение";
            this.button_LoadImage.UseVisualStyleBackColor = true;
            this.button_LoadImage.Click += new System.EventHandler(this.button_LoadImage_Click);
            // 
            // button_Recovery
            // 
            this.button_Recovery.Enabled = false;
            this.button_Recovery.Location = new System.Drawing.Point(182, 131);
            this.button_Recovery.Name = "button_Recovery";
            this.button_Recovery.Size = new System.Drawing.Size(168, 54);
            this.button_Recovery.TabIndex = 1;
            this.button_Recovery.Text = "Восстановить изображение";
            this.button_Recovery.UseVisualStyleBackColor = true;
            this.button_Recovery.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // groupBoxGaussFuncParameters
            // 
            this.groupBoxGaussFuncParameters.Controls.Add(this.numUpDownSlice);
            this.groupBoxGaussFuncParameters.Controls.Add(this.label2);
            this.groupBoxGaussFuncParameters.Controls.Add(this.numUpDownSigma);
            this.groupBoxGaussFuncParameters.Controls.Add(this.numUpDownDiameter);
            this.groupBoxGaussFuncParameters.Controls.Add(this.label1);
            this.groupBoxGaussFuncParameters.Controls.Add(this.labelDiameter);
            this.groupBoxGaussFuncParameters.Location = new System.Drawing.Point(12, 12);
            this.groupBoxGaussFuncParameters.Name = "groupBoxGaussFuncParameters";
            this.groupBoxGaussFuncParameters.Size = new System.Drawing.Size(250, 112);
            this.groupBoxGaussFuncParameters.TabIndex = 2;
            this.groupBoxGaussFuncParameters.TabStop = false;
            this.groupBoxGaussFuncParameters.Text = "Параметры гауссового купола";
            this.groupBoxGaussFuncParameters.Enter += new System.EventHandler(this.groupBoxGaussFuncParameters_Enter);
            // 
            // numUpDownSlice
            // 
            this.numUpDownSlice.DecimalPlaces = 2;
            this.numUpDownSlice.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numUpDownSlice.Location = new System.Drawing.Point(132, 77);
            this.numUpDownSlice.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownSlice.Name = "numUpDownSlice";
            this.numUpDownSlice.Size = new System.Drawing.Size(67, 22);
            this.numUpDownSlice.TabIndex = 5;
            this.numUpDownSlice.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Срез:";
            // 
            // numUpDownSigma
            // 
            this.numUpDownSigma.Location = new System.Drawing.Point(132, 49);
            this.numUpDownSigma.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownSigma.Name = "numUpDownSigma";
            this.numUpDownSigma.Size = new System.Drawing.Size(67, 22);
            this.numUpDownSigma.TabIndex = 3;
            this.numUpDownSigma.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // numUpDownDiameter
            // 
            this.numUpDownDiameter.Location = new System.Drawing.Point(132, 21);
            this.numUpDownDiameter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownDiameter.Name = "numUpDownDiameter";
            this.numUpDownDiameter.Size = new System.Drawing.Size(67, 22);
            this.numUpDownDiameter.TabIndex = 2;
            this.numUpDownDiameter.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sigma:";
            // 
            // labelDiameter
            // 
            this.labelDiameter.AutoSize = true;
            this.labelDiameter.Location = new System.Drawing.Point(6, 23);
            this.labelDiameter.Name = "labelDiameter";
            this.labelDiameter.Size = new System.Drawing.Size(67, 16);
            this.labelDiameter.TabIndex = 0;
            this.labelDiameter.Text = "Диаметр:";
            // 
            // groupBoxRecoveryParameters
            // 
            this.groupBoxRecoveryParameters.Controls.Add(this.numUpDownEpsRecovery);
            this.groupBoxRecoveryParameters.Controls.Add(this.numUpDownMaxIt);
            this.groupBoxRecoveryParameters.Controls.Add(this.label4);
            this.groupBoxRecoveryParameters.Controls.Add(this.label3);
            this.groupBoxRecoveryParameters.Controls.Add(this.numUpDownEpsSignal);
            this.groupBoxRecoveryParameters.Controls.Add(this.labelProcentSlice);
            this.groupBoxRecoveryParameters.Location = new System.Drawing.Point(268, 12);
            this.groupBoxRecoveryParameters.Name = "groupBoxRecoveryParameters";
            this.groupBoxRecoveryParameters.Size = new System.Drawing.Size(435, 112);
            this.groupBoxRecoveryParameters.TabIndex = 3;
            this.groupBoxRecoveryParameters.TabStop = false;
            this.groupBoxRecoveryParameters.Text = "Параметры восстановления";
            // 
            // numUpDownEpsRecovery
            // 
            this.numUpDownEpsRecovery.DecimalPlaces = 2;
            this.numUpDownEpsRecovery.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numUpDownEpsRecovery.Location = new System.Drawing.Point(356, 82);
            this.numUpDownEpsRecovery.Name = "numUpDownEpsRecovery";
            this.numUpDownEpsRecovery.Size = new System.Drawing.Size(64, 22);
            this.numUpDownEpsRecovery.TabIndex = 5;
            this.numUpDownEpsRecovery.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // numUpDownMaxIt
            // 
            this.numUpDownMaxIt.Location = new System.Drawing.Point(356, 54);
            this.numUpDownMaxIt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownMaxIt.Name = "numUpDownMaxIt";
            this.numUpDownMaxIt.Size = new System.Drawing.Size(64, 22);
            this.numUpDownMaxIt.TabIndex = 4;
            this.numUpDownMaxIt.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Точность восстановления:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(219, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Максимальное кол-во итераций:";
            // 
            // numUpDownEpsSignal
            // 
            this.numUpDownEpsSignal.DecimalPlaces = 2;
            this.numUpDownEpsSignal.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numUpDownEpsSignal.Location = new System.Drawing.Point(356, 25);
            this.numUpDownEpsSignal.Name = "numUpDownEpsSignal";
            this.numUpDownEpsSignal.Size = new System.Drawing.Size(64, 22);
            this.numUpDownEpsSignal.TabIndex = 1;
            this.numUpDownEpsSignal.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // labelProcentSlice
            // 
            this.labelProcentSlice.AutoSize = true;
            this.labelProcentSlice.Location = new System.Drawing.Point(6, 27);
            this.labelProcentSlice.Name = "labelProcentSlice";
            this.labelProcentSlice.Size = new System.Drawing.Size(320, 16);
            this.labelProcentSlice.TabIndex = 0;
            this.labelProcentSlice.Text = "Доля сигнала, не учитываемая при делении (%):";
            // 
            // progressBarImageRecovery
            // 
            this.progressBarImageRecovery.Location = new System.Drawing.Point(12, 191);
            this.progressBarImageRecovery.Name = "progressBarImageRecovery";
            this.progressBarImageRecovery.Size = new System.Drawing.Size(697, 23);
            this.progressBarImageRecovery.TabIndex = 4;
            // 
            // buttonShowRecoveryImage
            // 
            this.buttonShowRecoveryImage.Location = new System.Drawing.Point(356, 131);
            this.buttonShowRecoveryImage.Name = "buttonShowRecoveryImage";
            this.buttonShowRecoveryImage.Size = new System.Drawing.Size(208, 54);
            this.buttonShowRecoveryImage.TabIndex = 5;
            this.buttonShowRecoveryImage.Text = "Показать восстановленное изображение";
            this.buttonShowRecoveryImage.UseVisualStyleBackColor = true;
            this.buttonShowRecoveryImage.Click += new System.EventHandler(this.buttonShowRecoveryImage_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 225);
            this.Controls.Add(this.buttonShowRecoveryImage);
            this.Controls.Add(this.progressBarImageRecovery);
            this.Controls.Add(this.groupBoxRecoveryParameters);
            this.Controls.Add(this.groupBoxGaussFuncParameters);
            this.Controls.Add(this.button_Recovery);
            this.Controls.Add(this.button_LoadImage);
            this.Name = "MainWindow";
            this.Text = "Восстановление сигнала из свертки";
            this.groupBoxGaussFuncParameters.ResumeLayout(false);
            this.groupBoxGaussFuncParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSlice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDiameter)).EndInit();
            this.groupBoxRecoveryParameters.ResumeLayout(false);
            this.groupBoxRecoveryParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEpsRecovery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownMaxIt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownEpsSignal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_LoadImage;
        private System.Windows.Forms.Button button_Recovery;
        private System.Windows.Forms.GroupBox groupBoxGaussFuncParameters;
        private System.Windows.Forms.NumericUpDown numUpDownSigma;
        private System.Windows.Forms.NumericUpDown numUpDownDiameter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelDiameter;
        private System.Windows.Forms.NumericUpDown numUpDownSlice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxRecoveryParameters;
        private System.Windows.Forms.NumericUpDown numUpDownEpsSignal;
        private System.Windows.Forms.Label labelProcentSlice;
        private System.Windows.Forms.ProgressBar progressBarImageRecovery;
        private System.Windows.Forms.Button buttonShowRecoveryImage;
        private System.Windows.Forms.NumericUpDown numUpDownEpsRecovery;
        private System.Windows.Forms.NumericUpDown numUpDownMaxIt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}

