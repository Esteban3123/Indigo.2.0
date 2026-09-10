'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2016
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
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Presentador del frontal Conceptos de Notas
''' </summary>
Public Class PCostGeneralExpenseCategory

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostGeneralExpenseCategory

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostGeneralExpenseCategory)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Function ListCostOrganizationalStructureData() As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCategoryData()
    End Function

    Public Sub InitializeCostGeneralExpenseCategory()
        View.StructXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostGeneralExpenseCategoryByStatus(True)
    End Sub

    Public Function ListCostGeneralExpenseCategoryDatasourceTreeList() As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostGeneralExpenseCategoryDatasourceTreeList()
    End Function

    Public Function ListCostGeneralExpenseCategory() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.ListCostGeneralExpenseCategory()
    End Function

    Public Function InitializeCostGeneralExpenseCategoryWithOut(code As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CostService.InitializeCostGeneralExpenseCategoryWithOut(code)
    End Function

End Class
