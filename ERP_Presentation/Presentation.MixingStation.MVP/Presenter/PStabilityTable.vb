'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/10/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PStabilityTable

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Private _view As IStabilityTable

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Private _sessionValues As SessionValues
#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As IStabilityTable)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequense = Await model.GetSequence()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the ATC.
    ''' </summary>
    Public Function InitializeATC() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Return Model.ConsultarEntidades(eDataSource.ListATC)
        End Using
    End Function

    ''' <summary>
    ''' Initializes the ATC Diluent
    ''' </summary>
    Public Function InitializeATCDiluent() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListATCbyFilter("DiluentProduct = 1")
    End Function

    ''' <summary>
    ''' Inicia los tipos de dosis unitarias
    ''' </summary>
    Public Async Function InitializeUnitDoseType(AtcId As Integer) As Task(Of List(Of MixinStationUnitDoseTypeXpo))
        Dim criteria As String = $"ATCId.Id = {AtcId}"
        Return Await Task.Run(Function() XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of MedicinesProductionXpo)(Nothing, criteria) _
                                .Select(Function(x) x.UnitDoseTypeId).Distinct().ToList())
    End Function

    ''' <summary>
    ''' Inicia los tipos de temperatura de almacenamiento
    ''' </summary>
    Public Function InitializeStorageTemperature() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Return Model.ConsultarEntidades(eDataSource.ListStorageTemperature)
        End Using
    End Function

    ''' <summary>
    ''' Inicializa los productos
    ''' </summary>
    ''' <param name="atcId"></param>
    ''' <returns></returns>
    Public Function InitializeProduct(atcId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListInventoryProductByATCId(atcId)
    End Function

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetListDetails(Id As Integer) As List(Of StabilityTableDetailXpo)
        Dim filter As String = "StabilityTableId.Id = " & Id
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.GetCollection(Of StabilityTableDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Initializes the measure unit.
    ''' </summary>
    Public Function InitializeMeasureUnitWeight() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Dim _filter As Object() = {1}
            Return Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Function

    ''' <summary>
    ''' Initializes the measure unit volumen.
    ''' </summary>
    Public Function InitializeMeasureUnitVolumen() As XPInstantFeedbackSource
        Using Model As New MBusqueda
            Dim _filter As Object() = {2}
            Return Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Function

#End Region

End Class
