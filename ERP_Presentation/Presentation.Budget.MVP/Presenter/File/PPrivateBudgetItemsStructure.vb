'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Domain.Entities

#End Region

Public Class PPrivateBudgetItemsStructure

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IPrivateBudgetItemsStructure


    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IPrivateBudgetItemsStructure)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"
    Public Async Sub GetSequense()
        Using model As New ModelBaseBudget(View.MyTag)
            View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListPrivateBudgetItemsStructureXpo() As List(Of PrivateBudgetItemsStructureXpo)
        'Dim filtroConsulta As String = "SupplierDistributionLineId = " & SupplierDistributionLineId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.GetCollection(Of PrivateBudgetItemsStructureXpo)(Nothing, Nothing).ToList()
    End Function

    ''' <summary>
    ''' Lista el datasource padre
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeParent(PrivateBudgetItemsStructure As PrivateBudgetItemsStructure)
        If PrivateBudgetItemsStructure Is Nothing OrElse PrivateBudgetItemsStructure.Id = 0 Then
            View.ParentXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListPrivateBudgetItemsStructure()
        Else
            View.ParentXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListPrivateBudgetItemsStructureWithNotId(PrivateBudgetItemsStructure.Id)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa cuenta contable
    ''' </summary>
    Public Sub InitializeMainAccount(BudgetControl As Integer)
        View.MainAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListMainAccountByBudgetControl(True, BudgetControl)
    End Sub

    ''' <summary>
    ''' Inicializa tercero
    ''' </summary>
    Public Sub InitializeThirdParty()
        View.ThirdPartyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

    ''' <summary>
    ''' Inicializa centro costo
    ''' </summary>
    Public Sub InitializeCostCenter()
        View.CostCenterXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

#End Region

End Class
