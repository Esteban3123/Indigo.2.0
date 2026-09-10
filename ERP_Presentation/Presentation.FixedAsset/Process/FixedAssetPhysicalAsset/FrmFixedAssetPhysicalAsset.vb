'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/06/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

#End Region

Public Class FrmFixedAssetPhysicalAsset
    Implements IFixedAssetPhysicalAsset, ICustomizableForm

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bwList.DoWork, AddressOf bwList_DoWork
        AddHandler bwList.RunWorkerCompleted, AddressOf bwList_RunWorkerCompleted
    End Sub

#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwList_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        'Se obtiene la entidad xpo de activos para convertirlos a los listados que van 
        FixedAssetPhysicalAssetXpo = Presenter.GetFixedAssetPhysicalAssetXpoById(FixedAssetPhysicalAsset.Id)
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwList_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        'Convierte la entidad xpo en los listados que se asignan a las rejilla
        ConvertXpoToList()
        INDgcBooks.DataSource = ListFixedAssetPhysicalAssetDetailBook
        INDgcParts.DataSource = ListFixedAssetPhysicalAssetParts
        INDgcDeteriorationIndications.DataSource = ListDeteriorationIndicationByPhysicalAsset
        INDgcAccesories.DataSource = ListFixedAssetPhysicalAssetAccessory
    End Sub

#End Region

