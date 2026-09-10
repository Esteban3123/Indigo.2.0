
#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountManagementServiceManagementAreas

#Region "Methods"

    ''' <summary>
    ''' Lista todas las condiciones de venta por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    <OperationContract()>
    Function ListManagementAreasByUserCode(userCode As String) As List(Of ManagementAreas)

    ''' <summary>
    ''' elimina una condicion de venta
    ''' </summary>
    ''' <param name="ManagementAreas"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteManagementAreas(ManagementAreas As ManagementAreas, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda o actualiza una condicion de venta
    ''' </summary>
    ''' <param name="ManagementAreas"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveManagementAreas(ManagementAreas As ManagementAreas, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' Obtiene una condicion de venta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetManagementAreasById(id As Integer, audit As AuditMessage) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' Obtiene una condicion de venta por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetManagementAreasByCode(code As String, audit As AuditMessage) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateManagementAreas(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ManagementAreas)

    ''' <summary>
    ''' Obtiene todas las areas de gestión activas
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllManagementAreas() As ActionResult(Of List(Of ManagementAreas))


#End Region
End Interface
