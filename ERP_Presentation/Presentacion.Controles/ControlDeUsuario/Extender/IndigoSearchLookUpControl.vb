'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Juan F. Tamayo
' Created          : 2013-06-05
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-06-05
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Threading.Tasks
Imports DevExpress.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Editors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Utils
Imports Infrastructure.CrossCutting.Base

#End Region

''' <summary>
''' Extiende la funcionalidad del control <see cref="DevExpress.XtraEditors.SearchLookUpEdit"></see>
''' </summary>
<ProvideProperty("ShowFindButton", GetType(SearchLookUpEdit))>
<ProvideProperty("AppearanceTextFindControl", GetType(SearchLookUpEdit))>
<ProvideProperty("TextStringFormat", GetType(SearchLookUpEdit))>
<ProvideProperty("AppearanceEmbeddedNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("TxtFindEnterEnabled", GetType(SearchLookUpEdit))>
<ProvideProperty("UseEmbeddedNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("AppendButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("CancelEditButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("EditButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("EndEditButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("FirstButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("LastButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("NextButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("NextPageButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("PrevButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("PrevPageButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("RemoveButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("SaveXmlGrid", GetType(SearchLookUpEdit))>
<ProvideProperty("OpenForm", GetType(SearchLookUpEdit))>
<ProvideProperty("TagForm", GetType(SearchLookUpEdit))>
<ProvideProperty("ExportButton", GetType(SearchLookUpEdit))>
<ProvideProperty("ShowDeleteButton", GetType(SearchLookUpEdit))>
<ProvideProperty("PopupSizeable", GetType(SearchLookUpEdit))>
<ProvideProperty("AutomaticOpenForm", GetType(SearchLookUpEdit))>
<ProvideProperty("PopupBestFitHeight", GetType(SearchLookUpEdit))>
Public Class IndigoSearchLookUpControl
    Inherits Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Fields"

    ''' <summary>
    ''' Nombre del tipo de control que extiende
    ''' </summary>
    Private Const MY_TYPE As String = "SearchLookUpEdit"
    ''' <summary>
    ''' Formulario padre
    ''' </summary>
    Private _parentForm As Form

    ''' <summary>
    ''' Tabla hash que contiene los controles a extender
    ''' </summary>
    Private _hashTableControls As Hashtable
    ''' <summary>
    ''' Tabla hash que contiene los controles BaseView de cada uno de los controles a extender
    ''' </summary>
    Private _hashTableBaseView As Hashtable
    Private _hashTableSearchView As Hashtable
    ''' <summary>
    ''' Contenedor
    ''' </summary>
    Private _container As IContainer
    ''' <summary>
    ''' Variable para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Properties"

    ''' <summary>
    ''' Encapsula las propiedades extendidas del control
    ''' </summary>
    Private Class ExtendedProperty

        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el botón de busqueda en la ventana PopUp
        ''' </summary>
        ''' <value>Valor que indica si se muestra el botón de busqueda</value>
        ''' <returns>Un valor que indica si se muestra el botón de busqueda</returns>
        Public Property ShowFindButton As Boolean
        ''' <summary>
        ''' Obtiene o asigna la cadena de texto formato para el control de navegacion
        ''' </summary>
        ''' <value>Cadena de texto formato</value>
        ''' <returns>La cadena de texto formato</returns>
        Public Property TextStringFormat As String
        ''' <summary>
        ''' Obtiene o signa la apariencia del control de navegacion en el control Grid
        ''' </summary>
        Public Property AppearanceEmbeddedNavigator As DevExpress.Utils.AppearanceObject
        ''' <summary>
        ''' Obtiene o signa la apariencia del control textedit en la caja de busqueda
        ''' </summary>
        Public Property AppearanceTextFindControl As DevExpress.Utils.AppearanceObject
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se habilita el evento TxtFindEnter en el control
        ''' </summary>
        ''' <value>Un valor que indica si se habilita el evento TxtFindEnter en el control</value>
        ''' <returns>Valor que indica si se habilita el evento TxtFindEnter en el control</returns>
        Public Property TxtFindEnterEnabled As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se habilita el uso de los controles de navegacion en el control Grid
        ''' </summary>
        ''' <value>Un valor que indica si se habilita el uso de los controles de navegacion en el control Grid</value>
        ''' <returns>Valor que indica si se habilita el uso de los controles de navegacion en el control Grid</returns>
        Public Property UseEmbeddedNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property AppendButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property CancelEditButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property EditButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property EndEditButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property FirstButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property LastButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property NextButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property NextPageButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property PrevButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property PrevPageButtonNavigator As Boolean
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si se muestra el boton
        ''' </summary>
        ''' <value>Valor</value>
        ''' <returns>Valor</returns>
        Public Property RemoveButtonNavigator As Boolean

        Public SaveXmlGrid As Boolean
        Public OpenForm As Boolean
        Public TagForm As String

        Public DefaultLayout As System.IO.Stream

        Public Property ExportButton As Boolean
        Public Property ExportButtonControl As ExportDataButton
        Public Property ShowFooter As Boolean
        Public Property FindPanelStyle As Boolean
        ''' <summary>
        ''' propiedad para ocultar el boton de borrar seleccion
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property ShowDeleteButton As Boolean
        ''' <summary>
        ''' propiedad para identificar si el popup se puede redimensionar
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PopupSizeable As Boolean
        ''' <summary>
        ''' Abre el formulario modal según el tag de manera automática
        ''' </summary>
        ''' <returns></returns>
        Public Property AutomaticOpenForm As Boolean

        ''' <summary>
        '''  propiedad  de la bandera qu ajusta el tamaño del popup
        ''' </summary>
        ''' <returns></returns>
        Public Property PopupBestFitHeight As Boolean
    End Class

