'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/04/2016
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

Public Class FrmFixedAssetEntryItemDetail
    Implements IFixedAssetEntryItemDetail

#Region "Builder"

    ''' <summary>
    ''' Inicializa una instancia de la clase
    ''' </summary>
    ''' <param name="_currency"></param>
    Public Sub New(Optional _currency As Currency = Nothing)
        InitializeComponent()
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
    End Sub

#End Region

#Region "Properties"

    Public Property DepreciationTypeId As Integer? Implements IFixedAssetEntryItemDetail.DepreciationTypeId
        Get
            Return INDsleDepreciationType.EditValue
        End Get
        Set(value As Integer?)
            INDsleDepreciationType.EditValue = value
        End Set
    End Property

    Public Property LegalBookId As Integer? Implements IFixedAssetEntryItemDetail.LegalBookId
        Get
            Return INDsleLegalBook.EditValue
        End Get
        Set(value As Integer?)
            INDsleLegalBook.EditValue = value
        End Set
    End Property

    Public Property LegalBookXpo As XPInstantFeedbackSource Implements IFixedAssetEntryItemDetail.LegalBookXpo
        Get
            Return INDsleLegalBook.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleLegalBook.Properties.DataSource = value
        End Set
    End Property

    Public Property LifeTime As Integer Implements IFixedAssetEntryItemDetail.LifeTime
        Get
            Return INDseLifeUtil.EditValue
        End Get
        Set(value As Integer)
            INDseLifeUtil.EditValue = value
        End Set
    End Property

    Public Property PercentageRescue As Decimal Implements IFixedAssetEntryItemDetail.PercentageRescue
        Get
            Return INDsePercentageRescue.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageRescue.EditValue = value
        End Set
    End Property

    Public Property TotalProductionUnit As Long Implements IFixedAssetEntryItemDetail.TotalProductionUnit
        Get
            Return INDseTotalProductionUnit.EditValue
        End Get
        Set(value As Long)
            INDseTotalProductionUnit.EditValue = value
        End Set
    End Property

    Public Property UnitLifeTimeId As Integer? Implements IFixedAssetEntryItemDetail.UnitLifeTimeId
        Get
            Return INDsleUnitLifeUtil.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitLifeUtil.EditValue = value
        End Set
    End Property

    Public Property AdquisitionDate As Date? Implements IFixedAssetEntryItemDetail.AdquisitionDate
        Get
            Return INDdteAdquisitionDate.EditValue
        End Get
        Set(value As Date?)
            INDdteAdquisitionDate.EditValue = value
        End Set
    End Property

    Public Property Depreciate As Boolean? Implements IFixedAssetEntryItemDetail.Depreciate
        Get
            Return INDsleDepreciate.EditValue
        End Get
        Set(value As Boolean?)
            INDsleDepreciate.EditValue = value
        End Set
    End Property

    Public Property Amortize As Boolean Implements IFixedAssetEntryItemDetail.Amortize
        Get
            Return INDSleAmortize.EditValue
        End Get
        Set(value As Boolean)
            INDSleAmortize.EditValue = value
        End Set
    End Property

    Public Property HandlesWarranty As Boolean? Implements IFixedAssetEntryItemDetail.HandlesWarranty
        Get
            Return INDsleHandlesWarranty.EditValue
        End Get
        Set(value As Boolean?)
            INDsleHandlesWarranty.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que establece el valor del parametro valida menor cuantía
    ''' </summary>
    ''' <returns></returns>
    Public Property ValidSmallerAmount As Boolean? Implements IFixedAssetEntryItemDetail.ValidSmallerAmount
        Get
            Return INDGleValidSmallerAmount.EditValue
        End Get
        Set(value As Boolean?)
            INDGleValidSmallerAmount.EditValue = value
        End Set
    End Property

    Public Property LocationId As Integer? Implements IFixedAssetEntryItemDetail.LocationId
        Get
            Return INDsleLocation.EditValue
        End Get
        Set(value As Integer?)
            INDsleLocation.EditValue = value
        End Set
    End Property

    Public Property LocationXpo As XPCollection Implements IFixedAssetEntryItemDetail.LocationXpo
        Get
            Return INDsleLocation.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleLocation.Properties.DataSource = value
        End Set
    End Property

    Public Property Plate As String Implements IFixedAssetEntryItemDetail.Plate
        Get
            Return INDtxtPlate.EditValue
        End Get
        Set(value As String)
            INDtxtPlate.EditValue = value
        End Set
    End Property

    Public Property ResponsibleId As Integer? Implements IFixedAssetEntryItemDetail.ResponsibleId
        Get
            Return INDsleResponsible.EditValue
        End Get
        Set(value As Integer?)
            INDsleResponsible.EditValue = value
        End Set
    End Property

    Public Property ResponsibleXpo As XPInstantFeedbackSource Implements IFixedAssetEntryItemDetail.ResponsibleXpo
        Get
            Return INDsleResponsible.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleResponsible.Properties.DataSource = value
        End Set
    End Property
    Public Property Serie As String Implements IFixedAssetEntryItemDetail.Serie
        Get
            Return INDtxtSerie.EditValue
        End Get
        Set(value As String)
            INDtxtSerie.EditValue = value
        End Set
    End Property

    Public Property StatusAssetId As Integer? Implements IFixedAssetEntryItemDetail.StatusAssetId
        Get
            Return INDsleStatusAsset.EditValue
        End Get
        Set(value As Integer?)
            INDsleStatusAsset.EditValue = value
        End Set
    End Property

    Public Property StatusAssetXpo As XPInstantFeedbackSource Implements IFixedAssetEntryItemDetail.StatusAssetXpo
        Get
            Return INDsleStatusAsset.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleStatusAsset.Properties.DataSource = value
        End Set
    End Property

    Public Property WarrantyExpirationDate As Date? Implements IFixedAssetEntryItemDetail.WarrantyExpirationDate
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

    Private _fixedAssetEntryItemDetail As FixedAssetEntryItemDetail
    Public Property FixedAssetEntryItemDetail As FixedAssetEntryItemDetail
        Get
            Return _fixedAssetEntryItemDetail
        End Get
        Set(value As FixedAssetEntryItemDetail)
            _fixedAssetEntryItemDetail = value
        End Set
    End Property

    Private _listFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail)
    Public Property ListFixedAssetEntryItemDetail As List(Of FixedAssetEntryItemDetail)
        Get
            Return _listFixedAssetEntryItemDetail
        End Get
        Set(value As List(Of FixedAssetEntryItemDetail))
            _listFixedAssetEntryItemDetail = value
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

    Private _ItemId As Integer
    Public Property ItemId As Integer
        Get
            Return _ItemId
        End Get
        Set(value As Integer)
            _ItemId = value
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

    Private _countList As Integer
    Public Property CountList As Integer
        Get
            Return _countList
        End Get
        Set(value As Integer)
            _countList = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que almacena el valor unitario del artículo
    ''' </summary>
    ''' <returns></returns>
    Public Property _UnitValue As Decimal

    ''' <summary>
    ''' Obtiene los parámetros de activo fijo
    ''' </summary>
    Private _SettingsFixedAsset As SettingFixedAsset
    Public Property SettingsFixedAsset As SettingFixedAsset Implements IFixedAssetEntryItemDetail.SettingsFixedAsset
        Get
            Return _SettingsFixedAsset
        End Get
        Set(value As SettingFixedAsset)
            _SettingsFixedAsset = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFixedAssetEntryItemDetail

    ''' <summary>
    ''' Listado de detalles de las partes
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetEntryItemDetailPart As List(Of FixedAssetEntryItemDetailPart)

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEntryItemDetailPart As FixedAssetEntryItemDetailPart

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Permite saber si el registro esta en modo de edición
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagEditModePopupBook As Boolean = False

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook)

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim FixedAssetEntryItemDetailBook As FixedAssetEntryItemDetailBook

    ''' <summary>
    ''' Listado de libros oficiales
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetEntryItemDetailBook As List(Of FixedAssetEntryItemDetailBook)

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitLifeUtil As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListDepreciationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite saber si se permite cambiar el search
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangedSearch As Boolean = True

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
    ''' Bandera para que de Acuerdo al Tipo de Adquisición me diga si permite depreciar o no
    ''' </summary>
    ''' <remarks></remarks>
    Public FlagDepreciate As Boolean

    ''' <summary>
    ''' variable de session
    ''' </summary>
    Private _indigo As SessionValues

    ''' <summary>
    ''' Moneda de la cabecera del documento
    ''' </summary>
    Private _headCurrency As Currency

    ''' <summary>
    ''' Bandera para controlar la navegación de la barra de botones
    ''' </summary>
    Private FlagIsNavigatingBack As Boolean = False
#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddFixedAssetEntryItemDetailEventArgs(sender As Object, e As AddFixedAssetEntryItemDetail)

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

        Me.GridColumn3 = Window.Utils.FormatGrid(GridColumn3, _currencyAbbreviation)
    End Sub

    Private Sub LoadControls(FixedAssetEntryItemDetail As FixedAssetEntryItemDetail)
        With FixedAssetEntryItemDetail
            Plate = .Plate
            Serie = .Serie
            ResponsibleId = .ReponsibleId
            INDsleResponsible.Properties.NullText = .ResponsibleCodeName
            LocationId = .LocationId
            INDsleLocation.Properties.NullText = .LocationCodeName
            AdquisitionDate = .AdquisitionDate
            StatusAssetId = .StatusAssetId
            INDsleStatusAsset.Properties.NullText = .StatusAssetCodeName
            FlagChangedSearch = False
            Depreciate = .Depreciate
            Amortize = .Amortize
            FlagChangedSearch = True
            HandlesWarranty = .HandlesWarranty
            WarrantyExpirationDate = .WarrantyExpirationDate

            ListFixedAssetEntryItemDetailPart = .FixedAssetEntryItemDetailPart.ToList
            INDgcParts.DataSource = Nothing
            INDgcParts.DataSource = ListFixedAssetEntryItemDetailPart

            If .Depreciate OrElse .Amortize Then
				If .Depreciate And _UnitValue <= SettingsFixedAsset.TopMinorValue Then
					ShowSmallerAmount()
					ValidSmallerAmount = .ValidSmallerAmount
				End If
				ListFixedAssetEntryItemDetailBook = .FixedAssetEntryItemDetailBook.ToList
                INDgcDetailsBook.DataSource = Nothing
                INDgcDetailsBook.DataSource = ListFixedAssetEntryItemDetailBook
            End If
        End With
    End Sub

    Private Sub CleanControls()
        FixedAssetEntryItemDetail = Nothing
        EditMode = False
        Plate = Nothing
        Serie = Nothing
        ResponsibleId = Nothing
        INDsleResponsible.Properties.NullText = String.Empty
        LocationId = Nothing
        INDsleLocation.Properties.NullText = String.Empty
        AdquisitionDate = Nothing
        StatusAssetId = Nothing
        INDsleStatusAsset.Properties.NullText = String.Empty
        Depreciate = False
        Amortize = False
        HandlesWarranty = Nothing
        ValidSmallerAmount = Nothing
        WarrantyExpirationDate = Nothing
        ListFixedAssetEntryItemDetailPart = Nothing
        INDgcParts.DataSource = Nothing
        INDgcDetailsBook.DataSource = Nothing
        ListFixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPartBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPart = Nothing
        INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemWarrantyExpirationDate.AllowHide = True
        INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Sub AddItemDetail()
        'Se asigna los valores del item que se este viendo actualmente
        AssigningValues()
        If EditMode = False Then 'Si se esta guardando se valida que los campos del listado esten diligenciados
            Dim errors As String = ValidateFieldList()
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If
            ListFixedAssetEntryItemDetail.ForEach(Sub(item)
                                                      If String.IsNullOrEmpty(item.Plate) Then
                                                          item.Plate = "INDP" + GetDateServer.Year.ToString + "#"
                                                      End If
                                                  End Sub)
        Else 'Si se esta editando se valida que los campos del item esten diligenciados
            If ValidateControls() = False Then 'Se validan que los controles esten diligenciados
                Exit Sub
            End If
            If String.IsNullOrEmpty(FixedAssetEntryItemDetail.Plate) Then
                FixedAssetEntryItemDetail.Plate = "INDP" + GetDateServer.Year.ToString + "#"
            End If
        End If

        'Se crea el objeto que se va a devolver
        Dim args As New AddFixedAssetEntryItemDetail
        args.FixedAssetEntryItemDetail = FixedAssetEntryItemDetail
        args.ListFixedAssetEntryItemDetail = ListFixedAssetEntryItemDetail
        args.EditMode = EditMode
        args.ListDeleteFixedAssetEntryItemDetailPartBook = ListDeleteFixedAssetEntryItemDetailPartBook
        args.ListDeleteFixedAssetEntryItemDetailPart = ListDeleteFixedAssetEntryItemDetailPart
        args.ListDeleteFixedAssetEntryItemDetailBook = ListDeleteFixedAssetEntryItemDetailBook
        RaiseEvent AddFixedAssetEntryItemDetailEventArgs(Nothing, args)
        CleanControls()
        INDtxtPlate.Focus()
        Me.Close()
    End Sub

    Private Function ValidateFieldList() As String
        Dim ListErrors As New StringBuilder
        If ListFixedAssetEntryItemDetail IsNot Nothing AndAlso ListFixedAssetEntryItemDetail.Count > 0 Then
            ListFixedAssetEntryItemDetail.ForEach(Sub(item)
                                                      If INDlyItemResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                                          If item.ReponsibleId = Nothing Then
                                                              ListErrors.AppendLine("- Debe seleccionar un responsable del item " + (ListFixedAssetEntryItemDetail.IndexOf(item) + 1).ToString)
                                                          End If
                                                          If item.LocationId = Nothing Then
                                                              ListErrors.AppendLine("- Debe seleccionar una localización del item " + (ListFixedAssetEntryItemDetail.IndexOf(item) + 1).ToString)
                                                          End If
                                                      End If
                                                      If item.AdquisitionDate = Nothing Then
                                                          ListErrors.AppendLine("- Debe seleccionar una fecha de adquisición del item " + (ListFixedAssetEntryItemDetail.IndexOf(item) + 1).ToString)
                                                      End If
                                                      If item.HandlesWarranty Then
                                                          If item.WarrantyExpirationDate Is Nothing Then
                                                              ListErrors.AppendLine("- Debe seleccionar una fecha de vencimiento del item " + (ListFixedAssetEntryItemDetail.IndexOf(item) + 1).ToString)
                                                          End If
                                                      End If
                                                      If item.StatusAssetId = Nothing Then
                                                          ListErrors.AppendLine("- Debe seleccionar un estado del item " + (ListFixedAssetEntryItemDetail.IndexOf(item) + 1).ToString)
                                                      End If
                                                  End Sub)
        End If
        If ValidSmallerAmount Is Nothing Then
            ListErrors.AppendLine("Debe seleccionar un parámetro de Activo Menor Cuantía")
        End If
        Return ListErrors.ToString
    End Function

    Private Sub AssigningValues()
        With FixedAssetEntryItemDetail
            .Plate = Plate
            .Serie = Serie
            If INDlyItemResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ResponsibleId IsNot Nothing Then
                    .ReponsibleId = ResponsibleId
                    .ResponsibleCodeName = INDsleResponsible.Text
                End If
                If LocationId IsNot Nothing Then
                    .LocationId = LocationId
                    .LocationCodeName = INDsleLocation.Text
                End If
            Else
                .ReponsibleId = Nothing
                .ResponsibleCodeName = String.Empty
                .LocationId = Nothing
                .LocationCodeName = String.Empty
            End If
            .AdquisitionDate = AdquisitionDate
            If Depreciate IsNot Nothing Then
                .Depreciate = Depreciate
            End If
            .Amortize = Amortize
            If ValidSmallerAmount IsNot Nothing Then
                .ValidSmallerAmount = ValidSmallerAmount
            End If
            If HandlesWarranty IsNot Nothing Then
                .HandlesWarranty = HandlesWarranty
            End If
            If INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .WarrantyExpirationDate = WarrantyExpirationDate
            Else
                .WarrantyExpirationDate = Nothing
            End If
            If StatusAssetId IsNot Nothing Then
                .StatusAssetId = StatusAssetId
                .StatusAssetCodeName = INDsleStatusAsset.Text
            End If

            .FixedAssetEntryItemDetailPart.Clear()
            If ListFixedAssetEntryItemDetailPart IsNot Nothing AndAlso ListFixedAssetEntryItemDetailPart.Count > 0 Then
                For Each item In ListFixedAssetEntryItemDetailPart
                    .FixedAssetEntryItemDetailPart.Add(item)
                Next
            End If

            If INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si hay detalles de libros
                .FixedAssetEntryItemDetailBook.Clear()
                If ListFixedAssetEntryItemDetailBook IsNot Nothing AndAlso ListFixedAssetEntryItemDetailBook.Count > 0 Then
                    For Each item In ListFixedAssetEntryItemDetailBook
                        .FixedAssetEntryItemDetailBook.Add(item)
                    Next
                End If
            Else 'Si no hay detalles de libros
                If .FixedAssetEntryItemDetailBook IsNot Nothing AndAlso .FixedAssetEntryItemDetailBook.Count > 0 Then
                    For Each item In (From l In .FixedAssetEntryItemDetailBook Where l.Id > 0 Select l).ToList
                        If ListDeleteFixedAssetEntryItemDetailBook Is Nothing Then
                            ListDeleteFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
                        Else
                            If ListDeleteFixedAssetEntryItemDetailBook.Contains(item) Then
                                Continue For
                            End If
                        End If
                        ListDeleteFixedAssetEntryItemDetailBook.Add(item)
                    Next
                    .FixedAssetEntryItemDetailBook.Clear()
                End If
            End If
        End With
    End Sub

    Private Sub OpenFormParts(_FlagModeEdit As Boolean)
        If Depreciate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar si el articulo deprecia o no."
            Exit Sub
        End If
        Using formulario As New FrmFixedAssetEntryItemDetailPart(Me._headCurrency)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddFixedAssetEntryItemPartEventArgs, AddressOf ReturnAddPartsEventArgs
            formulario.EditMode = _FlagModeEdit
            formulario.ListCompare = ListFixedAssetEntryItemDetailPart
            formulario.FixedAssetEntryItemDetailPart = FixedAssetEntryItemDetailPart
            formulario.DepreciateItem = Depreciate
            formulario.Size = New Drawing.Size(837, 696)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Sub ReturnAddPartsEventArgs(sender As Object, e As AddFixedAssetEntryItemDetailPart)
        If e.EditMode = False Then 'Se esta ingresando un articulo
            If ListFixedAssetEntryItemDetailPart Is Nothing Then
                ListFixedAssetEntryItemDetailPart = New List(Of FixedAssetEntryItemDetailPart)
            End If
            ListFixedAssetEntryItemDetailPart.Add(e.FixedAssetEntryItemDetailPart)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Se esta modificando un articulo
            ListFixedAssetEntryItemDetailPart.Remove(FixedAssetEntryItemDetailPart)
            ListFixedAssetEntryItemDetailPart.Insert(IndexEditRecord, e.FixedAssetEntryItemDetailPart)
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        If e.ListDeleteFixedAssetEntryItemDetailPartBook IsNot Nothing Then
            If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
            End If
            ListDeleteFixedAssetEntryItemDetailPartBook.AddRange(e.ListDeleteFixedAssetEntryItemDetailPartBook)
        End If

        INDgcParts.DataSource = Nothing
        INDgcParts.DataSource = ListFixedAssetEntryItemDetailPart
    End Sub

    Private Sub EditPart()
        FixedAssetEntryItemDetailPart = DirectCast(INDviewParts.GetFocusedRow(), FixedAssetEntryItemDetailPart)
        IndexEditRecord = ListFixedAssetEntryItemDetailPart.IndexOf(FixedAssetEntryItemDetailPart)
        OpenFormParts(True)
    End Sub

    Private Sub RemovePart()
        FixedAssetEntryItemDetailPart = CType(INDviewParts.GetFocusedRow, FixedAssetEntryItemDetailPart)
        ListFixedAssetEntryItemDetailPart.Remove(FixedAssetEntryItemDetailPart)

        If FixedAssetEntryItemDetailPart.Id > 0 Then
            If ListDeleteFixedAssetEntryItemDetailPart Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailPart = New List(Of FixedAssetEntryItemDetailPart)
            End If
            ListDeleteFixedAssetEntryItemDetailPart.Add(FixedAssetEntryItemDetailPart)

            'Recorro los libros de las partes si hay y las elimino
            If FixedAssetEntryItemDetailPart.FixedAssetEntryItemDetailPartBook IsNot Nothing AndAlso FixedAssetEntryItemDetailPart.FixedAssetEntryItemDetailPartBook.Count > 0 Then
                For Each item In (From l In FixedAssetEntryItemDetailPart.FixedAssetEntryItemDetailPartBook Where l.Id > 0 Select l).ToList
                    If ListDeleteFixedAssetEntryItemDetailPartBook Is Nothing Then
                        ListDeleteFixedAssetEntryItemDetailPartBook = New List(Of FixedAssetEntryItemDetailPartBook)
                    End If
                    If ListDeleteFixedAssetEntryItemDetailPartBook.Contains(item) Then
                        Continue For
                    End If
                    ListDeleteFixedAssetEntryItemDetailPartBook.Add(item)
                Next
            End If
        End If

        INDgcParts.DataSource = Nothing
        INDgcParts.DataSource = ListFixedAssetEntryItemDetailPart
    End Sub

    Private Sub AddDetailsBook()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            If ListFixedAssetEntryItemDetailBook Is Nothing Then
                ListFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
            Else
                'Se valida que el libro que se esta agregando no exista en la rejilla
                Dim cont = (From l In ListFixedAssetEntryItemDetailBook Where l.LegalBookId = LegalBookId Select l).Count
                If cont > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El libro " + INDsleLegalBook.Text + " ya existe en la lista."
                    Exit Sub
                End If
            End If
        End If

        'Se asigna los valores de la entidad
        SetValues()

        If FlagEditModePopupBook = False Then 'Si se esta agregando el registro
            ListFixedAssetEntryItemDetailBook.Add(FixedAssetEntryItemDetailBook)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta modificando el registro
            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetEntryItemDetailBook
        CleanControlsPopup()
        'INDsleLegalBook.Focus()
        INDpceDetailsBook.ClosePopup()
    End Sub

    Private Sub SetValues()
        If FlagEditModePopupBook = False Then
            FixedAssetEntryItemDetailBook = New FixedAssetEntryItemDetailBook
        End If
        With FixedAssetEntryItemDetailBook
            .LegalBookId = LegalBookId
            .LegalBookCodeName = INDsleLegalBook.Text
            .LifeTime = LifeTime
            .UnitLifeTime = UnitLifeTimeId
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

    Private Sub CleanControlsPopup()
        FlagEditModePopupBook = False
        LegalBookId = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        LifeTime = Nothing
        UnitLifeTimeId = Nothing
        DepreciationTypeId = Nothing
        TotalProductionUnit = Nothing
        PercentageRescue = Nothing
        INDlyItemTotalProductionUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPercentageRescue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleLegalBook.Properties.ReadOnly = False
        INDpceDetailsBook.Properties.ReadOnly = True
    End Sub

    Private Sub EditBook()
        FlagEditModePopupBook = True
        FixedAssetEntryItemDetailBook = DirectCast(INDviewDetailsBook.GetFocusedRow(), FixedAssetEntryItemDetailBook)
        With FixedAssetEntryItemDetailBook
            LegalBookId = .LegalBookId
            INDsleLegalBook.Properties.NullText = .LegalBookCodeName
            LifeTime = .LifeTime
            UnitLifeTimeId = .UnitLifeTime
            DepreciationTypeId = .DepreciationType
            TotalProductionUnit = .TotalProductionUnit
            PercentageRescue = .PercentageRescue
        End With
        INDsleLegalBook.Properties.ReadOnly = True
        INDpceDetailsBook.ShowPopup()
    End Sub

    Private Sub RemoveBook()
        FixedAssetEntryItemDetailBook = CType(INDviewDetailsBook.GetFocusedRow, FixedAssetEntryItemDetailBook)
        ListFixedAssetEntryItemDetailBook.Remove(FixedAssetEntryItemDetailBook)

        If FixedAssetEntryItemDetailBook.Id > 0 Then
            If ListDeleteFixedAssetEntryItemDetailBook Is Nothing Then
                ListDeleteFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
            End If
            ListDeleteFixedAssetEntryItemDetailBook.Add(FixedAssetEntryItemDetailBook)
        End If

        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetEntryItemDetailBook
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

        'Valida Menor Cuantia
        Dim ListValidSmallercAmount = New List(Of Tuple(Of Boolean, String))
        ListValidSmallercAmount.Add(New Tuple(Of Boolean, String)(1, "Deprecia como Menor Cuantía"))
        ListValidSmallercAmount.Add(New Tuple(Of Boolean, String)(0, "Deprecia según Vida Útil"))
        INDGleValidSmallerAmount.Properties.DataSource = ListValidSmallercAmount.ToList

    End Sub

    Private Sub ValidateDepreciatedItem(EventShown As Boolean)
        ' Se obtiene el artículo por id
        Dim itemXpo = Presenter.GetFixedAssetItemById(ItemId)
        If itemXpo Is Nothing OrElse itemXpo.Count = 0 Then Return ' Salir si no hay datos

        ' Se pasa a una entidad xpo para que sea más fácil manipularlo
        Dim EntityXpo As FixedAssetEquipmentXpo = itemXpo(0)

        ' Manejo de activos intangibles
        If EntityXpo.ItemCatalogId.Classification = 2 Then ' Es un activo Intangible
            HandleIntangibleAssets(EntityXpo, EventShown)
        Else ' Manejo de otros activos
            HandleTangibleAssets(EntityXpo, EventShown)
        End If

        ' Configuración final de depreciación
        If Not FlagDepreciate Then
            INDsleDepreciate.Properties.ReadOnly = True
            INDsleDepreciate.EditValue = False
        End If
    End Sub

    Private Sub HandleIntangibleAssets(EntityXpo As FixedAssetEquipmentXpo, EventShown As Boolean)
        INDlyItemDepreciate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Depreciate = False

        ' Se asigna el mismo valor
        If Not EditMode AndAlso EventShown Then ' Si está agregando el detalle
            SetDepreciateAmortizeValues(False, EntityXpo.Amortizes)
        End If

        If EntityXpo.Amortizes Then
            If EntityXpo.FixedAssetItemDetailXpo IsNot Nothing AndAlso EntityXpo.FixedAssetItemDetailXpo.Count > 0 Then
                PopulateFixedAssetEntryItemDetailBook(EntityXpo.FixedAssetItemDetailXpo)
            End If
        Else
            INDSleAmortize.Properties.ReadOnly = True
            INDSleAmortize.EditValue = False
        End If
    End Sub

    Private Sub HandleTangibleAssets(EntityXpo As FixedAssetEquipmentXpo, EventShown As Boolean)
        INDLciAmortize.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Amortize = False
        ' Se asigna el mismo valor
        If Not EditMode AndAlso EventShown Then ' Si está agregando el detalle
            SetDepreciateAmortizeValues(EntityXpo.AllowDepreciate, False)
        End If

        If EntityXpo.AllowDepreciate Then ' Permite depreciar
            If EventShown Then INDsleDepreciate.Properties.ReadOnly = False

            If Not EditMode OrElse Not EventShown Then ' Si está agregando el detalle
                If EntityXpo.FixedAssetItemDetailXpo IsNot Nothing AndAlso EntityXpo.FixedAssetItemDetailXpo.Count > 0 Then
                    PopulateFixedAssetEntryItemDetailBook(EntityXpo.FixedAssetItemDetailXpo)
                End If
            End If
        Else ' No permite depreciar
            If EventShown Then INDsleDepreciate.Properties.ReadOnly = True
            INDsleDepreciate.EditValue = False
        End If
    End Sub

    Private Sub PopulateFixedAssetEntryItemDetailBook(FixedAssetItemDetail As IList(Of FixedAssetItemDetailXpo))
        ListFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
        For Each item In FixedAssetItemDetail
            Dim _fixedAssetEntryItemDetailBook As New FixedAssetEntryItemDetailBook With {
            .LegalBookId = item.LegalBookId.Id,
            .LegalBookCodeName = item.LegalBookId.CodeName,
            .LifeTime = item.LifeTime,
            .UnitLifeTime = item.UnitLifeTime,
            .DepreciationType = item.DepreciationType,
            .TotalProductionUnit = item.TotalProductionUnit,
            .PercentageRescue = item.PercentageRescue
        }
            ListFixedAssetEntryItemDetailBook.Add(_fixedAssetEntryItemDetailBook)
        Next
        INDgcDetailsBook.DataSource = Nothing
        INDgcDetailsBook.DataSource = ListFixedAssetEntryItemDetailBook
    End Sub

    Private Sub SetDepreciateAmortizeValues(depreciate As Boolean, amortize As Boolean)
        FlagChangedSearch = False
        depreciate = depreciate
        amortize = amortize
        FlagChangedSearch = True
    End Sub

    Private Sub NewEntities()
        ListFixedAssetEntryItemDetail = New List(Of FixedAssetEntryItemDetail)
        For i = 1 To CountList Step 1
            ListFixedAssetEntryItemDetail.Add(New FixedAssetEntryItemDetail With {.IsFirstSetValues = True})
        Next
    End Sub

    Private Sub SetValuesToList(_entryItemDetail As FixedAssetEntryItemDetail, _changedPlateAndSerie As Boolean)
        With _entryItemDetail
            .Plate = String.Empty
            .Serie = String.Empty
            If _changedPlateAndSerie Then
                .Plate = Plate
                .Serie = Serie
            End If
            If INDlyItemResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ResponsibleId IsNot Nothing Then
                    .ReponsibleId = ResponsibleId
                    .ResponsibleCodeName = INDsleResponsible.Text
                End If
                If LocationId IsNot Nothing Then
                    .LocationId = LocationId
                    .LocationCodeName = INDsleLocation.Text
                End If
            Else
                .ReponsibleId = Nothing
                .ResponsibleCodeName = String.Empty
                .LocationId = Nothing
                .LocationCodeName = String.Empty
            End If
            .AdquisitionDate = AdquisitionDate
            If Depreciate IsNot Nothing Then
                .Depreciate = Depreciate
            End If
            .Amortize = Amortize
            If HandlesWarranty IsNot Nothing Then
                .HandlesWarranty = HandlesWarranty
            End If
            If INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .WarrantyExpirationDate = WarrantyExpirationDate
            Else
                .WarrantyExpirationDate = Nothing
            End If
            If StatusAssetId IsNot Nothing Then
                .StatusAssetId = StatusAssetId
            End If
            .StatusAssetCodeName = INDsleStatusAsset.Text
            .IsFirstSetValues = False

            If Depreciate And ValidSmallerAmount IsNot Nothing Then
                .ValidSmallerAmount = ValidSmallerAmount
            End If

            .FixedAssetEntryItemDetailPart.Clear()
            If ListFixedAssetEntryItemDetailPart IsNot Nothing AndAlso ListFixedAssetEntryItemDetailPart.Count > 0 Then
                For Each item In ListFixedAssetEntryItemDetailPart
                    .FixedAssetEntryItemDetailPart.Add(item.Clone)
                Next
            End If

            If INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then 'Si hay detalles de libros
                .FixedAssetEntryItemDetailBook.Clear()
                If ListFixedAssetEntryItemDetailBook IsNot Nothing AndAlso ListFixedAssetEntryItemDetailBook.Count > 0 Then
                    For Each item In ListFixedAssetEntryItemDetailBook
                        .FixedAssetEntryItemDetailBook.Add(item.Clone)
                    Next
                End If
            Else 'Si no hay detalles de libros
                If .FixedAssetEntryItemDetailBook IsNot Nothing AndAlso .FixedAssetEntryItemDetailBook.Count > 0 Then
                    For Each item In (From l In .FixedAssetEntryItemDetailBook Where l.Id > 0 Select l).ToList
                        If ListDeleteFixedAssetEntryItemDetailBook Is Nothing Then
                            ListDeleteFixedAssetEntryItemDetailBook = New List(Of FixedAssetEntryItemDetailBook)
                        Else
                            If ListDeleteFixedAssetEntryItemDetailBook.Contains(item) Then
                                Continue For
                            End If
                        End If
                        ListDeleteFixedAssetEntryItemDetailBook.Add(item)
                    Next
                    .FixedAssetEntryItemDetailBook.Clear()
                End If
            End If
        End With
    End Sub

    Private Sub NextOrLast(IsNext As Boolean)
        If EditMode = False Then 'Si se esta agregando
            Dim index = ListFixedAssetEntryItemDetail.IndexOf(FixedAssetEntryItemDetail)
            If IsNext Then 'Si es el siguiente
                index = index + 1
                If index <= ListFixedAssetEntryItemDetail.Count - 1 Then
                    Me.BarraBotones.Focus()
                    BarraBotones.FilterNavigationPosition(index)
                    INDtxtPlate.Focus()
                End If
            Else 'Si es el anterior
                index = index - 1
                If index >= 0 Then
                    Me.BarraBotones.Focus()
                    BarraBotones.FilterNavigationPosition(index)
                    INDtxtPlate.Focus()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        ListFixedAssetEntryItemDetailPart = Nothing
        FixedAssetEntryItemDetailPart = Nothing
        IndexEditRecord = Nothing
        FlagEditModePopupBook = Nothing
        ListFixedAssetEntryItemDetailBook = Nothing
        FixedAssetEntryItemDetailBook = Nothing
        ListDeleteFixedAssetEntryItemDetailBook = Nothing
        ListUnitLifeUtil = Nothing
        ListDepreciationType = Nothing
        FlagChangedSearch = Nothing
        FlagIsNavigatingBack = Nothing
        ListDeleteFixedAssetEntryItemDetailPartBook = Nothing
        ListDeleteFixedAssetEntryItemDetailPart = Nothing
        FlagDepreciate = Nothing
        ValidSmallerAmount = Nothing
    End Sub

    Private Sub FrmFixedAssetEntryItemDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If FlagDepreciate = True Then
            INDsleDepreciate.Enabled = True
        Else
            INDsleDepreciate.Enabled = False
            INDsleDepreciate.EditValue = False
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(EntryDate.Year.ToString + "/" + EntryDate.Month.ToString + "/01")
        INDdteAdquisitionDate.Properties.MinValue = dateMin
        INDdteAdquisitionDate.Properties.MaxValue = GetDateServer()
        INDdteWarrantyExpirationDate.Properties.MinValue = EntryDate
        InitializeTuple()
        Presenter = New PFixedAssetEntryItemDetail(Me)
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True

        IndigoGridControl1.RefreshGrid(INDgcParts)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDviewParts, ListActions)

        IndigoGridControl1.RefreshGrid(INDgcDetailsBook)
        Dim ListActionsBook As New List(Of eAcciones)
        'ListActionsBook.Add(eAcciones.Remove)
        ListActionsBook.Add(eAcciones.Edit)
        IndigoGridView2.SetListAcction(INDviewDetailsBook, ListActionsBook)

        If GetLocationResponsible = 1 Then 'Si es general se ocultan
            INDlyItemResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemResponsible.AllowHide = True
            INDlyItemLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemLocation.AllowHide = True
        Else 'Si es especifico se muestran
            INDlyItemResponsible.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemResponsible.AllowHide = False
            INDlyItemLocation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemLocation.AllowHide = False
        End If

        Me.BarraBotones.ColumnInfo = {New ColumnInfo With {.Caption = "Placa", .FieldName = "Plate"},
                                      New ColumnInfo With {.Caption = "Serie", .FieldName = "Serie"}}.ToList()
        If EditMode Then 'Si esta en modo edición
            ListFixedAssetEntryItemDetail = Nothing
            LoadControls(FixedAssetEntryItemDetail)
        Else
            AdquisitionDate = EntryDate
            NewEntities()
            FixedAssetEntryItemDetail = ListFixedAssetEntryItemDetail(0)
        End If
        If FixedAssetEntryItemDetail.FixedAssetEntryItem IsNot Nothing Then
            'Bloqueamos controles si el activo esta confirmado
            If FixedAssetEntryItemDetail?.FixedAssetEntryItem?.FixedAssetEntry?.Status = 2 Then
                ActionsOnControls = False
            End If
        End If
        FlagChangedSearch = False
        Me.BarraBotones.FilterDataSource = ListFixedAssetEntryItemDetail
        FlagChangedSearch = True
        If Depreciate Then
            HandleDepreciateAmortizeChanged(Depreciate)
            LayoutControl1.Update()
        End If

    End Sub
    ''' <summary>
    ''' Habilita o desabilita controles 
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetEntryItemDetail.ActionsOnControls
        Set(value As Boolean)
            INDtxtPlate.Enabled = value
            INDtxtSerie.Enabled = value
            INDsleResponsible.Enabled = value
            INDsleLocation.Enabled = value
            INDdteAdquisitionDate.Enabled = value
            INDbtnAddDetail.Enabled = value
            INDbtnAddParts.Enabled = value
            INDsleStatusAsset.Enabled = value
            INDsleDepreciate.Enabled = value
            INDSleAmortize.Enabled = value
            INDGleValidSmallerAmount.Enabled = value
            INDsleHandlesWarranty.Enabled = value
            INDdteWarrantyExpirationDate.Enabled = value


        End Set
    End Property

#End Region

#Region "ButtonClick"

    Private Sub INDsleResponsible_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleResponsible.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1711, Nothing, True)
            Presenter.InitializeResponsible()
        End If
    End Sub

    Private Sub INDsleStatusAsset_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleStatusAsset.ButtonClick

    End Sub

    Private Sub INDsleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1682, Nothing, True)
            Presenter.InitializeLegalBook()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleResponsible_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleResponsible.QueryPopUp
        If ResponsibleXpo Is Nothing Then
            Presenter.InitializeResponsible()
        End If
    End Sub

    Private Sub INDsleStatusAsset_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleStatusAsset.QueryPopUp
        If StatusAssetXpo Is Nothing Then
            Presenter.InitializeStatusAsset()
        End If
    End Sub

    Private Sub INDsleLegalBook_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLegalBook.QueryPopUp
        If LegalBookXpo Is Nothing Then
            Presenter.InitializeLegalBook()
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmFixedAssetEntryItemDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDtxtPlate.Focus()
        Presenter.InitializeLocation()
        ValidateDepreciatedItem(True)
    End Sub

#End Region

#Region "EditValueChanged"
    Private Sub INDsleHandlesWarranty_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleHandlesWarranty.EditValueChanged
        If HandlesWarranty IsNot Nothing Then
            If HandlesWarranty Then 'Maneja garantia
                INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemWarrantyExpirationDate.AllowHide = False
            Else 'No maneja garatia
                INDlyItemWarrantyExpirationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemWarrantyExpirationDate.AllowHide = True
            End If
        End If
    End Sub

    Private Sub INDsleDepreciate_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDepreciate.EditValueChanged
        HandleDepreciateAmortizeChanged(Depreciate)
        If Depreciate And _UnitValue <= SettingsFixedAsset.TopMinorValue Then
            ShowSmallerAmount()
        Else
            HideSmallerAmount()
        End If
    End Sub

    Private Sub INDSleAmortize_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAmortize.EditValueChanged
        If FlagChangedSearch Then
            HandleDepreciateAmortizeChanged(Amortize)
            HideSmallerAmount()
        End If
    End Sub

    Private Sub HandleDepreciateAmortizeChanged(isActive As Boolean)
        If isActive Then 'Si deprecia muestra el grupo de los libros
            INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If FlagChangedSearch AndAlso (ListFixedAssetEntryItemDetailBook Is Nothing OrElse ListFixedAssetEntryItemDetailBook.Count = 0) Then
                ValidateDepreciatedItem(False)
            End If
        Else 'Si no deprecia no muestra el grupo de los libros
            INDlygDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub ShowSmallerAmount()
        INDLciValidSmallerAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        ValidSmallerAmount = Nothing
    End Sub

    Private Sub HideSmallerAmount()
        INDLciValidSmallerAmount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ValidSmallerAmount = False
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

#End Region

#Region "Click"

    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddItemDetail()
    End Sub

    Private Sub INDbtnAddParts_Click(sender As Object, e As EventArgs) Handles INDbtnAddParts.Click
        FixedAssetEntryItemDetailPart = Nothing
        OpenFormParts(False)
    End Sub

    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        AddDetailsBook()
    End Sub

#End Region

#Region "MenuContext"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditPart()
            Case "Remove"
                RemovePart()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditPart()
            Case "Remove"
                RemovePart()
        End Select
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        INDpceDetailsBook.Properties.ReadOnly = False
        EditBook()
    End Sub

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        INDpceDetailsBook.Properties.ReadOnly = False
        EditBook()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub FrmFixedAssetEntryItemDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Right Then
            NextOrLast(True)
        ElseIf e.KeyCode = System.Windows.Forms.Keys.Left Then
            NextOrLast(False)
        End If
    End Sub

    Private Sub INDpceDetailsBook_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetailsBook.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetailsBook.ShowPopup()
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

#End Region

#Region "CustomColumnDisplayText"

    Private Sub INDviewDetailsBook_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewDetailsBook.CustomColumnDisplayText
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

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Evento del siguiente siguiente de la barra botones
    ''' </summary>
    ''' <param name="Record"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_RecordNavigationChangeEvent(Record As Object) Handles BarraBotones.RecordNavigationChangeEvent
        If FlagIsNavigatingBack Then
            FlagIsNavigatingBack = False
            Exit Sub
        End If
        If FlagChangedSearch = False Then
            Exit Sub
        End If
        If ValidSmallerAmount Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un párametro de Activo Menor Cuantía"
            FlagIsNavigatingBack = True
            BarraBotones.FilterNavigationPosition(BarraBotones.LastRecordPosition)
            Exit Sub
        End If
        If ListFixedAssetEntryItemDetail.Where(Function(item) item.IsFirstSetValues = True).Count > 0 Then
            ListFixedAssetEntryItemDetail.ForEach(Sub(item)
                                                      SetValuesToList(item, False)
                                                  End Sub)
            SetValuesToList(CType(Record, FixedAssetEntryItemDetail), False)
        End If
        SetValuesToList(FixedAssetEntryItemDetail, True)
        FixedAssetEntryItemDetail = CType(Record, FixedAssetEntryItemDetail)
        LoadControls(FixedAssetEntryItemDetail)
    End Sub

#End Region

End Class