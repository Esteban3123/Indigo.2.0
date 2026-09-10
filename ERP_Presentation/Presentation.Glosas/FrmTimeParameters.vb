'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Juan F. Tamayo
' Created          : 2013-04-16
'
' Last Modified By : Juan Diego Diaz M.
' Last Modified On : 2014-20-01
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports System.ComponentModel
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Formulario Parametros de Tiempo
''' </summary>
Public Class FrmTimeParameters
    Implements ITimeParameters



#Region "Fields"

    ''' <summary>
    ''' Objeto del conjunto de parametros
    ''' </summary>
    Private _timeParameters As Domain.Entities.TimeParameters
    ''' <summary>
    ''' Objeto del conjunto de parametros Auxiliar
    ''' </summary>
    Private _timeParametersAux As Domain.Entities.TimeParameters
    ''' <summary>
    ''' Lista de conjunto de parametros
    ''' </summary>
    Private _listTimeParameters As List(Of Domain.Entities.TimeParameters)
    ''' <summary>
    ''' Lista de conjunto de parametros
    ''' </summary>
    Private _listConcetpGlosas As List(Of Domain.Entities.ConceptGlosas)
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PTimeParameters
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues
    ''' <summary>
    ''' Encapsula el cliente consultado
    ''' </summary> 
    Private _customer As Domain.Entities.Customer
    ''' <summary>
    ''' Listado para el repositorio de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDevolutionInjustificate As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
#End Region

