#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Treasury.MVP
Imports Presentation.Controls
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP
Imports System.Text

#End Region
''' <summary>
''' Clase que contiene la vista del funcional profesionales
''' </summary>
''' <remarks></remarks>
Public Class FrmAgreementsRedemptionPoints
    Implements IAgreementsRedemptionPoints

#Region "Variables"

    '''' <summary>
    '''' Variable que contiene el presentador
    '''' </summary>
    '''' <remarks></remarks>
    Dim Presenter As PAgreementsRedemptionPoints

    '''' <summary>
    '''' Variable que contiene el modelo
    '''' </summary>
    '''' <remarks></remarks>
    'Dim Model As MAgreementsRedemptionPoints

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad de bloqueo de registros
    ''' </summary>
    ''' <remarks></remarks>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Listado de contratos que tiene asociado el médico
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListAgreementsRedemptionPointsDetail As List(Of AgreementsRedemptionPointsDetail)

    ''' <summary>
    ''' Listado de contratos que tiene asociado el médico
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteAgreementsRedemptionPointsDetail As List(Of AgreementsRedemptionPointsDetail)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IAgreementsRedemptionPoints.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.EditValue
            End If
        End Get
        Set(value As String)
            INDbtnCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Nombre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Name As String Implements IAgreementsRedemptionPoints.Name
        Get
            Return INDtxtName.EditValue
        End Get
        Set(value As String)
            INDtxtName.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Public Property State As Integer Implements IAgreementsRedemptionPoints.State
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Integer)
            If value = 1 Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CustomerId As Integer Implements IAgreementsRedemptionPoints.CustomerId
        Get
            Return INDsleCustomer.EditValue
        End Get
        Set(value As Integer)
            INDsleCustomer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CustomerXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAgreementsRedemptionPoints.CustomerXPO
        Get
            Return INDsleCustomer.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCustomer.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la moneda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrencyId As Integer Implements IAgreementsRedemptionPoints.CurrencyId
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del proveedor linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrencyXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAgreementsRedemptionPoints.CurrencyXPO
        Get
            Return INDsleCurrency.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IAgreementsRedemptionPoints.Description
        Get
            Return INDtxtDescription.EditValue
        End Get
        Set(value As String)
            INDtxtDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad de puntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PointsAmount As Integer
        Get
            Return INDTxtPointsAmount.EditValue
        End Get
        Set(value As Integer)
            INDTxtPointsAmount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de los puntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PointsValue As Decimal
        Get
            Return INDTxtPointsValue.EditValue
        End Get
        Set(value As Decimal)
            INDTxtPointsValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date
        Get
            Return INDdeDate.EditValue
        End Get
        Set(value As Date)
            INDdeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date
        Get
            Return INDdeEndDate.EditValue
        End Get
        Set(value As Date)
            INDdeEndDate.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAgreementsRedemptionPoints.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As String Implements IAgreementsRedemptionPoints.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la moneda que esta siendo seleccionada del campo Moneda
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CurrencySelected As Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo
        Get
            Return TryCast(TryCast(SearchLookUpEdit1View1.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonCurrencyXpo)
        End Get
    End Property

    Public Property Sequense As TreasurySequence Implements IAgreementsRedemptionPoints.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene la entidad de la conciliación bancaria
    ''' </summary>
    ''' <remarks></remarks>
    Dim _agreementsRedemptionPoints As AgreementsRedemptionPoints

    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _blockRecord As Domain.Entities.BlockRecordTreasury

    ''' <summary>
    ''' bandera para identificar cuando se consulta un registro
    ''' </summary>
    ''' <remarks></remarks>
    Dim _isLoad As Boolean

    ''' <summary>
    ''' bandera para identificar cuando se consulta la moneda
    ''' </summary>
    Dim _FlagLoad As Boolean

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"
    ''' <summary>
    ''' Bandera que me permite editar una caja
    ''' </summary>
    Private _bandEditDetail As Boolean

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' obtiene la moneda a la cual se realiza el reporte 
    ''' </summary>
    Dim CurrencyData As Currency

    ''' <summary>
    ''' abreviacion de la moneda 
    ''' </summary>
    Dim CurrencyAbbreviation As String

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que agrega un contrato profesional de la salud
    ''' a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetails()
        Dim _AgreementsRedemptionPointsDetail As AgreementsRedemptionPointsDetail
        If _bandEditDetail Then
            _AgreementsRedemptionPointsDetail = CType(viewDetails.GetFocusedRow(), AgreementsRedemptionPointsDetail)
        Else
            _AgreementsRedemptionPointsDetail = New AgreementsRedemptionPointsDetail
        End If
        With _AgreementsRedemptionPointsDetail
            .PointsAmount = PointsAmount
            .PointsValue = PointsValue
            .InitialDate = InitialDate
            .EndDate = EndDate
        End With
        If Not _bandEditDetail Then
            _agreementsRedemptionPoints.AgreementsRedemptionPointsDetail.Add(_AgreementsRedemptionPointsDetail)
        Else
            _agreementsRedemptionPoints.MarkAsModified()
        End If
        INDgcAgreementsRedemptionPointsDetail.DataSource = _agreementsRedemptionPoints.AgreementsRedemptionPointsDetail.ToList()
        ListAgreementsRedemptionPointsDetail = _agreementsRedemptionPoints.AgreementsRedemptionPointsDetail.ToList()
        INDgcAgreementsRedemptionPointsDetail.RefreshDataSource()
        If _agreementsRedemptionPoints.AgreementsRedemptionPointsDetail.Count > 0 Then
            INDLciCurrency.Enabled = False
        End If
        CleanControlsDetails()
    End Sub

    ''' <summary>
    ''' Cleans the controls details.
    ''' </summary>
    Public Sub CleanControlsDetails()
        PointsAmount = 0
        PointsValue = 0D
        InitialDate = (Date.Now).ToString("d")
        EndDate = (Date.Now).ToString("d")
    End Sub

    ''' <summary>
    ''' Metodo que elimina un contrato profesional de la salud de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteMedicalFeesContract()
        Dim AgreementsRedemptionPointsDetail As AgreementsRedemptionPointsDetail = viewDetails.GetFocusedRow
        ListAgreementsRedemptionPointsDetail.Remove(AgreementsRedemptionPointsDetail)
        If AgreementsRedemptionPointsDetail.Id > 0 Then
            If ListDeleteAgreementsRedemptionPointsDetail Is Nothing Then
                ListDeleteAgreementsRedemptionPointsDetail = New List(Of AgreementsRedemptionPointsDetail)
            End If
            AgreementsRedemptionPointsDetail.MarkAsDeleted()
            ListDeleteAgreementsRedemptionPointsDetail.Add(AgreementsRedemptionPointsDetail)
        End If
        INDgcAgreementsRedemptionPointsDetail.DataSource = Nothing
        INDgcAgreementsRedemptionPointsDetail.DataSource = ListAgreementsRedemptionPointsDetail
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtName.Enabled = value
            INDtxtDescription.Enabled = value
            INDsleCurrency.Enabled = value
            INDsleCustomer.Enabled = value
            INDpceDetails.Enabled = value
            INDTxtPointsAmount.Enabled = value
            INDTxtPointsValue.Enabled = value
            INDdeDate.Enabled = value
            INDdeEndDate.Enabled = value
            INDbtnAddDetails.Enabled = value
            INDgcAgreementsRedemptionPointsDetail.Enabled = value

            If value = False Then
                INDbtnCode.Focus()
            Else
                INDtxtName.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Loads the status.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlyHealthProfessional.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        Name = String.Empty
        Description = String.Empty
        CustomerId = Nothing
        CurrencyId = indigo.OfficialCurrencyId
        INDsleCurrency.Properties.NullText = indigo.CurrencyISO4217
        INDsleCustomer.Properties.NullText = ""
        _FlagLoad = False
        SetFormatCurrencyUI(indigo.CurrencyISO4217)

        INDgcAgreementsRedemptionPointsDetail.DataSource = Nothing
        INDLciCurrency.Enabled = True
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.CleanAuditBasic()
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDlyHealthProfessional.EndUpdate()
        _bandEditDetail = False
        ListAgreementsRedemptionPointsDetail = Nothing
        ListDeleteAgreementsRedemptionPointsDetail = Nothing
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MAgreementsRedemptionPoints(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetAgreementsRedemptionPoints(INDbtnCode.Text.Trim)
                    If resultOperation IsNot Nothing Then
                        _agreementsRedemptionPoints = resultOperation
                        If _agreementsRedemptionPoints IsNot Nothing AndAlso _agreementsRedemptionPoints.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                                _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_agreementsRedemptionPoints.Id))
                                With _agreementsRedemptionPoints
                                    LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                    Me._isLoad = True
                                    _FlagLoad = True
                                    Code = .Code
                                    Name = .Name
                                    Description = .Description
                                    CustomerId = .CustomerId
                                    INDsleCustomer.Properties.NullText = .Customer.Name
                                    CurrencyId = .CurrencyId
                                    INDsleCurrency.Properties.NullText = .Currency.Abbreviation
                                    SetFormatCurrencyUI(.Currency.Abbreviation)
                                    Me.colPoinstValue = Window.Utils.FormatGrid(Me.colPoinstValue, .Currency.Abbreviation)
                                    Dim result = Await Model.ListAgreementsRedemptionPointsDetail(_agreementsRedemptionPoints.Id)
                                    If result.Any() Then
                                        ListAgreementsRedemptionPointsDetail = result
                                        INDgcAgreementsRedemptionPointsDetail.DataSource = Nothing
                                        INDgcAgreementsRedemptionPointsDetail.DataSource = ListAgreementsRedemptionPointsDetail
                                    End If
                                    Me._isLoad = False
                                    _FlagLoad = False

                                    Me.BarraBotones.StatusRecord = If(.State, eActionsStatusRecords.Active, eActionsStatusRecords.Inactive)
                                End With
                                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._agreementsRedemptionPoints.Code)
                                If _blockRecord.Id = 0 Then
                                    _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                            New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                                .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _agreementsRedemptionPoints.Id})
                                            ).ObjectEmbbeded
                                Else
                                    Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                                End If
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                                AsyncLoader(False)
                                ActionsOnControls = True
                            End Using
                        Else
                            AsyncLoader(False)
                            If Me.Sequense.IsManual Then
                                'Me.Ne()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Code = String.Empty
                                INDbtnCode.Focus()
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Método para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Sub AssigningValues()
        With _agreementsRedemptionPoints
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name
            .Description = Description
            .CustomerId = CustomerId
            .CurrencyId = CurrencyId

            Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
                Case eActionsStatusRecords.Active
                    .State = 1
                Case eActionsStatusRecords.Inactive
                    .State = 0
            End Select


            If ListAgreementsRedemptionPointsDetail IsNot Nothing Then
                For Each itemDetail As AgreementsRedemptionPointsDetail In ListAgreementsRedemptionPointsDetail
                    If itemDetail.Id > 0 Then
                        itemDetail.MarkAsModified()
                    End If
                    .AgreementsRedemptionPointsDetail.Add(itemDetail)
                Next
                If ListAgreementsRedemptionPointsDetail.Any(Function(d) d.Id > 0) Then
                    .MarkAsModified()
                End If
            End If

            If ListDeleteAgreementsRedemptionPointsDetail IsNot Nothing Then
                For Each itemDetail As AgreementsRedemptionPointsDetail In ListDeleteAgreementsRedemptionPointsDetail
                    .AgreementsRedemptionPointsDetail.Add(itemDetail)
                Next
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me._agreementsRedemptionPoints.Code) Then
            Try
                Using Model As New MAgreementsRedemptionPoints(Me.Tag)
                    Dim status As Integer
                    Select Case _agreementsRedemptionPoints.State
                        Case 1 'activo
                            status = False
                        Case 2 'inactivo
                            status = True
                    End Select
                    AsyncLoader(True)
                    Dim Result = Await Model.ChangeState(Me._agreementsRedemptionPoints.Code, status)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = Result.Message
                    End If
                    AsyncLoader(False)
                    CleanControls()
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

