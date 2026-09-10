Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface ICommonERPCity

#Region "City"

    ''' <summary>
    ''' Lista todas las ciudades
    ''' </summary>
    ''' <returns>Lista de Ciudades</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllCity(session As SessionValues) As List(Of City)

    ''' <summary>
    ''' Obtiene una ciudad en especifico
    ''' </summary>
    ''' <param name="code">Codigo de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCity(ByVal code As String, session As SessionValues) As City

    ''' <summary>
    ''' Obtiene una ciudad en especifico
    ''' </summary>
    ''' <param name="code">Codigo de la ciudad</param>
    ''' <param name="idDepartamento">Id Departamento</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCityByDepartment(ByVal code As String, ByVal idDepartamento As Integer, session As SessionValues) As City

    ''' <summary>
    ''' Graba o actualiza una Ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveCity(ByVal city As City, ByVal session As SessionValues) As ActionResult(Of City)

    ''' <summary>
    ''' Elimina una ciudad
    ''' </summary>
    ''' <param name="city">Ciudad</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteCity(ByVal city As City, session As SessionValues) As ActionMessageResult(Of City)

    ''' <summary>
    ''' Metodo que lista todas las ciudades dependiendo del departanmento
    ''' </summary>
    ''' <param name="IdDepartment">Id del departamento</param>
    ''' <returns>Lista de ciudades</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllCitiesByIdDepartment(ByVal idDepartment As Integer, session As SessionValues) As List(Of City)

    ''' <summary>
    ''' Obtiene una ciudad especifica
    ''' </summary>
    ''' <param name="idCity">Id de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetCityById(ByVal idCity As Integer, session As SessionValues) As City

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="idCity">Id de la ciudad</param>
    ''' <returns>Ciudad</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateCity(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of City)
#End Region

End Interface