#Region "Properties"


    ''' <summary>
    ''' Obtiene o asigna el objeto que encapsula los parametros de tiempo en el modulo de glosas
    ''' </summary>
    ''' <value>Parametros de tiempo</value>
    ''' <returns>Los parametros de tiempo</returns>
    Public Property TimeParameters As TimeParameters Implements ITimeParameters.TimeParameters
        Get
            Return Me._timeParameters
        End Get
        Set(value As TimeParameters)
            If value IsNot Nothing Then
                Me._timeParameters = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el toma el sabado como día laboral
    ''' </summary>
    ''' <value>Valor</value>
    ''' <returns>El valor</returns>
    Public Property IsDaybusinessSaturday As Boolean Implements ITimeParameters.IsDaybusinessSaturday
        Get
            Return Convert.ToBoolean(Me.INDrgpIsDaybusinessSaturday.EditValue)
        End Get
        Set(value As Boolean)
            Me.INDrgpIsDaybusinessSaturday.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el toma el domingo como día laboral
    ''' </summary>
    ''' <value>Valor</value>
    ''' <returns>El valor</returns>
    Public Property IsDaybusinessSunday As Boolean Implements ITimeParameters.IsDaybusinessSunday
        Get
            Return Convert.ToBoolean(Me.INDrgpIsDaybusinessSunday.EditValue)
        End Get
        Set(value As Boolean)
            Me.INDrgpIsDaybusinessSunday.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para realizar el proceso de conciliación
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeConciliation As Integer Implements ITimeParameters.MaxTimeConciliation
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeConciliation.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeConciliation.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para realizar el proceso de cobro juridico
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeJuridicalDebit As Integer Implements ITimeParameters.MaxTimeJuridicalDebit
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeJuridicalDebit.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeJuridicalDebit.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una glosa extemporanea
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeResponseExObjection As Integer Implements ITimeParameters.MaxTimeResponseExObjection
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeResponseExObjection.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeResponseExObjection.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una reiteración extemporanea
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeResponseExReiteration As Integer Implements ITimeParameters.MaxTimeResponseExReiteration
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeResponseExReiteration.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeResponseExReiteration.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una glosa
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeResponseObjection As Integer Implements ITimeParameters.MaxTimeResponseObjection
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeResponseObjection.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeResponseObjection.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una reiteración
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeResponseReiteration As Integer Implements ITimeParameters.MaxTimeResponseReiteration
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeResponseReiteration.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeResponseReiteration.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para enviar el oficio de respuesta a una glosa
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeSendResponseObjection As Integer Implements ITimeParameters.MaxTimeSendResponseObjection
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeSendResponseObjection.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeSendResponseObjection.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para enviar el oficio de respuesta a una reiteración
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property MaxTimeSendResponseReiteration As Integer Implements ITimeParameters.MaxTimeSendResponseReiteration
        Get
            Return Convert.ToInt32(Me.INDspeMaxTimeSendResponseReiteration.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeMaxTimeSendResponseReiteration.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tiempo en días para realizar las notificaciones
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Public Property NotificationPeriodicity As Integer Implements ITimeParameters.NotificationPeriodicity
        Get
            Return Convert.ToInt32(Me.INDspeNotificationPeriodicity.EditValue)
        End Get
        Set(value As Integer)
            If value >= 0 Then
                Me.INDspeNotificationPeriodicity.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Icono segun el tipo de mensaje</param>
    ''' <value>Mensaje a registrar</value>
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

    ''' <summary>
    ''' Asigna un valor que indica que se esta realizando una operacion asincrona
    ''' </summary>
    ''' <value>Valor</value>
    Public WriteOnly Property AsyncOperation As Boolean Implements ITimeParameters.AsyncOperation
        Set(value As Boolean)
            Me.AsyncLoader(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si afecta o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AffectedService As Boolean? Implements ITimeParameters.AffectedService
        Get
            Return INDsleAffectedService.EditValue
        End Get
        Set(value As Boolean?)
            INDsleAffectedService.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GeneralGlossConceptNoteId As Integer? Implements ITimeParameters.GeneralGlossConceptNoteId
        Get
            Return INDsleGeneralGlossConceptNoteId.EditValue
        End Get
        Set(value As Integer?)
            INDsleGeneralGlossConceptNoteId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property GeneralGlossConceptNoteIdXpo As XPInstantFeedbackSource Implements ITimeParameters.GeneralGlossConceptNoteIdXpo
        Get
            Return INDsleGeneralGlossConceptNoteId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleGeneralGlossConceptNoteId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConciliationJournalVoucherTypeId As Integer? Implements ITimeParameters.ConciliationJournalVoucherTypeId
        Get
            Return INDsleConciliationJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleConciliationJournalVoucherTypeId.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConciliationJournalVoucherTypeIdXpo As XPInstantFeedbackSource Implements ITimeParameters.ConciliationJournalVoucherTypeIdXpo
        Get
            Return INDsleConciliationJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleConciliationJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DetailedGlossConceptNoteId As Integer? Implements ITimeParameters.DetailedGlossConceptNoteId
        Get
            Return INDsleDetailedGlossConceptNoteId.EditValue
        End Get
        Set(value As Integer?)
            INDsleDetailedGlossConceptNoteId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Establece el datasource del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DetailedGlossConceptNoteIdXpo As XPInstantFeedbackSource Implements ITimeParameters.DetailedGlossConceptNoteIdXpo
        Get
            Return INDsleDetailedGlossConceptNoteId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDetailedGlossConceptNoteId.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DevolutionJournalVoucherTypeId As Integer? Implements ITimeParameters.DevolutionJournalVoucherTypeId
        Get
            Return INDsleDevolutionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleDevolutionJournalVoucherTypeId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DevolutionJournalVoucherTypeIdXpo As XPInstantFeedbackSource Implements ITimeParameters.DevolutionJournalVoucherTypeIdXpo
        Get
            Return INDsleDevolutionJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDevolutionJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el id concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreviousLifetimesConceptNoteId As Integer? Implements ITimeParameters.PreviousLifetimesConceptNoteId
        Get
            Return INDslePreviousLifetimesConceptNoteId.EditValue
        End Get
        Set(value As Integer?)
            INDslePreviousLifetimesConceptNoteId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Establece el datasource del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PreviousLifetimesConceptNoteIdXpo As XPInstantFeedbackSource Implements ITimeParameters.PreviousLifetimesConceptNoteIdXpo
        Get
            Return INDslePreviousLifetimesConceptNoteId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePreviousLifetimesConceptNoteId.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReceptionObjectionJournalVoucherTypeId As Integer? Implements ITimeParameters.ReceptionObjectionJournalVoucherTypeId
        Get
            Return INDsleReceptionObjectionJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleReceptionObjectionJournalVoucherTypeId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReceptionObjectionJournalVoucherTypeIdXpo As XPInstantFeedbackSource Implements ITimeParameters.ReceptionObjectionJournalVoucherTypeIdXpo
        Get
            Return INDsleReceptionObjectionJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReceptionObjectionJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferLegalJournalVoucherTypeId As Integer? Implements ITimeParameters.TransferLegalJournalVoucherTypeId
        Get
            Return INDsleTransferLegalJournalVoucherTypeId.EditValue
        End Get
        Set(value As Integer?)
            INDsleTransferLegalJournalVoucherTypeId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Establece el tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferLegalJournalVoucherTypeIdXpo As XPInstantFeedbackSource Implements ITimeParameters.TransferLegalJournalVoucherTypeIdXpo
        Get
            Return INDsleTransferLegalJournalVoucherTypeId.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTransferLegalJournalVoucherTypeId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si determina si la factura sale del radicado inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DevolutionInjustificate As Integer? Implements ITimeParameters.DevolutionInjustificate
        Get
            Return INDsleDevolutionInjustificate.EditValue
        End Get
        Set(value As Integer?)
            INDsleDevolutionInjustificate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o Asigna el ID del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdThirdParty As Integer? Implements ITimeParameters.IdThirdParty
        Get
            Return INDsleThird.EditValue
        End Get
        Set(value As Integer?)
            INDsleThird.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Asigna la lista de terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceThird As DevExpress.Xpo.XPInstantFeedbackSource Implements ITimeParameters.DataSourceThird
        Get
            Return Me.INDsleThird.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            Me.INDsleThird.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el ID del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer? Implements ITimeParameters.IdCostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Asigna lista de centros de costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceCostCenter As DevExpress.Xpo.XPInstantFeedbackSource Implements ITimeParameters.DataSourceCostCenter
        Get
            Return Me.INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            Me.INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Metodo para abrir formulario de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        ' Me.CleanControls()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
            .ValorSolicitado = "Nit"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
            .FormParent = Me
            .ShowSearch(False)
        End With
    End Sub


    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        If Not String.IsNullOrEmpty(ReturnValue) AndAlso ReturnObject IsNot Nothing Then
            Dim ctomer = ReturnObject
            Me._customer = New Domain.Entities.Customer With {.Id = ctomer.Id, .Nit = ctomer.Nit.ToString().Trim(), .Name = ctomer.Name.ToString().Trim(), .State = ctomer.State}
            Me.INDEntityBte.Text = ReturnValue.Trim()
            Me.INDEntityTxt.Text = ReturnObject.Name.ToString().Trim()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para buscar
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        Me.AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Deshace los cambios realizados
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.CleanControls()
    End Sub

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        Me._customer = Nothing
        DevolutionInjustificate = Nothing
        AffectedService = Nothing
        GeneralGlossConceptNoteId = Nothing
        INDsleGeneralGlossConceptNoteId.Properties.NullText = String.Empty
        DetailedGlossConceptNoteId = Nothing
        INDsleDetailedGlossConceptNoteId.Properties.NullText = String.Empty
        PreviousLifetimesConceptNoteId = Nothing
        INDslePreviousLifetimesConceptNoteId.Properties.NullText = String.Empty
        INDrgGroupAccounting.EditValue = Nothing
        INDRgDecimales.EditValue = Nothing
        ReceptionObjectionJournalVoucherTypeId = Nothing
        INDsleReceptionObjectionJournalVoucherTypeId.Properties.NullText = String.Empty
        ConciliationJournalVoucherTypeId = Nothing
        INDsleConciliationJournalVoucherTypeId.Properties.NullText = String.Empty
        DevolutionJournalVoucherTypeId = Nothing
        INDsleDevolutionJournalVoucherTypeId.Properties.NullText = String.Empty
        TransferLegalJournalVoucherTypeId = Nothing
        INDsleTransferLegalJournalVoucherTypeId.Properties.NullText = String.Empty
        IdThirdParty = Nothing
        INDsleThird.Properties.NullText = String.Empty
        INDsleThird.EditValue = Nothing
        IdCostCenter = Nothing
        INDsleCostCenter.Properties.NullText = String.Empty
        INDsleCostCenter.EditValue = Nothing
        ObjConceptNotePreviousLifetimes = New PortfolioNoteConcept
        Me.INDEntityBte.Focus()
    End Sub

    ''' <summary>
    ''' No aplica en este contexto
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Guarda o actualiza los datos modificados
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Try
            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                If ValidateControls() = False Then
                    Exit Sub
                End If
            End If

            If INDspeMaxTimeSendResponseObjection.EditValue < INDspeMaxTimeResponseObjection.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El tiempo de Envio de respuesta " & INDspeMaxTimeSendResponseObjection.EditValue & " días, debe ser mayor al tiempo de respuesta " & INDspeMaxTimeResponseObjection.EditValue & " días"
                Me.INDspeMaxTimeSendResponseObjection.Focus()
                Exit Sub
            End If

            If INDspeMaxTimeSendResponseReiteration.EditValue < INDspeMaxTimeResponseReiteration.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El tiempo de Envio de respuesta reiteración " & INDspeMaxTimeSendResponseReiteration.EditValue & " días debe ser mayor al tiempo de respuesta de reiteración " & INDspeMaxTimeResponseReiteration.EditValue & " días"
                Me.INDspeMaxTimeSendResponseReiteration.Focus()
                Exit Sub
            End If

            Me.AssignValues()
            If Me._timeParameters.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged _
                OrElse (Me._listTimeParameters.Count > 0 AndAlso Me._listTimeParameters.Exists(Function(x) x.ChangeTracker.State = ObjectState.Added OrElse x.ChangeTracker.State = ObjectState.Modified)) Then
                Using model As New MTimeParameters(Me.Tag)
                    Me.AsyncLoader(True)
                    Dim result = Await model.SaveListTimeParameters(Me._listTimeParameters, Me._listConcetpGlosas)
                    If result.StateResult Then
                        Me._listTimeParameters = result.ObjectEmbbeded
                        Dim ListAux = Await model.ListTimeParameters(_idOperativeUnit)
                        For Each item In Me._listTimeParameters
                            If item.ChangeTracker.State = ObjectState.Added Then
                                Me._doc = Nothing
                                Me._timeParametersAux = item
                                If ListAux.Count > 0 Then
                                    Me._timeParametersAux.Customer = ListAux.Where(Function(x) x.Id = item.Id).FirstOrDefault.Customer
                                End If
                                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            ElseIf item.ChangeTracker.State = ObjectState.Modified Then
                                Me.GetDocumentIndexed(Me.Tag & "_" & item.Id)
                                Me._timeParametersAux = item
                                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            End If
                        Next
                        Me._listTimeParameters = ListAux
                        Me._timeParameters = Await model.GetTimeParameters("0", _idOperativeUnit)
                        Me.INDExceptionsGc.DataSource = Me._listTimeParameters.Where(Function(x) x.IdCustomer IsNot Nothing).ToList
                        Me.INDExceptionsGv.RefreshData()
                        Me._timeParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged
                        Me.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado, Eform.Comunes)
                    Else
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia, Eform.Comunes)
                        Else
                            Me.Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesContacteAdministrador, Eform.Comunes)
                        End If
                    End If
                    Me.AsyncLoader(False)
                End Using
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para limpiar datos del grupo excepciones
    ''' </summary>
    Sub CleanDataExceptions()
        Me._customer = Nothing
        Me.INDEntityBte.Text = String.Empty
        Me.INDEntityTxt.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Overloads Function ValidateControls() As Boolean
        Dim res = Me.LayoutControls.ValidateFields()
        If Not res.ResultStatus Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), res.ToString())
            Return False
        Else
            Return True
        End If
        'If Me.INDrgpIsDaybusinessSaturday.EditValue Is Nothing Then
        '    ValidateControls = False
        'End If
        'If Me.INDrgpIsDaybusinessSunday.EditValue Is Nothing Then
        '    ValidateControls = False
        'End If
    End Function

    ''' <summary>
    ''' Permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    ''' <param name="existeDatos">Valor que indica si existen datos para actualizar</param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' No aplica en este contexto
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Oculta y muestra los controles correspondientes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControlsGroup()
        If _indigoSessionValues.IndigoCompanyType = 1 Then
            INDlyItemConciliationJournalVoucherTypeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemConciliationJournalVoucherTypeId.AllowHide = False

            INDlyItemTransferLegalJournalVoucherTypeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemTransferLegalJournalVoucherTypeId.AllowHide = False
        Else
            INDlyItemConciliationJournalVoucherTypeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemConciliationJournalVoucherTypeId.AllowHide = True

            INDlyItemTransferLegalJournalVoucherTypeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemTransferLegalJournalVoucherTypeId.AllowHide = True
        End If
    End Sub


    ''' <summary>
    ''' Asigna los valores de los controles a la entidad
    ''' </summary>
    Private Sub AssignValues()
        With _timeParameters
            .IdOperatingUnit = _idOperativeUnit
            If .MaxTimeResponse <> Me.MaxTimeResponseObjection Then
                .MaxTimeResponse = Me.MaxTimeResponseObjection
            End If
            If .MaxTimeSendingDocumentResponse <> Me.MaxTimeSendResponseObjection Then
                .MaxTimeSendingDocumentResponse = Me.MaxTimeSendResponseObjection
            End If
            If .MaxTimeExtemporaneousGlosa <> Me.MaxTimeResponseExObjection Then
                .MaxTimeExtemporaneousGlosa = Me.MaxTimeResponseExObjection
            End If
            If .MaxTimeExtemporaneousReiteration <> Me.MaxTimeResponseExReiteration Then
                .MaxTimeExtemporaneousReiteration = Me.MaxTimeResponseExReiteration
            End If
            If .MaxTimeSendingReiterationDocumentResponse <> Me.MaxTimeSendResponseReiteration Then
                .MaxTimeSendingReiterationDocumentResponse = Me.MaxTimeSendResponseReiteration
            End If
            If .MaxTimeReiterationResponse <> Me.MaxTimeResponseReiteration Then
                .MaxTimeReiterationResponse = Me.MaxTimeResponseReiteration
            End If
            If .MaxTimeConciliation <> Me.MaxTimeConciliation Then
                .MaxTimeConciliation = Me.MaxTimeConciliation
            End If
            If .MaxTimeJuridicalDebtCollectionStart <> Me.MaxTimeJuridicalDebit Then
                .MaxTimeJuridicalDebtCollectionStart = Me.MaxTimeJuridicalDebit
            End If
            If .NotificationPeriodicity <> Me.NotificationPeriodicity Then
                .NotificationPeriodicity = Me.NotificationPeriodicity
            End If
            If .IsDaybusinessSaturday <> Me.IsDaybusinessSaturday Then
                .IsDaybusinessSaturday = Me.IsDaybusinessSaturday
            End If
            If .IsDaybusinessSunday <> Me.IsDaybusinessSunday Then
                .IsDaybusinessSunday = Me.IsDaybusinessSunday
            End If
            If Me._customer IsNot Nothing AndAlso Me._timeParameters.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                Me._timeParameters.Customer = Nothing
                Me._timeParameters.IdCustomer = Me._customer.Id
            End If
            If Not Me._listTimeParameters.Exists(Function(x) x.Id = Me._timeParameters.Id) Then
                Me._listTimeParameters.Add(Me._timeParameters)
            End If

            If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                Me._timeParameters.DiscountedMedicalFees = IndChkMedicalFees.EditValue
                .DevolutionInjustificate = DevolutionInjustificate
                .AffectedService = AffectedService
                If INDlyItemGeneralGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .GeneralGlossConceptNoteId = GeneralGlossConceptNoteId
                Else
                    .GeneralGlossConceptNoteId = Nothing
                End If
                If INDlyItemDetailedGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .DetailedGlossConceptNoteId = DetailedGlossConceptNoteId
                Else
                    .DetailedGlossConceptNoteId = Nothing
                End If
                .PreviousLifetimesConceptNoteId = PreviousLifetimesConceptNoteId
                ' .RadicationJournalVoucherTypeId = RadicationJournalVoucherTypeId
                .GroupAccountingVoucher = INDrgGroupAccounting.EditValue
                .ManageDecimals = INDRgDecimales.EditValue
                .ReceptionObjectionJournalVoucherTypeId = ReceptionObjectionJournalVoucherTypeId
                If INDlyItemConciliationJournalVoucherTypeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .ConciliationJournalVoucherTypeId = ConciliationJournalVoucherTypeId
                Else
                    .ConciliationJournalVoucherTypeId = Nothing
                End If
                .DevolutionJournalVoucherTypeId = DevolutionJournalVoucherTypeId

                If INDlyItemTransferLegalJournalVoucherTypeId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .TransferLegalJournalVoucherTypeId = TransferLegalJournalVoucherTypeId
                Else
                    .TransferLegalJournalVoucherTypeId = Nothing
                End If

                If INDlyiThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PreviousLifetimesThirdPartyId = IdThirdParty
                Else
                    .PreviousLifetimesThirdPartyId = Nothing
                End If

                If INDlyiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PreviousLifetimesCostCenterId = IdCostCenter
                Else
                    .PreviousLifetimesCostCenterId = Nothing
                End If
            End If

        End With
    End Sub

    ''' <summary>
    ''' Metodo para cargar controles
    ''' </summary>
    Async Sub LoadControls()
        Try
            Using model As New MTimeParameters(Me.Tag)
                AsyncLoader(True)
                Me._listTimeParameters = New List(Of TimeParameters)
                Me._listTimeParameters = Await model.ListTimeParameters(_idOperativeUnit)
                Me.INDExceptionsGc.DataSource = Me._listTimeParameters.Where(Function(x) x.IdCustomer IsNot Nothing).ToList
                Me._timeParameters = Await model.GetTimeParameters("0", _idOperativeUnit)
                AsyncLoader(False)
                If Me.TimeParameters.Id = 0 Then
                    Me.BarraBotones.DisableBarDocument()
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.BarBtnDocumentos.Enabled = False
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
                    Me.BarraBotones.SetDocuments(Me._timeParameters.Id)
                End If
            End Using
            If Me._timeParameters IsNot Nothing Then
                With _timeParameters
                    'Logica para que muestre o no muestre decimales.
                    if .ManageDecimals  = False

                    End If

                    Me.INDspeMaxTimeResponseExObjection.EditValue = .MaxTimeExtemporaneousGlosa
                    Me.INDspeMaxTimeSendResponseObjection.EditValue = .MaxTimeSendingDocumentResponse
                    Me.INDspeMaxTimeResponseObjection.EditValue = .MaxTimeResponse
                    Me.INDspeMaxTimeResponseExReiteration.EditValue = .MaxTimeExtemporaneousReiteration
                    Me.INDspeMaxTimeSendResponseReiteration.EditValue = .MaxTimeSendingReiterationDocumentResponse
                    Me.INDspeMaxTimeResponseReiteration.EditValue = .MaxTimeReiterationResponse
                    Me.INDspeMaxTimeConciliation.EditValue = .MaxTimeConciliation
                    Me.INDspeMaxTimeJuridicalDebit.EditValue = .MaxTimeJuridicalDebtCollectionStart
                    Me.INDspeNotificationPeriodicity.EditValue = .NotificationPeriodicity
                    Me.INDrgpIsDaybusinessSaturday.EditValue = .IsDaybusinessSaturday
                    Me.INDrgpIsDaybusinessSunday.EditValue = .IsDaybusinessSunday
                    If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                        AffectedService = .AffectedService
                        GeneralGlossConceptNoteId = .GeneralGlossConceptNoteId
                        INDsleGeneralGlossConceptNoteId.Properties.NullText = .GeneralGlossConceptNoteDescription
                        DetailedGlossConceptNoteId = .DetailedGlossConceptNoteId
                        INDsleDetailedGlossConceptNoteId.Properties.NullText = .DetailedGlossConceptNoteDescription
                        PreviousLifetimesConceptNoteId = .PreviousLifetimesConceptNoteId
                        INDslePreviousLifetimesConceptNoteId.Properties.NullText = .PreviousLifetimesConceptNoteDescription
                        INDrgGroupAccounting.EditValue = .GroupAccountingVoucher
                        INDRgDecimales.EditValue = .ManageDecimals
                        ReceptionObjectionJournalVoucherTypeId = .ReceptionObjectionJournalVoucherTypeId
                        INDsleReceptionObjectionJournalVoucherTypeId.Properties.NullText = .ReceptionObjectionJournalVoucherTypeDescription
                        ConciliationJournalVoucherTypeId = .ConciliationJournalVoucherTypeId
                        INDsleConciliationJournalVoucherTypeId.Properties.NullText = .ConciliationJournalVoucherTypeDescription
                        DevolutionJournalVoucherTypeId = .DevolutionJournalVoucherTypeId
                        INDsleDevolutionJournalVoucherTypeId.Properties.NullText = .DevolutionJournalVoucherTypeDescription
                        TransferLegalJournalVoucherTypeId = .TransferLegalJournalVoucherTypeId
                        INDsleTransferLegalJournalVoucherTypeId.Properties.NullText = .TransferLegalJournalVoucherTypeDescription
                        INDsleThird.Properties.NullText = .CodeNamethird
                        IdThirdParty = .PreviousLifetimesThirdPartyId
                        INDsleCostCenter.Properties.NullText = .CodeNameCostCenter
                        IdCostCenter = .PreviousLifetimesCostCenterId
                        Me.IndChkMedicalFees.EditValue = .DiscountedMedicalFees
                        DevolutionInjustificate = .DevolutionInjustificate
                        Me.INDsleReceptionObjectionJournalVoucherTypeId.Focus()
                    Else
                        Me.INDspeMaxTimeResponseExObjection.Focus()
                    End If
                End With
            Else
                Me._timeParameters = New TimeParameters
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para refrescar lista de parametros
    ''' </summary>
    Sub RefreshListTimeParameters()
        Me._listTimeParameters.Remove(Me._timeParametersAux)
        Me.INDExceptionsGc.DataSource = Me._listTimeParameters.Where(Function(x) x.IdCustomer IsNot Nothing).ToList
        Me.INDExceptionsGv.RefreshData()
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        Dim NameCustomer = ""
        If Me._timeParametersAux.Customer Is Nothing Then
            NameCustomer = "Por Defecto"
        Else
            NameCustomer = Me._timeParametersAux.Customer.Name
        End If
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmTimeParametersMetaData, Eform.InfoMetaData), Me._timeParametersAux.Id, NameCustomer), .CreationDate = dateServer, .CreationUser = Me._indigoSessionValues.UserIndigo & "-" & Me._indigoSessionValues.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity =  "$#" & Me.Tag & "_" & Me._timeParametersAux.Id & "#$", .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(Eresources.FrmTimeParametersMetaDataTitle, Eform.InfoMetaData), Me._timeParametersAux.Id), .Update = dateServer, .UpdateUser = Me._indigoSessionValues.UserIndigo & "-" & Me._indigoSessionValues.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSessionValues.UserIndigo & "-" & Me._indigoSessionValues.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmTimeParametersMetaData, Eform.InfoMetaData), Me._timeParametersAux.Id, NameCustomer)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmTimeParametersMetaDataTitle, Eform.InfoMetaData), Me._timeParametersAux.Id)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo eliminar parametro
    ''' </summary>
    Private Async Sub DeleteTimeParameters()
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If Me._timeParametersAux.Id > 0 Then
                Using Model As New MTimeParameters(Me.Tag)
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteTimeParameters(Me._timeParametersAux)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        Me.RefreshListTimeParameters()
                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._timeParametersAux.Id)
                        Await Me.DeleteDocumentIndexed()
                    Else
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorDependencia, Comunes)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                    End If
                End Using
            Else
                Me._listTimeParameters.Remove(Me._timeParametersAux)
                Me.INDExceptionsGc.DataSource = Me._listTimeParameters
                Me.INDExceptionsGv.RefreshData()
            End If
        End If
    End Sub

    Async Sub LoadParameter(Optional idRecord As String = "")
        If idRecord <> "" Then
            Using model = New MTimeParameters(Me.Tag)
                Me._timeParametersAux = Await model.GetTimeParameters(idRecord, _idOperativeUnit)
            End Using
        Else
            Me._timeParametersAux = CType(Me.INDExceptionsGv.GetRow(Me.INDExceptionsGv.FocusedRowHandle), Domain.Entities.TimeParameters)
        End If
        Dim customerAux = Me._timeParametersAux.Customer
        If customerAux IsNot Nothing AndAlso Me._listTimeParameters.Exists(Function(x) x.IdCustomer = customerAux.Id) Then
            Dim _formTimeParameters = New FrmTimeParametersActions(Me._listTimeParameters, Me._timeParametersAux, customerAux, Me.Tag)
            _formTimeParameters.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim frmTrans = New FrmTransparent(_formTimeParameters, False)
            frmTrans.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim result = frmTrans.ShowDialog()
            If result = System.Windows.Forms.DialogResult.OK Then
                Me.INDExceptionsGc.DataSource = Me._listTimeParameters.Where(Function(x) x.IdCustomer IsNot Nothing).ToList
                Me.INDExceptionsGv.RefreshData()
                CleanDataExceptions()
            End If
        End If
    End Sub



