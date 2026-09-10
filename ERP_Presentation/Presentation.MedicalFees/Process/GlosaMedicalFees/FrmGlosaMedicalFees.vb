#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.MedicalFeesRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MedicalFees.MVP

#End Region

Public Class FrmGlosaMedicalFees
    Implements IGlosaMedicalFees, ICustomizableForm
    Private Const NAME_MODULE As String = "MedicalFees"

#Region "Globals"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PGlosaMedicalFees

    ''' <summary>
    ''' Representa el modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As MGlosaMedicalFees

    ''' <summary>
    ''' Representa la entidad de la tabla
    ''' </summary>
    ''' <remarks></remarks>
    Dim GlosaMedicalFees As GlosaMedicalFees

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMedicalFees

    ''' <summary>
    ''' Variable que contiene un item del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim GlosaMedicalFeesDetail As GlosaMedicalFeesDetail

    ''' <summary>
    ''' Bandera
    ''' </summary>
    Dim _isLoading As Boolean

    ''' <summary>
    ''' Bnadera para activar propiedad solo lectura
    ''' </summary>
    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' variable que contiene el Id del rgistro de cabecera
    ''' </summary>
    Dim IdGlosaMedicalFees As Integer

    ''' <summary>
    ''' almacena la entidad de la tabla CxP
    ''' </summary>
    Private AccountPayable As AccountPayable

    ''' <summary>
    ''' control de anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _ctrInfoGlosa As CtrDebitCredit

#End Region

