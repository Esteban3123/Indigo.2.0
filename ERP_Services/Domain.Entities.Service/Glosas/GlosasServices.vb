#Region "Libraries"
Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Linq
#End Region

Public Class GlosasServices
    Implements IGlosasServices

    Private _customerRepository As ICustomerRepository
    ''' <summary>
    ''' Repositorio de Cuentas por cobrar
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountReceivableRepository As IAccountReceivableRepository

    Private _portFolioRepository As IPortfolioGlosadaRepository

    Private _juridicalDRepository As ITransferJuridicalDebtDRepository

    ''' <summary>
    ''' 
    ''' </summary>
    Private _SettingPortfolioRepository As ISettingPortfolioRepository

    Public Sub New(customerRepository As ICustomerRepository, accountReceivableRepository As IAccountReceivableRepository, portFolioRepository As IPortfolioGlosadaRepository,
                   juridicalDRepository As ITransferJuridicalDebtDRepository, settingPortfolioRepository As ISettingPortfolioRepository)
        If settingPortfolioRepository Is Nothing Then
            Throw New ArgumentException("Repositorio de parámetros de cuentas por cobrar vacio")
        End If
        _SettingPortfolioRepository = settingPortfolioRepository
        _customerRepository = customerRepository
        _accountReceivableRepository = accountReceivableRepository
        _portFolioRepository = portFolioRepository
        _juridicalDRepository = juridicalDRepository
    End Sub

    Public Function TransferJuridicalDebtImportFile(data As List(Of ImportFileRow), customerId As Integer, TransferJuridicalDebtCollectionCId As Integer, ByVal _IdUnitoperating As Integer) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) Implements IGlosasServices.TransferJuridicalDebtImportFile
        Dim listErrors As New List(Of String)
        Dim _customer = _customerRepository.GetCustomerById(customerId)
        'valido que exista el cliente

        If _customer.Id = 0 Then
            listErrors.Add("El cliente seleccionado no existe")
            Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StatusCode = eStatusResult.WARNING, .MessageResult = listErrors}
        End If

        Dim listResult As New List(Of TransferJuridicalDebtCollectionD)
        Dim listStatus As New List(Of String)
        Dim _unReconcileInvoice As Boolean = False
        Dim parametro As SettingPortfolio = _SettingPortfolioRepository.GetSettingPortfolioByIdOperatingUnit(_IdUnitoperating)
        If parametro IsNot Nothing Then
            _unReconcileInvoice = IIf(parametro.UnReconciledInvoice.HasValue, parametro.UnReconciledInvoice.GetValueOrDefault, False)
        End If
        If _unReconcileInvoice Then
            listStatus.Add("10")
            listStatus.Add("13")
        End If
        For Each row In data
            Dim indexRow = row.IndexRow
            Dim billNumber As String
            If IsNumeric(row.Row.Item(0)) Then
                If row.Row.Item(0).ToString().StartsWith("0") Then
                    billNumber = row.Row.Item(0)
                Else
                    billNumber = CInt(row.Row.Item(0)).ToString()
                End If
            Else
                billNumber = row.Row.Item(0)
            End If


            Dim AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberAndCustomer(billNumber, customerId)
            If AccountReceivable Is Nothing Then
                listErrors.Add("La Factura del item " & (indexRow).ToString() & " no existe o no esta asociada al cliente seleccionado")
                Continue For
            End If
            Dim portfolioGlosaValidate = _portFolioRepository.GetPortfolioGlosadaWithAggregates(billNumber)
            Dim result = ValidateInvoice(AccountReceivable, portfolioGlosaValidate, False, _unReconcileInvoice)
            If result.StateResult = False Then
                listErrors.Add(result.Message)
                Continue For
            End If


            Dim billAdded = listResult.Find(Function(x) x.InvoiceNumber = billNumber)
            If billAdded IsNot Nothing Then
                listErrors.Add("La Factura del item " & (indexRow).ToString() & " ya esta repetida")
                Continue For
            End If

            Dim portfolioGlosa = _portFolioRepository.ListInvoicesByNumber(billNumber, _customer.Nit, listStatus)
            Dim TransferJuridicalDebtCollectionD = New TransferJuridicalDebtCollectionD
            If portfolioGlosa IsNot Nothing AndAlso portfolioGlosa.Id > 0 Then
                With TransferJuridicalDebtCollectionD
                    .TransferJuridicalDebtCollectionCId = TransferJuridicalDebtCollectionCId
                    .GlosaPortfolioGlosada = portfolioGlosa
                    .PortfolioGlosaId = portfolioGlosa.Id
                    .AccountReceivableId = AccountReceivable.Id
                    .InvoiceNumber = billNumber.Trim()
                    .AccountReceivableDate = AccountReceivable.AccountReceivableDate
                End With
            Else
                With TransferJuridicalDebtCollectionD
                    .TransferJuridicalDebtCollectionCId = TransferJuridicalDebtCollectionCId
                    .PortfolioGlosaId = Nothing
                    .AccountReceivableId = AccountReceivable.Id
                    .InvoiceNumber = billNumber.Trim()
                    .AccountReceivableDate = AccountReceivable.AccountReceivableDate
                End With
            End If
            listResult.Add(TransferJuridicalDebtCollectionD)
        Next
        Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}


    End Function
    ''' <summary>
    ''' Validar el copiar y pegar en traslado a cobro juridico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TransferJuridicalDebtCopyPaste(data As List(Of List(Of String)), customerId As Integer, TransferJuridicalDebtCollectionCId As Integer, ByVal _IdUnitoperating As Integer) As ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) Implements IGlosasServices.TransferJuridicalDebtCopyPaste
        Dim listErrors As New List(Of String)
        Dim _customer = _customerRepository.GetCustomerById(customerId)
        'valido que exista el cliente

        If _customer.Id = 0 Then
            listErrors.Add("El cliente seleccionado no existe")
            Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StatusCode = eStatusResult.WARNING, .MessageResult = listErrors}
        End If

        Dim listResult As New List(Of TransferJuridicalDebtCollectionD)
        Dim listStatus As New List(Of String)
        Dim _unReconcileInvoice As Boolean = False
        Dim parametro As SettingPortfolio = _SettingPortfolioRepository.GetSettingPortfolioByIdOperatingUnit(_IdUnitoperating)
        If parametro IsNot Nothing Then
            _unReconcileInvoice = IIf(parametro.UnReconciledInvoice.HasValue, parametro.UnReconciledInvoice.GetValueOrDefault, False)
        End If
        If _unReconcileInvoice Then
            listStatus.Add("10")
            listStatus.Add("13")
        End If

        Try
            For i As Integer = 0 To data.Count - 1 Step 1
                'valido la estructura
                If data.Item(i).Count <> 1 Then
                    listErrors.Add("El registro " & (i + 1).ToString() & " tiene una estructura incorrecta")
                    Continue For
                End If

                Dim AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumberAndCustomer(data.Item(i).Item(0), customerId)
                If AccountReceivable Is Nothing Then
                    listErrors.Add("La Factura del item " & (i + 1).ToString() & " no existe o no esta asociada al cliente seleccionado")
                    Continue For
                End If
                Dim portfolioGlosaValidate = _portFolioRepository.GetPortfolioGlosadaWithAggregates(data.Item(i).Item(0))
                Dim result = ValidateInvoice(AccountReceivable, portfolioGlosaValidate, False, _unReconcileInvoice)
                If result.StateResult = False Then
                    listErrors.Add(result.Message)
                    Continue For
                End If

                Dim billAdded = listResult.Find(Function(x) x.InvoiceNumber = data.Item(i).Item(0))
                If billAdded IsNot Nothing Then
                    listErrors.Add("La Factura del item " & (i + 1).ToString() & " ya esta repetida")
                    Continue For
                End If
                Dim portfolioGlosa = _portFolioRepository.ListInvoicesByNumber(data.Item(i).Item(0), _customer.Nit, listStatus)
                Dim TransferJuridicalDebtCollectionD = New TransferJuridicalDebtCollectionD
                If portfolioGlosa IsNot Nothing AndAlso portfolioGlosa.Id > 0 Then
                    With TransferJuridicalDebtCollectionD
                        .TransferJuridicalDebtCollectionCId = TransferJuridicalDebtCollectionCId
                        .GlosaPortfolioGlosada = portfolioGlosa
                        .PortfolioGlosaId = portfolioGlosa.Id
                        .AccountReceivableId = AccountReceivable.Id
                        .InvoiceNumber = data.Item(i).Item(0).Trim()
                        .AccountReceivableDate = AccountReceivable.AccountReceivableDate
                    End With
                Else
                    With TransferJuridicalDebtCollectionD
                        .TransferJuridicalDebtCollectionCId = TransferJuridicalDebtCollectionCId
                        .PortfolioGlosaId = Nothing
                        .AccountReceivableId = AccountReceivable.Id
                        .InvoiceNumber = data.Item(i).Item(0).Trim()
                        .AccountReceivableDate = AccountReceivable.AccountReceivableDate
                    End With
                End If
                listResult.Add(TransferJuridicalDebtCollectionD)
            Next

            Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StatusCode = eStatusResult.SUCCESS, .ObjectEmbbeded = listResult, .MessageResult = listErrors}
        Catch ex As Exception
            Return New ActionResult(Of List(Of TransferJuridicalDebtCollectionD)) With {.StatusCode = eStatusResult.EXCEPTION, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function


    Private Function ValidateInvoice(AccountReceivable As AccountReceivable, portfolioGlosa As GlosaPortfolioGlosada, ByVal isConfirm As Boolean, ByVal _unReconcileInvoice As Boolean) As ActionResult
        Dim result As New StringBuilder
        'For Each item As String In listInvoiceTransferDetailD
        'Dim AccountReceivable As AccountReceivable = _accountReceivableRepository.GetAccountReceivableByInvoiceNumber(item)
        If AccountReceivable IsNot Nothing AndAlso AccountReceivable.Id > 0 Then
            If AccountReceivable.Balance = 0 Then
                result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura sin saldo")
            End If
            'valido estado en cartera 
            Select Case AccountReceivable.PortfolioStatus
                Case 1
                    result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura esta sin radicar")
                Case 2
                    result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura esta radicada sin confirmar")
                Case 4
                    result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura esta radicada sin confirmar")
                Case 15
                    result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura es una cuenta de dificil recaudo")
                Case 16
                    result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura ya esta en un traslado a cobro jurídico Confirmado")
            End Select
        End If
        'valido estado en cartera de glosas '1', '4','8', '9', '10','13'
        'Dim portfolioGlosa = _portFolioRepository.GetPortfolioGlosadaWithAggregates(item)
        If portfolioGlosa IsNot Nothing AndAlso portfolioGlosa.Id > 0 Then
            'valido estado en cartera 
            'si permite agregar factura en estado glosado
            If _unReconcileInvoice Then
                Select Case portfolioGlosa.State
                    Case 13
                        result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura pendiente confirmar pago parcial")
                End Select
            Else
                Select Case portfolioGlosa.State
                    Case 1
                        result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura pendiente de confirmar glosas")
                    Case 4
                        result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura pendiente de confirmar Reiteración")
                    Case 7
                        result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura pendiente confirmar conciliacón")
                    Case 8
                        result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura Conciliada")
                    Case 9
                        result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura Conciliación parcial")
                    Case 13
                        result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura pendiente confirmar pago parcial")
                End Select
            End If

        End If
        If isConfirm = False Then
            Dim TransferD As TransferJuridicalDebtCollectionD = _juridicalDRepository.GetTransferJuridicalDebtDByInvoice(AccountReceivable.InvoiceNumber)
            If TransferD IsNot Nothing AndAlso TransferD.Id > 0 AndAlso {"1", "2"}.Contains(TransferD.TransferJuridicalDebtCollectionC.State) Then
                result.AppendLine(AccountReceivable.InvoiceNumber & " - Factura ya esta en el traslado a cobro jurídico N° " & TransferD.TransferJuridicalDebtCollectionC.JuridicalTransferConsecutive)
            End If
        End If
        ' Next
        If result.Length = 0 Then
            Return New ActionResult With {.StateResult = True}
        Else
            Return New ActionResult With {.StateResult = False, .Message = result.ToString()}
        End If
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _customerRepository = Nothing
            _accountReceivableRepository = Nothing
            _portFolioRepository = Nothing
            _juridicalDRepository = Nothing
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
