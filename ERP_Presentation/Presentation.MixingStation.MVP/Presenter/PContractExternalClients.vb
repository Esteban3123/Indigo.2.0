'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.MixingStation
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PContractExternalClients

#Region "Variables"
    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim _view As IContractExternalClients

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim _sessionValues As SessionValues
#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As IContractExternalClients)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
            Me._sessionValues = SessionValues.Instance
        End If
    End Sub

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenceMixingStation(Me._view.MyTag)
            Me._view.Sequense = Await model.GetSequence()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the cargos.
    ''' </summary>
    Public Function InitializeCustomer() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).CommonService.ListCustomerByStatus(True)
    End Function

    ''' <summary>
    ''' Inicializa los empleados
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeProcedureTemplate() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).ContractService.ListProcedureTemplateByStatus(True)
    End Function

    ''' <summary>
    ''' Inicializa los empleados
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeProductRate() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.ListProductTemplateByStatus(True)
    End Function

    ''' <summary>
    ''' Inicializa los empleados
    ''' </summary>
    ''' <returns></returns>
    Public Function InitializeDefinitionRate() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).ContractService.ListDefinitionRateByStatus(True)
    End Function

    ''' <summary>
    ''' Filtro Almacenes por cliente
    ''' </summary>
    ''' <param name="CustomerId"></param>
    ''' <returns></returns>
    Public Function GetSupplier(ThirdPartyId As Integer) As SupplierXpo
        Dim filter As String = "IdThirdParty = " & ThirdPartyId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of SupplierXpo)(Nothing, filter).FirstOrDefault
    End Function

    Public Function GetThirdPartyId(CustomerId As Integer) As CustomerXpo
        Dim filter As String = "Id = " & CustomerId
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of CustomerXpo)(Nothing, filter).FirstOrDefault
    End Function

    ''' <summary>
    ''' Filtro Almacenes por cliente
    ''' </summary>
    ''' <param name="CustomerId"></param>
    ''' <returns></returns>
    Public Function GetWareHouseBySupplier(SupplierId As Integer) As List(Of WarehouseXpo)
        Dim filter As String = "SupplierId = " & SupplierId & "AND ControlStore = 1 "
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of WarehouseXpo)(Nothing, filter).ToList()
    End Function
#End Region

End Class
