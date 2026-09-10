#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IPartService

    <OperationContract()> _
    Function ListAllPart(Empresa As String) As List(Of Part)


    <OperationContract()> _
    Function DeletePart(Empresa As String, ByVal Part As Part, ByVal audit As AuditMessage) As ActionResult


    <OperationContract()> _
    Function SavePart(Empresa As String, ByVal Part As Part, ByVal audit As AuditMessage) As ActionResult(Of Part)

    <OperationContract()> _
    Function GetPart(Empresa As String, ByVal codePart As String, ByVal audit As AuditMessage) As Part

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStatePart(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Part)
End Interface
