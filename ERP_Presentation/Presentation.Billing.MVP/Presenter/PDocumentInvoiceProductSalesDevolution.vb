'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/01/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PDocumentInvoiceProductSalesDevolution

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IDocumentInvoiceProductSalesDevolution

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
    Public Sub New(ByRef iview As IDocumentInvoiceProductSalesDevolution)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub ListWarehouse()
        Me.View.WarehouseXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Sub

    Public Sub ListDocumentInvoiceProductSales(warehouseId As Integer)
        Me.View.DocumentInvoiceProductSalesDatasourceXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListDocumentInvoiceProductSalesByStatus(2, warehouseId)
    End Sub

    Public Function GetDocumentInvoiceProductSales(Id As Integer) As InventoryDocumentInvoiceProductSalesXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryDocumentInvoiceProductSalesXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function ListDetailsDocumentInvoiceProductSales(Id As Integer) As List(Of ViewDocumentInvoiceProductSalesByDevolutionXpo)
        Dim filter As String = "DocumentInvoiceProductSalesId = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ViewDocumentInvoiceProductSalesByDevolutionXpo)(Nothing, filter).ToList()
    End Function

    Public Function ListDetailsDocumentInvoiceProductSalesDevolution(Id As Integer) As List(Of ViewDocumentInvoiceProductSalesDevolutionDetailsXpo)
        Dim filter As String = "DocumentInvoiceProductSalesDevolutionId = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ViewDocumentInvoiceProductSalesDevolutionDetailsXpo)(Nothing, filter).ToList()
    End Function

#End Region

#End Region

End Class
