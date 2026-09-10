'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 28-10-2014
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
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Common.MVP
Imports Presentation.Billing

#End Region

Public Class FrmOrdersService
    Implements IServiceOrder

#Region "BUILDER"
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrServiceOrderInfo()
        ctrTmp.SetInfoFunction(AddressOf getInfoServiceOrder)
        ctrTmp.PrintInfo()
        ctrTmp.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        INDGvEvents.OptionsView.ShowAutoFilterRow = False
    End Sub
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' Evento que se dispara para avisar que ya se terminó de cargar los controles
    ''' </summary>
    Public Event LoadEnded(sender As Object, e As EventArgs)
    ''' <summary>
    ''' Control para establecer informacion del ingreso
    ''' </summary>
    Private ctrTmp As CtrServiceOrderInfo
    ''' <summary>
    ''' almacena el valor del servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private _serviceValue As Decimal = 0
    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Billing"
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BillingSequence
    ''' <summary>
    ''' presenter de ordenes de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PServiceOrder
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable que representa la entidad de parámetros de contratos
    ''' </summary>
    Dim _settingsContractXpo As Infrastructure.Data.Xpo.ContractRepository.SettingsContractXpo
    ''' <summary>
    ''' representa la entidad de ordenes de servicios
    ''' </summary>
    ''' <remarks></remarks>
    Dim serviceOrder As ServiceOrder
    ''' <summary>
    ''' listado del detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Dim listServiceOrderDetail As List(Of ServiceOrderDetail)
    ''' <summary>
    ''' listado del detalle de la orden de servicio para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listServiceOrderDetailDelete As List(Of ServiceOrderDetail)
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordBilling
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
    ''' entodad que representa el detalle de la orden de servicio
    ''' </summary>
    ''' <remarks></remarks>
    Private serviceOrderDetail As ServiceOrderDetail
    ''' <summary>
    ''' guarda la posicion del item que se esta editando para que luego el item nuevo se agregue en el lugar que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Private indexEditItem As Integer
    ''' <summary>
    ''' variable para saber cual es el numero de la fila que se esta editando
    ''' </summary>
    ''' <remarks></remarks>
    Private rowEditing As Integer

    '''formulario modal para agregar servicios
    Private popupService As FrmPopupServices

    ''' <summary>
    ''' bandera para saber que el formulario esta en solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Private _readOnlyControlsForm As Boolean
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' guarda si el sistema es impuesto incluido o no
    ''' </summary>
    Private _flagTaxInclude As Boolean

    ''' <summary>
    ''' sumatoria del IVA
    ''' </summary>
    Private _sumTaxValues As Decimal = 0
#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As Domain.Entities.BillingSequence Implements IServiceOrder.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As Domain.Entities.BillingSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As BillingSequenceDetail In Me._sequense.BillingSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

    ''' <summary>
    ''' propiedad que establece la habilitacion de controles 
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IServiceOrder.ActionsOnControls
        Set(value As Boolean)
            INDLcServiceOrders.BeginUpdate()
            INDBteCode.Enabled = Not value
            If serviceOrder IsNot Nothing AndAlso serviceOrder.Id = 0 Then
                INDSleAdmissionNumber2.IsReadOnly = False
                INDDteOrderDate.Enabled = True
            Else
                INDDteOrderDate.Enabled = False
                INDSleAdmissionNumber2.IsReadOnly = True
            End If
            If serviceOrder Is Nothing Then
                INDBtnAddService.Enabled = False
            ElseIf serviceOrder.Id = 0 Then
                INDBtnAddService.Enabled = False
            End If
            INDGcService.Enabled = value
            INDLcServiceOrders.EndUpdate()
            If value Then
                Me.BeginInvoke(Sub() INDSleAdmissionNumber2.Focus())
            Else
                Me.BeginInvoke(Sub() INDBteCode.Focus())
            End If
        End Set
    End Property

    ''' <summary>
    ''' numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal
    ''' </summary>
    Private _admissionNumber As String
    Public ReadOnly Property AdmissionNumber As String Implements IServiceOrder.AdmissionNumber
        Get
            Return _admissionNumber
        End Get
    End Property

    ''' <summary>
    ''' Codigo de la orden de servicio
    ''' </summary>
    Public Property Code As String Implements IServiceOrder.Code
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

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IServiceOrder.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IServiceOrder.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Fecha de la orden de servicio
    ''' </summary>
    Public Property OrderDate As Date? Implements IServiceOrder.OrderDate
        Get
            Return CDate(INDDteOrderDate.EditValue)
        End Get
        Set(value As Date?)
            INDDteOrderDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que indica que el formulario se manda a cargar desde un padre
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [load in parent form]; otherwise, <c>false</c>.
    ''' </value>
    Public Property LoadInParentForm As Boolean = False

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteOutpatientServices As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteOutpatientServices)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios intrahospitalarios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteIntrahospitalServices As Boolean
        Get
            Return If(_settingsContractXpo Is Nothing, False, _settingsContractXpo.RequestQuoteIntrahospitalServices)
        End Get
    End Property

    ''' <summary>
    ''' Variable de parametrizacion que indica si se debe validar la cotización de servicios
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RequestQuoteServices As Boolean
        Get
            Dim result As Boolean
            If Me.admission IsNot Nothing Then
                If Me.admission.AdmissionType = 1 Then
                    result = Me.RequestQuoteOutpatientServices
                Else
                    result = Me.RequestQuoteIntrahospitalServices
                End If
            End If
            Return result
        End Get
    End Property

    ''' <summary>
    ''' propiedad que establece si el sistema es o no impuestos incluidos
    ''' </summary>
    Public WriteOnly Property TaxInclude As Boolean Implements IServiceOrder.FlagTaxInclude
        Set(value As Boolean)
            Me._flagTaxInclude = value
        End Set
    End Property
#End Region

