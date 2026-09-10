'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MDashboardRequestMixingStation
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
    Public Sub New(tag As String)
        Me._tagForm = tag
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetRequestMixingStationById(ByVal id As Integer) As Task(Of ActionResult(Of RequestMixingStation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetRequestMixingStationByIdAsync(id)
    End Function

    Public Async Function GetRequestMixingStation(ByVal code As String) As Task(Of ActionResult(Of RequestMixingStation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetRequestMixingStationAsync(code, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function SP_ProcessMixingStation(args As Object) As Task(Of ActionResult(Of SP_ProcessMixingStation_Result))
        Dim parameter As String = Utils.SerializeObjectToJson(args)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SP_ProcessMixingStationAsync(parameter, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveRequestMixingStation(data As Tuple(Of Integer, Integer)) As Task(Of ActionResult(Of RequestMixingStation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveRequestMixingStationAsync(data)
    End Function

    ''' <summary>
    ''' Proceso las Solicitudes de Central de Mezclas
    ''' </summary>
    ''' <param name="requestIds"></param>
    ''' <returns></returns>
    Public Async Function ProcessRequestMixingStation(ByVal requestIds As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ProcessRequestMixingStationAsync(requestIds, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function AnnulateRequestsAsync(ByVal requestDetailIds As List(Of Integer)) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.AnnulateRequestsAsync(requestDetailIds)
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
