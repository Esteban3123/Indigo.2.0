'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.InteropCost.Entities

Public Interface IDistributionFixedAssetAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una distribución de activos fijos
    ''' </summary>
    Function SaveDistributionFixedAsset(ByVal distributionFixedAsset As DistributionFixedAsset, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of DistributionFixedAsset)

    ''' <summary>
    ''' Elimina una distribución de activos fijos
    ''' </summary>
    Function DeleteDistributionFixedAsset(ByVal distributionFixedAsset As DistributionFixedAsset, ByVal audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Actualiza el estado del registro
    ' ''' </summary>
    Function UpdateStateDistributionFixedAsset(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DistributionFixedAsset)

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por codigo
    ''' </summary>
    Function GetDistributionFixedAsset(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DistributionFixedAsset)

    ''' <summary>
    ''' Obtiene una distribucion de activos fijos por id
    ''' </summary>
    Function GetDistributionFixedAssetById(id As Integer) As DistributionFixedAsset

    Function GetDistributionFixedAssetByActivoAndYearMonth(activoOid As Integer, year As Integer, month As Integer) As ActionResult(Of DistributionFixedAsset)

    ''' <summary>
    ''' Lista las distribuciones de activos fijos por año y mes
    ''' </summary>
    Function ListDistributionFixedAssetByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of DistributionFixedAsset)

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriod(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    Function GetAFNDEPRECIByOidYearMonth(oid As Integer, year As Integer, month As Integer) As AFNDEPRECI

    Function GetDeprecationValue(oidAfnActivo As Integer, year As Integer, month As Integer) As Decimal

    Function SP_ConfirmMasiveDistributionFixedAsset(Container As String, Year As Integer, Month As Integer, audit As AuditMessage) As ActionResult

    Function SP_ExportExcelDistributionFixedAsset(Container As String, Year As Integer, Month As Integer) As ActionResult(Of List(Of SP_ExportExcelDistributionFixedAsset_Result))

End Interface