#Region "CRUD"
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        If _searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            End If
        End If
    End Sub

    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Valida si la orden de servicio pertenece a Registro Soporte RIPS y no permite modificaciones
    ''' </summary>
    ''' <returns>True si la orden pertenece a RIPSSupportRecord y no se puede modificar</returns>
    Private Function ValidateRIPSSupportRecord() As Boolean
        If serviceOrder IsNot Nothing AndAlso Not String.IsNullOrEmpty(serviceOrder.EntityName) AndAlso serviceOrder.EntityName.Equals("RIPSSupportRecord", StringComparison.OrdinalIgnoreCase) Then
            Mensaje(EeventViewerImages.Advertencia) = "Esta orden de servicio pertenece a un Registro Soporte RIPS. Si desea realizar modificaciones, guardar o anular, debe realizarlas desde el formulario de Registro Soporte RIPS."
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' accion boton barra botones guardar
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        'Validar si la orden pertenece a RIPSSupportRecord
        If ValidateRIPSSupportRecord() Then
            Exit Sub
        End If

        If serviceOrder IsNot Nothing AndAlso serviceOrder.Status < 3 Then
            Dim errors = ValidateControls()
            If ValidateControls() = True Then
                If admission Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ingreso Vacio"
                    Exit Sub
                End If
                If INDGvService.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddItem", MODULE_NAME)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If
        AssigningValues()
        Try
            Using model As New MServiceOrder(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveServiceOrder(serviceOrder, _idCurrentSequense)
                AsyncLoader(False)
                If result.StateResult = True Then
                    If serviceOrder.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.BillingSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        If serviceOrder.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    If Not result.Message.Equals(String.Empty) Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, serviceOrder.Id, 0, serviceOrder.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, serviceOrder.Id, 0, serviceOrder.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, serviceOrder.Id, 0, serviceOrder.Id)
                    End Select

                    _searchMode = False
                    Me.Deshacer()
                Else
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        If serviceOrder.Status = 3 Then
                            Dim errors As New StringBuilder
                            errors.AppendLine("No se pudo anular la orden de servicio por:")
                            errors.AppendLine(result.Message)
                            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                        End If
                    End If
                    If serviceOrder.Id > 0 Then
                        serviceOrder = model.GetServiceOrderById(serviceOrder.Id)
                    Else
                        serviceOrder = New ServiceOrder
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Accion boton barra botones para crear un nuevo registro
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense Is Nothing OrElse Me._sequense.IsManual Then
            Deshacer()
        Else
            NewServiceOrder()
        End If
    End Sub
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Método que abre el form de importar la información
    ''' </summary>
    Private Sub OpenFormImport()
        If admission Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso"
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmImportQuotation()
            AddHandler Formulario.ImportEvent, AddressOf ImportInfo
            Formulario.PatientCode = admission.PatientCode.ToString().Trim()
            Formulario.ToolBar.Dock = System.Windows.Forms.DockStyle.None
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1054
            Formulario.Height = 500
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Métod que se ejecuta al cerrar el form de importar
    ''' </summary>
    ''' <param name="e"></param>
    Private Sub ImportInfo(e As AddImportQuotationServiceOrderDetail)
        If e IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo IsNot Nothing AndAlso e.ListQuotationServiceOrderDetailXpo.Count > 0 Then
            INDGvService.ShowLoadingPanel()
            Task.Factory.StartNew(Sub() ListDetailsImport(e.ListQuotationServiceOrderDetailXpo))
        End If
    End Sub

    ''' <summary>
    ''' Método que me importa la información de la cotización
    ''' </summary>
    ''' <param name="listXpo"></param>
    Private Sub ListDetailsImport(listXpo As List(Of QuotationServiceOrderDetailXpo))
        If listServiceOrderDetail Is Nothing Then
            listServiceOrderDetail = New List(Of ServiceOrderDetail)
        End If

        Dim itemsRepeats As New StringBuilder

        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each quotationServiceOrderDetailXpo In listXpo

                If (From x In listServiceOrderDetail Where x.QuotationServiceOrderDetailId = quotationServiceOrderDetailXpo.Id Select x).Count > 0 Then
                    itemsRepeats.AppendLine("El servicio " + quotationServiceOrderDetailXpo.IPSServiceId.CodeName + " no se puede agregar porque ya existe en la lista")
                    Continue For
                End If

                serviceOrderDetail = New ServiceOrderDetail
                With serviceOrderDetail
                    .ServiceOrderId = serviceOrder.Id
                    .QuotationServiceOrderDetailId = quotationServiceOrderDetailXpo.Id
                    .QuotationCode = quotationServiceOrderDetailXpo.QuotationId.Code
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
                    If quotationServiceOrderDetailXpo.RateManualDetailId IsNot Nothing Then
                        .RateManualDetailId = quotationServiceOrderDetailXpo.RateManualDetailId.Id
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
                For Each quotationServiceOrderDetailSurgicalXpo In quotationServiceOrderDetailXpo.QuotationServiceOrderDetailSurgicalXpo
                    Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                    With serviceOrderDetailSurgical
                        .ServiceOrderDetailId = 0
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
                        If quotationServiceOrderDetailSurgicalXpo.RateManualDetailSurgicalId IsNot Nothing Then
                            .RateManualDetailSurgicalId = quotationServiceOrderDetailSurgicalXpo.RateManualDetailSurgicalId.Id
                        End If
                        .SurchargeApply = quotationServiceOrderDetailSurgicalXpo.SurchargeApply
                        .OnlyMedicalFees = quotationServiceOrderDetailSurgicalXpo.OnlyMedicalFees
                        .IncomeMainAccountId = quotationServiceOrderDetailSurgicalXpo.IncomeMainAccountId
                    End With
                    serviceOrderDetail.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
                Next

                listServiceOrderDetail.Add(serviceOrderDetail)
            Next
        End If

        INDGcService.SafeInvoke(Sub()
                                    INDGvService.HideLoadingPanel()
                                    INDGcService.DataSource = Nothing
                                    INDGcService.DataSource = listServiceOrderDetail
                                    INDGcService.RefreshDataSource()

                                    _serviceValue = listServiceOrderDetail.Sum(Function(x) x.GrandTotalSalesPrice)
                                    _sumTaxValues = listServiceOrderDetail.Sum(Function(x) x.TaxValue * x.InvoicedQuantity)
                                    ctrTmp.PrintInfo()

                                    If Not String.IsNullOrEmpty(itemsRepeats.ToString()) Then
                                        Mensaje(EeventViewerImages.Advertencia) = itemsRepeats.ToString()
                                    End If
                                End Sub)
    End Sub

    ''' <summary>
    ''' Se ejecuta al presionar enter al control de admisiones
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function INDSleAdmissionNumber2_KeyDown(code As String) As Object
        Using model As New MServiceOrder(Me.MyTag)
            Dim admissionTempKeyDown = model.GetAdmissionByServiceOrderCollection(code)
            If admissionTempKeyDown IsNot Nothing AndAlso admissionTempKeyDown.Count > 0 Then
                If admissionTempKeyDown(0).Status = "F" Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ingreso seleccionado ya se encuentra facturado"
                    admission = Nothing
                    INDSleAdmissionNumber2.EditValue = Nothing
                    INDSleAdmissionNumber2.DisplayNullText = String.Empty
                    CleanControlsAdminssion()
                    INDBtnAddService.Enabled = False
                    INDSleAdmissionNumber2.Focus()
                Else
                    SetAdmission(admissionTempKeyDown(0))
                    INDBtnAddService.Enabled = True
                    If INDDteOrderDate.Enabled = False Then
                        INDBtnAddService.Focus()
                    Else
                        INDDteOrderDate.Focus()
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "El número del ingreso no existe"
                admission = Nothing
                INDSleAdmissionNumber2.EditValue = Nothing
                INDSleAdmissionNumber2.DisplayNullText = String.Empty
                CleanControlsAdminssion()
                INDBtnAddService.Enabled = False
                INDSleAdmissionNumber2.Focus()
            End If
        End Using
    End Function

    ''' <summary>
    ''' retorna la informacion que se establece en el control de la barra
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfoServiceOrder() As Tuple(Of String, String, Decimal)
        Return New Tuple(Of String, String, Decimal)(TxtPatientCode.Text + " - " + TxtPatientName.Text, Format(_serviceValue, "c2"), Me._sumTaxValues)
    End Function

    ''' <summary>
    ''' metodo que asigna los valore a la entidad para mandar a guardar
    ''' </summary>
    Private Sub AssigningValues()
        With serviceOrder
            .Code = Code
            .AdmissionNumber = admission.AdmissionCode.ToString().Trim()
            .PatientCode = admission.PatientCode.ToString().Trim()
            .OrderDate = OrderDate
            .OperatingUnitId = BarraBotones.OperatingUnitValue

            If .Status <> 3 Then
                .Status = 1
            End If

            For Each item In listServiceOrderDetail
                If .Status = 3 Then
                    If item.Id > 0 Then
                        .ServiceOrderDetail.Add(item)
                    End If
                Else
                    .ServiceOrderDetail.Add(item)
                End If
            Next

            If listServiceOrderDetailDelete IsNot Nothing Then
                For Each item In listServiceOrderDetailDelete
                    .ServiceOrderDetail.Add(item.MarkAsDeleted())
                Next
            End If

            While serviceOrder.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ServiceOrderDetail") AndAlso serviceOrder.ChangeTracker.ObjectsRemovedFromCollectionProperties("ServiceOrderDetail").Count > 0
                .ServiceOrderDetail.Add(serviceOrder.ChangeTracker.ObjectsRemovedFromCollectionProperties("ServiceOrderDetail")(0))
            End While
        End With
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvService, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvService.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' metodo para limpioar los controles del popup de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsAdminssion()
        Me.TxtAdmissionCode.Text = String.Empty
        Me.TxtFunctionalUnitAdmission.Text = String.Empty

        Me.TxtAdmissionDate.Text = String.Empty
        Me.TxtBedStay.Text = String.Empty

        Me.TxtAdmissionType.Text = String.Empty
        Me.TxtPlaceEntry.Text = String.Empty

        Me.TxtLiquidationType.Text = String.Empty
        Me.TxtAuthorization.Text = String.Empty

        Me.TxtAtentionCenter.Text = String.Empty
        Me.TxtEntityNameAdmission.Text = String.Empty

        Me.TxtResponsiblePhone.Text = String.Empty
        Me.TxtResponsibleName.Text = String.Empty

        Me.TxtPatientCode.Text = String.Empty
        Me.TxtPatientName.Text = String.Empty
        Me.TxtPatientBirth.Text = String.Empty
        Me.TxtPatientAge.Text = String.Empty
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtPatientEntityName.Text = String.Empty
        Me.TxtPatientEstrato.Text = String.Empty

        Me.TxtPatientType.EditValue = Nothing
        Me.TxtAfiliationType.EditValue = Nothing
        Me.TxtCareGroupPatient.Text = String.Empty
        Me.TxtCareGroupAdmission.Text = String.Empty
        Me.TxtRiskType.Text = String.Empty
        Me.TxtContact.Text = String.Empty

        admission = Nothing
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDLcgMainData.BeginUpdate()

        ReadOnlyControls(False)
        _readOnlyControlsForm = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        BarraBotones.CleanAuditBasic()
        BarraBotones.ReassignOperatingUnit()
        INDBteCode.Text = String.Empty
        INDSleAdmissionNumber2.DisplayNullText = String.Empty
        INDSleAdmissionNumber2.EditValue = Nothing
        INDSleAdmissionNumber2.Enabled = True
        INDDteOrderDate.EditValue = GetDateServer()
        INDDteOrderDate.Properties.MaxValue = INDDteOrderDate.EditValue
        CleanControlsAdminssion()
        INDGcService.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcService)
        INDColService.Visible = True
        INDColService.FieldName = "CodeNameIpsService"
        listServiceOrderDetail = Nothing
        listServiceOrderDetailDelete = Nothing
        serviceOrder = Nothing
        ActionsOnControls = False
        _serviceValue = 0
        _sumTaxValues = 0
        ctrTmp.PrintInfo()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        INDLcgMainData.EndUpdate()
    End Sub

    ''' <summary>
    ''' metodo para abrir el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        _searchMode = True
        DeleteBlockedRecord()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim listItemsColumnEdit As New List(Of Tuple(Of String, String))
        listItemsColumnEdit.Add(New Tuple(Of String, String)(ResourceManager.GetString("AccountControl").Split("-").ElementAt(0).Trim(), "AccountControl"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)(ResourceManager.GetString("PharmaceuticalDispensing").Split("-").ElementAt(0).Trim(), "PharmaceuticalDispensing"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Control Servicios Ambulatorios", "ControlOutPatientServices"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Liquidación de Estancias", "LiquidateStays"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Ninguno", "ServiceOrder"))
        listItemsColumnEdit.Add(New Tuple(Of String, String)("Registro soporte RIPS", "RIPSSupportRecord"))

        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Facturado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Admisión", .FieldName = "AdmissionNumber", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Paciente", .FieldName = "PatientCode", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha", .FieldName = "OrderDate", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Origen", .FieldName = "EntityName", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEdit},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = 200, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllServiceOrder
            .FiltroBusqueda = BarraBotones.OperatingUnitValue.ToString()
            .FormParent = Me
            .ShowSearch()
        End With
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
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateInvoiced"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub NewServiceOrder()
        Me.serviceOrder = New ServiceOrder
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            ShowHidePermissionImportButton()
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.BillingSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.BillingSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
                    Exit Sub
                End If
            End If
            If Not Me._sequense.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        ShowHidePermissionImportButton()
                    Else
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            ShowHidePermissionImportButton()
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
                    ShowHidePermissionImportButton()
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                ShowHidePermissionImportButton()
            End If
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.serviceOrder.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordBilling With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me.serviceOrder.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content = String.Format(ResourceManager.GetString("FrmOrdersService_IndexContent", MODULE_NAME), serviceOrder.Code, admission.AdmissionCode.ToString().Trim(), admission.PatientCode.ToString().Trim() + " - " + admission.PatientName.ToString().Trim())
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.serviceOrder.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.serviceOrder.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.serviceOrder.Code)
            Return Me._doc
        End If
    End Function

    Private Function LoadServiceOrderDetail() As Task
        Return Task.Factory.
            StartNew(Sub()
                         Dim details As XPCollection(Of ServiceOrderDetailXpo)
                         Dim objLock As New Object()
                         Dim objLockSurgical As New Object()
                         Using model As New MServiceOrder(MyTag)
                             details = model.ListServiceOrderDetailByServiceOrderId(serviceOrder.Id)
                             Parallel.ForEach(details,
                                              Sub(detail)
                                                  If listServiceOrderDetail Is Nothing Then
                                                      listServiceOrderDetail = New List(Of ServiceOrderDetail)
                                                  End If
                                                  Dim serviceOrderDetailTmp As New ServiceOrderDetail
                                                  With serviceOrderDetailTmp
                                                      .IdTmp = detail.Id
                                                      .Id = detail.Id
                                                      .ServiceOrderId = detail.ServiceOrderId.Id
                                                      If detail.QuotationServiceOrderDetailId IsNot Nothing Then
                                                          .QuotationServiceOrderDetailId = detail.QuotationServiceOrderDetailId.Id
                                                          .QuotationCode = detail.QuotationServiceOrderDetailId.QuotationId.Code
                                                      End If
                                                      .CareGroupId = detail.CareGroupId.Id
                                                      .CodeNameCareGroup = detail.CareGroupId.CodeName
                                                      If detail.HealthAdministratorId IsNot Nothing Then
                                                          .HealthAdministratorId = detail.HealthAdministratorId.Id
                                                          .CodeNameHealthAdministrator = detail.HealthAdministratorId.Code + " - " + detail.HealthAdministratorId.Name
                                                      End If
                                                      If detail.ThirdPartyId IsNot Nothing Then
                                                          .ThirdPartyId = detail.ThirdPartyId.Id
                                                          .NitNameThirdParty = detail.ThirdPartyId.NitName
                                                      End If
                                                      .ServiceType = detail.ServiceType
                                                      .RecordType = detail.RecordType
                                                      If detail.CUPSEntityId IsNot Nothing Then
                                                          .CUPSEntityId = detail.CUPSEntityId.Id
                                                          .CodeNameCups = detail.CUPSEntityId.CodeDescription
                                                      End If
                                                      If detail.IPSServiceId IsNot Nothing Then
                                                          .IPSServiceId = detail.IPSServiceId.Id
                                                          .CodeNameIpsService = detail.IPSServiceId.CodeName
                                                      End If
                                                      If detail.HospitalStayId > 0 Then
                                                          .HospitalStayId = detail.HospitalStayId
                                                      End If
                                                      If detail.HospitalStayDetailId > 0 Then
                                                          .HospitalStayDetailId = detail.HospitalStayDetailId
                                                      End If
                                                      If detail.ControlExternalConsultation > 0 Then
                                                          .ControlExternalConsultation = detail.ControlExternalConsultation
                                                      End If
                                                      If detail.ControlExternalConsultationCode > 0 Then
                                                          .ControlExternalConsultationCode = detail.ControlExternalConsultationCode
                                                      End If
                                                      .CUPSAssociateService = detail.CUPSAssociateService
                                                      If detail.CodeAssociateService IsNot Nothing Then
                                                          .CodeAssociateService = detail.CodeAssociateService
                                                      End If
                                                      .IsPackage = detail.IsPackage
                                                      .Packaging = detail.Packaging
                                                      If detail.PackageServiceOrderDetailId IsNot Nothing Then
                                                          .PackageServiceOrderDetailId = detail.PackageServiceOrderDetailId.Id
                                                      End If
                                                      .LiquidationType = detail.LiquidationType
                                                      .Presentation = detail.Presentation
                                                      If detail.ProductId IsNot Nothing Then
                                                          .ProductId = detail.ProductId.Id
                                                          .CodeNameProduct = detail.ProductId.CodeName
                                                      End If
                                                      .InvoicedQuantity = detail.InvoicedQuantity
                                                      .SupplyQuantity = detail.SupplyQuantity
                                                      .DevolutionQuantity = detail.DevolutionQuantity
                                                      .RateManualSalePrice = detail.RateManualSalePrice
                                                      .CostValue = detail.CostValue
                                                      .ServiceDate = detail.ServiceDate
                                                      If detail.AuthorizationNumber IsNot Nothing Then
                                                          .AuthorizationNumber = detail.AuthorizationNumber
                                                      End If
                                                      If detail.PerformsFunctionalUnitId IsNot Nothing Then
                                                          .PerformsFunctionalUnitId = detail.PerformsFunctionalUnitId.Id
                                                          .CodeNameFunctionalUnit = detail.PerformsFunctionalUnitId.Code + " - " + detail.PerformsFunctionalUnitId.Name
                                                      End If
                                                      If detail.PerformsHealthProfessionalCode IsNot Nothing Then
                                                          .PerformsHealthProfessionalCode = detail.PerformsHealthProfessionalCode
                                                      End If
                                                      If detail.PerformsProfessionalSpecialty IsNot Nothing Then
                                                          .PerformsProfessionalSpecialty = detail.PerformsProfessionalSpecialty
                                                      End If
                                                      If detail.PerformsHealthProfessionalThirdPartyId IsNot Nothing Then
                                                          .PerformsHealthProfessionalThirdPartyId = detail.PerformsHealthProfessionalThirdPartyId.Id
                                                      End If
                                                      If detail.BillingConceptId IsNot Nothing Then
                                                          .BillingConceptId = detail.BillingConceptId.Id
                                                      End If
                                                      .CostCenterId = detail.CostCenterId.Id
                                                      .CodeNameCostCenter = detail.CostCenterId.Code + " - " + detail.CostCenterId.Name
                                                      .SettlementType = detail.SettlementType
                                                      If detail.IncludeServiceOrderDetailId IsNot Nothing Then
                                                          .IncludeServiceOrderDetailId = detail.IncludeServiceOrderDetailId.Id
                                                      End If
                                                      .RecoveryRatio = detail.RecoveryRatio
                                                      If detail.RateManualId > 0 Then
                                                          .RateManualId = detail.RateManualId
                                                      End If
                                                      If detail.RateManualType > 0 Then
                                                          .RateManualType = detail.RateManualType
                                                      End If
                                                      If detail.RateManualDetailId IsNot Nothing Then
                                                          .RateManualDetailId = detail.RateManualDetailId.Id
                                                      End If
                                                      If detail.DefinitionRateDetailId IsNot Nothing Then
                                                          .DefinitionRateDetailId = detail.DefinitionRateDetailId.Id
                                                          .AllowValueChange = detail.DefinitionRateDetailId.AllowValueChange
                                                      End If
                                                      If detail.DefinitionRateDetailConditionId > 0 Then
                                                          .DefinitionRateDetailConditionId = detail.DefinitionRateDetailConditionId
                                                      End If
                                                      .IvaId = detail.IvaId
                                                      .TaxedService = (detail?.TaxPercent > 0)
                                                      .GrossValue = detail.GrossValue
                                                      .TaxValue = detail.TaxValue
                                                      .TaxPercent = detail.TaxPercent
                                                      .SubTotalSalesPrice = detail.SubTotalSalesPrice
                                                      .ThirdPartyDiscount = detail.ThirdPartyDiscount
                                                      .ThirdPartyDiscountPercentage = detail.ThirdPartyDiscountPercentage
                                                      .TotalSalesPrice = detail.TotalSalesPrice
                                                      .GrandTotalSalesPrice = detail.GrandTotalSalesPrice
                                                      .SurchargeApply = detail.SurchargeApply
                                                      .LiquidateAllMIVIE = detail?.RateManualXpo?.LiquidateAllMIVIE
                                                      If detail.SurgicalInterventionType > 0 Then
                                                          .SurgicalInterventionType = detail.SurgicalInterventionType
                                                      End If
                                                      .SurgeryNumber = detail.SurgeryNumber
                                                      .IsFirstEvent = detail.IsFirstEvent
                                                      .IsAnnulled = detail.IsAnnulled
                                                      .IsDelete = detail.IsDelete
                                                      .IncomeMainAccountId = detail.IncomeMainAccountId

                                                      .ApplyRIAS = Nothing
                                                      .RIASCupsId = Nothing
                                                      If detail.ApplyRIAS IsNot Nothing Then
                                                          .ApplyRIAS = detail.ApplyRIAS
                                                          If detail.RIASCupsId IsNot Nothing AndAlso detail.RIASCupsId > 0 Then
                                                              .RIASCupsId = detail.RIASCupsId
                                                          End If
                                                      End If

                                                      .CUPSEntityContractDescriptionId = Nothing
                                                      If detail.CUPSEntityContractDescriptionId IsNot Nothing Then
                                                          .CUPSEntityContractDescriptionId = detail.CUPSEntityContractDescriptionId.Id
                                                          .ContractDescriptionCodeName = detail.CUPSEntityContractDescriptionId.ContractDescriptionId.CodeName
                                                      End If
                                                  End With
                                                  SyncLock objLockSurgical
                                                      For Each detailSurgical In detail.ServiceOrderDetailSurgicalXpo
                                                          Dim serviceOrderDetailSurgical As New ServiceOrderDetailSurgical
                                                          With serviceOrderDetailSurgical
                                                              .Id = detailSurgical.Id
                                                              .ServiceOrderDetailId = detailSurgical.ServiceOrderDetailId.Id
                                                              .IPSServiceId = detailSurgical.IPSServiceId.Id
                                                              .CodeNameIpsService = detailSurgical.IPSServiceId.CodeName
                                                              Select Case detailSurgical.IPSServiceId.ServiceClass
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
                                                              .InvoicedQuantity = detailSurgical.InvoicedQuantity
                                                              .LiquidationPercentage = detailSurgical.LiquidationPercentage
                                                              .RateManualSalePrice = detailSurgical.RateManualSalePrice
                                                              .TotalSalesPrice = detailSurgical.TotalSalesPrice
                                                              If detailSurgical.PerformsHealthProfessionalCode IsNot Nothing Then
                                                                  .PerformsHealthProfessionalCode = detailSurgical.PerformsHealthProfessionalCode
                                                              End If
                                                              If detailSurgical.PerformsHealthProfessionalThirdPartyId IsNot Nothing Then
                                                                  .PerformsHealthProfessionalThirdPartyId = detailSurgical.PerformsHealthProfessionalThirdPartyId.Id
                                                              End If
                                                              .CostValue = detailSurgical.CostValue
                                                              .BillingConceptId = detailSurgical.BillingConceptId.Id
                                                              .CostCenterId = detailSurgical.CostCenterId
                                                              If detailSurgical.RateManualDetailSurgicalId IsNot Nothing Then
                                                                  .RateManualDetailSurgicalId = detailSurgical.RateManualDetailSurgicalId.Id
                                                              End If
                                                              .SurchargeApply = detailSurgical.SurchargeApply
                                                              .OnlyMedicalFees = detailSurgical.OnlyMedicalFees
                                                              .IncomeMainAccountId = detailSurgical.IncomeMainAccountId
                                                          End With
                                                          serviceOrderDetailSurgical.StartTracking()
                                                          serviceOrderDetailSurgical.MarkAsUnchanged()
                                                          serviceOrderDetailTmp.ServiceOrderDetailSurgical.Add(serviceOrderDetailSurgical)
                                                      Next
                                                  End SyncLock
                                                  serviceOrderDetailTmp.StartTracking()
                                                  serviceOrderDetailTmp.MarkAsUnchanged()
                                                  SyncLock objLock
                                                      listServiceOrderDetail.Add(serviceOrderDetailTmp)
                                                  End SyncLock
                                                  _serviceValue = listServiceOrderDetail.Sum(Function(x) x.GrandTotalSalesPrice)
                                                  _sumTaxValues = listServiceOrderDetail.Sum(Function(x) x.TaxValue * x.InvoicedQuantity)
                                                  ctrTmp.BeginInvoke(Sub()
                                                                         ctrTmp.PrintInfo()
                                                                     End Sub)
                                                  If serviceOrder.Status = 1 Then
                                                      'esto solo se hace cuando la orden es de estancia
                                                      If listServiceOrderDetail.FindAll(Function(x) x.HospitalStayId IsNot Nothing).Count > 0 Then
                                                          Mensaje(EeventViewerImages.Advertencia) = "La orden de servicio pertenece a una estancia y solo se puede anular"

                                                          BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

                                                          INDGcService.BeginInvoke(Sub()
                                                                                       ReadOnlyControls(True)
                                                                                   End Sub)
                                                          _readOnlyControlsForm = True
                                                          For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvService.Columns
                                                              If col.Name = "INDColSurgicalDetail" Then
                                                                  col.OptionsColumn.AllowEdit = True
                                                              End If
                                                          Next
                                                          INDBtnAddService.BeginInvoke(Sub()
                                                                                           INDColService.FieldName = "CodeNameIpsService"
                                                                                           INDGcSurgicalDetail.Enabled = True
                                                                                           INDBtnAddService.Enabled = False
                                                                                       End Sub)
                                                      End If
                                                      'cuando la orden es de dispensacion
                                                      If listServiceOrderDetail.FindAll(Function(x) x.RecordType = 2).Count > 0 Then
                                                          Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                                          BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

                                                          If serviceOrder.AffectInventory Then
                                                              BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                                                          Else
                                                              BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                                                          End If
                                                          INDGcService.BeginInvoke(Sub()
                                                                                       ReadOnlyControls(True)
                                                                                   End Sub)
                                                          _readOnlyControlsForm = True
                                                          INDBtnAddService.BeginInvoke(Sub()
                                                                                           INDColService.FieldName = "CodeNameProduct"
                                                                                           INDGcSurgicalDetail.Enabled = True
                                                                                           INDBtnAddService.Enabled = False
                                                                                       End Sub)
                                                      End If
                                                      'si la orden tiene items empaquetados
                                                      If listServiceOrderDetail.FindAll(Function(x) x.IsPackage).Count > 0 Then
                                                          INDBtnAddService.BeginInvoke(Sub()
                                                                                           INDBtnAddService.Enabled = False
                                                                                       End Sub)
                                                      End If
                                                  Else
                                                      INDBtnAddService.BeginInvoke(Sub()
                                                                                       INDBtnAddService.Enabled = False
                                                                                   End Sub)
                                                  End If
                                                  INDGcService.BeginInvoke(Sub()
                                                                               INDGcService.DataSource = Nothing
                                                                               INDGcService.DataSource = listServiceOrderDetail
                                                                           End Sub)
                                              End Sub)

                         End Using
                     End Sub)
    End Function

    ''' <summary>
    ''' Permite mostrar el botón de importar cotización
    ''' </summary>
    Private Sub ShowHidePermissionImportButton()
        If (From x In BarraBotones.PermissionsForm Where x.Key = 95 Select x).Count > 0 Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.ImportarInformacion, "Importar Cotización")
        Else
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        End If
    End Sub

    ''' <summary>
    ''' metodo para cargar controles al formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function LoadControls() As Task
        Try
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using model As New MServiceOrder(Me.MyTag)
                AsyncLoader(True)
                serviceOrder = Await model.GetServiceOrderAsync(Code)
                INDLcServiceOrders.BeginUpdate()
                If serviceOrder IsNot Nothing AndAlso serviceOrder.Id > 0 Then
                    INDBtnAddService.Enabled = True
                    With serviceOrder
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("InvoicedUser"), .InvoicedUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("InvoicedDate"), .InvoicedDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Me.BarraBotones.StatusRecordVisible = True
                        BarraBotones.OperatingUnitValue = .OperatingUnitId
                        Select Case .Status
                            Case 1
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                            Case Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                                _readOnlyControlsForm = True
                                For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvService.Columns
                                    If col.Name = "INDColSurgicalDetail" Then
                                        col.OptionsColumn.AllowEdit = True
                                    End If
                                Next
                                INDGcSurgicalDetail.Enabled = True
                        End Select
                        Code = .Code
                        OrderDate = .OrderDate
                        Dim admissionTmp = model.GetAdmissionByServiceOrder(.AdmissionNumber.Trim())
                        If admissionTmp IsNot Nothing Then
                            SetAdmission(admissionTmp)
                        End If
                        BarraBotones.StatusRecord = .Status.ToString()
                    End With
                    Me.GetDocumentIndexed(MyTag & "_" & serviceOrder.Code)
                    GenerateBlockRecord()
                    Await LoadServiceOrderDetail()
                    BarraBotones.SetDocuments(serviceOrder.Id)
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

                    ShowHidePermissionImportButton()

                    Await LoadReport()
                    AsyncLoader(False)
                    ActionsOnControls = True
                    Me.BeginInvoke(Sub() INDSleAdmissionNumber2.Focus())
                Else
                    AsyncLoader(False)
                    ActionsOnControls = True
                    If Me._sequense.IsManual Then
                        Me.NewServiceOrder()
                    Else
                        AsyncLoader(False)
                        Deshacer()
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Code = String.Empty
                        INDBteCode.Focus()
                    End If
                End If
                INDLcServiceOrders.EndUpdate()
            End Using
            RaiseEvent LoadEnded(Me, New EventArgs())
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' funcion que carga el reporte de forma asincrona
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, serviceOrder.Id, 0, serviceOrder.Id, serviceOrder.AdmissionNumber))
                                     End Sub)
    End Function

    ''' <summary>
    ''' metodo que se encarga de mostrar en la rejilla los registros agregados desde el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturAddServiceOrderDetail(sender As Object, e As AddServiceEventArgs)
        If e.ListServiceOrderDetail Is Nothing Then
            Return
        End If
        If e.EditMode = False Then
            If listServiceOrderDetail Is Nothing Then
                listServiceOrderDetail = New List(Of ServiceOrderDetail)
            End If
            Dim errorsEvent As New StringBuilder
            'recalculamos para saber que item va coomo primer evento y asi aplicar o no los porcentajes
            For Each item In e.ListServiceOrderDetail
                If item.Presentation = 2 Then

                    If item.SurgicalInterventionType <> 1 Then
                        Dim detailFirstEvent = listServiceOrderDetail.Find(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.IsFirstEvent = True)
                        If detailFirstEvent IsNot Nothing Then
                            If item.SubTotalSalesPrice > detailFirstEvent.SubTotalSalesPrice Then

                                'valido que los items no esten bloqueados o facturados en los folios  
                                Dim listEventsTmp = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.Id > 0)
                                For Each itemEvent In listEventsTmp
                                    If itemEvent.IsPackage Then
                                        errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el servicio " + itemEvent.CodeNameIpsService + " esta empaquetado")
                                        Exit For
                                    End If
                                    Using model As New MServiceOrder(MyTag)
                                        'valido que los items no esten distribuidos
                                        Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(itemEvent.Id)
                                        If listDistribution.Count > 1 Then
                                            errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el servicio " + itemEvent.CodeNameIpsService + " esta distribuido")
                                            Exit For
                                        End If
                                        'valido que los folios no esten bloqueado o facturados
                                        Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(itemEvent.Id)
                                        If revenueControlDetail.Status > 1 Then
                                            If revenueControlDetail.Status = 2 Then
                                                errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta facturado")
                                            Else
                                                errorsEvent.AppendLine("El servicio " + item.CodeNameIpsService + " no se puede agregar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta bloqueado")
                                            End If
                                            Exit For
                                        End If
                                    End Using
                                Next

                                If errorsEvent.Length > 0 Then
                                    Continue For
                                End If
                                item.IsFirstEvent = True
                                If item.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(item, False)
                                End If
                                detailFirstEvent.IsFirstEvent = False
                                If detailFirstEvent.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(detailFirstEvent, False)
                                End If
                            Else
                                item.IsFirstEvent = False
                                If item.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(item, False)
                                End If
                            End If
                        Else
                            item.IsFirstEvent = True
                            If item.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(item, False)
                            End If
                        End If
                    End If
                End If
                listServiceOrderDetail.Add(item)
            Next

            ValidateMIVIE()
            If errorsEvent.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errorsEvent.ToString()
                Exit Sub
            End If
            popupService.ListServiceOrderDetailSurgicalIntervention = listServiceOrderDetail
            popupService.ListServiceOrderDetailDatasourceIncludeService = listServiceOrderDetail
            _serviceValue = listServiceOrderDetail.Sum(Function(x) x.GrandTotalSalesPrice)
            _sumTaxValues = listServiceOrderDetail.Sum(Function(x) x.TaxValue * x.InvoicedQuantity)
            ctrTmp.PrintInfo()
            INDGcService.DataSource = Nothing
            INDGcService.DataSource = listServiceOrderDetail
        Else
            If e.ListServiceOrderDetailDelete IsNot Nothing Then
                For Each item As ServiceOrderDetail In e.ListServiceOrderDetailDelete
                    item.MarkAsDeleted()
                Next
                If Not serviceOrder.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("ServiceOrderDetail") Then
                    serviceOrder.ChangeTracker.ObjectsRemovedFromCollectionProperties.Add("ServiceOrderDetail", e.ListServiceOrderDetailDelete)
                Else
                    serviceOrder.ChangeTracker.ObjectsRemovedFromCollectionProperties("ServiceOrderDetail").AddRange(e.ListServiceOrderDetailDelete)
                End If

            End If
            listServiceOrderDetail.Remove(serviceOrderDetail)
            'recalculamos para saber que item va coomo primer evento y asi aplicar o no los porcentajes
            For Each item In e.ListServiceOrderDetail
                If item.Presentation = 2 Then
                    If item.SurgicalInterventionType <> 1 Then
                        Dim detailFirstEvent = listServiceOrderDetail.Find(Function(x) x.SurgeryNumber = item.SurgeryNumber And x.IsFirstEvent = True)
                        If detailFirstEvent IsNot Nothing Then
                            If item.SubTotalSalesPrice > detailFirstEvent.SubTotalSalesPrice Then
                                item.IsFirstEvent = True
                                If item.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(item, True)
                                End If
                                detailFirstEvent.IsFirstEvent = False
                                If detailFirstEvent.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(detailFirstEvent, True)
                                End If
                            Else
                                item.IsFirstEvent = False
                                If item.SurgicalInterventionType < 9 Then
                                    GetValueSurgicalEvenst(item, True)
                                End If
                            End If
                        Else
                            item.IsFirstEvent = True
                            If item.SurgicalInterventionType < 9 Then
                                GetValueSurgicalEvenst(item, True)
                            End If
                        End If
                    End If
                End If
                listServiceOrderDetail.Add(item)
            Next

            ValidateMIVIE()
            If popupService IsNot Nothing Then
                popupService.ListServiceOrderDetailSurgicalIntervention = listServiceOrderDetail
                popupService.ListServiceOrderDetailDatasourceIncludeService = listServiceOrderDetail
            End If
            _serviceValue = listServiceOrderDetail.Sum(Function(x) x.GrandTotalSalesPrice)
            _sumTaxValues = listServiceOrderDetail.Sum(Function(x) x.TaxValue * x.InvoicedQuantity)
            ctrTmp.PrintInfo()
            INDGcService.DataSource = Nothing
            INDGcService.DataSource = listServiceOrderDetail

        End If
        INDSleAdmissionNumber2.IsReadOnly = True
    End Sub

    ''' <summary>
    ''' metodo para validar que los items que son MIVIE y sean mas de 2, los dos primeros se liquiden como dice el manual y los demas no se cobren
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ValidateMIVIE()
        Dim listEvents = (From e In listServiceOrderDetail Where e.SettlementType = 1 Select e.SurgeryNumber).Distinct().ToList()

        For item As Integer = 0 To listEvents.Count - 1 Step 1
            Dim firstEvent = listServiceOrderDetail.Find(Function(x) x.IsFirstEvent = True And x.SurgeryNumber = listEvents(item))
            Dim index = 2
            Dim listMIVIE = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = listEvents(item) AndAlso x.RateManualType < 3 AndAlso x.SurgicalInterventionType IsNot Nothing AndAlso x.SurgicalInterventionType = 3 And (x.LiquidateAllMIVIE Is Nothing OrElse Not x.LiquidateAllMIVIE))
            If listMIVIE.Count > 2 Then
                listMIVIE = (From l In listMIVIE Order By l.RateManualSalePrice Descending).ToList()
                If listMIVIE.Exists(Function(x) x.IsFirstEvent = True) Then
                    listMIVIE.Remove(firstEvent)
                    index = 1
                End If
                For i As Integer = index To listMIVIE.Count - 1 Step 1
                    listMIVIE.ElementAt(i).SubTotalSalesPrice = 0
                    listMIVIE.ElementAt(i).TotalSalesPrice = 0
                    listMIVIE.ElementAt(i).GrandTotalSalesPrice = 0
                    For Each itemSurgical In listMIVIE.ElementAt(i).ServiceOrderDetailSurgical
                        itemSurgical.TotalSalesPrice = 0
                    Next
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' metodo para obtener valorres de los items y luego aplicar el porcentaje correspondiente
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RecalculateSurgicalItems()
        'obtengo los items que son quirurgicos para hacer los calculos
        Dim listServiceOrderDetailSurgicaTmp = listServiceOrderDetail.FindAll(Function(x) x.Presentation = 2 And x.SettlementType <> 3)

        For Each item In listServiceOrderDetailSurgicaTmp
            'si el item no es basico ni cruento se recalcula
            If item.SurgicalInterventionType > 1 And item.SurgicalInterventionType < 9 Then
                Using model As New MServiceOrder(MyTag)
                    Dim index = listServiceOrderDetail.IndexOf(item)
                    Dim detailTmp = model.RecalculateSurgicalEvents(item)
                    listServiceOrderDetail.Remove(item)
                    listServiceOrderDetail.Insert(index, detailTmp)
                    GetValueSurgicalEvenst(listServiceOrderDetail(index), True)
                End Using
            End If
        Next
        ValidateMIVIE()
    End Sub

    ''' <summary>
    ''' funcion que recalcula el valor de los detalles qx dependiedo de la parametrizacion del manual de tarifas
    ''' </summary>
    ''' <param name="serviceOrdeDetailItem"></param>
    ''' <param name="editMode"></param>
    Private Sub GetValueSurgicalEvenst(serviceOrdeDetailItem As ServiceOrderDetail, editMode As Boolean)
        Using model As New MServiceOrder(Me.Tag)
            Dim SurgeriesPercentageManual = model.GetSurgeriesPercetageManualByRateManualIdInterventionType(serviceOrdeDetailItem.RateManualId, serviceOrdeDetailItem.SurgicalInterventionType)
            If (serviceOrdeDetailItem.IsFirstEvent = True AndAlso SurgeriesPercentageManual.MainHundredPercent = False) OrElse (serviceOrdeDetailItem.IsFirstEvent = False) Then
                If SurgeriesPercentageManual.RateManual Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudo encontrar el porcentaje de liquidacion para la cirugia, por favor verifique el manual"
                    Exit Sub
                End If
                For Each item In serviceOrdeDetailItem.ServiceOrderDetailSurgical
                    If editMode Then
                        Select Case item.ClassServiceIps?.ToUpper()
                            Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.RateManualSalePrice * SurgeriesPercentageManual.SurgeonPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.RateManualSalePrice * SurgeriesPercentageManual.AnesthesiologistPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.RateManualSalePrice * SurgeriesPercentageManual.AssistantPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("RightRoom", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.RateManualSalePrice * SurgeriesPercentageManual.RoomPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.RateManualSalePrice * SurgeriesPercentageManual.MaterialsPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        End Select
                    Else
                        Select Case item.ClassServiceIps?.ToUpper()
                            Case ResourceManager.GetString("Surgeon", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.SurgeonPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("Anesthesiologist", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AnesthesiologistPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("Assistant", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.AssistantPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("RightRoom", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.RoomPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                            Case ResourceManager.GetString("SutureMaterials", "Contract").ToUpper()
                                item.TotalSalesPrice = Utils.RoundValue(item.TotalSalesPrice * SurgeriesPercentageManual.MaterialsPercentage / 100, SurgeriesPercentageManual.RateManual.RoundService)
                        End Select
                    End If

                Next
                serviceOrdeDetailItem.ThirdPartyDiscount = Utils.RoundValue(serviceOrdeDetailItem.ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice * serviceOrdeDetailItem.ThirdPartyDiscountPercentage) / 100, Utils.RoundLevel.Unit)
            End If

            With serviceOrdeDetailItem
                '/**************--Segmento Impuestos--**********************/
                'se suman el detalle de los qx
                .SubTotalSalesPrice = .ServiceOrderDetailSurgical.Sum(Function(x) x.TotalSalesPrice)
                .GrossValue = .SubTotalSalesPrice
                .TaxValue = 0
                'independientmente si viene de detalles qx o no al final en base a la parametrizacion del servicio y el sistema se establece el valor bruto, el iva y el subtotal
                Dim _dictionaryValues = Utils.SetValueSalesPrice(Me._flagTaxInclude,
                                                                  .SubTotalSalesPrice,
                                                                  ?.TaxPercent)
                If?.TaxedService AndAlso _dictionaryValues?.Any() Then
                    .GrossValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.GrossValue).Value
                    .TaxValue = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.TaxValue).Value
                    .SubTotalSalesPrice = _dictionaryValues?.FirstOrDefault(Function(x) x.Key = Utils.eTypeTaxControl.SubtotalSalesPrice).Value
                    If .SubTotalSalesPrice > .RoundService Then
                        .SubTotalSalesPrice = Utils.RoundValue(.SubTotalSalesPrice, .RoundService)
                    End If
                End If
                '/***************************************************************/
                .TotalSalesPrice = .SubTotalSalesPrice - .ThirdPartyDiscount
                .GrandTotalSalesPrice = .TotalSalesPrice * .InvoicedQuantity
            End With
        End Using
    End Sub

    ''' <summary>
    ''' metodo para establecer los datos del ingreso
    ''' </summary>
    ''' <param name="record"></param>
    ''' <remarks></remarks>
    Private Async Sub SetAdmission(record As Object)
        If record IsNot Nothing Then
            admission = record
            INDTxtAdmissionPopup.Text = "Tipo de Ingreso : " & ResourceManager.GetString(String.Concat("AdmissionType", record.AdmissionType.ToString().Trim()))
            Mensaje(EeventViewerImages.Informacion) = INDTxtAdmissionPopup.Text
            _admissionNumber = admission.AdmissionCode.ToString().Trim()
            patientDateBirth = record.PatientDateBirth
            patientGenus = record.PatientGenus
            If record.Status.Equals("B") Then
                Mensaje(EeventViewerImages.Informacion) = "El ingreso de esta orden de servicio se encuentra en estado: Bloqueado"
            End If
            'Consultamos de uns sp los ingresos
            Dim _auxAdmissionToReload As Infrastructure.Data.Xpo.CrystalRepository.ViewLiquidationGetAdmissionAll = Nothing
            Await Task.Factory.StartNew(Sub()
                                            _auxAdmissionToReload = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer) _
                                                    .CrystalService.Liquidation_GetAdmission(admission.AdmissionCode.ToString().Trim())
                                        End Sub)

            Me.INDSleAdmissionNumber2.DisplayNullText = String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"),
                        _auxAdmissionToReload.AdmissionCode.ToString().Trim(),
                        _auxAdmissionToReload.PatientCode.ToString().Trim(),
                        _auxAdmissionToReload.PatientName.ToString().Trim())

            Dim documentType As String = String.Empty
            Select Case _auxAdmissionToReload.PatientDocumentType.ToString().Trim()
                Case "1"
                    documentType = "CC"
                Case "2"
                    documentType = "CE"
                Case "3"
                    documentType = "TI"
                Case "4"
                    documentType = "RC"
                Case "5"
                    documentType = "PA"
                Case "6"
                    documentType = "AS"
                Case "7"
                    documentType = "MS"
                Case "8"
                    documentType = "NU"
            End Select
            Me.TxtPatientCode.Text = If(_auxAdmissionToReload.PatientCode Is Nothing, String.Empty, String.Concat(documentType, " - ", _auxAdmissionToReload.PatientCode.ToString().Trim()))
            Me.TxtPatientName.Text = If(_auxAdmissionToReload.PatientName Is Nothing, String.Empty, _auxAdmissionToReload.PatientName.ToString().Trim())
            Me.TxtPatientBirth.Text = Convert.ToDateTime(_auxAdmissionToReload.PatientBirth).ToString(SessionValues.Instance.Culture)
            Me.TxtPatientAge.Text = Utils.AgeToString(Convert.ToDateTime(_auxAdmissionToReload.PatientBirth))
            Me.TxtPatientType.EditValue = Convert.ToInt32(_auxAdmissionToReload.PatientType)
            Me.TxtAfiliationType.EditValue = Convert.ToInt32(_auxAdmissionToReload.PatientAfiliation)
            Me.TxtPatientEstrato.Text = (_auxAdmissionToReload.NivelCode & " - " & _auxAdmissionToReload.NivelName)

            'DATOS DEL INGRESO
            Me.TxtAdmissionCode.Text = _auxAdmissionToReload.AdmissionCode.ToString().Trim()
            Me.TxtAdmissionDate.Text = Convert.ToDateTime(_auxAdmissionToReload.AdmissionDate).ToString(SessionValues.Instance.Culture)
            INDDteOrderDate.Properties.MinValue = Convert.ToDateTime(_auxAdmissionToReload.AdmissionDate).ToString(SessionValues.Instance.Culture)
            Me.TxtEntityNameAdmission.Text = _auxAdmissionToReload.EntityName
            Select Case _auxAdmissionToReload.AdmissionRiskType
                Case "1"
                    Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
                Case "2"
                    Me.TxtRiskType.Text = "Accidente de Tránsito"
                Case "3"
                    Me.TxtRiskType.Text = "Catástrofe"
                Case "4"
                    Me.TxtRiskType.Text = "Enfermedad General y Maternidad"
                Case "5"
                    Me.TxtRiskType.Text = "Accidente de Trabajo"
                Case "6"
                    Me.TxtRiskType.Text = "Enfermedad Profesional"
                Case "7"
                    Me.TxtRiskType.Text = "Atención Inicial de Urgencias"
                Case "8"
                    Me.TxtRiskType.Text = "Otro Tipo de Accidente"
                Case "9"
                    Me.TxtRiskType.Text = "Lesión Por Agresión"
                Case "10"
                    Me.TxtRiskType.Text = "Lesión AutoInfligida"
                Case "11"
                    Me.TxtRiskType.Text = "Maltrato Físico"
                Case "12"
                    Me.TxtRiskType.Text = "Promoción y Prevención"
                Case "13"
                    Me.TxtRiskType.Text = "Otro"
                Case "14"
                    Me.TxtRiskType.Text = "Accidente Rabico"
                Case "15"
                    Me.TxtRiskType.Text = "Accidente Ofídico"
                Case "16"
                    Me.TxtRiskType.Text = "Sopecha de Abuso Sexual"
                Case "17"
                    Me.TxtRiskType.Text = "Sopecha de Violencia Sexual"
                Case "18"
                    Me.TxtRiskType.Text = "Sopecha de Maltrato Emocional"
            End Select
            Me.TxtPlaceEntry.Text = ResourceManager.GetString("PlaceEntry_" & _auxAdmissionToReload.PlaceEntry.ToString(), "IndigoCrystalHis")
            Me.TxtAdmissionType.EditValue = Convert.ToInt32(_auxAdmissionToReload.AdmissionType)
            Me.TxtBedStay.Text = If(_auxAdmissionToReload.BedStay Is Nothing, String.Empty, _auxAdmissionToReload.BedStay.ToString().Trim())
            Me.TxtLiquidationType.EditValue = Convert.ToInt32(_auxAdmissionToReload.LiquidationType)
            Me.TxtAuthorization.Text = _auxAdmissionToReload.AuthorizationNumber.ToString().Trim()
            Me.TxtAtentionCenter.Text = _auxAdmissionToReload.AdmissionCentAtencCodeName
            Me.TxtFunctionalUnitAdmission.Text = _auxAdmissionToReload.AdmissionUniFuncCodeName.ToString().Trim()
            Me.TxtResponsibleName.Text = If(_auxAdmissionToReload.ResponsibleName Is Nothing, String.Empty, _auxAdmissionToReload.ResponsibleName.ToString().Trim())
            Me.TxtResponsiblePhone.Text = _auxAdmissionToReload.ResponsiblePhone

            If _auxAdmissionToReload IsNot Nothing Then
                If CInt(_auxAdmissionToReload.CareGroupTypePatient) <> -1 Then
                    Select Case CByte(_auxAdmissionToReload.CareGroupTypePatient)
                        Case 1, 2, 4 'EAPB con contrato
                            Me.TxtPatientEntityName.Text = String.Concat(_auxAdmissionToReload.PatientEntityCode, " - ", _auxAdmissionToReload.PatientEntity)
                        Case 3 'Particulares
                            LiEntity.Text = ResourceManager.GetString("LyciSleThirdPartyText_ThirdParty", GetType(CtrFolio).Name)
                            'tercero
                            Me.TxtPatientEntityName.Text = _auxAdmissionToReload.PatientEntity
                    End Select
                    Me.TxtCareGroupPatient.Text = _auxAdmissionToReload.CareGroupCodeName
                End If
                Me.TxtCareGroupAdmission.Text = _auxAdmissionToReload.AdmissionCareGroupCodeName
            End If
        End If
    End Sub

    ''' <summary>
    ''' validar que el item se pueda editar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateEditItem() As String
        If serviceOrderDetail.IsPackage Then
            Return "El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede editar porque esta empaquetado"
        End If
        Using model As New MServiceOrder(MyTag)
            'valido que los items no esten distribuidos
            Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(serviceOrderDetail.Id)
            If listDistribution.Count > 1 Then
                Return "El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede editar porque  esta distribuido"
            End If
            Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetail.Id)
            If revenueControlDetail.Status > 1 Then
                If revenueControlDetail.Status = 2 Then
                    Return "El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede editar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta facturado"
                Else
                    Return "El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede editar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta bloqueado"
                End If
            End If
        End Using
        Return String.Empty
    End Function

    ''' <summary>
    ''' Carga Inicial del formulario
    ''' </summary>
    Public Async Function OnInitForm() As Task

        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await Task.Factory.StartNew(Sub()
                                        _indigoSession = SessionValues.Instance
                                        presenter = New PServiceOrder(Me)
                                        presenter.GetSequence()
                                        'parámetos de contratos
                                        _settingsContractXpo = presenter.GetSettingsContractByOperatingUnitId(_idOperativeUnit)
                                        'Datasource Admisiones
                                        Using model As New MServiceOrder(MyTag.ToString())
                                            Dim ds = model.GetViewAdmissionOpenAndPartialServiceOrder()
                                            INDSleAdmissionNumber2.SafeInvoke(Sub()
                                                                                  INDSleAdmissionNumber2.Datasource = ds
                                                                              End Sub)
                                        End Using
                                    End Sub)

        IndigoGridView1.MoreInfoColunmns(INDGvService)
        Me.INDSleAdmissionNumber2.FuncQueryOnKeyEnterPressed = AddressOf Me.INDSleAdmissionNumber2_KeyDown
        Me._doc = Nothing
        AddActionsColumns()
        Deshacer()
        LoadStatus()
        _searchMode = False
        Await presenter.LoadFlagTaxInclude()
        INDDteOrderDate.Properties.MaxValue = Me.GetDateServer()
    End Function
#End Region

#Region "HANDLES"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        _serviceValue = Nothing
        Me._sumTaxValues = 0
        admission = Nothing
        _sequense = Nothing
        presenter = Nothing
        _idOperativeUnit = Nothing
        serviceOrder = Nothing
        listServiceOrderDetail = Nothing
        listServiceOrderDetailDelete = Nothing
        _searchMode = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
        patientGenus = Nothing
        patientDateBirth = Nothing
        serviceOrderDetail = Nothing
        indexEditItem = Nothing
        rowEditing = Nothing
        popupService = Nothing
        _readOnlyControlsForm = Nothing
        varImp = Nothing
    End Sub

    Private Async Sub FrmOrdersService_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If LoadInParentForm Then
            Exit Sub
        End If
        AsyncLoader(True)
        Await OnInitForm()
        AsyncLoader(False)
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmOrdersService_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDBteCode.Enabled = True Then
            INDBteCode.Focus()
        End If
    End Sub
#End Region

#Region "FormClosing"
    Private Sub FrmOrdersService_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
        If Not Me.BarraBotones.Enabled Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnAddService_Click(sender As Object, e As EventArgs) Handles INDBtnAddService.Click
        If admission Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso"
            Exit Sub
        End If

        Me.Cursor = ChangeCursorIndigo()
        popupService = New FrmPopupServices
        popupService.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
        popupService.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
        popupService.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        popupService.RequestQuoteServices = Me.RequestQuoteServices
        popupService.ServiceOrderId = serviceOrder.Id
        popupService.ServiceOrderStatus = serviceOrder.Status.ToString()
        popupService.Admission = AdmissionNumber
        popupService.Patient = admission.PatientCode.ToString().Trim() + " - " + TxtPatientName.Text
        popupService.Stay = TxtBedStay.Text
        popupService.PatientDateBirth = patientDateBirth
        popupService.PatientGenus = patientGenus
        popupService.AdmissionDate = INDDteOrderDate.EditValue
        popupService.AdmissionDateMinValue = INDDteOrderDate.Properties.MinValue
        popupService.AutorizationNumber = IIf(String.IsNullOrEmpty(TxtAuthorization.Text), "", TxtAuthorization.Text)
        popupService.ServiceDate = INDDteOrderDate.EditValue
        popupService.TaxInclude = _flagTaxInclude

        If listServiceOrderDetail IsNot Nothing Then
            popupService.ListCompare = listServiceOrderDetail.Select(Function(x) x.CloneEntity()).Cast(Of ServiceOrderDetail).ToList()
        End If

        If listServiceOrderDetailDelete IsNot Nothing Then
            popupService.ListServiceOrderDetailDelete = listServiceOrderDetailDelete
        End If

        If admission.CareGroupId IsNot Nothing AndAlso CType(admission.CareGroupId, Integer) > 0 Then
            popupService.CareGroupAdmission = admission.CareGroupId.ToString()
        End If

        popupService.HealthAdministratorCrystal = admission.HealthAdministratorId
        AddHandler popupService.AddServiceOrderDetail, AddressOf ReturAddServiceOrderDetail

        If listServiceOrderDetail IsNot Nothing Then
            popupService.ListServiceOrderDetailSurgicalIntervention = listServiceOrderDetail
            popupService.ListServiceOrderDetailDatasourceIncludeService = listServiceOrderDetail
        Else
            popupService.ListServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)
            popupService.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
        End If

        Dim transparent = New Base.FrmTransparent(popupService, False)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        transparent.ShowDialog(Me.MdiParent)
    End Sub
#End Region

#Region "KeyDown"
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        'Se valida si no ha cargado la secuencia
        If Me._sequense Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha cargado la secuencia numérica"
            Exit Sub
        End If
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Me.NewServiceOrder()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

    Private Sub INDSleAdmissionNumber_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDDteOrderDate.Enabled = False Then
                INDBtnAddService.Focus()
            Else
                INDDteOrderDate.Focus()
            End If

        End If
    End Sub

    Private Sub INDDteOrderDate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDDteOrderDate.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDBtnAddService.Focus()
        End If
    End Sub
#End Region

#Region "NewSelectedValue"
    Private Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs)
        'valido que el ingreso no este facturado
        Using model As New MAdmissions(MyTag)
            Dim admissionTmp = model.GetAdmissionsByCodeSimple(e.AdmissionObject.AdmissionCode.ToString().Trim()).ObjectEmbbeded
            If admissionTmp.IESTADOIN = "F" Then
                Mensaje(EeventViewerImages.Advertencia) = "El ingreso esta facturado"
                admission = Nothing
                INDBtnAddService.Enabled = False
                Exit Sub
            End If
        End Using

        INDBtnAddService.Enabled = True
        SetAdmission(e.AdmissionObject)
        ctrTmp.PrintInfo()
    End Sub
