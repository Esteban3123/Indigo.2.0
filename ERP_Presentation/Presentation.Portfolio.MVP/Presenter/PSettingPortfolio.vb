'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 10-04-2014
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

#End Region

Public Class PSettingPortfolio
#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim View As ISettingPortfolio

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As ISettingPortfolio)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me.View = view
        End If
    End Sub
#End Region

#Region "Methods"
    Public Sub InitializeJournalVoucherTypeCreditNotesXPO()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeCreditNotesXPO = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    Public Sub InitializeJournalVoucherTypeDebitNotesXPO()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeDebitNotesXPO = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    Public Sub InitializeJournalVoucherTypeTranslationXPO()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeTranslationXPO = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    Public Sub InitializeJournalVoucherTypeProvisionXPO()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeProvisionXPO = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    Public Sub InitializeJournalVoucherTypeDeteriorationAccountXpo()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeDeteriorationAccountIdXpo = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    Public Sub InitializeJournalVoucherTypeFilingAccountXPO()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeFilingAccountXPO = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

    Public Sub InitializeJournalVoucherTypeDocumentAccountReceivableXPO()
        Using Model As New MBusqueda
            Dim state() As Object = {True}
            Me.View.JournalVoucherTypeDocumentAccountReceivableXpo = Model.ConsultarEntidades(eDataSource.ListDocumentTypes, state)
        End Using
    End Sub

#Region "Budget Interface"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        View.BudgetaryEntityXpo = XpoServiceEx.Instance(_indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        View.BudgetaryValidityXpo = XpoServiceEx.Instance(_indigo.TransactionalContainer).BudgetService.ListValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeDependency(ValidityId As Integer)
        View.DependencyXpo = XpoServiceEx.Instance(_indigo.TransactionalContainer).BudgetService.GetDependency(ValidityId)
    End Sub

#End Region

#End Region

End Class
