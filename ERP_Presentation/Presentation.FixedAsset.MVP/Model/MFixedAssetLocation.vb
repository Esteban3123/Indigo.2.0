'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Jeisson Herrera Peña
' Created          : 25/09/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent

#End Region

''' <summary>
''' Modelo de conexion con los servicios distribuidos de la corporación
''' </summary>
Public Class MFixedAssetLocation
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"

    Public Shared TAG As String = "1100"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        MyBase.New(Tag)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una Ubicación
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetFixedAssetLocationByCode(ByVal code As String) As Task(Of FixedAssetLocation)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetLocationAsync(Indigo.TransactionalContainer, code, Me.Indigo.AuditMessageWcf)
    End Function

   
    ''' <summary>
    ''' Guarda o actualiza una Ubicación
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveFixedAssetLocation(ByVal record As List(Of FixedAssetLocation), ByVal idSequense As Int64) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveLocationAsync(Indigo.TransactionalContainer, record, Indigo.AuditMessageWcf, idSequense)
    End Function

    ''' <summary>
    ''' Elimina una Ubicación
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteFixedAssetLocation(ByVal record As FixedAssetLocation) As Task(Of Boolean)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteLocationAsync(Indigo.TransactionalContainer, record, Indigo.AuditMessageWcf)
        End Using
    End Function

  

    ''' <summary>
    ''' Obtiene una Ubicación
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListAllLocation() As Task(Of List(Of FixedAssetLocation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllLocationAsync(Indigo.TransactionalContainer, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtener el listado de los tipos de ubicaciones
    ''' </summary>
    Public Async Function ListAllLocationTypeAsync() As Task(Of List(Of FixedAssetLocationType))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllLocationTypeAsync(Indigo.TransactionalContainer)
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
