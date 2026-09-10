'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports DevExpress.Xpo

#End Region

Public Class FrmCupsEntity
    Implements ICupsEntity

#Region "Builder"

    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bwGetRIAS.DoWork, AddressOf bwGetRIAS_DoWork
        AddHandler bwGetRIAS.RunWorkerCompleted, AddressOf bwGetRIAS_RunWorkerCompleted
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id del grupo de facturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillingGroupId As Integer? Implements ICupsEntity.BillingGroupId
        Get
            Return INDsleBillingGroup.EditValue
        End Get
        Set(value As Integer?)
            INDsleBillingGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de grupo facturacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillingGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICupsEntity.BillingGroupXpo
        Get
            Return INDsleBillingGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBillingGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo de servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceGroupId As Integer? Implements ICupsEntity.IPSServiceGroupId
        Get
            Return INDsleIPSServiceGroup.EditValue
        End Get
        Set(value As Integer?)
            INDsleIPSServiceGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de grupo de servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICupsEntity.IPSServiceGroupXpo
        Get
            Return INDsleIPSServiceGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIPSServiceGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si aplica a RIAS
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyRIAS As Boolean Implements ICupsEntity.ApplyRIAS
        Get
            Return INDsleApplyRIAS.EditValue
        End Get
        Set(value As Boolean)
            INDsleApplyRIAS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de servicio ips si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RIASBillingConceptId As Integer? Implements ICupsEntity.RIASBillingConceptId
        Get
            Return INDsleRIASBillingConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleRIASBillingConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de servicio ips si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RIASBillingConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICupsEntity.RIASBillingConceptXpo
        Get
            Return INDsleRIASBillingConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRIASBillingConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo de facturacion si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RIASBillingGroupId As Integer? Implements ICupsEntity.RIASBillingGroupId
        Get
            Return INDsleRIASBillingGroup.EditValue
        End Get
        Set(value As Integer?)
            INDsleRIASBillingGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de grupo facturacion si aplica RIAS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RIASBillingGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICupsEntity.RIASBillingGroupXpo
        Get
            Return INDsleRIASBillingGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRIASBillingGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements ICupsEntity.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICupsEntity.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ICupsEntity.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ICupsEntity.Code
        Get
            Return INDbtnCode.Text
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements ICupsEntity.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de subgrupos cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CupsSubGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICupsEntity.CupsSubGroupXpo
        Get
            Return INDsleCupsSubGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCupsSubGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del subgrupo cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCupsSubGroup As Integer? Implements ICupsEntity.IdCupsSubGroup
        Get
            Return INDsleCupsSubGroup.EditValue
        End Get
        Set(value As Integer?)
            INDsleCupsSubGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RipsCode As String Implements ICupsEntity.RipsCode
        Get
            Return INDtxtRipsCode.Text
        End Get
        Set(value As String)
            INDtxtRipsCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el servico de oxigeno
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OxigenServices As Boolean Implements ICupsEntity.OxigenServices
        Get
            Return INDsleOxigenService.EditValue
        End Get
        Set(value As Boolean)
            INDsleOxigenService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RipsDescription As String Implements ICupsEntity.RipsDescription
        Get
            Return INDtxtRipsDescription.Text
        End Get
        Set(value As String)
            INDtxtRipsDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del concepto rips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RIPSConcept As String Implements ICupsEntity.RIPSConcept
        Get
            Return INDsleRIPSConcept.EditValue
        End Get
        Set(value As String)
            INDsleRIPSConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el servicio RIPS
    ''' </summary>
    ''' <returns></returns>
    Public Property RIPSServicesId As Integer? Implements ICupsEntity.RIPSServicesId
        Get
            Return INDsleRIPSServices.EditValue
        End Get
        Set(value As Integer?)
            INDsleRIPSServices.EditValue = value
        End Set
    End Property

    Public Property RIPSServicesXpo As XPInstantFeedbackSource Implements ICupsEntity.RIPSServicesXpo
        Get
            Return INDsleRIPSServices.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRIPSServices.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece si es un cups Panel
    ''' </summary>
    ''' <returns></returns>
    Public Property IsPanel As Byte? Implements ICupsEntity.IsPanel
        Get
            Return INDGleIsPanel.EditValue
        End Get
        Set(value As Byte?)
            INDGleIsPanel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el informe del procedimiento quirúrico es o no obligatorio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SurgicalReport As Boolean Implements ICupsEntity.SurgicalReport
        Get
            Return INDGleSurgicalReport.EditValue
        End Get
        Set(value As Boolean)
            INDGleSurgicalReport.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que establece si el cup requiere lateralidad
    ''' </summary>
    ''' <returns></returns>
    Public Property RequiresLateraly As Boolean? Implements ICupsEntity.RequiresLateraly
        Get
            Return INDSleRequiresLaterality.EditValue
        End Get
        Set(value As Boolean?)
            INDSleRequiresLaterality.EditValue = value
        End Set
    End Property

    Dim _listServiceType As List(Of Tuple(Of Integer, String))
    ReadOnly Property ListServiceType As List(Of Tuple(Of Integer, String))
        Get
            If _listServiceType Is Nothing Then
                _listServiceType = New List(Of Tuple(Of Integer, String))
                _listServiceType.Add(New Tuple(Of Integer, String)(7, "Ninguno"))
                _listServiceType.Add(New Tuple(Of Integer, String)(1, "Laboratorios"))
                _listServiceType.Add(New Tuple(Of Integer, String)(2, "Patologias"))
                _listServiceType.Add(New Tuple(Of Integer, String)(3, "Imagenes Diagnosticas"))
                _listServiceType.Add(New Tuple(Of Integer, String)(4, "Procedimientos no Qx"))
                _listServiceType.Add(New Tuple(Of Integer, String)(5, "Procedimientos Qx"))
                _listServiceType.Add(New Tuple(Of Integer, String)(6, "Interconsultas"))
                _listServiceType.Add(New Tuple(Of Integer, String)(8, "Consulta Externa"))
                _listServiceType.Add(New Tuple(Of Integer, String)(9, "Hemocomponentes"))
            End If
            Return _listServiceType
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el datasource del combo que lista los CUPS
    ''' </summary>
    ''' <returns></returns>
    Public Property CupsEntityXPO As List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo) Implements ICupsEntity.CupsEntityXPO
        Get
            Return TryCast(INDSleCupsEntity.Properties.DataSource, List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo))
        End Get
        Set(value As List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo))
            INDSleCupsEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Cantidad de items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedCups As List(Of Infrastructure.Data.Xpo.ContractRepository.CupsEntityXpo)
        Get
            Dim items = CupsEntityXPO.Where(Function(m) m.SelectOption).ToList()
            Return items
        End Get
    End Property

    ''' <summary>
    ''' Cantidad de items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedCount As Integer
        Get
            Dim count = SelectedCups.Count
            Return count
        End Get
    End Property

    ''' <summary>
    ''' Propieda que postula la lista de la rejilla para el detalle del panel
    ''' </summary>
    ''' <returns></returns>
    Private Property ListCupsPanelDetail As List(Of CUPSEntityPanelDetail)
        Get
            Return _listCupsPanelDetail
        End Get
        Set(value As List(Of CUPSEntityPanelDetail))
            _listCupsPanelDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el servicio que se muestra en la sección de ordenes médicas
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowServiceMedicalOrder As Integer? Implements ICupsEntity.ShowServiceMedicalOrder
        Get
            Return INDsleShowServiceMedicalOrder.EditValue
        End Get
        Set(value As Integer?)
            INDsleShowServiceMedicalOrder.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece si se maneja Servicio de apoyo imagenológico a procedimientos
    ''' </summary>
    ''' <returns></returns>
    Public Property ImageGuidanceProcedures As Boolean? Implements ICupsEntity.ImageGuidanceProcedures
        Get
            Return INDSleImageGuidanceProcedures.EditValue
        End Get
        Set(value As Boolean?)
            INDSleImageGuidanceProcedures.EditValue = value
        End Set
    End Property
