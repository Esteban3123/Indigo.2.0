'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/05/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class PImports

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    
    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFixedAssetRemissionEntranceBySupplierDistributionLineId(SupplierDistributionLineId As Integer) As List(Of FixedAssetRemissionEntranceItemXpo)
        Dim filtroConsulta As String = "RemissionEntranceId.SupplierDistributionLineId = " & SupplierDistributionLineId & " And RemissionEntranceId.Status = 2 And OutstandingQuantity > 0"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetRemissionEntranceItemXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListFixedAssetPurchaseOrderBySupplierDistributionLineId(SupplierDistributionLineId As Integer, Optional CurrencyId As Integer = 0) As List(Of FixedAssetPurchaseOrderItemXpo)
        Dim filtroConsulta As String
        If CurrencyId = 0 Then
            filtroConsulta = "PurchaseOrderId.SupplierDistributionLineId = " & SupplierDistributionLineId & " And PurchaseOrderId.Status = 2 And OutstandingQuantity > 0"
        Else
            filtroConsulta = "PurchaseOrderId.SupplierDistributionLineId = " & SupplierDistributionLineId & " And PurchaseOrderId.Status = 2 And OutstandingQuantity > 0" & "And PurchaseOrderId.CurrencyId = " & CurrencyId
        End If
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetCollection(Of FixedAssetPurchaseOrderItemXpo)(Nothing, filtroConsulta)
    End Function

#End Region

End Class
