'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Judy Andrea Díaz Reyes
' Created          : 30/04/2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class MCMConfig
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"

	''' <summary>
	''' Variable que contiene la instancia de la clase singleton
	''' </summary>
	Dim _sessionValues As SessionValues
	''' <summary>
	''' Contiene el tag del formulario
	''' </summary>
	''' <remarks></remarks>
	Public Shared TagForm As String = "2063"

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Tag As String)
        MyBase.New(TagForm)
        _sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = TagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of CMConfiguration))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.UpdateStateCMConfigAsync(code, state, Me._sessionValues.AuditMessageWcf)
    End Function
    '***********************************************************************
    ''' <summary>
    ''' Guarda o actualizan los parámetros de configuración de central de mezclas
    ''' </summary>
    ''' <param name="cmConfig"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCMConfig(ByVal cmConfig As CMConfiguration, ByVal idSequence As Int64) As ActionResult(Of CMConfiguration)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCMConfig(cmConfig, idSequence, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la central de mezclas
    ''' </summary>
    ''' <param name="CMConfiguration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveCMConfigAsync(ByVal cmConfiguration As CMConfiguration, ByVal idSequence As Int64) As Task(Of ActionResult(Of CMConfiguration))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCMConfigAsync(cmConfiguration, idSequence, Me._sessionValues.AuditMessageWcf)
    End Function

    '***********************************************************************
    ''' <summary>
    ''' Guarda o actualizan los parámetros del centro de atencion
    ''' </summary>
    ''' <param name="cmCenterAttention"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCMCenterAttention(ByVal cmCenterAttention As CMCenterAttention) As ActionResult(Of CMCenterAttention)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCMCenterAttention(cmCenterAttention)
    End Function

    ''' <summary>
    ''' Guarda o actualiza los centros de atencion
    ''' </summary>
    ''' <param name="cmCenterAttention"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveCMCenterAttentionAsync(ByVal cmCenterAttention As CMCenterAttention) As Task(Of ActionResult(Of CMCenterAttention))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.SaveCMCenterAttentionAsync(cmCenterAttention)
    End Function

    ''' <summary>
    ''' Funcion para obtener los parametros de configuracion de central de mezclas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetCMConfigAsync(ByVal code As String) As Task(Of ActionResult(Of CMConfiguration))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCMConfigAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Funcion para obtener los centros de atencion
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetCMCenterAttentionAsync(ByVal id As String) As Task(Of ActionResult(Of CMCenterAttention))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCMCenterAttentionAsync(id)
    End Function

    Public Function GetCMConfig() As CMConfiguration
        'Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.GetCMConfigAsync(Me.Indigo.AuditMessageWcf)
        'TODO: HRR CENTRAL DE MEZCLA - AJUSTAR CUANDO SE REFACTORICE EL MODULO
        Return New CMConfiguration
    End Function

    '***********************************************************************
    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id 
    ''' </summary>
    ''' <param name="Id_MixingStation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCMMixingProducitonLine(ByVal Id_MixingStation As Integer) As List(Of Tuple(Of Integer, String))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllCMMixingProducitonLine(Id_MixingStation, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id asincrono
    ''' </summary>
    ''' <param name="Id_MixingStation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllCMMixingProducitonLineAsync(ByVal Id_MixingStation As Integer) As Task(Of List(Of Tuple(Of Integer, String)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListAllCMMixingProducitonLineAsync(Id_MixingStation, Me._sessionValues.AuditMessageWcf)
    End Function

    '***********************************************************************

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="mixingStationId"></param>
    ''' <param name="listCenterLine"></param>
    ''' <returns></returns>
    Public Async Function ListCMCenterLineUnit(ByVal mixingStationId As Integer, ByVal listCenterLine As List(Of Tuple(Of String, Integer, Boolean))) _
        As Task(Of ActionResult(Of List(Of SP_CMCenterLineUnit_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMixingStation.ListCMCenterLineUnitAsync(mixingStationId, listCenterLine)
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
