Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Treasury.MVP

Public Class FrmCashFlowReclassification
    Implements ICashFlowReclassification, ICustomizableForm
#Region "Properties"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICashFlowReclassification.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    '''  Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ICashFlowReclassification.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property DocumentType As Byte? Implements ICashFlowReclassification.DocumentType
        Get
            Return INDGleDocument.EditValue
        End Get
        Set(ByVal value As Byte?)
            INDGleDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements ICashFlowReclassification.Code
        Get
            Return INDTeDocumentCode.EditValue
        End Get
        Set(ByVal value As String)
            INDTeDocumentCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property DetailsDatasource As DataTable Implements ICashFlowReclassification.DetailsDatasource
        Get
            Return CType(INDGcDetail.DataSource, DataTable)
        End Get
        Set(ByVal value As DataTable)
            INDGcDetail.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Private _listDocument As List(Of Tuple(Of Byte, String))
    Public Property ListDocument() As List(Of Tuple(Of Byte, String))
        Get
            Return _listDocument
        End Get
        Set(ByVal value As List(Of Tuple(Of Byte, String)))
            _listDocument = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Private _listConcept As List(Of CashFlowConceptXpo)
    Public Property ListConcept() As List(Of CashFlowConceptXpo)
        Get
            Return _listConcept
        End Get
        Set(ByVal value As List(Of CashFlowConceptXpo))
            _listConcept = value
        End Set
    End Property

#End Region

#Region "Globals"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PCashFlowReclassification

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MCashFlowReclassification

    '''' <summary>
    '''' Representa la entidad
    '''' </summary>
    '''' <remarks></remarks>
    Dim _CashFlowReclassification As CashFlowReclassification

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer



