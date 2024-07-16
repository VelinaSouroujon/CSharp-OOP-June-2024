using System.Collections.Generic;

namespace P02.Graphic_Editor
{
    class Program
    {
        static void Main()
        {
            List<IShape> shapes = new List<IShape>()
            {
                new Circle(),
                new Rectangle(),
                new Square()
            };

            IWriter writer = new ConsoleWriter();
            GraphicEditor editor = new GraphicEditor(writer);

            foreach (IShape shape in shapes)
            {
                editor.DrawShape(shape);
            }
        }
    }
}
