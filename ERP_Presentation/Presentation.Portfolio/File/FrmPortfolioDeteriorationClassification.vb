'***********************************************************************
' Assembly         : Presentacion.Portafolio
' Author           : Oscar Stiven Astudillo
' Created          : 2024-10-10
'
' Last Modified By : 
' Last Modified On : 
' Description      : Clase que representa el formulario de clasificación 
'                    de deterioro de cartera
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Portfolio.MVP
Imports Infrastructure.CrossCutting.Base
Imports ResourceManager = Infrastructure.CrossCutting.Resources.ResourceManager
Imports Domain.Base.Entities
Imports Domain.Portfolio.Model
#End Region

Public Class FrmPortfolioDeteriorationClassification
    Implements IPortfolioDeteriorationClassification, ICustomizableForm

#Region "Variables"

    ''' <summary>
    ''' Indica si el formulario está en modo búsqueda.
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Presentador de la clasificación de deterioro del portafolio.
    ''' </summary>
    Dim Presenter As PPortfolioDeteriorationClassification

    ''' <summary>
    ''' Entidad de clasificación de deterioro del portafolio.
    ''' </summary>
    Dim PortfolioDeteriorationClassification As PortfolioDeteriorationClassification

    ''' <summary>
    ''' Registro bloqueado para el portafolio.
    ''' </summary>
    Private record As BlockRecordPortfolio

    ''' <summary>
    ''' ID de la unidad operativa seleccionada.
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Nombre del módulo al que pertenece el formulario.
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

    ''' <summary>
    ''' Secuencia numérica del formulario.
    ''' </summary>
    Private _sequence As PortfolioSequence

    ''' <summary>
    ''' ID de la configuración de secuencia seleccionada.
    ''' </summary>
    Private _idCurrentSequence As Integer

    ''' <summary>
    ''' Lista de acciones para la rejilla de Aplicación de Deterioro
    ''' </summary>
    Private _actionsForDeteriorationApplication As New List(Of eAcciones)



    ''' <summary>
    ''' Datasource de las edades de cartera
    ''' </summary>
    Private _agesPortfolioDataSource As New List(Of AgesPortfolioDTO)

    ''' <summary>
    ''' Bandera para saber si se esta editando un detalle
    ''' </summary>
    Private _flagEditDetail As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el código de un concepto de notas.
    ''' </summary>
    Public Property Code As String Implements IPortfolioDeteriorationClassification.Code
        Get
            If INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Nombre del deterioro de cartera.
    ''' </summary>
    Private Property Name As String Implements IPortfolioDeteriorationClassification.Name
        Get
            Return INDTxtName.EditValue
        End Get
        Set(value As String)
            INDTxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripción del deterioro.
    ''' </summary>
    Private Property Description As String Implements IPortfolioDeteriorationClassification.Description
        Get
            Return INDMeDescription.EditValue
        End Get
        Set(value As String)
            INDMeDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de los registros de los conceptos de notas.
    ''' </summary>
    Public Property Status As Boolean Implements IPortfolioDeteriorationClassification.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = If(value, eActionsStatusRecords.Active, eActionsStatusRecords.Inactive)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario.
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IPortfolioDeteriorationClassification.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia numérica del formulario.
    ''' </summary>
    Public Property Sequence As PortfolioSequence Implements IPortfolioDeteriorationClassification.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Lista de los detalles de los porcentajes de deterioro de las edades de cartera
    ''' </summary>
    Public Property ListPortfolioDeteriorationClassificationDetails As List(Of PortfolioDeteriorationClassificationDetails) Implements IPortfolioDeteriorationClassification.ListPortfolioDeteriorationClassificationDetails
        Get
            Return CType(INDGcDeteriorationApplication.DataSource, List(Of PortfolioDeteriorationClassificationDetails))
        End Get
        Set(value As List(Of PortfolioDeteriorationClassificationDetails))
            INDGcDeteriorationApplication.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si la edad de cartera es la mínima, máxima o intermedia
    ''' </summary>
    ''' <returns></returns>
    Private Property _rangeType As Byte

    ''' <summary>
    ''' Id de la edad de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Property AgePortfolioId As Integer
        Get
            Return INDSleAgesPortfolio.EditValue
        End Get
        Set(value As Integer)
            Dim ageItem = _agesPortfolioDataSource.FirstOrDefault(Function(x) x.RangeType = value)
            If ageItem IsNot Nothing Then
                _rangeType = ageItem.RangeType
            End If
            INDSleAgesPortfolio.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Porcentaje de deterioro para la cartera en el libro Niif
    ''' </summary>
    ''' <returns></returns>
    Public Property PercentageNiif As Decimal
        Get
            Return INDSePercentageNiif.EditValue
        End Get
        Set(value As Decimal)
            INDSePercentageNiif.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Porcentaje de deterioro para la cartera en el libro Fiscal
    ''' </summary>
    ''' <returns></returns>
    Public Property PercentageFiscal As Decimal
        Get
            Return INDSePercentageFiscal.EditValue
        End Get
        Set(value As Decimal)
            INDSePercentageFiscal.EditValue = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Propiedad que retorna el control de diseño.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPortfolioDeteriorationClassification.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que permite habilitar o deshabilitar controles.
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioDeteriorationClassification.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDTxtName.Enabled = value
            INDMeDescription.Enabled = value
            LayoutControlItem2.Enabled = value
            INDLcRoot.EndUpdate()
            If value Then
                INDTxtName.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para mostrar mensajes con diferentes iconos.
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            Select Case Icono
                Case EeventViewerImages.Advertencia
                    MessageIndigo.Show(value, MessageType.Warning, Me.Text)
                Case EeventViewerImages.Informacion
                    MessageIndigo.Show(value, MessageType.Information, Me.Text)
                Case EeventViewerImages.MensajeError
                    MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End Select
        End Set
    End Property

    ''' <summary>
    ''' Método para realizar una búsqueda.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Guarda o actualiza el registro.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of PortfolioDeteriorationClassification) = Await Presenter.SavePortfolioDeteriorationClassificationAsync(Me.PortfolioDeteriorationClassification, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If PortfolioDeteriorationClassification.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                    End If
                End If

                Me.PortfolioDeteriorationClassification = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Deshacer()
            Else
                INDBtnCode.Enabled = False
            End If
            ShowMessage(result.StatusCode) = result.Message
        Catch ex As Exception
            INDBtnCode.Enabled = False
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Crea un nuevo registro.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence.IsManual Then
            Deshacer()
        Else
            Await NewPortfolioDeteriorationClassification()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles del formulario.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina un registro.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If PortfolioDeteriorationClassification IsNot Nothing AndAlso PortfolioDeteriorationClassification.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result As ActionResult = Await Presenter.DeletePortfolioDeteriorationClassificationAsync(PortfolioDeteriorationClassification)

                    If result.StatusCode = eStatusResult.SUCCESS Then
                        DeleteDocumentIndexed()
                        Deshacer()
                    Else
                        INDBtnCode.Enabled = False
                    End If

                    ShowMessage(result.StatusCode) = result.Message
                Catch ex As Exception
                    INDBtnCode.Enabled = False
                    Throw ex
                Finally
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Despliega un popup para buscar un registro.
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If Not BarraBotones.PermiteConsultar Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda()
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        With FormSearchObjects
            .ListaColumnas = {
                New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}
            }.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioDeteriorationClassification
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Handlers"

