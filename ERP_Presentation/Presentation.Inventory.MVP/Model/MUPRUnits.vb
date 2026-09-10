'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Andrés Steven Rojas Rodríguez
' Created          : 29/02/2024
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

Public Class MUPRUnits
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
    ''' Obtiene la Unidad UPR por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function getUPRUnits(ByVal code As String) As Task(Of ActionResult(Of UPRUnits))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetUPRUnitsAsync(code, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Obtiene la Unidad UPR por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Async Function getUPRUnitsById(ByVal id As Integer) As Task(Of ActionResult(Of UPRUnits))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetUPRUnitsByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Guarda la entidad UPRUnit
    ''' </summary>
    ''' <param name="UPRUnit"></param>
    ''' <param name="IdSequence"></param>
    ''' <returns></returns>
    Public Async Function saveUPRUnits(ByVal UPRUnit As UPRUnits, ByVal IdSequence As Long) As Task(Of ActionResult(Of UPRUnits))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveUPRUnitsAsync(UPRUnit, IdSequence, Me.Indigo.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Async Function changeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of UPRUnits))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateUPRUnitsAsync(code, state, Me.Indigo.AuditMessageWcf)
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
