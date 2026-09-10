'***********************************************************************
' Assembly         : Presentation.Billing.MVP
' Author           : Andres Alarcon
' Created          : 26-07-2022
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MNumberingAuthorization
    Implements IDisposable


#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal Tag As String)
        Me._tagForm = Tag
        Me.Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una autorizacion de Documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetNumberingAuthorizationByCode(ByVal code As String) As Task(Of ActionResult(Of NumberingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetNumberingAuthorizationByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una autorizacion de Documento
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveNumberingAuthorization(ByVal record As NumberingAuthorization, ByVal idSequense As Int64) As Task(Of ActionResult(Of NumberingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveNumberingAuthorizationAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una autorizacion de Documento
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteNumberingAuthorization(ByVal record As NumberingAuthorization) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.DeleteNumberingAuthorizationAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateNumberingAuthorization(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of NumberingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.ChangeStateNumberingAuthorizationAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function GetNumberingAuthorizationResolution(ByVal operatingUnitId As Integer, ByVal record As NumberingAuthorization) As Task(Of ActionResult(Of NumberingAuthorization))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetNumberingAuthorizationResolutionAsync(operatingUnitId, record, Me.Indigo.AuditMessageWcf)
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