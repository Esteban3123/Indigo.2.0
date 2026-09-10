#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports DevExpress.Data.Async.Helpers
Imports Domain.Crystal.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
#End Region

Public Class FrmPopupSearchAdmission
    Implements IDashBoardPharmacyDetail

#Region "Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Inventory"

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PDashBoardPharmacyDetail

    ''' <summary>
    ''' filtra el tipo de motivo general
    ''' </summary>
    Private _filterType As Integer?
#End Region

#Region "Properties"

    ''' <summary>
    ''' Id del motivo general
    ''' </summary>
    ''' <returns></returns>
    Public Property AdmissionNumber As String
        Get
            Return INDsleAdmission.EditValue
        End Get
        Set(value As String)
            INDsleAdmission.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' codigo de la unidad funcional seleccionada
    ''' </summary>
    ''' <returns></returns>
    Public Property FunctionalUnitCode As String
        Get
            Return INDSleFunctionalUnit.EditValue
        End Get
        Set(value As String)
            INDSleFunctionalUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' objeto que contiene el ingreso seleccionado
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property _admissionObject As Object
        Get
            Return (TryCast(INDGvAdmission.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow)
        End Get
    End Property

    ''' <summary>
    ''' objeto que contiene la unidad funcional seleccionado
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property _functionalUnit As Object
        Get
            Return (TryCast(INDGvFunctional.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow)
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IDashBoardPharmacyDetail.MyTag
        Get
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IDashBoardPharmacyDetail.MyLayoutControl
        Get
        End Get
    End Property

    Public Property Sequence As InventorySequence Implements IDashBoardPharmacyDetail.Sequence
        Get
        End Get
        Set(value As InventorySequence)
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    Private WriteOnly Property ICrudBase_Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub
#End Region

#Region "Methods"
    Private Sub ShowRequestForm()
        Using RequestCrystalForm As New IndigoeHistorias.frmHCSolicitudMedicamentosInsumos(_admissionObject?.PatientCode, _admissionObject?.AdmissionCode, _admissionObject?.CenterAttentionCode, Me.FunctionalUnitCode, Nothing, IndigoeHistorias.frmHCSolicitudMedicamentosInsumos.eORIGEN.DashboardPacientesEnfermeria)
           RequestCrystalForm.ShowDialog(Me)
           RequestCrystalForm.Dispose()
        End Using
    End Sub
#End Region

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupWareHouse_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        _presenter = New PDashBoardPharmacyDetail(Me)

        Dim Obj = _presenter.FirstFunctionalSurgicalUnit()
        Me.FunctionalUnitCode = Obj.UFUCODIGO
        Me.INDSleFunctionalUnit.Properties.NullText = Obj.CodeName
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        Me.AdmissionNumber = Nothing
    End Sub

#End Region

#Region "QueryPopUp"
    Private Sub INDsleAdmission_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdmission.QueryPopUp
        INDsleAdmission.Properties.DataSource = _presenter.ListAdmissions()
    End Sub

    Private Sub INDSleFunctionalUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleFunctionalUnit.QueryPopUp
        If INDSleFunctionalUnit.Properties.DataSource Is Nothing Then
            INDSleFunctionalUnit.Properties.DataSource = _presenter.ListFunctionalUnit()
        End If
    End Sub
#End Region

#Region "KeyDown"
    Private Sub FrmPopupObservations_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "Click"

    Private Async Sub INDSbAgree_Click(sender As Object, e As EventArgs) Handles INDSbAgree.Click
        Try
            Dim stringBuilder = New StringBuilder

            If String.IsNullOrEmpty(AdmissionNumber) OrElse _admissionObject Is Nothing Then
                ShowMessage(EeventViewerImages.Advertencia) = $"El campo Ingreso está Vacío"
                Exit Sub
            End If

            If String.IsNullOrEmpty(_admissionObject?.PatientCode) Then
                stringBuilder.AppendLine("No se encontró el codigo del paciente")
            End If

            If String.IsNullOrEmpty(_admissionObject?.AdmissionCode) Then
                stringBuilder.AppendLine("No se encontró el número de ingreso")
            End If

            If String.IsNullOrEmpty(_admissionObject?.CenterAttentionCode) Then
                stringBuilder.AppendLine("No se encontró el centro de atención")
            End If

            If String.IsNullOrEmpty(Me.FunctionalUnitCode) Then
                stringBuilder.AppendLine("No se encontró la unidad funcional")
            End If

            If stringBuilder.Length > 0 Then
                ShowMessage(EeventViewerImages.Advertencia) = $"Se han presentado las siguientes validaciones: {stringBuilder.ToString()}"
                Exit Sub
            End If

            Using model As New MAdmissions(Me.Tag)
                Dim Ingress = Await model.AdmisionValidations(AdmissionNumber)

                If Not Ingress.StateResult Then
                    ICrudBase_Mensaje(EeventViewerImages.Advertencia) = Ingress.Message
                    Exit Sub
                End If

            End Using

            ShowRequestForm()

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        Catch ex As Exception
            ShowMessage(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

#End Region


End Class