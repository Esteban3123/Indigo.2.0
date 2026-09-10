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
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.FixedAssetRepository

#End Region

Public Class FrmValorizationDetail

#Region "Builder"

    ''' <summary>
    ''' Inicializa una instancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(Optional _currency As Currency = Nothing, Optional _tRMValue As Decimal = 1, Optional _roundType As Decimal = 1, Optional _documentDate As Date = Nothing)
        InitializeComponent()
        Me._tRMValue = _tRMValue
        Me._indigo = SessionValues.Instance
        Me._headCurrency = If(_currency Is Nothing, New Currency With {.Id = Me._indigo.OfficialCurrencyId,
                            .Abbreviation = Me._indigo.CurrencyISO4217}, _currency)
        Me.SetCurrencyUI(Me._headCurrency?.Abbreviation)
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(Me._headCurrency.Abbreviation.GetCultureId()).NumberFormat
        _culture.NumberFormat.CurrencyDecimalDigits = Utils.MaskByCurrencyRounding(_roundType, _culture.NumberFormat)
        roundingDecimals = _roundType
        Me.changeNumericFormatByCurrency(_culture.NumberFormat)
        Me._documentDate = _documentDate
    End Sub

#End Region

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddFixedAssetEntryItemPartEventArgs(sender As Object, e As AddValorizationDetailEventArgs)

#End Region

#Region "Properties"

    Private MyTag As String = "1121"

    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Private ListTransactionClass As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Private ListTransactionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Private ListValorizationType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Tupla para el tipo de regla
    ''' </summary>
    Private ListUnitLifeTime As New List(Of Tuple(Of Integer, String))

    Private _editMode As Boolean
    Public Property EditMode As Boolean
        Get
            Return _editMode
        End Get
        Set(value As Boolean)
            _editMode = value
        End Set
    End Property

    Private _FixedAssetTransactionDetail As FixedAssetTransactionDetail
    Public Property FixedAssetTransactionDetail As FixedAssetTransactionDetail
        Get
            Return _FixedAssetTransactionDetail
        End Get
        Set(value As FixedAssetTransactionDetail)
            _FixedAssetTransactionDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return INDSlItem.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlItem.Properties.DataSource = value
        End Set
    End Property

    Public Property PartsXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return INDslParts.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDslParts.Properties.DataSource = value
        End Set
    End Property

    Property AssetMainAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return INDSlAssetMainAccount.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlAssetMainAccount.Properties.DataSource = value
        End Set
    End Property

    Public Property DiscountValue As Decimal
        Get
            Return INDTxtDiscountValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtDiscountValue.EditValue = value
        End Set
    End Property

    Public Property DiscountPercentage As Decimal
        Get
            Return INDSpDiscountPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDSpDiscountPercentage.EditValue = value
        End Set
    End Property

    Public Property IvaPercentage As Decimal
        Get
            Return INDSpIvaPercentage.EditValue
        End Get
        Set(value As Decimal)
            INDSpIvaPercentage.EditValue = value
        End Set
    End Property

    Public Property IvaValue As Decimal
        Get
            Return INDTxtIVAValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtIVAValue.EditValue = value
        End Set
    End Property

    Public Property TotalValue As Decimal
        Get
            Return INDTxtTotalValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtTotalValue.EditValue = value
        End Set
    End Property

    Public Property Value As Decimal
        Get
            Return INDSpValue.EditValue
        End Get
        Set(value As Decimal)
            INDSpValue.EditValue = value
        End Set
    End Property

    Dim ObjFixedAssetTransactionDetailBook As FixedAssetTransactionDetailBook

    Dim ListFixedAssetTransactionDetailBook As New List(Of FixedAssetTransactionDetailBook)

    Dim EquipmentDetail As XPCollection(Of FixedAssetPhysicalAssetDetailBookXpo)

    Dim PartsDetail As XPCollection(Of FixedAssetPhysicalAssetPartsDetailBookXpo)

    Public Property IvaXpo As XPInstantFeedbackSource
        Get
            Return INDsleIVA.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleIVA.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Globals"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PFixedAssetEntryItem

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private FixedAssetTransactionDetailBook As FixedAssetTransactionDetailBook

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Private IndexEditRecord As Integer

    ''' <summary>
    ''' Bandera que indica si se va a crear CXP
    ''' </summary>
    Public FlagAccountPayable As Boolean = False

    ''' <summary>
    ''' Parámetros de activos fijos 
    ''' </summary>
    Public SettingFixedAsset As SettingFixedAsset

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
    ''' variable para determinar el tipo de redondeo 
    ''' </summary>
    Private roundingDecimals As Decimal

    ''' <summary>
    ''' Variable fecha del documento
    ''' </summary>
    Private _documentDate As Date
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

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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

