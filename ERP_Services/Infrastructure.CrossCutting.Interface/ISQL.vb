Imports System.ServiceModel
Imports System.Data.SqlClient
Imports System.Runtime.Serialization


Public Interface ISQL


#Region "Configuracion General y propiedades"


    ''' <summary>
    ''' Propiedad de Tipo SQlTransacion para Manejo de Transaciones en SQL
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IndigoTransaction() As SqlTransaction

    ''' <summary>
    ''' define si la conexion esta bajo una transaccion
    ''' </summary>
    ''' <value><c>true</c> if [en transaccion]; otherwise, <c>false</c>.</value>
    Property InTransaction As Boolean

    ''' <summary>
    ''' Propiedad que contiene la Conexion Web
    ''' </summary>
    ''' <value>Conexion Web</value>
    ''' <returns>Devuelve la Conexion Web</returns>
    ''' <remarks></remarks>
    Property sqlWebConection() As SqlConnection

    ''' <summary>
    ''' Conectar a SQL
    ''' </summary>
    <OperationContract()>
    Sub Connect(comnpany As String)
    'cargo la configuracion)

#End Region

#Region "Enumeraciones"


    ''' <summary>
    ''' enumeracion para la funcion de concatenar
    ''' </summary>
    <DataContract(), Flags()> _
    Enum Direccion As Integer
        ''' <summary>
        ''' concatenar a la derecha
        ''' </summary>
        <EnumMember()> Derecha = 1
        ''' <summary>
        ''' concatenar a la izquierda
        ''' </summary>
        <EnumMember()> Izquierda = 2
    End Enum


    ''' <summary>
    ''' Enumeracion necesaria para obtener la version de DGH a la que se esta conectado.
    ''' </summary>
    <DataContract(), Flags()> _
    Enum eVersionDGH As Integer
        ''' <summary>
        ''' Version ERP en Fox Pro
        ''' </summary>
        <EnumMember()> ERPFox = 1
        ''' <summary>
        ''' Version de ERP en .Net
        ''' </summary>
        <EnumMember()> ERPNet = 2
    End Enum



#End Region

#Region "Metodos"



    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL
    ''' </summary>
    ''' <param name="Command">Sentencia SQL</param>
    ''' <returns>True -> Correcto</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ExecuteCommand(ByVal Command As String) As Boolean

    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL, y Devover una Tabla
    ''' </summary>
    ''' <param name="Command">Sentencia SQL</param>
    ''' <returns>True -> Correcto</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ExecuteCommand_Data(ByVal Command As String) As DataTable

    ''' <summary>
    ''' Funcion para ejecutar un a sentencia SQl y devolver un count
    ''' </summary>
    ''' <param name="Command">Sentencia SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ExecuteCommand_Count(ByVal Command As String) As Integer


    ''' <summary>
    ''' funcion para concatenar cadenas.
    ''' </summary>
    ''' <param name="strCaracter">caracter a concatenar.</param>
    ''' <param name="strCadena">cadena a concatenar.</param>
    ''' <param name="intNumero">numero de caracteres.</param>
    ''' <param name="Dir">la Direccion.</param>
    ''' <param name="strPrefijo">el prefijo.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function fncConcatenar(ByVal strCaracter As String, ByVal strCadena As String, ByVal intNumero As Integer, ByVal Dir As [Direccion], Optional ByVal strPrefijo As String = "") As String

    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL con parametros
    ''' </summary>
    ''' <param name="Command">Sentencia SQL</param>
    ''' <param name="ClearParams"></param>
    ''' <returns>True -> Correcto</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ExecuteCommandParams(ByVal Command As String, Optional ByVal ClearParams As Boolean = True) As Boolean

    ''' <summary>
    ''' Adicion de parametro a lista
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    <OperationContract()>
    Sub AddParam(key As String, type As SqlDbType, value As Object)

    ''' <summary>
    ''' Funcion Para ejecutar Una Sentencia SQL con parametros, y Devover una Tabla
    ''' </summary>
    ''' <param name="Command"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ExecuteCommandParams_Data(ByVal Command As String) As DataTable

#End Region


End Interface
