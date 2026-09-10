'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Diego A. Roldán L.
' Created          : 2023-04-04
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
#End Region

Public Class MConsignmentTransfer
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Private _session As SessionValues
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
        _session = SessionValues.Instance
        _session.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un parametro de solicitud por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetByCodeAsync(code As String) As Task(Of ConsignmentTransfer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetConsignmentTransferByCodeAsync(code)
    End Function

    ''' <summary>
    ''' Consulta las cantidades en el almacén de consignación
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="productId"></param>
    ''' <returns></returns>
    Public Async Function GetConsignmentInventoryQuantitiesAsync(warehouseId As Integer, productId As Integer) As Task(Of ConsignmentMovementInventory)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetConsignmentInventoryQuantitiesAsync(warehouseId, productId)
    End Function

    ''' <summary>
    ''' Obtiene un parametro de solicitud por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetByIdAsync(id As Integer) As Task(Of ConsignmentTransfer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetConsignmentTransferByIdAsync(id)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un parametro de solicitud
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function Save(record As ConsignmentTransfer, ByVal idSequence As Long) As Task(Of ActionResult(Of ConsignmentTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveConsignmentTransferAsync(record, _session.AuditMessageWcf, idSequence)
    End Function

    Public Async Function SaveAndConfirmAsync(record As ConsignmentTransfer, ByVal idSequence As Long) As Task(Of ActionResult(Of ConsignmentTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmConsignmentTransferAsync(record, _session.AuditMessageWcf, idSequence)
    End Function

    ''' <summary>
    ''' Método para validar el archivo de Excel de importación
    ''' </summary>
    ''' <param name="listRows"></param>
    ''' <param name="sourceWarehouseId"></param>
    ''' <returns></returns>
    Public Function SetConsignmentTransferImportFile(listRows As List(Of ImportFileRow), sourceWarehouseId As Integer) As ActionResult(Of List(Of ConsignmentTransferDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetConsignmentTransferImportFile(listRows, sourceWarehouseId, _session.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Método para importar datos pegados desde Excel
    ''' </summary>
    ''' <param name="listRows"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetConsignmentTransferDetailFromCopyPaste(dataImport As List(Of List(Of String)), sourceWarehouseId As Integer) As Task(Of ActionResult(Of List(Of ConsignmentTransferDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetConsignmentTransferDetailFromCopyPasteAsync(dataImport, sourceWarehouseId, _session.AuditMessageWcf)
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
