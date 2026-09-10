'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Diego Adnrés Roldán Lozano
' Created          : 23-06-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Accounting.MVP
Imports Domain.Crystal.Entities

#End Region

Public Class FrmAddBedRate
    Implements IBedRate

#Region "Properties"

    Public Const MODULE_NAME As String = "Billing"

    Public Property CODCENATE As String Implements IBedRate.CODCENATE
        Get
            Return INDsleCenAten.EditValue
        End Get
        Set(value As String)
            INDsleCenAten.EditValue = value
        End Set
    End Property

    Public Property CODICAMAS1 As Integer Implements IBedRate.CODICAMAS
    '    Get
    '        Return INDsleCodCama.EditValue
    '    End Get
    '    Set(value As Integer)
    '        INDsleCodCama.EditValue = value
    '    End Set
    'End Property

    Public Property CODTIPEST As String Implements IBedRate.CODTIPEST
        Get
            Return INDsleTipEst.EditValue
        End Get
        Set(value As String)
            INDsleTipEst.EditValue = value
        End Set
    End Property

    Public Property GENCUPS As Integer? Implements IBedRate.GENCUPS
        Get
            Return INDsleLiqObs.EditValue
        End Get
        Set(value As Integer?)
            INDsleLiqObs.EditValue = value
        End Set
    End Property

    Public Property GENCUPS2 As Integer Implements IBedRate.GENCUPS2
        Get
            Return INDsleLiqHos.EditValue
        End Get
        Set(value As Integer)
            INDsleLiqHos.EditValue = value
        End Set
    End Property

    Public Property NUMHOREST As Byte Implements IBedRate.NUMHOREST
        Get
            Return INDspnNumHorEst.EditValue
        End Get
        Set(value As Byte)
            INDspnNumHorEst.EditValue = value
        End Set
    End Property

    Public Property TIPLIQEST As Byte Implements IBedRate.TIPLIQEST
        Get
            Return INDgleTipLiqEst.EditValue
        End Get
        Set(value As Byte)
            INDgleTipLiqEst.EditValue = value
        End Set
    End Property

    Public Property UFUCODIGO As String Implements IBedRate.UFUCODIGO
        Get
            Return INDsleUniFun.EditValue
        End Get
        Set(value As String)
            INDsleUniFun.EditValue = value
        End Set
    End Property

    Public Property CODCENATEXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBedRate.CODCENATEXpo
        Get
            Return INDsleCenAten.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCenAten.Properties.DataSource = value
        End Set
    End Property

    Public Property GENCUPS2Xpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBedRate.GENCUPS2Xpo
        Get
            Return INDsleLiqObs.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLiqObs.Properties.DataSource = value
        End Set
    End Property

    Public Property GENCUPSXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBedRate.GENCUPSXpo
        Get
            Return INDsleLiqHos.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLiqHos.Properties.DataSource = value
        End Set
    End Property

    Public Property CODTIPESTXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBedRate.CODTIPESTXpo
        Get
            Return INDsleTipEst.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleTipEst.Properties.DataSource = value
        End Set
    End Property

    Public Property UFUCODIGOXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IBedRate.UFUCODIGOXpo
        Get
            Return INDsleUniFun.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleUniFun.Properties.DataSource = value
        End Set
    End Property

    Public ReadOnly Property MyTag As String Implements IBedRate.MyTag
        Get
            Return Me.Tag.ToString()
        End Get
    End Property

    Property CODICAMAS As Integer

    Property BedDescription As String
        Get
            Return INDsleCodCama.Properties.NullText
        End Get
        Set(value As String)
            INDsleCodCama.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling
    ''' <summary>
    ''' The _presenter
    ''' </summary>
    Private _presenter As PBedRate
    ''' <summary>
    ''' entidad de tarifa de cama
    ''' </summary>
    Private _bedRate As CHGENTARI
    ''' <summary>
    ''' Evento para refrescar el datasource de las tarifas de las camas en el frmRateBed
    ''' </summary>
    Public Event OnRefreshBedRateDatasource()
    ''' <summary>
    ''' Datasource de tarifas de camas que se envia del form padre
    ''' </summary>
    Property BedRateDatasource As List(Of CHGENTARI)

    Property BedRate As CHGENTARI
        Get
            Return _bedRate
        End Get
        Set(value As CHGENTARI)
            _bedRate = value
        End Set
    End Property

    Public Property IsEdit As Boolean

