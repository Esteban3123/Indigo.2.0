'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Juan Carlos Bermudez
' Created          : 19/09/2015
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

#End Region

Public Class PSuspensionCancellation

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ISuspensionCancellation


    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ISuspensionCancellation)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
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
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetEntity()
        View.BudgetEntitiesXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeValidity(budgetEntityId As Integer)
        View.BudgetaryValidityXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityId)
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBudgetEntityPopUp()
        View.BudgetEntitiesPopUpXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeValidityPopUp(budgetEntityId As Integer)
        View.BudgetaryValidityPopUpXpo = XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityId)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las suspencions
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Sub InitializeSuspension(budgetEntityId As Integer)
        Dim filter() As Object = {budgetEntityId, 2}
        Using model As New MBusqueda
            Me.View.SuspensionXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuspensionByValidityIdAndStatus, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Carga los detalles de la suspencion por id de la suspencion
    ''' </summary>
    ''' <param name="SuspensionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadSuspensionDetailByCommitmentId(SuspensionId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceex.Instance(Indigo.TransactionalContainer).BudgetService.ListSuspensionDetailBySuspensionId(SuspensionId)
    End Function

#End Region

End Class
