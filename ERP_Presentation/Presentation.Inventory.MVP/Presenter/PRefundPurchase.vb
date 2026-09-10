'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Henry Alejandro Vargas Polania 
' Created          : 17/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

Public Class PRefundPurchase

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IRefundPurchase

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IRefundPurchase)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub LoadWarehouse()
        Me.View.ListWareHouse = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    Public Sub LoadEntranceVoucherByStatus()
        Using Model As New MRefundPurchase("")
            Me.View.ListEntranceVoucher = Model.ListEntranceVoucherByStatus(Indigo.TransactionalContainer, 2)
        End Using
    End Sub

    Public Sub LoadEntranceVoucherByStatusAndWarehouse(Optional warehouseId As Integer? = Nothing)
        Using Model As New MRefundPurchase("")
            Me.View.ListEntranceVoucher = Model.ListEntranceVoucherByStatusAndWarehouse(Indigo.TransactionalContainer, 2, warehouseId)
        End Using
    End Sub

    Public Sub LoadDevolutionCauses()
        Using Model As New MRefundPurchase("")
            Me.View.DevolutionCauseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.LoadDevolutionCausesByStatus(True)
        End Using
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListEntranceVoucherDevolutionByEntranceVoucherId(entranceVoucherId As Integer) As List(Of ViewEntranceVoucherDevolutionXpo)
        Dim filter As String = "EntranceVoucherId = " & entranceVoucherId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ViewEntranceVoucherDevolutionXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListObligationsOfEntranceVoucher(entranceVoucherId As Integer) As List(Of ViewListObligationOfEntranceVoucherXpo)
        Dim filter As String = "EntranceVoucherId = " & entranceVoucherId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.GetCollection(Of ViewListObligationOfEntranceVoucherXpo)(Nothing, filter)
    End Function

#End Region

End Class
