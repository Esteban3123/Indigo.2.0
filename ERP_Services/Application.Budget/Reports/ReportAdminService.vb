#Region "Imports"

Imports System.Data.SqlClient
Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class ReportAdminService
    Implements IReportAdminService

    Private _BudgetRepository As IBudgetRepository

#Region "Builder"

#Region "Income"

    Private Function GenerateArchiveReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder Implements IReportAdminService.GenerateArchiveReportIncomeProgramming

        Try
            Dim result As New StringBuilder()
            Dim ds = GetReportIncomeProgramming(criterias, Session)
            Dim CodigoCHIP = _BudgetRepository.GetThirdPartyByNit(Session.IndigoCompanyNit)
            Dim PeriodId As String = criterias("Month").ToString()
            If PeriodId < 10 Then
                PeriodId = "0" + PeriodId
            End If
            Dim Period As String = String.Format("1{0}{0}", PeriodId)
            Dim lineHead As String = ""
            Dim lineDet As String = ""

            lineHead &= String.Format("{0}{1}{2}{3}{4}{5}{6}",
                "S".Insert(1, vbTab),
                CodigoCHIP.Insert(CodigoCHIP.Length, vbTab),
                Period.Insert(Period.Length, vbTab),
                criterias("Year").Insert(criterias("Year").ToString().Length, vbTab),
                "A_PROGRAMACION_INGRESOS", vbTab, vbTab)
            result.AppendLine(lineHead)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                For Each detail In ds.Tables(0).Rows

                    Dim Code As String = detail("Code")
                    Dim InitialValue As String = detail("InitialValue")
                    Dim Balance As String = detail("Balance")

                    lineDet = String.Format("{0}{1}{2}{3}",
                    "D".Insert(1, vbTab),
                    Code.Insert(detail("Code").Length, vbTab),
                    InitialValue.ToString().Insert(detail("InitialValue").ToString().Length, vbTab),
                    Balance.ToString())
                    result.AppendLine(lineDet)
                Next
            End If

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try

    End Function

#End Region

#Region "Expense"

    Public Sub New(ByVal BudgetRepository As IBudgetRepository)
        If BudgetRepository Is Nothing Then
            Throw New ArgumentNullException("budgetTransferRepository Vacio")
        End If
        _BudgetRepository = BudgetRepository
    End Sub

    Public Function GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder Implements IReportAdminService.GetStringBuilderBudgetExecutionOfExpensesForTerritorialPublicEntities
        Try
            Dim ds As New DataSet
            ds = GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias, Session)
            Dim CodigoCHIP = _BudgetRepository.GetThirdPartyByNit(Session.IndigoCompanyNit)
            ''Variable que retorna los datos para el archivo plano
            Dim dataStringBuilder As New StringBuilder

            If ds.Tables.Count > 0 Then
                'Se crea la cabecera del archivo plano  
                Dim dataRow As String = ""
                Dim lineRow As String = ""
                Dim periodo As String = String.Format("1{0}{0}", criterias("Month").AsString.PadLeft(2, "0"))
                lineRow = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                                        ("S").Insert(("S").Length, vbTab),
                                        CodigoCHIP.Insert(CodigoCHIP.Length, vbTab),
                                        periodo.Insert(periodo.Length, vbTab),
                                        criterias("Year").Insert(criterias("Year").Length, vbTab),
                                        "E_EJECUCION_DE_GASTOS",
                                        vbTab,
                                        vbTab,
                                        vbTab,
                                        vbTab,
                                        vbTab,
                                        vbTab,
                                        vbTab,
                                        vbTab)
                dataStringBuilder.Append(lineRow)
                'Se agregan los detalles del archivo plano 
                For Each rows As DataRow In ds.Tables(0).Rows
                    dataStringBuilder.AppendLine()
                    Dim Code As String = rows("Code").ToString
                    Dim ValidityType As String = rows("ValidityType").ToString
                    Dim BudgetSection As String = rows("BudgetSection").ToString
                    Dim Sector As String = rows("Sector").ToString
                    Dim CPCCode As String = rows("CPCCode").ToString
                    Dim FinancialSourceCode As String = rows("FinancialSourceCode").ToString
                    Dim FundSituation As String = rows("FundSituation").ToString
                    Dim PublicPolicyCode As String = rows("PublicPolicyCode").ToString
                    Dim NitThirdParty As String = rows("NitThirdParty").ToString
                    Dim CommitmentValue As String = rows("CommitmentValue").ToString
                    Dim ObligationValue As String = rows("ObligationValue").ToString
                    Dim PaymentValue As String = rows("PaymentValue").ToString

                    lineRow = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                                        ("D").Insert(("D").Length, vbTab),
                                        Code.Insert(Code.Length, vbTab),
                                        ValidityType.Insert(ValidityType.Length, vbTab),
                                        BudgetSection.Insert(BudgetSection.Length, vbTab),
                                        Sector.Insert(Sector.Length, vbTab),
                                        CPCCode.Insert(CPCCode.Length, vbTab),
                                        FinancialSourceCode.Insert(FinancialSourceCode.Length, vbTab),
                                        FundSituation.Insert(FundSituation.Length, vbTab),
                                        PublicPolicyCode.Insert(PublicPolicyCode.Length, vbTab),
                                        NitThirdParty.Insert(NitThirdParty.Length, vbTab),
                                        CommitmentValue.Insert(CommitmentValue.Length, vbTab),
                                        ObligationValue.Insert(ObligationValue.Length, vbTab),
                                        PaymentValue)
                    dataStringBuilder.Append(lineRow)
                Next
            End If
            Return dataStringBuilder
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function


    Private Function GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As StringBuilder Implements IReportAdminService.GenerateArchiveReportExpendituresProgrammingForTerritorialPublicEstablisments

        Try
            Dim result As New StringBuilder()
            Dim ds = GetExpendituresProgrammingForTerritorialPublicEstablisments(criterias, Session)
            Dim CodigoCHIP = _BudgetRepository.GetThirdPartyByNit(Session.IndigoCompanyNit)
            Dim PeriodId As String = criterias("Month").ToString()
            If PeriodId < 10 Then
                PeriodId = "0" + PeriodId
            End If
            Dim Period As String = String.Format("1{0}{0}", PeriodId)
            Dim lineHead As String = ""
            Dim lineDet As String = ""

            lineHead &= String.Format("{0}{1}{2}{3}{4}{5}{6}",
                "S".Insert(1, vbTab),
                CodigoCHIP.Insert(CodigoCHIP.Length, vbTab),
                Period.Insert(Period.Length, vbTab),
                criterias("Year").Insert(criterias("Year").ToString().Length, vbTab),
                "D_EJECUCION_DE_GASTOS", vbTab, vbTab)
            result.AppendLine(lineHead)

            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                For Each detail In ds.Tables(0).Rows

                    Dim Code As String = detail("Code")
                    Dim ValidityType As String = detail("ValidityType")
                    Dim BudgetSection = detail("BudgetSection")
                    Dim Sector As String = detail("Sector")
                    Dim InitialValue As String = detail("InitialValue")
                    Dim Balance As String = detail("Balance")

                    lineDet = String.Format("{0}{1}{2}{3}{4}{5}{6}",
                    "D".Insert(1, vbTab),
                    Code.Insert(detail("Code").Length, vbTab),
                    ValidityType.ToString().Insert(detail("ValidityType").ToString().Length, vbTab),
                    BudgetSection.Insert(detail("BudgetSection").Length, vbTab),
                    Sector.Insert(detail("Sector").Length, vbTab),
                    InitialValue.ToString().Insert(detail("InitialValue").ToString().Length, vbTab),
                    Balance.ToString())
                    result.AppendLine(lineDet)
                Next
            End If

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try

    End Function