#End Region

#Region "Methods"

    Private Sub AddItem()

        Dim errors = FrmValidateControls()
        If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        'Se asigna los valores
        AssigningValues()

        'Se crea el objeto que se va a devolver
        Dim args As New AddValorizationDetailEventArgs
        args.FixedAssetTransactionDetail = FixedAssetTransactionDetail
        args.EditMode = EditMode
        RaiseEvent AddFixedAssetEntryItemPartEventArgs(Nothing, args)
        CleanControls()
        Me.Close()
    End Sub

    Private Sub AssigningValues()

        If Not EditMode Then 'Si se esta guardando el item se instancia el objeto
            FixedAssetTransactionDetail = New FixedAssetTransactionDetail
        End If

        With FixedAssetTransactionDetail

            .TransactionClass = INDsleTransactionClass.EditValue
            If INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PhysicalAssetId = INDSlItem.EditValue
                .NameItem = INDSlItem.Text
            Else
                .NameItem = String.Empty
                .PhysicalAssetId = 0
            End If

            If INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PhysicalAssetPartsId = INDslParts.EditValue
                .NamePart = INDslParts.Text
            Else
                .NamePart = String.Empty
                .PhysicalAssetPartsId = 0
            End If

            .TransactionType = INDSlTransactionType.EditValue
            If INDLciValorizationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ValorizationType = INDSlValorizationType.EditValue
            Else
                .ValorizationType = 0
            End If

            If INDSpValue.EditValue > 0 Then
                .Value = INDSpValue.EditValue
            Else
                .Value = 0
            End If

            .Detail = INDMeDetail.EditValue
            .AssetMainAccountId = INDSlAssetMainAccount.EditValue

            .AffectDepreciation = INDCtrYesNo.EditValue
            If INDCtrYesNo.EditValue = True Then
                If INDSpLifeUtil.EditValue > 0 Then
                    .LifeTime = INDSpLifeUtil.EditValue
                Else
                    .LifeTime = 0
                End If

                If INDSlUnitLifetime.EditValue > 0 Then
                    .UnitLifeTime = INDSlUnitLifetime.EditValue
                Else
                    .UnitLifeTime = 0
                End If

                .FixedAssetTransactionDetailBook.Clear()
                If ListFixedAssetTransactionDetailBook?.Any() Then
                    For Each item In ListFixedAssetTransactionDetailBook
                        .FixedAssetTransactionDetailBook.Add(item)
                    Next
                End If
            Else
                .LifeTime = 0
                .UnitLifeTime = 0
            End If

            .IVAId = INDsleIVA.EditValue
            .IvaPercentage = IvaPercentage
            .IvaValue = IvaValue
            .DiscountPercentage = DiscountPercentage
            .DiscountValue = DiscountValue
            .TotalValue = TotalValue
        End With
    End Sub

    Private Sub CleanControls()
        FixedAssetTransactionDetail = Nothing
        EditMode = False

        INDsleTransactionClass.Properties.ReadOnly = False
    End Sub

    Private Sub LoadControls()
        With FixedAssetTransactionDetail
            INDsleTransactionClass.EditValue = .TransactionClass
            If INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSlItem.EditValue = .PhysicalAssetId
                INDSlItem.Properties.NullText = .NameItem
            End If

            If INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDslParts.EditValue = .PhysicalAssetPartsId
                INDslParts.Properties.NullText = .NamePart
            End If
            LoadIVAItem()
            INDSlTransactionType.EditValue = .TransactionType
            INDCtrYesNo.EditValue = .AffectDepreciation
            INDMeDetail.EditValue = .Detail
            INDSlAssetMainAccount.EditValue = .AssetMainAccountId
            INDSpValue.EditValue = .Value
            INDSlValorizationType.EditValue = .ValorizationType
            INDSpLifeUtil.EditValue = .LifeTime
            INDSlUnitLifetime.EditValue = .UnitLifeTime

            INDsleIVA.EditValue = .IVAId
            INDSpIvaPercentage.EditValue = .IvaPercentage
            INDTxtIVAValue.EditValue = .IvaValue
            INDSpDiscountPercentage.EditValue = .DiscountPercentage
            INDTxtDiscountValue.EditValue = .DiscountValue
            INDTxtTotalValue.EditValue = .TotalValue

            If .FixedAssetTransactionDetailBook?.Any() Then
                ListFixedAssetTransactionDetailBook = New List(Of FixedAssetTransactionDetailBook)
                ListFixedAssetTransactionDetailBook = .FixedAssetTransactionDetailBook.ToList
                INDGcLegalBook.DataSource = ListFixedAssetTransactionDetailBook
            End If
        End With

        INDsleTransactionClass.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Carga la informacion de los registro IVA
    ''' </summary>
    Private Sub LoadIVAItem()
        If IvaXpo Is Nothing Then
            Using model As New MFixedAssetValorization(MyTag)
                IvaXpo = model.ListIVA()
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Tipo de adquisición
        ListTransactionClass = New List(Of Tuple(Of Integer, String))
        ListTransactionClass.Add(New Tuple(Of Integer, String)(1, "Activo"))
        ListTransactionClass.Add(New Tuple(Of Integer, String)(2, "Parte de Activo"))
        INDsleTransactionClass.Properties.DataSource = ListTransactionClass.ToList

        'Tipo de Transacción
        ListTransactionType = New List(Of Tuple(Of Integer, String))
        If FlagAccountPayable Then
            ListTransactionType.Add(New Tuple(Of Integer, String)(1, "Valorización"))
        Else
            ListTransactionType.Add(New Tuple(Of Integer, String)(1, "Valorización"))
            ListTransactionType.Add(New Tuple(Of Integer, String)(2, "Desvalorización"))
        End If
        INDSlTransactionType.Properties.DataSource = ListTransactionType.ToList

        ListUnitLifeTime = New List(Of Tuple(Of Integer, String))
        ListUnitLifeTime.Add(New Tuple(Of Integer, String)(1, "Año"))
        ListUnitLifeTime.Add(New Tuple(Of Integer, String)(2, "Mes"))
        ListUnitLifeTime.Add(New Tuple(Of Integer, String)(3, "Día"))
        INDSlUnitLifetime.Properties.DataSource = ListUnitLifeTime.ToList()
        INDsleUnitLifeUtil.Properties.DataSource = ListUnitLifeTime.ToList()

        ListValorizationType = New List(Of Tuple(Of Integer, String))
        ListValorizationType.Add(New Tuple(Of Integer, String)(0, "No Aplica"))
        ListValorizationType.Add(New Tuple(Of Integer, String)(1, "Adición"))
        ListValorizationType.Add(New Tuple(Of Integer, String)(2, "Mantenimiento"))
        ListValorizationType.Add(New Tuple(Of Integer, String)(3, "Mejora"))
        ListValorizationType.Add(New Tuple(Of Integer, String)(4, "Reparación"))
        INDSlValorizationType.Properties.DataSource = ListValorizationType.ToList
    End Sub

    Public Sub CreateItemLegalBooks()
        If Not ListFixedAssetTransactionDetailBook?.Any() Then
            ListFixedAssetTransactionDetailBook = New List(Of FixedAssetTransactionDetailBook)
        End If

        If EquipmentDetail?.Any() Then
            'Se limpían los detalles para agregar los nuevos sin registros anteriores o duplicación
            If ListFixedAssetTransactionDetailBook IsNot Nothing Then ListFixedAssetTransactionDetailBook.Clear()
            For Each itemEquipmentDetail As FixedAssetPhysicalAssetDetailBookXpo In EquipmentDetail
                Dim settingByBook = SettingFixedAsset?.SettingFixedAssetByLegalBook?.FirstOrDefault(Function(d) d.LegalBookId = itemEquipmentDetail.LegalBookId.Id)

                ObjFixedAssetTransactionDetailBook = New FixedAssetTransactionDetailBook()
                ObjFixedAssetTransactionDetailBook.LegalBookId = itemEquipmentDetail.LegalBookId.Id
                ObjFixedAssetTransactionDetailBook.CurrencyAbbreviationLegalBook = itemEquipmentDetail.LegalBookId.OfficialCurrencyId.Abbreviation
                ObjFixedAssetTransactionDetailBook.NameLegalBook = itemEquipmentDetail.LegalBookId.Name
                ObjFixedAssetTransactionDetailBook.Value = Value

                ObjFixedAssetTransactionDetailBook.LegalBook = New LegalBook With {.Id = itemEquipmentDetail.LegalBookId.Id, .OfficialCurrencyId = itemEquipmentDetail.LegalBookId.OfficialCurrencyId.Id}

                ObjFixedAssetTransactionDetailBook.LifeTime = INDSpLifeUtil.EditValue
                ObjFixedAssetTransactionDetailBook.UnitLifeTime = IIf(String.IsNullOrEmpty(INDSlUnitLifetime.EditValue), Nothing, INDSlUnitLifetime.EditValue)

                If settingByBook IsNot Nothing AndAlso settingByBook.IvaCost Then
                    ObjFixedAssetTransactionDetailBook.Value += IvaValue
                End If

                ListFixedAssetTransactionDetailBook.Add(ObjFixedAssetTransactionDetailBook)
            Next

            INDGcLegalBook.DataSource = Nothing
            INDGcLegalBook.DataSource = ListFixedAssetTransactionDetailBook
        End If
    End Sub

    Public Sub CreatePartsLegalBooks()

        ListFixedAssetTransactionDetailBook = New List(Of FixedAssetTransactionDetailBook)

        If PartsDetail IsNot Nothing AndAlso PartsDetail.Count > 0 Then

            For Each itemEquipmentDetail As FixedAssetPhysicalAssetPartsDetailBookXpo In PartsDetail
                ObjFixedAssetTransactionDetailBook = New FixedAssetTransactionDetailBook()
                ObjFixedAssetTransactionDetailBook.LegalBookId = itemEquipmentDetail.LegalBookId.Id
                ObjFixedAssetTransactionDetailBook.CurrencyAbbreviationLegalBook = itemEquipmentDetail.LegalBookId.OfficialCurrencyId.Abbreviation
                ObjFixedAssetTransactionDetailBook.NameLegalBook = itemEquipmentDetail.LegalBookId.Name
                ListFixedAssetTransactionDetailBook.Add(ObjFixedAssetTransactionDetailBook)
            Next

            INDGcLegalBook.DataSource = Nothing
            If ListFixedAssetTransactionDetailBook.Count > 0 Then
                INDGcLegalBook.DataSource = ListFixedAssetTransactionDetailBook
            End If
        End If
    End Sub

    Private Sub CleanControlsPopup()
        INDsleLegalBook.EditValue = Nothing
        INDsleLegalBook.Properties.NullText = String.Empty
        INDsleLegalBook.Properties.ReadOnly = False
        INDPopUpAddDetailBook.Properties.ReadOnly = True
    End Sub

    Private Sub OpenFormFixedAssetEntryItemDetail(EditMode As Boolean)
        FixedAssetTransactionDetailBook = DirectCast(INDGvLegalBook.GetFocusedRow(), FixedAssetTransactionDetailBook)
        With FixedAssetTransactionDetailBook
            INDsleLegalBook.EditValue = .LegalBookId
            INDsleLegalBook.Properties.NullText = .NameLegalBook
            INDTxtPopupValue.EditValue = .Value
            INDseLifeUtil.EditValue = .LifeTime
            INDsleUnitLifeUtil.EditValue = .UnitLifeTime
            INDsleLegalBook.Properties.ReadOnly = True
        End With

        INDsleLegalBook.Properties.ReadOnly = True
        INDPopUpAddDetailBook.Properties.ReadOnly = False
        INDPopUpAddDetailBook.ShowPopup()
    End Sub

    Private Sub EditDetail()
        FixedAssetTransactionDetailBook = DirectCast(INDGvLegalBook.GetFocusedRow(), FixedAssetTransactionDetailBook)
        IndexEditRecord = ListFixedAssetTransactionDetailBook.IndexOf(FixedAssetTransactionDetailBook)
        OpenFormFixedAssetEntryItemDetail(True)
    End Sub

    Private Sub AddDetailsBook()
        Try
            Dim errors = ValidateControlsPopup()
            If errors.Length > 0 Then 'Se validan que los controles esten diligenciados
                Mensaje(EeventViewerImages.Advertencia) = errors
                Exit Sub
            End If

            'Se asigna los valores de la entidad
            SetValues()

            Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente."

            INDGcLegalBook.DataSource = Nothing
            INDGcLegalBook.DataSource = ListFixedAssetTransactionDetailBook
            CleanControlsPopup()
            INDsleLegalBook.Focus()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Hubo un error agregando el Detalle."
        End Try
    End Sub

    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder
        If INDsleLegalBook.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un libro oficial.")
        End If
        If INDTxtPopupValue.EditValue = Nothing Then
            errors.AppendLine("Debe seleccionar un Valor Válido.")
        End If
        If INDseLifeUtil.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una vida útil.")
        End If
        If INDsleUnitLifeUtil.EditValue Is Nothing Then
            errors.AppendLine("Debe ingresar una Unidad de Vida válida.")
        End If

        Return errors.ToString
    End Function

    Private Function FrmValidateControls() As String
        Dim errors As New StringBuilder
        If INDsleTransactionClass.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una Clase de Transacción")
        End If
        If INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSlItem.EditValue = Nothing Then
                errors.AppendLine("Debe seleccionar un Activo.")
            End If
        End If

        If INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDslParts.EditValue = Nothing Then
                errors.AppendLine("Debe seleccionar una Parte.")
            End If
        End If

        If String.IsNullOrEmpty(INDMeDetail.EditValue) Then
            errors.AppendLine("Escriba un Detalle")
        End If

        If INDSlAssetMainAccount.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar una Cuenta de Activo")
        End If

        If INDSlTransactionType.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar un Tipo de Transacción")
        End If

        If INDCtrYesNo.EditValue Is Nothing Then
            errors.AppendLine("Debe seleccionar si Afecta Depreciación o No")
        Else
            If INDCtrYesNo.EditValue = True Then
                If INDSpValue.EditValue <= 0 AndAlso Not (INDSlUnitLifetime.EditValue IsNot Nothing And INDSpLifeUtil.EditValue > 0) Then
                    errors.AppendLine("Escriba un Valor válido superior a cero (0) Y/O una Vida Útil válida")
                End If

                If (INDSlUnitLifetime.EditValue Is Nothing AndAlso INDSpLifeUtil.EditValue > 0) Then
                    errors.AppendLine("Debe seleccionar una Unidad de Vida Útil válida")
                End If

                If INDGcLegalBook.DataSource Is Nothing Then
                    errors.AppendLine("Revise los Libros Oficiales. Debe existir al menos uno.")
                End If
            Else
                If INDSpValue.EditValue <= 0 Then
                    errors.AppendLine("Escriba un Valor Válido superior a cero (0)")
                End If
            End If
        End If

        If INDSpDiscountPercentage.EditValue < 0 Then
            errors.AppendLine("Escriba un porcentaje de Descuento Válido igual o superior a cero (0)")
        End If

        If INDSpIvaPercentage.EditValue < 0 Then
            errors.AppendLine("Escriba un porcentaje de IVA Válido igual o superior a cero (0)")
        End If

        Return errors.ToString
    End Function

    Private Sub CalculateDiscountValue()
        If DiscountPercentage <> Nothing AndAlso DiscountPercentage > 0 AndAlso Value <> Nothing AndAlso Value > 0 Then
            DiscountValue = Utils.RoundValue((Value * DiscountPercentage) / 100, Utils.RoundLevel.Unit)
        Else
            DiscountValue = 0
        End If
    End Sub

    Private Sub CalculateIVAValue()
        If Value <> Nothing AndAlso Value > 0 AndAlso IvaPercentage <> Nothing AndAlso IvaPercentage > 0 Then
            IvaValue = Utils.RoundValue(((Value - DiscountValue) * IvaPercentage) / 100, Utils.RoundLevel.Unit)
        Else
            IvaValue = 0
        End If
    End Sub

    Private Sub CalculateTotalValue()
        TotalValue = Value - DiscountValue + IvaValue
    End Sub

    Private Async Function ValuesListBookAsync() As Task
        Try
            Dim ListGcFixedAssetTransactionDetailBook = New List(Of FixedAssetTransactionDetailBook)

            If ListFixedAssetTransactionDetailBook IsNot Nothing And ListFixedAssetTransactionDetailBook.Count > 0 Then

                Using model As New MFixedAssetValorization(MyTag)
                    For Each ObjFixedAssetTransactionDetailBook As FixedAssetTransactionDetailBook In ListFixedAssetTransactionDetailBook

                        Dim settingByBook = SettingFixedAsset?.SettingFixedAssetByLegalBook?.FirstOrDefault(Function(d) d.LegalBookId = ObjFixedAssetTransactionDetailBook.LegalBookId)
                        Dim valueTmp = Value

                        If settingByBook?.IvaCost Then
                            valueTmp += IvaValue
                        End If

                        ObjFixedAssetTransactionDetailBook.Value = valueTmp

                        ' Obtiene el TRM  de la moneda oficial respecto a otra moneda
                        Dim resultTRM = Await model.GetTRMbyCurrencyId(_headCurrency.Id, ObjFixedAssetTransactionDetailBook.LegalBook.OfficialCurrencyId, _documentDate)

                        If resultTRM IsNot Nothing Then
                            ObjFixedAssetTransactionDetailBook.Value = resultTRM.ObjectEmbbeded.Value * ObjFixedAssetTransactionDetailBook.Value
                        End If

                        If INDLciLifeTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                            ObjFixedAssetTransactionDetailBook.LifeTime = INDSpLifeUtil.EditValue
                            ObjFixedAssetTransactionDetailBook.UnitLifeTime = IIf(String.IsNullOrEmpty(INDSlUnitLifetime.EditValue), Nothing, INDSlUnitLifetime.EditValue)
                        End If
                        ListGcFixedAssetTransactionDetailBook.Add(ObjFixedAssetTransactionDetailBook)
                    Next
                End Using

                If ListGcFixedAssetTransactionDetailBook IsNot Nothing And ListGcFixedAssetTransactionDetailBook.Count > 0 Then
                    INDGcLegalBook.DataSource = ListGcFixedAssetTransactionDetailBook
                End If
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    Private Sub SetValues()
        With FixedAssetTransactionDetailBook
            .LegalBookId = INDsleLegalBook.EditValue
            .NameLegalBook = INDsleLegalBook.Text
            .Value = INDTxtPopupValue.EditValue
            .LifeTime = INDseLifeUtil.EditValue
            .UnitLifeTime = INDsleUnitLifeUtil.EditValue
        End With
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        MyTag = Nothing
        ListTransactionClass = Nothing
        ListTransactionType = Nothing
        ListValorizationType = Nothing
        ListUnitLifeTime = Nothing
        _editMode = Nothing
        ObjFixedAssetTransactionDetailBook = Nothing
        ListFixedAssetTransactionDetailBook = Nothing
        EquipmentDetail = Nothing
        PartsDetail = Nothing
        Presenter = Nothing
        FixedAssetTransactionDetailBook = Nothing
        IndexEditRecord = Nothing
        FlagAccountPayable = Nothing
    End Sub

    Private Sub FrmFixedAssetEntryItem_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        InitializeTuple()

        Using model As New MFixedAssetValorization(MyTag)
            AssetMainAccountXpo = model.ListAccount()
        End Using

        IndigoGridView1.SetListAcction(INDGvLegalBook, {eAcciones.Edit}.ToList())
        If FlagAccountPayable Then
            INDLcgTotalValues.HideControl(False)
        Else
            INDLcgTotalValues.HideControl
        End If
        LoadIVAItem()
        If EditMode Then 'Si esta en modo edición
            LoadControls()
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

        Me.INDSpValue.Properties.Mask.Culture = _culture
        Me.INDTxtDiscountValue.Properties.Mask.Culture = _culture
        Me.INDTxtIVAValue.Properties.Mask.Culture = _culture
        Me.INDTxtTotalValue.Properties.Mask.Culture = _culture
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleItem_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleTransactionClass.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(572, Nothing, True)
            Presenter.InitializeItem()
        End If
    End Sub

    Private Sub INDsleTrademark_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1700, Nothing, True)
            Presenter.InitializeTrademark()
        End If
    End Sub

    Private Sub INDsleIVA_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1509, Nothing, True)
            Presenter.InitializeIVA()
        End If
    End Sub

    Private Sub INDslePolicy_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1702, Nothing, True)
            Presenter.InitializePolicy()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDslParts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDslParts.QueryPopUp
        If PartsXpo Is Nothing Then
            Using model As New MFixedAssetValorization(MyTag)
                PartsXpo = model.InitializeParts()
            End Using
        End If
    End Sub

    Private Sub INDSlItem_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSlItem.QueryPopUp
        If ItemXpo Is Nothing Then
            Using model As New MFixedAssetValorization(MyTag)
                ItemXpo = model.InitializeItem()
            End Using
        End If
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmFixedAssetEntryItem_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleTransactionClass.Focus()
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAddItem_Click(sender As Object, e As EventArgs) Handles INDBtnAddItem.Click
        AddItem()
    End Sub

    Private Sub INDbtnAddItemDetail_Click(sender As Object, e As EventArgs)
        OpenFormFixedAssetEntryItemDetail(False)
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleTransactionClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTransactionClass.EditValueChanged
        If INDsleTransactionClass.EditValue = 1 Then
            INDLcItem.ShowLayout()
            INDLciParts.HideLayout()
        Else
            INDLcItem.HideLayout()
            INDLciParts.ShowLayout()
        End If
    End Sub

    Private Async Sub INDSlItem_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlItem.EditValueChanged
        If ItemXpo IsNot Nothing Then
            If INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Dim modelXPO As New MFixedAssetValorization(MyTag)

                Dim FixedAssetFixedAssetPhysicalAssetXpo = Await modelXPO.GetPhysicalById(INDSlItem.EditValue)
                If FixedAssetFixedAssetPhysicalAssetXpo IsNot Nothing Then
                    EquipmentDetail = FixedAssetFixedAssetPhysicalAssetXpo.FixedAssetPhysicalAssetDetailBookXpo

                    Using model As New MFixedAssetValorization(MyTag)
                        Dim FixedAssetEquipmentCatalogXpo = Await model.EquipmentCatalogId(FixedAssetFixedAssetPhysicalAssetXpo.ItemId.ItemCatalogId.Id)
                        INDSlAssetMainAccount.EditValue = FixedAssetEquipmentCatalogXpo.IncomeAccountId
                    End Using
                    INDsleIVA.EditValue = FixedAssetFixedAssetPhysicalAssetXpo.ItemId.IVAId?.Id
                End If
                If INDLcgLegalBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    CreateItemLegalBooks()
                Else
                    INDGcLegalBook.DataSource = Nothing
                End If
            End If
        End If
    End Sub

    Private Async Sub INDslParts_EditValueChanged(sender As Object, e As EventArgs) Handles INDslParts.EditValueChanged
        If PartsXpo IsNot Nothing Then

            Dim FixedAssetPhysicalAssetPartsXpo = DirectCast(DirectCast(viewParts.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetPartsXpo)
            If FixedAssetPhysicalAssetPartsXpo IsNot Nothing Then
                PartsDetail = FixedAssetPhysicalAssetPartsXpo.FixedAssetPhysicalAssetPartsDetailBookXpo

                Using model As New MFixedAssetValorization(MyTag)
                    Dim FixedAssetEquipmentCatalogXpo = Await model.EquipmentCatalogId(FixedAssetPhysicalAssetPartsXpo.PhysicalAssetId.ItemId.ItemCatalogId.Id)
                    INDSlAssetMainAccount.EditValue = FixedAssetEquipmentCatalogXpo.IncomeAccountId
                End Using
                INDsleIVA.EditValue = FixedAssetPhysicalAssetPartsXpo.PhysicalAssetId.ItemId.IVAId?.Id
                If INDLcgLegalBook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    CreatePartsLegalBooks()
                Else
                    INDGcLegalBook.DataSource = Nothing
                End If

            End If
        End If
    End Sub

    Private Sub INDSlTransactionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlTransactionType.EditValueChanged
        If INDSlTransactionType.EditValue = 1 Then
            INDLciGenerateAccountPayable.ShowLayout()
            INDLciValorizationType.ShowLayout()
        Else
            INDLciGenerateAccountPayable.HideLayout()
            INDLciLifeTime.HideLayout()
            INDLciUnitLifeTime.HideLayout()
            INDLciValorizationType.HideLayout()
            INDCtrYesNo.EditValue = False
        End If
    End Sub

    Private Sub INDCtrYesNo_EditValueChanged(sender As Object, e As EventArgs) Handles INDCtrYesNo.EditValueChanged

        INDGcLegalBook.DataSource = Nothing

        If INDCtrYesNo.EditValue = True Then
            INDLciUnitLifeTime.ShowLayout()
            INDLciLifeTime.ShowLayout()
            INDLcgLegalBook.HideControl(False)
            ValuesListBookAsync()
        Else
            INDLciUnitLifeTime.HideLayout()
            INDLciLifeTime.HideLayout()
            INDLcgLegalBook.HideControl()
        End If

        If INDLcItem.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            'Está visible el ítem (Equipo), entonces se debe cargar el objeto los Libros del Equipo
            CreateItemLegalBooks()
        End If

        If INDLciParts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            'Está visible la Parte, entonces se debe cargar los libros de la Parte
            CreatePartsLegalBooks()
        End If
    End Sub

    Private Sub INDSpLifeUtil_EditValueChanged(sender As Object, e As EventArgs) Handles INDSpLifeUtil.EditValueChanged
        ValuesListBookAsync()
    End Sub

    Private Sub INDSlUnitLifetime_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlUnitLifetime.EditValueChanged
        ValuesListBookAsync()
    End Sub

    Private Sub INDSpValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDSpValue.EditValueChanged
        CalculateDiscountValue()
        CalculateIVAValue()
        CalculateTotalValue()
        ValuesListBookAsync()
    End Sub

    Private Sub INDSpDiscountPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDSpDiscountPercentage.EditValueChanged
        CalculateDiscountValue()
        CalculateIVAValue()
    End Sub

    Private Sub INDSpDiscountPercentage_ValueChanged(sender As Object, e As EventArgs) Handles INDSpDiscountPercentage.ValueChanged
        If INDSpDiscountPercentage.Value < 0 Then
            INDSpDiscountPercentage.Value = 0
        End If
    End Sub

    Private Sub INDTxtDiscountValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtDiscountValue.EditValueChanged
        CalculateTotalValue()
    End Sub

    Private Sub INDsleIVA_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleIVA.EditValueChanged
        If INDsleIVA.EditValue IsNot Nothing Then
            Using model As New MFixedAssetValorization(MyTag)
                IvaPercentage = model.GetIVAById(INDsleIVA.EditValue).Percentage
            End Using
        End If
    End Sub

    Private Sub INDSpIvaPercentage_EditValueChanged(sender As Object, e As EventArgs) Handles INDSpIvaPercentage.EditValueChanged
        CalculateIVAValue()
    End Sub

    Private Sub INDTxtIVAValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtIVAValue.EditValueChanged
        CalculateTotalValue()
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

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        EditDetail()
    End Sub

#End Region

#End Region



#Region "CustomColumnDisplayText"
    Private Sub INDGvLegalBook_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvLegalBook.CustomColumnDisplayText
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

        Dim currencyColumns As String() = {GridColumn16.Name}
        If currencyColumns.Contains(e.Column.Name) Then
            Dim GridColumn = e.Column
            Dim rowHandle As Integer = e.ListSourceRowIndex

            If rowHandle >= 0 Then
                Dim rowData = CType(INDGvLegalBook.GetRow(rowHandle), FixedAssetTransactionDetailBook)

                If rowData IsNot Nothing Then
                    Dim currencySymbol As String = rowData.CurrencyAbbreviationLegalBook
                    GridColumn = Window.Utils.FormatGrid(e.Column, currencySymbol)
                End If
            End If
        End If
    End Sub
#End Region

    Private Sub INDPopUpAddDetailBook_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPopUpAddDetailBook.CloseUp
        CleanControlsPopup()
    End Sub

    Private Sub INDbtnAddDetailsBook_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetailsBook.Click
        AddDetailsBook()
    End Sub

End Class