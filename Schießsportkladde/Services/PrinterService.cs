using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schießsportkladde.Services
{    
    public class PrinterService
    {
        private PrintDocument printDocument;
        private DataGridView dataGridView;

        public PrinterService()
        {
            printDocument = new PrintDocument();
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
        }

        public void Print(DataGridView dataGridViewToPrint)
        {
            dataGridView = dataGridViewToPrint;

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if(printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial",12);
            Brush brush = Brushes.Black;
            float startX = e.MarginBounds.Left;
            float startY = e.MarginBounds.Top;
            float rowHeight = font.GetHeight(e.Graphics);

            //Print DataGridView headers
            for(int i = 0; i < dataGridView.Columns.Count; i++)
            {
                // Get the actual width of the column
                float coloumnWidth = dataGridView.Columns[i].Width;
                
                e.Graphics.DrawString(dataGridView.Columns[i].HeaderText, font, brush, startX, startY);

                // Move startX for the next cell in the row
                startX += coloumnWidth;
            }

            // Move to next Row
            startY += rowHeight;
            // Reset startX for row Content
            startX = e.MarginBounds.Left;

            //Print DataGridView Rows
            for (int rowIndex = 0; rowIndex < dataGridView.Rows.Count; rowIndex++)
            {
                DataGridViewRow row = dataGridView.Rows[rowIndex];
                for (int cellIndex = 0; cellIndex < row.Cells.Count; cellIndex++)
                {
                    // Get the actual width of the column
                    float columnWidth = dataGridView.Columns[cellIndex].Width; 

                    e.Graphics.DrawString(row.Cells[cellIndex].Value?.ToString(), font, brush, startX, startY);

                    // Move startX for the next cell in the row
                    startX += columnWidth; 
                }
                startY += rowHeight; // Move to the next row
                startX = e.MarginBounds.Left; // Reset startX for the next row
            }
        }
    }
}