#End Region

#Region "Click_ButtonAction"
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If _readOnlyControlsForm Then
            Mensaje(EeventViewerImages.Advertencia) = "El registro esta en modo de solo lectura"
            Exit Sub
        End If
        If admission Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un ingreso"
            Exit Sub
        End If

        serviceOrderDetail = DirectCast(INDGvService.GetFocusedRow(), ServiceOrderDetail)
        Select Case sender.Tag
            Case "Edit"
                indexEditItem = listServiceOrderDetail.IndexOf(serviceOrderDetail)
                rowEditing = INDGvService.FocusedRowHandle
                Me.Cursor = ChangeCursorIndigo()
                Using formulario As New FrmPopupServices
                    If serviceOrderDetail.Id > 0 Then
                        Dim errorsEdit = ValidateEditItem()
                        If errorsEdit.Length > 0 Then
                            formulario.AllowEditItem = False
                            Mensaje(EeventViewerImages.Advertencia) = errorsEdit
                        Else
                            formulario.AllowEditItem = True
                        End If
                    Else
                        formulario.AllowEditItem = True
                    End If
                    formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
                    formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    formulario.RequestQuoteServices = Me.RequestQuoteServices
                    formulario.ServiceOrderId = serviceOrder.Id
                    formulario.ServiceOrderStatus = serviceOrder.Status.ToString()
                    formulario.Admission = AdmissionNumber
                    formulario.HealthAdministratorCrystal = admission.HealthAdministratorId
                    formulario.Patient = admission.PatientCode.ToString().Trim() + " - " + TxtPatientName.Text
                    formulario.Stay = TxtBedStay.Text
                    formulario.PatientDateBirth = patientDateBirth
                    formulario.PatientGenus = patientGenus
                    formulario.ServiceOrderDetailEdit = serviceOrderDetail
                    formulario.AdmissionDate = INDDteOrderDate.EditValue
                    formulario.AdmissionDateMinValue = INDDteOrderDate.Properties.MinValue
                    formulario.EditMode = True
                    formulario.TaxInclude = Me._flagTaxInclude
                    formulario.ListCompare = listServiceOrderDetail.Where(Function(item) Not item.Equals(serviceOrderDetail)).ToList()
                    If listServiceOrderDetailDelete IsNot Nothing Then
                        formulario.ListServiceOrderDetailDelete = listServiceOrderDetailDelete
                    End If
                    AddHandler formulario.AddServiceOrderDetail, AddressOf ReturAddServiceOrderDetail
                    If listServiceOrderDetail IsNot Nothing Then
                        formulario.ListServiceOrderDetailSurgicalIntervention = listServiceOrderDetail
                        formulario.ListServiceOrderDetailDatasourceIncludeService = listServiceOrderDetail
                    Else
                        formulario.ListServiceOrderDetailSurgicalIntervention = New List(Of ServiceOrderDetail)
                        formulario.ListServiceOrderDetailDatasourceIncludeService = New List(Of ServiceOrderDetail)
                    End If
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)

                    Using bg As New BackgroundWorker
                        AddHandler bg.DoWork, AddressOf bg_DoWork
                        AddHandler bg.RunWorkerCompleted, AddressOf bg_RunWorkerCompleted
                        bg.RunWorkerAsync()
                    End Using
                End If
        End Select
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDRptPceSurgicalDetail_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDRptPceSurgicalDetail.QueryPopUp
        Dim serviceOrderDatailTmp = DirectCast(INDGvService.GetFocusedRow, ServiceOrderDetail)
        INDGcSurgicalDetail.DataSource = Nothing
        INDGcSurgicalDetail.DataSource = serviceOrderDatailTmp.ServiceOrderDetailSurgical
        INDTxtEventNumber.EditValue = serviceOrderDatailTmp.SurgeryNumber
        Dim listEventsTmp = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = serviceOrderDatailTmp.SurgeryNumber)
        INDGcEvents.DataSource = Nothing
        INDGcEvents.DataSource = listEventsTmp
    End Sub