#End Region

#Region "BackgroundWorker"

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwGetRIAS_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        Dim ListXpo = Presenter.GetRIASCUPS(Code)
        If ListXpo IsNot Nothing AndAlso ListXpo.Count > 0 Then
            If ListCupsEntityRIAS Is Nothing Then
                ListCupsEntityRIAS = New List(Of CupsEntityRIAS)
            End If
            For Each itemXpo In ListXpo
                Dim entity As New CupsEntityRIAS
                With entity
                    .Id = itemXpo.ID
                    .CupsCode = itemXpo.CODSERIPS
                    .RiasId = itemXpo.IDRIAS.ID
                    .RiasDescription = itemXpo.IDRIAS.CodeName
                    .ConceptRIPS = itemXpo.CONCEPTORIPSRIAS
                    .IsDelete = 0

                    For Each itemRules In itemXpo.RIASCUPSDXpo
                        Dim entityRules As New CupsEntityRIASDetail
                        entityRules.Id = itemRules.ID
                        entityRules.CupsEntityRIASId = itemRules.IDRIASCUPS.ID
                        entityRules.Rule = itemRules.REGLA
                        entityRules.MinimunAge = itemRules.EDADMINIMA
                        entityRules.MaximunAge = itemRules.EDADMAXIMA
                        entityRules.Unit = itemRules.UNIDADRANGO
                        entityRules.Frequency = itemRules.FRECUENCIA
                        entityRules.FrequencyUnit = itemRules.UNIDADFRECUENCIA
                        entityRules.RequireMedicalOrder = itemRules.REQUIEREORDENMED
                        entityRules.PeriodQuantity = itemRules.CANTIDADPERIODO
                        entityRules.Sex = itemRules.SEXO
                        entityRules.Status = itemRules.ESTADO
                        entityRules.IsDelete = 0
                        .ListCupsEntityRIASDetail.Add(entityRules)
                    Next

                End With
                ListCupsEntityRIAS.Add(entity)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwGetRIAS_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        INDgcRIAS.DataSource = Nothing
        INDgcRIAS.DataSource = ListCupsEntityRIAS
        INDviewRIAS.HideLoadingPanel()
        If ListCupsEntityRIAS IsNot Nothing AndAlso ListCupsEntityRIAS.Count > 0 Then
            INDsleApplyRIAS.Properties.ReadOnly = True
        End If
    End Sub

    Public Overrides Sub AsyncLoader(State As Boolean) Implements ICupsEntity.AsyncLoader
        MyBase.AsyncLoader(State)
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Asyncrono para obtener las RIAS en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private bwGetRIAS As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim CupsEntityRIAS As CupsEntityRIAS

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim CUPSEntityContractDescriptions As CUPSEntityContractDescriptions

    ''' <summary>
    ''' Listado de descripciones
    ''' </summary>
    Public ListCUPSEntityContractDescriptions As List(Of CUPSEntityContractDescriptions)

    ''' <summary>
    ''' Listado de eliminados de descripciones
    ''' </summary>
    Public ListDeleteCUPSEntityContractDescriptions As List(Of CUPSEntityContractDescriptions)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PCupsEntity

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Representa la entidad de grupo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim cupsEntity As CUPSEntity

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordContract

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRIPSConcept As New List(Of Tuple(Of String, String))

    ''' <summary>
    ''' Lista las unidades
    ''' </summary>
    Dim ListAge As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de sexos
    ''' </summary>
    Dim ListSex As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de Oxigen
    ''' </summary>
    Dim ListOxigen As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListShowServiceMedicalOrder As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListShowDashboardOf As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListShowDashboardOfAmbulatory As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListYesNo As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Listado de RIAS
    ''' </summary>
    Dim ListCupsEntityRIAS As List(Of CupsEntityRIAS)

    ''' <summary>
    ''' Listado de RIAS
    ''' </summary>
    Dim ListDeleteCupsEntityRIAS As List(Of CupsEntityRIAS)

    ''' <summary>
    ''' listado de las opciones combo panel
    ''' </summary>
    Private _listIsPanel As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Lista de los detalle del panel
    ''' </summary>
    Private _listCupsPanelDetail As List(Of CUPSEntityPanelDetail)

    ''' <summary>
    ''' Lista informe quirúrgico obligatorio
    ''' </summary>
    Dim ListSurgicalReport As List(Of Tuple(Of Boolean, String))
