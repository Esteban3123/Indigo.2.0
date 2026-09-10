'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Giovanny Plazas
' Created          : 2022-05-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base

Public Class PBatchSerialSetting

    ''' <summary>
    ''' view
    ''' </summary>
    Private _iview As IBatchSerialSetting
    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="iview"></param>
    Public Sub New(iview As IBatchSerialSetting)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _iview = iview
        Me._sessionValues = SessionValues.Instance

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMixingStations() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListViewMixingStations()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUnitDoseType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).MixingStationService.ListUnitDoseType()
    End Function
End Class
