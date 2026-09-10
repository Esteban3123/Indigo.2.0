'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2014
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
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class MPharmacologicalGroup
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
    ''' Obtiene un grupo farmacologico por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPharmacologicalGroup(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of PharmacologicalGroup))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmacologicalGroupAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un grupo farmacologico por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPharmacologicalGroupById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of PharmacologicalGroup))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmacologicalGroupByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un grupo farmacologico
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePharmacologicalGroup(ByVal record As PharmacologicalGroup, ByVal idSequense As Int64) As Task(Of ActionResult(Of PharmacologicalGroup))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SavePharmacologicalGroupAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un grupo farmacologico
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePharmacologicalGroup(ByVal record As PharmacologicalGroup) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeletePharmacologicalGroupAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of PharmacologicalGroup))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStatePharmacologicalGroupAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Gets the list admissions filter.
    ''' </summary>
    Function ListAllIHGRUFARM() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListAllIHGRUFARM()
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