#End Region

#Region "Handlers"
#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _record = Nothing
        _presenter = Nothing
        _bedRate = Nothing
        BedRateDatasource = Nothing
        IsEdit = Nothing
    End Sub


    Private Sub FrmAddBedRate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        'Me._idOperativeUnit = BarraBotones.OperatingUnitValue
        Me.BarraBotones.OperatingUnitVisible = False
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PBedRate(Me)

        CreateStayTypeLiquidation()
        Deshacer()
        If IsEdit Then
            LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmInvoicesCapitatedEntities control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAddBedRate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Handles the Activated event of the FrmInvoicesCapitatedEntities control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    Private Sub FrmInvoicesCapitatedEntities_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDsleCodCama.Enabled = True Then
            INDsleCodCama.Focus()
        Else
            INDsleTipEst.Focus()
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    Private Sub INDgleTipLiqEst_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleTipLiqEst.EditValueChanged
        If INDgleTipLiqEst.EditValue IsNot Nothing Then
            If CByte(INDgleTipLiqEst.EditValue) = 1 Then
                INDliCups1.HideControl(False)
            Else
                INDsleLiqObs.EditValue = Nothing
                INDliCups1.HideControl()
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmAddBedRate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDsleTipEst_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleTipEst.QueryPopUp
        If CODTIPESTXpo Is Nothing Then
            _presenter.GetAllStayType()
        End If
    End Sub

    Private Sub INDsleLiqObs_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLiqObs.QueryPopUp
        If GENCUPSXpo Is Nothing Then
            _presenter.GetAllCupsEntity()
        End If
    End Sub

    Private Sub INDsleLiqHos_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleLiqHos.QueryPopUp
        If GENCUPS2Xpo Is Nothing Then
            _presenter.GetAllCupsEntity()
        End If
    End Sub

    Private Sub INDsleCenAten_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCenAten.QueryPopUp
        If CODCENATEXpo Is Nothing Then
            _presenter.GetAllCareCenter()
        End If
    End Sub

    Private Sub INDsleUniFun_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleUniFun.QueryPopUp
        If UFUCODIGOXpo Is Nothing Then
            _presenter.GetAllFunctionalUnit()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDsleLiqObs_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLiqObs.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("970", GENCUPS, True)
            _presenter.GetAllCupsEntity()
        End If
    End Sub

    Private Sub INDsleLiqHos_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleLiqHos.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("970", GENCUPS2, True)
            _presenter.GetAllCupsEntity()
        End If
    End Sub
#End Region
#End Region

