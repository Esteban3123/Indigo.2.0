'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 09-09-2014
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
Imports Domain.Entities
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PDispersionFunds
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IDispersionFunds

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IDispersionFunds)
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
    ''' Obtiene los parametros de pagos por unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer)
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Me.View.PaymentsSettingPaymentsXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Sub

    ''' <summary>
    ''' Obtiene una secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury("636")
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the entity bank account.
    ''' </summary>
    Public Sub InitializeEntityBankAccount()
        Using Model As New MBusqueda
            Dim filter() As Object = {Indigo.UserIndigo, True}
            Me.View.EntityBankAccountDatasource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListEntityBankAccountByUser, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the schedule payment datasource with code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    Public Async Function InitializeSchedulePaymentDatasourceWithCode(code As String) As Task
        Using Model As New MSchedulePayment(Me.View.MyTag)
            Dim _resultSchedule = Await Model.GetSchedulePaymentDatasourceWithPayment(code, True)
            If Not _resultSchedule.StateResult AndAlso Me.View.Status = 2 Then
                Me.View.Mensaje(EeventViewerImages.Advertencia) = _resultSchedule.Message
            End If
            Me.View.SchedulePaymentDatasource = _resultSchedule.ObjectEmbbeded
            Me.View.OriginalDatasource = _resultSchedule.ObjectEmbbeded
        End Using
    End Function

    ''' <summary>
    ''' Initializes the cost center.
    ''' </summary>
    Sub InitializeCostCenter()
        'Using Model As New MBusqueda
        Me.View.CostCenterDatasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

End Class