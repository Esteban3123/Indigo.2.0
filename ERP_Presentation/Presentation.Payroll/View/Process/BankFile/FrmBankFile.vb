'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 03-03-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Payroll.MVP
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports DevExpress.XtraEditors

#End Region

Public Class FrmBankFile
    Implements IBankFile, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()

        Me._ctrTotal = New CtrTotalBank()
        Me._ctrTotal.SetTotalValues(AddressOf getValue)
        Me._ctrTotal.RefreshTotalValues()
        Me._ctrTotal.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(Me._ctrTotal)
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Payroll"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PayrollSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _currentSequenceId As Int64

    ''' <summary>
    ''' variable para controlar el presentador del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _presenter As PBankFile

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As Domain.Entities.BlockRecordPayroll

    ''' <summary>
    ''' bandera para identificar cuando se consulta un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private _isLoad As Boolean

    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private _ctrTotal As CtrTotalBank

    ''' <summary>
    ''' representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _bankFile As BankFile
    ''' <summary>
    ''' Variable que almacena las primas ya liquidas
    ''' </summary>
    Private _bankFileIncentivePayment As List(Of BankFileIncentivePaymentConfirmXpo)

    ''' <summary>
    ''' listado de detalle de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _listBankFileDetail As List(Of BankFileDetail)

    ''' <summary>
    ''' listado de detalle a eliminar de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private _listBankFileDetailDelete As List(Of Integer)

    ''' <summary>
    ''' listado de procesos (Liquidacion primas y liquidacion nomina)
    ''' </summary>
    ''' <remarks></remarks>
    Dim listProcess As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de las primas generadas segun periodo y año 
    ''' </summary>
    Private _BankFileIncentivePaymentWithoutConfirmList As List(Of SP_BankFileIncentivePaymentWithoutConfirm_Result)

#End Region

#Region "Fields"

    Public ReadOnly Property MyTag As Object Implements IBankFile.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBankFile.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements IBankFile.ActionsOnControls
        Set(value As Boolean)
            INDLyBankFile.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDsleCompany.Enabled = value
            INDgleLastLiquidationDate.Enabled = value
            INDsleBank.Enabled = value
            INDSleExpenseConcept.Enabled = value

            INDLyBankFile.EndUpdate()

            If value Then
                INDsleCompany.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    Public Property Sequence As Domain.Entities.PayrollSequence Implements IBankFile.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.PayrollSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequence.PayrollSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Properties"

    Public Property OperatingUnitId As Integer Implements IBankFile.OperatingUnitId
        Get
            Return Me._operativeUnitId
        End Get
        Set(value As Integer)
            Me._operativeUnitId = value
        End Set
    End Property

    Public Property Code As String Implements IBankFile.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public Property CompanyId As Integer Implements IBankFile.CompanyId
        Get
            Return IIf(String.IsNullOrEmpty(INDsleCompany.EditValue), 0, INDsleCompany.EditValue)
        End Get
        Set(value As Integer)
            INDsleCompany.EditValue = value
        End Set
    End Property

    Public Property LiquidationDate As Date? Implements IBankFile.LiquidationDate
        Get
            Return IIf(String.IsNullOrEmpty(INDgleLastLiquidationDate.EditValue), Nothing, INDgleLastLiquidationDate.EditValue)
        End Get
        Set(value As Date?)
            INDgleLastLiquidationDate.EditValue = value
        End Set
    End Property

    Public Property EntityBankAccountId As Integer Implements IBankFile.EntityBankAccountId
        Get
            Return INDsleBank.EditValue
        End Get
        Set(value As Integer)
            INDsleBank.EditValue = value
        End Set
    End Property

    Public Property ExpenseConceptId As Integer Implements IBankFile.ExpenseConceptId
        Get
            Return INDSleExpenseConcept.EditValue
        End Get
        Set(value As Integer)
            INDSleExpenseConcept.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para escoger que proceso se va a realizar
    ''' </summary>
    ''' <returns></returns>
    Public Property ProcessLiquidation As String Implements IBankFile.listProcess_
        Get
            Dim value = INDsleProcess.EditValue
            If value Is Nothing OrElse (value <> 1 AndAlso value <> 2) Then
                Return Nothing
            End If
            Return value
        End Get
        Set(value As String)
            If value IsNot Nothing AndAlso (value = 1 OrElse value = 2) Then
                INDsleProcess.EditValue = value
            Else
                INDsleProcess.EditValue = Nothing
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad para el periodo del año que se va a liquidar la prima
    ''' </summary>
    ''' <returns></returns>
    Public Property PeriodIncentivePayment As Integer Implements IBankFile.PeriodIncentivePayment
        Get
            Return INDCbeSemestre.EditValue
        End Get
        Set(value As Integer)
            INDCbeSemestre.EditValue = value
        End Set
    End Property


#End Region

