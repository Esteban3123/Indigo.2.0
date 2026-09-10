'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 27-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Treasury.MVP
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports DevExpress.Xpo
Imports System.Text
Imports Presentation.Common.MVP
Imports System.ComponentModel

#End Region

Public Class FrmCheckCashing
    Implements ICashing, ICustomizableForm

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmCheckCashing"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrTotalVoucher()
        ctrTmp.SetVoucherValueFunction(AddressOf getVoucherValue)
        ctrTmp.PrintValueVoucher()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub
#End Region

#Region "Variables and Properties"

    ''' <summary>
    ''' Nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Control para manejar los debitos y créditos
    ''' </summary>
    Public ctrTmp As CtrTotalVoucher

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequence As TreasurySequence Implements ICashing.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de comprobantes de egreso
    ''' </summary>
    Public Property VoucherTransactionId As Integer Implements ICashing.VoucherTransactionId
        Get
            Return CType(INDsleVoucherTransactionNumber.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleVoucherTransactionNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del comprobante de egreso
    ''' </summary>
    Public Property Code As String Implements ICashing.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero del cheque
    ''' </summary>
    Public Property CurrentCheck As Long Implements ICashing.CurrentCheck
        Get
            Return INDspnCurrentCheck.EditValue
        End Get
        Set(value As Long)
            INDspnCurrentCheck.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el detalle
    ''' </summary>
    Public Property Detail As String Implements ICashing.Detail
        Get
            Return INDmeDetail.EditValue
        End Get
        Set(value As String)
            INDmeDetail.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    Public Property DocumentDate As Date Implements ICashing.DocumentDate
        Get
            Return INDdeDate.EditValue
        End Get
        Set(value As Date)
            INDdeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta bancaria
    ''' </summary>
    Public Property IdEntityBankAccount As Integer Implements ICashing.IdEntityBankAccount
        Get
            Return INDsleEntityAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleEntityAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tercero
    ''' </summary>
    Public Property IdThirdParty As Integer? Implements ICashing.IdThirdParty
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    Public Property MainAccountId As Integer Implements ICashing.MainAccountId
        Get
            Return INDsleMainAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero del cheque a reemplazar
    ''' </summary>
    Public Property NextCheck As Long Implements ICashing.NextCheck
        Get
            Return INDspnNextCheck.EditValue
        End Get
        Set(value As Long)
            INDspnNextCheck.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la clase de comprobante
    ''' </summary>
    Public Property VoucherClass As Byte Implements ICashing.VoucherClass
        Get
            Return INDgleVoucherClass.EditValue
        End Get
        Set(value As Byte)
            INDgleVoucherClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del comprobante
    ''' </summary>
    Public Property Status As Byte Implements ICashing.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la tasa por mil
    ''' </summary>
    Public Property TaxByMil As Decimal? Implements ICashing.TaxByMil

    ''' <summary>
    ''' Obtiene o establece el valor del comprobante
    ''' </summary>
    Public Property Value As Decimal Implements ICashing.Value

    ''' <summary>
    ''' Obtiene o establece el datasource de comprobantes de egreso
    ''' </summary>
    Public Property VoucherTransactionDatasource As XPInstantFeedbackSource Implements ICashing.VoucherTransactionDatasource
        Get
            Return CType(INDsleVoucherTransactionNumber.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherTransactionNumber.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICashing.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICashing.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' estado del popup de cuentas bancarias
    ''' </summary>
    Private _statePopUpVoucherTransaction As Boolean

    ''' <summary>
    ''' Lista las clases de comprobante
    ''' </summary>
    Dim ListVoucherClass As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PCashing

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordTreasury

    ''' <summary>
    ''' voucher transaction
    ''' </summary>
    Dim _voucherTransaction As VoucherTransaction

    ''' <summary>
    ''' entidad de cambio de cheque
    ''' </summary>
    Dim _cashing As CheckCashing

    ''' <summary>
    ''' Cuenta bancaria con que se pago el cheque
    ''' </summary>
    Dim _entityBankAccount As EntityBankAccounts

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        varImp = Nothing
        _statePopUpVoucherTransaction = Nothing
        ListVoucherClass = Nothing
        _presenter = Nothing
        record = Nothing
        _voucherTransaction = Nothing
        _cashing = Nothing
        _entityBankAccount = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmChangeCancellation control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmChangeCancellation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PCashing(Me)
        _presenter.GetSequence()

        CreateVoucherClass()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmCheckCashing control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCheckCashing_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the 1 event of the INDbteCode_KeyDown control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown_1(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewCashing()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
#End Region

#Region "EditValueChenged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleVoucherClass control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleVoucherClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleVoucherClass.EditValueChanged
        If VoucherClass <> 0 Then
            INDliThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If VoucherClass = eVoucherClss.Payment Then
                INDliThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleVoucherTransactionNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleVoucherTransactionNumber_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleVoucherTransactionNumber.EditValueChanged
        If VoucherTransactionId <> 0 Then
            Using Model As New MVoucherTransaction(Me.Tag)
                _voucherTransaction = (Await Model.GetVoucherTransactionById(VoucherTransactionId)).ObjectEmbbeded
                LoadVoucerTransactionData()
            End Using
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleVoucherTransactionNumber control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleVoucherTransactionNumber_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleVoucherTransactionNumber.QueryPopUp
        If Not _statePopUpVoucherTransaction Then
            _presenter.InitializeVoucherTransaction()
            _statePopUpVoucherTransaction = True
        End If
    End Sub
#End Region

#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' News the cashing.
    ''' </summary>
    Private Async Function NewCashing() As Task
        _cashing = New CheckCashing()
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
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        DocumentDate = Me.GetDateServer()

        'If Sequence IsNot Nothing AndAlso Sequence.Id > 0 Then
        '    Me._cashing = New CheckCashing()
        '    If Me._sequence.IsManual Then
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    Else
        '        If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '            Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
        '        ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '            Dim res = (From ou As TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
        '            If res IsNot Nothing AndAlso res.Count > 0 Then
        '                Me._idCurrentSequence = res(0).Id
        '            Else
        '                Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
        '            End If
        '        End If
        '        If Not Me._sequence.Sequential Then
        '            If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '                If Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
        '                    Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Using model As New MCommonTreasury(Me.Tag)
        '                        Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
        '                    End Using
        '                    If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
        '                        Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                    Else
        '                        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                    End If
        '                End If
        '            Else
        '                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        'End If
        'DocumentDate = Me.GetDateServer()
    End Function

    ''' <summary>
    ''' Creates the voucher class.
    ''' </summary>
    Private Sub CreateVoucherClass()
        ListVoucherClass = New List(Of Tuple(Of Integer, String))
        ListVoucherClass.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("VoucherClassPayment", NAME_MODULE)))
        ListVoucherClass.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("VoucherClassRefund", NAME_MODULE)))
        ListVoucherClass.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("DocumentTypeConsignmentTransfer", NAME_MODULE)))
        INDgleVoucherClass.Properties.DataSource = ListVoucherClass
    End Sub

    ''' <summary>
    ''' Consulta el total de los debitos y creditos
    ''' </summary>
    ''' <returns></returns>
    Private Function getVoucherValue() As Tuple(Of Decimal, Decimal, Decimal)
        Dim _byMilValue As Decimal = 0
        If TaxByMil IsNot Nothing AndAlso TaxByMil <> 0 Then
            _byMilValue = TaxByMil
        End If
        Return New Tuple(Of Decimal, Decimal, Decimal)(Value, _byMilValue, 0)
    End Function

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Cheque Actual", .FieldName = "CurrentCheckNumber", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Cheque Siguiente", .FieldName = "NextCheckNumber", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "CancellationDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCheckCashing
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICashing.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDsleVoucherTransactionNumber.Enabled = value
            INDgleVoucherClass.Enabled = value
            INDsleThirdParty.Enabled = value
            INDsleEntityAccount.Enabled = value
            INDsleMainAccount.Enabled = value
            INDspnCurrentCheck.Enabled = value
            INDspnNextCheck.Enabled = value
            INDdeDate.Enabled = value
            INDmeDetail.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()

            If value Then
                INDsleVoucherTransactionNumber.Focus()
                INDdeDate.Properties.ReadOnly = True
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements ICashing.CleanControls
        INDlcRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        INDbteCode.Text = String.Empty
        INDsleVoucherTransactionNumber.EditValue = Nothing
        INDgleVoucherClass.EditValue = Nothing
        INDspnCurrentCheck.EditValue = 0
        INDspnNextCheck.EditValue = 0
        INDdeDate.EditValue = Nothing
        INDmeDetail.EditValue = String.Empty
        INDlcRoot.EndUpdate()

        Value = 0
        TaxByMil = Nothing

        ctrTmp.PrintValueVoucher()

        _statePopUpVoucherTransaction = False

        INDsleEntityAccount.Text = String.Empty
        INDsleMainAccount.Text = String.Empty
        INDsleThirdParty.Text = String.Empty

        INDsleThirdParty.Properties.ReadOnly = True
        INDsleEntityAccount.Properties.ReadOnly = True
        INDsleMainAccount.Properties.ReadOnly = True
        INDspnCurrentCheck.Properties.ReadOnly = True
        INDgleVoucherClass.Properties.ReadOnly = True
        INDsleVoucherTransactionNumber.Properties.NullText = String.Empty
        INDliThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        _voucherTransaction = Nothing
        _cashing = Nothing
        DeleteBlockedRecord()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._cashing.Code, Me._voucherTransaction.Code, Me._cashing.CancellationDate), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me._cashing.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._cashing.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._cashing.Code, Me._voucherTransaction.Code, Me._cashing.CancellationDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._cashing.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        Dim _cancellationCheck As New CancellationChecks()
        With _cancellationCheck
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .IdEntityAccount = _entityBankAccount.Id
            .IdCheckBook = _entityBankAccount.Checkbooks.ToList().Where(Function(x) x.Status = 1).Cast(Of Checkbooks).FirstOrDefault().Id
            .CheckNumber = CurrentCheck
            .Description = Detail
        End With
        With _cashing
            .Code = Code
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .VoucherTransactionId = _voucherTransaction.Id
            .CurrentCheckNumber = CurrentCheck
            .NextCheckNumber = NextCheck
            .CancellationDate = DocumentDate
            .CancellationDescription = Detail
            .CancellationChecks = _cancellationCheck
        End With
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
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
                Using Model As New MCashing(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetCashing(INDbteCode.Text.Trim)
                    INDlcRoot.BeginUpdate()
                    _cashing = resultOperation.ObjectEmbbeded
                    _voucherTransaction = _cashing.VoucherTransaction
                    If _cashing IsNot Nothing AndAlso _cashing.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MCommonTreasury(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecordTreasury(CStr(Me.Tag), CStr(_cashing.Id))
                            With _cashing
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Code = .Code
                                NextCheck = .NextCheckNumber
                                DocumentDate = .CancellationDate
                                Detail = .CancellationDescription
                            End With
                            LoadVoucerTransactionData()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._cashing.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecordTreasury(
                                New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _cashing.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(_cashing.Id, Me.Tag.ToString(), Nothing, GetType(CheckCashing).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _voucherTransaction.Id, 0, _voucherTransaction.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDsleVoucherTransactionNumber.Enabled = False
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewCashing()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If

        'If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
        '    If Me.BarraBotones.PermiteConsultar = False Then
        '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '        Exit Function
        '    End If

        '    Me.BarraBotones.StatusRecordVisible = True
        '    Using Model As New MCashing(Me.Tag)
        '        AsyncLoader(True)
        '        Dim resultOperation = Await Model.GetCashing(Code)
        '        INDlcRoot.BeginUpdate()
        '        _cashing = resultOperation.ObjectEmbbeded
        '        _voucherTransaction = _cashing.VoucherTransaction
        '        If Not _cashing Is Nothing Then
        '            If _cashing.Id > 0 Then
        '                Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
        '                    Dim result = Await ModelCommonTreasury.GetBlockRecordTreasury(Me.Tag, _cashing.Id)

        '                    With _cashing
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
        '                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
        '                        Code = .Code
        '                        NextCheck = .NextCheckNumber
        '                        DocumentDate = .CancellationDate
        '                        Detail = .CancellationDescription
        '                    End With

        '                    LoadVoucerTransactionData()

        '                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._cashing.Code)
        '                    If result.Id = 0 Then
        '                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                        state.State = Domain.Base.Entities.ObjectState.Added
        '                        record = New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _cashing.Id}
        '                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordTreasury(record)
        '                        record = operation.ObjectEmbbeded
        '                    Else
        '                        record = result
        '                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                    End If
        '                    Me.BarraBotones.SetDocuments(_cashing.Id, Me.Tag.ToString(), Nothing, GetType(CheckCashing).Name)
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
        '                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '                    'Dim reportDef As New Reporter.rptCheckChange()
        '                    'Me.BarraBotones.PrintReport(reportDef, _voucherTransaction.Id, False, Me.Tag, Nothing, "FrmCheckCashing")
        '                    Me.BarraBotones.PrintReport(PrintReportAction.None, _voucherTransaction.Id, 0, _voucherTransaction.Id)
        '                    AsyncLoader(False)
        '                    ActionsOnControls = True
        '                    INDsleVoucherTransactionNumber.Enabled = False
        '                End Using
        '            Else
        '                'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                'Me.Code = String.Empty
        '                AsyncLoader(False)
        '                If Me._sequence.IsManual Then
        '                    Me.NewCashing()
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                    Me.Code = String.Empty
        '                    Deshacer()
        '                    INDbteCode.Focus()
        '                End If
        '            End If
        '        Else
        '            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            'Me.Code = String.Empty
        '            'Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        '            AsyncLoader(False)
        '            If Me._sequence.IsManual Then
        '                Me.NewCashing()
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.Code = String.Empty
        '                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        '                Deshacer()
        '                INDbteCode.Focus()
        '            End If
        '        End If
        '    End Using
        'End If
        'INDlcRoot.EndUpdate()
    End Function

    ''' <summary>
    ''' Loads the voucer transaction data.
    ''' </summary>
    Private Async Sub LoadVoucerTransactionData()

        If _cashing.Id = 0 Then
            Using ModelEntity As New MEntityAccount(Me.Tag)
                _entityBankAccount = Await ModelEntity.GetEntityBankAccountById(_voucherTransaction.IdEntityBankAccount)
                If _entityBankAccount IsNot Nothing AndAlso _entityBankAccount.Id > 0 AndAlso _entityBankAccount.Checkbooks.ToList().FindAll(Function(x) x.Status = 1).Cast(Of Checkbooks).ToList().Count > 0 Then
                    INDspnNextCheck.Properties.MaxValue = _entityBankAccount.Checkbooks.Where(Function(x) x.Status = 1).Cast(Of Checkbooks).ToList().ElementAt(0).EndNumber
                    INDspnNextCheck.Properties.MinValue = _entityBankAccount.Checkbooks.Where(Function(x) x.Status = 1).Cast(Of Checkbooks).ToList().ElementAt(0).CurrentNumber
                    INDspnNextCheck.EditValue = INDspnNextCheck.Properties.MinValue
                Else
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CheckActiveNoExist", NAME_MODULE)
                End If
            End Using
            If _voucherTransaction.CheckNumber Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró Cheque"
                Exit Sub
            End If
            CurrentCheck = _voucherTransaction.CheckNumber
        Else
            CurrentCheck = _cashing.CurrentCheckNumber
        End If

        INDsleVoucherTransactionNumber.Properties.NullText = _voucherTransaction.Code
        VoucherClass = _voucherTransaction.VoucherClass
        Value = _voucherTransaction.Value
        TaxByMil = _voucherTransaction.TaxByMilValue
        Status = _voucherTransaction.Status

        INDsleEntityAccount.Text = _voucherTransaction.FullNameEntityBankAccount
        INDsleThirdParty.Text = _voucherTransaction.FullNameThird
        INDsleMainAccount.Text = _voucherTransaction.FullNameMainAccount

        ctrTmp.PrintValueVoucher()
    End Sub

    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._cashing IsNot Nothing AndAlso Me._cashing.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Icrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MCashing(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of CheckCashing) = Await Model.SaveCashing(Me._cashing, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _cashing.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._cashing = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _voucherTransaction.Id, 0, _voucherTransaction.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _voucherTransaction.Id, 0, _voucherTransaction.Id)
                    End Select
                    Deshacer()
                    ShowMessage(result.StatusCode) = result.Message
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    If result.Message IsNot Nothing Then
                        generateListError(result.Message)
                    End If
                End If
            End Using
            'Using Model As New MCashing(Me.Tag.ToString())
            '    AsyncLoader(True)
            '    Dim Result = Await Model.SaveCashing(Me._cashing, Me._idCurrentSequence)
            '    AsyncLoader(False)
            '    If Result.StateResult = True Then
            '        If _cashing.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            '            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
            '                Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
            '            End If
            '            If Me._sequence.Sequential Then
            '                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
            '            Else
            '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
            '            End If
            '        ElseIf _cashing.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
            '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
            '        End If
            '        Me._cashing = Result.ObjectEmbbeded
            '        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

            '        Select Case varImp
            '            Case 1
            '                Me.BarraBotones.PrintReport(PrintReportAction.Create, _voucherTransaction.Id, 0, _voucherTransaction.Id)
            '            Case 2
            '                Me.BarraBotones.PrintReport(PrintReportAction.Update, _voucherTransaction.Id, 0, _voucherTransaction.Id)
            '        End Select

            '        _searchMode = False
            '        Me.Deshacer()
            '    Else
            '        If Result.Message IsNot Nothing Then
            '            generateListError(Result.Message)
            '        End If
            '    End If
            'End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewCashing()
        End If
    End Sub
#End Region

#Region "Bar button Events"
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
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar

    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
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

    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Clcik Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir

        'Dim reportDef As New Reporter.rptCheckChange()
        'Me.BarraBotones.PrintReport(reportDef, _voucherTransaction.Id, True, Me.Tag, Nothing, "FrmCheckCashing")
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _voucherTransaction.Id, 0, _voucherTransaction.Id)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
#End Region

    Public Enum eVoucherClss
        Payment = 1
        Refund = 2
        Transfer = 3
    End Enum

End Class