''' <summary>
''' Encapsula y administra los datos de configuración almacenados en el archivo de configuración
''' </summary>
Public Class ConfigurationFile

#Region "Consts"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el nombre del contenedor a inyectar
    ''' </summary>
    Public Const SESS_CONTAINER As String = "_ContainerName_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el nombre del contenedor HIS a inyectar
    ''' </summary>
    Public Const SESS_CONTAINER_HIS As String = "_ContainerHISName_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el nombre del contenedor de interoperabilidad de costros a inyectar
    ''' </summary>
    Public Const SESS_CONTAINER_INTEROPCOST As String = "_ContainerInteropCostName_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el id de la secuencia a usar
    ''' </summary>
    Public Const SESS_IDSEQUENSE As String = "_IdSequense_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para la secuencia de cabecera a usar
    ''' </summary>
    Public Const SESS_SEQUENCEC As String = "_SequenceC_"

    ''' <summary>
    ''' Nombre de la clave del valor de cabecera enviado
    ''' para el mensage de auditoria
    ''' </summary>
    Public Const SESS_AUDITMESSAGE As String = "_AuditMessage_"

    ''' <summary>
    ''' Espacio de nombre usado para almacenar los valores
    ''' enviados por la cabecera
    ''' </summary>
    Public Const SESS_NAME_SPACE As String = "ns"

    ''' <summary>
    ''' Nombre de la cadena de conexion a la base transaccional
    ''' </summary>
    Public Const CONX_GENESIS As String = "CONX_GENESIS"

    ''' <summary>
    ''' Nombre del parametro en web.config que contiene el nombre del contenedor de seguridad
    ''' </summary>
    Public Const SECURITY_CONTAINER_PARAMETER_NAME As String = "containerSecurity"

    ''' <summary>
    ''' Nombre del parametro en web.config que contiene el nombre del contenedor de Indigo Vie Cloud Platform
    ''' </summary>
    Public Const HIS_CONTAINER_PARAMETER_NAME As String = "containerHis"

    ''' <summary>
    ''' Nombre del parametro en web.config que contiene el nombre del contenedor de Indigo Vie Cloud Platform
    ''' </summary>
    Public Const INTEROP_COST_CONTAINER_PARAMETER_NAME As String = "containerInteropCost"

    ''' <summary>
    ''' Nombre de la cadena de conexion a la base de reportes
    ''' </summary>
    Public Const CONX_GENESIS_REPORTS As String = "CONX_GENESIS_REPORTS"

    ''' <summary>
    ''' Nombre del contenedor de la DB Az Cosmos DB
    ''' </summary>
    Public Const SESS_CONTAINER_AZCOS As String = "_CosmosDbContainer_"

    ''' <summary>
    ''' Nombre de la Base de datos de Az Cosmos DB
    ''' </summary>
    Public Const SESS_DATABASE_AZCOS As String = "_CosmosDbDatabase_"

    ''' <summary>
    ''' Server de la AZ cosmos
    ''' </summary>
    Public Const CONX_DB_URI_AZCOS As String = "CosmosDbURI"

    ''' <summary>
    ''' Key de la Az Cosmo 
    ''' </summary>
    Public Const CONX_DB_KEY_AZCOS As String = "CosmosDbKEY"

    ''' <summary>
    ''' Nombre del contendeor del blob storage
    ''' </summary>
    Public Const CONX_BLOB_CONTAINER_NAME_AZ As String = "BlobContainerName"

    ''' <summary>
    ''' Cadena de conexion del blob storage
    ''' </summary>
    Public Const CONX_BLOB_STRING_AZ As String = "AzureBlobConnectionString"

    ''' <summary>
    ''' Versión de Indigo del cliente
    ''' </summary>
    Public Const INDIGO_VERSION As String = "IndigoVersion"
#End Region

End Class
