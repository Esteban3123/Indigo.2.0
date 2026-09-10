'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/04/2016
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class PFixedAssetInitialBalanceItem

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IFixedAssetInitialBalanceItem

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
    Public Sub New(ByRef iview As IFixedAssetInitialBalanceItem)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        Me.View = iview
        Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Listado de los equipos por estado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeItem()
        View.ItemXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetEquipmentByStatus(True)
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
    Public Sub InitializeResponsible()
        View.ResponsibleXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetResponsibleByStatus(True)
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
    Public Sub InitializePolicy()
        View.PolicyXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetPolizaByStatus(True)
    End Sub

    ''' <summary>
    ''' lista las Marcas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLocation()
        View.LocationXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetLocationByStatus()
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLegalBook()
        View.LegalBookXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeStatusAsset()
        View.StatusAssetXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.ListFixedAssetStatusAssetByStatus(True)
    End Sub

    ''' <summary>
    ''' Obtiene un articulo por id
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetFixedAssetItemById(Id As Integer) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).FixedAsset.GetFixedAssetItemById(Id)
    End Function

    ''' <summary>
    ''' obtiene el libro contable por medio del Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetLegalBookById(Id As Integer) As Task(Of Infrastructure.Data.Xpo.AccountingRepository.BookXpo)

        Return Await Task.Factory.StartNew(Function() As Infrastructure.Data.Xpo.AccountingRepository.BookXpo
                                               Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.GetXPOObject(Of Infrastructure.Data.Xpo.AccountingRepository.BookXpo)($"Id = {Id}")
                                           End Function)

    End Function

#End Region

End Class
