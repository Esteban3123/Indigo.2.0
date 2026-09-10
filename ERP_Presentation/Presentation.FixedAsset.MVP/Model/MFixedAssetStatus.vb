'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 16-09-2014
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
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region

Public Class MFixedAssetStatus
    'Inherits ModelBase
    Implements IDisposable

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String


#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtener una marca por su codigo, metodo asincrono
    ''' </summary>
    ''' <param name="code">El codigo de la marca</param>
    ''' <returns>objeto accesorio</returns>
    Public Async Function GetFixedAssetStatusAsync(ByVal code As String) As Task(Of Object)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetStatusAssetByCodeAsync(code)
    End Function
    ''' <summary>
    ''' Graba la marca en modo asincrono
    ''' </summary>
    ''' <param name="FixedAssetStatus">FixedAssetStatus</param>
    ''' <returns>Un valor que indica si se grabo la marca</returns>
    ''' 

    Public Async Function SaveFixedAssetStatusAsync(ByVal FixedAssetStatus As FixedAssetStatusAsset, ByVal idSequense As Int64) As Task(Of ActionResult(Of FixedAssetStatusAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetStatusAssetAsync(FixedAssetStatus, idSequense, Me.Indigo.AuditMessageWcf)
    End Function
    'Public Async Function SaveFixedAssetStatusAsync(ByVal FixedAssetStatus As FixedAssetStatusAsset, ByVal idSequense As Int64) As Task(Of Boolean)

    '    Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.InnerChannel)
    '        Dim mess1 As New MessageHeader(Of Int64)(idSequense)
    '        Dim header1 As System.ServiceModel.Channels.MessageHeader = mess1.GetUntypedHeader(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
    '        Me.Indigo.AuditMessageWcf.Functional = _tagForm
    '        Dim mess2 As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
    '        Dim header2 As System.ServiceModel.Channels.MessageHeader = mess2.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
    '        OperationContext.Current.OutgoingMessageHeaders.Add(header1)
    '        OperationContext.Current.OutgoingMessageHeaders.Add(header2)

    '        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.SaveFixedAssetStatusAssetAsync(FixedAssetStatus)
    '    End Using
    'End Function
    ''' <summary>
    ''' Elimina Marca modo asincrono
    ''' </summary>
    ''' <param name="FixedAssetStatus">FixedAssetStatus</param>
    ''' <returns>Un valor que indica si se elimino con exito la Marca</returns>
    ''' 
    Public Async Function DeleteFixedAssetStatusAsync(ByVal FixedAssetStatus As FixedAssetStatusAsset) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteFixedAssetStatusAssetAsync(FixedAssetStatus, Me.Indigo.AuditMessageWcf)
    End Function
    'Public Async Function DeleteFixedAssetStatusAsync(ByVal FixedAssetStatus As FixedAssetStatusAsset) As Task(Of Boolean)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.DeleteFixedAssetStatusAssetAsync(FixedAssetStatus, Indigo)
    'End Function
    ''' <summary>
    ''' Lista todos los accesorios
    ''' </summary>
    Public Async Function ListAllFixedAssetStatusAsync() As Task(Of List(Of FixedAssetStatusAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ListAllFixedAssetStatusAssetAsync(Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="State"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Public Async Function ChangeStateStatus(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of FixedAssetStatusAsset))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ChangeFixedAssetStatusAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    'Public Async Function ChangeStateStatus(Code As String, State As Boolean) As Task(Of Boolean)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.ChangeFixedAssetStatusAsync(Code, State, Indigo)
    'End Function
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
