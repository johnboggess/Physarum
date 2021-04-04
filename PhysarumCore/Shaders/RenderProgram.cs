using System;
using System.Collections.Generic;
using System.Text;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using ObjectTK.Shaders.Sources;
using ObjectTK.Shaders.Variables;
using ObjectTK.Textures;
using ObjectTK.Buffers;

namespace PhysarumCore.Shaders
{
    [VertexShaderSource("RenderShader.Vertex")]
    [FragmentShaderSource("RenderShader.Fragment")]
    public class RenderProgram : ObjectTK.Shaders.Program
    {
        [VertexAttrib(3, VertexAttribPointerType.Float)]
        public VertexAttrib InPosition { get; protected set; }

        [VertexAttrib(2, VertexAttribPointerType.Float)]
        public VertexAttrib InUV { get; protected set; }

        public TextureUniform<Texture2D> Texture { get; set; }



        internal VertexArray _screen;
        internal Buffer<Vector2> uvBuffer;
        internal Buffer<Vector3> vertexBuffer;
        internal Buffer<int> indexBuffer;

        public static RenderProgram Create()
        {
            RenderProgram _renderProgram = ObjectTK.Shaders.ProgramFactory.Create<RenderProgram>();
            _renderProgram.Use();
            _renderProgram.Texture.Set(TextureUnit.Texture0);

            Vector3[] vertices = new Vector3[] { new Vector3(-1, -1, 0), new Vector3(-1, 1, 0), new Vector3(1, -1, 0), new Vector3(1, 1, 0) };
            _renderProgram.vertexBuffer = new Buffer<Vector3>();
            _renderProgram.vertexBuffer.Init(BufferTarget.ArrayBuffer, vertices);

            Vector2[] uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
            _renderProgram.uvBuffer = new Buffer<Vector2>();
            _renderProgram.uvBuffer.Init(BufferTarget.ArrayBuffer, uv);

            _renderProgram.indexBuffer = new Buffer<int>();
            _renderProgram.indexBuffer.Init(BufferTarget.ElementArrayBuffer, new int[] { 0, 1, 2, 3, 2, 1 });

            _renderProgram._screen = new VertexArray();
            _renderProgram._screen.Bind();
            _renderProgram._screen.BindAttribute(_renderProgram.InPosition, _renderProgram.vertexBuffer);
            _renderProgram._screen.BindAttribute(_renderProgram.InUV, _renderProgram.uvBuffer);
            _renderProgram._screen.BindElementBuffer(_renderProgram.indexBuffer);

            return _renderProgram;
        }

        public void Draw()
        {
            _screen.DrawElements(PrimitiveType.Triangles, indexBuffer.ElementCount);
        }

    }
}
