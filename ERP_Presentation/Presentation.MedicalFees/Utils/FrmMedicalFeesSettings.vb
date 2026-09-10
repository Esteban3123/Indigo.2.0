'***********************************************************************
' Assembly         : Presentacion.MedicalFees.Utils
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Accounting
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MedicalFees.MVP
Imports Presentation.Payments

#End Region

Public Class FrmMedicalFeesSettings
    Implements IMedicalFeesSettings

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del concepto de pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptId As Integer? Implements IMedicalFeesSettings.AccountPayableConceptId
        Get
            Return INDsleAccountPayableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPayableConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de los conceptos de pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptXpo As XPInstantFeedbackSource Implements IMedicalFeesSettings.AccountPayableConceptXpo
        Get
            Return INDsleAccountPayableConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobantes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JournalVoucherTypeId As Integer? Implements IMedicalFeesSettings.JournalVoucherTypeId
        Get
            Return INDsleJournalVoucherType.EditValue
        End Get
        Set(value As Integer?)
            INDsleJournalVoucherType.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobantes de reconocimiento de costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostRecognitionVoucherId As Integer? Implements IMedicalFeesSettings.CostRecognitionVoucherId
        Get
            Return INDSleCostRecognitionVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDSleCostRecognitionVoucher.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobantes de reversion de reconocimiento de costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostRecognitionReversalVoucherId As Integer? Implements IMedicalFeesSettings.CostRecognitionReversalVoucherId
        Get
            Return INDSleCostRecognitionReversalVoucher.EditValue
        End Get
        Set(value As Integer?)
            INDSleCostRecognitionReversalVoucher.EditValue = value
        End Set
    End Property

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MedicalFees"

    Dim filter() As Object = {5, True}

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMedicalFees

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PMedicalFeesSettings

    ''' <summary>
    ''' Representa la entidad de parametros de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _settingMedicalFees As SettingMedicalFees

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Método: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        DeleteBlockedRecord()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Método: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Using model As New MMedicalFeesSettings(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveMedicalFeesSettings(_settingMedicalFees)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If _settingMedicalFees.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                ElseIf _settingMedicalFees.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me._settingMedicalFees = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                DeleteBlockedRecord()
                CleanControls()
                LoadControls()
                INDsleAccountPayableConcept.Focus()

                Me.BarraBotones.CleanAuditBasic()
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Result.ObjectEmbbeded.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Result.ObjectEmbbeded.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Result.ObjectEmbbeded.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Result.ObjectEmbbeded.ModificationDate)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _record = Nothing
        Presenter = Nothing
        _settingMedicalFees = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlySettings, True)
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PMedicalFeesSettings(Me)
        LoadStatus()
        LoadControls()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento cerrar del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesSettings_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Cuando se activa el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesSettings_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If AccountPayableConceptId Is Nothing Then
            INDsleAccountPayableConcept.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayableConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountPayableConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmConceptsAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeAccountPayableConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario correspondiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleJournalVoucherType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleJournalVoucherType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmDocumentType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeJournalVoucherType()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayableConcept_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleAccountPayableConcept.QueryPopUp
        If AccountPayableConceptXpo Is Nothing Then
            Presenter.InitializeAccountPayableConcept()
        End If
    End Sub

    ''' <summary>
    ''' Se encarga de setear el Datasource de todos los contoles dispuestos, con los tipos de comprobantes contables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ControlsByJournalVoucherType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleJournalVoucherType.QueryPopUp, INDSleCostRecognitionReversalVoucher.QueryPopUp,
                                                                                                                        INDSleCostRecognitionVoucher.QueryPopUp

        ChargeDatasourceJournalVoucherTypes(sender)
    End Sub

    ''' <summary>
    ''' Se encarga de cargar los tipos de comprobantes contables
    ''' </summary>
    ''' <param name="_Control"></param>
    Public Sub ChargeDatasourceJournalVoucherTypes(_Control As Object)
        If _Control.Properties.DataSource Is Nothing Then
            _Control.Properties.DataSource = Presenter.InitializeJournalVoucherType()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        'Dim state As Boolean
        'Select Case Status
        '    Case CBool(eActionsStatusRecords.Active)
        '        state = True
        '    Case CBool(eActionsStatusRecords.Inactive)
        '        state = False
        'End Select
        'Using model As New MParameters(Me.Tag.ToString())
        '    AsyncLoader(True)
        '    Dim Result = Await model.ChangeState(_settingPayments.IdOperatingUnit, state)
        '    AsyncLoader(False)
        '    If Result.StateResult = True Then
        '        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '    Else
        '        If Result.MessageResult(0) = ErrorConcurrencia Then
        '            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '        Else
        '            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '        End If
        '    End If
        'End Using
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        AccountPayableConceptId = Nothing
        JournalVoucherTypeId = Nothing
        CostRecognitionReversalVoucherId = Nothing
        CostRecognitionVoucherId = Nothing

        INDsleAccountPayableConcept.Properties.NullText = String.Empty
        INDsleJournalVoucherType.Properties.NullText = String.Empty
        INDSleCostRecognitionVoucher.Properties.NullText = String.Empty
        INDSleCostRecognitionReversalVoucher.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await (Model.DeleteBlockRecord(_record))
                _record = Nothing
            End Using
        End If
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
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()
        Using Model As New MMedicalFeesSettings(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resulOperation = Await (Model.GetMedicalFeesSettings())
            AsyncLoader(False)
            _settingMedicalFees = resulOperation.ObjectEmbbeded
            If _settingMedicalFees IsNot Nothing AndAlso _settingMedicalFees.Id > 0 Then
                Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_settingMedicalFees.Id))
                    With _settingMedicalFees
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

                        Me.BarraBotones.CleanAuditBasic()
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        AccountPayableConceptId = .AccountPayableConceptId
                        INDsleAccountPayableConcept.Properties.NullText = .AccountPayableConceptDescription
                        JournalVoucherTypeId = .JournalVoucherTypeId
                        INDsleJournalVoucherType.Properties.NullText = .JournalVoucherTypeDescription

                        CostRecognitionVoucherId = .CostRecognitionVoucherId
                        INDSleCostRecognitionVoucher.Properties.NullText = .CostRecognitionVoucherDescription

                        CostRecognitionReversalVoucherId = .CostRecognitionReversalVoucherId
                        INDSleCostRecognitionReversalVoucher.Properties.NullText = .CostRecognitionReversalVoucherDescription

                    End With
                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._settingMedicalFees.Id)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordMedicalFees With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = _settingMedicalFees.Id}
                        Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                        _record = result
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                    Me.BarraBotones.SetDocuments(_settingMedicalFees.Id)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                End Using
            Else
                _settingMedicalFees = New SettingMedicalFees
                BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
            End If
        End Using
        INDsleAccountPayableConcept.Focus()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _settingMedicalFees
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .AccountPayableConceptId = AccountPayableConceptId
            .JournalVoucherTypeId = JournalVoucherTypeId
            .CostRecognitionVoucherId = CostRecognitionVoucherId
            .CostRecognitionReversalVoucherId = CostRecognitionReversalVoucherId
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Barra Botones: Activa o desactiva el estado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barra botones: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Actualizar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

#End Region
    
End Class