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
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Popup
Imports DevExpress.XtraLayout
Imports DevExpress.XtraGrid.Editors
Imports DevExpress.XtraLayout.Utils
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid
Imports Infrastructure.CrossCutting.Base
Imports System.Threading.Tasks

#End Region

''' <summary>
''' Extiende la funcionalidad del control <see cref="DevExpress.XtraEditors.SearchLookUpEdit"></see>
''' </summary>
<ProvideProperty("ShowFindButton", GetType(SearchLookUpEdit))> _
<ProvideProperty("AppearanceTextFindControl", GetType(SearchLookUpEdit))> _
<ProvideProperty("TextStringFormat", GetType(SearchLookUpEdit))> _
<ProvideProperty("AppearanceEmbeddedNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("TxtFindEnterEnabled", GetType(SearchLookUpEdit))> _
<ProvideProperty("UseEmbeddedNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("AppendButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("CancelEditButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("EditButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("EndEditButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("FirstButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("LastButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("NextButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("NextPageButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("PrevButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("PrevPageButtonNavigator", GetType(SearchLookUpEdit))> _
<ProvideProperty("RemoveButtonNavigator", GetType(SearchLookUpEdit))>
<ProvideProperty("SaveXmlGrid", GetType(SearchLookUpEdit))> _
<ProvideProperty("OpenForm", GetType(SearchLookUpEdit))> _
<ProvideProperty("TagForm", GetType(SearchLookUpEdit))> _
<ProvideProperty("ExportButton", GetType(SearchLookUpEdit))> _
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

        Public Property ExportButton As Boolean
        Public Property ExportButtonControl As ExportDataButton
        Public Property FindPanelStyle As Boolean
    End Class

#Region "ExportButton"

    ''' <summary>
    ''' Obtiene un valor que indica si se muestra el botón de exportado
    ''' </summary>
    ''' <param name="p">Objeto a extender</param>
    ''' <returns>Un valor que indica si se muestra el botón de exportado</returns>
    <Description("Obtiene o asigna un valor que indica si se muestra el botón de exportado")> _
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
        If EnsurePropertiesExists(Obj).ExportButton AndAlso Obj.Properties.View IsNot Nothing AndAlso CType(Obj.Properties.View, GridView).OptionsFind.AllowFindPanel AndAlso CType(Obj.Properties.View, GridView).OptionsFind.AlwaysVisible Then
            If EnsurePropertiesExists(Obj).ExportButtonControl IsNot Nothing Then
                EnsurePropertiesExists(Obj).ExportButtonControl.Visible = True
            End If
        Else
            If EnsurePropertiesExists(Obj).ExportButtonControl IsNot Nothing Then
                EnsurePropertiesExists(Obj).ExportButtonControl.Visible = False
            End If
        End If
    End Sub

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
    <Description("Propiedad que especifica si se guarda una definicion Xml del SearchLookUpEdit")> _
    Public Function GetSaveXmlGrid(ByVal p As SearchLookUpEdit) As Boolean
        Return EnsurePropertiesExists(p).SaveXmlGrid
    End Function

    ''' <summary>
    ''' Funcion que devuelve si el SearchLookUpEdit abre formulario para crear nuevo registro del datasource
    ''' </summary>
    ''' <param name="p">The p.</param>
    ''' <returns></returns>
    <Description("Propiedad que especifica si se agrega el boton para abrir los formulario de archivo segun correspondan")> _
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

    ''' <summary>
    ''' Captura el evento PopUp del control y aplica la propiedad ShowFindButton segun su valor
    ''' </summary>
    Private Sub SearchLookUpEdit_PopUp(sender As Object, e As EventArgs)
        If TryCast(sender, DevExpress.Utils.Win.IPopupControl).PopupWindow IsNot Nothing Then
            Dim p As SearchLookUpEdit = TryCast(sender, SearchLookUpEdit)
            Dim popup As DevExpress.Utils.Win.IPopupControl = TryCast(sender, DevExpress.Utils.Win.IPopupControl)

            'Aqui se agrega el botón
            If Not popup.PopupWindow.Controls.Contains(EnsurePropertiesExists(p).ExportButtonControl) Then
                If EnsurePropertiesExists(p).ExportButtonControl.IsDisposed Then
                    EnsurePropertiesExists(p).ExportButtonControl = New ExportDataButton(p)
                End If
                popup.PopupWindow.Controls.Add(EnsurePropertiesExists(p).ExportButtonControl)
                EnsurePropertiesExists(p).ExportButtonControl.Location = New Point((popup.PopupWindow.Size.Width - EnsurePropertiesExists(p).ExportButtonControl.Size.Width - 25), 13)
                EnsurePropertiesExists(p).ExportButtonControl.Anchor = AnchorStyles.Top Or AnchorStyles.Right
                EnsurePropertiesExists(p).ExportButtonControl.BringToFront()
            End If

            Dim form As PopupBaseForm = TryCast(popup.PopupWindow, PopupBaseForm)
            If form IsNot Nothing Then
                Dim btFindLCI As LayoutControlItem = GetFindButtonLayoutItem(form)
                Dim txtFindControl As LayoutControlItem = GetTextFindControl(form)
                Dim btnClearControl As Control = GetClearButtonControl(form)
                If btFindLCI IsNot Nothing Then
                    'txtFindControl.SizeConstraintsType = SizeConstraintsType.Custom
                    txtFindControl.Control.Font = New Font("Segoe UI Light", 12.0!)
                    txtFindControl.Control.MinimumSize = New Size(txtFindControl.Control.MinimumSize.Width, 28)
                    txtFindControl.Control.MaximumSize = New Size(txtFindControl.Control.MaximumSize.Width, 28)
                    btFindLCI.Control.Font = New Font("Segoe UI Light", 12.0!)
                    btnClearControl.Font = New Font("Segoe UI Light", 12.0!)
                    btnClearControl.Size = New Size(100, 36)
                    If GetShowFindButton(p) Then
                        btFindLCI.Visibility = LayoutVisibility.Always
                    Else
                        btFindLCI.Visibility = LayoutVisibility.Never
                    End If
                End If

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


                If txtFindControl IsNot Nothing Then
                    CType(txtFindControl.Control, TextEdit).EnterMoveNextControl = True
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.BackColor = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.BackColor
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.BackColor2 = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.BackColor2
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.BorderColor = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.BorderColor
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.Font = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.Font
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.ForeColor = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.ForeColor
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.GradientMode = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.GradientMode
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.Image = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.Image
                    CType(txtFindControl.Control, TextEdit).Properties.Appearance.Name = Me.EnsurePropertiesExists(p).AppearanceTextFindControl.Name
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
        End If
    End Sub

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

    Private Async Sub INDGridControl_DataSourceChanged(sender As Object, e As EventArgs)
        Await Me.LoadDefinitionFromXmlAsync(sender)
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
        If My.Computer.FileSystem.FileExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml")) Then
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
                                         If view.GridControl.InvokeRequired Then
                                             view.GridControl.BeginInvoke(Sub()
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
        If Not My.Computer.FileSystem.DirectoryExists(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, "")) Then
            My.Computer.FileSystem.CreateDirectory(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, ""))
        End If
        view.SaveLayoutToXml(String.Format(Utils.PATHDEF, Window.Utils.GetPathApplicationFiles(), Me._parentForm.Name, MY_TYPE, view.Name & ".IndigoUser_" & SessionValues.Instance.UserIndigo & ".xml"))
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
                        Dim grid = TryCast(FormLayout.Controls(10), GridControl)
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
    ''' Indica al control que se ha comenzado la inicialización
    ''' </summary>
    Public Sub BeginInit() Implements ISupportInitialize.BeginInit
        'No se expecifica nada al inicio
    End Sub

    ''' <summary>
    ''' Indica al control que se ha completado la inicialización
    ''' </summary>
    Public Sub EndInit() Implements ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Me._hashTableControls
            Dim p As SearchLookUpEdit = TryCast(de.Key, SearchLookUpEdit)

            If Not _hashTableSearchView.ContainsKey(p.Properties.View) Then
                _hashTableSearchView.Add(p.Properties.View, p)
            End If

            p.Properties.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            If p.Properties.Buttons.Count > 1 Then
                p.Properties.Buttons.RemoveAt(1)
            End If
            p.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete, "Limpiar selección (Supr)")})
            AddHandler p.Properties.ButtonClick, AddressOf SearchLookUpEdit_ButtonClick
            AddHandler p.Popup, AddressOf SearchLookUpEdit_PopUp

            'Botón de exportar
            If Not DesignMode Then
                'Habilitamos el botón de exportado
                EnsurePropertiesExists(de.Key).ExportButtonControl = New ExportDataButton(p)
                If EnsurePropertiesExists(de.Key).ExportButton Then
                    EnsurePropertiesExists(de.Key).ExportButtonControl.Visible = True
                Else
                    EnsurePropertiesExists(de.Key).ExportButtonControl.Visible = False
                End If
            End If

            Me._parentForm = TryCast(de.Key, SearchLookUpEdit).FindForm()

            p.Properties.PopupSizeable = False
            p.Properties.ShowFooter = False

            Dim v As GridView
            v = TryCast(de.Key, SearchLookUpEdit).Properties.View
            If v IsNot Nothing Then

                AddHandler v.RowStyle, AddressOf INDGridControl_RowStyle

                If Me.EnsurePropertiesExists(p).SaveXmlGrid Then
                    AddHandler v.ColumnWidthChanged, AddressOf INDGridControl_ColumnWidthChanged
                    AddHandler v.HideCustomizationForm, AddressOf INDGridControl_HideCustomizationForm
                    AddHandler v.ColumnPositionChanged, AddressOf INDGridControl_ColumnPositionChanged
                    AddHandler v.DataSourceChanged, AddressOf INDGridControl_DataSourceChanged
                End If

            End If

            If GetOpenForm(p) = True Then
                If p.Properties.Buttons.Count > 1 Then
                    p.Properties.Buttons.RemoveAt(1)
                End If
                If indigo.ActiveForms IsNot Nothing AndAlso GetTagForm(p) IsNot Nothing Then
                    If indigo.ActiveForms.ContainsKey(GetTagForm(p)) Then
                        Dim exist = indigo.ActiveForms(GetTagForm(p)).Item4.Exists(Function(x) x = "40")
                        If exist Then
                            p.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)})
                        End If
                    End If
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