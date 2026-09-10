'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 27/09/2024
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Windows.Forms
Imports DevExpress.DataAccess.Native.Json
Imports DevExpress.Utils.Extensions
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Billing.POCO.E_RIPS
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base
Imports Presentation.Billing.MVP

#End Region

Public Class FrmPopUpAdjustmentNoteRIPS


#Region "Properties"

    ''' <summary>
    ''' Listado de facturas seleccionadas
    ''' </summary>
    ''' <returns></returns>
    Public Property ripsTraceabilityList As List(Of Object)

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Private _presenter As PAdjustementNoteRIPS

    ''' <summary>
    ''' Diccionario para almacenar en cache los datos del Json
    ''' </summary>
    Private _cacheObjectJson As New Dictionary(Of String, ElectronicRIPSModel)()

#End Region

#Region "Methods"


    ''' <summary>
    ''' Carga los datos en un control si no han sido cargados previamente.
    ''' </summary>
    ''' <param name="control">El control donde se cargarán los datos.</param>
    ''' <param name="getDataMethod">Método del presenter que obtiene los datos.</param>
    Private Sub LoadDataIfNeeded(ByVal control As Object, ByVal getDataMethod As Func(Of Object))
        If control.DataSource Is Nothing Then
            control.DataSource = getDataMethod()
        End If
    End Sub

    ''' <summary>
    ''' Carga los datos de los controles
    ''' </summary>
    Public Sub LoadDataUser()
        LoadDataIfNeeded(INDRisleIdentificationType, AddressOf _presenter.GetTypeIdentification)
        LoadDataIfNeeded(INDRislePatientType, AddressOf _presenter.GetTypePatient)
        LoadDataIfNeeded(INDRisleCountry, AddressOf _presenter.GetCountry)
        LoadDataIfNeeded(INDRisleBiologicalSex, AddressOf _presenter.GetBiologicalSex)
        LoadDataIfNeeded(INDRisleCity, AddressOf _presenter.GetCity)
        LoadDataIfNeeded(INDRisleTerritorialZone, AddressOf _presenter.GetTerritorialZone)
        LoadDataIfNeeded(INDRilueinability, AddressOf _presenter.GetInability)
        LoadDataIfNeeded(INDRisleBiologicalBornSex, Function() _presenter.GetBiologicalSex(True))
        LoadDataIfNeeded(INDRisleConditionDestination, Function() _presenter.GetConditionDestinationEgressUser)
        LoadDataIfNeeded(INDRisleEntryRoutesHealthServices, AddressOf _presenter.GetEntryRoutesHealthServices)
        LoadDataIfNeeded(INDRisleAttentionCauseMotive, AddressOf _presenter.GetCausesOfAttention)
        LoadDataIfNeeded(INDRisleServiceGroup, AddressOf _presenter.GetRIPSServiceGroup)
        LoadDataIfNeeded(INDRisleAdmissionModalities, AddressOf _presenter.GetAdmissionModalities)
        LoadDataIfNeeded(INDRisleServiceRIPS, AddressOf _presenter.GetRIPSServices)
        LoadDataIfNeeded(INDRisleHealthTechonologyPurposes, AddressOf _presenter.GetHealthPurposes)
    End Sub


    ''' <summary>
    ''' Obtiene la información del Json por tipo de objeto desde la base de datos de Cosmos.
    ''' Almacena los resultados en el diccionario, para cuando se vaya a volver abrir no re-consulte nuevamente la info
    ''' Se asegura para nota de ajuste viene un solo usuario, por lo tanto se obtiene el primero
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="cosmoDBId">.</param>
    ''' <returns></returns>
    Private Async Function GetObjectJsonByType(type As String, cosmoDBId As String) As Task(Of IEnumerable(Of Object))
        If Not _cacheObjectJson.ContainsKey(cosmoDBId) Then
            Using Model As New MAdjustmentNoteRIPS()
                Dim result As ActionResult(Of ElectronicRIPSModel) = Await Model.GetObjectJsonRIPSbyId(cosmoDBId)
                If result Is Nothing OrElse result.ObjectEmbbeded Is Nothing Then
                    Throw New InvalidOperationException("JSON no válido: " + result.Message)
                End If
                _cacheObjectJson(cosmoDBId) = result.ObjectEmbbeded
            End Using
        End If
        Dim obj = _cacheObjectJson(cosmoDBId)
        Select Case type
            Case "user"
                If obj?.usuarios?.Any() Then
                    Return obj.usuarios
                End If
            Case "queries"
                If obj?.usuarios.FirstOrDefault()?.servicios?.consultas IsNot Nothing Then
                    Return obj.usuarios.First().servicios.consultas
                End If
            Case "newborn"
                If obj?.usuarios.FirstOrDefault()?.servicios?.recienNacidos?.Any() Then
                    Return obj.usuarios.First().servicios.recienNacidos
                End If
            Case "hospitalization"
                If obj?.usuarios.FirstOrDefault()?.servicios?.hospitalizacion?.Any() Then
                    Return obj.usuarios.First().servicios.hospitalizacion
                End If
            Case "procedure"
                If obj?.usuarios.FirstOrDefault()?.servicios?.procedimientos?.Any() Then
                    Return obj.usuarios.First().servicios.procedimientos
                End If
            Case "emergency"
                If obj?.usuarios.FirstOrDefault()?.servicios?.urgencias?.Any() Then
                    Return obj.usuarios.First().servicios.urgencias
                End If
        End Select
        Mensaje(EeventViewerImages.Advertencia) = $"No se encontraron datos para el tipo '{type}'."
        Return Nothing
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpAdjustmentNoteRIPS_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PAdjustementNoteRIPS
        LoadDataUser()
        INDGcInvoice.DataSource = ripsTraceabilityList
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpAdjustmentNoteRIPS_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

    End Sub



