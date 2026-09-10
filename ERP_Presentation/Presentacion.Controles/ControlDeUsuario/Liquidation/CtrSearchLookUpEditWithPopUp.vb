'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan F. Tamayo
' Created          : 2014-11-11
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraEditors

#End Region

Public Class CtrSearchLookUpEditWithPopUp

#Region "Fields"

    ''' <summary>
    ''' Bandera para saber cuando el control ya se encuentra cargado
    ''' </summary>
    Private _flagControlLoaded As Boolean

    ''' <summary>
    ''' Bandera para saber cuando se ha usado el metodo SetNullText
    ''' y controlar el evento EditValueChanged
    ''' </summary>
    Private _flagSetNullText As Boolean

    ''' <summary>
    ''' Tag del frontal a abrir
    ''' </summary>
    Private _tagForm As String

    ''' <summary>
    ''' Acción a ejecutar cuando se da click en el
    ''' botón de abrir formulario
    ''' </summary>
    Private _openFormAction As Action

#End Region

#Region "Events"

    ''' <summary>
    ''' Se lanza cuando se presiona un botón
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)

    ''' <summary>
    ''' Se lanza cuando el botón agregar es presionado
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event AddButtonClick(ByVal sender As Object, ByVal e As EventArgs)

    ''' <summary>
    ''' Se lanza cuando se ha seleccionado un nuevo valor
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event NewSelectedValue(ByVal sender As Object, ByVal e As SearchAdmissionClosingEventArgs)

    Public Event MouseEnterAdmission(sender As Object, e As EventArgs)
#End Region

