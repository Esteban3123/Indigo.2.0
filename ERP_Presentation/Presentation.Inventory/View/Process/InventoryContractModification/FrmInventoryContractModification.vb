#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmInventoryContractModification
    Implements IInventoryContractModification

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        _ctrTmp = New CtrContractTotalInfo()
        _ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        _ctrTmp.PrintInfo()
        _ctrTmp.TextIvaValue = String.Empty
        _ctrTmp.TextDiscountValue = String.Empty
        _ctrTmp.INDPceNetValue.Visible = False
        _ctrTmp.INDPceTotalValue.Size = New Size(292, 52)
        _ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_ctrTmp)
    End Sub

    ''' <summary>
    ''' Devuelve el listado para mostrar el Total del contrato y sus derivados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, String, String)
        Return New Tuple(Of String, String, String, String)("0", "0", Value.ToString(), "0")
    End Function

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

#End Region

#Region "Globals"

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private _ctrTmp As CtrContractTotalInfo

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PInventoryContractModification

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numérica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As BlockRecordInventory

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Private _paymentsSettings As PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Representa la entidad de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim _InventoryContractModification As InventoryContractModification

    ''' <summary>
    ''' Variable para identificar si se esta cargando un registro
    ''' </summary>
    Dim _isLoading As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim _varImp As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IInventoryContractModification.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInventoryContractModification.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Public Property Sequense As InventorySequence Implements IInventoryContractModification.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces el codigo del tipo de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IInventoryContractModification.Code
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
    ''' Obtiene o estableces la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IInventoryContractModification.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Establece el Id de Contrato de Inventarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractId As Integer Implements IInventoryContractModification.ContractId
        Get
            Return INDSleContract.EditValue
        End Get
        Set(value As Integer)
            INDSleContract.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de modificación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModificationType As Byte Implements IInventoryContractModification.ModificationType
        Get
            Return INDGleModificationType.EditValue
        End Get
        Set(value As Byte)
            INDGleModificationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la fecha final de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date? Implements IInventoryContractModification.EndDate
        Get
            Return INDdeEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdeEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el subtotal del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Value As Decimal Implements IInventoryContractModification.Value
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estableces la descripcion de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IInventoryContractModification.Description
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
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As String Implements IInventoryContractModification.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

#Region "Budget Interface"

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Dim _budgetInterface As Boolean
            If _paymentsSettings IsNot Nothing Then
                If _paymentsSettings.BudgetInterface AndAlso (ModificationType = 1 OrElse ModificationType = 3) Then
                    _budgetInterface = True
                End If
            End If
            Return _budgetInterface
        End Get
    End Property

    Public ReadOnly Property ObligationBudgetInterface As Boolean
        Get
            Return If(_paymentsSettings Is Nothing, False, _paymentsSettings.ObligationBudgetInterface)
        End Get
    End Property

    Public Property BudgetaryEntityId As Integer?
        Get
            Return If(String.IsNullOrEmpty(INDSleBudgetaryEntityId.EditValue), Nothing, INDSleBudgetaryEntityId.EditValue)
        End Get
        Set(value As Integer?)
            INDSleBudgetaryEntityId.EditValue = value
        End Set
    End Property

    Public Property BudgetaryValidityId As Integer?
        Get
            Return If(String.IsNullOrEmpty(INDSleBudgetaryValidityId.EditValue), Nothing, INDSleBudgetaryValidityId.EditValue)
        End Get
        Set(value As Integer?)
            INDSleBudgetaryValidityId.EditValue = value
        End Set
    End Property

    Public Property ListContractAvailability As List(Of InventoryContractModificationAvailability) Implements IInventoryContractModification.ListContractAvailability
        Get
            Return INDgcAvailability.DataSource
        End Get
        Set(value As List(Of InventoryContractModificationAvailability))
            INDgcAvailability.DataSource = value
            INDgcAvailability.RefreshDataSource()
        End Set
    End Property

    Private ListDeleteContractAvailability As List(Of InventoryContractModificationAvailability)

#End Region

#End Region