#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    '''  METODO: Item Guardar de control de cheques.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            Dim _seleccionado As Boolean
            Dim _sinConcepto As Boolean
            If DetailsDatasource Is Nothing OrElse INDGvDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay documentos a reclasificar en la rejilla"
                Exit Sub
            Else
                For Each itemDetail As DataRow In DetailsDatasource.Rows
                    If CBool(itemDetail("Seleccionado")) Then
                        _seleccionado = True
                        If itemDetail("CurrentCfeId") Is Nothing OrElse itemDetail("CurrentCfeId") = 0 Then
                            _sinConcepto = True
                        End If
                    End If
                Next
            End If
            If Not _seleccionado Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un documento a reclasificar"
                Exit Sub
            ElseIf _sinConcepto Then
                Mensaje(EeventViewerImages.Advertencia) = "Hay documento a reclasificar sin seleccionar el nuevo concepto de flujo de efectivo"
                Exit Sub
            End If
            AssigningValues()
            Try
                Using model As New MCashFlowReclassification(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim Result = Await model.SaveCashFlowReclassification(_CashFlowReclassification)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        _CashFlowReclassification = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.BarraBotones.PrintReport(PrintReportAction.Create, _CashFlowReclassification.Id, 0, _CashFlowReclassification.Id)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If Result.MessageResult Is Nothing Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        ElseIf Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                        _CashFlowReclassification = New CashFlowReclassification
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        Deshacer()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListDocument = New List(Of Tuple(Of Byte, String))
        ListDocument.Add(New Tuple(Of Byte, String)(1, "Recibos de caja"))
        ListDocument.Add(New Tuple(Of Byte, String)(2, "Comprobantes de egreso"))
        ListDocument.Add(New Tuple(Of Byte, String)(4, "Notas"))
        ListDocument.Add(New Tuple(Of Byte, String)(5, "Cruce de cuentas"))

        INDGleDocument.Properties.DataSource = ListDocument.ToList
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        Try
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Id", .FieldName = "Id", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                                  New ColumnInfo() With {.Caption = "Documento", .FieldName = "DocumentName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                                  New ColumnInfo() With {.Caption = "Fecha documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)}}.ToList
                .ValorSolicitado = "Id"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCashFlowReclassification
                .FormParent = Me
                .ShowSearch()
            End With
        Catch ex As Exception
            Dim hola As Boolean
            hola = True
        End Try

    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Dim _Id As String = FormSearchObjects.GridViewBusquedas.GetRowCellValue(FormSearchObjects.GridViewBusquedas.FocusedRowHandle, "Id")
        Await LoadControls(_Id)
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICashFlowReclassification.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            CtrDateNavigator1.Enabled = True
            INDGleDocument.Enabled = True
            INDTeDocumentCode.Enabled = value
            INDGvDetail.OptionsBehavior.ReadOnly = Not value
            BarraBotones.StatusRecordVisible = False
            INDLcRoot.EndUpdate()
            If value Then
                CtrDateNavigator1.Focus()
            Else
                INDGleDocument.Focus()
            End If

        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        'DeleteBlockedRecord()
        INDLcRoot.BeginUpdate()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        'Me.BarraBotones.StatusRecordVisible = False

        _CashFlowReclassification = Nothing
        Dim fecha As DateTime
        fecha = Me.GetServerDate
        CtrDateNavigator1.SetMonth = fecha.Month
        CtrDateNavigator1.SetYear = fecha.Year
        DocumentType = Nothing
        Code = Nothing

        ReadOnlyControls(False)
        INDGcDetail.DataSource = Nothing

        IndigoGridControl1.RefreshGrid(INDGcDetail)
        BarraBotones.ReassignOperatingUnit()
        INDLcRoot.EndUpdate()
        ActionsOnControls = False
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        Dim _detalle As CashFlowReclassificationDetail
        _CashFlowReclassification = New CashFlowReclassification
        Dim _documentDate = New DateTime(CtrDateNavigator1.GetYear, CtrDateNavigator1.GetMonth, 1)

        With _CashFlowReclassification
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            '.OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .DocumentDate = _documentDate
            .DocumentType = Me.DocumentType
            .DocumentCode = Code
            .CreationDate = GetDateServer()

            If Me.DetailsDatasource IsNot Nothing And Me.DetailsDatasource.Rows.Count > 0 Then

                For Each itemDetail As DataRow In Me.DetailsDatasource.Rows
                    If CBool(itemDetail("Seleccionado")) Then
                        _detalle = New CashFlowReclassificationDetail
                        _detalle.DocumentId = CInt(itemDetail("IdResource"))
                        _detalle.CreditValue = CDec(itemDetail("ValorCredito"))
                        _detalle.DebitValue = CDec(itemDetail("ValorDebito"))
                        _detalle.PreviousCashFlowConcept = IIf(itemDetail("PreviousCfeId") = 0, Nothing, itemDetail("PreviousCfeId"))
                        _detalle.CurrentCashFlowConcept = IIf(itemDetail("CurrentCfeId") = 0, Nothing, itemDetail("CurrentCfeId"))
                        .CashFlowReclassificationDetail.Add(_detalle)
                    End If
                Next
            End If
            'If .Id > 0 Then
            '.MarkAsModified()
            'End If
        End With
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub ListDocumentToReclassification()
        If DocumentType.GetValueOrDefault <> 0 Then
            If INDRepositorySleConcepto.DataSource Is Nothing Then
                Dim _presenter As New PCashFlowConcept
                INDRepositorySleConcepto.DataSource = _presenter.GetCashFlowConcept(New String() {"1", Nothing})
                _presenter = Nothing
            End If

            Dim _fecha As DateTime
            _fecha = New DateTime(CtrDateNavigator1.GetYear, CtrDateNavigator1.GetMonth, 1)

            Dim parametros As String = String.Format("<{0}>{1}</{0}>", "Opcion", 1)
            parametros = String.Format("{0}<{1}>{2:dd/MM/yyyy hh:mm:ss}</{1}>", parametros, "InitialDate", _fecha)
            parametros = String.Format("{0}<{1}>{2:dd/MM/yyyy hh:mm:ss}</{1}>", parametros, "EndDate", _fecha.AddMonths(1))
            parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "DocumentType", DocumentType)
            If Not String.IsNullOrEmpty(Code) Then
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "DocumentCode", DocumentType)
            End If
            parametros = String.Format("<{0}>{1}</{0}>", "Parameters", parametros)
            Presenter.ListCashFlowStatus(parametros)
        Else
            INDGcDetail.DataSource = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls(Optional ByVal _Id As Integer = 0) As Task
        If _Id <> 0 Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using Model As New MCashFlowReclassification(Me.Tag.ToString())
                AsyncLoader(True)
                INDLcRoot.BeginUpdate()
                Dim resultOperation = Await Model.GetCashFlowReclassificationById(_Id)
                Dim parametros As String = String.Format("<{0}>{1}</{0}>", "Opcion", 2)
                parametros = String.Format("{0}<{1}>{2}</{1}>", parametros, "Id", _Id)
                parametros = String.Format("<{0}>{1}</{0}>", "Parameters", parametros)
                Dim _documentName As String = String.Empty
                _CashFlowReclassification = resultOperation
                If _CashFlowReclassification IsNot Nothing Then
                    If _CashFlowReclassification.Id > 0 Then
                        With _CashFlowReclassification
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Select Case _CashFlowReclassification.DocumentType
                                Case 1
                                    _documentName = "Recibo de caja"
                                Case 2
                                    _documentName = "Comprobante de egreso"
                                Case 3
                                    _documentName = "Consignación"
                                Case 4
                                    _documentName = "Nota"
                                Case 5
                                    _documentName = "Cruce de cuentas"
                            End Select
                            INDGleDocument.Properties.NullText = _documentName
                            CtrDateNavigator1.SetYear = _CashFlowReclassification.DocumentDate.Year
                            CtrDateNavigator1.SetMonth = _CashFlowReclassification.DocumentDate.Month
                            INDTeDocumentCode.EditValue = _CashFlowReclassification.DocumentCode
                            Presenter.ListCashFlowStatus(parametros)
                        End With
                        AsyncLoader(False)
                        ActionsOnControls = False
                        ReadOnlyControls(True)
                        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        INDGleDocument.Enabled = False
                    End If
                End If
            End Using
        End If
        INDLcRoot.EndUpdate()
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub PermiteConsultarHabilitar()
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        ActionsOnControls = True
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.IdEntity = String.Empty
    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        'record = Nothing
        Presenter = Nothing
        Model = Nothing
        _CashFlowReclassification = Nothing
        _idOperativeUnit = Nothing
        'indexEditRecord = Nothing
        ListDocument = Nothing
        ListConcept = Nothing
        'varImp = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCashFlowReclassification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        IndigoGridView1.MoreInfoColunmns(INDGvDetail)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            ElseIf col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next
        Me.LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCashFlowReclassification(Me)
        ListConcept = New List(Of CashFlowConceptXpo)
        InitializeTuples()
        Deshacer()
        IndigoGridView1.MoreInfoColunmns(INDGvDetail)
        IndigoGridControl1.RefreshGrid(INDGcDetail)
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando el formulario se activa, direge el foco al control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCashFlowReclassification_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If CtrDateNavigator1.Enabled Then
            CtrDateNavigator1.Focus()
        End If
    End Sub


    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCashFlowReclassification_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the CtrDateNavigator1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDGleDocument_KeyDown(sender As Object, e As KeyEventArgs) Handles INDGleDocument.KeyDown
        If e.KeyCode = Keys.Enter Then
            PermiteConsultarHabilitar()
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleDocument_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleDocument.EditValueChanged
        PermiteConsultarHabilitar()
        ListDocumentToReclassification()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDTeDocumentCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDTeDocumentCode.EditValueChanged
        ListDocumentToReclassification()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CtrDateNavigator1_Leave(sender As Object, e As EventArgs) Handles CtrDateNavigator1.Leave
        ListDocumentToReclassification()
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleAffectCashFlowConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs)
        If INDRepositorySleConcepto.DataSource Is Nothing Then
            Dim _presenter As New PCashFlowConcept
            INDRepositorySleConcepto.DataSource = _presenter.GetCashFlowConcept(New String() {"1", Nothing})
            _presenter = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        'banConfirm = False
        'banSaveAndConfirm = False
        'banAnular = False
        '_CashFlowReclassification.Status = 1
        'varImp = 2
        'Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar ', INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
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
        'banConfirm = False
        'banSaveAndConfirm = False
        'banAnular = False
        '_CashFlowReclassification.Status = 1
        'varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        'Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        'Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, PurchaseRequest.Id, 0, PurchaseRequest.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            'If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
            '    If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
            '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            '    End If
            'End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        'banConfirm = True
        'banSaveAndConfirm = False
        'banAnular = False
        '_CashFlowReclassification.Status = 2
        'varImp = 3
        'Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        'banConfirm = False
        'banSaveAndConfirm = False
        'banAnular = True
        '_CashFlowReclassification.Status = 3
        'varImp = 4
        'Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        'banConfirm = True
        'banSaveAndConfirm = True
        'banAnular = False
        '_CashFlowReclassification.Status = 2
        'varImp = 3
        'Guardar()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        'Throw New NotImplementedException()
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar
        'Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvDetail_ShowingEditor(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDGvDetail.ShowingEditor
        If INDGvDetail.FocusedColumn.FieldName = "CurrentCfeId" Then
            Dim _document = TryCast(INDGvDetail.GetFocusedRow(), DataRowView)
            If _document IsNot Nothing Then
                If Not CBool(_document("Seleccionado")) Then
                    e.Cancel = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepositorioCheck_CheckedChanged(sender As Object, e As EventArgs) Handles INDRepositoryCheck.CheckedChanged
        Dim _document = TryCast(INDGvDetail.GetFocusedRow(), DataRowView)
        If _document IsNot Nothing Then
            _document("CurrentCfeId") = 0
            INDGcDetail.RefreshDataSource()
            INDGcDetail.Invalidate()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRepositorySleConcepto_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRepositorySleConcepto.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) Then
            e.NewValue = 0
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvDetail_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDGvDetail.CustomColumnDisplayText
        If e.Column.FieldName = "CurrentCfeId" Then
            If e.Value Is Nothing OrElse e.Value = 0 Then
                e.DisplayText = "Seleccione"
            Else
                Dim _presenter As New PCashFlowConcept
                Dim _concepto As Infrastructure.Data.Xpo.TreasuryRepository.CashFlowConceptXpo
                _concepto = ListConcept.FirstOrDefault(Function(c) c.Id = e.Value)
                If _concepto Is Nothing Then
                    _concepto = _presenter.GetCashFlowConceptById(e.Value)
                End If
                If _concepto IsNot Nothing Then
                    ListConcept.Add(_concepto)
                    e.DisplayText = _concepto.CodeName
                End If
            End If
        End If
    End Sub
#End Region
End Class