#End Region

#Region "Grid"

#Region "Levels"

    ''' <summary>
    ''' Indica cantidad de niveles a mostrar por fila
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewMaster_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDGvInvoice.MasterRowGetRelationCount
        e.RelationCount = 6
    End Sub

    ''' <summary>
    ''' Indica el nombre de cada nivel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewMaster_MasterRowGetRelationName(sender As Object, e As MasterRowGetRelationNameEventArgs) Handles INDGvInvoice.MasterRowGetRelationName
        Dim level As Integer = e.RelationIndex

        Select Case level
            Case 0
                e.RelationName = "UserData"
            Case 1
                e.RelationName = "QueriesData"
            Case 2
                e.RelationName = "NewbornData"
            Case 3
                e.RelationName = "HospitalizationData"
            Case 4
                e.RelationName = "ProcedureData"
            Case 5
                e.RelationName = "EmergencyData"
        End Select
    End Sub



    ''' <summary>
    ''' Eventos para asignar la relacion y el dataSource a la sub-rejilla 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDGvInvoice_MasterRowGetChildList(sender As Object, e As MasterRowGetChildListEventArgs) Handles INDGvInvoice.MasterRowGetChildList
        Try
            INDGvInvoice.OptionsDetail.AllowExpandEmptyDetails = True
            Dim cosmoDBId As String = INDGvInvoice.GetRowCellValue(e.RowHandle, "CosmoDBId")
            Dim level = e.RelationIndex
            INDGvInvoice.ShowLoadingPanel()
            Select Case level
                Case 0
                    Dim childList As New BindingList(Of UsuarioModel)()
                    e.ChildList = childList
                    Dim listUsers = Await GetObjectJsonByType("user", cosmoDBId)
                    If listUsers Is Nothing Then Exit Sub
                    For Each usuario In listUsers
                        childList.Add(usuario)
                    Next
                Case 1
                    Dim childList As New BindingList(Of ConsultaModel)()
                    e.ChildList = childList
                    Dim listQueries = Await GetObjectJsonByType("queries", cosmoDBId)
                    If listQueries Is Nothing Then Exit Sub
                    For Each querie In listQueries
                        childList.Add(querie)
                    Next
                    IndigoGridView1.SetListAcction(INDGvQueriesData, {eAcciones.Edit, eAcciones.Remove}.ToList())
                Case 2
                    Dim childList As New BindingList(Of RecienNacidosModel)()
                    e.ChildList = childList
                    Dim listNewborns = Await GetObjectJsonByType("newborn", cosmoDBId)
                    If listNewborns Is Nothing Then Exit Sub
                    For Each newborn In listNewborns
                        childList.Add(newborn)
                    Next
                    IndigoGridView1.SetListAcction(INDGvNewbornData, {eAcciones.Edit, eAcciones.Remove}.ToList())
                Case 3
                    Dim childList As New BindingList(Of HospitalizacionModel)()
                    e.ChildList = childList
                    Dim listHospitalization = Await GetObjectJsonByType("hospitalization", cosmoDBId)
                    If listHospitalization Is Nothing Then Exit Sub
                    For Each hospitalization In listHospitalization
                        childList.Add(hospitalization)
                    Next
                    IndigoGridView1.SetListAcction(INDGvHospitalizationData, {eAcciones.Edit, eAcciones.Remove}.ToList())
                Case 4
                    Dim childList As New BindingList(Of ProcedimientoModel)()
                    e.ChildList = childList
                    Dim listProcedure = Await GetObjectJsonByType("procedure", cosmoDBId)
                    If listProcedure Is Nothing Then Exit Sub
                    For Each procedure In listProcedure
                        childList.Add(procedure)
                    Next
                    IndigoGridView1.SetListAcction(INDGvProcedureData, {eAcciones.Edit, eAcciones.Remove}.ToList())
                Case 5
                    Dim childList As New BindingList(Of UrgenciaModel)()
                    e.ChildList = childList
                    Dim listEmergency = Await GetObjectJsonByType("emergency", cosmoDBId)
                    If listEmergency Is Nothing Then Exit Sub
                    For Each emergency In listEmergency
                        childList.Add(emergency)
                    Next
                    IndigoGridView1.SetListAcction(INDGvEmergencywithObservationData, {eAcciones.Edit, eAcciones.Remove}.ToList())
            End Select

        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            INDGvInvoice.HideLoadingPanel()
        End Try
    End Sub

