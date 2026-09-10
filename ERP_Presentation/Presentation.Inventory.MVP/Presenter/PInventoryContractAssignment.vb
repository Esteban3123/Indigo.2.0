#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PInventoryContractAssignment

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IInventoryContractAssignment

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
    Public Sub New(ByRef iview As IInventoryContractAssignment)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    Public Function GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer) As PaymentsRepository.PaymentsSettingPaymentsXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsRepository.PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub LoadContractInventory()
        Using Model As New MPurchaseOrder("")
            Me.View.ListContractInventory = Model.ListContractInventoryXPO()
        End Using
    End Sub

    Public Function GetContractById(Id As Integer) As InventoryRepository.InventoryContractXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRepository.InventoryContractXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub InitializeSupplierAssignee()
        Dim model As New MBusqueda
        Me.View.SuppliersDistributionLinesAssigneeXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
    End Sub

    Public Function GetSuppliersDistributionLineIdById(Id As Integer) As CommonRepository.CommonSuppliersDistibutionLineXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of CommonRepository.CommonSuppliersDistibutionLineXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
