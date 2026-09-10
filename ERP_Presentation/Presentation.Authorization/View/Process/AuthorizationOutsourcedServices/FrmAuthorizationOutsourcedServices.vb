'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/07/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Controls.MVP
Imports DevExpress.Utils.Menu
Imports DevExpress.XtraTreeList
Imports System.Windows.Forms
Imports Presentation.Common.MVP
Imports Presentation.Billing
Imports Presentation.Authorization.MVP
Imports Presentation.Common
#End Region

Public Class FrmAuthorizationOutsourcedServices
    Implements IAuthorizationOutsourcedServices

#Region "Properties"

    ''' <summary>
    ''' Habilita o inhabilita los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAuthorizationOutsourcedServices.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDsleQuotationType.Enabled = value
            INDsleAdmission.Enabled = value
            INDslePatient.Enabled = value
            INDmemoDescription.Enabled = value
            INDbtnAddServices.Enabled = value
            INDgcServices.Enabled = value
            INDbtnAddProducts.Enabled = value
            INDgcProducts.Enabled = value
            INDlyRoot.EndUpdate()

            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IAuthorizationOutsourcedServices.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene la secuencia del formulario
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As AuthorizationSequence Implements IAuthorizationOutsourcedServices.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As AuthorizationSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As AuthorizationSequenceDetail In Me._sequense.AuthorizationSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Código del registro
    ''' </summary>
    ''' <returns></returns>
    Private Property Code As String Implements IAuthorizationOutsourcedServices.Code
        Get
            If INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha del documento
    ''' </summary>
    ''' <returns></returns>
    Private Property DocumentDate As Date? Implements IAuthorizationOutsourcedServices.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de cotización
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As Integer Implements IAuthorizationOutsourcedServices.Type
        Get
            Return INDsleQuotationType.EditValue
        End Get
        Set(value As Integer)
            INDsleQuotationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero que representa al paciente
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientThirdPartyId As Integer? Implements IAuthorizationOutsourcedServices.PatientThirdPartyId
        Get
            Return INDslePatient.EditValue
        End Get
        Set(value As Integer?)
            INDslePatient.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción del paciente que viene desde la dashboard de autorizaciones
    ''' </summary>
    Public WriteOnly Property PatientThirdPartyCodeName As String
        Set(value As String)
            INDslePatient.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property PatientThirdPartyXpo As XPInstantFeedbackSource Implements IAuthorizationOutsourcedServices.PatientThirdPartyXpo
        Get
            Return INDslePatient.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePatient.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Detalle
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String Implements IAuthorizationOutsourcedServices.Description
        Get
            Return INDmemoDescription.EditValue
        End Get
        Set(value As String)
            INDmemoDescription.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As AuthorizationSequence

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PAuthorizationOutsourcedServices

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Entidad
    ''' </summary>
    Private AuthorizationOutsourcedServices As AuthorizationOutsourcedServices

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordAuthorization

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Authorization"

    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object

    ''' <summary>
    ''' numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal
    ''' </summary>
    Private AdmissionNumber As String

    ''' <summary>
    ''' representa el genero del paciente
    ''' </summary>
    ''' <remarks></remarks>
    Private patientGenus As String

    ''' <summary>
    ''' representa la fecha de naciemiento
    ''' </summary>
    ''' <remarks></remarks>
    Private patientDateBirth As String

    ''' <summary>
    ''' listado del detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Public ListServiceOrderDetail As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' listado de eliminados del detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteServiceOrderDetail As List(Of ServiceOrderDetail)

    ''' <summary>
    ''' Listado de detalle de la dispensación farmaceutica
    ''' </summary>
    Dim ListPharmaceuticalDispensingDetail As List(Of PharmaceuticalDispensingDetail)

    ''' <summary>
    ''' Listado de eliminados de detalle de la dispensación farmaceutica
    ''' </summary>
    Dim ListDeletePharmaceuticalDispensingDetail As List(Of PharmaceuticalDispensingDetail)

    ''' <summary>
    ''' entodad que representa el detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private serviceOrderDetail As ServiceOrderDetail

    ''' <summary>
    ''' Entidad que representa al detalle de la dispensación farmaceutica
    ''' </summary>
    Private pharmaceuticalDispensingDetail As PharmaceuticalDispensingDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Permite saber si se esta cargando los controles desde el loadControls
    ''' </summary>
    Dim IsLoad As Boolean = False

    ''' <summary>
    ''' Permite saber si el form se esta abriendo desde el formulario de dasboard
    ''' </summary>
    Public IsDashboard As Boolean = False

    ''' <summary>
    ''' Listado de ids de los trámites seleccionados
    ''' </summary>
    Public ListTraceabilityPaperworkIds As List(Of Integer)

    ''' <summary>
    ''' id del grupo de atención que seleccionaron en el form remitir
    ''' </summary>
    Public CareGroupId As Integer

    ''' <summary>
    ''' Descripción del grupo de atención que seleccionaron en el form remitir
    ''' </summary>
    Public CareGroupCodeName As String

#End Region

#Region "ICrudBase"

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

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        If AuthorizationOutsourcedServices.Status <> 3 Then
            If (ListServiceOrderDetail Is Nothing OrElse ListServiceOrderDetail.Count = 0) AndAlso (ListPharmaceuticalDispensingDetail Is Nothing OrElse ListPharmaceuticalDispensingDetail.Count = 0) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos un item en servicios o productos"
                Exit Sub
            End If
        End If

        Try
            AssigningValues()

            Using model As New MAuthorizationOutsourcedServices(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveAuthorizationOutsourcedServices(AuthorizationOutsourcedServices, _idCurrentSequense)
                If Result.StateResult = True Then
                    If AuthorizationOutsourcedServices.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me.Sequense.IsManual AndAlso Not Me.Sequense.Sequential Then
                            Me.DicSequense(Me.Sequense.AuthorizationSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me.Sequense.Sequential Then
                            If AuthorizationOutsourcedServices.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf AuthorizationOutsourcedServices.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If AuthorizationOutsourcedServices.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf AuthorizationOutsourcedServices.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = Result.Message
                        ElseIf AuthorizationOutsourcedServices.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = Result.Message
                        End If
                    End If

                    If IsDashboard Then
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                        Me.Close()
                    End If

                    Me.AuthorizationOutsourcedServices = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewQuotation()
        End If
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "TypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Grupo Atención", .FieldName = "AdmissionInformation.CareGroupCodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Paciente", .FieldName = "AdmissionInformation.PatientCodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAuthorizationOutsourcedServices
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que abre el form de importar la información
    ''' </summary>
    Private Sub OpenImportInfo()
        Dim errors = ControlsValidate()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmImportInfoAOS()
            AddHandler Formulario.ImportInfoEvent, AddressOf ImportInfo
            Formulario.ToolBar.Dock = DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 920
            Formulario.Height = 600
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que importa la información
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ImportInfo(e As AddInfoImportAOS)
        If e IsNot Nothing AndAlso e.AuthorizationOutsourcedServicesId > 0 Then
            INDviewServices.ShowLoadingPanel()
            INDviewProducts.ShowLoadingPanel()
            Task.Factory.StartNew(Sub() ListDetails(e.AuthorizationOutsourcedServicesId, True))
        End If
    End Sub

    ''' <summary>
    ''' Método que elimina un detalle de la rejilla de productos
    ''' </summary>
    Private Sub DeletePharmaceuticalDispensingDetail()
        Dim entityDelete = DirectCast(INDviewProducts.GetFocusedRow(), PharmaceuticalDispensingDetail)
        ListPharmaceuticalDispensingDetail.Remove(entityDelete)
        INDgcProducts.DataSource = Nothing
        INDgcProducts.DataSource = ListPharmaceuticalDispensingDetail
        Mensaje(EeventViewerImages.Informacion) = "Item eliminado de la rejilla correctamente"

        If entityDelete.Id > 0 Then
            If ListDeletePharmaceuticalDispensingDetail Is Nothing Then
                ListDeletePharmaceuticalDispensingDetail = New List(Of PharmaceuticalDispensingDetail)
            End If
            entityDelete.MarkAsDeleted()
            ListDeletePharmaceuticalDispensingDetail.Add(entityDelete)
        End If
    End Sub

    ''' <summary>
    ''' Método que elimina un detalle de la rejilla de servicios
    ''' </summary>
    Private Sub DeleteServiceOrderDetail()
        Dim entityDelete = DirectCast(INDviewServices.GetFocusedRow(), ServiceOrderDetail)
        ListServiceOrderDetail.Remove(entityDelete)
        INDgcServices.DataSource = Nothing
        INDgcServices.DataSource = ListServiceOrderDetail
        Mensaje(EeventViewerImages.Informacion) = "Item eliminado de la rejilla correctamente"

        If entityDelete.Id > 0 Then
            If ListDeleteServiceOrderDetail Is Nothing Then
                ListDeleteServiceOrderDetail = New List(Of ServiceOrderDetail)
            End If
            entityDelete.MarkAsDeleted()
            ListDeleteServiceOrderDetail.Add(entityDelete)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        IndigoGridView1.MoreInfoColunmns(INDviewServices)
        IndigoGridView2.MoreInfoColunmns(INDviewProducts)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)

        IndigoGridView1.SetListAcction(INDviewServices, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewServices.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        IndigoGridView2.SetListAcction(INDviewProducts, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' Valida los controles necesarios para abrir los formularios modales
    ''' </summary>
    ''' <returns></returns>
    Private Function ControlsValidate() As String
        Dim errors As New StringBuilder

        If DocumentDate Is Nothing Then
            errors.AppendLine("Debe seleccionar una fecha")
        End If
        If Type = Nothing Then
            errors.AppendLine("Debe seleccionar un tipo de cotización")
        Else
            If Type = 1 AndAlso INDsleAdmission.EditValue Is Nothing Then
                errors.AppendLine("Debe seleccionar un ingreso")
            ElseIf Type = 2 AndAlso PatientThirdPartyId Is Nothing Then
                errors.AppendLine("Debe seleccionar un paciente")
            End If
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Método que abre el formulario de detalle de la orden de servicio
    ''' </summary>
    Private Sub OpenFormServiceOrderDetail(editMode As Boolean)
        Dim errors = ControlsValidate()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Using popupService As New FrmPopupServices(True)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler popupService.AddServiceOrderDetail, AddressOf ReturAddServiceOrderDetail

            If editMode Then
                popupService.AllowEditItem = True
                popupService.ServiceOrderDetailEdit = serviceOrderDetail
            End If

            popupService.EditMode = editMode
            popupService.RequestQuoteServices = True
            popupService.ServiceOrderId = 0
            popupService.ServiceOrderStatus = "1"
            popupService.Admission = If(Type = 1, AdmissionNumber, "")
            popupService.Patient = If(Type = 1, INDTxtPatient.Text, INDslePatient.Text)
            popupService.Stay = If(Type = 1, INDTxtStay.Text, "")
            popupService.PatientDateBirth = If(Type = 1, patientDateBirth, GetDateServer().ToString())
            popupService.PatientGenus = If(Type = 1, patientGenus, Nothing)
            popupService.AdmissionDate = DocumentDate
            popupService.AdmissionDateMinValue = INDdteDocumentDate.Properties.MinValue
            popupService.AutorizationNumber = If(Type = 1, IIf(String.IsNullOrEmpty(INDTxtAuthorizationNumber.Text), "", INDTxtAuthorizationNumber.Text), "")
            popupService.ServiceDate = DocumentDate

            If editMode = False Then
                If ListServiceOrderDetail IsNot Nothing Then
                    popupService.ListCompare = ListServiceOrderDetail.Select(Function(x) x.CloneEntity()).Cast(Of ServiceOrderDetail).ToList()
                End If
            Else
                popupService.ListCompare = ListServiceOrderDetail.Where(Function(item) Not item.Equals(serviceOrderDetail)).ToList()
            End If

            If editMode = False AndAlso Type = 1 AndAlso admission IsNot Nothing AndAlso admission.CareGroupId IsNot Nothing AndAlso CType(admission.CareGroupId, Integer) > 0 AndAlso IsDashboard = False Then
                popupService.CareGroupAdmission = admission.CareGroupId.ToString()
            ElseIf IsDashboard Then
                popupService.CareGroupAdmission = CareGroupId
            End If

            If editMode = False Then
                popupService.HealthAdministratorCrystal = If(Type = 1, admission.HealthAdministratorId, 0)
            End If

            If ListServiceOrderDetail IsNot Nothing Then
                popupService.ListServiceOrderDetailSurgicalIntervention = ListServiceOrderDetail
                popupService.ListServiceOrderDetailDatasourceIncludeService = ListServiceOrderDetail
            Else
                popupService.ListServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)
                popupService.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
            End If

            popupService.Size = New Drawing.Size(800, 700)
            popupService.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(popupService, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me.MdiParent)
        End Using
    End Sub

    ''' <summary>
    ''' Abre el formulario detalle para agregar o editar
    ''' </summary>
    Private Sub OpenFormPharmaceuticalDetail(editMode As Boolean)
        Dim errors = ControlsValidate()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Exit Sub
        End If

        Using Formulario As New FrmBillingPharmaceuticalDispensingDetail
            Me.Cursor = ChangeCursorIndigo()
            AddHandler Formulario.AddPharmaceuticalDispensingDetail, AddressOf ReturnAddPharmaceuticalDispensingDetail

            Formulario.AffectedInventory = 0
            Formulario.AdmissionNumberHeader = If(Type = 1, AdmissionNumber, "")
            Formulario.AdmissionDate = DocumentDate
            Formulario.ServiceDate = DocumentDate
            Formulario.ThirdPartyPatientId = If(Type = 1, 0, PatientThirdPartyId)
            Formulario.FullNameThirdPartyPatient = If(Type = 1, INDTxtPatient.Text, INDslePatient.Text)
            Formulario.PatientCode = If(Type = 1, admission.PatientCode.ToString().Trim(), INDslePatient.Text.Split(" - ")(0).ToString().Trim())

            If editMode Then
                Formulario.PharmaceuticalDispensingDetail = CType(INDviewProducts.GetFocusedRow(), PharmaceuticalDispensingDetail)
                Formulario.EditMode = True
                Formulario.LoadControls()
            Else
                Formulario.CleanControls(True)
                Formulario.PharmaceuticalDispensingDetail = New PharmaceuticalDispensingDetail()
                Formulario.HealthAdministratorCrystal = If(Type = 1, admission.HealthAdministratorId, 0)
                Formulario.CareGroupId = If(Type = 1, admission.CareGroupId, 0)
                Formulario.CareGroupCodeName = ""
                Formulario.AutorizationNumber = If(Type = 1, IIf(String.IsNullOrEmpty(INDTxtAuthorizationNumber.Text), "", INDTxtAuthorizationNumber.Text), "")
            End If

            Formulario.StatusInventory = 0

            Formulario.ViewModeEditHold = True
            Formulario.MinimizeBox = False
            Formulario.MaximizeBox = False
            Formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Formulario.Size = New Drawing.Size(800, 700)
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me.MdiParent)
        End Using
    End Sub

    ''' <summary>
    ''' metodo que se encarga de mostrar en la rejilla los registros agregados desde el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddPharmaceuticalDispensingDetail(sender As Object, e As AddBillingPharmaceuticalDispensingDetailEventArgs)
        If e IsNot Nothing Then
            If e.EditMode = False Then
                If ListPharmaceuticalDispensingDetail Is Nothing Then
                    ListPharmaceuticalDispensingDetail = New List(Of PharmaceuticalDispensingDetail)
                End If
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else
                ListPharmaceuticalDispensingDetail.Remove(pharmaceuticalDispensingDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
                e.PharmaceuticalDispensingDetail.Id = pharmaceuticalDispensingDetail.Id
                e.PharmaceuticalDispensingDetail.AuthorizationOutsourcedServicesId = pharmaceuticalDispensingDetail.AuthorizationOutsourcedServicesId
            End If
            ListPharmaceuticalDispensingDetail.Add(e.PharmaceuticalDispensingDetail)

            INDgcProducts.DataSource = Nothing
            INDgcProducts.DataSource = ListPharmaceuticalDispensingDetail
        End If
    End Sub

    ''' <summary>
    ''' metodo que se encarga de mostrar en la rejilla los registros agregados desde el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturAddServiceOrderDetail(sender As Object, e As AddServiceEventArgs)
        If e IsNot Nothing Then
            If e.EditMode = False Then
                If ListServiceOrderDetail Is Nothing Then
                    ListServiceOrderDetail = New List(Of ServiceOrderDetail)
                End If
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else
                ListServiceOrderDetail.Remove(serviceOrderDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
                e.ListServiceOrderDetail(0).Id = serviceOrderDetail.Id
                e.ListServiceOrderDetail(0).AuthorizationOutsourcedServicesId = serviceOrderDetail.AuthorizationOutsourcedServicesId
            End If
            ListServiceOrderDetail.AddRange(e.ListServiceOrderDetail)

            INDgcServices.DataSource = Nothing
            INDgcServices.DataSource = ListServiceOrderDetail
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al presionar enter al control de admisiones
    ''' </summary>
    ''' <param name="code"></param>
    ''' <remarks></remarks>
    Private Function INDsleAdmission_KeyDown(code As String) As Object
        Using model As New MServiceOrder(Me.MyTag)
            Dim admissionTempKeyDown = model.GetAdmissionByServiceOrderCollection(code)
            If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown.Count > 0 Then
                If admissionTempKeyDown(0).Status = "F" Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ingreso seleccionado ya se encuentra facturado"
                    admission = Nothing
                    INDsleAdmission.EditValue = Nothing
                    INDsleAdmission.DisplayNullText = String.Empty
                    CleanControlsAdminssion()
                    INDbtnAddServices.Enabled = False
                    INDsleAdmission.Focus()
                Else
                    SetAdmission(admissionTempKeyDown(0))
                    INDbtnAddServices.Enabled = True
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
                admission = Nothing
                INDsleAdmission.EditValue = Nothing
                INDsleAdmission.DisplayNullText = String.Empty
                CleanControlsAdminssion()
                INDbtnAddServices.Enabled = False
                INDsleAdmission.Focus()
            End If
        End Using
    End Function

    ''' <summary>
    ''' metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <param name="record"></param>
    ''' <remarks></remarks>
    Private Sub SetAdmission(record As Object)
        If record IsNot Nothing Then
            admission = record
            'INDTxtAdmissionPopup.Text = "Tipo de Ingreso : " & ResourceManager.GetString(String.Concat("AdmissionType", record.AdmissionType.ToString().Trim()))
            'Mensaje(EeventViewerImages.Informacion) = INDTxtAdmissionPopup.Text
            AdmissionNumber = admission.AdmissionCode.ToString().Trim()
            INDsleAdmission.DisplayNullText = String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), If(admission.PatientCode Is Nothing, "", admission.PatientCode.ToString().Trim()), If(admission.PatientName Is Nothing, "", admission.PatientName.ToString().Trim()))
            If record.AdmissionType IsNot Nothing Then
                record.AdmissionTypeName = ResourceManager.GetString(String.Concat("AdmissionType", record.AdmissionType.ToString().Trim()))
            End If
            If record.AdmissionType.ToString().Trim() <> ResourceManager.GetString("OutpatientRevenue", MODULE_NAME) Then
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            If record.LiquidationType IsNot Nothing Then
                record.LiquidationTypeName = ResourceManager.GetString(String.Concat("LiquidationType", record.LiquidationType.ToString().Trim()))
            End If
            If record.PatientCode IsNot Nothing And record.PatientName IsNot Nothing Then
                record.PatientCodeName = record.PatientCode.ToString().Trim() + " - " + record.PatientName.ToString().Trim()
            End If
            If record.PlaceEntry IsNot Nothing Then
                record.AdmissionPlace = ResourceManager.GetString(String.Concat("PlaceEntry", record.PlaceEntry.ToString().Trim()))
            End If
            patientDateBirth = record.PatientDateBirth
            patientGenus = record.PatientGenus
            INDsleAdmission.SetMoreInfoData(record)
        End If
    End Sub

    ''' <summary>
    ''' metodo para limpioar los controles del popup de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdminssion()
        INDTxtAdmissionCode.Text = String.Empty
        INDTxtStay.Text = String.Empty
        INDTxtPatient.Text = String.Empty
        INDTxtAdmissionDate.Text = String.Empty
        INDTxtAdmissionType.Text = String.Empty
        INDTxtAdmissionPlace.Text = String.Empty
        INDTxtLiquidationType.Text = String.Empty
        INDTxtEntity.Text = String.Empty
        INDTxtBenefitsPlan.Text = String.Empty
        INDTxtAuthorizationNumber.Text = String.Empty
        INDTxtResponsibleName.Text = String.Empty
        INDTxtResponsiblePhone.Text = String.Empty
        admission = Nothing
    End Sub

    ''' <summary>
    ''' Inicializa los search que son con datos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listQuotationType As New List(Of Tuple(Of Integer, String))
        listQuotationType.Add(New Tuple(Of Integer, String)(1, "Intrahospitalario"))
        listQuotationType.Add(New Tuple(Of Integer, String)(2, "Ambulatoria"))
        INDsleQuotationType.Properties.DataSource = listQuotationType.ToList()
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.AuthorizationOutsourcedServices.Code, Me.AuthorizationOutsourcedServices.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.AuthorizationOutsourcedServices.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.AuthorizationOutsourcedServices.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.AuthorizationOutsourcedServices.Code, Me.AuthorizationOutsourcedServices.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.AuthorizationOutsourcedServices.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        Using Model As New Presentation.Authorization.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssigningValues()
        With AuthorizationOutsourcedServices
            .Code = Code
            .DocumentDate = DocumentDate
            .Type = Type

            If INDlyItemAdmission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AdmissionNumber = AdmissionNumber
            Else
                .AdmissionNumber = Nothing
            End If

            If INDlyItemPatient.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ThirdPartyId = PatientThirdPartyId
            Else
                .ThirdPartyId = Nothing
            End If

            .Description = Description
            .OperatingUnitId = BarraBotones.OperatingUnitValue

            .ListServiceOrderDetail = ListServiceOrderDetail
            .ListPharmaceuticalDispensingDetail = ListPharmaceuticalDispensingDetail

            If ListDeleteServiceOrderDetail IsNot Nothing AndAlso ListDeleteServiceOrderDetail.Count > 0 Then
                If .ListServiceOrderDetail Is Nothing Then
                    .ListServiceOrderDetail = ListDeleteServiceOrderDetail
                Else
                    .ListServiceOrderDetail.AddRange(ListDeleteServiceOrderDetail)
                End If
            End If

            If ListDeletePharmaceuticalDispensingDetail IsNot Nothing AndAlso ListDeletePharmaceuticalDispensingDetail.Count > 0 Then
                If .ListPharmaceuticalDispensingDetail Is Nothing Then
                    .ListPharmaceuticalDispensingDetail = ListDeletePharmaceuticalDispensingDetail
                Else
                    .ListPharmaceuticalDispensingDetail.AddRange(ListDeletePharmaceuticalDispensingDetail)
                End If
            End If

            .ListTraceabilityPaperworkIds = ListTraceabilityPaperworkIds

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        ReadOnlyControls(False)
        CleanControlsAdminssion()
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Code = Nothing
        DocumentDate = Nothing
        Type = Nothing
        PatientThirdPartyId = Nothing
        INDslePatient.Properties.NullText = String.Empty
        INDsleAdmission.EditValue = Nothing
        INDsleAdmission.DisplayNullText = String.Empty
        Description = Nothing
        serviceOrderDetail = Nothing
        ListServiceOrderDetail = Nothing
        pharmaceuticalDispensingDetail = Nothing
        ListPharmaceuticalDispensingDetail = Nothing
        INDgcServices.DataSource = Nothing
        INDgcProducts.DataSource = Nothing
        ListDeleteServiceOrderDetail = Nothing
        ListDeletePharmaceuticalDispensingDetail = Nothing
        admission = Nothing
        AdmissionNumber = Nothing
        AuthorizationOutsourcedServices = Nothing
        INDlyItemAdmission.HideControl()
        INDlyItemPatient.HideControl()
        INDlyRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' metodo para cargar controles al formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            Try
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If
                Using model As New MAuthorizationOutsourcedServices(MyTag)
                    AsyncLoader(True)
                    INDlyRoot.BeginUpdate()

                    Me.AuthorizationOutsourcedServices = (Await model.GetAuthorizationOutsourcedServices(Code)).ObjectEmbbeded
                    If Me.AuthorizationOutsourcedServices IsNot Nothing AndAlso Me.AuthorizationOutsourcedServices.Id > 0 Then
                        Using ModelRecord As New Presentation.Authorization.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Me.AuthorizationOutsourcedServices.Id))

                            With Me.AuthorizationOutsourcedServices
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
                                If .Status = 1 Then
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                                ElseIf .Status = 2 Then
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
                                    ReadOnlyControls(True)
                                Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                    ReadOnlyControls(True)
                                End If

                                DocumentDate = .DocumentDate
                                Type = .Type
                                Description = .Description
                                PatientThirdPartyId = .ThirdPartyId
                                INDslePatient.Properties.NullText = .ThirdPartyDescription

                                AdmissionNumber = .AdmissionNumber
                                IsLoad = True
                                INDsleAdmission.EditValue = .AdmissionNumber
                                IsLoad = False

                                If .AdmissionNumber IsNot Nothing Then
                                    Dim admissionTmp = model.GetAdmissionByServiceOrder(.AdmissionNumber.Trim())
                                    If admissionTmp IsNot Nothing Then
                                        SetAdmission(admissionTmp)
                                    End If
                                End If

                                INDviewServices.ShowLoadingPanel()
                                INDviewProducts.ShowLoadingPanel()
                                Await Task.Factory.StartNew(Sub() ListDetails(.Id, False))
                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.AuthorizationOutsourcedServices.Code)

                            If Me.record.Id = 0 Then
                                Me.record = (Await ModelRecord.SaveBlockRecord(
                                        New BlockRecordAuthorization With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                            .CodUser = Me.indigo.UserIndigo, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .IdRecord = Me.AuthorizationOutsourcedServices.Id})
                                        ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), Me.record.CodUser, Me.record.NameUser, Me.record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, Me.record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(Me.AuthorizationOutsourcedServices.Id, Me.Tag.ToString(), Nothing, GetType(BasicBilling).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, AuthorizationOutsourcedServices.Id, 0, AuthorizationOutsourcedServices.Id)

                            AsyncLoader(False)
                        End Using
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me.Sequense.IsManual Then
                            Await Me.NewQuotation()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If

                    INDlyRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Método que carga los detalles de la rejilla de servicios y de productos
    ''' </summary>
    Private Sub ListDetails(AuthorizationOutsourcedServicesId As Integer, IsImportInfo As Boolean)
        CheckForIllegalCrossThreadCalls = False

        If ListServiceOrderDetail Is Nothing Then
            ListServiceOrderDetail = New List(Of ServiceOrderDetail)
        End If

        If ListPharmaceuticalDispensingDetail Is Nothing Then
            ListPharmaceuticalDispensingDetail = New List(Of PharmaceuticalDispensingDetail)
        End If

        Dim listQuotationServiceOrderDetailXpo = Presenter.ListServiceOrderDetails(AuthorizationOutsourcedServicesId)
        If listQuotationServiceOrderDetailXpo IsNot Nothing AndAlso listQuotationServiceOrderDetailXpo.Count > 0 Then
            For Each quotationServiceOrderDetailXpo In listQuotationServiceOrderDetailXpo
                serviceOrderDetail = New ServiceOrderDetail
                With serviceOrderDetail
                    If IsImportInfo = False Then
                        .IdTmp = quotationServiceOrderDetailXpo.Id
                        .Id = quotationServiceOrderDetailXpo.Id
                        .ServiceOrderId = quotationServiceOrderDetailXpo.AuthorizationOutsourcedServicesId.Id
                        .AuthorizationOutsourcedServicesId = quotationServiceOrderDetailXpo.AuthorizationOutsourcedServicesId.Id
                    End If
                    .CareGroupId = quotationServiceOrderDetailXpo.CareGroupId.Id
                    .CodeNameCareGroup = quotationServiceOrderDetailXpo.CareGroupId.CodeName
                    If quotationServiceOrderDetailXpo.HealthAdministratorId IsNot Nothing Then
                        .HealthAdministratorId = quotationServiceOrderDetailXpo.HealthAdministratorId.Id
                        .CodeNameHealthAdministrator = quotationServiceOrderDetailXpo.HealthAdministratorId.Code + " - " + quotationServiceOrderDetailXpo.HealthAdministratorId.Name
                    End If
                    If quotationServiceOrderDetailXpo.ThirdPartyId IsNot Nothing Then
                        .ThirdPartyId = quotationServiceOrderDetailXpo.ThirdPartyId.Id
                        .NitNameThirdParty = quotationServiceOrderDetailXpo.ThirdPartyId.NitName
                    End If
                    .ServiceType = quotationServiceOrderDetailXpo.ServiceType
                    .RecordType = quotationServiceOrderDetailXpo.RecordType
                    If quotationServiceOrderDetailXpo.CUPSEntityId IsNot Nothing Then
                        .CUPSEntityId = quotationServiceOrderDetailXpo.CUPSEntityId.Id
                        .CodeNameCups = quotationServiceOrderDetailXpo.CUPSEntityId.CodeDescription
                    End If
                    If quotationServiceOrderDetailXpo.IPSServiceId IsNot Nothing Then
                        .IPSServiceId = quotationServiceOrderDetailXpo.IPSServiceId.Id
                        .CodeNameIpsService = quotationServiceOrderDetailXpo.IPSServiceId.CodeName
                    End If
                    If quotationServiceOrderDetailXpo.HospitalStayId > 0 Then
                        .HospitalStayId = quotationServiceOrderDetailXpo.HospitalStayId
                    End If
                    If quotationServiceOrderDetailXpo.HospitalStayDetailId > 0 Then
                        .HospitalStayDetailId = quotationServiceOrderDetailXpo.HospitalStayDetailId
                    End If
                    If quotationServiceOrderDetailXpo.ControlExternalConsultation > 0 Then
                        .ControlExternalConsultation = quotationServiceOrderDetailXpo.ControlExternalConsultation
                    End If
                    If quotationServiceOrderDetailXpo.ControlExternalConsultationCode > 0 Then
                        .ControlExternalConsultationCode = quotationServiceOrderDetailXpo.ControlExternalConsultationCode
                    End If
                    .CUPSAssociateService = quotationServiceOrderDetailXpo.CUPSAssociateService
                    If quotationServiceOrderDetailXpo.CodeAssociateService IsNot Nothing Then
                        .CodeAssociateService = quotationServiceOrderDetailXpo.CodeAssociateService
                    End If
                    .IsPackage = quotationServiceOrderDetailXpo.IsPackage
                    .Packaging = quotationServiceOrderDetailXpo.Packaging
                    If quotationServiceOrderDetailXpo.PackageServiceOrderDetailId <> Nothing Then
                        .PackageServiceOrderDetailId = quotationServiceOrderDetailXpo.PackageServiceOrderDetailId
                    End If
                    .LiquidationType = quotationServiceOrderDetailXpo.LiquidationType
                    .Presentation = quotationServiceOrderDetailXpo.Presentation
                    If quotationServiceOrderDetailXpo.ProductId IsNot Nothing Then
                        .ProductId = quotationServiceOrderDetailXpo.ProductId.Id
                        .CodeNameProduct = quotationServiceOrderDetailXpo.ProductId.CodeName
                    End If
                    .InvoicedQuantity = quotationServiceOrderDetailXpo.InvoicedQuantity
                    .SupplyQuantity = quotationServiceOrderDetailXpo.SupplyQuantity
                    .DevolutionQuantity = quotationServiceOrderDetailXpo.DevolutionQuantity
                    .RateManualSalePrice = quotationServiceOrderDetailXpo.RateManualSalePrice
                    .CostValue = quotationServiceOrderDetailXpo.CostValue
                    .ServiceDate = quotationServiceOrderDetailXpo.ServiceDate
                    If quotationServiceOrderDetailXpo.AuthorizationNumber IsNot Nothing Then
                        .AuthorizationNumber = quotationServiceOrderDetailXpo.AuthorizationNumber
                    End If
                    If quotationServiceOrderDetailXpo.PerformsFunctionalUnitId IsNot Nothing Then
                        .PerformsFunctionalUnitId = quotationServiceOrderDetailXpo.PerformsFunctionalUnitId.Id
                        .CodeNameFunctionalUnit = quotationServiceOrderDetailXpo.PerformsFunctionalUnitId.Code + " - " + quotationServiceOrderDetailXpo.PerformsFunctionalUnitId.Name
                    End If
                    If quotationServiceOrderDetailXpo.PerformsHealthProfessionalCode IsNot Nothing Then
                        .PerformsHealthProfessionalCode = quotationServiceOrderDetailXpo.PerformsHealthProfessionalCode
                    End If
                    If quotationServiceOrderDetailXpo.PerformsProfessionalSpecialty IsNot Nothing Then
                        .PerformsProfessionalSpecialty = quotationServiceOrderDetailXpo.PerformsProfessionalSpecialty
                    End If
                    If quotationServiceOrderDetailXpo.PerformsHealthProfessionalThirdPartyId <> Nothing Then
                        .PerformsHealthProfessionalThirdPartyId = quotationServiceOrderDetailXpo.PerformsHealthProfessionalThirdPartyId
                    End If
                    If quotationServiceOrderDetailXpo.BillingConceptId IsNot Nothing Then
                        .BillingConceptId = quotationServiceOrderDetailXpo.BillingConceptId.Id
                    End If

                    .CostCenterId = quotationServiceOrderDetailXpo.CostCenterId.Id
                    .CodeNameCostCenter = quotationServiceOrderDetailXpo.CostCenterId.Code + " - " + quotationServiceOrderDetailXpo.CostCenterId.Name
                    .SettlementType = quotationServiceOrderDetailXpo.SettlementType
                    If quotationServiceOrderDetailXpo.IncludeServiceOrderDetailId <> Nothing Then
                        .IncludeServiceOrderDetailId = quotationServiceOrderDetailXpo.IncludeServiceOrderDetailId
                    End If
                    .RecoveryRatio = quotationServiceOrderDetailXpo.RecoveryRatio

                    If quotationServiceOrderDetailXpo.RateManualId > 0 Then
                        .RateManualId = quotationServiceOrderDetailXpo.RateManualId
                    End If
                    If quotationServiceOrderDetailXpo.RateManualType > 0 Then
                        .RateManualType = quotationServiceOrderDetailXpo.RateManualType
                    End If
                    If quotationServiceOrderDetailXpo.RateManualDetailId <> Nothing AndAlso quotationServiceOrderDetailXpo.RateManualDetailId > 0 Then
                        .RateManualDetailId = quotationServiceOrderDetailXpo.RateManualDetailId
                    End If
                    If quotationServiceOrderDetailXpo.DefinitionRateDetailId > 0 Then
                        .DefinitionRateDetailId = quotationServiceOrderDetailXpo.DefinitionRateDetailId
                    End If
                    If quotationServiceOrderDetailXpo.DefinitionRateDetailConditionId > 0 Then
                        .DefinitionRateDetailConditionId = quotationServiceOrderDetailXpo.DefinitionRateDetailConditionId
                    End If
                    .SubTotalSalesPrice = quotationServiceOrderDetailXpo.SubTotalSalesPrice
                    .ThirdPartyDiscount = quotationServiceOrderDetailXpo.ThirdPartyDiscount
                    .ThirdPartyDiscountPercentage = quotationServiceOrderDetailXpo.ThirdPartyDiscountPercentage
                    .TotalSalesPrice = quotationServiceOrderDetailXpo.TotalSalesPrice
                    .GrandTotalSalesPrice = quotationServiceOrderDetailXpo.GrandTotalSalesPrice
                    .SurchargeApply = quotationServiceOrderDetailXpo.SurchargeApply
                    If quotationServiceOrderDetailXpo.SurgicalInterventionType > 0 Then
                        .SurgicalInterventionType = quotationServiceOrderDetailXpo.SurgicalInterventionType
                    End If
                    .SurgeryNumber = quotationServiceOrderDetailXpo.SurgeryNumber
                    .IsFirstEvent = quotationServiceOrderDetailXpo.IsFirstEvent
                    .IsAnnulled = quotationServiceOrderDetailXpo.IsAnnulled
                    .IsDelete = quotationServiceOrderDetailXpo.IsDelete
                    .IncomeMainAccountId = quotationServiceOrderDetailXpo.IncomeMainAccountId

                    .ApplyRIAS = Nothing
                    .RIASCupsId = Nothing
                    If quotationServiceOrderDetailXpo.ApplyRIAS IsNot Nothing Then
                        .ApplyRIAS = quotationServiceOrderDetailXpo.ApplyRIAS
                        If quotationServiceOrderDetailXpo.RIASCupsId IsNot Nothing AndAlso quotationServiceOrderDetailXpo.RIASCupsId > 0 Then
                            .RIASCupsId = quotationServiceOrderDetailXpo.RIASCupsId
                        End If
                    End If

                    .CUPSEntityContractDescriptionId = Nothing
                    If quotationServiceOrderDetailXpo.CUPSEntityContractDescriptionId IsNot Nothing Then
                        .CUPSEntityContractDescriptionId = quotationServiceOrderDetailXpo.CUPSEntityContractDescriptionId.Id
                        .ContractDescriptionCodeName = quotationServiceOrderDetailXpo.CUPSEntityContractDescriptionId.ContractDescriptionId.CodeName
                    End If
                End With
                For Each quotationServiceOrderDetailSurgicalXpo In quotationServiceOrderDetailXpo.AuthorizationOutsourcedServicesServiceOrderDetailSurgicalXpo
                    Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                    With serviceOrderDetailSurgical
                        If IsImportInfo = False Then
                            .Id = quotationServiceOrderDetailSurgicalXpo.Id
                            .ServiceOrderDetailId = quotationServiceOrderDetailSurgicalXpo.AuthorizationOutsourcedServicesServiceOrderDetailId.Id
                            .QuotationServiceOrderDetailId = quotationServiceOrderDetailSurgicalXpo.AuthorizationOutsourcedServicesServiceOrderDetailId.Id
                        End If
                        .IPSServiceId = quotationServiceOrderDetailSurgicalXpo.IPSServiceId.Id
                        .CodeNameIpsService = quotationServiceOrderDetailSurgicalXpo.IPSServiceId.CodeName
                        Select Case quotationServiceOrderDetailSurgicalXpo.IPSServiceId.ServiceClass
                            Case 1
                                .ClassServiceIps = "Ninguno"
                            Case 2
                                .ClassServiceIps = "Cirujano"
                            Case 3
                                .ClassServiceIps = "Anestesiólogo"
                            Case 4
                                .ClassServiceIps = "Ayudante"
                            Case 5
                                .ClassServiceIps = "Derecho Sala"
                            Case 6
                                .ClassServiceIps = "Materiales Sutura"
                            Case 7
                                .ClassServiceIps = "Instrumentación Quirúrgica"
                        End Select
                        .InvoicedQuantity = quotationServiceOrderDetailSurgicalXpo.InvoicedQuantity
                        .LiquidationPercentage = quotationServiceOrderDetailSurgicalXpo.LiquidationPercentage
                        .RateManualSalePrice = quotationServiceOrderDetailSurgicalXpo.RateManualSalePrice
                        .TotalSalesPrice = quotationServiceOrderDetailSurgicalXpo.TotalSalesPrice
                        If quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalCode IsNot Nothing Then
                            .PerformsHealthProfessionalCode = quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalCode
                        End If
                        If quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalThirdPartyId <> Nothing Then
                            .PerformsHealthProfessionalThirdPartyId = quotationServiceOrderDetailSurgicalXpo.PerformsHealthProfessionalThirdPartyId
                        End If
                        .CostValue = quotationServiceOrderDetailSurgicalXpo.CostValue
                        .BillingConceptId = quotationServiceOrderDetailSurgicalXpo.BillingConceptId.Id
                        .CostCenterId = quotationServiceOrderDetailSurgicalXpo.CostCenterId
                        If quotationServiceOrderDetailSurgicalXpo.RateManualDetailSurgicalId <> Nothing AndAlso quotationServiceOrderDetailSurgicalXpo.RateManualDetailSurgicalId > 0 Then
                            .RateManualDetailSurgicalId = quotationServiceOrderDetailSurgicalXpo.RateManualDetailSurgicalId
                        End If
                        .SurchargeApply = quotationServiceOrderDetailSurgicalXpo.SurchargeApply
                        .OnlyMedicalFees = quotationServiceOrderDetailSurgicalXpo.OnlyMedicalFees
                        .IncomeMainAccountId = quotationServiceOrderDetailSurgicalXpo.IncomeMainAccountId
                    End With
                    serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
                Next

                ListServiceOrderDetail.Add(serviceOrderDetail)
            Next
        End If

        INDviewServices.HideLoadingPanel()
        INDgcServices.DataSource = Nothing
        INDgcServices.DataSource = ListServiceOrderDetail
        INDgcServices.RefreshDataSource()

        Dim listQuotationPharmaceuticalDispensingDetailXpo = Presenter.ListPharmaceuticalDispensingDetails(AuthorizationOutsourcedServicesId)
        If listQuotationPharmaceuticalDispensingDetailXpo IsNot Nothing AndAlso listQuotationPharmaceuticalDispensingDetailXpo.Count > 0 Then
            For Each quotationPharmaceuticalDispensingDetailXpo In listQuotationPharmaceuticalDispensingDetailXpo
                pharmaceuticalDispensingDetail = New PharmaceuticalDispensingDetail
                With pharmaceuticalDispensingDetail
                    If IsImportInfo = False Then
                        .Id = quotationPharmaceuticalDispensingDetailXpo.Id
                        .AuthorizationOutsourcedServicesId = quotationPharmaceuticalDispensingDetailXpo.AuthorizationOutsourcedServicesId.Id
                    End If
                    .CareGroupId = quotationPharmaceuticalDispensingDetailXpo.CareGroupId.Id
                    .ProductId = quotationPharmaceuticalDispensingDetailXpo.ProductId.Id
                    .WarehouseId = quotationPharmaceuticalDispensingDetailXpo.WarehouseId.Id
                    .HealthAdministratorId = quotationPharmaceuticalDispensingDetailXpo.HealthAdministratorId.Id
                    If quotationPharmaceuticalDispensingDetailXpo.ThirdPartyId IsNot Nothing Then
                        .ThirdPartyId = quotationPharmaceuticalDispensingDetailXpo.ThirdPartyId.Id
                    End If
                    .Quantity = quotationPharmaceuticalDispensingDetailXpo.Quantity
                    .ReturnedQuantity = quotationPharmaceuticalDispensingDetailXpo.ReturnedQuantity
                    .ServiceDate = quotationPharmaceuticalDispensingDetailXpo.ServiceDate
                    .FunctionalUnitId = quotationPharmaceuticalDispensingDetailXpo.FunctionalUnitId.Id
                    .OrderedHealthProfessionalCode = quotationPharmaceuticalDispensingDetailXpo.OrderedHealthProfessionalCode
                    .OrderedProfessionalSpecialty = quotationPharmaceuticalDispensingDetailXpo.OrderedProfessionalSpecialty
                    If quotationPharmaceuticalDispensingDetailXpo.OrderedHealthProfessionalThirdPartyId <> Nothing AndAlso quotationPharmaceuticalDispensingDetailXpo.OrderedHealthProfessionalThirdPartyId > 0 Then
                        .OrderedHealthProfessionalThirdPartyId = quotationPharmaceuticalDispensingDetailXpo.OrderedHealthProfessionalThirdPartyId
                    End If
                    .AuthorizationNumber = quotationPharmaceuticalDispensingDetailXpo.AuthorizationNumber
                    .LiquidationType = quotationPharmaceuticalDispensingDetailXpo.LiquidationType
                    If quotationPharmaceuticalDispensingDetailXpo.CUPSEntityId IsNot Nothing Then
                        .CupsEntityId = quotationPharmaceuticalDispensingDetailXpo.CUPSEntityId.Id
                        .CodeNameCups = quotationPharmaceuticalDispensingDetailXpo.CUPSEntityId.Code + " - " + quotationPharmaceuticalDispensingDetailXpo.CUPSEntityId.Description
                    End If
                    .SurchargeApply = quotationPharmaceuticalDispensingDetailXpo.SurchargeApply
                    .SalePrice = quotationPharmaceuticalDispensingDetailXpo.SalePrice
                    .AverageCost = quotationPharmaceuticalDispensingDetailXpo.AverageCost
                    .TotalSalesPrice = quotationPharmaceuticalDispensingDetailXpo.TotalSalesPrice
                    .GrandTotalSalesPrice = quotationPharmaceuticalDispensingDetailXpo.GrandTotalSalesPrice
                    .DiscountPercentage = quotationPharmaceuticalDispensingDetailXpo.DiscountPercentage
                    .DiscountValue = quotationPharmaceuticalDispensingDetailXpo.DiscountValue
                    .CodeProduct = quotationPharmaceuticalDispensingDetailXpo.ProductId.Code
                    .NameProduct = quotationPharmaceuticalDispensingDetailXpo.ProductId.Code + " - " + quotationPharmaceuticalDispensingDetailXpo.ProductId.Name
                    .CodeNameCareGroup = quotationPharmaceuticalDispensingDetailXpo.CareGroupId.Code + " - " + quotationPharmaceuticalDispensingDetailXpo.CareGroupId.Name
                    .CodeNameWareHouse = quotationPharmaceuticalDispensingDetailXpo.WarehouseId.Code + " - " + quotationPharmaceuticalDispensingDetailXpo.WarehouseId.Name
                    .CodeNameHealthProfessional = quotationPharmaceuticalDispensingDetailXpo.OrderedHealthProfessionalCode + " - " + quotationPharmaceuticalDispensingDetailXpo.ThirdPartyId.Name
                    .CodeNameHealthProfessionalSpeciality = quotationPharmaceuticalDispensingDetailXpo.OrderedProfessionalSpecialty
                    .FullNameFunctionalUnit = quotationPharmaceuticalDispensingDetailXpo.FunctionalUnitId.Code + " - " + quotationPharmaceuticalDispensingDetailXpo.FunctionalUnitId.Name
                    .Custody = 0
                End With

                ListPharmaceuticalDispensingDetail.Add(pharmaceuticalDispensingDetail)
            Next
        End If

        INDviewProducts.HideLoadingPanel()
        INDgcProducts.DataSource = Nothing
        INDgcProducts.DataSource = ListPharmaceuticalDispensingDetail
        INDgcProducts.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewQuotation() As Task
        If Me._sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No existe secuencia numérica para el formulario"
            INDbtnCode.Focus()
            Exit Function
        End If
        AuthorizationOutsourcedServices = New AuthorizationOutsourcedServices
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.AuthorizationSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.AuthorizationSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.AuthorizationSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                    Exit Function
                End If
            End If
            If Not Me._sequense.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                    Else
                        Using model As New Presentation.Billing.MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
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
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            End If
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmQuotation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAuthorizationOutsourcedServices(Me)
        Presenter.GetSequense()
        LoadStatus()

        If IsDashboard = False Then 'Si no se abre desde la dashboard de autorizaciones
            Deshacer()
        Else 'Si se abre desde la dashboard de autorizaciones
            ActionsOnControls = False

            INDdteDocumentDate.EditValue = GetDateServer()
            INDsleQuotationType.EditValue = 2

            INDgcServices.DataSource = Nothing
            INDgcServices.DataSource = ListServiceOrderDetail
            INDgcServices.RefreshDataSource()
        End If
        InitializeTuples()
        AddActionsColumns()

        Task.Factory.StartNew(Sub()
                                  Using model As New MServiceOrder(MyTag.ToString())
                                      Dim ds = model.GetViewAdmissionOpenAndPartial()
                                      INDsleAdmission.SafeInvoke(Sub()
                                                                     INDsleAdmission.Datasource = ds
                                                                 End Sub)
                                  End Using
                              End Sub)
        INDsleAdmission.FuncQueryOnKeyEnterPressed = AddressOf INDsleAdmission_KeyDown
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePatient_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePatient.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.ListPatientThirdParty()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePatient_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePatient.QueryPopUp
        If PatientThirdPartyXpo Is Nothing Then
            Presenter.ListPatientThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de qx de la rejilla de servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRptPceSurgicalDetail_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRptPceSurgicalDetail.QueryPopUp
        Dim serviceOrderDatailTmp = DirectCast(INDviewServices.GetFocusedRow, ServiceOrderDetail)
        INDGcSurgicalDetail.DataSource = Nothing
        INDGcSurgicalDetail.DataSource = serviceOrderDatailTmp.ServiceOrderDetailSurgical

        INDTxtEventNumber.EditValue = serviceOrderDatailTmp.SurgeryNumber
        Dim listEventsTmp = ListServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = serviceOrderDatailTmp.SurgeryNumber)
        INDGcEvents.DataSource = Nothing
        INDGcEvents.DataSource = listEventsTmp
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de cotización
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleQuotationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleQuotationType.EditValueChanged
        If Type <> Nothing Then
            If Type = 1 Then 'Intrahospitalaria
                INDlyItemAdmission.HideControl(False)
                INDlyItemPatient.HideControl()
            Else 'Ambulatoria
                INDlyItemAdmission.HideControl()
                INDlyItemPatient.HideControl(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de admisiones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAdmission_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDsleAdmission.EditValueChanged
        If INDsleAdmission.EditValue IsNot Nothing AndAlso IsLoad = False Then
            'valido que el ingreso no este facturado
            Dim admissionTmp As Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionOpenAndPartial
            Using model As New MAdmissions(MyTag)
                admissionTmp = CType(e.NewObject, Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionOpenAndPartial)
                If admissionTmp.Status = "F" Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ingreso esta facturado"
                    INDsleAdmission.EditValue = Nothing
                    INDsleAdmission.DisplayNullText = String.Empty
                    admission = Nothing
                    CleanControlsAdminssion()
                    INDbtnAddServices.Enabled = False
                    Exit Sub
                End If
            End Using

            INDbtnAddServices.Enabled = True

            Dim objTemp = Me.viewSearchAdmission.GetFocusedRow
            If objTemp IsNot Nothing Then
                Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                    SetAdmission(obj.OriginalRow)
                End If
            End If
            'ctrTmp.PrintInfo()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape en el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmQuotation_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If IsDashboard AndAlso e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter sobre el control de código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No ha terminado de cargar la secuencia numérica"
                Exit Sub
            End If

            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await NewQuotation()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Public Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.AuthorizationOutsourcedServices IsNot Nothing AndAlso Me.AuthorizationOutsourcedServices.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmQuotation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmQuotation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddServices_Click(sender As Object, e As EventArgs) Handles INDbtnAddServices.Click
        OpenFormServiceOrderDetail(False)
    End Sub

    ''' <summary>
    ''' Evento que se dispar al presionar click sobre el botón de agregar productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDbtnAddProducts.Click
        OpenFormPharmaceuticalDetail(False)
    End Sub

#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Menu rejilla servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Select Case sender.Tag
            Case "Edit"
                serviceOrderDetail = DirectCast(INDviewServices.GetFocusedRow(), ServiceOrderDetail)
                OpenFormServiceOrderDetail(True)
            Case "Remove"
                DeleteServiceOrderDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Menu rejilla servicios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                serviceOrderDetail = DirectCast(INDviewServices.GetFocusedRow(), ServiceOrderDetail)
                OpenFormServiceOrderDetail(True)
            Case "Remove"
                DeleteServiceOrderDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Menu rejilla productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Select Case sender.Tag
            Case "Edit"
                pharmaceuticalDispensingDetail = DirectCast(INDviewProducts.GetFocusedRow(), PharmaceuticalDispensingDetail)
                OpenFormPharmaceuticalDetail(True)
            Case "Remove"
                DeletePharmaceuticalDispensingDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Menu rejilla productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                pharmaceuticalDispensingDetail = DirectCast(INDviewProducts.GetFocusedRow(), PharmaceuticalDispensingDetail)
                OpenFormPharmaceuticalDetail(True)
            Case "Remove"
                DeletePharmaceuticalDispensingDetail()
        End Select
    End Sub

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        AuthorizationOutsourcedServices.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        AuthorizationOutsourcedServices.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        AuthorizationOutsourcedServices.Status = 2
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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        AuthorizationOutsourcedServices.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.AuthorizationSequenceDetail IsNot Nothing Then
            If Me._sequense.AuthorizationSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.AuthorizationSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            AuthorizationOutsourcedServices.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barra Botones: ImportarInformación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        OpenImportInfo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el botón desconfirmar de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        If MessageIndigo.Show("Esta seguro que desea desconfimar el registro?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            AuthorizationOutsourcedServices.Status = 0
            Guardar()
        End If
    End Sub

#End Region

End Class
