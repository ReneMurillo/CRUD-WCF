using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MiWS
{
    [ServiceContract]
    public interface IWSPersonas
    {
        [OperationContract]
        int NuevaPersona(Personas persona);

        [OperationContract]
        int EditarPersona(Personas persona);

        [OperationContract]
        int EliminarPersona(int idPersona);

        [OperationContract]
        Personas buscarPersona(int idPersona);

        [OperationContract]
        List<Personas> listarPersonas();
    }

    [DataContract]
    public class Persona: BaseRespuesta
    {
        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public int Edad { get; set; }
    }

    [DataContract]
    public class BaseRespuesta
    {
        [DataMember]
        public string MensajeRespuesta { get; set; }

        [DataMember]
        public string Error { get; set; }
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class Personas
    {
        int _Id;
        string _Nombre;
        string _Apellido;
        int _Edad;
        string _DocumentoIdentificacion;

        [DataMember]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        [DataMember]
        public string Apellido
        {
            get { return _Apellido; }
            set { _Apellido = value; }
        }

        [DataMember]
        public int Edad
        {
            get { return _Edad; }
            set { _Edad = value; }
        }

        [DataMember]
        public string DocumentoIdentificacion
        {
            get { return _DocumentoIdentificacion; }
            set { _DocumentoIdentificacion = value; }
        }
    }
}
