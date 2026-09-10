'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 03-06-2014
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

Public Class PInitialBudgetEntry

''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IInitialBudgetEntry
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IInitialBudgetEntry)
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
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeValidity(budgetEntityId As Integer)
        View.ValidityXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityId)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del treeList de rubro presupuestal
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListCategoryTreeList(budgetaryValidityId As Integer, type As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListCategoryByBudgetaryValidityIdAndItemTypeForTreeList(budgetaryValidityId, type)
    End Function

    ''' <summary>
    ''' Inicializa el datasource del listado de los tipos de Gasto
    ''' </summary>
    ''' <param name="budgetValidityId"></param>
    ''' <remarks></remarks>
    Public Function ListRevenueTypeByBudgetValidityIdAndType(budgetValidityId As Integer, type As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListRevenueTypeByBudgetValidityIdAndType(budgetValidityId, type)
    End Function

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
#End Region

End Class