#Region "ExportButton"

    Public Function GetShowDeleteButton(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).ShowDeleteButton
    End Function

    Public Function SetShowDeleteButton(ByVal p As SearchLookUpEdit, Value As Boolean) As Boolean
        EnsurePropertiesExists(p).ShowDeleteButton = Value
    End Function

    Public Function GetPopupSizeable(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).PopupSizeable
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si se muestra el popup es redimensionable
    ''' </summary>
    ''' <param name="p">Objeto a extender</param>
    ''' <returns>Un valor que indica si el popup es redimensionable</returns>
    <Description("Obtiene o asigna un valor que indica si el popup es redimensionable")>
    Public Function SetPopupSizeable(ByVal p As SearchLookUpEdit, Value As Boolean) As Boolean
        EnsurePropertiesExists(p).PopupSizeable = Value
        EnsurePropertiesExists(p).ShowFooter = Value
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si se muestra el botón de exportado
    ''' </summary>
    ''' <param name="p">Objeto a extender</param>
    ''' <returns>Un valor que indica si se muestra el botón de exportado</returns>
    <Description("Obtiene o asigna un valor que indica si se muestra el botón de exportado")>
    Public Function GetExportButton(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).ExportButton
    End Function

    ''' <summary>
    ''' Asigna un valor que indica si se muestra el boton de exportado
    ''' </summary>
    ''' <param name="Obj">Objeto a extender</param>
    ''' <param name="Value">Valor que indica si se muestra el botón de exportado</param>
    Public Sub SetExportButton(ByVal Obj As SearchLookUpEdit, Value As Boolean)
        EnsurePropertiesExists(Obj).ExportButton = Value
        If EnsurePropertiesExists(Obj).ExportButton AndAlso Obj.Properties.View IsNot Nothing AndAlso CType(Obj.Properties.View, GridView).OptionsView.ShowFooter Then
            If EnsurePropertiesExists(Obj).ExportButtonControl IsNot Nothing Then
                EnsurePropertiesExists(Obj).ExportButtonControl.Visible = True
            End If
        Else
            If EnsurePropertiesExists(Obj).ExportButtonControl IsNot Nothing Then
                EnsurePropertiesExists(Obj).ExportButtonControl.Visible = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' funcion que establece el valor de la bandera para el ajuste del popup
    ''' </summary>
    ''' <param name="p"></param>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    <Description("Establece el dimensionamiento automatico en altural del Popup del SearchLookUpEdit")>
    Public Function SetPopupBestFitHeight(ByVal p As SearchLookUpEdit, Value As Boolean) As Boolean
        EnsurePropertiesExists(p).PopupBestFitHeight = Value
    End Function

    ''' <summary>
    ''' funcion que obtiene el valor de la bandera para el ajuste del popup
    ''' </summary>
    ''' <param name="p"></param>
    ''' <returns></returns>
    Public Function GetPopupBestFitHeight(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).PopupBestFitHeight
    End Function
