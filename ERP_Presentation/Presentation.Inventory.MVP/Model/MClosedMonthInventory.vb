'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Juan Carlos Bermudez
' Created          : 25/06/2015
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
#End Region

Public Class MClosedMonthInventory
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
    ''' Metodo para Cerrar el mes de inventario
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ClosedMonthInventory(ByVal MonthClosed As Integer, ByVal YearClosed As Integer, ByVal OperatingUnitId As Integer, ByVal confirm As Boolean) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryClosedMonth))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ClosedMonthInventoryAsync(MonthClosed, YearClosed, OperatingUnitId, Me.Indigo.AuditMessageWcf, confirm)
    End Function

    ''' <summary>
    ''' Metodo para verificar documentos
    ''' </summary>
    ''' <param name="MonthClosed"></param>
    ''' <param name="YearClosed"></param>
    ''' <returns></returns>
    Public Async Function VerifiyHasConfirmAllDocuments(ByVal MonthClosed As Integer, ByVal YearClosed As Integer) As Task(Of ActionResult(Of List(Of SP_VerifiyHasConfirmAllDocuments_Result)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.VerifiyHasConfirmAllDocumentsAsync(MonthClosed, YearClosed)
    End Function


    ''' <summary>
    ''' Obtiene resumen del mes a cerrar
    ''' </summary>
    ''' <param name="YearClosed"></param>
    ''' <param name="MonthClosed"></param>
    ''' <returns></returns>
    Public Function GetMonthlyClosureSummary(ByVal YearClosed As Integer, MonthClosed As Integer) As ActionResult(Of List(Of SP_GetMonthlyClosureSummary_Result))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetMonthlyClosureSummary(YearClosed, MonthClosed)
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
