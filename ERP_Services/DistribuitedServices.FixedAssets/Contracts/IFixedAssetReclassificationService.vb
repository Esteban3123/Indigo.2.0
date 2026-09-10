#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
#End Region

<ServiceContract()>
Public Interface IFixedAssetReclassificationService

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetReclassificationById(Id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetFixedAssetReclassificationByCode(Code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification)

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="FixedAssetReclassification"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveFixedAssetReclassification(FixedAssetReclassification As Domain.Entities.FixedAssetReclassification, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification)

    ''' <summary>
    ''' Confirma un registro
    ''' </summary>
    ''' <param name="FixedAssetReclassification"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ConfirmFixedAssetReclassification(FixedAssetReclassification As Domain.Entities.FixedAssetReclassification, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetReclassification)

End Interface
