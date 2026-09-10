'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 15/04/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Controls
Imports DevExpress.Xpo
Imports Presentation.FixedAsset.MVP
Imports Domain.Entities
Imports System.Drawing
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Inventory

#End Region

Public Class FrmFixedAssetEntryItem
    Implements IFixedAssetEntryItem

#Region "Builder"

    ''' <summary>
    ''' Inicializa una instancia de la clase
    ''' </summary>
    ''' <param name="_Rounding"></param>
    ''' <remarks></remarks>
    Public Sub New(Optional ByVal _Rounding As Integer = 1, Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1)
        InitializeComponent()
        Me._tRMValue = _tRMValue
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
        Rounding = _Rounding
    End Sub

#End Region

#Region "Properties"

    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset Implements IFixedAssetEntryItem.SettingsFixedAsset
        Get
            Return _SettingsFixedAsset
        End Get
        Set(value As SettingFixedAsset)
            _SettingsFixedAsset = value
        End Set
    End Property

    Public Property DiscountPercentage As Decimal Implements IFixedAssetEntryItem.DiscountPercentage
        Get
            Return INDseDiscountPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseDiscountPercentage.EditValue = value
        End Set
    End Property

    Public Property DiscountValue As Decimal Implements IFixedAssetEntryItem.DiscountValue
        Get
            Return INDtxtDiscountValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtDiscountValue.EditValue = value
        End Set
    End Property

    Public Property ItemId As Integer? Implements IFixedAssetEntryItem.ItemId
        Get
            Return INDsleItem.EditValue
        End Get
        Set(value As Integer?)
            INDsleItem.EditValue = value
        End Set
    End Property

    Public Property ItemXpo As XPInstantFeedbackSource Implements IFixedAssetEntryItem.ItemXpo
        Get
            Return INDsleItem.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleItem.Properties.DataSource = value
        End Set
    End Property

    Public Property IvaId As Integer? Implements IFixedAssetEntryItem.IvaId
        Get
            Return INDsleIVA.EditValue
        End Get
        Set(value As Integer?)
            INDsleIVA.EditValue = value
        End Set
    End Property

    Public Property IvaPercentage As Decimal Implements IFixedAssetEntryItem.IvaPercentage
        Get
            Return INDseIvaPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDseIvaPercentage.EditValue = value
        End Set
    End Property

    Public Property IvaValue As Decimal Implements IFixedAssetEntryItem.IvaValue
        Get
            Return INDtxtIvaValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtIvaValue.EditValue = value
        End Set
    End Property

    Public Property IvaXpo As XPInstantFeedbackSource Implements IFixedAssetEntryItem.IvaXpo
        Get
            Return INDsleIVA.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIVA.Properties.DataSource = value
        End Set
    End Property

    Public Property ModelItem As String Implements IFixedAssetEntryItem.ModelItem
        Get
            Return INDtxtModel.EditValue
        End Get
        Set(value As String)
            INDtxtModel.EditValue = value
        End Set
    End Property

    Public Property PolicyId As Integer? Implements IFixedAssetEntryItem.PolicyId
        Get
            Return INDslePolicy.EditValue
        End Get
        Set(value As Integer?)
            INDslePolicy.EditValue = value
        End Set
    End Property

    Public Property PolicyXpo As XPInstantFeedbackSource Implements IFixedAssetEntryItem.PolicyXpo
        Get
            Return INDslePolicy.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePolicy.Properties.DataSource = value
        End Set
    End Property

    Public Property Quantity As Integer Implements IFixedAssetEntryItem.Quantity
        Get
            Return INDseQuantity.EditValue
        End Get
        Set(value As Integer)
            INDseQuantity.EditValue = value
        End Set
    End Property

    Public Property SubTotalValue As Decimal Implements IFixedAssetEntryItem.SubTotalValue
        Get
            Return INDtxtSubTotal.EditValue
        End Get
        Set(value As Decimal)
            INDtxtSubTotal.EditValue = value
        End Set
    End Property

    Public Property TotalValue As Decimal Implements IFixedAssetEntryItem.TotalValue
        Get
            Return INDtxtTotalValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtTotalValue.EditValue = value
        End Set
    End Property

    Public Property TrademarkId As Integer? Implements IFixedAssetEntryItem.TrademarkId
        Get
            Return INDsleTrademark.EditValue
        End Get
        Set(value As Integer?)
            INDsleTrademark.EditValue = value
        End Set
    End Property

    Public Property TrademarkXpo As XPInstantFeedbackSource Implements IFixedAssetEntryItem.TrademarkXpo
        Get
            Return INDsleTrademark.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTrademark.Properties.DataSource = value
        End Set
    End Property

    Public Property UnitValue As Decimal Implements IFixedAssetEntryItem.UnitValue
        Get
            Return INDtxtUnitValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtUnitValue.EditValue = value
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

    Private _fixedAssetEntryItem As FixedAssetEntryItem
    Public Property FixedAssetEntryItem As FixedAssetEntryItem
        Get
            Return _fixedAssetEntryItem
        End Get
        Set(value As FixedAssetEntryItem)
            _fixedAssetEntryItem = value
        End Set
    End Property

    Private _listCompare As List(Of FixedAssetEntryItem)
    Public Property ListCompare As List(Of FixedAssetEntryItem)
        Get
            Return _listCompare
        End Get
        Set(value As List(Of FixedAssetEntryItem))
            _listCompare = value
        End Set
    End Property

    Private _getLocationResponsible As Integer
    Public Property GetLocationResponsible As Integer
        Get
            Return _getLocationResponsible
        End Get
        Set(value As Integer)
            _getLocationResponsible = value
        End Set
    End Property

    Private _entryDate As DateTime
    Public Property EntryDate As DateTime
        Get
            Return _entryDate
        End Get
        Set(value As DateTime)
            _entryDate = value
        End Set
    End Property

    Private _importData As Boolean
    Public Property ImportData As Boolean
        Get
            Return _importData
        End Get
        Set(value As Boolean)
            _importData = value
        End Set
    End Property

    Private _listFixedAssetEntryItem As List(Of FixedAssetEntryItem)
    Public Property ListFixedAssetEntryItem As List(Of FixedAssetEntryItem)
        Get
            Return _listFixedAssetEntryItem
        End Get
        Set(value As List(Of FixedAssetEntryItem))
            _listFixedAssetEntryItem = value
        End Set
    End Property

    Dim _declarant As Boolean
    Public WriteOnly Property Declarant As Boolean
        Set(value As Boolean)
            _declarant = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetEntryItem

    ''' <summary>
    ''' Permite saber si se puede cambiar el valor del search
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangedSearch As Boolean = True

    ''' <summary>
    ''' Listado de detalles del ingreso de activo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail)

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEntryItemDetail As FixedAssetEntryItemDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Permite saber si el articulo deprecia o no
    ''' </summary>
    ''' <remarks></remarks>
    Dim DepreciateItem As Boolean

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailPartBook As List(Of FixedAssetEntryItemDetailPartBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook)

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail)

    ''' <summary>
    ''' True = Cambia, False = No cambia
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangedItemBarButtons As Boolean = True

    ''' <summary>
    ''' Variable que toma el Tipo de Adquisición para las Validaciones Pertinentes
    ''' </summary>
    ''' <remarks></remarks>
    Public AdquisitionType As Integer

    ''' <summary>
    ''' Variable para saber si Deprecia o No de acuerdo al Tipo de Adquisición
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagDepreciation As Boolean

    ''' <summary>
    ''' Variable para saber si paga IVA o no
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagIva As Boolean

    ''' <summary>
    ''' variable de la tasa de cambio, por defecto es 1 cuando la moneda es igual a la oficial
    ''' </summary>
    Private _tRMValue As Decimal = 1

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

    ''' <summary>
    ''' Variableu que contiene el valor del redondeo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Rounding As Integer

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddFixedAssetEntryItemEventArgs(sender As Object, e As AddFixedAssetEntryItem)

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
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
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

    Private Function ValidateList() As String
        Dim ListErrors As New StringBuilder
        For Each entryItem In ListFixedAssetEntryItem
            If entryItem.FixedAssetEntryItemDetail Is Nothing OrElse entryItem.FixedAssetEntryItemDetail.Count = 0 Then
                ListErrors.AppendLine("Debe agregar un detalle del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString)
            End If
            If entryItem.Quantity <> entryItem.FixedAssetEntryItemDetail.Count Then
                ListErrors.AppendLine("La cantidad del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString + " es diferente a la cantidad de sus detalles")
            End If
            If entryItem.ItemId = Nothing Then
                ListErrors.AppendLine("Debe seleccionar un articulo del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString)
            End If
            If entryItem.TrademarkId = Nothing Then
                ListErrors.AppendLine("Debe seleccionar una marca del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString)
            End If
            If entryItem.Model = String.Empty Then
                ListErrors.AppendLine("Debe ingresar un modelo del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString)
            End If
            If entryItem.PolicyId = Nothing Then
                ListErrors.AppendLine("Debe seleccionar una poliza del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString)
            End If
            If entryItem.IVAId = DirectCast(Nothing, Integer?) Then
                ListErrors.AppendLine("Debe seleccionar un IVA del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString)
            End If
            If entryItem.Quantity = Nothing Then
                ListErrors.AppendLine("Debe ingresar una cantidad del item " + (ListFixedAssetEntryItem.IndexOf(entryItem) + 1).ToString)
            End If
        Next
        Return ListErrors.ToString
    End Function

    Private Sub AddItem()
        If ValidateControls() = False Then 'Se validan que los controles esten diligenciados
            Exit Sub
        Else
            If ImportData = False Then 'Si viene desde el form principal
                If ListFixedAssetEntryItemDetail Is Nothing OrElse ListFixedAssetEntryItemDetail.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe agregar un detalle."
                    Exit Sub
                End If
                If Quantity <> ListFixedAssetEntryItemDetail.Count Then
                    Mensaje(EeventViewerImages.Advertencia) = "La cantidad especificada es diferente a la cantidad de detalles."
                    Exit Sub
                End If
            Else 'Si viene desde el form de importar
                'Se asigna los valores
                AssigningValues()

                Dim errors As String = ValidateList()
                If errors.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errors
                    Exit Sub
                End If
            End If
        End If

        If ImportData = False Then 'Si viene desde el form principal
            If EditMode = False Then 'Esta guardando el item
                If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then 'Se valida que el articulo no se encuentre en el form principal
                    Dim cont = (From l In ListCompare Where l.ItemId = ItemId Select l).Count
                    If cont > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "El articulo " + INDsleItem.Text + " ya se encuentra en el listado."
                        Exit Sub
                    End If
                End If
            End If

            'Se asigna los valores
            AssigningValues()

            'Se crea el objeto que se va a devolver
            Dim args As New AddFixedAssetEntryItem
            args.FixedAssetEntryItem = FixedAssetEntryItem
            args.EditMode = EditMode
            args.ListDeleteFixedAssetEntryItemDetailPartBook = ListDeleteFixedAssetEntryItemDetailPartBook
            args.ListDeleteFixedAssetEntryItemDetailPart = ListDeleteFixedAssetEntryItemDetailPart
            args.ListDeleteFixedAssetEntryItemDetailBook = ListDeleteFixedAssetEntryItemDetailBook
            args.ListDeleteFixedAssetEntryItemDetail = ListDeleteFixedAssetEntryItemDetail
            RaiseEvent AddFixedAssetEntryItemEventArgs(Nothing, args)
            CleanControls()
            INDsleItem.Focus()
        Else 'Si viene desde el form importar
            'Se crea el objeto que se va a devolver
            Dim args As New AddFixedAssetEntryItem
            args.ListFixedAssetEntryItem = ListFixedAssetEntryItem
            RaiseEvent AddFixedAssetEntryItemEventArgs(Nothing, args)
            Me.Close()
        End If
    End Sub

    Private Sub AssigningValues()
        If ImportData = False Then
            If EditMode = False Then 'Si se esta guardando el item se instancia el objeto
                FixedAssetEntryItem = New FixedAssetEntryItem
            End If
        End If
        Dim retentionConceptId As Integer = Nothing
        Dim retentionConceptXpo As New GeneralLedgerRetentionConceptsReportXpo()
        With FixedAssetEntryItem
            If ImportData = False Then
                If EditMode = False Then
                    .RemissionSource = 1 'Ninguna
                End If
            End If
            .ItemId = ItemId
            .ItemCodeName = INDsleItem.Text
            .TrademarkId = TrademarkId
            .TrademarkCodeName = INDsleTrademark.Text
            .Model = ModelItem
            .PolicyId = PolicyId
            .PolicyCodeName = INDslePolicy.Text
            .IVAId = IvaId
            .IVACodeName = INDsleIVA.Text
            .Quantity = Quantity
            .OutstandingQuantity = Quantity
            .UnitValue = UnitValue
            .SubTotalValue = SubTotalValue
            .IvaPercentage = IvaPercentage
            .IvaValue = IvaValue
            .DiscountPercentage = DiscountPercentage
            .DiscountValue = DiscountValue
            .TotalValue = TotalValue
            .Observation = INDMeObservation.EditValue
            .FixedAssetEntryItemDetail.Clear()
            If ListFixedAssetEntryItemDetail IsNot Nothing AndAlso ListFixedAssetEntryItemDetail.Count > 0 Then
                For Each item In ListFixedAssetEntryItemDetail
                    .FixedAssetEntryItemDetail.Add(item)
                Next
            End If
            Dim fixedAssetItemXpo = Presenter.GetFixedAssetItemById(.ItemId)
            If fixedAssetItemXpo.ItemCatalogId.Classification <> 3 Then 'Si el artículo pertenece a un Catálogo de Bienes Controlables no aplica la lógica siguiente
                Dim accountPayableConceptId = If(_declarant, fixedAssetItemXpo.ItemCatalogId.DeclarantRetentionAccountPayableConceptId, fixedAssetItemXpo.ItemCatalogId.NotDeclarantRetentionAccountPayableConceptId)
                If accountPayableConceptId > 0 Then
                    Dim accountPayableConceptXpo = Presenter.GetAccountPayableConceptById(accountPayableConceptId)
                    retentionConceptId = accountPayableConceptXpo.RetentionConceptId
                End If
                If retentionConceptId > 0 Then
                    retentionConceptXpo = Presenter.GetRetentionConceptById(retentionConceptId)
                    .MinBase = retentionConceptXpo.MinBase
                    .Rate = retentionConceptXpo.Rate
                    .TypeRounding = retentionConceptXpo.TypeRounding
                End If
            End If
        End With
    End Sub

    Private Sub CleanControls()
        FixedAssetEntryItem = Nothing
        EditMode = False
        ItemId = Nothing
        INDsleItem.Properties.NullText = String.Empty
        TrademarkId = Nothing
        INDsleTrademark.Properties.NullText = String.Empty
        ModelItem = Nothing
        PolicyId = Nothing
        INDslePolicy.Properties.NullText = String.Empty

        IvaId = Nothing
        INDsleIVA.Properties.NullText = String.Empty

        Select Case AdquisitionType
            Case 1, 7
                INDlyItemIVA.ShowInCustomizationForm = False
                INDlyItemIVA.AllowHide = False
            Case Else
                INDlyItemIVA.ShowInCustomizationForm = True
                INDlyItemIVA.AllowHide = True
        End Select

        Quantity = Nothing
        INDseQuantity.Properties.MinValue = 0
        INDseQuantity.Properties.MaxValue = 99999
        UnitValue = Nothing
        SubTotalValue = Nothing
        IvaPercentage = Nothing
        IvaValue = Nothing
        DiscountPercentage = Nothing
        DiscountValue = Nothing
        TotalValue = Nothing
        INDMeObservation.EditValue = Nothing
        ListFixedAssetEntryItemDetail = Nothing
        INDgcDetail.DataSource = Nothing
        INDsleItem.Properties.ReadOnly = False
        ListDeleteFixedAssetEntryItemDetailPartBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPart = Nothing
        ListDeleteFixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetail = Nothing
    End Sub

    Private Sub LoadControls()
        With FixedAssetEntryItem
            FlagChangedSearch = False
            ItemId = .ItemId
            INDsleItem.Properties.NullText = .ItemCodeName
            FlagChangedSearch = True
            TrademarkId = .TrademarkId
            INDsleTrademark.Properties.NullText = .TrademarkCodeName
            ModelItem = .Model
            PolicyId = .PolicyId
            INDslePolicy.Properties.NullText = .PolicyCodeName
            IvaId = .IVAId
            INDsleIVA.Properties.NullText = .IVACodeName
            Quantity = .Quantity
            Dim minValue As Integer = Quantity
            If ImportData Then
                minValue = .FixedAssetEntryItemDetail.Count
            End If
            INDseQuantity.Properties.MinValue = minValue
            INDseQuantity.Properties.MaxValue = 99999
            UnitValue = .UnitValue
            SubTotalValue = .SubTotalValue
            IvaPercentage = .IvaPercentage
            IvaValue = .IvaValue
            DiscountPercentage = .DiscountPercentage
            DiscountValue = .DiscountValue
            TotalValue = .TotalValue
            INDMeObservation.EditValue = .Observation
            ListFixedAssetEntryItemDetail = .FixedAssetEntryItemDetail.ToList()
            SetOrderItems()
            INDgcDetail.DataSource = Nothing
            INDgcDetail.DataSource = ListFixedAssetEntryItemDetail
        End With
        INDsleItem.Properties.ReadOnly = True
    End Sub

    Private Sub LoadControlsImportData()

    End Sub

    Private Sub CalculateValue()
        If Quantity <> Nothing AndAlso Quantity > 0 AndAlso UnitValue <> Nothing AndAlso UnitValue > 0 Then
            If Quantity = 1 Then 'Por tema de redondeo se deja que si la cantidad es uno deje el valor unitario del articulo
                SubTotalValue = UnitValue
            Else
                SubTotalValue = Math.Round((Quantity * UnitValue), 2, MidpointRounding.AwayFromZero) 'Se deja a dos decimales mientras se levanta PBI
            End If
        Else
            SubTotalValue = 0
        End If
    End Sub

    Private Sub CalculateIVAValue()
        If SubTotalValue <> Nothing AndAlso SubTotalValue > 0 AndAlso IvaPercentage <> Nothing AndAlso IvaPercentage > 0 Then
            Dim ValCalculate As Decimal = SubTotalValue - DiscountValue
            Dim ValIVA = (ValCalculate * IvaPercentage) / 100
            IvaValue = Math.Round(ValIVA, 2, MidpointRounding.AwayFromZero) 'Se deja a dos decimales mientras se levanta PBI
        Else
            IvaValue = 0
        End If
    End Sub

    Private Sub CalculateTotalValue()
        TotalValue = SubTotalValue - DiscountValue + IvaValue
    End Sub

    Private Sub CalculateDiscountValue()
        If DiscountPercentage <> Nothing AndAlso DiscountPercentage > 0 AndAlso SubTotalValue <> Nothing AndAlso SubTotalValue > 0 Then
            DiscountValue = (SubTotalValue * DiscountPercentage) / 100
        Else
            DiscountValue = 0
        End If
    End Sub

    Private Sub OpenFormFixedAssetEntryItemDetail(EditMode As Boolean)
        Using formulario As New FrmFixedAssetEntryItemDetail(Me._headCurrency)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddFixedAssetEntryItemDetailEventArgs, AddressOf ReturnAddEventArgs
            formulario.EditMode = EditMode
            formulario.ItemId = ItemId

            If EditMode = False Then
                Dim contList As Integer = 0
                If ListFixedAssetEntryItemDetail IsNot Nothing AndAlso ListFixedAssetEntryItemDetail.Count > 0 Then
                    contList = ListFixedAssetEntryItemDetail.Count
                End If
                contList = Quantity - contList
                formulario.CountList = contList
            End If

            formulario._UnitValue = UnitValue
            formulario.SettingsFixedAsset = Me.SettingsFixedAsset
            formulario.ListFixedAssetEntryItemDetail = ListFixedAssetEntryItemDetail
            formulario.FixedAssetEntryItemDetail = FixedAssetEntryItemDetail
            formulario.GetLocationResponsible = GetLocationResponsible
            formulario.EntryDate = EntryDate
            formulario.FlagDepreciate = FlagDepreciation
            formulario.Width = Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.7
            formulario.Height = Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.7
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para establecer un orden de los items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOrderItems()
        For Each item In ListFixedAssetEntryItemDetail
            item.OrderItem = ListFixedAssetEntryItemDetail.IndexOf(item) + 1
        Next
    End Sub


    Private Sub ReturnAddEventArgs(sender As Object, e As AddFixedAssetEntryItemDetail)
        INDsleItem.Properties.ReadOnly = True
        If e.EditMode = False Then 'Se esta ingresando
            If ListFixedAssetEntryItemDetail Is Nothing Then
                ListFixedAssetEntryItemDetail = New List(Of FixedAssetEntryItemDetail)
            End If
            ListFixedAssetEntryItemDetail.AddRange(e.ListFixedAssetEntryItemDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalles agregados correctamente."
        Else 'Se esta modificando
            ListFixedAssetEntryItemDetail.Remove(FixedAssetEntryItemDetail)
            ListFixedAssetEntryItemDetail.Insert(IndexEditRecord, e.FixedAssetEntryItemDetail)
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If
        SetOrderItems()
        INDseQuantity.Properties.MinValue = ListFixedAssetEntryItemDetail.Count
        INDseQuantity.Properties.MaxValue = 99999

        If e.ListDeleteFixedAssetEntryItemDetailPartBook IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
            End If
            ListDeleteFixedAssetEntryItemDetailPartBook.AddRange(e.ListDeleteFixedAssetEntryItemDetailPartBook)
        End If

        If e.ListDeleteFixedAssetEntryItemDetailPart IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetailPart Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailPart = New List(Of FixedAssetEntryItemDetailPart)
            End If
            ListDeleteFixedAssetEntryItemDetailPart.AddRange(e.ListDeleteFixedAssetEntryItemDetailPart)
        End If

        If e.ListDeleteFixedAssetEntryItemDetailBook IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetailBook Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
            End If
            ListDeleteFixedAssetEntryItemDetailBook.AddRange(e.ListDeleteFixedAssetEntryItemDetailBook)
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListFixedAssetEntryItemDetail
    End Sub

    Private Sub EditDetail()
        FixedAssetEntryItemDetail = DirectCast(INDviewDetail.GetFocusedRow(), FixedAssetEntryItemDetail)
        IndexEditRecord = ListFixedAssetEntryItemDetail.IndexOf(FixedAssetEntryItemDetail)
        OpenFormFixedAssetEntryItemDetail(True)
    End Sub

    Private Sub RemoveDetail()
        FixedAssetEntryItemDetail = CType(INDviewDetail.GetFocusedRow, FixedAssetEntryItemDetail)
        ListFixedAssetEntryItemDetail.Remove(FixedAssetEntryItemDetail)

        If FixedAssetEntryItemDetail.Id > 0 Then
            If ListDeleteFixedAssetEntryItemDetail Is Nothing Then
                ListDeleteFixedAssetEntryItemDetail = New List(Of FixedAssetEntryItemDetail)
            End If
            ListDeleteFixedAssetEntryItemDetail.Add(FixedAssetEntryItemDetail)

            'Se recorren los libros del articulo, si hay para eliminarlos
            If FixedAssetEntryItemDetail.FixedAssetEntryItemDetailBook IsNot Nothing AndAlso FixedAssetEntryItemDetail.FixedAssetEntryItemDetailBook.Count > 0 Then
                For Each item In (From l In FixedAssetEntryItemDetail.FixedAssetEntryItemDetailBook Where l.Id > 0 Select l).ToList
                    If ListDeleteFixedAssetEntryItemDetailBook Is Nothing Then
                        ListDeleteFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
                    End If
                    If ListDeleteFixedAssetEntryItemDetailBook.Contains(item) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetEntryItemDetailBook.Add(item)
                Next
            End If

            'Se recorren las partes y los libros de las partes, si hay para eliminarlos
            If FixedAssetEntryItemDetail.FixedAssetEntryItemDetailPart IsNot Nothing AndAlso FixedAssetEntryItemDetail.FixedAssetEntryItemDetailPart.Count > 0 Then
                For Each itemPart In (From l In FixedAssetEntryItemDetail.FixedAssetEntryItemDetailPart Where l.Id > 0 Select l).ToList 'Se recorre las partes

                    If itemPart.FixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso itemPart.FixedAssetEntryItemDetailPartBook.Count > 0 Then
                        For Each itemPDB In (From l In itemPart.FixedAssetEntryItemDetailPartBook Where l.Id > 0 Select l).ToList 'Se recorren los libros de las partes
                            If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                                ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
                            End If
                            If ListDeleteFixedAssetEntryItemDetailPartBook.Contains(itemPDB) Then
                                Continue For
                            End If
                            ListDeleteFixedAssetEntryItemDetailPartBook.Add(itemPDB)
                        Next
                    End If

                    If ListDeleteFixedAssetEntryItemDetailPart Is Nothing Then
                        ListDeleteFixedAssetEntryItemDetailPart = New List(Of FixedAssetEntryItemDetailPart)
                    End If
                    If ListDeleteFixedAssetEntryItemDetailPart.Contains(itemPart) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetEntryItemDetailPart.Add(itemPart)

                Next
            End If

        End If

        If ListFixedAssetEntryItemDetail Is Nothing OrElse ListFixedAssetEntryItemDetail.Count = 0 Then 'Si no hay detalles puede cambiar el articulo
            INDsleItem.Properties.ReadOnly = False
            INDseQuantity.Properties.MinValue = 0
        Else
            INDseQuantity.Properties.MinValue = ListFixedAssetEntryItemDetail.Count
        End If
        INDseQuantity.Properties.MaxValue = 99999
        SetOrderItems()
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListFixedAssetEntryItemDetail
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

        Me.INDtxtUnitValue.Properties.Mask.Culture = _culture
        Me.INDtxtSubTotal.Properties.Mask.Culture = _culture
        Me.INDtxtDiscountValue.Properties.Mask.Culture = _culture
        Me.INDtxtIvaValue.Properties.Mask.Culture = _culture
        Me.INDtxtTotalValue.Properties.Mask.Culture = _culture

    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        FlagChangedSearch = Nothing
        ListFixedAssetEntryItemDetail = Nothing
        FixedAssetEntryItemDetail = Nothing
        IndexEditRecord = Nothing
        DepreciateItem = Nothing
        ListFixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPartBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPart = Nothing
        ListDeleteFixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetail = Nothing
        FlagChangedItemBarButtons = Nothing
        AdquisitionType = Nothing
        FlagDepreciation = Nothing
        FlagIva = Nothing
    End Sub

    Private Sub FrmFixedAssetEntryItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PFixedAssetEntryItem(Me)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Presenter.GetSettingFixedAssetByOperatingUnitId(Me.BarraBotones.OperatingUnitValue)
        IndigoGridControl1.RefreshGrid(INDgcDetail)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDviewDetail, ListActions)

        'Valido el Tipo de Adquisición para saber qué controles activar o no
        If AdquisitionType = 1 Then
            'Compra Directa
            FlagIva = True
            INDseIvaPercentage.Enabled = True
            INDsleIVA.Enabled = True
            INDlyItemIVA.ShowInCustomizationForm = False
            INDlyItemIVA.AllowHide = False
            FlagDepreciation = True
        ElseIf AdquisitionType = 3 Then
            'Comodato
            INDseIvaPercentage.Enabled = False
            INDsleIVA.Enabled = False
            INDtxtIvaValue.EditValue = 0
            IvaId = Nothing
            INDlyItemIVA.ShowInCustomizationForm = True
            INDlyItemIVA.AllowHide = True
            FlagIva = False
            FlagDepreciation = True
        ElseIf AdquisitionType = 4 Then
            'Donación
            FlagIva = False
            IvaId = Nothing
            INDlyItemIVA.ShowInCustomizationForm = True
            INDlyItemIVA.AllowHide = True
            INDseIvaPercentage.Enabled = False
            INDtxtIvaValue.EditValue = 0
            INDsleIVA.Enabled = False
            FlagDepreciation = True
        ElseIf AdquisitionType = 5 Then
            'Traspaso de Bienes
            FlagIva = False
            IvaId = Nothing
            INDlyItemIVA.ShowInCustomizationForm = True
            INDlyItemIVA.AllowHide = True
            INDseIvaPercentage.Enabled = False
            INDsleIVA.Enabled = False
            INDtxtIvaValue.EditValue = 0
            FlagDepreciation = True
        ElseIf AdquisitionType = 6 Then
            'Otros Conceptos
            FlagIva = False
            IvaId = Nothing
            INDlyItemIVA.ShowInCustomizationForm = True
            INDlyItemIVA.AllowHide = True
            INDseIvaPercentage.Enabled = False
            INDsleIVA.Enabled = False
            INDtxtIvaValue.EditValue = 0
            FlagDepreciation = True
        ElseIf AdquisitionType = 7 Then
            'Leasing Financiero
            FlagIva = True
            INDseIvaPercentage.Enabled = True
            INDsleIVA.Enabled = True
            INDlyItemIVA.ShowInCustomizationForm = False
            INDlyItemIVA.AllowHide = False
            FlagDepreciation = True
        ElseIf AdquisitionType = 8 Then
            'Comodato Tercerizado
            INDseIvaPercentage.Enabled = False
            INDsleIVA.Enabled = False
            INDtxtIvaValue.EditValue = 0
            IvaId = Nothing
            INDlyItemIVA.ShowInCustomizationForm = True
            INDlyItemIVA.AllowHide = True
            FlagIva = False
            FlagDepreciation = False
        ElseIf AdquisitionType = 9 Then
            'Renting Financiero
            FlagIva = True
            INDseIvaPercentage.Enabled = True
            INDsleIVA.Enabled = True
            INDlyItemIVA.ShowInCustomizationForm = False
            INDlyItemIVA.AllowHide = False
            FlagDepreciation = True
        ElseIf AdquisitionType = 10 Then
            'Renting Operativo
            INDseIvaPercentage.Enabled = False
            INDsleIVA.Enabled = False
            INDtxtIvaValue.EditValue = 0
            IvaId = Nothing
            INDlyItemIVA.ShowInCustomizationForm = True
            INDlyItemIVA.AllowHide = True
            FlagIva = False
            FlagDepreciation = False
        End If

        'Se revisa si por la parametrización este deshabilitado o no
        If FlagIva Then
            INDseIvaPercentage.Value = 0
            IvaId = 0
            INDsleIVA.Properties.NullText = “IVA 0%”
        End If

        If ImportData = False Then 'Si viene desde el form principal
            If EditMode Then 'Si esta en modo edición
                LoadControls()
            End If
        Else 'Si viene desde el form de importar
            Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Artículo", .FieldName = "ItemCodeName"}}.ToList()
            FlagChangedItemBarButtons = False
            Me.BarraBotones.FilterDataSource = ListFixedAssetEntryItem
            FlagChangedItemBarButtons = True
            If ListFixedAssetEntryItem.Count = 1 Then
                FixedAssetEntryItem = ListFixedAssetEntryItem(0)
                LoadControls()
            End If
        End If
        If FixedAssetEntryItem IsNot Nothing Then
            'Evaluamos si el ingreso del activo esta confirmado 
            If FixedAssetEntryItem?.FixedAssetEntry?.Status = 2 Then
                ActionsOnControls = False
            End If
        End If
    End Sub
    ''' <summary>
    ''' Habilita o deshabilita los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetEntryItem.ActionsOnControls
        Set(value As Boolean)
            INDsleItem.Enabled = value
            INDsleTrademark.Enabled = value
            INDtxtModel.Enabled = value
            INDslePolicy.Enabled = value
            INDsleIVA.Enabled = value
            INDseQuantity.Enabled = value
            INDMeObservation.Enabled = value
            INDtxtUnitValue.Enabled = value
            INDtxtSubTotal.Enabled = value
            INDseDiscountPercentage.Enabled = value
            INDtxtDiscountValue.Enabled = value
            INDseIvaPercentage.Enabled = value
            INDtxtIvaValue.Enabled = value
            INDtxtTotalValue.Enabled = value
            INDbtnAddItemDetail.Enabled = value
            INDBtnAddItem.Enabled = value
        End Set
    End Property

#End Region

#Region "ButtonClick"

    Private Sub INDsleItem_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleItem.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(572, Nothing, True)
            Presenter.InitializeItem(AdquisitionType)
        End If
    End Sub

    Private Sub INDsleTrademark_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTrademark.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1700, Nothing, True)
            Presenter.InitializeTrademark()
        End If
    End Sub

    Private Sub INDsleIVA_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIVA.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1509, Nothing, True)
            Presenter.InitializeIVA()
        End If
    End Sub

    Private Sub INDslePolicy_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePolicy.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1702, Nothing, True)
            Presenter.InitializePolicy()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleItem.QueryPopUp
        If ItemXpo Is Nothing Then
            Presenter.InitializeItem(AdquisitionType)
        End If
    End Sub

    Private Sub INDsleTrademark_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleTrademark.QueryPopUp
        If TrademarkXpo Is Nothing Then
            Presenter.InitializeTrademark()
        End If
    End Sub

    Private Sub INDsleIVA_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleIVA.QueryPopUp
        If IvaXpo Is Nothing Then
            Presenter.InitializeIVA()
        End If
    End Sub

    Private Sub INDslePolicy_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslePolicy.QueryPopUp
        If PolicyXpo Is Nothing Then
            Presenter.InitializePolicy()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmFixedAssetEntryItem_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleItem.Focus()
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddItem_Click(sender As Object, e As EventArgs) Handles INDBtnAddItem.Click
        AddItem()
    End Sub

    Private Sub INDbtnAddItemDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddItemDetail.Click
        If ItemId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un articulo."
            Exit Sub
        End If
        If Quantity = Nothing OrElse Quantity = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar una cantidad."
            Exit Sub
        End If
        If ListFixedAssetEntryItemDetail IsNot Nothing AndAlso ListFixedAssetEntryItemDetail.Count > 0 Then
            If Quantity = ListFixedAssetEntryItemDetail.Count Then
                Mensaje(EeventViewerImages.Advertencia) = "No puede agregar más detalles porque la cantidad especificada es igual a la cantidad del listado."
                Exit Sub
            End If
        End If
        FixedAssetEntryItemDetail = Nothing
        OpenFormFixedAssetEntryItemDetail(False)
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleIVA_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIVA.EditValueChanged
        If IvaId IsNot Nothing Then
            IvaPercentage = INDsleIVA.Properties.View.GetFocusedRowCellValue("Percentage")
        End If
    End Sub

    Private Sub INDseQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDseQuantity.EditValueChanged
        CalculateValue()
    End Sub

    Private Sub INDtxtUnitValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtUnitValue.EditValueChanged
        CalculateValue()
    End Sub

    Private Sub INDtxtSubTotal_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtSubTotal.EditValueChanged, INDseIvaPercentage.EditValueChanged
        CalculateDiscountValue()
        If FlagIva Then
            CalculateIVAValue()
        End If
        CalculateTotalValue()
    End Sub

    Private Sub INDtxtIvaValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtIvaValue.EditValueChanged
        CalculateTotalValue()
    End Sub

    Private Sub INDseDiscountPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDseDiscountPercentage.EditValueChanged
        CalculateDiscountValue()
        CalculateIVAValue()
    End Sub

    Private Sub INDtxtDiscountValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtDiscountValue.EditValueChanged
        CalculateValue()
    End Sub

    Private Sub INDsleItem_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleItem.EditValueChanged
        If ItemId IsNot Nothing AndAlso FlagChangedSearch = True Then
            'Dim itemXpo = DirectCast(DirectCast(ViewItemSearch.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetEquipmentXpo)
            Dim itemXpo = Presenter.GetFixedAssetItemById(ItemId)
            If itemXpo IsNot Nothing Then
                If itemXpo.AllowDepreciate Then 'Si el articulo permite depreciar
                    If itemXpo.FixedAssetItemDetailXpo Is Nothing OrElse itemXpo.FixedAssetItemDetailXpo.Count = 0 Then 'Si el articulo no tiene detalles de contabilidad
                        Mensaje(EeventViewerImages.Advertencia) = "El articulo " + INDsleItem.Text.Trim + " no se puede seleccionar porque permite depreciar y no tiene detalles contables."
                        UnitValue = Nothing
                        INDsleItem.Properties.NullText = String.Empty
                        ItemId = Nothing
                        Exit Sub
                    End If
                End If
                UnitValue = Math.Round(itemXpo.LastCostItem / _tRMValue, 2, MidpointRounding.AwayFromZero)

                If FlagIva Then
                    IvaId = itemXpo.IVAId.Id
                    INDsleIVA.Properties.NullText = itemXpo.IVAId.Code + " - " + itemXpo.IVAId.Name
                    IvaPercentage = itemXpo.IVAId.Percentage
                End If

            End If
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetEntryItem_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "MenuContext"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                RemoveDetail()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                RemoveDetail()
        End Select
    End Sub

#End Region

#End Region

#Region "BarButtons"

    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        If FlagChangedItemBarButtons Then
            AssigningValues()
        End If
        FixedAssetEntryItem = Record
        LoadControls()
    End Sub

#End Region

End Class
