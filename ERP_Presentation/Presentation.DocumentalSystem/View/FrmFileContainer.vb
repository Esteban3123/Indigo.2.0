Imports Presentation.DocumentalSystem.MPV
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.DocumentalSystem.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Controls
Imports Domain.Security.Entities
Imports System.ComponentModel


Public Class FrmFileContainer
    Implements IFileContainer

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _modelFileContainer As MFileContainer
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PFileContainer
    ''' <summary>
    ''' Variable de sessión
    ''' </summary>
    Private _indigoSessionValues As SessionValues
    ''' <summary>
    ''' Objeto contenedor de archivos
    ''' </summary>
    Private _fileContainer As FileContainer
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord
    ''' <summary>
    ''' Objeto tipo metadata
    ''' </summary>
    ''' <remarks></remarks>
    Dim objMetadata As Domain.DocumentalSystem.Entities.Metadata


#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para controlar el estado de los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFileContainer.ActionsOnControls
        Set(value As Boolean)
            Me.INDCodeTxt.Enabled = Not value
            Me.INDTxtName.Enabled = value
            Me.INDUseInformationRdg.Enabled = value

            INDlcgFormulario.Enabled = value
            INDlcgMetadata.Enabled = value

            If value = True Then
                Me.INDTxtName.Focus()
            Else
                Me.INDCodeTxt.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para mostrar mensajes al usuario
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(ByVal value As String)
            
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
    ''' propiedad que contiene el estado del contenedor de archivos
    ''' </summary>
    Public Property StatusResponsible As Boolean Implements IFileContainer.StatusResponsible
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para asignar los detalles de formularios por contenedor
    ''' </summary>
    ''' <value>Objeto</value>
    Public Property DataSourceForm As List(Of FileContainersForm) Implements IFileContainer.DataSourceForm
        Get
            Return Me.INDFormsGc.DataSource
        End Get
        Set(value As List(Of FileContainersForm))
            Me.INDFormsGc.DataSource = value
            Me.INDFormsGc.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Encapsula una Lista de Metadata
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceMetadata As List(Of Domain.DocumentalSystem.Entities.Metadata) Implements IFileContainer.DataSourceMetadata
        Get
            Return CType(INDgcFields.DataSource, List(Of Domain.DocumentalSystem.Entities.Metadata))
        End Get
        Set(value As List(Of Domain.DocumentalSystem.Entities.Metadata))
            Me.INDgcFields.DataSource = value
            Me.INDgcFields.RefreshDataSource()
        End Set
    End Property



#End Region

#Region "Load"
    Private Sub FrmFileContainer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me._modelFileContainer = New MFileContainer(Me.Tag)
        Me._indigoSessionValues = SessionValues.Instance

        Me._presenter = New PFileContainer(Me)
        Deshacer()
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True

        Me.loadForms()

        Dim List As New List(Of Tuple(Of String, String))
        Dim TText As Tuple(Of String, String) = New Tuple(Of String, String)("Texto", "Texto")
        Dim TLongText As Tuple(Of String, String) = New Tuple(Of String, String)("TextoLargo", "Texto Largo")
        Dim TDate As Tuple(Of String, String) = New Tuple(Of String, String)("Fecha", "Fecha")
        List.Add(TText)
        List.Add(TLongText)
        List.Add(TDate)
        INDgleTypeField.Properties.DataSource = List
        Me.ActionsOnControls = False
    End Sub
#End Region

#Region "Toolbar Events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
    End Sub
    ''' <summary>
    ''' Evento buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' Evento customizar
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub
    ''' <summary>
    ''' Evento eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub
    ''' <summary>
    ''' Evento guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub
    ''' <summary>
    ''' Evento nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Nuevo()
    End Sub
    ''' <summary>
    ''' Evento actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub
    ''' <summary>
    ''' Evento deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub
    ''' <summary>
    ''' Ejecuta la opcion de reestablecer la definicion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' Metodo base buscar
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        Me.AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' Metodo base deshacer
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        Me.CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub
    ''' <summary>
    ''' Metodo base nuevo
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        Me.CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub
    ''' <summary>
    ''' Metodo base logicaBotonActualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateMyControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        If _fileContainer.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged OrElse _fileContainer.FileContainersForm.ToList().Exists(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Or x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted) OrElse _fileContainer.Metadata.ToList().Exists(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Or x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted) Then
            AsyncLoader(True)
            Dim result = Await _modelFileContainer.SaveFileContainer(_fileContainer)
            AsyncLoader(False)
            If result.StateResult = True Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                CleanControls()
                Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = result.MessageResult(0)
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If _fileContainer IsNot Nothing Then
            If _fileContainer.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    Dim result = Await _modelFileContainer.DeleteFileContainer(_fileContainer)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        CleanControls()
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                    Else
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorDependencia, Comunes)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = result.MessageResult(0)
                        End If
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
            End If
        End If
    End Sub
    ''' <summary>
    ''' Metodo para abrir el formulario de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Id"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}, New ColumnInfo With {.Caption = "Usa Info. Formulario", .FieldName = "UseFormMetada"}}.ToList()
            .ValorSolicitado = "Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.FileContainerDocumentalSystem
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDCodeTxt.Text = ReturnValue
        If INDCodeTxt.Text <> String.Empty Then
            Me.DeleteBlockedRecord()
            LoadControls()
            If INDCodeTxt.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDCodeTxt.Enabled = False
        End If
    End Sub

