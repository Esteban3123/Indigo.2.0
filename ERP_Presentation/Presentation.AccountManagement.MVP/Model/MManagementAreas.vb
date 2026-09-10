'***********************************************************************
' Assembly         : Presentacion.AccountManagement.MVP
' Author           : Felix Camilo Salar Roldan
' Created          : 14-11-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Class MManagementAreas
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
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una condicion de venta por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetManagementAreasByCode(ByVal code As String) As Task(Of ActionResult(Of ManagementAreas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetManagementAreasByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una condicion de venta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetManagementAreasById(ByVal id As Integer) As Task(Of ActionResult(Of ManagementAreas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.GetManagementAreasByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una condicion de venta
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveManagementAreas(ByVal item As ManagementAreas, ByVal idSequense As Int64) As Task(Of ActionResult(Of ManagementAreas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.SaveManagementAreasAsync(item, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina una condicion de venta
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteManagementAreas(ByVal item As ManagementAreas) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.DeleteManagementAreasAsync(item, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateManagementAreas(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of ManagementAreas))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccountManagement.ChangeStateManagementAreasAsync(code, state, Me.Indigo.AuditMessageWcf)
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
