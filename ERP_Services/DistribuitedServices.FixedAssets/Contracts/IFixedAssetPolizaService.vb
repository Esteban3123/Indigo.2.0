#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IFixedAssetPolicyService

    <OperationContract()> _
    Function ListAllPoliza(Empresa As String) As List(Of FixedAssetPolicy)

    <OperationContract()> _
    Function DeletePoliza(Empresa As String, ByVal Poliza As FixedAssetPolicy, ByVal audit As AuditMessage) As ActionResult

    <OperationContract()>
    Function SavePoliza(Empresa As String, Poliza As Domain.Entities.FixedAssetPolicy, audit As Infrastructure.CrossCutting.Base.AuditMessage, idSequense As Int64) As ActionResult(Of Domain.Entities.FixedAssetPolicy)

    <OperationContract()> _
    Function GetPoliza(Empresa As String, ByVal codePoliza As String, ByVal audit As AuditMessage) As FixedAssetPolicy
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStatePoliza(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetPolicy)
End Interface
