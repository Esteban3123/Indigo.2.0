#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.CloudAgent

#End Region

Public Class MPharmaceuticalDispensingTransfer
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
    ''' obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPharmaceuticalDispensingTransferByCode(code As String) As Task(Of PharmaceuticalDispensingTransfer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingTransferByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' guardar un traslado
    ''' </summary>
    ''' <param name="PharmaceuticalDispensingTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePharmaceuticalDispensingTransfer(PharmaceuticalDispensingTransfer As PharmaceuticalDispensingTransfer) As Task(Of ActionResult(Of PharmaceuticalDispensingTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SavePharmaceuticalDispensingTransferAsync(PharmaceuticalDispensingTransfer, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista los productos con cantidades para trasladar
    ''' </summary>
    ''' <param name="warehouseId"></param>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPharmaceuticalDispensingDetailBatchSerialByWarehouseAndAdmissionNumber(warehouseId As Integer, admissionNumber As String) As XPCollection(Of PharmaceuticalDispensingDetailBatchSerialXpo)
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).InventoryService.ListPharmaceuticalDispensingDetailBatchSerialByWarehouseAndAdmissionNumber(warehouseId, admissionNumber)
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
