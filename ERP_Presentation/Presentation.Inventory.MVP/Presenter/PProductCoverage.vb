'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-12-2014
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
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.AccountingRepository

#End Region

Public Class PProductCoverage

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IProductCoverage

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
    Public Sub New(ByRef iview As IProductCoverage, Optional value As Boolean = True)
        If value Then
            If iview Is Nothing Then
                Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
            End If
            Me.View = iview
        End If
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
    ''' Lista las definiciones de tarifa para la rejilla de importar información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductRateDetailByProductRateId(productRateId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListProductRateDetailByProductRateId(productRateId)
    End Function

    ''' <summary>
    ''' Lista las definiciones de tarifa para la rejilla de importar información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListProductRateConditionsByProductRateId(productRateId As Integer) As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListProductRateConditionByProductRateId(productRateId)
    End Function

    ''' <summary>
    ''' consulta las descripciones relacionadas por cups
    ''' </summary>
    ''' <param name="CUPSEntityId"></param>
    ''' <returns></returns>
    Public Function InitializeContractDescription(CUPSEntityId As Integer) As List(Of CUPSEntityContractDescriptionsXpo)
        Dim filter As String = "CUPSEntityId = '" & CUPSEntityId & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of CUPSEntityContractDescriptionsXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' consulta el producto por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function ProductbyId(Id As Integer) As List(Of InventoryProductXpo)
        Dim filter As String = $"Id = {Id}"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of InventoryProductXpo)(Nothing, filter)
    End Function



    ''' <summary>
    ''' Obtiene la configuracion de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOfficialCurrencyFromCompanySettings() As GeneralLedgerCompanySettingsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of GeneralLedgerCompanySettingsXpo)(Nothing)
    End Function

#End Region

End Class