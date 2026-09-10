'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/02/2021
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Threading
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmRequestUnitDoseExternalCareCenter
    Implements IRequestUnitDoseExternalCareCenter

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListRequestUnitDoseExternalCareCenterMaquila As List(Of RequestUnitDoseExternalCareCenterMaquila)

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListDeleteRequestUnitDoseExternalCareCenterMaquila As List(Of RequestUnitDoseExternalCareCenterMaquila)

    ''' <summary>
    ''' Cabacera
    ''' </summary>
    Dim RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Tabla de detalle
    ''' </summary>
    Dim RequestUnitDoseExternalCareCenterMaquila As RequestUnitDoseExternalCareCenterMaquila

    ''' <summary>
    ''' Paciente
    ''' </summary>
    Dim RequestUnitDoseExternalCareCenterPatient As RequestUnitDoseExternalCareCenterPatient

    ''' <summary>
    ''' Paciente
    ''' </summary>
    Dim ListRequestUnitDoseExternalCareCenterPatient As List(Of RequestUnitDoseExternalCareCenterPatient)

    ''' <summary>
    ''' Paciente
    ''' </summary>
    Dim ListDeleteRequestUnitDoseExternalCareCenterPatient As List(Of RequestUnitDoseExternalCareCenterPatient)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PRequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As MixingStationSequence

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
    Private record As BlockRecordMixingStation

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsyncMaquila As CancellationTokenSource

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsyncPatient As CancellationTokenSource

    ''' <summary>
    ''' Permite identificar si el formulario se encuentra en modo lectura
    ''' </summary>
    Private isLoading As Boolean = True

    ''' <summary>
    ''' Guarda la fecha de terminación del contrato con el cliente
    ''' </summary>
    Private contractEndDate As DateTime? = Nothing

    ''' <summary>
    ''' Listado para agregar una solicitud de pacientes externos a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public ListAddRequestExternalPatientDetail As List(Of RequestUnitDoseExternalCareCenterPatient)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRequestUnitDoseExternalCareCenter.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRequestUnitDoseExternalCareCenter.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleCMConfiguration.Enabled = value
            INDsleExternalCareCenter.Enabled = value
            INDSleContract.Enabled = value
            INDdteDocumentDate.Enabled = value
            INDsleRequestType.Enabled = value
            INDbtnRequestsMaquila.Enabled = value
            INDgcRequestMaquila.Enabled = value
            INDgcRequestPatients.Enabled = value

            If BarraBotones.StatusRecord <> 1 Then
                INDbtnRequestPatients.Enabled = False
            Else
                INDbtnRequestPatients.Enabled = value
            End If

            If value Then
                INDsleCMConfiguration.Focus()
            Else
                INDbtnCode.Focus()
            End If

            INDlyRoot.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IRequestUnitDoseExternalCareCenter.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IRequestUnitDoseExternalCareCenter.Code
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
    ''' Secuencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As MixingStationSequence Implements IRequestUnitDoseExternalCareCenter.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As MixingStationSequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MixingStationSequenceDetail In Me._sequense.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
            AsyncLoader(False)
        End Set
    End Property

#End Region