#End Region

#Region "Load"
    ''' <summary>
    ''' Valida si se debe mostrar la opción "Sí" en el popup de incapacidad
    ''' </summary>
    ''' <param name="sender">.</param>
    ''' <param name="e"></param>
    Private Sub INDRilueinability_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRilueinability.QueryPopUp
        Dim lookUpEdit As DevExpress.XtraEditors.LookUpEdit = CType(sender, DevExpress.XtraEditors.LookUpEdit)
        Dim admissionNumber As Object = INDGvInvoice.GetRowCellValue(INDGvInvoice.FocusedRowHandle, "AdmissionNumber")
        If admissionNumber IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(admissionNumber.ToString()) Then
            Dim inabilty = _presenter.GetInabilityByAdmissionNumber(admissionNumber)
            If inabilty Is Nothing Then
                lookUpEdit.Properties.DataSource = _presenter.GetInability(False)
                lookUpEdit.Refresh()
            End If
        End If
    End Sub



#End Region

#Region "CustomRowFilter"
    ''' <summary>
    ''' Maneja el filtrado personalizado del GridView para asegurar que los filtros solo se apliquen
    ''' a la fila padre actualmente seleccionada.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvUserData_ColumnFilterChanged(sender As Object, e As EventArgs) Handles INDGvUserData.ColumnFilterChanged
        Dim gridView As GridView = CType(sender, GridView)
        Dim parentView As GridView = INDGvInvoice
        Dim focusedParentRowHandle As Integer = parentView.FocusedRowHandle
        If gridView.SourceRowHandle <> focusedParentRowHandle Then
            For Each column As GridColumn In gridView.Columns
                column.FilterInfo = New ColumnFilterInfo()
            Next
        End If
    End Sub

#End Region

#End Region

#End Region

End Class

