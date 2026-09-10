'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 02-09-2013
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
Imports Presentation.FixedAsset.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository

#End Region
''' <summary>
''' 
''' </summary>
Public Class PFixedAssetEquipment

    ''' <summary>s
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetEquipment

    ''' <summary>
    ''' Variable que se usa para tratar la torre como un objeto
    ''' </summary>
    Dim Equipment As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IFixedAssetEquipment)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLegalBook()
        View.LegalBookXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Sub

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequenceFixedAsset(View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Datasource de catalogo de equipos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEquipmentCatalog()
        View.EquipmentCatalogXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedEquipmentCatalogByStatus(True)
    End Sub

    ''' <summary>
    ''' Datasource del IVA
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeIVA()
        View.IVAXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListGeneralLedgerIvaByStatus(True)
    End Sub

    ''' <summary>
    ''' Datasource tipo de equipo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEquipmentType()
        View.EquipmentTypeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListAllEquipmentType()
    End Sub

    ''' <summary>
    ''' Datasource Catalogo de bienes y servicios
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCatalogOfPropertyandServices()
        Dim filter = "State = True"
        View.CatalogOfPropertyandServicesXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListXPInstantFeedbackSource(Of FixedAssetRepository.FixedAssetCatalogOfPropertyandServicesXpo)(filter:=filter)
    End Sub

    ''' <summary>
    ''' Datasource de las monedas parametrizadas en el cliente
    ''' </summary>
    Public Function InitializeCurrency() As List(Of Infrastructure.Data.Xpo.FixedAssetRepository.CommonCurrencyXpo)
        Dim criteria As String = "State = True"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of Infrastructure.Data.Xpo.FixedAssetRepository.CommonCurrencyXpo)(Nothing, criteria).ToList()
    End Function

End Class
