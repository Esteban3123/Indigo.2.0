#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IFixedAssetRetirementTypesService

    ''' <summary>
    ''' Guarda o Actualiza un Tipo de baja de activo fijo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveFixedAssetRetirementTypes(FixedAssetRetirementTypes As FixedAssetRetirementTypes, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Elimina un Tipo de baja de activo fijo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteFixedAssetRetirementTypes(FixedAssetRetirementTypes As FixedAssetRetirementTypes, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un Tipo de baja de activo fijo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFixedAssetRetirementTypesByCode(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Obtiene un Tipo de baja de activo fijo por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetFixedAssetRetirementTypesById(id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Obtiene todo el grupo de atencion
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllFixedAssetRetirementTypes(ByVal audit As AuditMessage) As List(Of FixedAssetRetirementTypes)


End Interface
