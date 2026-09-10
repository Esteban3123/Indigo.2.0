'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Domain.InteropCost.Entities

<ServiceContract()>
Public Interface IInteropCostServiceDistributionFixedAsset

    ''' <summary>
    ''' Guarda una distribución de activos fijos
    ''' </summary>
    <OperationContract()>
    Function SaveDistributionFixedAsset(distributionFixedAsset As DistributionFixedAsset, idSequence As Int64, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset)

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    <OperationContract()>
    Function DeleteDistributionFixedAsset(distributionFixedAsset As DistributionFixedAsset, audit As AuditMessage) As ActionResult

    <OperationContract()> _
    Function GetDistributionFixedAssetByActivoAndYearMonth(activoOid As Integer, year As Integer, month As Integer) As ActionResult(Of DistributionFixedAsset)

    ' ''' <summary>
    ' ''' Actualiza el estado del registro
    ' ''' </summary>
    <OperationContract()>
    Function UpdateStateDistributionFixedAsset(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset)

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    <OperationContract()>
    Function GetDistributionFixedAsset(code As String, audit As AuditMessage) As ActionResult(Of DistributionFixedAsset)

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    <OperationContract()>
    Function GetDistributionFixedAssetById(id As Integer) As DistributionFixedAsset

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    <OperationContract()>
    Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionFixedAsset)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    <OperationContract()>
    Function ListPeriodWithDataByMaximumPeriodFixedAsset(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    <OperationContract()> _
    Function GetAFNDEPRECIByOidYearMonth(oid As Integer, year As Integer, month As Integer) As AFNDEPRECI
    <OperationContract()> _
    Function GetDeprecationValue(oidAfnActivo As Integer, year As Integer, month As Integer) As Decimal

    <OperationContract()>
    Function SP_ConfirmMasiveDistributionFixedAsset(Container As String, Year As Integer, Month As Integer, audit As AuditMessage) As ActionResult

    <OperationContract()> _
    Function SP_ExportExcelDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result))

End Interface