#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _timeParameters = Nothing
        _timeParametersAux = Nothing
        _listTimeParameters = Nothing
        _listConcetpGlosas = Nothing
        _presenter = Nothing
        _customer = Nothing
        ListDevolutionInjustificate = Nothing
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    ''' Inicializa los campos del frontal
    ''' </summary>
    Private Sub FrmTimeParameters_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables****'
        Me._indigoSessionValues = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        '*****************************'
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        _presenter = New PTimeParameters(Me)
        INDlycConceptGlosa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
            INDlygInterfaceJournalVoucher.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlygInterfaceConcepts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            IndLyiCheckEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ListDevolutionInjustificate = New List(Of Tuple(Of Integer, String))
            ListDevolutionInjustificate.Add(New Tuple(Of Integer, String)(1, "Liberar Factura del Radicado"))
            ListDevolutionInjustificate.Add(New Tuple(Of Integer, String)(2, "Mantener Factura en el Radicado"))
            ListDevolutionInjustificate.Add(New Tuple(Of Integer, String)(3, "Definido por el Usuario"))
            INDsleDevolutionInjustificate.Properties.DataSource = ListDevolutionInjustificate.ToList()
            ' INDsleDevolutionInjustificate.Properties.Buttons.Item(1).Visible = False

            INDlyItemDetailedGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemDetailedGlossConceptNoteId.AllowHide = True
            INDlyItemGeneralGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemGeneralGlossConceptNoteId.AllowHide = True

            INDlyiThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiThird.AllowHide = True
            INDlyiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiCostCenter.AllowHide = True

            HideControlsGroup()
        Else
            INDlygInterfaceJournalVoucher.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlygInterfaceConcepts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            IndChkMedicalFees.EditValue = False
            IndLyiCheckEdit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlycConceptGlosa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiThird.AllowHide = True
            INDlyiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiCostCenter.AllowHide = True
        End If
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        LoadControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Consulta un cliente por su nit
    ''' </summary>
    Private Async Sub INDEntityBte_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDEntityBte.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Using model As New MTimeParameters(Me.Tag)
                If INDEntityBte.Text IsNot Nothing OrElse Me.INDEntityBte.Text <> "" Then
                    Dim ctomer As Domain.Entities.Customer
                    ctomer = Await model.GetCustomerByNit(Me.INDEntityBte.Text.Trim())
                    If ctomer.Name IsNot Nothing AndAlso Not ctomer.Name.Trim().Equals(String.Empty) Then
                        Me._customer = ctomer
                        'Customer existe
                        AsyncLoader(True)
                        Me._timeParametersAux = Await model.GetTimeParameters(Me._customer.Id, _idOperativeUnit)
                        AsyncLoader(False)
                        Me.INDEntityTxt.Text = Me._customer.Name.Trim()
                    Else
                        Me.INDEntityBte.Focus()
                        'Mensaje, El cliente no existe
                        If Me._customer Is Nothing Then
                            Me.INDEntityBte.Text = ""
                        Else
                            Me.INDEntityBte.Text = Me._customer.Nit.Trim()
                        End If
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesClienteNoExiste, Eform.Comunes)
                        Me.INDEntityBte.SelectAll()
                    End If
                End If
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' El envio de respuesta tiene que ser mayor al tiempo de respuesta
    ''' </summary>
    Private Sub INDspeMaxTimeSendResponseObjection_EditValueChanged(sender As Object, e As EventArgs) Handles INDspeMaxTimeSendResponseObjection.EditValueChanged
        If INDspeMaxTimeSendResponseObjection.EditValue < INDspeMaxTimeResponseObjection.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El tiempo de Envio de respuesta " & INDspeMaxTimeSendResponseObjection.EditValue & " días, debe ser mayor al tiempo de respuesta " & INDspeMaxTimeResponseObjection.EditValue & " días"
            Me.INDspeMaxTimeSendResponseObjection.EditValue = Me.INDspeMaxTimeResponseObjection.EditValue + 1
        End If
    End Sub

    ''' <summary>
    ''' El envio de respuesta tiene que ser mayor al tiempo de respuesta
    ''' </summary>
    Private Sub INDspeMaxTimeSendResponseReiteration_EditValueChanged(sender As Object, e As EventArgs) Handles INDspeMaxTimeSendResponseReiteration.EditValueChanged
        If INDspeMaxTimeSendResponseReiteration.EditValue < INDspeMaxTimeResponseReiteration.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El tiempo de Envio de respuesta reiteración " & INDspeMaxTimeSendResponseReiteration.EditValue & " días debe ser mayor al tiempo de respuesta de reiteración " & INDspeMaxTimeResponseReiteration.EditValue & " días"
            Me.INDspeMaxTimeSendResponseReiteration.EditValue = Me.INDspeMaxTimeResponseReiteration.EditValue + 1
        End If
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAffectedService_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAffectedService.EditValueChanged
        If AffectedService IsNot Nothing Then
            If AffectedService Then
                INDlyItemDetailedGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemDetailedGlossConceptNoteId.AllowHide = False
                INDlyItemGeneralGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemGeneralGlossConceptNoteId.AllowHide = True
            Else
                INDlyItemDetailedGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemDetailedGlossConceptNoteId.AllowHide = True
                INDlyItemGeneralGlossConceptNoteId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemGeneralGlossConceptNoteId.AllowHide = False
            End If
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Click en el boton Adicionar
    ''' </summary>
    Private Sub INDAddSmb_Click(sender As Object, e As EventArgs) Handles INDAddSmb.Click
        Dim CustomerID As Nullable(Of Integer)
        If Me._customer IsNot Nothing Then
            CustomerID = CType(Me._customer.Id, Nullable(Of Integer))
        End If
        If Me._customer IsNot Nothing AndAlso Not Me._listTimeParameters.Exists(Function(x) IIf(x.Customer IsNot Nothing, x.IdCustomer = CustomerID, False)) Then
            Dim _formTimeParameters = New FrmTimeParametersActions(Me._listTimeParameters, Nothing, Me._customer, Me.Tag)
            _formTimeParameters.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim frmTrans = New FrmTransparent(_formTimeParameters, False)
            frmTrans.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim result = frmTrans.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                Me.INDExceptionsGc.DataSource = Me._listTimeParameters.Where(Function(x) x.IdCustomer IsNot Nothing).ToList
                Me.INDExceptionsGv.RefreshData()
                CleanDataExceptions()
            End If
        Else
            If Me._customer Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione una entidad antes de agregar"
            ElseIf Not Me._listTimeParameters.Exists(Function(x) IIf(x.Customer IsNot Nothing, x.IdCustomer = CustomerID, False)) Then
                Mensaje(EeventViewerImages.Advertencia) = "La entidad ya se encuentra en la lista"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Click en el boton Editar
    ''' </summary>
    Private Sub INDEditBtn_Click(sender As Object, e As EventArgs) Handles INDEditBtn.Click
        LoadParameter()
    End Sub


    ''' <summary>
    ''' Click en el boton eliminar del registro
    ''' </summary>
    Private Sub INDDeleteBtn_Click(sender As Object, e As EventArgs) Handles INDDeleteBtn.Click
        Me._timeParametersAux = CType(Me.INDExceptionsGv.GetRow(Me.INDExceptionsGv.FocusedRowHandle), Domain.Entities.TimeParameters)
        DeleteTimeParameters()
        Me.INDEntityBte.Text = ""
        Me.INDEntityTxt.Text = ""
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._timeParametersAux IsNot Nothing AndAlso Me._timeParametersAux.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                LoadParameter(Me.IdEntity.Trim())
            End If
        Else 'Realiza la consulta normal
            LoadParameter(Me.IdEntity.Trim())
        End If
        Me.IdEntity =  String.Empty
    End Sub
