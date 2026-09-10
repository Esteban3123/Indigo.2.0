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
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PContractPackage

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IContractPackage

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IContractPackage)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Sub InitializeCups()
        Using model As New MBusqueda
            View.XpoCupsEntity = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsEntityByStatus, True)
            View.XpoCupsEntityService = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCupsEntityByStatus, True)
        End Using
    End Sub

    Public Sub InitializeIPSServices()
        Using model As New MBusqueda
            View.XpoIPSServiceByPackage = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListIPSServiceByStatusAndPresentation, True, 3)
        End Using
    End Sub

    Public Sub InitializeContractDescription(CUPSEntityId As Integer)
        Dim filter As String = "CUPSEntityId = '" & CUPSEntityId & "'"
        View.XpoContractDescription = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of ContractRepository.CUPSEntityContractDescriptionsXpo)(Nothing, filter)
    End Sub

    Public Sub InitializeContractDescriptionPopUp(CUPSEntityId As Integer)
        Dim filter As String = "CUPSEntityId = '" & CUPSEntityId & "'"
        View.XpoContractDescriptionService = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of ContractRepository.CUPSEntityContractDescriptionsXpo)(Nothing, filter)
    End Sub

    Public Sub InitializeProductByStatus()
        Using model As New MBusqueda
            View.XpoProductByStatus = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInventoryProductByStatus)
        End Using
    End Sub

    ''' <summary>
    ''' trae la descripcion del producto
    ''' </summary>
    Public Function GetProductDescription(Id As Integer) As String
        Dim filter As String = "Id = '" & Id & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of InventoryProductXpo)(Nothing, filter).FirstOrDefault.Description
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    Public Function GetCupsEntityCodeName(Id As Integer) As String
        Dim filter As String = "Id = '" & Id & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of InventoryProductXpo)(Nothing, filter).FirstOrDefault.Description
    End Function

    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer) As SettingsContractXpo
        Dim filter As String = "OperatingUnitId = " & operatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of SettingsContractXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
