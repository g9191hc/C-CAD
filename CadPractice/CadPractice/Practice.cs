using IntelliCAD.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teigha.Colors;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using Teigha.Runtime;

namespace CadPractice
{
    public class Practice
    {
        [CommandMethod("MYLINE")]
        public void DrawMyLine()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                Point3d start = new Point3d(0, 0, 0);
                Point3d end = new Point3d(10, 10, 0);

                Line line = new Line(start, end);
                line.Color = Color.FromColorIndex(ColorMethod.ByColor, 3); // Set the color to red
                line.Linetype = "Continuous"; // Set the linetype to continuous
                line.Layer = db.Clayer.ToString(); // Set the layer to the current layer

                btr.AppendEntity(line);
                tr.AddNewlyCreatedDBObject(line, true);

                tr.Commit();
            }
        }
    }
}