#End Region

#Region "Events"

#Region "KeyDown"

    ''' <summary>
    ''' Evento cuando se presiona una tecla en el código del paciente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDgcAgreementsRedemptionPointsDetail.KeyDown, INDbtnCode.KeyDown
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
                    Await Me.NewAgreementsRedemptionPoints()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 al control del popup container
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceMedicalFeesContract_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceDetails.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceDetails.ShowPopup()
        End If
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Carga los contratos cuando se abra el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCustomer_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCustomer.QueryPopUp
        If CustomerXpo Is Nothing Then
            CustomerXpo = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).GlosasService.GetCustomers()
        End If
    End Sub

    ''' <remarks></remarks>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If CurrencyXpo Is Nothing Then
            CurrencyXpo = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub

#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        SearchMode = Nothing
        _agreementsRedemptionPoints = Nothing
        CustomerId = Nothing
        CurrencyId = Nothing
        ListAgreementsRedemptionPointsDetail = Nothing
        ListDeleteAgreementsRedemptionPointsDetail = Nothing
        Description = Nothing
        Name = Nothing
        _bandEditDetail = False

    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProfessional_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyHealthProfessional, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAgreementsRedemptionPoints(Me)
        Presenter.GetSequense()
        '******* Inicializo Controles**********
        'InitializeTuples()
        LoadStatus()
        Deshacer()

        Dim listActions As New List(Of eAcciones)()
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewDetails, listActions)

        SearchMode = False
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmHealthCareProfessional_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceMedicalFeesContract_Popup(sender As Object, e As EventArgs) Handles INDpceDetails.Popup
        INDTxtPointsAmount.Focus()
        If Not _bandEditDetail Then
            EndDate = (Date.Now).ToString("d")
            InitialDate = (Date.Now).ToString("d")
        End If
    End Sub

#End Region

#End Region
#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag)
            Case "Add", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Add")
                INDpceDetails.ShowPopup()
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                EditDetails()
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteDetails()
        End Select
    End Sub
#End Region
#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        Dim actionResult As ActionResult
        If _agreementsRedemptionPoints IsNot Nothing Then
            If _agreementsRedemptionPoints.Code IsNot String.Empty Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Try
                        Using Model As New MAgreementsRedemptionPoints(Me.Tag)
                            AsyncLoader(True)
                            actionResult = Await Model.DeleteAgreementsRedemptionPoints(_agreementsRedemptionPoints)
                            AsyncLoader(False)
                            If actionResult.StateResult = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                                SearchMode = False
                                Deshacer()
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = actionResult.Message
                            End If

                        End Using
                    Catch ex As Exception
                        Throw ex
                        AsyncLoader(False)
                    End Try
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectProfessional", NAME_MODULE)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectProfessional", NAME_MODULE)
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Dim StatusOperation As Domain.Base.Entities.ObjectState
        If ValidateControls() = False Then
            Exit Sub
        End If
        'Await LoadSpecialtyToPropertyEntity()
        AssigningValues()
        StatusOperation = _agreementsRedemptionPoints.ChangeTracker.State
        Try
            Using Model As New MAgreementsRedemptionPoints(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveAgreementsRedemptionPoints(Me._agreementsRedemptionPoints, Me._idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If StatusOperation = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format("Se actualizó de manera correcta el convenio:", _agreementsRedemptionPoints.Code.Trim)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = String.Format("Se guardó de manera correcta el convenio:", _agreementsRedemptionPoints.Code.Trim)
                    End If
                    'Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    SearchMode = False
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewAgreementsRedemptionPoints() As Task
        _agreementsRedemptionPoints = New AgreementsRedemptionPoints() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MCommonTreasury(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    INDpceDetails.Enabled = False
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewAgreementsRedemptionPoints()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Using Model As New MAgreementsRedemptionPoints(Me.Tag)
            FormSearchObjects = New FrmBusqueda
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
            With FormSearchObjects
                .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                                  New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6)}}.ToList
                .ValorSolicitado = "Code"
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAgreementsRedemptionPoints
                .FormParent = Me
                .ShowSearch()
            End With
        End Using
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
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
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub INDbtnAddDetails_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetails.Click
        If ValidateDetails() Then
            AddDetails()
        End If
    End Sub

    ''' <summary>
    ''' Valida los campos que vienen de Detalles del convenio
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateDetails() As Boolean
        Dim errorList As New StringBuilder()
        If INDTxtPointsAmount.EditValue Is Nothing OrElse Me.PointsAmount = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cantidad de Puntos"))
        End If
        If INDTxtPointsValue.EditValue Is Nothing OrElse Me.PointsValue = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Valor de Puntos"))
        End If
        If INDdeDate.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha Inicial"))
        End If
        If INDdeEndDate.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha Final"))
        End If

        If errorList.Length = 0 Then
            Return True
        Else
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
    End Function

    Private Sub EditDetails()
        _bandEditDetail = True
        Dim _AgreementsRedemptionPointsDetail As AgreementsRedemptionPointsDetail = CType(viewDetails.GetFocusedRow(), AgreementsRedemptionPointsDetail)
        PointsAmount = _AgreementsRedemptionPointsDetail.PointsAmount
        PointsValue = _AgreementsRedemptionPointsDetail.PointsValue
        InitialDate = _AgreementsRedemptionPointsDetail.InitialDate.ToString("d")
        EndDate = _AgreementsRedemptionPointsDetail.EndDate.ToString("d")
        INDpceDetails.ShowPopup()
    End Sub

    Private Sub INDpceDetails_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceDetails.CloseUp
        If _bandEditDetail Then
            CleanControlsDetails()
            _bandEditDetail = False
        End If
    End Sub

    ''' <summary>
    ''' Deletes the cash from grid.
    ''' </summary>
    Private Sub DeleteDetails()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _AgreementsRedemptionPointsDetail As AgreementsRedemptionPointsDetail = CType(viewDetails.GetFocusedRow(), AgreementsRedemptionPointsDetail)
            If _AgreementsRedemptionPointsDetail.Id <> 0 Then
                If ListDeleteAgreementsRedemptionPointsDetail Is Nothing Then
                    ListDeleteAgreementsRedemptionPointsDetail = New List(Of AgreementsRedemptionPointsDetail)
                End If
                _AgreementsRedemptionPointsDetail.MarkAsDeleted()
                ListDeleteAgreementsRedemptionPointsDetail.Add(_AgreementsRedemptionPointsDetail)
            End If
            ListAgreementsRedemptionPointsDetail.Remove(_AgreementsRedemptionPointsDetail)
            INDgcAgreementsRedemptionPointsDetail.DataSource = ListAgreementsRedemptionPointsDetail
            INDgcAgreementsRedemptionPointsDetail.RefreshDataSource()
        End If
    End Sub

    Private Sub INDdeEndDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeEndDate.EditValueChanged
        If InitialDate > EndDate Then
            Mensaje(EeventViewerImages.Informacion) = "La fecha final no puede ser menor a la fecha inicial"
            EndDate = (Date.Now).ToString("d")
        End If
    End Sub

    Private Sub INDdeDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeDate.EditValueChanged
        If InitialDate > EndDate Then
            Mensaje(EeventViewerImages.Informacion) = "La fecha Inicial no puede ser mayor a la fecha final"
            InitialDate = (Date.Now).ToString("d")
        End If
    End Sub

    Private Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If CurrencyId > 0 Then
            INDpceDetails.Enabled = True
        End If

        If Not _FlagLoad Then
            SetFormatCurrencyUI(CurrencySelected?.Abbreviation)
        End If

    End Sub

    ''' <summary>
    ''' Establece el formato de la moneda que venga o la de por defecto
    ''' </summary>
    ''' <param name="currencyAbbreviation"></param>
    Private Sub SetFormatCurrencyUI(currencyAbbreviation As String)
        If String.IsNullOrEmpty(currencyAbbreviation) Then
            currencyAbbreviation = indigo.CurrencyISO4217
        End If
        Me.changeNumericFormatByCurrency(currencyAbbreviation.GetNumberFormat)
        Me.changeNumericFormatByCurrency(currencyAbbreviation.GetNumberFormat, Me.INDlyPopup.Controls)
    End Sub
#End Region

End Class
