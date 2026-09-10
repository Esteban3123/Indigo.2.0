'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/06/2016
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
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class PFixedAssetPhysicalAsset

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetPhysicalAsset

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetPhysicalAsset)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplier()
        View.SupplierXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).MaintenanceService.ListSupplierByStatus(True)
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeTrademark()
        View.TrademarkXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListTrademarkByState(True)
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializePolicy()
        View.PolicyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPolizaByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLegalBook()
        View.LegalBookXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de indicios de deterioro
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeDeteriorationIndication()
        View.DeteriorationIndicationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListDeteriorationIndicationsByStatus(True)
    End Sub

    ''' <summary>
    ''' Lista los activos
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetXpoById(Id As Integer) As FixedAssetPhysicalAssetXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetXPOObject(Of FixedAssetPhysicalAssetXpo)(filtroConsulta)
    End Function

    ''' <summary>
    ''' Metodo para traer la configuracion de activos por unidad operativa
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    Public Async Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer) As Task
        Using model As New MSettingFixedAsset(Me.View.MyTag)
            View.SettingsFixedAsset = Await model.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
        End Using
    End Function

#End Region

End Class