#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If cupsEntity IsNot Nothing AndAlso cupsEntity.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MCupsEntity(Me.Tag.ToString())
                    AssigningValues()
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteCupsEntity(cupsEntity)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        SearchMode = False
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        ElseIf result.MessageResult(0) = "-001" Then
                            Mensaje(EeventViewerImages.MensajeError) = "El registro no se puede eliminar porque tiene relación con Servicio IPS."
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If

        'Se valida el rango de fechas
        Dim validate = ValidateAge()
        If validate.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = validate
            Exit Sub
        End If

        'Si el cups aplica a RIAS se valida que hayan diligenciado una
        If ApplyRIAS Then
            If ListCupsEntityRIAS Is Nothing OrElse ListCupsEntityRIAS.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar una RIAS porque esta seleccionado que aplica"
                Exit Sub
            End If
        End If

        AssigningValues()
        Using model As New MCupsEntity(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveCupsEntity(cupsEntity, 0)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If cupsEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                ElseIf cupsEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.cupsEntity = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                SearchMode = False
                Me.Deshacer()
            Else
                If Result.Message = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        Await ValidateCode()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = ResourceManager.GetString("Code", "ContractTitles"), .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo() With {.Caption = ResourceManager.GetString("Description", "ContractTitles"), .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo() With {.Caption = ResourceManager.GetString("RIPSCode", "ContractTitles"), .FieldName = "RIPSCode", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo() With {.Caption = ResourceManager.GetString("RIPSDescription", "ContractTitles"), .FieldName = "RIPSDescription", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                               New ColumnInfo() With {.Caption = ResourceManager.GetString("CUPSSubGroup", "ContractTitles"), .FieldName = "CUPSSubGroupId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCupsEntity
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Método que muestra u oculta el grupo de descripciones
    ''' </summary>
    Private Sub SetVisibleGroupDescriptions()
        Dim visibleGroup As Boolean = False

        Dim settingsContractXpo = Presenter.GetSettingsContractByOperatingUnitId(Me.BarraBotones.OperatingUnitValue)
        If settingsContractXpo IsNot Nothing Then
            visibleGroup = settingsContractXpo.CUPSWithRelatedDescription
        End If

        INDgcDescriptions.SafeInvoke(Sub()
                                         INDlygDescriptions.Visibility = If(visibleGroup = True, DevExpress.XtraLayout.Utils.LayoutVisibility.Always, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                                     End Sub)
    End Sub

    ''' <summary>
    ''' Método que carga las descripciones asociadas al cups
    ''' </summary>
    Private Sub LoadDescriptions()
        CheckForIllegalCrossThreadCalls = False

        Try
            Dim listXpo = Presenter.ListDescriptionsByCupsId(cupsEntity.Id)

            If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                ListCUPSEntityContractDescriptions = New List(Of CUPSEntityContractDescriptions)
                ListDeleteCUPSEntityContractDescriptions = New List(Of CUPSEntityContractDescriptions)

                For Each itemXpo In listXpo
                    Dim entity As New CUPSEntityContractDescriptions
                    With entity
                        .Id = itemXpo.Id
                        .CUPSEntityId = itemXpo.CUPSEntityId.Id
                        .ContractDescriptionId = itemXpo.ContractDescriptionId.Id
                        .DescriptionCodeName = itemXpo.ContractDescriptionId.CodeName
                        .CupsSubgroupId = itemXpo.CupsSubgroupId.Id
                        .CupsSubgroupCodeName = itemXpo.CupsSubgroupId.CodeName
                        .BillingGroupId = itemXpo.BillingGroupId.Id
                        .BillingGroupCodeName = itemXpo.BillingGroupId.CodeName
                        .BillingConceptId = itemXpo.BillingConceptId.Id
                        .BillingConceptCodeName = itemXpo.BillingConceptId.CodeName
                        .IsDelete = itemXpo.IsDelete
                    End With
                    If entity.IsDelete = 0 Then
                        ListCUPSEntityContractDescriptions.Add(entity)
                    Else
                        ListDeleteCUPSEntityContractDescriptions.Add(entity)
                    End If
                Next
            End If

            INDviewDescriptions.HideLoadingPanel()
            INDgcDescriptions.DataSource = Nothing
            INDgcDescriptions.DataSource = ListCUPSEntityContractDescriptions
            INDgcDescriptions.RefreshDataSource()
        Catch ex As Exception
            ListCUPSEntityContractDescriptions = Nothing
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
            INDviewDescriptions.HideLoadingPanel()
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se ejcuta despues de agregar una descripción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddDescriptionEventArgs(sender As Object, e As AddDescriptionEventArgs)
        If e IsNot Nothing Then

            If e.EditMode = False Then
                If ListCUPSEntityContractDescriptions Is Nothing Then
                    ListCUPSEntityContractDescriptions = New List(Of CUPSEntityContractDescriptions)
                End If
                ListCUPSEntityContractDescriptions.Add(e.CUPSEntityContractDescriptions)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else
                ListCUPSEntityContractDescriptions.Remove(CUPSEntityContractDescriptions)
                ListCUPSEntityContractDescriptions.Insert(IndexEditRecord, e.CUPSEntityContractDescriptions)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDgcDescriptions.DataSource = Nothing
            INDgcDescriptions.DataSource = ListCUPSEntityContractDescriptions
        End If
    End Sub

    ''' <summary>
    ''' Edita una descripción
    ''' </summary>
    Private Sub EditDescription()
        CUPSEntityContractDescriptions = DirectCast(INDviewDescriptions.GetFocusedRow(), CUPSEntityContractDescriptions)
        IndexEditRecord = ListCUPSEntityContractDescriptions.IndexOf(CUPSEntityContractDescriptions)
        OpenFormDescriptions(True)
    End Sub

    ''' <summary>
    ''' Elimina una descripción
    ''' </summary>
    Private Async Sub DeleteDescription()
        Dim entityDelete = DirectCast(INDviewDescriptions.GetFocusedRow(), CUPSEntityContractDescriptions)

        If entityDelete.Id > 0 Then

            AsyncLoader(True)
            Dim result As ActionResult(Of SP_ValidateDescriptionsInCrystal_Result) = Nothing
            Using model As New MCupsEntity(Tag)
                result = Await model.SP_ValidateDescriptionsInCrystal(entityDelete.Id)
                If result.StateResult = False OrElse (result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.CodeValidation <> 0) Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Exit Sub
                End If
            End Using
            AsyncLoader(False)

            If ListDeleteCUPSEntityContractDescriptions Is Nothing Then
                ListDeleteCUPSEntityContractDescriptions = New List(Of CUPSEntityContractDescriptions)
            End If

            entityDelete.IsDelete = 1

            'If result.ObjectEmbbeded.CodeValidation = 0 Then
            '    entityDelete.IsDelete = 0
            '    entityDelete.MarkAsDeleted()
            'Else
            '    entityDelete.IsDelete = 1
            'End If

            ListDeleteCUPSEntityContractDescriptions.Add(entityDelete)
        End If

        ListCUPSEntityContractDescriptions.Remove(entityDelete)
        INDgcDescriptions.DataSource = Nothing
        INDgcDescriptions.DataSource = ListCUPSEntityContractDescriptions
        Mensaje(EeventViewerImages.Informacion) = "Regla eliminada de la rejilla correctamente"
    End Sub

    ''' <summary>
    ''' Método que abre el form de descripciones
    ''' </summary>
    Private Sub OpenFormDescriptions(EditMode As Boolean)
        Using formulario As New FrmAddDescriptions()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddDescriptionArgs, AddressOf ReturnAddDescriptionEventArgs
            formulario.EditMode = EditMode
            formulario.CUPSEntityContractDescriptions = CUPSEntityContractDescriptions
            formulario.ToolBar.Visible = False
            formulario.FrmCupsEntity = Me
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Elimina un RIAS
    ''' </summary>
    Private Sub DeleteRIAS()
        Dim entityDelete = DirectCast(INDviewRIAS.GetFocusedRow(), CupsEntityRIAS)
        If entityDelete.Id > 0 Then
            If ListDeleteCupsEntityRIAS Is Nothing Then
                ListDeleteCupsEntityRIAS = New List(Of CupsEntityRIAS)
            End If
            entityDelete.IsDelete = True
            entityDelete.ListCupsEntityRIASDetail.ForEach(Sub(item) item.IsDelete = True)
            ListDeleteCupsEntityRIAS.Add(entityDelete)
        End If
        ListCupsEntityRIAS.Remove(entityDelete)
        INDgcRIAS.DataSource = Nothing
        INDgcRIAS.DataSource = (From x In ListCupsEntityRIAS Where x.IsDelete = False)
        Mensaje(EeventViewerImages.Informacion) = "Regla eliminada de la rejilla correctamente"
        If ListCupsEntityRIAS.Count = 0 Then
            INDsleApplyRIAS.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Edita un RIAS
    ''' </summary>
    Private Sub EditRIAS()
        CupsEntityRIAS = DirectCast(INDviewRIAS.GetFocusedRow(), CupsEntityRIAS)
        IndexEditRecord = ListCupsEntityRIAS.IndexOf(CupsEntityRIAS)
        OpenFormRIAS(True)
    End Sub

    ''' <summary>
    ''' Metodo que abre el from para agregar los RIAS
    ''' </summary>
    Private Sub OpenFormRIAS(EditMode As Boolean)
        Using formulario As New FrmAddRias()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddRIASArgs, AddressOf ReturnAddEventArgs
            formulario.EditModeRIAS = EditMode
            If EditMode Then
                formulario.ListRIASCompare = (From x In ListCupsEntityRIAS Where x.RiasId <> CupsEntityRIAS.RiasId).ToList()
            Else
                formulario.ListRIASCompare = ListCupsEntityRIAS
            End If
            formulario.CupsEntityRIAS = CupsEntityRIAS
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(1200, 600)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddCupsEntityRIAS)
        If e IsNot Nothing Then

            If e.EditMode = False Then 'Si se esta insertando
                If ListCupsEntityRIAS Is Nothing Then
                    ListCupsEntityRIAS = New List(Of CupsEntityRIAS)
                End If
                ListCupsEntityRIAS.Add(e.CupsEntityRIAS)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else 'Si se esta actualizando
                ListCupsEntityRIAS.Remove(CupsEntityRIAS)
                ListCupsEntityRIAS.Insert(IndexEditRecord, e.CupsEntityRIAS)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDgcRIAS.DataSource = Nothing
            INDgcRIAS.DataSource = (From x In ListCupsEntityRIAS Where x.IsDelete = False)
            INDsleApplyRIAS.Properties.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del search que maneja tupla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListRIPSConcept = New List(Of Tuple(Of String, String))
        ListRIPSConcept.Add(New Tuple(Of String, String)("01", "Consultas (AC)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("02", "Procedimientos de diagnósticos (AP)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("03", "Procedimientos terapéuticos no quirúrgicos (AP)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("04", "Procedimientos terapéuticos quirúrgicos (AP)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("05", "Procedimientos de promoción y prevención (AP)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("06", "Estancias (AT)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("07", "Honorarios (AT)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("08", "Derechos de sala (AT)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("09", "Materiales e insumos (AT)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("10", "Banco de sangre (AP)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("11", "Prótesis y órtesis (AT)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("12", "Medicamentos POS (AM)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("13", "Medicamentos NO POS (AM)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("14", "Traslado de pacientes (AT)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("15", "Servicios Complementarios (AT)"))
        INDsleRIPSConcept.Properties.DataSource = ListRIPSConcept.ToList

        ListAge = New List(Of Tuple(Of Integer, String))
        ListAge.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Years", NAME_MODULE)))
        ListAge.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Months", NAME_MODULE)))
        ListAge.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("Days", NAME_MODULE)))
        INDsleMinimunAgeUnit.Properties.DataSource = ListAge.ToList()
        INDsleMaximumAgeUnit.Properties.DataSource = ListAge.ToList()

        ListSex = New List(Of Tuple(Of Integer, String))
        ListSex.Add(New Tuple(Of Integer, String)(0, "Masculino"))
        ListSex.Add(New Tuple(Of Integer, String)(1, "Femenino"))
        ListSex.Add(New Tuple(Of Integer, String)(2, "Ambos"))
        INDsleSex.Properties.DataSource = ListSex.ToList()

        ListOxigen = New List(Of Tuple(Of Integer, String))
        ListOxigen.Add(New Tuple(Of Integer, String)(1, "Si"))
        ListOxigen.Add(New Tuple(Of Integer, String)(0, "No"))
        INDsleOxigenService.Properties.DataSource = ListOxigen.ToList()

        ListShowServiceMedicalOrder = New List(Of Tuple(Of Integer, String))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(7, "Ninguno"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(1, "Laboratorios"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(2, "Patologías"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(3, "Imágenes Diagnósticas"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(4, "Procedimientos no Qx"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(5, "Procedimientos Qx"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(6, "Interconsultas"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(8, "Consulta Externa"))
        ListShowServiceMedicalOrder.Add(New Tuple(Of Integer, String)(9, "Hemocomponentes"))
        INDsleShowServiceMedicalOrder.Properties.DataSource = ListShowServiceMedicalOrder.ToList()

        ListYesNo = New List(Of Tuple(Of Boolean, String))
        ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))

        Me._listIsPanel = New List(Of Tuple(Of Byte, String))
        _listIsPanel.Add(New Tuple(Of Byte, String)(1, "Si"))
        _listIsPanel.Add(New Tuple(Of Byte, String)(0, "No"))
        INDGleIsPanel.Properties.DataSource = _listIsPanel.ToList()

        INDsleTherapyProcedure.Properties.DataSource = ListYesNo.ToList()
        INDsleAllowDiligenceInPlace.Properties.DataSource = ListYesNo.ToList()
        INDsleAllowDiligenceReportRealizationQx.Properties.DataSource = ListYesNo.ToList()
        INDsleSerialService.Properties.DataSource = ListYesNo.ToList()
        INDsleRequiresInterpretation.Properties.DataSource = ListYesNo.ToList()
        INDsleRequiresConfirmationRealization.Properties.DataSource = ListYesNo.ToList()
        INDsleApplyRIAS.Properties.DataSource = ListYesNo.ToList()
        INDsleFinancedResourceUPC.Properties.DataSource = ListYesNo.ToList()
        INDSleRequestRoomAutomatically.Properties.DataSource = ListYesNo.ToList()
        INDSleRequiresLaterality.Properties.DataSource = ListYesNo.ToList()
        INDSleImageGuidanceProcedures.Properties.DataSource = ListYesNo.ToList()

        ListSurgicalReport = New List(Of Tuple(Of Boolean, String))
        ListSurgicalReport.Add(New Tuple(Of Boolean, String)(True, "Obligatorio"))
        ListSurgicalReport.Add(New Tuple(Of Boolean, String)(False, "No obligatorio"))
        INDGleSurgicalReport.Properties.DataSource = ListSurgicalReport

        SetListShowDashboardOf()
    End Sub

    ''' <summary>
    ''' Datasource del control listar este servicio en el dashboard de
    ''' </summary>
    Private Sub SetListShowDashboardOf()
        ListShowDashboardOf = New List(Of Tuple(Of Integer, String))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(8, "Ninguno"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(1, "Laboratorios"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(2, "Patologías"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(3, "Imágenes Diagnósticas"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(4, "Consulta Externa"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(5, "Quimioterapias"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(6, "Radioterapias"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(7, "Terapia de reemplazo renal")) 'Diálisis 7
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(9, "Procedimientos No Qx - Procedimientos Invasivos"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(10, "Procedimientos Qx - Procedimientos Invasivos"))
        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(11, "Interconsultas"))

        If ShowServiceMedicalOrder IsNot Nothing AndAlso (ShowServiceMedicalOrder = 4 OrElse ShowServiceMedicalOrder = 5) Then
            ListShowDashboardOf.Add(New Tuple(Of Integer, String)(12, "Procedimientos Invasivos u Otros Procedimientos"))
        End If

        ListShowDashboardOf.Add(New Tuple(Of Integer, String)(13, "Braquiterapia"))
        INDsleShowDashboardOf.Properties.DataSource = ListShowDashboardOf.ToList()
        INDsleShowDashboardOfAmbulatory.Properties.DataSource = ListShowDashboardOf.ToList()
    End Sub

    ''' <summary>
    ''' metodo para validar las edades minimas y maximas convirtiendolas a dias
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateAge() As String
        Dim errors As New StringBuilder
        Dim minimunAgeTmp As Integer = 0
        Dim maximunAgeTmp As Integer = 0
        Select Case INDsleMinimunAgeUnit.EditValue
            Case 1
                minimunAgeTmp = INDseMinimunAge.EditValue * 360
            Case 2
                minimunAgeTmp = INDseMinimunAge.EditValue * 30
            Case 3
                minimunAgeTmp = INDseMinimunAge.EditValue
        End Select
        Select Case INDsleMaximumAgeUnit.EditValue
            Case 1
                maximunAgeTmp = INDseMaximumAge.EditValue * 360
            Case 2
                maximunAgeTmp = INDseMaximumAge.EditValue * 30
            Case 3
                maximunAgeTmp = INDseMaximumAge.EditValue
        End Select
        If minimunAgeTmp >= maximunAgeTmp Then
            errors.AppendLine(ResourceManager.GetString("Age", NAME_MODULE))
        End If
        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Valida el codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ValidateCode() As Task
        If INDbtnCode.Text = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty", NAME_MODULE)
            INDbtnCode.Focus()
        Else
            Await Me.LoadControls()
        End If
    End Function

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICupsEntity.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleCupsSubGroup.Enabled = value
            INDtxtRipsCode.Enabled = value
            INDtxtRipsDescription.Enabled = value
            INDsleRIPSConcept.Enabled = value
            INDsleIPSServiceGroup.Enabled = value
            INDsleRIPSServices.Enabled = value
            INDsleBillingGroup.Enabled = value
            INDGleServiceType.Enabled = value
            INDsleApplyRIAS.Enabled = value
            INDsleRIASBillingConcept.Enabled = value
            INDsleRIASBillingGroup.Enabled = value

            INDsleMinimunAgeUnit.Enabled = value
            INDseMinimunAge.Enabled = value
            INDsleMaximumAgeUnit.Enabled = value
            INDseMaximumAge.Enabled = value

            INDsleSex.Enabled = value
            INDsleOxigenService.Enabled = value

            INDccbeType4505.Enabled = value
            INDsleShowServiceMedicalOrder.Enabled = value
            INDsleShowDashboardOf.Enabled = value
            INDsleShowDashboardOfAmbulatory.Enabled = value
            INDsleFinancedResourceUPC.Enabled = value
            INDsleRequiresInterpretation.Enabled = value
            INDSleImageGuidanceProcedures.Enabled = value

            INDbtnAddDescription.Enabled = value
            INDgcDescriptions.Enabled = value
            INDGleIsPanel.Enabled = value

            If value Then
                INDtxtDescription.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.cupsEntity IsNot Nothing AndAlso Me.cupsEntity.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsEntity.Code, Me.cupsEntity.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.cupsEntity.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsEntity.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsEntity.Code, Me.cupsEntity.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsEntity.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyCupsEntity.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        Description = String.Empty
        IdCupsSubGroup = Nothing
        INDsleCupsSubGroup.Properties.NullText = String.Empty
        RipsCode = String.Empty
        RipsDescription = String.Empty
        RIPSConcept = Nothing
        INDsleRIPSServices.EditValue = Nothing
        INDsleRIPSServices.Properties.NullText = String.Empty
        RIPSServicesId = Nothing
        IPSServiceGroupId = Nothing
        INDsleIPSServiceGroup.Properties.NullText = String.Empty
        BillingGroupId = Nothing
        INDsleBillingGroup.Properties.NullText = String.Empty
        INDGleServiceType.EditValue = Nothing
        INDGleSurgicalReport.EditValue = Nothing
        INDsleApplyRIAS.Properties.ReadOnly = False
        ApplyRIAS = Nothing
        RIASBillingConceptId = Nothing
        INDsleRIASBillingConcept.Properties.NullText = String.Empty
        RIASBillingGroupId = Nothing
        INDsleRIASBillingGroup.Properties.NullText = String.Empty
        Status = True

        Me.IsPanel = 0
        Me.ListCupsPanelDetail = Nothing
        Me.INDGcPanelCups.DataSource = Nothing
        Me.CupsEntityXPO = Nothing

        BarraBotones.CleanAuditBasic()
        INDlygHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDgcDescriptions.DataSource = Nothing
        ListCUPSEntityContractDescriptions = Nothing
        ListDeleteCUPSEntityContractDescriptions = Nothing

        INDlyCupsEntity.EndUpdate()

        cupsEntity = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing

        INDsleMinimunAgeUnit.EditValue = Nothing
        INDseMinimunAge.EditValue = Nothing
        INDsleMaximumAgeUnit.EditValue = Nothing
        INDseMaximumAge.EditValue = Nothing
        INDsleSex.EditValue = Nothing
        INDsleOxigenService.EditValue = Nothing
        If INDccbeType4505.Properties.Items.GetCheckedValues().Count > 0 Then
            For Each i In INDccbeType4505.Properties.Items.GetCheckedValues()
                INDccbeType4505.Properties.Items(i).CheckState = System.Windows.Forms.CheckState.Unchecked
            Next
        End If
        INDsleShowDashboardOf.EditValue = Nothing
        INDsleShowDashboardOfAmbulatory.EditValue = Nothing
        INDsleTherapyProcedure.EditValue = Nothing
        INDsleAllowDiligenceInPlace.EditValue = Nothing
        INDsleAllowDiligenceReportRealizationQx.EditValue = Nothing
        INDsleSerialService.EditValue = Nothing
        INDsleRequiresInterpretation.EditValue = Nothing
        INDsleRequiresConfirmationRealization.EditValue = Nothing

        INDlyItemTherapyProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAllowDiligenceInPlace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAllowDiligenceReportRealizationQx.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSerialService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRequiresConfirmationRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciRequiresLaterality.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ShowServiceMedicalOrder = 7
        INDsleShowDashboardOf.EditValue = 8
        INDsleShowDashboardOfAmbulatory.EditValue = 8
        INDsleFinancedResourceUPC.EditValue = Nothing
        ImageGuidanceProcedures = Nothing

        INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ListCupsEntityRIAS = Nothing
        ListDeleteCupsEntityRIAS = Nothing
        INDgcRIAS.DataSource = Nothing
        CupsEntityRIAS = Nothing
        INDGleSurgicalReport.EditValue = Nothing
        INDlyItemRequiresInterpretation.HideControl()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With cupsEntity
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .CUPSSubGroupId = IdCupsSubGroup
            .RIPSCode = RipsCode
            '.OxigenService = OxigenServices
            .RIPSDescription = RipsDescription
            .RIPSConcept = RIPSConcept
            .RIPSServiceId = RIPSServicesId
            .BillingConceptId = IPSServiceGroupId
            .BillingGroupId = BillingGroupId
            .ServiceType = INDGleServiceType.EditValue
            .ApplyRIAS = ApplyRIAS
            .IsPanel = Me.IsPanel
            .SurgicalReport = SurgicalReport


            If .ApplyRIAS Then
                .RIASBillingConceptId = RIASBillingConceptId
                .RIASBillingGroupId = RIASBillingGroupId
            End If

            If INDsleMinimunAgeUnit.EditValue IsNot Nothing Then
                .MinimunAgeUnit = INDsleMinimunAgeUnit.EditValue
            Else
                .MinimunAgeUnit = 0
            End If
            If INDseMinimunAge.EditValue IsNot Nothing Then
                .MinimunAge = INDseMinimunAge.EditValue
            Else
                .MinimunAge = 0
            End If
            If INDsleMaximumAgeUnit.EditValue IsNot Nothing Then
                .MaximumAgeUnit = INDsleMaximumAgeUnit.EditValue
            Else
                .MaximumAgeUnit = 0
            End If
            If INDseMaximumAge.EditValue IsNot Nothing Then
                .MaximumAge = INDseMaximumAge.EditValue
            Else
                .MaximumAge = 0
            End If
            If INDsleSex.EditValue IsNot Nothing Then
                .Sex = INDsleSex.EditValue
            Else
                .Sex = 0
            End If
            If INDsleOxigenService.EditValue IsNot Nothing Then
                .OxigenService = INDsleOxigenService.EditValue
            Else
                .OxigenService = 0
            End If

            .ShowServiceMedicalOrder = CInt(ShowServiceMedicalOrder)
            .ShowDashboardOf = CInt(INDsleShowDashboardOf.EditValue)
            .ShowDashboardOfAmbulatory = CInt(INDsleShowDashboardOfAmbulatory.EditValue)
            .FinancedResourceUPC = INDsleFinancedResourceUPC.EditValue

            If INDlyItemTherapyProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .TherapyProcedure = INDsleTherapyProcedure.EditValue
            Else
                .TherapyProcedure = Nothing
            End If
            If INDlyItemAllowDiligenceInPlace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AllowDiligenceInPlace = INDsleAllowDiligenceInPlace.EditValue
            Else
                .AllowDiligenceInPlace = Nothing
            End If
            If INDlyItemAllowDiligenceReportRealizationQx.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AllowDiligenceReportRealizationQx = INDsleAllowDiligenceReportRealizationQx.EditValue
            Else
                .AllowDiligenceReportRealizationQx = Nothing
            End If
            If INDlyItemSerialService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SerialService = INDsleSerialService.EditValue
            Else
                .SerialService = Nothing
            End If

            If INDlyItemRequiresInterpretation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RequiresInterpretation = INDsleRequiresInterpretation.EditValue
            Else
                .RequiresInterpretation = Nothing
            End If

            If INDlyItemRequiresConfirmationRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RequiresConfirmationRealization = INDsleRequiresConfirmationRealization.EditValue
            Else
                .RequiresConfirmationRealization = Nothing
            End If

            If INDLciRequestRoomAutomatically.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RequestRoomAutomatically = INDSleRequestRoomAutomatically.EditValue
            Else
                .RequestRoomAutomatically = False
            End If

            If INDLciRequiresLaterality.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RequiresLaterality = RequiresLateraly
            Else
                .RequiresLaterality = Nothing
            End If

            If INDLciImageGuidanceProcedures.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ImageGuidanceProcedure = ImageGuidanceProcedures
            Else
                .ImageGuidanceProcedure = 0
            End If


            Dim itemsCheckedList = INDccbeType4505.Properties.Items
            For Each itemChecked As DevExpress.XtraEditors.Controls.CheckedListBoxItem In itemsCheckedList
                Dim field = cupsEntity.GetType().GetProperties().Where(Function(y) y.Name.Equals(itemChecked.Value)).FirstOrDefault()
                If field IsNot Nothing Then
                    If itemChecked.CheckState = System.Windows.Forms.CheckState.Checked Then
                        field.SetValue(cupsEntity, True)
                    Else
                        field.SetValue(cupsEntity, False)
                    End If
                End If
            Next

            If ListCupsPanelDetail IsNot Nothing AndAlso ListCupsPanelDetail.Any() Then
                ListCupsPanelDetail.ForEach(Sub(item) .CUPSEntityPanelDetail.Add(item))
            End If

            If ListCupsEntityRIAS IsNot Nothing AndAlso ListCupsEntityRIAS.Count > 0 Then
                ListCupsEntityRIAS.ForEach(Sub(item) .ListCupsEntityRIAS.Add(item))
            End If

            If ListDeleteCupsEntityRIAS IsNot Nothing AndAlso ListDeleteCupsEntityRIAS.Count > 0 Then
                ListDeleteCupsEntityRIAS.ForEach(Sub(item) .ListCupsEntityRIAS.Add(item))
            End If

            If ListCUPSEntityContractDescriptions IsNot Nothing AndAlso ListCUPSEntityContractDescriptions.Count > 0 Then
                ListCUPSEntityContractDescriptions.ForEach(Sub(item) .CUPSEntityContractDescriptions.Add(item))
            End If

            If ListDeleteCUPSEntityContractDescriptions IsNot Nothing AndAlso ListDeleteCUPSEntityContractDescriptions.Count > 0 Then
                ListDeleteCUPSEntityContractDescriptions.ForEach(Sub(item) .CUPSEntityContractDescriptions.Add(item))
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Try
            Me.BarraBotones.StatusRecordVisible = True
            Using Model As New MCupsEntity(CStr(Me.Tag))
                AsyncLoader(True)
                Dim resultOperation = Await Model.GetCupsEntity(INDbtnCode.Text.Trim)
                AsyncLoader(False)
                cupsEntity = resultOperation.ObjectEmbbeded
                If Not cupsEntity Is Nothing Then
                    If cupsEntity.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(cupsEntity.Id))
                            With cupsEntity
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code

                                If .ApplyRIAS Then 'Si aplica Rias se envia a ejecutar la consulta
                                    INDviewRIAS.ShowLoadingPanel()
                                    bwGetRIAS.RunWorkerAsync()
                                End If

                                Description = .Description
                                IdCupsSubGroup = .CUPSSubGroupId
                                INDsleCupsSubGroup.Properties.NullText = .CupsSubGroupDescription
                                RipsCode = .RIPSCode
                                'OxigenServices = .OxigenService

                                RipsDescription = .RIPSDescription
                                RIPSConcept = .RIPSConcept
                                RIPSServicesId = .RIPSServiceId
                                IPSServiceGroupId = .BillingConceptId
                                INDsleIPSServiceGroup.Properties.NullText = .IPSServiceGroupDescription
                                BillingGroupId = .BillingGroupId
                                INDsleBillingGroup.Properties.NullText = .BillingGroupDescription
                                INDGleServiceType.EditValue = .ServiceType
                                ApplyRIAS = .ApplyRIAS
                                RIASBillingConceptId = .RIASBillingConceptId
                                INDsleRIASBillingConcept.Properties.NullText = .RIASBillingConceptCodeName
                                RIASBillingGroupId = .RIASBillingGroupId
                                INDsleRIASBillingGroup.Properties.NullText = .RIASBillingGroupCodeName
                                Status = .Status
                                SurgicalReport = .SurgicalReport
                                IsPanel = If(.IsPanel Is Nothing, Convert.ToByte(0), .IsPanel)
                                'Asignamos cuando el tipo de servicio es Imagenes Diagnosticos/Procedimientos Qx/Procedimientos No Qx/ Patologías
                                Dim ValidatinLaterality = {2, 3, 4, 5}
                                RequiresLateraly = If(ValidatinLaterality.Contains(.ShowServiceMedicalOrder) = True, .RequiresLaterality, False)
                                ImageGuidanceProcedures = .ImageGuidanceProcedure

                                INDsleMinimunAgeUnit.EditValue = .MinimunAgeUnit
                                INDseMinimunAge.EditValue = .MinimunAge
                                INDsleMaximumAgeUnit.EditValue = .MaximumAgeUnit
                                INDseMaximumAge.EditValue = .MaximumAge
                                INDsleSex.EditValue = .Sex
                                INDsleOxigenService.EditValue = .OxigenService
                                ShowServiceMedicalOrder = .ShowServiceMedicalOrder
                                INDsleShowDashboardOf.EditValue = .ShowDashboardOf
                                INDsleShowDashboardOfAmbulatory.EditValue = .ShowDashboardOfAmbulatory
                                INDsleFinancedResourceUPC.EditValue = .FinancedResourceUPC
                                INDsleTherapyProcedure.EditValue = .TherapyProcedure
                                INDsleAllowDiligenceInPlace.EditValue = .AllowDiligenceInPlace
                                INDsleAllowDiligenceReportRealizationQx.EditValue = .AllowDiligenceReportRealizationQx
                                INDsleSerialService.EditValue = .SerialService
                                INDsleRequiresInterpretation.EditValue = .RequiresInterpretation
                                INDsleRequiresConfirmationRealization.EditValue = .RequiresConfirmationRealization
                                INDSleRequestRoomAutomatically.EditValue = .RequestRoomAutomatically
                                ListCupsPanelDetail = .CUPSEntityPanelDetail.ToList()
                                INDGcPanelCups.DataSource = ListCupsPanelDetail
                                INDGcPanelCups.RefreshDataSource()

                                Dim itemsCheckedList = INDccbeType4505.Properties.Items
                                For Each itemChecked As DevExpress.XtraEditors.Controls.CheckedListBoxItem In itemsCheckedList
                                    Dim field = cupsEntity.GetType().GetProperties().Where(Function(y) y.Name.Equals(itemChecked.Value)).FirstOrDefault()
                                    If field IsNot Nothing Then
                                        Dim valueCompare = field.GetValue(cupsEntity)
                                        If valueCompare = True Then
                                            itemChecked.CheckState = System.Windows.Forms.CheckState.Checked
                                        Else
                                            itemChecked.CheckState = System.Windows.Forms.CheckState.Unchecked
                                        End If
                                    End If
                                Next

                                INDGcHomologation.DataSource = Nothing
                                Dim resultHomologation As ActionResult(Of List(Of CupsHomologation)) = Await Model.GetListCupsHomologationByCupsEntityId(.Id)
                                If resultHomologation.StateResult = False Then
                                    Mensaje(EeventViewerImages.Advertencia) = resultHomologation.Message
                                Else
                                    If resultHomologation.ObjectEmbbeded IsNot Nothing Then
                                        INDGcHomologation.DataSource = resultHomologation.ObjectEmbbeded.ToList
                                        For Each item In resultHomologation.ObjectEmbbeded
                                            .CupsHomologation.Add(item)
                                        Next
                                    Else
                                        INDGcHomologation.DataSource = Nothing
                                    End If
                                End If

                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.cupsEntity.Code)
                            If result.Id = 0 Then
                                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                                state.State = Domain.Base.Entities.ObjectState.Added
                                record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = cupsEntity.Id}
                                Dim operation = Await ModelRecord.SaveBlockRecord(record)
                                record = operation.ObjectEmbbeded
                            Else
                                record = result
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(cupsEntity.Id)
                            ActionsOnControls = True
                            INDlygHomologation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDbtnCode.Enabled = False
                        End Using

                        INDviewDescriptions.ShowLoadingPanel()
                        Await Task.Factory.StartNew(Sub() LoadDescriptions())
                    Else
                        CreateNew()
                    End If
                Else
                    CreateNew()
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDviewDescriptions.HideLoadingPanel()
            INDviewRIAS.HideLoadingPanel()
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Crea la entidad cups
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateNew()
        Status = True
        cupsEntity = New CUPSEntity With {.Status = True}
        ActionsOnControls = True
        INDtxtDescription.Focus()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MCupsEntity(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not cupsEntity.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")

                    cupsEntity = Result.ObjectEmbbeded
                Else
                    If Result.Message = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Establece el ancho de la columna mas info
    ''' </summary>
    Private Sub WidthActionsColumns()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewRIAS.Columns
            If col.Name = "colActions" Then
                col.Width = 20
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewDescriptions.Columns
            If col.Name = "colActions" Then
                col.Width = 20
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo para agregar detalle de panel al cups
    ''' </summary>
    Private Sub AddPanelDetail()
        If SelectedCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione al menos un item de entidad CUPS."
            Exit Sub
        End If
        Dim StringBuilder = New StringBuilder

        If ListCupsPanelDetail Is Nothing Then
            ListCupsPanelDetail = New List(Of CUPSEntityPanelDetail)
        End If

        For Each item In SelectedCups
            If ListCupsPanelDetail.Any(Function(x) x.CUPSEntityId = item.Id) Then
                StringBuilder.AppendLine(item.Code)
                Continue For
            End If
            Dim _cUPSEntityPanelDetail = New CUPSEntityPanelDetail
            With _cUPSEntityPanelDetail
                .CUPSCode = item.Code
                .CUPSName = item.Description
                .CUPSEntityId = item.Id
            End With
            ListCupsPanelDetail.Add(_cUPSEntityPanelDetail)
        Next
        INDGcPanelCups.DataSource = ListCupsPanelDetail.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
        INDGcPanelCups.RefreshDataSource()

        If StringBuilder.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = $"Lo siguientes servicios ya estan agregados: {StringBuilder.ToString}"
        End If

        ClearSelectedCups()
        INDSleCupsEntity.Properties.NullText = "0 item seleccionado"
        INDSleCupsEntity.Focus()
        INDGvCupsEntity.RefreshData()
    End Sub

    ''' <summary>
    ''' metodo para eliminar detalle de panel al cups
    ''' </summary>
    Private Sub DeletePanelDetail()
        Dim _panel = TryCast(INDGvPanelCups.GetFocusedRow, CUPSEntityPanelDetail)
        If _panel Is Nothing Then
            Exit Sub
        End If
        If _panel.Id > 0 Then
            _panel.MarkAsDeleted()
        Else
            ListCupsPanelDetail.Remove(_panel)
        End If
        INDGcPanelCups.DataSource = ListCupsPanelDetail.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
        INDGcPanelCups.RefreshDataSource()
    End Sub
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        Presenter = Nothing
        cupsEntity = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
        ListRIPSConcept = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCupsEntity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCupsEntity, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCupsEntity(Me)
        INDGleServiceType.Properties.DataSource = ListServiceType
        LoadStatus()
        Deshacer()
        InitializeTuple()
        IndigoGridControl1.RefreshGrid(INDGcHomologation)
        SearchMode = False

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDviewRIAS, ListActions)
        IndigoGridView3.SetListAcction(INDviewDescriptions, ListActions)
        INDLciImageGuidanceProcedures.HideControl()

        WidthActionsColumns()
        Task.Factory.StartNew(Sub() SetVisibleGroupDescriptions())
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCupsEntity_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await ValidateCode()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCupsEntity_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de subgrupos cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCupsSubGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCupsSubGroup.QueryPopUp
        If INDsleCupsSubGroup.Properties.DataSource Is Nothing Then
            Presenter.InitializeCupsSubGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el contro de servicios RIPS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIPSServices_QueryPopUP(sender As Object, e As CancelEventArgs) Handles INDsleRIPSServices.QueryPopUp
        If INDsleRIPSServices.Properties.DataSource Is Nothing Then
            Presenter.InitializeRIPSServices()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de grupo de servicios ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSServiceGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIPSServiceGroup.QueryPopUp
        If INDsleIPSServiceGroup.Properties.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                Me.INDsleIPSServiceGroup.Properties.DataSource = model.GetBillinConceptByType(2) 'Servicios de Salud
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de grupo de facturacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBillingGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBillingGroup.QueryPopUp
        If INDsleBillingGroup.Properties.DataSource Is Nothing Then
            Presenter.InitializeBillingGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de grupo de servicios ips si aplica RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRIASBillingConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIASBillingConcept.QueryPopUp
        If INDsleRIASBillingConcept.Properties.DataSource Is Nothing Then
            Using model As New MIPSServiceGroup(Me.Tag)
                Me.INDsleRIASBillingConcept.Properties.DataSource = model.GetBillinConceptByType(2) 'Servicios de Salud
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de grupo de facturacion si aplica RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRIASBillingGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIASBillingGroup.QueryPopUp
        If INDsleRIASBillingGroup.Properties.DataSource Is Nothing Then
            Presenter.InitializeRIASBillingGroup()
        End If
    End Sub

    ''' <summary>
    ''' EVENTO DE CONSULTA DEL COMBO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleCupsEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCupsEntity.QueryPopUp
        If INDSleCupsEntity.Properties.DataSource Is Nothing Then
            Await Presenter.InitializeCUPSEntity()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del contro de servicios RIPS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIPSServices_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRIPSServices.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRIPSServices With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeRIPSServices()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de subgrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCupsSubGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCupsSubGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCupsSubGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCupsSubGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de grupo de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSServiceGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleIPSServiceGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            'Dim size As System.Drawing.Size
            'size.Width = 780
            'size.Height = 768
            'Using pop As New FrmTransparent(New FrmIPSServiceGroups With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
            '    pop.Show()
            'End Using
            OpenForm("749", Nothing, True)
            Presenter.InitializeIPSServiceGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de grupo de facturacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBillingGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBillingGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm("1503", Nothing, True)
            Presenter.InitializeBillingGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de grupo de servicio ips si aplica RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRIASBillingConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRIASBillingConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("749", Nothing, True)
            Presenter.InitializeRIASBillingConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del control de grupo de facturacion si aplica RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRIASBillingGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRIASBillingGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm("1503", Nothing, True)
            Presenter.InitializeRIASBillingGroup()
        End If
    End Sub

    ''' <summary>
    ''' accion click del boton para agregar los detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAddCups_Click(sender As Object, e As EventArgs) Handles INDBtnAddCups.Click
        AddPanelDetail()
    End Sub

    ''' <summary>
    ''' Accion para eliminar un item de la rejilla panel 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepBtnDelete_Click(sender As Object, e As EventArgs) Handles INDrepBtnDelete.Click
        DeletePanelDetail()
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de aplica a rias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleApplyRIAS_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyRIAS.EditValueChanged
        If INDsleApplyRIAS.EditValue Then
            INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciRIASBillingConcept.AllowHide = False
            INDlciRIASBillingConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciRIASBillingGroup.AllowHide = False
            INDlciRIASBillingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlygRIAS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciRIASBillingConcept.AllowHide = True
            INDlciRIASBillingConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciRIASBillingGroup.AllowHide = True
            INDlciRIASBillingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Sub INDsleShowServiceMedicalOrder_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleShowServiceMedicalOrder.EditValueChanged
        If ShowServiceMedicalOrder IsNot Nothing Then
            If ShowServiceMedicalOrder = 7 Then 'Ninguno
                INDlyItemTherapyProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAllowDiligenceInPlace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAllowDiligenceReportRealizationQx.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSerialService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRequiresLaterality.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                If ShowServiceMedicalOrder = 5 Then 'Procedimiento Qx
                    INDlyItemAllowDiligenceReportRealizationQx.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyItemAllowDiligenceReportRealizationQx.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                If ShowServiceMedicalOrder = 1 Then 'Laboratorios
                    INDlyItemSerialService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyItemSerialService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                INDlyItemTherapyProcedure.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAllowDiligenceInPlace.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                If {2, 3, 4, 5}.Contains(ShowServiceMedicalOrder) Then 'Imagenes Diagnosticas- Procedimientos no Qx - Procedimientos Qx - Patologías
                    INDLciRequiresLaterality.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDLciRequiresLaterality.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End If

            If ShowServiceMedicalOrder <> 4 AndAlso ShowServiceMedicalOrder <> 5 Then
                INDsleShowDashboardOf.EditValue = Nothing
                INDsleShowDashboardOfAmbulatory.EditValue = Nothing
                INDlyItemRequiresInterpretation.HideControl()
            End If

            INDSleRequestRoomAutomatically.EditValue = False
            INDLciRequestRoomAutomatically.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If ShowServiceMedicalOrder = 4 AndAlso INDsleShowDashboardOf.EditValue = 5 Then
                INDLciRequestRoomAutomatically.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If

            SetListShowDashboardOf()
        End If
    End Sub

    Private Sub INDsleRequiresInterpretation_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRequiresInterpretation.EditValueChanged
        'If INDsleRequiresInterpretation.EditValue IsNot Nothing Then
        '    If INDsleRequiresInterpretation.EditValue Then
        '        INDlyItemRequiresConfirmationRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '    Else
        '        INDlyItemRequiresConfirmationRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de Procedimiento Terapia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTherapyProcedure_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTherapyProcedure.EditValueChanged

        If INDsleTherapyProcedure.EditValue IsNot Nothing Then
            If INDsleTherapyProcedure.EditValue Then
                INDlyItemRequiresConfirmationRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                If INDsleRequiresInterpretation.EditValue IsNot Nothing Then
                    If INDsleRequiresInterpretation.EditValue Then
                        INDlyItemRequiresConfirmationRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        INDlyItemRequiresConfirmationRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                End If

            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control listar este servicio en dashboard de
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleShowDashboardOf_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleShowDashboardOf.EditValueChanged
        If INDsleShowDashboardOf.EditValue IsNot Nothing Then
            If INDsleShowDashboardOf.EditValue = 12 Then
                INDlyItemRequiresInterpretation.HideControl(False)
            Else
                INDlyItemRequiresInterpretation.HideControl()
            End If

            INDSleRequestRoomAutomatically.EditValue = False
            INDLciRequestRoomAutomatically.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            If ShowServiceMedicalOrder = 4 AndAlso INDsleShowDashboardOf.EditValue = 5 Then
                INDLciRequestRoomAutomatically.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que oculta o muestra la seccion de cups panel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleIsPanel_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleIsPanel.EditValueChanged
        If String.IsNullOrEmpty(INDGleIsPanel.EditValue) OrElse IsPanel = 0 Then
            INDlygHCOptions3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSleCupsEntity.Properties.DataSource = Nothing
            If ListCupsPanelDetail Is Nothing Then
                ListCupsPanelDetail = New List(Of CUPSEntityPanelDetail)
            End If
            ListCupsPanelDetail.RemoveAll(Function(x) x.Id = 0)
            ListCupsPanelDetail.FindAll(Function(x) x.Id > 0).ForEach(Sub(d) d.MarkAsDeleted())
            INDGcPanelCups.DataSource = ListCupsPanelDetail.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
            INDGcPanelCups.RefreshDataSource()
            Exit Sub
        End If
        INDlygHCOptions3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el editvalue del campo Tipo de Servicio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleServiceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleServiceType.EditValueChanged
        If INDGleServiceType.EditValue = 5 Then
            INDLciSurgicalReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            SurgicalReport = True
        ElseIf INDGleServiceType.EditValue = 3 Then
            INDLciImageGuidanceProcedures.HideControl(False)
        Else
            INDLciSurgicalReport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciImageGuidanceProcedures.HideControl()
            SurgicalReport = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el editvalue del campo Servicios RIPS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIPSServices_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRIPSServices.EditValueChanged
        If RIPSServicesXpo Is Nothing Then
            Presenter.InitializeRIPSServices()
        End If
    End Sub
#End Region

#Region "Selection"

    Private Sub ClearSelectedCups()
        If INDSleCupsEntity.Properties.DataSource IsNot Nothing Then
            SelectedCups?.ForEach(Sub(m) m.SelectOption = False)
        End If
    End Sub

    Private Sub INDGv_RowCellClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs) Handles INDGvCupsEntity.RowCellClick
        If e.Column.FieldName.Contains("SelectOption") Then
            If e.RowHandle >= 0 Then
                Dim row = INDGvCupsEntity.GetRow(e.RowHandle)
                row.SelectOption = Not row.SelectOption
                INDGvCupsEntity.RefreshRow(e.RowHandle)
            Else
                ClearSelectedCups()
                INDGvCupsEntity.RefreshData()
            End If
        End If
    End Sub

    Private Sub INDGvSelector_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDSleCupsEntity.Closed
        Dim searchLookupEdit = TryCast(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        searchLookupEdit.EditValue = Nothing

        If SelectedCount = 1 Then
            searchLookupEdit.Properties.NullText = "1 Item Seleccionado"
        Else
            searchLookupEdit.Properties.NullText = String.Format("{0} Items Seleccionados", SelectedCount)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de agregar descripción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddDescription_Click(sender As Object, e As EventArgs) Handles INDbtnAddDescription.Click
        CUPSEntityContractDescriptions = Nothing
        OpenFormDescriptions(False)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddRIAS_Click(sender As Object, e As EventArgs) Handles INDbtnAddRIAS.Click
        CupsEntityRIAS = Nothing
        OpenFormRIAS(False)
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditRIAS()
            Case "Remove"
                DeleteRIAS()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditRIAS()
            Case "Remove"
                DeleteRIAS()
        End Select
    End Sub

    ''' <summary>
    ''' Listado de acciones de la rejilla de descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDescription()
            Case "Remove"
                DeleteDescription()
        End Select
    End Sub

    ''' <summary>
    ''' Listado de acciones de la rejilla de descripciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDescription()
            Case "Remove"
                DeleteDescription()
        End Select
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
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
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub


#End Region

End Class