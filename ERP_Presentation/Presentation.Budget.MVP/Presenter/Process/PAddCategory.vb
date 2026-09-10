'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/08/2015
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

Public Class PAddCategory

    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As IAddCategory
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAddCategory)
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
    Public Sub InitializeCategory(ValidityId As Integer, type As Integer)
        View.CategoryXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId(True, ValidityId, type)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los tipos de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeType(ValidityId As Integer, type As Integer)
        View.TypeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListRevenueTypeByBudgetValidityIdAndTypeAndStatus(ValidityId, type, True)
    End Sub

    ''' <summary>
    ''' Carga el presupuesto inicial dependiendo de la vigencia
    ''' seleccionada y el estado confirmado
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LoadBudgetByValidityId(ValidityId As Integer, type As Integer, withBalance As Boolean) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BudgetService.ListBudgetByBudgetValidityId(ValidityId, 2, type, withBalance)
    End Function

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

#End Region

End Class