#End Region

#Region "CheckedChanged"
    Private Async Sub IndChkMedicalFees_CheckedChanged(sender As Object, e As EventArgs) Handles IndChkMedicalFees.CheckedChanged
        If IndChkMedicalFees.EditValue = True Then
            INDlycConceptGlosa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Using model As New MTimeParameters(Me.Tag)
                AsyncLoader(True)
                _listConcetpGlosas = Await model.ListConceptGlosaByType(1)
                AsyncLoader(False)
                INDgcConcept.DataSource = _listConcetpGlosas
            End Using
            INDgcConcept.RefreshDataSource()
        Else
            INDlycConceptGlosa.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
#End Region

#Region "MouseDoubleClick"
    ''' <summary>
    ''' Seleccionar todo con doble click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcConcept_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcConcept.MouseDoubleClick
        Dim objHit = Me.IndgvConcept.CalcHitInfo(e.Location)
        If objHit.InColumn AndAlso objHit.Column IsNot Nothing AndAlso objHit.Column.Name.Equals("clSelection") Then
            If Me.INDgcConcept.DataSource IsNot Nothing Then
                If DirectCast(Me.INDgcConcept.DataSource, List(Of Domain.Entities.ConceptGlosas)).Where(Function(p) p.DiscountedMedicalFees).ToList().Count = DirectCast(Me.INDgcConcept.DataSource, List(Of Domain.Entities.ConceptGlosas)).Count Then
                    For Each p As Domain.Entities.ConceptGlosas In DirectCast(Me.INDgcConcept.DataSource, List(Of Domain.Entities.ConceptGlosas))
                        p.DiscountedMedicalFees = False
                    Next
                Else
                    For Each p As Domain.Entities.ConceptGlosas In DirectCast(Me.INDgcConcept.DataSource, List(Of Domain.Entities.ConceptGlosas))
                        p.DiscountedMedicalFees = True
                    Next
                End If
            End If
            Me.INDgcConcept.RefreshDataSource()
            INDgcConcept.Invalidate()
        End If
    End Sub