#End Region

#Region "ShowingEditor"
    Private Sub INDGvService_ShowingEditor(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDGvService.ShowingEditor
        Dim serviceOrderDatailTmp = DirectCast(INDGvService.GetFocusedRow, ServiceOrderDetail)
        If serviceOrderDatailTmp.Presentation <> 2 Then
            INDRptPceSurgicalDetail.ReadOnly = True
        Else
            INDRptPceSurgicalDetail.ReadOnly = False
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
        If Me.serviceOrder IsNot Nothing AndAlso Me.serviceOrder.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "EditValueChanging"
    Private Sub INDRptRgIsFirstEvent_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptRgIsFirstEvent.EditValueChanging
        AsyncLoader(True)
        Dim serviceOrderDatailFirstEventNew = DirectCast(INDGvEvents.GetFocusedRow, ServiceOrderDetail)
        Dim listEventsTmp = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = serviceOrderDatailFirstEventNew.SurgeryNumber And x.Presentation = 2 And x.Id > 0)

        For Each itemEvent In listEventsTmp
            If itemEvent.IsPackage Then
                Mensaje(EeventViewerImages.Advertencia) = "El servicio " + serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el servicio " + itemEvent.CodeNameIpsService + " esta empaquetado"
                e.Cancel = True
                AsyncLoader(False)
                Exit Sub
            End If
            Using model As New MServiceOrder(MyTag)
                'valido que los items no esten distribuidos
                Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(itemEvent.Id)
                If listDistribution.Count > 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El servicio " + serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el servicio " + itemEvent.CodeNameIpsService + " esta distribuido"
                    e.Cancel = True
                    AsyncLoader(False)
                    Exit Sub
                End If

                Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(itemEvent.Id)
                If revenueControlDetail.Status > 1 Then
                    If revenueControlDetail.Status = 2 Then
                        Mensaje(EeventViewerImages.Advertencia) = "El servicio " + serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta facturado"
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "El servicio " + serviceOrderDatailFirstEventNew.CodeNameIpsService + " no se puede modificar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta bloqueado"
                    End If
                    e.Cancel = True
                    AsyncLoader(False)
                    Exit Sub
                End If
            End Using
        Next

        Dim indexNew = listServiceOrderDetail.IndexOf(serviceOrderDatailFirstEventNew)

        Dim serviceOrderDetailFirstEventOld = listServiceOrderDetail.Find(Function(x) x.SurgeryNumber = INDTxtEventNumber.EditValue And x.Presentation = 2 And x.IsFirstEvent = True)
        Dim indexOld = listServiceOrderDetail.IndexOf(serviceOrderDetailFirstEventOld)

        serviceOrderDatailFirstEventNew.IsFirstEvent = e.NewValue
        serviceOrderDetailFirstEventOld.IsFirstEvent = False
        RecalculateSurgicalItems()
        _serviceValue = listServiceOrderDetail.Sum(Function(x) x.GrandTotalSalesPrice)
        _sumTaxValues = listServiceOrderDetail.Sum(Function(x) x.TaxValue * x.InvoicedQuantity)
        ctrTmp.PrintInfo()
        Dim rowHandle = INDGvService.FocusedRowHandle
        INDGcService.DataSource = Nothing
        INDGcService.DataSource = listServiceOrderDetail
        INDGvService.FocusedRowHandle = rowHandle
        AsyncLoader(False)
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAdmissionNumber2_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDSleAdmissionNumber2.EditValueChanged
        If INDSleAdmissionNumber2.EditValue IsNot Nothing Then
            'valido que el ingreso no este facturado
            Dim admissionTmp As Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionOpenAndPartialServiceOrder
            Using model As New MAdmissions(MyTag)
                admissionTmp = CType(e.NewObject, Infrastructure.Data.Xpo.CrystalRepository.ViewAdmissionOpenAndPartialServiceOrder) 'model.GetAdmissionsByCodeSimple(INDSleAdmissionNumber2.EditValue.ToString().Trim()).ObjectEmbbeded
                If admissionTmp.Status = "F" Then
                    Mensaje(EeventViewerImages.Advertencia) = "El ingreso esta facturado"
                    INDSleAdmissionNumber2.EditValue = Nothing
                    INDSleAdmissionNumber2.DisplayNullText = String.Empty
                    admission = Nothing
                    CleanControlsAdminssion()
                    INDBtnAddService.Enabled = False
                    Exit Sub
                End If
            End Using

            INDBtnAddService.Enabled = True

            Dim objTemp = Me.viewSearchAdmission.GetFocusedRow
            If objTemp IsNot Nothing Then
                Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                    SetAdmission(obj.OriginalRow)
                End If
            End If
            If admissionTmp IsNot Nothing Then
                INDDteOrderDate.Properties.MinValue = admissionTmp.AdmissionDate
            End If
            ctrTmp.PrintInfo()
        End If
    End Sub

