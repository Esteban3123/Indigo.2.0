'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
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


Public Class MRemissionDevolution
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
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRemissionDevolutionByCode(code As String) As Task(Of RemissionDevolution)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRemissionDevolutionByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' guardar una remision
    ''' </summary>
    ''' <param name="RemissionDevolution"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveRemissionDevolution(RemissionDevolution As RemissionDevolution, idSequense As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of RemissionDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveRemissionDevolutionAsync(RemissionDevolution, Me._indigoSessionValues.AuditMessageWcf, idSequense, sequenceC)
    End Function
    ''' <summary>
    ''' guardar y confirmar una remision
    ''' </summary>
    ''' <param name="RemissionDevolution"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmRemissionDevolution(RemissionDevolution As RemissionDevolution, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence, Optional controlCost As Boolean = False) As Task(Of ActionResult(Of Domain.Entities.RemissionDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbRemissionDevolutionAsync(RemissionDevolution, Me._indigoSessionValues.AuditMessageWcf, idSequense, sequenceC, action, controlCost)
    End Function

    ''' <summary>
    ''' lista los almacenes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouse(DevolutionType As Integer?) As XPInstantFeedbackSource
        If DevolutionType = 3 Then
            Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListConsignmentWarehouseByStatusAndUser(True, Nothing, _indigoSessionValues.UserIndigo)
        Else
            Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, _indigoSessionValues.UserIndigo)
        End If
    End Function

    ''' <summary>
    ''' lista las remisiones de entrada
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRemissionEntrance(Optional warehouseId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListRemissionEntranceByStatus(2, warehouseId)
    End Function

    ''' <summary>
    ''' lista las remisiones de salida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRemissionOutput(Optional warehouseId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListRemissionOutputByStatus(2, warehouseId)
    End Function

    ''' <summary>
    ''' lista las remisiones de inventario en consignación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConsignmentInventoryRemission(Optional warehouseId As Integer? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListConsignmentInventoryRemissionByStatus(2, warehouseId)
    End Function

    ''' <summary>
    ''' lista los detalles del detella de la remision de entrada
    ''' </summary>
    ''' <param name="RemissionEntranceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListRemissionEntranceDetailBatchSerialByRemissionEntranceId(RemissionEntranceId As Integer) As Task(Of List(Of RemissionEntranceDetailBatchSerial))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListRemissionEntranceDetailBatchSerialByRemissionEntranceIdAsync(RemissionEntranceId)
    End Function
    ''' <summary>
    ''' lista los detalles del detalle de la remision de salida
    ''' </summary>
    ''' <param name="RemissionOutputId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId As Integer) As Task(Of List(Of RemissionOutputDetailPhysical))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListRemissionOutputDetailPhysicalByRemissionOutputIdAsync(RemissionOutputId)
    End Function
    ''' <summary>
    ''' lista los detalles del detalle de la remision de inventario en consignación
    ''' </summary>
    ''' <param name="ConsignmentInventoryRemissionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionId(ConsignmentInventoryRemissionId As Integer) As Task(Of List(Of ConsignmentInventoryRemissionDetailBatchSerial))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListConsignmentInventoryRemissionDetailBatchSerialByConsignmentInventoryRemissionIdAsync(ConsignmentInventoryRemissionId)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
