Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Glosas.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources

''' <summary>
''' Formulario modal Acciones parametros de tiempo
''' </summary>
Public Class FrmTimeParametersActions

#Region "Fields"

    ''' <summary>
    ''' Variable parametros de tiempo
    ''' </summary>
    Public _timeParameters As TimeParameters
    ''' <summary>
    ''' Variable entidad
    ''' </summary>
    Private _customer As Domain.Entities.Customer
    ''' <summary>
    ''' Variable tag formulario
    ''' </summary>
    Private _tagForm As String
    ''' <summary>
    ''' Lista de Parametros de tiempo
    ''' </summary>
    Private _listParameters As List(Of TimeParameters)
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private indigoAux As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

#End Region

#Region "Builder and Handlers"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="listParameters">Lista de Parametros de Tiempo</param>
    ''' <param name="timeParameters">Objeto Parametros de Tiempo</param>
    ''' <param name="customer">Objeto Entidad</param>
    ''' <param name="tagForm">Tag del formulario</param>
    Sub New(ByRef listParameters As List(Of TimeParameters), timeParameters As TimeParameters, customer As Domain.Entities.Customer, tagForm As String)
        ' This call is required by the designer.
        InitializeComponent()
        Me._timeParameters = timeParameters
        Me._customer = customer
        Me._tagForm = tagForm
        Me._listParameters = listParameters
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Sub FrmTimeParametersActions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.indigoAux = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
    End Sub


    Private Sub Guardar()
        AssignValues(Me._timeParameters)
        If Me._listParameters.Where(Function(x) x.Customer IsNot Nothing).ToList.Exists(Function(x) x.IdCustomer = Me._timeParameters.IdCustomer) Then
            Dim dataListParameters = Me._listParameters.Where(Function(x) x.Id = Me._timeParameters.Id).FirstOrDefault
            AssignValues(dataListParameters)
            dataListParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Else
            Me._listParameters.Add(Me._timeParameters)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _timeParameters = Nothing
        _customer = Nothing
        _tagForm = Nothing
        _listParameters = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento click sobre el boton de operaciones
    ''' </summary>
    Private Sub INDOperationSmb_Click(sender As Object, e As EventArgs)
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(Me._tagForm))
        Dim result
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        If Me._timeParameters Is Nothing Then
            Me._timeParameters = New TimeParameters
            'Me.INDOperationSmb.Text = obtenerRecurso(Eresources.AgregarComunes, Eform.Comunes)
            Me.BarraBotones.RibbonPagEform.Visible = False
        Else
            LoadValues()
            Me.BarraBotones.RibbonPagEform.Visible = False
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            'Me.INDOperationSmb.Text = obtenerRecurso(Eresources.ModificarComunes, Eform.Comunes)
            Using Model As New MTimeParameters(Me._tagForm)
                result = Await Model.GetBlockRecord(Me._tagForm, Me._timeParameters.Id)
            End Using
            If result.Id = 0 Then
                If Me._timeParameters.Id > 0 Then
                    Me.BarraBotones.SetDocuments(Me._timeParameters.Id, Me._tagForm)
                End If
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigoAux.UserIndigoName, .IdForm = Me._tagForm, .CodUser = Me.indigoAux.UserIndigo, .IdRecord = Me._timeParameters.Id}
                Dim operation
                Using Model As New MTimeParameters(Me._tagForm)
                    operation = Await Model.SaveBlockRecord(record)
                End Using
                record = operation.ObjectEmbbeded
            Else
                If Not (Me.record IsNot Nothing AndAlso Me.record.CodUser = Me.indigoAux.UserIndigo AndAlso Me.record.IdRecord = Me._timeParameters.Id) Then
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            End If
        End If
        Me.INDParametersLycg.Text = String.Format(obtenerRecurso(Eresources.TituloParametrosComunes, Eform.Comunes), Me._customer.Name)
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmTimeParametersActions_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Valida tiempos
    ''' </summary>
    Private Sub INDspeMaxTimeResponseExObjection_EditValueChanged(sender As Object, e As EventArgs) Handles INDspeMaxTimeResponseExObjection.EditValueChanged
        Me.INDspeMaxTimeResponseObjection.Properties.MinValue = Me.INDspeMaxTimeResponseExObjection.EditValue
        Me.INDspeMaxTimeSendResponseObjection.Properties.MinValue = Me.INDspeMaxTimeResponseObjection.EditValue
    End Sub

    ''' <summary>
    ''' Valida tiempos
    ''' </summary>
    Private Sub INDspeMaxTimeResponseObjection_EditValueChanged(sender As Object, e As EventArgs) Handles INDspeMaxTimeResponseObjection.EditValueChanged
        Me.INDspeMaxTimeResponseObjection.Properties.MinValue = Me.INDspeMaxTimeResponseExObjection.EditValue
        Me.INDspeMaxTimeSendResponseObjection.Properties.MinValue = Me.INDspeMaxTimeResponseObjection.EditValue
    End Sub

    ''' <summary>
    ''' Valida tiempos
    ''' </summary>
    Private Sub INDspeMaxTimeSendResponseObjection_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDspeMaxTimeSendResponseObjection.EditValueChanging
        Me.INDspeMaxTimeSendResponseObjection.Properties.MinValue = Me.INDspeMaxTimeResponseObjection.EditValue
    End Sub

    ''' <summary>
    ''' Valida tiempos
    ''' </summary>
    Private Sub INDspeMaxTimeResponseExReiteration_EditValueChanged(sender As Object, e As EventArgs) Handles INDspeMaxTimeResponseExReiteration.EditValueChanged
        Me.INDspeMaxTimeResponseReiteration.Properties.MinValue = Me.INDspeMaxTimeResponseExReiteration.EditValue
        Me.INDspeMaxTimeSendResponseReiteration.Properties.MinValue = Me.INDspeMaxTimeResponseReiteration.EditValue
    End Sub

    ''' <summary>
    ''' Valida tiempos
    ''' </summary>
    Private Sub INDspeMaxTimeResponseReiteration_EditValueChanged(sender As Object, e As EventArgs) Handles INDspeMaxTimeResponseReiteration.EditValueChanged
        Me.INDspeMaxTimeResponseReiteration.Properties.MinValue = Me.INDspeMaxTimeResponseExReiteration.EditValue
        Me.INDspeMaxTimeSendResponseReiteration.Properties.MinValue = Me.INDspeMaxTimeResponseReiteration.EditValue
    End Sub

    ''' <summary>
    ''' Valida tiempos
    ''' </summary>
    Private Sub INDspeMaxTimeSendResponseReiteration_EditValueChanged(sender As Object, e As EventArgs) Handles INDspeMaxTimeSendResponseReiteration.EditValueChanged
        Me.INDspeMaxTimeSendResponseReiteration.Properties.MinValue = Me.INDspeMaxTimeResponseReiteration.EditValue
    End Sub

    ''' <summary>
    ''' Metodo para asignar valores
    ''' </summary>
    ''' <param name="timeParam">Parametros de Tiempo</param>
    Sub AssignValues(ByRef timeParam As TimeParameters)
        If timeParam.MaxTimeResponse <> Me.INDspeMaxTimeResponseObjection.EditValue Then
            timeParam.MaxTimeResponse = Me.INDspeMaxTimeResponseObjection.EditValue
        End If
        If timeParam.MaxTimeSendingDocumentResponse <> Me.INDspeMaxTimeSendResponseObjection.EditValue Then
            timeParam.MaxTimeSendingDocumentResponse = Me.INDspeMaxTimeSendResponseObjection.EditValue
        End If
        If timeParam.MaxTimeExtemporaneousGlosa <> Me.INDspeMaxTimeResponseExObjection.EditValue Then
            timeParam.MaxTimeExtemporaneousGlosa = Me.INDspeMaxTimeResponseExObjection.EditValue
        End If
        If timeParam.MaxTimeExtemporaneousReiteration <> Me.INDspeMaxTimeResponseExReiteration.EditValue Then
            timeParam.MaxTimeExtemporaneousReiteration = Me.INDspeMaxTimeResponseExReiteration.EditValue
        End If
        If timeParam.MaxTimeSendingReiterationDocumentResponse <> Me.INDspeMaxTimeSendResponseReiteration.EditValue Then
            timeParam.MaxTimeSendingReiterationDocumentResponse = Me.INDspeMaxTimeSendResponseReiteration.EditValue
        End If
        If timeParam.MaxTimeReiterationResponse <> Me.INDspeMaxTimeResponseReiteration.EditValue Then
            timeParam.MaxTimeReiterationResponse = Me.INDspeMaxTimeResponseReiteration.EditValue
        End If
        If timeParam.MaxTimeConciliation <> Me.INDspeMaxTimeConciliation.EditValue Then
            timeParam.MaxTimeConciliation = Me.INDspeMaxTimeConciliation.EditValue
        End If
        If timeParam.MaxTimeJuridicalDebtCollectionStart <> Me.INDspeMaxTimeJuridicalDebit.EditValue Then
            timeParam.MaxTimeJuridicalDebtCollectionStart = Me.INDspeMaxTimeJuridicalDebit.EditValue
        End If
        If timeParam.NotificationPeriodicity <> Me.INDspeNotificationPeriodicity.EditValue Then
            timeParam.NotificationPeriodicity = Me.INDspeNotificationPeriodicity.EditValue
        End If
        If timeParam.IsDaybusinessSaturday <> Me.INDrgpIsDaybusinessSaturday.EditValue Then
            timeParam.IsDaybusinessSaturday = Me.INDrgpIsDaybusinessSaturday.EditValue
        End If
        If timeParam.IsDaybusinessSunday <> Me.INDrgpIsDaybusinessSunday.EditValue Then
            timeParam.IsDaybusinessSunday = Me.INDrgpIsDaybusinessSunday.EditValue
        End If
        If Me._customer IsNot Nothing AndAlso timeParam.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
            timeParam.Customer = Me._customer
            timeParam.IdCustomer = Me._customer.Id
        End If
    End Sub

    ''' <summary>
    ''' Evento Click Deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        If Me._timeParameters IsNot Nothing Then
            CleanControls()
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MTimeParameters(Me._tagForm)
                Await Model.DeleteBlockRecord(record)
            End Using
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    Sub CleanControls()
        Me.INDspeMaxTimeResponseExObjection.EditValue = String.Empty
        Me.INDspeMaxTimeSendResponseObjection.EditValue = String.Empty
        Me.INDspeMaxTimeResponseObjection.EditValue = String.Empty
        Me.INDspeMaxTimeResponseExReiteration.EditValue = String.Empty
        Me.INDspeMaxTimeSendResponseReiteration.EditValue = String.Empty
        Me.INDspeMaxTimeResponseReiteration.EditValue = String.Empty
        Me.INDspeMaxTimeConciliation.EditValue = String.Empty
        Me.INDspeMaxTimeJuridicalDebit.EditValue = String.Empty
        Me.INDspeNotificationPeriodicity.EditValue = String.Empty
        Me.INDrgpIsDaybusinessSaturday.EditValue = False
        Me.INDrgpIsDaybusinessSunday.EditValue = False
        Me.INDspeMaxTimeResponseObjection.Focus()
    End Sub

    ''' <summary>
    ''' Cargar valores
    ''' </summary>
    Sub LoadValues()
        Me.INDspeMaxTimeResponseExObjection.EditValue = Me._timeParameters.MaxTimeExtemporaneousGlosa
        Me.INDspeMaxTimeSendResponseObjection.EditValue = Me._timeParameters.MaxTimeSendingDocumentResponse
        Me.INDspeMaxTimeResponseObjection.EditValue = Me._timeParameters.MaxTimeResponse
        Me.INDspeMaxTimeResponseExReiteration.EditValue = Me._timeParameters.MaxTimeExtemporaneousReiteration
        Me.INDspeMaxTimeSendResponseReiteration.EditValue = Me._timeParameters.MaxTimeSendingReiterationDocumentResponse
        Me.INDspeMaxTimeResponseReiteration.EditValue = Me._timeParameters.MaxTimeReiterationResponse
        Me.INDspeMaxTimeConciliation.EditValue = Me._timeParameters.MaxTimeConciliation
        Me.INDspeMaxTimeJuridicalDebit.EditValue = Me._timeParameters.MaxTimeJuridicalDebtCollectionStart
        Me.INDspeNotificationPeriodicity.EditValue = Me._timeParameters.NotificationPeriodicity
        Me.INDrgpIsDaybusinessSaturday.EditValue = Me._timeParameters.IsDaybusinessSaturday
        Me.INDrgpIsDaybusinessSunday.EditValue = Me._timeParameters.IsDaybusinessSunday
    End Sub

#End Region


    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub FrmTimeParametersActions_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDspeMaxTimeResponseExObjection.Focus()
    End Sub
End Class