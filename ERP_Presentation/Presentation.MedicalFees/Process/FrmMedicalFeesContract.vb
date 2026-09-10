'***********************************************************************
' Assembly         : Presentacion.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Contract
Imports Presentation.Controls
Imports Presentation.Maintenance
Imports Presentation.MedicalFees.MVP

#End Region

Public Class FrmMedicalFeesContract
    Implements IMedicalFeesContract

#Region "Builder"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler bw.DoWork, AddressOf bw_DoWork
        AddHandler bw.RunWorkerCompleted, AddressOf bw_RunWorkerCompleted
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Establece el datasource de los médicos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HealthProfessionalXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.HealthProfessionalXpo
        Get
            Return INDrepSleHealthProfessional.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDrepSleHealthProfessional.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la rejilla de tipos de liquidacion
    ''' dependiendo del tipo que escojan
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceGridControls As DevExpress.Xpo.XPCollection Implements IMedicalFeesContract.DataSourceGridControls
        Get
            Return INDgcControlsLiquidationType.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDgcControlsLiquidationType.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceId As Integer Implements IMedicalFeesContract.IPSServiceId
        Get

        End Get
        Set(value As Integer)

        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del servicio ips
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IPSServiceXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.IPSServiceXpo
        Get

        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)

        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la linea de distribucion proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineId As Integer? Implements IMedicalFeesContract.SupplierDistributionLineId
        Get
            Return INDsleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la linea de distribucion proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierDistributionLineXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.SupplierDistributionLineXpo
        Get
            Return INDsleSupplier.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si se realiza un descuento por aceptaciones de IPS
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutomaticDiscountForObjections As Boolean? Implements IMedicalFeesContract.AutomaticDiscountForObjections
        Get
            Return INDsleAutomaticDiscountForObjections.EditValue
        End Get
        Set(value As Boolean?)
            INDsleAutomaticDiscountForObjections.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la ultima liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property LastLiquidationDate As Date? Implements IMedicalFeesContract.LastLiquidationDate
        Get
            Return INDdteLastLiquidationDate.EditValue
        End Get
        Set(value As Date?)
            INDdteLastLiquidationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Integer Implements IMedicalFeesContract.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Integer)
            'Falta el status
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As MedicalFeesSecuence Implements IMedicalFeesContract.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As MedicalFeesSecuence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.MedicalFeesSecuenceDetail In Me._sequense.MedicalFeesSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IMedicalFeesContract.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IMedicalFeesContract.MyTag
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
    Public Property Code As String Implements IMedicalFeesContract.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece el nombre del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractName As String Implements IMedicalFeesContract.ContractName
        Get
            Return INDtxtContractName.Text
        End Get
        Set(value As String)
            INDtxtContractName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el numero del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractNumber As String Implements IMedicalFeesContract.ContractNumber
        Get
            Return INDtxtContractNumber.Text
        End Get
        Set(value As String)
            INDtxtContractNumber.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EndDate As Date? Implements IMedicalFeesContract.EndDate
        Get
            Return INDdteEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdteEndDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date? Implements IMedicalFeesContract.InitialDate
        Get
            Return INDdteInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observations As String Implements IMedicalFeesContract.Observations
        Get
            Return INDmemoObservations.Text
        End Get
        Set(value As String)
            INDmemoObservations.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractType As Integer? Implements IMedicalFeesContract.ContractType
        Get
            Return INDsleContractType.EditValue
        End Get
        Set(value As Integer?)
            INDsleContractType.EditValue = value
        End Set
    End Property

#Region "Popup Properties"

    ''' <summary>
    ''' Obtiene o establece el valor fijo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AmountPayablePopup As Integer Implements IMedicalFeesContract.AmountPayablePopup
        Get
            Return INDtxtAmountPayablePopup.EditValue
        End Get
        Set(value As Integer)
            INDtxtAmountPayablePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CareGroupId As Integer Implements IMedicalFeesContract.CareGroupId
        Get
            'Return INDsleCareGroup.EditValue
        End Get
        Set(value As Integer)
            'INDsleCareGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del grupo de atencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CareGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.CareGroupXpo
        Get
            'Return INDsleCareGroup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            'INDsleCareGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractEntityId As Integer Implements IMedicalFeesContract.ContractEntityId
        Get
            'Return INDsleContractEntity.EditValue
        End Get
        Set(value As Integer)
            'INDsleContractEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del contrato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ContractEntityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.ContractEntityXpo
        Get
            'Return INDsleContractEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            'INDsleContractEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CUPSEntityId As Integer Implements IMedicalFeesContract.CUPSEntityId
        Get
            Return INDsleCUPSEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleCUPSEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad cups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CUPSEntityXpo As DevExpress.Xpo.XPCollection Implements IMedicalFeesContract.CUPSEntityXpo
        Get
            Return INDgcCupsEntity.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDgcCupsEntity.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de excepcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExceptionType As Integer Implements IMedicalFeesContract.ExceptionType
        Get
            Return INDsleExceptionType.EditValue
        End Get
        Set(value As Integer)
            INDsleExceptionType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PercentageRatePopup As Decimal Implements IMedicalFeesContract.PercentageRatePopup
        Get
            Return INDsePercentageRatePopup.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentageRatePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualIdPopup As Integer Implements IMedicalFeesContract.RateManualIdPopup
        Get
            Return INDsleRateManualPopup.EditValue
        End Get
        Set(value As Integer)
            INDsleRateManualPopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de manual
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualType As Integer Implements IMedicalFeesContract.RateManualType
        Get
            Return INDsleRateManualType.EditValue
        End Get
        Set(value As Integer)
            INDsleRateManualType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del manual tarifario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateManualXpoPopup As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.RateManualXpoPopup
        Get
            Return INDsleRateManualPopup.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleRateManualPopup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateTypePopup As Integer Implements IMedicalFeesContract.RateTypePopup
        Get
            Return INDsleRateTypePopup.EditValue
        End Get
        Set(value As Integer)
            INDsleRateTypePopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de variacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RateVariationPopup As Decimal Implements IMedicalFeesContract.RateVariationPopup
        Get
            Return INDseRateVariationPopup.EditValue
        End Get
        Set(value As Decimal)
            INDseRateVariationPopup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CUPSGroupId As Integer Implements IMedicalFeesContract.CUPSGroupId
        Get
            'Return INDsleGroupCUPS.EditValue
        End Get
        Set(value As Integer)
            'INDsleGroupCUPS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CUPSGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.CUPSGroupXpo
        Get
            'Return INDsleGroupCUPS.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            'INDsleGroupCUPS.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CUPSSubgroupId As Integer Implements IMedicalFeesContract.CUPSSubgroupId
        Get
            'Return INDsleSubGroupCUPS.EditValue
        End Get
        Set(value As Integer)
            'INDsleSubGroupCUPS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CUPSSubgroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IMedicalFeesContract.CUPSSubgroupXpo
        Get
            'Return INDsleSubGroupCUPS.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            'INDsleSubGroupCUPS.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PMedicalFeesContract

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MedicalFees"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.MedicalFeesSecuence

    ''' <summary>
    ''' Representa la entidad de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim medicalFeesContract As MedicalFeesContract

    ''' <summary>
    ''' Representa la entidad de contrato
    ''' </summary>
    ''' <remarks></remarks>
    Dim medicalFeesContractGeneral As MedicalFeesContractGeneral

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMedicalFees

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListContractType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRateType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListExceptionType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRateManualType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de excepciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListMedicalFeesContractException As List(Of MedicalFeesContractException)

    ''' <summary>
    ''' Listado de eliminados de excepciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteMedicalFeesContractException As List(Of MedicalFeesContractException)

    ''' <summary>
    ''' True = modificar y False = guardar en las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeModify As Boolean = False

    ''' <summary>
    ''' Listado para comparar los id de cada tipo de excepcion
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listCompare As List(Of MedicalFeesContractException)

    ''' <summary>
    ''' Representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _medicalFeesContractException As MedicalFeesContractException

    ''' <summary>
    ''' Bandera para saber si entra al editvaluechanged del control de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSupplier As Boolean = False

    ''' <summary>
    ''' Representa el id de la cuenta del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idMainAccountSupplier As Integer?

    ''' <summary>
    ''' Representa el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idSupplier As Integer?

    ''' <summary>
    ''' Representa el id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Dim _idThirdParty As Integer?

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Asyncrono
    ''' </summary>
    ''' <remarks></remarks>
    Private bw As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Listado para el datasource de los controles de tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpCollection As DevExpress.Xpo.XPCollection

#End Region

#Region "ICrud"

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
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If medicalFeesContract IsNot Nothing AndAlso medicalFeesContract.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MMedicalFeesContract(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteMedicalFeesContract(medicalFeesContract)
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
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ValidateFields() = False Then
                Exit Sub
            End If
        End If
        AssigningValues()
        Using model As New MMedicalFeesContract(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveMedicalFeesContract(medicalFeesContract, _idCurrentSequense)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If medicalFeesContract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    'Se descarta la secuencia numerica usada
                    If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                        Me.DicSequense(Me._sequense.MedicalFeesSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                    If Me._sequense.Sequential Then
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    End If
                ElseIf medicalFeesContract.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.medicalFeesContract = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                SearchMode = False
                Me.Deshacer()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Valida los controles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateFields() As Boolean
        If ListMedicalFeesContractException Is Nothing OrElse ListMedicalFeesContractException.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay detalles de reglas de liquidación."
            Return False
        End If
        Return True
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewMedicalFeesContract()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "ContractName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Número", .FieldName = "ContractNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "ContractTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListMedicalFeesContract
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Muestra u oculta las columnas dependiendo del tipo de liquidacion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideColumnsOfGridControlsLiquidationType()
        Dim ListStringNames As New List(Of String)
        Select Case ExceptionType
            Case 1 'IPSService
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolManualType")
                ListStringNames.Add("INDcolPresentation")
            Case 3 'CupsSubGroup
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolGroup")
            Case 4 'CupsGroup
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
            Case 5 'CareGroup
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
                ListStringNames.Add("INDcolType")
                ListStringNames.Add("INDcolEntityType")
            Case 6 'ContractEntity
                ListStringNames.Add("INDcolSelectionOption")
                ListStringNames.Add("INDcolCode")
                ListStringNames.Add("INDcolName")
        End Select
        FieldsGrid(ListStringNames)
    End Sub

    ''' <summary>
    ''' Metodo que recorre las columnas de la rejilla y las coloca visible 
    ''' dependiendo del listado de colName que le envien
    ''' </summary>
    ''' <param name="ListStringNames"></param>
    ''' <remarks></remarks>
    Private Sub FieldsGrid(ByVal ListStringNames As List(Of String))
        'Asigno si la columna es visible o no dependiendo del listado que envien anteriormente
        For iColumns = 0 To viewControlsLiquidationType.Columns.Count - 1
            For iList = 0 To ListStringNames.Count - 1
                If viewControlsLiquidationType.Columns.Item(iColumns).Name = ListStringNames.Item(iList) Then
                    viewControlsLiquidationType.Columns.Item(iColumns).Visible = True
                    Exit For
                Else
                    viewControlsLiquidationType.Columns.Item(iColumns).Visible = False
                End If
            Next
        Next

        'Asigno los visibleIndex para que aparezcan en orden las columnas
        Dim cont As Integer = 0
        For iColumns = 0 To viewControlsLiquidationType.Columns.Count - 1
            If viewControlsLiquidationType.Columns.Item(iColumns).Visible = True Then
                viewControlsLiquidationType.Columns.Item(iColumns).VisibleIndex = cont
                cont += 1
            End If
        Next
    End Sub

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        ListXpCollection = Nothing
        ListXpCollection = Presenter.InitializeDataSourceGridControlsLiquidationType(ExceptionType)
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bw_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        INDgcControlsLiquidationType.DataSource = Nothing
        INDgcControlsLiquidationType.DataSource = ListXpCollection
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptions(optionCheck As Integer)
        Dim view As GridView = viewGridCupsEntity
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), optionCheck)
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    row.SelectOption = optionCheck
                End If
            Next
        End If
        INDgcCupsEntity.RefreshDataSource()
        Dim cont = (From l In CUPSEntityXpo Where l.SelectOption = True Select l).Count
        INDsleCUPSEntity.Text = cont.ToString + " item seleccionado"
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        If Not view.IsGroupRow(groupRowHandle) Then
            Return
        End If

        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    row.SelectOption = False
                Else
                    row.SelectOption = True
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Obtiene los childsRowHandles
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetChildRowsHandles(view As GridView, groupRowHandle As Integer) As Integer
        Dim childRows As Integer = 0
        If Not view.IsGroupRow(groupRowHandle) Then
            childRows = 1
            Return childRows
        End If
        Return childRows
    End Function

    ''' <summary>
    ''' Valida que el item no se encuentre en la rejilla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateItem(ListValidate As List(Of MedicalFeesContractException)) As Boolean
        Dim cont As Integer
        Dim listErrors As New StringBuilder
        Select Case ExceptionType
            Case 1, 3, 4, 5, 6 'IPSService, SubGroup, Group, CareGroup, ContractEntity
                If ListXpCollection IsNot Nothing Then
                    Dim ListXpo = (From l In ListXpCollection Where l.SelectOption = True Select l).ToList
                    If ListXpo IsNot Nothing AndAlso ListXpo.Count > 0 Then
                        For Each itemXpo In ListXpo
                            Select Case ExceptionType
                                Case 1 'IPSService
                                    cont = ListValidate.ToList.FindAll(Function(item) item.IPSServiceId IsNot Nothing AndAlso item.IPSServiceId = itemXpo.Id).ToList().Count
                                Case 3 'SubGroup
                                    cont = ListValidate.ToList.FindAll(Function(item) item.CUPSSubgroupId IsNot Nothing AndAlso item.CUPSSubgroupId = itemXpo.Id).ToList().Count
                                Case 4 'Group
                                    cont = ListValidate.ToList.FindAll(Function(item) item.CUPSGroupId IsNot Nothing AndAlso item.CUPSGroupId = itemXpo.Id).ToList().Count
                                Case 5 'CareGroup
                                    cont = ListValidate.ToList.FindAll(Function(item) item.CareGroupId IsNot Nothing AndAlso item.CareGroupId = itemXpo.Id).ToList().Count
                                Case 6 'Contract
                                    cont = ListValidate.ToList.FindAll(Function(item) item.ContractEntityId IsNot Nothing AndAlso item.ContractEntityId = itemXpo.Id).ToList().Count
                            End Select
                            If cont > 0 Then
                                listErrors.AppendLine(String.Format(ResourceManager.GetString("ItemDuplicated", NAME_MODULE), itemXpo.CodeName))
                            End If
                        Next
                        If listErrors.Length > 0 Then
                            cont = 1
                        End If
                    End If
                End If
            Case 2 'CupsEntity
                If CUPSEntityXpo IsNot Nothing Then
                    Dim ListXpo = (From l In CUPSEntityXpo Where l.SelectOption = True Select l).ToList
                    If ListXpo IsNot Nothing AndAlso ListXpo.Count > 0 Then
                        For Each itemXpo In ListXpo
                            cont = ListValidate.ToList.FindAll(Function(item) item.CUPSEntityId IsNot Nothing AndAlso item.CUPSEntityId = itemXpo.Id).ToList().Count
                            If cont > 0 Then
                                listErrors.AppendLine(String.Format(ResourceManager.GetString("ItemDuplicated", NAME_MODULE), itemXpo.CodeDescription))
                            End If
                        Next
                        If listErrors.Length > 0 Then
                            cont = 1
                        End If
                    End If
                End If
            Case 7 'RateManualType
                cont = ListValidate.ToList.FindAll(Function(item) item.RateManualType IsNot Nothing AndAlso item.RateManualType = RateManualType).ToList().Count
                listErrors.AppendLine(String.Format(ResourceManager.GetString("ItemDuplicated", NAME_MODULE), INDsleRateManualType.Text))
            Case 8 'General
                cont = ListValidate.ToList.FindAll(Function(item) item.ExceptionType = 8).ToList().Count
                listErrors.AppendLine(String.Format(ResourceManager.GetString("ItemDuplicated", NAME_MODULE), INDsleExceptionType.Text))
        End Select

        If cont > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            INDsleExceptionType.Focus()
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Metodo que agrega al listado de excepciones
    ''' la entidad generada
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AgregateEntityToList()
        Select Case ExceptionType
            Case 1, 3, 4, 5, 6 'IPSService, SubGroup, Group, CareGroup, ContractEntity
                Dim ListXpo = (From l In ListXpCollection Where l.SelectOption = True Select l).ToList
                For Each itemXpo In ListXpo
                    Dim MedicalFeesContractException As New MedicalFeesContractException
                    With MedicalFeesContractException
                        .ExceptionType = ExceptionType
                        Select Case ExceptionType
                            Case 1 'IPSService
                                .IPSServiceId = itemXpo.Id
                            Case 3 'SubGroup
                                .CUPSSubgroupId = itemXpo.Id
                            Case 4 'Group
                                .CUPSGroupId = itemXpo.Id
                            Case 5 'CareGroup
                                .CareGroupId = itemXpo.Id
                            Case 6 'Contract
                                .ContractEntityId = itemXpo.Id
                        End Select
                        .ExceptionDescription = itemXpo.CodeName
                        .RateType = RateTypePopup
                        Select Case RateTypePopup
                            Case 1
                                .PercentageRate = PercentageRatePopup
                                .RateDescription = PercentageRatePopup.ToString + "%"
                            Case 2
                                .RateManualId = RateManualIdPopup
                                .RateManualDescription = INDsleRateManualPopup.Text
                                .RateVariation = RateVariationPopup
                                .RateDescription = INDsleRateManualPopup.Text + " - " + RateVariationPopup.ToString + "%"
                            Case 3
                                .AmountPayable = AmountPayablePopup
                                .RateDescription = AmountPayablePopup.ToString("C2")
                        End Select
                    End With

                    ListMedicalFeesContractException.Add(MedicalFeesContractException)
                Next
            Case 2 'CupsEntity
                Dim ListXpo = (From l In CUPSEntityXpo Where l.SelectOption = True Select l).ToList
                For Each itemXpo In ListXpo
                    Dim MedicalFeesContractException As New MedicalFeesContractException
                    With MedicalFeesContractException
                        .ExceptionType = ExceptionType
                        .CUPSEntityId = itemXpo.Id
                        .ExceptionDescription = itemXpo.CodeDescription
                        .RateType = RateTypePopup
                        Select Case RateTypePopup
                            Case 1
                                .PercentageRate = PercentageRatePopup
                                .RateDescription = PercentageRatePopup.ToString + "%"
                            Case 2
                                .RateManualId = RateManualIdPopup
                                .RateManualDescription = INDsleRateManualPopup.Text
                                .RateVariation = RateVariationPopup
                                .RateDescription = INDsleRateManualPopup.Text + " - " + RateVariationPopup.ToString + "%"
                            Case 3
                                .AmountPayable = AmountPayablePopup
                                .RateDescription = AmountPayablePopup.ToString("C2")
                        End Select
                    End With

                    ListMedicalFeesContractException.Add(MedicalFeesContractException)
                Next
            Case 7, 8 'RateManualType, General
                Dim MedicalFeesContractException As New MedicalFeesContractException
                With MedicalFeesContractException
                    .ExceptionType = ExceptionType
                    Select Case ExceptionType
                        Case 7 'RateManualType
                            .RateManualType = RateManualType
                            .ExceptionDescription = INDsleRateManualType.Text
                        Case 8 'General
                            .ExceptionDescription = INDsleExceptionType.Text
                    End Select
                    .RateType = RateTypePopup
                    Select Case RateTypePopup
                        Case 1
                            .PercentageRate = PercentageRatePopup
                            .RateDescription = PercentageRatePopup.ToString + "%"
                        Case 2
                            .RateManualId = RateManualIdPopup
                            .RateManualDescription = INDsleRateManualPopup.Text
                            .RateVariation = RateVariationPopup
                            .RateDescription = INDsleRateManualPopup.Text + " - " + RateVariationPopup.ToString + "%"
                        Case 3
                            .AmountPayable = AmountPayablePopup
                            .RateDescription = AmountPayablePopup.ToString("C2")
                    End Select
                End With

                ListMedicalFeesContractException.Add(MedicalFeesContractException)
        End Select
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ItemSatisfactory", NAME_MODULE)
    End Sub

    ''' <summary>
    ''' Metodo que agrega una excepcion a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddException()
        Dim listErrors = ValidateControlsPopup()
        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors
            INDsleExceptionType.Focus()
            Exit Sub
        End If

        If modeModify = False Then
            If ListMedicalFeesContractException Is Nothing Then
                ListMedicalFeesContractException = New List(Of MedicalFeesContractException)
            Else
                If ValidateItem(ListMedicalFeesContractException) = False Then
                    Exit Sub
                End If
            End If
            AgregateEntityToList()
        Else
            If ValidateItem(_listCompare) = False Then
                Exit Sub
            End If

            With _medicalFeesContractException
                .ExceptionType = ExceptionType
                Select Case ExceptionType
                    Case 1
                        .IPSServiceId = _medicalFeesContractException.IPSServiceId
                        .ExceptionDescription = _medicalFeesContractException.ExceptionDescription

                        .CUPSEntityId = Nothing
                        .CUPSSubgroupId = Nothing
                        .CUPSGroupId = Nothing
                        .CareGroupId = Nothing
                        .ContractEntityId = Nothing
                        .RateManualType = Nothing
                    Case 2
                        .CUPSEntityId = _medicalFeesContractException.CUPSEntityId
                        .ExceptionDescription = _medicalFeesContractException.ExceptionDescription

                        .IPSServiceId = Nothing
                        .CUPSSubgroupId = Nothing
                        .CUPSGroupId = Nothing
                        .CareGroupId = Nothing
                        .ContractEntityId = Nothing
                        .RateManualType = Nothing
                    Case 3
                        .CUPSSubgroupId = _medicalFeesContractException.CUPSSubgroupId
                        .ExceptionDescription = _medicalFeesContractException.ExceptionDescription

                        .IPSServiceId = Nothing
                        .CUPSEntityId = Nothing
                        .CUPSGroupId = Nothing
                        .CareGroupId = Nothing
                        .ContractEntityId = Nothing
                        .RateManualType = Nothing
                    Case 4
                        .CUPSGroupId = _medicalFeesContractException.CUPSGroupId
                        .ExceptionDescription = _medicalFeesContractException.ExceptionDescription

                        .IPSServiceId = Nothing
                        .CUPSEntityId = Nothing
                        .CUPSSubgroupId = Nothing
                        .CareGroupId = Nothing
                        .ContractEntityId = Nothing
                        .RateManualType = Nothing
                    Case 5
                        .CareGroupId = _medicalFeesContractException.CareGroupId
                        .ExceptionDescription = _medicalFeesContractException.ExceptionDescription

                        .IPSServiceId = Nothing
                        .CUPSSubgroupId = Nothing
                        .CUPSGroupId = Nothing
                        .CUPSEntityId = Nothing
                        .ContractEntityId = Nothing
                        .RateManualType = Nothing
                    Case 6
                        .ContractEntityId = _medicalFeesContractException.ContractEntityId
                        .ExceptionDescription = _medicalFeesContractException.ExceptionDescription

                        .IPSServiceId = Nothing
                        .CUPSSubgroupId = Nothing
                        .CUPSGroupId = Nothing
                        .CareGroupId = Nothing
                        .CUPSEntityId = Nothing
                        .RateManualType = Nothing
                    Case 7
                        .RateManualType = RateManualType
                        .ExceptionDescription = INDsleRateManualType.Text

                        .IPSServiceId = Nothing
                        .CUPSSubgroupId = Nothing
                        .CUPSGroupId = Nothing
                        .ContractEntityId = Nothing
                        .CareGroupId = Nothing
                        .CUPSEntityId = Nothing
                    Case 8
                        .IPSServiceId = Nothing
                        .CUPSSubgroupId = Nothing
                        .CUPSGroupId = Nothing
                        .RateManualType = Nothing
                        .ContractEntityId = Nothing
                        .CareGroupId = Nothing
                        .CUPSEntityId = Nothing

                        .ExceptionDescription = INDsleExceptionType.Text
                End Select
                .RateType = RateTypePopup
                Select Case RateTypePopup
                    Case 1
                        .PercentageRate = PercentageRatePopup
                        .RateDescription = PercentageRatePopup.ToString + "%"

                        .RateManualId = Nothing
                        .RateVariation = Nothing
                        .AmountPayable = Nothing
                    Case 2
                        .RateManualId = RateManualIdPopup
                        .RateManualDescription = INDsleRateManualPopup.Text
                        .RateVariation = RateVariationPopup
                        .RateDescription = INDsleRateManualPopup.Text + " - " + RateVariationPopup.ToString + "%"

                        .PercentageRate = Nothing
                        .AmountPayable = Nothing
                    Case 3
                        .AmountPayable = AmountPayablePopup
                        .RateDescription = AmountPayablePopup.ToString("C2")

                        .PercentageRate = Nothing
                        .RateManualId = Nothing
                        .RateVariation = Nothing
                End Select
            End With
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ItemModified", NAME_MODULE)
        End If

        INDgcExceptions.DataSource = Nothing
        INDgcExceptions.DataSource = ListMedicalFeesContractException
        modeModify = False
        CleanControlsPopup()
        INDpceExceptions.ShowPopup()
        INDsleExceptionType.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que elimina una excepcion de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteException()
        _medicalFeesContractException = viewGridExceptions.GetFocusedRow
        ListMedicalFeesContractException.Remove(_medicalFeesContractException)

        If _medicalFeesContractException.Id > 0 Then
            If ListDeleteMedicalFeesContractException Is Nothing Then
                ListDeleteMedicalFeesContractException = New List(Of MedicalFeesContractException)
            End If
            _medicalFeesContractException.MarkAsDeleted()
            ListDeleteMedicalFeesContractException.Add(_medicalFeesContractException)
        End If

        INDgcExceptions.DataSource = Nothing
        INDgcExceptions.DataSource = ListMedicalFeesContractException
    End Sub

    ''' <summary>
    ''' Metodo que edita una excepcion de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditException()
        modeModify = True
        _medicalFeesContractException = viewGridExceptions.GetFocusedRow
        With _medicalFeesContractException
            ExceptionType = .ExceptionType

            Select Case ExceptionType
                Case 1, 3, 4, 5, 6 'IPSService, SubGroup, Group, CareGroup, ContractEntity
                    ListXpCollection = Nothing
                    INDpceControlsLiquidationType.Text = "1 item seleccionado"
                    INDpceControlsLiquidationType.Properties.ReadOnly = True
                    INDsleExceptionType.Properties.ReadOnly = True
                Case 2 'CupsEntity
                    CUPSEntityXpo = Nothing
                    INDsleCUPSEntity.Properties.NullText = "1 item seleccionado"
                    INDsleCUPSEntity.Text = "1 item seleccionado"
                    INDsleCUPSEntity.Properties.ReadOnly = True
                    INDsleExceptionType.Properties.ReadOnly = True
                Case 7 'RateManualType
                    RateManualType = .RateManualType
                    INDsleRateManualType.Properties.NullText = .ExceptionDescription
            End Select
            GenrateListCompare()

            RateTypePopup = .RateType
            Select Case RateTypePopup
                Case 1
                    PercentageRatePopup = .PercentageRate
                Case 2
                    RateManualIdPopup = .RateManualId
                    INDsleRateManualPopup.Properties.NullText = .RateManualDescription
                    RateVariationPopup = .RateVariation
                Case 3
                    AmountPayablePopup = .AmountPayable
            End Select
        End With

        INDpceExceptions.ShowPopup()
        INDsleExceptionType.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que saca el listado para comparar cuando se modifica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenrateListCompare()
        Select Case ExceptionType
            Case 1
                _listCompare = (From en In ListMedicalFeesContractException Where en.IPSServiceId <> _medicalFeesContractException.IPSServiceId Select en).ToList()
            Case 2
                _listCompare = (From en In ListMedicalFeesContractException Where en.CUPSEntityId <> _medicalFeesContractException.CUPSEntityId Select en).ToList()
            Case 3
                _listCompare = (From en In ListMedicalFeesContractException Where en.CUPSSubgroupId <> _medicalFeesContractException.CUPSSubgroupId Select en).ToList()
            Case 4
                _listCompare = (From en In ListMedicalFeesContractException Where en.CUPSGroupId <> _medicalFeesContractException.CUPSGroupId Select en).ToList()
            Case 5
                _listCompare = (From en In ListMedicalFeesContractException Where en.CareGroupId <> _medicalFeesContractException.CareGroupId Select en).ToList()
            Case 6
                _listCompare = (From en In ListMedicalFeesContractException Where en.ContractEntityId <> _medicalFeesContractException.ContractEntityId Select en).ToList()
            Case 7
                _listCompare = (From en In ListMedicalFeesContractException Where en.RateManualType <> _medicalFeesContractException.RateManualType Select en).ToList()
            Case 8
                _listCompare = (From en In ListMedicalFeesContractException Where en.ExceptionType <> _medicalFeesContractException.ExceptionType Select en).ToList()
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que saca el listado para comparar cuando se modifica
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateListCompareChanged()
        Select Case ExceptionType
            Case 1
                If _medicalFeesContractException.IPSServiceId IsNot Nothing Then
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.IPSServiceId IsNot Nothing AndAlso item.IPSServiceId <> _medicalFeesContractException.IPSServiceId).ToList
                Else
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType = ExceptionType).ToList
                End If
            Case 2
                If _medicalFeesContractException.CUPSEntityId IsNot Nothing Then
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.CUPSEntityId IsNot Nothing AndAlso item.CUPSEntityId <> _medicalFeesContractException.CUPSEntityId).ToList
                Else
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType = ExceptionType).ToList
                End If
            Case 3
                If _medicalFeesContractException.CUPSSubgroupId IsNot Nothing Then
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.CUPSSubgroupId IsNot Nothing AndAlso item.CUPSSubgroupId <> _medicalFeesContractException.CUPSSubgroupId).ToList
                Else
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType = ExceptionType).ToList
                End If
            Case 4
                If _medicalFeesContractException.CUPSGroupId IsNot Nothing Then
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.CUPSGroupId IsNot Nothing AndAlso item.CUPSGroupId <> _medicalFeesContractException.CUPSGroupId).ToList
                Else
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType = ExceptionType).ToList
                End If
            Case 5
                If _medicalFeesContractException.CareGroupId IsNot Nothing Then
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.CareGroupId IsNot Nothing AndAlso item.CareGroupId <> _medicalFeesContractException.CareGroupId).ToList
                Else
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType = ExceptionType).ToList
                End If
            Case 6
                If _medicalFeesContractException.ContractEntityId IsNot Nothing Then
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ContractEntityId IsNot Nothing AndAlso item.ContractEntityId <> _medicalFeesContractException.ContractEntityId).ToList
                Else
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType = ExceptionType).ToList
                End If
            Case 7
                If _medicalFeesContractException.RateManualType IsNot Nothing Then
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.RateManualType IsNot Nothing AndAlso item.RateManualType <> _medicalFeesContractException.RateManualType).ToList
                Else
                    _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType = ExceptionType).ToList
                End If
            Case 8
                _listCompare = ListMedicalFeesContractException.FindAll(Function(item) item.ExceptionType <> _medicalFeesContractException.ExceptionType).ToList
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder

        If ExceptionType = Nothing Then
            listErrors.AppendLine("- Seleccione un tipo de excepción.")
        End If
        If INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If CUPSEntityXpo IsNot Nothing AndAlso modeModify = False Then
                Dim cont = (From l In CUPSEntityXpo Where l.SelectOption = True Select l).Count
                If cont = 0 Then
                    listErrors.AppendLine("- Seleccione al menos un item de entidad CUPS.")
                End If
            Else
                If modeModify = False Then
                    listErrors.AppendLine("- Seleccione al menos un item de entidad CUPS.")
                End If
            End If
        End If
        If INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ListXpCollection IsNot Nothing AndAlso modeModify = False Then
                Dim cont = (From l In ListXpCollection Where l.SelectOption = True Select l).Count
                If cont = 0 Then
                    listErrors.AppendLine("- Seleccione al menos un item de " & INDlyItemControlsLiquidationType.Text & ".")
                End If
            Else
                If modeModify = False Then
                    listErrors.AppendLine("- Seleccione al menos un item de " & INDlyItemControlsLiquidationType.Text & ".")
                End If
            End If
        End If
        If INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If RateManualType = Nothing Then
                listErrors.AppendLine("- Seleccione un tipo de manual tarifario.")
            End If
        End If
        If RateTypePopup = Nothing Then
            listErrors.AppendLine("- Seleccione un tipo de tarifa.")
        End If
        If INDlyItemRateManualPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If RateManualIdPopup = Nothing Then
                listErrors.AppendLine("- Seleccione un manual de tarifa.")
            End If
        End If
        If INDlyItemAmountPayablePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If AmountPayablePopup = Nothing OrElse AmountPayablePopup = 0 Then
                listErrors.AppendLine("- Ingrese un valor fijo.")
            End If
        End If

        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para redimensionar el popup dependiendo del tipo de liquidacion con su cantidad de controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RezizablePopup()
        Dim size As System.Drawing.Size

        If ExceptionType > 0 AndAlso RateTypePopup = 0 Then
            Select Case ExceptionType
                Case 1, 2, 3, 4, 5, 6, 7
                    size.Width = 416
                    size.Height = 260
                Case 8
                    size.Width = 416
                    size.Height = 201
            End Select
        ElseIf ExceptionType = 0 AndAlso RateTypePopup > 0 Then
            Select Case RateTypePopup
                Case 1, 3
                    size.Width = 416
                    size.Height = 270
                Case 2
                    size.Width = 416
                    size.Height = 340
            End Select
        ElseIf ExceptionType > 0 AndAlso RateTypePopup > 0 Then
            If ExceptionType = 8 Then
                Select Case RateTypePopup
                    Case 1, 3
                        size.Width = 416
                        size.Height = 260
                    Case 2
                        size.Width = 416
                        size.Height = 340
                End Select
            Else
                Select Case RateTypePopup
                    Case 1, 3
                        size.Width = 416
                        size.Height = 330
                    Case 2
                        size.Width = 416
                        size.Height = 390
                End Select
            End If
        Else
            size.Width = 416
            size.Height = 201
        End If

        INDpceExceptions.Properties.PopupSizeable = True
        INDpopupExceptions.Size = size
        INDpceExceptions.Properties.PopupSizeable = False
        If ExceptionType > 0 OrElse RateTypePopup > 0 Then
            INDpceExceptions.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        ExceptionType = Nothing

        INDpceControlsLiquidationType.Text = "0 item seleccionado"
        INDgcControlsLiquidationType.DataSource = Nothing
        ListXpCollection = Nothing
        INDpceControlsLiquidationType.Properties.ReadOnly = False

        INDsleCUPSEntity.Properties.NullText = "0 item seleccionado"
        INDsleCUPSEntity.Text = "0 item seleccionado"
        CUPSEntityXpo = Nothing
        INDsleCUPSEntity.Properties.ReadOnly = False
        INDsleExceptionType.Properties.ReadOnly = False

        RateManualType = Nothing
        INDsleRateManualType.Properties.NullText = String.Empty
        RateTypePopup = Nothing
        PercentageRatePopup = Nothing
        RateManualIdPopup = Nothing
        INDsleRateManualPopup.Properties.NullText = String.Empty
        RateVariationPopup = Nothing
        AmountPayablePopup = Nothing
    End Sub

    ''' <summary>
    ''' Llena los datasource de los controles de modo de impresion y control terminacion contrato
    ''' </summary>
    Private Sub InitializeSearch()
        ListContractType = New List(Of Tuple(Of Integer, String))
        ListContractType.Add(New Tuple(Of Integer, String)(1, "Estandar"))
        ListContractType.Add(New Tuple(Of Integer, String)(2, "Agremiaciones"))
        INDsleContractType.Properties.DataSource = ListContractType.ToList

        ListRateType = New List(Of Tuple(Of Integer, String))
        ListRateType.Add(New Tuple(Of Integer, String)(1, "Por % del valor cobrado"))
        ListRateType.Add(New Tuple(Of Integer, String)(2, "Por Manual Tarifario"))
        ListRateType.Add(New Tuple(Of Integer, String)(3, "Por Valor Fijo"))
        INDsleRateTypePopup.Properties.DataSource = ListRateType.ToList
        INDrepSleRateType.DataSource = ListRateType.ToList

        ListExceptionType = New List(Of Tuple(Of Integer, String))
        ListExceptionType.Add(New Tuple(Of Integer, String)(1, "01 - Servicio IPS"))
        ListExceptionType.Add(New Tuple(Of Integer, String)(2, "02 - CUPS"))
        ListExceptionType.Add(New Tuple(Of Integer, String)(3, "03 - SubGrupo CUPS"))
        ListExceptionType.Add(New Tuple(Of Integer, String)(4, "04 - Grupo CUPS"))
        ListExceptionType.Add(New Tuple(Of Integer, String)(5, "05 - Grupo de Atencion"))
        ListExceptionType.Add(New Tuple(Of Integer, String)(6, "06 - Entidad"))
        ListExceptionType.Add(New Tuple(Of Integer, String)(7, "07 - Tipo de Manual Tarifario"))
        ListExceptionType.Add(New Tuple(Of Integer, String)(8, "08 - General"))
        INDsleExceptionType.Properties.DataSource = ListExceptionType.ToList
        INDrepSleExceptionsType.DataSource = ListExceptionType.ToList

        ListRateManualType = New List(Of Tuple(Of Integer, String))
        ListRateManualType.Add(New Tuple(Of Integer, String)(1, "ISS 2001"))
        ListRateManualType.Add(New Tuple(Of Integer, String)(2, "ISS 2004"))
        ListRateManualType.Add(New Tuple(Of Integer, String)(3, "SOAT"))
        INDsleRateManualType.Properties.DataSource = ListRateManualType.ToList
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IMedicalFeesContract.ActionsOnControls
        Set(value As Boolean)
            INDlyMedicalFeesContract.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleContractType.Enabled = value
            INDsleSupplier.Enabled = value
            INDtxtContractName.Enabled = value
            INDtxtContractNumber.Enabled = value
            INDdteInitialDate.Enabled = value
            INDdteEndDate.Enabled = value
            INDdteLastLiquidationDate.Enabled = value
            INDsleAutomaticDiscountForObjections.Enabled = value
            INDmemoObservations.Enabled = value
            INDpceExceptions.Enabled = value
            INDgcExceptions.Enabled = value
            INDlyMedicalFeesContract.EndUpdate()
            If value Then
                INDsleContractType.Focus()
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
        If Me.medicalFeesContract IsNot Nothing AndAlso Me.medicalFeesContract.Id > 0 Then
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
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.medicalFeesContract.Code, INDsleContractType.Text, Me.medicalFeesContract.ContractName), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.medicalFeesContract.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.medicalFeesContract.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.medicalFeesContract.Code, INDsleContractType.Text, Me.medicalFeesContract.ContractName)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.medicalFeesContract.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = eActionsStatusRecords.Active, .StatusName = ResourceManager.GetString("Current"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("Suspended"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("Finished"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyMedicalFeesContract.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        ContractType = Nothing
        _idMainAccountSupplier = Nothing
        _idSupplier = Nothing
        _idThirdParty = Nothing
        SupplierDistributionLineId = Nothing
        INDsleSupplier.Properties.NullText = String.Empty
        ContractName = String.Empty
        ContractNumber = String.Empty
        InitialDate = Nothing
        EndDate = Nothing
        LastLiquidationDate = Nothing
        AutomaticDiscountForObjections = Nothing
        Observations = String.Empty
        modeModify = False
        ListMedicalFeesContractException = Nothing
        ListDeleteMedicalFeesContractException = Nothing
        INDgcExceptions.DataSource = Nothing
        INDlyItemSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemSupplier.AllowHide = True
        CleanControlsPopup()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDgcHealthProfessional.DataSource = Nothing
        INDlygHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyMedicalFeesContract.EndUpdate()

        medicalFeesContract = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With medicalFeesContract
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .ContractType = ContractType

            If INDlyItemSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SupplierId = _idSupplier
                .SupplierDistributionLineId = SupplierDistributionLineId
            Else
                .SupplierId = Nothing
                .SupplierDistributionLineId = Nothing
            End If

            .ContractName = ContractName
            .ContractNumber = ContractNumber
            .InitialDate = InitialDate
            .EndDate = EndDate
            .LastLiquidationDate = LastLiquidationDate
            .AutomaticDiscountForObjections = AutomaticDiscountForObjections
            .Observations = Observations
            If medicalFeesContract.ChangeTracker.State = ObjectState.Added Then
                .Status = 1
            End If


            If ListMedicalFeesContractException IsNot Nothing AndAlso ListMedicalFeesContractException.Count > 0 Then
                For Each itemDetail As MedicalFeesContractException In ListMedicalFeesContractException
                    .MedicalFeesContractException.Add(itemDetail)
                Next
            End If

            If ListDeleteMedicalFeesContractException IsNot Nothing AndAlso ListDeleteMedicalFeesContractException.Count > 0 Then
                For Each itemDelete As MedicalFeesContractException In ListDeleteMedicalFeesContractException
                    .MedicalFeesContractException.Add(itemDelete)
                Next
            End If

        End With

        If medicalFeesContract.Id > 0 Then
            medicalFeesContract.MarkAsModified()
        End If
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
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MMedicalFeesContract(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetMedicalFeesContract(INDbtnCode.Text.Trim)
            medicalFeesContract = resultOperation.ObjectEmbbeded
            If Not medicalFeesContract Is Nothing Then
                If medicalFeesContract.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(medicalFeesContract.Id))
                        With medicalFeesContract
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            ContractType = .ContractType
                            banSupplier = True
                            _idSupplier = .SupplierId
                            SupplierDistributionLineId = .SupplierDistributionLineId
                            INDsleSupplier.Properties.NullText = .DescriptionSupplier
                            banSupplier = False
                            ContractName = .ContractName
                            ContractNumber = .ContractNumber
                            InitialDate = .InitialDate
                            EndDate = .EndDate
                            LastLiquidationDate = .LastLiquidationDate
                            AutomaticDiscountForObjections = .AutomaticDiscountForObjections
                            Observations = .Observations

                            ListMedicalFeesContractException = .MedicalFeesContractException.ToList
                            INDgcExceptions.DataSource = Nothing
                            INDgcExceptions.DataSource = ListMedicalFeesContractException

                            If .Status = 1 Then
                                BarraBotones.StatusRecord = eActionsStatusRecords.Active
                            Else
                                BarraBotones.StatusRecord = .Status.ToString
                            End If
                            PrepareTool(.Status)

                            INDlygHealthProfessional.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            INDgcHealthProfessional.DataSource = Nothing
                            INDgcHealthProfessional.DataSource = Presenter.LoadHealthProfessionalByMedicalFeesContractId(.Id)

                        End With
                        AsyncLoader(False)
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.medicalFeesContract.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordMedicalFees With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = medicalFeesContract.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(medicalFeesContract.Id)
                        ActionsOnControls = True
                    End Using
                Else
                    'AsyncLoader(False)
                    'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    'Me.Code = String.Empty
                    If Me._sequense.IsManual Then
                        Await Me.NewMedicalFeesContract()
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Code = String.Empty
                        INDbtnCode.Focus()
                    End If
                End If
            Else
                'AsyncLoader(False)
                'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                'Me.Code = String.Empty
                If Me._sequense.IsManual Then
                    Await Me.NewMedicalFeesContract()
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    INDbtnCode.Focus()
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Prepara la barra de acciones
    ''' </summary>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Private Sub PrepareTool(state As Integer)
        BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True

        'Si el usuario no tiene permisos del formulario sale  del metodo
        If BarraBotones.PermissionsForm Is Nothing Then
            Exit Sub
        End If

        Select Case state
            Case 1
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True

                'Si el usuario tiene permiso para suspender
                If (From x In BarraBotones.PermissionsForm Where x.Key = 20 Select x).Count > 0 Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = False
                End If

                'Si el usuario tiene permiso para terminar
                If (From x In BarraBotones.PermissionsForm Where x.Key = 82 Select x).Count > 0 Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Terminar) = False
                End If
            Case 2
                'Si el usuario tiene permiso para activar
                If (From x In BarraBotones.PermissionsForm Where x.Key = 33 Select x).Count > 0 Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = False
                End If

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True

                'Si el usuario tiene permiso para terminar
                If (From x In BarraBotones.PermissionsForm Where x.Key = 82 Select x).Count > 0 Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Terminar) = False
                End If
            Case 3
                'Si el usuario tiene permiso para activar
                If (From x In BarraBotones.PermissionsForm Where x.Key = 33 Select x).Count > 0 Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = False
                End If

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Suspender) = True
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Terminar) = True
        End Select
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewMedicalFeesContract() As Task
        medicalFeesContract = New MedicalFeesContract
        medicalFeesContractGeneral = New MedicalFeesContractGeneral
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.MedicalFeesSecuenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequense.MedicalFeesSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.MedicalFeesSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
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
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState(state As Integer) As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MMedicalFeesContract(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If state = 1 Then
                        BarraBotones.StatusRecord = eActionsStatusRecords.Active
                    Else
                        BarraBotones.StatusRecord = state.ToString
                    End If

                    'PrepareTool(state)
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Deshacer()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Oculta los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsPopup()
        INDpceControlsLiquidationType.Text = "0 item seleccionado"
        Me.INDcolSelectionOption.Image = Global.Presentation.MedicalFees.My.Resources.Resources.undcheck
        If ExceptionType <> Nothing Then
            Select Case ExceptionType
                Case 1 'IPSService
                    INDlyItemControlsLiquidationType.Text = "Servicio IPS"
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 2 'CUPS
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 3 'SubGrupo CUPS
                    INDlyItemControlsLiquidationType.Text = "SubGrupo CUPS"
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 4 'Grupo CUPS
                    INDlyItemControlsLiquidationType.Text = "Grupo CUPS"
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 5 'Grupo Atencion
                    INDlyItemControlsLiquidationType.Text = "Grupo Atención"
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 6 'Entidad
                    INDlyItemControlsLiquidationType.Text = "Entidad Contrato"
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 7 'Tipo Manual Tarifario
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Case 8 'General
                    INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End Select
        Else
            INDlyItemControlsLiquidationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemCupsEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRateManualType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        _sequense = Nothing
        medicalFeesContract = Nothing
        medicalFeesContractGeneral = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
        ListContractType = Nothing
        ListRateType = Nothing
        ListExceptionType = Nothing
        ListRateManualType = Nothing
        ListMedicalFeesContractException = Nothing
        ListDeleteMedicalFeesContractException = Nothing
        modeModify = Nothing
        _listCompare = Nothing
        _medicalFeesContractException = Nothing
        banSupplier = Nothing
        _idMainAccountSupplier = Nothing
        _idSupplier = Nothing
        _idThirdParty = Nothing
        SearchMode = Nothing
        bw = Nothing
        ListXpCollection = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesContract_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyMedicalFeesContract, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PMedicalFeesContract(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        InitializeSearch()
        INDsleContractType.Properties.Buttons.Item(1).Visible = False
        INDsleExceptionType.Properties.Buttons.Item(1).Visible = False
        INDsleRateManualType.Properties.Buttons.Item(1).Visible = False
        INDsleRateTypePopup.Properties.Buttons.Item(1).Visible = False
        IndigoGridControl1.RefreshGrid(INDgcExceptions)
        IndigoGridControl1.RefreshGrid(INDgcHealthProfessional)
        Presenter.InitializeHealthProfessional()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewGridExceptions, ListActions)

        Dim ListActionsControlsLiquidation As New List(Of eAcciones)
        ListActionsControlsLiquidation.Add(eAcciones.CheckOptions)
        ListActionsControlsLiquidation.Add(eAcciones.UnCheckOptions)
        IndigoGridView2.SetListAcction(viewControlsLiquidationType, ListActionsControlsLiquidation)

        SearchMode = False
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmMedicalFeesContract_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
        '        Await Me.NewMedicalFeesContract()
        '    Else
        '        Await Me.LoadControls()
        '    End If
        'End If
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.NewMedicalFeesContract()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter al control de popup de excepciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceExceptions_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceExceptions.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceExceptions.ShowPopup()
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
    Private Sub FrmMedicalFeesContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del mas para abrir el form de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSService_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmIPSService With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeIPSService()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del mas en el control de manual tarifario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManual_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRateManualPopup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmRateManual With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeRateManual()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del mas en el control de cups entity
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCUPSEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCupsEntity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCUPSEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del mas del control de grupo de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCareGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCareGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del mas del control de entidad de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmContractEntity With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeContractEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del mas del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del mas del control de grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleGroupCUPS_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCupsGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeGroupCUPS()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton del mas del control de subgrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSubGroupCUPS_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCupsSubGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSubgroupCUPS()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la entidad cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCUPSEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCUPSEntity.QueryPopUp
        If CUPSEntityXpo Is Nothing Then
            Presenter.InitializeCUPSEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de servicio ips
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIPSService_QueryPopUp(sender As Object, e As CancelEventArgs)
        If IPSServiceXpo Is Nothing Then
            Presenter.InitializeIPSService()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de grupo de atencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCareGroup_QueryPopUp(sender As Object, e As CancelEventArgs)
        If CareGroupXpo Is Nothing Then
            Presenter.InitializeCareGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de entidad de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractEntity_QueryPopUp(sender As Object, e As CancelEventArgs)
        If ContractEntityXpo Is Nothing Then
            Presenter.InitializeContractEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de manual tarifario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateManualPopup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRateManualPopup.QueryPopUp
        If RateManualXpoPopup Is Nothing Then
            Presenter.InitializeRateManual()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplier.QueryPopUp
        If SupplierDistributionLineXpo Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de subgrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSubGroupCUPS_QueryPopUp(sender As Object, e As CancelEventArgs)
        If CUPSSubgroupXpo Is Nothing Then
            Presenter.InitializeSubgroupCUPS()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleGroupCUPS_QueryPopUp(sender As Object, e As CancelEventArgs)
        If CUPSGroupXpo Is Nothing Then
            Presenter.InitializeGroupCUPS()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleContractType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleContractType.EditValueChanged
        If ContractType IsNot Nothing Then
            If ContractType = 1 Then
                INDlyItemSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSupplier.AllowHide = True
            Else
                INDlyItemSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSupplier.AllowHide = False
            End If
        Else
            INDlyItemSupplier.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemSupplier.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDate.EditValueChanged
        If InitialDate IsNot Nothing Then
            INDdteEndDate.Properties.MinValue = InitialDate
            INDdteLastLiquidationDate.Properties.MinValue = InitialDate
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha final
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdteEndDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteEndDate.EditValueChanged
        If EndDate IsNot Nothing Then
            INDdteLastLiquidationDate.Properties.MaxValue = EndDate
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo de tarifa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRateTypePopup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRateTypePopup.EditValueChanged
        If RateTypePopup <> Nothing Then
            Select Case RateTypePopup
                Case 1
                    INDlyItemPercentageRatePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemRateManualPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateVariationPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAmountPayablePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 2
                    INDlyItemPercentageRatePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemRateVariationPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAmountPayablePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 3
                    INDlyItemPercentageRatePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateManualPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemRateVariationPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAmountPayablePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End Select
        Else
            INDlyItemPercentageRatePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRateManualPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRateVariationPopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemAmountPayablePopup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        RezizablePopup()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo excepcion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleExceptionType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleExceptionType.EditValueChanged
        HideControlsPopup()
        HideColumnsOfGridControlsLiquidationType()
        bw.RunWorkerAsync()
        RezizablePopup()
    End Sub

    Private Sub INDsleCUPSEntity_EditValueChanged(sender As Object, e As EventArgs)
        If CUPSEntityId <> Nothing AndAlso modeModify = True Then
            GenerateListCompareChanged()
        End If
    End Sub

    Private Sub INDsleCareGroup_EditValueChanged(sender As Object, e As EventArgs)
        If CareGroupId <> Nothing AndAlso modeModify = True Then
            GenerateListCompareChanged()
        End If
    End Sub

    Private Sub INDsleContractEntity_EditValueChanged(sender As Object, e As EventArgs)
        If ContractEntityId <> Nothing AndAlso modeModify = True Then
            GenerateListCompareChanged()
        End If
    End Sub

    Private Sub INDsleRateManualType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRateManualType.EditValueChanged
        If RateManualType <> Nothing AndAlso modeModify = True Then
            GenerateListCompareChanged()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplier.EditValueChanged
        If SupplierDistributionLineId > 0 Then
            If banSupplier = False Then
                Dim suppplierMainAccount = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.CommonSuppliersDistibutionLineXpo)
                _idMainAccountSupplier = suppplierMainAccount.IdDistributionLine.IdMainAccount.Id
                _idSupplier = suppplierMainAccount.IdSupplier.Id
                _idThirdParty = suppplierMainAccount.IdSupplier.IdThirdParty.Id
            End If
        Else
            _idMainAccountSupplier = Nothing
            _idSupplier = Nothing
            _idThirdParty = Nothing
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio check
    ''' de la rejilla de cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e.NewValue Then
            Dim cont = (From x In CUPSEntityXpo Where x.SelectOption = True Select x).Count
            cont = cont + 1
            INDsleCUPSEntity.Text = cont.ToString + " item seleccionado"
        Else
            Dim cont = (From x In CUPSEntityXpo Where x.SelectOption = True Select x).Count
            If cont > 0 Then
                cont = cont - 1
            End If
            INDsleCUPSEntity.Text = cont.ToString + " item seleccionado"
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del repositorio check de la rejilla
    ''' de los controles de tipo de liquidacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckControlsLiquidationType_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckControlsLiquidationType.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Dim item = viewControlsLiquidationType.GetFocusedRow()
            item.SelectOption = e.NewValue
            Dim cont = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
            INDpceControlsLiquidationType.Text = cont.ToString + " item seleccionado"
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceExceptions_Popup(sender As Object, e As EventArgs) Handles INDpceExceptions.Popup
        INDsleExceptionType.Focus()
        If RateTypePopup > 0 Then
            INDsleRateTypePopup.Focus()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddException_Click(sender As Object, e As EventArgs) Handles INDbtnAddException.Click
        AddException()
    End Sub