#Region "Properties"

    Public Property _flagLoadEditValue As Boolean
    ''' <summary>
    ''' Establece el EditValue del Search
    ''' </summary>
    <BrowsableAttribute(False)> _
    Public WriteOnly Property SetEditValue As Object
        Set(value As Object)
            _flagLoadEditValue = True
            SleSearchLookUpEdit.EditValue = value
            _flagLoadEditValue = False
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el control
    ''' es de solo lectura
    ''' </summary>
    ''' <value>Valor que indica si el control es de solo lectura</value>
    ''' <returns>Un valor que indica si el control es de solo lectura</returns>
    Public Property IsReadOnly As Boolean
        Get
            Return Me.SleSearchLookUpEdit.Properties.ReadOnly
        End Get
        Set(value As Boolean)
            Me.SleSearchLookUpEdit.Properties.ReadOnly = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el SearchLookUpEdit interno
    ''' </summary>
    ''' <returns>SearchLookUpEdit interno</returns>
    Public ReadOnly Property Search As DevExpress.XtraEditors.SearchLookUpEdit
        Get
            Return Me.SleSearchLookUpEdit
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tag del frontal a abrir
    ''' </summary>
    ''' <value>Tag del frontal</value>
    ''' <returns>El tag del frontal</returns>
    Public Property TagForm As String
        Get
            Return Me._tagForm
        End Get
        Set(value As String)
            Me._tagForm = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la acción a ejecutar cuando se da click
    ''' al botón de abrir formulario
    ''' </summary>
    ''' <value>Accion a ejecutar</value>
    ''' <returns>La acción a ejecutar</returns>
    Public Property OpenFormAction As Action
        Get
            Return Me._openFormAction
        End Get
        Set(value As Action)
            Me._openFormAction = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el origen de datos
    ''' </summary>
    ''' <value>Origen de datos</value>
    ''' <returns>El origen de datos</returns>
    <BrowsableAttribute(False)> _
    Public Property Datasource As Object
        Get
            Return Me.SleSearchLookUpEdit.Properties.DataSource
        End Get
        Set(value As Object)
            Me.SleSearchLookUpEdit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)> _
    Public Property PopupContainerControl As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.PcePopUpEdit.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.PcePopUpEdit.Properties.PopupControl = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la colección de botones en el ButtonEdit
    ''' </summary>
    ''' <returns>La colección de botones en el ButtonEdit</returns>
    <BrowsableAttribute(False)> _
    Public ReadOnly Property Buttons As DevExpress.XtraEditors.Controls.EditorButtonCollection
        Get
            Return Me.SleSearchLookUpEdit.Properties.Buttons
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._flagControlLoaded = False
        Me._tagForm = String.Empty
        Me._openFormAction = Nothing
        Me._flagSetNullText = False
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Carga el control
    ''' </summary>
    Private Sub CtrSearchLookUpEditWithPopUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._flagControlLoaded = True
        Me._flagLoadEditValue = False
        AddHandler Me.SleSearchLookUpEdit.GotFocus, AddressOf SleSearchLookUpEdit_GotFocus
        'Validamos los permisos del usuario para mostrar el boton de abrir formulario
        If Me._tagForm Is Nothing OrElse Me._tagForm.Trim().Equals(String.Empty) OrElse Me._openFormAction Is Nothing Then
            Me.SetOpenForm(False)
        Else
            Me.SetOpenForm(Infrastructure.CrossCutting.Base.SessionValues.Instance.IsAllowPermissionForm(Me._tagForm))
        End If
    End Sub

    ''' <summary>
    ''' Aqui se lanza el evento GotFocus del control
    ''' </summary>
    Private Sub SleSearchLookUpEdit_GotFocus(ByVal sender As Object, ByVal e As EventArgs)
        Me.OnGotFocus(e)
    End Sub

    ''' <summary>
    ''' Aqui se lanza el evento cuando cambia el valor seleccionado
    ''' </summary>
    Private Sub SleSearchLookUpEdit_EditValueChanged(sender As Object, e As EventArgs) Handles SleSearchLookUpEdit.EditValueChanged
        If Me._flagControlLoaded AndAlso Me.SleSearchLookUpEdit.EditValue IsNot Nothing AndAlso Not _flagLoadEditValue Then
            Me.SleSearchLookUpEdit.EditValue = Nothing
            Dim objTemp = Me.SlevSearchLookUpEdit.GetFocusedRow()
            If objTemp IsNot Nothing Then
                If objTemp.GetType().Name.Equals(GetType(DevExpress.Data.NotLoadedObject).Name) Then
                    Exit Sub
                End If
                Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                    Me.OnNewSelectedValue(Me, New SearchAdmissionClosingEventArgs(obj.OriginalRow))
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla el lanzamiento del popUp
    ''' </summary>
    Private Sub BteButtonEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles SleSearchLookUpEdit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph AndAlso e.Button.Tag.ToString().Equals("MORE_INFO") AndAlso Me.PcePopUpEdit.Properties.PopupControl IsNot Nothing Then
            Me.PcePopUpEdit.ShowPopup()
        End If
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph AndAlso e.Button.Tag.ToString().Equals("ADD") Then
            Me.OnAddButtonClick(sender, New EventArgs())
        End If
        Me.OnButtonClick(sender, e)
    End Sub

    ''' <summary>
    ''' Aqui se da el foco al ButtonEdit cuando se cierra el popUp
    ''' </summary>
    Private Sub PcePopUpEdit_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles PcePopUpEdit.CloseUp
        Me.SleSearchLookUpEdit.Focus()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Asigna un valor para mostrar o no el boton del abrir formlario
    ''' </summary>
    ''' <param name="value">Valor que indica si se muestra el botón</param>
    Private Sub SetOpenForm(ByVal value As Boolean)
        Me.SleSearchLookUpEdit.Properties.Buttons.Clear()
        If value Then
            Me.SleSearchLookUpEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Controls.My.Resources.Resources.Add_16x16_blue, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject1, "", "ADD", Nothing, True), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Controls.My.Resources.Resources.MoreInfo_16x16_blue, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "MORE_INFO", Nothing, True)})
        Else
            Me.SleSearchLookUpEdit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo), New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, True, True, False, DevExpress.XtraEditors.ImageLocation.MiddleCenter, Global.Presentation.Controls.My.Resources.Resources.MoreInfo_16x16_blue, New DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), SerializableAppearanceObject2, "", "MORE_INFO", Nothing, True)})
        End If
    End Sub

    ''' <summary>
    ''' Asigna el texto a la propiedad NullText del SearchLookUpEdit
    ''' </summary>
    ''' <param name="text">Texto a asignar</param>
    Public Sub SetNullText(ByVal text As String)
        If Me.SleSearchLookUpEdit.EditValue IsNot Nothing Then
            Me.SleSearchLookUpEdit.EditValue = Nothing
        End If
        Me.SleSearchLookUpEdit.Properties.NullText = text
        Me.SleSearchLookUpEdit.Invalidate()
    End Sub

    ''' <summary>
    ''' Obtiene el texto asignado a la propiedad NullText del SearchLookUpEdit
    ''' </summary>
    ''' <returns></returns>
    Public Function GetNullText() As String
        Return Me.SleSearchLookUpEdit.Properties.NullText
    End Function

    ''' <summary>
    ''' Lanza el evento cuando se presiona un botón
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub OnButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        RaiseEvent ButtonClick(sender, e)
    End Sub

    ''' <summary>
    ''' Lanza el evento cuando el botón agregar es presionado
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub OnAddButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        If Me._openFormAction IsNot Nothing Then
            Me._openFormAction()
        End If
        RaiseEvent AddButtonClick(sender, e)
    End Sub

    ''' <summary>
    ''' Lanza el evento cuando se ha seleccionado un nuevo valor
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub OnNewSelectedValue(ByVal sender As Object, ByVal e As SearchAdmissionClosingEventArgs)
        RaiseEvent NewSelectedValue(sender, e)
    End Sub

#End Region

    Public Event SearchOpenPopUp(sender As Object, e As CancelEventArgs)
    Private Sub SleSearchLookUpEdit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles SleSearchLookUpEdit.QueryPopUp
        RaiseEvent SearchOpenPopUp(sender, e)
    End Sub

    Private Sub SleSearchLookUpEdit_MouseEnter(sender As Object, e As EventArgs) Handles SleSearchLookUpEdit.MouseEnter
        RaiseEvent MouseEnterAdmission(Me, EventArgs.Empty)
    End Sub

    ''' <summary>
    ''' evento para quitar el nombre del color, de la columna
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRicColor_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDRicColor.CustomDisplayText
        e.DisplayText = String.Empty
    End Sub
End Class

''' <summary>
''' Encapsula los argumentos del evento SearchAdmissionClosing
''' </summary>
Public Class SearchAdmissionClosingEventArgs
    Inherits EventArgs

#Region "Members"

    ''' <summary>
    ''' Obtiene o asigna el objeto de ingreso seleccionado
    ''' </summary>
    ''' <value>Número de ingreso seleccionado</value>
    ''' <returns>El número de ingreso seleccionado</returns>
    Public Property AdmissionObject As Object

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="admissionObject">Objeto del ingreso seleccionado</param>
    Public Sub New(ByVal admissionObject As Object)
        Me.AdmissionObject = admissionObject
    End Sub

#End Region

End Class