'***********************************************************************
' Assembly         : Presentacion.InteropCost.MVP
' Author           : Diego Andrés Roldán
' Created          : 11-12-2014
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

Public Class PGeneralExpenses

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IGeneralExpenses

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IGeneralExpenses)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using Model As New MCommonInteropCost(View.MyTag)
            Me.View.Sequence = Await Model.GetSequense()
        End Using
    End Sub



    ''' <summary>
    ''' Carga los parámetros de costos
    ''' </summary>
    Public Sub LoadSettingCost()
        Using Model As New MInteropCostSetting(Me.View.MyTag)
            Me.View.SettingsCost = Model.GetInteropCostSetting()
        End Using
    End Sub

    Public Sub InitializeCategory()
        View.GeneralExpenseCategoryXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).InteropCostService.ListGeneralExpenseCategoryByStatusTreeList(True)
    End Sub

End Class