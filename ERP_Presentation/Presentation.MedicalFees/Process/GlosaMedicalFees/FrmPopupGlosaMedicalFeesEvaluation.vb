#Region "Imports"

Imports System.Drawing
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MedicalFees.MVP

#End Region

Public Class FrmPopupGlosaMedicalFeesEvaluation



#Region "Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "MedicalFees"

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PGlosaMedicalFees

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Private _GlosaMedicalFeesEvaluationEntity As GlosaMedicalFeesEvaluation

    Public GMFeesDetailId As Integer
    Public GMFEvolutionId As Integer

    Dim GlosaMedicalFeesEvaluationId As Integer
    Dim GlosaMedicalFeesEvaluationDescription As String

    Public GlosaMedicalFeesEvaluation As New TrackableCollection(Of GlosaMedicalFeesEvaluation)

#End Region

#Region "Properties"

    Public Property GlossedValue As Decimal
        Get
            Return INDTeGlossedValue.EditValue
        End Get
        Set(value As Decimal)
            INDTeGlossedValue.EditValue = value
        End Set
    End Property

    Public Property SAcceptedValue As Decimal
        Get
            Return INDTeSupplierAcceptedValue.EditValue
        End Get
        Set(value As Decimal)
            INDTeSupplierAcceptedValue.EditValue = value
        End Set
    End Property

    Public Property RaisedValue As Decimal
        Get
            Return INDTeRaisedValue.EditValue
        End Get
        Set(value As Decimal)
            INDTeRaisedValue.EditValue = value
        End Set
    End Property

    Public Property PendingValue As Decimal
        Get
            Return INDTePedingValue.EditValue
        End Get
        Set(value As Decimal)
            INDTePedingValue.EditValue = value
        End Set
    End Property

    Public Property Comment As String
        Get
            Return INDMmoObservation.EditValue
        End Get
        Set(value As String)
            INDMmoObservation.EditValue = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el formulario para adicion de productos
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 730)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Limpia los controles para agregar un producto nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        GlossedValue = Nothing
        SAcceptedValue = Nothing
        RaisedValue = Nothing
        PendingValue = Nothing
        Comment = Nothing
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

#Region "Events"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupGlosaMedicalFeesEvaluation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        If _presenter Is Nothing Then _presenter = New PGlosaMedicalFees()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        Me.GlosaMedicalFeesEvaluationId = Nothing
    End Sub

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr("2232"))
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub

#End Region

#Region "Click"

    Private Function AssignValue()
        _GlosaMedicalFeesEvaluationEntity = New GlosaMedicalFeesEvaluation
        With _GlosaMedicalFeesEvaluationEntity
            If GMFEvolutionId > 0 Then
                .Id = GMFEvolutionId
            End If
            .GlosaMedicalFeesDetailId = GMFeesDetailId
            .AcceptedValueProv = SAcceptedValue
            .RaisedValue = RaisedValue
            .PendingValue = PendingValue
            .Observation = Comment
        End With
    End Function

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Try

            AssignValue()
            GlosaMedicalFeesEvaluation.Add(_GlosaMedicalFeesEvaluationEntity)

            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlySave)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
    End Sub

    Private Sub INDTeGlossedValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTeGlossedValue.EditValueChanged, INDTeSupplierAcceptedValue.EditValueChanged, INDTeRaisedValue.EditValueChanged

        PendingValue = GlossedValue - SAcceptedValue - RaisedValue

    End Sub

#End Region

End Class