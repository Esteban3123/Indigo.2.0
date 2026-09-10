'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Faiber Julian Mora Dussan
' Created          : 06-10-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Domain.Base

#End Region

Public Class MCircularZeroThirty
    Implements IDisposable

#Region "Variables"

    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    ''' <remarks></remarks>
    Private _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="tag"></param>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    Public Function GetDate() As DateTime
        Return IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetServerDate()
    End Function

    Public Async Function GenerateDocument030Async(ByVal year As Int32, ByVal trimester As Int32) As Task(Of ActionResult(Of List(Of Domain.Entities.GenerateDocument030_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GenerateDocument030Async(year, trimester, Me._sessionValues.AuditMessageWcf)
    End Function

    Public Async Function ListTrimestersAsync() As Task(Of Dictionary(Of Int32, String))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.InnerChannel)
            Me._sessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._sessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListTrimestersAsync()
        End Using
    End Function

    Public Function ListTrimesters() As Dictionary(Of Int32, String)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.InnerChannel)
            Me._sessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._sessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListTrimesters()
        End Using
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
