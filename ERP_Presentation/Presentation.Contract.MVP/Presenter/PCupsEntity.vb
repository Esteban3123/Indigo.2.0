'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PCupsEntity

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ICupsEntity

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Task
    ''' </summary>
    Dim taskCups As Task(Of List(Of CupsEntityXpo))
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ICupsEntity)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        taskCups = Task.Factory.StartNew(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetXPCollectionEntity(Of CupsEntityXpo)("Status = True")?.ToList())
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Sub InitializeCupsSubGroup()
        Using model As New MBusqueda
            View.CupsSubGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsSubGroupByStatus, True)
        End Using
    End Sub

    Public Sub InitializeIPSServiceGroup()
        Using model As New MBusqueda
            View.IPSServiceGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServicesGroupByStatus, True)
        End Using
    End Sub

    Public Sub InitializeBillingGroup()
        Using model As New MBusqueda
            View.BillingGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBillingGroupByStatus, True)
        End Using
    End Sub

    ''' <summary>
    ''' Datasource de los RIAS
    ''' </summary>
    Public Function InitializeRIAS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListRIAS(1)
    End Function

    ''' <summary>
    ''' Datasource de los RIAS
    ''' </summary>
    Public Function GetRIASCUPS(CupsCode As String) As List(Of RIASCUPSXpo)
        Dim filter As String = "CODSERIPS = '" & CupsCode & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of RIASCUPSXpo)(Nothing, filter).ToList()
    End Function

    Public Sub InitializeRIASBillingConcept()
        Using model As New MBusqueda
            View.RIASBillingConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServicesGroupByStatus, True)
        End Using
    End Sub

    Public Sub InitializeRIASBillingGroup()
        Using model As New MBusqueda
            View.RIASBillingGroupXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBillingGroupByStatus, True)
        End Using
    End Sub

    Public Function InitializeDescriptions() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractDescriptionsByStatus(True)
    End Function

    Public Function InitializeSubgroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsSubGroupByStatus(True)
    End Function

    Public Function InitializeBillingGroupDescriptions() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListBillingGroupByStatus(True)
    End Function

    Public Function InitializeRIPSServices()
        View.RIPSServicesXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListRIPSServices()
    End Function

    Public Function InitializeBillingConcept() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListBillingConceptByStatus(True)
    End Function

    Public Function ListDescriptionsByCupsId(Id As Integer) As List(Of CUPSEntityContractDescriptionsXpo)
        Dim filter As String = "CUPSEntityId.Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of CUPSEntityContractDescriptionsXpo)(Nothing, filter).ToList()
    End Function

    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer) As SettingsContractXpo
        Dim filter As String = "OperatingUnitId = " & operatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of SettingsContractXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' inicializa el combo de cups
    ''' </summary>
    ''' <returns></returns>
    Public Async Function InitializeCUPSEntity() As Task
        View.AsyncLoader(True)
        View.CupsEntityXPO = Await taskCups
        View.AsyncLoader(False)
    End Function
#End Region

End Class
