'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class PAddRuleRestriction

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IAddRuleRestriction

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

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
    Public Sub New(ByRef iview As IAddRuleRestriction)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource de la rejilla de tipos de regla
    ''' dependiendo del tipo que escojan
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeDataSourceGridControlsRulesType(type As Integer) As DevExpress.Xpo.XPCollection
        Select Case type
            Case Utils.EItemsRestrictionRuleType.CUPS 'CUPS
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatusXpCollection(True)
            Case Utils.EItemsRestrictionRuleType.SubGroupCUPS 'CupsSubGroup
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsSubGroupByStatusXpCollection(True)
            Case Utils.EItemsRestrictionRuleType.GroupCUPS 'CupsGroup
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsGroupByStatusXpCollection(True)
            Case Utils.EItemsRestrictionRuleType.Product ' product
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductByStatusCollection(True)
            Case Utils.EItemsRestrictionRuleType.SubGroupProduct 'subgroup
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductSubGroupByStatusCollection(True)
            Case Utils.EItemsRestrictionRuleType.GroupProduct 'groupProduct
                Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductGroupByStatusCollection(True)
            Case Else
                Return Nothing
        End Select
    End Function

    Public Sub InitializateCUPDataSource()
        Me.View.DataSourceIncludeToCUPSEntity = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatus(True)
    End Sub

#End Region

End Class