#End Region

#Region "Methods and Functions"
    Private Sub loadForms()
        AsyncLoader(True)
        Dim ListFormERP As List(Of VieForm) = BaseClass.GetXmlWithAggregates(Of VieForm)(Base.eDataXml.XMLForms)
        Me.INDFormsGle.Properties.DataSource = ListFormERP
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = "Activo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = "Inactivo", .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        Me.INDCodeTxt.Text = String.Empty
        Me.INDTxtName.Text = String.Empty
        Me.INDUseInformationRdg.EditValue = Nothing
        Me.INDlcgMetadata.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDtxtNameMetadata.Text = String.Empty
        INDgleTypeField.Text = String.Empty
        INDtxtLengthField.Text = String.Empty

        Me.BarraBotones.StatusRecordVisible = False
        Me._fileContainer = Nothing
        Me.DataSourceForm = New List(Of FileContainersForm)
        Me.DataSourceMetadata = New List(Of Domain.DocumentalSystem.Entities.Metadata)
        Me.objMetadata = New Domain.DocumentalSystem.Entities.Metadata
        DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
    End Sub
    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Await Me._modelFileContainer.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        ActionsOnControls = True
        AsyncLoader(True)
        _fileContainer = Await Me._modelFileContainer.GetFileContainer(Me.INDCodeTxt.Text)
        AsyncLoader(False)
        If Not _fileContainer Is Nothing Then
            If _fileContainer.Id > 0 Then
                Dim result = Await _modelFileContainer.GetBlockRecord(Me.Tag, _fileContainer.Id)
                With _fileContainer
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                    Me.INDCodeTxt.Text = .Id
                    Me.INDTxtName.Text = .Name
                    Me.INDUseInformationRdg.EditValue = .UseFormMetada
                    StatusResponsible = .State
                    Me.DataSourceMetadata = .Metadata.ToList()
                    If .FileContainersForm.ToList().Count() > 0 Then
                        Parallel.ForEach(.FileContainersForm.ToList(), Sub(item As FileContainersForm)
                                                                           If Me.indigo.ListFormPermission.Where(Function(x) x.Id = item.IdForm).Count > 0 Then
                                                                               If Me.indigo.ListFormPermission.Where(Function(x) x.Id = item.IdForm).FirstOrDefault.Module IsNot Nothing Then
                                                                                   item.NameModule = Me.indigo.ListFormPermission.Where(Function(x) x.Id = item.IdForm).SingleOrDefault.Module.Name
                                                                               End If
                                                                           End If
                                                                       End Sub)
                        Me.DataSourceForm = .FileContainersForm.ToList()
                    End If
                End With

                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me._indigoSessionValues.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me._indigoSessionValues.UserIndigo, .IdRecord = _fileContainer.Id}
                    Dim operation = Await _modelFileContainer.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            Else
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            _fileContainer = New FileContainer
            INDCodeTxt.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
            Me.BarraBotones.StatusRecordVisible = True
            Me.BarraBotones.StatusRecord = Me.BarraBotones.States(0).StatusValue
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
    End Sub
    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateMyControls() As Boolean
        If INDCodeTxt.Text.Trim() = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciCode.Text)
            Return False
        End If
        If INDTxtName.Text.Trim() = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciName.Text)
            Return False
        End If
        If INDUseInformationRdg.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciUseInformation.Text)
            Return False
        End If
        If Me._fileContainer IsNot Nothing AndAlso Me._fileContainer.FileContainersForm.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos un formulario al archivador."
            Return False
        End If
        If Not CType(INDUseInformationRdg.EditValue, Boolean) AndAlso Me._fileContainer IsNot Nothing AndAlso Me._fileContainer.Metadata.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Si no usa la información del formulario, debe crear al menos un campo de MetaData para indexar la información."
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With _fileContainer
            '.Id = Me.INDCodeTxt.Text
            .Name = Me.INDTxtName.Text
            .UseFormMetada = Me.INDUseInformationRdg.EditValue
            .State = Me.BarraBotones.StatusRecord
        End With
    End Sub


    ''' <summary>
    ''' valida, asigna y crea un objeto metadata
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddItemMetadata()
        If ValidateControlsMetadata() = False Then
            Exit Sub
        End If
        Dim _Metadata = AssigningValuesMetadata()
        If _Metadata IsNot Nothing Then
            Dim index = Me.DataSourceMetadata.IndexOf(_Metadata)
            If index >= 0 Then
                'procedo a eliminar un item
                Me.DataSourceMetadata.Remove(_Metadata)
                _fileContainer.Metadata.Remove(_Metadata)
                'agrego el item actualizado en la misma posicion
                Me.DataSourceMetadata.Insert(index, _Metadata)
                _fileContainer.Metadata.Add(_Metadata)
            Else
                DataSourceMetadata.Add(_Metadata)
                _fileContainer.Metadata.Add(_Metadata)
            End If
            If Me._fileContainer.Id > 0 Then
                _fileContainer.MarkAsModified()
            End If
            Me.CleanControlsAdd()
        End If
        INDgcFields.RefreshDataSource()
        Me.INDtxtNameMetadata.Focus()
    End Sub

    ''' <summary>
    ''' metodo que limpia controles y objeto una ves agregado item a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdd()
        ActionsOnControls = True
        INDtxtNameMetadata.Text = String.Empty
        INDgleTypeField.EditValue = Nothing
        INDtxtLengthField.Text = String.Empty
        objMetadata = New Domain.DocumentalSystem.Entities.Metadata
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Function AssigningValuesMetadata() As Domain.DocumentalSystem.Entities.Metadata
        ' objMetadata = New Domain.DocumentalSystem.Entities.Metadata()
        With objMetadata
            .IdFileContainer = Me._fileContainer.Id
            .Name = Me.INDtxtNameMetadata.Text
            .DataType = Me.INDgleTypeField.EditValue
            .Long = Me.INDtxtLengthField.Text
        End With
        Return objMetadata
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsMetadata() As Boolean
        If INDtxtNameMetadata.Text.Trim() = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciNameMetadata.Text)
            Return False
        End If
        If INDgleTypeField.Text.Trim() = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciTypeField.Text)
            Return False
        End If
        If INDtxtLengthField.Text.Trim() = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlciLengthField.Text)
            Return False
        End If
        If _fileContainer.Metadata.Any(Function(m) m.Name.ToLower() = INDtxtNameMetadata.Text.Trim().ToLower() And m.DataType.ToLower() = INDgleTypeField.Text.Trim.ToLower()) Then
            Mensaje(EeventViewerImages.Advertencia) = "Ya existe un campo de metadata con el mismo nombre y tipo de dato."
            Return False
        End If
        Return True
    End Function


    ''' <summary>
    ''' Edita  Objeto metadata seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDataEdit()
        Me.objMetadata = CType(Me.INDgcvFields.GetRow(Me.INDgcvFields.FocusedRowHandle), Domain.DocumentalSystem.Entities.Metadata)
        If Me.objMetadata IsNot Nothing Then
            Me.INDtxtNameMetadata.Text = objMetadata.Name
            Me.INDgleTypeField.EditValue = objMetadata.DataType
            Me.INDtxtLengthField.Text = objMetadata.Long
            Me.INDtxtNameMetadata.Focus()
        End If
    End Sub


