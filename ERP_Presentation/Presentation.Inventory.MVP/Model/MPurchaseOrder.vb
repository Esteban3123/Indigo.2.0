'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 10/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
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

Public Class MPurchaseOrder
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

#Region "Interface"

    Public Async Function LoadPurcharseOrderSBS(purchaseOrderURL As String, purchaseOrderIdentifier As String, purchaseOrderUser As String, purchaseOrderPass As String, orderCode As String) As Task(Of ActionResult(Of Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)))
        Try
            Dim address As New EndpointAddress(purchaseOrderURL)
            Dim binding As New BasicHttpBinding With
            {
                .Security = New BasicHttpSecurity With
                {
                    .Mode = BasicHttpSecurityMode.TransportWithMessageCredential,
                    .Transport = New HttpTransportSecurity With
                    {
                        .ClientCredentialType = HttpClientCredentialType.Basic
                    }
                },
                .MaxBufferSize = "2147483647",
                .MaxReceivedMessageSize = "2147483647"
            }

            Dim errors As New StringBuilder
            Dim supplierIdentification As String = Nothing
            Dim paymentMethodName As String = Nothing
            Dim inventoryProduct As InventoryProduct = Nothing
            Dim dictionaryNotFound As New Dictionary(Of String, Object)()
            Dim dictionaryProducts As New Dictionary(Of String, InventoryProduct)()
            Dim listPurcharseOrderDetail As New Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)

            Using service As New IndigoReference.SBS.VIEERPIntegrationClient(binding, address)
                service.ClientCredentials.UserName.UserName = purchaseOrderUser
                service.ClientCredentials.UserName.Password = purchaseOrderPass
                Using scope As New OperationContextScope(service.InnerChannel)
                    Dim CompanyID = New Guid(purchaseOrderIdentifier)
                    Dim results = Await service.GetProductListByPurchaseOrderCodeAsync(CompanyID, orderCode)
                    If results IsNot Nothing AndAlso results.Count > 0 Then
                        For Each result In results
                            If result.ContractProduct Is Nothing Then
                                Continue For
                            End If
                            If result.ContractProduct.BiddingOfferSupplier Is Nothing Then
                                Continue For
                            End If
                            If result.ContractProduct.BiddingOfferSupplier.ProductSupplierProduct Is Nothing Then
                                Continue For
                            End If
                            If result.ContractProduct.BiddingOfferSupplier.ProductSupplierProduct.SupplierProduct Is Nothing Then
                                Continue For
                            End If
                            If result.Product Is Nothing Then
                                Continue For
                            End If

                            If result.PurchaseOrder IsNot Nothing Then
                                If result.PurchaseOrder.Supplier IsNot Nothing Then
                                    If Not String.IsNullOrEmpty(result.PurchaseOrder.Supplier.SupplierTIN) Then
                                        supplierIdentification = result.PurchaseOrder.Supplier.SupplierTIN.Split("-").ElementAt(0)
                                        supplierIdentification = supplierIdentification.Replace(".", "")
                                    End If
                                End If

                                If result.PurchaseOrder.Contract IsNot Nothing Then
                                    If result.PurchaseOrder.Contract.PaymentMethod IsNot Nothing Then
                                        paymentMethodName = result.PurchaseOrder.Contract.PaymentMethod.PaymentMethodName
                                    End If
                                End If
                            End If

                            If result.PurchaseOrder IsNot Nothing Then
                                If result.PurchaseOrder.Contract IsNot Nothing Then
                                    If result.PurchaseOrder.Contract.PaymentMethod IsNot Nothing Then
                                        paymentMethodName = result.PurchaseOrder.Contract.PaymentMethod.PaymentMethodName
                                    End If
                                End If
                            End If

                            Dim Quantity = result.PurchaseOrderProductQuantity
                            Dim UnitValue = Utils.RoundValue(CDec(result.ContractProduct.BiddingOfferSupplier.ProductSupplierProduct.Price), 1) ' Valor unitario
                            Dim TotalPrice = result.TotalPrice
                            Dim ProductCode = result.Product.ProductSKU
                            Dim ProductName = result.Product.ProductName
                            Dim DiscountPercent = result.ContractProduct.BiddingOfferSupplier.Discount
                            Dim DiscountValue = 0
                            Dim PercententIva = result.ContractProduct.BiddingOfferSupplier.ProductSupplierProduct.TypeSaleTax.TypeSaleTaxPercent
                            Dim Subtotal = Utils.RoundValue(CDec(Quantity * UnitValue), 1) 'Sub total
                            Dim IvaValueCalulate = PercententIva + 1
                            Dim TotalUnitValue = Utils.RoundValue(CDec(UnitValue * IvaValueCalulate), 1) 'Valor unitario + Iva incluido
                            Dim IvaValue = 0 'Iva del valor unitario
                            If DiscountPercent > 100 Then
                                DiscountPercent = 100
                            End If
                            If PercententIva > 0 Then
                                If DiscountPercent > 0 Then
                                    Dim discountValueIva = Utils.RoundValue(CDec((UnitValue) * (DiscountPercent / 100)), 1)
                                    IvaValue = Utils.RoundValue(CDec(TotalUnitValue * PercententIva), 1)
                                    Dim final = (TotalUnitValue + IvaValue) - discountValueIva
                                    Subtotal = Quantity * final
                                Else
                                    IvaValue = UnitValue * PercententIva
                                    TotalPrice = (IvaValue * Quantity) + TotalPrice
                                End If
                            Else
                                DiscountValue = Utils.RoundValue(CDec(UnitValue - (UnitValue * (DiscountPercent / 100))), 1)
                                Subtotal = DiscountValue * Quantity
                                TotalUnitValue = TotalUnitValue - (TotalUnitValue - DiscountValue)
                                UnitValue = DiscountValue
                                DiscountValue = 0
                            End If
                            DiscountPercent = DiscountPercent / 100

                            If dictionaryNotFound.ContainsKey(ProductCode) Then
                                Continue For
                            End If

                            If Not dictionaryProducts.ContainsKey(ProductCode) Then
                                Using model As New MInventoryProduct(_tagForm)
                                    Dim resultProduct = Await model.GetInventoryProduct(ProductCode)
                                    If resultProduct.StateResult Then
                                        inventoryProduct = resultProduct.ObjectEmbbeded
                                        If inventoryProduct Is Nothing OrElse inventoryProduct.Id = 0 Then
                                            dictionaryNotFound.Add(ProductCode, Nothing)
                                            errors.AppendLine(String.Format("El producto {0} - {1} no existe", ProductCode, ProductName))
                                            Continue For
                                        End If
                                    Else
                                        dictionaryNotFound.Add(ProductCode, Nothing)
                                        errors.AppendLine(String.Format("Occurrio un error consultando el producto {0} - {1}", ProductCode, ProductName))
                                        Continue For
                                    End If
                                End Using
                                dictionaryProducts.Add(ProductCode, inventoryProduct)
                            Else
                                inventoryProduct = dictionaryProducts(ProductCode)
                            End If

                            listPurcharseOrderDetail.Add(New PurchaseOrderDetail With
                            {
                                .InventoryProduct = inventoryProduct,
                                .ProductId = inventoryProduct.Id,
                                .Quantity = Quantity,
                                .OutstandingQuantity = .Quantity,
                                .Value = Utils.RoundValue(CDec(UnitValue), Utils.RoundLevel.Unit),
                                .SubTotalValue = Utils.RoundValue(CDec(Subtotal), Utils.RoundLevel.Unit),
                                .TotalValue = TotalPrice,
                                .DiscountPercentage = DiscountPercent,
                                .IvaPercentage = Math.Round(CDec(PercententIva * 100), 2),
                                .IvaValue = Subtotal * PercententIva
                            })
                        Next
                    Else
                        errors.AppendLine(String.Format("No se encontraron datos para la orden {0}", orderCode))
                    End If
                End Using
            End Using

            Dim messageResult = New List(Of String)
            messageResult.Add(supplierIdentification)
            messageResult.Add(paymentMethodName)

            Return New ActionResult(Of Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)) With {.StateResult = True, .ObjectEmbbeded = listPurcharseOrderDetail, .Message = errors.ToString(), .MessageResult = messageResult}
        Catch ex As Exception
            Return New ActionResult(Of Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)) With {.StateResult = False, .Message = String.Format("No se encontraron datos para la orden {0}: {1}", orderCode, vbCrLf & Utils.GetInnerExceptionMessageToString(ex))}
        End Try
    End Function

    'Public Async Function LoadPurcharseOrderBionexo(orderCode As String) As Task(Of ActionResult(Of Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)))
    Public Async Function LoadPurcharseOrderBionexo(purchaseOrderURL As String, purchaseOrderIdentifier As String, purchaseOrderUser As String, purchaseOrderPass As String, orderCode As String) As Task(Of ActionResult(Of Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)))
        Try
            Dim address = New EndpointAddress(purchaseOrderURL)
            Dim binding As New BasicHttpBinding With
                {.Security = New BasicHttpSecurity With
                    {.Mode = BasicHttpSecurityMode.Transport},
                .MaxBufferSize = "2147483647",
                .MaxReceivedMessageSize = "2147483647"
            }

            Using IndigoBionexo As New BionexoInterfaceClient(binding, address)
                Dim Resultado As String
                'Resultado = IndigoBionexo.request("ws_medilaser", "wsmdl2012", "WEG", "LAYOUT=WE;ID=" & orderCode & "")
                Resultado = IndigoBionexo.request(purchaseOrderUser, purchaseOrderPass, purchaseOrderIdentifier, "LAYOUT=WE;ID=" & orderCode & "")
                Dim count As Integer = Resultado.Length
                Resultado = Resultado.Substring(22, count - 22)
                Dim byteArray As Byte() = Encoding.ASCII.GetBytes(Resultado)
                Dim Stream As System.IO.Stream = New System.IO.MemoryStream(byteArray)
                Dim dtDatos As New DataSet

                'leo el xsd definido como recurso 
                Dim RecursoXsd As String = My.Resources.BionexoStruct

                Dim byteArrayXsd As Byte() = Encoding.ASCII.GetBytes(RecursoXsd)
                Dim streamXsd As System.IO.Stream = New MemoryStream(byteArrayXsd)

                dtDatos.ReadXmlSchema(streamXsd)
                dtDatos.ReadXml(Stream)

                Dim errors As New StringBuilder
                Dim listPurcharseOrderDetail As New Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)

                Using model As New MInventoryProduct(_tagForm)
                    For Each item As DataRow In dtDatos.Tables("item").Rows
                        For Each detail In dtDatos.Tables("Resposta").Select("Item_Id= " & item("Item_Id").ToString())
                            Dim purcharseOrderDetail As New PurchaseOrderDetail

                            Dim result = Await model.GetInventoryProduct(item("Cod_Produto"))
                            If result.StateResult = False Then
                                errors.AppendLine("Occurrio un error consultando el producto " & item("Cod_Produto"))
                                Continue For
                            End If
                            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Id = 0 Then
                                errors.AppendLine("El producto " & item("Cod_Produto") & " no existe")
                                Continue For
                            End If

                            Dim product = result.ObjectEmbbeded
                            Dim iva As Decimal = 0
                            Dim ivaField = detail.GetChildRows("Resposta_Campo_Extra").Where(Function(r) r("Nome").ToString() = "IVA" AndAlso r("Valor") IsNot DBNull.Value).FirstOrDefault()
                            If ivaField IsNot Nothing Then
                                Decimal.TryParse(ivaField("Valor").ToString().Replace(",", "."), iva)
                            End If
                            With purcharseOrderDetail
                                .InventoryProduct = product
                                .ProductId = product.Id
                                .ProductCode = item("Cod_Produto")
                                .ProductName = product.Name
                                .Quantity = item("Quantidade")
                                .ManufacturerName = detail("Fabricante")
                                .Presentation = detail("Embalagem")
                                .OutstandingQuantity = .Quantity
                                .Value = Utils.RoundValue(CDec(detail("Preco_Unitario").Replace(",", ".")), Utils.RoundLevel.Unit)
                                .TotalIva = iva
                                .SubTotalValue = Utils.RoundValue(CDec(detail("Preco_Total").Replace(",", ".")), Utils.RoundLevel.Unit)
                                .TotalValue = .SubTotalValue
                            End With
                            listPurcharseOrderDetail.Add(purcharseOrderDetail)
                        Next
                    Next
                End Using

                Dim messageResult = New List(Of String)
                messageResult.Add(dtDatos.Tables("Fornecedor").Rows.Item(0)("CNPJ").ToString().Split("-").ElementAt(0))

                Return New ActionResult(Of Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)) With {.StateResult = True, .ObjectEmbbeded = listPurcharseOrderDetail, .Message = errors.ToString(), .MessageResult = messageResult}
            End Using

        Catch ex As Exception
            Return New ActionResult(Of Domain.Entities.TrackableCollection(Of PurchaseOrderDetail)) With {.StateResult = False, .Message = "No se encontraron datos para la orden " & orderCode & ", contacte al administrador"}
        End Try
    End Function

