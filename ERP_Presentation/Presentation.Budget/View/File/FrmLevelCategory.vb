'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 21-07-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

''' <summary>
''' Contiene la vista de el frontal fuentes de financiación
''' </summary>
''' <remarks></remarks>
Public Class FrmLevelCategory
    Implements ILevelCategory

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

#End Region

#Region "Globals"
    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' DataTable
    ''' </summary>
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim _Indigo As SessionValues

    ''' <summary>
    ''' Variable para controlar la entidad presente en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim LevelsCategory As List(Of LevelCategory)

    ''' <summary>
    ''' Contiene el modelo del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MLevelCategory

    ''' <summary>
    ''' Contiene el presentador de formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PLevelCategory

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Establece el listado de niveles de categoria a eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListToDelete As List(Of LevelCategory)

    ''' <summary>
    ''' Variable para almacenar el nivel de categoria seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim levelCategoryCurrent As LevelCategory

    ''' <summary>
    ''' bandera que establece si se guarda por primera vez o se actualiza
    ''' </summary>
    ''' <remarks></remarks>
    Dim SaveOrUpdate As ESaveOrUpdate

    ''' <summary>
    ''' contiene el numero de rubros existentes
    ''' </summary>
    ''' <remarks></remarks>
    Dim countCategory As Integer
#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad para controlar la accion que se hace sobre los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements ILevelCategory.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ILevelCategory.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
#End Region

#Region "Handles"
    ''' <summary>
    ''' limpia las variables cuando se cierra el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        _Indigo = Nothing
        LevelsCategory = Nothing
        model = Nothing
        presenter = Nothing
        blockRecord = Nothing
        ListToDelete = Nothing
        levelCategoryCurrent = Nothing
        SaveOrUpdate = Nothing
        countCategory = Nothing
    End Sub


    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLevelCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlyLevelCategory, True)
        '****Inicializar variables*****'
        'Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Dim ListActions As New List(Of eAcciones)

        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvLevelCategory, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvLevelCategory.Columns
            If col.Name = "colActions" Then
                col.Width = 250
            End If
        Next
        ListToDelete = New List(Of LevelCategory)
        'Me.Funct = AddressOf GenerateDoc
        presenter = New PLevelCategory(Me)
        presenter.LoadDefinitionLayout()
        'presenter.GetSequense()
        '******************************
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que abre el form de busqueda desde el click del boton buscar del control codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmLevelCategory_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Abre el formulario de Entidades Presupuestales en un pop-up
    ''' </summary>
    Private Sub INDSleBudgetEntity_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar 
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag
            Case "Remove"
                If INDgvLevelCategory.FocusedRowHandle = INDgvLevelCategory.RowCount - 1 Then
                    levelCategoryCurrent = INDgvLevelCategory.GetRow(INDgvLevelCategory.FocusedRowHandle)
                    If levelCategoryCurrent.ChangeTracker.State <> ObjectState.Added Then
                        ListToDelete.Add(levelCategoryCurrent)
                    End If
                    LevelsCategory.Remove(levelCategoryCurrent)
                    levelCategoryCurrent = Nothing
                    INDgcLevelCategory.RefreshDataSource()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ElimineRegistrosDeMayorAMenor", NAME_MODULE)
                End If
                
            Case "Edit"
                levelCategoryCurrent = INDgvLevelCategory.GetRow(INDgvLevelCategory.FocusedRowHandle)
                INDPopupButtonAddLevel.ShowPopup()
        End Select
    End Sub

    ''' <summary>
    ''' Cuando se presiona click en agregar el nuevo nivel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAddLevel_Click(sender As Object, e As EventArgs) Handles INDsbAddLevel.Click
        If INDtxtName.Text IsNot String.Empty AndAlso INDtxtLength.Text IsNot String.Empty AndAlso CInt(INDtxtLength.Text) > 0 Then
            Dim levelCategory As LevelCategory
            If levelCategoryCurrent IsNot Nothing Then
                levelCategory = LevelsCategory.Find(Function(x) x.Level = levelCategoryCurrent.Level)
                levelCategoryCurrent = Nothing
            Else
                levelCategory = New LevelCategory
                LevelsCategory.Add(levelCategory)
            End If
            levelCategory.Level = INDtxtLevel.Text
            levelCategory.Length = INDtxtLength.Text
            levelCategory.Name = INDtxtName.Text
            INDgcLevelCategory.RefreshDataSource()
            INDPopupButtonAddLevel.ClosePopup()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        End If
    End Sub

    ''' <summary>
    ''' Cuando se abre el pop up de agregar nivel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerEdit1_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDPopupButtonAddLevel.QueryPopUp
        CleanControlsAddLevel()
        If levelCategoryCurrent IsNot Nothing Then
            INDtxtLevel.Text = levelCategoryCurrent.Level
            INDtxtName.Text = levelCategoryCurrent.Name
            INDtxtLength.Text = levelCategoryCurrent.Length
            If countCategory > 0 OrElse countCategory = -1 Then
                INDtxtLength.Properties.ReadOnly = True
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SoloPuedeModificarNombre", NAME_MODULE)
            Else
                INDtxtLength.Properties.ReadOnly = False
            End If
        Else
            If countCategory > 0 OrElse countCategory = -1 Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("YaHayRubros", NAME_MODULE)
                Exit Sub
            End If
            If LevelsCategory IsNot Nothing AndAlso LevelsCategory.Count > 0 Then
                INDtxtLevel.Text = (LevelsCategory.Item(LevelsCategory.Count - 1).Level) + 1
            Else
                INDtxtLevel.Text = 1
            End If
        End If
        INDtxtName.Focus()
    End Sub

    ''' <summary>
    ''' Cuando se cierra el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPopUpAddLevel_CloseUp(sender As Object, e As EventArgs) Handles INDPopupButtonAddLevel.Closed
        CleanCurrent()
    End Sub
