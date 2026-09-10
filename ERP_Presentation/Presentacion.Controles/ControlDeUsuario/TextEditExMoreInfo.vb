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

Public Class TextEditExMoreInfo
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
    ''' Indica si el popup de moreinfo se encuentra abierto
    ''' </summary>
    Private _isPopUpOpen As Boolean = False

    ''' <summary>
    ''' Valor que indica si al presionar la tecla ENTER
    ''' el foco es pasado al siguiente control
    ''' </summary>
    Private _enterMoveNextControl As Boolean

    ''' <summary>
    ''' Valor anteriormente seleccionado
    ''' </summary>
    Private _oldEditValue As Object



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

    ''' <summary>
    ''' Obtiene o asigna si el control esta de solo lectura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsReadOnly As Boolean
        Get
            Return Me._btnEdit.Properties.ReadOnly
        End Get
        Set(value As Boolean)
            Me._btnEdit.Properties.ReadOnly = value
        End Set
    End Property

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
    ''' Obtiene o asigna el valor seleccionado
    ''' </summary>
    ''' <value>Valor seleccionado</value>
    ''' <returns>El valor seleccionado</returns>
    <Browsable(False)>
    Public Property EditValue As Object Implements IValidable.EditValue
        Get
            Return _btnEdit.EditValue
        End Get
        Set(value As Object)
            _oldEditValue = Me._btnEdit.EditValue
            _btnEdit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece la propiedad Visible del botón Show More Info
    ''' </summary>
    Public WriteOnly Property ShowButtonMoreInfo As Boolean
        Set(value As Boolean)
            Me._btnEdit.Properties.Buttons.Item(0).Visible = value
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
            MyBase.MaximumSize = New System.Drawing.Size(5000, Me._btnEdit.Size.Height)
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
            MyBase.Size = New System.Drawing.Size(value.Width, Me._btnEdit.Height)
        End Set
    End Property

#End Region

#Region "Events"

    ''' <summary>
    ''' Se dispara cuando cambia el valor seleccionado
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event EditValueChanged(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Se dispara cuando se presiona una tecla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event OnKeyPressed(sender As Object, e As KeyEventArgs)

    ''' <summary>
    ''' Se dispara al dar click en el botón Plus del control
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event OpenFormButtonClick(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Se dispara cuando se va a ocultar el popUp
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event QueryCloseUp(ByVal sender As Object, ByVal e As CancelEventArgs)

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
    ''' Aqui se inicializa el control
    ''' </summary>
    Private Sub TextEditExMoreInfo_Load(sender As Object, e As EventArgs) Handles Me.Load
    End Sub

    ''' <summary>
    ''' Aqui se realiza la lógica de cambiar el texto mostrado por el
    ''' formato asignado en la propiedad DisplayTextFormat
    ''' </summary>
    Private Sub _btnEdit_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        Me.SelectAllText()
    End Sub

    ''' <summary>
    ''' Aqui se abre el popUp para la seleccion de un nuevo valor
    ''' </summary>
    Private Sub _btnEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles _btnEdit.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.PcePopUpEdit.ClosePopup()
        End If

        RaiseEvent OnKeyPressed(sender, e)
    End Sub

    ''' <summary>
    ''' Se dispara cuando cambia el valor seleccionado
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub _btnEdit_EditValueChanged(sender As Object, e As EventArgs) Handles _btnEdit.EditValueChanged
        RaiseEvent EditValueChanged(sender, e)
    End Sub

    ''' <summary>
    ''' Aqui se realiza la lógica de cambiar el texto mostrado por el
    ''' formato asignado en la propiedad DisplayTextFormat
    ''' </summary>
    Private Sub _btnEdit_Click(sender As Object, e As EventArgs) Handles Me.Click, _btnEdit.Click
        Me.SelectAllText()
    End Sub


    ''' <summary>
    ''' Aqui se realiza el lanzamiento del evento AddButtonClick
    ''' </summary>
    Private Sub _btnEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles _btnEdit.ButtonClick
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
    ''' Se dispara cuando se va a ocultar el popUp
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub PcePopUpEdit_QueryPopUp(ByVal sender As Object, ByVal e As CancelEventArgs) Handles PcePopUpEdit.QueryPopUp
        RaiseEvent QueryCloseUp(sender, e)
    End Sub

    ''' <summary>
    ''' Evento closed para indicar que se cerro el popup de more info
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PcePopUpEdit_Closed(sender As Object, e As EventArgs) Handles PcePopUpEdit.QueryCloseUp, PcePopUpEdit.Closed
        _isPopUpOpen = False
    End Sub


#End Region

#Region "Methods"

    ''' <summary>
    ''' Selecciona todo el texto en la caja de texto
    ''' </summary>
    Private Sub SelectAllText()
        Me._btnEdit.SelectionStart = 0
        Me._btnEdit.SelectAll()
    End Sub

    ''' <summary>
    ''' Inicialización en modo de diseño
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        Me._isFirstInit = False
    End Sub

    ''' <summary>
    ''' Finaliza la inicialización en modo de diseño
    ''' </summary>
    Public Sub EndInit() Implements ISupportInitialize.EndInit
        Me.SetButtons()
    End Sub

    ''' <summary>
    ''' Asigna los botones al popUpEdit segun la lógica de permiso
    ''' de usuario
    ''' </summary>
    Private Sub SetButtons()
        If Me._idOpenForm > 0 Then
            If SessionValues.Instance.IsAllowPermissionForm(Me._idOpenForm.ToString()) Then
                Me._btnEdit.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al dar click en el botón Plus del control
    ''' </summary>
    ''' <param name="sender">Objeto quien dispara el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Friend Sub OnOpenFormButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent OpenFormButtonClick(Me, New EventArgs())
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

End Class