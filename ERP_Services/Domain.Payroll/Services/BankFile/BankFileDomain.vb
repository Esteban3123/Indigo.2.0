'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-03-2014
'
' Copyright        : (c) . All rights reserved.

'***********************************************************************
#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports System.Globalization

#End Region

Public Class BankFileDomain
    Implements IBankFileDomain

    Private _thirdPartyRepository As IThirdPartyRepository
    Private _OfficialCurrency As ICompanySettingsRepository
    Private _IncentivePaymentDomain As IIncentivePaymentDomain
    Private _IncentivePaymentRepository As IIncentivePaymentRepository
    Private _EntityBankAccountRepository As IEntityBankAccountRepository
    Private _PayrollBankRepository As IBankRepository
    Private _CompanyPayrollRepository As ICompanyRepository

#Region "Fields"



#End Region

#Region "Builder"

    Public Sub New(bankRepository As IBankRepository, personRepository As IPersonRepository, thirdPartyRepository As IThirdPartyRepository, companySettingsRepository As ICompanySettingsRepository, incentivePaymentDomain As IIncentivePaymentDomain, incetivePaymentRepository As IIncentivePaymentRepository, entityBankAccount As IEntityBankAccountRepository, payrollBank As IBankRepository, companyRespoitory As ICompanyRepository)
        Me._thirdPartyRepository = thirdPartyRepository
        Me._OfficialCurrency = companySettingsRepository
        Me._IncentivePaymentDomain = incentivePaymentDomain
        Me._IncentivePaymentRepository = incetivePaymentRepository
        Me._EntityBankAccountRepository = entityBankAccount
        Me._PayrollBankRepository = payrollBank
        Me._CompanyPayrollRepository = companyRespoitory
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Funcion para seleccionar banco y generar la cadena de texto 
    ''' </summary>
    ''' <param name="bankFile"></param>
    ''' <returns></returns>
    Public Function SelectBank(bankFile As BankFile) As ActionMessageResult(Of StringBuilder) Implements IBankFileDomain.SelectBank
        Try
            Dim resultActionMessage As New ActionMessageResult(Of StringBuilder)()
            Dim result As New StringBuilder()
            If bankFile.Process = 1 Then 'Primas

                Dim incentivePaymentList As New List(Of IncentivePayment)
                Dim entityBanAccount = _EntityBankAccountRepository.GetEntityBankAccountById(bankFile.EntityBankAccountId)
                Dim entityBanAccountIdBank = entityBanAccount.IdBank
                Dim accountNumber = entityBanAccount.Number
                Dim accountType = entityBanAccount.Type
                Dim bank As Domain.Payroll.Entities.Bank
                Dim company = _CompanyPayrollRepository.GetCompanyById(bankFile.CompanyId)
                bank = _PayrollBankRepository.GetBankById(entityBanAccountIdBank)

                incentivePaymentList = _IncentivePaymentRepository.GetIncentivePaymentBankFileProcess(bankFile)
                'Generamos el txt del archivo para primas
                resultActionMessage = _IncentivePaymentDomain.SelectBankEmployee(incentivePaymentList, bank, accountNumber, accountType, company)


                Return resultActionMessage
            Else

                Select Case bankFile.BankFileCode
                    Case "001"
                        result = GenerateArchiveBancolombia(bankFile)
                    Case "002"
                        result = GenerateArchiveAvVillas(bankFile)
                    Case "003"
                        result = GenerateArchiveBancoPopular(bankFile)
                    Case "004"
                        result = GenerateArchiveBancoOccidente(bankFile)
                    Case "005"
                        result = GenerateArchiveBancoBBVA(bankFile)
                    Case "006"
                        result = GenerateArchiveDavivienda(bankFile)
                    Case "008"
                        result = GenerateArchiveBancoBogota(bankFile)
                    Case "009"
                        result = GenerateArchiveBancolombiaSAP(bankFile)
                    Case "014" 'Estructura CR
                        result = GenerateArchiveDaviviendaCR(bankFile)
                    Case Else
                        result = Nothing
                End Select
            End If

            If result Is Nothing Then
                resultActionMessage.StateResult = False
                Dim idBank = _EntityBankAccountRepository.GetEntityBankAccountById(bankFile.EntityBankAccountId)?.IdBank
                Dim bankName As String = If(_PayrollBankRepository.GetBankById(idBank)?.Name, "Desconocido")
                resultActionMessage.Message = $"No existe una estructura definida para el banco: {bankName}."

            Else

                resultActionMessage.StateResult = True
                resultActionMessage.ObjectEmbbeded = result
            End If

            Return resultActionMessage
        Catch ex As Exception
            Return New ActionMessageResult(Of StringBuilder)() With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    Private Function GenerateArchiveBancolombia(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveBancolombia
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim bankTypeAccount As String = IIf(bankFile.EntityBankAccountType = 1, "S", "D")
            Dim totalRegister As Integer = bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Count
            bankFile.Value = bankFile.Value * 100

            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.CompanyNit, 15, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("I", 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 15, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(225, 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("NOM" & MonthName(Month(bankFile.LiquidationDate)).ToUpper(), 10, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("A", 2, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 17, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.Value, 17, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.EntityBankAccountNumber, 11, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankTypeAccount, 1, "", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each thirdPartyNit In bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Select(Function(d) d.Key)
                Dim details = bankFile.BankFileDetail.Where(Function(d) d.ThirdPartyNit = thirdPartyNit).ToList()
                Dim detail = details.First()
                Dim totalPaid As Double = details.Sum(Function(d) d.TotalPaid) * 100
                Dim BankAccountType As String
                Select Case detail.EmployeeBankTypeAccount
                    Case 1 ' Ahorros
                        BankAccountType = "37"
                    Case 2 ' Corriente
                        BankAccountType = "27"
                    Case Else 'Ahorros
                        BankAccountType = "37"
                End Select

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad(6, 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyNit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.ThirdPartyName, 30, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.EmployeeBankAchCode, 9, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(BankAccountType, 2, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(totalPaid, 17, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(0, 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("NOM" & MonthName(Month(bankFile.LiquidationDate)).ToUpper() & " " & dateNow, 21, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(" ", 6, " ", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    Private Function GenerateArchiveAvVillas(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveAvVillas
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim dateNow As String = Date.Now
            Dim bankTypeAccount As String '= IIf(bankFile.EntityBankAccountType = 1, "01", "06")
            Dim totalRegister As Integer = bankFile.BankFileDetail.Count

            Dim lineHead As String = Utils.StringPad(1, 2, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(Format(dateNow, "d"), 8, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(Format(dateNow, "h:mm:ss"), 6, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("088", 3, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad("02", 2, 0, Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(" ", 50, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(" ", 120, " ", Utils.PadType.STR_PAD_RIGHT)
            result.Append(lineHead)

            Dim secuence As Integer
            For Each detail As BankFileDetail In bankFile.BankFileDetail
                secuence = secuence + 1
                Dim EmployeeBankTypeAccount As String = IIf(detail.EmployeeBankTypeAccount = 1, "01", "06")

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("02", 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("000023", 6, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(bankTypeAccount, 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("052", 3, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(secuence, 9, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.TotalPaid, 18, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 16, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.ThirdPartyName, 30, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyNit, 11, 0, Utils.PadType.STR_PAD_RIGHT)
                result.Append(lineDet)
            Next

            Dim lineFin As String = vbCrLf
            lineFin &= Utils.StringPad("03", 2, 0, Utils.PadType.STR_PAD_RIGHT)
            lineFin &= Utils.StringPad(totalRegister, 9, 0, Utils.PadType.STR_PAD_RIGHT)
            lineFin &= Utils.StringPad(bankFile.Value, 20, 0, Utils.PadType.STR_PAD_RIGHT)
            lineFin &= Utils.StringPad(1, 15, " ", Utils.PadType.STR_PAD_LEFT) ' Algoritmo CRC32
            result.Append(lineFin)
        End If

        Return result
    End Function

    Private Function GenerateArchiveBancoPopular(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveBancoPopular
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim bankTypeAccount As String = IIf(bankFile.EntityBankAccountType = 1, "000", "110")
            Dim totalRegister As Integer = bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Count
            bankFile.Value = bankFile.Value * 100

            Dim lineHead As String = Utils.StringPad("01", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.CompanyName, 16, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankTypeAccount, 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.EntityBankAccountNumber, 9, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.CompanyNit, 10, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.Value, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 12, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("NOMINA", 6, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 75, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("V", 1, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 41, " ", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each thirdPartyNit In bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Select(Function(d) d.Key)
                Dim details = bankFile.BankFileDetail.Where(Function(d) d.ThirdPartyNit = thirdPartyNit).ToList()
                Dim detail = details.First()
                Dim totalPaid As Double = details.Sum(Function(d) d.TotalPaid) * 100
                Dim EmployeeBankTypeAccount As String = IIf(detail.EmployeeBankTypeAccount = 1, "32", "22")

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("02", 2, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyNit, 15, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(totalPaid, 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyName, 22, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("000010029", 9, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 2, 0, Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(bankFile.CompanyNit, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("NOMINA", 10, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("0", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("V", 53, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 41, " ", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    Private Function GenerateArchiveBancoOccidente(bankFile As BankFile) As StringBuilder
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim bankTypeAccount As String = IIf(bankFile.EntityBankAccountType = 1, "A", "C")
            Dim totalRegister As Integer = bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Count
            bankFile.Value = bankFile.Value * 100

            Dim lineHead As String = Utils.StringPad("10000", 5, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.Value, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.EntityBankAccountNumber, 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 148, 0, Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            Dim secuence As Integer
            For Each thirdPartyNit In bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Select(Function(d) d.Key)
                secuence = secuence + 1
                Dim details = bankFile.BankFileDetail.Where(Function(d) d.ThirdPartyNit = thirdPartyNit).ToList()
                Dim detail = details.First()
                Dim totalPaid As Double = details.Sum(Function(d) d.TotalPaid) * 100
                Dim EmployeeBankTypeAccount As String = IIf(detail.EmployeeBankTypeAccount = 1, "A", "C")

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("2", 1, " ", Utils.PadType.STR_PAD_LEFT) 'Tipo de registro
                lineDet &= Utils.StringPad(secuence, 4, 0, Utils.PadType.STR_PAD_LEFT) 'Consecutivo
                lineDet &= Utils.StringPad(bankFile.EntityBankAccountNumber, 16, 0, Utils.PadType.STR_PAD_LEFT) 'Cuenta Origen
                lineDet &= Utils.StringPad(detail.ThirdPartyName, 30, " ", Utils.PadType.STR_PAD_RIGHT) 'Beneficiario
                lineDet &= Utils.StringPad(detail.ThirdPartyNit, 11, 0, Utils.PadType.STR_PAD_LEFT) ' NIT Beneficiario
                lineDet &= Utils.StringPad(detail.EmployeeBankCenitCode, 4, "0", Utils.PadType.STR_PAD_LEFT) 'Codigo del Banco del Beneficiario
                lineDet &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT) 'Fecha de Pago
                lineDet &= Utils.StringPad(If(bankFile.BankCenitCode = detail.EmployeeBankCenitCode, "2", "3"), 1, " ", Utils.PadType.STR_PAD_LEFT) 'Forma de pago: 1 - Cheque 2 - Pago cuenta Occidente - Abono otras entidades
                lineDet &= Utils.StringPad(totalPaid, 15, 0, Utils.PadType.STR_PAD_LEFT) 'Valor
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber, 16, " ", Utils.PadType.STR_PAD_RIGHT) 'Cuenta Destino
                lineDet &= Utils.StringPad(Utils.GetNumberFromString(bankFile.VoucherTransactionCode), 12, " ", Utils.PadType.STR_PAD_LEFT) ' Comprobante
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 1, 0, Utils.PadType.STR_PAD_RIGHT) 'Tipo de Cuenta Destino
                lineDet &= Utils.StringPad("NOMINA" & MonthName(Month(bankFile.LiquidationDate)).ToUpper() & Year(bankFile.LiquidationDate), 80, " ", Utils.PadType.STR_PAD_RIGHT) 'Concepto
                result.Append(lineDet)
            Next

            Dim lineEnd As String = vbCrLf
            lineEnd &= Utils.StringPad("39999", 5, " ", Utils.PadType.STR_PAD_LEFT)
            lineEnd &= Utils.StringPad(totalRegister, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineEnd &= Utils.StringPad(bankFile.Value, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineEnd &= Utils.StringPad(0, 172, 0, Utils.PadType.STR_PAD_LEFT)
            result.Append(lineEnd)
        End If

        Return result
    End Function

    Private Function GenerateArchiveBancoBBVA(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveBancoBBVA
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim bankTypeAccount As String = IIf(bankFile.EntityBankAccountType = 1, "S", "D")
            Dim totalRegister As Integer = bankFile.BankFileDetail.Count
            bankFile.Value = bankFile.Value * 100

            For Each detail In bankFile.BankFileDetail
                Dim EmployeeBankTypeAccount As String = IIf(detail.EmployeeBankTypeAccount = 1, "02", "01")
                Dim documentType As String
                Select Case detail.ThirdPartyIdentificationType
                    Case 1 ' cedula de extranjeria
                        documentType = "02"
                    Case 2 'tarjeta de identidad
                        documentType = "04"
                    Case 4 'pasaporte
                        documentType = "05"
                    Case 7 'nit
                        documentType = "03"
                    Case Else 'cedula 
                        documentType = "01"
                End Select

                Dim lineDet As String
                lineDet = Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                lineDet += Utils.StringPad(String.Concat(detail.ThirdPartyNit, 0), 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet += Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet += Utils.StringPad(bankFile.BankCenitCode, 4, 0, Utils.PadType.STR_PAD_LEFT)

                Dim accountNumber As String = String.Empty
                Dim accountType As String = String.Empty
                Dim supplierAccountNumber As String = String.Empty

                If bankFile.BankCenitCode = "0013" Then
                    accountType = Utils.StringPad(EmployeeBankTypeAccount, 4, 0, Utils.PadType.STR_PAD_RIGHT)
                    Dim office = "0" + Left(detail.EmployeeBankAccountNumber.Trim.Replace("-", ""), 3)
                    accountNumber = office + "00" + accountType + Right(detail.EmployeeBankAccountNumber.Trim.Replace("-", ""), 6)
                    accountType = "00"
                    supplierAccountNumber = "00000000000000000"
                Else
                    accountNumber = "0000000000000000"
                    accountType = Utils.StringPad(EmployeeBankTypeAccount, 2, 0, Utils.PadType.STR_PAD_RIGHT)
                    supplierAccountNumber = detail.EmployeeBankAccountNumber.Trim.Replace("-", "")
                End If

                'numero cuenta BBVA ==============================================================porque va en 0
                lineDet += Utils.StringPad(accountNumber, 16, 0, Utils.PadType.STR_PAD_LEFT)
                'tipo de cuenta 02 = ahorros , 01 = corriente
                lineDet += Utils.StringPad(accountType, 2, "", Utils.PadType.STR_PAD_LEFT)
                'numero cuenta beneficiario
                lineDet += Utils.StringPad(supplierAccountNumber.Trim.Replace("-", ""), 17, " ", Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion parte entera
                lineDet += Utils.StringPad(detail.TotalPaid, 13, 0, Utils.PadType.STR_PAD_LEFT)
                'valor de la transaccion parte decimal
                lineDet += Utils.StringPad("", 2, 0, Utils.PadType.STR_PAD_RIGHT)
                'fecha - 00000000 se hizo este cambio porque la documentacion enviada lo decia asi
                lineDet += Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
                'codigo oficna pagadora
                lineDet += Utils.StringPad("0000", 4, 0, Utils.PadType.STR_PAD_RIGHT)
                'nombre del beneficiario
                lineDet += Utils.StringPad(detail.ThirdPartyName, 36, " ", Utils.PadType.STR_PAD_RIGHT)
                'direccion
                lineDet += Utils.StringPad("BOGOTA", 36, " ", Utils.PadType.STR_PAD_RIGHT)
                'direccion 2
                lineDet += Utils.StringPad("", 36, " ", Utils.PadType.STR_PAD_LEFT)
                'email
                lineDet += Utils.StringPad("", 48, " ", Utils.PadType.STR_PAD_LEFT)
                'concepto
                lineDet += Utils.StringPad("NOMINA" & MonthName(Month(bankFile.LiquidationDate)).ToUpper() & Year(bankFile.LiquidationDate), 40, " ", Utils.PadType.STR_PAD_RIGHT)
                result.AppendLine(lineDet)
            Next
        End If

        Return result
    End Function

    Private Function GenerateArchiveDavivienda(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveDavivienda
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim totalRegister As Integer = bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Count
            bankFile.Value = bankFile.Value * 100

            Dim CompanyThirdParty = _thirdPartyRepository.GetThirdPartyByNit(bankFile.CompanyNit)
            Dim VerificationDigit As String = "0"

            If CompanyThirdParty IsNot Nothing Then
                VerificationDigit = CompanyThirdParty.DigitVerification
            End If

            Dim lineHead As String = Utils.StringPad("RC", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.CompanyNit + VerificationDigit, 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("NOMI", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("NOMI", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.EntityBankAccountNumber.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(IIf(bankFile.EntityBankAccountType = 1, "CA", "CC"), 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.BankCenitCode, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.Value, 18, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(Date.Now.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(DateTime.Now.ToString("hhmmss"), 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(9999, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("03", 2, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 12, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("", 40, 0, Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each thirdPartyNit In bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Select(Function(d) d.Key)
                Dim details = bankFile.BankFileDetail.Where(Function(d) d.ThirdPartyNit = thirdPartyNit).ToList()
                Dim detail = details.First()
                Dim totalPaid As Double = details.Sum(Function(d) d.TotalPaid) * 100
                Dim documentType As String = "01"
                Select Case detail.ThirdPartyIdentificationType
                    Case 0 'cedula 
                        documentType = "01"
                    Case 1 ' cedula de extranjeria
                        documentType = "02"
                    Case 2 'tarjeta de identidad
                        documentType = "04"
                    Case 4 'pasaporte
                        documentType = "05"
                    Case 7 'nit
                        documentType = "03"
                End Select

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("TR", 2, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(LTrim(RTrim(detail.ThirdPartyNit)), 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber.Trim.Replace("-", ""), 16, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(If(detail.EmployeeBankTypeAccount = 1, "CA", "CC"), 2, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.EmployeeBankCenitCode, 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(totalPaid, 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 6, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(documentType, 2, "", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(9999, 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 40, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 4, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("", 7, 0, Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function
    ''' <summary>
    ''' Genera el Txt para El banco Davivienda CR 
    ''' </summary>
    ''' <param name="bankFile"></param>
    ''' <returns></returns>
    Private Function GenerateArchiveDaviviendaCR(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveDaviviendaCR
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim totalRegister As Integer = bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Count
            bankFile.Value = bankFile.Value * 100

            Dim CompanyThirdParty = _thirdPartyRepository.GetThirdPartyByNit(bankFile.CompanyNit)
            Dim VerificationDigit As String = "0"

            If CompanyThirdParty IsNot Nothing Then
                VerificationDigit = CompanyThirdParty.DigitVerification
            End If
            Dim nominaDateText As String = "Pago Nomina " & bankFile.LiquidationDate.ToString("MMMM dd 'de' yyyy", New CultureInfo("es-CR"))

            For Each thirdPartyNit In bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Select(Function(d) d.Key)
                Dim details = bankFile.BankFileDetail.Where(Function(d) d.ThirdPartyNit = thirdPartyNit).ToList()
                Dim detail = details.First()
                Dim totalPaid As Double = details.Sum(Function(d) d.TotalPaid)
                Dim documentType As String = "01"


                Dim lineDet As String = ""
                lineDet &= Utils.StringPad("CMB", 3, 0, Utils.PadType.STR_PAD_LEFT) & ","
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber.Trim().Replace("-", ""), 11, 0, Utils.PadType.STR_PAD_LEFT) & ","
                lineDet &= "0,"
                lineDet &= "0,"
                lineDet &= totalPaid.ToString("F2", CultureInfo.InvariantCulture) & ","
                lineDet &= nominaDateText & ","
                lineDet &= "0,"
                lineDet &= "0,"
                lineDet &= "0"


                result.Append(lineDet & vbCrLf)
            Next

        End If

        Return result
    End Function
    Private Function GenerateArchiveBancoBogota(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveBancoBogota
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim dateNow As String = Date.Now.ToString("yyyyMMdd")
            Dim bankTypeAccount As String = IIf(bankFile.EntityBankAccountType = 1, "2", "1")
            Dim totalRegister As Integer = bankFile.BankFileDetail.Count

            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow, 8, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 5, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.Value, 16, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankTypeAccount, 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.EntityBankAccountNumber, 11, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.CompanyName, 40, " ", Utils.PadType.STR_PAD_RIGHT)
            lineHead &= Utils.StringPad(bankFile.CompanyNit & GetVerificationCode(bankFile.CompanyNit), 11, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("002", 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("0032", 4, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("000055", 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(" ", 135, " ", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each detail In bankFile.BankFileDetail
                Dim EmployeeBankTypeAccount As String = IIf(detail.EmployeeBankTypeAccount = 1, "2", "1")
                Dim documentType As String
                Select Case detail.ThirdPartyIdentificationType
                    Case 1 ' cedula de extranjeria
                        documentType = "E"
                    Case 2 'tarjeta de identidad
                        documentType = "T"
                    Case 7 'nit
                        documentType = "N"
                    Case Else 'cedula 
                        documentType = "C"
                End Select

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad("2", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(documentType, 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyNit, 11, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyName, 40, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(0, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(EmployeeBankTypeAccount, 1, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber, 17, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad(detail.TotalPaid, 18, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("A", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("000", 3, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("001", 3, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("0001", 4, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(" ", 80, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("0", 11, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("N", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(" ", 48, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("N00000000", 9, " ", Utils.PadType.STR_PAD_LEFT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    Private Function GenerateArchiveBancolombiaSAP(bankFile As BankFile) As StringBuilder Implements IBankFileDomain.GenerateArchiveBancolombiaSAP
        Dim result As New StringBuilder()

        If bankFile.BankFileDetail IsNot Nothing AndAlso bankFile.BankFileDetail.Count > 0 Then
            Dim dateNow As Date = Date.Now
            Dim totalRegister As Integer = bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Count

            Dim lineHead As String = Utils.StringPad(1, 1, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.CompanyNit, 10, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.CompanyName, 16, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(225, 3, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("NOM" & MonthName(Month(bankFile.LiquidationDate)).ToUpper(), 10, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow.ToString("yyMMdd"), 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad("A", 1, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(dateNow.ToString("yyMMdd"), 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(totalRegister, 6, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.Value, 12, 0, Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(bankFile.EntityBankAccountNumber.Trim.Replace("-", ""), 11, " ", Utils.PadType.STR_PAD_LEFT)
            lineHead &= Utils.StringPad(If(bankFile.EntityBankAccountType = 1, "S", "D"), 1, "", Utils.PadType.STR_PAD_LEFT)
            result.Append(lineHead)

            For Each thirdPartyNit In bankFile.BankFileDetail.GroupBy(Function(d) d.ThirdPartyNit).Select(Function(d) d.Key)
                Dim details = bankFile.BankFileDetail.Where(Function(d) d.ThirdPartyNit = thirdPartyNit).ToList()
                Dim detail = details.First()
                Dim totalPaid As Double = details.Sum(Function(d) d.TotalPaid)

                Dim lineDet As String = vbCrLf
                lineDet &= Utils.StringPad(6, 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyNit, 15, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.ThirdPartyName, 18, " ", Utils.PadType.STR_PAD_RIGHT)
                lineDet &= Utils.StringPad("005600078", 9, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(detail.EmployeeBankAccountNumber.Trim().Replace("-", ""), 17, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad("S", 1, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(If(detail.EmployeeBankTypeAccount = 1, 37, 27), 2, " ", Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(totalPaid, 10, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(dateNow.ToString("yyyyMMdd"), 8, 0, Utils.PadType.STR_PAD_LEFT)
                lineDet &= Utils.StringPad(" NOMINA", 14, " ", Utils.PadType.STR_PAD_RIGHT)
                result.Append(lineDet)
            Next
        End If

        Return result
    End Function

    Private Function GetCRC32(ByVal sFileName As String) As String
        Try
            'Dim FS As FileStream = New FileStream(sFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 8192)
            Dim CRC32Result As Integer = &HFFFFFFFF
            Dim Buffer(4096) As Byte
            Dim ReadSize As Integer = 4096
            'Dim Count As Integer = FS.Read(Buffer, 0, ReadSize)
            Dim CRC32Table(256) As Integer
            Dim DWPolynomial As Integer = &HEDB88320
            Dim DWCRC As Integer
            Dim i As Integer, j As Integer, n As Integer

            'Create CRC32 Table
            For i = 0 To 255
                DWCRC = i
                For j = 8 To 1 Step -1
                    If (DWCRC And 1) Then
                        DWCRC = ((DWCRC And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                        DWCRC = DWCRC Xor DWPolynomial
                    Else
                        DWCRC = ((DWCRC And &HFFFFFFFE) \ 2&) And &H7FFFFFFF
                    End If
                Next j
                CRC32Table(i) = DWCRC
            Next i

            'Calcualting CRC32 Hash
            'Do While (Count > 0)
            'For i = 0 To Count - 1
            '    n = (CRC32Result And &HFF) Xor Buffer(i)
            '    CRC32Result = ((CRC32Result And &HFFFFFF00) \ &H100) And &HFFFFFF
            '    CRC32Result = CRC32Result Xor CRC32Table(n)
            'Next i
            'Count = FS.Read(Buffer, 0, ReadSize)
            'Loop
            Return Hex(Not (CRC32Result))
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function GetVerificationCode(ByVal nit As String) As Integer
        nit = Format(Val("" & nit), "000000000000000")
        Dim residue As Integer = 0
        Dim mul As Integer = 0

        For i As Integer = 15 To 1 Step -1
            If i = 15 Then
                mul = 3
            ElseIf i = 14 Then
                mul = 7
            ElseIf i = 13 Then
                mul = 13
            ElseIf i = 12 Then
                mul = 17
            ElseIf i = 11 Then
                mul = 19
            ElseIf i = 10 Then
                mul = 23
            ElseIf i = 9 Then
                mul = 29
            ElseIf i = 8 Then
                mul = 37
            ElseIf i = 7 Then
                mul = 41
            ElseIf i = 6 Then
                mul = 43
            ElseIf i = 5 Then
                mul = 47
            ElseIf i = 4 Then
                mul = 53
            ElseIf i = 3 Then
                mul = 59
            ElseIf i = 2 Then
                mul = 67
            Else
                mul = 71
            End If
            residue = residue + (Val(GetChar(nit, i)) * mul)
        Next
        residue = residue Mod 11

        If residue = 0 Then
            residue = 0
        ElseIf residue = 1 Then
            residue = 1
        Else
            residue = 11 - residue
        End If

        Return residue
    End Function

    Public Function CreateVoucherTransaction(bankFile As BankFile) As ActionResult(Of Domain.Entities.VoucherTransaction) Implements IBankFileDomain.CreateVoucherTransaction
        Dim ResultObjVoucherTransaction As New ActionResult(Of Domain.Entities.VoucherTransaction)
        Dim Officialcurrency = _OfficialCurrency.GetCompanySettings()
        Dim currencyId = Officialcurrency.OfficialCurrencyId
        Try
            'Cabecera
            Dim VoucherTransaction As New Domain.Entities.VoucherTransaction With
            {
                .Code = "",
                .IdThirdParty = bankFile.ThirdPartyId,
                .IdMainAccount = bankFile.MainAccountId,
                .VoucherClass = 1, 'Pago
                .ExpenseType = 1, 'Afecta Banco
                .Detail = bankFile.ExpenseConceptName + " " + bankFile.LiquidationDate.ToShortDateString(),
                .DocumentDate = Date.Now(),
                .IdEntityBankAccount = bankFile.EntityBankAccountId,
                .Value = bankFile.Value,
                .PaymentMethod = 2, 'Nota Debito
                .NoteNumber = 1,
                .TaxByMil = 0,
                .TaxByMilValue = 0,
                .CashRegisterExpense = 0,
                .RefundCashRegisterExpense = 0,
                .BeneficiaryIdentification = bankFile.ThirdPartyNit,
                .Beneficiary = bankFile.ThirdPartyName,
                .TransactionRelationship = 0,
                .CheckReconciled = 0,
                .Printed = 0,
                .RTEValue = 0,
                .IVAValue = 0,
                .ICAValue = 0,
                .OtherValue = 0,
                .BankAccountNumber = bankFile.EntityBankAccountNumber,
                .BankName = bankFile.BankName,
                .IdUnitOperative = bankFile.OperatingUnitId,
                .Status = 2,
                .CreationUser = bankFile.CreationUser,
                .CreationDate = bankFile.CreationDate,
                .ConfirmationUser = bankFile.ConfirmationUser,
                .ConfirmationDate = bankFile.ConfirmationDate,
                .EmailSent = 0,
                .CurrencyId = If(Not bankFile.BankAccountCurrencyId.HasValue, currencyId, bankFile.BankAccountCurrencyId)
            }

            'Detalles
            For Each detail In bankFile.BankFileDetail
                VoucherTransaction.VoucherTransactionDetails.Add(New Domain.Entities.VoucherTransactionDetails With
                {
                    .IdThirdParty = detail.Employee.ThirdPartyId,
                    .IdExpenseConcept = bankFile.ExpenseConceptId,
                    .IdMainAccount = bankFile.ExpenseConceptMainAccountId,
                    .Nature = 1,
                    .Value = detail.TotalPaid,
                    .PercentRetention = 0,
                    .TotalConcept = detail.TotalPaid
                })
            Next

            ResultObjVoucherTransaction.ObjectEmbbeded = VoucherTransaction
            ResultObjVoucherTransaction.StateResult = True
        Catch ex As Exception
            ResultObjVoucherTransaction.ObjectEmbbeded = Nothing
            ResultObjVoucherTransaction.StateResult = False
            ResultObjVoucherTransaction.Message = ex.Message.ToString()
        End Try

        Return ResultObjVoucherTransaction
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

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
