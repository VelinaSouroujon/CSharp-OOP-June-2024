using System;

namespace P02.Graphic_Editor
{
    public class GraphicEditor
    {
        private IWriter writer;

        public GraphicEditor(IWriter writer)
        {
            this.writer = writer;
        }

        public void DrawShape(IShape shape)
        {
            writer.WriteLine(shape.GetShape());
        }
    }
}
