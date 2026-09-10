Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports System.ServiceModel
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Presentation.CloudAgent.IndigoReference.Bionexo
Imports System.Text
Imports System.Data
Imports System.IO

Public Class MInventoryContractAssignment
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
    ''' Obtiene una cesión de contrato por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryContractAssignment(ByVal code As String) As Task(Of ActionResult(Of InventoryContractAssignment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryContractAssignmentAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene una cesión de contrato por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryContractAssignmentById(ByVal id As Integer) As Task(Of ActionResult(Of InventoryContractAssignment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryContractAssignmentByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una cesión de contrato
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveInventoryContractAssignment(ByVal record As InventoryContractAssignment) As Task(Of ActionResult(Of InventoryContractAssignment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveInventoryContractAssignmentAsync(record, Me.Indigo.AuditMessageWcf)
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

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    Protected Overrides Sub Finalize()
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(False)
        MyBase.Finalize()
    End Sub

#End Region

End Class
