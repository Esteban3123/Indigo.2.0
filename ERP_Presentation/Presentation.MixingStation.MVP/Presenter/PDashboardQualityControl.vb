'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Andrés Felipe Aros Escobar
' Created          : 29/09/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports System.Globalization
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports DevExpress.Data.PLinq
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PDashboardQualityControl

#Region "Variables"

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

#End Region

#Region "Builder"

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la información de la pestaña de Control de Calidad
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewQualityControl(productionLineId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListXPInstantFeedbackSource(Of ViewQualityControlXpo)($"ProductionLineId = {productionLineId}")
    End Function



    ''' <summary>
    ''' Carga la información de control final de Productos centro de atencion Externos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewFinalControlProduct(cmConfigurationId As Integer, productionLineId As Integer) As List(Of ViewListFinalControlProductXpo)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListFinalControlProductXpo)(Function(m) m.ProductionLineId = productionLineId And m.CMconfigurationId = cmConfigurationId)
    End Function

    ''' <summary>
    ''' Lista las Re-adecuaciones enviadas a control de calidad 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewReadjustments(productionLineId As Integer) As List(Of ViewReadjustmentsXpo)
        Dim filter As String = String.Format("ProductionLineId = {0} And SendTo = {1} And Status <> {2}", productionLineId, 1, 2)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewReadjustmentsXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Inicia las Causa de Reproceso
    ''' </summary>
    Public Function InitializeReprocessingCause(TypeAction As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListReprocessingCause(TypeAction)
    End Function

    ''' <summary>
    ''' Obtengo el almacén por el tipo de paramterizacion de la Central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewMixingSationWarehouse(cmConfigurationId As Integer, type As Integer) As ViewListWarehouseTypeXpo
        Dim filter As String = String.Format("IdMixingStation = {0} and WarehouseType = {1}", cmConfigurationId, type)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListWarehouseTypeXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Carga la información de control final de Productos por ID
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewFinalControlProductById(Id As Integer) As List(Of ViewListFinalControlProductXpo)
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of ViewListFinalControlProductXpo)(Function(m) m.Id = Id)
    End Function
#End Region

End Class