#Region "ICrudBase"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        If RequestUnitDoseExternalCareCenter.Status <> 3 Then 'Anular
            If INDlygRequestMaquila.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso
            (ListRequestUnitDoseExternalCareCenterMaquila Is Nothing OrElse Not ListRequestUnitDoseExternalCareCenterMaquila.Any(Function(rudeccm) rudeccm.ChangeTracker.State <> ObjectState.Deleted)) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar como mínimo una solicitud maquila."
                Exit Sub
            End If

            If INDlygRequestPatients.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso
                (ListRequestUnitDoseExternalCareCenterPatient Is Nothing OrElse Not ListRequestUnitDoseExternalCareCenterPatient.Any(Function(rudeccp) rudeccp.ChangeTracker.State <> ObjectState.Deleted)) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar como mínimo una solicitud."
                Exit Sub
            End If
        End If

        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MRequestUnitDoseExternalCareCenter(Me.Tag.ToString())
                Dim result = Await model.SaveRequestUnitDoseExternalCareCenter(RequestUnitDoseExternalCareCenter, _idCurrentSequense, _idOperativeUnit)
                If result.StateResult Then
                    If RequestUnitDoseExternalCareCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.MixingStationSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    ElseIf RequestUnitDoseExternalCareCenter.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If RequestUnitDoseExternalCareCenter.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf RequestUnitDoseExternalCareCenter.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                        ElseIf RequestUnitDoseExternalCareCenter.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me.RequestUnitDoseExternalCareCenter = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.AsyncLoader(False)
                    Me.Deshacer()
                Else
                    Me.AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequense Is Nothing OrElse _sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequense.IsManual Then
            Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            Deshacer()
        Else
            Await NewRequestUnitDoseExternalCareCenter()
        End If
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo Solicitud", .FieldName = "RequestTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRequestUnitDoseExternalCareCenter
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
    ''' Inicializa los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listRequestType = New List(Of Tuple(Of Integer, String))
        listRequestType.Add(New Tuple(Of Integer, String)(1, "Solicitud de dosis personalizada"))
        listRequestType.Add(New Tuple(Of Integer, String)(2, "Solicitud de dosis estándar"))
        INDsleRequestType.Properties.DataSource = listRequestType
    End Sub

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    Private Sub GetDetailsMaquila()
        INDviewRequestMaquila.ShowLoadingPanel()
        tokenAsyncMaquila = New CancellationTokenSource()
        Task.Factory.StartNew(Sub() GetListRequestDetailMaquila(), tokenAsyncMaquila.Token)
    End Sub

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    Private Sub GetDetailsPatient()
        viewRequestPatients.ShowLoadingPanel()
        tokenAsyncPatient = New CancellationTokenSource()
        Task.Factory.StartNew(Sub() GetListRequestDetailPatient(), tokenAsyncPatient.Token)
    End Sub

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    Private Sub GetListRequestDetailMaquila()
        Dim result = Presenter.GetListDetailsMaquila(RequestUnitDoseExternalCareCenter.Id)

        If result IsNot Nothing AndAlso result.Count > 0 Then
            ListRequestUnitDoseExternalCareCenterMaquila = New List(Of RequestUnitDoseExternalCareCenterMaquila)
            For Each itemDetail In result
                Dim detail = New RequestUnitDoseExternalCareCenterMaquila
                With detail
                    .Id = itemDetail.Id
                    .RequestUnitDoseExternalCareCenterId = itemDetail.RequestUnitDoseExternalCareCenterId.Id

                    .Type = itemDetail.Type
                    .TypeName = If(itemDetail.Type = 1, "Medicamento", "Paquete")

                    If itemDetail.ATCId IsNot Nothing Then
                        .ATCId = itemDetail.ATCId.Id
                        .ItemDescription = itemDetail.ATCId.CodeName
                    End If

                    If itemDetail.PackageId IsNot Nothing Then
                        .PackageId = itemDetail.PackageId.Id
                        .ItemDescription = itemDetail.PackageId.CodeName
                    End If

                    .UnitDoseTypeId = itemDetail.UnitDoseTypeId.Id
                    .UnitDoseTypeCodeName = itemDetail.UnitDoseTypeId.CodeDescription
                    .Quantity = itemDetail.Quantity
                End With

                ListRequestUnitDoseExternalCareCenterMaquila.Add(detail)
            Next
        End If

        If Not tokenAsyncMaquila.IsCancellationRequested Then
            INDgcRequestMaquila.SafeInvoke(Sub()
                                               INDviewRequestMaquila.HideLoadingPanel()
                                               INDgcRequestMaquila.DataSource = ListRequestUnitDoseExternalCareCenterMaquila
                                               If ListRequestUnitDoseExternalCareCenterMaquila IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterMaquila.Count > 0 Then
                                                   INDsleRequestType.Properties.ReadOnly = True
                                               End If
                                           End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    Private Sub GetListRequestDetailPatient()
        Dim result = Presenter.GetListDetailsPatient(RequestUnitDoseExternalCareCenter.Id)
        If result IsNot Nothing AndAlso result.Count > 0 Then
            ListRequestUnitDoseExternalCareCenterPatient = New List(Of RequestUnitDoseExternalCareCenterPatient)
            For Each itemDetail In result
                Dim detail = New RequestUnitDoseExternalCareCenterPatient
                With detail
                    .Id = itemDetail.Id
                    .RequestUnitDoseExternalCareCenterId = itemDetail.RequestUnitDoseExternalCareCenterId.Id

                    .PatientExternalCareCenterId = itemDetail.PatientExternalCareCenterId.Id
                    .PatientExternalCareCenterNitName = itemDetail.PatientExternalCareCenterId.IdentificationName

                    .PatientExternalCareCenter = New PatientExternalCareCenter With {
                        .Id = itemDetail.PatientExternalCareCenterId.Id,
                        .IdentificationNumber = itemDetail.PatientExternalCareCenterId.IdentificationNumber,
                        .IdentificationTypeId = itemDetail.PatientExternalCareCenterId.IdentificationTypeId,
                        .Name = itemDetail.PatientExternalCareCenterId.Name,
                        .LastName = itemDetail.PatientExternalCareCenterId.LastName,
                        .GenderTypeId = itemDetail.PatientExternalCareCenterId.GenderTypeId,
                        .PatientMobileNumber = itemDetail.PatientExternalCareCenterId.PatientMobileNumber,
                        .PatientEmail = itemDetail.PatientExternalCareCenterId.PatientEmail,
                        .ExternalFunctionalUnit = itemDetail.PatientExternalCareCenterId.ExternalFunctionalUnit,
                        .PatientBed = itemDetail.PatientExternalCareCenterId.PatientBed
                     }

                    .UnitDoseTypeId = itemDetail.UnitDoseTypeId.Id
                    .UnitDoseTypeCodeName = itemDetail.UnitDoseTypeId.CodeDescription
                    .NptId = itemDetail.NptId?.ID
                    .NutritionTypeCodeName = itemDetail.NptId?.CodeName
                    .ExternalFunctionalUnitCode = itemDetail.ExternalFunctionalUnitCode
                    .Bed = itemDetail.Bed

                    If itemDetail.ExternalPatientPreparationsXpo IsNot Nothing AndAlso itemDetail.ExternalPatientPreparationsXpo.Any Then
                        For Each preparationXpo In itemDetail.ExternalPatientPreparationsXpo
                            Dim preparation As New ExternalPatientPreparation
                            preparation.Id = preparationXpo.Id
                            preparation.RequestUnitDoseExternalCareCenterPatientId = preparationXpo.RequestUnitDoseExternalCareCenterPatientId.Id
                            preparation.PreparationsRequested = preparationXpo.PreparationsRequested
                            preparation.PreparationTypeId = preparationXpo.PreparationTypeId
                            preparation.AdministrationRouteId = preparationXpo.AdministrationRouteId.Id
                            preparation.AdministrationRouteCodeName = preparationXpo.AdministrationRouteId.CodeName
                            preparation.AssociatedPackageId = preparationXpo.AssociatedPackageId?.Id
                            preparation.VolumeTotalOrder = preparationXpo.VolumeTotalOrder
                            preparation.TotalPreparedUnitMeasurementId = preparationXpo.TotalPreparedUnitMeasurementId.Id
                            preparation.TotalPreparedUnitMeasurementCodeName = preparationXpo.TotalPreparedUnitMeasurementId.CodeName
                            preparation.TotalPreparedUnitMeasurementAbreviation = preparationXpo.TotalPreparedUnitMeasurementId.Abbreviation
                            preparation.Concentration = preparationXpo.Concentration
                            preparation.Description = preparationXpo.Description

                            If preparationXpo.ExternalPatientPreparationDetailsXpo IsNot Nothing AndAlso preparationXpo.ExternalPatientPreparationDetailsXpo.Count > 0 Then
                                For Each preparationDetailXpo In preparationXpo.ExternalPatientPreparationDetailsXpo
                                    Dim preparationDetail As New ExternalPatientPreparationDetail
                                    preparationDetail.Id = preparationDetailXpo.Id
                                    preparationDetail.ExternalPatientPreparationId = preparationDetailXpo.ExternalPatientPreparationId.Id
                                    preparationDetail.itemType = preparationDetailXpo.itemType
                                    preparationDetail.AtcId = preparationDetailXpo.AtcId?.Id
                                    preparationDetail.AtcCodeName = preparationDetailXpo.AtcId?.CodeName
                                    preparationDetail.SupplieId = preparationDetailXpo.SupplieId?.Id
                                    preparationDetail.SupplieCodeName = preparationDetailXpo.SupplieId?.CodeName
                                    preparationDetail.ProductId = preparationDetailXpo.ProductId?.Id
                                    preparationDetail.ProductCodeName = preparationDetailXpo.ProductId?.CodeName
                                    preparationDetail.ComponentType = preparationDetailXpo.ComponentType
                                    preparationDetail.Quantity = preparationDetailXpo.Quantity
                                    preparationDetail.MeasurementUnitId = preparationDetailXpo.MeasurementUnitId?.Id
                                    preparationDetail.MeasurementUnitCodeName = preparationDetailXpo.MeasurementUnitId?.CodeName
                                    preparationDetail.MeasurementUnitAbreviation = preparationDetailXpo.MeasurementUnitId?.Abbreviation
                                    preparationDetail.Volume = preparationDetailXpo.Volume
                                    preparationDetail.VolumeMeasureUnitId = preparationDetailXpo.VolumeMeasureUnitId?.Id
                                    preparationDetail.VolumeMeasureUnitCodeName = preparationDetailXpo.VolumeMeasureUnitId?.CodeName
                                    preparationDetail.VolumeMeasureUnitAbreviation = preparationDetailXpo.VolumeMeasureUnitId?.Abbreviation
                                    preparationDetail.LoadDescriptions()

                                    preparation.ExternalPatientPreparationDetail.Add(preparationDetail)
                                Next
                            End If

                            preparation.LoadDescriptions()
                            .ExternalPatientPreparation.Add(preparation)
                        Next
                    End If
                End With

                ListRequestUnitDoseExternalCareCenterPatient.Add(detail)
            Next
        End If

        If Not tokenAsyncPatient.IsCancellationRequested Then
            INDgcRequestPatients.SafeInvoke(Sub()
                                                viewRequestPatients.HideLoadingPanel()
                                                INDgcRequestPatients.DataSource = ListRequestUnitDoseExternalCareCenterPatient
                                                If ListRequestUnitDoseExternalCareCenterPatient IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterPatient.Count > 0 Then
                                                    INDsleRequestType.Properties.ReadOnly = True
                                                End If
                                            End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
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
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Sub AssigningValues()
        With RequestUnitDoseExternalCareCenter
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .CMConfigurationId = INDsleCMConfiguration.EditValue
            .ExternalCareCenterId = INDsleExternalCareCenter.EditValue
            .ContractExternalClientsId = INDSleContract.EditValue
            .RequestType = INDsleRequestType.EditValue
            .DocumentDate = INDdteDocumentDate.EditValue

            .RequestUnitDoseExternalCareCenterMaquila.Clear()
            .RequestUnitDoseExternalCareCenterPatient.Clear()

            If INDlygRequestMaquila.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ListRequestUnitDoseExternalCareCenterMaquila IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterMaquila.Count > 0 Then
                    ListRequestUnitDoseExternalCareCenterMaquila.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterMaquila.Add(item))
                End If

                If ListDeleteRequestUnitDoseExternalCareCenterMaquila IsNot Nothing AndAlso ListDeleteRequestUnitDoseExternalCareCenterMaquila.Count > 0 Then
                    ListDeleteRequestUnitDoseExternalCareCenterMaquila.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterMaquila.Add(item.MarkAsDeleted()))
                End If
            Else
                If ListRequestUnitDoseExternalCareCenterMaquila IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterMaquila.Count > 0 Then
                    ListRequestUnitDoseExternalCareCenterMaquila.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterMaquila.Add(item.MarkAsDeleted()))
                End If

                If ListDeleteRequestUnitDoseExternalCareCenterMaquila IsNot Nothing AndAlso ListDeleteRequestUnitDoseExternalCareCenterMaquila.Count > 0 Then
                    ListDeleteRequestUnitDoseExternalCareCenterMaquila.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterMaquila.Add(item.MarkAsDeleted()))
                End If
            End If

            If INDlygRequestPatients.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If ListRequestUnitDoseExternalCareCenterPatient IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterPatient.Count > 0 Then
                    ListRequestUnitDoseExternalCareCenterPatient.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterPatient.Add(item))
                End If

                If ListDeleteRequestUnitDoseExternalCareCenterPatient IsNot Nothing AndAlso ListDeleteRequestUnitDoseExternalCareCenterPatient.Count > 0 Then
                    ListDeleteRequestUnitDoseExternalCareCenterPatient.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterPatient.Add(item.MarkAsDeleted()))
                End If
            Else
                If ListRequestUnitDoseExternalCareCenterPatient IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterPatient.Count > 0 Then
                    ListRequestUnitDoseExternalCareCenterPatient.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterPatient.Add(item.MarkAsDeleted()))
                End If

                If ListDeleteRequestUnitDoseExternalCareCenterPatient IsNot Nothing AndAlso ListDeleteRequestUnitDoseExternalCareCenterPatient.Count > 0 Then
                    ListDeleteRequestUnitDoseExternalCareCenterPatient.ForEach(Sub(item) .RequestUnitDoseExternalCareCenterPatient.Add(item.MarkAsDeleted()))
                End If
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Code = String.Empty
        INDsleCMConfiguration.EditValue = Nothing
        INDsleCMConfiguration.Properties.NullText = String.Empty
        CleanControlExternalCareCenter()
        INDdteDocumentDate.EditValue = Nothing
        INDgcRequestMaquila.DataSource = Nothing
        RequestUnitDoseExternalCareCenter = Nothing
        RequestUnitDoseExternalCareCenterMaquila = Nothing
        ListRequestUnitDoseExternalCareCenterMaquila = Nothing
        ListDeleteRequestUnitDoseExternalCareCenterMaquila = Nothing
        RequestUnitDoseExternalCareCenterPatient = Nothing
        ListRequestUnitDoseExternalCareCenterPatient = Nothing
        ListDeleteRequestUnitDoseExternalCareCenterPatient = Nothing
        INDgcRequestPatients.DataSource = Nothing
        contractEndDate = Nothing
        INDlygRequestMaquila.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygRequestPatients.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub


    ''' <summary>
    ''' Limpia los valores del control ExternalCareCenter 
    ''' </summary>
    Private Sub CleanControlExternalCareCenter()
        INDsleExternalCareCenter.Properties.DataSource = Nothing
        INDsleExternalCareCenter.EditValue = Nothing
        INDsleExternalCareCenter.Properties.NullText = String.Empty
        CleanControlContract()
    End Sub

    ''' <summary>
    ''' Limpia los valores del control Contract
    ''' </summary>
    Private Sub CleanControlContract()
        INDSleContract.Properties.DataSource = Nothing
        INDSleContract.EditValue = Nothing
        INDSleContract.Properties.NullText = String.Empty
        CleanControlRequestType()
    End Sub

    ''' <summary>
    ''' Limpia los valores del control Contract
    ''' </summary>
    Private Sub CleanControlRequestType()
        INDsleRequestType.EditValue = Nothing
        INDsleRequestType.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Bloquea el registro
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.RequestUnitDoseExternalCareCenter IsNot Nothing AndAlso Me.RequestUnitDoseExternalCareCenter.Id > 0 Then
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
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.RequestUnitDoseExternalCareCenter.Code, Me.RequestUnitDoseExternalCareCenter.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.RequestUnitDoseExternalCareCenter.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.RequestUnitDoseExternalCareCenter.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.RequestUnitDoseExternalCareCenter.Code, Me.RequestUnitDoseExternalCareCenter.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.RequestUnitDoseExternalCareCenter.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la información
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Using Model As New MRequestUnitDoseExternalCareCenter(CStr(Me.Tag))
                AsyncLoader(True)
                RequestUnitDoseExternalCareCenter = (Await Model.GetRequestUnitDoseExternalCareCenter(INDbtnCode.Text.Trim)).ObjectEmbbeded
                If RequestUnitDoseExternalCareCenter IsNot Nothing AndAlso RequestUnitDoseExternalCareCenter.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(RequestUnitDoseExternalCareCenter.Id))
                        With RequestUnitDoseExternalCareCenter
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
                            ElseIf .Status = 2 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If

                            isLoading = False
                            Code = .Code

                            INDsleCMConfiguration.EditValue = .CMConfigurationId
                            INDsleCMConfiguration.Properties.NullText = .CMConfigurationCodeName

                            INDsleExternalCareCenter.EditValue = .ExternalCareCenterId
                            INDsleExternalCareCenter.Properties.NullText = .ExternalCareCenterCodeName

                            INDSleContract.EditValue = .ContractExternalClientsId
                            INDSleContract.Properties.NullText = .NumberContractExternalClients

                            INDsleRequestType.EditValue = .RequestType
                            INDdteDocumentDate.EditValue = .DocumentDate

                            isLoading = True
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.RequestUnitDoseExternalCareCenter.Code)
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = RequestUnitDoseExternalCareCenter.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(RequestUnitDoseExternalCareCenter.Id)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using

                    If INDsleRequestType.EditValue = 1 Then 'Pacientes
                        GetDetailsPatient()
                    Else 'Maquila
                        GetDetailsMaquila()
                    End If
                Else
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Await Me.NewRequestUnitDoseExternalCareCenter()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDbtnCode.Focus()
                    End If
                End If
            End Using
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewRequestUnitDoseExternalCareCenter() As Task
        If Me._sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No existe secuencia numérica para el formulario"
            INDbtnCode.Focus()
            Exit Function
        End If
        RequestUnitDoseExternalCareCenter = New RequestUnitDoseExternalCareCenter
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.MixingStationSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.MixingStationSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"

        End If
        INDdteDocumentDate.EditValue = GetDateServer()
    End Function

    ''' <summary>
    ''' Metodo que abre el from para agregar los RIAS
    ''' </summary>
    Private Sub OpenFormRequestUnitDoseExternalCareCenterMaquila(EditMode As Boolean)
        Using formulario As New FrmRequestUnitDoseExternalCareCenterMaquila()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddRequestUnitDoseExternalCareCenterMaquilaArgs, AddressOf ReturnAddEventArgsMaquila
            formulario.EditModeDetail = EditMode
            If EditMode Then
                If RequestUnitDoseExternalCareCenterMaquila.ATCId IsNot Nothing Then
                    formulario.ListRequestUnitDoseExternalCareCenterMaquilaCompare = (From x In ListRequestUnitDoseExternalCareCenterMaquila Where x.ATCId <> RequestUnitDoseExternalCareCenterMaquila.ATCId).ToList()
                Else
                    formulario.ListRequestUnitDoseExternalCareCenterMaquilaCompare = (From x In ListRequestUnitDoseExternalCareCenterMaquila Where x.PackageId <> RequestUnitDoseExternalCareCenterMaquila.PackageId).ToList()
                End If
            Else
                formulario.ListRequestUnitDoseExternalCareCenterMaquilaCompare = ListRequestUnitDoseExternalCareCenterMaquila
            End If
            formulario.RequestUnitDoseExternalCareCenterMaquila = RequestUnitDoseExternalCareCenterMaquila
            formulario.CMConfigurationId = INDsleCMConfiguration.EditValue
            formulario.ExternalCareCenterId = INDsleExternalCareCenter.EditValue
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4, System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)
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
    Private Sub ReturnAddEventArgsMaquila(sender As Object, e As AddRequestUnitDoseExternalCareCenterMaquila)
        If e IsNot Nothing Then

            If e.EditMode = False Then 'Si se esta insertando
                If ListRequestUnitDoseExternalCareCenterMaquila Is Nothing Then
                    ListRequestUnitDoseExternalCareCenterMaquila = New List(Of RequestUnitDoseExternalCareCenterMaquila)
                End If
                ListRequestUnitDoseExternalCareCenterMaquila.Add(e.RequestUnitDoseExternalCareCenterMaquila)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else 'Si se esta actualizando
                ListRequestUnitDoseExternalCareCenterMaquila.Remove(RequestUnitDoseExternalCareCenterMaquila)
                ListRequestUnitDoseExternalCareCenterMaquila.Insert(IndexEditRecord, e.RequestUnitDoseExternalCareCenterMaquila)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDsleRequestType.Properties.ReadOnly = True
            INDgcRequestMaquila.DataSource = Nothing
            INDgcRequestMaquila.DataSource = ListRequestUnitDoseExternalCareCenterMaquila
        End If
    End Sub

    ''' <summary>
    ''' Edita un detalle
    ''' </summary>
    Private Sub EditDetailMaquila()
        RequestUnitDoseExternalCareCenterMaquila = DirectCast(INDviewRequestMaquila.GetFocusedRow(), RequestUnitDoseExternalCareCenterMaquila)
        IndexEditRecord = ListRequestUnitDoseExternalCareCenterMaquila.IndexOf(RequestUnitDoseExternalCareCenterMaquila)
        OpenFormRequestUnitDoseExternalCareCenterMaquila(True)
    End Sub

    ''' <summary>
    ''' Elimina un RIAS
    ''' </summary>
    Private Sub DeleteDetailMaquila()
        Dim entityDelete = DirectCast(INDviewRequestMaquila.GetFocusedRow(), RequestUnitDoseExternalCareCenterMaquila)
        If entityDelete.Id > 0 Then
            If ListDeleteRequestUnitDoseExternalCareCenterMaquila Is Nothing Then
                ListDeleteRequestUnitDoseExternalCareCenterMaquila = New List(Of RequestUnitDoseExternalCareCenterMaquila)
            End If
            ListDeleteRequestUnitDoseExternalCareCenterMaquila.Add(entityDelete)
        End If
        ListRequestUnitDoseExternalCareCenterMaquila.Remove(entityDelete)
        INDgcRequestMaquila.DataSource = Nothing
        INDgcRequestMaquila.DataSource = ListRequestUnitDoseExternalCareCenterMaquila
        Mensaje(EeventViewerImages.Informacion) = "Detalle eliminado de la rejilla correctamente"

        If ListRequestUnitDoseExternalCareCenterMaquila Is Nothing OrElse ListRequestUnitDoseExternalCareCenterMaquila.Count = 0 Then
            INDsleRequestType.Properties.ReadOnly = False
        End If
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
    ''' Metodo que abre el from para agregar los RIAS
    ''' </summary>
    Private Sub OpenFormRequestUnitDoseExternalCareCenterPatient(EditMode As Boolean)
        Using formulario As New FrmRequestUnitDoseExternalCareCenterPatient()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddRequestUnitDoseExternalCareCenterPatientArgs, AddressOf ReturnAddEventArgsPatients
            formulario.EditMode = EditMode
            formulario.AllowAdd = If(BarraBotones.StatusRecord <> "1", 0, 1)
            If EditMode Then
                formulario.ListRequestUnitDoseExternalCareCenterPatientCompare = (From x In ListRequestUnitDoseExternalCareCenterPatient Where x.PatientExternalCareCenterId <> RequestUnitDoseExternalCareCenterPatient.PatientExternalCareCenterId).ToList()
            Else
                formulario.ListRequestUnitDoseExternalCareCenterPatientCompare = ListRequestUnitDoseExternalCareCenterPatient
            End If
            formulario.RequestUnitDoseExternalCareCenterPatient = RequestUnitDoseExternalCareCenterPatient
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(1500, 650)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Retorno del form modal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddEventArgsPatients(sender As Object, e As AddRequestUnitDoseExternalCareCenterPatient)
        If e IsNot Nothing Then

            If e.EditMode = False Then 'Si se esta insertando
                If ListRequestUnitDoseExternalCareCenterPatient Is Nothing Then
                    ListRequestUnitDoseExternalCareCenterPatient = New List(Of RequestUnitDoseExternalCareCenterPatient)
                End If
                ListRequestUnitDoseExternalCareCenterPatient.Add(e.RequestUnitDoseExternalCareCenterPatient)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else 'Si se esta actualizando
                ListRequestUnitDoseExternalCareCenterPatient.Remove(RequestUnitDoseExternalCareCenterPatient)
                ListRequestUnitDoseExternalCareCenterPatient.Insert(IndexEditRecord, e.RequestUnitDoseExternalCareCenterPatient)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDsleRequestType.Properties.ReadOnly = True
            INDgcRequestPatients.DataSource = Nothing
            INDgcRequestPatients.DataSource = ListRequestUnitDoseExternalCareCenterPatient
        End If
    End Sub

    ''' <summary>
    ''' Edita un detalle
    ''' </summary>
    Private Sub EditDetailPatients()
        OpenFormRequestUnitDoseExternalCareCenterPatient(True)
    End Sub

    ''' <summary>
    ''' Elimina un detalle de los pacientes
    ''' </summary>
    Private Sub DeleteDetailPatients()
        If RequestUnitDoseExternalCareCenterPatient IsNot Nothing AndAlso RequestUnitDoseExternalCareCenterPatient.Id > 0 Then
            If ListDeleteRequestUnitDoseExternalCareCenterPatient Is Nothing Then
                ListDeleteRequestUnitDoseExternalCareCenterPatient = New List(Of RequestUnitDoseExternalCareCenterPatient)
            End If

            If RequestUnitDoseExternalCareCenterPatient.RequestUnitDoseExternalCareCenterPatientDetails IsNot Nothing AndAlso RequestUnitDoseExternalCareCenterPatient.RequestUnitDoseExternalCareCenterPatientDetails.Count > 0 Then
                Dim listTemp = RequestUnitDoseExternalCareCenterPatient.RequestUnitDoseExternalCareCenterPatientDetails.ToList()
                RequestUnitDoseExternalCareCenterPatient.RequestUnitDoseExternalCareCenterPatientDetails.Clear()

                For Each item In listTemp
                    RequestUnitDoseExternalCareCenterPatient.RequestUnitDoseExternalCareCenterPatientDetails.Add(item.MarkAsDeleted())
                Next
            End If

            ListDeleteRequestUnitDoseExternalCareCenterPatient.Add(RequestUnitDoseExternalCareCenterPatient)
        End If
        ListRequestUnitDoseExternalCareCenterPatient.Remove(RequestUnitDoseExternalCareCenterPatient)
        INDgcRequestPatients.DataSource = Nothing
        INDgcRequestPatients.DataSource = ListRequestUnitDoseExternalCareCenterPatient
        Mensaje(EeventViewerImages.Informacion) = "Detalle eliminado de la rejilla correctamente"

        If ListRequestUnitDoseExternalCareCenterPatient Is Nothing OrElse ListRequestUnitDoseExternalCareCenterPatient.Count = 0 Then
            INDsleRequestType.Properties.ReadOnly = False
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseExternalCareCenter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Deshacer()
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PRequestUnitDoseExternalCareCenter(Me)
        AsyncLoader(True)
        Presenter.GetSequence()
        InitializeTuples()
        LoadStatus()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewRequestMaquila, ListActions)
        IndigoGridView2.SetListAcction(viewRequestPatients, ListActions)
        IndigoGridView3.SetListAcction(viewRequestPatientsDetail, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewRequestMaquila.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRequestPatients.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRequestPatientsDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento click del boton agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnRequestsMaquila_Click(sender As Object, e As EventArgs) Handles INDbtnRequestsMaquila.Click
        RequestUnitDoseExternalCareCenterMaquila = Nothing
        OpenFormRequestUnitDoseExternalCareCenterMaquila(False)
    End Sub

    ''' <summary>
    ''' Evento click del boton agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnRequestPatients_Click(sender As Object, e As EventArgs) Handles INDbtnRequestPatients.Click
        RequestUnitDoseExternalCareCenterPatient = Nothing
        OpenFormRequestUnitDoseExternalCareCenterPatient(False)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Enter del campo código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewRequestUnitDoseExternalCareCenter()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetailMaquila()
            Case "Remove"
                DeleteDetailMaquila()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetailMaquila()
            Case "Remove"
                DeleteDetailMaquila()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual rejilla solicitudes personalizadas (Pacientes externos)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Try
            RequestUnitDoseExternalCareCenterPatient = DirectCast(viewRequestPatients.GetFocusedRow(), RequestUnitDoseExternalCareCenterPatient)
            IndexEditRecord = ListRequestUnitDoseExternalCareCenterPatient.IndexOf(RequestUnitDoseExternalCareCenterPatient)

            Select Case (sender.Tag.ToString)
                Case "Edit"
                    EditDetailPatients()
                Case "Remove"
                    DeleteDetailPatients()
            End Select
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se ejecuta al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseExternalCareCenter_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Se ejecuta al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseExternalCareCenter_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsyncMaquila IsNot Nothing Then
            tokenAsyncMaquila.Cancel()
        End If

        If tokenAsyncPatient IsNot Nothing Then
            tokenAsyncPatient.Cancel()
        End If

        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMConfiguration_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCMConfiguration.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2063, Nothing, True)
            INDsleCMConfiguration.Properties.DataSource = Presenter.InitializeCMConfiguration()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de almacén
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleExternalCareCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleExternalCareCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2201, Nothing, True)
            DataSourceExternalCareCenter()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Datasource central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMConfiguration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCMConfiguration.QueryPopUp
        If INDsleCMConfiguration.Properties.DataSource Is Nothing Then
            INDsleCMConfiguration.Properties.DataSource = Presenter.InitializeCMConfiguration()
        End If
    End Sub

    ''' <summary>
    ''' Datasurce almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleExternalCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleExternalCareCenter.QueryPopUp
        DataSourceExternalCareCenter()
    End Sub

    Private Sub DataSourceExternalCareCenter()
        If INDsleCMConfiguration.EditValue IsNot Nothing Then
            Dim DataSource = Presenter.InitializeExternalCareCenter(INDsleCMConfiguration.EditValue)
            If DataSource Is Nothing OrElse DataSource.Count = 0 Then
                INDsleExternalCareCenter.Properties.DataSource = Nothing
                Me.Mensaje(EeventViewerImages.Advertencia) = "No existen centros de atención externos asociados a la central de mezclas seleccionada"
                Exit Sub
            End If
            INDsleExternalCareCenter.Properties.DataSource = DataSource
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar la central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMConfiguration_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCMConfiguration.EditValueChanged
        CleanControlExternalCareCenter()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del centro de atención externo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleExternalCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleExternalCareCenter.EditValueChanged
        If INDsleExternalCareCenter.EditValue IsNot Nothing AndAlso isLoading Then
            INDSleContract.Enabled = True
            Dim ContractExternalClients = Presenter.InitializeContractExternalClients(INDsleExternalCareCenter.EditValue)
            If ContractExternalClients Is Nothing OrElse ContractExternalClients.Count = 0 Then
                CleanControlContract()
                INDsleRequestType.Enabled = False
                INDbtnRequestPatients.Enabled = False
                Me.Mensaje(EeventViewerImages.Advertencia) = "No existen contratos asociados al centros de atención externo seleccionado"
                Exit Sub
            End If

            INDSleContract.Properties.DataSource = ContractExternalClients

            If ContractExternalClients.Count = 1 Then
                INDbtnRequestPatients.Enabled = True
                INDSleContract.EditValue = ContractExternalClients.Select(Function(x) x.Id).FirstOrDefault
                INDSleContract.Properties.NullText = ContractExternalClients.Select(Function(x) x.ContractNumber).FirstOrDefault
                contractEndDate = ContractExternalClients.Select(Function(x) x.EndDate).FirstOrDefault
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del contrato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleContract_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleContract.EditValueChanged
        If INDSleContract.EditValue IsNot Nothing AndAlso isLoading Then
            Dim ContractExternalClientSelected = Presenter.GetContractExternalClientsById(INDSleContract.EditValue)

            If ContractExternalClientSelected Is Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Contrato asociado al centro de atención inválido"
                CleanControlContract()
                Exit Sub
            End If

            contractEndDate = ContractExternalClientSelected.EndDate

            If INDdteDocumentDate.EditValue IsNot Nothing Then
                If contractEndDate < INDdteDocumentDate.EditValue Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "El contrato seleccionado no se encuentra vigente"
                    INDsleRequestType.Enabled = False
                    Exit Sub
                Else
                    INDsleRequestType.Enabled = True
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de solicitud
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRequestType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRequestType.EditValueChanged
        If INDsleRequestType.EditValue IsNot Nothing AndAlso INDSleContract.EditValue IsNot Nothing Then
            If INDsleRequestType.EditValue = 1 Then 'Pacientes
                INDlygRequestMaquila.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlygRequestPatients.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDbtnRequestPatients.Enabled = True
            Else 'Maquila
                INDlygRequestMaquila.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlygRequestPatients.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDdteDocumentDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDdteDocumentDate.EditValueChanging
        If INDdteDocumentDate.EditValue IsNot Nothing AndAlso isLoading Then
            If contractEndDate IsNot Nothing Then
                If contractEndDate < e.NewValue Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "El contrato seleccionado no se encuentra vigente para la fecha seleccionada"
                    INDbtnRequestsMaquila.Enabled = False
                    INDbtnRequestPatients.Enabled = False
                    Exit Sub
                Else
                    INDbtnRequestsMaquila.Enabled = True
                    INDbtnRequestPatients.Enabled = True
                End If
            End If
        End If

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
        RequestUnitDoseExternalCareCenter.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        RequestUnitDoseExternalCareCenter.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        RequestUnitDoseExternalCareCenter.Status = 2
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
        RequestUnitDoseExternalCareCenter.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.MixingStationSequenceDetail IsNot Nothing Then
            If Me._sequense.MixingStationSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.MixingStationSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
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
            RequestUnitDoseExternalCareCenter.Status = 3
            Guardar()
        End If
    End Sub

#End Region


#Region "Subgrid"

    ''' <summary>
    ''' items seleccionados
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property SelectedItems As List(Of ExternalPatientPreparation)
        Get
            Dim mainView = CType(INDgcRequestPatients.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim detailView = CType(mainView.GetDetailView(mainView.FocusedRowHandle, 0), DevExpress.XtraGrid.Views.Grid.GridView)

            Return detailView.GetSelectedRows() _
                   .Select(Function(m) CType(detailView.GetRow(m), ExternalPatientPreparation)) _
                   .ToList()
        End Get
    End Property

    ''' <summary>
    ''' Establece el IsEmpty de la subrejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewRequestPatients_MasterRowEmpty(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowEmptyEventArgs) Handles viewRequestPatients.MasterRowEmpty
        e.IsEmpty = False
    End Sub

    ''' <summary>
    ''' Establece los hijos de la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewRequestPatients_MasterRowGetChildList(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs) Handles viewRequestPatients.MasterRowGetChildList
        If e.ChildList Is Nothing Then
            Dim RequestExternalPatientDetail = viewRequestPatients.GetFocusedObject(Of RequestUnitDoseExternalCareCenterPatient)
            e.ChildList = RequestExternalPatientDetail.ExternalPatientPreparation.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        End If
    End Sub

    ''' <summary>
    ''' Establece el RelationCount de la subrejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewRequestPatients_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles viewRequestPatients.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

    ''' <summary>
    ''' Establece el RelationName de la subrejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewRequestPatients_MasterRowGetRelationName(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs) Handles viewRequestPatients.MasterRowGetRelationName
        e.RelationName = "viewRequestPatients"
    End Sub

    ''' <summary>
    ''' Establece el ButtonAction de la subrejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction, IndigoGridView3.ContexMenuActions
        Try
            RequestUnitDoseExternalCareCenterPatient = DirectCast(viewRequestPatients.GetFocusedRow(), RequestUnitDoseExternalCareCenterPatient)
            IndexEditRecord = ListRequestUnitDoseExternalCareCenterPatient.IndexOf(RequestUnitDoseExternalCareCenterPatient)

            Select Case (sender.Tag.ToString)
                Case "Edit"
                    EditDetailPatients()
                Case "Remove"
                    DeleteExternalPreparation()
            End Select
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Elimina los registros seleccionados
    ''' </summary>
    Private Sub DeleteExternalPreparation()
        Dim listSelected = SelectedItems

        If BarraBotones.StatusRecord <> "1" Then
            Mensaje(EeventViewerImages.Advertencia) = "El medicamento no se puede eliminar, la solicitud ya se encuentra confirmada"
            Exit Sub
        End If

        If listSelected Is Nothing OrElse listSelected.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione los items a eliminar"
            Exit Sub
        End If

        Try
            If MessageIndigo.Show("¿Desea eliminar los medicamentos seleccionados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            Dim ListDeleteExternalPatientPreparation = New List(Of ExternalPatientPreparation)
            For Each item In listSelected
                RequestUnitDoseExternalCareCenterPatient.ExternalPatientPreparation.Remove(item)
                If item.Id > 0 Then
                    RequestUnitDoseExternalCareCenterPatient.ExternalPatientPreparation.Add(item.MarkAsDeleted())
                End If
            Next

            If RequestUnitDoseExternalCareCenterPatient.ExternalPatientPreparation.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).Any() Then
                INDgcRequestPatients.DataSource = Nothing
                INDgcRequestPatients.DataSource = ListRequestUnitDoseExternalCareCenterPatient

                Mensaje(EeventViewerImages.Informacion) = "Registro(s) eliminado(s) de la rejilla correctamente"
            Else
                DeleteDetailPatients()
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region

End Class