#Region "XPO"

    Public Property CompanyXpo As XPInstantFeedbackSource Implements IBankFile.CompanyXpo
        Get
            Return CType(INDsleCompany.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCompany.Properties.DataSource = value
        End Set
    End Property

    Public Property LiquidationDateXpo As List(Of Date) Implements IBankFile.LiquidationDateXpo
        Get
            Return CType(INDgleLastLiquidationDate.Properties.DataSource, List(Of Date))
        End Get
        Set(value As List(Of Date))
            INDgleLastLiquidationDate.Properties.DataSource = value
        End Set
    End Property

    Public Property EntityBankAccountXpo As LinqInstantFeedbackSource Implements IBankFile.EntityBankAccountXpo
        Get
            Return CType(INDsleBank.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleBank.Properties.DataSource = value
        End Set
    End Property

    Public Property ExpenseConceptXpo As XPInstantFeedbackSource Implements IBankFile.ExpenseConceptXpo
        Get
            Return CType(INDSleExpenseConcept.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleExpenseConcept.Properties.DataSource = value
        End Set
    End Property

    Public Property LiquidationXpo As XPCollection(Of PayrollLiquidationXpo) Implements IBankFile.LiquidationXpo
        Get
            Return CType(INDGcListEmployee.DataSource, XPCollection(Of PayrollLiquidationXpo))
        End Get
        Set(value As XPCollection(Of PayrollLiquidationXpo))
            INDGcListEmployee.DataSource = value
            INDGvListEmployee.ExpandAllGroups()
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que carga el datasource para la rejilla de empleados generados, esto se hace segun la criteria
    ''' </summary>
    ''' <returns></returns>
    Public Property LiquidationXpo2 As XPCollection(Of PayrollLiquidationXpo) Implements IBankFile.LiquidationXpo2
        Get
            Return CType(INDGcListEmployee1.DataSource, XPCollection(Of PayrollLiquidationXpo))
        End Get
        Set(value As XPCollection(Of PayrollLiquidationXpo))
            INDGcListEmployee1.DataSource = value
            INDGvListEmployee.ExpandAllGroups()
        End Set
    End Property

    ''' <summary>
    ''' Establece los grupos filtrados por empresa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property datasourceGroups As List(Of Domain.Payroll.Entities.Group) Implements IBankFile.datasourceGroups
        Set(value As List(Of Domain.Payroll.Entities.Group))
            value = value.Where(Function(x) x.PayrollParameter.MaxPremiumByYear > 0).ToList()
            ''INDgcGroups.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el control del periodo o año
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PeriodControl As ComboBoxEdit Implements IBankFile.PeriodControl
        Get
            Return INDCbeYear
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que carga el datasource para la rejilla de empleados generados, esto se hace segun la criteria
    ''' </summary>
    ''' <returns></returns>
    Public Property ListIncentivePayment As XPCollection(Of IncentivePayment) Implements IBankFile.ListaPrimasLiquidadas
        Get
            Return CType(INDGcListEmployee1.DataSource, XPCollection(Of IncentivePayment))
        End Get
        Set(value As XPCollection(Of IncentivePayment))
            INDgcLiquidationResult.DataSource = value
            INDGvLiquidationResult.ExpandAllGroups()
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que asigna a la rejilla los valores de las primas ya liquididas y sin confirmar en el Archivo plano por medio del xpo
    ''' </summary>
    ''' <returns></returns>
    Public Property BankFileIncentivePaymentXpo As XPCollection(Of PayrollBankFileIncentivePaymentXpo) Implements IBankFile.BankFileIncentivePaymentXpo
        Get
            Return CType(INDgcLiquidationResult.DataSource, XPCollection(Of PayrollBankFileIncentivePaymentXpo))
        End Get
        Set(value As XPCollection(Of PayrollBankFileIncentivePaymentXpo))
            INDgcLiquidationResult.DataSource = value
            INDGvLiquidationResult.ExpandAllGroups()
        End Set
    End Property
    ''' <summary>
    ''' Trae las primas ya confrimadas y dispensadas en el archivo plano
    ''' </summary>
    ''' <returns></returns>
    Public Property ListIncentivePaymentIncentivePaymentConfirm As XPCollection(Of BankFileIncentivePaymentConfirmXpo) Implements IBankFile.BankFileIncentivePaymentConfirmXpo
        Get
            Return CType(INDgcLiquidationResult1.DataSource, XPCollection(Of BankFileIncentivePaymentConfirmXpo))
        End Get
        Set(value As XPCollection(Of BankFileIncentivePaymentConfirmXpo))
            INDgcLiquidationResult1.DataSource = value
            INDGvLiquidationResultIncentivePayment.ExpandAllGroups()
        End Set
    End Property


#End Region

#Region "Crud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = (Me._presenter.LoadPayrollSettings().CurrencyId.Abbreviation).GetNumberFormat()
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Compañia", .FieldName = "CompanyId.CodeName", .ColumnWidth = 300},
                              New ColumnInfo With {.Caption = "Tipo Proceso", .FieldName = "ProcessName", .ColumnWidth = 150},
                              New ColumnInfo With {.Caption = "Período", .FieldName = "PeriodIncentivePayment", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Fecha Liquidacion", .FieldName = "LiquidationDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Banco", .FieldName = "EntityBankAccountId.CodeName", .ColumnWidth = 300},
                              New ColumnInfo With {.Caption = "Valor", .FieldName = "Value", .FormatCulture = _culture, .ColumnWidth = 200, .ColumnFormat = "C0"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBankFiles
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence Is Nothing OrElse Me._sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewBankFile()
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If Me._bankFile Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se ha instanciado un objeto a guardar"
                Exit Sub
            End If

            If Me._bankFile.Status = 2 Then
                Mensaje(EeventViewerImages.Advertencia) = "Accion Equivocada"
                Exit Sub
            End If

            If Me._bankFile.Status < 3 Then
                If Not Me.ValidateControls() Then
                    Exit Sub
                End If
                Me.AssigningValues()
            End If

            Using model As New MBankFile(MyTag)
                AsyncLoader(True)

                Dim result = Await model.SaveBankFile(Me._bankFile, Me._listBankFileDetailDelete)
                If result.StateResult = True Then
                    If Me._bankFile.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._currentSequenceId).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If Me._bankFile.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me._bankFile = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    If result.StateResult = False And result.StateResultAux = False Then
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                            Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = result.Message
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If

                    If Me._bankFile.Id > 0 Then
                        Me._bankFile = Await model.GetBankFileByCode(Code)
                    Else
                        Me._bankFile = New BankFile With {.Status = 1}
                    End If
                    Me._bankFile.Value = Me._listBankFileDetail.Sum(Function(d) d.TotalPaid)

                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Private Async Sub SaveAndConfirmBankFile()
        Try
            If Not Me.ValidateControls() = True Then
                Exit Sub
            End If

            AssigningValues()
            AsyncLoader(True)

            Using model As New MBankFile(MyTag)
                Dim result = Await model.SaveAndConfirmBankFile(Me._bankFile, Me._listBankFileDetailDelete)
                If result.StateResult = True And result.StateResultAux = True Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        If DicSequense.Count > 0 Then
                            Me.DicSequense.Remove(Me._currentSequenceId)
                        End If
                    End If

                    Mensaje(EeventViewerImages.Informacion) = result.Message

                    Me._bankFile = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        If DicSequense.Count > 0 Then
                            Me.DicSequense.Remove(Me._currentSequenceId)
                        End If
                    End If

                    Mensaje(EeventViewerImages.Advertencia) = result.Message

                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult.First().ToString() <> "" Then
                        Mensaje(EeventViewerImages.Advertencia) = String.Join(vbCrLf, result.MessageResult)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If

                    If Me._bankFile.Id > 0 Then
                        Me._bankFile = Await model.GetBankFileByCode(Code)
                    Else
                        Me._bankFile = New BankFile With {.Status = 1}
                    End If
                    Me._bankFile.Value = Me._listBankFileDetail.Sum(Function(d) d.TotalPaid)

                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me.OperatingUnitId Then
            Me.OperatingUnitId = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PayrollSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.SaveAndConfirmBankFile()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.SaveAndConfirmBankFile()
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me._bankFile.Status = 3
            Guardar()
        End If
    End Sub

    Private Async Sub BarraBotones_Click_GenerateFile() Handles BarraBotones.Click_GenerateFile
        Await GenerateFile()
    End Sub

#End Region

#Region "Handles"

#Region "Load And Disposed"

    Private Sub FrmBankFile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLyBankFile, True)

        Me._doc = Nothing
        Me._presenter = New PBankFile(Me)
        Me._presenter.LoadDefinitionLayout()
        Me._presenter.GetSequence()

        Me.OperatingUnitId = BarraBotones.OperatingUnitValue
        Me.Deshacer()

        ' Configura la mascara de liquidationDate
        INDgleLastLiquidationDate.Properties.Mask.EditMask = "dd/MM/yyyy" ' Define el patron de la mascara

        ' Habilita la visibilidad de la mascara
        INDgleLastLiquidationDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTime

        ' opcional, deshabilita la mascara para prevenir que sea evitable desde el inicio
        INDgleLastLiquidationDate.Properties.Mask.UseMaskAsDisplayFormat = True

        Me.LoadStatus()
        CreateListProcess()

        Me._presenter.initialize()
        SetCurrencyFormat(Me._presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        INDCbeSemestre.Properties.Items.Add(1)
        INDCbeSemestre.Properties.Items.Add(2)
    End Sub

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        _ctrTotal.CurrencyAbbreviation = _currencyAbbreviation
        _ctrTotal.RefreshTotalValues()

    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Me._operativeUnitId = Nothing
        Me._sequence = Nothing
        Me._currentSequenceId = Nothing
        Me._presenter = Nothing
        Me._record = Nothing
        Me._isLoad = Nothing
        Me._ctrTotal = Nothing
        Me._bankFile = Nothing
        Me._listBankFileDetail = Nothing
        Me._listBankFileDetailDelete = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmBankFile_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewBankFile()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDsleCompany_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCompany.QueryPopUp
        If CompanyXpo Is Nothing Then
            Me._presenter.InitializeCompanyXPO()
        End If
    End Sub

    Private Sub INDgleLastLiquidationDate_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDgleLastLiquidationDate.QueryPopUp
        If LiquidationDateXpo Is Nothing Then
            Me._presenter.InitializeLiquidationDateXpo()
        End If
    End Sub

    Private Sub INDsleBank_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBank.QueryPopUp
        If EntityBankAccountXpo Is Nothing Then
            If _presenter.LoadPayrollSettings()?.CurrencyId?.Id > 0 Then
                Me._presenter.InitializeEntityBankAccountXpo(_presenter.LoadPayrollSettings().CurrencyId.Id)
            End If
        End If
    End Sub

    Private Sub INDSleExpenseConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleExpenseConcept.QueryPopUp
        If ExpenseConceptXpo Is Nothing Then
            Me._presenter.InitializeExpenseConceptXPO()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleBank_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBank.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("628", Nothing, True)
        End If
    End Sub

    Private Sub INDSleExpenseConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleExpenseConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("631", Nothing, True)
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    Private Sub INDGvListEmployee_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDGvListEmployee.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            Dim view = CType(sender, GridView)
            Dim hitInfo As GridHitInfo = view.CalcHitInfo(e.Point)
            view.FocusedRowHandle = hitInfo.RowHandle

            Dim groupRow = view.GetParentRowHandle(view.FocusedRowHandle)
            Dim childRows As Integer = GetChildRowsHandles(view, groupRow)

            PopupMenu1.Manager = BarManager1
            PopupMenu1.ShowPopup(INDGvListEmployee.GridControl.PointToScreen(e.Point))
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleCompany_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleCompany.EditValueChanging
        If Not Me._isLoad AndAlso INDsleCompany.Properties.ReadOnly Then
            e.Cancel = True
        End If
    End Sub

    Private Sub INDgleLastLiquidationDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDgleLastLiquidationDate.EditValueChanging
        If Not Me._isLoad AndAlso INDgleLastLiquidationDate.Properties.ReadOnly Then
            e.Cancel = True
        End If
    End Sub

    Private Sub INDsleCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCompany.EditValueChanged, INDgleLastLiquidationDate.EditValueChanged
        If Me._isLoad = False Then
            If CompanyId > 0 AndAlso LiquidationDate IsNot Nothing Then
                Me._presenter.ListPayrollDateLiquidated(CompanyId, LiquidationDate)
                If LiquidationXpo Is Nothing OrElse LiquidationXpo.Count = 0 Then
                    Mensaje(EeventViewerImages.Informacion) = "No hay liquidaciones disponibles para generar archivo bancario. Todas las liquidaciones de este período ya fueron incluidas en otro archivo plano."
                End If
                Me._listBankFileDetail = New List(Of BankFileDetail)
                Me._listBankFileDetailDelete = New List(Of Integer)
                Me._bankFile.Value = Me._listBankFileDetail.Sum(Function(d) d.TotalPaid)
            End If
        End If

        If Me._ctrTotal IsNot Nothing Then
            Me._ctrTotal.RefreshTotalValues()
        End If
    End Sub
    ''' <summary>
    ''' Evento que oculta y limpia el dataSource de las rejillas si el combobox es prima o Nómina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProcess_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProcess.EditValueChanged
        If INDsleProcess.EditValue = 1 Then 'Prima
            INDLcgEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgEmployee1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDlyItemSemestre.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemYear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgListIncentivePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgListIncentivePayment1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemLastLiquidationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDGcListEmployee1.DataSource = Nothing
            INDGcListEmployee.DataSource = Nothing
            LiquidationDate = Nothing

            INDgcLiquidationResult.DataSource = _BankFileIncentivePaymentWithoutConfirmList
            INDgcLiquidationResult1.DataSource = ListIncentivePaymentIncentivePaymentConfirm
        ElseIf INDsleProcess.EditValue = 2 Then 'Nómina
            INDLcgEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgEmployee1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemLastLiquidationDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDlyItemSemestre.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemYear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgListIncentivePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDGcListEmployee.DataSource = LiquidationXpo
            INDGcListEmployee1.DataSource = LiquidationXpo2
            INDgcLiquidationResult.DataSource = Nothing
            INDgcLiquidationResult1.DataSource = Nothing
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim row = INDGvListEmployee.GetFocusedRow()
            If row IsNot Nothing Then
                If e.NewValue Then
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Add)
                Else
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Remove)
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento cuando se selecciona una fila del listado de primas liquidadas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepCheckSelectOption2_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption2.EditValueChanging
        If e IsNot Nothing Then
            Dim row = INDGvLiquidationResult.GetFocusedRow()

            If row IsNot Nothing Then
                If e.NewValue Then
                    Me.SelectedOrUnSelected2(row, CollectionChangeAction.Add)
                Else
                    Me.SelectedOrUnSelected2(row, CollectionChangeAction.Remove)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambiar el perido de la prima 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Function INDCbeSemestre_EditValueChanged(sender As Object, e As EventArgs) As Task Handles INDCbeSemestre.EditValueChanged
        INDgcLiquidationResult.DataSource = Nothing
        INDgcLiquidationResult1.DataSource = Nothing
        Dim fechaLiquidacionPrimasPeriodo = ObtenerFechaPrimas(PeriodIncentivePayment, PeriodControl.EditValue)

        Using modelo As New MBankFile(MyTag)
            _BankFileIncentivePaymentWithoutConfirmList = Await modelo.ShowIncentivePaymentWithoutConfirm(PeriodIncentivePayment, fechaLiquidacionPrimasPeriodo)
        End Using
        If _BankFileIncentivePaymentWithoutConfirmList?.Count > 0 Then
            INDgcLiquidationResult.DataSource = _BankFileIncentivePaymentWithoutConfirmList
            INDGvLiquidationResult.ExpandAllGroups()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encuentran Primas liquidadas en la fecha: " + fechaLiquidacionPrimasPeriodo
        End If
        Me._ctrTotal.RefreshTotalValues()
    End Function
#End Region

#Region "ItemClick"

    Private Sub INDbbSelection_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbSelection.ItemClick
        SelectOptions(1)
        'SelectOptionsIncentivePayment(1)
    End Sub

    Private Sub INDbbUnselection_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbUnselection.ItemClick
        SelectOptions(0)
        'SelectOptionsIncentivePayment(0)
    End Sub

#End Region

#Region "MouseDown"

    Private Sub INDGvListEmployee_MouseDown(sender As Object, e As MouseEventArgs) Handles INDGvListEmployee.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing Then
            If hi.InColumn AndAlso hi.Column.FieldName = "SelectOption" Then
                If Not hi.InRow Then
                    Dim listFilterXpCollection = view.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                    Dim action As CollectionChangeAction = IIf((listFilterXpCollection.Count = cont), CollectionChangeAction.Remove, CollectionChangeAction.Add)
                    For Each row In listFilterXpCollection
                        Me.SelectedOrUnSelected(row, action)
                    Next
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento para seleccionar una cantidad especifica de empleados en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvLiquidationResult_MouseDown(sender As Object, e As MouseEventArgs) Handles INDGvLiquidationResult.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing Then
            If hi.InColumn AndAlso hi.Column.FieldName = "SelectOption" Then
                If Not hi.InRow Then
                    Dim listFilterXpCollection = view.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                    Dim action As CollectionChangeAction = IIf((listFilterXpCollection.Count = cont), CollectionChangeAction.Remove, CollectionChangeAction.Add)
                    For Each row In listFilterXpCollection
                        Me.SelectedOrUnSelected2(row, action)
                    Next
                End If
            End If
        End If
    End Sub

#End Region

#Region "DataSourceChanged"
    ''' <summary>
    ''' Evento que carga las liquidaciones que ya esten confirmadas en bankfiles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGcListEmployee_DataSourceChanged(sender As Object, e As EventArgs) Handles INDGcListEmployee1.DataSourceChanged

        If Me._isLoad AndAlso Me._listBankFileDetail IsNot Nothing AndAlso Me._listBankFileDetail.Count > 0 AndAlso Me.LiquidationXpo2 IsNot Nothing Then
            For Each detail In Me._listBankFileDetail
                Dim rows = Me.LiquidationXpo2.Where(Function(d) d.Id = detail.LiquidationId)

                If rows Is Nothing OrElse rows.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Existen detalles que no fueron cargados"
                ElseIf rows.Count > 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Existen detalles cargados más de una vez"
                Else
                    rows.FirstOrDefault().SelectOption = True
                End If
            Next
        End If

    End Sub

#End Region

#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene el valor para el ctr
    ''' </summary>
    ''' <returns></returns>
    Private Function getValue() As Tuple(Of Decimal)
        Dim SumValue As Decimal = 0
        If Me._bankFile IsNot Nothing Then
            SumValue = Me._bankFile.Value

        End If
        If _bankFileIncentivePayment IsNot Nothing Then
            For Each item In _bankFileIncentivePayment
                SumValue += item.PaidValue
            Next
        End If
        Return New Tuple(Of Decimal)(SumValue)
    End Function

    Sub CleanControls()
        INDLyBankFile.BeginUpdate()

        Me.ReadOnlyControls(False)
        Me.DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        Me.Code = String.Empty
        INDsleCompany.Properties.NullText = String.Empty
        INDsleCompany.EditValue = Nothing
        INDgleLastLiquidationDate.Properties.NullText = String.Empty
        INDgleLastLiquidationDate.EditValue = Nothing
        INDsleBank.Properties.NullText = String.Empty
        INDsleBank.EditValue = Nothing
        INDSleExpenseConcept.Properties.NullText = String.Empty
        INDSleExpenseConcept.EditValue = Nothing
        INDsleProcess.EditValue = Nothing
        PeriodIncentivePayment = Nothing

        INDsleProcess.Enabled = False
        If _bankFileIncentivePayment IsNot Nothing Then
            _bankFileIncentivePayment.Clear()
        End If
        INDsleProcess.Properties.NullText = String.Empty
        INDlyItemSemestre.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me._presenter.LoadYears() 'se llama la funcion para el campo de año en primas
        INDlyItemYear.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDCbeSemestre.Visible = False
        Me._bankFile = Nothing
        Me._listBankFileDetail = Nothing
        Me._listBankFileDetailDelete = Nothing
        Me._BankFileIncentivePaymentWithoutConfirmList = Nothing
        INDGcListEmployee.DataSource = Nothing
        INDGcListEmployee1.DataSource = Nothing
        INDgcLiquidationResult.DataSource = Nothing
        INDgcLiquidationResult1.DataSource = Nothing
        INDLcgListIncentivePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgListIncentivePayment1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLcgEmployee1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me._ctrTotal.RefreshTotalValues()


        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        ActionsOnControls = False
        INDsleBank.Focus()
        EntityBankAccountXpo = Nothing
        INDLyBankFile.EndUpdate()


    End Sub

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            Try
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If
                Using model As New MBankFile(MyTag)
                    AsyncLoader(True)
                    INDLyBankFile.BeginUpdate()

                    Me._bankFile = Await model.GetBankFileByCode(Code)
                    If Me._bankFile IsNot Nothing AndAlso Me._bankFile.Id > 0 Then
                        Me._listBankFileDetail = model.GetBankFileDetailByBankFileId(Me._bankFile.Id)
                        Dim objListLiquidated = Me._listBankFileDetail.Where(Function(x) x.BankFile.Status = 2)
                        Me._listBankFileDetailDelete = New List(Of Integer)

                        INDsleCompany.Properties.ReadOnly = True
                        INDgleLastLiquidationDate.Properties.ReadOnly = True
                        INDsleProcess.Properties.ReadOnly = True

                        Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                            Me._record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Me._bankFile.Id))

                            With Me._bankFile
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Me.BarraBotones.StatusRecordVisible = True
                                Me.BarraBotones.StatusRecord = .Status.ToString()
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case 2
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select

                                Me._isLoad = True

                                Code = .Code
                                BarraBotones.OperatingUnitValue = .OperatingUnitId

                                CompanyId = .CompanyId
                                INDsleCompany.Properties.NullText = .CompanyName

                                ProcessLiquidation = .Process
                                INDsleProcess.Properties.NullText = .Process

                                LiquidationDate = .LiquidationDate
                                INDgleLastLiquidationDate.Properties.NullText = .LiquidationDate
                                EntityBankAccountId = .EntityBankAccountId
                                INDsleBank.Properties.NullText = .EntityBankAccountNumber

                                ExpenseConceptId = .ExpenseConceptId
                                INDSleExpenseConcept.Properties.NullText = .ExpenseConceptName
                                If LiquidationDateXpo Is Nothing Then
                                    Me._presenter.InitializeLiquidationDateXpo()
                                End If

                                'validamos que sea el proceso de primas para asignarle valor a estos controles y bloquearlos
                                If ProcessLiquidation = 1 Then
                                    PeriodIncentivePayment = .PeriodIncentivePayment
                                    Dim bankFileId = .Id
                                    Dim periodDate = ObtenerFechaPrimas(PeriodIncentivePayment, .YearIncentivePayment)
                                    Dim resBankFileDetails = Me._presenter.ListBankFileIncentivePaymentConfirm(CType(PeriodIncentivePayment, String), periodDate, bankFileId)
                                    If resBankFileDetails IsNot Nothing Then
                                        For Each item In resBankFileDetails
                                            item.SelectOption = True
                                        Next
                                    End If
                                End If

                                'Condicional para que se cargue el datasource de nomina solo si es el proceso es 2
                                If ProcessLiquidation = 2 Then
                                    Me._presenter.ListPayrollDateLiquidated(CompanyId, LiquidationDate)
                                    Me._presenter.ListPayrollDateLiquidated2(CompanyId, LiquidationDate)
                                End If

                                Me._isLoad = False
                                Me._ctrTotal.RefreshTotalValues()

                                If .Status = 1 OrElse .Status = 2 Then
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                                End If
                            End With

                            If Me._record.Id = 0 Then
                                Me._record = (Await ModelRecord.SaveBlockRecord(
                                        New Domain.Entities.BlockRecordPayroll With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .CodUser = Me.indigo.UserIndigo, .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .RecordId = Me._bankFile.Id})
                                        ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), Me._record.CodUser, Me._record.NameUser, Me._record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, Me._record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(Me._bankFile.Id, Me.Tag.ToString(), Nothing, GetType(BankFile).Name)

                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDsleBank.Focus()
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewBankFile()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If

                    INDLyBankFile.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Async Function NewBankFile() As Task
        Me._bankFile = New BankFile() With {.Status = 1}
        Me._listBankFileDetail = New List(Of BankFileDetail)
        Me._listBankFileDetailDelete = New List(Of Integer)

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.PayrollSequenceDetail Is Nothing OrElse Me._sequence.PayrollSequenceDetail.Count = 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "No se ha podido cargar el detalle de la Secuencia Numerica."
                Exit Function
            End If

            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.PayrollSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me.OperatingUnitId) Then
                    Me._currentSequenceId = Me._sequence.PayrollSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me.OperatingUnitId).Id
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
                    If Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._currentSequenceId)) = Await model.GetNumericSequenseGroup(CInt(Me._currentSequenceId))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._currentSequenceId)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
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
            INDsleProcess.Enabled = True

            If ProcessLiquidation = "1" Then
                INDLcgEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLcgEmployee1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            ElseIf ProcessLiquidation = "2" Then
                INDLcgEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLcgEmployee1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Function

    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    Private Async Sub DeleteBlockedRecord()
        If Me._record IsNot Nothing AndAlso Me._record.Id > 0 AndAlso Me._record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                Await model.DeleteBlockRecord(Me._record)
                Me._record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    Function ValidateControls() As Boolean
        If Not CompanyId > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Compañia"
            Return False
        End If
        If ProcessLiquidation Is Nothing OrElse (ProcessLiquidation <> 1 AndAlso ProcessLiquidation <> 2) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Proceso (Liquidación Prima o Liquidación Nómina)"
            Return False
        End If

        If ProcessLiquidation = 2 AndAlso String.IsNullOrEmpty(LiquidationDate) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Fecha de Liquidación"
            Return False
        End If

        If String.IsNullOrEmpty(EntityBankAccountId) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una Cuenta Bancaria"
            Return False
        End If

        If String.IsNullOrEmpty(ExpenseConceptId) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Concepto de Egreso"
            Return False
        End If

        If Me._listBankFileDetail.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar un detalle"
            Return False
        End If

        Return True
    End Function

    Private Sub AssigningValues()
        With Me._bankFile
            .CustomProperties = LayoutControls.GetCustomFieldsValue()

            .Code = Me.Code
            .OperatingUnitId = Me.OperatingUnitId
            .CompanyId = Me.CompanyId
            .PeriodIncentivePayment = Me.PeriodIncentivePayment
            If ProcessLiquidation = 1 Then 'Proceso de prima
                .LiquidationDate = ObtenerFechaPrimas(Me.PeriodIncentivePayment, PeriodControl.EditValue)
            Else
                .LiquidationDate = Me.LiquidationDate 'Proceso de nomina
            End If

            .EntityBankAccountId = Me.EntityBankAccountId
            .ExpenseConceptId = Me.ExpenseConceptId
            If ProcessLiquidation IsNot Nothing AndAlso (ProcessLiquidation = 1 OrElse ProcessLiquidation = 2) Then
                .Process = ProcessLiquidation
            End If
            .YearIncentivePayment = CInt(Me.PeriodControl.Text)

            For Each detail In Me._listBankFileDetail.Where(Function(d) d.ChangeTracker.State = ObjectState.Added OrElse d.ChangeTracker.State = ObjectState.Modified)
                .BankFileDetail.Add(detail)
            Next
        End With
    End Sub

    Public Async Function GenerateFile() As Task
        If Not Me._bankFile.Id > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe guardar el documento antes de generar el Archivo de Banco"
        End If

        AsyncLoader(True)

        Dim resultGenerateFile As ActionMessageResult(Of StringBuilder)
        Using model As New MBankFile(Me.Tag)
            resultGenerateFile = Await model.GenerateBankFileAsync(Me._bankFile.Id)
        End Using

        If resultGenerateFile.StateResult = True Then
            Me.DialogGenerateFile(resultGenerateFile.ObjectEmbbeded)
        Else
            Mensaje(EeventViewerImages.Advertencia) = resultGenerateFile.Message
        End If

        AsyncLoader(False)
    End Function

    Public Sub DialogGenerateFile(content As StringBuilder)
        Dim save As SaveFileDialog = New SaveFileDialog()
        save.Filter = "Texto|*.txt"
        save.Title = INDlyGrBankFile.Text
        Dim dateLiquidation As Date = CType(INDgleLastLiquidationDate.EditValue, Date).Date
        Dim currency = Me._presenter.LoadPayrollSettings()?.CurrencyId?.Abbreviation

        If currency IsNot Nothing And currency = "CRC" Then
            save.FileName = "ARCHIVO BANCO " & dateLiquidation.ToString("yyyy-MM-dd") & ".txt"
        Else

            save.FileName = dateLiquidation.Year & "-" & Utils.StringPad(dateLiquidation.Month, 2, 0, Utils.PadType.STR_PAD_LEFT) & "-" & Utils.StringPad(dateLiquidation.Day, 2, 0, Utils.PadType.STR_PAD_LEFT) & "  " & INDsleBank.Text & ".txt"
        End If

        If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim file = save.OpenFile()
            Dim streamWrite As New StreamWriter(file)
            streamWrite.Write(content)
            streamWrite.Flush()
            streamWrite.Close()
            If MessageIndigo.Show(obtenerRecurso(GuardadoDeseaAbrir, Eform.ArchivoBanco), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Process.Start(save.FileName)
            End If
        End If
    End Sub

    Public Function GetChildRowsHandles(view As GridView, groupRowHandle As Integer) As Integer
        Dim childRows As Integer = 0
        If Not view.IsGroupRow(groupRowHandle) Then
            childRows = 1
            Return childRows
        End If
        Return childRows
    End Function
    ''' <summary>
    ''' Evento para seleccionar empleado y remover los demas para filtrar la cantidad de empleados a liquidar
    ''' </summary>
    ''' <param name="optionCheck"></param>
    Private Sub SelectOptions(optionCheck As Integer)
        Dim listHandlesSelected = INDGvListEmployee.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If INDGvListEmployee.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(INDGvListEmployee, listHandlesSelected(i), optionCheck)
                Else
                    Dim row = INDGvListEmployee.GetRow(listHandlesSelected(i))
                    If optionCheck = 0 Then
                        Me.SelectedOrUnSelected(row, CollectionChangeAction.Remove)
                    Else
                        Me.SelectedOrUnSelected(row, CollectionChangeAction.Add)
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub SelectOptionsIncentivePayment(optionCheck As Integer)
        Dim listHandlesSelected = INDGvLiquidationResult.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If INDGvLiquidationResult.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows2(INDGvLiquidationResult, listHandlesSelected(i), optionCheck)
                Else
                    Dim row = INDGvLiquidationResult.GetRow(listHandlesSelected(i))
                    If optionCheck = 0 Then
                        Me.SelectedOrUnSelected2(row, CollectionChangeAction.Remove)
                    Else
                        Me.SelectedOrUnSelected2(row, CollectionChangeAction.Add)
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Remove)
                Else
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Add)
                End If
            End If
        Next
    End Sub
    ''' <summary>
    ''' Obtiene las filas hijas para rejilla de primas
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <param name="optionCheck"></param>
    Private Sub GetChildsRows2(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows2(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    Me.SelectedOrUnSelected2(row, CollectionChangeAction.Remove)
                Else
                    Me.SelectedOrUnSelected2(row, CollectionChangeAction.Add)
                End If
            End If
        Next
    End Sub

    Private Sub SelectedOrUnSelected(row As PayrollLiquidationXpo, action As CollectionChangeAction)
        If row IsNot Nothing Then

            If action = CollectionChangeAction.Add And Not row.SelectOption Then
                row.SelectOption = True
                If Not Me._listBankFileDetail.Any(Function(d) d.LiquidationId = row.Id) Then
                    Me._listBankFileDetail.Add(New BankFileDetail With
                        {
                            .LiquidationId = row.Id,
                            .GroupId = row.GroupId.Id,
                            .PositionId = row.ContractId.PositionId.Id,
                            .FunctionalUnitId = row.ContractId.FunctionalUnitId.Id,
                            .ContractId = row.ContractId.Id,
                            .EmployeeId = row.EmployeeId.Id,
                            .EmployeeBankId = row.ContractId.BankId.Id,
                            .EmployeeBankTypeAccount = row.ContractId.BankAccountType,
                            .EmployeeBankAccountNumber = row.ContractId.BankAccountNumber,
                            .BasicSalary = row.BasicSalary,
                            .TotalAccrued = row.TotalAccrued,
                            .TotalDeducted = row.TotalDeducted,
                            .TotalPaid = row.TotalPaid
                        }
                    )
                End If
            ElseIf action = CollectionChangeAction.Remove And row.SelectOption Then
                row.SelectOption = False
                For Each detail In Me._listBankFileDetail.Where(Function(d) d.LiquidationId = row.Id).ToList()
                    If detail.Id > 0 Then
                        Me._listBankFileDetailDelete.Add(detail.Id)
                    End If
                    Me._listBankFileDetail.Remove(detail)
                Next
            End If

            If Me._listBankFileDetail.Count > 0 Then
                INDsleCompany.Properties.ReadOnly = True
                INDgleLastLiquidationDate.Properties.ReadOnly = True
            Else
                INDsleCompany.Properties.ReadOnly = False
                INDgleLastLiquidationDate.Properties.ReadOnly = False
            End If

            Me._bankFile.Value = Me._listBankFileDetail.Sum(Function(d) d.TotalPaid)
            INDGcListEmployee.RefreshDataSource()
            INDGcListEmployee1.RefreshDataSource()
            Me._ctrTotal.RefreshTotalValues()
        End If
    End Sub
    ''' <summary>
    ''' Asigna valores al objeto de primas para que este pueda crear la lista y guardar el archivo plano
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="action"></param>
    Private Sub SelectedOrUnSelected2(row As SP_BankFileIncentivePaymentWithoutConfirm_Result, action As CollectionChangeAction)

        If row IsNot Nothing Then

            If action = CollectionChangeAction.Add And Not row.SelectOption Then
                row.SelectOption = True
                If Not Me._listBankFileDetail.Any(Function(d) d.LiquidationId = row.LiquidationId) Then
                    Me._listBankFileDetail.Add(New BankFileDetail With
                        {
                            .LiquidationId = row.LiquidationId,
                            .GroupId = row.GroupId,
                            .PositionId = row.PositionId,
                            .FunctionalUnitId = row.FunctionalUnitId,
                            .ContractId = row.ContractId,
                            .EmployeeId = row.EmployeeId,
                            .EmployeeBankId = row.BankId,
                            .EmployeeBankTypeAccount = row.EmployeeBankTypeAccount,
                            .EmployeeBankAccountNumber = row.BankAccount,
                            .BasicSalary = row.BasicSalary,
                            .TotalAccrued = row.TotalAccrued,
                            .TotalDeducted = row.TotalDeducted,
                            .TotalPaid = row.PaidValue
                        }
                    )
                End If

            ElseIf action = CollectionChangeAction.Remove And row.SelectOption Then
                row.SelectOption = False
                For Each detail In Me._listBankFileDetail.Where(Function(d) d.LiquidationId = row.LiquidationId).ToList()
                    If detail.Id > 0 Then
                        Me._listBankFileDetailDelete.Add(detail.Id)
                    End If
                    Me._listBankFileDetail.Remove(detail)
                Next
            End If
            If Me._listBankFileDetail.Count > 0 Then
                INDsleCompany.Properties.ReadOnly = True
                INDgleLastLiquidationDate.Properties.ReadOnly = True
            Else
                INDsleCompany.Properties.ReadOnly = False
                INDgleLastLiquidationDate.Properties.ReadOnly = False
            End If
            Me._bankFile.Value = Me._listBankFileDetail.Sum(Function(d) d.TotalPaid)


            ' Actualizar las vistas
            INDgcLiquidationResult.RefreshDataSource()
            INDgcLiquidationResult1.RefreshDataSource()

            Me._ctrTotal.RefreshTotalValues()
        End If
    End Sub
    ''' <summary>
    ''' Trae los valores del atributo proceso
    ''' </summary>
    Private Sub CreateListProcess()
        listProcess = New List(Of Tuple(Of Integer, String))
        listProcess.Add(New Tuple(Of Integer, String)(1, "Liquidación Prima"))
        listProcess.Add(New Tuple(Of Integer, String)(2, "Liquidación Nómina"))
        INDsleProcess.Properties.DataSource = listProcess
    End Sub
    ''' <summary>
    ''' Funcion que obtiene la fecha final dependiendo del perido que se seleccione
    ''' </summary>
    ''' <param name="Period"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    Public Function ObtenerFechaPrimas(Period As String, Year As Integer)

        Dim EndMonth As Byte
        Dim EndDay As Byte
        Dim LiquidationDate As Date
        If Period = "1" Then
            EndMonth = 6
            EndDay = 30

        ElseIf Period = "2" Then
            EndMonth = 12
            EndDay = 31
        End If
        LiquidationDate = New Date(Year, EndMonth, EndDay)
        Return LiquidationDate
    End Function

    ''' <summary>
    ''' Evento que limpia el datasource al cambiar el año en primas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCbeYear_EditValueChanged(sender As Object, e As EventArgs) Handles INDCbeYear.EditValueChanged

        INDCbeSemestre.EditValue = Nothing
        INDgcLiquidationResult.DataSource = Nothing
        INDgcLiquidationResult1.DataSource = Nothing
    End Sub

#End Region

End Class