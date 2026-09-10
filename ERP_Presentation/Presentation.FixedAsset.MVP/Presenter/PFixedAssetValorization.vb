'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/04/2016
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
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base

#End Region

Public Class PFixedAssetValorization

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetValorization

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
    Public Sub New(ByRef iview As IFixedAssetValorization)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
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
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequenceFixedAsset(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    Public Async Function InitializeFixedAssetSetting(OperatingUnitId As Integer) As Task
        Using model As New MSettingFixedAsset(Me.View.MyTag)
            View.SettingFixedAsset = Await model.GetSettingFixedAssetByOperatingUnitId(OperatingUnitId)
        End Using
    End Function

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeThirdParty()
        View.ThirdPartyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

    ''' <summary>
    ''' Establece el datasource de las cuentas contables
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeMainAccount()
        View.MainAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de centro costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenter()
        View.CostCenterXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplierDistribution()
        View.SupplierDistributionLineXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListSuppliersDistributionLines()
    End Sub

    ''' <summary>
    ''' Obtiene la linea de distribución
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetSupplierDistributionLineById(Id As Integer) As CommonSuppliersDistibutionLineXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of CommonSuppliersDistibutionLineXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetPhysicalAssetByTransactionClass(TransactionClass As Byte, PhysicalAssetId As Integer?, PhysicalAssetPartsId As Integer?) As FixedAssetPhysicalAssetXpo
        If TransactionClass = 1 Then
            Dim filter As String = "Id = " & PhysicalAssetId
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of FixedAssetPhysicalAssetXpo)(Nothing, filter).FirstOrDefault()
        Else
            Dim filter As String = "Id = " & PhysicalAssetPartsId
            Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of FixedAssetPhysicalAssetPartsXpo)(Nothing, filter).FirstOrDefault().PhysicalAssetId
        End If
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemById(Id As Integer) As FixedAssetItemReportXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of FixedAssetItemReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAccountPayableConceptById(Id As Integer) As PaymentsAccountPayableConceptsXpoP
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of PaymentsAccountPayableConceptsXpoP)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista las remisiones de entrada
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetRetentionConceptById(Id As Integer) As GeneralLedgerRetentionConceptsReportXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of GeneralLedgerRetentionConceptsReportXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Metodo que conecta al modelo para ejecutar el metodo que trae un colección desde el xpo
    ''' </summary>
    Public Sub GetListCurrency()
        Using model As New MSettingFixedAsset("")
            Me.View.ListCurrencyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCurrency
        End Using
    End Sub

#End Region

End Class
