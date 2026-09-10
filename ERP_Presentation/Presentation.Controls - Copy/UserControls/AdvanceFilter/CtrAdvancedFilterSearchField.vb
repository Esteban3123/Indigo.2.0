'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Juan F. Tamayo
' Created          : 2013-08-24
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-08-24
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.XtraLayout
Imports DevExpress.XtraEditors
Imports System.Runtime.CompilerServices
Imports System.ComponentModel
Imports Presentation.Controls.TypeExtendedMethod

#End Region

Friend Class CtrAdvancedFilterSearchField

#Region "Consts"

    ''' <summary>
    ''' Texto por defecto del label del control
    ''' </summary>
    Private Const DISPLAY_TEXT As String = "Valor"
    ''' <summary>
    ''' lista de caracteres y palabras reservadas usadas para ataques de SqlInjection
    ''' </summary>
    Private _listSlqInjection As New List(Of String)(New String() {"--", ";", "/*", "*/", "@@", _
                                                                   "@", "char", "nchar", "varchar", "nvarchar", "alter", _
                                                                   "begin", "cast", "create", "cursor", "declare", "delete", _
                                                                   "drop", "end", "exec", "execute", "fetch", "insert", _
                                                                   "kill", "open", "select", "sys", "sysobjects", "syscolumns", _
                                                                   "table", "update", "xp_", "#", "'", "&", "%", "=", ">", "<", _
                                                                   "(", ")", "?", """", "[", "]", "{", "}", "´", "~", "`", "|"})

#End Region

#Region "Events"

    ''' <summary>
    ''' Se lanza cuando la tecla ENTER es presionada sobre el último control generado
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event EnterKeyPressedOnLastControl(ByVal sender As Object, ByVal e As EventArgs)

#End Region

#Region "Fields"

    ''' <summary>
    ''' Encapsula el tipo de dato segun la enumeracion de tipos
    ''' </summary>
    Private _eDataType As eTypes
    ''' <summary>
    ''' Encapsula el tipo de dato de busqueda
    ''' </summary>
    Private _dataType As Type
    ''' <summary>
    ''' Encapsula el tipo de criterio usado en la busqueda
    ''' </summary>
    Private _criteria As eCriteriaAdvancedFilter
    ''' <summary>
    ''' Encapsula el texto mostrado en el label del item
    ''' </summary>
    Private _text As String

#Region "On Runtime"

    ''' <summary>
    ''' Layout que encapsula el primer campo a generar
    ''' </summary>
    Private _itemField1 As LayoutControlItem
    ''' <summary>
    ''' Layout que encapsula el segundo campo a generar
    ''' </summary>
    Private _itemField2 As LayoutControlItem

#End Region

#End Region

#Region "Propiedades"

    ''' <summary>
    ''' Obtiene o asigna el texto mostrado en el label del control
    ''' </summary>
    ''' <value>Texto a mostrar</value>
    ''' <returns>El texto en el label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el texto mostrado en el label del control")>
    Public Property DisplayText As String
        Get
            Return Me._text.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._text = value.Trim()
                If Me._itemField1 IsNot Nothing Then
                    Me._itemField1.Text = Me._text.Trim()
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tipo de datos usado en la busqueda
    ''' </summary>
    ''' <value>Tipo de dato</value>
    ''' <returns>El tipo de dato</returns>
    <Category("Indigo"), Description("Obtiene o asigna el tipo de datos usado en la busqueda")>
    Public Property DataType As eTypes
        Get
            Return Me._eDataType
        End Get
        Set(value As eTypes)
            If Not Object.Equals(Me._eDataType, value) Then
                Me._dataType = Me.GetTypeBy_eTypes(value)
                If Not Object.Equals(Me._criteria, Nothing) Then
                    Me.CreateSearchFields()
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tipo de criterio usado en la busqueda
    ''' </summary>
    ''' <value>Tipo de criterio</value>
    ''' <returns>El tipo de criterio</returns>
    <Category("Indigo"), Description("Obtiene o asigna el tipo de criterio usado en la busqueda")>
    Public Property Criteria As eCriteriaAdvancedFilter
        Get
            Return Me._criteria
        End Get
        Set(value As eCriteriaAdvancedFilter)
            If Object.Equals(Me._criteria, Nothing) OrElse Not Object.Equals(Me._criteria, value) Then
                Me._criteria = value
                If Me._dataType IsNot Nothing Then
                    Me.CreateSearchFields()
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiuene un valor que indica si hay valores ingresados en los controles y si son válidos o no
    ''' </summary>
    ''' <returns>Valor que indica si los valores son válidos</returns>
    Public ReadOnly Property IsValidValues As Boolean
        Get
            If Me._itemField1 IsNot Nothing AndAlso Me._itemField1.Control IsNot Nothing AndAlso CType(Me._itemField1.Control, BaseEdit).EditValue IsNot Nothing Then
                If Me._eDataType = eTypes.E_String OrElse Me._eDataType = eTypes.E_Char Then
                    If CType(Me._itemField1.Control, BaseEdit).EditValue.ToString().Trim().Equals(String.Empty) Then
                        Me._itemField1.Control.Focus()
                        Return False
                    End If
                End If
            Else
                Return False
            End If
            If Me._itemField2 IsNot Nothing AndAlso Me._itemField2.Control IsNot Nothing Then
                If Me._eDataType = eTypes.E_String OrElse Me._eDataType = eTypes.E_Char Then
                    If CType(Me._itemField2.Control, BaseEdit).EditValue.ToString().Trim().Equals(String.Empty) Then
                        Me._itemField2.Control.Focus()
                        Return False
                    End If
                End If
            End If

            Return True
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Crea una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._dataType = Nothing
        Me._criteria = Nothing
        Me._eDataType = eTypes.E_Nothing
        Me._text = DISPLAY_TEXT
        Me.CreateSearchFields()
    End Sub

    ''' <summary>
    ''' Crea una nueva instancia de la clase
    ''' </summary>
    ''' <param name="dataType">Tipo de dato</param>
    ''' <param name="criteria">Tipo de criterio usado</param>
    ''' <param name="displayText">Texto a mostrar en el label del control</param>
    Public Sub New(ByVal dataType As eTypes, ByVal criteria As eCriteriaAdvancedFilter, Optional ByVal displayText As String = DISPLAY_TEXT)
        InitializeComponent()
        Me._dataType = Me.GetTypeBy_eTypes(dataType)
        Me._eDataType = dataType
        Me._criteria = criteria
        Me._text = Microsoft.VisualBasic.IIf(displayText IsNot Nothing, displayText.Trim(), DISPLAY_TEXT)
        Me.CreateSearchFields()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna el foco al primer control generado
    ''' </summary>
    Public Sub SetFocus()
        If Me._itemField1 IsNot Nothing AndAlso CType(Me._itemField1, LayoutControlItem).Control IsNot Nothing Then
            CType(Me._itemField1, LayoutControlItem).Control.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Reconfigura los controls por defecto
    ''' </summary>
    Public Sub ResetControls()
        Me._dataType = Nothing
        Me._criteria = Nothing
        Me.CreateSearchFields()
    End Sub

    ''' <summary>
    ''' Obtiene el tipo de dato segun la enumeracion pasada por parámetro
    ''' </summary>
    ''' <returns>El tipo segun la enumeración</returns>
    Private Function GetTypeBy_eTypes(ByVal e As eTypes) As Type
        Select Case e
            Case eTypes.E_Byte
                Return GetType(Byte)
            Case eTypes.E_SByte
                Return GetType(SByte)
            Case eTypes.E_UInt16
                Return GetType(UInt16)
            Case eTypes.E_UInt32
                Return GetType(UInt32)
            Case eTypes.E_UInt64
                Return GetType(UInt64)
            Case eTypes.E_Int16
                Return GetType(Int16)
            Case eTypes.E_Int32
                Return GetType(Int32)
            Case eTypes.E_Int64
                Return GetType(Int64)
            Case eTypes.E_Decimal
                Return GetType(Decimal)
            Case eTypes.E_Double
                Return GetType(Double)
            Case eTypes.E_Single
                Return GetType(Single)
            Case eTypes.E_Char
                Return GetType(Char)
            Case eTypes.E_String
                Return GetType(String)
            Case eTypes.E_DateTime
                Return GetType(DateTime)
            Case eTypes.E_Date
                Return GetType(E_Date)
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' Crea en tiempo de ejecución los campos necesarios según el tipo de datos y el criterio seleccionados
    ''' </summary>
    Private Sub CreateSearchFields()
        Me.INDlycRoot.Root.Clear()
        Me._itemField1 = Nothing
        Me._itemField2 = Nothing
        Me._itemField1 = Me.INDlycRoot.Root.AddItem()
        Me._itemField1.Name = "_itemField1"
        Me._itemField1.Text = Me._text
        Me._itemField1.TextAlignMode = TextAlignModeItem.CustomSize
        Me._itemField1.TextToControlDistance = 1
        Me._itemField1.ShowInCustomizationForm = False
        Me._itemField1.AllowHide = False
        Me._itemField1.TextSize = New System.Drawing.Size(80, 21)
        Me._itemField1.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me._itemField1.AppearanceItemCaption.Font = New Font("Segoe UI Light", 12.0!)
        Me._itemField1.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 144, 223)

        If Me._dataType IsNot Nothing AndAlso Not Object.Equals(Me._criteria, Nothing) Then
            If TypeExtendedMethod.IsNumericType(Me._dataType, False) Then
                Me.CreateNumericFields()
            ElseIf Me._dataType = GetType(E_Date) OrElse Me._dataType = GetType(DateTime) Then
                If Me._criteria = eCriteriaAdvancedFilter.Between Then
                    Me._itemField1.Control = New DateEdit()
                    CType(Me._itemField1.Control, DateEdit).TabIndex = 0
                    CType(Me._itemField1.Control, DateEdit).EnterMoveNextControl = True
                    CType(Me._itemField1.Control, DateEdit).Properties.ShowClear = False
                    CType(Me._itemField1.Control, DateEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
                    CType(Me._itemField1.Control, DateEdit).Properties.Mask.EditMask = "dd MMMM yyyy"
                    CType(Me._itemField1.Control, DateEdit).Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
                    CType(Me._itemField1.Control, DateEdit).Properties.Mask.UseMaskAsDisplayFormat = True
                    CType(Me._itemField1.Control, DateEdit).Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.Default
                    CType(Me._itemField1.Control, DateEdit).Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.Default
                    If Me._dataType = GetType(DateTime) Then
                        CType(Me._itemField1.Control, DateEdit).Properties.Mask.EditMask = "G"
                        CType(Me._itemField1.Control, DateEdit).Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True
                        CType(Me._itemField1.Control, DateEdit).Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True
                    End If

                    Me._itemField2 = Me.INDlycRoot.Root.AddItem()
                    Me._itemField2.Name = "_itemField2"
                    Me._itemField2.Text = "-"
                    Me._itemField2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    Me._itemField2.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                    Me._itemField2.TextAlignMode = TextAlignModeItem.CustomSize
                    Me._itemField2.TextToControlDistance = 1
                    Me._itemField2.ShowInCustomizationForm = False
                    Me._itemField2.AllowHide = False
                    Me._itemField2.TextSize = New System.Drawing.Size(50, 35)
                    Me._itemField1.AppearanceItemCaption.Font = New Font("Segoe UI Light", 12.0!)
                    Me._itemField2.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 144, 223)
                    Me._itemField2.Control = New DateEdit()
                    CType(Me._itemField2.Control, DateEdit).TabIndex = 1
                    CType(Me._itemField2.Control, DateEdit).EnterMoveNextControl = False
                    AddHandler CType(Me._itemField2.Control, DateEdit).KeyDown, AddressOf LastControl_KeyDown
                    CType(Me._itemField2.Control, DateEdit).Properties.ShowClear = False
                    CType(Me._itemField2.Control, DateEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
                    CType(Me._itemField2.Control, DateEdit).Properties.Mask.EditMask = "dd MMMM yyyy"
                    CType(Me._itemField2.Control, DateEdit).Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
                    CType(Me._itemField2.Control, DateEdit).Properties.Mask.UseMaskAsDisplayFormat = True
                    CType(Me._itemField2.Control, DateEdit).Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.Default
                    CType(Me._itemField2.Control, DateEdit).Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.Default
                    If Me._dataType = GetType(DateTime) Then
                        CType(Me._itemField2.Control, DateEdit).Properties.Mask.EditMask = "G"
                        CType(Me._itemField2.Control, DateEdit).Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True
                        CType(Me._itemField2.Control, DateEdit).Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True
                    End If
                    Me._itemField2.Move(Me._itemField1, Utils.InsertType.Right)
                    Me._itemField2.SizeConstraintsType = SizeConstraintsType.Custom
                    Me._itemField2.MinSize = New Size(1, 36)
                    Me._itemField2.MaxSize = New Size(0, 36)
                Else
                    Me._itemField1.Control = New DateEdit()
                    CType(Me._itemField1.Control, DateEdit).TabIndex = 0
                    CType(Me._itemField1.Control, DateEdit).EnterMoveNextControl = False
                    AddHandler CType(Me._itemField1.Control, DateEdit).KeyDown, AddressOf LastControl_KeyDown
                    CType(Me._itemField1.Control, DateEdit).Properties.ShowClear = False
                    CType(Me._itemField1.Control, DateEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
                    CType(Me._itemField1.Control, DateEdit).Properties.Mask.EditMask = "dd MMMM yyyy"
                    CType(Me._itemField1.Control, DateEdit).Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret
                    CType(Me._itemField1.Control, DateEdit).Properties.Mask.UseMaskAsDisplayFormat = True
                    CType(Me._itemField1.Control, DateEdit).Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.Default
                    CType(Me._itemField1.Control, DateEdit).Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.Default
                    If Me._dataType = GetType(DateTime) Then
                        CType(Me._itemField1.Control, DateEdit).Properties.Mask.EditMask = "G"
                        CType(Me._itemField1.Control, DateEdit).Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True
                        CType(Me._itemField1.Control, DateEdit).Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True
                    End If
                End If
            Else 'Cuando son Char, String o cualquier otro
                If Me._criteria = eCriteriaAdvancedFilter.Between Then
                    Me._itemField1.Control = New TextEdit()
                    CType(Me._itemField1.Control, TextEdit).TabIndex = 0
                    CType(Me._itemField1.Control, TextEdit).EnterMoveNextControl = True
                    AddHandler CType(Me._itemField1.Control, TextEdit).TextChanged, AddressOf TextEdit_TextChanged
                    If Me._dataType = GetType(Char) Then
                        CType(Me._itemField1.Control, TextEdit).Properties.MaxLength = 1
                    End If
                    CType(Me._itemField1.Control, TextEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)

                    Me._itemField2 = Me.INDlycRoot.Root.AddItem()
                    Me._itemField2.Name = "_itemField2"
                    Me._itemField2.Text = "-"
                    Me._itemField2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
                    Me._itemField2.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
                    Me._itemField2.TextAlignMode = TextAlignModeItem.CustomSize
                    Me._itemField2.TextToControlDistance = 1
                    Me._itemField2.ShowInCustomizationForm = False
                    Me._itemField2.AllowHide = False
                    Me._itemField2.TextSize = New System.Drawing.Size(50, 35)
                    Me._itemField2.AppearanceItemCaption.Font = New Font("Segoe UI Light", 12.0!)
                    Me._itemField2.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 144, 223)
                    Me._itemField2.Control = New TextEdit()
                    CType(Me._itemField2.Control, TextEdit).TabIndex = 1
                    CType(Me._itemField2.Control, TextEdit).EnterMoveNextControl = False
                    AddHandler CType(Me._itemField2.Control, TextEdit).KeyDown, AddressOf LastControl_KeyDown
                    If Me._dataType = GetType(Char) Then
                        CType(Me._itemField2.Control, TextEdit).Properties.MaxLength = 1
                    End If
                    CType(Me._itemField2.Control, TextEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
                    Me._itemField2.Move(Me._itemField1, Utils.InsertType.Right)
                    Me._itemField2.SizeConstraintsType = SizeConstraintsType.Custom
                    Me._itemField2.MinSize = New Size(1, 36)
                    Me._itemField2.MaxSize = New Size(0, 36)
                Else
                    Me._itemField1.Control = New TextEdit()
                    CType(Me._itemField1.Control, TextEdit).TabIndex = 0
                    CType(Me._itemField1.Control, TextEdit).TabStop = False
                    AddHandler CType(Me._itemField1.Control, TextEdit).KeyDown, AddressOf LastControl_KeyDown
                    AddHandler CType(Me._itemField1.Control, TextEdit).TextChanged, AddressOf TextEdit_TextChanged
                    If Me._dataType = GetType(Char) Then
                        CType(Me._itemField1.Control, TextEdit).Properties.MaxLength = 1
                    End If
                    CType(Me._itemField1.Control, TextEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
                End If
            End If
        Else
            Me._itemField1.Control = New TextEdit()
            CType(Me._itemField1.Control, TextEdit).TabIndex = 0
            CType(Me._itemField1.Control, TextEdit).EnterMoveNextControl = False
            AddHandler CType(Me._itemField1.Control, TextEdit).KeyDown, AddressOf LastControl_KeyDown
            AddHandler CType(Me._itemField1.Control, TextEdit).TextChanged, AddressOf TextEdit_TextChanged
            CType(Me._itemField1.Control, TextEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
        End If
        Me._itemField1.SizeConstraintsType = SizeConstraintsType.Custom
        Me._itemField1.MinSize = New Size(1, 36)
        Me._itemField1.MaxSize = New Size(0, 36)
    End Sub

    ''' <summary>
    ''' Crea los campos necesarios para todos los tipos de datos numéricos
    ''' </summary>
    Private Sub CreateNumericFields()
        If Me._criteria = eCriteriaAdvancedFilter.Between Then
            Me._itemField1.Control = New TextEdit()
            CType(Me._itemField1.Control, TextEdit).TabIndex = 0
            CType(Me._itemField1.Control, TextEdit).EnterMoveNextControl = True
            CType(Me._itemField1.Control, TextEdit).Properties.Mask.EditMask = "n" & Microsoft.VisualBasic.IIf(TypeExtendedMethod.IsNumericType(Me._dataType, True), "2", "0")
            CType(Me._itemField1.Control, TextEdit).Properties.Mask.MaskType = Mask.MaskType.Numeric
            CType(Me._itemField1.Control, TextEdit).Properties.Mask.UseMaskAsDisplayFormat = True
            CType(Me._itemField1.Control, TextEdit).Properties.MaxLength = TypeExtendedMethod.GetMaxValueNumericType(Me._dataType).ToString().Length
            CType(Me._itemField1.Control, TextEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
            'CType(Me._itemField1.Control, TextEdit).EditValue = 0
            'CType(Me._itemField1.Control, TextEdit).Refresh()

            Me._itemField2 = Me.INDlycRoot.Root.AddItem()
            Me._itemField2.Name = "_itemField2"
            Me._itemField2.Text = "-"
            Me._itemField2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Me._itemField2.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
            Me._itemField2.TextAlignMode = TextAlignModeItem.CustomSize
            Me._itemField2.TextToControlDistance = 1
            Me._itemField2.ShowInCustomizationForm = False
            Me._itemField2.AllowHide = False
            Me._itemField2.TextSize = New System.Drawing.Size(50, 35)
            Me._itemField2.AppearanceItemCaption.Font = New Font("Segoe UI Light", 12.0!)
            Me._itemField2.AppearanceItemCaption.ForeColor = Color.FromArgb(0, 144, 223)
            Me._itemField2.Control = New TextEdit()
            CType(Me._itemField2.Control, TextEdit).TabIndex = 1
            CType(Me._itemField2.Control, TextEdit).EnterMoveNextControl = False
            AddHandler CType(Me._itemField2.Control, TextEdit).KeyDown, AddressOf LastControl_KeyDown
            CType(Me._itemField2.Control, TextEdit).Properties.Mask.EditMask = "n" & Microsoft.VisualBasic.IIf(TypeExtendedMethod.IsNumericType(Me._dataType, True), "2", "0")
            CType(Me._itemField2.Control, TextEdit).Properties.Mask.MaskType = Mask.MaskType.Numeric
            CType(Me._itemField2.Control, TextEdit).Properties.Mask.UseMaskAsDisplayFormat = True
            CType(Me._itemField2.Control, TextEdit).Properties.MaxLength = TypeExtendedMethod.GetMaxValueNumericType(Me._dataType).ToString().Length
            CType(Me._itemField2.Control, TextEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
            'CType(Me._itemField2.Control, TextEdit).EditValue = 0
            'CType(Me._itemField2.Control, TextEdit).Refresh()
            Me._itemField2.Move(Me._itemField1, Utils.InsertType.Right)
            Me._itemField2.SizeConstraintsType = SizeConstraintsType.Custom
            Me._itemField2.MinSize = New Size(1, 36)
            Me._itemField2.MaxSize = New Size(0, 36)
        Else
            Me._itemField1.Control = New TextEdit()
            CType(Me._itemField1.Control, TextEdit).TabIndex = 0
            CType(Me._itemField1.Control, TextEdit).EnterMoveNextControl = False
            AddHandler CType(Me._itemField1.Control, TextEdit).KeyDown, AddressOf LastControl_KeyDown
            CType(Me._itemField1.Control, TextEdit).Properties.Mask.EditMask = "n" & Microsoft.VisualBasic.IIf(TypeExtendedMethod.IsNumericType(Me._dataType, True), "2", "0")
            CType(Me._itemField1.Control, TextEdit).Properties.Mask.MaskType = Mask.MaskType.Numeric
            CType(Me._itemField1.Control, TextEdit).Properties.Mask.UseMaskAsDisplayFormat = True
            CType(Me._itemField1.Control, TextEdit).Properties.MaxLength = TypeExtendedMethod.GetMaxValueNumericType(Me._dataType).ToString().Length
            CType(Me._itemField1.Control, TextEdit).Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
            'CType(Me._itemField1.Control, TextEdit).EditValue = 0
            'CType(Me._itemField1.Control, TextEdit).Refresh()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene una lista de los valores editados en los campos del control
    ''' </summary>
    ''' <returns>Lista de valores</returns>
    Public Function GetEditValues() As List(Of Object)
        Dim list As New List(Of Object)()
        If Me._itemField1 IsNot Nothing AndAlso Me._itemField1.Control IsNot Nothing Then
            list.Add(CType(Me._itemField1.Control, BaseEdit).EditValue)
        End If
        If Me._itemField2 IsNot Nothing AndAlso Me._itemField2.Control IsNot Nothing Then
            list.Add(CType(Me._itemField2.Control, BaseEdit).EditValue)
        End If
        Return list
    End Function

    ''' <summary>
    ''' Asigna los valores a los EditValue de cada control
    ''' </summary>
    ''' <param name="values">Lista de valores</param>
    Public Sub SetEditValues(ByVal values As List(Of Object))
        If Me._itemField1 IsNot Nothing AndAlso Me._itemField1.Control IsNot Nothing Then
            If values.Count > 0 Then
                CType(Me._itemField1.Control, BaseEdit).EditValue = values(0)
            End If
        End If
        If Me._itemField2 IsNot Nothing AndAlso Me._itemField2.Control IsNot Nothing Then
            If values.Count > 1 Then
                CType(Me._itemField2.Control, BaseEdit).EditValue = values(1)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Retorna el control a su configuración por defecto
    ''' </summary>
    Public Sub Reset()
        Me._dataType = Nothing
        Me._criteria = Nothing
        Me._eDataType = eTypes.E_Nothing
        Me.CreateSearchFields()
    End Sub

    ''' <summary>
    ''' Limpia la cadena de caracteres y palabras reservadas que puedan ser una amenaza para ataques de tipo SqlInjection
    ''' </summary>
    ''' <param name="str">Cadena a limpiar</param>
    ''' <returns>La cadena limpia de SqlInjection</returns>
    Private Function ClearSqlInjectionString(ByVal str As String) As String
        If str IsNot Nothing AndAlso Not str.Trim().Equals(String.Empty) Then
            For Each s As String In Me._listSlqInjection
                str = str.Trim().ToLower().Replace(s, "")
            Next
            Return str
        End If
        Return String.Empty
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se dispara el evento EnterKeyPressedOnLastControl
    ''' </summary>
    Private Sub LastControl_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            RaiseEvent EnterKeyPressedOnLastControl(Me, New EventArgs())
        End If
    End Sub

    ''' <summary>
    ''' Aqui se valida que el EditValue no contenga caracteres o palabras reservadas SqlInjection
    ''' </summary>
    Private Sub TextEdit_TextChanged(sender As Object, e As EventArgs)
        If sender IsNot Nothing Then
            If CType(sender, TextEdit).Text IsNot Nothing AndAlso Not CType(sender, TextEdit).Text.Trim().Equals(String.Empty) Then
                Dim aux = Me.ClearSqlInjectionString(CType(sender, TextEdit).Text.Trim())
                If Not CType(sender, TextEdit).Text.Trim().ToLower().Trim().Equals(aux.Trim().ToLower()) Then
                    CType(sender, TextEdit).EditValue = aux
                    CType(sender, TextEdit).Refresh()
                End If
            End If
        End If
    End Sub

#End Region

End Class

#Region "MyTypes"

''' <summary>
''' Identifica los tipos de dato Date
''' </summary>
Friend Class E_Date

    ''' <summary>
    ''' Contiene su propia instancia
    ''' </summary>
    Public Property MyType As String = "E_Date"

End Class

#End Region

#Region "Enums"

''' <summary>
''' Enumera los tipos aceptados por el control
''' </summary>
<Serializable>
Public Enum eTypes
    E_Invalid
    E_Nothing
    E_Byte
    E_SByte
    E_UInt16
    E_UInt32
    E_UInt64
    E_Int16
    E_Int32
    E_Int64
    E_Decimal
    E_Double
    E_Single
    E_Date
    E_DateTime
    E_Char
    E_String
End Enum

#End Region

Public Module TypeExtendedMethod

#Region "Methods"

    ''' <summary>
    ''' Evalua si el tipo de dato es numérico
    ''' </summary>
    ''' <param name="o">Objeto que extiende el método</param>
    ''' <param name="isDecimal">Valor que indica si se evalua que el tipo sea un numérico que soporte decimales</param>
    ''' <returns>Un valor que indica si el tipo es numérico</returns>
    <Extension()>
    Public Function IsNumericType(ByVal o As Type, ByVal isDecimal As Boolean) As Boolean
        If Not isDecimal Then
            If Type.GetTypeCode(o) = TypeCode.Byte OrElse Type.GetTypeCode(o) = TypeCode.SByte OrElse Type.GetTypeCode(o) = TypeCode.UInt16 OrElse Type.GetTypeCode(o) = TypeCode.UInt32 OrElse Type.GetTypeCode(o) = TypeCode.UInt64 OrElse Type.GetTypeCode(o) = TypeCode.Int16 OrElse Type.GetTypeCode(o) = TypeCode.Int32 OrElse Type.GetTypeCode(o) = TypeCode.Int64 OrElse Type.GetTypeCode(o) = TypeCode.Decimal OrElse Type.GetTypeCode(o) = TypeCode.Double OrElse Type.GetTypeCode(o) = TypeCode.Single Then
                Return True
            Else
                Return False
            End If
        Else
            If Type.GetTypeCode(o) = TypeCode.Decimal OrElse Type.GetTypeCode(o) = TypeCode.Double Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene el valor minimo para el tipo si es numérico
    ''' </summary>
    ''' <param name="o">Objeto quien extiende el método</param>
    ''' <returns>El valor minimo del tipo numerico</returns>
    <Extension()>
    Public Function GetMinValueNumericType(ByVal o As Type) As Double
        If o.IsNumericType(False) Then
            Select Case Type.GetTypeCode(o)
                Case TypeCode.Byte
                    Return Byte.MinValue
                Case TypeCode.SByte
                    Return SByte.MinValue
                Case TypeCode.UInt16
                    Return UInt16.MinValue
                Case TypeCode.UInt32
                    Return UInt32.MinValue
                Case TypeCode.UInt64
                    Return UInt64.MinValue
                Case TypeCode.Int16
                    Return Int16.MinValue
                Case TypeCode.Int32
                    Return Int32.MinValue
                Case TypeCode.Int64
                    Return Int64.MinValue
                Case TypeCode.Decimal
                    Return Decimal.MinValue
                Case TypeCode.Double
                    Return Double.MinValue
                Case TypeCode.Single
                    Return Single.MinValue
            End Select
        Else
            Return -1
        End If
    End Function

    ''' <summary>
    ''' Obtiene el valor maximo para el tipo si es numérico
    ''' </summary>
    ''' <param name="o">Objeto quien extiende el método</param>
    ''' <returns>El valor máximo del tipo numerico</returns>
    <Extension()>
    Public Function GetMaxValueNumericType(ByVal o As Type) As Double
        If o.IsNumericType(False) Then
            Select Case Type.GetTypeCode(o)
                Case TypeCode.Byte
                    Return Byte.MaxValue
                Case TypeCode.SByte
                    Return SByte.MaxValue
                Case TypeCode.UInt16
                    Return UInt16.MaxValue
                Case TypeCode.UInt32
                    Return UInt32.MaxValue
                Case TypeCode.UInt64
                    Return UInt64.MaxValue
                Case TypeCode.Int16
                    Return Int16.MaxValue
                Case TypeCode.Int32
                    Return Int32.MaxValue
                Case TypeCode.Int64
                    Return Int64.MaxValue
                Case TypeCode.Decimal
                    Return Decimal.MaxValue
                Case TypeCode.Double
                    Return Double.MaxValue
                Case TypeCode.Single
                    Return Single.MaxValue
            End Select
        Else
            Return -1
        End If
    End Function

    ''' <summary>
    ''' Obtiene el tipo de dato segun la enumeracion
    ''' </summary>
    ''' <param name="o">Objeto que extiende el método</param>
    ''' <returns>El tipo segun la enumeración</returns>
    <Extension()>
    Public Function Get_eTypes(ByVal o As Type) As eTypes
        Select Case Type.GetTypeCode(o)
            Case TypeCode.Byte
                Return eTypes.E_Byte
            Case TypeCode.SByte
                Return eTypes.E_SByte
            Case TypeCode.UInt16
                Return eTypes.E_UInt16
            Case TypeCode.UInt32
                Return eTypes.E_UInt32
            Case TypeCode.UInt64
                Return eTypes.E_UInt64
            Case TypeCode.Int16
                Return eTypes.E_Int16
            Case TypeCode.Int32
                Return eTypes.E_Int32
            Case TypeCode.Int64
                Return eTypes.E_Int64
            Case TypeCode.Decimal
                Return eTypes.E_Decimal
            Case TypeCode.Double
                Return eTypes.E_Double
            Case TypeCode.Single
                Return eTypes.E_Single
            Case TypeCode.Char
                Return eTypes.E_Char
            Case TypeCode.String
                Return eTypes.E_String
            Case TypeCode.DateTime
                Return eTypes.E_DateTime
            Case Else
                Return eTypes.E_Invalid
        End Select
    End Function

#End Region

End Module