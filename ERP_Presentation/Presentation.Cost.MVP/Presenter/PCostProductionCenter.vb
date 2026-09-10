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

Public Class PCostProductionCenter

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICostProductionCenter

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICostProductionCenter)
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
        Using Model As New MCommonCost(View.MyTag)
            Me.View.Sequence = Await Model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the organizational structure.
    ''' </summary>
    Public Sub InitializeOrganizationalStructure()
        Using Model As New MCostOrganizationalStructure(View.MyTag)
            Me.View.OrganizationalStructureDatasourse = Model.ListCostOrganizationalStructure()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the organizational structure.
    ''' </summary>
    Public Sub InitializeCategory()
        Using Model As New MCostProductionCenterCategory(View.MyTag)
            Me.View.OrganizationalStructureDatasourse = Model.ListCategories()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the cost center dinamic.
    ''' </summary>
    Public Sub InitializeCostCenterDinamic()
        Using model As New MCostProductionCenter(View.MyTag)
            Me.View.CostCenterDatasource = model.ListCostCenter()
        End Using
    End Sub

    Public Sub ListMainAccount()
        Me.View.CancellationCostMainAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True)
    End Sub

End Class