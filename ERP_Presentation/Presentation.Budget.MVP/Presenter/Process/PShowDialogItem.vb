'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 28-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Presentador del frontal
''' </summary>
''' <remarks></remarks>
Public Class PShowDialogItem

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IShowDialogItems
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IShowDialogItems)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFinancialSource(budgetaryValidityId As Integer)
        View.FinancialSourceXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListFinancialSourceByStatus(True, budgetaryValidityId)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del rubro padre
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeParent(budgetaryValidityId As Integer, type As Integer)
        View.ParentXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetCategoryByStatusAndBudgetaryValidityIdAndItemType(True, budgetaryValidityId, type)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de LOS codigo CCPET
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCCPETCode(ItemType As Integer)
        View.CCPETXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetCCPETByStatusAndItemType(True, ItemType)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de LOS codigo CPC
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCPCCode()
        View.CPCCatalogXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetCPCCatalogTSon(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de política pública
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializePublicPolicy()
        View.PublicPolicyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.GetPublicPolicy(True)
    End Sub
End Class
