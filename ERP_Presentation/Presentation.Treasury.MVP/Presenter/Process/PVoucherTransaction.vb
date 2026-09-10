'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 29-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Accounting.MVP
Imports Presentation.Common.MVP
Imports Domain.Entities.Service
Imports Domain.Entities
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PVoucherTransaction
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IVoucherTransaction

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IVoucherTransaction)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Carga la fecha del servidor
    ''' </summary>
    Public Async Sub LoadDateServer(ByVal DateServer As DateTime)
        Using Model As New MDocumentAccount(Me.View.MyTag)
            If Await Model.ValidatePeriod(DateServer.Month, DateServer.Year) Then
                View.DateServer = DateServer
            Else
                View.DateServer = Nothing
                View.Mensaje(EeventViewerImages.Advertencia) = "El periodo actual no se encuentra abierto en contabilidad"
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Loads the transactional date.
    ''' </summary>
    Public Sub LoadTransactionalDate(ByVal idUnitOperative As Integer)
        Using Model As New MHoliday
            Dim holidays As List(Of Domain.Entities.Holiday) = Model.ListHolidayBetweenDate(Me.View.ConfirmationDate.Value.AddDays(1), Me.View.ConfirmationDate.Value.AddDays(1).AddMonths(1))
            Using ModelSettingTreasury As New MSettingsTreasury(View.MyTag)
                Dim setting As SettingsTreasury = (ModelSettingTreasury.GetSettingsTreasuryByIdUnitOperativeSimple(idUnitOperative)).ObjectEmbbeded
                View.TransactionDate = CommonService.GetBusinessDay(Me.View.ConfirmationDate.Value.AddDays(1), holidays, setting.SaturdaySkillful, setting.SundaySkillful)
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Lista todos los terceros y lo asigna al datasource de terceros
    ''' </summary>
    Public Sub InitializeThirdParty()
        Using Model As New MBusqueda
            Me.View.ThirdPartyDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' Lista las autorizaciones de resolución (documento soporte)
    ''' </summary>
    Public Sub GetAuthorizationResolution()
        Me.View.listAuthorizationResolution = XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListDocumentSupportAuthorizationByUser(Indigo.UserIndigo)
    End Sub

    ''' <summary>
    ''' Lista todos los centros de costo
    ''' </summary>
    Public Sub InitializeCostCenter()
        'Using Model As New MBusqueda
        Me.View.CostCenterDatasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Lista todas cuentas bancarias de entidades y lo asigna al datasource de cuentas bancarias
    ''' </summary>
    Public Sub InitializeEntityBankAccount()
        Me.View.EntityAccountDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListEntityBankAccountByUser(Indigo.UserIndigo, True)
    End Sub

    ''' <summary>
    ''' Lista todas las cajas
    ''' </summary>
    Public Sub InitializeCash(ByVal type As Short)
        Using Model As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigoId, type, True}
            Me.View.CashDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashRegisterByUser, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene una secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequence = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene los conceptos de pago
    ''' </summary>
    Sub InitializePaymentConcept()
        Using Model As New MBusqueda
            'Dim result As LinqInstantFeedbackSource =
            Me.View.PaymentConceptDatasource = Model.ConsultarEntidades(eDataSource.ListAllPaymentConceptByState, True)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene los parametros de pagos por unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer)
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Me.View.PaymentsSettingPaymentsXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Sub

    Public Function GetEntityBankAccount(Id As Integer?) As PayrollRepository.TreasuryEntityBankAccountXpo
        If Id Is Nothing Then
            Return Nothing
        End If
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetCollection(Of PayrollRepository.TreasuryEntityBankAccountXpo)(Nothing, $"Id = {Id}").FirstOrDefault()
    End Function
End Class
