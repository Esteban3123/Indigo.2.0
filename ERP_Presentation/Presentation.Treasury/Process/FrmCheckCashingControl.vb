#Region "Imports"
Imports System.ComponentModel
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Treasury.MVP

#End Region



Public Class FrmCheckCashingControl
    Implements ICheckCashingControl, ICustomizableForm

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As DateTime? Implements ICheckCashingControl.DocumentDate
        Get
            Return INDDeDate.EditValue
        End Get
        Set(value As DateTime?)
            INDDeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICheckCashingControl.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements ICheckCashingControl.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece una observacion de la solicitud
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements ICheckCashingControl.Observation
        Get
            Return INDMeObservation.EditValue
        End Get
        Set(value As String)
            INDMeObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de entidades bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Public Property EntityBankAccountDatasource As LinqInstantFeedbackSource Implements ICheckCashingControl.EntityBankAccountDatasource
        Get
            Return CType(INDSleBankAccount.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDSleBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Saldo final libros
    ''' </summary>
    ''' <returns></returns>
    Public Property EndBalanceBook As Decimal Implements ICheckCashingControl.EndBalanceBook
        Get
            Return INDSeBalanceAccount.EditValue
        End Get
        Set(value As Decimal)
            INDSeBalanceAccount.EditValue = value
            _ConsultoSaldo = True
            EvaluarFinalizar()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de entidades bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Public Property VoucherTransactionDatasource As XPCollection(Of TreasuryVoucherTransactionXpo) Implements ICheckCashingControl.VoucherTransactionDatasource
        Get
            Return CType(INDGcCheckCashingDetail.DataSource, XPCollection(Of TreasuryVoucherTransactionXpo))
        End Get
        Set(value As XPCollection(Of TreasuryVoucherTransactionXpo))
            INDGcCheckCashingDetail.DataSource = value
            _listoVoucherTransaction = True
            EvaluarFinalizar()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de entidades bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Public WriteOnly Property DetailsVoucherTransaction As XPCollection(Of TreasuryVoucherTransactionXpo) Implements ICheckCashingControl.DetailsVoucherTransaction
        Set(value As XPCollection(Of TreasuryVoucherTransactionXpo))
            Dim linq = From vt In value
                       Join d In _CheckCashingControl.CheckCashingControlDetail
                       On vt.Id Equals d.IdVoucherTransaction
                       Select vt, d.PreviousCheckStatus, d.CurrentCheckStatus
            For Each item In linq
                item.vt.IdCheckCashingStatus = item.PreviousCheckStatus
                item.vt.IdCheckStatusNew = item.CurrentCheckStatus
            Next
            INDSleCheckCashingStatus.Properties.NullText = String.Join(",", value.Select(Function(vt) vt.IdCheckCashingStatus).Distinct().Select(Function(s) CType(s, eCheckStatus).ToString).ToList)
            VoucherTransactionDatasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id de la entidad bancaria que contiene el cheque a cancelar
    ''' </summary>
    ''' <value>
    ''' The identifier entity account.
    ''' </value>
    Public Property IdBankAccount As String Implements ICheckCashingControl.IdBankAccount
        Get
            Return CType(INDSleBankAccount.EditValue, String)
        End Get
        Set(value As String)
            INDSleBankAccount.EditValue = value
        End Set
    End Property

    Private _checkStatus As String
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CheckStatus() As String
        Get
            Return _checkStatus
        End Get
        Set(ByVal value As String)
            _checkStatus = value
            ListVoucherTransactionTask()
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
    Dim Presenter As PCheckCashingControl

    ''' <summary>
    ''' Representa el modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MCheckCashingControl

    '''' <summary>
    '''' Representa la entidad
    '''' </summary>
    '''' <remarks></remarks>
    Dim _CheckCashingControl As CheckCashingControl

    ''' <summary>
    ''' define la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idOperativeUnit As Integer

    ''' <summary>
    ''' Variable que contiene la lista de estados
    ''' </summary>
    Dim ListStatus As New List(Of Tuple(Of Byte, String))

    '''' <summary>
    '''' Variable que define el tipo de evento para la impresión del reporte
    '''' </summary>
    '''' <remarks></remarks>
    'Dim varImp As Integer

    ''' <summary>
    ''' Contiene la cuenta bancaria seleccionada
    ''' </summary>
    Private _entityBankAccount As EntityBankAccounts

    ''' <summary>
    ''' 
    ''' </summary>
    Private _CheckBookControl As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    Private _ConsultoSaldo As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    Private _listoVoucherTransaction As Boolean
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
            If VoucherTransactionDatasource Is Nothing OrElse INDGvCheckCashingDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay comprobantes de egreso en la rejilla"
                Exit Sub
            ElseIf Not CType(VoucherTransactionDatasource, XPCollection(Of TreasuryVoucherTransactionXpo)).Any(Function(r) r.Seleccionado) Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un comprobante de egreso"
                Exit Sub
            ElseIf CType(VoucherTransactionDatasource, XPCollection(Of TreasuryVoucherTransactionXpo)).Any(Function(r) r.Seleccionado AndAlso r.IdCheckStatusNew.GetValueOrDefault = 0) Then
                Mensaje(EeventViewerImages.Advertencia) = "Hay comprobantes de egreso sin seleccionar el nuevo estado"
                Exit Sub
            End If
            AssigningValues()
            Try
                Using model As New MCheckCashingControl(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim Result = Await model.SaveCheckCashingControl(_CheckCashingControl)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        _CheckCashingControl = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        'Me.BarraBotones.PrintReport(PrintReportAction.Create, _CheckCashingControl.Id, 0, _CheckCashingControl.Id)
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
                        _CheckCashingControl = New CheckCashingControl
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
        INDSleBankAccount.Enabled = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True

    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListStatus = New List(Of Tuple(Of Byte, String))
        Dim items As Array
        items = System.Enum.GetValues(GetType(eCheckStatus))

        For Each s In items
            ListStatus.Add(New Tuple(Of Byte, String)(s.GetHashCode, s.ToString))
        Next
        ListStatus.RemoveAt(4) 'Anulado
        ListStatus.RemoveAt(2) 'cobrado
        INDSleCheckCashingStatus.Properties.DataSource = ListStatus.ToList
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
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Id", .FieldName = "Id", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.04)},
                                  New ColumnInfo() With {.Caption = "Banco", .FieldName = "IdEntityAccount.IdBank.Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.32)},
                                  New ColumnInfo() With {.Caption = "Cuenta", .FieldName = "IdEntityAccount.Number", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.32)},
                                  New ColumnInfo() With {.Caption = "Fecha de corte", .FieldName = "DueDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.32)}}.ToList
                .ValorSolicitado = "Id"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCheckCashingControl
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
        If Not INDSleBankAccount.Enabled Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICheckCashingControl.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            'INDSleBankAccount.Enabled = True
            INDSleBankAccount.Enabled = value
            INDDeDate.Enabled = value
            INDSleCheckCashingStatus.Enabled = value
            INDMeObservation.Enabled = value
            INDGcCheckCashingDetail.Enabled = value
            BarraBotones.StatusRecordVisible = False
            INDLcRoot.EndUpdate()
            If value Then
                INDDeDate.Focus()
            Else
                INDSleBankAccount.Focus()
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

        _CheckCashingControl = Nothing
        IdBankAccount = Nothing
        DocumentDate = Me.GetDateServer()
        INDSleCheckCashingStatus.EditValue = Nothing
        Observation = Nothing
        EndBalanceBook = 0
        INDSeOverdraft.EditValue = 0

        ReadOnlyControls(False)
        INDGcCheckCashingDetail.DataSource = Nothing

        IndigoGridControl1.RefreshGrid(INDGcCheckCashingDetail)
        BarraBotones.ReassignOperatingUnit()
        INDLcRoot.EndUpdate()
        ActionsOnControls = False
        'Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        Dim _detalle As CheckCashingControlDetail
        _CheckCashingControl = New CheckCashingControl
        With _CheckCashingControl
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            '.OperatingUnitId = Me.BarraBotones.OperatingUnitValue
            .DueDate = DocumentDate
            .CreationDate = GetDateServer()
            .BalanceAccount = EndBalanceBook
            .Observation = Observation
            .IdEntityAccount = CInt(IdBankAccount)

            For Each itemDetail As TreasuryVoucherTransactionXpo In CType(VoucherTransactionDatasource, XPCollection(Of TreasuryVoucherTransactionXpo)).Where(Function(r) r.Seleccionado)
                _detalle = New CheckCashingControlDetail
                _detalle.CheckNumber = itemDetail.CheckNumber
                _detalle.CurrentCheckStatus = itemDetail.IdCheckStatusNew
                _detalle.IdCheckBook = itemDetail.IdChecks
                _detalle.IdVoucherTransaction = itemDetail.Id
                _detalle.PreviousCheckStatus = itemDetail.IdCheckCashingStatus
                .CheckCashingControlDetail.Add(_detalle)
            Next
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Calcula el valor de saldo en libros 
    ''' </summary>
    Private Sub CalculateLastBalance()
        AsyncLoader(True)
        If (Not String.IsNullOrEmpty(IdBankAccount)) AndAlso DocumentDate IsNot Nothing Then
            _ConsultoSaldo = False
            _listoVoucherTransaction = False
            Presenter.CalculateBalance(Convert.ToInt32(IdBankAccount), DocumentDate.GetValueOrDefault)
            ListVoucherTransaction()
        Else
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>

    Private Sub ListVoucherTransaction()
        Dim parametros As String()
        If (Not String.IsNullOrEmpty(IdBankAccount)) AndAlso DocumentDate IsNot Nothing And (Not String.IsNullOrEmpty(CheckStatus)) Then
            parametros = New String() {IdBankAccount, String.Format("{0:dd/MM/yyyy hh:mm:ss}", DocumentDate), CheckStatus}
            Presenter.ListVoucherTransaction(parametros)
        Else
            VoucherTransactionDatasource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub ListVoucherTransactionTask()
        AsyncLoader(True)
        _ConsultoSaldo = True
        _listoVoucherTransaction = False
        ListVoucherTransaction()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub EvaluarFinalizar()
        If _ConsultoSaldo AndAlso _listoVoucherTransaction Then
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="keyField"></param>
    ''' <param name="descripcionField"></param>
    ''' <returns></returns>
    Private Function RecuperarSeleccionados(sender As Object, keyField As String, descripcionField As String) As String
        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim identificadores As String = String.Empty
        Dim identificador As String = String.Empty
        Dim descripciones As String = String.Empty
        Dim separador As String = String.Empty
        Dim selectedRows As Integer() = edit.Properties.View.GetSelectedRows()
        For Each selectionRow As Integer In selectedRows
            identificador = edit.Properties.View.GetRowCellValue(selectionRow, keyField).ToString()
            If Not String.IsNullOrEmpty(identificador) Then
                identificadores += separador & identificador
                descripciones += separador & edit.Properties.View.GetRowCellValue(selectionRow, descripcionField).ToString()
                separador = ","
            End If
        Next
        edit.Properties.NullText = descripciones.ToString()
        edit.ToolTip = descripciones.ToString()
        Return identificadores.ToString()
    End Function

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls(Optional ByVal _Id As Integer = 0) As Task
        If _Id <> 0 Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using Model As New MCheckCashingControl(Me.Tag.ToString())
                AsyncLoader(True)
                INDLcRoot.BeginUpdate()
                Dim resultOperation = Await Model.GetCheckCashingControlById(_Id)
                _CheckCashingControl = resultOperation
                If _CheckCashingControl IsNot Nothing Then
                    If _CheckCashingControl.Id > 0 Then
                        With _CheckCashingControl
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)

                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            INDSleBankAccount.Properties.NullText = String.Format("{0} - {1} CTA. {2} {3}", .EntityBankAccounts.Code, .EntityBankAccounts.Bank.Name, IIf(.EntityBankAccounts.Type = 1, "Ahorro", "Corriente"),
                                .EntityBankAccounts.Number)
                            DocumentDate = .DueDate
                            Observation = .Observation
                            EndBalanceBook = .BalanceAccount
                            Dim _ids As String = String.Join(",", .CheckCashingControlDetail.Select(Function(r) r.IdVoucherTransaction).ToList)
                            Presenter.GetVoucherTransaction(_ids)
                        End With
                        AsyncLoader(False)
                        ActionsOnControls = False
                        ReadOnlyControls(True)
                        BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                        Dim _parameters As String = String.Empty
                        _parameters = String.Format("<{0}>{1}</{0}>", "Opcion", 1)
                        _parameters = String.Format("{0}<{1}>{2}</{1}>", _parameters, "Id", _CheckCashingControl.Id)
                        _parameters = String.Format("<{0}>{1}</{0}>", "CCC", _parameters)
                        Me.BarraBotones.PrintReport(PrintReportAction.None, _CheckCashingControl.Id, 0, New Object() {_parameters})
                        INDSleBankAccount.Enabled = False
                    End If
                End If
            End Using
        End If
        INDLcRoot.EndUpdate()
    End Function

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
        _CheckCashingControl = Nothing
        _idOperativeUnit = Nothing
        'indexEditRecord = Nothing
        ListStatus = Nothing
        _entityBankAccount = Nothing
        'varImp = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCheckControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        IndigoGridView1.MoreInfoColunmns(INDGvCheckCashingDetail)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvCheckCashingDetail.Columns
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
        Presenter = New PCheckCashingControl(Me)
        InitializeTuples()
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDGcCheckCashingDetail)
    End Sub
    ''' <summary>
    ''' Evento que se dispara cuando el formulario se activa, direge el foco al control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCheckControl_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDSleBankAccount.Enabled Then
            INDSleBankAccount.Focus()
        End If
    End Sub


    ''' <summary>
    ''' Evento que se dispara cuando el formulario se va a cerrar, elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCheckControl_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleBankAccount_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSleBankAccount.KeyDown
        If e.KeyCode = Keys.Enter And Not String.IsNullOrEmpty(IdBankAccount) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            BarraBotones.PrepareToolbar(eAction.OnlySave)
            ActionsOnControls = True
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleBankAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("628", INDSleBankAccount.EditValue, True)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBankAccount.EditValueChanged
        If Not String.IsNullOrEmpty(IdBankAccount) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            Using Model As New MEntityAccount(Me.Tag)
                _entityBankAccount = Await Model.GetEntityBankAccountById(CInt(IdBankAccount))
                If _entityBankAccount IsNot Nothing AndAlso _entityBankAccount.Id > 0 Then
                    If _entityBankAccount.Checkbooks.Any(Function(c) c.Status = 1) Then
                        Dim setting As ActionResult(Of SettingsTreasury) = Nothing
                        Using ModelSetting As New MSettingsTreasury(Me.Tag)
                            setting = Await ModelSetting.GetSettingsTreasuryByIdUnitOperative(Me._idOperativeUnit)
                            If setting.StateResult = False Then
                                CleanControls()
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmSettingsTreasury_DontExists", NAME_MODULE)
                                Exit Sub
                            Else
                                Me._CheckBookControl = Not setting.ObjectEmbbeded.CheckBookControl
                            End If
                        End Using

                    Else
                        CleanControls()
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CheckActiveNoExist", NAME_MODULE)
                        Exit Sub
                    End If
                    BarraBotones.PrepareToolbar(eAction.OnlySave)
                    ActionsOnControls = True
                    DocumentDate = Me.GetDateServer()
                    INDSeOverdraft.EditValue = _entityBankAccount.Quota
                End If
            End Using
        Else
            INDSleBankAccount.Properties.NullText = String.Empty
        End If
        CalculateLastBalance()
    End Sub

    ''' <summary>
    ''' carga el datasource del combo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBankAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBankAccount.QueryPopUp
        If INDSleBankAccount.Properties.DataSource Is Nothing Then
            Presenter.InitializeBankAccountXPO()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDeDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDeDate.EditValueChanged
        CalculateLastBalance()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCheckStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCheckCashingStatus.EditValueChanged
        If String.IsNullOrEmpty(INDSleCheckCashingStatus.EditValue) Then
            CheckStatus = String.Empty
            INDSleCheckCashingStatus.Properties.NullText = String.Empty
            INDSleCheckCashingStatus.ToolTip = String.Empty
            INDGvCheckStatus.ClearSelection()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCheckStatus_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDSleCheckCashingStatus.CloseUp
        Me.CheckStatus = RecuperarSeleccionados(sender, "Item1", "Item2")
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
        '_CheckCashingControl.Status = 1
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
        '_CheckCashingControl.Status = 1
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

    '''' <summary>
    '''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
    '    Dim _parameters As String = String.Empty
    '    _parameters = String.Format("<{0}>{1}</{0}>", "Opcion", 1)
    '    _parameters = String.Format("{0}<{1}>{2}</{1}>", _parameters, "Id", _CheckCashingControl.Id)
    '    _parameters = String.Format("<{0}>{1}</{0}>", "CCC", _parameters)
    '    Me.BarraBotones.PrintReport(PrintReportAction.ViewPrinting, _CheckCashingControl.Id, 0, New Object() {_parameters})
    'End Sub

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
        '_CheckCashingControl.Status = 2
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
        '_CheckCashingControl.Status = 3
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
        '_CheckCashingControl.Status = 2
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
    Private Sub INDGvCheckCashingDetail_ShowingEditor(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles INDGvCheckCashingDetail.ShowingEditor
        If INDGvCheckCashingDetail.FocusedColumn.FieldName = "IdCheckStatusNew" Then
            Dim _voucherTransaction = TryCast(INDGvCheckCashingDetail.GetFocusedRow(), TreasuryVoucherTransactionXpo)
            If _voucherTransaction IsNot Nothing Then
                If Not _voucherTransaction.Seleccionado Then
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
    Private Sub GridViewMain_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDGvCheckCashingDetail.CustomRowCellEdit
        If e.Column.FieldName = "IdCheckStatusNew" Then
            Dim INDRepositoryLookUpEdit = TryCast(e.RepositoryItem, Repository.RepositoryItemLookUpEdit)
            If INDRepositoryLookUpEdit Is Nothing Then
                Dim _voucherTransaction = TryCast(INDGvCheckCashingDetail.GetFocusedRow(), TreasuryVoucherTransactionXpo)
                If _voucherTransaction IsNot Nothing Then
                    INDRepositoryLookUpEdit = New Repository.RepositoryItemLookUpEdit
                    INDRepositoryLookUpEdit.Name = String.Format("RepositoryLookUpEdit{0}", _voucherTransaction.Id)
                    Dim ListStatusNew As New List(Of Tuple(Of Byte, String))
                    Dim items As Array
                    items = System.Enum.GetValues(GetType(eCheckStatus))

                    For Each s In items
                        ListStatusNew.Add(New Tuple(Of Byte, String)(s.GetHashCode, s.ToString))
                    Next

                    Select Case _voucherTransaction.IdCheckCashingStatus
                        Case 1 'girado
                            ListStatusNew.RemoveAt(0) 'girado
                        Case 2 'entregado
                            ListStatusNew.RemoveAt(1) 'entregado
                            ListStatusNew.RemoveAt(0) 'girado
                        Case 4 'devuelto
                            ListStatusNew.RemoveAt(3) 'devuelto
                            ListStatusNew.RemoveAt(0) 'girado
                    End Select
                    INDRepositoryLookUpEdit.DisplayMember = "Item2"
                    INDRepositoryLookUpEdit.ValueMember = "Item1"
                    INDRepositoryLookUpEdit.NullText = "Seleccione"
                    INDRepositoryLookUpEdit.DataSource = ListStatusNew.ToList
                    INDRepositoryLookUpEdit.PopulateColumns()
                    INDRepositoryLookUpEdit.Columns("Item1").Visible = False
                    INDRepositoryLookUpEdit.Columns("Item2").Caption = "Estado"
                    INDRepositoryLookUpEdit.BestFitMode = BestFitMode.BestFitResizePopup
                    INDRepositoryLookUpEdit.DropDownRows = ListStatusNew.Count
                    INDRepositoryLookUpEdit.SearchMode = SearchMode.AutoComplete
                    INDRepositoryLookUpEdit.AutoSearchColumnIndex = 1
                    e.RepositoryItem = INDRepositoryLookUpEdit
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub INDrepositorioCheck_CheckedChanged(sender As Object, e As EventArgs) Handles INDrepositorioCheck.CheckedChanged
        Dim _voucherTransaction = TryCast(INDGvCheckCashingDetail.GetFocusedRow(), TreasuryVoucherTransactionXpo)
        If _voucherTransaction IsNot Nothing Then
            _voucherTransaction.IdCheckStatusNew = Nothing
            INDGcCheckCashingDetail.RefreshDataSource()
            INDGcCheckCashingDetail.Invalidate()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvCheckCashingDetail_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDGvCheckCashingDetail.CustomColumnDisplayText
        If e.Column.FieldName = "IdCheckStatusNew" Then
            If e.Value Is Nothing Then
                e.DisplayText = "Seleccione"
            Else
                e.DisplayText = CType(e.Value, eCheckStatus).ToString
            End If
        End If
    End Sub


#End Region

    Enum eCheckStatus
        Girado = 1
        Entregado = 2
        Cobrado = 3
        Devuelto = 4
        Anulado = 5
    End Enum
End Class