'***********************************************************************
' Assembly         : Presentacion.Accounting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports System.Dynamic
Imports Domain.Security.Entities
Imports Domain.Crystal.Entities
Imports DevExpress.Xpo
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

''' <summary>
''' Se encarga de establecer comunicación con los servicios de niveles de cuenta
''' </summary>
Public Class MCommon
    Inherits ModelBase
    Implements IDisposable

#Region "Fields"

    Public Shared TAG As String = ""

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Id de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Public ValidityId As Integer

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        MyBase.New(tag)
        _tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Funcion para obtener una remisión de entrada por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetFixedAssetRemisionEntranceByCode(ByVal code As String) As Task(Of FixedAssetRemissionEntrance)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetRemissionEntranceAsync(code)
            If res Is Nothing OrElse res.Id = 0 Then
                res.Code = code
            End If
            Return res
        End Using
    End Function

    ''' <summary>
    ''' Funcion para obtener un ingreso de activo por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetFixedAssetEntryByCode(ByVal code As String) As Task(Of FixedAssetEntry)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoFixedAssets.GetFixedAssetEntryAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Funcion para obtener una dispensacion farmaceutica por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetpharmaceuticalDisphensingByCode(ByVal code As String) As Task(Of PharmaceuticalDispensing)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPharmaceuticalDispensingAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res.ObjectEmbbeded Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Funcion para obtener una unidad funcional por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetFinancialSourceByCode(ByVal code As String) As Task(Of FinancialSource)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetFinancialSourceAsync(code, ValidityId, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Name = "La fuente de financiación no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener una unidad funcional por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetFunctionalUnitByCode(ByVal code As String) As Task(Of Domain.Payroll.Entities.FunctionalUnit)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFunctionalUnitAsync(code, _indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Name = "La Unidad Funcional no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener una orden de traslado por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetTransferOrderByCode(ByVal code As String) As Task(Of TransferOrder)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetTranferOrderByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Description = "La Orden de Traslado no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener un radicado por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetRadicatedByConsecutive(ByVal code As String) As Task(Of RadicateInvoiceC)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetInvocieRadicateAsync(code, _indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.RadicatedConsecutive = code
            res.Comment = "El Radicado no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener un rubro por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetCategoryBudgetByCode(ByVal code As String) As Task(Of Category)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoBudget.GetBudgetItemByCodeAndBudgetaryValidityIdAndItemTypeAsync(code, ValidityId, 1, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Name = "El Rubro no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Funcion para obtener la factura por numer de factura
    ''' </summary>
    ''' <param name="numberInvoice">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetInvoiceByNumberInvoice(ByVal numberInvoice As String) As Task(Of Invoice)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceByInvoiceNumberAsync(numberInvoice, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.InvoiceNumber = numberInvoice
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener el ingreso por numero de ingreso
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Function GetAdmissionNumberByNumberSlipOut(ByVal code As String) As ViewAdmissionsToReportSlipOut
        Using msearch As New MBusqueda
            Dim filter As Object() = {code}
            Dim itemAdmission As XPCollection(Of ViewAdmissionsToReportSlipOut) = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdmissionsToReportSlipOutById, filter)
            If itemAdmission IsNot Nothing AndAlso itemAdmission.Count > 0 Then
                Return itemAdmission(0)
            Else
                Return Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Funcion para obtener el ingreso por numero de ingreso
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Function GetAdmissionNumberByNumberListBilling(ByVal code As String) As ViewAdmissionsToReport
        Using msearch As New MBusqueda
            Dim filter As Object() = {code}
            Dim itemAdmission As XPCollection(Of ViewAdmissionsToReport) = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAdmissionsToReportById, filter)
            If itemAdmission IsNot Nothing AndAlso itemAdmission.Count > 0 Then
                Return itemAdmission(0)
            Else
                Return Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Funcion para obtener la categoria por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetCategoryByCode(ByVal code As String) As Task(Of InvoiceCategories)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetInvoiceCategoryAsync(code, _indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Id = code
            res.Name = "La categoria no existe"
        End If
        Return res
    End Function

    Public Function GetPayrollReport(startDate As Date, endDate As Date, registerStatus As String, cédula As String, startGroupCode As String, endGroupCode As String) As DataTable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPayrollReport(startDate, endDate, registerStatus, cédula, startGroupCode, endGroupCode, Indigo)
    End Function

    'Public Function GetPayrollReport(pStartDate As Date, pEndDate As Date, registerStatus As String, cédula As String) As DataTable
    '    Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPayrollReport(pStartDate, pEndDate, Indigo)
    'End Function

    ''' <summary>
    ''' Funcion para obtener el grupo de contrato por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetCareGroupByCode(ByVal code As String) As Task(Of CareGroup)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetCareGroupAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res.ObjectEmbbeded Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Id = code
            res.ObjectEmbbeded.Name = "el grupo de contrato no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Funcion para obtener la entidad por codigo
    ''' </summary>
    ''' <param name="code">The Code.</param>
    ''' <returns></returns>
    Public Async Function GetHealthAdministratorByCode(ByVal code As String) As Task(Of HealthAdministrator)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetHealthAdministratorAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res.ObjectEmbbeded Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Id = code
            res.ObjectEmbbeded.Name = "La entidad no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Funcion para obtener  el paciente por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetPattientByCode(ByVal code As String) As Task(Of INPACIENT)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoCrystal.GetPatientByIdentificationAsync(code)
        If res.ObjectEmbbeded Is Nothing Then
            res.ObjectEmbbeded.IPCODPACI = code
            res.ObjectEmbbeded.IPNOMCOMP = "el paciente no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Funcion para obtener  la cuenta por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetAccountByCode(ByVal code As String) As Task(Of Domain.Entities.MainAccounts)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountByCodeAsync(code, False, _indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Number = code
            res.Name = "La cuenta no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener el tercero Asincrono
    ''' </summary>
    ''' <param name="Nit">Codigo del tercero</param>
    ''' <returns></returns>
    Public Async Function GetThirdPartyAsync(ByVal Nit As String) As Task(Of Domain.Entities.ThirdParty)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetThirdPartyByNitAsync(Nit, Me._indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Nit = Nit
            res.Name = "El tercero no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Función para obtener el empleado Asíncrono
    ''' </summary>
    ''' <param name="codigoEmpleado">Código del empleado</param>
    ''' <returns></returns>
    Public Async Function GetEmployeeAsync(ByVal codigoEmpleado As String) As Task(Of Domain.Payroll.Entities.Employee)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeAsync(codigoEmpleado, Me._indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Id = codigoEmpleado
            res.EmployeeName = "El empleado no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcio para obterner el centro de costo por codigo 
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCostCenterAsync(ByVal code As String) As Task(Of Domain.Payroll.Entities.CostCenter)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetCostCenterAsync(code, Me._indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Name = "El centro de costo no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Consulta un tipo de documento
    ''' </summary>
    ''' <param name="code">Código del tipo de documento</param>
    ''' <returns>Tipo de documento consultado</returns>
    Public Async Function GetDocumentType(ByVal code As String) As Task(Of Domain.Entities.JournalVoucherTypes)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetDocumentTypeAsync(code)
            If res Is Nothing OrElse res.Id = 0 Then
                res.Code = code
                res.Name = "El tipo de comprobante no existe"
            End If
            Return res
        End Using
    End Function

    ''' <summary>
    ''' consulta un comprobante por consecutivo
    ''' </summary>
    ''' <param name="consecutive">The consecutive.</param>
    ''' <returns></returns>
    Public Async Function GetAccountingDocumenteByConsecutive(ByVal consecutive As String) As Task(Of Domain.Entities.JournalVouchers)

        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetAccountingDocumenteByConsecutiveAsync(consecutive, False)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Consecutive = consecutive
            res.Detail = "El comprobante no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un grupo producto por codigo
    ''' </summary>
    ''' <param name="code">codigo del grupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProductGroup(ByVal code As String) As Task(Of Domain.Entities.ProductGroup)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductGroupAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Name = "El grupo no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un subgrupo producto
    ''' </summary>
    ''' <param name="code">Código Del SubGrupo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetProductSubGroup(ByVal code As String) As Task(Of Domain.Entities.ProductSubGroup)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetProductSubGroupAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Name = "El SubGrupo no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un producto por codigo
    ''' </summary>
    ''' <param name="code">Código Del Producto</param>
    ''' <returns></returns>
    Public Async Function GetInventoryProduct(ByVal code As String) As Task(Of Domain.Entities.InventoryProduct)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetInventoryProductAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Name = "El Producto no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code">Código De Remisión</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRemissionEntranceByCode(ByVal code As String) As Task(Of RemissionEntrance)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRemissionEntranceByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Description = "La Remisión no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener el Proveedor teniendo en cuenta el Nit Del Tercero
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetSupplierByNitThirdParty(ByVal Nit As String) As Task(Of Object)
        Dim obj As Object = New ExpandoObject()
        obj.IdThirdParty = New ExpandoObject()
        obj.IdThirdParty.Nit = ""
        obj.IdThirdParty.Name = ""
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoMaintenance.GetSupplierByNitThirdPartyAsync(Nit, Me._indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            obj.IdThirdParty.Nit = Nit
            obj.IdThirdParty.Name = "El Proveedor no existe"
        Else
            obj.IdThirdParty.Nit = res.ThirdParty.Nit
            obj.IdThirdParty.Name = res.ThirdParty.Name
        End If
        Return obj
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code">Código De Cuenta por Pagar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountPayable(ByVal code As String) As Task(Of AccountPayable)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountPayableAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Coments = "La Cuenta por Pagar no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un cuenta por pagar por su numero de factura
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountsPayableByBillNumber(ByVal BillNumber As String) As Task(Of AccountPayable)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAccountsPayableByBillNumberAsync(BillNumber)
        If res Is Nothing OrElse res.Id = 0 Then
            res.BillNumber = BillNumber
            res.Coments = "La Cuenta por Pagar no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener el cliente teniendo en cuenta el Nit Del Tercero
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetCustomerByNit(ByVal Nit As String) As Task(Of Domain.Entities.Customer)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerByNitAsync(Nit, Me._indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Nit = Nit
            res.Name = "El cliente no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene una nota debito/credito por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPaymentsNote(ByVal code As String) As Task(Of PaymentNotes)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentsNoteAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Comment = "La Nota De Pago no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPaymentTransfer(ByVal code As String) As Task(Of PaymentTransfer)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetPaymentsTransferAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Observations = "El Traslado de Pago no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un Entrance Voucher por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetEntranceVoucher(ByVal code As String) As Task(Of Domain.Entities.EntranceVoucher)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetEntranceVoucherAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Description = "El comprobante de entrada no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un almacen por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetWarehouse(ByVal code As String) As Task(Of Domain.Entities.Warehouse)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetWarehouseAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Name = "El almacen no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un lote por codigo
    ''' </summary>
    ''' <param name="code">Código Del Lote</param>
    ''' <returns></returns>
    Public Function BatchSerialByCode(ByVal code As String) As BatchSerial
        Dim batchSerial As New BatchSerial With {.BatchCode = code}

        'Dim filtroConsulta As String = "BatchCode = " & code
        'Dim batchSerialXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).InventoryService.GetCollection(Of Infrastructure.Data.Xpo.InventoryRepository.BatchSerialXpo)(Nothing, filtroConsulta)(0)
        'If batchSerialXpo IsNot Nothing Then
        '    batchSerial.Barcode = batchSerialXpo.Barcode
        'End If

        Return batchSerial
    End Function

    ''' <summary>
    ''' Obtiene una orden de servicio por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPurchaseOrderByCode(ByVal code As String) As Task(Of PurchaseOrder)
        Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
        Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
        Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
        OperationContext.Current.OutgoingMessageHeaders.Add(header)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetPurchaseOrderByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Description = "la orden de servicio no existe existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' obtiene una remision por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetRemissionDevolutionByCode(ByVal code As String) As Task(Of RemissionDevolution)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoInventory.GetRemissionDevolutionByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Description = "la devolución de remision no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' obtener la nota de cartera por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPortfolioNoteByCode(code As String) As Task(Of PortfolioNote)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioNoteByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Observations = "la nota de cartera no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetCancellationCheckById(id As Integer) As Task(Of CancellationChecks)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCancellationCheckByIdAsync(id)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Id = id
            res.Description = "el cheque cancelado no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' obtiene una caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetcashRegister(ByVal code As String) As Task(Of CashRegisters)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashRegisterAsync(code, Me._indigoSessionValues.AuditMessageWcf)
            If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
                res.ObjectEmbbeded.Code = code
                res.ObjectEmbbeded.Name = "la caja no existe"
            End If
            Return res.ObjectEmbbeded
        End Using
    End Function

    ''' <summary>
    ''' obtener el recibo de caja por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCashReceiptsByCode(code As String) As Task(Of CashReceipts)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptsByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Detail = "el recibo de caja no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetVoucherTransactionByCode(ByVal code As String) As Task(Of Domain.Entities.VoucherTransaction)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetVoucherTransactionAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Detail = "el cheque no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' obtiene un concepto de egreso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetExpenseConcept(ByVal code As String) As Task(Of Domain.Entities.ExpenseConcepts)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetExpenseConceptAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Description = "el concepto de egreso no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' obtiene un concepto de recibo de caja por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetCashReceiptConcept(ByVal code As String) As Task(Of CashReceiptConcepts)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCashReceiptConceptAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Name = "el concepto de recibo de caja no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Async Function GetTreasuryNote(code As String) As Task(Of TreasuryNote)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetTreasuryNoteAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Description = "la nota de tesoreria no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' obtiene un reembolso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetRefund(ByVal code As String) As Task(Of Refunds)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetRefundAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Detail = "el reembolso no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' obtener una Cuenta por Cobrar teniendo en cuenta el Código de la misma
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAccountReceivableByInvoiceNumber(ByVal invoicenumber As String) As Task(Of Domain.Entities.AccountReceivable)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)

            Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetAccountReceivableByInvoiceNumberAsync(invoicenumber)
            If res Is Nothing OrElse res.Id = 0 Then
                res.InvoiceNumber = invoicenumber
                res.Observations = "La Cuenta por Cobrar no existe existe"
            End If
            Return res
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de atencion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCareGroup(ByVal code As String) As Task(Of Domain.Entities.CareGroup)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetCareGroupAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Name = "el grupo de atencion no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' obtener la cuenta bancaria por codigo.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetEntityBankAccount(ByVal code As String) As Task(Of Domain.Entities.EntityBankAccounts)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetEntityBankAccountAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Name = "la cuenta bancaria no existe no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' obtener un traslado teniendo en cuenta el Código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPortfolioTransfersByCode(code As String) As Task(Of PortfolioTransfer)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioTransfersByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = code
            res.Observations = "El Traslado no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Funcion para obtener el traslado o consigancion Asincrono
    ''' </summary>
    ''' <param name="Code">Codigo del traslado o consignacion</param>
    ''' <returns></returns>
    Public Async Function GetVReportConsignmentTransferByCode(ByVal Code As String) As Task(Of Domain.Entities.VReportConsignmentTransfer)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetVReportConsignmentTransferByCodeAsync(Code)
            If res.Row = 0 Then
                res.Code = Code
                res.Detail = "El traslado no existe"
            End If
            Return res
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por el id de la configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia numerica</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Async Function GetNumericSequenseGroup(ByVal id As Int32) As Task(Of List(Of String))
        'Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetNumericSequenseGroupByIdAsync(id)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por su código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetAdvanceByCode(ByVal code As String) As Task(Of AdvancePayments)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPayments.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetAdvanceByCodeAsync(code)
            If res Is Nothing OrElse res.Id = 0 Then
                res.Code = code
                res.Comments = "El Anticipo no existe"
            End If
            Return res
        End Using

    End Function

    ''' <summary>
    ''' Funcion para consultar el usuario por el codigo correspondiente.
    ''' </summary>
    ''' <param name="codigo">codigo as string</param>
    Public Async Function ConsultarUsuarioCodigo(ByVal codigo As String) As Threading.Tasks.Task(Of User)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUserContainerAsync(codigo, Me._indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.UserCode = codigo
            res.UserNameLync = "El Usuario no existe"
        Else
            res.UserCode.Trim()
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un comprobante de egreso por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetVoucherTransactionByCheckNumber(ByVal checkNumber As Integer) As Task(Of Domain.Entities.VoucherTransaction)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetVoucherTransactionByCheckNumberAsync(checkNumber)
            If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
                res.ObjectEmbbeded.CheckNumber = checkNumber
                res.ObjectEmbbeded.Detail = "El Cheque no existe"
            End If
            Return res.ObjectEmbbeded
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    Public Async Function GetCrossingAccount(ByVal code As String, Optional tracking As Boolean = False) As Task(Of CrossingAccount)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GetCrossingAccountAsync(code, Me._indigoSessionValues.AuditMessageWcf, tracking)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Description = "El Cruce de cuentas no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetPortfolioAdvance(ByVal code As String) As Task(Of PortfolioAdvance)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioAdvanceAsync(code, Me._indigoSessionValues.AuditMessageWcf)
        If res Is Nothing OrElse res.ObjectEmbbeded.Id = 0 Then
            res.ObjectEmbbeded.Code = code
            res.ObjectEmbbeded.Observations = "El Anticipo no existe"
        End If
        Return res.ObjectEmbbeded
    End Function

    ''' <summary>
    ''' Funcion para obtener el predio Asincrono
    ''' </summary>
    ''' <param name="Code">Codigo del tercero</param>
    ''' <returns></returns>
    Public Async Function GetTaxesPropertyAsync(ByVal Code As String) As Task(Of Domain.Entities.TaxesProperty)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoTaxes.GetTaxesPropertyByCodeAsync(Code, Me._indigoSessionValues)
        If res Is Nothing OrElse res.Id = 0 Then
            res.Code = Code
            res.Code = "El Predio no existe"
        End If
        Return res
    End Function

    ''' <summary>
    ''' Function by get schedule payment  by code 
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentTreasury(ByVal Code As String) As SchedulePaymentXpo
        Using msearch As New MBusqueda
            Dim res As XPCollection(Of SchedulePaymentXpo) = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetSchedulePaymentTreasuryByCode, Code)
            If res IsNot Nothing AndAlso res.Count > 0 Then
                Return res(0)
            Else    
                Dim resultError As New SchedulePaymentXpo()
                resultError.Code = "La programación de pago no existe"
                Return resultError
            End If
        End Using

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
