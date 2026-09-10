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

Public Class PProductionCenter

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IProductionCenter

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IProductionCenter)
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

    Public Sub LoadStructure()
        Using Model As New MBusqueda
            Dim _structure As XPCollection = Model.ConsultarEntidades(eDataSource.ListOrganizationalStructureData)
            If _structure IsNot Nothing Then
                Me.View.OrganizationalStructureDatasourse = New List(Of Domain.Entities.OrganizationalStructureOfCosts)()
                For Each itemXpo As Infrastructure.Data.Xpo.InteropCostRepository.OrganizationalStructureOfCostsXpo In _structure
                    Dim _item As New OrganizationalStructureOfCosts()
                    With _item
                        .Id = itemXpo.Id
                        .Code = itemXpo.Code
                        .Name = itemXpo.Name
                        If itemXpo.ParentId IsNot Nothing Then
                            .ParentId = itemXpo.ParentId.Id
                        End If
                        .Status = itemXpo.Status
                        .MarkAsModified()
                    End With
                    Me.View.OrganizationalStructureDatasourse.Add(_item)
                Next
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the cost center dinamic.
    ''' </summary>
    Public Sub InitializeCostCenterDinamic()
        Using Model As New MBusqueda
            Me.View.CostCenterDatasource = Model.ConsultarEntidades(eDataSource.ListCostCenterDinamic)
        End Using
    End Sub

    Public Sub ListMainAccount()
        Me.View.CancellationCostMainAccountXpo = XpoServiceEx.Instance(Indigo.InteropCostContainer).InteropCostService.ListMainAccountErpByNivel({5}.ToList)
    End Sub

End Class