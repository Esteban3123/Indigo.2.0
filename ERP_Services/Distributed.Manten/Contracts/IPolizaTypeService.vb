#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IPolizaTypeService


    <OperationContract()> _
    Function ListAllPolizaType(Empresa As String) As List(Of PolizaType)


    <OperationContract()> _
    Function DeletePolizaType(Empresa As String, ByVal PolizaType As PolizaType, ByVal audit As AuditMessage) As ActionResult


    <OperationContract()> _
    Function SavePolizaType(Empresa As String, ByVal PolizaType As PolizaType, ByVal audit As AuditMessage) As ActionResult(Of PolizaType)

    <OperationContract()> _
    Function GetPolizaType(Empresa As String, ByVal codePolizaType As String, ByVal audit As AuditMessage) As PolizaType
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStatePolizaType(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PolizaType)
End Interface
