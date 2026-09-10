'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 30-01-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetInventoryTypeService

    <OperationContract()>
    Function DeleteFixedAssetInventoryType(FixedAssetInventoryType As FixedAssetInventoryType, audit As AuditMessage) As ActionResult

    <OperationContract()> _
    Function GetFixedAssetInventoryType(code As String) As FixedAssetInventoryType

    <OperationContract()> _
    Function ListAllFixedAssetInventoryType() As List(Of FixedAssetInventoryType)

    <OperationContract()>
    Function SaveFixedAssetInventoryType(FixedAssetInventoryType As FixedAssetInventoryType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetInventoryType)

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeFixedAssetInventoryTypeStatus(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetInventoryType)


End Interface