#End Region

#Region "ToolBar Events"


    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            CleanControls()
            Me._idOperativeUnit = operatingUnit.Id
            LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.Buscar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de personalizacion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar

    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub


#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePreviousLifetimesConceptNoteId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePreviousLifetimesConceptNoteId.QueryPopUp
        If PreviousLifetimesConceptNoteIdXpo Is Nothing Then
            _presenter.InitializePreviousLifetimesConceptNote()
        End If
    End Sub


    ''' <summary>
    ''' Evento que se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleReceptionObjectionJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReceptionObjectionJournalVoucherTypeId.QueryPopUp
        If ReceptionObjectionJournalVoucherTypeIdXpo Is Nothing Then
            _presenter.InitializeReceptionObjectionJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConciliationJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConciliationJournalVoucherTypeId.QueryPopUp
        If ConciliationJournalVoucherTypeIdXpo Is Nothing Then
            _presenter.InitializeConciliationJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDevolutionJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDevolutionJournalVoucherTypeId.QueryPopUp
        If DevolutionJournalVoucherTypeIdXpo Is Nothing Then
            _presenter.InitializeDevolutionJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleGeneralGlossConceptNoteId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleGeneralGlossConceptNoteId.QueryPopUp
        If GeneralGlossConceptNoteIdXpo Is Nothing Then
            _presenter.InitializeGeneralGlossConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDetailedGlossConceptNoteId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDetailedGlossConceptNoteId.QueryPopUp
        If DetailedGlossConceptNoteIdXpo Is Nothing Then
            _presenter.InitializeDetailedGlossConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTransferLegalJournalVoucherTypeId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTransferLegalJournalVoucherTypeId.QueryPopUp
        If TransferLegalJournalVoucherTypeIdXpo Is Nothing Then
            _presenter.InitializeTransferLegalJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThird_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThird.QueryPopUp
        If DataSourceThird Is Nothing Then
            _presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centros de costos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If DataSourceCostCenter Is Nothing Then
            _presenter.InitializeCostCenter()
        End If
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Evento click en el Button Edit de entidades
    ''' </summary>
    Private Sub INDEntityBte_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDEntityBte.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de conceptos de nota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleGeneralGlossConceptNoteId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleGeneralGlossConceptNoteId.ButtonClick, INDsleDetailedGlossConceptNoteId.ButtonClick, INDslePreviousLifetimesConceptNoteId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(677, "", True)
            _presenter.InitializePortfolioNoteConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de tipo de documento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRadicationJournalVoucherTypeId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleReceptionObjectionJournalVoucherTypeId.ButtonClick, INDsleConciliationJournalVoucherTypeId.ButtonClick, INDsleDevolutionJournalVoucherTypeId.ButtonClick, INDsleTransferLegalJournalVoucherTypeId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(607, "", True)
            _presenter.InitializerJournalVoucherType()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de Terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThird_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThird.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Common.FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Common.FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeCostCenter()
        End If
    End Sub
#End Region

#End Region

    ''' <summary>
    ''' Cambio de Concepto de vigencias anteriores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePreviousLifetimesConceptNoteId_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePreviousLifetimesConceptNoteId.EditValueChanged
        If INDslePreviousLifetimesConceptNoteId.EditValue IsNot Nothing Then
            LoadConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Dim ObjConceptNotePreviousLifetimes As PortfolioNoteConcept
    Private Async Sub LoadConceptNote()
        Using model As New MTimeParameters(Me.Tag)
            ObjConceptNotePreviousLifetimes = Await model.GetConceptPrevious(PreviousLifetimesConceptNoteId)
        End Using
        If ObjConceptNotePreviousLifetimes.MainAccounts.HandlesThirdParty = True Then
            INDlyiThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiThird.AllowHide = False
        Else
            INDlyiThird.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiThird.AllowHide = True
        End If
        If ObjConceptNotePreviousLifetimes.MainAccounts.HandlesCostCenter = True Then
            INDlyiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiCostCenter.AllowHide = False
        Else
            INDlyiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiCostCenter.AllowHide = True
        End If
    End Sub

End Class