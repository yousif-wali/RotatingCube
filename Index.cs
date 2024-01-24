using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private GLControl glControl;
    private float rotation = 0.0f;

    public Form1()
    {
        InitializeComponent();
        glControl = new GLControl();
        glControl.Dock = DockStyle.Fill;
        this.Controls.Add(glControl);

        glControl.Load += glControl_Load;
        glControl.Resize += glControl_Resize;
        glControl.Paint += glControl_Paint;
        Application.Idle += Application_Idle;
    }

    private void glControl_Load(object sender, EventArgs e)
    {
        GL.ClearColor(Color4.CornflowerBlue);
        GL.Enable(EnableCap.DepthTest);
    }

    private void glControl_Resize(object sender, EventArgs e)
    {
        GL.Viewport(glControl.ClientRectangle);
        Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, glControl.Width / (float)glControl.Height, 1.0f, 64.0f);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadMatrix(ref projection);
    }

    private void glControl_Paint(object sender, PaintEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        Matrix4 modelview = Matrix4.LookAt(Vector3.UnitZ * 3, Vector3.Zero, Vector3.UnitY);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref modelview);

        GL.Rotate(rotation, Vector3.UnitY);
        DrawCube();

        glControl.SwapBuffers();
    }

    private void Application_Idle(object sender, EventArgs e)
    {
        rotation += 1.0f;
        glControl.Invalidate();
    }

    private void DrawCube()
    {
    // Define vertices for a cube
    float[] vertices = {
        // Front face
        -1.0f, -1.0f,  1.0f, // Bottom-left
         1.0f, -1.0f,  1.0f, // Bottom-right
         1.0f,  1.0f,  1.0f, // Top-right
        -1.0f,  1.0f,  1.0f, // Top-left

        // Back face
        -1.0f, -1.0f, -1.0f,
        -1.0f,  1.0f, -1.0f,
         1.0f,  1.0f, -1.0f,
         1.0f, -1.0f, -1.0f,

        // Top face
        -1.0f,  1.0f, -1.0f,
        -1.0f,  1.0f,  1.0f,
         1.0f,  1.0f,  1.0f,
         1.0f,  1.0f, -1.0f,

        // Bottom face
        -1.0f, -1.0f, -1.0f,
         1.0f, -1.0f, -1.0f,
         1.0f, -1.0f,  1.0f,
        -1.0f, -1.0f,  1.0f,

        // Right face
         1.0f, -1.0f, -1.0f,
         1.0f,  1.0f, -1.0f,
         1.0f,  1.0f,  1.0f,
         1.0f, -1.0f,  1.0f,

        // Left face
        -1.0f, -1.0f, -1.0f,
        -1.0f, -1.0f,  1.0f,
        -1.0f,  1.0f,  1.0f,
        -1.0f,  1.0f, -1.0f,
    };

    // Define indices for the cube (two triangles per face)
    int[] indices = {
        // Front face
        0, 1, 2,
        2, 3, 0,

        // Back face
        4, 5, 6,
        6, 7, 4,

        // Top face
        8, 9, 10,
        10, 11, 8,

        // Bottom face
        12, 13, 14,
        14, 15, 12,

        // Right face
        16, 17, 18,
        18, 19, 16,

        // Left face
        20, 21, 22,
        22, 23, 20,
    };

    // Enable vertex array
    GL.EnableClientState(ArrayCap.VertexArray);

    // Pass vertex pointer
    GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
    
    // Draw cube
    GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, indices);

    // Disable vertex array
    GL.DisableClientState(ArrayCap.VertexArray);
    }

}

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}