#End Region

#Region "Methods"
   
    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If
        AsyncLoader(True)
        Using Model As New MLevelCategory
            LevelsCategory = Await Model.ListLevelsCategoryAsync()
            INDgcLevelCategory.DataSource = LevelsCategory
            prepareToolBar()
        End Using
        Using modelCategory As New MBudgetItem
            countCategory = Await modelCategory.CountCategory
        End Using
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Prepara la barra de botones segun los niveles de rubros
    ''' </summary>
    ''' <remarks></remarks>
    Sub prepareToolBar()
        If LevelsCategory IsNot Nothing AndAlso LevelsCategory.Count > 0 Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            SaveOrUpdate = ESaveOrUpdate.Update
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            SaveOrUpdate = ESaveOrUpdate.Save
        End If
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        'Dim res = Me.LayoutControls.ValidateFields()
        'If res IsNot Nothing AndAlso res.Count > 0 Then
        '    Dim sb As New StringBuilder()
        '    For Each s As String In res
        '        sb.AppendLine(s)
        '    Next
        '    Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), sb.ToString())
        '    Return False
        'Else
        '    Return True
        'End If
        If LevelsCategory Is Nothing OrElse (LevelsCategory.Count + ListToDelete.Count) = 0 Then
            Return False
        Else
            'If LevelsCategory.FindAll(Function(x) x.ChangeTracker.State = ObjectState.Added Or x.ChangeTracker.State = ObjectState.Modified Or x.ChangeTracker.State = ObjectState.Deleted).Count = 0 Then
            '    Return False
            'End If
        End If
    End Function

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CleanControls() As Task
        ActionsOnControls = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        LevelsCategory = Nothing
        ListToDelete = New List(Of LevelCategory)
        Await LoadControls()
    End Function

    ''' <summary>
    ''' Limpia controles del pop up de agregar niveles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControlsAddLevel()
        INDtxtName.Text = String.Empty
        INDtxtLevel.Text = String.Empty
        INDtxtLength.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Limpiar la variable que contiene el registro seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanCurrent()
        If levelCategoryCurrent IsNot Nothing Then
            levelCategoryCurrent = Nothing
        End If
    End Sub

    ' ''' <summary>
    ' ''' Función para generar la data de indexación
    ' ''' </summary>
    'Private Function GenerateDoc() As IndexedDocument2
    '    Dim dateServer = Me.GetDateServer
    '    If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
    '        Me._doc = New IndexedDocument2 With { _
    '            .Content = String.Format(obtenerRecurso(Eresources.FrmLevelCategoryMetaData, Eform.InfoMetaData), Me.LevelCategory.Code, Me.LevelCategory.Name, INDGleClassification.Text), _
    '            .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
    '            .IdEntity =  "$#" & Me.Tag & "_" & Me.LevelCategory.Code & "#$", .IdForm = Me.Tag, _
    '            .Title = String.Format(obtenerRecurso(Eresources.FrmLevelCategoryMetaDataTitle, Eform.InfoMetaData), Me.LevelCategory.Code), _
    '            .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
    '        Return Me._doc
    '    Else
    '        Me._doc.Update = dateServer
    '        Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
    '        Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmLevelCategoryMetaData, Eform.InfoMetaData), Me.LevelCategory.Code, Me.LevelCategory.Name, INDGleClassification.Text)
    '        Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmLevelCategoryMetaDataTitle, Eform.InfoMetaData), Me.LevelCategory.Code)
    '        Return Me._doc
    '    End If
    'End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MLevelCategory
                Await Model.DeleteBlockRecord(blockRecord)
            End Using

            blockRecord = Nothing
        End If
    End Sub
