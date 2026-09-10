'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 06/02/2022
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
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo


#End Region

Public Class MContributorSubtype
    Implements IDisposable

#Region "Fields"

    '' <summary>
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
    ''' Obtiene todos los subtipos de cotizantes
    ''' </summary>
    Public Async Function ListAllContributorSubtype() As Task(Of List(Of ContributorSubtype))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ListAllContributorSubtypeAsync(Indigo)
    End Function

    ''' <summary>
    ''' Elimina un subtipos de cotizante
    ''' </summary>
    ''' <param name="justificationControl">La justificacion control </param>
    ''' <returns></returns>
    Public Async Function DeleteContributorSubtype(ByVal contributorSubtype As ContributorSubtype) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteContributorSubtypeAsync(contributorSubtype, Me.Indigo.AuditMessageWcf, Indigo)
    End Function

    ''' <summary>
    ''' graba un subtipos de cotizante
    ''' </summary>
    ''' <param name="justificationControl">La justificacion control</param>
    ''' <returns></returns>
    Public Async Function SaveContributorSubtype(ByVal contributorSubtype As ContributorSubtype, Optional idSequense As Long = 0) As Task(Of ActionResult(Of ContributorSubtype))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveContributorSubtypeAsync(contributorSubtype, Me.Indigo.AuditMessageWcf, Indigo, idSequense)
    End Function

    ''' <summary>
    ''' consulta un subtipos de cotizante
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetContributorSubtypeByCode(ByVal code As String) As Task(Of ActionResult(Of ContributorSubtype))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContributorSubtypeByCodeAsync(code, Me.Indigo.AuditMessageWcf, Indigo)
    End Function

    ''' <summary>
    ''' Devuelve un subtipos de cotizante por ID
    ''' </summary>
    ''' <param name="id">Id del subtipo de cotizante </param>
    ''' <returns>La justificacion Control</returns>
    ''' <remarks></remarks>
    Public Async Function GetContributorSubtypeById(ByVal id As Integer) As Task(Of ActionResult(Of ContributorSubtype))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetContributorSubtypeByIdAsync(id, Indigo)
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