#Region "Datasources"

    Private _listModificationType As List(Of Tuple(Of Byte, String))
    ReadOnly Property ListModificationType As List(Of Tuple(Of Byte, String))
        Get
            If _listModificationType Is Nothing Then
                _listModificationType = New List(Of Tuple(Of Byte, String))
                _listModificationType.Add(New Tuple(Of Byte, String)(1, "Valor"))
                _listModificationType.Add(New Tuple(Of Byte, String)(2, "Fecha de Terminación"))
                _listModificationType.Add(New Tuple(Of Byte, String)(3, "Valor y Fecha de Terminación"))
            End If
            Return _listModificationType
        End Get
    End Property

    Public Property ListContractInventory As XPInstantFeedbackSource Implements IInventoryContractModification.ListContractInventory
        Get
            Return INDSleContract.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleContract.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryEntityXpo As DevExpress.Xpo.XPCollection Implements IInventoryContractModification.BudgetaryEntityXpo
        Get
            Return INDSleBudgetaryEntityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryEntityId.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetaryValidityXpo As XPCollection Implements IInventoryContractModification.BudgetaryValidityXpo
        Get
            Return INDSleBudgetaryValidityId.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDSleBudgetaryValidityId.Properties.DataSource = value
        End Set
    End Property

    Public Property AvailabilityXpo As XPInstantFeedbackSource Implements IInventoryContractModification.AvailabilityXpo
        Get
            Return INDsleAvailability.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAvailability.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Eliminar.
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click Guardar.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If _InventoryContractModification.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            End If
            If Not ValidateDetails() Then
                Exit Sub
            End If
            If Not ValidateInterfaceBudget() Then
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Using model As New MInventoryContractModification(MyTag)
                AsyncLoader(True)
                Dim Result = Await model.SaveInventoryContractModification(_InventoryContractModification)
                AsyncLoader(False)
                If Result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message

                    _InventoryContractModification = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case _varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _InventoryContractModification.Id, 0, _InventoryContractModification.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _InventoryContractModification.Id, 0, _InventoryContractModification.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _InventoryContractModification.Id, 0, _InventoryContractModification.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _InventoryContractModification.Id, 0, _InventoryContractModification.Id)
                    End Select
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

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
            Await NewInventoryContractModification()
        End If
    End Sub

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
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code"},
                              New ColumnInfo With {.Caption = "Fecha Documento", .FieldName = "DocumentDate"},
                              New ColumnInfo With {.Caption = "Contrato", .FieldName = "ContractId.ContractNumber"},
                              New ColumnInfo With {.Caption = "Tipo Modificación", .FieldName = "ModificationTypeName"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName"}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInventoryContractModification
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ReturnValue As String, ReturnObject As Object)
        Await DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

#End Region