#End Region

#Region "ButtonClick"
    Private Sub INDFpAdmission_ButtonClick(sender As Object, e As DevExpress.Utils.FlyoutPanelButtonClickEventArgs) Handles INDFpAdmission.ButtonClick
    End Sub
#End Region

#Region "MouseEnter"
    Private Sub INDsleAdmissionNumber_MouseEnterAdmission(sender As Object, e As EventArgs) Handles INDSleAdmissionNumber2.MouseEnterAdmission
        If admission IsNot Nothing Then
            INDFpAdmission.ShowBeakForm()
        End If
    End Sub
#End Region

#Region "Mouseleave"
    Private Sub INDsleAdmissionNumber_MouseLeaveAdmission(sender As Object, e As EventArgs) Handles INDSleAdmissionNumber2.MouseLeaveAdmission
        If admission IsNot Nothing Then
        End If
    End Sub
#End Region

#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        BarraBotones.Focus()
        varImp = 1
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
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, serviceOrder.Id, 0, serviceOrder.Id)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        BarraBotones.Focus()
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BillingSequenceDetail IsNot Nothing Then
            If Me._sequense.BillingSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BillingSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                'parámetos de contratos
                _settingsContractXpo = presenter.GetSettingsContractByOperatingUnitId(_idOperativeUnit)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            End If
        End If
    End Sub

    ''' <summary>
    ''' click boton anular barraBotones
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        'Validar si la orden pertenece a RIPSSupportRecord
        If ValidateRIPSSupportRecord() Then
            Exit Sub
        End If

        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            serviceOrder.Status = 3
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barra Botones: ImportarInformación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        OpenFormImport()
    End Sub

    Private Sub BarraBotones_QueryLoadReportsAndDefinitions(ByVal sender As Object, ByVal e As LoadReportsAndDefinitionsEventArgs) Handles BarraBotones.QueryLoadReportsAndDefinitions
        If serviceOrder IsNot Nothing Then
            e.ListReportsAndDefinitions = e.ListReportsAndDefinitions.Where(Function(r) r.CodeEntity = String.Empty OrElse r.CodeEntity = serviceOrder.EntityName).ToList()
        End If
    End Sub

