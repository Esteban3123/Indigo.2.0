'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Juan Carlos Bermudez
' Created          : 02-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.Xpo
Imports System.Dynamic

#End Region

Public Class MTransferOrderDevolution
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una devolucion de orden de traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetTransferOrderDevolutionByCode(code As String) As Task(Of TransferOrderDevolution)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetTransferOrderDevolutionByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' guarda, actualiza y confirma una devolucion de orden de traslado
    ''' </summary>
    ''' <param name="transferOrderDevolution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveTransferOrderDevolution(transferOrderDevolution As TransferOrderDevolution) As Task(Of ActionResult(Of TransferOrderDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveTransferOrderDevolutionAsync(transferOrderDevolution, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista los detalles del detelle de la orden de traslado
    ''' </summary>
    ''' <param name="transferOrderId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListTransferOrderDetailBatchSerialByTransferOrderId(transferOrderId As Integer, flagQuantiyZero As Boolean) As Task(Of List(Of TransferOrderDetailBatchSerial))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListTransferOrderDetailBatchSerialByTransferOrderIdAsync(transferOrderId, flagQuantiyZero)
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