#End Region
#End Region
#Region "Methods"

#Region "Income"

    Public Function GetListReportBudgetExecutionIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportBudgetExecutionIncome
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportBudgetExcutionIncome] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportBudgetExcutionIncome")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportListDocumentIncome(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportListDocumentIncome
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportListDocumentIncome] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportListDocumentIncome")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportMonthlyIncomeExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportMonthlyIncomeExecution
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportMonthlyIncomeExecution] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportMonthlyIncomeExecution")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportIncomeRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportIncomeRecordBook
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportIncomeRecordBook] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportIncomeRecordBook")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportIncomeProgramming(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportIncomeProgramming
        Try
            Dim ds As New DataSet
            Dim query As String = String.Format("EXEC [Budget].[SP_ReportIncomeSchedule_Co] {0},{1}", criterias("ValidityId"), criterias("Month"))
            Dim dt = Me.GetDatatable(query, Session, "ReportIncomeProgramming")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Expense"

    Public Function GetListReportBudgetExecutionExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportBudgetExecutionExpense
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportBudgetExcutionExpense] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportBudgetExcutionExpense")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetExpendituresProgrammingForTerritorialPublicEstablisments(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetExpendituresProgrammingForTerritorialPublicEstablisments
        Try
            Dim ds As New DataSet
            Dim query As String = String.Format("EXEC [Budget].[SP_ReportExpenseSchedule_Co] {0},{1}", criterias("ValidityId"), criterias("Month"))
            Dim dt = Me.GetDatatable(query, Session, "ReportExpendituresProgrammingForTerritorialPublicEstablisments")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportBudgetExecutionByCategoryThird(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportBudgetExecutionByCategoryThird
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportBudgetExecutionByCategoryThird] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportBudgetExecutionByCategoryThird")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportListDocumentExpense(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportListDocumentExpense
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportListDocumentExpense] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportListDocumentExpense")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportMonthlyExpenseExecution(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportMonthlyExpenseExecution
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportMonthlyExpenseExecution] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportMonthlyExpenseExecution")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetReportExpenseRecordBook(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportExpenseRecordBook
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Budget].[SP_ReportExpenseRecordBook] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportExpenseRecordBook")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    Public Function GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetListReportBudgetExecutionOfExpensesForTerritorialPublicEntities
        Try
            Dim ds As New DataSet
            Dim query As String = String.Format("EXEC [Budget].[SP_ReportExpenseExecution_Co] {0}, {1}", criterias("ValidityId"), criterias("Month"))
            Dim dt = Me.GetDatatable(query, Session, "ReportBudgetExcutionOfExpensesForTerritorialPublicEntities")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

#End Region

#End Region

#Region "Methods Privates"

    Private Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
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
