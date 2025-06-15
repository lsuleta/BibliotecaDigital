using System.Runtime.CompilerServices;

namespace BibliotecaDigital
{
    public abstract class Material
    {
        public string titulo { get; protected set; }
        public string autor { get; protected set; }
        public int año { get; protected set; }
        public bool disponible { get; protected set; }

        public Material(string titulo, string autor, int año)
        {
            this.titulo = titulo;
            this.autor = autor;
            this.año = año;
            this.disponible = false;
        }
    }

    public class Libro : Material, IPrestable
    {
        private string genero;
        private int paginas;
        public Libro(string titulo, string autor, int año, string genero, int paginas)
            : base(titulo, autor, año)
        {
            this.genero = genero;
            this.paginas = paginas;
        }
        public void MostrarInformacion()
        {
            Console.WriteLine($"Título: {titulo}, Autor: {autor}, Año: {año}, Género: {genero}, Páginas: {paginas}");
        }

        public override bool Prestar(string usuario) 
        { 
            if (!EstaDisponible)
            {
                throw new MaterialNoDisponibleException("El libro no está disponible para préstamo.");
            }
            disponible = true;

            return true;

        }

        public override bool Devolver()
        {
            if (!disponible) { return false; }

            disponible = false;            
        }

        public override bool EstaDisponible()
        {
            return !disponible;
        }
       

    }

    public class Revista : Material, IPrestable
    {
        private string periodicidad;
        private int numeroEdicion;
        public Revista(string titulo, string autor, int año, string periodicidad, int numeroEdicion)
            : base(titulo, autor, año)
        {
            this.periodicidad = periodicidad;
            this.numeroEdicion = numeroEdicion;
        }
        public void MostrarInformacion()
        {
            Console.WriteLine($"Título: {titulo}, Autor: {autor}, Año: {año}, Periodicidad: {periodicidad}, Número de Edición: {numeroEdicion}");
        }
        public override bool Prestar(string usuario)
        {
            if (!EstaDisponible)
            {
                throw new MaterialNoDisponibleException("El libro no está disponible para préstamo.");
            }
            disponible = true;

            return true;

        }

        public override bool Devolver()
        {
            if (!disponible) { return false; }

            disponible = false;
        }

        public override bool EstaDisponible()
        {
            return !disponible;
        }

    }

    public class AudioLibro : Material
    {
        private int duracion; // Duración en minutos
        private string narrador;
        public AudioLibro(string titulo, string autor, int año, int duracion, string narrador)
            : base(titulo, autor, año)
        {
            this.duracion = duracion;
            this.narrador = narrador;
        }
        public void MostrarInformacion()
        {
            Console.WriteLine($"Título: {titulo}, Autor: {autor}, Año: {año}, Duración: {duracion} minutos, Narrador: {narrador}");
        }
    }

    public class DVD : Material
    {
        private string genero;
        private int duracion; // Duración en minutos
        public DVD(string titulo, string autor, int año, string genero, int duracion)
            : base(titulo, autor, año)
        {
            this.genero = genero;
            this.duracion = duracion;
        }
        public void MostrarInformacion()
        {
            Console.WriteLine($"Título: {titulo}, Autor: {autor}, Año: {año}, Género: {genero}, Duración: {duracion} minutos");
        }
    }

    public interface IPrestable
    {
        void Prestar(string usuario);
        void Devolver();
        void estaDisponible();
    }
    public class MaterialNoDisponibleException : Exception
    {
        public MaterialNoDisponibleException(string mensaje) : base(mensaje) { }
    }
    public class Biblioteca
    {
        public List<Libro> libros;
        public List<Revista> revistas;
        public List<AudioLibro> audioLibros;
        public List<DVD> dvds;

        public Biblioteca()
        {
            libros = new List<Libro>();
            revistas = new List<Revista>();
            audioLibros = new List<AudioLibro>();
            dvds = new List<DVD>();
        }

        public void AgregarLibro(Libro libro)
        {
            if (libro == null)
            {
                throw new ArgumentNullException(nameof(libro), "El libro no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(libro.titulo) || string.IsNullOrWhiteSpace(libro.autor) || libro.año <= 0)
            {
                throw new ArgumentException("Los datos del libro son inválidos.");
            }
            if (string.IsNullOrWhiteSpace(libro.genero) || libro.paginas <= 0)
            {
                throw new ArgumentException("Los datos del libro son inválidos");
            }
            libros.Add(libro);
        }

        public void AgregarRevista(Revista revista)
        {
            if(revista == null)
            {
                throw new ArgumentNullException(nameof(revista), "La revista no puede ser nula.");
            }
            if (string.IsNullOrWhiteSpace(revista.titulo) || string.IsNullOrWhiteSpace(revista.autor) || revista.año <= 0 ||
                string.IsNullOrWhiteSpace(revista.periodicidad) || string.IsNullOrWhiteSpace(revista.numeroEdicion))
            {
                throw new ArgumentException("Los datos de la revista son inválidos.");
            }
            revistas.Add(revista);
        }

        public void AgregarAudioLibro(AudioLibro audioLibro)
        {
            if (audioLibro == null)
            {
                throw new ArgumentNullException(nameof(audioLibro), "El audiolibro no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(audioLibro.titulo) || string.IsNullOrWhiteSpace(audioLibro.autor) || audioLibro.año <= 0
                || audioLibro.duracion <= 0 || string.IsNullOrWhiteSpace(audioLibro.narrador))
            {
                throw new ArgumentException("Los datos del audiolibro son inválidos.");
            }
            audioLibros.Add(audioLibro);
        }

        public void AgregarDVD(DVD dvd)
        {
            if (dvd == null)
            {
                throw new ArgumentNullException(nameof(dvd), "El DVD no puede ser nulo.");
            }
            if (string.IsNullOrWhiteSpace(dvd.titulo) || string.IsNullOrWhiteSpace(dvd.autor) || dvd.año <= 0 
                || string.IsNullOrWhiteSpace(dvd.genero) || string.IsNullOrWhiteSpace(dvd.duracion))
            {
                throw new ArgumentException("Los datos del DVD son inválidos.");
            }
            dvds.Add(dvd);
        }
    }


}