#Region "Methods"
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    Public Sub NewBedRate()
        _bedRate = New CHGENTARI()
        ActionsOnControls = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        INDliCups1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Creates the stay type liquidation.
    ''' </summary>
    Private Sub CreateStayTypeLiquidation()
        Dim stayTypeDatasource As New List(Of Tuple(Of Byte, String))()
        stayTypeDatasource.Add(New Tuple(Of Byte, String)(1, "Observacion Urgencias"))
        stayTypeDatasource.Add(New Tuple(Of Byte, String)(2, "Recuperacion Post - Quirurgico"))
        stayTypeDatasource.Add(New Tuple(Of Byte, String)(3, "Hospitalaria"))
        INDgleTipLiqEst.Properties.DataSource = stayTypeDatasource
    End Sub

    ''' <summary>
    ''' Esta Propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBedRate.ActionsOnControls
        Set(value As Boolean)
            INDsleCodCama.Enabled = False
            INDsleTipEst.Enabled = value
            INDsleLiqObs.Enabled = value
            INDsleLiqHos.Enabled = value
            INDsleCenAten.Enabled = value
            INDsleUniFun.Enabled = value
            INDgleTipLiqEst.Enabled = value
            INDspnNumHorEst.Enabled = value

            INDsleTipEst.Focus()
        End Set
    End Property

    ''' <summary>
    ''' Asigna los valores a la Entidad
    ''' </summary>
    Private Sub AssigningValues()
        With _bedRate
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .CODICAMASExt = CODICAMAS
            .CODTIPESTExt = CODTIPEST
            '.CHCAMASHO.CODICAMAS = CODICAMAS
            '.CHTIPESTA.CODTIPEST = CODTIPEST
            .TIPLIQEST = TIPLIQEST
            .NUMHOREST = NUMHOREST
            .GENCUPS = GENCUPS
            .GENCUPS2 = GENCUPS2
            .CODCENATEExt = CODCENATE
            .UFUCODIGOExt = UFUCODIGO
            '.ADCENATEN.CODCENATE = CODCENATE
            '.INUNIFUNC.UFUCODIGO = UFUCODIGO
        End With
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Public Async Sub LoadControls()
        If Not String.IsNullOrEmpty(_bedRate.CODCONCEC) AndAlso Not _bedRate.CODCONCEC = 0 Then
            If _bedRate.CODCONCEC > 0 Then
                Using modelBlock As New MBlockRecordAndSequense(Me.Tag)
                    Dim result = Await modelBlock.GetBlockRecord(Me.Tag, _bedRate.CODCONCEC)
                    With _bedRate
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        CODICAMAS = _bedRate.CHCAMASHO.CODICAMAS
                        CODTIPEST = _bedRate.CHTIPESTA.CODTIPEST
                        TIPLIQEST = _bedRate.TIPLIQEST
                        NUMHOREST = _bedRate.NUMHOREST
                        GENCUPS = _bedRate.GENCUPS
                        GENCUPS2 = _bedRate.GENCUPS2
                        CODCENATE = _bedRate.ADCENATEN.CODCENATE
                        UFUCODIGO = _bedRate.INUNIFUNC.UFUCODIGO
                    End With

                    INDsleTipEst.Properties.NullText = String.Concat(_bedRate.CHTIPESTA.CODTIPEST, " - ", _bedRate.CHTIPESTA.DESTIPEST)
                    INDsleLiqObs.Properties.NullText = _bedRate.CupsEntityObservationFullName
                    INDsleLiqHos.Properties.NullText = _bedRate.CupsEntityHospitalizationFullName
                    INDsleCenAten.Properties.NullText = _bedRate.CareCenterFullName
                    INDsleUniFun.Properties.NullText = _bedRate.FunctionalUnitFullName
                    _bedRate = _bedRate
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _bedRate.CODCONCEC}
                        Dim operation = Await modelBlock.SaveBlockRecord(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    'Me.BarraBotones.SetDocuments(_bedRate.CODCONCEC, Me.Tag.ToString(), Nothing, GetType(CHGENTARI).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                    ActionsOnControls = True
                End Using
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
            End If
            IsEdit = False
        End If
    End Sub

    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlcRoot.BeginUpdate()
        ActionsOnControls = True
        INDsleTipEst.EditValue = Nothing
        INDgleTipLiqEst.EditValue = Nothing
        INDspnNumHorEst.EditValue = 0
        INDsleLiqObs.EditValue = Nothing
        INDsleLiqHos.EditValue = Nothing
        INDsleCenAten.EditValue = Nothing
        INDsleUniFun.EditValue = Nothing
        INDlcRoot.EndUpdate()

        INDsleTipEst.Properties.NullText = String.Empty
        INDsleLiqObs.Properties.NullText = String.Empty
        INDsleLiqHos.Properties.NullText = String.Empty
        INDsleCenAten.Properties.NullText = String.Empty
        INDsleUniFun.Properties.NullText = String.Empty

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()

        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    Private Function ValidateFields() As Boolean
        If ValidateControls() = False Then
            Return False
        End If
        If BedRateDatasource IsNot Nothing Then
            If BedRateDatasource.FindAll(Function(o) o.CHCAMASHO.CODICAMAS = CODICAMAS AndAlso o.TIPLIQEST = TIPLIQEST AndAlso o.ADCENATEN.CODCENATE = CODCENATE _
                                             AndAlso o.INUNIFUNC.UFUCODIGO = UFUCODIGO AndAlso o.CHTIPESTA.CODTIPEST = CODTIPEST).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar una tarifa para los parámetros seleccionados"
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub
#End Region

#Region "Crud"
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If Not IsEdit Then
            NewBedRate()
        End If
        'Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateFields() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MBedRate(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveBedRateAsync(Me._bedRate)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _bedRate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.CODCONCEC)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._bedRate = Result.ObjectEmbbeded
                    Me.Deshacer()
                    Me.NewBedRate()
                    RaiseEvent OnRefreshBedRateDatasource()
                Else
                    If Result.Message IsNot Nothing Then
                        GenerateListError(Result.Message)
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub GenerateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Deshacer()
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
#End Region

#Region "BarButtonEvents"

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
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
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
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            'Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

End Class