#Region "Methods"

    Private Sub LoadPaymentsSetting()
        Try
            AsyncLoader(True)
            'Se obtiene los parámetros de pagos por unidad operativa
            If BarraBotones.OperatingUnitValue <> Nothing AndAlso BarraBotones.OperatingUnitValue > 0 Then
                _paymentsSettings = _presenter.GetSettingsPaymentsByOperatingUnitId(BarraBotones.OperatingUnitValue)
            End If

            ShowHideBudgetInterface()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "Se presentó un error al cargar los datos de la unidad operativa"
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements IInventoryContractModification.ActionsOnControls
        Set(value As Boolean)
            INDlyContract.BeginUpdate()

            ' Cabecera
            INDBtnCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDSleContract.Enabled = value
            INDMmoDescription.Enabled = value
            ' Detalles del Contrato
            INDGleModificationType.Enabled = value
            INDdeEndDate.Enabled = value
            INDtxtValue.Enabled = value
            ' Detalles Presupuestales
            INDSleBudgetaryEntityId.Enabled = value
            INDSleBudgetaryValidityId.Enabled = value
            INDsleAvailability.Enabled = value
            INDbtnAddAvailability.Enabled = value
            INDgcAvailability.Enabled = value

            INDlyContract.EndUpdate()
            If value Then
                INDdeDocumentDate.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    Private Sub ShowHideBudgetInterface()
        INDLciBudgetaryEntityId.AllowHide = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDLciBudgetaryEntityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDLciBudgetaryValidityId.AllowHide = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDLciBudgetaryValidityId.ShowInCustomizationForm = Not (BudgetInterface AndAlso ObligationBudgetInterface)
        INDlygBudget.Visibility = If(BudgetInterface, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
    End Sub

    Private Async Sub CleanControls()
        INDlyContract.BeginUpdate()
        Await DeleteBlockedRecord()

        INDBtnCode.Text = String.Empty
        INDdeDocumentDate.EditValue = Nothing
        INDSleContract.EditValue = Nothing
        INDSleContract.Properties.NullText = Nothing
        INDMmoDescription.EditValue = Nothing

        INDGleModificationType.EditValue = Nothing
        INDGleModificationType.Properties.NullText = String.Empty
        INDdeEndDate.EditValue = Nothing
        INDtxtValue.EditValue = 0

        INDsleAvailability.EditValue = Nothing
        INDgcAvailability.DataSource = Nothing

        _doc = Nothing
        _isLoading = False
        _InventoryContractModification = Nothing
        ListContractAvailability = Nothing
        ListDeleteContractAvailability = Nothing
        _ctrTmp.PrintInfo()

        ReadOnlyControls(False)
        ActionsOnControls = False
        INDBtnCode.Focus()

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing

        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        INDlyContract.EndUpdate()
    End Sub

    Private Sub CleanBudgetInterface(level As Integer)
        If level < 1 Then
            BudgetaryEntityId = Nothing
            INDSleBudgetaryEntityId.Properties.NullText = String.Empty
        End If
        If level < 2 Then
            BudgetaryValidityId = Nothing
            INDSleBudgetaryValidityId.Properties.NullText = String.Empty
            BudgetaryValidityXpo = Nothing
        End If
        If level < 3 Then
            If Not _isLoading Then
                If ListContractAvailability IsNot Nothing AndAlso ListContractAvailability.Any() Then
                    If ListDeleteContractAvailability Is Nothing Then
                        ListDeleteContractAvailability = New List(Of InventoryContractModificationAvailability)
                    End If

                    For Each AccountPayableCommitment In ListContractAvailability
                        If AccountPayableCommitment.Id > 0 Then
                            ListDeleteContractAvailability.Add(AccountPayableCommitment)
                        End If
                    Next
                End If
                ListContractAvailability = Nothing
                _InventoryContractModification.InventoryContractModificationAvailability.Clear()
            End If
        End If
    End Sub

    Sub SetFirstOrDefaultEntity()
        If BudgetaryEntityXpo IsNot Nothing AndAlso BudgetaryEntityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryEntityXpo Where l.Status = 1 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryEntityId = item.Id
            End If
        End If
    End Sub

    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
            End If
        End If
    End Sub

    Private Sub EnableBudgetInterface()
        If _InventoryContractModification IsNot Nothing AndAlso _InventoryContractModification.Status <> 1 Then
            Exit Sub
        End If

        Dim status As Boolean = False
        If ListContractAvailability IsNot Nothing AndAlso ListContractAvailability.Any() Then
            status = True
        End If

        INDSleBudgetaryEntityId.Properties.ReadOnly = status
        INDSleBudgetaryValidityId.Properties.ReadOnly = status
    End Sub

    Private Function ValidateDetails() As Boolean
        Dim listErrors As New StringBuilder

        If ModificationType = 1 OrElse ModificationType = 3 Then
            If Value <= 0 Then
                listErrors.AppendLine("Debe establecer un Valor válido del Otro si del Contrato")
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

    Private Function ValidateInterfaceBudget() As Boolean
        If BudgetInterface Then
            Dim listErrors As New StringBuilder

            If ObligationBudgetInterface Then
                If ListContractAvailability Is Nothing OrElse Not ListContractAvailability.Any(Function(d) d.Value > 0) Then
                    listErrors.AppendLine("Debe agregar al menos una disponibilidad con el cual realizar el compromiso presupuestal")
                End If
            End If

            If ListContractAvailability IsNot Nothing AndAlso ListContractAvailability.Any Then
                Dim contractValue As Decimal = Math.Round(Value, 0, MidpointRounding.AwayFromZero)
                Dim commitmentValue As Decimal = ListContractAvailability.Where(Function(d) d.Value > 0).Sum(Function(d) d.Value)
                If commitmentValue > contractValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales superan el valor del otro si de contrato")
                ElseIf commitmentValue < contractValue Then
                    listErrors.AppendLine("La suma de los valores de los detalles presupuestales son menores al valor del otro si de contrato")
                End If
            End If

            If listErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub DeleteAvailability()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim entity As InventoryContractModificationAvailability = INDviewAvailability.GetFocusedRow()
        ListContractAvailability.Remove(entity)

        If entity.Id > 0 Then
            If ListDeleteContractAvailability Is Nothing Then
                ListDeleteContractAvailability = New List(Of InventoryContractModificationAvailability)
            End If
            entity.MarkAsDeleted()
            ListDeleteContractAvailability.Add(entity)
        End If

        EnableBudgetInterface()
        INDgcAvailability.RefreshDataSource()
    End Sub

    Private Async Function NewInventoryContractModification() As Task
        _InventoryContractModification = New InventoryContractModification() With {.Status = 1}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If
    End Function

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._InventoryContractModification.Code, Me._InventoryContractModification.ContractNumber),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._InventoryContractModification.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._InventoryContractModification.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._InventoryContractModification.Code, Me._InventoryContractModification.ContractNumber)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._InventoryContractModification.Code)
            Return Me._doc
        End If
    End Function

    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        End If
    End Function

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._InventoryContractModification IsNot Nothing AndAlso Me._InventoryContractModification.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
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

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MInventoryContractModification(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetInventoryContractModification(INDBtnCode.Text.Trim)
                    If Not resultOperation.StateResult Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = resultOperation.Message
                        Deshacer()
                        Exit Function
                    End If

                    _InventoryContractModification = resultOperation.ObjectEmbbeded
                    INDlyContract.BeginUpdate()
                    If _InventoryContractModification IsNot Nothing AndAlso _InventoryContractModification.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            With _InventoryContractModification
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                _isLoading = True
                                Me._idOperativeUnit = .OperatingUnitId
                                Me.BarraBotones.OperatingUnitValue = .OperatingUnitId
                                Code = .Code
                                DocumentDate = .DocumentDate
                                ContractId = .ContractId
                                INDSleContract.Properties.NullText = .ContractNumber
                                Description = .Description
                                ModificationType = .ModificationType
                                EndDate = .EndDate
                                Value = .Value
                                Status = .Status.ToString()
                                Me.BarraBotones.StatusRecordVisible = True
                                _ctrTmp.PrintInfo()

                                If BudgetInterface Then
                                    BudgetaryEntityId = .BudgetaryEntityId
                                    INDSleBudgetaryEntityId.Properties.NullText = .BudgetaryEntityDescription
                                    BudgetaryValidityId = .BudgetaryValidityId
                                    INDSleBudgetaryValidityId.Properties.NullText = .BudgetaryValidityDescription
                                    ListContractAvailability = .InventoryContractModificationAvailability.ToList()

                                    EnableBudgetInterface()
                                End If
                                _isLoading = False
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._InventoryContractModification.Code)

                            _blockRecord = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_InventoryContractModification.Id))
                            If _blockRecord.Id = 0 Then
                                _blockRecord = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _InventoryContractModification.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                            End If

                            If _InventoryContractModification.Status = 1 Then 'Registrado
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else 'Confirmado o Anulado
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                ReadOnlyControls(True)
                            End If

                            Me.BarraBotones.SetDocuments(_InventoryContractModification.Id, Me.Tag.ToString(), Nothing, GetType(InventoryContractAssignment).Name)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _InventoryContractModification.Id, 0, _InventoryContractModification.Id)

                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInventoryContractModification()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDlyContract.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Sub AssigningValues()
        With _InventoryContractModification
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .Code = Code
            .DocumentDate = DocumentDate
            .ContractId = ContractId
            .Description = Description
            .ModificationType = ModificationType
            .EndDate = If(ModificationType = 2 OrElse ModificationType = 3, EndDate, Nothing)
            .Value = If(ModificationType = 1 OrElse ModificationType = 3, Value, 0)

            If BudgetInterface Then
                If ListContractAvailability IsNot Nothing Then
                    _InventoryContractModification.BudgetaryEntityId = BudgetaryEntityId
                    _InventoryContractModification.BudgetaryEntityDescription = INDSleBudgetaryEntityId.Text
                    _InventoryContractModification.BudgetaryValidityId = BudgetaryValidityId
                    _InventoryContractModification.BudgetaryValidityDescription = INDSleBudgetaryValidityId.Text

                    For Each contractAvailability In ListContractAvailability
                        If contractAvailability.Value > 0 Then
                            If contractAvailability.ChangeTracker.State = ObjectState.Added Then
                                _InventoryContractModification.InventoryContractModificationAvailability.Add(contractAvailability)
                            ElseIf contractAvailability.ChangeTracker.State = ObjectState.Modified Then
                                contractAvailability.MarkAsModified()
                            End If
                        Else
                            contractAvailability.MarkAsDeleted()
                        End If
                    Next
                End If
            End If

            If ListDeleteContractAvailability IsNot Nothing Then
                For Each contractAvailability In ListDeleteContractAvailability
                    contractAvailability.MarkAsDeleted()
                    _InventoryContractModification.InventoryContractModificationAvailability.Add(contractAvailability)
                Next
            End If
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Se dispara cuando incial la carga del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmInventoryContractModification_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyContract, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        _presenter = New PInventoryContractModification(Me)
        '******************************
        AsyncLoader(True)
        Await _presenter.GetSequense()
        LoadPaymentsSetting()
        AsyncLoader(False)
        '******************************

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDviewAvailability, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewAvailability.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        INDGleModificationType.Properties.DataSource = ListModificationType

        LoadStatus()
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _ctrTmp = Nothing
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _blockRecord = Nothing
        _InventoryContractModification = Nothing
        _isLoading = Nothing
        _varImp = Nothing
        _listModificationType = Nothing
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Se dispara cuando el formulario se activa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInventoryContractModification_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        INDBtnCode.Focus()
        INDdeDocumentDate.Properties.MaxValue = GetDateServer()
    End Sub