#Region "Click"
    ''' <summary>
    ''' Maneja el evento KeyDown del botón INDBtnCode. 
    ''' Permite realizar acciones cuando se presionan teclas específicas.
    ''' </summary>
    Private Async Function INDBtnCode_KeyDownAsync(sender As Object, e As KeyEventArgs) As Task Handles INDBtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Function
            End If
            If _sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica está configurada como manual, por favor digite un código."
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await NewPortfolioDeteriorationClassification()
                Else
                    LoadControls()
                End If
            End If
            ' Suprimir el comportamiento por defecto del Enter para prevenir el movimiento automático de foco
            e.SuppressKeyPress = True
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Function
#End Region

#Region "Bar Button"

    ''' <summary>
    ''' Carga permisos al iniciar la barra de botones.
    ''' </summary>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString())
    End Sub

    ''' <summary>
    ''' Maneja el evento de búsqueda de la barra de botones.
    ''' Se invoca tanto al hacer clic en el botón de buscar como al presionar INDBtnCode.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Maneja el evento de actualización de la barra de botones.
    ''' Guarda los cambios actuales.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Maneja el evento de carga del formulario.
    ''' Inicializa valores y carga datos necesarios.
    ''' </summary>
    Private Sub FrmPortfolioDeteriorationClassification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        _doc = Nothing
        _funct = AddressOf GenerateDoc
        indigo = SessionValues.Instance
        Presenter = New PPortfolioDeteriorationClassification(Me)
        SetAgesPortfolioDataSource()
        _actionsForDeteriorationApplication.Add(eAcciones.Edit)
        _actionsForDeteriorationApplication.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDGvDeteriorationApplication, _actionsForDeteriorationApplication)
        Presenter.GetSequence()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Método que obtiene las listas de edades de cartera
    ''' </summary>
    Private Async Sub SetAgesPortfolioDataSource()
        Dim settingPortfolio As SettingPortfolio
        Using model As New MSettingPortfolio(MyTag)
            settingPortfolio = Await model.GetSettingPortfolioByIdOperatingUnitAsync(_idOperativeUnit)
            Dim minimumAge As New AgesPortfolioDTO With {.Id = -1, .Name = settingPortfolio.NameMinimumAgeRange, .RangeType = eRangeType.MinimumAge}
            _agesPortfolioDataSource.Add(minimumAge)
        End Using
        Using model As New MAgesPortfolio(MyTag)
            Dim listAgesPortfolio As List(Of AgesPortfolio) = model.ListAgesPortfolioByIdSettingPortfolio(settingPortfolio.Id)
            For Each agePortfolio In listAgesPortfolio
                Dim age As New AgesPortfolioDTO With {.Id = agePortfolio.Id, .Name = agePortfolio.Name, .RangeType = eRangeType.IntermediateAge}
                _agesPortfolioDataSource.Add(age)
            Next
            Dim maximumAge As New AgesPortfolioDTO With {.Id = -2, .Name = settingPortfolio.NameMaximumAgeRange, .RangeType = eRangeType.MaximumAge}
            _agesPortfolioDataSource.Add(maximumAge)
        End Using
        INDSleAgesPortfolio.Properties.DataSource = _agesPortfolioDataSource
    End Sub

    ''' <summary>
    ''' Maneja el evento de guardado de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Maneja el evento de eliminación de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Maneja el evento de activar/desactivar un registro.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        ChangeState()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Maneja el evento de deshacer en la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Maneja logica de actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Funcion para limitar cantidad de caracteres en control memoEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDMeDescription_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDMeDescription.EditValueChanging
        If e.NewValue Is Nothing Then
            Return
        End If
        Dim maxLength As Integer = 500
        Dim edit As DevExpress.XtraEditors.MemoEdit = TryCast(sender, DevExpress.XtraEditors.MemoEdit)
        For Each str As String In edit.Lines
            If str.Length > maxLength Then
                e.Cancel = True
                Return
            End If
        Next str
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Genera un documento a indexar.
    ''' </summary>
    ''' <returns>Un objeto de tipo IndexedDocument2.</returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PortfolioDeteriorationClassification.Code, Me.PortfolioDeteriorationClassification.Name),
                .CreationDate = dateServer,
                .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.PortfolioDeteriorationClassification.Code & "#$",
                .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PortfolioDeteriorationClassification.Code),
                .Update = dateServer,
                .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            }
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PortfolioDeteriorationClassification.Code, Me.PortfolioDeteriorationClassification.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PortfolioDeteriorationClassification.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Asigna valores al objeto que se enviará.
    ''' </summary>
    Private Sub AssigningValues()
        With PortfolioDeteriorationClassification
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name
            .Description = Description
            MergePortfolioDeteriorationClassificationDetails(.PortfolioDeteriorationClassificationDetails)
        End With
    End Sub

    ''' <summary>
    ''' Método para actualizar o agregar detalles
    ''' </summary>
    ''' <param name="targetCollection"></param>
    Private Sub MergePortfolioDeteriorationClassificationDetails(targetCollection As ICollection(Of PortfolioDeteriorationClassificationDetails))
        If ListPortfolioDeteriorationClassificationDetails Is Nothing Then Return

        For Each detail In ListPortfolioDeteriorationClassificationDetails
            Dim existingDetail = targetCollection.FirstOrDefault(Function(x) x.Id = detail.Id AndAlso detail.Id > 0)
            If existingDetail IsNot Nothing Then
                ' Modificar detalle
                UpdatePortfolioDeteriorationClassificationDetail(existingDetail, detail)
                existingDetail.MarkAsModified()
            Else
                ' Agregar nuevo
                If detail.Id > 0 Then detail.MarkAsModified()
                targetCollection.Add(detail)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Método que actualiza el detalle existente para evitar agregar detalles duplicados
    ''' </summary>
    ''' <param name="target"></param>
    ''' <param name="source"></param>
    Private Sub UpdatePortfolioDeteriorationClassificationDetail(target As PortfolioDeteriorationClassificationDetails, source As PortfolioDeteriorationClassificationDetails)
        target.AgesPortfolioId = source.AgesPortfolioId
        target.RangeType = source.RangeType
        target.AgesPortfolioName = source.AgesPortfolioName
        target.DeteriorationNiifPercent = source.DeteriorationNiifPercent
        target.DeteriorationFiscalPercent = source.DeteriorationFiscalPercent
    End Sub

    ''' <summary>
    ''' Retorna el valor de la búsqueda de forma asíncrona.
    ''' </summary>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Not String.IsNullOrEmpty(Code) Then
            Await LoadControls()
            If Not INDBtnCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Carga el estado de los botones de la barra.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los controles de la interfaz de usuario de forma asíncrona.
    ''' </summary>
    Private Async Sub CleanControls()
        INDLcRoot.BeginUpdate()  ' Comienza la actualización del layout
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        Code = Nothing
        Name = Nothing
        Description = Nothing
        PortfolioDeteriorationClassification = Nothing
        ListPortfolioDeteriorationClassificationDetails = Nothing
        CleanPopupControls()
        ' Configura la barra de botones según el modo de vista del usuario
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDLcRoot.EndUpdate()
        Await DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Limpia los controles de los detalles
    ''' </summary>
    Private Sub CleanPopupControls()
        AgePortfolioId = 0
        PercentageNiif = 0
        PercentageFiscal = 0
        _flagEditDetail = False
    End Sub

    ''' <summary>
    ''' Crea una nueva clasificación de deterioro del portafolio.
    ''' </summary>
    Private Async Function NewPortfolioDeteriorationClassification() As Task
        PortfolioDeteriorationClassification = New PortfolioDeteriorationClassification() With {.Status = True}
        If _sequence.IsManual Then
            ActionsOnControls = True
            BarraBotones.PrepareToolbar(eAction.OnlySave)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If _sequence.Scope.Equals("O") Then
                _idCurrentSequence = _sequence.PortfolioSequenceDetail(0).Id
            ElseIf _sequence.Scope.Equals("OU") Then
                If _sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = _idOperativeUnit) Then
                    _idCurrentSequence = _sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = _idOperativeUnit).Id
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If _sequence.Sequential Then
                Code = ResourceManager.GetString("LabelOrTextboxNew")
                ActionsOnControls = True
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If DicSequense IsNot Nothing AndAlso DicSequense.Count > 0 Then
                    If DicSequense(CInt(_idCurrentSequence)).Count > 0 Then
                        Code = DicSequense(CInt(_idCurrentSequence))(0)
                        ActionsOnControls = True
                        BarraBotones.PrepareToolbar(eAction.OnlySave)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Tag))
                            DicSequense(CInt(_idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(_idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If DicSequense(CInt(_idCurrentSequence)) IsNot Nothing AndAlso DicSequense(CInt(_idCurrentSequence)).Count > 0 Then
                            Code = DicSequense(CInt(_idCurrentSequence))(0)
                            ActionsOnControls = True
                            BarraBotones.PrepareToolbar(eAction.OnlySave)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Code = ResourceManager.GetString("LabelOrTextboxNew")
                    ActionsOnControls = True
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado si existe.
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Carga los controles con datos del registro de forma asíncrona.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                AsyncLoader(True)
                Dim resultOperation = Await Presenter.GetPortfolioDeteriorationClassificationByCode(Code)
                INDLcRoot.BeginUpdate()
                PortfolioDeteriorationClassification = resultOperation.ObjectEmbbeded
                If PortfolioDeteriorationClassification IsNot Nothing AndAlso PortfolioDeteriorationClassification.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True

                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(PortfolioDeteriorationClassification.Id))
                        With PortfolioDeteriorationClassification
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)  ' Carga los campos personalizados
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Code = .Code
                            Name = .Name
                            Description = .Description
                            ListPortfolioDeteriorationClassificationDetails = .PortfolioDeteriorationClassificationDetails.ToList()
                            Status = .Status
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & PortfolioDeteriorationClassification.Code)
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = PortfolioDeteriorationClassification.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(PortfolioDeteriorationClassification.Id, Me.Tag.ToString(), Nothing, GetType(PortfolioDeteriorationClassification).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewPortfolioDeteriorationClassification
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDBtnCode.Focus()
                    End If
                End If
                INDLcRoot.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad de forma asíncrona.
    ''' </summary>
    Private Async Function ChangeState() As Task
        If PortfolioDeteriorationClassification IsNot Nothing Then
            Try
                AsyncLoader(True)
                Dim state As Boolean = Not PortfolioDeteriorationClassification.Status
                Dim result As ActionResult(Of PortfolioDeteriorationClassification) = Await Presenter.ChangeStatePortfolioDeteriorationClassification(Me.PortfolioDeteriorationClassification, state)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    PortfolioDeteriorationClassification = result.ObjectEmbbeded
                    UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Else
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Evento click de los botones de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim button As DevExpress.XtraEditors.SimpleButton = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case button.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Evento para manejar las acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Método para editar detalles
    ''' </summary>
    Private Sub EditDetail()
        Try
            ' Obtener el detalle seleccionado de la grilla
            Dim selectedDetail = CType(INDGvDeteriorationApplication.GetFocusedRow(), PortfolioDeteriorationClassificationDetails)

            If selectedDetail IsNot Nothing Then
                ' Establecer la bandera de edición
                _flagEditDetail = True

                ' Cargar los valores del detalle seleccionado en los controles
                ' Si AgesPortfolioId es Nothing, buscar por RangeType
                If selectedDetail.AgesPortfolioId.HasValue Then
                    AgePortfolioId = selectedDetail.AgesPortfolioId.Value
                Else
                    ' Buscar el ID por RangeType cuando AgesPortfolioId es nulo
                    Dim ageItem = _agesPortfolioDataSource.FirstOrDefault(Function(x) x.RangeType = selectedDetail.RangeType)
                    If ageItem IsNot Nothing Then
                        AgePortfolioId = ageItem.Id
                    End If
                End If

                ' Cargar los porcentajes
                PercentageNiif = selectedDetail.DeteriorationNiifPercent
                PercentageFiscal = selectedDetail.DeteriorationFiscalPercent

                ' Enfocar el popup de aplicación de deterioro de edad de cartera
                INDPceAddDeteriorationApplication.Focus()
                INDPceAddDeteriorationApplication.ShowPopup()
                INDSleAgesPortfolio.Focus()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un detalle para editar"
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al cargar el detalle para edición: " & ex.Message
            _flagEditDetail = False
        End Try
    End Sub

    ''' <summary>
    ''' Método para eliminar detalles
    ''' </summary>
    Private Sub DeleteDetail()
        Try
            ' Obtener el detalle seleccionado de la grilla
            Dim selectedDetail = CType(INDGvDeteriorationApplication.GetFocusedRow(), PortfolioDeteriorationClassificationDetails)

            If selectedDetail IsNot Nothing Then
                ' Confirmar la eliminación
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                    ' Obtener la lista actual
                    Dim currentList As List(Of PortfolioDeteriorationClassificationDetails) = ListPortfolioDeteriorationClassificationDetails

                    If currentList IsNot Nothing Then
                        ' Si el objeto tiene un ID (ya existe en BD), marcarlo como eliminado
                        If selectedDetail.Id > 0 Then
                            ' Marcar el ChangeTracker como eliminado en lugar de removerlo de la lista
                            selectedDetail.MarkAsDeleted()
                        End If
                        currentList.Remove(selectedDetail)

                        ' Actualizar el DataSource de la grilla
                        INDGcDeteriorationApplication.DataSource = Nothing
                        INDGcDeteriorationApplication.DataSource = currentList
                        INDGcDeteriorationApplication.RefreshDataSource()

                        Mensaje(EeventViewerImages.Informacion) = "Detalle eliminado correctamente"
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un detalle para eliminar"
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al eliminar el detalle: " & ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Botón para agregar o modificar detalles de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        Try
            ' Validar que se haya seleccionado una edad de cartera
            If AgePortfolioId = 0 OrElse AgePortfolioId = Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Edad de Cartera"
                INDSleAgesPortfolio.Focus()
                Exit Sub
            End If

            ' Validar que los porcentajes sean válidos
            If PercentageNiif < 0 OrElse PercentageNiif > 100 Then
                Mensaje(EeventViewerImages.Advertencia) = "El porcentaje NIIF debe estar entre 0 y 100"
                INDSePercentageNiif.Focus()
                Exit Sub
            End If

            If PercentageFiscal < 0 OrElse PercentageFiscal > 100 Then
                Mensaje(EeventViewerImages.Advertencia) = "El porcentaje Fiscal debe estar entre 0 y 100"
                INDSePercentageFiscal.Focus()
                Exit Sub
            End If

            ' Obtener la lista actual de detalles
            Dim currentList As List(Of PortfolioDeteriorationClassificationDetails) = ListPortfolioDeteriorationClassificationDetails
            If currentList Is Nothing Then
                currentList = New List(Of PortfolioDeteriorationClassificationDetails)
            End If

            ' Verificar si estamos en modo edición
            If _flagEditDetail Then
                ' Modo edición: modificar el objeto existente
                Dim selectedDetail = CType(INDGvDeteriorationApplication.GetFocusedRow(), PortfolioDeteriorationClassificationDetails)

                If selectedDetail IsNot Nothing Then
                    ' Buscar el objeto correspondiente de PortfolioAges
                    Dim selectedAge = _agesPortfolioDataSource.FirstOrDefault(Function(x) x.Id = AgePortfolioId)

                    If selectedAge IsNot Nothing Then
                        ' Actualizar las propiedades del objeto existente
                        selectedDetail.AgesPortfolioId = If(AgePortfolioId < 0, Nothing, CType(AgePortfolioId, Integer?))
                        selectedDetail.AgesPortfolioName = selectedAge.Name
                        selectedDetail.RangeType = selectedAge.RangeType
                        selectedDetail.DeteriorationNiifPercent = PercentageNiif
                        selectedDetail.DeteriorationFiscalPercent = PercentageFiscal

                        ' Si el objeto tiene Id, marcarlo como modificado
                        If selectedDetail.Id > 0 Then
                            selectedDetail.MarkAsModified()
                        End If
                    End If

                    ' Resetear la bandera de edición
                    _flagEditDetail = False
                End If
            Else
                ' Modo creación: crear un nuevo objeto
                ' Verificar que no exista ya un detalle con la misma edad de cartera
                Dim existingDetail As PortfolioDeteriorationClassificationDetails
                If AgePortfolioId > 0 Then
                    existingDetail = currentList.FirstOrDefault(Function(x) If(x.AgesPortfolioId, 0) = AgePortfolioId)
                Else
                    If AgePortfolioId = -1 Then
                        existingDetail = currentList.FirstOrDefault(Function(x) x.RangeType = eRangeType.MinimumAge)
                    Else
                        existingDetail = currentList.FirstOrDefault(Function(x) x.RangeType = eRangeType.MaximumAge)
                    End If
                End If

                If existingDetail IsNot Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya existe un detalle para esta Edad de Cartera"
                    Exit Sub
                End If

                ' Buscar el objeto correspondiente de PortfolioAges
                Dim selectedAge = _agesPortfolioDataSource.FirstOrDefault(Function(x) x.Id = AgePortfolioId)

                If selectedAge IsNot Nothing Then
                    ' Crear nuevo objeto PortfolioDeteriorationClassificationDetails
                    Dim newDetail As New PortfolioDeteriorationClassificationDetails()

                    With newDetail
                        .PortfolioDeteriorationClassificationId = PortfolioDeteriorationClassification.Id
                        ' Para IDs negativos, el AgesPortfolioId es nulo (se obtiene info del RangeType)
                        .AgesPortfolioId = If(AgePortfolioId < 0, Nothing, CType(AgePortfolioId, Integer?))
                        .AgesPortfolioName = selectedAge.Name
                        .RangeType = selectedAge.RangeType
                        .DeteriorationNiifPercent = PercentageNiif
                        .DeteriorationFiscalPercent = PercentageFiscal
                    End With

                    ' Agregar a la lista
                    currentList.Add(newDetail)
                End If
            End If

            ' Actualizar el DataSource de la grilla
            INDGcDeteriorationApplication.DataSource = Nothing
            INDGcDeteriorationApplication.DataSource = currentList
            INDGcDeteriorationApplication.RefreshDataSource()

            ' Limpiar los controles
            CleanPopupControls()

            ' Enfocar el control de edad de cartera para siguiente entrada
            INDSleAgesPortfolio.Focus()

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = "Error al agregar/modificar el detalle: " & ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Evento al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceAddDeteriorationApplication_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceAddDeteriorationApplication.CloseUp
        If _flagEditDetail Then
            CleanPopupControls()
        End If
    End Sub

#End Region


End Class
