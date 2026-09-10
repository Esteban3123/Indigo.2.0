'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure
Imports Application.Payroll
Imports Domain.Entities
Imports System.Text
Imports System.Data.SqlClient
Imports System.Data.Entity.Core
Imports System.Transactions

Public Class InitialBalancePayrollAdminService
    Implements IInitialBalancePayrollAdminService

#Region "Variables"

    ''' <summary>
    ''' Variable tipo repositorio para dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _initialBalancePayrollRepository As IInitialBalancePayrollRepository

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(initialBalancePayrollRepository As IInitialBalancePayrollRepository)
        If initialBalancePayrollRepository Is Nothing Then
            Throw New ArgumentNullException("initialBalancePayrollRepository Vacio")
        End If
        _initialBalancePayrollRepository = initialBalancePayrollRepository
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Importa el archivo de excel y valida la información
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SP_ImportFileInitialBalancePayroll(data As List(Of ImportFileRow)) As ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result)) Implements IInitialBalancePayrollAdminService.SP_ImportFileInitialBalancePayroll
        If data Is Nothing OrElse data.Count = 0 Then
            Throw New ArgumentNullException("data")
        End If
        Try
            'Objeto xml
            Dim xmlObject = ConvertToXmlByImportFile(data)

            'Se consume el procedimiento almacenado
            Dim resultStore = _initialBalancePayrollRepository.SP_ImportFileInitialBalancePayroll(xmlObject)

            'Se devuelve el mensaje
            Return New ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result)) With {.StateResult = True, .ObjectEmbbeded = resultStore}
        Catch ex As SqlException
            If ex.ErrorCode = -2146232060 Then
                Return New ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result)) With {.StateResult = False, .Message = "Los valores contienen decimales con un formato no valido, por favor corrija para poder continuar"}
            Else
                Return New ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result)) With {.StateResult = False, .Message = ex.ToString}
            End If
        Catch ex As Exception
            Return New ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result)) With {.StateResult = False, .Message = ex.ToString}
        End Try
    End Function

    ''' <summary>
    ''' Metodo que me convierte el listado de datos en xml cuando es por importacion
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Private Function ConvertToXmlByImportFile(data As List(Of ImportFileRow)) As String
        'String de xml que se arma con el listado
        Dim builder As StringBuilder = New StringBuilder()

        'Cantidad de item para validar 
        Dim count As Integer = 0

        builder.Append("<Data>")

        For Each info In data

            'Se asigna la cantidad de items
            count = info.Row.Count

            builder.Append("<Row>")

            builder.Append("<CountFields>" & info.Row.Count & "</CountFields>")
            builder.Append("<StatusField>" & 0 & "</StatusField>")
            builder.Append("<MessageField>" & "-" & "</MessageField>")

            If count > 0 Then
                builder.Append("<Nit>" & info.Row(0) & "</Nit>")
                count -= 1
            Else
                builder.Append("<Nit></Nit>")
            End If

            builder.Append("<NitName>" & "-" & "</NitName>")
            builder.Append("<EmployeeId>" & 0 & "</EmployeeId>")

            If count > 0 Then
                builder.Append("<PayrollDate>" & info.Row(1) & "</PayrollDate>")
                count -= 1
            Else
                builder.Append("<PayrollDate></PayrollDate>")
            End If

            If count > 0 Then
                builder.Append("<DaysWorked>" & info.Row(2) & "</DaysWorked>")
                count -= 1
            Else
                builder.Append("<DaysWorked></DaysWorked>")
            End If

            If count > 0 Then
                builder.Append("<TotalAccrued>" & info.Row(3) & "</TotalAccrued>")
                count -= 1
            Else
                builder.Append("<TotalAccrued></TotalAccrued>")
            End If

            If count > 0 Then
                builder.Append("<TotalDeducted>" & IIf(info.Row(4) Is Nothing OrElse info.Row(4) < "0", "0", info.Row(4)) & "</TotalDeducted>")
                count -= 1
            Else
                builder.Append("<TotalDeducted></TotalDeducted>")
            End If

            If count > 0 Then
                builder.Append("<PensionJCB>" & IIf(info.Row(5) Is Nothing OrElse info.Row(5) < "0", "0", info.Row(5)) & "</PensionJCB>")
                count -= 1
            Else
                builder.Append("<PensionJCB></PensionJCB>")
            End If

            If count > 0 Then
                builder.Append("<PensionContributionValue>" & IIf(info.Row(6) Is Nothing OrElse info.Row(6) < "0", "0", info.Row(6)) & "</PensionContributionValue>")
                count -= 1
            Else
                builder.Append("<PensionContributionValue></PensionContributionValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodePensionContributionValue>" & info.Row(7) & "</ConceptCodePensionContributionValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodePensionContributionValue></ConceptCodePensionContributionValue>")
            End If

            builder.Append("<ConceptIdPensionContributionValue>" & 0 & "</ConceptIdPensionContributionValue>")

            If count > 0 Then
                builder.Append("<EmployerPensionContributionValue>" & IIf(info.Row(8) Is Nothing OrElse info.Row(8) < "0", "0", info.Row(8)) & "</EmployerPensionContributionValue>")
                count -= 1
            Else
                builder.Append("<EmployerPensionContributionValue></EmployerPensionContributionValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeEmployerPensionContributionValue>" & info.Row(9) & "</ConceptCodeEmployerPensionContributionValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeEmployerPensionContributionValue></ConceptCodeEmployerPensionContributionValue>")
            End If

            builder.Append("<ConceptIdEmployerPensionContributionValue>" & 0 & "</ConceptIdEmployerPensionContributionValue>")

            If count > 0 Then
                builder.Append("<HealthJCB>" & IIf(info.Row(10) Is Nothing OrElse info.Row(10) < "0", "0", info.Row(10)) & "</HealthJCB>")
                count -= 1
            Else
                builder.Append("<HealthJCB></HealthJCB>")
            End If

            If count > 0 Then
                builder.Append("<EmployeeHealthContributionValue>" & IIf(info.Row(11) Is Nothing OrElse info.Row(11) < "0", "0", info.Row(11)) & "</EmployeeHealthContributionValue>")
                count -= 1
            Else
                builder.Append("<EmployeeHealthContributionValue></EmployeeHealthContributionValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeEmployeeHealthContributionValue>" & info.Row(12) & "</ConceptCodeEmployeeHealthContributionValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeEmployeeHealthContributionValue></ConceptCodeEmployeeHealthContributionValue>")
            End If

            builder.Append("<ConceptIdEmployeeHealthContributionValue>" & 0 & "</ConceptIdEmployeeHealthContributionValue>")

            If count > 0 Then
                builder.Append("<EmployerHealthContributionValue>" & IIf(info.Row(13) Is Nothing OrElse info.Row(13) < "0", "0", info.Row(13)) & "</EmployerHealthContributionValue>")
                count -= 1
            Else
                builder.Append("<EmployerHealthContributionValue></EmployerHealthContributionValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeEmployerHealthContributionValue>" & info.Row(14) & "</ConceptCodeEmployerHealthContributionValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeEmployerHealthContributionValue></ConceptCodeEmployerHealthContributionValue>")
            End If

            builder.Append("<ConceptIdEmployerHealthContributionValue>" & 0 & "</ConceptIdEmployerHealthContributionValue>")

            If count > 0 Then
                builder.Append("<IBCIncentivePayment>" & IIf(info.Row(15) Is Nothing OrElse info.Row(15) < "0", "0", info.Row(15)) & "</IBCIncentivePayment>")
                count -= 1
            Else
                builder.Append("<IBCIncentivePayment></IBCIncentivePayment>")
            End If

            If count > 0 Then
                builder.Append("<ProvisionIncentive>" & IIf(info.Row(16) Is Nothing OrElse info.Row(16) < "0", "0", info.Row(16)) & "</ProvisionIncentive>")
                count -= 1
            Else
                builder.Append("<ProvisionIncentive></ProvisionIncentive>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeProvisionIncentive>" & info.Row(17) & "</ConceptCodeProvisionIncentive>")
                count -= 1
            Else
                builder.Append("<ConceptCodeProvisionIncentive></ConceptCodeProvisionIncentive>")
            End If

            builder.Append("<ConceptIdProvisionIncentive>" & 0 & "</ConceptIdProvisionIncentive>")

            If count > 0 Then
                builder.Append("<IBCVacation>" & IIf(info.Row(18) Is Nothing OrElse info.Row(18) < "0", "0", info.Row(18)) & "</IBCVacation>")
                count -= 1
            Else
                builder.Append("<IBCVacation></IBCVacation>")
            End If

            If count > 0 Then
                builder.Append("<ProvisionVacation>" & IIf(info.Row(19) Is Nothing OrElse info.Row(19) < "0", "0", info.Row(19)) & "</ProvisionVacation>")
                count -= 1
            Else
                builder.Append("<ProvisionVacation></ProvisionVacation>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeProvisionVacation>" & info.Row(20) & "</ConceptCodeProvisionVacation>")
                count -= 1
            Else
                builder.Append("<ConceptCodeProvisionVacation></ConceptCodeProvisionVacation>")
            End If

            builder.Append("<ConceptIdProvisionVacation>" & 0 & "</ConceptIdProvisionVacation>")

            If count > 0 Then
                builder.Append("<IBCUnemployment>" & IIf(info.Row(21) Is Nothing OrElse info.Row(21) < "0", "0", info.Row(21)) & "</IBCUnemployment>")
                count -= 1
            Else
                builder.Append("<IBCUnemployment></IBCUnemployment>")
            End If

            If count > 0 Then
                builder.Append("<UnemploymentAccumulated>" & IIf(info.Row(22) Is Nothing OrElse info.Row(22) < "0", "0", info.Row(22)) & "</UnemploymentAccumulated>")
                count -= 1
            Else
                builder.Append("<UnemploymentAccumulated></UnemploymentAccumulated>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeUnemploymentAccumulated>" & info.Row(23) & "</ConceptCodeUnemploymentAccumulated>")
                count -= 1
            Else
                builder.Append("<ConceptCodeUnemploymentAccumulated></ConceptCodeUnemploymentAccumulated>")
            End If

            builder.Append("<ConceptIdUnemploymentAccumulated>" & 0 & "</ConceptIdUnemploymentAccumulated>")

            If count > 0 Then
                builder.Append("<ProvisionInterestsUnemployment>" & IIf(info.Row(24) Is Nothing OrElse info.Row(24) < "0", "0", info.Row(24)) & "</ProvisionInterestsUnemployment>")
                count -= 1
            Else
                builder.Append("<ProvisionInterestsUnemployment></ProvisionInterestsUnemployment>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeProvisionInterestsUnemployment>" & info.Row(25) & "</ConceptCodeProvisionInterestsUnemployment>")
                count -= 1
            Else
                builder.Append("<ConceptCodeProvisionInterestsUnemployment></ConceptCodeProvisionInterestsUnemployment>")
            End If

            builder.Append("<ConceptIdProvisionInterestsUnemployment>" & 0 & "</ConceptIdProvisionInterestsUnemployment>")

            If count > 0 Then
                builder.Append("<AmbulatoryDisabilityValue>" & IIf(info.Row(26) Is Nothing OrElse info.Row(26) < "0", "0", info.Row(26)) & "</AmbulatoryDisabilityValue>")
                count -= 1
            Else
                builder.Append("<AmbulatoryDisabilityValue></AmbulatoryDisabilityValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeAmbulatoryDisabilityValue>" & info.Row(27) & "</ConceptCodeAmbulatoryDisabilityValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeAmbulatoryDisabilityValue></ConceptCodeAmbulatoryDisabilityValue>")
            End If

            builder.Append("<ConceptIdAmbulatoryDisabilityValue>" & 0 & "</ConceptIdAmbulatoryDisabilityValue>")

            If count > 0 Then
                builder.Append("<DisabilityHospitalValue>" & IIf(info.Row(28) Is Nothing OrElse info.Row(28) < "0", "0", info.Row(28)) & "</DisabilityHospitalValue>")
                count -= 1
            Else
                builder.Append("<DisabilityHospitalValue></DisabilityHospitalValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeDisabilityHospitalValue>" & info.Row(29) & "</ConceptCodeDisabilityHospitalValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeDisabilityHospitalValue></ConceptCodeDisabilityHospitalValue>")
            End If

            builder.Append("<ConceptIdDisabilityHospitalValue>" & 0 & "</ConceptIdDisabilityHospitalValue>")

            If count > 0 Then
                builder.Append("<MaternityLeaveValue>" & IIf(info.Row(30) Is Nothing OrElse info.Row(30) < "0", "0", info.Row(30)) & "</MaternityLeaveValue>")
                count -= 1
            Else
                builder.Append("<MaternityLeaveValue></MaternityLeaveValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeMaternityLeaveValue>" & info.Row(31) & "</ConceptCodeMaternityLeaveValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeMaternityLeaveValue></ConceptCodeMaternityLeaveValue>")
            End If

            builder.Append("<ConceptIdMaternityLeaveValue>" & 0 & "</ConceptIdMaternityLeaveValue>")

            If count > 0 Then
                builder.Append("<IBCSENA>" & IIf(info.Row(32) Is Nothing OrElse info.Row(32) < "0", "0", info.Row(32)) & "</IBCSENA>")
                count -= 1
            Else
                builder.Append("<IBCSENA></IBCSENA>")
            End If

            If count > 0 Then
                builder.Append("<SenaContributionValue>" & IIf(info.Row(33) Is Nothing OrElse info.Row(33) < "0", "0", info.Row(33)) & "</SenaContributionValue>")
                count -= 1
            Else
                builder.Append("<SenaContributionValue></SenaContributionValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeSenaContributionValue>" & info.Row(34) & "</ConceptCodeSenaContributionValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeSenaContributionValue></ConceptCodeSenaContributionValue>")
            End If

            builder.Append("<ConceptIdSenaContributionValue>" & 0 & "</ConceptIdSenaContributionValue>")

            If count > 0 Then
                builder.Append("<IBCICBF>" & IIf(info.Row(35) Is Nothing OrElse info.Row(35) < "0", "0", info.Row(35)) & "</IBCICBF>")
                count -= 1
            Else
                builder.Append("<IBCICBF></IBCICBF>")
            End If

            If count > 0 Then
                builder.Append("<ICBFContributionValue>" & IIf(info.Row(36) Is Nothing OrElse info.Row(36) < "0", "0", info.Row(36)) & "</ICBFContributionValue>")
                count -= 1
            Else
                builder.Append("<ICBFContributionValue></ICBFContributionValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeICBFContributionValue>" & info.Row(37) & "</ConceptCodeICBFContributionValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeICBFContributionValue></ConceptCodeICBFContributionValue>")
            End If

            builder.Append("<ConceptIdICBFContributionValue>" & 0 & "</ConceptIdICBFContributionValue>")

            If count > 0 Then
                builder.Append("<IBCCompensationFund>" & IIf(info.Row(38) Is Nothing OrElse info.Row(38) < "0", "0", info.Row(38)) & "</IBCCompensationFund>")
                count -= 1
            Else
                builder.Append("<IBCCompensationFund></IBCCompensationFund>")
            End If

            If count > 0 Then
                builder.Append("<FamilyCompensationFundContributionValue>" & IIf(info.Row(39) Is Nothing OrElse info.Row(39) < "0", "0", info.Row(39)) & "</FamilyCompensationFundContributionValue>")
                count -= 1
            Else
                builder.Append("<FamilyCompensationFundContributionValue></FamilyCompensationFundContributionValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeFamilyCompensationFundContributionValue>" & info.Row(40) & "</ConceptCodeFamilyCompensationFundContributionValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeFamilyCompensationFundContributionValue></ConceptCodeFamilyCompensationFundContributionValue>")
            End If

            builder.Append("<ConceptIdFamilyCompensationFundContributionValue>" & 0 & "</ConceptIdFamilyCompensationFundContributionValue>")

            If count > 0 Then
                builder.Append("<RealBaseRetention>" & IIf(info.Row(41) Is Nothing OrElse info.Row(41) < "0", "0", info.Row(41)) & "</RealBaseRetention>")
                count -= 1
            Else
                builder.Append("<RealBaseRetention></RealBaseRetention>")
            End If

            If count > 0 Then
                builder.Append("<CalculatedWithholdingValue>" & IIf(info.Row(42) Is Nothing OrElse info.Row(42) < "0", "0", info.Row(42)) & "</CalculatedWithholdingValue>")
                count -= 1
            Else
                builder.Append("<CalculatedWithholdingValue></CalculatedWithholdingValue>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeCalculatedWithholdingValue>" & info.Row(43) & "</ConceptCodeCalculatedWithholdingValue>")
                count -= 1
            Else
                builder.Append("<ConceptCodeCalculatedWithholdingValue></ConceptCodeCalculatedWithholdingValue>")
            End If

            builder.Append("<ConceptIdCalculatedWithholdingValue>" & 0 & "</ConceptIdCalculatedWithholdingValue>")

            If count > 0 Then
                builder.Append("<RecargoNocturno>" & IIf(info.Row(44) Is Nothing OrElse info.Row(44) < "0", "0", info.Row(44)) & "</RecargoNocturno>")
                count -= 1
            Else
                builder.Append("<RecargoNocturno></RecargoNocturno>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeRecargoNocturno>" & info.Row(45) & "</ConceptCodeRecargoNocturno>")
                count -= 1
            Else
                builder.Append("<ConceptCodeRecargoNocturno></ConceptCodeRecargoNocturno>")
            End If

            builder.Append("<ConceptIdRecargoNocturno>" & 0 & "</ConceptIdRecargoNocturno>")
            builder.Append("<TotalPaid>" & 0 & "</TotalPaid>")

            If count > 0 Then
                builder.Append("<SanctionDays>" & IIf(info.Row(46) Is Nothing OrElse info.Row(46) < "0", "0", info.Row(46)) & "</SanctionDays>")
                count -= 1
            Else
                builder.Append("<SanctionDays></SanctionDays>")
            End If

            If count > 0 Then
                builder.Append("<Overtime>" & IIf(info.Row(47) Is Nothing OrElse info.Row(47) < "0", "0", info.Row(47)) & "</Overtime>")
                count -= 1
            Else
                builder.Append("<Overtime></Overtime>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeOvertime>" & info.Row(48) & "</ConceptCodeOvertime>")
                count -= 1
            Else
                builder.Append("<ConceptCodeOvertime></ConceptCodeOvertime>")
            End If

            builder.Append("<ConceptIdOvertime>" & 0 & "</ConceptIdOvertime>")

            'Nuevos Conceptos
            If count > 0 Then
                builder.Append("<RecargoNocturnoFestivo>" & IIf(info.Row(49) Is Nothing OrElse info.Row(49) < "0", "0", info.Row(49)) & "</RecargoNocturnoFestivo>")
                count -= 1
            Else
                builder.Append("<RecargoNocturnoFestivo></RecargoNocturnoFestivo>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeRecargoNocturnoFestivo>" & info.Row(50) & "</ConceptCodeRecargoNocturnoFestivo>")
                count -= 1
            Else
                builder.Append("<ConceptCodeRecargoNocturnoFestivo></ConceptCodeRecargoNocturnoFestivo>")
            End If

            builder.Append("<ConceptIdRecargoNocturnoFestivo>" & 0 & "</ConceptIdRecargoNocturnoFestivo>")
            '----------
            If count > 0 Then
                builder.Append("<ValorDominicalOrdinario>" & IIf(info.Row(51) Is Nothing OrElse info.Row(51) < "0", "0", info.Row(51)) & "</ValorDominicalOrdinario>")
                count -= 1
            Else
                builder.Append("<ValorDominicalOrdinario></ValorDominicalOrdinario>")
            End If

            If count > 0 Then
                builder.Append("<ConceptCodeValorDominical>" & info.Row(52) & "</ConceptCodeValorDominical>")
                count -= 1
            Else
                builder.Append("<ConceptCodeValorDominical></ConceptCodeValorDominical>")
            End If

            builder.Append("<ConceptIdValorDominical>" & 0 & "</ConceptIdValorDominical>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

    ''' <summary>
    ''' Guarda la informacion de los saldos iniciales
    ''' </summary>
    ''' <returns></returns>
    Public Function SP_SaveInitialBalancePayroll(ListInfo As List(Of SP_ImportFileInitialBalancePayroll_Result), Audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IInitialBalancePayrollAdminService.SP_SaveInitialBalancePayroll
        If ListInfo Is Nothing OrElse ListInfo.Count = 0 Then
            Throw New ArgumentNullException("ListInfo")
        End If

        Dim txSettings As New TransactionOptions()
        txSettings.Timeout = TransactionManager.MaximumTimeout
        txSettings.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        Using Transaction As New TransactionScope(TransactionScopeOption.Required, txSettings)
            Try
                'Objeto xml
                Dim xmlObject = ConvertToXmlBySave(ListInfo)

                'Se envia la info al sp
                Dim resultStore = _initialBalancePayrollRepository.SP_SaveInitialBalancePayroll(xmlObject, Audit.CodeUser)

                If resultStore Is Nothing OrElse resultStore.Count = 0 Then 'Si no hay datos
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = "No se pudo guardar las liquidaciones"}
                End If

                If (From x In resultStore Where x.CodeResult = 888 Select x).Count > 0 Then 'Se valida si hay algun error de catch en el sql
                    Transaction.Dispose()
                    Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = (From x In resultStore Where x.CodeResult = 888 Select x.MessageResult).FirstOrDefault()}
                End If

                'Listado de errores y de oks
                Dim ListReturn As New List(Of Tuple(Of String, Integer))

                'Se recorre la info para armar el listado de ok o error
                resultStore.ForEach(Sub(x) ListReturn.Add(New Tuple(Of String, Integer)(x.MessageResult, IIf(x.CodeResult = 0, 1, 2))))

                Transaction.Complete()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = True, .ObjectEmbbeded = ListReturn}
            Catch ex As OptimisticConcurrencyException
                Transaction.Dispose()
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.Message}
            Catch ex As Exception
                Transaction.Dispose()
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                Return New ActionResult(Of List(Of Tuple(Of String, Integer))) With {.StateResult = False, .Message = ex.Message}
            End Try
        End Using
    End Function

    ''' <summary>
    ''' Metodo que me convierte el listado de datos en xml cuando es para guardar
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertToXmlBySave(ListInfo As List(Of SP_ImportFileInitialBalancePayroll_Result)) As String
        'String de xml que se arma con el listado
        Dim builder As StringBuilder = New StringBuilder()

        'Cantidad de item para validar 
        Dim count As Integer = 0

        builder.Append("<Data>")

        For Each info In ListInfo

            builder.Append("<Row>")

            builder.Append("<Nit>" & info.Nit & "</Nit>")
            builder.Append("<EmployeeId>" & info.EmployeeId & "</EmployeeId>")
            builder.Append("<PayrollDate>" & info.PayrollDate & "</PayrollDate>")
            builder.Append("<DaysWorked>" & info.DaysWorked & "</DaysWorked>")
            builder.Append("<TotalAccrued>" & info.TotalAccrued & "</TotalAccrued>")
            builder.Append("<TotalDeducted>" & info.TotalDeducted & "</TotalDeducted>")
            builder.Append("<PensionJCB>" & info.PensionJCB & "</PensionJCB>")
            builder.Append("<PensionContributionValue>" & info.PensionContributionValue & "</PensionContributionValue>")
            builder.Append("<ConceptIdPensionContributionValue>" & info.ConceptIdPensionContributionValue & "</ConceptIdPensionContributionValue>")
            builder.Append("<EmployerPensionContributionValue>" & info.EmployerPensionContributionValue & "</EmployerPensionContributionValue>")
            builder.Append("<ConceptIdEmployerPensionContributionValue>" & info.ConceptIdEmployerPensionContributionValue & "</ConceptIdEmployerPensionContributionValue>")
            builder.Append("<HealthJCB>" & info.HealthJCB & "</HealthJCB>")
            builder.Append("<EmployeeHealthContributionValue>" & info.EmployeeHealthContributionValue & "</EmployeeHealthContributionValue>")
            builder.Append("<ConceptIdEmployeeHealthContributionValue>" & info.ConceptIdEmployeeHealthContributionValue & "</ConceptIdEmployeeHealthContributionValue>")
            builder.Append("<EmployerHealthContributionValue>" & info.EmployerHealthContributionValue & "</EmployerHealthContributionValue>")
            builder.Append("<ConceptIdEmployerHealthContributionValue>" & info.ConceptIdEmployerHealthContributionValue & "</ConceptIdEmployerHealthContributionValue>")
            builder.Append("<IBCIncentivePayment>" & info.IBCIncentivePayment & "</IBCIncentivePayment>")
            builder.Append("<ProvisionIncentive>" & info.ProvisionIncentive & "</ProvisionIncentive>")
            builder.Append("<ConceptIdProvisionIncentive>" & info.ConceptIdProvisionIncentive & "</ConceptIdProvisionIncentive>")
            builder.Append("<IBCVacation>" & info.IBCVacation & "</IBCVacation>")
            builder.Append("<ProvisionVacation>" & info.ProvisionVacation & "</ProvisionVacation>")
            builder.Append("<ConceptIdProvisionVacation>" & info.ConceptIdProvisionVacation & "</ConceptIdProvisionVacation>")
            builder.Append("<IBCUnemployment>" & info.IBCUnemployment & "</IBCUnemployment>")
            builder.Append("<UnemploymentAccumulated>" & info.UnemploymentAccumulated & "</UnemploymentAccumulated>")
            builder.Append("<ConceptIdUnemploymentAccumulated>" & info.ConceptIdUnemploymentAccumulated & "</ConceptIdUnemploymentAccumulated>")
            builder.Append("<ProvisionInterestsUnemployment>" & info.ProvisionInterestsUnemployment & "</ProvisionInterestsUnemployment>")
            builder.Append("<ConceptIdProvisionInterestsUnemployment>" & info.ConceptIdProvisionInterestsUnemployment & "</ConceptIdProvisionInterestsUnemployment>")
            builder.Append("<AmbulatoryDisabilityValue>" & info.AmbulatoryDisabilityValue & "</AmbulatoryDisabilityValue>")
            builder.Append("<ConceptIdAmbulatoryDisabilityValue>" & info.ConceptIdAmbulatoryDisabilityValue & "</ConceptIdAmbulatoryDisabilityValue>")
            builder.Append("<DisabilityHospitalValue>" & info.DisabilityHospitalValue & "</DisabilityHospitalValue>")
            builder.Append("<ConceptIdDisabilityHospitalValue>" & info.ConceptIdDisabilityHospitalValue & "</ConceptIdDisabilityHospitalValue>")
            builder.Append("<MaternityLeaveValue>" & info.MaternityLeaveValue & "</MaternityLeaveValue>")
            builder.Append("<ConceptIdMaternityLeaveValue>" & info.ConceptIdMaternityLeaveValue & "</ConceptIdMaternityLeaveValue>")
            builder.Append("<IBCSENA>" & info.IBCSENA & "</IBCSENA>")
            builder.Append("<SenaContributionValue>" & info.SenaContributionValue & "</SenaContributionValue>")
            builder.Append("<ConceptIdSenaContributionValue>" & info.ConceptIdSenaContributionValue & "</ConceptIdSenaContributionValue>")
            builder.Append("<IBCICBF>" & info.IBCICBF & "</IBCICBF>")
            builder.Append("<ICBFContributionValue>" & info.ICBFContributionValue & "</ICBFContributionValue>")
            builder.Append("<ConceptIdICBFContributionValue>" & info.ConceptIdICBFContributionValue & "</ConceptIdICBFContributionValue>")
            builder.Append("<IBCCompensationFund>" & info.IBCCompensationFund & "</IBCCompensationFund>")
            builder.Append("<FamilyCompensationFundContributionValue>" & info.FamilyCompensationFundContributionValue & "</FamilyCompensationFundContributionValue>")
            builder.Append("<ConceptIdFamilyCompensationFundContributionValue>" & info.ConceptIdFamilyCompensationFundContributionValue & "</ConceptIdFamilyCompensationFundContributionValue>")
            builder.Append("<RealBaseRetention>" & info.RealBaseRetention & "</RealBaseRetention>")
            builder.Append("<CalculatedWithholdingValue>" & info.CalculatedWithholdingValue & "</CalculatedWithholdingValue>")
            builder.Append("<ConceptIdCalculatedWithholdingValue>" & info.ConceptIdCalculatedWithholdingValue & "</ConceptIdCalculatedWithholdingValue>")
            builder.Append("<RecargoNocturno>" & info.RecargoNocturno & "</RecargoNocturno>")
            builder.Append("<ConceptIdRecargoNocturno>" & info.ConceptIdRecargoNocturno & "</ConceptIdRecargoNocturno>")
            builder.Append("<TotalPaid>" & info.TotalPaid & "</TotalPaid>")
            builder.Append("<SanctionDays>" & info.SanctionDays & "</SanctionDays>")
            builder.Append("<Overtime>" & info.Overtime & "</Overtime>")
            builder.Append("<ConceptIdOvertime>" & info.ConceptIdOvertime & "</ConceptIdOvertime>")
            'Nuevos Campos
            builder.Append("<RecargoNocturnoFestivo>" & info.RecargoNocturnoFestivo & "</RecargoNocturnoFestivo>")
            builder.Append("<ConceptIdRecargoNocturnoFestivo>" & info.ConceptIdRecargoNocturnoFestivo & "</ConceptIdRecargoNocturnoFestivo>")
            builder.Append("<ValorDominicalOrdinario>" & info.ValorDominicalOrdinario & "</ValorDominicalOrdinario>")
            builder.Append("<ConceptIdValorDominical>" & info.ConceptIdValorDominical & "</ConceptIdValorDominical>")

            builder.Append("</Row>")
        Next

        builder.Append("</Data>")
        Return builder.ToString
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _initialBalancePayrollRepository = Nothing
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
