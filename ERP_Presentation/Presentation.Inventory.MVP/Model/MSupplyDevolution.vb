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

Public Class MSupplyDevolution
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
    ''' lista un detalle de la dispensacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalDispensingDetailBatchSerialById(id As Integer) As PharmaceuticalDispensingDetailBatchSerial
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingDetailBatchSerialById(id)
    End Function
    ''' <summary>
    ''' lista los detalles de la devolucion
    ''' </summary>
    ''' <param name="IdPharmaceuticalDispensingDevolution"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolution(IdPharmaceuticalDispensingDevolution As Integer) As Task(Of List(Of PharmaceuticalDispensingDevolutionDetail))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingDevolutionDetailByIdPharmaceuticalDispensingDevolutionAsync(IdPharmaceuticalDispensingDevolution)
    End Function

    ''' <summary>
    ''' obtiene una devolucion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPharmaceuticalDispensingDevolutionByCode(code As String) As Task(Of PharmaceuticalDispensingDevolution)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingDevolutionByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene una devolucion por codigo
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPharmaceuticalDispensingDevolutionById(id As Integer) As PharmaceuticalDispensingDevolution
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingDevolutionById(id)
    End Function
    ''' <summary>
    ''' guardar una devolucion
    ''' </summary>
    ''' <param name="PharmaceuticalDispensingDevolution"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePharmaceuticalDispensingDevolution(PharmaceuticalDispensingDevolution As PharmaceuticalDispensingDevolution, idSequense As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of PharmaceuticalDispensingDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SavePharmaceuticalDispensingDevolutionAsync(PharmaceuticalDispensingDevolution, idSequense, Me._indigoSessionValues.AuditMessageWcf, sequenceC)
    End Function
    ''' <summary>
    ''' guardar y confirmar una devolucion
    ''' </summary>
    ''' <param name="PharmaceuticalDispensingDevolution"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="action"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmPharmaceuticalDispensingDevolution(PharmaceuticalDispensingDevolution As PharmaceuticalDispensingDevolution, idSequense As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of Domain.Entities.PharmaceuticalDispensingDevolution))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmPharmaceuticalDispensingDevolutionAsync(PharmaceuticalDispensingDevolution, idSequense, Me._indigoSessionValues.AuditMessageWcf, sequenceC, action)
    End Function

    ''' <summary>
    ''' lista los almacenes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouse() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, _indigoSessionValues.UserIndigo)
    End Function
    ''' <summary>
    ''' lista los productos con cantidades para devolver
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber)
    End Function


    ''' <summary>
    ''' lista los productos con cantidades para devolver
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPharmaceuticalDispensingDetailBatchSerialByWarehouseAndAdmissionNumber(warehouseId As Integer, admissionNumber As String) As XPCollection(Of PharmaceuticalDispensingDetailBatchSerialXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPharmaceuticalDispensingDetailBatchSerialByWarehouseAndAdmissionNumber(Nothing, admissionNumber)
    End Function
    ' ''' <summary>
    ' ''' retornal un listado del detalle de la dispensacion para la devolucion
    ' ''' </summary>
    ' ''' <param name="admissionNumber"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Function ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber As String) As List(Of PharmaceuticalDispensingDetailBatchSerial)
    '    Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ListPharmaceuticalDispensingDetailBatchSerialByAdmissionNumber(admissionNumber)
    'End Function
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
