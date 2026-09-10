'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-12-2014
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

Public Class PATC

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IATC

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
    Public Sub New(ByRef iview As IATC)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Gets the sequense.
    ''' </summary>
    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequence = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the dci.
    ''' </summary>
    Public Sub InitializeDCI()
        Using Model As New MBusqueda
            Me.View.DCIDatasource = Model.ConsultarEntidades(eDataSource.ListDCI)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the administration route.
    ''' </summary>
    Public Sub InitializeAdministrationRoute()
        Using Model As New MBusqueda
            Me.View.AdmisnistrationRouteDatasource = Model.ConsultarEntidades(eDataSource.ListAdministrationRoute)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the pharmacological group.
    ''' </summary>
    Public Sub InitializePharmacologicalGroup()
        Using Model As New MBusqueda
            Me.View.PharmacologicalGroupDatasource = Model.ConsultarEntidades(eDataSource.ListPharmacologicalGroup)
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
    ''' Initializes the measure unit.
    ''' </summary>
    Public Sub InitializeMeasureUnit()
        Using Model As New MBusqueda
            Dim _filter As Object() = {1}
            Me.View.MeasureUnitDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the measure unit volumen.
    ''' </summary>
    Public Sub InitializeMeasureUnitVolumen()
        Using Model As New MBusqueda
            Dim _filter As Object() = {2}
            Me.View.MeasureUnitVolumenDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the administration unit.
    ''' </summary>
    Public Sub InitializeAdministrationUnit()
        Using Model As New MBusqueda
            Dim _filter As Object() = {3}
            Me.View.AdministrationUnitDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnitByType, _filter)
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the administration unit.
    ''' </summary>
    Public Sub InitializeMeasureUnitConcentration()
        Using Model As New MBusqueda
            Me.View.MeasureUnitConcentrationDatasource = Model.ConsultarEntidades(eDataSource.ListMeasureUnit)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el datasource de las unidades de medida para sustancias 
    ''' </summary>
    Public Function InitializeMeasureUnitSubstanceConcentration()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListMeasureUnitSubstance()
    End Function

    ''' <summary>
    ''' Obtiene el datasource de las unidades de presentación UPR ACTIVAS.
    ''' </summary>
    Public Sub InitializeUPRUnits()
        If View.UPRUnitsDatasource Is Nothing Then
            View.UPRUnitsDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListUPRUnits(True)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el datasource de los laboratorios.
    ''' </summary>
    Public Function InitializeLaboratories()
        Dim _filter = $"ServiceType = {1}"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollectionAsList(Of CupsEntityXpo)(Nothing, _filter)
    End Function

    ''' <summary>
    ''' Metodo que carga los atc asociados al dci
    ''' </summary>
    ''' <param name="DCIId"></param>
    ''' <returns></returns>
    Public Function LoadATC(DCIId) As DevExpress.Xpo.XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListATCEntityRelatedDCI(DCIId)
    End Function

    ''' <summary>
    ''' Metodo que carga el DCI por Id
    ''' </summary>
    ''' <param name="DCIId"></param>
    ''' <returns></returns>
    Public Function GetDCIById(DCIId As Integer) As DCIXpo
        Dim filter As String = "Id = " & DCIId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of DCIXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function GetATCEntityById(ATCEntityId As Integer) As ATCEntityXpo
        Dim filter As String = "Id = " & ATCEntityId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ATCEntityXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Function ListPathologiesByMedicamentId(MedicamentId As Integer) As List(Of POSPathologiesXpo)
        Dim filter As String = "MedicamentId = " & MedicamentId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of POSPathologiesXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetAdministrationRouteById(ByVal Id As Integer) As AdministrationRouteXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetAdministrationRouteById(Id)
    End Function

    Public Sub InitializePharmaceuticalForm()
        Using model As New MBusqueda
            View.PharmaceuticalFormXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPharmaceuticalFormByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="PharmaceuticalFormId"></param>
    ''' <returns></returns>
    Public Function GetPharmaceuticalFormById(PharmaceuticalFormId As Integer) As PharmaceuticalFormXpo
        Dim filter As String = "Id = " & PharmaceuticalFormId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of PharmaceuticalFormXpo)(Nothing, filter).FirstOrDefault()
    End Function
#End Region

End Class