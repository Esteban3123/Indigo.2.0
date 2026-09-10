'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 21-11-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

Public Class PInventoryProduct

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IProduct

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IProduct)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Inicializa un nuevo constructor
    ''' </summary>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the type of the produt.
    ''' </summary>
    Public Sub InitializeProdutType()
        Using Model As New MBusqueda
            Me.View.ProductTypeDatasource = Model.ConsultarEntidades(eDataSource.ListProductType)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the sca.
    ''' </summary>
    Public Sub InitializeATC()
        Using Model As New MBusqueda
            Me.View.ATCDatasource = Model.ConsultarEntidades(eDataSource.ListATC)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the risk level.
    ''' </summary>
    Public Sub InitializeRiskLevel()
        Using Model As New MBusqueda
            Me.View.RiskLevelDatasource = Model.ConsultarEntidades(eDataSource.ListInventoryRiskLevel)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the Storage Temperature.
    ''' </summary>
    Public Sub InitializeStorageTemperature()
        Using Model As New MBusqueda
            Me.View.StorageTemperatureDatasource = Model.ConsultarEntidades(eDataSource.ListStorageTemperature)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia las unidades de empaque
    ''' </summary>
    Public Sub InitializePackingUnit()
        Using Model As New MBusqueda
            Me.View.PackingUnitDatasource = Model.ConsultarEntidades(eDataSource.ListPackagingUnit)
        End Using
    End Sub

    ''' <summary>
    ''' Lista los grupos de productos
    ''' </summary>
    Public Sub InitializeGroups()
        Using Model As New MBusqueda
            Me.View.GroupDatasource = Model.ConsultarEntidades(eDataSource.ListProductGroup)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia los subgrupos
    ''' </summary>
    Public Sub InitializeSubGroups()
        Using Model As New MBusqueda
            Me.View.SubGroupDatasource = Model.ConsultarEntidades(eDataSource.ListProductSubGroup)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia los grupos de facturacion
    ''' </summary>
    Public Sub InitializeBillingGroup()
        Using Model As New MBusqueda
            Me.View.BillingGroupDatasource = Model.ConsultarEntidades(eDataSource.ListBillingGroup)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia Iva
    ''' </summary>
    Public Sub InitializeIva()
        Using Model As New MBusqueda
            Me.View.IVADatasource = Model.ConsultarEntidades(eDataSource.ListGeneralLedgerIva)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia los fabricantes
    ''' </summary>
    Public Sub InitializeManufacturer()
        Using Model As New MBusqueda
            Me.View.ManufacturerDatasource = Model.ConsultarEntidades(eDataSource.ListManufacturers)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia los insumos
    ''' </summary>
    Public Sub InitializeSupplie()
        Using Model As New MBusqueda
            Me.View.SupplieDatasource = Model.ConsultarEntidades(eDataSource.ListInventorySupplie)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el atc ahora llamado medicamentos
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetMedicamentById(MedicamentId As Integer) As Task(Of ATCXpo)
        Dim filter As String = "Id = " & MedicamentId
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)(filter))
    End Function

    ''' <summary>
    ''' Obtiene el insumo 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetSupplieById(ByVal SupplieId As Integer) As Task(Of InventorySupplieXpo)
        Dim filter As String = "Id = " & SupplieId
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventorySupplieXpo)(filter))
    End Function

    Public Function GetRiskLevelById(RiskLevelId As Integer) As InventoryRiskLevelXpo
        Dim filter As String = "Id = " & RiskLevelId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRiskLevelXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function GetListPathologiesByMedicamentId(MedicamentId As Integer) As List(Of POSPathologiesXpo)
        Dim filter As String = "MedicamentId = " & MedicamentId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of POSPathologiesXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="product"></param>
    ''' <returns></returns>
    Public Async Function ValidateMedicationTypeByProduct(ByVal product As Domain.Entities.InventoryProduct) As Task(Of Domain.Base.Entities.ActionResult)
        Using model As New MInventoryProduct("")
            Return Await model.ValidateMedicationTypeByProduct(product)
        End Using
    End Function

    Public Sub InitializeMedicationType()
        Using Model As New MBusqueda
            Me.View.MedicationTypeDataSource = Model.ConsultarEntidades(eDataSource.ListMedicationType, {True})
        End Using
    End Sub

    Public Sub LoadPermissionsForm(tag As String)
        Using model As New MInventoryProduct("")
            Dim permissions = model.GetPermissions(tag)
            Me.View.PermissionsForm = (From a In permissions Select Action = a.TagButton, Name = [Enum].GetName(GetType(PermissionsActionsForm), a.TagButton)).ToDictionary(Function(x) x.Action, Function(y) y.Name)
        End Using
    End Sub
#End Region

End Class