#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetDepreciationService

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetDepreciation(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetDepreciationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation)

    ''' <summary>
    ''' Genera y guarda la depreciación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, ModeConfirm As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation)

    ''' <summary>
    ''' Confirma la depreciación
    ''' </summary>
    ''' <param name="DepreciationDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetDepreciation)

End Interface
