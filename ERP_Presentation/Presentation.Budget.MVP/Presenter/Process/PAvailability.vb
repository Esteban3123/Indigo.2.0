'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Jeisson Herrera Peña
' Created          : 04-09-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo

#End Region

Public Class PAvailability

#Region "Variables"

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IAvailability
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Construct"
    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAvailability)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetEntity()
        View.BudgetEntityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetEntityPopup()
        View.BudgetEntityXpoPopup = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeValidity(budgetEntityId As Integer)
        View.ValidityXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityId)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityIdPopup"></param>
    ''' <remarks></remarks>
    Public Sub InitializeValidityPopup(budgetEntityIdPopup As Integer)
        View.ValidityXpoPopup = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityIdPopup)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del treeList de rubro presupuestal
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListCategoryTreeList(budgetaryValidityId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCategoryByBudgetaryValidityIdAndItemTypeForTreeList(budgetaryValidityId, 1)
    End Function

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Async Sub GetSequense()
        Using model As New ModelBaseBudget(View.MyTag)
            View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub LoadListDependency(validity As Integer)
        Using Model As New MAvailability("")
            Me.View.ListDependency() = Model.ListAllDependencyXpo(validity)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ListCPCCatalogActiveWithoutChildren()
        View.ListCPCCatalog = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListCPCCatalogActiveWithoutChildren()
    End Sub

#End Region

End Class
