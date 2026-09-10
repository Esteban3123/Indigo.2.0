Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 20-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Class PEquipmentCatalog

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IEquipmentCatalog

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IEquipmentCatalog)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub InitializeIVAAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.IVAAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeWithholdingTaxAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingTaxAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeWithholdingICAAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingICAAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeWithholdingTaxConcept()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingTaxConceptXpo = ModelXpo.ConsultarEntidades(eDataSource.ListRetentionConcept)
        End Using
    End Sub

    Public Sub InitializeWithholdingICAConcept()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingICAConceptXpo = ModelXpo.ConsultarEntidades(eDataSource.ListRetentionConcept)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryEntity()
        View.BudgetaryEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetaryValidity(budgetEntityId As Integer)
        View.BudgetaryValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetByBudgetEntityIdAndStatus(budgetEntityId, 2)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto de las facturas dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeBudget(ValidityId As Integer)
        View.BudgetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityIdAndTypeAndStatus(ValidityId, 2, 2, False)
    End Sub

#End Region

End Class
