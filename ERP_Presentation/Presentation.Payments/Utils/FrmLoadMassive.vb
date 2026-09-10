#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Payments.MVP

#End Region

Public Class FrmLoadMassive
    Implements ILoadMassive, ICustomizableForm

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    ''' <summary>
    ''' Variable que representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PLoadMassive

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim NatureType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la entidad de dependencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim LoadMassive As LoadMassive

    ''' <summary>
    ''' lista de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListLoadMassiveAccountPayable As List(Of LoadMassiveAccountPayable)

    ''' <summary>
    ''' lista de detalle de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListLoadMassiveAccountPayableDetail As List(Of LoadMassiveAccountPayableDetail)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements ILoadMassive.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As PaymentsSecuence Implements ILoadMassive.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As PaymentsSecuence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequense.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ILoadMassive.ActionsOnControls
        Set(value As Boolean)
            INDlycLoadMassive.BeginUpdate()

            INDtxtCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDMeObservation.Enabled = value
            INDEsbLoadMassive.Enabled = value
            INDBtnImportFile.Enabled = value
            INDgcBills.Enabled = value

            INDlycLoadMassive.EndUpdate()
            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDtxtCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que asigna el mensaje
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
    ''' Obtiene o establece el Codigo de la Carga Masiva de Cuentas por Pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ILoadMassive.Code
        Get
            If INDtxtCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDtxtCode.Text
            End If
        End Get
        Set(value As String)
            INDtxtCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements ILoadMassive.DocumentDate
        Get
            Return CDate(INDdteDocumentDate.EditValue)
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el detalle de la carga masiva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements ILoadMassive.Observations
        Get
            Return INDMeObservation.EditValue
        End Get
        Set(value As String)
            INDMeObservation.EditValue = value
        End Set
    End Property

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento que se dispara la presionar click en la barra de botones en buscar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barra botones: Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de actualizar confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
    End Sub

    ''' <summary>
    ''' Barra botones: Click anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
    End Sub

    ''' <summary>
    ''' Aqui se captura la unidad operativa seleccionada
    ''' </summary>
    ''' <param name="operatingUnit">Unidad operativa seleccionada</param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._operativeUnitId Then
            Me._operativeUnitId = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequense.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

#End Region

#Region "Crud"

    Public Sub Buscar() Implements IcrudBase.Buscar
    End Sub

    ''' <summary>
    ''' Abre el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If

        Dim ListItems As New List(Of Tuple(Of String, Byte))
        ListItems.Add(New Tuple(Of String, Byte)("Sin Confirmar", 1))
        ListItems.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        ListItems.Add(New Tuple(Of String, Byte)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code"},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate"},
                              New ColumnInfo() With {.Caption = "Observación", .FieldName = "Observations"}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListLoadMassive
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Método: Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If _sequense Is Nothing OrElse _sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If

        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewLoadMassive()
        End If
    End Sub

    ''' <summary>
    ''' Método: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Metodo: Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Anular()
    End Sub

    ''' <summary>
    ''' Metodo: Eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLoadMassive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycAccountPayable, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PLoadMassive(Me)
        Presenter.GetSequense()
        Me._operativeUnitId = Me.BarraBotones.OperatingUnitValue

        IndigoGridView1.MoreInfoColunmns(viewBill)
        IndigoGridControl1.RefreshGrid(INDgcBills)

        INDEsbLoadMassive.AddExcelSheets(New List(Of ExcelSheet) From {
            New ExcelSheet With {
                .Name = "Cabecera",
                .Columns = New List(Of ExcelColumn) From
                {
                    New ExcelColumn With {.Name = "Id"},
                    New ExcelColumn With {.Name = "Nit Tercero"},
                    New ExcelColumn With {.Name = "Linea Distribución"},
                    New ExcelColumn With {.Name = "Centro Costo"},
                    New ExcelColumn With {.Name = "No. Factura"},
                    New ExcelColumn With {.Name = "Maneja documento soporte"},
                    New ExcelColumn With {.Name = "Fecha Factura"},
                    New ExcelColumn With {.Name = "Fecha Documento"},
                    New ExcelColumn With {.Name = "Unidad Radicación"},
                    New ExcelColumn With {.Name = "Tipo Proveedor"},
                    New ExcelColumn With {.Name = "Plazo"},
                    New ExcelColumn With {.Name = "Valor Factura"},
                    New ExcelColumn With {.Name = "Observacion"}
                }
            },
            New ExcelSheet With {
                .Name = "Detalle",
                .Columns = New List(Of ExcelColumn) From
                {
                    New ExcelColumn With {.Name = "Id Cabecera"},
                    New ExcelColumn With {.Name = "Concepto"},
                    New ExcelColumn With {.Name = "Nit Tercero"},
                    New ExcelColumn With {.Name = "Centro Costo"},
                    New ExcelColumn With {.Name = "Naturaleza"},
                    New ExcelColumn With {.Name = "Valor"},
                    New ExcelColumn With {.Name = "Concepto Retención"},
                    New ExcelColumn With {.Name = "Valor Base"},
                    New ExcelColumn With {.Name = "Detalle"}
                }
            }
        })

        InitializeTuples()
        LoadStatus()
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _operativeUnitId = Nothing
        NatureType = Nothing
        LoadMassive = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLoadMassive_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If Code = String.Empty Then
            INDtxtCode.Focus()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If

            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewLoadMassive()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDtxtCode.ButtonClick
        OpenSearch()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de repositorio para ver los conceptos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepPceConcepts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceConcepts.QueryPopUp
        Dim _ap As LoadMassiveAccountPayable = CType(viewBill.GetFocusedRow, LoadMassiveAccountPayable)
        If _ap.LoadMassiveAccountPayableDetail.Count > 0 Then
            INDgcDetailConcepts.DataSource = Nothing
            INDgcDetailConcepts.DataSource = _ap.LoadMassiveAccountPayableDetail.ToList
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa las tuplas
    ''' </summary>
    Private Sub InitializeTuples()
        NatureType = New List(Of Tuple(Of Integer, String))
        NatureType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        NatureType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDrepSleNature.DataSource = NatureType.ToList()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDtxtCode.Text = ReturnValue
        If INDtxtCode.Text <> String.Empty Then
            Await LoadControls()
            If INDtxtCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDtxtCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                AsyncLoader(True)
                INDlycLoadMassive.BeginUpdate()
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MLoadMassive(CStr(Me.Tag))
                    LoadMassive = Await Model.GetLoadMassive(Code)
                    If LoadMassive IsNot Nothing AndAlso LoadMassive.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(LoadMassive.Id))
                            With LoadMassive
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Observations = .Observations
                                Me.BarraBotones.StatusRecord = .Status.ToString()

                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)

                                ListLoadMassiveAccountPayable = LoadMassive.LoadMassiveAccountPayable.ToList()
                                INDgcBills.DataSource = Nothing
                                INDgcBills.DataSource = ListLoadMassiveAccountPayable
                            End With

                            Me.GetDocumentIndexed(Me.Tag & "_" & LoadMassive.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = LoadMassive.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(LoadMassive.Id, Me.Tag.ToString(), Nothing, GetType(InitialBalance).Name)
                            ActionsOnControls = True
                            ReadOnlyControls(True)
                        End Using
                    Else
                        If Me._sequense.IsManual Then
                            Await Me.NewLoadMassive()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDtxtCode.Focus()
                        End If
                    End If
                End Using
            Catch ex As Exception
                INDtxtCode.Enabled = False
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            Finally
                AsyncLoader(False)
                INDlycLoadMassive.EndUpdate()
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que llena la entidad con los valores de los controles
    ''' </summary>
    Private Sub AssigningValues()
        With Me.LoadMassive
            .CustomProperties = LayoutControls.GetCustomFieldsValue()

            .OperatingUnitId = Me._operativeUnitId
            .Code = Me.Code
            .DocumentDate = Me.DocumentDate
            .Observations = Me.Observations
        End With
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlycLoadMassive.BeginUpdate()

        ReadOnlyControls(False)
        ActionsOnControls = False

        Code = String.Empty
        DocumentDate = Nothing
        Observations = Nothing
        INDgcBills.DataSource = Nothing

        Me._doc = Nothing
        DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        AdditionalControlPanel.Controls.Clear()
        Me.BarraBotones.StatusRecordVisible = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycLoadMassive.EndUpdate()
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para
    ''' crear una nueva cuenta por pagar
    ''' </summary>
    Private Async Function NewLoadMassive() As Task
        LoadMassive = New LoadMassive() With {.Status = 1}
        ListLoadMassiveAccountPayable = New List(Of LoadMassiveAccountPayable)
        Me.BarraBotones.StatusRecordVisible = True
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._operativeUnitId).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

        Using model As New MLoadMassive(CStr(Tag))
            INDdteDocumentDate.EditValue = Await model.GetServerDate()
        End Using
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.LoadMassive.Code, Me.LoadMassive.Observations),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.LoadMassive.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.LoadMassive.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.LoadMassive.Code, Me.LoadMassive.Observations)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.LoadMassive.Code)
            Return Me._doc
        End If
    End Function

#Region "Import File"

#Region "propiedades de la importacion"

    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de importar archivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim progress As CtrProgress

    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalItems As Integer

    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer = 0

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rowDetails As RowCollection

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDetails As List(Of LoadMassiveAccountPayableDetail)

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrosImportFile As List(Of String)

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 100

#End Region

    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        If LoadMassive Is Nothing OrElse LoadMassive.Status <> 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento no se encuentra en estado registrado"
            Exit Sub
        End If

        If ValidateControls() = False Then
            Exit Sub
        End If

        AssigningValues()
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    If MessageIndigo.Show("Desea continuar con el proceso de importación", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        listErrosImportFile = New List(Of String)

                        Await Me.LoadImportFile(myStream)

                        If LoadMassive.Id > 0 Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format("Se confirmo el documento con el código {0} con {1} facturas guardadas", LoadMassive.Code, ListLoadMassiveAccountPayable.Count)
                        End If

                        'si existen errores informamos al usurio y el proceso no continua
                        If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                            Mensaje(EeventViewerImages.MensajeError) = "El archivo presento los siguientes errores"
                            Using formulario As New FrmListErrors(listErrosImportFile)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                transparent.SafeInvoke(Sub(f) f.ShowDialog())
                            End Using
                        End If
                    End If
                End If
                AsyncLoader(False)
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message

                INDgcBills.DataSource = Nothing
                INDgcBills.DataSource = ListLoadMassiveAccountPayable

                totalItems = 0
                totalProcessedItems = 0
                progress.PrintInfo()
                AdditionalControlPanel.Controls.Clear()
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' metodo que se encarga de validar el archivo de excel
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LoadImportFile(ByVal fileName As String) As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim sddf = New SpreadsheetControl()
                                         sddf.AllowDrop = False
                                         sddf.LoadDocument(myStream)
                                         Dim workBook As IWorkbook = sddf.Document

                                         If workBook.Worksheets.Count <> 2 Then
                                             listErrosImportFile.Add("El archivo debe tener dos hojas (Cabecera - Detalle)")
                                             Exit Sub
                                         End If

                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex = 0 Then
                                             listErrosImportFile.Add("No se encontraron registros de cabecera en el archivo")
                                             Exit Sub
                                         End If

                                         rowDetails = workBook.Worksheets(1).Rows
                                         'If rowDetails.LastUsedIndex = 0 Then
                                         '    listErrosImportFile.Add("No se encontraron registros de detalle en el archivo")
                                         '    Exit Sub
                                         'End If

                                         ListLoadMassiveAccountPayableDetail = New List(Of LoadMassiveAccountPayableDetail)
                                         Me.SetListLoadMassiveAccountPayableDetail()

                                         totalProcessedItems = 0
                                         totalItems = rows.LastUsedIndex

                                         progress = New CtrProgress
                                         progress.SetInfoFunction(AddressOf getInfo)
                                         progress.PrintInfo()
                                         progress.Dock = DockStyle.Fill
                                         AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
                                         AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))
                                         progress.SafeInvoke(Sub(x)
                                                                 x.SetTitle = "Registros Procesados"
                                                                 x.PrintInfo()
                                                             End Sub)

                                         Dim lastProcess As Boolean = False
                                         Using trasparent = New FrmTransparent(Nothing, False)
                                             trasparent.SafeInvoke(Sub(f) f.ShowDialog())
                                             Using Model As New MLoadMassive(MyTag)
                                                 Dim indexSend As Integer = 1
                                                 Dim positionEnd As Integer = 0
                                                 While indexSend <= rows.LastUsedIndex
                                                     Try
                                                         positionEnd = indexSend + itemsSend - 1
                                                         listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                                         listDetails = New List(Of LoadMassiveAccountPayableDetail)
                                                         lastProcess = (positionEnd >= rows.LastUsedIndex)

                                                         If Not lastProcess Then
                                                             SetRow(indexSend, positionEnd + 1)
                                                         Else
                                                             SetRow(indexSend, rows.LastUsedIndex + 1)
                                                             positionEnd = rows.LastUsedIndex
                                                         End If

                                                         'ListLoadMassiveAccountPayableDetail.RemoveRange(listDetails)
                                                         Dim result As ActionResult(Of LoadMassive) = Model.SaveLoadMassive(LoadMassive, listRows.ToList(), listDetails, lastProcess)

                                                         If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Id > 0 Then
                                                             LoadMassive = result.ObjectEmbbeded
                                                             progress.SafeInvoke(Sub(x)
                                                                                     Code = LoadMassive.Code
                                                                                     DocumentDate = LoadMassive.DocumentDate
                                                                                     Me.BarraBotones.StatusRecord = LoadMassive.Status.ToString()
                                                                                 End Sub)

                                                             If LoadMassive.LoadMassiveAccountPayable IsNot Nothing AndAlso LoadMassive.LoadMassiveAccountPayable.Count > 0 Then
                                                                 ListLoadMassiveAccountPayable.AddRange(LoadMassive.LoadMassiveAccountPayable)
                                                                 progress.SafeInvoke(Sub(x)
                                                                                         INDgcBills.DataSource = Nothing
                                                                                         INDgcBills.DataSource = ListLoadMassiveAccountPayable
                                                                                     End Sub)
                                                             End If
                                                         End If

                                                         If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                                             listErrosImportFile.AddRange(result.MessageResult)
                                                         End If
                                                     Catch ex As Exception
                                                         listErrosImportFile.Add(Utils.GetInnerExceptionMessageToString(ex))
                                                     Finally
                                                         indexSend = positionEnd + 1
                                                         totalProcessedItems = positionEnd
                                                         progress.SafeInvoke(Sub(x) x.PrintInfo())
                                                     End Try
                                                 End While
                                             End Using
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

    Private Sub SetListLoadMassiveAccountPayableDetail()
        Dim indexSend As Integer = 1
        While indexSend <= rowDetails.LastUsedIndex
            Dim row = New ImportFileRow With {.IndexRow = indexSend, .Row = rowDetails.Item(indexSend).SpreadsheetRowToList(9)}

            Dim loadMassiveAccountPayableDetail As New LoadMassiveAccountPayableDetail
            With loadMassiveAccountPayableDetail
                Dim headerId As Integer = 0
                Dim nature As Byte = 0
                Dim decimalValue As Integer = 0
                Dim decimalBaseValue As Integer = 0

                If Not Integer.TryParse(row.Row.Item(0), headerId) Then
                    listErrosImportFile.Add(String.Format("DETALLE: El registro {0} debe tener un identificador numerico valido", indexSend))
                    indexSend = indexSend + 1
                    Continue While
                End If

                If Not Byte.TryParse(row.Row.Item(4), nature) Then
                    listErrosImportFile.Add(String.Format("DETALLE: El registro {0} debe tener una naturaleza válida (1. Debito, 2. Credito)", indexSend))
                    indexSend = indexSend + 1
                    Continue While
                End If

                If Not Decimal.TryParse(row.Row.Item(5), decimalValue) Then
                    listErrosImportFile.Add(String.Format("DETALLE: El registro {0} debe tener un valor numerico valido", indexSend))
                    indexSend = indexSend + 1
                    Continue While
                End If

                If Not String.IsNullOrEmpty(row.Row.Item(7)) AndAlso Not Decimal.TryParse(row.Row.Item(7), decimalBaseValue) Then
                    listErrosImportFile.Add(String.Format("DETALLE: El registro {0} debe tener un valor base numerico valido", indexSend))
                    indexSend = indexSend + 1
                    Continue While
                End If

                .Id = indexSend
                .HeaderId = headerId
                .AccountPayableConceptCode = row.Row.Item(1)
                .ThirdPartyNit = row.Row.Item(2)
                .CostCenterCode = row.Row.Item(3)
                .Nature = nature
                .Value = decimalValue
                .RetentionConceptCode = row.Row.Item(6)
                .BaseValue = decimalBaseValue
                .Detail = row.Row.Item(8)
            End With

            ListLoadMassiveAccountPayableDetail.Add(loadMassiveAccountPayableDetail)

            indexSend = indexSend + 1
        End While
    End Sub

    ''' <summary>
    ''' metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    ''' <remarks></remarks>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  Dim row = New ImportFileRow With {.IndexRow = x, .Row = rows.Item(x).SpreadsheetRowToList(13)}
                                                  listRows.Add(row)

                                                  Dim headerId As Integer = 0
                                                  If Integer.TryParse(row.Row.Item(0), headerId) Then
                                                      Dim details = ListLoadMassiveAccountPayableDetail.Where(Function(d) d.HeaderId = headerId).ToList()
                                                      If details IsNot Nothing AndAlso details.Count > 0 Then
                                                          listDetails.AddRange(details)
                                                          ListLoadMassiveAccountPayableDetail.RemoveAll(AddressOf listDetails.Contains)
                                                      End If
                                                  End If
                                              End SyncLock
                                          End Sub)
    End Sub

#End Region

#End Region

End Class