#End Region

#Region "FromClosing"

    ''' <summary>
    ''' Se dispara cuando el formulario se va a cerrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmInventoryContractModification_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
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
                    Await Me.NewInventoryContractModification()
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

    Private Sub INDSleContract_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleContract.QueryPopUp
        If INDSleContract.Properties.DataSource Is Nothing Then
            _presenter.LoadContractInventory()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryEntityId.QueryPopUp
        _presenter.InitializeBudgetaryEntity()
    End Sub

    Private Sub INDsleBudgetaryValidityId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBudgetaryValidityId.QueryPopUp
        _presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
    End Sub

    Private Sub INDsleAvailability_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAvailability.QueryPopUp
        If BudgetaryEntityId IsNot Nothing Then
            _presenter.InitializeAvailabilityDetails(BudgetaryEntityId)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDSleContract_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleContract.EditValueChanged
        If INDSleContract.EditValue IsNot Nothing AndAlso INDSleContract.EditValue <> 0 AndAlso Not _isLoading Then
            Dim contract = DirectCast(INDSleContract.GetSelectedObject(), Infrastructure.Data.Xpo.InventoryRepository.InventoryContractXpo)
            If contract Is Nothing Then
                contract = _presenter.GetContractById(ContractId)
            End If

            INDdeEndDate.Properties.MinValue = contract.EndDate
        End If
    End Sub

    Private Sub INDGleModificationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleModificationType.EditValueChanged
        EndDate = Nothing
        Value = 0

        If ModificationType = 1 Then
            INDLciEndDate.Visibility = XtraLayout.Utils.LayoutVisibility.Never
            INDLciValue.Visibility = XtraLayout.Utils.LayoutVisibility.Always
        ElseIf ModificationType = 2 Then
            INDLciEndDate.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            INDLciValue.Visibility = XtraLayout.Utils.LayoutVisibility.Never
        ElseIf ModificationType = 3 Then
            INDLciEndDate.Visibility = XtraLayout.Utils.LayoutVisibility.Always
            INDLciValue.Visibility = XtraLayout.Utils.LayoutVisibility.Always
        End If

        ShowHideBudgetInterface()
        CleanBudgetInterface(2)

        If BudgetInterface Then
            _presenter.InitializeBudgetaryEntity()
            Me.SetFirstOrDefaultEntity()
        End If
    End Sub

    Private Sub INDsleBudgetaryEntityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryEntityId.EditValueChanged
        CleanBudgetInterface(1)

        If BudgetaryEntityId IsNot Nothing Then
            _presenter.InitializeBudgetaryValidity(BudgetaryEntityId)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    Private Sub INDsleBudgetaryValidityId_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBudgetaryValidityId.EditValueChanged
        CleanBudgetInterface(2)

        If BudgetaryValidityId IsNot Nothing Then
            _presenter.InitializeAvailabilityDetails(BudgetaryValidityId)
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se ejecuta al cambiar el valor del control de repositorio de la rejilla para el valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e IsNot Nothing AndAlso e.NewValue IsNot Nothing Then
            Dim entity As InventoryContractModificationAvailability = INDviewAvailability.GetFocusedRow()
            If CDec(e.NewValue) > CDec(entity.Balance) Then
                Mensaje(EeventViewerImages.Advertencia) = "El valor a ejecutar no puede ser mayor al saldo de la disponibilidad"
                e.Cancel = True
                Exit Sub
            End If
            entity.Value = e.NewValue
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar disponibilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddAvailability_Click(sender As Object, e As EventArgs) Handles INDbtnAddAvailability.Click
        If INDsleAvailability.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una disponibilidad"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        If ListContractAvailability Is Nothing Then
            ListContractAvailability = New List(Of InventoryContractModificationAvailability)
        End If

        If (From x In ListContractAvailability Where x.AvailabilityDetailId = INDsleAvailability.EditValue Select x).Count() > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La disponibilidad " + INDsleAvailability.Text + " ya existe en la lista"
            INDsleAvailability.Focus()
            Exit Sub
        End If

        Dim viewXpo As ViewListAvailabilityDetailXpo = DirectCast(INDsleAvailability.GetSelectedObject(), ViewListAvailabilityDetailXpo)
        If viewXpo Is Nothing Then
            viewXpo = _presenter.GetAvailabilityDetailById(INDsleAvailability.EditValue)
        End If

        Dim InventoryContractModificationAvailability = New InventoryContractModificationAvailability()
        With InventoryContractModificationAvailability
            .AvailabilityDetailId = viewXpo.Id
            .AvailabilityCode = viewXpo.AvailabilityCode
            .CategoryCodeName = viewXpo.CategoryCodeName
            .FinancialSourceCodeName = viewXpo.FinancialSourceCodeName
            .RevenueTypeCodeName = viewXpo.RevenueTypeCodeName
            .Balance = viewXpo.Balance
            .Value = 0
        End With

        ListContractAvailability.Add(InventoryContractModificationAvailability)
        EnableBudgetInterface()
        INDgcAvailability.RefreshDataSource()

        Mensaje(EeventViewerImages.Informacion) = "Disponibilidad agregada correctamente"
        INDsleAvailability.EditValue = Nothing
        INDsleAvailability.Focus()
    End Sub

#End Region

#Region "MenuActions"

    ''' <summary>
    ''' Acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        DeleteAvailability()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _InventoryContractModification.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _InventoryContractModification.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _InventoryContractModification.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _InventoryContractModification.Status = 2
            _varImp = 3
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _InventoryContractModification.Status = 3
            _varImp = 4
            Guardar()
        End If
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _InventoryContractModification.Id, 0, _InventoryContractModification.Id)
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnitAsync(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Me.LoadPaymentsSetting()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class