#Region " Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IGlosaMedicalFees.Code
        Get
            If (INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece el Id de la linea de distribucion del proveedor asociado 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineId As Integer Implements IGlosaMedicalFees.SupplierDistributionLineId
        Get
            Return INDSleSupplier.EditValue
        End Get
        Set(value As Integer)
            INDSleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de glosa de honorarios medicos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GlosaMedicalFeesConceptId As Integer Implements IGlosaMedicalFees.GlosaMedicalFeesConceptId
        Get
            Return INDSleGMFConcepts.EditValue
        End Get
        Set(value As Integer)
            INDSleGMFConcepts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IGlosaMedicalFees.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cabecera
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IGlosaMedicalFees.Observation
        Get
            If (INDMmoDescription.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDMmoDescription.Text
            End If
        End Get
        Set(value As String)
            INDMmoDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' valor unitario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitValue As Decimal Implements IGlosaMedicalFees.UnitValue
        Get
            Return INDTeUnitValue.Text
        End Get
        Set(value As Decimal)
            INDTeUnitValue.Text = value
        End Set
    End Property

    ''' <summary>
    ''' la cantidad 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Quantity As Integer Implements IGlosaMedicalFees.Quantity
        Get
            Return INDSeQuantity.EditValue
        End Get
        Set(value As Integer)
            INDSeQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' valor total glosado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalValue As Decimal Implements IGlosaMedicalFees.TotalValue
        Get
            Return INDTeTotalValue.EditValue
        End Get
        Set(value As Decimal)
            INDTeTotalValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces las lineas de distribucion del proveedor del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _supplierId As Integer

    Public Property SupplierId As Integer Implements IGlosaMedicalFees.SupplierId
        Get
            Return _supplierId
        End Get
        Set(value As Integer)
            _supplierId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IGlosaMedicalFees.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As MedicalFeesSecuence

    Public Property Sequense As MedicalFeesSecuence Implements IGlosaMedicalFees.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As MedicalFeesSecuence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MedicalFeesSecuenceDetail In Me._sequence.MedicalFeesSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IGlosaMedicalFees.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IGlosaMedicalFees.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Establece las acciones de abilitacion de los controles del frontal
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IGlosaMedicalFees.ActionsOnControls
        Set(value As Boolean)
            ' Datos Principales
            INDlyGlosaMedicalFees.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDSleSupplier.Enabled = value
            INDMmoDescription.Enabled = value
            INDpceAcountPayable.Enabled = value
            INDGcGMedicalFeesD.Enabled = value
            INDpceAcountPayable.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDlyGlosaMedicalFees.EndUpdate()
            If value Then
                INDdeDocumentDate.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

#End Region

#Region "DataSource"

    ''' <summary>
    ''' Obtiene o establece el proveedor con sus lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SuppliersDistributionLinesXpo As Xpo.XPInstantFeedbackSource Implements IGlosaMedicalFees.SuppliersDistributionLinesXpo
        Get
            Return CType(INDSleSupplier.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As Xpo.XPInstantFeedbackSource)
            INDSleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene los Conceptos de glosa de honorarios medicos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListGlosaMedicalFeesConcepts As Xpo.XPInstantFeedbackSource Implements IGlosaMedicalFees.ListGlosaMedicalFeesConcepts
        Get
            Return CType(INDSleGMFConcepts.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As Xpo.XPInstantFeedbackSource)
            INDSleGMFConcepts.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de las cuentas por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListAccountPayable As XPInstantFeedbackSource Implements IGlosaMedicalFees.ListAccountPayable
        Get
            Return INDSleCxP.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCxP.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable que almacena o establece el valor pendiente total del registro de glosamedicalfees
    ''' </summary>
    Private _pendingValue As Decimal
    Public Property PendingValue As Decimal Implements IGlosaMedicalFees.PendingValue
        Get
            Return _pendingValue
        End Get
        Set(value As Decimal)
            _pendingValue = value
        End Set
    End Property

    ''' <summary>
    ''' variable que obtiene o establece el valor glosado total del registro de GlosaMedicalFees
    ''' </summary>
    Private _glossedValue As Decimal
    Public Property GlossedValue As Decimal Implements IGlosaMedicalFees.GlossedValue
        Get
            Return _glossedValue
        End Get
        Set(value As Decimal)
            _glossedValue = value
        End Set
    End Property

#End Region

#Region "ICrud"

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewContract()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click desconfirmar
    ''' </summary>
    Public Sub Desconfirmar() Implements ICrudBase.Eliminar
        If MessageIndigo.Show("¿Esta Seguro de Desconfirmar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.Status = 1
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Guardar.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() Then
        Else
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MGlosaMedicalFees(Me.Tag.ToString())
                AsyncLoader(True)

                Dim Result = Await model.SaveGlosaMedicalFeesAsync(GlosaMedicalFees, _idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If GlosaMedicalFees.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me.GlosaMedicalFees = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult.Count > 0 AndAlso Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Activated"

    ''' <summary>
    ''' Se dispara cuando el formulario se activa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmGlosaMedicalFees_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub

#End Region

#Region "FromClosing"

    ''' <summary>
    ''' Se dispara cuando el formulario se va a cerrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmGlosaMedicalFees_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Load"
    ''' <summary>
    ''' Disposed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        Model = Nothing
        GlosaMedicalFees = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        GlosaMedicalFeesDetail = Nothing
        OnlyRead = Nothing
        _isLoading = Nothing
        varImp = Nothing
        ListAccountPayable = Nothing
        AccountPayable = Nothing
        ''_ctrInfoGlosa = Nothing
        PendingValue = Nothing
        GlossedValue = Nothing
        Quantity = Nothing
        UnitValue = Nothing
        AccountPayable = Nothing
    End Sub

    ''' <summary>
    ''' Se dispara cuando incial la carga del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmGlosaMedicalFees_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Personalizacion de la rejilla, muestra las columnas ocultas como mas infomacion
        BarraBotones.StatusRecordVisible = True
        Me.LayoutControls.SetIsCustomizable(Me.INDlyGlosaMedicalFees, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PGlosaMedicalFees(Me)
        Presenter.GetSequense()
        LoadStatus()
        BarLoad()
        Deshacer()
    End Sub

    Private Sub BarLoad()
        _ctrInfoGlosa = New CtrDebitCredit()
        _ctrInfoGlosa.SetDebitAndCredit(AddressOf SetTotalAndPendingValue)
        _ctrInfoGlosa.SetNewNames("Info Glosa", "Valor Glosado", "Valor Pendiente")
        _ctrInfoGlosa.RefreshDebitCredit()
        _ctrInfoGlosa.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_ctrInfoGlosa)
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control para cargar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewContract()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Realiza la consulta  de las lineas de distribucion por supplier
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSupplier.QueryPopUp
        If INDSleSupplier.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta de las cuentas por pagar por Supplier
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCxP_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCxP.QueryPopUp
        If INDSleCxP.Properties.DataSource() Is Nothing OrElse INDSleCxP.EditValue <> SupplierId Then
            Presenter.LoadAccountPayableBySupplier(SupplierId)
        End If
    End Sub

    ''' <summary>
    ''' Realiza la consulta de los conceptos de glosa de honorarios medicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleGMFConcepts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleGMFConcepts.QueryPopUp
        If ListGlosaMedicalFeesConcepts Is Nothing Then
            Presenter.ListGlosaMedicalFeesConcepts()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' cuando se haga un cambio en la linea de sitribucion del supplier se asigna el nuevo supplierId
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplier.EditValueChanged

        If SupplierDistributionLineId > 0 Then
            If _isLoading = False Then
                Dim suppplierMainAccount = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                SupplierId = suppplierMainAccount.IdSupplier.Id
                INDpceAcountPayable.Enabled = True
            End If
        Else
            SupplierId = 0
        End If
    End Sub

    ''' <summary>
    ''' cuando se cambia la CxP Cambia los datos añadidos a esta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCxP_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCxP.EditValueChanged
        If INDSleCxP.EditValue > 0 Then
            Using Model As New MGlosaMedicalFees(CStr(Me.Tag))
                Me.AccountPayable = New AccountPayable
                AccountPayable = Model.GetAccountPayableById(INDSleCxP.EditValue)
                With AccountPayable
                    INDDeInvoiceDate.EditValue = .BillDate
                    INDDeInvoiceExpirationDate.EditValue = .ExpirationDate
                    INDTeInvoiceValue.EditValue = .Value
                    INDTeInvoiceBalance.EditValue = .Balance
                End With
            End Using
        End If
    End Sub
    ''' <summary>
    ''' Realiza la operacion para generar el valor total del item del detalle 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeQuantity.EditValueChanged, INDTeUnitValue.EditValueChanged
        TotalValue = UnitValue * Quantity
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que agrega la CxP al detalle del GlosaMedicalFeesDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddCxP_Click(sender As Object, e As EventArgs) Handles INDBtnAddCxP.Click
        If INDSleCxP.EditValue IsNot Nothing Then

            If GlosaMedicalFeesConceptId = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Falta agregar un concepto"
                Return
            End If

            If Quantity = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad debe ser mayor a 0"
                Return
            End If

            If GlosaMedicalFees.GlosaMedicalFeesDetail.Where(Function(x) x.AccountPayableId = INDSleCxP.EditValue).Count > 0 Then
                If GlosaMedicalFees.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then

                    With GlosaMedicalFees.GlosaMedicalFeesDetail.Where(Function(x) x.AccountPayableId = INDSleCxP.EditValue).FirstOrDefault
                        .InvoiceNumber = AccountPayable.BillNumber
                        .InvoiceValue = AccountPayable.InvoiceValue
                        .AccountPayableId = AccountPayable.Id
                        .GlosaMedicalFeesConceptsId = GlosaMedicalFeesConceptId
                        .ConceptDescription = INDSleGMFConcepts.Text
                        .UnitValue = UnitValue
                        .Quantity = Quantity
                        .TotalValue = TotalValue
                        .Observation = INDMmoObservation.EditValue
                        .Evaluated = False
                    End With

                    INDGcGMedicalFeesD.DataSource = GlosaMedicalFees.GlosaMedicalFeesDetail
                    INDGcGMedicalFeesD.Refresh()
                    Return
                End If
                Mensaje(EeventViewerImages.Advertencia) = "La cuenta por pagar ya esta agregada"
                Return
            End If

            Dim GlosaMedicalFeesDetail As New GlosaMedicalFeesDetail()
            With GlosaMedicalFeesDetail
                .InvoiceNumber = AccountPayable.BillNumber
                .InvoiceValue = AccountPayable.InvoiceValue
                .AccountPayableId = AccountPayable.Id
                .GlosaMedicalFeesConceptsId = GlosaMedicalFeesConceptId
                .ConceptDescription = INDSleGMFConcepts.Text
                .UnitValue = UnitValue
                .Quantity = Quantity
                .TotalValue = TotalValue
                .Observation = INDMmoObservation.EditValue
                .Evaluated = False
            End With

            GlosaMedicalFees.GlosaMedicalFeesDetail.Add(GlosaMedicalFeesDetail)

            If GlosaMedicalFees.ChangeTracker.State = ObjectState.Unchanged Then
                GlosaMedicalFees.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            INDSleCxP.EditValue = Nothing
            INDGcGMedicalFeesD.DataSource = GlosaMedicalFees.GlosaMedicalFeesDetail
            CleanDetaiPopUp()
            GetTotalAndPendingValue()
            _ctrInfoGlosa.SetDebitAndCredit(AddressOf SetTotalAndPendingValue)
            _ctrInfoGlosa.RefreshDebitCredit()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Elija una CxP"
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la accion de eliminar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbIDelete_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles INDBbIDelete.ItemClick
        DeleteItem()
        INDGcGMedicalFeesD.DataSource = Nothing
        INDGcGMedicalFeesD.DataSource = GlosaMedicalFees.GlosaMedicalFeesDetail.ToList()
    End Sub

    ''' <summary>
    ''' ejecuta la accion de editar el detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbIEdit_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles INDBbIEdit.ItemClick
        Dim ObjAccount = CType(INDGvGMFDetail.GetFocusedRow(), GlosaMedicalFeesDetail)
        If ObjAccount IsNot Nothing Then
            With ObjAccount
                INDSleCxP.EditValue = .AccountPayableId
                INDSleCxP.Properties.NullText = .AccountPayableDescription
                INDSleCxP.Enabled = False
                Using Model As New MGlosaMedicalFees(CStr(Me.Tag))
                    Me.AccountPayable = New AccountPayable
                    AccountPayable = Model.GetAccountPayableById(.AccountPayableId)
                    With AccountPayable
                        INDDeInvoiceDate.EditValue = .BillDate
                        INDDeInvoiceExpirationDate.EditValue = .ExpirationDate
                        INDTeInvoiceValue.EditValue = .Value
                        INDTeInvoiceBalance.EditValue = .Balance
                    End With
                End Using
                Me.UnitValue = .UnitValue
                Me.Quantity = .Quantity
                Me.TotalValue = .TotalValue
                Me.INDSleGMFConcepts.EditValue = .GlosaMedicalFeesConceptsId
                Me.INDSleGMFConcepts.Properties.NullText = .ConceptDescription
                Me.INDMmoObservation.EditValue = .Observation
            End With
            GlosaMedicalFees.MarkAsModified()
            INDpceAcountPayable.ShowPopup()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un elemento"

        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la aacion de evaluar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBbIEvaluation_ItemClick(sender As Object, e As XtraBars.ItemClickEventArgs) Handles INDBbIEvaluation.ItemClick
        If GlosaMedicalFees.GlosaMedicalFeesDetail IsNot Nothing Then
            Try
                Dim ObjAccount = CType(INDGvGMFDetail.GetFocusedRow(), GlosaMedicalFeesDetail)
                Using formulario As New FrmPopupGlosaMedicalFeesEvaluation
                    Me.Cursor = ChangeCursorIndigo()
                    formulario.ViewModeEditHold = True
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.GlossedValue = ObjAccount.TotalValue

                    If ObjAccount.Evaluated Then
                        Dim GlosaMedicalFeesdetail As New GlosaMedicalFeesDetail

                        GlosaMedicalFeesdetail = GlosaMedicalFees.GlosaMedicalFeesDetail.Where(Function(x) x.AccountPayableId = ObjAccount.AccountPayableId).FirstOrDefault
                        formulario.GMFeesDetailId = GlosaMedicalFeesdetail.Id

                        With GlosaMedicalFeesdetail
                            With .GlosaMedicalFeesEvaluation.FirstOrDefault
                                formulario.GMFEvolutionId = .Id
                                formulario.SAcceptedValue = .AcceptedValueProv
                                formulario.RaisedValue = .RaisedValue
                                formulario.PendingValue = .PendingValue
                                formulario.Comment = .Observation
                            End With
                        End With
                    End If

                    Dim transparent = New Base.FrmTransparent(formulario, False)

                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)

                    With GlosaMedicalFees.GlosaMedicalFeesDetail.Where(Function(x) x.AccountPayableId = ObjAccount.AccountPayableId).FirstOrDefault
                        If .Evaluated Then
                            For Each Item In .GlosaMedicalFeesEvaluation
                                Dim Evaluation As New GlosaMedicalFeesEvaluation
                                Evaluation = formulario.GlosaMedicalFeesEvaluation.FirstOrDefault
                                If Evaluation IsNot Nothing Then
                                    Item.AcceptedValueProv = Evaluation.AcceptedValueProv
                                    Item.PendingValue = Evaluation.PendingValue
                                    Item.RaisedValue = Evaluation.RaisedValue
                                    Item.Observation = Evaluation.Observation
                                End If
                            Next

                        ElseIf formulario.GlosaMedicalFeesEvaluation.Count > 0 Then
                            .Evaluated = True
                            .GlosaMedicalFeesEvaluation.Add(formulario.GlosaMedicalFeesEvaluation.FirstOrDefault)
                        End If

                    End With
                    INDGcGMedicalFeesD.DataSource = GlosaMedicalFees.GlosaMedicalFeesDetail
                    INDGcGMedicalFeesD.Refresh()
                    GetTotalAndPendingValue()
                    _ctrInfoGlosa.SetDebitAndCredit(AddressOf SetTotalAndPendingValue)
                    _ctrInfoGlosa.RefreshDebitCredit()
                End Using
            Catch ex As Exception
                Throw ex
            End Try

        End If
    End Sub

#End Region

#Region "VisibleChanged"
    ''' <summary>
    ''' Accion que permite que se carguen y muestre la evaluacion en el detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPccMoreInfoAdmission_VisibleChanged(sender As Object, e As EventArgs) Handles INDPccMoreInfoAdmission.VisibleChanged
        Dim ObjAccount = CType(INDGvGMFDetail.GetFocusedRow(), GlosaMedicalFeesDetail)

        If ObjAccount.Evaluated Then
            Dim GlosaMedicalFeesdetail As New GlosaMedicalFeesDetail

            GlosaMedicalFeesdetail = GlosaMedicalFees.GlosaMedicalFeesDetail.Where(Function(x) x.AccountPayableId = ObjAccount.AccountPayableId).FirstOrDefault
            With GlosaMedicalFeesdetail
                INDTeGlossedValue1.Text = ObjAccount.TotalValue
                With .GlosaMedicalFeesEvaluation.FirstOrDefault
                    INDTeSupplierAcceptedValue1.Text = .AcceptedValueProv
                    INDTeRaisedValue1.Text = .RaisedValue
                    INDTePedingValue1.Text = .PendingValue
                    INDMmoObservation11.Text = .Observation
                End With
            End With
        Else
            INDTeGlossedValue1.Text = 0
            INDTeSupplierAcceptedValue1.Text = 0
            INDTeRaisedValue1.Text = 0
            INDTePedingValue1.Text = 0
            INDMmoObservation11.Text = "La Glosa No esta Evaluada"
            Exit Sub

        End If
    End Sub

#End Region

#Region "PopUpShowing"
    ''' <summary>
    ''' Despliega el menu para eliminar. editar o evaluar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvGMFDetail_PopupMenuShowing(sender As Object, e As XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvGMFDetail.PopupMenuShowing
        If Status = 0 OrElse Status = 1 Then
            INDBbIEvaluation.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbIEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            INDBbIDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
        Else
            INDBbIEvaluation.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDBbIEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            INDBbIDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
        End If
        Dim View = CType(sender, GridView)
        INDPopMenuActions.Manager = BarManager1
        INDPopMenuActions.ShowPopup(View.GridControl.PointToScreen(e.Point))
    End Sub
#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina el activo
    ''' </summary>
    Private Sub DeleteItem()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim Objcontract = CType(INDGvGMFDetail.GetFocusedRow(), GlosaMedicalFeesDetail)
            Objcontract.MarkAsDeleted()
            GlosaMedicalFees.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' Limpia las varibles del popUp del detalle
    ''' </summary>
    Private Sub CleanDetaiPopUp()
        INDDeInvoiceDate.EditValue = Nothing
        INDDeInvoiceExpirationDate.EditValue = Nothing
        INDTeInvoiceValue.EditValue = Nothing
        INDTeInvoiceBalance.EditValue = Nothing
        Quantity = 0
        UnitValue = 0
        INDMmoObservation.EditValue = Nothing
        GlosaMedicalFeesConceptId = Nothing
    End Sub

    ''' <summary>
    ''' Ejecuta la validacion para saber si se puede confirmar o no
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateEvaluated() As Boolean
        Dim concat = New System.Text.StringBuilder

        Dim GlosaMedicalFeesDetail As List(Of GlosaMedicalFeesDetail)

        GlosaMedicalFeesDetail = GlosaMedicalFees.GlosaMedicalFeesDetail.Where(Function(x) x.Evaluated = False).ToList()

        If GlosaMedicalFeesDetail.Count > 0 Then
            For Each item In GlosaMedicalFeesDetail
                concat.AppendLine("el concepto " & item.ConceptDescription & "- para la factura " & item.InvoiceNumber)
            Next
            Mensaje(EeventViewerImages.Advertencia) = "No se puede confirmar documento, Falta evaluar : " & concat.ToString
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.GlosaMedicalFees.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.GlosaMedicalFees.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.GlosaMedicalFees.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.GlosaMedicalFees.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.GlosaMedicalFees.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Establece los valor del valor glosado y el valor pendiente en la barra de botones
    ''' </summary>
    ''' <returns></returns>
    Private Function SetTotalAndPendingValue() As Tuple(Of Decimal, Decimal)
        Return New Tuple(Of Decimal, Decimal)(GlossedValue, PendingValue)
    End Function

    ''' <summary>
    ''' Obtiene la suma del valor glosado y valor pendiente total
    ''' </summary>
    Private Sub GetTotalAndPendingValue()
        GlossedValue = 0
        PendingValue = 0
        If GlosaMedicalFees.GlosaMedicalFeesDetail.Count > 0 Then
            GlossedValue += GlosaMedicalFees.GlosaMedicalFeesDetail.Sum(Function(y) y.TotalValue)
            For Each Item In GlosaMedicalFees.GlosaMedicalFeesDetail
                For Each Ev In Item.GlosaMedicalFeesEvaluation
                    PendingValue += Ev.PendingValue
                Next
            Next
        End If
        _ctrInfoGlosa.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Carga los estados de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusLegalized"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Genera una nueva entidad de Contrato
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function NewContract() As Task
        GlosaMedicalFees = New GlosaMedicalFees()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MedicalFeesSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MedicalFeesSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MedicalFeesSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If

    End Function

    ''' <summary>
    ''' Despliega el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Proveedor", .FieldName = "SupplierId.CodeName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Valor Glosado", .FieldName = "GlossedValue", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Valor Pendiente", .FieldName = "PendingValue", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListGlosaMedicalFees
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles y las variables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyGlosaMedicalFees.BeginUpdate()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        ListAccountPayable = Nothing
        INDGcGMedicalFeesD.DataSource = Nothing
        INDSleSupplier.EditValue = 0
        Code = String.Empty
        IdGlosaMedicalFees = 0
        DocumentDate = Nothing
        Description = String.Empty
        SupplierId = 0
        SupplierDistributionLineId = 0
        INDSleSupplier.Properties.NullText = String.Empty
        GlosaMedicalFeesConceptId = 0
        Me.GlosaMedicalFees = New GlosaMedicalFees
        GlossedValue = 0
        PendingValue = 0
        Status = 0
        GetTotalAndPendingValue()
        _isLoading = False
        OnlyRead = False
        ReadOnlyControls(False)
        ActionsOnControls = False
        INDBtnCode.Focus()
        INDlyGlosaMedicalFees.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        DeleteBlockedRecord()
        Dim MedicalFees As GlosaMedicalFeesXpo = ReturnObject
        With MedicalFees
            Code = .Code
            IdGlosaMedicalFees = .Id
        End With
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que carga los controles de la consulta 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MGlosaMedicalFees(CStr(Me.Tag))
                    AsyncLoader(True)
                    GlosaMedicalFees = Await Model.GetGlosaMedicalFeesByCodeAsync(Code)
                    INDlyGlosaMedicalFees.BeginUpdate()
                    If GlosaMedicalFees IsNot Nothing AndAlso GlosaMedicalFees.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(GlosaMedicalFees.Id))

                            _isLoading = True
                            With GlosaMedicalFees
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Description = .Observation
                                SupplierId = .SupplierId
                                INDSleSupplier.Properties.NullText = .DescriptionSupplier
                                Status = .Status

                                INDGcGMedicalFeesD.DataSource = .GlosaMedicalFeesDetail.ToList()
                            End With
                            GetTotalAndPendingValue()
                            _ctrInfoGlosa.SetDebitAndCredit(AddressOf SetTotalAndPendingValue)
                            _ctrInfoGlosa.RefreshDebitCredit()

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.GlosaMedicalFees.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMedicalFees With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = GlosaMedicalFees.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            ActionsOnControls = True
                            If Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                                Me.ReadOnlyControls(False)
                                OnlyRead = False
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                                Me.ReadOnlyControls(True)
                                OnlyRead = True
                                If Status = 2 Then
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
                                End If
                            End If
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, GlosaMedicalFees.Id, 0, GlosaMedicalFees.Id)
                            Me.BarraBotones.SetDocuments(GlosaMedicalFees.Id, Me.Tag.ToString(), Nothing, GetType(GlosaMedicalFees).Name)

                            _isLoading = False
                            AsyncLoader(False)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewContract()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDlyGlosaMedicalFees.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With GlosaMedicalFees
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .GlossedValue = GlossedValue
            .PendingValue = PendingValue
            .SupplierId = SupplierId
            .Observation = Description
            .Status = Me.Status
        End With
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.GlosaMedicalFees IsNot Nothing AndAlso Me.GlosaMedicalFees.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                INDBtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            INDBtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Private Function DesconfirmValidate() As Boolean
        If GlosaMedicalFees IsNot Nothing AndAlso GlosaMedicalFees.GlosaMedicalFeesDetail.Count > 0 Then
            If GlosaMedicalFees.PendingValue = 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = ("No se puede Desconfirmar Debido a que el Valor Pendiente es igual a 0")
                Return False
            Else
                Return True
            End If
        End If
        Return False
        Exit Function
    End Function

#End Region

#Region "BarButtons"

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Status = 1
        varImp = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Status = 2
        varImp = 3
        If ValidateEvaluated() Then
        Else
            Exit Sub
        End If
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Status = 2
        varImp = 3
        If ValidateEvaluated() Then
        Else
            Exit Sub
        End If
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        If DesconfirmValidate() Then
        Else
            Exit Sub
        End If
        Desconfirmar()
    End Sub

    Private Sub BarraBotones_ClickLegalize() Handles BarraBotones.ClickConfirmar
        Status = 4
        varImp = 3
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MedicalFeesSecuenceDetail IsNot Nothing Then
                If Not Me._sequence.MedicalFeesSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

End Class