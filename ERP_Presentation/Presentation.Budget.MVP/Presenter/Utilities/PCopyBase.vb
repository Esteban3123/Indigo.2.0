'***********************************************************************
' Assembly         : Presentacion.Budget.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/09/2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.Data.Xpo

#End Region

Public Class PCopyBase

#Region "Fields"

    Dim _view As ICopyBase

    Dim indigo As SessionValues

#End Region

#Region "Builders"

    Sub New(ByRef view As ICopyBase)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            _view = view
            indigo = SessionValues.Instance
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource de las entidades presupuestales
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeBudgetEntity() As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
    End Function

    ''' <summary>
    ''' Inicializa el datasource de las vigencias
    ''' </summary>
    ''' <param name="budgetEntityId"></param>
    ''' <remarks></remarks>
    Public Function InitializeValidity(budgetEntityId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(budgetEntityId)
    End Function

#End Region

End Class
