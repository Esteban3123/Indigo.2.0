'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Henry Alejandro Vargas Polania
' Created          : 27/09/2014
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
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class MInventoryAdjustments
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

    '''' <summary>
    '''' Referencia a los valores de session
    '''' </summary>
    'Private _indigoSessionValues As SessionValues

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
        '_indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un InventoryAdjustment por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryAdjustment(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of InventoryAdjustment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryAdjustmentAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un InventoryAdjustment por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInventoryAdjustmentById(ByVal id As Integer) As Task(Of InventoryAdjustment)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoInventory.InnerChannel)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryAdjustmentByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza un ContractType
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveInventoryAdjustment(ByVal record As InventoryAdjustment, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of InventoryAdjustment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveInventoryAdjustmentAsync(record, idSequense, Me.Indigo.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' guardar y confirmar un comprobante de entrada
    ''' </summary>
    ''' <param name="InventoryAdjustment"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveAndConfirmInventoryAdjustment(InventoryAdjustment As InventoryAdjustment, idSequense As Integer, OperatingUnitId As Integer, action As Integer, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of Domain.Entities.InventoryAdjustment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SaveAndConfirmbInventoryAdjustmentAsync(InventoryAdjustment, OperatingUnitId, Me.Indigo.AuditMessageWcf, idSequense, sequenceC, action)
    End Function

    ''' <summary>
    ''' Elimina un ContractType
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteInventoryAdjustment(ByVal record As InventoryAdjustment) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeleteInventoryAdjustmentAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Byte) As Task(Of ActionResult(Of InventoryAdjustment))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateInventoryAdjustmentAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Lista los almacenes xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCustodyWarehouseByStatusAndUser(Company As String, Status As Boolean, User As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListCustodyWarehouseByStatusAndUser(Status, User)
    End Function

    ''' <summary>
    ''' Lista almacenes propios por estado y usuario con permiso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListOwnWarehouseByStatusAndUser() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Function

    ''' <summary>
    ''' Lista almacenes propios y de control por estado y usuario con permiso
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListOwnAndControlWarehouseByStatusAndUser() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnAndControlWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Function

    ''' <summary>
    ''' Lista los terceros por estados xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllThirdPartyXpo() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Function

    ''' <summary>
    ''' Lista los conceptos de ajuste de inventerio xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAdjustmentConceptsByTypeXpo(Type As Byte) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListAdjustmentConceptByConceptTypeAndMovement(Type, 1, True, Indigo.UserIndigo)
    End Function


    Public Function GetAdmissionByInventoryAdjustmentCollection(admissionNumber As String) As XPCollection(Of ViewAdmissionOpenAndPartial)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetViewAdmissionOpenAndPartialCollection(admissionNumber)
    End Function

    ''' <summary>
    ''' Lista los centro de costo por xpo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCostCenterXpo() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
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