#End Region

#Region "ICRUD BASE"
    ''' <summary>
    ''' Metodo para buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Metodo para deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Deshacer() Implements IcrudBase.Deshacer
        Await CleanControls()
        'If SearchMode = False Then
        '    'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Metodo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'If LevelCategory IsNot Nothing Then
        '    If LevelCategory.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Dim result As ActionResult
        '            Using model As New MLevelCategory
        '                result = Await Me.RunAsyncOperation(model.DeleteLevelCategoryAsync(LevelCategory))
        '            End Using

        '            If result.StateResult = True Then
        '                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '                Await Me.DeleteDocumentIndexed()
        '                SearchMode = False
        '                Deshacer()
        '            Else
        '                If result.MessageResult.Count > 0 Then
        '                    If result.MessageResult.Item(0).ToString = "c-0000" Then
        '                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
        '                    ElseIf result.MessageResult.Item(0).ToString = "-999" Then
        '                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorConcurrencia)
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                    End If
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                End If
        '            End If
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
        '    End If
        'Else
        'End If
    End Sub

    ''' <summary>
    ''' metodo para guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Dim result As ActionResult(Of List(Of LevelCategory))
        If ListToDelete.Count > 0 Then
            For Each item In ListToDelete
                item.MarkAsDeleted()
                LevelsCategory.Add(item)
            Next
        End If
        Try
            AsyncLoader(True)
            Using model As New MLevelCategory
                result = Await model.SaveLevelCategoryAsync(LevelsCategory)
            End Using
            If result.StateResult = True Then
                Me.LevelsCategory = result.ObjectEmbbeded
                If SaveOrUpdate = ESaveOrUpdate.Save Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf SaveOrUpdate = ESaveOrUpdate.Update Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
                AsyncLoader(False)
                Deshacer()
            Else
                AsyncLoader(False)
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    If result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End If
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Obsoleto
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad que establece los mensajes 
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' MEtodo Cuando se da click en boton nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        'NewLevelCategory()
    End Sub

    ''' <summary>
    ''' Metodo para abrir busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        'If BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
        '    Exit Sub
        'End If
        'FormSearchObjects = New FrmBusqueda
        'AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        'With FormSearchObjects
        '    .ListaColumnas = {New ColumnInfo With {.Caption = "Codigo", .FieldName = "Code"}, New ColumnInfo With {.Caption = "Descripción", .FieldName = "Name"}}.ToList()
        '    .ValorSolicitado = "Code"
        '    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListLevelCategory
        '    'BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    .FormParent = Me
        '    .ShowSearch()
        'End With
        'SearchMode = True
    End Sub

    ' ''' <summary>
    ' ''' Metodo para obtener el valor del formulario de busqueda
    ' ''' </summary>
    ' ''' <param name="ReturnValue"></param>
    ' ''' <param name="ReturnObject"></param>
    ' ''' <remarks></remarks>
    'Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
    '    DeleteBlockedRecord()
    '    INDBtnCode.Text = ReturnValue
    '    If INDBtnCode.Text <> String.Empty Then
    '        LoadControls()
    '        If INDBtnCode.Enabled = False Then
    '            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    '        End If
    '        INDBtnCode.Enabled = False
    '    End If
    'End Sub
#End Region

#Region "Eventos Barra Botones"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        'Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ' ''' <summary>
    ' ''' Barras the botones_ changue operating unit.
    ' ''' </summary>
    ' ''' <param name="operatingUnit">The operating unit.</param>
    'Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
    '    If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BudgetSequenceDetail IsNot Nothing Then
    '        If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
    '            Me._idOperativeUnit = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
    '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    '        Else
    '            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
    '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    '        End If
    '    End If
    'End Sub
#End Region
    
    
End Class

Enum ESaveOrUpdate
    Save = 1
    Update = 2
End Enum
