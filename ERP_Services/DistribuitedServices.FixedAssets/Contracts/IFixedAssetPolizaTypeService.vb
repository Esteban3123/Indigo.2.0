#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IFixedAssetPolicyTypeService


    <OperationContract()> _
    Function ListAllPolizaType(Empresa As String) As List(Of FixedAssetPolicyType)


    <OperationContract()> _
    Function DeletePolizaType(Empresa As String, ByVal PolizaType As FixedAssetPolicyType, ByVal audit As AuditMessage) As ActionResult


    <OperationContract()>
    Function SavePolizaType(Empresa As String, PolizaType As Domain.Entities.FixedAssetPolicyType, audit As Infrastructure.CrossCutting.Base.AuditMessage, idSequense As Int64) As ActionResult(Of Domain.Entities.FixedAssetPolicyType)

    <OperationContract()> _
    Function GetPolizaType(Empresa As String, ByVal codePolizaType As String, ByVal audit As AuditMessage) As FixedAssetPolicyType
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    <OperationContract()> _
    Function ChangeStatePolizaType(Empresa As String, ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetPolicyType)
End Interface
