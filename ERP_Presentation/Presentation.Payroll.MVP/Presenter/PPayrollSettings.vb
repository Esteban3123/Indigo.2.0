'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Daniel Eduardo Arévalo
' Created          : 02-11-2018
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PPayrollSettings

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPayrollSettings

    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Private _indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IPayrollSettings)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los controles del formulario
    ''' </summary>
    Public Async Function ConceptsDatasource() As Task
        Using model As New MConcept(MConcept.TAG)
            Dim ConceptDatasource = Await model.GetConceptByConceptClassAsync(New List(Of String)({"042", "043", "051", "052"}))
            Me.View.Concept_Datasource = ConceptDatasource

            Dim ConceptVacationDatasource = Await model.GetConceptByConceptClassAsync(New List(Of String)({"030"}))
            Me.View.ConceptVacation_Datasource = ConceptVacationDatasource
        End Using
    End Function

    Public Function LoadConceptAdjustment(ConceptClass As String) As List(Of PayrollConceptXpo)
        Using Model As New MGroups(MGroups.TAG)
            Dim filtroConsulta As String = "ConceptClass = '" & ConceptClass & "' AND ConceptType = 2"
            Return XpoServiceEx.Instance(_indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollConceptXpo)(Nothing, filtroConsulta)
        End Using
    End Function

    Public Sub InitializeEntityBankAccount()
        Me.View.EntityAccountDatasource = XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListEntityBankAccountByUser(_indigo.UserIndigo, True)
    End Sub

    Public Sub InitializeEntityBankAccountVacation()
        Me.View.EntityAccountDatasourceVacation = XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListEntityBankAccountByUser(_indigo.UserIndigo, True)
    End Sub

    Public Sub InitializeEntityBankAccountLiquidation()
        Me.View.EntityAccountDatasourceLiquidation = XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListEntityBankAccountByUser(_indigo.UserIndigo, True)
    End Sub

    ''' <summary>
    ''' Lista todas las cajas
    ''' </summary>
    Public Sub InitializeCash(ByVal type As Short)
        Using Model As New MBusqueda
            Dim filter() As Object = {_indigo.UserIndigoId, type, True}
            Me.View.CashDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    Public Sub InitializeCashVacation(ByVal type As Short)
        Using Model As New MBusqueda
            Dim filter() As Object = {_indigo.UserIndigoId, type, True}
            Me.View.CashDatasourceVacation = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    Public Sub InitializeCashLiquidation(ByVal type As Short)
        Using Model As New MBusqueda
            Dim filter() As Object = {_indigo.UserIndigoId, type, True}
            Me.View.CashDatasourceLiquidation = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    Public Sub InitializeExpenseConceptVacation()
        Me.View.ExpenseConceptDatasourceVacation = XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListExpenseConceptsByBehavior(6)
    End Sub

    Public Sub InitializeExpenseConceptLiquidation()
        Me.View.ExpenseConceptDatasourceLiquidation = XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListExpenseConceptsByBehavior(6)
    End Sub

    Public Sub InitializeNoteConceptsDatasource()
        Me.View.NoteConceptsDatasource = XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListAllNoteConcept()
    End Sub

    Public Sub InitializePortfolioNoteConceptDatasource()
        Me.View.PortfolioNoteConceptDatasource = XpoServiceEx.Instance(_indigo.TransactionalContainer).PortfolioService.GetAllPortfolioNoteConceptGeneral(1)
    End Sub

    Public Sub InitializeCashFlowConceptDatasource()
        Me.View.CashFlowConceptDatasource = XpoServiceEx.Instance(_indigo.TransactionalContainer).TreasuryService.ListCashFlowConcept()
    End Sub

    Public Function GetListCurrency()
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).CommonService.GetCurrency()
    End Function

    Public Function GetOfficialCurrencyId()
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing).OfficialCurrencyId
    End Function

	''' <summary>
	''' Obtiene los datos de la moneda oficial
	''' </summary>
	''' <returns></returns>
	Public Function LoadPayrollSettings() As PayrollSettingsXpo
		Return XpoServiceEx.Instance(_indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
	End Function
End Class
