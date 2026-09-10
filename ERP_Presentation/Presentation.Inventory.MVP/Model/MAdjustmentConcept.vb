'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/09/2014
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
Imports Infrastructure.Data.Xpo.InventoryRepository
#End Region

Public Class MAdjustmentConcept
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
    ''' Obtiene un concepto de ajuste por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAdjustmentConcept(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of AdjustmentConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetAdjustmentConceptAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un concepto de ajuste por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAdjustmentConceptById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of AdjustmentConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetAdjustmentConceptByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un concepto de ajuste
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAdjustmentConcept(ByVal record As AdjustmentConcept, ByVal idSequense As Int64) As Task(Of ActionResult(Of AdjustmentConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAdjustmentConceptAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un concepto de ajuste
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteAdjustmentConcept(ByVal record As AdjustmentConcept) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteAdjustmentConceptAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of AdjustmentConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateAdjustmentConceptAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtiene la cuenta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountById(id As Integer, Optional tracking As Boolean = True) As Task(Of Domain.Entities.MainAccounts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByIdAsync(id, tracking, Indigo)
    End Function

    ''' <summary>
    ''' Lista los conceptos de ajuste de inventario
    ''' </summary>
    ''' <param name="state"></param>
    ''' <param name="movementClass"></param>
    ''' <param name="conceptType"></param>
    ''' <param name="affectsAverageCost"></param>
    ''' <returns></returns>
    Public Function ListAdjustmentConcept(state As Boolean, movementClass As Byte, conceptType As Byte, affectsAverageCost As Boolean) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService _
            .ListXPInstantFeedbackSource(Of AdjustmentConceptXpo)($"Status={state} And MovementClass={movementClass} And ConceptType={conceptType} And AffectsAverageCost={affectsAverageCost}", "Id;Code;Name;CodeName")
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
