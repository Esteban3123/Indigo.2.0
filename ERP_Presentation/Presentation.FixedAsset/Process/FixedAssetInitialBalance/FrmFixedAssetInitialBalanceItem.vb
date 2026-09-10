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

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.FixedAsset.MVP

#End Region

Public Class FrmFixedAssetInitialBalanceItem
    Implements IFixedAssetInitialBalanceItem

#Region "Properties"

    Public Property AdquisitionType As Integer? Implements IFixedAssetInitialBalanceItem.AdquisitionType
        Get
            Return INDsleAdquisitionType.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdquisitionType.EditValue = value
        End Set
    End Property

    Public Property StatusAssetId As Integer? Implements IFixedAssetInitialBalanceItem.StatusAssetId
        Get
            Return INDsleStatusAsset.EditValue
        End Get
        Set(value As Integer?)
            INDsleStatusAsset.EditValue = value
        End Set
    End Property

    Public Property StatusAssetXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItem.StatusAssetXpo
        Get
            Return INDsleStatusAsset.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleStatusAsset.Properties.DataSource = value
        End Set
    End Property

    Public Property DepreciatedDays As Integer Implements IFixedAssetInitialBalanceItem.DepreciatedDays
        Get
            Return INDseDepreciatedDays.EditValue
        End Get
        Set(value As Integer)
            INDseDepreciatedDays.EditValue = value
        End Set
    End Property

    Public Property HistoricalValueInBook As Decimal Implements IFixedAssetInitialBalanceItem.HistoricalValueInBook
        Get
            Return INDtxtHistoricalValueInBook.EditValue
        End Get
        Set(value As Decimal)
            INDtxtHistoricalValueInBook.EditValue = value
        End Set
    End Property

    Public Property DepreciatedValue As Decimal Implements IFixedAssetInitialBalanceItem.DepreciatedValue
        Get
            Return INDtxtDepreciatedValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtDepreciatedValue.EditValue = value
        End Set
    End Property

    Public Property DepreciationTypeId As Integer? Implements IFixedAssetInitialBalanceItem.DepreciationTypeId
        Get
            Return INDsleDepreciationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleDepreciationType.EditValue = value
        End Set
    End Property

    Public Property LegalBookId As Integer? Implements IFixedAssetInitialBalanceItem.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    Public Property LegalBookXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItem.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    Public Property LifeTime As Integer Implements IFixedAssetInitialBalanceItem.LifeTime
        Get
            Return INDseLifeUtil.EditValue
        End Get
        Set(value As Integer)
            INDseLifeUtil.EditValue = value
        End Set
    End Property

    Public Property PercentageRescue As Decimal Implements IFixedAssetInitialBalanceItem.PercentageRescue
        Get
            Return INDsePercentageRescue.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageRescue.EditValue = value
        End Set
    End Property

    Public Property TotalProductionUnit As Long Implements IFixedAssetInitialBalanceItem.TotalProductionUnit
        Get
            Return INDseTotalProductionUnit.EditValue
        End Get
        Set(value As Long)
            INDseTotalProductionUnit.EditValue = value
        End Set
    End Property

    Public Property UnitLifeTimeId As Integer? Implements IFixedAssetInitialBalanceItem.UnitLifeTimeId
        Get
            Return INDsleUnitLifeUtil.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitLifeUtil.EditValue = value
        End Set
    End Property

    Public Property AdquisitionDate As Date? Implements IFixedAssetInitialBalanceItem.AdquisitionDate
        Get
            Return INDdteAdquisitionDate.EditValue
        End Get
        Set(value As Date?)
            INDdteAdquisitionDate.EditValue = value
        End Set
    End Property

    Public Property Depreciate As Boolean Implements IFixedAssetInitialBalanceItem.Depreciate
        Get
            Return INDsleDepreciate.EditValue
        End Get
        Set(value As Boolean)
            INDsleDepreciate.EditValue = value
        End Set
    End Property

    Public Property Amortize As Boolean Implements IFixedAssetInitialBalanceItem.Amortize
        Get
            Return INDsleAmortizes.EditValue
        End Get
        Set(value As Boolean)
            INDsleAmortizes.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el valor del parametro valida menor cuantía
    ''' </summary>
    ''' <returns></returns>
    Public Property ValidMinorAmount As Boolean? Implements IFixedAssetInitialBalanceItem.ValidMinorAmount
        Get
            Return INDsleValidMinorAmount.EditValue
        End Get
        Set(value As Boolean?)
            INDsleValidMinorAmount.EditValue = value
        End Set
    End Property

    Public Property FairValue As Decimal Implements IFixedAssetInitialBalanceItem.FairValue
        Get
            Return INDtxtFairValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtFairValue.EditValue = value
        End Set
    End Property

    Public Property HandlesWarranty As Boolean Implements IFixedAssetInitialBalanceItem.HandlesWarranty
        Get
            Return INDsleHandlesWarranty.EditValue
        End Get
        Set(value As Boolean)
            INDsleHandlesWarranty.EditValue = value
        End Set
    End Property

    Public Property HistoricalValue As Decimal Implements IFixedAssetInitialBalanceItem.HistoricalValue
        Get
            Return INDtxtHistoricalValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtHistoricalValue.EditValue = value
        End Set
    End Property

    Public Property ItemId As Integer? Implements IFixedAssetInitialBalanceItem.ItemId
        Get
            Return INDsleItem.EditValue
        End Get
		Set(value As Integer?)
			INDsleItem.EditValue = value
		End Set
	End Property

    Public Property ItemXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItem.ItemXpo
        Get
            Return INDsleItem.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleItem.Properties.DataSource = value
        End Set
    End Property

    Public Property LocationId As Integer? Implements IFixedAssetInitialBalanceItem.LocationId
        Get
            Return INDsleLocation.EditValue
        End Get
        Set(value As Integer?)
            INDsleLocation.EditValue = value
        End Set
    End Property

    Public Property LocationXpo As XPCollection Implements IFixedAssetInitialBalanceItem.LocationXpo
        Get
            Return INDsleLocation.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleLocation.Properties.DataSource = value
        End Set
    End Property

    Public Property ModelItem As String Implements IFixedAssetInitialBalanceItem.ModelItem
        Get
            Return INDtxtModel.EditValue
        End Get
        Set(value As String)
            INDtxtModel.EditValue = value
        End Set
    End Property

    Public Property Plate As String Implements IFixedAssetInitialBalanceItem.Plate
        Get
            Return INDtxtPlate.EditValue
        End Get
        Set(value As String)
            INDtxtPlate.EditValue = value
        End Set
    End Property

    Public Property PolicyId As Integer? Implements IFixedAssetInitialBalanceItem.PolicyId
        Get
            Return INDslePolicy.EditValue
        End Get
        Set(value As Integer?)
            INDslePolicy.EditValue = value
        End Set
    End Property

    Public Property PolicyXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItem.PolicyXpo
        Get
            Return INDslePolicy.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePolicy.Properties.DataSource = value
        End Set
    End Property

    Public Property ResponsibleId As Integer? Implements IFixedAssetInitialBalanceItem.ResponsibleId
        Get
            Return INDsleResponsible.EditValue
        End Get
        Set(value As Integer?)
            INDsleResponsible.EditValue = value
        End Set
    End Property

    Public Property ResponsibleXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItem.ResponsibleXpo
        Get
            Return INDsleResponsible.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleResponsible.Properties.DataSource = value
        End Set
    End Property

    Public Property Serie As String Implements IFixedAssetInitialBalanceItem.Serie
        Get
            Return INDtxtSerie.EditValue
        End Get
        Set(value As String)
            INDtxtSerie.EditValue = value
        End Set
    End Property

    Public Property SupplierId As Integer? Implements IFixedAssetInitialBalanceItem.SupplierId
        Get
            Return INDsleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplier.EditValue = value
        End Set
    End Property

    Public Property SupplierXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItem.SupplierXpo
        Get
            Return INDsleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

    Public Property TrademarkId As Integer? Implements IFixedAssetInitialBalanceItem.TrademarkId
        Get
            Return INDsleTrademark.EditValue
        End Get
        Set(value As Integer?)
            INDsleTrademark.EditValue = value
        End Set
    End Property

    Public Property TrademarkXpo As XPInstantFeedbackSource Implements IFixedAssetInitialBalanceItem.TrademarkXpo
        Get
            Return INDsleTrademark.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTrademark.Properties.DataSource = value
        End Set
    End Property

    Public Property WarrantyExpirationDate As Date? Implements IFixedAssetInitialBalanceItem.WarrantyExpirationDate
        Get
            Return INDdteWarrantyExpirationDate.EditValue
        End Get
        Set(value As Date?)
            INDdteWarrantyExpirationDate.EditValue = value
        End Set
    End Property

    Private _editMode As Boolean

    Public Property EditMode As Boolean
        Get
            Return _editMode
        End Get
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    Private _listCompare As List(Of FixedAssetInitialBalanceItem)

    Public Property ListCompare As List(Of FixedAssetInitialBalanceItem)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetInitialBalanceItem))
            _listCompare = value
        End Set
    End Property

    Private _fixedAssetInitialBalanceItem As FixedAssetInitialBalanceItem

    Public Property FixedAssetInitialBalanceItem As FixedAssetInitialBalanceItem
        Get
            Return _fixedAssetInitialBalanceItem
        End Get
        Set(value As FixedAssetInitialBalanceItem)
            _fixedAssetInitialBalanceItem = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene los parámetros de activos fijos
    ''' </summary>
    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset
        Get
            Return _SettingsFixedAsset
        End Get
        Set(value As SettingFixedAsset)
            _SettingsFixedAsset = value
        End Set
    End Property

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddFixedAssetInitialBalanceItemEventArgs(sender As Object, e As AddFixedAssetInitialBalanceItem)

#End Region

#Region "Globals"

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListAdquisitionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetInitialBalanceItem

    ''' <summary>
    ''' Listado de detalles de las partes
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts)

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetInitialBalanceItemParts As FixedAssetInitialBalanceItemParts

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItemParts As List(Of FixedAssetInitialBalanceItemParts)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItemPartsDetailBook As List(Of FixedAssetInitialBalanceItemPartsDetailBook)

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitLifeUtil As New List(Of Tuple(Of Integer, String))

    Dim ItemDepreciate As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListDepreciationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite saber si el registro esta en modo de edición
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagEditModePopupBook As Boolean = False

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook)

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetInitialBalanceItemDetailBook As FixedAssetInitialBalanceItemDetailBook

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetInitialBalanceItemDetailBook As List(Of FixedAssetInitialBalanceItemDetailBook)

    ''' <summary>
    ''' Permite saber si se puede cambiar el valor del search
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangedSearch As Boolean = True

    ''' <summary>
    ''' Listado de opciones para activo menor cuantía
    ''' </summary>
    Dim ListValidMinorAmount As New List(Of Tuple(Of Boolean, String))

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    Private Sub AddDetail()
        If ValidateControls() = False Then 'Se validan que los controles esten diligenciados
            Exit Sub
        End If

        If Depreciate Or Amortize Then
            Dim errors = ValidateBooksDetails()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
        End If

        'If EditMode = False Then 'Esta guardando el item
        If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then 'Se valida que el articulo no se encuentre en el form principal
            'Variable para saber si se debe validar dependiendo de las condiciones
            Dim banValidate As Boolean = False

            If EditMode = False Then 'Si está guardando
                banValidate = True
            Else 'Si está editando
                If Not (FixedAssetInitialBalanceItem.ItemId = ItemId AndAlso FixedAssetInitialBalanceItem.Plate = Plate AndAlso FixedAssetInitialBalanceItem.Serie = Serie) Then
                    banValidate = True
                End If
            End If

            If banValidate Then 'Si valida es porque cumple con las condiciones anteriores
                Dim cont = (From l In ListCompare Where l.ItemId = ItemId And l.Serie = Serie And l.Plate = Plate Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El articulo " + INDsleItem.Text + " ya se encuentra en el listado."
                    Exit Sub
                End If
            End If
        End If
        'End If

        'Se asigna los valores
        AssigningValues()

        'Se crea el objeto que se va a devolver
        Dim args As New AddFixedAssetInitialBalanceItem
        args.FixedAssetInitialBalanceItem = FixedAssetInitialBalanceItem
        args.ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = ListDeleteFixedAssetInitialBalanceItemPartsDetailBook
        args.ListDeleteFixedAssetInitialBalanceItemParts = ListDeleteFixedAssetInitialBalanceItemParts
        args.ListDeleteFixedAssetInitialBalanceItemDetailBook = ListDeleteFixedAssetInitialBalanceItemDetailBook
        args.EditMode = EditMode
        RaiseEvent AddFixedAssetInitialBalanceItemEventArgs(Nothing, args)
        CleanControls()
        INDsleItem.Focus()
    End Sub

    Private Sub AssigningValues()
        If EditMode = False Then 'Si se esta guardando el item se instancia el objeto
            FixedAssetInitialBalanceItem = New FixedAssetInitialBalanceItem
        End If
        With FixedAssetInitialBalanceItem
            .ItemId = ItemId
            .ItemCodeName = INDsleItem.Text
            .Serie = Serie
            .Plate = Plate
            .LocationId = LocationId
            .LocationCodeName = INDsleLocation.Text
            .ResponsibleId = ResponsibleId
            .ResponsibleCodeName = INDsleResponsible.Text
            .SupplierId = SupplierId
            .SupplierCodeName = INDsleSupplier.Text
            .HistoricalValue = HistoricalValue
            .FairValue = FairValue
            .TrademarkId = TrademarkId
            .TrademarkCodeName = INDsleTrademark.Text
            .Model = ModelItem
            .PolicyId = PolicyId
            .PolicyCodeName = INDslePolicy.Text
            .HandlesWarranty = HandlesWarranty
            If HandlesWarranty Then 'Si maneja garantía
                .WarrantyExpirationDate = WarrantyExpirationDate
            Else 'Si no maneja garantía
                .WarrantyExpirationDate = Nothing
            End If
            .AdquisitionDate = AdquisitionDate
            .Depreciate = Depreciate
            .Amortize = Amortize
            If Depreciate Then 'Si deprecia
                .ValidMinorAmount = ValidMinorAmount
                .Amortize = False
            Else 'Si no deprecia
                .ValidMinorAmount = Nothing
            End If
            If Amortize Then
                .ValidMinorAmount = Nothing
                .Depreciate = False
            End If
            .AdquisitionType = AdquisitionType
            .StatusAssetId = StatusAssetId
            .StatusAssetCodeName = INDsleStatusAsset.Text
            .Status = True
            .FixedAssetInitialBalanceItemParts.Clear()
            If ListFixedAssetInitialBalanceItemParts IsNot Nothing AndAlso ListFixedAssetInitialBalanceItemParts.Count > 0 Then
                For Each item In ListFixedAssetInitialBalanceItemParts
                    .FixedAssetInitialBalanceItemParts.Add(item)
                Next
            End If

            If INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si hay detalles de libros
                .FixedAssetInitialBalanceItemDetailBook.Clear()
                If ListFixedAssetInitialBalanceItemDetailBook IsNot Nothing AndAlso ListFixedAssetInitialBalanceItemDetailBook.Count > 0 Then
                    For Each item In ListFixedAssetInitialBalanceItemDetailBook
                        .FixedAssetInitialBalanceItemDetailBook.Add(item)
                    Next
                End If
            Else 'Si no hay detalles de libros
                If .FixedAssetInitialBalanceItemDetailBook IsNot Nothing AndAlso .FixedAssetInitialBalanceItemDetailBook.Count > 0 Then
                    For Each item In (From l In .FixedAssetInitialBalanceItemDetailBook Where l.Id > 0 Select l).ToList
                        If ListDeleteFixedAssetInitialBalanceItemDetailBook Is Nothing Then
                            ListDeleteFixedAssetInitialBalanceItemDetailBook = New List(Of FixedAssetInitialBalanceItemDetailBook)
                        Else
                            If ListDeleteFixedAssetInitialBalanceItemDetailBook.Contains(item) Then
                                Continue For
                            End If
                        End If
                        ListDeleteFixedAssetInitialBalanceItemDetailBook.Add(item)
                    Next
                    .FixedAssetInitialBalanceItemDetailBook.Clear()
                End If
            End If

        End With
    End Sub


    ''' <summary>
    ''' Función que valida el diligenciamiento de los detalles de los libros
    ''' </summary>
    ''' <returns></returns>
    Function ValidateBooksDetails() As String

        Dim errors As New StringBuilder
        For Each itemDetail In ListFixedAssetInitialBalanceItemDetailBook
            If itemDetail.DepreciatedDays = Nothing OrElse itemDetail.DepreciatedDays = 0 Then
                errors.AppendLine($"Debe ingresar los días depreciados para el libro: {itemDetail.LegalBookCodeName}")
            End If
            If itemDetail.HistoricalValue = Nothing OrElse itemDetail.HistoricalValue = 0 Then
                errors.AppendLine($"Debe ingresar un valor histórico del Activo para el libro: {itemDetail.LegalBookCodeName}")
            End If
            If itemDetail.DepreciatedValue = Nothing OrElse itemDetail.DepreciatedValue = 0 Then
                errors.AppendLine($"Debe ingresar un valor depreciado para el libro: {itemDetail.LegalBookCodeName}")
            End If
        Next
        Return errors.ToString
    End Function

	Private Sub CleanControls()
		FixedAssetInitialBalanceItem = Nothing
		EditMode = False
		ItemId = Nothing
		INDsleItem.Properties.NullText = String.Empty
		Serie = Nothing
		Plate = Nothing
		LocationId = Nothing
		INDsleLocation.Properties.NullText = String.Empty
		ResponsibleId = Nothing
		INDsleResponsible.Properties.NullText = String.Empty
		SupplierId = Nothing
		INDsleSupplier.Properties.NullText = String.Empty
		HistoricalValue = Nothing
		FairValue = Nothing
		TrademarkId = Nothing
		INDsleTrademark.Properties.NullText = String.Empty
		ModelItem = Nothing
		PolicyId = Nothing
		INDslePolicy.Properties.NullText = String.Empty
		HandlesWarranty = Nothing
		WarrantyExpirationDate = Nothing
		INDlyItemWarrantyExpirationDate.AllowHide = True
		INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
		AdquisitionDate = Nothing
		AdquisitionType = Nothing
		Depreciate = Nothing
		Amortize = Nothing
		ValidMinorAmount = Nothing
		ItemDepreciate = False
		StatusAssetId = Nothing
		INDsleStatusAsset.Properties.NullText = String.Empty
		INDgcParts.DataSource = Nothing
		ListFixedAssetInitialBalanceItemParts = Nothing
		INDsleItem.Properties.ReadOnly = False
		INDgcDetailsBook.DataSource = Nothing
		ListFixedAssetInitialBalanceItemDetailBook = Nothing
		ListDeleteFixedAssetInitialBalanceItemDetailBook = Nothing
		ListDeleteFixedAssetInitialBalanceItemParts = Nothing
		ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = Nothing
		INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
		INDsleDepreciate.Properties.ReadOnly = False
	End Sub

	Private Sub LoadControls()
        With FixedAssetInitialBalanceItem
            FlagChangedSearch = False
            ItemId = .ItemId
            INDsleItem.Properties.NullText = .ItemCodeName
            FlagChangedSearch = True
            Serie = .Serie
            Plate = .Plate
            LocationId = .LocationId
            INDsleLocation.Properties.NullText = .LocationCodeName
            ResponsibleId = .ResponsibleId
            INDsleResponsible.Properties.NullText = .ResponsibleCodeName
            SupplierId = .SupplierId
            INDsleSupplier.Properties.NullText = .SupplierCodeName
            HistoricalValue = .HistoricalValue
            FairValue = .FairValue
            TrademarkId = .TrademarkId
            INDsleTrademark.Properties.NullText = .TrademarkCodeName
            ModelItem = .Model
            PolicyId = .PolicyId
            INDslePolicy.Properties.NullText = .PolicyCodeName
            HandlesWarranty = .HandlesWarranty
            If HandlesWarranty Then
                WarrantyExpirationDate = .WarrantyExpirationDate
            Else
                WarrantyExpirationDate = Nothing
            End If
            AdquisitionDate = .AdquisitionDate
            Depreciate = .Depreciate
            If Depreciate Then
				ValidMinorAmount = .ValidMinorAmount
				INDlyItemValidMinorAmount.HideControl(False)
				INDsleDepreciate.Properties.ReadOnly = False
			Else
				ValidMinorAmount = Nothing
				INDlyItemValidMinorAmount.HideControl
				INDsleDepreciate.Properties.ReadOnly = True
			End If
            ItemDepreciate = .Depreciate
            AdquisitionType = .AdquisitionType
            StatusAssetId = .StatusAssetId
            INDsleStatusAsset.Properties.NullText = .StatusAssetCodeName
            Amortize = .Amortize

            If Amortize = False Then
                INDsleAmortizes.Properties.ReadOnly = True
            Else
                INDsleAmortizes.Properties.ReadOnly = False
            End If
            ListFixedAssetInitialBalanceItemParts = .FixedAssetInitialBalanceItemParts.ToList()
            INDgcParts.DataSource = Nothing
            INDgcParts.DataSource = ListFixedAssetInitialBalanceItemParts

            If .Depreciate Or .Amortize Then
                ListFixedAssetInitialBalanceItemDetailBook = .FixedAssetInitialBalanceItemDetailBook.ToList
                INDgcDetailsBook.DataSource = Nothing
                INDgcDetailsBook.DataSource = ListFixedAssetInitialBalanceItemDetailBook
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If

            If .Amortize = False And INDsleAmortizes.Properties.ReadOnly = False Then
                If ListFixedAssetInitialBalanceItemDetailBook IsNot Nothing AndAlso ListFixedAssetInitialBalanceItemDetailBook.Count = 0 Then
                    INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End If

        End With
        ShowHideAmortizationByAssetType()
        INDsleItem.Properties.ReadOnly = True
    End Sub

    Private Sub OpenFormParts(_FlagModeEdit As Boolean)
        Using formulario As New FrmFixedAssetInitialBalanceItemParts
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddFixedAssetInitialBalanceItemPartsEventArgs, AddressOf ReturnAddPartsEventArgs
            formulario.EditMode = _FlagModeEdit
            formulario.ListCompare = ListFixedAssetInitialBalanceItemParts
            formulario.FixedAssetInitialBalanceItemParts = FixedAssetInitialBalanceItemParts
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.6
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ReturnAddPartsEventArgs(sender As Object, e As AddFixedAssetInitialBalanceItemParts)
        If e.EditMode = False Then 'Se esta ingresando un articulo
            If ListFixedAssetInitialBalanceItemParts Is Nothing Then
                ListFixedAssetInitialBalanceItemParts = New List(Of FixedAssetInitialBalanceItemParts)
            End If
            ListFixedAssetInitialBalanceItemParts.Add(e.FixedAssetInitialBalanceItemParts)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Se esta modificando un articulo
            ListFixedAssetInitialBalanceItemParts.Remove(FixedAssetInitialBalanceItemParts)
            ListFixedAssetInitialBalanceItemParts.Insert(IndexEditRecord, e.FixedAssetInitialBalanceItemParts)
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        If e.ListDeleteFixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing Then
            If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = New List(Of FixedAssetInitialBalanceItemPartsDetailBook)
            End If
            ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.AddRange(e.ListDeleteFixedAssetInitialBalanceItemPartsDetailBook)
        End If

        INDgcParts.DataSource = Nothing
        INDgcParts.DataSource = ListFixedAssetInitialBalanceItemParts
    End Sub

    Private Sub EditPart()
        FixedAssetInitialBalanceItemParts = DirectCast(ViewParts.GetFocusedRow(), FixedAssetInitialBalanceItemParts)
        IndexEditRecord = ListFixedAssetInitialBalanceItemParts.IndexOf(FixedAssetInitialBalanceItemParts)
        OpenFormParts(True)
    End Sub

    Private Sub RemovePart()
        FixedAssetInitialBalanceItemParts = CType(ViewParts.GetFocusedRow, FixedAssetInitialBalanceItemParts)
        ListFixedAssetInitialBalanceItemParts.Remove(FixedAssetInitialBalanceItemParts)

        If FixedAssetInitialBalanceItemParts.Id > 0 Then
            If ListDeleteFixedAssetInitialBalanceItemParts Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItemParts = New List(Of FixedAssetInitialBalanceItemParts)
            End If
            ListDeleteFixedAssetInitialBalanceItemParts.Add(FixedAssetInitialBalanceItemParts)

            'Recorro los libros de las partes si hay y las elimino
            If FixedAssetInitialBalanceItemParts.FixedAssetInitialBalanceItemPartsDetailBook IsNot Nothing AndAlso FixedAssetInitialBalanceItemParts.FixedAssetInitialBalanceItemPartsDetailBook.Count > 0 Then
                For Each item In (From l In FixedAssetInitialBalanceItemParts.FixedAssetInitialBalanceItemPartsDetailBook Where l.Id > 0 Select l).ToList
                    If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook Is Nothing Then
                        ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = New List(Of FixedAssetInitialBalanceItemPartsDetailBook)
                    End If
                    If ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Contains(item) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetInitialBalanceItemPartsDetailBook.Add(item)
                Next
            End If
        End If

        INDgcParts.DataSource = Nothing
        INDgcParts.DataSource = ListFixedAssetInitialBalanceItemParts
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
        'Tipo de adquisición
        InitializeAdquisitionType()
        'Activo menor cuantía
        ListValidMinorAmount = New List(Of Tuple(Of Boolean, String))
        ListValidMinorAmount.Add(New Tuple(Of Boolean, String)(True, "Deprecia como Menor Cuantía"))
        ListValidMinorAmount.Add(New Tuple(Of Boolean, String)(False, "Deprecia según Vida Útil"))
        INDsleValidMinorAmount.Properties.DataSource = ListValidMinorAmount.ToList
    End Sub

    Private Sub CleanControlsPopup()
        FlagEditModePopupBook = False
        LegalBookId = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        LifeTime = Nothing
        UnitLifeTimeId = Nothing
        DepreciatedDays = Nothing
        DepreciationTypeId = Nothing
        TotalProductionUnit = Nothing
        PercentageRescue = Nothing
        HistoricalValueInBook = Nothing
        DepreciatedValue = Nothing
        INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleLegalBook.Properties.ReadOnly = False
        INDpceDetailsBook.Properties.ReadOnly = True
    End Sub

    Private Sub AddDetailsBook()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            If ListFixedAssetInitialBalanceItemDetailBook Is Nothing Then
                ListFixedAssetInitialBalanceItemDetailBook = New List(Of FixedAssetInitialBalanceItemDetailBook)
            Else
                'Se valida que el libro que se esta agregando no exista en la rejilla
                Dim cont = (From l In ListFixedAssetInitialBalanceItemDetailBook Where l.LegalBookId = LegalBookId Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    Exit Sub
                End If
            End If
        End If

        'Se asigna los valores de la entidad
        SetValues()

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            ListFixedAssetInitialBalanceItemDetailBook.Add(FixedAssetInitialBalanceItemDetailBook)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta modificando el registro
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetInitialBalanceItemDetailBook
        CleanControlsPopup()
        'INDsleLegalBook.Focus()
        INDpceDetailsBook.ClosePopup()
    End Sub

    Private Sub SetValues()
        If FlagEditModePopupBook = False Then
            FixedAssetInitialBalanceItemDetailBook = New FixedAssetInitialBalanceItemDetailBook
        End If
        With FixedAssetInitialBalanceItemDetailBook
            .LegalBookId = LegalBookId
            .LegalBookCodeName = INDsleLegalBook.Text
            .LifeTime = LifeTime
            .UnitLifeTime = UnitLifeTimeId
            .DaysPendingDepreciate = IIf(UnitLifeTimeId = 1, LifeTime * 360, IIf(UnitLifeTimeId = 2, LifeTime * 30, LifeTime)) - DepreciatedDays
            .DepreciatedDays = DepreciatedDays
            .DepreciationType = DepreciationTypeId
            If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TotalProductionUnit = TotalProductionUnit
            Else
                .TotalProductionUnit = 0
            End If
            If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PercentageRescue = PercentageRescue
            Else
                .PercentageRescue = 0
            End If
            .DepreciatedValue = DepreciatedValue
            .DepreciatedValuePart = 0
            .ResidualValue = HistoricalValueInBook - .DepreciatedValue
            .ResidualValuePart = 0
            .HistoricalValue = HistoricalValueInBook
        End With
    End Sub

    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If LegalBookId Is Nothing Then
            errors.AppendLine("Debe seleccionar un libro oficial.")
        End If
        If LifeTime = Nothing OrElse LifeTime = 0 Then
            errors.AppendLine("Debe seleccionar una vida útil.")
        End If
        If UnitLifeTimeId Is Nothing Then
            errors.AppendLine("Debe seleccionar una unidad de vida útil.")
        End If
        If DepreciatedDays = Nothing OrElse DepreciatedDays = 0 Then
            errors.AppendLine("Debe ingresar los días depreciados.")
        End If
        If HistoricalValueInBook = Nothing OrElse HistoricalValueInBook = 0 Then
            errors.AppendLine("Debe ingresar un valor histórico del Activo para el libro.")
        End If
        If DepreciatedValue = Nothing OrElse DepreciatedValue = 0 Then
            errors.AppendLine("Debe ingresar un valor depreciado.")
        End If
        If DepreciationTypeId Is Nothing Then
            errors.AppendLine("Debe ingresar un tipo de depreciación.")
        End If
        If INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If TotalProductionUnit = Nothing OrElse TotalProductionUnit = 0 Then
                errors.AppendLine("Debe ingresar total de unidades producidas.")
            End If
        End If
        If INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If PercentageRescue = Nothing Then
                errors.AppendLine("Debe ingresar un % de salvamento.")
            End If
        End If
        Return errors.ToString
    End Function

    Private Async Sub EditBook()
        Try
            'Se valida que antes de editar el libro se ingrese el valor historico ya que con este valor se realiza la operación para el valor residual
            If HistoricalValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Antes de editar el libro debe ingresar un valor histórico"
                Exit Sub
            End If
            FlagEditModePopupBook = True
            FixedAssetInitialBalanceItemDetailBook = DirectCast(ViewDetailsBook.GetFocusedRow(), FixedAssetInitialBalanceItemDetailBook)
            With FixedAssetInitialBalanceItemDetailBook
                LegalBookId = .LegalBookId
                INDsleLegalBook.Properties.NullText = .LegalBookCodeName
                LifeTime = .LifeTime
                UnitLifeTimeId = .UnitLifeTime
                DepreciatedDays = .DepreciatedDays
                DepreciationTypeId = .DepreciationType
                HistoricalValueInBook = .HistoricalValue
                DepreciatedValue = .DepreciatedValue
                TotalProductionUnit = .TotalProductionUnit
                PercentageRescue = .PercentageRescue
            End With

            Dim LegalBook = Await Presenter.GetLegalBookById(FixedAssetInitialBalanceItemDetailBook.LegalBookId)
            SetCurrencyUI(LegalBook?.CommonCurrency?.Abbreviation)

            INDsleLegalBook.Properties.ReadOnly = True
            INDpceDetailsBook.ShowPopup()

        Catch ex As Exception
			Throw
		End Try
    End Sub

    Private Sub RemoveBook()
        FixedAssetInitialBalanceItemDetailBook = CType(ViewDetailsBook.GetFocusedRow, FixedAssetInitialBalanceItemDetailBook)
        ListFixedAssetInitialBalanceItemDetailBook.Remove(FixedAssetInitialBalanceItemDetailBook)

        If FixedAssetInitialBalanceItemDetailBook.Id > 0 Then
            If ListDeleteFixedAssetInitialBalanceItemDetailBook Is Nothing Then
                ListDeleteFixedAssetInitialBalanceItemDetailBook = New List(Of FixedAssetInitialBalanceItemDetailBook)
            End If
            ListDeleteFixedAssetInitialBalanceItemDetailBook.Add(FixedAssetInitialBalanceItemDetailBook)
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetInitialBalanceItemDetailBook
    End Sub

    ''' <summary>
    ''' Obtiene la informacion detallada del articulo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Private Function GetFixedAssetItemById(Id As Integer) As FixedAssetEquipmentXpo
        Dim itemXpo = Presenter.GetFixedAssetItemById(Id)
        If itemXpo IsNot Nothing AndAlso itemXpo.Count > 0 Then
            Dim EntityXpo As FixedAssetEquipmentXpo = itemXpo(0)
            Return EntityXpo
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Validar si el articulo tiene parametrizado en catalago de articulos la asociacion de la clasificación en "Intangibles" 
    ''' </summary>
    Private Sub ShowHideAmortizationByAssetType(Optional ItemObject As FixedAssetEquipmentXpo = Nothing)
        If ItemObject Is Nothing Then
            ItemObject = GetFixedAssetItemById(ItemId)
        End If
        INDlyItemAmortizes.HideControl(True)
		INDlyItemDepreciate.HideControl(True)
		If ItemObject IsNot Nothing Then
            Dim ClassificationIntangible = ItemObject.ItemCatalogId?.Classification
            If ClassificationIntangible IsNot Nothing AndAlso ClassificationIntangible = 2 Then
                INDlyItemAmortizes.HideControl(False)
                INDlyItemValidMinorAmount.HideControl(True)
                ValidMinorAmount = Nothing
            Else
                INDlyItemDepreciate.HideControl(False)
            End If
        End If
    End Sub


#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListAdquisitionType = Nothing
        Presenter = Nothing
        ListFixedAssetInitialBalanceItemParts = Nothing
        FixedAssetInitialBalanceItemParts = Nothing
        IndexEditRecord = Nothing
        ListDeleteFixedAssetInitialBalanceItemParts = Nothing
        ListDeleteFixedAssetInitialBalanceItemPartsDetailBook = Nothing
        ListUnitLifeUtil = Nothing
        ItemDepreciate = Nothing
        ListDepreciationType = Nothing
        FlagEditModePopupBook = Nothing
        ListFixedAssetInitialBalanceItemDetailBook = Nothing
        FixedAssetInitialBalanceItemDetailBook = Nothing
        ListDeleteFixedAssetInitialBalanceItemDetailBook = Nothing
        FlagChangedSearch = Nothing
        ListValidMinorAmount = Nothing
    End Sub

    Private Sub FrmFixedAssetInitialBalanceItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTuple()
        Presenter = New PFixedAssetInitialBalanceItem(Me)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        IndigoGridControl1.RefreshGrid(INDgcParts)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(ViewParts, ListActions)

        IndigoGridControl1.RefreshGrid(INDgcDetailsBook)
        Dim ListActionsBook As New List(Of eAcciones)
        ListActionsBook.Add(eAcciones.Remove)
        ListActionsBook.Add(eAcciones.Edit)
        IndigoGridView2.SetListAcction(ViewDetailsBook, ListActionsBook)
		INDlyItemValidMinorAmount.HideControl
		If EditMode Then 'Si esta en modo edición
            LoadControls()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleItem.QueryPopUp
        If ItemXpo Is Nothing Then
            Presenter.InitializeItem()
        End If
    End Sub

    Private Sub INDsleTrademark_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleTrademark.QueryPopUp
        If TrademarkXpo Is Nothing Then
            Presenter.InitializeTrademark()
        End If
    End Sub

    Private Sub INDsleResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleResponsible.QueryPopUp
        If ResponsibleXpo Is Nothing Then
            Presenter.InitializeResponsible()
        End If
    End Sub

    Private Sub INDsleSupplier_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleSupplier.QueryPopUp
        If SupplierXpo Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    Private Sub INDslePolicy_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslePolicy.QueryPopUp
        If PolicyXpo Is Nothing Then
            Presenter.InitializePolicy()
        End If
    End Sub

    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Presenter.InitializeLegalBook()
        End If
    End Sub

    Private Sub INDsleStatusAsset_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleStatusAsset.QueryPopUp
        If StatusAssetXpo Is Nothing Then
            Presenter.InitializeStatusAsset()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleItem_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleItem.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(572, Nothing, True)
            Presenter.InitializeItem()
        End If
    End Sub

    Private Sub INDsleTrademark_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTrademark.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1700, Nothing, True)
            Presenter.InitializeTrademark()
        End If
    End Sub

    Private Sub INDsleResponsible_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleResponsible.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1711, Nothing, True)
            Presenter.InitializeResponsible()
        End If
    End Sub

    Private Sub INDsleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(558, Nothing, True)
            Presenter.InitializeSupplier()
        End If
    End Sub

    Private Sub INDslePolicy_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePolicy.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1702, Nothing, True)
            Presenter.InitializePolicy()
        End If
    End Sub

    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Presenter.InitializeLegalBook()
        End If
    End Sub

    Private Sub INDsleStatusAsset_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleStatusAsset.ButtonClick

    End Sub

#End Region

#Region "Shown"

    Private Sub FrmFixedAssetInitialBalanceItem_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleItem.Focus()
        Presenter.InitializeLocation()
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleAdquisitionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdquisitionType.EditValueChanged
		If AdquisitionType IsNot Nothing AndAlso (AdquisitionType = 8 Or AdquisitionType = 10) Then
			INDsleDepreciate.EditValue = False
			INDsleDepreciate.Properties.ReadOnly = True
		Else
			If Depreciate Then
				INDsleDepreciate.EditValue = ItemDepreciate
				INDsleDepreciate.Properties.ReadOnly = False
			End If
		End If
    End Sub

    Private Sub INDsleDepreciationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciationType.EditValueChanged
        If DepreciationTypeId IsNot Nothing Then
            If DepreciationTypeId = 4 Then 'Unidades producidas
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf DepreciationTypeId = 3 Then 'Reduccion de saldos
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

    Private Sub INDsleHandlesWarranty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesWarranty.EditValueChanged
        If HandlesWarranty = True Then 'Si maneja garantía
            INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemWarrantyExpirationDate.AllowHide = False
        ElseIf HandlesWarranty = False Then 'Si no maneja garantía
            INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemWarrantyExpirationDate.AllowHide = True
            INDdteWarrantyExpirationDate.EditValue = Nothing
        End If
    End Sub

    Private Sub INDsleDepreciate_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciate.EditValueChanged
        If Depreciate Then 'Si deprecia muestra el grupo de los libros
            INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else 'Si no deprecia no muestra el grupo de los libros
            INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        If Depreciate And HistoricalValue <= SettingsFixedAsset.TopMinorValue Then
            INDlyItemValidMinorAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemValidMinorAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ValidMinorAmount = Nothing
        End If
    End Sub

    Private Sub INDsleItem_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleItem.EditValueChanged
        If ItemId IsNot Nothing AndAlso FlagChangedSearch = True Then
            ValidateDepreciatedAmortizesItem()
        End If
        ValidateArticleCategory()
    End Sub

    Private Sub INDsleAmortizes_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAmortizes.EditValueChanged
        If Amortize Then
            INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Establece el formato moneda en los controles del formulario
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_currencyAbbreviation As String)

        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = _currencyAbbreviation.GetNumberFormat

        Me.INDtxtHistoricalValueInBook.Properties.Mask.Culture = _culture
        Me.INDtxtDepreciatedValue.Properties.Mask.Culture = _culture
    End Sub
    ''' <summary>
    ''' Establece el articulo cuyo catálogo de este definido como "Bienes Controlables"
    ''' </summary>
    Private Sub ValidateArticleCategory()
		' Obtener el artículo por id
		If ItemId IsNot Nothing Then
			Dim itemXpo = GetFixedAssetItemById(ItemId)
			If itemXpo IsNot Nothing Then
				' Obtener la clasificación desde el artículo
				Dim classification As Integer? = itemXpo.ItemCatalogId?.Classification
				' Llamar al método para inicializar los tipos de adquisición con la clasificación
				InitializeAdquisitionType(classification)
			End If
		End If
	End Sub
    ''' <summary>
    ''' Oculta o muestra el tipo de adquisición de acuerdo a la clasificacion del articulo
    ''' </summary>
    ''' <param name="classification"></param>
    Private Sub InitializeAdquisitionType(Optional ByVal classification As Integer? = Nothing)
        ' Inicializa la lista de tipos de adquisición
        ListAdquisitionType = New List(Of Tuple(Of Integer, String))

        ' Verificar si la clasificación es igual a 3 o no
        If classification.HasValue AndAlso classification.Value = 3 Then
            ' Si Classification es 3, solo agregar "Compra Directa"
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
        Else
            ' Si Classification no es 3 o es Nothing, agregar todas las opciones
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(1, "Compra Directa"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(3, "Comodato"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(8, "Comodato Tercerizado"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(4, "Donación"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(5, "Traspaso de Bienes"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(6, "Otro Concepto"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(7, "Leasing Financiero"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(9, "Renting Financiero"))
            ListAdquisitionType.Add(New Tuple(Of Integer, String)(10, "Renting Operativo"))
        End If

        INDsleAdquisitionType.Properties.DataSource = ListAdquisitionType.ToList()
    End Sub


    Private Sub ValidateDepreciatedAmortizesItem()
        'Se obtiene el articulo por id
        Dim itemXpo = GetFixedAssetItemById(ItemId) '' Presenter.GetFixedAssetItemById(ItemId)
        If itemXpo IsNot Nothing Then 'Si viene con datos
            'Se pasa a una entidad xpo para que sea mas facil manipularlo
            Dim EntityXpo = itemXpo
            'Se asigna el mismo valor
            Depreciate = EntityXpo.AllowDepreciate
            ItemDepreciate = EntityXpo.AllowDepreciate
            Amortize = EntityXpo.Amortizes
            ''Validacion  deprecia/amortiza
            ShowHideAmortizationByAssetType(EntityXpo)
            If Amortize = False Then
                INDsleAmortizes.Properties.ReadOnly = True
            Else
                INDsleAmortizes.Properties.ReadOnly = False
            End If
            If EntityXpo.AllowDepreciate Or EntityXpo.Amortizes Then 'Permite depreciar/Amortizar
                'Si deprecia postulo el mismo valor que trae el articulo y permito cambiar el del form
                INDsleDepreciate.Properties.ReadOnly = False
                INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'Se crea las entidades que van en la rejilla del libro del articulo de este form
                If EntityXpo.FixedAssetItemDetailXpo IsNot Nothing AndAlso EntityXpo.FixedAssetItemDetailXpo.Count > 0 Then
                    ListFixedAssetInitialBalanceItemDetailBook = New List(Of FixedAssetInitialBalanceItemDetailBook)
                    For Each item As FixedAssetItemDetailXpo In EntityXpo.FixedAssetItemDetailXpo
                        Dim _fixedAssetInitialBalanceItemDetailBook As New FixedAssetInitialBalanceItemDetailBook
                        With _fixedAssetInitialBalanceItemDetailBook
                            .LegalBookId = item.LegalBookId.Id
                            .LegalBookCodeName = item.LegalBookId.CodeName
                            .LifeTime = item.LifeTime
                            .UnitLifeTime = item.UnitLifeTime
                            .DaysPendingDepreciate = 0
                            .DepreciatedDays = 0
                            .DepreciationType = item.DepreciationType
                            .TotalProductionUnit = item.TotalProductionUnit
                            .PercentageRescue = item.PercentageRescue
                            .DepreciatedValue = 0
                            .DepreciatedValuePart = 0
                            .ResidualValue = 0
                            .ResidualValuePart = 0
                            .HistoricalValue = HistoricalValue
                        End With
                        ListFixedAssetInitialBalanceItemDetailBook.Add(_fixedAssetInitialBalanceItemDetailBook)
                    Next
                    INDgcDetailsBook.DataSource = Nothing
                    INDgcDetailsBook.DataSource = ListFixedAssetInitialBalanceItemDetailBook
                Else 'Si el articulo deprecia pero no tiene detalles contables
                    Mensaje(EeventViewerImages.Advertencia) = "El articulo " + INDsleItem.Text.Trim + " no se puede seleccionar porque permite depreciar/amortizar y no tiene detalles contables."
                    Depreciate = False
                    ListFixedAssetInitialBalanceItemDetailBook = Nothing
                    INDgcDetailsBook.DataSource = Nothing
                    INDsleItem.Properties.NullText = String.Empty
                    ItemId = Nothing
                End If
            Else 'No permite depreciar
                'Si no deprecia postulo el mismo valor que trae el articulo y no permito cambiar el del form
                INDsleDepreciate.Properties.ReadOnly = True
            End If
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAddItem_Click(sender As Object, e As EventArgs) Handles INDbtnAddItem.Click
        AddDetail()
    End Sub

    Private Sub INDbtnAddParts_Click(sender As Object, e As EventArgs) Handles INDbtnAddParts.Click
        FixedAssetInitialBalanceItemParts = Nothing
        OpenFormParts(False)
    End Sub

    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        AddDetailsBook()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetInitialBalanceItem_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
        End If
    End Sub

#End Region

#Region "MenuContext"

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditPart()
            Case "Remove"
                RemovePart()
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        INDpceDetailsBook.Properties.ReadOnly = False
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditBook()
            Case "Remove"
                RemoveBook()
        End Select
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    Private Sub ViewDetailsBook_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles ViewDetailsBook.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolUnitLifeTime.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Año"
                Case 2
                    e.DisplayText = "Mes"
                Case 3
                    e.DisplayText = "Día"
                Case Else

            End Select
        End If
        If e.Column.Name = INDcolDepreciationType.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Línea Recta"
                Case 2
                    e.DisplayText = "Suma de Dígitos"
                Case 3
                    e.DisplayText = "Reducción de Saldos"
                Case 4
                    e.DisplayText = "Unidades de Producción"
                Case Else

            End Select
        End If
    End Sub

#End Region

#Region "Popup"

    Private Sub INDpceDetailsBook_Popup(sender As Object, e As EventArgs) Handles INDpceDetailsBook.Popup
        INDsleLegalBook.Focus()
    End Sub

#End Region

#Region "CloseUp"

    Private Sub INDpceDetailsBook_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDetailsBook.CloseUp
        If FlagEditModePopupBook Then
            CleanControlsPopup()
        End If
    End Sub

    ''' <summary>
    ''' Valida el valor histórico cuando termina de llenarse el campo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtHistoricalValue_Validated(sender As Object, e As EventArgs) Handles INDtxtHistoricalValue.Validated
        If Depreciate And HistoricalValue <= SettingsFixedAsset.TopMinorValue Then
            INDlyItemValidMinorAmount.HideControl(False)
        Else
            INDlyItemValidMinorAmount.HideControl
        End If
    End Sub

#End Region

#End Region

End Class