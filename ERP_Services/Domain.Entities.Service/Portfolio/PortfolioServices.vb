#Region "Libraries"
Imports System.Data
Imports System.Text
Imports System.Text.RegularExpressions
Imports DevExpress.Spreadsheet
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class PortfolioServices
    Implements IPortfolioService

#Region "Fields"

    Private _accountReceivableRepository As IAccountReceivableRepository
    Private _portfolioAdvanceRepository As IPortfolioAdvanceRepository
    Private _repositoryCloseMont As ICloseMonthRepository
    Private _repositoryMainAccounts As IPUCRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _customerRepository As ICustomerRepository
    Private _costCenterRepository As ICostCenterRepository
    Private _portfolioNoteConceptRepository As IPortfolioNoteConceptRepository
    Private _documentTypeRepository As IDocumentTypeRepository
    Private _settingPortfolioRepository As ISettingPortfolioRepository
    Private _billingInvoiceCategoriesRepository As IBillingInvoiceCategories
    Private _portfolioTransfersRepository As IPortfolioTransferRepository
    Private _portfolioNoteRepository As IPortfolioNoteRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' constructor para copiar y pegar en la traslados
    ''' </summary>
    ''' <param name="accountReceivableRepository"></param>
    ''' <param name="repositoryMainAccounts"></param>
    ''' <remarks></remarks>
    Public Sub New(accountReceivableRepository As IAccountReceivableRepository, repositoryMainAccounts As IPUCRepository, portfolioTransfersRepository As IPortfolioTransferRepository)
        _accountReceivableRepository = accountReceivableRepository
        _repositoryMainAccounts = repositoryMainAccounts
        _portfolioTransfersRepository = portfolioTransfersRepository
    End Sub

    ''' <summary>
    ''' constructor utilizado para notas de cuentas por cobrar y traslados
    ''' </summary>
    ''' <param name="accountReceivableRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(accountReceivableRepository As IAccountReceivableRepository, portfolioAdvanceRepository As IPortfolioAdvanceRepository,
                   repositoryPUC As IPUCRepository, _RepositoryCloseMonth As ICloseMonthRepository)
        _accountReceivableRepository = accountReceivableRepository
        _portfolioAdvanceRepository = portfolioAdvanceRepository
        _repositoryCloseMont = _RepositoryCloseMonth
        _repositoryMainAccounts = repositoryPUC
    End Sub

    ''' <summary>
    ''' constructor utilizado para hacer el copiar y pegar de las facturas en saldos iniciales
    ''' </summary>
    ''' <param name="accountReceivableRepository"></param>
    ''' <param name="repositoryMainAccounts"></param>
    ''' <param name="thirdPartyRepository"></param>
    ''' <param name="customerRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(accountReceivableRepository As IAccountReceivableRepository, repositoryMainAccounts As IPUCRepository, thirdPartyRepository As IThirdPartyRepository, customerRepository As ICustomerRepository,
                   costCenterRepository As ICostCenterRepository, portfolioNoteConceptRepository As IPortfolioNoteConceptRepository, documentTypeRepository As IDocumentTypeRepository,
                   billingInvoiceCategoriesRepository As IBillingInvoiceCategories)
        _accountReceivableRepository = accountReceivableRepository
        _repositoryMainAccounts = repositoryMainAccounts
        _thirdPartyRepository = thirdPartyRepository
        _customerRepository = customerRepository
        _costCenterRepository = costCenterRepository
        _portfolioNoteConceptRepository = portfolioNoteConceptRepository
        _documentTypeRepository = documentTypeRepository
        _billingInvoiceCategoriesRepository = billingInvoiceCategoriesRepository
    End Sub

    ''' <summary>
    ''' constructor utilizado para hacer el copiar y pegar de los anticipos en saldos iniciales
    ''' </summary>
    ''' <param name="repositoryMainAccounts"></param>
    ''' <param name="thirdPartyRepository"></param>
    ''' <param name="customerRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(repositoryMainAccounts As IPUCRepository, thirdPartyRepository As IThirdPartyRepository, customerRepository As ICustomerRepository,
                   costCenterRepository As ICostCenterRepository)
        _repositoryMainAccounts = repositoryMainAccounts
        _thirdPartyRepository = thirdPartyRepository
        _customerRepository = customerRepository
        _costCenterRepository = costCenterRepository
    End Sub

    ''' <summary>
    ''' Constructor utilizados desde la interfaces de glosas al ERP de manera nativa
    ''' </summary>
    ''' <param name="repositoryMainAccounts"></param>
    ''' <remarks></remarks>
    Public Sub New(repositoryMainAccounts As IPUCRepository)
        _repositoryMainAccounts = repositoryMainAccounts
    End Sub

    ''' <summary>
    ''' COnstructor Utilizado Para la creacion de comprobantes contables
    ''' </summary>
    ''' <param name="settingPortfolioRepository"></param>
    ''' <remarks></remarks>
    Public Sub New(settingPortfolioRepository As ISettingPortfolioRepository, repositoryMainAccounts As IPUCRepository, customerRepository As ICustomerRepository)
        _settingPortfolioRepository = settingPortfolioRepository
        _repositoryMainAccounts = repositoryMainAccounts
        _customerRepository = customerRepository
    End Sub

    ''' <summary>
    ''' Constructor para Notas de cartera
    ''' </summary>
    ''' <param name="portfolioNoteRepository"></param>
    Public Sub New(portfolioNoteRepository As IPortfolioNoteRepository)
        _portfolioNoteRepository = portfolioNoteRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para el copiar y pegar de saldos iniciales de facturas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="companyType"></param>
    ''' <returns></returns>
    Public Function SetBillsCopyPaste(data As List(Of List(Of String)), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
        Dim listBills As New List(Of PortfolioInitialBalanceAccountReceivable)
        Dim portfolioInitialBalanceAccountReceivable As PortfolioInitialBalanceAccountReceivable = Nothing
        Dim costCenter As Domain.Payroll.Entities.CostCenter = Nothing
        Dim listErrors As New List(Of String)
        For i As Integer = 0 To data.Count - 1 Step 1

            'valido que el cliente exista
            Dim customer = _customerRepository.GetCustomer(data.Item(i).Item(0))
            If customer.Id = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("CustomerNotExists", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If

            'valido que el numero de la factura no pueda ser mas grande de 20 caracteres
            If data.Item(i).Item(1).Length > 20 Then
                listErrors.Add("El número de factura del item " + (i + 1).ToString() + " tiene una lonitud mayor a 20")
                Continue For
            End If

            'valido si es factura electrónica y CUFE si es necesario
            If data.Item(i).Item(2) IsNot String.Empty Then
                If data.Item(i).Item(2) = 0 Then
                    If data.Item(i).Item(3) IsNot String.Empty Then
                        listErrors.Add("El item " + (i + 1).ToString() + " NO debe tener un CUFE asignado")
                        Continue For
                    End If
                ElseIf data.Item(i).Item(2) = 1 Then
                    If data.Item(i).Item(3) Is String.Empty Then
                        listErrors.Add("El item " + (i + 1).ToString() + " debe tener un CUFE asignado")
                        Continue For
                    Else
                        Dim regex As New Regex("^[a-zA-Z0-9\s\.,-]*$")
                        If Not regex.IsMatch(data.Item(i).Item(3)) Then
                            listErrors.Add("El CUFE del item " + (i + 1).ToString() + " contiene caracteres especiales")
                            Continue For
                        End If
                    End If
                Else
                    listErrors.Add("La columna ¿Es factura electrónica? del item " + (i + 1).ToString() + " posee un valor inválido")
                    Continue For
                End If
            Else
                listErrors.Add("La columna ¿Es factura electrónica? se encuenta vacia en el item " + (i + 1).ToString())
                Continue For
            End If

            'Se valida el estado de la factura
            If data.Item(i).Item(4) Is String.Empty Then
                listErrors.Add("El estado de la factura del item " + (i + 1).ToString() + " esta vacio")
                Continue For
            End If
            If Not IsNumeric(data.Item(i).Item(4)) Then
                listErrors.Add("El estado de factura del item " + (i + 1).ToString() + " no es numerico")
                Continue For
            End If
            If CInt(data.Item(i).Item(4)) <= 0 Or CInt(data.Item(i).Item(4)) > 14 Then
                listErrors.Add("El estado de factura del item " + (i + 1).ToString() + " no tiene un valor requerido")
                Continue For
            End If

            Dim invoiceCategory As InvoiceCategories = Nothing
            'valido la categoria de factura
            If data.Item(i).Item(5) IsNot String.Empty Then
                invoiceCategory = _billingInvoiceCategoriesRepository.GetInvoiceCategory(data.Item(i).Item(5))
                If invoiceCategory.Id = 0 Then
                    listErrors.Add("La categoria de factura del item " + (i + 1).ToString() + " no existe")
                    Continue For
                End If
            End If

            'Se valida columna Fecha
            If Not IsDate(data.Item(i).Item(6)) Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectDate", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If

            'Se valida columna Plazo
            If Not IsNumeric(data.Item(i).Item(7)) Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectTerm", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If

            'Se valida campo número de cuota
            If Not IsNumeric(data.Item(i).Item(8)) Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectShare", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If
            If data.Item(i).Item(8) = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("ZeroShare", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If

            If data.Item(i).Item(9) = String.Empty Then
                listErrors.Add("La cuenta contable del item " + (i + 1).ToString() + " esta vacia")
                Continue For
            End If

            Dim account = _repositoryMainAccounts.GetAccountByCode(data.Item(i).Item(9), False)
            If account.Id = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("AccountNotExists", "Portfolio"), data.Item(i).Item(9), (i + 1).ToString()))
                Continue For
            Else
                If Not account.AllowsMovement Then
                    listErrors.Add("La cuenta contable " + data.Item(i).Item(8) + " del item " + (i + 1).ToString() + "no permite movimientos")
                    Continue For
                ElseIf account.HandlesCostCenter Then
                    If data.Item(i).Item(10) = String.Empty Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterEmpty", "Portfolio"), (i + 1).ToString()))
                        Continue For
                    Else
                        costCenter = _costCenterRepository.GetCostCenter(data.Item(i).Item(10))
                        If costCenter.Id = 0 Then
                            listErrors.Add(String.Format(ResourceManager.GetString("CostCenterNotExists", "Portfolio"), data.Item(i).Item(10), (i + 1).ToString()))
                            Continue For
                        End If
                    End If
                End If
            End If


            'Se valida columna observación
            If data.Item(i).Item(11) Is String.Empty Then
                listErrors.Add("La observación del item " + (i + 1).ToString() + " esta vacia")
                Continue For
            End If

            'Se valida el valor de la factura
            If Not IsNumeric(data.Item(i).Item(12)) Then
                listErrors.Add(String.Format(ResourceManager.GetString("ValueNotNumeric", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If

            If CInt(data.Item(i).Item(12)) <= 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("ValueEmpty", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If

            'Se valida columna Saldo cuenta
            If Not IsNumeric(data.Item(i).Item(13)) Then
                listErrors.Add("El saldo del item " + (i + 1).ToString() + " no es numerico")
                Continue For
            End If
            If CInt(data.Item(i).Item(13)) <= 0 Then
                listErrors.Add("El saldo del item " + (i + 1).ToString() + " esta vacio")
                Continue For
            End If

            'valido que el saldo no sea mayor al valor de la factura
            If CDec(data.Item(i).Item(12)) < CDec(data.Item(i).Item(13)) Then
                listErrors.Add("El saldo del item " + (i + 1).ToString() + " es mayor al valor de la factura")
                Continue For
            End If

            Dim accountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberAndCustomer(data.Item(i).Item(1), customer.Id)
            If accountReceivable IsNot Nothing Then
                listErrors.Add(String.Format(ResourceManager.GetString("CustomerBill", "Portfolio"), data.Item(i).Item(1)))
                Continue For
            End If

            'hago las validaciones de los campos para glosas             
            Dim accountTmp = listBills.Find(Function(x) x.CustomerId = customer.Id And x.InvoiceNumber = data.Item(i).Item(1) And x.AccountReceivableDate = CDate(data.Item(i).Item(6)) And x.Term = CInt(data.Item(i).Item(7)))
            If accountTmp Is Nothing Then
                accountTmp = listBills.Find(Function(x) x.InvoiceNumber = data.Item(i).Item(1))
                If accountTmp IsNot Nothing Then
                    listErrors.Add(String.Format(ResourceManager.GetString("DifferentBills", "Portfolio"), data.Item(i).Item(1), (i + 1).ToString()))
                    Continue For
                End If
                portfolioInitialBalanceAccountReceivable = New PortfolioInitialBalanceAccountReceivable
                With portfolioInitialBalanceAccountReceivable
                    'valido las cuentas de glosas cuando la estructura del item sea mayor a 13
                    If data.Item(i).Count > 14 Then

                        'valido el centro de costos para glosas
                        Dim costCenterGlosas As Domain.Payroll.Entities.CostCenter = Nothing

                        If data.Item(i).Item(14) = String.Empty Then
                            listErrors.Add("El centro de costo de glosas del item " + (i + 1).ToString() + " esta vacío")
                            Continue For
                        Else
                            costCenterGlosas = _costCenterRepository.GetCostCenter(data.Item(i).Item(14))
                            If costCenterGlosas.Id = 0 Then
                                listErrors.Add("El centro de costos de glosas " + data.Item(i).Item(14) + " del item " + (i + 1).ToString() + " no existe")
                                Continue For
                            End If
                        End If

                        .CostCenterId = costCenterGlosas.Id
                        .CodeNameGlosasCostCenter = costCenterGlosas.Code + " - " + costCenterGlosas.Name
                        'validacion cuentas contables
                        'cuenta sin radicar
                        Dim resultAccount = ValidateGlosasAccount(data.Item(i).Item(15), "cuenta sin radicar", (i + 1).ToString())
                        If resultAccount.StateResult = False Then
                            listErrors.Add(resultAccount.Message)
                            Continue For
                        End If
                        .AccountWithoutRadicateId = resultAccount.ObjectEmbbeded.Item1
                        .CodeNameAccountWithoutRadicate = resultAccount.ObjectEmbbeded.Item2
                        'valido que la cuenta no este repetida
                        Dim resultAccountRepeat = RepeatedValidateAccounts(companyType, EGlosasAccounts.AccountWithoutRadicate, data.Item(i), i + 1)
                        If resultAccountRepeat.StateResult = False Then
                            listErrors.AddRange(resultAccountRepeat.MessageResult)
                            Continue For
                        End If

                        'cuenta radicada
                        resultAccount = ValidateGlosasAccount(data.Item(i).Item(16), "cuenta radicada", (i + 1).ToString())
                        If resultAccount.StateResult = False Then
                            listErrors.Add(resultAccount.Message)
                            Continue For
                        End If
                        .AccountRadicateId = resultAccount.ObjectEmbbeded.Item1
                        .CodeNameAccountRadicate = resultAccount.ObjectEmbbeded.Item2
                        'valido que la cuenta no este repetida
                        resultAccountRepeat = RepeatedValidateAccounts(companyType, EGlosasAccounts.AccountRadicate, data.Item(i), i + 1)
                        If resultAccountRepeat.StateResult = False Then
                            listErrors.AddRange(resultAccountRepeat.MessageResult)
                            Continue For
                        End If

                        If companyType = 1 Then 'empresa privada
                            'cuenta glosa subsanable
                            resultAccount = ValidateGlosasAccount(data.Item(i).Item(17), "glosa subsanable", (i + 1).ToString())
                            If resultAccount.StateResult = False Then
                                listErrors.Add(resultAccount.Message)
                                Continue For
                            End If
                            .AccountObjectionRemediedId = resultAccount.ObjectEmbbeded.Item1
                            .CodeNameAccountObjectionRemedied = resultAccount.ObjectEmbbeded.Item2
                            'valido que la cuenta no este repetida
                            resultAccountRepeat = RepeatedValidateAccounts(companyType, EGlosasAccounts.AccountObjectionRemedied, data.Item(i), i + 1)
                            If resultAccountRepeat.StateResult = False Then
                                listErrors.AddRange(resultAccountRepeat.MessageResult)
                                Continue For
                            End If

                            'cuenta conciliacion
                            resultAccount = ValidateGlosasAccount(data.Item(i).Item(18), "conciliacion", (i + 1).ToString())
                            If resultAccount.StateResult = False Then
                                listErrors.Add(resultAccount.Message)
                                Continue For
                            End If
                            .AccountConciliationId = resultAccount.ObjectEmbbeded.Item1
                            .CodeNameAccountConciliation = resultAccount.ObjectEmbbeded.Item2
                            'valido que la cuenta no este repetida
                            resultAccountRepeat = RepeatedValidateAccounts(companyType, EGlosasAccounts.AccountConciliation, data.Item(i), i + 1)
                            If resultAccountRepeat.StateResult = False Then
                                listErrors.AddRange(resultAccountRepeat.MessageResult)
                                Continue For
                            End If

                            'cuenta cobro juridico o dificil recaudo
                            resultAccount = ValidateGlosasAccount(data.Item(i).Item(19), "cobro juridico o dificil recaudo", (i + 1).ToString())
                            If resultAccount.StateResult = False Then
                                listErrors.Add(resultAccount.Message)
                                Continue For
                            End If
                            .AccountLegalCollectionId = resultAccount.ObjectEmbbeded.Item1
                            .CodeNameAccountLegalCollection = resultAccount.ObjectEmbbeded.Item2
                            'valido que la cuenta no este repetida
                            resultAccountRepeat = RepeatedValidateAccounts(companyType, EGlosasAccounts.AccountLegalCollection, data.Item(i), i + 1)
                            If resultAccountRepeat.StateResult = False Then
                                listErrors.AddRange(resultAccountRepeat.MessageResult)
                                Continue For
                            End If

                        Else 'empresa publica
                            'cuenta orden de glosas
                            resultAccount = ValidateGlosasAccount(data.Item(i).Item(20), "orden de glosas", (i + 1).ToString())
                            If resultAccount.StateResult = False Then
                                listErrors.Add(resultAccount.Message)
                                Continue For
                            End If
                            .AccountDebtorOrder = resultAccount.ObjectEmbbeded.Item1
                            .CodeNameAccountDebtorOrder = resultAccount.ObjectEmbbeded.Item2
                            'valido que la cuenta no este repetida
                            resultAccountRepeat = RepeatedValidateAccounts(companyType, EGlosasAccounts.AccountDebtorOrder, data.Item(i), i + 1)
                            If resultAccountRepeat.StateResult = False Then
                                listErrors.AddRange(resultAccountRepeat.MessageResult)
                                Continue For
                            End If

                            'cuenta acreedores glosas
                            resultAccount = ValidateGlosasAccount(data.Item(i).Item(21), "acreedores glosas", (i + 1).ToString())
                            If resultAccount.StateResult = False Then
                                listErrors.Add(resultAccount.Message)
                                Continue For
                            End If
                            .AccountCreditorOrder = resultAccount.ObjectEmbbeded.Item1
                            .CodeNameAccountCreditorOrder = resultAccount.ObjectEmbbeded.Item2
                            'valido que la cuenta no este repetida
                            resultAccountRepeat = RepeatedValidateAccounts(companyType, EGlosasAccounts.AccountCreditorOrder, data.Item(i), i + 1)
                            If resultAccountRepeat.StateResult = False Then
                                listErrors.AddRange(resultAccountRepeat.MessageResult)
                                Continue For
                            End If
                        End If
                    End If

                    .AccountReceivableType = 1
                    .ThirdPartyId = customer.ThirdPartyId
                    .CodeNameCustomer = customer.Nit + " - " + customer.Name
                    .CustomerId = customer.Id
                    .InvoiceNumber = data.Item(i).Item(1)
                    .IsElectronicInvoice = data.Item(i).Item(2)
                    .CUFE = data.Item(i).Item(3)
                    .PortfolioStatus = CInt(data.Item(i).Item(4))
                    If invoiceCategory IsNot Nothing Then
                        .InvoiceCategoryId = invoiceCategory.Id
                    End If
                    .AccountReceivableDate = CDate(data.Item(i).Item(6))
                    .Term = CInt(data.Item(i).Item(7))
                    .ExpiredDate = .AccountReceivableDate.AddDays(.Term)
                    .Observations = data.Item(i).Item(11)
                    .NumberShares = 1
                    .Value = CDec(data.Item(i).Item(12))
                    .Balance = CDec(data.Item(i).Item(13))
                    Dim portfolioInitialBalanceAccounting As New PortfolioInitialBalanceAccountReceivableAccounting
                    portfolioInitialBalanceAccounting.MainAccountId = account.Id
                    portfolioInitialBalanceAccounting.CodeNameMainAccount = account.Number + " - " + account.Name
                    If account.HandlesCostCenter Then
                        portfolioInitialBalanceAccounting.CostCenterId = costCenter.Id
                        portfolioInitialBalanceAccounting.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
                    End If
                    portfolioInitialBalanceAccounting.ThirdPartyId = customer.ThirdPartyId
                    portfolioInitialBalanceAccounting.Value = .Balance
                    .PortfolioInitialBalanceAccountReceivableAccounting.Add(portfolioInitialBalanceAccounting)
                    Dim share As New PortfolioInitialBalanceAccountReceivableShare
                    share.ExpiredDate = .ExpiredDate
                    share.Number = CInt(data.Item(i).Item(8))
                    share.Value = .Balance
                    .PortfolioInitialBalanceAccountReceivableShare.Add(share)
                End With
                listBills.Add(portfolioInitialBalanceAccountReceivable)
            Else
                With accountTmp
                    'valido que el valor de la factura sea el mismo 
                    If CDec(data.Item(i).Item(12)) <> .Value Then
                        listErrors.Add("El valor de la factura " + .InvoiceNumber + " del item " + (i + 1).ToString() + " es diferente a" + .Value.ToString("C0"))
                        Continue For
                    End If
                    'si es a una cuota y a muchas cuentas
                    If data.Item(i).Item(8) = 1 Then
                        Dim portfolioInitialBalanceAccounting As New PortfolioInitialBalanceAccountReceivableAccounting
                        portfolioInitialBalanceAccounting.MainAccountId = account.Id
                        portfolioInitialBalanceAccounting.CodeNameMainAccount = account.Number + " - " + account.Name
                        If account.HandlesCostCenter Then
                            portfolioInitialBalanceAccounting.CostCenterId = costCenter.Id
                            portfolioInitialBalanceAccounting.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
                            Dim accountCostCenterExists = .PortfolioInitialBalanceAccountReceivableAccounting.Where(Function(x) x.MainAccountId = account.Id And x.CostCenterId = costCenter.Id).FirstOrDefault()
                            If accountCostCenterExists IsNot Nothing Then
                                listErrors.Add(String.Format(ResourceManager.GetString("AccountCostCenter", "Portfolio"), data.Item(i).Item(9), (i + 1).ToString()))
                                Continue For
                            End If
                        Else
                            Dim accountExists = .PortfolioInitialBalanceAccountReceivableAccounting.Where(Function(x) x.MainAccountId = account.Id).FirstOrDefault()
                            If accountExists IsNot Nothing Then
                                listErrors.Add(String.Format(ResourceManager.GetString("AccountAdded", "Portfolio"), data.Item(i).Item(9), (i + 1).ToString()))
                                Continue For
                            End If
                        End If
                        portfolioInitialBalanceAccounting.ThirdPartyId = customer.ThirdPartyId
                        portfolioInitialBalanceAccounting.Value = CDec(data.Item(i).Item(13))

                        Dim valueItems = .PortfolioInitialBalanceAccountReceivableAccounting.Sum(Function(x) x.Value) + portfolioInitialBalanceAccounting.Value
                        If valueItems > .Value Then
                            listErrors.Add("El item " + (i + 1).ToString() + " no se puede agregar porque con su saldo se superaria el valor de la factura " + .InvoiceNumber)
                            Continue For
                        End If
                        .PortfolioInitialBalanceAccountReceivableAccounting.Add(portfolioInitialBalanceAccounting)
                        .Balance = .PortfolioInitialBalanceAccountReceivableAccounting.Sum(Function(x) x.Value)
                        .PortfolioInitialBalanceAccountReceivableShare.ElementAt(0).Value = .Balance

                    Else
                        'si es a varias cuotas y a una cuenta
                        Dim shareAdd = .PortfolioInitialBalanceAccountReceivableShare.Where(Function(x) x.Number = data.Item(i).Item(8)).FirstOrDefault()
                        If shareAdd IsNot Nothing Then
                            listErrors.Add("La cuota número " + data.Item(i).Item(8) + " ya esta agregada en la factura " + .InvoiceNumber)
                            Continue For
                        End If
                        Dim differentAccount = .PortfolioInitialBalanceAccountReceivableAccounting.Where(Function(x) x.MainAccountId = account.Id).FirstOrDefault()
                        If differentAccount Is Nothing Then
                            listErrors.Add("La cuenta contable  " + account.Number + " del item " + (i + 1).ToString() + " es diferente y la factura " + .InvoiceNumber + " es a cuotas")
                            Continue For
                        End If

                        Dim share As New PortfolioInitialBalanceAccountReceivableShare
                        With share
                            .Number = CInt(data.Item(i).Item(8))
                            .ExpiredDate = CDate(data.Item(i).Item(6)).AddDays(accountTmp.Term)
                            .Value = CDec(data.Item(i).Item(13))
                        End With

                        Dim valueItems = .PortfolioInitialBalanceAccountReceivableShare.Sum(Function(x) x.Value) + share.Value
                        If valueItems > .Value Then
                            listErrors.Add("El item " + (i + 1).ToString() + " no se puede agregar porque con su saldo se superaria el valor de la factura " + .InvoiceNumber)
                            Continue For
                        End If
                        .NumberShares += 1
                        .PortfolioInitialBalanceAccountReceivableShare.Add(share)
                        .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).Value += CDec(data.Item(i).Item(13))
                        .Balance = .PortfolioInitialBalanceAccountReceivableAccounting.ElementAt(0).Value
                    End If
                End With
            End If
        Next

        ''valido que las facturas con una cuota tengan los datos para glosas
        Dim listTmp = listBills.FindAll(Function(x) x.PortfolioInitialBalanceAccountReceivableShare.Count = 1 And x.CostCenterId Is Nothing)
        If listTmp.Count > 0 Then
            listErrors.Add("Las facturas " + String.Join(",", (From e In listTmp Select e.InvoiceNumber).ToList()) + " tienen solo una cuota y no se asignaron valores para glosas")
            For Each item In listTmp
                listBills.Remove(item)
            Next
        End If


        Return New ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)) With {.ObjectEmbbeded = listBills, .MessageResult = listErrors}
    End Function

    ''' <summary>
    ''' metodo para importar archivo en saldos iniciales
    ''' </summary>
    Public Function SetBillsImportFile(data As List(Of ImportFileRow), companyType As Integer) As ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable))
        Dim listErrors As New List(Of String)
        Dim listBills As New List(Of PortfolioInitialBalanceAccountReceivable)

        Try
            Dim listFilterErrors As New List(Of String)

            'Validar que los registros cuenten con una estructura valida
            listFilterErrors = data.Where(Function(d) d.Row.Count < 12).Select(Function(d) String.Format("El registro {0} no tiene una estructura válida", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) d.Row.Count < 12)
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar que la factura no tenga mas de 20 caracteres
            listFilterErrors = data.Where(Function(d) d.Row.Item(1).ToString().Trim().Length > 20).Select(Function(d) String.Format("El número de factura del item {0} tiene una longitud mayor a 20", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) d.Row.Item(1).ToString().Trim().Length > 20)
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar que el estado de la cartera sea un número
            listFilterErrors = data.Where(Function(d) Not IsNumeric(d.Row.Item(4))).Select(Function(d) String.Format("El estado de factura del item {0} no es numerico", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(4)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar que el estado de la cartera sea un número válido
            listFilterErrors = data.Where(Function(d) Not {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}.Contains(d.Row.Item(4))).Select(Function(d) String.Format("El estado de factura del item {0} no es valor válido", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16}.Contains(d.Row.Item(4)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar fecha sea valida
            listFilterErrors = data.Where(Function(d) Not IsDate(d.Row.Item(6))).Select(Function(d) String.Format(ResourceManager.GetString("IncorrectDate", "Portfolio"), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not IsDate(d.Row.Item(6)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar plazo sea numerico
            listFilterErrors = data.Where(Function(d) Not IsNumeric(d.Row.Item(7))).Select(Function(d) String.Format(ResourceManager.GetString("IncorrectTerm", "Portfolio"), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(7)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar numero de cuotas sea numerico
            listFilterErrors = data.Where(Function(d) Not IsNumeric(d.Row.Item(8))).Select(Function(d) String.Format(ResourceManager.GetString("IncorrectShare", "Portfolio"), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(8)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar que se escriba una observacion
            listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(11))).Select(Function(d) String.Format("La observación del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(11)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar valor sea numerico
            listFilterErrors = data.Where(Function(d) Not IsNumeric(d.Row.Item(12))).Select(Function(d) String.Format(ResourceManager.GetString("ValueNotNumeric", "Portfolio"), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(12)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar que el valor no sea cero o negativo
            listFilterErrors = data.Where(Function(d) CDec(d.Row.Item(12)) <= 0).Select(Function(d) String.Format(ResourceManager.GetString("ValueEmpty", "Portfolio"), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) CDec(d.Row.Item(12)) < 0)
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar saldo sea numerico
            listFilterErrors = data.Where(Function(d) Not IsNumeric(d.Row.Item(13))).Select(Function(d) String.Format("El saldo del item {0} no es numerico", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not IsNumeric(d.Row.Item(13)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar saldo no sea cero o negativo
            listFilterErrors = data.Where(Function(d) CDec(d.Row.Item(13)) <= 0).Select(Function(d) String.Format("El saldo del item {0} esta vacio", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) CDec(d.Row.Item(13)) <= 0)
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar saldo no sea mayor al valor de la factura
            listFilterErrors = data.Where(Function(d) CDec(d.Row.Item(13)) > CDec(d.Row.Item(12))).Select(Function(d) String.Format("El saldo del item {0} es mayor al valor de la factura", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) CDec(d.Row.Item(13)) > CDec(d.Row.Item(12)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar si el cliente no existe
            Dim listCustomerCode = data.Select(Function(d) d.Row.Item(0).ToString()).Distinct().ToList()
            Dim listCustomers = _customerRepository.GetListCustomerPOCO(listCustomerCode)
            listFilterErrors = data.Where(Function(d) Not listCustomers.Any(Function(c) c.Nit = d.Row.Item(0))).Select(Function(d) String.Format(ResourceManager.GetString("CustomerNotExists", "Portfolio"), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not listCustomers.Any(Function(c) c.Nit = d.Row.Item(0)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar si la factura existe
            Dim listInvoiceNumber = data.Select(Function(d) d.Row.Item(1).ToString()).Distinct().ToList()
            Dim listAccountReceivables = _accountReceivableRepository.GetListAccountReceivable(listInvoiceNumber)
            listFilterErrors = data.Where(Function(d) listAccountReceivables.Any(Function(c) c.InvoiceNumber = d.Row.Item(1))).Select(Function(d) String.Format(ResourceManager.GetString("BillExists", "Portfolio"), d.Row.Item(1))).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) listAccountReceivables.Any(Function(c) c.InvoiceNumber = d.Row.Item(1)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar si la categoria no existe
            Dim listInvoiceCategoryCode = data.Where(Function(d) d.Row.Item(5) IsNot Nothing).Select(Function(d) d.Row.Item(5).ToString()).Distinct().ToList()
            Dim listInvoiceCategories = _billingInvoiceCategoriesRepository.GetListInvoiceCategoryPOCO(listInvoiceCategoryCode)
            listFilterErrors = data.Where(Function(d) d.Row.Item(5) IsNot Nothing AndAlso Not listInvoiceCategories.Any(Function(c) c.Code = d.Row.Item(5))).Select(Function(d) String.Format("La categoria de factura del item {0} no existe", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) d.Row.Item(5) IsNot Nothing AndAlso Not listInvoiceCategories.Any(Function(c) c.Code = d.Row.Item(5)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar que se haya agregado una cuenta contable de saldos
            listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(9))).Select(Function(d) String.Format("La cuenta contable de saldos del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(9)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Listados bases
            Dim listAccountNumber = data.Select(Function(d) d.Row.Item(9).ToString()).Distinct().ToList()
            Dim listCostCenterCodes = data.Select(Function(d) d.Row.Item(10)?.ToString()).Distinct().ToList()

            If data.Where(Function(d) d.Row.Count > 12).Any() Then
                'Validar que se haya agregado un centro de costo de glosas
                listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(14))).Select(Function(d) String.Format("El centro de costo de glosas del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(14)))
                    listErrors.AddRange(listFilterErrors)
                End If

                'Validar que se haya agregado una cuenta contable sin radicar
                listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(15))).Select(Function(d) String.Format("La cuenta contable sin radicar del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(15)))
                    listErrors.AddRange(listFilterErrors)
                End If

                'Validar que se haya agregado una cuenta contable radicada
                listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(16))).Select(Function(d) String.Format("La cuenta contable radicada del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(16)))
                    listErrors.AddRange(listFilterErrors)
                End If

                'Validar que se haya agregado una cuenta contable glosa subsanable
                If companyType = 1 Then
                    listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(17))).Select(Function(d) String.Format("La cuenta contable glosa subsanable del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(17)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar que se haya agregado una cuenta contable conciliacion
                    listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(18))).Select(Function(d) String.Format("La cuenta contable conciliacion del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(18)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar que se haya agregado una cuenta contable cobro juridico
                    listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(19))).Select(Function(d) String.Format("La cuenta contable cobro juridico del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(19)))
                        listErrors.AddRange(listFilterErrors)
                    End If
                Else
                    'Validar que se haya agregado una cuenta contable de orden de glosa
                    listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(20))).Select(Function(d) String.Format("La cuenta contable de orden de glosa del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(20)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar que se haya agregado una cuenta contable acreedores glosa
                    listFilterErrors = data.Where(Function(d) String.IsNullOrEmpty(d.Row.Item(21))).Select(Function(d) String.Format("La cuenta contable acreedores glosa del item {0} esta vacia", (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) String.IsNullOrEmpty(d.Row.Item(21)))
                        listErrors.AddRange(listFilterErrors)
                    End If
                End If

                'Incluir datos al Listado base
                listCostCenterCodes = listCostCenterCodes.Union(data.Select(Function(d) d.Row.Item(14).ToString()).Distinct().ToList()).ToList()
                listAccountNumber = listAccountNumber.Union(data.Select(Function(d) d.Row.Item(15).ToString()).Distinct().ToList()).ToList()
                listAccountNumber = listAccountNumber.Union(data.Select(Function(d) d.Row.Item(16).ToString()).Distinct().ToList()).ToList()
                If companyType = 1 Then
                    listAccountNumber = listAccountNumber.Union(data.Select(Function(d) d.Row.Item(17).ToString()).Distinct().ToList()).ToList()
                    listAccountNumber = listAccountNumber.Union(data.Select(Function(d) d.Row.Item(18).ToString()).Distinct().ToList()).ToList()
                    listAccountNumber = listAccountNumber.Union(data.Select(Function(d) d.Row.Item(19).ToString()).Distinct().ToList()).ToList()
                Else
                    listAccountNumber = listAccountNumber.Union(data.Select(Function(d) d.Row.Item(20).ToString()).Distinct().ToList()).ToList()
                    listAccountNumber = listAccountNumber.Union(data.Select(Function(d) d.Row.Item(21).ToString()).Distinct().ToList()).ToList()
                End If
            End If

            'Se obtienen la información de los listados
            Dim listMainAccounts = _repositoryMainAccounts.GetListAccountByCodePOCO(listAccountNumber)
            Dim listCostCenters = _costCenterRepository.GetListCostCenterByCodePOCO(listCostCenterCodes)

            'Validar las cuentas contables
            listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(9))).Select(Function(d) String.Format(ResourceManager.GetString("AccountNotExists", "Portfolio"), d.Row.Item(9), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(9)))
                listErrors.AddRange(listFilterErrors)
            End If

            'Validar cuentas contables manejen movimiento
            listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(9) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable {0} del registro {1} no permite movimientos", d.Row.Item(9), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(9) AndAlso c.AllowsMovement))
                listErrors.AddRange(listFilterErrors)
            End If

            'Se validar los centros de costos
            listFilterErrors = data.Where(Function(d) Not listCostCenters.Any(Function(c) d.Row.Item(10) Is Nothing Or c.Code = d.Row.Item(10))).Select(Function(d) String.Format(ResourceManager.GetString("CostCenterNotExists", "Portfolio"), d.Row.Item(10), (d.IndexRow).ToString())).ToList()
            If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                data.RemoveAll(Function(d) Not listCostCenters.Any(Function(c) c.Code = d.Row.Item(10)))
                listErrors.AddRange(listFilterErrors)
            End If

            If data.Where(Function(d) d.Row.Count > 14).Any() Then
                'Se validar los centros de costos de glosas
                listFilterErrors = data.Where(Function(d) Not listCostCenters.Any(Function(c) c.Code = d.Row.Item(14))).Select(Function(d) String.Format("El centro de costos de glosas {0} del registro {1} no existe", d.Row.Item(14), (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) Not listCostCenters.Any(Function(c) c.Code = d.Row.Item(14)))
                    listErrors.AddRange(listFilterErrors)
                End If

                'Validar las cuentas contables sin radicar
                listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(15))).Select(Function(d) String.Format("La cuenta contable sin radicar {0} del registro {1} no existe", d.Row.Item(15), (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(15)))
                    listErrors.AddRange(listFilterErrors)
                End If

                'Validar cuentas contables manejen movimiento sin radicar
                listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(16) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable sin radicar {0} del registro {1} no permite movimientos", d.Row.Item(16), (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(16) AndAlso c.AllowsMovement))
                    listErrors.AddRange(listFilterErrors)
                End If

                'Validar las cuentas contables radicada
                listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(16))).Select(Function(d) String.Format("La cuenta contable radicada {0} del registro {1} no existe", d.Row.Item(16), (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(16)))
                    listErrors.AddRange(listFilterErrors)
                End If

                'Validar cuentas contables radicada manejen movimiento
                listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(16) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable radicada {0} del registro {1} no permite movimientos", d.Row.Item(16), (d.IndexRow).ToString())).ToList()
                If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                    data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(16) AndAlso c.AllowsMovement))
                    listErrors.AddRange(listFilterErrors)
                End If

                If companyType = 1 Then
                    'Validar las cuentas contables glosa subsanable
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(17))).Select(Function(d) String.Format("La cuenta contable glosa subsanable {0} del registro {1} no existe", d.Row.Item(17), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(17)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar cuentas contables manejen movimiento glosa subsanable
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(17) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable glosa subsanable {0} del registro {1} no permite movimientos", d.Row.Item(17), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(17) AndAlso c.AllowsMovement))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar las cuentas contables conciliacion
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(18))).Select(Function(d) String.Format("La cuenta contable conciliacion {0} del registro {1} no existe", d.Row.Item(18), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(18)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar cuentas contables conciliacion manejen movimiento
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(18) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable conciliacion {0} del registro {1} no permite movimientos", d.Row.Item(18), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(18) AndAlso c.AllowsMovement))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar las cuentas contables cobro juridico
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(19))).Select(Function(d) String.Format("La cuenta contable cobro juridico {0} del registro {1} no existe", d.Row.Item(19), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(19)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar cuentas contables cobro juridico manejen movimiento
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(19) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable cobro juridico {0} del registro {1} no permite movimientos", d.Row.Item(19), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(19) AndAlso c.AllowsMovement))
                        listErrors.AddRange(listFilterErrors)
                    End If
                Else
                    'Validar las cuentas contables orden de glosa
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(20))).Select(Function(d) String.Format("La cuenta contable orden de glosa {0} del registro {1} no existe", d.Row.Item(20), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(20)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar cuentas contables orden de glosa manejen movimiento
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(20) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable orden de glosa {0} del registro {1} no permite movimientos", d.Row.Item(20), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(20) AndAlso c.AllowsMovement))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar las cuentas contables acreedores glosa
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(21))).Select(Function(d) String.Format("La cuenta contable acreedores glosa {0} del registro {1} no existe", d.Row.Item(21), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(21)))
                        listErrors.AddRange(listFilterErrors)
                    End If

                    'Validar cuentas contables acreedores glosa manejen movimiento
                    listFilterErrors = data.Where(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(21) AndAlso c.AllowsMovement)).Select(Function(d) String.Format("La cuenta contable acreedores glosa {0} del registro {1} no permite movimientos", d.Row.Item(21), (d.IndexRow).ToString())).ToList()
                    If listFilterErrors IsNot Nothing AndAlso listFilterErrors.Any Then
                        data.RemoveAll(Function(d) Not listMainAccounts.Any(Function(c) c.Number = d.Row.Item(21) AndAlso c.AllowsMovement))
                        listErrors.AddRange(listFilterErrors)
                    End If
                End If
            End If

            If data.Any() Then
                For Each row In data
                    Dim indexRow = row.IndexRow
                    Dim invoiceNumber = row.Row.Item(1).ToString()

                    Dim portfolioInitialBalanceAccountReceivable = listBills.Where(Function(d) d.InvoiceNumber = invoiceNumber).FirstOrDefault()
                    If portfolioInitialBalanceAccountReceivable Is Nothing Then
                        portfolioInitialBalanceAccountReceivable = New PortfolioInitialBalanceAccountReceivable
                        portfolioInitialBalanceAccountReceivable.AccountReceivableType = 1
                        portfolioInitialBalanceAccountReceivable.ThirdPartyId = listCustomers.FirstOrDefault(Function(d) d.Nit = row.Row.Item(0)).ThirdPartyId
                        portfolioInitialBalanceAccountReceivable.CustomerId = listCustomers.FirstOrDefault(Function(d) d.Nit = row.Row.Item(0)).Id
                        portfolioInitialBalanceAccountReceivable.CodeNameCustomer = listCustomers.FirstOrDefault(Function(d) d.Nit = row.Row.Item(0)).Nit + " - " + listCustomers.FirstOrDefault(Function(d) d.Nit = row.Row.Item(0)).Name
                        If row.Row.Item(5) IsNot Nothing Then
                            portfolioInitialBalanceAccountReceivable.InvoiceCategoryId = listInvoiceCategories.FirstOrDefault(Function(c) c.Code = row.Row.Item(5)).Id
                        End If
                        portfolioInitialBalanceAccountReceivable.InvoiceNumber = row.Row.Item(1)
                        portfolioInitialBalanceAccountReceivable.AccountReceivableDate = row.Row.Item(6)
                        portfolioInitialBalanceAccountReceivable.Term = row.Row.Item(7)
                        portfolioInitialBalanceAccountReceivable.ExpiredDate = portfolioInitialBalanceAccountReceivable.AccountReceivableDate.AddDays(portfolioInitialBalanceAccountReceivable.Term)
                        portfolioInitialBalanceAccountReceivable.Observations = row.Row.Item(11)
                        portfolioInitialBalanceAccountReceivable.PortfolioStatus = row.Row.Item(4)
                        portfolioInitialBalanceAccountReceivable.NumberShares = 1 'row.Row.Item(6)
                        portfolioInitialBalanceAccountReceivable.Value = row.Row.Item(12)
                        portfolioInitialBalanceAccountReceivable.Balance = row.Row.Item(13)
                        If row.Row.Count > 12 Then
                            portfolioInitialBalanceAccountReceivable.CostCenterId = listCostCenters.FirstOrDefault(Function(c) c.Code = row.Row.Item(14)).Id
                            portfolioInitialBalanceAccountReceivable.CodeNameGlosasCostCenter = listCostCenters.FirstOrDefault(Function(c) c.Code = row.Row.Item(14)).Code + " - " + listCostCenters.FirstOrDefault(Function(c) c.Code = row.Row.Item(14)).Name
                            portfolioInitialBalanceAccountReceivable.AccountWithoutRadicateId = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(15)).Id
                            portfolioInitialBalanceAccountReceivable.CodeNameAccountWithoutRadicate = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(15)).Number + " - " + listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(15)).Name
                            portfolioInitialBalanceAccountReceivable.AccountRadicateId = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(16)).Id
                            portfolioInitialBalanceAccountReceivable.CodeNameAccountRadicate = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(16)).Number + " - " + listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(16)).Name
                            If companyType = 1 Then
                                portfolioInitialBalanceAccountReceivable.AccountObjectionRemediedId = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(17)).Id
                                portfolioInitialBalanceAccountReceivable.CodeNameAccountObjectionRemedied = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(17)).Number + " - " + listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(17)).Name
                                portfolioInitialBalanceAccountReceivable.AccountConciliationId = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(18)).Id
                                portfolioInitialBalanceAccountReceivable.CodeNameAccountConciliation = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(18)).Number + " - " + listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(18)).Name
                                portfolioInitialBalanceAccountReceivable.AccountLegalCollectionId = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(19)).Id
                                portfolioInitialBalanceAccountReceivable.CodeNameAccountLegalCollection = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(19)).Number + " - " + listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(19)).Name
                            Else
                                portfolioInitialBalanceAccountReceivable.AccountDebtorOrder = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(20)).Id
                                portfolioInitialBalanceAccountReceivable.CodeNameAccountDebtorOrder = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(20)).Number + " - " + listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(20)).Name
                                portfolioInitialBalanceAccountReceivable.AccountCreditorOrder = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(21)).Id
                                portfolioInitialBalanceAccountReceivable.CodeNameAccountCreditorOrder = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(21)).Number + " - " + listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(21)).Name
                            End If
                        End If
                        listBills.Add(portfolioInitialBalanceAccountReceivable)
                    End If

                    Dim accounting As New PortfolioInitialBalanceAccountReceivableAccounting
                    Dim account = listMainAccounts.FirstOrDefault(Function(c) c.Number = row.Row.Item(9))
                    accounting.MainAccountId = account.Id
                    accounting.CodeNameMainAccount = account.Number + " - " + account.Name
                    If account.HandlesCostCenter Then
                        accounting.CostCenterId = listCostCenters.FirstOrDefault(Function(c) c.Code = row.Row.Item(10)).Id
                        accounting.CodeNameCostCenter = listCostCenters.FirstOrDefault(Function(c) c.Code = row.Row.Item(10)).Code + " - " + listCostCenters.FirstOrDefault(Function(c) c.Code = row.Row.Item(10)).Name
                    End If
                    accounting.ThirdPartyId = portfolioInitialBalanceAccountReceivable.ThirdPartyId
                    accounting.Value = row.Row.Item(12)
                    portfolioInitialBalanceAccountReceivable.PortfolioInitialBalanceAccountReceivableAccounting.Add(accounting)

                    Dim share As New PortfolioInitialBalanceAccountReceivableShare
                    share.ExpiredDate = portfolioInitialBalanceAccountReceivable.ExpiredDate
                    share.Number = row.Row.Item(8)
                    share.Value = row.Row.Item(13)
                    portfolioInitialBalanceAccountReceivable.PortfolioInitialBalanceAccountReceivableShare.Add(share)
                Next
            End If

            Return New ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)) With {.ObjectEmbbeded = listBills, .MessageResult = listErrors}
        Catch ex As Exception
            listErrors.Add(Utils.GetInnerExceptionMessageToString(ex))
            Return New ActionResult(Of List(Of PortfolioInitialBalanceAccountReceivable)) With {.ObjectEmbbeded = listBills, .MessageResult = listErrors}
        End Try
    End Function

    ''' <summary>
    ''' Metodo para el copiar y pegar de saldos iniciales de anticipos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetAdvancesCopyPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of PortfolioInitialBalanceAdvance))
        Dim listAdvances As New List(Of PortfolioInitialBalanceAdvance)
        Dim portfolioInitialBalanceAdvance As PortfolioInitialBalanceAdvance = Nothing
        Dim costCenter As Domain.Payroll.Entities.CostCenter = Nothing
        Dim listErrors As New List(Of String)
        For i As Integer = 0 To data.Count - 1 Step 1
            If data.Item(i).Count <> 8 Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectStructure", "Treasury"), (i + 1).ToString()))
                Continue For
            End If
            If Not IsNumeric(data.Item(i).Item(0)) Then
                listErrors.Add("El tipo del item " + (i + 1).ToString() + " no es numerico")
                Continue For
            End If
            If data.Item(i).Item(0) < 1 Or data.Item(i).Item(0) > 2 Then
                listErrors.Add("El tipo del item " + (i + 1).ToString() + " debe ser 1 o 2")
                Continue For
            End If
            Dim customer As Customer = Nothing
            Dim thirdParty As ThirdParty = Nothing
            If data.Item(i).Item(0) = 1 Then
                customer = _customerRepository.GetCustomer(data.Item(i).Item(2))
                If customer.Id = 0 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("CustomerNotExists", "Portfolio"), (i + 1).ToString()))
                    Continue For
                End If
                thirdParty = _thirdPartyRepository.GetThirdPartyById(customer.ThirdPartyId)
            Else
                thirdParty = _thirdPartyRepository.GetThirdPartyByNit(data.Item(i).Item(1))
                If thirdParty.Id = 0 Then
                    listErrors.Add("El tercero del item " + (i + 1).ToString() + " no existe")
                    Continue For
                End If
            End If

            If Not IsDate(data.Item(i).Item(3)) Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectDate", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If
            Dim account = _repositoryMainAccounts.GetAccountByCode(data.Item(i).Item(4), False)
            If account.Id = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("AccountNotExists", "Portfolio"), data.Item(i).Item(4), (i + 1).ToString()))
                Continue For
            Else
                If Not account.AllowsMovement Then
                    listErrors.Add(String.Format("La cuenta contable " + data.Item(i).Item(4) + " del item " + (i + 1).ToString() + " no permite movimientos"))
                    Continue For
                ElseIf account.HandlesCostCenter Then
                    If data.Item(i).Item(5) = String.Empty Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterEmpty", "Portfolio"), (i + 1).ToString()))
                        Continue For
                    Else
                        costCenter = _costCenterRepository.GetCostCenter(data.Item(i).Item(5))
                        If costCenter.Id = 0 Then
                            listErrors.Add(String.Format(ResourceManager.GetString("CostCenterNotExists", "Portfolio"), data.Item(i).Item(5), (i + 1).ToString()))
                            Continue For
                        End If
                    End If
                End If
            End If
            If Not IsNumeric(data.Item(i).Item(7)) Then
                listErrors.Add(String.Format(ResourceManager.GetString("ValueNotNumeric", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If
            If CDec(data.Item(i).Item(7)) <= 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("ValueEmpty", "Portfolio"), (i + 1).ToString()))
                Continue For
            End If
            portfolioInitialBalanceAdvance = New PortfolioInitialBalanceAdvance
            With portfolioInitialBalanceAdvance
                .Type = CInt(data.Item(i).Item(0))
                .ThirdPartyId = thirdParty.Id
                .NitNameThirdParty = thirdParty.Nit + " - " + thirdParty.Name
                .MainAccountId = account.Id
                .CodeNameMainAccount = account.Number + " - " + account.Name
                If costCenter IsNot Nothing Then
                    .CostCenterId = costCenter.Id
                    .CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
                End If
                .DocumentDate = CDate(data.Item(i).Item(3))
                If customer IsNot Nothing Then
                    .CustomerId = customer.Id
                    .CodeNameCustomer = customer.Nit + " - " + customer.Name
                End If

                .Value = CDec(data.Item(i).Item(7))
                .Observations = data.Item(i).Item(6)
            End With
            listAdvances.Add(portfolioInitialBalanceAdvance)
        Next
        Return New ActionResult(Of List(Of PortfolioInitialBalanceAdvance)) With {.ObjectEmbbeded = listAdvances, .MessageResult = listErrors}
    End Function

    ''' <summary>
    ''' Metodo para el copiar y pegar o importar de facturas o anticipos de nota de cartera
    ''' </summary>
    ''' <param name="dataImportFile"></param>
    ''' <param name="dataCopyPaste"></param>
    ''' <param name="CompanyType"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    Public Function SetCopyPasteOrImportFilePortfolioNote(dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String)), CompanyType As Integer, OperatingUnit As Integer, ParamArray parameters As Object()) As ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance)) Implements IPortfolioService.SetCopyPasteOrImportFilePortfolioNote
        If Not (dataImportFile IsNot Nothing AndAlso dataImportFile.Count > 0) AndAlso Not (dataCopyPaste IsNot Nothing AndAlso dataCopyPaste.Count > 0) Then
            Throw New ArgumentNullException("data")
        End If
        If parameters Is Nothing OrElse parameters.Count <> 4 Then
            Throw New ArgumentNullException("parameters")
        End If

        Try
            'Objeto xml
            Dim xmlParameters As String = String.Empty
            Dim xmlObject As String = String.Empty
            'Listado de datos copiados o importados que se devuelven a la rejilla del form
            Dim ListPortfolioNoteAccountReceivableAdvance As New List(Of PortfolioNoteAccountReceivableAdvance)
            'Listado de errores
            Dim listErrors As New List(Of String)

            'Conversión a string del Objeto
            xmlParameters = ConvertToXmlParametersPortfolioNoteAccountReceivableAdvance(CompanyType, OperatingUnit, parameters)
            If dataImportFile IsNot Nothing Then
                xmlObject = ConvertToXmlImportFilePortfolioNoteAccountReceivableAdvance(CompanyType, parameters.ElementAt(0), dataImportFile)
            Else
                xmlObject = ConvertToXmlCopyPastePortfolioNoteAccountReceivableAdvance(CompanyType, parameters.ElementAt(0), dataCopyPaste)
            End If

            'Se consume el procedimiento almacenado
            Dim resultStore = Me._portfolioNoteRepository.SP_CopyAndPastePortfolioNoteAccountReceivableAdvance(xmlParameters, xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListPortfolioNoteAccountReceivableAdvance.Add(New PortfolioNoteAccountReceivableAdvance With
                        {
                            .AccountReceivableId = itemXml.AccountReceivableId,
                            .InvoiceNumber = itemXml.InvoiceNumber,
                            .AccountReceivableShareId = itemXml.AccountReceivableShareId,
                            .NumberShare = itemXml.Number,
                            .MainAccountId = itemXml.MainAccountId,
                            .CodeNameMainAccount = itemXml.MainAccountNumberName,
                            .AccountReceivableAccountingId = itemXml.AccountReceivableAccountingId,
                            .PortfolioAdvanceId = itemXml.PortfolioAdvanceId,
                            .CodeAdvance = itemXml.PortfolioAdvanceCode,
                            .Nature = parameters.ElementAt(2),
                            .AdjusmentValue = itemXml.AdjusmentValue,
                            .Value = itemXml.Value,
                            .Balance = itemXml.Balance,
                            .PortfolioStatusName = itemXml.PortfolioStatusName,
                            .ConceptId = itemXml.FEConcept
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        listErrors.Add(itemXml.MessageField)
                    End If
                Next
            End If

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance)) With {.StateResult = True, .ObjectEmbbeded = ListPortfolioNoteAccountReceivableAdvance, .MessageResult = listErrors}
        Catch ex As SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of PortfolioNoteAccountReceivableAdvance)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo para validar que las cuentas contables de glosas no se repitan cuando se importa un archivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RepeatedValidateAccounts(companyType As Integer, accountType As EGlosasAccounts, row As List(Of Object), index As Integer) As ActionResult
        Dim errors As New List(Of String)
        If companyType = 1 Then
            Select Case accountType
                Case EGlosasAccounts.AccountWithoutRadicate
                    If row.Item(13) = row.Item(14) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable radicada")
                    End If
                    If row.Item(13) = row.Item(15) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para glosa subsanable")
                    End If
                    If row.Item(13) = row.Item(16) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para conciliación")
                    End If
                    If row.Item(13) = row.Item(17) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountRadicate
                    If row.Item(14) = row.Item(15) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para glosa subsanable")
                    End If
                    If row.Item(14) = row.Item(16) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para conciliación")
                    End If
                    If row.Item(14) = row.Item(17) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountObjectionRemedied
                    If row.Item(15) = row.Item(16) Then
                        errors.Add("La cuenta contable de glosa subsanable del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para conciliación")
                    End If
                    If row.Item(15) = row.Item(17) Then
                        errors.Add("La cuenta contable de glosa subsanable del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountConciliation
                    If row.Item(16) = row.Item(17) Then
                        errors.Add("La cuenta contable de conciliación del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountLegalCollection
            End Select
        Else
            Select Case accountType
                Case EGlosasAccounts.AccountWithoutRadicate
                    If row.Item(13) = row.Item(14) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable radicada")
                    End If
                    If row.Item(13) = row.Item(18) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para orden de glosa")
                    End If
                    If row.Item(13) = row.Item(19) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para acreedores de glosa")
                    End If
                Case EGlosasAccounts.AccountRadicate
                    If row.Item(14) = row.Item(18) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para orden de glosa")
                    End If
                    If row.Item(14) = row.Item(19) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para acreedores de glosa")
                    End If
                Case EGlosasAccounts.AccountDebtorOrder
                    If row.Item(18) = row.Item(19) Then
                        errors.Add("La cuenta contable para orden de glosa del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para acreedores de glosa")
                    End If
                Case EGlosasAccounts.AccountCreditorOrder
            End Select
        End If
        If errors.Count > 0 Then
            Return New ActionResult With {.StateResult = False, .MessageResult = errors}
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' metodo para validar que las cuentas contables de glosas no se repitan cuando se hace copiar y pegar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RepeatedValidateAccounts(companyType As Integer, accountType As EGlosasAccounts, row As List(Of String), index As Integer) As ActionResult
        Dim errors As New List(Of String)
        If companyType = 1 Then
            Select Case accountType
                Case EGlosasAccounts.AccountWithoutRadicate
                    If row.Item(13) = row.Item(14) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable radicada")
                    End If
                    If row.Item(13) = row.Item(15) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para glosa subsanable")
                    End If
                    If row.Item(13) = row.Item(16) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para conciliación")
                    End If
                    If row.Item(13) = row.Item(17) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountRadicate
                    If row.Item(14) = row.Item(15) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para glosa subsanable")
                    End If
                    If row.Item(14) = row.Item(16) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para conciliación")
                    End If
                    If row.Item(14) = row.Item(17) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountObjectionRemedied
                    If row.Item(15) = row.Item(16) Then
                        errors.Add("La cuenta contable de glosa subsanable del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para conciliación")
                    End If
                    If row.Item(15) = row.Item(17) Then
                        errors.Add("La cuenta contable de glosa subsanable del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountConciliation
                    If row.Item(16) = row.Item(17) Then
                        errors.Add("La cuenta contable de conciliación del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para cobro juridico")
                    End If
                Case EGlosasAccounts.AccountLegalCollection
            End Select
        Else
            Select Case accountType
                Case EGlosasAccounts.AccountWithoutRadicate
                    If row.Item(13) = row.Item(14) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable radicada")
                    End If
                    If row.Item(13) = row.Item(18) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para orden de glosa")
                    End If
                    If row.Item(13) = row.Item(19) Then
                        errors.Add("La cuenta contable sin radicar del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para acreedores de glosa")
                    End If
                Case EGlosasAccounts.AccountRadicate
                    If row.Item(14) = row.Item(18) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para orden de glosa")
                    End If
                    If row.Item(14) = row.Item(19) Then
                        errors.Add("La cuenta contable radicada del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para acreedores de glosa")
                    End If
                Case EGlosasAccounts.AccountDebtorOrder
                    If row.Item(18) = row.Item(19) Then
                        errors.Add("La cuenta contable para orden de glosa del item" + (index + 1).ToString() + " esta repetida con la cuenta contable para acreedores de glosa")
                    End If
                Case EGlosasAccounts.AccountCreditorOrder
            End Select
        End If
        If errors.Count > 0 Then
            Return New ActionResult With {.StateResult = False, .MessageResult = errors}
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Valido que los valores credito y debito sean iguales y no este desbalanceado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ValidateCreditDebit(ByVal accounting As JournalVouchers) As Boolean
        Dim valCredit As Decimal = 0
        Dim valDebit As Decimal = 0
        For Each itemDetail As JournalVoucherDetails In accounting.JournalVoucherDetails
            If itemDetail.CreditValue > 0 Then
                valCredit = valCredit + itemDetail.CreditValue
            ElseIf itemDetail.DebitValue > 0 Then
                valDebit = valDebit + itemDetail.DebitValue
            End If
        Next
        If valCredit <> valDebit Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' metodo para validar las cuentas contables que se estan ingresando
    ''' </summary>
    ''' <param name="accountCode">numero de la cuenta contable</param>
    ''' <param name="accountSource">para que se va a utilizar la cuenta seleccionada</param>
    ''' <param name="index">item que se esta recorriendo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateGlosasAccount(accountCode As String, accountSource As String, index As String) As ActionResult(Of Tuple(Of Integer, String))
        If accountCode Is String.Empty Then
            Return New ActionResult(Of Tuple(Of Integer, String)) With {.StateResult = False, .Message = "La cuenta contable para " + accountSource + " del item " + index + " esta vacía"}
        End If
        Dim account = _repositoryMainAccounts.GetAccountByCodePOCO(accountCode, False)
        If account Is Nothing OrElse account.Id = 0 Then
            Return New ActionResult(Of Tuple(Of Integer, String)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("AccountGlosasNotExists", "Portfolio"), accountCode, accountSource, index)}
        Else
            If Not account.AllowsMovement Then
                Return New ActionResult(Of Tuple(Of Integer, String)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("AccountGlosasNotAux", "Portfolio"), accountCode, accountSource, index)}

            ElseIf account.HandlesCostCenter Then
                Return New ActionResult(Of Tuple(Of Integer, String)) With {.StateResult = False, .Message = String.Format(ResourceManager.GetString("AccountGlosasHandlesCostCenter", "Portfolio"), accountCode, accountSource, index)}
            End If
        End If
        Return New ActionResult(Of Tuple(Of Integer, String)) With {.StateResult = True, .ObjectEmbbeded = New Tuple(Of Integer, String)(account.Id, String.Concat(account.Number, " - ", account.Name))}
    End Function

    Private Function ConvertToXmlParametersPortfolioNoteAccountReceivableAdvance(CompanyType As Integer, OperatingUnit As Integer, Parameters As Object())
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")
        builder.Append("<Row>")

        builder.Append("<CompanyType>" & CompanyType & "</CompanyType>")
        builder.Append("<OperatingUnit>" & OperatingUnit & "</OperatingUnit>")
        builder.Append("<NoteType>" & Parameters.ElementAt(0) & "</NoteType>")
        builder.Append("<ThirdPartyId>" & Parameters.ElementAt(1) & "</ThirdPartyId>")
        builder.Append("<Nature>" & Parameters.ElementAt(2) & "</Nature>")
        builder.Append("<CurrencyId>" & Parameters.ElementAt(3) & "</CurrencyId>")

        builder.Append("</Row>")
        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlImportFilePortfolioNoteAccountReceivableAdvance(CompanyType As Integer, NoteType As Integer, dataImportFile As List(Of ImportFileRow))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each importFile In dataImportFile
            Dim indexRow = importFile.IndexRow
            Dim columns = importFile.Row.Count

            builder.Append("<Row>")
            builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
            builder.Append("<RowColumns>" & columns & "</RowColumns>")

            If NoteType = 1 Then
                If (CompanyType = 2 AndAlso columns >= 2) Then
                    builder.Append("<InvoiceNumber>" & importFile.Row.Item(0) & "</InvoiceNumber>")
                    builder.Append("<FEConcept>" & importFile.Row.Item(1) & "</FEConcept>")
                    builder.Append("<Value>" & If(importFile.Row.Item(2) Is Nothing, 0, importFile.Row.Item(2).ToString.Replace(",", ".")) & "</Value>")
                Else
                    builder.Append("<InvoiceNumber>" & importFile.Row.Item(0) & "</InvoiceNumber>")
                    builder.Append("<SpecificPortfolioStatus>" & importFile.Row.Item(1) & "</SpecificPortfolioStatus>")
                    builder.Append("<FEConcept>" & importFile.Row.Item(2) & "</FEConcept>")
                    builder.Append("<Value>" & If(importFile.Row.Item(3) Is Nothing, 0, importFile.Row.Item(3).ToString.Replace(",", ".")) & "</Value>")
                End If
            ElseIf NoteType = 2 Then
                If (columns >= 3) Then
                    builder.Append("<InvoiceNumber>" & importFile.Row.Item(0) & "</InvoiceNumber>")
                    builder.Append("<ShareNumber>" & importFile.Row.Item(1) & "</ShareNumber>")
                    builder.Append("<Value>" & If(importFile.Row.Item(2) Is Nothing, 0, importFile.Row.Item(2).ToString.Replace(",", ".")) & "</Value>")
                End If
            ElseIf NoteType = 3 Then
                If (columns >= 2) Then
                    builder.Append("<PortfolioAdvanceCode>" & importFile.Row.Item(0) & "</PortfolioAdvanceCode>")
                    builder.Append("<Value>" & If(importFile.Row.Item(1) Is Nothing, 0, importFile.Row.Item(1).ToString.Replace(",", ".")) & "</Value>")
                End If
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlCopyPastePortfolioNoteAccountReceivableAdvance(CompanyType As Integer, NoteType As Integer, data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim indexRow As Integer = 0
        For Each item In data
            indexRow = indexRow + 1
            Dim columns = item.Count

            builder.Append("<Row>")
            builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
            builder.Append("<RowColumns>" & columns & "</RowColumns>")

            If NoteType = 1 Then
                If CompanyType = 1 Then
                    If columns >= 4 Then
                        builder.Append("<InvoiceNumber>" & item(0) & "</InvoiceNumber>")
                        builder.Append("<SpecificPortfolioStatus>" & item(1) & "</SpecificPortfolioStatus>")
                        builder.Append("<FEConcept>" & item(2) & "</FEConcept>")
                        builder.Append("<Value>" & If(item(3) Is Nothing, 0, item(3).ToString.Replace(",", ".")) & "</Value>")
                    End If
                Else
                    If columns >= 3 Then
                        builder.Append("<InvoiceNumber>" & item(0) & "</InvoiceNumber>")
                        builder.Append("<FEConcept>" & item(1) & "</FEConcept>")
                        builder.Append("<Value>" & If(item(2) Is Nothing, 0, item(2).ToString.Replace(",", ".")) & "</Value>")
                    End If
                End If
            ElseIf NoteType = 2 Then
                If (columns >= 3) Then
                    builder.Append("<InvoiceNumber>" & item(0) & "</InvoiceNumber>")
                    builder.Append("<ShareNumber>" & item(1) & "</ShareNumber>")
                    builder.Append("<Value>" & If(item(2) Is Nothing, 0, item(2).ToString.Replace(",", ".")) & "</Value>")
                End If
            ElseIf NoteType = 3 Then
                If (columns >= 2) Then
                    builder.Append("<PortfolioAdvanceCode>" & item(0) & "</PortfolioAdvanceCode>")
                    builder.Append("<Value>" & If(item(1) Is Nothing, 0, item(1).ToString.Replace(",", ".")) & "</Value>")
                End If
            End If

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#Region "Functions"

    ''' <summary>
    ''' Crea el comprobante contable para los documentos de reclasificacion de cartera - Glosas
    ''' </summary>
    ''' <param name="_IdJournalVoucher">Id tipo de documento</param>
    ''' <param name="_Coments">comentario de la cabecera</param>
    ''' <param name="_EntityCode">codigo de la entidad que genera el Documento</param>
    ''' <param name="_EntityId">Id de la entidad que genera el documento</param>
    ''' <param name="_EntityName">Nombre de la entidad que genera el documento</param>
    ''' <param name="_IdAccountCredit">Id cuenta contable que acredita</param>
    ''' <param name="_IdAccountDebit">Id cuenta contable que debita</param>
    ''' <param name="_IdThirdParty">Id del tercero</param>
    ''' <param name="_ComentsDetails">Comentario del detalle</param>
    ''' <param name="_Value">valor del movimiento</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateAccountingAccount(_IdJournalVoucher As Integer, _Coments As String, _EntityCode As String, _EntityId As Integer, _EntityName As String,
                                                          _IdAccountCredit As Integer, _IdAccountDebit As Integer, _IdThirdParty As Integer, _ComentsDetails As String, _Value As Decimal, _CostCenterId As Integer?) As JournalVouchers Implements IPortfolioService.CreateAccountingAccount
        Dim IdCenterCost1 As Integer? = Nothing
        Dim IdCenterCost2 As Integer? = Nothing
        Dim account1 = _repositoryMainAccounts.GetAccountById(_IdAccountCredit, False)
        If account1.HandlesCostCenter = True Then
            IdCenterCost1 = _CostCenterId
        Else
            IdCenterCost1 = Nothing
        End If
        Dim account2 = _repositoryMainAccounts.GetAccountById(_IdAccountDebit, False)
        If account2.HandlesCostCenter = True Then
            IdCenterCost2 = _CostCenterId
        Else
            IdCenterCost2 = Nothing
        End If
        Dim accounting As New Domain.Entities.JournalVouchers
        With accounting
            .IdJournalVoucher = _IdJournalVoucher
            .VoucherDate = Date.Now
            .Status = 2 'confirmado
            .Detail = _Coments
            .EntityCode = _EntityCode
            .EntityId = _EntityId
            .EntityName = _EntityName
            .IsClosedYear = False
            Dim accountDetail As New JournalVoucherDetails
            With accountDetail
                .IdMainAccount = _IdAccountCredit
                .IdThirdParty = _IdThirdParty
                .IdCostCenter = IdCenterCost1
                .DebitValue = 0
                .CreditValue = _Value
                .Detail = _ComentsDetails
                .IdRetention = Nothing
                .RetentionRate = Nothing
            End With
            accounting.JournalVoucherDetails.Add(accountDetail)
            Dim accountDetail2 As New JournalVoucherDetails
            With accountDetail2
                .IdMainAccount = _IdAccountDebit
                .IdThirdParty = _IdThirdParty
                .IdCostCenter = IdCenterCost2
                .DebitValue = _Value
                .CreditValue = 0
                .Detail = _ComentsDetails
                .IdRetention = Nothing
                .RetentionRate = Nothing
            End With
            accounting.JournalVoucherDetails.Add(accountDetail2)
        End With
        Return accounting
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _accountReceivableRepository = Nothing
            _portfolioAdvanceRepository = Nothing
            _repositoryCloseMont = Nothing
            _repositoryMainAccounts = Nothing
            _thirdPartyRepository = Nothing
            _customerRepository = Nothing
            _costCenterRepository = Nothing
            _portfolioNoteConceptRepository = Nothing
            _documentTypeRepository = Nothing
            _settingPortfolioRepository = Nothing
            _billingInvoiceCategoriesRepository = Nothing
            _portfolioTransfersRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class

''' <summary>
''' enumeracion para las cuentas de glosas
''' </summary>
''' <remarks></remarks>
Public Enum EGlosasAccounts As Integer
    ''' <summary>
    ''' cuenta sin radicar
    ''' </summary>
    ''' <remarks></remarks>
    AccountWithoutRadicate = 1
    ''' <summary>
    ''' radicada
    ''' </summary>
    ''' <remarks></remarks>
    AccountRadicate = 2
    ''' <summary>
    ''' cuenta glosa subsanable
    ''' </summary>
    ''' <remarks></remarks>
    AccountObjectionRemedied = 3
    ''' <summary>
    ''' conciliacion
    ''' </summary>
    ''' <remarks></remarks>
    AccountConciliation = 4
    ''' <summary>
    ''' cobro juridico
    ''' </summary>
    ''' <remarks></remarks>
    AccountLegalCollection = 5
    ''' <summary>
    ''' orden de glosas
    ''' </summary>
    ''' <remarks></remarks>
    AccountDebtorOrder = 6
    ''' <summary>
    ''' acreedores de glosas
    ''' </summary>
    ''' <remarks></remarks>
    AccountCreditorOrder = 7
End Enum