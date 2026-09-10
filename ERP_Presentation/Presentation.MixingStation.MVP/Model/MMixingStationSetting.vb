'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Diego A. Roldan
' Created          : 2021-09-16
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent
#End Region

Public Class MMixingStationSetting
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"
    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String
#End Region
#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        Me._tagForm = tag
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Consulta los parámetros para una unidad operativa
    ''' </summary>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    Public Function GetMixingStationSettingByOperativeUnitIdAsync(operativeUnitId As Integer) As Task(Of ActionResult(Of MixingStationSetting))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetMixingStationSettingByOperativeUnitIdAsync(operativeUnitId, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda unos parámetros de mezclas
    ''' </summary>
    ''' <param name="mixingStationSettign"></param>
    ''' <returns></returns>
    Public Function SaveMixingStationSettingAsync(mixingStationSettign As MixingStationSetting, attentionCenters As List(Of MixingStationSettingAttentionCenter)) As Task(Of ActionResult(Of MixingStationSetting))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveMixingStationSettingAsync(mixingStationSettign, attentionCenters, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Function ListMixingStationSettignAttentionCenters() As Task(Of List(Of MixingStationSettingAttentionCenter))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAttentionCentersAsync()
    End Function
#End Region


#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
