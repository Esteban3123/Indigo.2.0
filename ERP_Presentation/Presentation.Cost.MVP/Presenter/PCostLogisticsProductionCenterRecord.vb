'***********************************************************************
' Assembly         : Presentacion.Cost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/12/2016
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
Imports Infrastructure.Data.Xpo

#End Region

Public Class PCostLogisticsProductionCenterRecord
    Implements IDisposable

#Region "Fields"

    Private _indigoSessionValues As SessionValues

    Private _view As ICostLogisticsProductionCenterRecord

#End Region

#Region "Builders"

    Public Sub New(ByVal iView As ICostLogisticsProductionCenterRecord)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _view = iView
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using Model As New MCommonCost(_view.MyTag)
            Me._view.Sequence = Await Model.GetSequense()
        End Using
    End Sub

    Public Sub InitializeProductionCenter()
        Me._view.ProductionCenterLogisticXpo = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterTypes(True, 3)
    End Sub

    Public Sub InitializeProductionCenterTarget()
        Me._view.ProductionCenterTargetXpo = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.ListProductionCenterByStatusAndCenterTypes(True, 1, 2, 3)
    End Sub

    Public Sub InitializeMeasurementUnit()
        Me._view.MeasurementUnitXpo = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CostService.CostListMeasureUnitByType()
    End Sub

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
