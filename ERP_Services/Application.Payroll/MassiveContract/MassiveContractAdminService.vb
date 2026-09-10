Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports System.IO
Imports System.Xml
Imports System.Transactions
Imports System.Resources
Imports System.Data.Entity.Core

Public Class MassiveContractAdminService
    Implements IMassiveContractAdminService


    'Repositorio de contratos
    Private _ContractRepository As IContractRepository

    Private _employeeScheduleDetailRepository As IEmployeeScheduleDetailRepository

    ''' <summary>
    ''' inicia el repositorio de bancos
    ''' </summary>
    ''' <param name="contractRepository">Repositorio de bancos</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal contractRepository As IContractRepository, employeeScheduleDetailRepository As IEmployeeScheduleDetailRepository)
        If (contractRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de grupos de contratos vacio")
        End If
        If (employeeScheduleDetailRepository Is Nothing) Then
            Throw New ArgumentNullException("Repositorio de grupos de contratos vacio")
        End If
        _ContractRepository = contractRepository
        _employeeScheduleDetailRepository = employeeScheduleDetailRepository
    End Sub


    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _ContractRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Private Function ConvertImportFileRowToXML(pData As List(Of ImportFileRow)) As String
        Using sw As StringWriter = New StringWriter()
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                For Each ifr As ImportFileRow In pData
                    xw.WriteStartElement("Row")
                    xw.WriteElementString("CommonThirdPartyNit", ifr.Row(0))
                    xw.WriteElementString("CommonPersonIdentificationType", ifr.Row(1))
                    xw.WriteElementString("CommonIDCityName", ifr.Row(2))
                    xw.WriteElementString("CommonPersonIdentificationExpeditionDate", ifr.Row(3))
                    xw.WriteElementString("CommonPersonMilitaryCardId", ifr.Row(4))
                    xw.WriteElementString("CommonPersonMilitaryCardNumber", ifr.Row(5))
                    xw.WriteElementString("CommonPersonFirstName", ifr.Row(6))
                    xw.WriteElementString("CommonPersonSecondName", ifr.Row(7))
                    xw.WriteElementString("CommonPersonFirstLastName", ifr.Row(8))
                    xw.WriteElementString("CommonPersonSecondLastName", ifr.Row(9))
                    xw.WriteElementString("CommonPersonBirthDate", ifr.Row(10))
                    xw.WriteElementString("CommonBirthCityName", ifr.Row(11))
                    xw.WriteElementString("CommonPersonGender", ifr.Row(12))
                    xw.WriteElementString("CommonPersonBloodGroup", ifr.Row(13))
                    xw.WriteElementString("CommonPersonMaritalStatus", ifr.Row(14))
                    xw.WriteElementString("PayrollProfessionalRiskCode", ifr.Row(15))
                    xw.WriteElementString("PayrollEmployeePensionary", ifr.Row(16))
                    xw.WriteElementString("PayrollEmployeeTypeCode", ifr.Row(17))
                    xw.WriteElementString("PayrollCostCenterCode", ifr.Row(18))
                    xw.WriteElementString("PayrollWorkCenterCode", ifr.Row(19))
                    xw.WriteElementString("PayrollPositionCode", ifr.Row(20))
                    xw.WriteElementString("PayrollFunctionalUnitCode", ifr.Row(21))
                    xw.WriteElementString("PayrollContractTypeCode", ifr.Row(22))
                    xw.WriteElementString("PayrollContractContractInitialDate", ifr.Row(23))
                    xw.WriteElementString("PayrollContractContractEndingDate", ifr.Row(24))
                    xw.WriteElementString("PayrollContractBasicSalary", ifr.Row(25))
                    xw.WriteElementString("PayrollContractPaymentPeriod", ifr.Row(26))
                    xw.WriteElementString("PayrollContractPaymentType", ifr.Row(27))
                    xw.WriteElementString("PayrollContractTrialPeriod", ifr.Row(28))
                    xw.WriteElementString("PayrollContractTrialPeriodTime", ifr.Row(29))
                    xw.WriteElementString("PayrollContractTypeOfPensionContribution", ifr.Row(30))
                    xw.WriteElementString("PayrollGroupCode", ifr.Row(31))
                    xw.WriteElementString("PayrollBankCode", ifr.Row(32))
                    xw.WriteElementString("PayrollContractBankAccountNumber", ifr.Row(33))
                    xw.WriteElementString("PayrollContractBankAccountType", ifr.Row(34))
                    xw.WriteElementString("PayrollContractHoursDaily", ifr.Row(35))
                    xw.WriteElementString("PayrollContractContingency", ifr.Row(36))
                    xw.WriteElementString("HealthCode", ifr.Row(37))
                    xw.WriteElementString("PensionCode", ifr.Row(38))
                    xw.WriteElementString("UnemploymentCode", ifr.Row(39))
                    xw.WriteElementString("OccupationalAccidentInsuranceCode", ifr.Row(40))
                    xw.WriteElementString("FamilyWelfareCode", ifr.Row(41))
                    xw.WriteEndElement()
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXML = sw.ToString()
        End Using
    End Function

    Public Function ValidateMassiveContract(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveContract_Result) Implements IMassiveContractAdminService.ValidateMassiveContract
        Dim xmlObj As String = ConvertImportFileRowToXML(pData)
        Return _ContractRepository.ValidateMassiveContract(xmlObj)
    End Function

    Public Function GetMassiveContract(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveContract_Result) Implements IMassiveContractAdminService.GetMassiveContract
        Dim xmlObj As String = ConvertImportFileRowToXML(pData)
        Return _ContractRepository.GetMassiveContract(xmlObj)
    End Function

    Public Sub SaveMassiveContract(pData As List(Of ImportFileRow), pCodeUser As String, pIdUser As Integer) Implements IMassiveContractAdminService.SaveMassiveContract
        Dim xmlObj As String = ConvertImportFileRowToXML(pData)
        _ContractRepository.SaveMassiveContract(xmlObj, pCodeUser, pIdUser)
    End Sub

    Public Function ValidateMassiveContractExtension(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveContractExtension_Result) Implements IMassiveContractAdminService.ValidateMassiveContractExtension
        Dim xmlObj As String = ConvertImportFileRowToXMLContractExtension(pData)
        Return _ContractRepository.ValidateMassiveContractExtension(xmlObj)
    End Function

    Private Function ConvertImportFileRowToXMLContractExtension(pData As List(Of ImportFileRow), Optional psession As SessionValues = Nothing) As String
        Using sw As StringWriter = New StringWriter()
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                If psession IsNot Nothing Then
                    xw.WriteElementString("User", psession.AuditMessageWcf.IdUser)
                End If
                For Each ifr As ImportFileRow In pData
                    xw.WriteStartElement("Row")
                    xw.WriteElementString("Nit", ifr.Row(0))
                    xw.WriteElementString("ProfessionalRiskPercentage", ifr.Row(1))
                    xw.WriteElementString("Workcenter", ifr.Row(2))
                    xw.WriteElementString("ContractModificationReasonCode", ifr.Row(3))
                    xw.WriteElementString("PositionCode", ifr.Row(4))
                    xw.WriteElementString("FunctionalUnitCode", ifr.Row(5))
                    xw.WriteElementString("ContractTypeCode", ifr.Row(6))
                    xw.WriteElementString("ContractInitialDate", ifr.Row(7))
                    xw.WriteElementString("ContractEndingDate", ifr.Row(8))
                    xw.WriteElementString("BasicSalary", ifr.Row(9))
                    xw.WriteElementString("PaymentPeriod", ifr.Row(10))
                    xw.WriteElementString("PaymentType", ifr.Row(11))
                    xw.WriteElementString("GroupCode", ifr.Row(12))
                    xw.WriteElementString("BankCode", ifr.Row(13))
                    xw.WriteElementString("BankAccountNumber", ifr.Row(14))
                    xw.WriteElementString("BankAccountType", ifr.Row(15))
                    xw.WriteElementString("HoursDaily", ifr.Row(16))
                    xw.WriteElementString("Contingency", ifr.Row(17))
                    xw.WriteElementString("HealthCode", ifr.Row(18))
                    xw.WriteElementString("PensionCode", ifr.Row(19))
                    xw.WriteElementString("UnemploymentCode", ifr.Row(20))
                    xw.WriteElementString("OccupationalAccidentInsurance", ifr.Row(21))
                    xw.WriteElementString("FamilyWelfare", ifr.Row(22))

                    xw.WriteEndElement()
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXMLContractExtension = sw.ToString()
        End Using
    End Function

    ''' <summary>
    ''' MEtodo para validar el excel que se va importar de dependientes y familiares.
    ''' </summary>
    ''' <param name="pData"></param>
    ''' <returns></returns>
    Public Function ValidateMassiveDependentRelatives(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveDependentRelatives_Result) Implements IMassiveContractAdminService.ValidateMassiveDependentRelatives
        Dim xmlObj As String = ConvertImportFileRowToXMLDependentRelatives(pData)
        Return _ContractRepository.ValidateMassiveDependentRelatives(xmlObj)
    End Function

    Private Function ConvertImportFileRowToXMLDependentRelatives(pData As List(Of ImportFileRow), Optional psession As SessionValues = Nothing) As String
        Dim Size As Integer = pData.Count
        Using sw As StringWriter = New StringWriter()
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                If psession IsNot Nothing Then
                    xw.WriteElementString("User", psession.AuditMessageWcf.IdUser)
                End If
                xw.WriteElementString("Action", pData.Item(Size - 1).Row(0))
                Dim RelationName As String = String.Empty

                For Each ifr As ImportFileRow In pData
                    If ifr.Row(0).ToString() <> "Confirmar" Then
                        xw.WriteStartElement("Row")
                        xw.WriteElementString("Nit", ifr.Row(0))
                        xw.WriteElementString("Kinship", ifr.Row(1))
                        RelationName = ifr.Row(2).ToString().Replace(Microsoft.VisualBasic.Constants.vbCrLf, "")
                        xw.WriteElementString("RelationName", RelationName.Replace("  ", " "))
                        xw.WriteElementString("BirthDate", ifr.Row(3))
                        xw.WriteEndElement()
                    End If
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXMLDependentRelatives = sw.ToString()
        End Using
    End Function

    Public Function GetMassiveContractExtension(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveContractExtension_Result) Implements IMassiveContractAdminService.GetMassiveContractExtension
        Dim xmlObj As String = ConvertImportFileRowToXMLContractExtension(pData)
        Return _ContractRepository.GetMassiveContractExtension(xmlObj)
    End Function

    Public Sub SaveMassiveContractExtension(pData As List(Of ImportFileRow), psession As SessionValues) Implements IMassiveContractAdminService.SaveMassiveContractExtension
        Dim xmlObj As String = ConvertImportFileRowToXMLContractExtension(pData, psession:=psession)
        _ContractRepository.SaveMassiveContractExtension(xmlObj)
    End Sub

    Public Function ValidateMassiveExternalEntities(pData As List(Of ImportFileRow), psession As SessionValues) As List(Of SP_ValidateMassiveExternalEntities_Result) Implements IMassiveContractAdminService.ValidateMassiveExternalEntities
        Dim xmlObj As String = ConvertImportFileRowToXMLExternalEntities(pData, psession:=psession)
        Return _ContractRepository.ValidateMassiveExternalEntities(xmlObj)
    End Function

    Private Function ConvertImportFileRowToXMLExternalEntities(pData As List(Of ImportFileRow), Optional psession As SessionValues = Nothing) As String
        Using sw As StringWriter = New StringWriter()
            Dim Size As Integer = pData.Count
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                If psession IsNot Nothing Then
                    xw.WriteElementString("User", psession.AuditMessageWcf.IdUser)
                End If
                xw.WriteElementString("Action", pData.Item(Size - 1).Row(0))

                For Each ifr As ImportFileRow In pData
                    If ifr.Row(0) Is Nothing OrElse ifr.Row(0).ToString() <> "Confirmar" Then
                        xw.WriteStartElement("Row")
                        xw.WriteElementString("Nit", ifr.Row(0))
                        xw.WriteElementString("FundCode", ifr.Row(1))
                        xw.WriteElementString("FundType", ifr.Row(2))
                        xw.WriteElementString("FundInitialDate", ifr.Row(3))
                        xw.WriteElementString("VoluntaryContribution", ifr.Row(4))
                        xw.WriteElementString("VoluntaryContributionValue", ifr.Row(5))
                        xw.WriteElementString("BankCode", ifr.Row(6))
                        xw.WriteElementString("BankAccountNumberType", ifr.Row(7))
                        xw.WriteElementString("BankAccountNumber", ifr.Row(8))
                        xw.WriteEndElement()
                    End If
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXMLExternalEntities = sw.ToString()
        End Using
    End Function

    Public Function ValidateMassiveNovelties(pData As GenericListNovelty, psession As SessionValues) As List(Of SP_ValidateMassiveNovelties_Result) Implements IMassiveContractAdminService.ValidateMassiveNovelties
        Dim xmlObj As String = String.Empty
        If pData.ListSP_ValidateMassiveNovelty IsNot Nothing AndAlso pData.ListSP_ValidateMassiveNovelty.Count > 0 Then
            xmlObj = ConvertImportFileRowToXMLNoveltiesConfirm(pData.ListSP_ValidateMassiveNovelty, psession:=psession)
        ElseIf pData.List_ImportFileRow IsNot Nothing AndAlso pData.List_ImportFileRow.Count > 0 Then
            xmlObj = ConvertImportFileRowToXMLNovelties(pData.List_ImportFileRow, psession:=psession)
        End If

        Return _ContractRepository.ValidateMassiveNovelties(xmlObj)
    End Function

    Private Function ConvertImportFileRowToXMLNovelties(pData As List(Of ImportFileRow), Optional psession As SessionValues = Nothing) As String
        Using sw As StringWriter = New StringWriter()
            Dim Size As Integer = pData.Count
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                If psession IsNot Nothing Then
                    xw.WriteElementString("User", psession.AuditMessageWcf.IdUser)
                End If
                xw.WriteElementString("Action", pData.Item(Size - 1).Row(0))

                For Each ifr As ImportFileRow In pData
                    If ifr.Row(0) Is Nothing OrElse ifr.Row(0).ToString() <> "Confirmar" Then
                        xw.WriteStartElement("Row")
                        xw.WriteElementString("Nit", ifr.Row(0))
                        xw.WriteElementString("TypeNovelty", ifr.Row(1))
                        xw.WriteElementString("Extension", ifr.Row(2))
                        xw.WriteElementString("InabilityClass", ifr.Row(3))
                        xw.WriteElementString("RiskType", ifr.Row(4))
                        xw.WriteElementString("RealDate", ifr.Row(5))
                        xw.WriteElementString("Days", ifr.Row(6))
                        xw.WriteElementString("Reason", ifr.Row(7))
                        xw.WriteElementString("TipoLiquidar", ifr.Row(8))
                        xw.WriteElementString("PostularValor", ifr.Row(9))

                        xw.WriteEndElement()
                    End If
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXMLNovelties = sw.ToString()
        End Using
    End Function

    Private Function ConvertImportFileRowToXMLNoveltiesConfirm(pData As List(Of SP_ValidateMassiveNovelties_Result), Optional psession As SessionValues = Nothing) As String
        Using sw As StringWriter = New StringWriter()
            Dim Size As Integer = pData.Count
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                If psession IsNot Nothing Then
                    xw.WriteElementString("User", psession.AuditMessageWcf.IdUser)
                End If
                xw.WriteElementString("Action", pData.Item(Size - 1).Mensaje)

                For Each ifr As SP_ValidateMassiveNovelties_Result In pData
                    If ifr.Tipo IsNot Nothing AndAlso ifr.Tipo.ToString() = "Datos" Then
                        xw.WriteStartElement("Row")
                        xw.WriteElementString("Linea", ifr.Linea)
                        xw.WriteElementString("Nit", ifr.Nit)
                        xw.WriteElementString("TypeNovelty", ifr.TypeNovelty)
                        xw.WriteElementString("Extension", ifr.Extension)
                        xw.WriteElementString("InabilityClass", ifr.InabilityClass)
                        xw.WriteElementString("RiskType", ifr.RiskType)
                        xw.WriteElementString("RealDate", ifr.RealDate)
                        xw.WriteElementString("EndDate", ifr.EndDate)
                        xw.WriteElementString("Days", ifr.Days)
                        xw.WriteElementString("Reason", ifr.Reason)
                        xw.WriteElementString("TipoLiquidar", ifr.TipoLiquidar)
                        xw.WriteElementString("PostularValor", ifr.PostularValor)
                        If ifr.PaidEmployervalue IsNot Nothing Then
                            xw.WriteElementString("PaidEmployervalue", ifr.PaidEmployervalue)
                        End If
                        If ifr.EPSRecognizeValue IsNot Nothing Then
                            xw.WriteElementString("EPSRecognizeValue", ifr.EPSRecognizeValue)
                        End If
                        If ifr.Value IsNot Nothing Then
                            xw.WriteElementString("Value", ifr.Value)
                        End If
                        xw.WriteElementString("ActionVacation", ifr.ActionVacation)
                        xw.WriteElementString("ActionSchedule", ifr.ActionSchedule)
                        If ifr.EmployerDays IsNot Nothing Then
                            xw.WriteElementString("EmployerDays", ifr.EmployerDays)
                        End If
                        If ifr.EPSDays IsNot Nothing Then
                            xw.WriteElementString("EPSDays", ifr.EPSDays)
                        End If
                        If ifr.BasicSalary IsNot Nothing Then
                            xw.WriteElementString("BasicSalary", ifr.BasicSalary)
                        End If
                        xw.WriteEndElement()
                    End If
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXMLNoveltiesConfirm = sw.ToString()
        End Using
    End Function
    ''' <summary>
    ''' Metodo para validar la informacion que se tiene en el front de ingreso y salida de empleados.
    ''' </summary>
    ''' <param name="pData"></param>
    ''' <param name="psession"></param>
    ''' <returns></returns>
    Public Function ValidateMassiveEmployeeSchedule(pData As GenericListEmployeeSchedule, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, psession As SessionValues) As List(Of SP_ValidateMassiveEmployeeSchedule_Result) Implements IMassiveContractAdminService.ValidateMassiveEmployeeSchedule
        Dim xmlObj As String = String.Empty
        If pData.ListSP_ValidateMassiveEmployeeSchedule IsNot Nothing AndAlso pData.ListSP_ValidateMassiveEmployeeSchedule.Count > 0 Then
            xmlObj = ConvertImportFileRowToXMLEmployeeScheduleConfirm(pData.ListSP_ValidateMassiveEmployeeSchedule, psession:=psession)
        ElseIf pData.List_ImportFileRow IsNot Nothing AndAlso pData.List_ImportFileRow.Count > 0 Then
            xmlObj = ConvertImportFileRowToXMLEmployeeSchedule(pData.List_ImportFileRow, psession:=psession)
        End If
        Try
            Return _ContractRepository.ValidateMassiveEmployeeSchedule(xmlObj, Action, Code, Description, Status, Id, Prefix)
        Catch ex As Exception
            Dim ResultListEx As List(Of SP_ValidateMassiveEmployeeSchedule_Result) = New List(Of SP_ValidateMassiveEmployeeSchedule_Result)
            Dim ResultEx As SP_ValidateMassiveEmployeeSchedule_Result = New SP_ValidateMassiveEmployeeSchedule_Result
            ResultEx.Linea = "1"
            ResultEx.Mensaje = ex.InnerException.ToString()
            ResultListEx.Add(ResultEx)
            Return ResultListEx
        End Try
    End Function
    ''' <summary>
    ''' Armar XML cuando se va validar lo que se importa del archivo de excel.
    ''' </summary>
    ''' <param name="pData"></param>
    ''' <param name="psession"></param>
    ''' <returns></returns>
    Private Function ConvertImportFileRowToXMLEmployeeSchedule(pData As List(Of ImportFileRow), Optional psession As SessionValues = Nothing) As String
        Using sw As StringWriter = New StringWriter()
            Dim Size As Integer = pData.Count
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                If psession IsNot Nothing Then
                    xw.WriteElementString("User", psession.AuditMessageWcf.CodeUser)
                End If
                xw.WriteElementString("Action", pData.Item(Size - 1).Row(0))

                For Each ifr As ImportFileRow In pData
                    If ifr.Row(0) Is Nothing OrElse ifr.Row(0).ToString() <> "Confirmar" Then
                        Dim InitialDate As Date = Convert.ToDateTime(ifr.Row(1))
                        Dim EndDate As Date = Convert.ToDateTime(ifr.Row(3))
                        Dim InitialHour As Date = Convert.ToDateTime(ifr.Row(2))
                        Dim EndHour As Date = Convert.ToDateTime(ifr.Row(4))
                        xw.WriteStartElement("Row")
                        xw.WriteElementString("Nit", ifr.Row(0))
                        xw.WriteElementString("InitialDate", ifr.Row(1))
                        xw.WriteElementString("InitialHourDate", InitialHour.ToString("HH:mm"))
                        xw.WriteElementString("EndDate", ifr.Row(3))
                        xw.WriteElementString("EndHourDate", EndHour.ToString("HH:mm"))
                        xw.WriteElementString("InitialDateComplete", New DateTime(InitialDate.Year, InitialDate.Month, InitialDate.Day, InitialHour.Hour, InitialHour.Minute, 0).ToString("dd/MM/yyyy HH:mm"))
                        xw.WriteElementString("EndDateComplete", New DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndHour.Hour, EndHour.Minute, 0).ToString("dd/MM/yyyy HH:mm"))
                        xw.WriteEndElement()
                    End If
                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXMLEmployeeSchedule = sw.ToString()
        End Using
    End Function
    ''' <summary>
    ''' Armar XML cuando se va confirmar lo que se importa del archivo de excel y se valido previamente.
    ''' </summary>
    ''' <param name="pData"></param>
    ''' <param name="psession"></param>
    ''' <returns></returns>
    Function ConvertImportFileRowToXMLEmployeeScheduleConfirm(pData As List(Of SP_ValidateMassiveEmployeeSchedule_Result), Optional psession As SessionValues = Nothing) As String
        Using sw As StringWriter = New StringWriter()
            Dim Size As Integer = pData.Count
            Using xw As XmlWriter = XmlWriter.Create(sw)
                xw.WriteStartElement("Data")
                If psession IsNot Nothing Then
                    xw.WriteElementString("User", psession.AuditMessageWcf.CodeUser)
                End If
                xw.WriteElementString("Action", pData.Item(Size - 1).Mensaje)

                For Each ifr As SP_ValidateMassiveEmployeeSchedule_Result In pData
                    If ifr.Tipo IsNot Nothing AndAlso ifr.Tipo.ToString() = "Datos" Then
                        Dim InitialDate As Date = ifr.InitialDate
                        Dim EndDate As Date = ifr.EndDate
                        Dim InitialHour As Date = ifr.InitialHourDate
                        Dim EndHour As Date = ifr.EndHourDate
                        xw.WriteStartElement("Row")
                        xw.WriteElementString("Linea", ifr.Linea)
                        xw.WriteElementString("Nit", ifr.Nit)
                        xw.WriteElementString("InitialDate", ifr.InitialDate)
                        xw.WriteElementString("InitialHourDate", ifr.InitialHourDate)
                        xw.WriteElementString("EndDate", ifr.EndDate)
                        xw.WriteElementString("EndHourDate", ifr.EndHourDate)
                        xw.WriteElementString("Hours", ifr.Hours)
                        xw.WriteElementString("Comments", ifr.Comments)
                        xw.WriteElementString("InitialDateComplete", New DateTime(InitialDate.Year, InitialDate.Month, InitialDate.Day, InitialHour.Hour, InitialHour.Minute, 0).ToString("dd/MM/yyyy HH:mm"))
                        xw.WriteElementString("EndDateComplete", New DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndHour.Hour, EndHour.Minute, 0).ToString("dd/MM/yyyy HH:mm"))
                        xw.WriteEndElement()
                    End If

                    If ifr.Mensaje = "Reporte" Then

                        Dim TmpMonth As Integer = ifr.EndDate
                        Dim TmpYear As Integer = ifr.InitialDate

                        Dim InitialDate = New Date(TmpYear, TmpMonth, 1)
                        Dim EndDate = New Date(TmpYear, TmpMonth, Date.DaysInMonth(TmpYear, TmpMonth))
                        xw.WriteStartElement("Row")
                        xw.WriteElementString("InitialDateComplete", New DateTime(InitialDate.Year, InitialDate.Month, InitialDate.Day, 0, 0, 0).ToString("dd/MM/yyyy HH:mm"))
                        xw.WriteElementString("EndDateComplete", New DateTime(EndDate.Year, EndDate.Month, EndDate.Day, 23, 59, 0).ToString("dd/MM/yyyy HH:mm"))
                        xw.WriteEndElement()
                    End If

                Next
                xw.WriteEndElement()
            End Using
            ConvertImportFileRowToXMLEmployeeScheduleConfirm = sw.ToString()
        End Using
    End Function

    Public Function GetEmployeeScheduleC(Code As String) As EmployeeScheduleC Implements IMassiveContractAdminService.GetEmployeeScheduleC
        Try
            Return _ContractRepository.GetEmployeeScheduleC(Code)
        Catch ex As Exception
            Return New EmployeeScheduleC
        End Try

    End Function

    Public Function DeleteEmployeeScheduleDetail(IdEmployeeSchedule As Integer, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, psession As SessionValues) As ActionResult(Of List(Of SP_ValidateMassiveEmployeeSchedule_Result)) Implements IMassiveContractAdminService.DeleteEmployeeScheduleDetail

        Dim result As New ActionResult(Of List(Of SP_ValidateMassiveEmployeeSchedule_Result))
        result.StateResult = True

        Dim ObjEmployeeScheduleDetail = _employeeScheduleDetailRepository.GetEmployeeScheduleDetail(IdEmployeeSchedule)

        If ObjEmployeeScheduleDetail Is Nothing Then
            Throw New ArgumentNullException("ObjEmployeeScheduleDetail Vacia")
        End If
        Dim unitWork As IUnitWork = _employeeScheduleDetailRepository.UnitWork
        Try
            _employeeScheduleDetailRepository.DeleteEntity(ObjEmployeeScheduleDetail)
            unitWork.Commit()
            '/***** Auditoria Basica ********/
            IndigoAuditBasic.Execute("EmployeeScheduleDetail", ObjEmployeeScheduleDetail.EmployeeId, ObjEmployeeScheduleDetail.Id, psession.AuditMessageWcf.NameUser, psession.AuditMessageWcf.CodeUser, psession.AuditMessageWcf.WindowsUser, DateTime.Now, ActionsAudit.Eliminar, psession.AuditMessageWcf.Company, psession.AuditMessageWcf.ContainerSecurity)
            '/*****Auditoria Avanzada ******/
            Dim auditObject As New IndigoAuditSimpleEntity(Of EmployeeScheduleDetail)(ObjEmployeeScheduleDetail, psession.AuditMessageWcf, Infrastructure.CrossCutting.Audit.Actions.Delete)
            auditObject.Execute()
            'IndigoAuditSimpleEntity(Of BranchOffice).Execute(branchOffice, audit, Infrastructure.CrossCutting.Audit.Actions.Delete, branchOffice)
            Return result
        Catch ex As DbUpdateException
            unitWork.RollbackChanges()
            result.StateResult = False
            result.Message = ex.Message
            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", psession)
            result.StateResult = False
            Return result
        End Try

    End Function



End Class
