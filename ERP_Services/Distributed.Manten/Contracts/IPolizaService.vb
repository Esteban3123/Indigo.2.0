#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IPolizaService

    <OperationContract()> _
    Function ListAllPoliza(Empresa As String) As List(Of Poliza)


    <OperationContract()> _
    Function DeletePoliza(Empresa As String, ByVal Poliza As Poliza, ByVal audit As AuditMessage) As ActionResult


    <OperationContract()> _
    Function SavePoliza(Empresa As String, ByVal Poliza As Poliza, ByVal audit As AuditMessage) As ActionResult(Of Poliza)

    <OperationContract()> _
    Function GetPoliza(Empresa As String, ByVal codePoliza As String, ByVal audit As AuditMessage) As Poliza
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStatePoliza(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Poliza)
End Interface