#End Region

    Public Function SetPurchaseOrderImportFile(data As List(Of ImportFileRow), operatingUnitId As Integer, Optional roundingType As Decimal = 0.01) As Task(Of ActionResult(Of List(Of PurchaseOrderDetail)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SetProductsPurchaseOrderImportFileAsync(data, operatingUnitId, Indigo, roundingType)
    End Function

    ''' <summary>
    ''' Obtiene un almacen por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPurchaseOrderByCode(ByVal code As String) As Task(Of PurchaseOrder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseOrderByCodeAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene el id del proveedor por el id de la linea de distribucción
    ''' </summary>
    ''' <param name="IdDistributionLines"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierIdByIdDistributionLines(ByVal IdDistributionLines As Integer) As Integer
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetSupplierIdByIdDistributionLines(IdDistributionLines)
    End Function

    ''' <summary>
    ''' Obtiene un almacen por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPurchaseOrderById(ByVal id As Integer) As Task(Of PurchaseOrder)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseOrderByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza un almacen
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePurchaseOrder(ByVal record As PurchaseOrder, ByVal idSequense As Int64, ByVal sequenceC As Domain.Entities.InventorySequence) As Task(Of ActionResult(Of PurchaseOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.SavePurchaseOrderAsync(record, idSequense, Me.Indigo.AuditMessageWcf, sequenceC)
    End Function

    ''' <summary>
    ''' Elimina una unidad de medida
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeletePurchaseOrder(ByVal record As PurchaseOrder) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DeletePurchaseOrderAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Warehouse))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.ChangeStateWarehouseAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' lista los almacenes activos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListWarehouseXPO() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListOwnWarehouseByStatusAndUser(True, Indigo.UserIndigo)
    End Function

    ''' <summary>
    ''' lista los Contratos xpo 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListContractInventoryXPO() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryContractByStatus(4)
    End Function

    ''' <summary>
    ''' Desconfirma una orden de compra
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DisconfirmPurchaseOrder(ByVal record As PurchaseOrder) As Task(Of ActionResult(Of Domain.Entities.PurchaseOrder))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.DisconfirmPurchaseOrderAsync(record, Me.Indigo.AuditMessageWcf)
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
