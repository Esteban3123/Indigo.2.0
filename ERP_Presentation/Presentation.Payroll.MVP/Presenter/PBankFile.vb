'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 03-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region

Public Class PBankFile

#Region "Fields"

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IBankFile

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Lista de Grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim GroupList As List(Of Group)

#End Region

#Region "Builder"

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IBankFile)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequensePayroll(View.MyTag)
            Me.View.Sequence = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista las compañias
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCompanyXPO()
        View.CompanyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCompanySession(Indigo.IndigoCompanyNit)
    End Sub

    ''' <summary>
    ''' Lista las fechas liquidadas
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub InitializeLiquidationDateXpo()
        Using modelLiquidation As New MPayrollLiquidation
            View.LiquidationDateXpo = Await modelLiquidation.GetLiquidationDatesConfirmPayroll()
        End Using
    End Sub

    ''' <summary>
    ''' Lista los empleados que se encuentre de acuerdo a los filtros
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListPayrollDateLiquidated(companyId As Integer, liquidationDate As DateTime)
        View.LiquidationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListLiquidationsByCompanyAndPayrollDateLiquidated(companyId, liquidationDate)
    End Sub
    ''' <summary>
    ''' Lista que trae los empleados que ya se les confirme el pago por medio de archivo plano para bancos
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <param name="liquidationDate"></param>
    Public Sub ListPayrollDateLiquidated2(companyId As Integer, liquidationDate As DateTime)
        View.LiquidationXpo2 = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListLiquidationsByCompanyAndPayrollDateLiquidated2(companyId, liquidationDate)
    End Sub

    ''' <summary>
    ''' Lista las cuentas bancarias
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEntityBankAccountXpo(currencyId As Integer)
        View.EntityBankAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListEntityBankAccountByUser(Indigo.UserIndigo, True, currencyId)
    End Sub

    ''' <summary>
    ''' Lista las compañias
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeExpenseConceptXPO()
        View.ExpenseConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListExpenseConceptsByBehavior(6)
    End Sub

    ''' <summary>
    ''' Lista los empleados que se encuentre de acuerdo a los filtros
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListPayrollDateLiquidatedAsync(companyId As Integer, liquidationDate As DateTime) As Task(Of DevExpress.Xpo.XPCollection(Of PayrollRepository.PayrollLiquidationXpo))
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListLiquidationsByCompanyAndPayrollDateLiquidatedAsync(companyId, liquidationDate)
    End Function
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function

#Region "Method IncentivePayment"
    ''' <summary>
    ''' Metodo para inicializar controles en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function initialize() As Task
        'llenar empresas y empleados en controles
        Dim modelBusqueda As Presentation.Controls.MVP.MBusqueda = New Controls.MVP.MBusqueda()
        '       Me._view.datasourceCompany = modelBusqueda.ConsultarEntidades(eDataSource.CompanyPayroll)
        Await LoadYears()
    End Function

    ''' <summary>
    ''' Carga los años en el combo box de periodo, y establece si hay nominas liquidadas para habilitar el frontal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function LoadYears() As Task
        'llenar lista de años para el control de periodo
        Using modelLiquidation As New MPayrollLiquidation
            Dim YearMin As Date
            Dim YearMax As Date
            YearMin = Await modelLiquidation.GetLiquidationMinDateAsync()
            YearMax = Await modelLiquidation.GetLiquidationMaxDateAsync()
            If YearMin.Year = 1 OrElse YearMax.Year = 1 Then
                YearMin = GroupList.FirstOrDefault.NextDateLiquidation
                YearMax = GroupList.FirstOrDefault.NextDateLiquidation
            End If
            If YearMin.Year <> 1 Then
                Me.View.PeriodControl.Properties.Items.Clear()
                For i As Integer = YearMax.Year To YearMin.Year Step -1
                    Me.View.PeriodControl.Properties.Items.Add(i)
                Next
                Me.View.PeriodControl.SelectedIndex = 0
            End If
        End Using
    End Function

    ''' <summary>
    ''' Traemos las liquidaciones de primas para el archivo plano para bancos 
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    Public Sub ListBankFileIncentivePayment(Period As String, DateLiquidated As Date)
        View.BankFileIncentivePaymentXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListBankFileIncentivePayment(Period, DateLiquidated)
    End Sub
    ''' <summary>
    ''' trae el dataSource de las primas ya confirmadas en el archivo plano
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    Public Function ListBankFileIncentivePaymentConfirm(Period As String, DateLiquidated As Date, bankFileId As Integer) As DevExpress.Xpo.XPCollection(Of PayrollRepository.BankFileIncentivePaymentConfirmXpo)
        View.BankFileIncentivePaymentConfirmXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListBankFileIncentivePaymentConfirm(Period, DateLiquidated, bankFileId)
        Return View.BankFileIncentivePaymentConfirmXpo
    End Function

    ''' <summary>
    ''' Funcion para obtner los valores de las primas ya confirmadas y sumarlos en el ctr
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="DateLiquidated"></param>
    ''' <returns></returns>
    Public Function ListBankFileIncentivePaymentConfirm2(Period As String, DateLiquidated As Date) As List(Of PayrollRepository.BankFileIncentivePaymentConfirmXpo)
        Dim filter As String = "Period = " & Period & " And PeriodEndDate = '" & Format(DateLiquidated, "yyyy-MM-dd") & "' And BankFileStatus = 2 And Process = 1"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollRepository.BankFileIncentivePaymentConfirmXpo)(Nothing, filter).ToList()
    End Function

#End Region

#End Region

End Class
