#Region "Imports"

Imports DevExpress.XtraGrid.Views.Grid
Imports System.ComponentModel
Imports System.Collections
Imports System.Drawing.Design
Imports System.Runtime.Serialization
Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors
Imports System.ComponentModel.Design
Imports System.Dynamic
Imports System.Reflection
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Data
Imports Infrastructure.CrossCutting.Base
Imports System.Threading.Tasks

#End Region

Public Class SearchLookUpEditExAdmission
    Implements ISupportInitialize, IValidable

#Region "Fields"
    ''' <summary>
    ''' Primera inicialización
    ''' </summary>
    Private _isFirstInit As Boolean
    ''' <summary>
    ''' Id del formulario que se abrirá al dar click sobre el botón Plus
    ''' </summary>
    Private _idOpenForm As Integer
    ''' <summary>
    ''' Valor que indica si el control permite la consulta simple
    ''' </summary>
    Private _allowQueryOne As Boolean
    ''' <summary>
    ''' Valor que indica que el valor seleccionado ha sido asignado por medio del
    ''' click en la rejilla de datos
    ''' </summary>
    Private _selectedValueOnClickGrid As Boolean
    ''' <summary>
    ''' Valor que indica que se perdió el foco desde un QueryPopUp
    ''' </summary>
    Private _isQueryPopUp As Boolean
    ''' <summary>
    ''' Instancia a la rejilla
    ''' </summary>
    Private WithEvents _grid As GridControl
    ''' <summary>
    ''' Tamaño del popUpContainer
    ''' </summary>
    Private _popUpFormSize As System.Drawing.Size
    ''' <summary>
    ''' Nombre del miembro valor
    ''' </summary>
    Private _valueMember As String
    ''' <summary>
    ''' Cadena formateada con los miembros a mostrar en la caja de texto.
    ''' Ej. {Id} - {Name}
    ''' </summary>
    Private _displayMember As String
    ''' <summary>
    ''' Texto a mostrar cuando el valor seleccionado es nulo
    ''' </summary>
    Private _displayNullText As String
    ''' <summary>
    ''' Lista de miembros mostrados en la cadena formateada
    ''' </summary>
    Private _membersDisplayed() As String
    ''' <summary>
    ''' Valor que indica si al presionar la tecla ENTER
    ''' el foco es pasado al siguiente control
    ''' </summary>
    Private _enterMoveNextControl As Boolean
    ''' <summary>
    ''' Valor anteriormente seleccionado
    ''' </summary>
    Private _oldEditValue As Object
    ''' <summary>
    ''' Objeto anteriormente seleccionado
    ''' </summary>
    Private _oldSelectedObject As Object
    ''' <summary>
    ''' Valor seleccionado
    ''' </summary>
    Private _editValue As Object
    ''' <summary>
    ''' Objeto seleccionado
    ''' </summary>
    Private _selectedObject As Object
    ''' <summary>
    ''' Fuente de datos a mostrar en la rejilla
    ''' </summary>
    Private _datasource As Object
    ''' <summary>
    ''' Delegado a la función que se ejecutará cuando se presiones la
    ''' tecla ENTER en la caja de texto, para realizar la consulta
    ''' </summary>
    Private _funcQueryOnKeyEnterPressed As Func(Of Object, Object)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <Category("Diseño"),
        Description("Obtiene el PopUpContainerControl del mas info para asignar los valores a sus controles"),
        Browsable(True)>
    Public Property PopupContainerControl As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.PcePopUpEdit.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.PcePopUpEdit.Properties.PopupControl = value
        End Set
    End Property

    'Private _PopUpContainerControlMoreInfo As PopupContainerControl

    'Public Property PopUpContainerControlMoreInfo As PopupContainerControl
    '    Get
    '        Return _PopUpContainerControlMoreInfo
    '    End Get
    '    Set(value As PopupContainerControl)
    '        _PopUpContainerControlMoreInfo = value
    '    End Set
    'End Property


    ''' <summary>
    ''' Obtiene o asigna si el control esta de solo lectura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsReadOnly As Boolean
        Get
            Return Me._popUpEdit.Properties.ReadOnly
        End Get
        Set(value As Boolean)
            Me._popUpEdit.Properties.ReadOnly = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si se permite la consulta simple
    ''' </summary>
    ''' <value>Valor que indica si se permite consulta simple</value>
    ''' <returns>Un valor que indica si se permite consulta simple</returns>
    Public Property AllowQueryOne As Boolean
        Get
            Return Me._allowQueryOne
        End Get
        Set(value As Boolean)
            Me._allowQueryOne = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el objeto seleccionado
    ''' </summary>
    ''' <returns>Objeto seleccionado</returns>
    <Browsable(False)>
    Public ReadOnly Property SelectedObject As Object
        Get
            Return Me._selectedObject
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el objeto anteriormente seleccionado
    ''' </summary>
    ''' <returns>Objeto anteriormente seleccionado</returns>
    <Browsable(False)>
    Public ReadOnly Property OldSelectedObject As Object
        Get
            Return Me._oldSelectedObject
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el delegado a la función que se ejecutará cuando se presiones la
    ''' tecla ENTER en la caja de texto, para realizar la consulta
    ''' </summary>
    ''' <value>Delegado a la función</value>
    ''' <returns>El delegado a la función</returns>
    <Browsable(False)>
    Public Property FuncQueryOnKeyEnterPressed As Func(Of Object, Object)
        Get
            Return Me._funcQueryOnKeyEnterPressed
        End Get
        Set(value As Func(Of Object, Object))
            Me._funcQueryOnKeyEnterPressed = value
        End Set
    End Property

    '<Browsable(True), Description("Evento que se ejecuta al presionar la tecla enter en el edit de busqueda")>
    'Public Event OnQueryOnKeyEnterPressed As EventHandler

    ''' <summary>
    ''' Obtiene o asigna el id del formulario que se abrirá cuando
    ''' se de click sobre el botón Plus
    ''' </summary>
    ''' <value>Id del formulario</value>
    ''' <returns>El id del formulario</returns>
    Public Property IdOpenForm As Integer
        Get
            Return Me._idOpenForm
        End Get
        Set(value As Integer)
            Me._idOpenForm = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el foco es pasado al siguiente control
    ''' del orden de tabulación, cuando se presional la tecla ENTER
    ''' </summary>
    ''' <value>Valor que indica si se pasa el foco</value>
    ''' <returns>Un valor que indica si se pasa el foco</returns>
    Public Property EnterMoveNextControl As Boolean
        Get
            Return Me._enterMoveNextControl
        End Get
        Set(value As Boolean)
            Me._enterMoveNextControl = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto a mostrar cuando el valor seleccionado es nulo
    ''' </summary>
    ''' <value>Texto a mostrar</value>
    ''' <returns>El texto a mostrar</returns>
    Public Property DisplayNullText As String
        Get
            If Me._displayNullText Is Nothing Then
                Me._displayNullText = String.Empty
            End If
            Return Me._displayNullText
        End Get
        Set(value As String)
            Me._displayNullText = value.Trim()
            Me.SetDisplayText()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor seleccionado
    ''' </summary>
    ''' <value>Valor seleccionado</value>
    ''' <returns>El valor seleccionado</returns>
    <Browsable(False)>
    Public Property EditValue As Object Implements IValidable.EditValue
        Get
            Return Me._editValue
        End Get
        Set(value As Object)
            If value IsNot Nothing Then
                Me._oldSelectedObject = Me._selectedObject
                If Me._valueMember IsNot Nothing AndAlso Not Me._valueMember.Equals(String.Empty) Then
                    Me._selectedObject = Me.GetValueByValueMember(Me._valueMember, value)
                Else
                    Me._selectedObject = Nothing
                End If
                Me._oldEditValue = Me._editValue
                Me._editValue = value
            Else
                Me._oldSelectedObject = Me._selectedObject
                Me._selectedObject = value
                Me._oldEditValue = Me._editValue
                Me._editValue = value
            End If
            Me.OnEditValueChanged(Me, New EditValueChangedEventArgs(Me._editValue, Me._selectedObject))
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el valor anteriormente seleccionado
    ''' </summary>
    ''' <returns>El valor anteriormente seleccionado</returns>
    <Browsable(False)>
    Public ReadOnly Property OldEditValue As Object
        Get
            Return Me._oldEditValue
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre del miembro valor
    ''' </summary>
    ''' <value>Nombre del miembro valor</value>
    ''' <returns>El nombre del miembro valor</returns>
    Public Property ValueMember As String
        Get
            If Me._valueMember Is Nothing Then
                Me._valueMember = String.Empty
            End If
            Return Me._valueMember
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                If Not value.Trim().Contains(" ") Then
                    Me._valueMember = value.Trim()
                Else
                    XtraMessageBox.Show("El valor en la propiedad es incorrecto. No se admite más de una palabra como nombre del miembro valor", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la cadena formateada con los miembros a mostrar en la caja de texto
    ''' </summary>
    ''' <value>Cadena formateada</value>
    ''' <returns>La cadena formateada</returns>
    ''' <remarks>Ej. {Code} - {Name}</remarks>
    Public Property DisplayMember As String
        Get
            If Me._displayMember Is Nothing Then
                Me._displayMember = String.Empty
            End If
            Return Me._displayMember
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._displayMember = value.Trim()
                Me.ExtractMembers()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la vista de la rejilla
    ''' </summary>
    ''' <value>Vita de la rejilla</value>
    ''' <returns>La vista de la rejilla</returns>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), TypeConverter(GetType(ExpandableObjectConverter)), Editor("DevExpress.XtraGrid.Design.GridLookUpViewEditor, DevExpress.XtraGrid.v15.1.Design", GetType(UITypeEditor))>
    Public Property View As GridView
        Get
            Me.SetupView()
            Return Me._grid.MainView
        End Get
        Set(value As GridView)
            Me._grid.MainView = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tamaño minimo del control
    ''' </summary>
    ''' <value>Tamaño minimo del control</value>
    ''' <returns>El tamaño minimo del control</returns>
    <Browsable(False)>
    Public Overrides Property MinimumSize As System.Drawing.Size
        Get
            Return MyBase.MinimumSize
        End Get
        Set(value As System.Drawing.Size)
            MyBase.MinimumSize = New System.Drawing.Size(100, 28)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tamaño maximo del control
    ''' </summary>
    ''' <value>Tamaño maximo del control</value>
    ''' <returns>El tamaño maximo del control</returns>
    <Browsable(False)>
    Public Overrides Property MaximumSize As System.Drawing.Size
        Get
            Return MyBase.MaximumSize
        End Get
        Set(value As System.Drawing.Size)
            MyBase.MaximumSize = New System.Drawing.Size(5000, Me._popUpEdit.Size.Height)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tamaño del control
    ''' </summary>
    ''' <value>Tamaño del control</value>
    ''' <returns>El tamaño del control</returns>
    Public Overloads Property Size As System.Drawing.Size
        Get
            Return MyBase.Size
        End Get
        Set(value As System.Drawing.Size)
            MyBase.Size = New System.Drawing.Size(value.Width, Me._popUpEdit.Height)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tamaño del popUpContainer
    ''' </summary>
    ''' <value>Tamaño del popUpContainer</value>
    ''' <returns>El tamaño del popUpContainer</returns>
    Public Property PopUpFormSize As System.Drawing.Size
        Get
            If Me._popUpFormSize.Equals(New System.Drawing.Size(0, 0)) Then
                Me._popUpFormSize = New System.Drawing.Size(Me.Size.Width + 100, Me.Size.Width + 200)
            End If
            Return Me._popUpFormSize
        End Get
        Set(value As System.Drawing.Size)
            Me._popUpFormSize = value
            Me._popUpEdit.Properties.PopupFormSize = Me._popUpFormSize
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fuente de datos a usar en el control
    ''' </summary>
    ''' <value>Fuente de datos</value>
    ''' <returns>La fuente de datos</returns>
    <Browsable(False)>
    Public Property Datasource As Object
        Get
            Return Me._datasource
        End Get
        Set(value As Object)
            If value Is DBNull.Value Then
                value = Nothing
            End If
            If Me.IsValidDataSource(value) Then
                Me._datasource = value
                Me._grid.DataSource = value
            End If
        End Set
    End Property

    Public ReadOnly Property TextEditValue As String
        Get
            Return Me._popUpEdit.Text
        End Get
    End Property

    Public WriteOnly Property ShowButtonOk As Boolean
        Set(value As Boolean)
            Me._popUpEdit.Properties.Buttons.Item(2).Visible = value
        End Set
    End Property

    Public WriteOnly Property ShowButtonMoreInfo As Boolean
        Set(value As Boolean)
            Me._popUpEdit.Properties.Buttons.Item(1).Visible = value
        End Set
    End Property
    ''' <summary>
    ''' Indica si el popup de moreinfo se encuentra abierto
    ''' </summary>
    Private _isPopUpOpen As Boolean = False

    'Private _PopUpContainerControlMoreInfo As PopupContainerControl
    '<Category("Diseño"),
    '    Description("Obtiene el PopUpContainerControl del mas info para asignar los valores a sus controles"),
    '    Browsable(True)>
    'Public Property PopUpContainerControlMoreInfo As PopupContainerControl
    '    Get
    '        Return _PopUpContainerControlMoreInfo
    '    End Get
    '    Set(value As PopupContainerControl)
    '        _PopUpContainerControlMoreInfo = value
    '    End Set
    'End Property

#End Region

#Region "Events"

    ''' <summary>
    ''' Se dispara cuando ocurre un error interno
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event InternalError(ByVal sender As Object, ByVal e As InternalErrorEventArgs)

    ''' <summary>
    ''' Se dispara al dar click en el botón Plus del control
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event OpenFormButtonClick(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Se dispara cuando cambia el valor seleccionado
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event EditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)

    ''' <summary>
    ''' Se dispara cuando se va a mostrar el popUp
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event QueryPopUp(ByVal sender As Object, ByVal e As CancelEventArgs)

    ''' <summary>
    ''' Se dispara cuando se va a ocultar el popUp
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event QueryCloseUp(ByVal sender As Object, ByVal e As CancelEventArgs)

    ''' <summary>
    ''' Se dispara cuando inicia la ejecución de la función de consulta
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event BeginPerformQueryFunction(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Se dispara cuando finaliza la ejecución de la función de consulta
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event EndPerformQueryFunction(ByVal sender As Object, ByVal e As EndPerformQueryFunctionEventArgs)

    Public Event MouseEnterAdmission(sender As Object, e As EventArgs)
    Public Event MouseLeaveAdmission(sender As Object, e As EventArgs)
    Public Event ButtonOk(sender As Object, e As EventArgs)
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia del control
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._isFirstInit = True
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se realiza la lógica de cambiar el texto mostrado por el
    ''' formato asignado en la propiedad DisplayTextFormat
    ''' </summary>
    Private Sub _popUpEdit_Click(sender As Object, e As EventArgs) Handles _popUpEdit.Click, Me.Click
        Me.SelectAllText()
    End Sub

    ''' <summary>
    ''' Aqui se inicializa el control
    ''' </summary>
    Private Sub SearchLookUpEditEx_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._popUpEdit.Properties.TextEditStyle = If(Me._allowQueryOne, DevExpress.XtraEditors.Controls.TextEditStyles.Standard, DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor)
        Me.SetupGrid()
        Me.LoadGridViewDefinitions()
        Me._popUpEdit.Properties.Buttons.Item(2).Visible = False
    End Sub

    ''' <summary>
    ''' Aqui se realiza el lanzamiento del evento AddButtonClick
    ''' </summary>
    Private Sub _popUpEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles _popUpEdit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OnOpenFormButtonClick(sender, e)
        End If
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph AndAlso e.Button.Tag.ToString().Equals("MORE_INFO") AndAlso Me.PcePopUpEdit.Properties.PopupControl IsNot Nothing Then
            If _isPopUpOpen Then
                _isPopUpOpen = False
                Me.PcePopUpEdit.ClosePopup()
            Else
                _isPopUpOpen = True
                Me.PcePopUpEdit.ShowPopup()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento closed para indicar que se cerro el popup de more info
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PcePopUpEdit_Closed(sender As Object, e As EventArgs) Handles PcePopUpEdit.QueryCloseUp, PcePopUpEdit.Closed
        _isPopUpOpen = False
    End Sub

    ''' <summary>
    ''' Aqui se realiza la lógica de cambiar el texto mostrado por el
    ''' formato asignado en la propiedad DisplayTextFormat
    ''' </summary>
    Private Sub _popUpEdit_GotFocus(sender As Object, e As EventArgs) Handles _popUpEdit.GotFocus, Me.GotFocus
        Me.SelectAllText()
    End Sub

    ''' <summary>
    ''' Aqui se obtiene el valor seleccionado al dar click a la rejilla
    ''' </summary>
    Private Sub _grid_Click(sender As Object, e As EventArgs)
        Dim pMouse As System.Drawing.Point = MousePosition
        Dim hit As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo = Me._grid.MainView.CalcHitInfo(Me._grid.PointToClient(pMouse))
        If hit IsNot Nothing AndAlso hit.InDataRow Then
            Me._oldSelectedObject = Me._selectedObject
            Me._selectedObject = Me.GetSelectedObject()
            Me._oldEditValue = Me._editValue
            Me._editValue = Me.GetEditValue()
            Me._selectedValueOnClickGrid = True
            Me.OnEditValueChanged(Me, New EditValueChangedEventArgs(Me._editValue, Me._selectedObject))
        End If
    End Sub

    ''' <summary>
    ''' Aqui se da estilo al panel de busqueda de la rejilla
    ''' </summary>
    Private Sub _grid_Invalidated(sender As Object, e As InvalidateEventArgs)
        Me.SetStyleFindPanel()
    End Sub

    ''' <summary>
    ''' Aqui se obtiene el valor seleccionado al presionar la tecla ENTER en la rejilla
    ''' </summary>
    Private Sub _grid_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter AndAlso DirectCast(Me._grid.MainView, GridView).FocusedRowHandle > -1 Then
            Me._oldSelectedObject = Me._selectedObject
            Me._selectedObject = Me.GetSelectedObject()
            Me._oldEditValue = Me._editValue
            Me._editValue = Me.GetEditValue()
            Me.OnEditValueChanged(Me, New EditValueChangedEventArgs(Me._editValue, Me._selectedObject))
        End If
    End Sub

    ''' <summary>
    ''' Aqui se persiste la definición de la rejilla
    ''' </summary>
    Private Async Sub _grid_ColumnPositionChanged(sender As Object, e As EventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender.View)
    End Sub

    ''' <summary>
    ''' Aqui se persiste la definición de la rejilla
    ''' </summary>
    Private Async Sub _grid_HideCustomizationForm(sender As Object, e As EventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    ''' <summary>
    ''' Aqui se persiste la definición de la rejilla
    ''' </summary>
    Private Async Sub _grid_ColumnWidthChanged(sender As Object, e As Views.Base.ColumnEventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    ''' <summary>
    ''' Aqui se abre el popUp para la seleccion de un nuevo valor
    ''' </summary>
    Private Sub _popUpEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles _popUpEdit.KeyDown
        If e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.F4 Then
            Me._popUpEdit.ShowPopup()
            Me.SetFocusGrid()
        ElseIf e.KeyCode = Keys.Enter Then
            Me.PerformQuery()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se dispara el evento QueryPopUp
    ''' </summary>
    Private Sub _popUpEdit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles _popUpEdit.QueryPopUp
        Me._isQueryPopUp = True
        Me.OnQueryPopUp(sender, e)
    End Sub

    ''' <summary>
    ''' Aqui se dispara el evento QueryCloseUp
    ''' </summary>
    Private Sub _popUpEdit_QueryCloseUp(sender As Object, e As CancelEventArgs) Handles _popUpEdit.QueryCloseUp
        Me.OnQueryCloseUp(sender, e)
    End Sub

    ''' <summary>
    ''' Aqui se agrega la lógica para enviar el foco a la vista de la rejilla
    ''' cuando el panel esta visible y se presiona la recla DOWN
    ''' </summary>
    Private Sub findPanel_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Down OrElse e.KeyCode = Keys.Tab Then
            Me._grid.MainView.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se verifica si el text del control es vacio para limpiar el EditValue, de l contrario se ejecuta la consulta simple
    ''' </summary>
    Private Sub _popUpEdit_LostFocus(sender As Object, e As System.EventArgs) Handles _popUpEdit.LostFocus
        'If Me._popUpEdit.Text.Trim().Equals(String.Empty) AndAlso (Me._displayNullText Is Nothing OrElse Me._displayNullText.Trim().Equals(String.Empty)) AndAlso Me.EditValue IsNot Nothing Then
        '    Me.EditValue = Nothing
        'End If
        'If Not Me._selectedValueOnClickGrid AndAlso Not Me._isQueryPopUp Then
        '    Me.PerformQuery(True)
        'End If
        Me._selectedValueOnClickGrid = False
        Me._isQueryPopUp = False
        'Me.OnLostFocus(New EventArgs())
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna el foco a la rejilla
    ''' </summary>
    Private Sub SetFocusGrid()
        If Me._grid IsNot Nothing AndAlso Me._grid.MainView IsNot Nothing Then
            If CType(Me._grid.MainView, GridView).OptionsFind.AlwaysVisible Then
                Dim find As DevExpress.XtraGrid.Controls.FindControl = TryCast(Me._grid.Controls.OfType(Of DevExpress.XtraGrid.Controls.FindControl)().FirstOrDefault(), DevExpress.XtraGrid.Controls.FindControl)
                If find IsNot Nothing Then
                    find.FindEdit.Focus()
                Else
                    Me._grid.MainView.Focus()
                End If
            Else
                Me._grid.MainView.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Asigna estilo al panel de busqueda de la rejilla
    ''' </summary>
    Private Sub SetStyleFindPanel()
        If Me._grid IsNot Nothing AndAlso Me._grid.MainView IsNot Nothing AndAlso CType(Me._grid.MainView, GridView).OptionsFind.AlwaysVisible Then
            Dim find As DevExpress.XtraGrid.Controls.FindControl = TryCast(Me._grid.Controls.OfType(Of DevExpress.XtraGrid.Controls.FindControl)().FirstOrDefault(), DevExpress.XtraGrid.Controls.FindControl)
            If find IsNot Nothing AndAlso find.Appearance.BackColor <> Color.White Then
                find.Dock = DockStyle.Top
                find.BorderStyle = BorderStyle.None
                find.Appearance.BackColor = Color.White
                find.MinimumSize = New Size(find.Size.Width, find.Size.Height + 30)
                find.MaximumSize = New Size(find.Size.Width, find.Size.Height + 30)

                find.FindEdit.Font = New Font("Segoe UI Light", 12.0!)
                find.FindEdit.Properties.AllowFocused = True
                AddHandler find.FindEdit.KeyDown, AddressOf findPanel_KeyDown

                find.FindButton.Font = New Font("Segoe UI Light", 12.0!)
                find.FindButton.Size = New Size(100, 36)

                find.ClearButton.Font = New Font("Segoe UI Light", 12.0!)
                find.ClearButton.Size = New Size(100, 36)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la cosulta simple
    ''' </summary>
    Public Sub PerformQuery(Optional ByVal isLostFocus As Boolean = False)
        If Me._allowQueryOne Then
            If Not Me._popUpEdit.Text.Trim().Equals(String.Empty) AndAlso Not Me._popUpEdit.Text.Trim().Equals(Me.GetDisplayText()) Then
                'Realizo la busqueda con el delegado
                Me.PerformQueryFunction(Me._popUpEdit.Text.Trim())
            End If
            If Me._enterMoveNextControl AndAlso Not isLostFocus Then
                Me._selectedValueOnClickGrid = True
                Me.Parent.SelectNextControl(Me, True, True, True, False)
            ElseIf Me._displayNullText IsNot Nothing AndAlso Not Me._displayNullText.Trim().Equals(String.Empty) Then
                Me._popUpEdit.Text = Me.GetDisplayText()
                Me.SelectAllText()
            End If
        ElseIf Me._enterMoveNextControl AndAlso Not isLostFocus Then
            Me._selectedValueOnClickGrid = True
            Me.Parent.SelectNextControl(Me, True, True, True, False)
        ElseIf Me._displayNullText IsNot Nothing AndAlso Not Me._displayNullText.Trim().Equals(String.Empty) Then
            Me._popUpEdit.Text = Me.GetDisplayText()
            Me.SelectAllText()
        End If
    End Sub

    ''' <summary>
    ''' Valida y ejecuta la función de consulta si está configurada
    ''' </summary>
    Private Sub PerformQueryFunction(ByVal arg As Object)
        Me.OnBeginPerformQueryFunction(Me, New EventArgs())
        If Me._funcQueryOnKeyEnterPressed IsNot Nothing Then
            Dim params = Me._funcQueryOnKeyEnterPressed.Method.GetParameters()
            If params IsNot Nothing AndAlso params.Length > 0 AndAlso Me._funcQueryOnKeyEnterPressed.Method.ReturnParameter IsNot Nothing Then
                Try
                    Dim res = Me._funcQueryOnKeyEnterPressed(arg)
                    If TypeOf res Is Task Then
                        Me.PerformQueryFunctionAsync(res)
                    Else
                        Me.OnEndPerformQueryFunction(Me, New EndPerformQueryFunctionEventArgs(res))
                        Me._oldSelectedObject = Me._selectedObject
                        Me._selectedObject = res
                        Me._oldEditValue = Me._editValue
                        Me._editValue = Me.GetEditValue()
                        Me.OnEditValueChanged(Me, New EditValueChangedEventArgs(Me._editValue, Me._selectedObject))
                    End If
                Catch ex As Exception
                    Me.OnInternalError(Me, New InternalErrorEventArgs(ex))
                End Try
            Else
                Throw New PerformQueryFunctionException()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida y ejecuta la función de consulta si está configurada
    ''' </summary>
    Private Async Sub PerformQueryFunctionAsync(ByVal arg As Task)
        Try
            Await arg
            If Me.PropertyExists(arg.GetType(), "Result") Then
                Dim res = Me.GetPropertyValue(arg, "Result")
                Me._oldSelectedObject = Me._selectedObject
                Me._selectedObject = res
                Me._oldEditValue = Me._editValue
                Me._editValue = Me.GetEditValue()
                Me.OnEndPerformQueryFunction(Me, New EndPerformQueryFunctionEventArgs(Me._selectedObject))
                Me.OnEditValueChanged(Me, New EditValueChangedEventArgs(Me._editValue, Me._selectedObject))
            Else
                Me.OnEndPerformQueryFunction(Me, New EndPerformQueryFunctionEventArgs(arg))
            End If
        Catch ex As Exception
            Me.OnInternalError(Me, New InternalErrorEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Carga las definiciones de todas la vistas en el control
    ''' </summary>
    Private Async Sub LoadGridViewDefinitions()
        If Me._grid IsNot Nothing AndAlso Me._grid.MainView IsNot Nothing Then
            Await Me.LoadDefinitionFromXmlAsync(Me._grid.MainView)
        End If
    End Sub

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefinitionFromXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If Me._grid.InvokeRequired Then
                                             Me._grid.BeginInvoke(Sub()
                                                                      Me.LoadDefinitionFromXml(view)
                                                                  End Sub)
                                         Else
                                             Me.LoadDefinitionFromXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefinitionFromXml(ByVal view As GridView)
        If My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.ParentForm.Name, Me.GetType().Name, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
            view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.ParentForm.Name, Me.GetType().Name, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
            view.ClearColumnsFilter()
            view.FindFilterText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Function SaveDefinitionToXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If Me._grid.InvokeRequired Then
                                             Me._grid.BeginInvoke(Sub()
                                                                      Me.SaveDefinitionToXml(view)
                                                                  End Sub)
                                         Else
                                             Me.SaveDefinitionToXml(view)
                                         End If
                                     End Sub)
    End Function

    ''' <summary>
    ''' Graba la definición de la vista a un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a persistir</param>
    Private Sub SaveDefinitionToXml(ByVal view As GridView)
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.ParentForm.Name, Me.GetType().Name, "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.ParentForm.Name, Me.GetType().Name, ""))
        End If
        view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me.ParentForm.Name, Me.GetType().Name, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
    End Sub

    ''' <summary>
    ''' Obtiene el valor seleccionado o el objeto seleccionado
    ''' </summary>
    ''' <returns>Objeto o valor seleccionado</returns>
    Private Function GetEditValue() As Object
        If Me._valueMember IsNot Nothing AndAlso Not Me._valueMember.Equals(String.Empty) Then
            If Me._selectedObject IsNot Nothing Then
                Return Me.GetPropertyValue(Me._selectedObject, Me._valueMember)
            End If
        End If
        Return Me._selectedObject
    End Function

    ''' <summary>
    ''' Obtiene el objeto con propiedad [valueMember] igual al [value]
    ''' </summary>
    ''' <param name="valueMember">Nombre del miembro valor</param>
    ''' <param name="value">Valor que debe tener el miembro valor</param>
    ''' <returns>Objeto de la fuente de datos que coincide con el [valor] en su propiedad [valueMember]</returns>
    Private Function GetValueByValueMember(ByVal valueMember As String, ByVal value As Object) As Object
        If Me._datasource IsNot Nothing AndAlso valueMember IsNot Nothing AndAlso Not valueMember.Trim().Equals(String.Empty) Then
            If Not Me.GetIsServerMode(Me._datasource) Then
                If Me._datasource IsNot Nothing Then
                    If (TypeOf Me._datasource Is IList) OrElse (TypeOf Me._datasource Is IListSource AndAlso CType(Me._datasource, IListSource).ContainsListCollection) Then
                        Dim res = (From o In CType(Me._datasource, IList) Where o.GetType().GetProperties().Any(Function(p) p.Name.Equals(valueMember)) AndAlso o.GetType().GetProperty(valueMember).GetValue(o).Equals(value) Select o).ToList()
                        If res IsNot Nothing AndAlso res.Count > 0 Then
                            Dim rowHandler = CType(Me._grid.MainView, GridView).GetRowHandle(CType(Me._datasource, IList).IndexOf(res(0)))
                            If rowHandler > -1 Then
                                CType(Me._grid.MainView, GridView).FocusedRowHandle = rowHandler
                            End If
                            Return res(0)
                        End If
                    End If
                End If
                'Else
                'Throw New SetEditValueInstantFeedBackSourceException()
            End If
        End If
        Return Nothing
        'Return value
    End Function

    ''' <summary>
    ''' Obtiene un objeto ViewInfo con toda la información visual de la rejilla
    ''' </summary>
    ''' <param name="view">Rejilla a verificar</param>
    ''' <returns>El objeto de información visual</returns>
    Private Function GetGridViewInfo(ByVal view As GridView) As GridViewInfo
        Dim fi As FieldInfo
        fi = GetType(GridView).GetField("fViewInfo", BindingFlags.NonPublic Or BindingFlags.Instance)
        Return CType(fi.GetValue(view), GridViewInfo)
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el [dataSource] es valido como fuente de datos
    ''' </summary>
    ''' <param name="dataSource">Fuente de datos a analizar</param>
    ''' <returns>Valor que indica si es valido</returns>
    Private Function IsValidDataSource(ByVal dataSource As Object) As Boolean
        If Me.GetIsServerMode(Me._datasource) Then
            Return True
        End If
        If (dataSource Is Nothing) Then
            Return True
        End If
        If TypeOf dataSource Is IList Then
            Return True
        End If
        If TypeOf dataSource Is IListSource Then
            Return True
        End If
        If TypeOf dataSource Is DataSet Then
            Return True
        End If
        If TypeOf dataSource Is DataView Then
            Dim view As DataView = TryCast(dataSource, DataView)
            If (view.Table Is Nothing) Then
                Return False
            End If
            Return True
        End If
        Return (TypeOf dataSource Is DataTable OrElse TypeOf dataSource Is IEnumerable)
    End Function

    ''' <summary>
    ''' Selecciona todo el texto en la caja de texto
    ''' </summary>
    Private Sub SelectAllText()
        Me._popUpEdit.SelectionStart = 0
        Me._popUpEdit.SelectAll()
    End Sub

    ''' <summary>
    ''' Obtiene el valor seleccionado en la rejilla
    ''' </summary>
    ''' <returns>Objeto del valor seleccionado en la rejilla</returns>
    Private Function GetSelectedObject() As Object
        If Me._datasource IsNot Nothing AndAlso Me._grid IsNot Nothing AndAlso Me._grid.MainView IsNot Nothing Then
            Return Me.GetOriginalObject(CType(Me._grid.MainView, GridView).GetFocusedRow())
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Conforma y asigna el texto a mostrar en la caja de texto
    ''' </summary>
    Private Sub SetDisplayText()
        Me._popUpEdit.Text = Me.GetDisplayText()
    End Sub

    ''' <summary>
    ''' Obtiene el texto a mostrar
    ''' </summary>
    ''' <returns>Texto a mostrar</returns>
    Private Function GetDisplayText() As String
        If Me._selectedObject IsNot Nothing Then
            If Me._membersDisplayed IsNot Nothing AndAlso Me._membersDisplayed.Length > 0 Then
                Dim aux As String = Me._displayMember
                For Each s In Me._membersDisplayed
                    Dim res = Me.GetPropertyValue(Me._selectedObject, s.Replace("{", "").Replace("}", ""))
                    If res IsNot Nothing Then
                        aux = aux.Replace(s, res.ToString())
                    Else
                        aux = aux.Replace(s, String.Empty)
                    End If
                Next
                Return aux.Trim()
            Else
                Return Me._selectedObject.ToString().Trim()
            End If
        Else
            Return Me._displayNullText
        End If
    End Function

    ''' <summary>
    ''' Obtiene el valor de una propiedad
    ''' </summary>
    ''' <param name="obj">Objeto que contiene la propiedad a obtener</param>
    ''' <param name="pathPropertyName">Ruta completa de la propiedad que se va a obtener</param>
    Private Function GetPropertyValue(ByVal obj As Object, ByVal pathPropertyName As String) As Object
        Return Me.InternalGetValueToProperty(obj, pathPropertyName)
    End Function
    Private Function InternalGetValueToProperty(ByVal obj As Object, ByVal pathPropertyName As String) As Object
        Dim props() As String = pathPropertyName.Split(".")
        Dim listProps As New List(Of String)(props)
        If props.Length = 0 Then Return Nothing
        If props IsNot Nothing AndAlso props.Length > 1 Then
            listProps.RemoveAt(0)
            Dim newPathPropertyName As String = String.Join(".", listProps.ToArray())
            If obj.GetType().Equals(GetType(ExpandoObject)) AndAlso CType(obj, IDictionary(Of String, Object)).ContainsKey(props(0)) Then
                Dim objAux = CType(obj, IDictionary(Of String, Object))(props(0))
                If objAux IsNot Nothing Then
                    Return Me.InternalGetValueToProperty(objAux, newPathPropertyName)
                End If
            Else
                Dim listp As List(Of PropertyInfo) = obj.GetType().GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
                If listp IsNot Nothing AndAlso listp.Count > 0 Then
                    For Each p As PropertyInfo In listp
                        Dim objAux = p.GetValue(obj)
                        If objAux IsNot Nothing Then
                            Return Me.InternalGetValueToProperty(objAux, newPathPropertyName)
                        End If
                    Next
                End If
            End If
            Return Nothing
        Else
            If obj.GetType().Equals(GetType(ExpandoObject)) AndAlso CType(obj, IDictionary(Of String, Object)).ContainsKey(props(0)) Then
                Return CType(obj, IDictionary(Of String, Object))(props(0))
            Else
                Dim listp As List(Of PropertyInfo) = obj.GetType().GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
                If listp IsNot Nothing AndAlso listp.Count > 0 Then
                    For Each p As PropertyInfo In listp
                        Return p.GetValue(obj)
                    Next
                End If
            End If
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si la propiedad existe en el tipo
    ''' </summary>
    ''' <param name="obj">Tipo a comprobar</param>
    ''' <param name="pathPropertyName">Ruta completa de la propiedad que se va a comprobar</param>
    Private Function PropertyExists(ByVal obj As Type, ByVal pathPropertyName As String) As Boolean
        Return Me.InternalPropertyExists(obj, pathPropertyName)
    End Function
    Private Function InternalPropertyExists(ByVal obj As Type, ByVal pathPropertyName As String) As Boolean
        Dim props() As String = pathPropertyName.Split(".")
        Dim listProps As New List(Of String)(props)
        If props.Length = 0 Then Return Nothing
        If props IsNot Nothing AndAlso props.Length > 1 Then
            listProps.RemoveAt(0)
            Dim newPathPropertyName As String = String.Join(".", listProps.ToArray())
            Dim listp As List(Of PropertyInfo) = obj.GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
            If listp IsNot Nothing AndAlso listp.Count > 0 Then
                For Each p As PropertyInfo In listp
                    Return Me.InternalGetValueToProperty(p, newPathPropertyName)
                Next
            End If
            Return False
        Else
            Dim listp As List(Of PropertyInfo) = obj.GetProperties().Where(Function(prop) prop.Name.Equals(props(0))).ToList()
            If listp IsNot Nothing AndAlso listp.Count > 0 Then
                For Each p As PropertyInfo In listp
                    If p.Name.Equals(pathPropertyName) Then
                        Return True
                    End If
                Next
            End If
            Return False
        End If
    End Function

    ''' <summary>
    ''' Extrae los miembros de la cadena formateada
    ''' </summary>
    Private Sub ExtractMembers()
        If Me._displayMember IsNot Nothing Then
            Dim res = Me._displayMember.Split(" ")
            Dim list As New List(Of String)()
            For Each s In res
                If s.StartsWith("{") AndAlso s.EndsWith("}") Then
                    list.Add(s)
                End If
            Next
            Me._membersDisplayed = list.ToArray()
        End If
    End Sub

    ''' <summary>
    ''' Obtiene el objeto original
    ''' </summary>
    ''' <param name="obj">Objeto a evaluar y limpiar</param>
    ''' <returns></returns>
    ''' <remarks>Cuando la fuente de datos es de tipo ServerMode el objeto seleccionado
    ''' en la rejilla es de un tipo que encapsula el objeto original</remarks>
    Private Function GetOriginalObject(ByVal obj As Object) As Object
        If Me.GetIsServerMode(Me._datasource) Then
            Return DirectCast(obj, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        End If
        Return obj
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si la fuente de datos es de tipo ServerMode
    ''' </summary>
    ''' <param name="datasource">Fuente de datos</param>
    ''' <returns>Valor que indica si la fuente de datos es de tipo ServerMode</returns>
    Private Function GetIsServerMode(ByVal datasource As Object) As Boolean
        If Me._datasource IsNot Nothing Then
            Return Me._datasource.GetType().Name.EndsWith("InstantFeedbackSource")
        End If
        Return False
    End Function

    ''' <summary>
    ''' Inicialización en modo de diseño
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        Me._isFirstInit = False
        Me.SetupView()
        Me._grid.BeginInit()
    End Sub

    ''' <summary>
    ''' Finaliza la inicialización en modo de diseño
    ''' </summary>
    Public Sub EndInit() Implements ISupportInitialize.EndInit
        Me._grid.EndInit()
        Me.SetButtons()
    End Sub

    ''' <summary>
    ''' Asigna los botones al popUpEdit segun la lógica de permiso
    ''' de usuario
    ''' </summary>
    Private Sub SetButtons()
        If Me._idOpenForm > 0 Then
            If SessionValues.Instance.IsAllowPermissionForm(Me._idOpenForm.ToString()) Then
                Me._popUpEdit.Properties.Buttons.Clear()
                Me._popUpEdit.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown))
                Me._popUpEdit.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Configura la vista y la rejilla en modo de diseño
    ''' </summary>
    Private Sub SetupView()
        If Me._grid Is Nothing Then
            Me._grid = New GridControl()
        End If
        If Me._grid.MainView Is Nothing Then
            Me._grid.MainView = New GridView()
            CType(_grid.MainView, GridView).OptionsView.ShowGroupPanel = False
            Me.AddComponentToContainer(Me._grid.MainView, "View")
        End If
    End Sub

    ''' <summary>
    ''' Configura la rejilla en su apariencia visual
    ''' </summary>
    Private Sub SetupGrid()
        Me._popUpContainer.Controls.Add(Me._grid)
        Me._grid.Dock = DockStyle.Fill
        Me._grid.Padding = New Padding(0)
        AddHandler Me._grid.Invalidated, AddressOf _grid_Invalidated
        If Me._grid.MainView IsNot Nothing Then
            Me._grid.MainView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            AddHandler Me._grid.MainView.Click, AddressOf _grid_Click
            AddHandler Me._grid.MainView.KeyDown, AddressOf _grid_KeyDown
            AddHandler CType(Me._grid.MainView, GridView).ColumnPositionChanged, AddressOf _grid_ColumnPositionChanged
            AddHandler CType(Me._grid.MainView, GridView).ColumnWidthChanged, AddressOf _grid_ColumnWidthChanged
            AddHandler CType(Me._grid.MainView, GridView).HideCustomizationForm, AddressOf _grid_HideCustomizationForm
        End If
    End Sub

    ''' <summary>
    ''' Agrega un componente al contenedor
    ''' </summary>
    ''' <param name="comp">Componente a agregar</param>
    ''' <param name="sufix">Sufijo del nombre</param>
    Private Sub AddComponentToContainer(ByVal comp As Component, ByVal sufix As String)
        If DesignMode AndAlso Me._isFirstInit Then
            Dim cont As IContainer = Me.Site.Container
            If cont IsNot Nothing Then
                Dim countName As Integer = 1
                For Each c In cont.Components
                    If c.GetType().Equals(comp.GetType()) Then
                        countName += 1
                    End If
                Next
                Dim ctrName As String = Me.Name & sufix & countName
                cont.Add(comp, ctrName)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se dispara cuando cambia el valor seleccionado
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnEditValueChanged(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)
        Me.SetDisplayText()
        Me.SelectAllText()
        Me._popUpEdit.ClosePopup()
        RaiseEvent EditValueChanged(sender, e)
    End Sub

    ''' <summary>
    ''' Se dispara cuando se va a mostrar el popUp
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnQueryPopUp(ByVal sender As Object, ByVal e As CancelEventArgs)
        If (Me._popUpEdit.Properties.PopupFormSize.Width < (Me.Size.Width + 100)) Then
            Me._popUpEdit.Properties.PopupFormSize = New System.Drawing.Size((Me.Size.Width + 100), Me._popUpEdit.Properties.PopupFormSize.Height)
        End If
        RaiseEvent QueryPopUp(sender, e)
    End Sub

    ''' <summary>
    ''' Se dispara cuando se va a ocultar el popUp
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnQueryCloseUp(ByVal sender As Object, ByVal e As CancelEventArgs)
        RaiseEvent QueryCloseUp(sender, e)
    End Sub

    ''' <summary>
    ''' Se dispara cuando ocurre un error interno
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnInternalError(ByVal sender As Object, ByVal e As InternalErrorEventArgs)
        RaiseEvent InternalError(sender, e)
    End Sub

    ''' <summary>
    ''' Se dispara al dar click en el botón Plus del control
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnOpenFormButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent OpenFormButtonClick(Me, New EventArgs())
    End Sub

    ''' <summary>
    ''' Se dispara cuando inicia la ejecución de la función de consulta
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnBeginPerformQueryFunction(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent BeginPerformQueryFunction(sender, e)
    End Sub

    ''' <summary>
    ''' Se dispara cuando finaliza la ejecución de la función de consulta
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnEndPerformQueryFunction(ByVal sender As Object, ByVal e As EndPerformQueryFunctionEventArgs)
        RaiseEvent EndPerformQueryFunction(sender, e)
    End Sub


    Private Function GetIsBaseEditBaseType(ByVal type As Type) As Boolean
        Dim result As Boolean = False
        IsBaseEditBaseType(type, result)
        Return result
    End Function

    Private Sub IsBaseEditBaseType(ByVal type As Type, ByRef result As Boolean)
        If type.BaseType.Equals(GetType(DevExpress.XtraEditors.BaseEdit)) Then
            result = True
        Else
            If Not type.BaseType.Equals(GetType(Object)) Then
                IsBaseEditBaseType(type.BaseType, result)
            Else
                result = False
            End If
        End If
    End Sub

    Public Sub SetMoreInfoData(objData As Object)
        If PopupContainerControl IsNot Nothing Then
            If PopupContainerControl.Controls.OfType(Of DevExpress.XtraLayout.LayoutControl).Any() Then
                Dim mainLayoutControl As DevExpress.XtraLayout.LayoutControl = PopupContainerControl.
                    Controls.OfType(Of DevExpress.XtraLayout.LayoutControl).FirstOrDefault()
                If mainLayoutControl.Controls.Count > 0 Then
                    Task.Factory.StartNew(Sub()
                                              For Each ctrl As Control In mainLayoutControl.Controls
                                                  If TypeOf ctrl Is IValidable OrElse Me.GetIsBaseEditBaseType(ctrl.GetType()) Then
                                                      'Obtenemos el tag
                                                      If ctrl.Tag IsNot Nothing Then
                                                          Dim vl = GetPropertyValue(objData, ctrl.Tag)
                                                          If ctrl.InvokeRequired Then
                                                              ctrl.BeginInvoke(Sub()
                                                                                   CType(ctrl, Object).EditValue = vl
                                                                               End Sub)
                                                          Else
                                                              CType(ctrl, Object).EditValue = vl
                                                          End If
                                                      End If
                                                  End If
                                              Next
                                          End Sub)
                End If
            End If
        End If
    End Sub

#End Region

    Private Sub _popUpEdit_MouseEnter(sender As Object, e As EventArgs) Handles _popUpEdit.MouseEnter
        RaiseEvent MouseEnterAdmission(Me, EventArgs.Empty)
    End Sub

    Private Sub _popUpEdit_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        RaiseEvent MouseLeaveAdmission(Me, EventArgs.Empty)
    End Sub

    Private Sub _popUpEdit_ButtonOk(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles _popUpEdit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.OK Then
            RaiseEvent ButtonOk(Me, EventArgs.Empty)
        End If
    End Sub

End Class