'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : ----
' Created          : ----
'
' Last Modified By : Juan Diego Diaz
' Last Modified On : 30-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.DocumentalSystem.Entities
Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraReports.UI
Imports Domain.Entities
Imports System.Threading.Tasks
Imports DevExpress.XtraLayout
Imports System.Data
Imports System.Linq
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors
Imports System.Reflection
Imports System.ComponentModel
Imports DevExpress.XtraEditors.CustomEditor
Imports DevExpress.XtraLayout.Utils
Imports Infrastructure.Base.Security

#End Region

''' <summary>
''' Formulario Base
''' </summary>
Public Class FormBase

#Region "Fields"

    ''' <summary>
    ''' Constante error dependencia
    ''' </summary>
    Public Const ErrorDependencia As String = "-000"
    ''' <summary>
    ''' Constante error concurrencia
    ''' </summary>
    Public Const ErrorConcurrencia As String = "-999"
    ''' <summary>
    ''' Id de la entidad a abrir
    ''' </summary>
    Protected _idEntity As String
    ''' <summary>
    ''' Documento para indexar
    ''' </summary>
    Public _doc As IndexedDocument2
    ''' <summary>
    ''' Función para construir el documento de indexación
    ''' </summary>
    Protected _funct As Func(Of IndexedDocument2)
    ''' <summary>
    ''' Modelo para formulario base
    ''' </summary>
    Protected _model As MformBase
    ''' <summary>
    ''' Bandera que indica si ya se esta ejecutando el asyncloader
    ''' </summary>
    Private _isAsyncLoader As Boolean
    ''' <summary>
    ''' Función para generar reporte
    ''' </summary>
    Protected _actionReport As Action(Of PrintReportAction, Integer, Integer, Object())
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Protected indigo As SessionValues
    ''' <summary>
    ''' Variable que contiene la instancia del formulario de busquedas
    ''' </summary>
    ''' <remarks></remarks>
    Public FormSearchObjects As FrmBusqueda
    ''' <summary>
    ''' Diccionario de secuencias numericas del frontal
    ''' </summary>
    Private _dicSequense As Dictionary(Of Int32, List(Of String))
    ''' <summary>
    ''' Variable para determinar si la busqueda es lanzada como principal del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public IsMainSearch As Boolean
    ''' <summary>
    ''' Diccionario para almacenar las columnas y saber cuales desbloquear luego
    ''' </summary>
    Dim _dictionaryColumns As Dictionary(Of Control, List(Of Columns.GridColumn))


    Public Event EventLoadOperatingUnit(listOperatingUnit As IEnumerable(Of Object))
#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Public WriteOnly Property ShowMessage(StatusCode As eStatusResult) As String
        Set(value As String)
            If StatusCode = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf StatusCode = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf StatusCode = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Retorna el panelcontrol donde se encuentra la barra de botones
    ''' </summary>
    Public ReadOnly Property ToolBar As PanelControl
        Get
            Return ToolBars
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el diccionario de secuencias numericas almacenadas 
    ''' en memoria para el frontal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property DicSequense As Dictionary(Of Int32, List(Of String))
        Get
            Return Me._dicSequense
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que obtiene y establece el panel de la barra de botones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdditionalControlPanel As DevExpress.XtraEditors.PanelControl
        Get
            Return Me.BarraBotones.AdditionalControlPanel
        End Get
        Set(value As DevExpress.XtraEditors.PanelControl)
            Me.BarraBotones.AdditionalControlPanel = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el id de la entidad a abrir
    ''' </summary>
    ''' <value>Id de la entidad</value>
    ''' <returns>El id de la entidad</returns>
    Public Property IdEntity As String
        Get
            Return Me._idEntity
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._idEntity = value
            Else
                Me._idEntity = String.Empty
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de solo escritura para establecer un valor al tipo Func
    ''' </summary>
    Protected WriteOnly Property Funct As Func(Of IndexedDocument2)
        Set(value As Func(Of IndexedDocument2))
            Me._funct = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de solo escritura para establecer un valor al tipo Action
    ''' </summary>
    Protected WriteOnly Property ActionReport As Action(Of PrintReportAction, Integer, Integer, Object())
        Set(value As Action(Of PrintReportAction, Integer, Integer, Object()))
            Me._actionReport = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece si el modo de vita por defecto es edicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ViewModeEditHold As Boolean

    ''' <summary>
    ''' Obtiene o asigna el Id del módulo al que pertenece
    ''' </summary>
    ''' <value>Id del módulo al que pertenece</value>
    ''' <returns>El Id del módulo al que pertenece</returns>
    Public Property IdModuleSource As Integer

#End Region

#Region "Methods and Functions"
    ''' <summary>
    ''' Verifica si un formulario tiene un permiso habilitado
    ''' </summary>
    ''' <param name="permission"></param>
    ''' <returns></returns>
    Public Function HasPermission(permission As PermissionsActionsForm) As Boolean
        Return Me.BarraBotones.PermissionsForm?.ContainsKey(permission.GetHashCode())
    End Function

    ''' <summary>
    ''' Verifica si un formulario tiene un permiso habilitado
    ''' </summary>
    ''' <param name="permission"></param>
    ''' <returns></returns>
    Public Function HasPermission(permission As Integer) As Boolean
        Return Me.BarraBotones.PermissionsForm?.ContainsKey(permission)
    End Function

    ''' <summary>
    ''' Restablece el formulario y sus campos al diseño original
    ''' </summary>
    Public Sub RestoreLayoutForm()
        Me.LayoutControls.ResetLayouts()
        Me.DeleteCustomizationControls()
    End Sub

    ''' <summary>
    ''' Enumera todos los componentes del formulario
    ''' </summary>
    ''' <returns>Enumeración de componentes</returns>
    Private Function EnumerateComponents() As IEnumerable(Of Component)
        Return From field In Me.GetType().GetFields(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
               Where GetType(Component).IsAssignableFrom(field.FieldType)
               Let component = DirectCast(field.GetValue(Me), Component)
               Where component IsNot Nothing
               Select component
    End Function

    ''' <summary>
    ''' Recorre todos los controles del formulario y vulve personalizable todos los
    ''' LayoutControls que encuentre
    ''' </summary>
    Private Sub SetLayoutsExtended()
        If Me.BarraBotones.FirstLayoutControl IsNot Nothing Then
            Me.SetLayoutsExtendedRecursive(Me.BarraBotones.FirstLayoutControl)
        End If
    End Sub
    Private Sub SetLayoutsExtendedRecursive(ByVal lyc As LayoutControl)
        If lyc.Controls IsNot Nothing AndAlso lyc.Controls.Count > 0 AndAlso (From c In lyc.Controls Where TypeOf c Is LayoutControl Select c).ToList().Count > 0 Then
            For Each ctr In (From c In lyc.Controls Where TypeOf c Is LayoutControl Select c).ToList()
                SetLayoutsExtendedRecursive(ctr)
            Next
        End If
        Me.LayoutControls.SetIsCustomizable(lyc, True)
        lyc.AllowCustomization = False
    End Sub

    ''' <summary>
    ''' Abre un formulario por su identificación
    ''' </summary>
    ''' <param name="idForm">Identificación del formulario</param>
    ''' <param name="idEntity">Id de la entidad que se va a consultar en el formulario luego de abrirlo</param>
    ''' <param name="isModal">Valor que indica si el formulario se va a abrir como modal</param>
    Protected Sub OpenForm(ByVal idForm As String, Optional ByVal idEntity As String = Nothing, Optional isModal As Boolean = False)
        If Me.MdiParent IsNot Nothing AndAlso (TypeOf Me.MdiParent Is IOpenForm) Then
            CType(Me.MdiParent, IOpenForm).OpenForm(idForm, idEntity, isModal)
        ElseIf Me.Owner IsNot Nothing AndAlso (TypeOf Me.Owner Is IOpenForm) Then
            CType(Me.Owner, IOpenForm).OpenForm(idForm, idEntity, isModal)
        ElseIf Me.Parent IsNot Nothing AndAlso (TypeOf Me.Parent.TopLevelControl Is IOpenForm) Then
            CType(Me.Parent.TopLevelControl, IOpenForm).OpenForm(idForm, idEntity, isModal)
        End If
    End Sub

    ''' <summary>
    ''' Libera la memoria restante
    ''' </summary>
    Protected Sub FreeMemory()
        BaseClass.FreeMemory()
    End Sub

    ''' <summary>
    ''' Pone readOnly todos los controles
    ''' </summary>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    Public Overridable Sub ReadOnlyControls(ByVal state As Boolean)
        If _dictionaryColumns Is Nothing Then
            _dictionaryColumns = New Dictionary(Of Control, List(Of Columns.GridColumn))()
        End If
        For i = 0 To Me.INDPanelControlBase.Controls.Count - 1
            If Me.INDPanelControlBase.Controls(i).GetType Is GetType(LayoutControl) Then
                For Each c As Control In CType(Me.INDPanelControlBase.Controls(i), LayoutControl).Controls
                    If c.ProductName = "DevExpress.XtraEditors" OrElse c.ProductName = "DevExpress.XtraGrid" OrElse c.ProductName = "Controls" Then
                        If c.GetType().BaseType.GetProperty("Properties", Reflection.BindingFlags.InvokeMethod OrElse Reflection.BindingFlags.GetProperty) IsNot Nothing Then
                            CType(c, Object).Properties.ReadOnly = state
                        ElseIf c.ProductName = "DevExpress.XtraGrid" Then
                            Dim _listColumn As New List(Of Columns.GridColumn)()
                            For Each col As Columns.GridColumn In CType(CType(c, Object).MainView, Object).Columns
                                If state Then
                                    If col.OptionsColumn.AllowEdit Then
                                        If col.Name <> "MoreInfo" Then
                                            col.OptionsColumn.AllowEdit = False
                                            _listColumn.Add(col)
                                        End If
                                    End If
                                Else
                                    If _dictionaryColumns.ContainsKey(c) Then
                                        If _dictionaryColumns.Where(Function(x) x.Key.Equals(c)).Cast(Of KeyValuePair(Of Control, List(Of Columns.GridColumn)))() _
                                                   .Select(Function(y) y.Value).Cast(Of List(Of Columns.GridColumn))().FirstOrDefault().FindAll(Function(z) z.Equals(col)).ToList().Count > 0 Then
                                            col.OptionsColumn.AllowEdit = True
                                        End If
                                    End If
                                End If
                            Next
                            If _listColumn.Count > 0 Then
                                If Not _dictionaryColumns.ContainsKey(c) OrElse Not _dictionaryColumns(c).Any(Function(m) _listColumn.Any(Function(o) o.Name = m.Name)) Then
                                    _dictionaryColumns.Add(c, _listColumn)
                                End If
                            End If
                            If Not state Then
                                If _dictionaryColumns.ContainsKey(c) Then
                                    _dictionaryColumns.Remove(c)
                                End If
                            End If
                        Else
                            CType(c, Object).Enabled = Not state
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Valida que controles tienen visibilididad (internacional)
    ''' </summary>
    Public Overridable Sub VisibilityControls()
        If _dictionaryColumns Is Nothing Then
            _dictionaryColumns = New Dictionary(Of Control, List(Of Columns.GridColumn))()
        End If
        For i = 0 To Me.INDPanelControlBase.Controls.Count - 1
            If Me.INDPanelControlBase.Controls(i).GetType Is GetType(LayoutControl) Then
                For Each c In DirectCast(Me.INDPanelControlBase.Controls(i), DevExpress.XtraLayout.LayoutControl).Items
                    If c.GetType().Name = "LayoutControlItem" Then
                        If Not c.ContentVisible Then
                            c.Visibility = LayoutVisibility.Never
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Public Function GetCultureFromCurrencyFormat(Optional CurrencyNumbertFormat As Globalization.NumberFormatInfo = Nothing) As Globalization.CultureInfo
        Dim Culture As Globalization.CultureInfo 'se deja de manera local la variable Culture
        Culture = Globalization.CultureInfo.CurrentCulture().Clone()

        If CurrencyNumbertFormat Is Nothing Then
            CurrencyNumbertFormat = Culture.NumberFormat()
        Else
            Culture.NumberFormat = CurrencyNumbertFormat
        End If

        Culture.NumberFormat = SessionValues.Instance.ConfigurationSeparatorNumberFormat(Culture.NumberFormat)

        Return Culture
    End Function
    ''' <summary>
    ''' Valida los controles numericos que deben cambiar formato al de la moneda oficial
    ''' </summary>
    Public Overridable Sub changeNumericFormatByCurrency(Optional CurrencyNumbertFormat As Globalization.NumberFormatInfo = Nothing, Optional principalControls As Control.ControlCollection = Nothing, Optional objMaskDisplay As Object = Nothing)
        Dim _culture = GetCultureFromCurrencyFormat(CurrencyNumbertFormat)

        If principalControls Is Nothing Then
            principalControls = Me.INDPanelControlBase.Controls
        End If

        If principalControls Is Nothing Then
            principalControls = Me.INDPanelControlBase.Controls
        End If
        For i = 0 To principalControls.Count - 1
            If principalControls(i).GetType Is GetType(LayoutControl) Then
                For Each c In DirectCast(principalControls(i), DevExpress.XtraLayout.LayoutControl).Items
                    If c.GetType().Name = "LayoutControlItem" Then
                        ''Los labels que son numericos
                        If c.Control.GetType().Name = "TextEdit" Or c.Control.GetType().Name = "SpinEdit" Then
                            If c.Control.Properties.Mask IsNot Nothing Then
                                If c.Control.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric AndAlso (c.Control.Properties.Mask.EditMask.Contains("C") Or c.Control.Properties.Mask.EditMask.Contains("c")) Then
                                    c.control.properties.Mask.Culture = _culture
                                    c.Control.Properties.Mask.EditMask = "c" + CurrencyNumbertFormat.CurrencyDecimalDigits.ToString()
                                    If objMaskDisplay IsNot Nothing Then
                                        c.Control.Properties.Mask.UseMaskAsDisplayFormat = objMaskDisplay.useMaskAsDisplayFormat
                                        c.Control.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                        c.Control.Properties.DisplayFormat.FormatString = "c" + objMaskDisplay?.decimals.ToString()
                                    End If
                                End If
                            End If
                        End If

                        ''las rejillas con columnas de valor numerico cambien el formato segun la moneda
                        If c.Control.GetType().Name = "GridControl" Then
                            For Each item In c.Control.MainView.columns
                                If (item.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric OrElse item.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom) _
                                    AndAlso (item.DisplayFormat.FormatString.Contains("C") Or item.DisplayFormat.FormatString.Contains("c")) Then
                                    item.DisplayFormat.Format = _culture
                                    item.DisplayFormat.FormatString = "c" + CurrencyNumbertFormat.CurrencyDecimalDigits.ToString()
                                End If
                            Next
                        End If
                    End If
                Next
            Else
                ''En caso de que sea popupcontainer y tenga sus elementos directos
                For Each control In principalControls
                    If control.GetType().Name = "TextEdit" Then
                        If control.Properties.Mask IsNot Nothing AndAlso control?.Properties?.Mask?.editmask?.ToString?.ToLower?.Contains("c") Then
                            control.properties.Mask.Culture = _culture
                            control.Properties.Mask.EditMask = "c" + SessionValues.Instance.CurrencyNumbertFormat.CurrencyDecimalDigits.ToString()
                            If objMaskDisplay IsNot Nothing Then
                                control.Properties.Mask.UseMaskAsDisplayFormat = objMaskDisplay.useMaskAsDisplayFormat
                                control.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                                control.Properties.DisplayFormat.FormatString = "c" + objMaskDisplay?.decimals.ToString()
                            End If
                        End If
                    End If
                Next
            End If
        Next
    End Sub


    ''' <summary>
    ''' Reads the only controls.
    ''' </summary>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="layout">The layout.</param>
    Public Sub ReadOnlyControls(ByVal state As Boolean, ByVal layout As LayoutControl)
        If _dictionaryColumns Is Nothing Then
            _dictionaryColumns = New Dictionary(Of Control, List(Of Columns.GridColumn))()
        End If
        For Each c As Control In layout.Controls
            If c.ProductName = "DevExpress.XtraEditors" OrElse c.ProductName = "DevExpress.XtraGrid" Then
                If c.GetType().BaseType.GetProperty("Properties", Reflection.BindingFlags.InvokeMethod OrElse Reflection.BindingFlags.GetProperty) IsNot Nothing Then
                    CType(c, Object).Properties.ReadOnly = state
                ElseIf c.ProductName = "DevExpress.XtraGrid" Then
                    Dim _listColumn As New List(Of Columns.GridColumn)()
                    For Each col As Columns.GridColumn In CType(CType(c, Object).MainView, Object).Columns
                        If state Then
                            If col.OptionsColumn.AllowEdit Then
                                col.OptionsColumn.AllowEdit = False
                                _listColumn.Add(col)
                            End If
                        Else
                            If _dictionaryColumns.ContainsKey(c) Then
                                If _dictionaryColumns.Where(Function(x) x.Key.Equals(c)).Cast(Of KeyValuePair(Of Control, List(Of Columns.GridColumn)))() _
                                           .Select(Function(y) y.Value).Cast(Of List(Of Columns.GridColumn))().FirstOrDefault().FindAll(Function(z) z.Equals(col)).ToList().Count > 0 Then
                                    col.OptionsColumn.AllowEdit = True
                                End If
                            End If
                        End If
                    Next
                    If _listColumn.Count > 0 Then
                        _dictionaryColumns.Add(c, _listColumn)
                    End If
                    If Not state Then
                        If _dictionaryColumns.ContainsKey(c) Then
                            _dictionaryColumns.Remove(c)
                        End If
                    End If
                Else
                    CType(c, Object).Enabled = Not state
                End If
            End If
        Next
    End Sub

    Protected Function GetServerDate() As DateTime
        Return Me._model.GetDateServer
    End Function

    ''' <summary>
    ''' En estos eventos se lanza el evento para abrir una entidad por linea de comando
    ''' </summary>
    Public Sub OnIdEntityLoaded()
        If Me._idEntity IsNot Nothing AndAlso Not Me._idEntity.Trim().Equals(String.Empty) Then
            RaiseEvent IdEntityLoaded(Me, New EventArgs())
        End If
    End Sub

    ''' <summary>
    ''' Ocurre cuando el usuario actual elimina sus preferencias
    ''' </summary>
    Public Async Sub DeleteCustomizationControls()
        For Each c In Me.EnumerateComponents()
            If TypeOf c Is IndigoGridControl Then
                Await DirectCast(c, IndigoGridControl).RestoreDefaultLayoutsAsync()
            End If
            If TypeOf c Is IndigoSearchLookUpControl Then
                Await DirectCast(c, IndigoSearchLookUpControl).RestoreDefaultLayoutsAsync()
            End If
            If TypeOf c Is IndigoGridLookUpControl Then
                Await DirectCast(c, IndigoGridLookUpControl).RestoreDefaultLayoutsAsync()
            End If
            If TypeOf c Is IndigoPivotGridControl Then
                Await DirectCast(c, IndigoPivotGridControl).RestoreDefaultLayoutsAsync()
            End If
        Next
    End Sub

    ''' <summary>
    ''' Reindexa el formulario de forma asíncrona
    ''' </summary>
    ''' <param name="listDocuments">Lista de coumentos adjuntos al registro</param>
    Public Function ReindexFormAsync(Optional listDocuments As List(Of DocumentsStore) = Nothing) As Task
        Return Task.Factory.StartNew(Sub()
                                         ReindexForm(listDocuments)
                                     End Sub)
    End Function

    ''' <summary>
    ''' Reindexa el formulario
    ''' </summary>
    ''' <param name="listDocuments">Lista de coumentos adjuntos al registro</param>
    Public Sub ReindexForm(Optional listDocuments As List(Of DocumentsStore) = Nothing)
        If Not ApplicationSetting.Instance.LoginAzure Then
            If Me._funct IsNot Nothing Then
                Me._doc = Me._funct()
            End If
            If listDocuments IsNot Nothing AndAlso listDocuments.Count > 0 Then
                For Each document As DocumentsStore In listDocuments
                    If document.UseFormMetadata Then
                        Dim ResultDocIndexed = Me._model.SearchIndexingDocumentNormal(document.IdForm.ToString & "_" & document.Id.ToString)
                        Dim DocIndexed = ResultDocIndexed.Results.FirstOrDefault
                        If DocIndexed IsNot Nothing Then
                            If _doc IsNot Nothing Then
                                DocIndexed.Content = Me._doc.Content
                                DocIndexed.UpdateUser = Me._doc.UpdateUser
                                DocIndexed.Update = Me._doc.Update
                            End If
                            Me._model.SaveIndexingDocumentNormal(DocIndexed)
                        End If
                    End If
                Next
            End If
        Else
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Metodo para guardar el documento de indexación
    ''' </summary>
    Public Async Sub UpdateIndexedDocument(listDocuments As List(Of DocumentsStore))
        Try
            If Not ApplicationSetting.Instance.LoginAzure Then
                If Me._funct IsNot Nothing Then
                    Me._doc = Me._funct()
                    If Me._doc IsNot Nothing Then
                        Await Me._model.SaveIndexingDocument(Me._doc)
                    End If
                End If
                If listDocuments IsNot Nothing AndAlso listDocuments.Count > 0 Then
                    For Each document As DocumentsStore In listDocuments
                        If document.UseFormMetadata Then
                            Dim ResultDocIndexed = Await Me._model.SearchIndexingDocument(document.IdForm.ToString & "_" & document.Id.ToString)
                            Dim DocIndexed = ResultDocIndexed.Results.FirstOrDefault
                            If DocIndexed IsNot Nothing Then
                                If _doc IsNot Nothing Then
                                    DocIndexed.Content = Me._doc.Content
                                    DocIndexed.UpdateUser = Me._doc.UpdateUser
                                    DocIndexed.Update = Me._doc.Update
                                End If
                                Await Me._model.SaveIndexingDocument(DocIndexed)
                            End If
                        End If
                    Next
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' Metodo para obtener un documento indexado
    ''' </summary>
    Public Async Sub GetDocumentIndexed(TextSearch As String)
        Try
            If Not ApplicationSetting.Instance.LoginAzure Then
                If String.IsNullOrEmpty(SessionValues.Instance.VituelContainer) Then
                    Exit Sub
                End If

                Dim indexDocument As IndexedDocumentResultSet2 = Await Me._model.SearchIndexingDocument(TextSearch)
                Me._doc = indexDocument.Results.FirstOrDefault
                If Me._doc Is Nothing Then 'Se deindexa
                    Me.ReindexForm(Me.BarraBotones._listDocuments)
                    'Actualizamos el documento indexado para que al actualizar o realizar otra acción no guarde otro
                    indexDocument = Await Me._model.SearchIndexingDocument(TextSearch)
                    Me._doc = indexDocument.Results.FirstOrDefault
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para eliminar un documento indexado
    ''' </summary>
    Public Function DeleteDocumentIndexed() As Task
        If Not ApplicationSetting.Instance.LoginAzure Then
            'Return Nothing
            Return Task.FromResult(Of Task)(Nothing)

            Return Task.Factory.StartNew(Sub()
                                             Me._model.DeleteIndexingDocument(Me._doc)
                                         End Sub)
        Else
            'Return Nothing
            Return Task.FromResult(Of Task)(Nothing)
        End If

    End Function

    ''' <summary>
    ''' Propiedad que permite cambiar el mensaje 'Cargando' de la barra de progreso
    ''' </summary>
    Public WriteOnly Property ChangeMessageProgressBar As String
        Set(value As String)
            BarraBotones.ChangeMessageProgressBar = value
        End Set
    End Property

    ''' <summary>
    ''' Metodo para visualizar el control de carga
    ''' </summary>
    ''' <param name="State">Estado</param>
    Public Overridable Sub AsyncLoader(ByVal State As Boolean)
        If _isAsyncLoader <> State Then
            _isAsyncLoader = State
            BarraBotones.ProgressBar = State
            Me.ToolBars.Enabled = Not State
            For i = 0 To Me.INDPanelControlBase.Controls.Count - 1
                If Me.INDPanelControlBase.Controls(i).GetType Is GetType(LayoutControl) Then
                    If State Then
                        CType(Me.INDPanelControlBase.Controls(i), LayoutControl).BeginUpdate()
                    Else
                        CType(Me.INDPanelControlBase.Controls(i), LayoutControl).EndUpdate()
                    End If
                    Me.INDPanelControlBase.Controls(i).Enabled = Not State
                    If State = False Then
                        INDPanelControlBase.Controls(i).Focus()
                    End If
                    Exit For
                End If
            Next
        End If
    End Sub

    Public Overridable Sub AsyncLoaderOnlyBar(ByVal State As Boolean)
        If _isAsyncLoader <> State Then
            _isAsyncLoader = State
            BarraBotones.ProgressBar = State
            Me.ToolBars.Enabled = Not State
        End If
    End Sub


    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    Public Function GetDateServer() As DateTime
        Return Me._model.GetDateServer
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    Public Function IsHoliday(holidayDate As Date) As Boolean
        Dim holiday = Me._model.GetHoliday(holidayDate.AsDate)
        Return holiday.Id > 0
    End Function

    ''' <summary>
    ''' Metodo para preguntar si el formulario permite mostrar opción imprimir
    ''' </summary>
    ''' <param name="identity"></param>
    ''' <param name="idform"></param>
    ''' <param name="params"></param>
    Public Sub ShowPrintOption(evt As PrintReportAction, identity As Integer, idform As Integer, ParamArray params As Object())
        If MessageIndigo.Show(obtenerRecurso(ComunesGenerarReporte, Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._actionReport(evt, identity, idform, params)
        End If
    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Function ValidateControls() As Boolean
        Dim res = Me.LayoutControls.ValidateFields()
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Valida si un LayoutControlItem es visible
    ''' </summary>
    ''' <param name="Layouts"></param>
    ''' <returns></returns>
    Protected Function LayoutIsVisible(Layouts As List(Of LayoutControlItem)) As Boolean
        Return Layouts.Any(Function(a) a.Visible)
    End Function

    ''' <summary>
    ''' Muestra u oculta una lista de layouts control item
    ''' </summary>
    ''' <param name="listLayout"></param>
    ''' <param name="value"></param>
    Protected Sub ShowHideLayaouts(listLayout As List(Of LayoutControlItem), value As DevExpress.XtraLayout.Utils.LayoutVisibility)
        If listLayout?.Any() Then
            For Each layaout In listLayout
                layaout.Visibility = value
            Next
        End If
    End Sub

    ''' <summary>
    ''' Cuando se crea un formulario tipo popup con las dimensiones 1066 x 764, los equipos con
    ''' resolucion 1366 x 768 no muestran toda la ventana, en algunos casos generando que no se
    ''' vean botones en la parte baja de la ventana (frmPopupBills).
    ''' </summary>
    Protected Sub CheckScreenResolution()
        Dim intX As Integer = Screen.PrimaryScreen.Bounds.Width
        Dim intY As Integer = Screen.PrimaryScreen.Bounds.Height
        If intX = 1366 And intY = 768 Then
            Me.Size = New System.Drawing.Size(1066, 700)
        End If
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Ocurre cuando se ha recibido un Id de entidad y se hace necesario
    ''' consultarla por mandato del formulario base
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Event IdEntityLoaded(ByVal sender As Object, ByVal e As EventArgs)

#End Region

#Region "Handlers"
    ''' <summary>
    ''' Constructor ponemos el panel contenedor principal en False
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        Me.Opacity = 0
        Me._model = New MformBase
        Me._dicSequense = New Dictionary(Of Integer, List(Of String))()
        Dim isInDesignMode As Boolean = LicenseManager.UsageMode = LicenseUsageMode.Designtime
        If isInDesignMode = False Then
            FormSearchObjects = New FrmBusqueda()
            If Me.ViewModeEditHold = False Then
                Me.INDPanelControlBase.Visible = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Maneja los eventos clien en los botones Nuevo y Deshacer de la barra
    ''' para realizar el limpiado del id de la entidad
    ''' </summary>
    Private Sub CleanIdEntity() Handles BarraBotones.ClickDeshacer, BarraBotones.ClickNuevo
        Me._idEntity = String.Empty
        If FormSearchObjects IsNot Nothing Then
            FormSearchObjects.Cancelar()
        End If
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Aqui liberamos memoria de la aplicación
    ''' </summary>
    Private Sub FormBase_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Lanzamos la función para liberar memoria
        Me.FreeMemory()
    End Sub

    ''' <summary>
    ''' Evento load del formularioon
    ''' </summary>
    Private Sub FormBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If DesignMode = False Then
            'Lanzamos la función para liberar memoria
            Me.FreeMemory()
            Me.SetLayoutsExtended()
            Me.LayoutControls.LoadDefinitionAsync()
            BarraBotones.OperatingUnitValue = SessionValues.Instance.IndigoOperatingUnitId
            Me.ToolBars.Size = New Size(Me.ToolBars.Size.Width, 130)
            Me.Cursor = Cursors.Default
            Me.indigo = SessionValues.Instance
            If IdEntity Is Nothing OrElse IdEntity = String.Empty Then
                If ViewModeEditHold = False Then
                    If indigo.UserViewMode = True Then
                        If TypeOf Me Is ICrudBase Then
                            IsMainSearch = True
                            BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
                            Dim base As ICrudBase = Me
                            base.OpenSearch()
                        End If
                    End If
                End If
            End If

            Me.INDPanelControlBase.Visible = True
            VisibilityControls()
        End If
    End Sub

    ''' <summary>
    ''' Evento redimensionar de la barra botones
    ''' </summary>
    Private Sub ToolBars_SizeChanged(sender As Object, e As EventArgs) Handles ToolBars.SizeChanged
        If Me.ToolBars.Size.Height > 28 Then
            Me.ToolBars.Size = New Size(Me.ToolBars.Size.Width, 130)
            ' INDPanelControlBase.Size = New Size(Me.ToolBars.Size.Width, 98)
        End If
    End Sub

    ''' <summary>
    ''' Aqui se gestiona el maximizado y minimizado de la barra
    ''' </summary>
    Private Sub BarraBotones_ToolBarsMinimized(sender As Object, e As EventArgs) Handles BarraBotones.ToolBarsMinimized
        Me.ToolBars.Size = New Size(Me.ToolBars.Size.Width, 28)
        'INDPanelControlBase.Size = New Size(Me.ToolBars.Size.Width, 98)
    End Sub
    Private Sub BarraBotones_ToolBarsMaximized(sender As Object, e As EventArgs) Handles BarraBotones.ToolBarsMaximized
        Me.ToolBars.Size = New Size(Me.ToolBars.Size.Width, 130)
        ' INDPanelControlBase.Size = New Size(Me.ToolBars.Size.Width, 98)
    End Sub

    ''' <summary>
    ''' Metodo que se dispara cuando termina de cargar el formulario
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnShown(e As EventArgs)
        If DesignMode = False Then
            BarraBotones.ListOperatingUnit = SessionValues.Instance.ListOperatingUnitPermission
            RaiseEvent EventLoadOperatingUnit(BarraBotones.ListOperatingUnit)
        End If
        Me.Opacity = 100
        MyBase.OnShown(e)
    End Sub

    Protected Overrides Sub OnFormClosing(e As FormClosingEventArgs)
        If Not TokenManager.Instance.IsTokenExpired Then
            MyBase.OnFormClosing(e)
        End If
    End Sub

#End Region

End Class