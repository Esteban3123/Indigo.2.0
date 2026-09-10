#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IResponsibleService
    <OperationContract()> _
    Function ListAllResponsible(Empresa As String) As List(Of Responsible)


    <OperationContract()> _
    Function DeleteResponsible(Empresa As String, ByVal Responsible As Responsible, ByVal audit As AuditMessage) As ActionResult


    <OperationContract()> _
    Function SaveResponsible(Empresa As String, ByVal Responsible As Responsible, ByVal audit As AuditMessage) As ActionResult(Of Responsible)

    <OperationContract()> _
    Function GetResponsible(Empresa As String, ByVal codeAResponsible As String, ByVal audit As AuditMessage) As Responsible


    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStateResponsible(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Responsible)
End Interface
