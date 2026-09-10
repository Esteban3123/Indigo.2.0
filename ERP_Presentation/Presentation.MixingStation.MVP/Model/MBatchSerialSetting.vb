'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Giovanny Plazas
' Created          : 2022-05-24
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

Public Class MBatchSerialSetting
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

    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' consulta el registro de parametrizacion de lote
    ''' </summary>
    ''' <returns></returns>
    Public Async Function BatchSerialSettings() As Task(Of ActionResult(Of BatchSerialSetting))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetBatchSerialSettingsAsync()
    End Function
    ''' <summary>
    ''' guarda el registro de parametrizacion de lote
    ''' </summary>
    ''' <param name="BatchSerialSetting"></param>
    ''' <returns></returns>
    Public Async Function SaveBatchSerialSetting(BatchSerialSetting As BatchSerialSetting) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveBatchSerialSettingAsync(BatchSerialSetting, _sessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' consulta los patrones de secuencias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSequencePatterns() As List(Of Domain.Entities.Sequense)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListSequences(_sessionValues)
    End Function
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

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
