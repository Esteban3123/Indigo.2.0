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

#End Region

Public Class CtrButtonEditWithPopUp

#Region "Fields"

    ''' <summary>
    ''' Tipo de los botones que lanzarán el popUp al dar click
    ''' </summary>
    Private _kindButtonOnPopUp As DevExpress.XtraEditors.Controls.ButtonPredefines

#End Region

#Region "Events"

    ''' <summary>
    ''' Se lanza cuando se presiona un botón
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event ButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el tipo de los botones que lanzarán el popUp al dar click
    ''' </summary>
    ''' <value>Tipo de los botones que lanzarán el popUp</value>
    ''' <returns>El tipo de los botones que lanzarán el popUp</returns>
    <BrowsableAttribute(False)> _
    Public Property KindButtonOnPopUp As DevExpress.XtraEditors.Controls.ButtonPredefines
        Get
            Return Me._kindButtonOnPopUp
        End Get
        Set(value As DevExpress.XtraEditors.Controls.ButtonPredefines)
            Me._kindButtonOnPopUp = value
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
            Return Me.BteButtonEdit.Properties.Buttons
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el texto se configura en solo lectura
    ''' </summary>
    ''' <value>Valor que indica si el texto es de solo lectura</value>
    ''' <returns>El valor que indica si el texto es de solo lectura</returns>
    <BrowsableAttribute(False)> _
    Public Property TextReadOnly As Boolean
        Get
            Return Me.BteButtonEdit.Properties.ReadOnly
        End Get
        Set(value As Boolean)
            Me.BteButtonEdit.Properties.ReadOnly = value
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._kindButtonOnPopUp = DevExpress.XtraEditors.Controls.ButtonPredefines.Combo
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se controla el lanzamiento del popUp
    ''' </summary>
    Private Sub BteButtonEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles BteButtonEdit.ButtonClick
        If e.Button.Kind = Me._kindButtonOnPopUp AndAlso Me.PcePopUpEdit.Properties.PopupControl IsNot Nothing Then
            Me.PcePopUpEdit.ShowPopup()
        End If
        Me.OnButtonClick(sender, e)
    End Sub

    ''' <summary>
    ''' Aqui se da el foco al ButtonEdit cuando se cierra el popUp
    ''' </summary>
    Private Sub PcePopUpEdit_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles PcePopUpEdit.CloseUp
        Me.BteButtonEdit.Focus()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lanza el evento cuando se presiona un botón
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub OnButtonClick(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        RaiseEvent ButtonClick(sender, e)
    End Sub

#End Region

End Class