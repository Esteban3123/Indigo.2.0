'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/08/2015
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

Public Class PBudgetTransfer

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IBudgetTransfers
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IBudgetTransfers)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetEntity()
        View.BudgetEntityXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetEntityPopup()
        View.BudgetEntityXpoPopup = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeValidity(budgetEntityId As Integer)
        View.ValidityXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityId)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityIdPopup"></param>
    ''' <remarks></remarks>
    Public Sub InitializeValidityPopup(budgetEntityIdPopup As Integer)
        View.ValidityXpoPopup = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityIdPopup)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del treeList de rubro presupuestal
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListCategoryTreeList(budgetaryValidityId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListCategoryByBudgetaryValidityIdAndItemTypeForTreeList(budgetaryValidityId, 1)
    End Function

    ''' <summary>
    ''' Inicializa el datasource del listado de los tipos de ingreso
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <remarks></remarks>
    Public Function ListRevenueTypeByBudgetValidityIdAndType(budgetValidityId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListRevenueTypeByBudgetValidityIdAndType(budgetValidityId, 1)
    End Function

    ''' <summary>
    ''' Lista todos los traslados de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListBudgetTransferByType(budgetValidityId As Integer) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetTransferByType(1, budgetValidityId)
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

#End Region

End Class
