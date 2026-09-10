'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 20-06-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Imports"

Imports System.Globalization
Imports System.Text
Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources

#End Region

Public Class AccountingServices

#Region "Fields"
    Private _closeMonthRepository As ICloseMonthRepository
    Private _mainAccountsRepository As IPUCRepository
    Private _thirdPartyRepository As IThirdPartyRepository
    Private _journalVoucherRepository As IAccountingDocumentRepository
#End Region

#Region "Builders"

    Public Sub New(_repositoryMainAccounts As IPUCRepository, thirdPartyRepository As IThirdPartyRepository, journalVoucherRepository As IAccountingDocumentRepository)
        _mainAccountsRepository = _repositoryMainAccounts
        _thirdPartyRepository = thirdPartyRepository
        _journalVoucherRepository = journalVoucherRepository
    End Sub

    Public Sub New(ByVal repository As IPUCRepository, ByVal _RepositoryCloseMonth As ICloseMonthRepository)
        _closeMonthRepository = _RepositoryCloseMonth
        _mainAccountsRepository = repository
    End Sub
#End Region

    ''' <summary>
    ''' Método para calcular la retención cuando sea de tipo variable(3)
    ''' </summary>
    ''' <returns></returns>
    Public Function CalculateRetention(BaseValue As Decimal, MinBase As Decimal, Percentaje As Decimal, ThirdPartyId As Integer, MainAccountId As Integer, Optional TypeRounding As Byte = 0) As ActionResult(Of Object)
        If BaseValue = 0 Then 'Se valida el valor base
            Return New ActionResult(Of Object) With {.StateResult = False, .Message = "El valor base no puede ser cero"}
        End If
        If BaseValue < MinBase Then 'El valor base no puede ser menor al valor minimo del concepto de retencion
            Return New ActionResult(Of Object) With {.StateResult = False, .Message = "El valor base debe ser mayor al valor mínimo base(" + MinBase.ToString + ") del concepto de retención"}
        End If
        If Percentaje = 0 Then 'Se valida el porcentaje
            Return New ActionResult(Of Object) With {.StateResult = False, .Message = "El valor del porcentaje no puede ser cero"}
        End If

        'Se obtiene la cuenta contable para poder validar
        Dim MainAccount As MainAccounts = _mainAccountsRepository.GetAccountById(MainAccountId, False)

        'Valor por el cual se divide
        Dim ValueDivision As Decimal = 100

        'Se valida que la cuenta contable sea de tipo ReteICA
        If MainAccount.RetencionType = 3 Then

            'Se obtiene el tercero para poder sacar el porcentaje
            Dim ThirdParty As ThirdParty = _thirdPartyRepository.GetThirdPartyById(ThirdPartyId, False)

            'Se valida que si han escogido una cuenta contable de tipo ReteICA pero el tercero no maneja ICA devolvemos el error
            If ThirdParty.Ica = False Then
                Return New ActionResult(Of Object) With {.StateResult = False, .Message = "La cuenta contable " + MainAccount.Number + " - " + MainAccount.Name + " es de tipo ReteICA pero el tercero " + ThirdParty.Nit + " - " + ThirdParty.Name + " no maneja ICA"}
            End If

            'Se valida que si el tercero maneja ICA tenga valor en el porcentaje
            If ThirdParty.IcaPercentage = 0 Then
                Return New ActionResult(Of Object) With {.StateResult = False, .Message = "El tercero " + ThirdParty.Nit + " - " + ThirdParty.Name + " maneja ICA pero el porcentaje ICA es cero"}
            End If

            'Se asignan los valores correspondientes para realizar las operaciones
            Percentaje = ThirdParty.IcaPercentage
            ValueDivision = 1000
        End If

        'Variable que se retorna con el valor calculado
        Dim ValueReturn As Decimal = (BaseValue * Percentaje) / ValueDivision

        Select Case TypeRounding
            Case 1
                ValueReturn = Math.Round(ValueReturn * Math.Pow(10, 0)) / Math.Pow(10, 0)
            Case 2
                ValueReturn = Math.Round(ValueReturn * Math.Pow(10, -1)) / Math.Pow(10, -1)
            Case 3
                ValueReturn = Math.Round(ValueReturn * Math.Pow(10, -2)) / Math.Pow(10, -2)
            Case 4
                ValueReturn = Math.Round(ValueReturn * Math.Pow(10, -3)) / Math.Pow(10, -3)
            Case 5
                ValueReturn = Math.Round(ValueReturn, 1, MidpointRounding.AwayFromZero)
            Case 6
                ValueReturn = Math.Round(ValueReturn, 2, MidpointRounding.AwayFromZero)
        End Select

        Return New ActionResult(Of Object) With {.StateResult = True, .ObjectEmbbeded = ValueReturn}
    End Function

    ''' <summary>
    ''' sobrecarga para calcular el valor de la retencion cuando es de tipo fija [1]
    ''' </summary>
    ''' <param name="baseValue">The base value.</param>
    ''' <returns></returns>
    Public Shared Function CalculateRetention(ByVal baseValue As Decimal, ByVal retention As RetentionConcepts) As Decimal
        If retention Is Nothing Then
            Throw New ArgumentNullException("Seleccione la Retención")
        ElseIf baseValue = 0 Then
            Throw New ArgumentOutOfRangeException(ResourceManager.GetString("BaseInvalid", "Accounting"))
        End If
        Dim resultOperation As Decimal
        Dim rate As Decimal
        If baseValue < retention.MinBase Then
            Throw New ArgumentOutOfRangeException(ResourceManager.GetString("BaseInvalid", "Accounting"))
        End If
        rate = retention.Rate
        resultOperation = (baseValue * rate) / 100

        Select Case retention.TypeRounding
            Case 1
                resultOperation = Math.Round(resultOperation * Math.Pow(10, 0)) / Math.Pow(10, 0)
            Case 2
                resultOperation = Math.Round(resultOperation * Math.Pow(10, -1)) / Math.Pow(10, -1)
            Case 3
                resultOperation = Math.Round(resultOperation * Math.Pow(10, -2)) / Math.Pow(10, -2)
            Case 4
                resultOperation = Math.Round(resultOperation * Math.Pow(10, -3)) / Math.Pow(10, -3)
            Case 5
                resultOperation = Math.Round(resultOperation, 1, MidpointRounding.AwayFromZero)
            Case 6
                resultOperation = Math.Round(resultOperation, 2, MidpointRounding.AwayFromZero)
        End Select

        Return resultOperation
    End Function

    ''' <summary>
    ''' sobrecarga para calcular el valor de la retencion cuando es de tipo variable [3]
    ''' </summary>
    ''' <param name="baseValue">The base value.</param>
    ''' <param name="minBase">The retention.</param>
    ''' <param name="percentaje">The percentaje.</param>
    ''' <returns></returns>
    Public Shared Function CalculateRetention(ByVal baseValue As Decimal, ByVal minBase As Decimal, ByVal percentaje As Decimal, Optional TypeRounding As Byte = 0) As Decimal
        If baseValue = 0 Then
            Throw New ArgumentOutOfRangeException(ResourceManager.GetString("BaseInvalid", "Accounting"))
        End If
        If baseValue < minBase Then
            Throw New ArgumentNullException(ResourceManager.GetString("BaseInvalid", "Accounting"))
        End If
        If percentaje = 0 Then
            Throw New InvalidOperationException("El valor del porcentaje no puede ser cero.")
        End If

        Dim resultOperation As Decimal
        resultOperation = (baseValue * percentaje) / 100

        Select Case TypeRounding
            Case 1
                resultOperation = Math.Round(resultOperation * Math.Pow(10, 0)) / Math.Pow(10, 0)
            Case 2
                resultOperation = Math.Round(resultOperation * Math.Pow(10, -1)) / Math.Pow(10, -1)
            Case 3
                resultOperation = Math.Round(resultOperation * Math.Pow(10, -2)) / Math.Pow(10, -2)
            Case 4
                resultOperation = Math.Round(resultOperation * Math.Pow(10, -3)) / Math.Pow(10, -3)
            Case 5
                resultOperation = Math.Round(resultOperation, 1, MidpointRounding.AwayFromZero)
            Case 6
                resultOperation = Math.Round(resultOperation, 2, MidpointRounding.AwayFromZero)
        End Select

        Return resultOperation
    End Function


    ''' <summary>
    ''' sobrecarga para calcular el valor de la retencion cuando es de tipo rangos [2]
    ''' </summary>
    ''' <param name="baseValue">The base value.</param>
    ''' <param name="conceptRetention">The retention.</param>
    ''' <param name="UVT">The uvt.</param>
    ''' <returns></returns>
    Public Shared Function CalculateRetention(ByVal baseValue As Decimal, ByVal conceptRetention As RetentionConcepts, ByVal UVT As Decimal) As Decimal
        If conceptRetention Is Nothing Then
            Throw New ArgumentNullException("El concepto de retencion no puede ser nulo, por favor seleccione uno")
        ElseIf baseValue = 0 Then
            Throw New ArgumentOutOfRangeException(ResourceManager.GetString("BaseInvalid", "Accounting"))
        ElseIf UVT = 0 Then
            Throw New ArgumentNullException("El valor de los UVT no puede ser 0, por favor cambie el valor de los UVT en los parametros de empresa (Contabilidad)")
        End If
        Dim BaseUVT As Decimal
        BaseUVT = baseValue / UVT
        BaseUVT = Decimal.Round(BaseUVT, 2)
        Dim BaseWithDeduced As Decimal
        Dim ResultPercentaje As Decimal
        Dim ResultIncrement As Decimal
        Dim ValRetentionFinish As Decimal
        For Each item As RetentionConceptRanges In conceptRetention.RetentionConceptRanges
            If BaseUVT >= item.ValueInitial And BaseUVT <= item.ValueFinish Then
                BaseWithDeduced = BaseUVT - item.ValueDeducted
                ResultPercentaje = (BaseWithDeduced * item.Percentage) / 100
                ResultPercentaje = Decimal.Round(ResultPercentaje, 2)
                ResultIncrement = ResultPercentaje + item.UVTIncrement
                ResultIncrement = Decimal.Round(ResultIncrement, 2)
                ValRetentionFinish = ResultIncrement * UVT
                ValRetentionFinish = Decimal.Round(ValRetentionFinish, 0)
                Exit For
            End If
        Next
        If BaseWithDeduced = 0 Then
            Throw New IndexOutOfRangeException(ResourceManager.GetString("NoIndexRetention", "Accounting"))
        End If

        Select Case conceptRetention.TypeRounding
            Case 1
                ValRetentionFinish = Math.Round(ValRetentionFinish * Math.Pow(10, 0)) / Math.Pow(10, 0)
            Case 2
                ValRetentionFinish = Math.Round(ValRetentionFinish * Math.Pow(10, -1)) / Math.Pow(10, -1)
            Case 3
                ValRetentionFinish = Math.Round(ValRetentionFinish * Math.Pow(10, -2)) / Math.Pow(10, -2)
            Case 4
                ValRetentionFinish = Math.Round(ValRetentionFinish * Math.Pow(10, -3)) / Math.Pow(10, -3)
            Case 5
                ValRetentionFinish = Math.Round(ValRetentionFinish, 1, MidpointRounding.AwayFromZero)
            Case 6
                ValRetentionFinish = Math.Round(ValRetentionFinish, 2, MidpointRounding.AwayFromZero)
        End Select

        Return ValRetentionFinish
    End Function

    ''' <summary>
    ''' Validates the detail document accounting.
    ''' </summary>
    ''' <param name="ListDetail">The list detalle.</param>
    ''' <returns></returns>
    Public Shared Function ValidateDetailDocumentAccounting(ByVal ListDetail As List(Of JournalVoucherDetails)) As ActionResult(Of JournalVoucherDetails)
        If ListDetail Is Nothing Then
            Return New ActionResult(Of JournalVoucherDetails) With {.StateResult = False, .MessageResult = {"0001"}.ToList()}
        ElseIf ListDetail.Count = 1 Then
            Return New ActionResult(Of JournalVoucherDetails) With {.StateResult = False, .MessageResult = {"0002"}.ToList()}
        Else
            Dim debit As Decimal = 0
            Dim credit As Decimal = 0
            For Each item As JournalVoucherDetails In ListDetail
                If item.Nature = 2 Then
                    credit = credit + item.CreditValue
                Else
                    debit = debit + item.DebitValue
                End If
            Next
            If credit <> debit Then
                Return New ActionResult(Of JournalVoucherDetails) With {.StateResult = False, .MessageResult = {"0003"}.ToList()}
            Else
                Return New ActionResult(Of JournalVoucherDetails) With {.StateResult = True, .MessageResult = {"00000"}.ToList()}
            End If
        End If
    End Function

    ''' <summary>
    ''' funcion para calcular la diferencia entre los débitos y los créditos
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function CalculateDifferenceDebitCreditValue(ByVal debit As Decimal, ByVal credit As Decimal) As Decimal
        Dim resultOperation As Decimal
        resultOperation = (debit - credit)
        Return Math.Abs(resultOperation)
    End Function

    ''' <summary>
    ''' Genera un string con la lista de periodos
    ''' </summary>
    ''' <param name="periods">The periods.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">No se encontraron periodos</exception>
    Public Shared Function GetListOpenPeriods(ByVal periods As List(Of ClosedMonth)) As String
        Dim DateFormat As DateTimeFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat
        Dim ListOpenPeriodDate As String = String.Empty
        If periods.Count > 0 Then
            Dim initialPeriod As ClosedMonth = periods.ElementAt(0)
            Dim finishPeriod As ClosedMonth = periods.ElementAt(periods.Count - 1)
            Dim MonthName As String = DateFormat.GetMonthName(initialPeriod.Month)
            Dim DateOpen As String = String.Format(ResourceManager.GetString("PeriodFormat", "Accounting"), MonthName, initialPeriod.Year)
            If initialPeriod.Equals(finishPeriod) Then
                ListOpenPeriodDate = String.Format(ResourceManager.GetString("MonthSelectClose", "Accounting"), DateOpen)
            Else
                Dim MonthfinishName As String = DateFormat.GetMonthName(finishPeriod.Month)
                Dim DateFinishOpen As String = String.Format(ResourceManager.GetString("PeriodFormat", "Accounting"), MonthfinishName, finishPeriod.Year)
                ListOpenPeriodDate = String.Concat(DateOpen, " a ", DateFinishOpen)
                ListOpenPeriodDate = String.Format(ResourceManager.GetString("MonthsSelectClosed", "Accounting"), ListOpenPeriodDate)
            End If
        Else
            Throw New ArgumentNullException("No se encontraron periodos")
        End If
        Return ListOpenPeriodDate
    End Function


    ''' <summary>
    ''' Validates the creation journal vouchers.
    ''' </summary>
    ''' <param name="journalVouchers">The journal vouchers.</param>
    ''' <returns></returns>
    Public Function ValidateCreationJournalVouchers(ByVal journalVouchers As JournalVouchers) As ActionMessageResult(Of JournalVouchers)
        Dim messageResult As New ActionMessageResult(Of JournalVouchers)
        Dim message As New StringBuilder()
        Dim listDetail As New List(Of JournalVoucherDetails)
        For Each itemDetail In journalVouchers.JournalVoucherDetails
            If itemDetail.ChangeTracker.State <> ObjectState.Deleted Then
                listDetail.Add(itemDetail)
            End If
        Next
        If ValidatePeriod(journalVouchers.VoucherDate) = False Then
            'retornamos el mensaje JV01 que corresponde a que el mes no se encuentra abierto
            messageResult.MessageResult.Add(New MessageResult("JV01", journalVouchers.VoucherDate.ToString()))
            message.AppendLine("El mes no se encuentra abierto") 'New MessageResult("JV01", journalVouchers.VoucherDate.ToString()))
            messageResult.StateResult = False
        End If
        If ValidateDebitCreditDetailJournalVouchers(listDetail) = False Then
            'retornamos el mensaje JV02 que corresponde a que el comprobante esta desbalanceado
            messageResult.MessageResult.Add(New MessageResult("JV02", journalVouchers.Consecutive.ToString()))
            message.AppendLine("El comprobante esta desbalanceado")
            messageResult.StateResult = False
        End If
        For Each itemDetail In journalVouchers.JournalVoucherDetails
            If ValidateThirdParty(itemDetail) = False Then
                'retornamos el mensaje JV03 que corresponde a que las cuentas contables del detalle del comprobante manejan tercero pero el  idthidparty viene nulo   
                messageResult.MessageResult.Add(New MessageResult("JV03", itemDetail.IdMainAccount.ToString()))
                message.AppendLine("Las cuentas contables del detalle del comprobante manejan tercero pero el  idthidparty viene nulo")
                messageResult.StateResult = False
            End If
            If ValidateCosteCenter(itemDetail) = False Then
                'retornamos el mensaje JV04 que corresponde a que las cuentas contables del detalle del comprobante manejan centro de costo pero el  idCosCenter viene nulo
                messageResult.MessageResult.Add(New MessageResult("JV04", itemDetail.IdMainAccount.ToString()))
                message.AppendLine("las cuentas contables del detalle del comprobante manejan centro de costo pero el  idCosCenter viene nulo")
                messageResult.StateResult = False
            End If
        Next
        If messageResult.MessageResult.Count() = 0 Then
            'si todo se valido correctamente retornamos un true con un codigo de mensaje JV05
            messageResult.StateResult = True
            messageResult.MessageResult.Add(New MessageResult("JV05", journalVouchers.Consecutive.ToString()))
        Else
            messageResult.Message = message.ToString()
        End If
        Return messageResult
    End Function



    ''' <summary>
    ''' funcion para validar los creditos y los debitos del detalle
    ''' </summary>
    ''' <param name="Detail">The detail.</param>
    ''' <returns></returns>
    Private Function ValidateDebitCreditDetailJournalVouchers(ByVal Detail As List(Of JournalVoucherDetails)) As Boolean
        Dim TemDebit As Decimal
        Dim TemCredit As Decimal
        For Each item As JournalVoucherDetails In Detail
            If item.CreditValue = 0 Then
                TemDebit = TemDebit + item.DebitValue
            Else
                TemCredit = TemCredit + item.CreditValue
            End If
        Next
        If TemCredit <> TemDebit Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Funcion para validar si la cuenta contable exige tercero
    ''' </summary>
    Private Function ValidateThirdParty(ByVal item As JournalVoucherDetails) As Boolean

        Dim mainaccount As MainAccounts
        mainaccount = _mainAccountsRepository.GetAccountById(item.IdMainAccount, False)
        If mainaccount.HandlesThirdParty = True Then
            If item.IdThirdParty Is Nothing Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' funcion para validar el centro de costo
    ''' </summary>    
    Private Function ValidateCosteCenter(ByVal item As JournalVoucherDetails) As Boolean
        Dim mainaccount As MainAccounts
        mainaccount = _mainAccountsRepository.GetAccountById(item.IdMainAccount, False)
        If mainaccount.HandlesCostCenter = True Then
            If item.IdCostCenter Is Nothing Then
                Return False
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' funcion para validar si el periodo esta abierto
    ''' </summary>
    Private Function ValidatePeriod(ByVal dateJournalVouchers As DateTime) As Boolean
        If _closeMonthRepository.ValidatePeriodOpen(dateJournalVouchers.Month, dateJournalVouchers.Year) = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function ValidatePeriodAndReturnOpendMonths(ByVal dateJournalVouchers As DateTime) As ActionResult(Of String)
        If _closeMonthRepository.ValidatePeriodOpen(dateJournalVouchers.Month, dateJournalVouchers.Year) = False Then
            Dim periods = _closeMonthRepository.GetOpenPeriod()
            Dim opendPeriods As New StringBuilder
            If periods IsNot Nothing AndAlso periods.Count > 0 Then
                opendPeriods.AppendLine(GetListOpenPeriods(periods))
            Else
                opendPeriods.AppendLine(ResourceManager.GetString("NoOpenPeriods", "Treasury"))
            End If
            Return New ActionResult(Of String) With {.StateResult = False, .Message = opendPeriods.ToString()}
        Else
            Return New ActionResult(Of String) With {.StateResult = True}
        End If
    End Function

    Public Function SetCopyPasteOrImportFilePortfolioNote(LegalBookId As Integer, dataImportFile As List(Of ImportFileRow), dataCopyPaste As List(Of List(Of String))) As ActionResult(Of List(Of JournalVoucherDetails))
        If Not (dataImportFile IsNot Nothing AndAlso dataImportFile.Count > 0) AndAlso Not (dataCopyPaste IsNot Nothing AndAlso dataCopyPaste.Count > 0) Then
            Throw New ArgumentNullException("data")
        End If

        Try
            'Objeto xml
            Dim xmlObject As String = String.Empty
            'Listado de datos copiados o importados que se devuelven a la rejilla del form
            Dim ListJournalVoucherDetails As New List(Of JournalVoucherDetails)
            'Listado de registros con errores
            Dim listRecordsErrors As New List(Of String())
            'Listado de errores
            Dim listErrors As New List(Of String)

            'Conversión a string del Objeto
            If dataImportFile IsNot Nothing Then
                xmlObject = ConvertToXmlImportFileJournalVoucherDetails(dataImportFile)
            Else
                xmlObject = ConvertToXmlCopyPasteJournalVoucherDetails(dataCopyPaste)
            End If

            'Se consume el procedimiento almacenado
            Dim resultStore = Me._journalVoucherRepository.SP_CopyAndPasteJournalVoucherDetails(LegalBookId, xmlObject)

            'Se crean los objetos para devolver y pegar en la rejilla
            If resultStore IsNot Nothing AndAlso resultStore.Count > 0 Then
                For Each itemXml In resultStore
                    If itemXml.StatusField = 0 Then
                        'Se crea el nuevo objeto para agregarlo al listado
                        ListJournalVoucherDetails.Add(New JournalVoucherDetails With
                        {
                            .CodeNameMainAccount = itemXml.MainAccount,
                            .IdMainAccount = itemXml.MainAccountId,
                            .CodeNameThirdParty = itemXml.ThirdParty,
                            .IdThirdParty = itemXml.ThirdPartyId,
                            .CodeNameCostCenter = itemXml.CostCenter,
                            .IdCostCenter = itemXml.CostCenterId,
                            .DebitValue = itemXml.DebitValue,
                            .CreditValue = itemXml.CreditValue,
                            .Detail = itemXml.Detail,
                            .CodeNameRetention = itemXml.RetentionConcept,
                            .IdRetention = itemXml.RetentionConceptId,
                            .RetentionRate = itemXml.RetentionRate,
                            .BaseValue = itemXml.BaseValue,
                            .BillingValue = itemXml.BillingValue,
                            .Nature = If(itemXml.DebitValue > 0, 1, 2)
                        })
                    Else 'Si el estado del item es False y no pasó alguna validación
                        If dataImportFile IsNot Nothing Then
                            If itemXml.MainAccount?.Contains("-") Then
                                Dim _MainAccount = itemXml.MainAccount.Split("-")
                                itemXml.MainAccount = _MainAccount(0)
                            End If

                            If itemXml.ThirdParty?.Contains("-") Then
                                Dim _ThirdParty = itemXml.ThirdParty.Split("-")
                                itemXml.ThirdParty = _ThirdParty(0)
                            End If

                            If itemXml.CostCenter?.Contains("-") Then
                                Dim _Costcenter = itemXml.CostCenter.Split("-")
                                itemXml.CostCenter = _Costcenter(0)
                            End If

                            If itemXml.RetentionConcept?.Contains("-") Then
                                Dim _RetentionConcept = itemXml.RetentionConcept.Split("-")
                                itemXml.RetentionConcept = _RetentionConcept(0)
                            End If

                            Dim Nature As String = IIf(itemXml.DebitValue > 0, "1", "2")

                            Dim datos As String() = {itemXml.MainAccount, itemXml.ThirdParty, itemXml.CostCenter, Nature, itemXml.Detail,
                                                    IIf(Nature = "1", itemXml.DebitValue.AsString, itemXml.CreditValue.AsString),
                                                    itemXml.RetentionConcept, itemXml.BillingValue.AsString, itemXml.BaseValue.AsString, itemXml.MessageField}

                            listRecordsErrors.Add(datos)
                        Else
                            listErrors.Add(itemXml.MessageField)
                        End If
                    End If
                Next
            End If
            Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = True, .ObjectEmbbeded = ListJournalVoucherDetails, .MessageResult = listErrors, .ListMessageResult = listRecordsErrors}

        Catch ex As Data.SqlClient.SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            If ex.InnerException.HResult = -2146232060 Then
                Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .ListMessageResult = {{"El archivo contiene caracteres especiales los cuales no son permitidos"}.ToArray}.ToList()}
            Else
                Return New ActionResult(Of List(Of JournalVoucherDetails)) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
            End If
        End Try
    End Function

#Region "Private Functions"

    Private Function ConvertToXmlImportFileJournalVoucherDetails(dataImportFile As List(Of ImportFileRow))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        For Each importFile In dataImportFile
            If importFile?.Row?.Item(0) IsNot Nothing Then
                Dim indexRow = importFile.IndexRow
                Dim columns = importFile.Row.Count

                builder.Append("<Row>")
                builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
                builder.Append("<RowColumns>" & columns & "</RowColumns>")
                builder.Append("<MainAccount>" & If(columns > 0, importFile.Row.Item(0), String.Empty) & "</MainAccount>")
                builder.Append("<ThirdParty>" & If(columns > 1, importFile.Row.Item(1), String.Empty) & "</ThirdParty>")
                builder.Append("<CostCenter>" & If(columns > 2, importFile.Row.Item(2), String.Empty) & "</CostCenter>")
                builder.Append("<StringNature>" & If(columns > 3, importFile.Row.Item(3), String.Empty) & "</StringNature>")
                builder.Append("<Detail>" & If(columns > 4, importFile.Row.Item(4)?.ToString()?.CleanSpecialChars, String.Empty) & "</Detail>")
                builder.Append("<StringValue>" & If(columns > 5 AndAlso importFile.Row.Item(5) IsNot Nothing, importFile.Row.Item(5).ToString.Replace(",", "."), String.Empty) & "</StringValue>")
                builder.Append("<RetentionConcept>" & If(columns > 6, importFile.Row.Item(6), String.Empty) & "</RetentionConcept>")
                builder.Append("<StringBaseValue>" & If(columns > 7 AndAlso importFile.Row.Item(7) IsNot Nothing, importFile.Row.Item(7).ToString.Replace(",", "."), String.Empty) & "</StringBaseValue>")
                builder.Append("<StringBillingValue>" & If(columns > 8 AndAlso importFile.Row.Item(8) IsNot Nothing, importFile.Row.Item(8).ToString.Replace(",", "."), String.Empty) & "</StringBillingValue>")
                builder.Append("</Row>")
            End If
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    Private Function ConvertToXmlCopyPasteJournalVoucherDetails(data As List(Of List(Of String)))
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<Data>")

        Dim indexRow As Integer = 0
        For Each item In data
            indexRow = indexRow + 1
            Dim columns = item.Count

            builder.Append("<Row>")
            builder.Append("<RowIndex>" & indexRow & "</RowIndex>")
            builder.Append("<RowColumns>" & columns & "</RowColumns>")
            builder.Append("<MainAccount>" & If(columns > 0, item(0), String.Empty) & "</MainAccount>")
            builder.Append("<ThirdParty>" & If(columns > 1, item(1), String.Empty) & "</ThirdParty>")
            builder.Append("<CostCenter>" & If(columns > 2, item(2), String.Empty) & "</CostCenter>")
            builder.Append("<StringNature>" & If(columns > 3, item(3), String.Empty) & "</StringNature>")
            builder.Append("<Detail>" & If(columns > 4, item(4)?.ToString()?.CleanSpecialChars(), String.Empty) & "</Detail>")
            builder.Append("<StringValue>" & If(columns > 5, item(5).ToString.Replace(",", "."), String.Empty) & "</StringValue>")
            builder.Append("<RetentionConcept>" & If(columns > 6, item(6), String.Empty) & "</RetentionConcept>")
            builder.Append("<StringBaseValue>" & If(columns > 7, item(7).ToString.Replace(",", "."), String.Empty) & "</StringBaseValue>")
            builder.Append("<StringBillingValue>" & If(columns > 8, item(8).ToString.Replace(",", "."), String.Empty) & "</StringBillingValue>")
            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

End Class