namespace Laboratorio_4_DSIV
{
    partial class Cliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblFarmacia = new System.Windows.Forms.Label();
            this.btnCarrito = new System.Windows.Forms.Button();
            this.flowLayoutPanelCatalogo = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Red;
            this.btnLogout.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogout.Location = new System.Drawing.Point(676, 13);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(87, 34);
            this.btnLogout.TabIndex = 8;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblFarmacia
            // 
            this.lblFarmacia.AutoSize = true;
            this.lblFarmacia.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFarmacia.Location = new System.Drawing.Point(304, 12);
            this.lblFarmacia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFarmacia.Name = "lblFarmacia";
            this.lblFarmacia.Size = new System.Drawing.Size(119, 35);
            this.lblFarmacia.TabIndex = 7;
            this.lblFarmacia.Text = "Farmacia";
            // 
            // btnCarrito
            // 
            this.btnCarrito.BackColor = System.Drawing.Color.Gold;
            this.btnCarrito.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarrito.Location = new System.Drawing.Point(568, 12);
            this.btnCarrito.Margin = new System.Windows.Forms.Padding(4);
            this.btnCarrito.Name = "btnCarrito";
            this.btnCarrito.Size = new System.Drawing.Size(100, 34);
            this.btnCarrito.TabIndex = 6;
            this.btnCarrito.Text = "Carrito";
            this.btnCarrito.UseVisualStyleBackColor = false;
            this.btnCarrito.Click += new System.EventHandler(this.btnCarrito_Click);
            // 
            // flowLayoutPanelCatalogo
            // 
            this.flowLayoutPanelCatalogo.AutoScroll = true;
            this.flowLayoutPanelCatalogo.BackColor = System.Drawing.Color.Teal;
            this.flowLayoutPanelCatalogo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelCatalogo.Location = new System.Drawing.Point(0, 60);
            this.flowLayoutPanelCatalogo.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanelCatalogo.Name = "flowLayoutPanelCatalogo";
            this.flowLayoutPanelCatalogo.Size = new System.Drawing.Size(800, 390);
            this.flowLayoutPanelCatalogo.TabIndex = 5;
            // 
            // Cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblFarmacia);
            this.Controls.Add(this.btnCarrito);
            this.Controls.Add(this.flowLayoutPanelCatalogo);
            this.Name = "Cliente";
            this.Text = "Cliente";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblFarmacia;
        private System.Windows.Forms.Button btnCarrito;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCatalogo;
    }
}