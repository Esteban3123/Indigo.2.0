#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface ITechnicalLogService



    <OperationContract()> _
    Function ListAllTechnicalLog(Empresa As String) As List(Of TechnicalLog)


    <OperationContract()> _
    Function DeleteTechnicalLog(Empresa As String, ByVal TechnicalLog As TechnicalLog, ByVal audit As AuditMessage) As ActionResult


    <OperationContract()> _
    Function SaveTechnicalLog(Empresa As String, ByVal TechnicalLog As TechnicalLog, ByVal audit As AuditMessage) As ActionResult(Of TechnicalLog)

    <OperationContract()> _
    Function GetTechnicalLog(Empresa As String, ByVal codeTechnicalLog As String, ByVal audit As AuditMessage) As TechnicalLog

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStateTechnicalLog(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TechnicalLog)
End Interface