#End Region

#Region "Customización"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyFileContainer.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(_indigoSessionValues.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(_indigoSessionValues.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyFileContainer.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

#Region "Handlers"


    ''' <summary>
    ''' Evento para consultar un archivador
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDCodeTxt_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDCodeTxt.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDCodeTxt.Text.ToString.Trim()) Then
                LoadControls()
                If INDCodeTxt.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                INDCodeTxt.Enabled = False
            Else
                Me._fileContainer = New FileContainer()
                INDCodeTxt.Text = "Nuevo"
                ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                If INDCodeTxt.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                INDCodeTxt.Enabled = False
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento el boton de la INDCodeTxt
    ''' </summary>
    Private Sub INDCodeTxt_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDCodeTxt.ButtonClick
        Me.AbrirBusqueda()
    End Sub


    ''' <summary>
    ''' Elimina un objeto metadata de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnDelete_Click(sender As Object, e As EventArgs) Handles INDbtnDelete.Click
        Dim ObjMetadataTmp = CType(INDgcvFields.GetRow(INDgcvFields.FocusedRowHandle), Domain.DocumentalSystem.Entities.Metadata)
        If ObjMetadataTmp IsNot Nothing Then
            _fileContainer.Metadata.ToList().ForEach(Sub(item)
                                                         If item.Name.ToLower() = ObjMetadataTmp.Name.ToLower() AndAlso item.Long = ObjMetadataTmp.Long AndAlso item.DataType.ToLower() = ObjMetadataTmp.DataType.ToLower() Then
                                                             'If item.Id > 0 Then
                                                             '    item.MarkAsDeleted()
                                                             'End If
                                                             item.MarkAsDeleted()
                                                             If Me._fileContainer.Id > 0 Then
                                                                 _fileContainer.MarkAsModified()
                                                             End If
                                                             Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                                         End If
                                                     End Sub)


            Me.DataSourceMetadata.Remove(ObjMetadataTmp)
            Me.INDgcFields.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._fileContainer IsNot Nothing AndAlso Me._fileContainer.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me.INDCodeTxt.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDCodeTxt.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub
    ''' <summary>
    ''' elimina un formulario de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteDeleteForm_Click(sender As Object, e As EventArgs) Handles INDbteDeleteForm.Click
        Dim ObjFileContainerTmp = CType(INDFormsGv.GetRow(INDFormsGv.FocusedRowHandle), Domain.DocumentalSystem.Entities.FileContainersForm)
        If ObjFileContainerTmp IsNot Nothing Then
            _fileContainer.FileContainersForm.ToList().ForEach(Sub(item)
                                                                   If item.IdForm = ObjFileContainerTmp.IdForm Then
                                                                       'If item.Id > 0 Then
                                                                       '    item.MarkAsDeleted()
                                                                       'End If
                                                                       item.MarkAsDeleted()
                                                                       If Me._fileContainer.Id > 0 Then
                                                                           _fileContainer.MarkAsModified()
                                                                       End If
                                                                       Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                                                   End If
                                                               End Sub)


            Me.DataSourceForm.Remove(ObjFileContainerTmp)
            Me.INDFormsGc.RefreshDataSource()
        End If
    End Sub


    ''' <summary>
    ''' Agregar Item de metadata
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AddItemMetadata()
    End Sub

    ''' <summary>
    ''' Agrega un objeto Formulario archivo contenedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDAddFormSb_Click(sender As Object, e As EventArgs) Handles INDAddFormSb.Click
        If INDFormsGle.EditValue IsNot Nothing Then
            Dim ObjForm As New FileContainersForm
            Dim Form As VieForm = INDFormsGle.GetSelectedDataRow()
            With ObjForm
                .FormName = Form.Name
                .IdForm = Form.Id
                If Me.indigo.ListFormPermission.Where(Function(x) x.Id = Form.Id).Count > 0 Then
                    If Me.indigo.ListFormPermission.Where(Function(x) x.Id = Form.Id).SingleOrDefault?.Module Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = "No existe un modulo para este formulario."
                    End If
                    .NameModule = Me.indigo.ListFormPermission.Where(Function(x) x.Id = Form.Id).SingleOrDefault?.Module.Name
                End If

            End With
            If Me.DataSourceForm.Exists(Function(item) item.IdForm = ObjForm.IdForm) = False Then
                Me.DataSourceForm.Add(ObjForm)
                Me._fileContainer.FileContainersForm.Add(ObjForm)
                If Me._fileContainer.Id > 0 Then
                    Me._fileContainer.MarkAsModified()
                End If
                Me.INDFormsGc.RefreshDataSource()
                INDFormsGle.EditValue = Nothing
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ItemYaAgregado, Eform.Comunes)
            End If
            INDFormsGle.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Elimina un Formulario de la lista
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnEliminarForm_Click(sender As Object, e As EventArgs) Handles INDbtnEliminarForm.Click
        Dim ObjFileContainerTmp = CType(INDFormsGv.GetRow(INDFormsGv.FocusedRowHandle), Domain.DocumentalSystem.Entities.FileContainersForm)
        If ObjFileContainerTmp IsNot Nothing Then
            _fileContainer.FileContainersForm.ToList().ForEach(Sub(item)
                                                                   If item.Id = ObjFileContainerTmp.Id Then
                                                                       item.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                                                                   End If
                                                               End Sub)


            Me.DataSourceForm.Remove(ObjFileContainerTmp)
            Me.INDFormsGc.RefreshDataSource()
        End If
    End Sub
    ''' <summary>
    ''' Para Editar un objeto de la lista metadata
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnEdit_Click(sender As Object, e As EventArgs) Handles INDbtnEdit.Click
        If Me.INDgcvFields.FocusedRowHandle >= 0 Then
            LoadDataEdit()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se muestra u oculta el grupo de metadata dependiendo de la opción elegida
    ''' </summary>
    Private Sub INDUseInformationRdg_EditValueChanged(sender As Object, e As EventArgs) Handles INDUseInformationRdg.EditValueChanged
        If CType(INDUseInformationRdg.EditValue, Boolean) Then
            Me.INDlcgMetadata.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            Me.INDlcgMetadata.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

#End Region

End Class