#End Region

#Region "Asyncs"
    Dim errorsDelete As StringBuilder

    Private Sub bg_DoWork(sender As Object, e As DoWorkEventArgs)
        errorsDelete = New StringBuilder
        Dim listEventsTmp = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = serviceOrderDetail.SurgeryNumber And serviceOrderDetail.SurgeryNumber <> 0 And x.Id > 0)

        For Each itemEvent In listEventsTmp
            If itemEvent.IsPackage Then
                e.Cancel = True
                errorsDelete.AppendLine("El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede eliminar porque el servicio " + itemEvent.CodeNameIpsService + " esta empaquetado")
                Exit Sub
            End If
            Using model As New MServiceOrder(MyTag)
                'valido que los items no esten distribuidos
                Dim listDistribution = model.GetServiceOrderDetailDistributionByServideOrderDetailId(itemEvent.Id)
                If listDistribution.Count > 1 Then
                    e.Cancel = True
                    errorsDelete.AppendLine("El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede eliminar porque el servicio " + itemEvent.CodeNameIpsService + " esta distribuido")
                    Exit Sub
                End If
                Dim revenueControlDetail = model.GetRevenueControlDetailByServiceOrderDetailId(itemEvent.Id)
                If revenueControlDetail.Status > 1 Then
                    If revenueControlDetail.Status = 2 Then
                        errorsDelete.AppendLine("El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede eliminar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta facturado")
                    Else
                        errorsDelete.AppendLine("El servicio " + serviceOrderDetail.CodeNameIpsService + " no se puede eliminar porque el folio #" + (revenueControlDetail.FolioOrder).ToString() + " esta bloqueado")
                    End If
                    e.Cancel = True

                    Exit Sub
                End If
            End Using
        Next

        If serviceOrderDetail.IsFirstEvent = True Then
            Dim listValidateEvent = listServiceOrderDetail.FindAll(Function(x) x.SurgeryNumber = serviceOrderDetail.SurgeryNumber)
            If listValidateEvent.Count > 1 Then
                e.Cancel = True
                errorsDelete.AppendLine(ResourceManager.GetString("FirstEvent", MODULE_NAME))
                Exit Sub
            End If
        End If
        If serviceOrderDetail.Id > 0 Then
            If listServiceOrderDetailDelete Is Nothing Then
                listServiceOrderDetailDelete = New List(Of ServiceOrderDetail)
            End If
            Using model As New MServiceOrder(MyTag)
                'valido que el detalle no este facturado
                Dim invoiceDetail = model.GetInvoiceDetailByServiceOrderDetailId(serviceOrderDetail.Id)
                If invoiceDetail IsNot Nothing Then
                    serviceOrderDetail.IsDelete = True
                    listServiceOrderDetailDelete.Add(serviceOrderDetail)
                Else
                    If serviceOrderDetail.Presentation = 2 Then
                        serviceOrderDetail.ServiceOrderDetailSurgical.ToList().ForEach(Sub(x) x.MarkAsDeleted())
                    End If
                    serviceOrderDetail.MarkAsDeleted()
                    listServiceOrderDetailDelete.Add(serviceOrderDetail)
                End If

            End Using

        Else

            Dim listInclude = listServiceOrderDetail.FindAll(Function(x) x.ServiceOrderDetail3 IsNot Nothing AndAlso x.ServiceOrderDetail3.IPSServiceId = serviceOrderDetail.IPSServiceId AndAlso x.ServiceOrderDetail3.CUPSEntityId = serviceOrderDetail.CUPSEntityId)
            If listInclude.Count > 0 Then
                errorsDelete.AppendLine("No se puede eliminar el item porque esta incluido en otros servicios")
                e.Cancel = True
                Exit Sub
            End If
        End If


        listServiceOrderDetail.Remove(serviceOrderDetail)
        RecalculateSurgicalItems()
    End Sub

    Private Sub bg_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        AsyncLoader(False)
        If e.Cancelled Then
            Mensaje(EeventViewerImages.Advertencia) = errorsDelete.ToString()
        Else
            INDGcService.DataSource = listServiceOrderDetail
            INDGcService.RefreshDataSource()
            _serviceValue = listServiceOrderDetail.Sum(Function(x) x.GrandTotalSalesPrice)
            _sumTaxValues = listServiceOrderDetail.Sum(Function(x) x.TaxValue * x.InvoicedQuantity)
            ctrTmp.PrintInfo()
            If listServiceOrderDetail.Count = 0 Then
                INDSleAdmissionNumber2.IsReadOnly = False
            End If
        End If

    End Sub

#End Region

End Class