#End Region

    ''' <summary>
    ''' Obtiene el valor de la propiedad ShowFindButton del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se muestra el botón de búsqueda en la ventana PopUp</returns>
    Public Function GetShowFindButton(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).ShowFindButton
    End Function

    ''' <summary>
    ''' Obtiene el valor de la propiedad TextStringFormat del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Cadena de texto formato</returns>
    Public Function GetTextStringFormat(ByVal control As SearchLookUpEdit) As String
        Return Me.EnsurePropertiesExists(control).TextStringFormat
    End Function

    ''' <summary>
    ''' Obtiene el valor de la propiedad UseEmbeddedNavigator del control Grid
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita los controles de navegacion en el control Grid</returns>
    Public Function GetUseEmbeddedNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).UseEmbeddedNavigator
    End Function

    ''' <summary>
    ''' Obtiene el valor de la propiedad TxtFindEnterEnabled del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetTxtFindEnterEnabled(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).TxtFindEnterEnabled
    End Function

    ''' <summary>
    ''' Obtiene el valor de la propiedad AppearanceTextFindControl del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Apariencia asignada la control</returns>
    Public Function GetAppearanceTextFindControl(ByVal control As SearchLookUpEdit) As DevExpress.Utils.AppearanceObject
        Return Me.EnsurePropertiesExists(control).AppearanceTextFindControl
    End Function

    ''' <summary>
    ''' Obtiene el valor de la propiedad AppearanceEmbeddedNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Apariencia asignada la control</returns>
    Public Function GetAppearanceEmbeddedNavigator(ByVal control As SearchLookUpEdit) As DevExpress.Utils.AppearanceObject
        Return Me.EnsurePropertiesExists(control).AppearanceEmbeddedNavigator
    End Function

    ''' <summary>
    ''' Funcion que retorna si el gridlookedit guarde la definicion del xml
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se guarda una definicion Xml del SearchLookUpEdit")>
    Public Function GetSaveXmlGrid(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).SaveXmlGrid
    End Function

    ''' <summary>
    ''' Funcion que devuelve si el SearchLookUpEdit abre formulario para crear nuevo registro del datasource
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se agrega el boton para abrir los formulario de archivo segun correspondan")>
    Public Function GetOpenForm(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).OpenForm
    End Function

    ''' <summary>
    ''' Funcion que devuelve el tag del formulario para abrir y crear nuevo
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    Public Function GetTagForm(ByVal p As SearchLookUpEdit) As String
        Return EnsurePropertiesExists(p).TagForm
    End Function


    ''' <summary>
    ''' Funcion que devuelve el tag del formulario para abrir y crear nuevo
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    Public Function GetAutomaticOpenForm(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).AutomaticOpenForm
    End Function

    ''' <summary>
    ''' Metodo para establecer si el SearchLookUpEdit guarda xml
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetSaveXmlGrid(ByVal Obj As SearchLookUpEdit, Value As Boolean)
        EnsurePropertiesExists(Obj).SaveXmlGrid = Value
    End Sub

    ''' <summary>
    '''Metodo para establecer si el SearchLookUpEdit abre un fomrulario
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">if set to <c>true</c> [value].</param>
    Public Sub SetOpenForm(ByVal Obj As SearchLookUpEdit, Value As Boolean)
        EnsurePropertiesExists(Obj).OpenForm = Value
    End Sub

    ''' <summary>
    '''Metodo para establecer el tag del formulario para abrir
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">The value.</param>
    Public Sub SetTagForm(ByVal Obj As SearchLookUpEdit, Value As String)
        EnsurePropertiesExists(Obj).TagForm = Value
    End Sub

    ''' <summary>
    '''Metodo para establecer el tag del formulario para abrir
    ''' </summary>
    ''' <param name="Obj">The obj.</param>
    ''' <param name="Value">The value.</param>
    Public Sub SetAutomaticOpenForm(ByVal Obj As SearchLookUpEdit, Value As Boolean)
        EnsurePropertiesExists(Obj).AutomaticOpenForm = Value
    End Sub

    ''' <summary>
    ''' Asigna el valor a la propiedad ShowFindButton del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se muestra el botón de búsqueda en la ventana PopUp</param>
    Public Sub SetShowFindButton(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).ShowFindButton = value
    End Sub

    ''' <summary>
    ''' Asigna el valor a la propiedad TextStringFormat del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Cadena formato</param>
    Public Sub SetTextStringFormat(ByVal control As SearchLookUpEdit, ByVal value As String)
        Me.EnsurePropertiesExists(control).TextStringFormat = value.Trim()
    End Sub

    ''' <summary>
    ''' Asigna el valor a la propiedad UseEmbeddedNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita los controles de navegacion en el control Grid</param>
    Public Sub SetUseEmbeddedNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).UseEmbeddedNavigator = value
    End Sub

    ''' <summary>
    ''' Asigna el valor a la propiedad TxtFindEnterEnabled del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetTxtFindEnterEnabled(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).TxtFindEnterEnabled = value
    End Sub

    ''' <summary>
    ''' Asigna el valor a la propiedad AppearanceTextFindControl del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Apariencia que se va a asignar al control</param>
    Public Sub SetAppearanceTextFindControl(ByVal control As SearchLookUpEdit, ByVal value As DevExpress.Utils.AppearanceObject)
        Me.EnsurePropertiesExists(control).AppearanceTextFindControl = value
    End Sub

    ''' <summary>
    ''' Asigna el valor a la propiedad AppearanceEmbeddedNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Apariencia que se va a asignar al control</param>
    Public Sub SetAppearanceEmbeddedNavigator(ByVal control As SearchLookUpEdit, ByVal value As DevExpress.Utils.AppearanceObject)
        Me.EnsurePropertiesExists(control).AppearanceEmbeddedNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad AppendButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetAppendButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).AppendButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad AppendButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetAppendButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).AppendButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad CancelEditButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetCancelEditButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).CancelEditButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad CancelEditButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetCancelEditButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).CancelEditButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad EditButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetEditButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).EditButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad EditButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetEditButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).EditButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad EndEditButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetEndEditButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).EndEditButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad EndEditButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetEndEditButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).EndEditButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad FirstButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetFirstButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).FirstButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad FirstButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetFirstButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).FirstButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad LastButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetLastButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).LastButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad LastButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetLastButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).LastButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad NextButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetNextButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).NextButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad NextButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetNextButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).NextButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad NextPageButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetNextPageButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).NextPageButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad NextPageButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetNextPageButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).NextPageButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad PrevButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetPrevButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).PrevButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad PrevButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetPrevButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).PrevButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad PrevPageButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetPrevPageButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).PrevPageButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad PrevPageButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetPrevPageButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).PrevPageButtonNavigator = value
    End Sub

    ''' <summary>
    ''' Obtiene el valor de la propiedad RemoveButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> del cual se va a retornar la propiedad</param>
    ''' <returns>Valor que indica si se habilita el evento TxtFindEnter</returns>
    Public Function GetRemoveButtonNavigator(ByVal control As SearchLookUpEdit) As Boolean
        Return Me.EnsurePropertiesExists(control).RemoveButtonNavigator
    End Function

    ''' <summary>
    ''' Asigna el valor a la propiedad RemoveButtonNavigator del control
    ''' </summary>
    ''' <param name="control">Control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" /> al cual se va a asignar la propiedad</param>
    ''' <param name="value">Valor que indica si se habilita el evento TxtFindEnter</param>
    Public Sub SetRemoveButtonNavigator(ByVal control As SearchLookUpEdit, ByVal value As Boolean)
        Me.EnsurePropertiesExists(control).RemoveButtonNavigator = value
    End Sub

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de <see cref="IndigoSearchLookUpControl" />
    ''' </summary>
    Public Sub New()
        Me._hashTableControls = New Hashtable()
        Me._hashTableBaseView = New Hashtable()
        Me._hashTableSearchView = New Hashtable()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Inicializa una nueva instancia de <see cref="IndigoSearchLookUpControl" />
    ''' </summary>
    ''' <param name="container">Contenedor</param>
    Public Sub New(ByVal container As System.ComponentModel.IContainer)
        Me.New()
        If container IsNot Nothing Then
            container.Add(Me)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el componente
    ''' </summary>
    Private Sub InitializeComponent()
        Me._container = New Container()
    End Sub

#End Region

#Region "Handlers"
    Private ControlsApplyedStyle As New List(Of String)()

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SearchLookUpEdit_Paint(sender As Object, e As PaintEventArgs)
        Dim p As SearchLookUpEdit = TryCast(sender, SearchLookUpEdit)
        If p.EditValue Is Nothing Then
            Dim currentSkin As DevExpress.Skins.Skin = DevExpress.Skins.CommonSkins.GetSkin(p.LookAndFeel)
            p.Properties.Appearance.ForeColor = currentSkin.Colors.GetColor(DevExpress.Skins.CommonColors.WindowText)
        End If
    End Sub


    ''' <summary>
    ''' Captura el evento PopUp del control y aplica la propiedad ShowFindButton segun su valor
    ''' </summary>
    Private Sub SearchLookUpEdit_PopUp(sender As Object, e As EventArgs)
        If TryCast(sender, DevExpress.Utils.Win.IPopupControl).PopupWindow IsNot Nothing Then
            Dim p As SearchLookUpEdit = TryCast(sender, SearchLookUpEdit)
            If ControlsApplyedStyle.Contains(p.Name) Then
                Exit Sub
            End If
            ControlsApplyedStyle.Add(p.Name)
            Dim popup As DevExpress.Utils.Win.IPopupControl = TryCast(sender, DevExpress.Utils.Win.IPopupControl)
            'Aqui se agrega el botón
            If Not popup.PopupWindow.Controls.Contains(EnsurePropertiesExists(p).ExportButtonControl) Then
                If EnsurePropertiesExists(p).ExportButtonControl.IsDisposed Then
                    EnsurePropertiesExists(p).ExportButtonControl = New ExportDataButton(p)
                End If
                popup.PopupWindow.Controls.Add(EnsurePropertiesExists(p).ExportButtonControl)
            End If

            Dim form As PopupBaseForm = TryCast(popup.PopupWindow, PopupBaseForm)
            If form IsNot Nothing Then
                Dim btFindLCI As LayoutControlItem = GetFindButtonLayoutItem(form)
                Dim txtFindControl As LayoutControlItem = GetTextFindControl(form)
                Dim btnClearControl As Control = GetClearButtonControl(form)
                If btFindLCI IsNot Nothing Then
                    txtFindControl.Control.Font = New Font("Segoe UI Light", 10.0!)
                    txtFindControl.Control.MinimumSize = New Size(txtFindControl.Control.MinimumSize.Width, 28)
                    txtFindControl.Control.MaximumSize = New Size(txtFindControl.Control.MaximumSize.Width, 28)
                    btFindLCI.Control.Font = New Font("Segoe UI Light", 10.0!)
                    btnClearControl.Font = New Font("Segoe UI Light", 10.0!)
                    btnClearControl.Size = New Size(100, 36)
                    If GetShowFindButton(p) Then
                        btFindLCI.Visibility = LayoutVisibility.Always
                    Else
                        btFindLCI.Visibility = LayoutVisibility.Never
                    End If
                End If

                If Me.EnsureBaseViewExists(p) IsNot Nothing Then
                    Me.EnsureBaseViewExists(p).GridControl.UseEmbeddedNavigator = Me.EnsurePropertiesExists(p).UseEmbeddedNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.TextStringFormat = Me.EnsurePropertiesExists(p).TextStringFormat

                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.BackColor = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.BackColor
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.BackColor2 = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.BackColor2
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.BorderColor = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.BorderColor
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.Font = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.Font
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.ForeColor = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.ForeColor
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.GradientMode = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.GradientMode
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.Image = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.Image
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Appearance.Name = Me.EnsurePropertiesExists(p).AppearanceEmbeddedNavigator.Name

                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.Append.Visible = Me.EnsurePropertiesExists(p).AppendButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = Me.EnsurePropertiesExists(p).CancelEditButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.Edit.Visible = Me.EnsurePropertiesExists(p).EditButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = Me.EnsurePropertiesExists(p).EndEditButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.First.Visible = Me.EnsurePropertiesExists(p).FirstButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.Last.Visible = Me.EnsurePropertiesExists(p).LastButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.Next.Visible = Me.EnsurePropertiesExists(p).NextButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.NextPage.Visible = Me.EnsurePropertiesExists(p).NextPageButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.Prev.Visible = Me.EnsurePropertiesExists(p).PrevButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = Me.EnsurePropertiesExists(p).PrevPageButtonNavigator
                    Me.EnsureBaseViewExists(p).GridControl.EmbeddedNavigator.Buttons.Remove.Visible = Me.EnsurePropertiesExists(p).RemoveButtonNavigator
                End If

                If txtFindControl IsNot Nothing Then
                    CType(txtFindControl.Control, TextEdit).EnterMoveNextControl = True
                End If
                AddHandler p.KeyDown, AddressOf SearchLookUpEdit_KeyDown
                If GetTxtFindEnterEnabled(p) Then
                    txtFindControl.Tag = p
                    AddHandler CType(txtFindControl.Control, TextEdit).KeyDown, AddressOf TxtFindControl_KeyDown
                Else
                    txtFindControl.Tag = Nothing
                    RemoveHandler CType(txtFindControl.Control, TextEdit).KeyDown, AddressOf TxtFindControl_KeyDown
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que inicializa el tamaño del popup con valor pordefecto minimo ( se deja esto para que el ajuste el popup, se de arriba hacia abajo)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SearchLookUpEdit_Init(sender As Object, e As EventArgs)
        Dim p As SearchLookUpEdit = TryCast(sender, SearchLookUpEdit)

        If Not GetPopupBestFitHeight(p) Then
            Exit Sub
        End If

        If ControlsApplyedStyle.Contains(p.Name) Then
            Exit Sub
        End If
        Dim popupForm = TryCast(p.GetPopupEditForm(), PopupSearchLookUpEditForm)
        popupForm.BeginUpdate()
        p.Properties.PopupFormSize = New System.Drawing.Size(p.Width, 139)
    End Sub

    ''' <summary>
    ''' evento que recalcula el tamaño del popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub SearchLookUpEdit_BestHeight(sender As Object, e As EventArgs)
        Dim p As SearchLookUpEdit = TryCast(sender, SearchLookUpEdit)

        If Not GetPopupBestFitHeight(p) Then
            Exit Sub
        End If

        Dim popupForm = TryCast(p.GetPopupEditForm(), PopupSearchLookUpEditForm)
        Try
            Dim rowCount As Integer?
            Dim xpSource As DevExpress.Xpo.XPInstantFeedbackSource = TryCast(p.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
            If xpSource IsNot Nothing Then
                ' Crear TaskCompletionSource para manejar la tarea asíncrona
                Dim taskCompletionSource = New TaskCompletionSource(Of Boolean)()
                ' Definir el controlador del evento AsyncCompleted
                Dim handler As EventHandler = Nothing
                handler = Async Sub(senderObj As Object, eArgs As EventArgs)
                              ' Verificar si la tarea ya ha sido completada
                              If Not taskCompletionSource.Task.IsCompleted Then
                                  ' Obtener la cantidad de filas
                                  rowCount = p?.Properties?.View?.RowCount
                                  ' Ajustar el tamaño del popup
                                  Await Me.SearchLookUpEditSizePopup(p, If(rowCount, 0))
                                  ' Eliminar el controlador del evento para evitar múltiples suscripciones
                                  RemoveHandler p.Properties.View.AsyncCompleted, handler
                                  ' Completar la tarea
                                  taskCompletionSource.SetResult(True)
                              End If
                          End Sub

                ' Agregar el controlador del evento AsyncCompleted
                AddHandler p.Properties.View.AsyncCompleted, handler
                ' Esperar la tarea asíncrona
                Await taskCompletionSource.Task
            Else
                rowCount = p?.Properties?.View?.RowCount
                Await Me.SearchLookUpEditSizePopup(p, If(rowCount, 0))
            End If
        Finally
            popupForm.EndUpdate()
            p.ShowPopup()
        End Try
    End Sub

    ''' <summary>
    ''' Captura el evento KeyDown en el control Textedit de la caja de texto
    ''' </summary>
    Private Sub TxtFindControl_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode.Equals(Keys.Enter) Then
            Dim p = TryCast(sender, TextEdit)
            If p IsNot Nothing Then
                RaiseEvent TxtFindControlEnter(p.Tag, New TxtFindControlEnterEventArgs(Me.EnsureBaseViewExists(TryCast(p.Tag, SearchLookUpEdit)), p))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Captura el evento KeyDow en el control SearchLookUpEdit
    ''' </summary>
    Private Sub SearchLookUpEdit_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode.Equals(Keys.Delete) Then
            Dim p = TryCast(sender, SearchLookUpEdit)
            If p IsNot Nothing Then
                p.EditValue = Nothing
                p.Text = String.Empty
            End If
        End If
    End Sub

    ''' <summary>
    ''' Captura el evento ButtonClick del control <see cref="DevExpress.XtraEditors.SearchLookUpEdit" />
    ''' </summary>
    Private Sub SearchLookUpEdit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            Dim p = TryCast(sender, SearchLookUpEdit)
            If p IsNot Nothing Then
                p.Text = String.Empty
                p.EditValue = Nothing
            End If
        ElseIf e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus AndAlso GetAutomaticOpenForm(sender) Then
            Dim mdi = GetMDIForm(DirectCast(DirectCast(sender, Control).TopLevelControl, Form))
            If mdi IsNot Nothing Then
                CType(mdi, Object).OpenForm(GetTagForm(sender), Nothing, True)
                Dim p = TryCast(sender, SearchLookUpEdit)
                p.Properties.DataSource = Nothing
            End If
        End If
    End Sub

    Private Function GetMDIForm(form As Form) As Form
        If form?.Owner Is Nothing Then Return form

        Return GetMDIForm(form.Owner.TopLevelControl)
    End Function

#End Region

#Region "Methods"

    ''' <summary>
    ''' Evento que se utiliza dar la apariencia al autofilterrow
    ''' </summary>
    Private Sub INDGridControl_RowStyle(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs)
        If e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            e.Appearance.BackColor = Color.LightGray
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que dimensionamos el tamaño de las columnas.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraGrid.Views.Base.ColumnEventArgs" /> instance containing the event data.</param>
    Private Async Sub INDGridControl_ColumnWidthChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.ColumnEventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para guardar la definicion de la rejilla cada ves que se agregan,se quitan columnas de la rejila se dispara al cerrar el formulario de customizacion.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDGridControl_HideCustomizationForm(ByVal sender As Object, ByVal e As System.EventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender)
    End Sub

    Private Async Sub INDGridControl_ColumnPositionChanged(sender As Object, e As EventArgs)
        Await Me.SaveDefinitionToXmlAsync(sender.View)
    End Sub

    ''' <summary>
    ''' Carga la definición de la vista de un archivo Xml
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefinitionFromXmlAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub()
                                         If TryCast(_hashTableSearchView(view), SearchLookUpEdit).InvokeRequired Then
                                             TryCast(_hashTableSearchView(view), SearchLookUpEdit).BeginInvoke(Sub()
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
        If Me._parentForm IsNot Nothing AndAlso My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
            view.RestoreLayoutFromXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
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
                                         If TryCast(_hashTableSearchView(view), SearchLookUpEdit).InvokeRequired Then
                                             TryCast(_hashTableSearchView(view), SearchLookUpEdit).BeginInvoke(Sub()
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
        If Me._parentForm IsNot Nothing Then
            If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, "")) Then
                My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, ""))
            End If
            view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
        End If
    End Sub

    ''' <summary>
    ''' Graba la definición por defecto de la vista a los archivos temporales de forma asíncrona
    ''' </summary>
    ''' <param name="view">Vista a grabar</param>
    Private Function SaveDefaultDefinitionAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub() SaveDefaultDefinition(view))
    End Function

    ''' <summary>
    ''' Graba la definición por defecto de la vista a los archivos temporales
    ''' </summary>
    ''' <param name="view">Vista a grabar</param>
    Private Sub SaveDefaultDefinition(ByVal view As GridView)
        EnsurePropertiesExists(TryCast(_hashTableSearchView(view), SearchLookUpEdit)).DefaultLayout = New System.IO.MemoryStream()
        view.SaveLayoutToStream(EnsurePropertiesExists(TryCast(_hashTableSearchView(view), SearchLookUpEdit)).DefaultLayout)
        EnsurePropertiesExists(TryCast(_hashTableSearchView(view), SearchLookUpEdit)).DefaultLayout.Position = 0
    End Sub

    ''' <summary>
    ''' Carga la definición por defecto de forma asíncrona
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Function LoadDefaultDefinitionAsync(ByVal view As GridView) As Task
        Return Task.Factory.StartNew(Sub() LoadDefaultDefinition(view))
    End Function

    ''' <summary>
    ''' Carga la definición por defecto
    ''' </summary>
    ''' <param name="view">Vista a cargar</param>
    Private Sub LoadDefaultDefinition(ByVal view As GridView)
        EnsurePropertiesExists(TryCast(_hashTableSearchView(view), SearchLookUpEdit)).DefaultLayout.Position = 0
        If TryCast(_hashTableSearchView(view), SearchLookUpEdit).InvokeRequired Then
            TryCast(_hashTableSearchView(view), SearchLookUpEdit).BeginInvoke(Sub()
                                                                                  view.RestoreLayoutFromStream(EnsurePropertiesExists(TryCast(_hashTableSearchView(view), SearchLookUpEdit)).DefaultLayout)
                                                                                  view.ClearColumnsFilter()
                                                                                  view.FindFilterText = String.Empty
                                                                              End Sub)
        Else
            view.RestoreLayoutFromStream(EnsurePropertiesExists(TryCast(_hashTableSearchView(view), SearchLookUpEdit)).DefaultLayout)
            view.ClearColumnsFilter()
            view.FindFilterText = String.Empty
        End If
        EnsurePropertiesExists(TryCast(_hashTableSearchView(view), SearchLookUpEdit)).DefaultLayout.Position = 0
    End Sub

    ''' <summary>
    ''' Restaura los layouts por defecto de todas las rejillas
    ''' </summary>
    Public Function RestoreDefaultLayoutsAsync() As Task
        Return Task.Factory.StartNew(AddressOf RestoreDefaultLayouts)
    End Function

    ''' <summary>
    ''' Restaura los layouts por defecto de todas las rejillas
    ''' </summary>
    Public Sub RestoreDefaultLayouts()
        For Each de As DictionaryEntry In Me._hashTableControls
            Dim gridView As GridView = DirectCast(de.Key, SearchLookUpEdit).Properties.View
            If gridView IsNot Nothing Then
                Dim def = String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, gridView.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")
                'Eliminamos la definición personalizada
                If System.IO.File.Exists(def) Then
                    System.IO.File.Delete(def)
                End If
                'Cargamos la definición por defecto
                Me.LoadDefaultDefinition(gridView)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Permite encontrar el ItemLayout del boton de busqueda en el formulario de la ventana PopUp
    ''' </summary>
    ''' <param name="Form">PopUp control base</param>
    ''' <returns>El ItemLayout del boton de busqueda</returns>
    Private Function GetFindButtonLayoutItem(Form As PopupBaseForm) As LayoutControlItem
        For Each FormC As Control In Form.Controls
            If TypeOf FormC Is SearchEditLookUpPopup Then
                Dim SearchPopup As SearchEditLookUpPopup = TryCast(FormC, SearchEditLookUpPopup)
                For Each SearchPopupC As Control In SearchPopup.Controls
                    If TypeOf SearchPopupC Is LayoutControl Then
                        Dim FormLayout As LayoutControl = TryCast(SearchPopupC, LayoutControl)
                        Dim Button As Control = FormLayout.GetControlByName("btFind")
                        If Button IsNot Nothing Then
                            Return FormLayout.GetItemByControl(Button)
                        End If
                    End If
                Next
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Permite encontrar el control textedit de la caja de busqueda
    ''' </summary>
    ''' <param name="Form">PopUp control base</param>
    ''' <returns>El control text de la caja de busqueda</returns>
    Private Function GetTextFindControl(Form As PopupBaseForm) As LayoutControlItem
        For Each FormC As Control In Form.Controls
            If TypeOf FormC Is SearchEditLookUpPopup Then
                Dim SearchPopup As SearchEditLookUpPopup = TryCast(FormC, SearchEditLookUpPopup)
                For Each SearchPopupC As Control In SearchPopup.Controls
                    If TypeOf SearchPopupC Is LayoutControl Then
                        Dim FormLayout As LayoutControl = TryCast(SearchPopupC, LayoutControl)
                        Dim Text As Control = FormLayout.GetControlByName("teFind")
                        Return FormLayout.GetItemByControl(Text)
                    End If
                Next
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Permite encontrar el control simplebutton que limpia de la caja de busqueda
    ''' </summary>
    ''' <param name="Form">PopUp control base</param>
    ''' <returns>El control simplebutton de la caja de busqueda</returns>
    Private Function GetClearButtonControl(Form As PopupBaseForm) As Control
        For Each FormC As Control In Form.Controls
            If TypeOf FormC Is SearchEditLookUpPopup Then
                Dim SearchPopup As SearchEditLookUpPopup = TryCast(FormC, SearchEditLookUpPopup)
                For Each SearchPopupC As Control In SearchPopup.Controls
                    If TypeOf SearchPopupC Is LayoutControl Then
                        Dim FormLayout As LayoutControl = TryCast(SearchPopupC, LayoutControl)
                        Return FormLayout.GetControlByName("btClear")
                    End If
                Next
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Permite encontrar el control BaseView de la caja de busqueda
    ''' </summary>
    ''' <param name="Form">PopUp control base</param>
    ''' <returns>El control BaseView de la caja de busqueda</returns>
    Private Function GetGridViewControl(Form As PopupBaseForm) As GridView
        For Each FormC As Control In Form.Controls
            If TypeOf FormC Is SearchEditLookUpPopup Then
                Dim SearchPopup As SearchEditLookUpPopup = TryCast(FormC, SearchEditLookUpPopup)
                For Each SearchPopupC As Control In SearchPopup.Controls
                    If TypeOf SearchPopupC Is LayoutControl Then
                        Dim FormLayout As LayoutControl = TryCast(SearchPopupC, LayoutControl)
                        Dim grid = TryCast(FormLayout.Controls(8), GridControl)
                        If grid IsNot Nothing Then
                            Return grid.MainView
                        End If
                        Return Nothing
                    End If
                Next
            End If
        Next
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene o asigna las propiedades extendidas del control
    ''' </summary>
    ''' <param name="key">Clave que indica el control del cual se retorna las propiedades</param>
    ''' <returns>Objeto de tipo <see cref="ExtendedProperty" /> que encapsula las propiedades el control</returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As ExtendedProperty
        Dim p As ExtendedProperty = DirectCast(Me._hashTableControls(key), ExtendedProperty)
        If p Is Nothing Then
            p = New ExtendedProperty()
            p.AppearanceEmbeddedNavigator = New DevExpress.Utils.AppearanceObject(New DevExpress.Utils.AppearanceDefault())
            p.AppearanceTextFindControl = New DevExpress.Utils.AppearanceObject(New DevExpress.Utils.AppearanceDefault())
            p.TextStringFormat = "{0} - {1}"
            p.ShowFindButton = True
            p.SaveXmlGrid = True
            p.ShowDeleteButton = True
            Me._hashTableControls(key) = p
        End If
        Return p
    End Function

    ''' <summary>
    ''' Obtiene o asigna los controles <see cref="DevExpress.XtraGrid.Views.Grid.GridView" /> de cada uno de los controles a extender
    ''' </summary>
    ''' <param name="key">Clave que indica el control del cual se retorna el <see cref="DevExpress.XtraGrid.Views.Grid.GridView" /></param>
    ''' <returns>Objeto de tipo <see cref="DevExpress.XtraGrid.Views.Grid.GridView" /></returns>
    Private Function EnsureBaseViewExists(ByVal key As Object) As GridView
        Dim popup As DevExpress.Utils.Win.IPopupControl = TryCast(key, DevExpress.Utils.Win.IPopupControl)
        Dim form As PopupBaseForm = TryCast(popup.PopupWindow, PopupBaseForm)
        If form IsNot Nothing Then
            Dim p As GridView = DirectCast(Me._hashTableBaseView(key), GridView)
            If p Is Nothing Then
                p = GetGridViewControl(form)
                Me._hashTableBaseView(key) = p
            End If
            Return p
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el formulario en el cual se encuentra el control
    ''' </summary>
    ''' <param name="c">Control a usar como base de busqueda</param>
    Private Sub GetFormInstance(ByVal c As Control)
        Me._parentForm = c.FindForm()
        If Me._parentForm Is Nothing AndAlso c.Parent IsNot Nothing Then
            Me.GetFormInstance(c.Parent)
        End If
    End Sub

    ''' <summary>
    ''' Indica al control que se ha comenzado la inicialización
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        'No se expecifica nada al inicio
    End Sub

    ''' <summary>
    ''' Indica al control que se ha completado la inicialización
    ''' </summary>
    Public Async Sub EndInit() Implements ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Me._hashTableControls
            Dim p As SearchLookUpEdit = TryCast(de.Key, SearchLookUpEdit)

            If Not _hashTableSearchView.ContainsKey(p.Properties.View) Then
                _hashTableSearchView.Add(p.Properties.View, p)
            End If
            If DesignMode Then
                p.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            End If
            If p.Properties.Buttons.Count > 1 Then
                p.Properties.Buttons.RemoveAt(1)
            End If
            If GetShowDeleteButton(p) Then
                p.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "Limpiar selección (Supr)")})
            End If

            AddHandler p.Properties.ButtonClick, AddressOf SearchLookUpEdit_ButtonClick
            AddHandler p.Popup, AddressOf SearchLookUpEdit_PopUp
            AddHandler p.Paint, AddressOf SearchLookUpEdit_Paint
            AddHandler p.Popup, AddressOf SearchLookUpEdit_BestHeight
            AddHandler p.BeforePopup, AddressOf SearchLookUpEdit_Init

            'Botón de exportar
            If Not DesignMode Then
                'Habilitamos el botón de exportado
                EnsurePropertiesExists(de.Key).ExportButtonControl = New ExportDataButton(p)
                EnsurePropertiesExists(de.Key).ShowFooter = p.Properties.View.OptionsView.ShowFooter
                If EnsurePropertiesExists(de.Key).ExportButton AndAlso EnsurePropertiesExists(de.Key).ShowFooter Then
                    EnsurePropertiesExists(de.Key).ExportButtonControl.Visible = True
                Else
                    EnsurePropertiesExists(de.Key).ExportButtonControl.Visible = False
                End If
            End If

            If Me._parentForm Is Nothing Then
                Me.GetFormInstance(de.Key)
            End If

            p.Properties.PopupSizeable = GetPopupSizeable(p)
            p.Properties.ShowFooter = GetPopupSizeable(p)

            Dim v As GridView
            v = TryCast(de.Key, SearchLookUpEdit).Properties.View
            If v IsNot Nothing AndAlso Not DesignMode Then

                AddHandler v.RowStyle, AddressOf INDGridControl_RowStyle
                'Guardamos la definición por defecto
                Await Me.SaveDefaultDefinitionAsync(v)
                If Me.EnsurePropertiesExists(p).SaveXmlGrid Then
                    AddHandler v.ColumnWidthChanged, AddressOf INDGridControl_ColumnWidthChanged
                    AddHandler v.HideCustomizationForm, AddressOf INDGridControl_HideCustomizationForm
                    AddHandler v.ColumnPositionChanged, AddressOf INDGridControl_ColumnPositionChanged
                    'Cargamos la definición personalizada
                    Await Me.LoadDefinitionFromXmlAsync(v)
                End If

            End If

            If GetOpenForm(p) = True Then
                If p.Properties.Buttons.Count > 1 Then
                    p.Properties.Buttons.RemoveAt(1)
                End If
                If GetTagForm(p) IsNot Nothing AndAlso indigo.IsAllowPermissionForm(GetTagForm(p)) Then
                    p.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Obtiene un valor que indica si el control puede ser extendido
    ''' </summary>
    ''' <param name="extendee">Control a extender</param>
    ''' <returns>Valor que indica si el control puede ser extendido</returns>
    Public Function CanExtend(extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
        If TypeOf (extendee) Is SearchLookUpEdit Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Libera los recursos no administrados que utiliza <see cref="T:System.ComponentModel.Component" /> y libera los recursos administrados de forma opcional.
    ''' </summary>
    ''' <param name="disposing">Es true para liberar tanto recursos administrados como no administrados; es false para liberar únicamente recursos no administrados.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Me._container IsNot Nothing Then
                Me._container.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ''' <summary>
    ''' Metodo que recalcula el alto del popup dependiendo el # de registros
    ''' </summary>
    ''' <param name="searchLookUpEdit"></param>
    ''' <param name="rowCount"></param>
    Public Function SearchLookUpEditSizePopup(ByRef searchLookUpEdit As DevExpress.XtraEditors.SearchLookUpEdit, rowCount As Integer) As Task


        If searchLookUpEdit Is Nothing OrElse rowCount <= 0 Then
            Return Task.CompletedTask
        End If

        Dim popupForm = TryCast(searchLookUpEdit.GetPopupEditForm(), PopupSearchLookUpEditForm)

        Dim gridView As DevExpress.XtraGrid.Views.Grid.GridView = searchLookUpEdit.Properties.View

        If gridView Is Nothing Then
            Return Task.CompletedTask
        End If

        Dim maxHeight As Integer = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height * 0.4
        Dim viewInfo = CType(gridView.GetViewInfo(), DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)
        Dim rowInfo As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridRowInfo = viewInfo.RowsInfo(0)

        ' Altura del header
        Dim headerHeight As Integer = viewInfo.ColumnRowHeight + CalHeaderHeight(popupForm, gridView.GridControl)

        ' Altura de una fila
        Dim rowHeight As Integer = If(viewInfo.RowsInfo.Count > 0, viewInfo.RowsInfo(0).Bounds.Height, viewInfo.MinRowHeight)

        ' Altura total del popup - se establece relacion proporcinal a la altura max
        Dim totalHeight As Integer = headerHeight + (rowHeight * rowCount) + If(gridView.OptionsView.ShowAutoFilterRow, rowHeight, 0) + (maxHeight * 0.011576)

        ' Ajustar para no exceder la altura máxima
        totalHeight = Math.Min(totalHeight, maxHeight)

        ' Tamaño del popup
        searchLookUpEdit.Properties.PopupFormSize = New System.Drawing.Size(searchLookUpEdit.Width, totalHeight)

        Return Task.CompletedTask
    End Function

    ''' <summary>
    ''' metodo que calcula el tamaño del header del popup (se le resta al tamaño del popup el tamaño del gridcontrol)
    ''' </summary>
    ''' <param name="popup"></param>
    ''' <returns></returns>
    Private Function CalHeaderHeight(popup As PopupBaseForm, grid As GridControl) As Integer
        Dim headerHeight As Integer = 0

        If popup Is Nothing Then
            Return headerHeight
        End If
        ' se resta el tamño total del popup - el tamaño de la rejilla para obtener la altura complentaria
        headerHeight = popup.Height - grid.Height
        Return headerHeight
    End Function
#End Region

#Region "Events"

    ''' <summary>
    ''' Ocurre cuando se presiona la tecla enter en la caja de texto buscar de la ventana PopUp
    ''' </summary>
    ''' <param name="sender">Objeto quien lanzó el evento</param>
    ''' <param name="e">Datos del evento</param>
    Public Event TxtFindControlEnter(ByVal sender As Object, ByVal e As TxtFindControlEnterEventArgs)

#End Region

End Class

''' <summary>
''' Encapsula los datos del evento TxtFindControlEnter
''' </summary>
''' <remarks></remarks>
Public Class TxtFindControlEnterEventArgs
    Inherits EventArgs

#Region "Fields"

    ''' <summary>
    ''' Encapsula el control <see cref="DevExpress.XtraGrid.Views.Grid.GridView" /> de la ventana PopUp
    ''' </summary>
    Private _baseView As GridView
    ''' <summary>
    ''' Encapsula el control <see cref="DevExpress.XtraEditors.TextEdit" /> de la ventana PopUp
    ''' </summary>
    Private _txtFind As TextEdit

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el objeto <see cref="DevExpress.XtraGrid.Views.Grid.GridView" />
    ''' </summary>
    ''' <returns>El objeto <see cref="DevExpress.XtraGrid.Views.Grid.GridView" /></returns>
    Public ReadOnly Property BaseView As GridView
        Get
            Return Me._baseView
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el objeto <see cref="DevExpress.XtraEditors.TextEdit" />
    ''' </summary>
    ''' <returns>El objeto <see cref="DevExpress.XtraEditors.TextEdit" /></returns>
    Public ReadOnly Property TxtFind As TextEdit
        Get
            Return Me._txtFind
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Instancia un objecto <see cref="TxtFindControlEnterEventArgs" />
    ''' </summary>
    ''' <param name="baseView">Objecto de tipo <see cref="DevExpress.XtraGrid.Views.Grid.GridView" /></param>
    ''' <param name="txtFind">Objecto de tipo <see cref="DevExpress.XtraEditors.TextEdit" /></param>
    Public Sub New(ByVal baseView As GridView, ByVal txtFind As TextEdit)
        Me._baseView = baseView
        Me._txtFind = txtFind
    End Sub

#End Region

End Class