#Region "Properties"

    Public Property LegalBookXpo As XPInstantFeedbackSource Implements IFixedAssetPhysicalAsset.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    Public Property DeteriorationIndicationXpo As XPInstantFeedbackSource Implements IFixedAssetPhysicalAsset.DeteriorationIndicationXpo
        Get
            Return INDsleDeteriorationIndication.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDeteriorationIndication.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Estado del activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IFixedAssetPhysicalAsset.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Valor razonable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FairValue As Decimal Implements IFixedAssetPhysicalAsset.FairValue
        Get
            Return INDtxtFairValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFairValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor recuperable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RecoverableValue As Decimal Implements IFixedAssetPhysicalAsset.RecoverableValue
        Get
            Return INDtxtRecoverableValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtRecoverableValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Maneja garantia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HandlesWarranty As Boolean? Implements IFixedAssetPhysicalAsset.HandlesWarranty
        Get
            Return INDsleHandlesWarranty.EditValue
        End Get
        Set(value As Boolean?)
            INDsleHandlesWarranty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor historico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HistoricalValue As Decimal Implements IFixedAssetPhysicalAsset.HistoricalValue
        Get
            Return INDtxtHistoricalValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtHistoricalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descuento Financiero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinancialDiscount As Decimal Implements IFixedAssetPhysicalAsset.FinancialDiscount
        Get
            Return INDtxtFinancialDiscount.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFinancialDiscount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Valor Histórico Neto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NetHistoricalValue As Decimal Implements IFixedAssetPhysicalAsset.NetHistoricalValue
        Get
            Return INDtxtNetHistoricalValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtNetHistoricalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de instalacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InstallationDate As Date? Implements IFixedAssetPhysicalAsset.InstallationDate
        Get
            Return INDdteInstallationDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInstallationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de la compra
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PurchaseDate As Date? Implements IFixedAssetPhysicalAsset.PurchaseDate
        Get
            Return INDdtePurchaseDate.EditValue
        End Get
        Set(value As Date?)
            INDdtePurchaseDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Número del ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EntryNumber As String Implements IFixedAssetPhysicalAsset.EntryNumber
        Get
            Return INDtxtEntryNumber.Properties.NullText
        End Get
        Set(value As String)
            INDtxtEntryNumber.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Número del comprobate de egreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property VoucherTransactionNumber As String Implements IFixedAssetPhysicalAsset.VoucherTransactionNumber
        Get
            Return INDtxtVoucherTransactionNumber.EditValue
        End Get
        Set(value As String)
            INDtxtVoucherTransactionNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Articulo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemDescription As String Implements IFixedAssetPhysicalAsset.ItemDescription
        Get
            Return INDtxtItem.Properties.NullText
        End Get
        Set(value As String)
            INDtxtItem.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Localizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LocationDescription As String Implements IFixedAssetPhysicalAsset.LocationDescription
        Get
            Return INDtxtLocation.EditValue
        End Get
        Set(value As String)
            INDtxtLocation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountDescription As String Implements IFixedAssetPhysicalAsset.MainAccountDescription
        Get
            Return INDtxtMainAccount.EditValue
        End Get
        Set(value As String)
            INDtxtMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Modelo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModelPhysical As String Implements IFixedAssetPhysicalAsset.ModelPhysical
        Get
            Return INDtxtModel.EditValue
        End Get
        Set(value As String)
            INDtxtModel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Placa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Plate As String Implements IFixedAssetPhysicalAsset.Plate
        Get
            Return INDtxtPlate.EditValue
        End Get
        Set(value As String)
            INDtxtPlate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la poliza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PolicyId As Integer? Implements IFixedAssetPhysicalAsset.PolicyId
        Get
            Return INDslePolicy.EditValue
        End Get
        Set(value As Integer?)
            INDslePolicy.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource poliza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PolicyXpo As XPInstantFeedbackSource Implements IFixedAssetPhysicalAsset.PolicyXpo
        Get
            Return INDslePolicy.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePolicy.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Responsable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResponsibleDescription As String Implements IFixedAssetPhysicalAsset.ResponsibleDescription
        Get
            Return INDtxtResponsible.EditValue
        End Get
        Set(value As String)
            INDtxtResponsible.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Serie
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Serie As String Implements IFixedAssetPhysicalAsset.Serie
        Get
            Return INDtxtSerie.EditValue
        End Get
        Set(value As String)
            INDtxtSerie.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierId As Integer? Implements IFixedAssetPhysicalAsset.SupplierId
        Get
            Return INDsleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierXpo As XPInstantFeedbackSource Implements IFixedAssetPhysicalAsset.SupplierXpo
        Get
            Return INDsleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la marca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TrademarkId As Integer? Implements IFixedAssetPhysicalAsset.TrademarkId
        Get
            Return INDsleTrademark.EditValue
        End Get
        Set(value As Integer?)
            INDsleTrademark.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de la marca
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TrademarkXpo As XPInstantFeedbackSource Implements IFixedAssetPhysicalAsset.TrademarkXpo
        Get
            Return INDsleTrademark.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTrademark.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de venicmiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WarrantyExpirationDate As Date? Implements IFixedAssetPhysicalAsset.WarrantyExpirationDate
        Get
            Return INDdteWarrantyExpirationDate.EditValue
        End Get
        Set(value As Date?)
            INDdteWarrantyExpirationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetPhysicalAsset.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetPhysicalAsset.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitLifeUtil As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListDepreciationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim FixedAssetPhysicalAsset As FixedAssetPhysicalAsset

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetPhysicalAsset

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Listado de libros del activo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetPhysicalAssetDetailBook As List(Of FixedAssetPhysicalAssetDetailBook)

    ''' <summary>
    ''' Listado de partes del activo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetPhysicalAssetParts As List(Of FixedAssetPhysicalAssetParts)

    ''' <summary>
    ''' Listado de indicios de deterioro asociados del activo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeteriorationIndicationByPhysicalAsset As List(Of DeteriorationIndicationByPhysicalAsset)

    ''' <summary>
    ''' Listado de accesorios asociados del activo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetPhysicalAssetAccessory As List(Of FixedAssetPhysicalAssetAccessory)

    ''' <summary>
    ''' Asyncrono para obtener los libros y las partes del activo
    ''' </summary>
    ''' <remarks></remarks>
    Private bwList As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Entidad xpo para physicalAsset
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetPhysicalAssetXpo As FixedAssetPhysicalAssetXpo

    ''' <summary>
    ''' Establece o toma la configuracion de parametros de activos
    ''' </summary>
    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset Implements IFixedAssetPhysicalAsset.SettingsFixedAsset
        Get
            Return _SettingsFixedAsset
        End Get
        Set(value As SettingFixedAsset)
            _SettingsFixedAsset = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControlsPopup(1)
        CleanControlsPopup(2)
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            'Si la clase de la localización del activo es 4=Almacen y Bodegaje, se valida que la fecha de instalación no la puedan borrar
            If FixedAssetPhysicalAsset.ClassLocation = 4 AndAlso InstallationDate Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha de instalación no puede estar vacía para el tipo de clase(Almacén y Bodegaje) de la localización"
                Exit Sub
            End If
            AssigningValues()
            Using model As New MFixedAssetPhysicalAsset(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveFixedAssetPhysicalAsset(FixedAssetPhysicalAsset)
                If Result.StateResult = True Then
                    If FixedAssetPhysicalAsset.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    AsyncLoader(False)
                    Me.FixedAssetPhysicalAsset = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    AsyncLoader(False)
                    SearchMode = False
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If Result.StatusCode = eStatusResult.EXCEPTION Then
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    ElseIf Result.StatusCode = eStatusResult.WARNING Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

#End Region

#Region "Methods"

    Private Function ValidateControlsPopup(Optional ByVal options As Integer = 1) As String
        Dim errors As New StringBuilder

        If options = 1 Then
            If INDsleLegalBook.EditValue Is Nothing Then
                errors.AppendLine("Debe seleccionar un libro oficial.")
            End If
            If INDseLifeUtil.EditValue = Nothing OrElse INDseLifeUtil.EditValue = 0 Then
                errors.AppendLine("Debe seleccionar una vida útil.")
            End If
            If INDsleUnitLifeUtil.EditValue Is Nothing Then
                errors.AppendLine("Debe seleccionar una unidad de vida útil.")
            End If
            If INDsleDepreciationType.EditValue Is Nothing Then
                errors.AppendLine("Debe ingresar un tipo de depreciación.")
            End If
            If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDseTotalProductionUnit.EditValue = Nothing OrElse INDseTotalProductionUnit.EditValue = 0 Then
                    errors.AppendLine("Debe ingresar total de unidades producidas.")
                End If
            End If
            If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsePercentageRescue.EditValue = Nothing Then
                    errors.AppendLine("Debe ingresar un % de salvamento.")
                End If
            End If
        ElseIf options = 2 Then
            If INDsleDeteriorationIndication.EditValue Is Nothing Then
                errors.AppendLine("Debe seleccionar un indicio de deterioro.")
            End If
        ElseIf options = 3 Then
            If String.IsNullOrEmpty(INDMeAccesoryDescription.EditValue) Then
                errors.AppendLine("Debe una descripción.")
            End If
            If Not INDSeAccesoryQuantity.EditValue > 0 Then
                errors.AppendLine("Debe ingresar una cantidad válida.")
            End If
            If Not INDSeAccesoryValue.EditValue > 0 Then
                errors.AppendLine("Debe ingresar un valor válida.")
            End If
        End If

        Return errors.ToString
    End Function

	Private Sub AddDetailsBook()
		Dim errors = ValidateControlsPopup(1)
		If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
			Mensaje(EeventViewerImages.Advertencia) = errors
			Exit Sub
		End If

        If ListFixedAssetPhysicalAssetDetailBook IsNot Nothing Then
            Dim bookAdded = ListFixedAssetPhysicalAssetDetailBook.Find(Function(i) i.LegalBookId = INDsleLegalBook.EditValue)
            If bookAdded IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra agregado el libro"
                Exit Sub
            End If
        End If

        If ListFixedAssetPhysicalAssetDetailBook Is Nothing Then
            ListFixedAssetPhysicalAssetDetailBook = New List(Of FixedAssetPhysicalAssetDetailBook)
        End If

        Dim book = DirectCast(DirectCast(INDGvBook.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.BookXpo)

		Dim detail As New FixedAssetPhysicalAssetDetailBook
		With detail
			detail.LegalBook = New LegalBook With {
													.Id = book.Id,
													.Currency = New Currency With {.Abbreviation = book.CommonCurrency.Abbreviation}
												}
			.LegalBookId = INDsleLegalBook.EditValue
			.LegalBookDescription = INDsleLegalBook.Text
			.LagalBookType = book.TypeBook
			.LifeTime = INDseLifeUtil.EditValue
			.UnitLifeTime = INDsleUnitLifeUtil.EditValue
			.DepreciationType = INDsleDepreciationType.EditValue
			If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				.TotalProductionUnit = INDseTotalProductionUnit.EditValue
			Else
				.TotalProductionUnit = 0
			End If
			If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
				.PercentageRescue = INDsePercentageRescue.EditValue
			Else
				.PercentageRescue = 0
			End If
			If book.TypeBook = 2 Then
				.ResidualValue = INDtxtFairValue.EditValue
			Else
				.ResidualValue = INDTxtResidualValue.EditValue
			End If
			.HistoricalValue = .ResidualValue
		End With
		detail.LegalBook.Currency.MarkAsUnchanged()
		detail.LegalBook.MarkAsUnchanged()
		detail.LegalBook.Currency.StopTracking()
		detail.LegalBook.StopTracking()
		ListFixedAssetPhysicalAssetDetailBook.Add(detail)

		INDgcBooks.DataSource = ListFixedAssetPhysicalAssetDetailBook
		INDgcBooks.RefreshDataSource()
		CleanControlsPopup(1)
		INDsleLegalBook.Focus()
	End Sub

	Private Sub CleanControlsPopup(Optional ByVal options As Integer = 1)
        If options = 1 Then
            INDsleLegalBook.EditValue = Nothing
            INDseLifeUtil.EditValue = 1
            INDsleUnitLifeUtil.EditValue = Nothing
            INDsleDepreciationType.EditValue = Nothing
            INDseTotalProductionUnit.EditValue = 0
            INDsePercentageRescue.EditValue = 0
            INDTxtResidualValue.EditValue = 0
        ElseIf options = 2 Then
            INDsleDeteriorationIndication.EditValue = Nothing
        ElseIf options = 3 Then
            INDMeAccesoryDescription.EditValue = Nothing
            INDTxtAccesorySerie.EditValue = Nothing
            INDSeAccesoryQuantity.EditValue = Nothing
            INDSeAccesoryValue.EditValue = Nothing
        End If
    End Sub

    Private Sub InitializeTuple()
        'Unidad vida util
        ListUnitLifeUtil = New List(Of Tuple(Of Integer, String))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(1, "Año"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(2, "Mes"))
        ListUnitLifeUtil.Add(New Tuple(Of Integer, String)(3, "Día"))
        INDsleUnitLifeUtil.Properties.DataSource = ListUnitLifeUtil.ToList
        'Tipo de depreciación
        ListDepreciationType = New List(Of Tuple(Of Integer, String))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(1, "Línea Recta"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(2, "Suma de Dígitos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(3, "Reducción de Saldos"))
        ListDepreciationType.Add(New Tuple(Of Integer, String)(4, "Unidades de Producción"))
        INDsleDepreciationType.Properties.DataSource = ListDepreciationType.ToList
    End Sub

    Private Sub AddDeteriorationIndication()
        Dim errors = ValidateControlsPopup(2)
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If ListDeteriorationIndicationByPhysicalAsset IsNot Nothing AndAlso ListDeteriorationIndicationByPhysicalAsset.Count() > 0 Then
            Dim DeteriorationIndicationByPhysicalAsset = ListDeteriorationIndicationByPhysicalAsset.Find(Function(i) i.DeteriorationIndicationId = INDsleDeteriorationIndication.EditValue)
            If DeteriorationIndicationByPhysicalAsset IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Ya se encuentra agregado el indicio de deterioro"
                Exit Sub
            End If
        End If

        Dim DeteriorationIndicationXpo = DirectCast(DirectCast(INDgvDeteriorationIndication.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.DeteriorationIndicationsXpo)

        Dim detail As New DeteriorationIndicationByPhysicalAsset
        With detail
            .DeteriorationIndicationId = INDsleDeteriorationIndication.EditValue
            .DeteriorationIndicationCodeName = INDsleDeteriorationIndication.Text
            .DeteriorationIndicationRate = DeteriorationIndicationXpo.Rate
        End With
        ListDeteriorationIndicationByPhysicalAsset.Add(detail)

        INDgcDeteriorationIndications.DataSource = ListDeteriorationIndicationByPhysicalAsset
        INDgcDeteriorationIndications.RefreshDataSource()
        CleanControlsPopup(2)
        INDsleDeteriorationIndication.Focus()
    End Sub

    Private Sub AddAccesory()
        Dim errors = ValidateControlsPopup(3)
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        Dim detail As New FixedAssetPhysicalAssetAccessory
        With detail
            .Description = INDMeAccesoryDescription.EditValue
            .Serie = INDTxtAccesorySerie.EditValue
            .Quantity = INDSeAccesoryQuantity.EditValue
            .Value = INDSeAccesoryValue.EditValue
        End With
        ListFixedAssetPhysicalAssetAccessory.Add(detail)

        INDgcAccesories.DataSource = ListFixedAssetPhysicalAssetAccessory
        INDgcAccesories.RefreshDataSource()
        CleanControlsPopup(3)
        INDMeAccesoryDescription.Focus()
    End Sub

    Private Sub ConvertXpoToList()
        If FixedAssetPhysicalAssetXpo Is Nothing Then
            Exit Sub
        End If

        'Si tiene libros el activo
        If FixedAssetPhysicalAssetXpo.FixedAssetPhysicalAssetDetailBookXpo?.Any() Then

            'Se instancia el listado de libros del activo
            If ListFixedAssetPhysicalAssetDetailBook Is Nothing Then
                ListFixedAssetPhysicalAssetDetailBook = New List(Of FixedAssetPhysicalAssetDetailBook)
            End If

            'Se recorre los detalles de activo
            For Each physicalAssetDetailBookXpo In FixedAssetPhysicalAssetXpo.FixedAssetPhysicalAssetDetailBookXpo
                Dim physicalAssetDetailBook As New FixedAssetPhysicalAssetDetailBook
				physicalAssetDetailBook.Id = physicalAssetDetailBookXpo.Id
				physicalAssetDetailBook.LegalBookDescription = physicalAssetDetailBookXpo.LegalBookId.CodeName
                physicalAssetDetailBook.LagalBookType = physicalAssetDetailBookXpo.LegalBookId.TypeBook
				physicalAssetDetailBook.LegalBook = New LegalBook With {
														.Id = physicalAssetDetailBookXpo.LegalBookId.Id,
														.Currency = New Currency With {.Abbreviation = physicalAssetDetailBookXpo.LegalBookId.OfficialCurrencyId.Abbreviation}
													}
				physicalAssetDetailBook.LegalBookId = physicalAssetDetailBook.LegalBook.Id
				physicalAssetDetailBook.LifeTime = physicalAssetDetailBookXpo.LifeTime
                physicalAssetDetailBook.UnitLifeTime = physicalAssetDetailBookXpo.UnitLifeTime
                physicalAssetDetailBook.Valorization = physicalAssetDetailBookXpo.Valorization
                physicalAssetDetailBook.Devaluation = physicalAssetDetailBookXpo.Devaluation
                physicalAssetDetailBook.AdjustedValue = physicalAssetDetailBookXpo.AdjustedValue
                physicalAssetDetailBook.DepreciatedDays = physicalAssetDetailBookXpo.DepreciatedDays
                physicalAssetDetailBook.DepreciatedValue = physicalAssetDetailBookXpo.DepreciatedValue
                physicalAssetDetailBook.ResidualValue = physicalAssetDetailBookXpo.ResidualValue
                physicalAssetDetailBook.DaysPendingDepreciate = physicalAssetDetailBookXpo.DaysPendingDepreciate
                physicalAssetDetailBook.DepreciationType = physicalAssetDetailBookXpo.DepreciationType
                physicalAssetDetailBook.TotalProductionUnit = physicalAssetDetailBookXpo.TotalProductionUnit
                physicalAssetDetailBook.PercentageRescue = physicalAssetDetailBookXpo.PercentageRescue
                physicalAssetDetailBook.HistoricalValue = physicalAssetDetailBookXpo.HistoricalValue
				physicalAssetDetailBook.StartTracking()
				physicalAssetDetailBook.LegalBook.Currency.MarkAsUnchanged()
				physicalAssetDetailBook.LegalBook.MarkAsUnchanged()
				physicalAssetDetailBook.LegalBook.Currency.StopTracking()
				physicalAssetDetailBook.LegalBook.StopTracking()
				physicalAssetDetailBook.MarkAsUnchanged()
                ListFixedAssetPhysicalAssetDetailBook.Add(physicalAssetDetailBook)
            Next
        End If

        'Si tiene partes el activo
        If FixedAssetPhysicalAssetXpo.FixedAssetPhysicalAssetPartsXpo?.Any() Then

            'Se instancia el listado de partes del activo
            If ListFixedAssetPhysicalAssetParts Is Nothing Then
                ListFixedAssetPhysicalAssetParts = New List(Of FixedAssetPhysicalAssetParts)
            End If

            'Se recorre las partes del activo
            For Each physicalAssetPartsXpo In FixedAssetPhysicalAssetXpo.FixedAssetPhysicalAssetPartsXpo
                Dim physicalAssetParts As New FixedAssetPhysicalAssetParts
                physicalAssetParts.PartDescription = physicalAssetPartsXpo.PartAccesoriesConsumiblesId.CodeName
                physicalAssetParts.DepreciatePart = physicalAssetPartsXpo.DepreciatePart
                physicalAssetParts.HistoricalValue = physicalAssetPartsXpo.HistoricalValue

                'Si la parte tiene libros
                If physicalAssetPartsXpo.FixedAssetPhysicalAssetPartsDetailBookXpo?.Any() Then
                    'Se recorre los libros de la parte
                    For Each physicalAssetPartsDetailBookXpo In physicalAssetPartsXpo.FixedAssetPhysicalAssetPartsDetailBookXpo
                        Dim physicalAssetPartsDetailBook As New FixedAssetPhysicalAssetPartsDetailBook
                        physicalAssetPartsDetailBook.LegalBookDescription = physicalAssetPartsDetailBookXpo.LegalBookId.CodeName
                        physicalAssetPartsDetailBook.LifeTime = physicalAssetPartsDetailBookXpo.LifeTime
                        physicalAssetPartsDetailBook.UnitLifeTime = physicalAssetPartsDetailBookXpo.UnitLifeTime
                        physicalAssetPartsDetailBook.Valorization = physicalAssetPartsDetailBookXpo.Valorization
                        physicalAssetPartsDetailBook.Devaluation = physicalAssetPartsDetailBookXpo.Devaluation
                        physicalAssetPartsDetailBook.AdjustedValue = physicalAssetPartsDetailBookXpo.AdjustedValue
                        physicalAssetPartsDetailBook.DepreciatedValue = physicalAssetPartsDetailBookXpo.DepreciatedValue
                        physicalAssetPartsDetailBook.ResidualValue = physicalAssetPartsDetailBookXpo.ResidualValue
                        physicalAssetParts.FixedAssetPhysicalAssetPartsDetailBook.Add(physicalAssetPartsDetailBook)
                    Next
                End If

                ListFixedAssetPhysicalAssetParts.Add(physicalAssetParts)
            Next
        End If

        'Se instancia el listado de indicios de deterioro por activo
        ListDeteriorationIndicationByPhysicalAsset = New List(Of DeteriorationIndicationByPhysicalAsset)
        'Si tiene asignado indicios de deterioro
        If FixedAssetPhysicalAssetXpo.DeteriorationIndicationByPhysicalAssetXpo?.Any() Then
            'Se recorre los indicios de deterioro
            For Each DeteriorationIndicationByPhysicalAssetXpo In FixedAssetPhysicalAssetXpo.DeteriorationIndicationByPhysicalAssetXpo
                Dim DeteriorationIndicationByPhysicalAsset As New DeteriorationIndicationByPhysicalAsset
                DeteriorationIndicationByPhysicalAsset.Id = DeteriorationIndicationByPhysicalAssetXpo.Id
                DeteriorationIndicationByPhysicalAsset.DeteriorationIndicationId = DeteriorationIndicationByPhysicalAssetXpo.DeteriorationIndicationId.Id
                DeteriorationIndicationByPhysicalAsset.DeteriorationIndicationCodeName = DeteriorationIndicationByPhysicalAssetXpo.DeteriorationIndicationId.CodeName
                DeteriorationIndicationByPhysicalAsset.DeteriorationIndicationRate = DeteriorationIndicationByPhysicalAssetXpo.DeteriorationIndicationId.Rate
                DeteriorationIndicationByPhysicalAsset.PhysicalAssetId = DeteriorationIndicationByPhysicalAssetXpo.PhysicalAssetId.Id
                DeteriorationIndicationByPhysicalAsset.StartTracking()
                DeteriorationIndicationByPhysicalAsset.MarkAsUnchanged()
                ListDeteriorationIndicationByPhysicalAsset.Add(DeteriorationIndicationByPhysicalAsset)
            Next
        End If

        'Se instancia el listado de accesorios por activo
        ListFixedAssetPhysicalAssetAccessory = New List(Of FixedAssetPhysicalAssetAccessory)
        'Si tiene asignado accesorios
        If FixedAssetPhysicalAssetXpo.FixedAssetPhysicalAssetAccessoryXpo?.Any() Then
            'Se recorre los accesorios
            For Each FixedAssetPhysicalAssetAccessoryXpo In FixedAssetPhysicalAssetXpo.FixedAssetPhysicalAssetAccessoryXpo
                Dim FixedAssetPhysicalAssetAccessory As New FixedAssetPhysicalAssetAccessory
                FixedAssetPhysicalAssetAccessory.Id = FixedAssetPhysicalAssetAccessoryXpo.Id
                FixedAssetPhysicalAssetAccessory.Description = FixedAssetPhysicalAssetAccessoryXpo.Description
                FixedAssetPhysicalAssetAccessory.Serie = FixedAssetPhysicalAssetAccessoryXpo.Serie
                FixedAssetPhysicalAssetAccessory.Quantity = FixedAssetPhysicalAssetAccessoryXpo.Quantity
                FixedAssetPhysicalAssetAccessory.Value = FixedAssetPhysicalAssetAccessoryXpo.Value
                FixedAssetPhysicalAssetAccessory.PhysicalAssetId = FixedAssetPhysicalAssetAccessoryXpo.PhysicalAssetId.Id
                FixedAssetPhysicalAssetAccessory.StartTracking()
                FixedAssetPhysicalAssetAccessory.MarkAsUnchanged()
                ListFixedAssetPhysicalAssetAccessory.Add(FixedAssetPhysicalAssetAccessory)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.FixedAssetPhysicalAsset IsNot Nothing AndAlso Me.FixedAssetPhysicalAsset.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDtxtPlate.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDtxtPlate.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetPhysicalAsset.ActionsOnControls
        Set(value As Boolean)

            INDlyPhysical.BeginUpdate()
            INDtxtPlate.Enabled = Not value
            INDsleSupplier.Enabled = value
            INDtxtSerie.Enabled = value
            INDtxtModel.Enabled = value
            INDsleTrademark.Enabled = value
            INDslePolicy.Enabled = value
            INDsleHandlesWarranty.Enabled = value
            INDdteWarrantyExpirationDate.Enabled = value
            INDdteInstallationDate.Enabled = value
            INDdtePurchaseDate.Enabled = value
            INDtxtEntryNumber.Enabled = value
            INDtxtVoucherTransactionNumber.Enabled = value
            INDtxtItem.Enabled = value
            INDtxtMainAccount.Enabled = value
            INDtxtLocation.Enabled = value
            INDtxtResponsible.Enabled = value
            INDtxtHistoricalValue.Enabled = value
            INDtxtFinancialDiscount.Enabled = value
            INDtxtNetHistoricalValue.Enabled = value
            INDtxtFairValue.Enabled = value
            INDtxtRecoverableValue.Enabled = value
            INDgcBooks.Enabled = value
            INDgcParts.Enabled = value
            INDgcDeteriorationIndications.Enabled = value
            INDPceDetailBooks.Enabled = value
            INDPceDeteriorationIndications.Enabled = value
            INDMeObservation.Enabled = value
            INDlyPhysical.EndUpdate()

            If value Then
                INDsleSupplier.Focus()
            Else
                INDtxtPlate.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Placa", .FieldName = "Plate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Responsable", .FieldName = "ResponsibleId.CodeNitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Ubicación", .FieldName = "LocationId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Articulo", .FieldName = "ItemId.CodeDescription", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Serie", .FieldName = "Serie", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Modelo", .FieldName = "Model", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Plate"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetPhysicalAsset
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDtxtPlate.Text = ReturnValue
        If INDtxtPlate.Text <> String.Empty Then
            LoadControls()
            If INDtxtPlate.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDtxtPlate.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetPhysicalAsset.Plate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetPhysicalAsset.Plate & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetPhysicalAsset.Plate),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetPhysicalAsset.Plate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetPhysicalAsset.Plate)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = "Activo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = "Inactivo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = "Devuelto", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()

        INDlyPhysical.BeginUpdate()
        ActionsOnControls = False
        Plate = String.Empty
        SupplierId = Nothing
        INDsleSupplier.Properties.NullText = String.Empty
        Serie = String.Empty
        ModelPhysical = String.Empty
        TrademarkId = Nothing
        INDsleTrademark.Properties.NullText = String.Empty
        INDMeObservation.EditValue = Nothing
        PolicyId = Nothing
        INDslePolicy.Properties.NullText = String.Empty
        HandlesWarranty = Nothing
        WarrantyExpirationDate = Nothing
        INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemWarrantyExpirationDate.AllowHide = True
        InstallationDate = Nothing
        PurchaseDate = Nothing
        EntryNumber = String.Empty
        VoucherTransactionNumber = String.Empty
        ItemDescription = String.Empty
        MainAccountDescription = String.Empty
        LocationDescription = String.Empty
        ResponsibleDescription = String.Empty
        HistoricalValue = Nothing
        NetHistoricalValue = Nothing
        FinancialDiscount = Nothing
        FairValue = Nothing
        RecoverableValue = Nothing
        FixedAssetPhysicalAsset = Nothing
        ListFixedAssetPhysicalAssetDetailBook = Nothing
        ListFixedAssetPhysicalAssetParts = Nothing
        ListDeteriorationIndicationByPhysicalAsset = Nothing
        ListFixedAssetPhysicalAssetAccessory = Nothing
        INDgcBooks.DataSource = Nothing
        INDgcParts.DataSource = Nothing
        INDgcPartsDetailBook.DataSource = Nothing
        INDgcDeteriorationIndications.DataSource = Nothing
        BarraBotones.CleanAuditBasic()
        INDlyPhysical.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDtxtPlate.Focus()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetPhysicalAsset

            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .SupplierId = SupplierId
            .Serie = Serie
            .Model = ModelPhysical
            .TrademarkId = TrademarkId
            .PolicyId = PolicyId
            .HandlesWarranty = HandlesWarranty
            .FairValue = FairValue
            .RecoverableValue = RecoverableValue
            .Observation = INDMeObservation.EditValue

            If INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .WarrantyExpirationDate = WarrantyExpirationDate
            Else
                .WarrantyExpirationDate = Nothing
            End If

            .InstallationDate = InstallationDate
            .PurchaseDate = PurchaseDate
            .EntryNumber = EntryNumber
            .VoucherTransactionNumber = VoucherTransactionNumber

			If ListFixedAssetPhysicalAssetDetailBook IsNot Nothing Then
				For Each item In ListFixedAssetPhysicalAssetDetailBook
					.FixedAssetPhysicalAssetDetailBook.Add(item)
				Next
			End If

			If ListDeteriorationIndicationByPhysicalAsset?.Any() Then
                For Each item In ListDeteriorationIndicationByPhysicalAsset
                    .DeteriorationIndicationByPhysicalAsset.Add(item)
                Next
            End If

            If ListFixedAssetPhysicalAssetAccessory?.Any() Then
                For Each item In ListFixedAssetPhysicalAssetAccessory
                    .FixedAssetPhysicalAssetAccessory.Add(item)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Try
            AsyncLoader(True)
            Using Model As New MFixedAssetPhysicalAsset(MyTag)
                Dim resultOperation = Await Model.GetFixedAssetPhysicalAssetByPlate(INDtxtPlate.Text.Trim)
                INDlyPhysical.BeginUpdate()
                FixedAssetPhysicalAsset = resultOperation.ObjectEmbbeded
                If Not FixedAssetPhysicalAsset Is Nothing Then
                    If FixedAssetPhysicalAsset.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetPhysicalAsset.Id))
                            With FixedAssetPhysicalAsset
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                'Se consulta los libros y las partes del activo
                                bwList.RunWorkerAsync()

                                'Campos que se pueden modificar
                                Plate = .Plate
                                SupplierId = .SupplierId
                                INDsleSupplier.Properties.NullText = .SupplierDescription
                                Serie = .Serie
                                ModelPhysical = .Model
                                INDMeObservation.EditValue = .Observation
                                TrademarkId = .TrademarkId
                                INDsleTrademark.Properties.NullText = .TrademarkDescription
                                PolicyId = .PolicyId
                                INDslePolicy.Properties.NullText = .PolicyDescription
                                HandlesWarranty = .HandlesWarranty
                                WarrantyExpirationDate = .WarrantyExpirationDate
                                InstallationDate = .InstallationDate
                                PurchaseDate = .PurchaseDate
                                EntryNumber = .EntryNumber
                                VoucherTransactionNumber = .VoucherTransactionNumber

                                'Campos que no se modifican
                                ItemDescription = .ItemDescription
                                MainAccountDescription = .MainAccountDescription
                                LocationDescription = .LocationDescription
                                ResponsibleDescription = .ResponsibleDescription
                                HistoricalValue = .HistoricalValue
                                FinancialDiscount = .FinancialDiscount
                                NetHistoricalValue = .NetHistoricalValue
                                FairValue = .FairValue
                                RecoverableValue = .RecoverableValue

                                'Se valida si el activo fue devuelto
                                If .OutputRefund Then
                                    BarraBotones.StatusRecord = "3"
                                Else
                                    If .HasOutput Then
                                        BarraBotones.StatusRecord = "2"
                                    Else
                                        BarraBotones.StatusRecord = "1"
                                    End If
                                End If
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetPhysicalAsset.Plate)
                            If result.Id = 0 Then
                                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                                state.State = Domain.Base.Entities.ObjectState.Added
                                record = New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetPhysicalAsset.Id}
                                Dim operation = Await ModelRecord.SaveBlockRecord(record)
                                record = operation.ObjectEmbbeded
                            Else
                                record = result
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                            End If

                            'Si es un comodato tercerizado o un renting operativo no tiene datos contables puesto que no contabiliza
                            If FixedAssetPhysicalAsset.AdquisitionType = 8 OrElse FixedAssetPhysicalAsset.AdquisitionType = 10 Then
                                INDlygBooks.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                INDlyItemMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                INDlyItemVoucherTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            Else
                                INDlygBooks.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDlyItemMainAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDlyItemVoucherTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            End If

                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                            Me.BarraBotones.SetDocuments(FixedAssetPhysicalAsset.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Plate = String.Empty
                        Deshacer()
                        INDtxtPlate.Focus()
                    End If
                Else
                    AsyncLoader(False)
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Plate = String.Empty
                    Deshacer()
                    INDtxtPlate.Focus()
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
			Throw ex
		End Try
        INDlyPhysical.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodo de customizacion a partir de la moneda
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat
        Me.INDtxtFinancialDiscount.Properties.Mask.Culture = _culture
        Me.INDtxtHistoricalValue.Properties.Mask.Culture = _culture
        Me.INDtxtNetHistoricalValue.Properties.Mask.Culture = _culture
        Me.INDtxtFairValue.Properties.Mask.Culture = _culture
        Me.INDtxtRecoverableValue.Properties.Mask.Culture = _culture


    End Sub


#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        FixedAssetPhysicalAsset = Nothing
        SearchMode = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        ListFixedAssetPhysicalAssetDetailBook = Nothing
        ListFixedAssetPhysicalAssetParts = Nothing
        ListDeteriorationIndicationByPhysicalAsset = Nothing
        ListFixedAssetPhysicalAssetAccessory = Nothing
        bwList = Nothing
        FixedAssetPhysicalAssetXpo = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmFixedAssetPhysicalAsset_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyPhysical, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetPhysicalAsset(Me)
        Presenter.LoadDefinitionLayout()
        IndigoGridControl1.RefreshGrid(INDgcBooks)
        IndigoGridControl1.RefreshGrid(INDgcParts)
        IndigoGridControl1.RefreshGrid(INDgcDeteriorationIndications)

        IndigoGridView1.SetListAcction(INDviewBooks, {eAcciones.Remove}.ToList())
        INDviewBooks.Columns.ColumnByName("colActions").Width = 100

        IndigoGridView2.SetListAcction(INDviewDeteriorationIndications, {eAcciones.Remove}.ToList())
        INDviewDeteriorationIndications.Columns.ColumnByName("colActions").Width = 100

        IndigoGridView3.SetListAcction(INDviewAccesories, {eAcciones.Remove}.ToList())
        INDviewAccesories.Columns.ColumnByName("colActions").Width = 100

        InitializeTuple()
        LoadStatus()
        Deshacer()
        SearchMode = False
        Await Presenter.GetSettingFixedAssetByOperatingUnitId(_idOperativeUnit)
        SetCurrencyUI(SettingsFixedAsset?.Currency?.Abbreviation)
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetPhysicalAsset_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtPlate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDtxtPlate.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If String.IsNullOrEmpty(INDtxtPlate.Text.Trim()) Then
                Mensaje(EeventViewerImages.Advertencia) = "Ingrese una placa"
                Exit Sub
            End If
            Await Me.LoadControls()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetPhysicalAsset_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDtxtPlate.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que abre el form de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(558, Nothing, True)
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de ingreso de activos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtEntryNumber_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtEntryNumber.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1116, EntryNumber, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de ingreso de activos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtItem_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtItem.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(572, FixedAssetPhysicalAsset.CodeItem, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de marca
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTrademark_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTrademark.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1700, Nothing, True)
            Presenter.InitializeTrademark()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el form de poliza
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePolicy_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePolicy.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1702, Nothing, True)
            Presenter.InitializePolicy()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Presenter.InitializeLegalBook()
        End If
    End Sub

    Private Sub INDsleDeteriorationIndication_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDeteriorationIndication.QueryPopUp
        If DeteriorationIndicationXpo Is Nothing Then
            Presenter.InitializeDeteriorationIndication()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al depslegar el control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplier.QueryPopUp
        If SupplierXpo Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de marca
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTrademark_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTrademark.QueryPopUp
        If TrademarkXpo Is Nothing Then
            Presenter.InitializeTrademark()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de poliza
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePolicy_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePolicy.QueryPopUp
        If PolicyXpo Is Nothing Then
            Presenter.InitializePolicy()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control del + en la rejilla de las partes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepPopupPartsDetailBook_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPopupPartsDetailBook.QueryPopUp
        Dim fixedAssetPhysicalAssetParts As FixedAssetPhysicalAssetParts = INDviewParts.GetFocusedRow
        INDgcPartsDetailBook.DataSource = Nothing

        If fixedAssetPhysicalAssetParts.FixedAssetPhysicalAssetPartsDetailBook?.Any() Then
            INDgcPartsDetailBook.DataSource = fixedAssetPhysicalAssetParts.FixedAssetPhysicalAssetPartsDetailBook.ToList
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleDepreciationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciationType.EditValueChanged
        If INDsleDepreciationType.EditValue IsNot Nothing Then
            If INDsleDepreciationType.EditValue = 4 Then 'Unidades producidas
                INDlyItemTotalProductionUnit.ShowLayout()
                INDlyItemPercentageRescue.HideLayout()

            ElseIf INDsleDepreciationType.EditValue = 3 Then 'Reduccion de saldos
                INDlyItemTotalProductionUnit.HideLayout()
                INDlyItemPercentageRescue.ShowLayout()

            Else
                INDlyItemTotalProductionUnit.HideLayout()
                INDlyItemPercentageRescue.HideLayout()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de maneja garantía
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleHandlesWarranty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesWarranty.EditValueChanged
        If HandlesWarranty IsNot Nothing Then
            If HandlesWarranty Then
                INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemWarrantyExpirationDate.AllowHide = False
            Else
                INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemWarrantyExpirationDate.AllowHide = True
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDtxtFairValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDtxtFairValue.EditValueChanging
        If e.NewValue IsNot Nothing Then
            If ListFixedAssetPhysicalAssetDetailBook IsNot Nothing Then
                Using model As New MFixedAssetPhysicalAsset(MyTag)
                    Dim listId = (From i In ListFixedAssetPhysicalAssetDetailBook Where i.LagalBookType = 2 Select i.Id).ToList()
                    If model.GetCountFixedAssetDepreciationDetailByFixedAssetPhysicalAssetDetailBookId(listId) > 0 Then
                        e.Cancel = True
                        Exit Sub
                    End If
                End Using
                For Each item In ListFixedAssetPhysicalAssetDetailBook.FindAll(Function(x) x.LagalBookType = 2)
                    item.ResidualValue = CDec(e.NewValue)
                    item.MarkAsModified()
                Next
                INDgcBooks.DataSource = ListFixedAssetPhysicalAssetDetailBook
                INDgcBooks.RefreshDataSource()
            End If
        End If
    End Sub

#End Region

#Region "CustomCoulmnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas de la rejilla de libros del activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDviewBooks_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewBooks.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If

        If e.Column.Name = INDcolUnitLifeTimeBook.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Año"
                Case 2
                    e.DisplayText = "Mes"
                Case 3
                    e.DisplayText = "Día"
                Case Else
                    e.DisplayText = String.Empty
            End Select
        End If

        Dim currencyColumns As String() = {INDcolValorization.Name, Me.INDcolDevaluation.Name, INDcolDepreciatedValue.Name, INDcolResidualValue.Name}
        If currencyColumns.Contains(e.Column.Name) Then
            Dim GridColumn = e.Column
            Dim rowHandle As Integer = e.ListSourceRowIndex

            If rowHandle >= 0 Then
                Dim rowData = INDviewBooks.GetRow(rowHandle)

                If rowData IsNot Nothing Then
                    Dim currencySymbol As String = rowData.LegalBook.Currency.Abbreviation
                    GridColumn = Window.Utils.FormatGrid(e.Column, currencySymbol)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas de la rejilla de libros de la parte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDviewPartsDetailBook_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewPartsDetailBook.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolUnitLifeTimePartsDetailBook.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Año"
                Case 2
                    e.DisplayText = "Mes"
                Case 3
                    e.DisplayText = "Día"
                Case Else
                    e.DisplayText = String.Empty
            End Select
        End If
    End Sub

#End Region

#Region "ShowingEditor"

    ''' <summary>
    ''' Evento que se dispara al tener contacto con alguna columna de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDviewParts_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDviewParts.ShowingEditor
        Dim pMouse As System.Drawing.Point = INDgcParts.PointToClient(Control.MousePosition)
        Dim hit = INDviewParts.CalcHitInfo(pMouse)
        If hit.Column IsNot Nothing AndAlso hit.Column.Name.Equals("INDcolMore") Then
            Dim fixedAssetPhysicalParts As FixedAssetPhysicalAssetParts = INDviewParts.GetFocusedRow()
            If fixedAssetPhysicalParts.DepreciatePart Then
                e.Cancel = False
            Else
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "Click"

	Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
		AddDetailsBook()
	End Sub

	Private Sub INDbtnAddDeteriorationIndication_Click(sender As Object, e As EventArgs) Handles INDbtnAddDeteriorationIndication.Click
        AddDeteriorationIndication()
    End Sub

    Private Sub INDbtnAddAccesory_Click(sender As Object, e As EventArgs) Handles INDbtnAddAccesory.Click
        AddAccesory()
    End Sub

#End Region

#Region "MenuContext"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim detail = DirectCast(INDviewBooks.GetFocusedRow(), FixedAssetPhysicalAssetDetailBook)
        If detail.Id > 0 Then
            Using model As New MFixedAssetPhysicalAsset(MyTag)
                If model.GetCountFixedAssetDepreciationDetailByFixedAssetPhysicalAssetDetailBookId(New List(Of Integer) From {detail.Id}) > 0 Then
                    Exit Sub
                End If
                FixedAssetPhysicalAsset.FixedAssetPhysicalAssetDetailBook.Add(detail.MarkAsDeleted())
            End Using
        End If
        ListFixedAssetPhysicalAssetDetailBook.Remove(detail)
        INDgcBooks.DataSource = ListFixedAssetPhysicalAssetDetailBook
        INDgcBooks.RefreshDataSource()
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Dim detail = DirectCast(INDviewDeteriorationIndications.GetFocusedRow(), DeteriorationIndicationByPhysicalAsset)
        If detail.Id > 0 Then
            FixedAssetPhysicalAsset.DeteriorationIndicationByPhysicalAsset.Add(detail.MarkAsDeleted())
        End If
        ListDeteriorationIndicationByPhysicalAsset.Remove(detail)
        INDgcDeteriorationIndications.DataSource = ListDeteriorationIndicationByPhysicalAsset
        INDgcDeteriorationIndications.RefreshDataSource()
    End Sub

    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        Dim detail = DirectCast(INDviewAccesories.GetFocusedRow(), FixedAssetPhysicalAssetAccessory)
        If detail.Id > 0 Then
            FixedAssetPhysicalAsset.FixedAssetPhysicalAssetAccessory.Add(detail.MarkAsDeleted())
        End If
        ListFixedAssetPhysicalAssetAccessory.Remove(detail)
        INDgcAccesories.DataSource = ListFixedAssetPhysicalAssetAccessory
        INDgcAccesories.RefreshDataSource()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDtxtPlate.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, FixedAssetPhysicalAsset.Id, 0, FixedAssetPhysicalAsset.Id, _idOperativeUnit)
    End Sub

#End Region

End Class