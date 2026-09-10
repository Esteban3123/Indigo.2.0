'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-03-2014
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

#End Region

''' <summary>
''' Presentador del frontal Parametros
''' </summary>
Public Class PParameters

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ISettingsTreasury

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ISettingsTreasury)
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
    ''' Initializes the journar voucher type cash receipt.
    ''' </summary>
    Public Sub InitializeJournarVoucherTypeCashReceipt()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.DatasourceCashReceipt = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the journar voucher type voucher transaction.
    ''' </summary>
    Public Sub InitializeJournarVoucherTypeVoucherTransaction()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.DatasourceVoucherTransaction = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the journar voucher type voucher transaction crossing.
    ''' </summary>
    Public Sub InitializeJournarVoucherTypeVoucherTransactionCrossing()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.DatasourceVoucherTransactionCrossing = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the journar voucher bank appropriation.
    ''' </summary>
    Public Sub InitializeJournarVoucherBankAppropriation()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.DatasourceBankAppropriation = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    ''' <summary>
    ''' Carga el datasource del fondo de caja menor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeConstitutionCash()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeConstitutionCashXpo = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the journar voucher treasury note.
    ''' </summary>
    Public Sub InitializeJournarVoucherTreasuryNote()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.DatasourceTreasuryNote = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the main account payment.
    ''' </summary>
    Public Sub InitializeMainAccountPayment()
        Using Model As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.DatasourceMainAccountPayment = Model.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the main account expenses.
    ''' </summary>
    Public Sub InitializeMainAccountExpenses()
        Using Model As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.DatasourceMainAccountExpense = Model.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

End Class