#End Region

#Region "MenuContextual"

#Region "ContextMenu ViewException"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString()
            Case "Remove"
                DeleteException()
            Case "Edit"
                EditException()
        End Select
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditException()
            Case "Remove"
                DeleteException()
        End Select
    End Sub

#End Region

#Region "ContextMenu ViewControlsLiquidationType"

    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "CheckOptions"
                CheckOrUnCheckOptions(True)
            Case "UnCheckOptions"
                CheckOrUnCheckOptions(False)
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que selecciona o deselecciona
    ''' los items de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckOrUnCheckOptions(optionCheck As Boolean)
        Dim rowSelectCount = viewControlsLiquidationType.SelectedRowsCount
        If rowSelectCount > 0 Then
            For i = 0 To rowSelectCount - 1
                If viewControlsLiquidationType.GetSelectedRows()(i) >= 0 Then
                    Dim row = viewControlsLiquidationType.GetRow(viewControlsLiquidationType.GetSelectedRows()(i))
                    row.SelectOption = optionCheck
                End If
            Next
            INDgcControlsLiquidationType.RefreshDataSource()
            Dim cont = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
            INDpceControlsLiquidationType.Text = cont.ToString + " item seleccionado"
        End If
    End Sub

#End Region

#End Region

#Region "PopupMenuShowing"

    Private Sub viewGridCupsEntity_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles viewGridCupsEntity.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            Dim view = CType(sender, GridView)
            INDbarButtonSelectAll.Caption = "Seleccionar"
            INDbarButtonUnSelectAll.Caption = "Quitar Selección"
            PopupMenuActions.Manager = BarManager
            PopupMenuActions.ShowPopup(view.GridControl.PointToScreen(e.Point))
        End If
    End Sub

#End Region

#Region "ItemClick"

    ''' <summary>
    ''' Seleccionar todo el grupo o subGrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonSelectAll.ItemClick
        SelectOptions(1)
    End Sub

    ''' <summary>
    ''' Quitar seleccion de todo el grupo o subGrupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbarButtonUnSelectAll_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbarButtonUnSelectAll.ItemClick
        SelectOptions(0)
    End Sub

#End Region

#Region "MouseDoubleClick"

    ''' <summary>
    ''' Evento que se dispara al presionar docleClick sobre el check de la cabecera
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcControlsLiquidationType_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcControlsLiquidationType.MouseDoubleClick
        If ListXpCollection IsNot Nothing AndAlso ListXpCollection.Count > 0 Then
            Dim hitPoint = Me.viewControlsLiquidationType.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelectionOption") Then

                    Dim listFilterXpCollection = viewControlsLiquidationType.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.MedicalFees.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        Me.INDcolSelectionOption.Image = Global.Presentation.MedicalFees.My.Resources.Resources.check
                    End If
                    Me.INDgcControlsLiquidationType.RefreshDataSource()
                    Dim contItems = (From x In ListXpCollection Where x.SelectOption = True Select x).Count
                    INDpceControlsLiquidationType.Text = contItems.ToString + " item seleccionado"
                    Me.INDgcControlsLiquidationType.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState(1)
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
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.MedicalFeesSecuenceDetail IsNot Nothing Then
            If Me._sequense.MedicalFeesSecuenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.MedicalFeesSecuenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Click terminar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_Terminar() Handles BarraBotones.Click_Terminar
        Await ChangeState(3)
    End Sub

    ''' <summary>
    ''' Click Suspender
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickSuspender() Handles BarraBotones.ClickSuspender
        Await ChangeState(2)
    End Sub

#End Region

End Class