Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.MixingStation.MVP

Public Class FrmPopupTechnicalConceptReadjusments

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        _CtrGeneric2Labels = New CtrGeneric2Labels()
        _CtrGeneric2Labels.Dock = DockStyle.Top
        AdditionalControlPanel.Controls.Add(_CtrGeneric2Labels)
        'INDGvDefectClassification.OptionsView.ShowAutoFilterRow = False
    End Sub
#End Region

#Region "Properties"

    Public Property ViewReadjustment As ViewReadjustmentsXpo

    Public Property RequestPackageDetailStatusId As Integer

    Public Property unitDoseClass As Integer

    Public Property OperatingUnitId As Integer

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

    Private Property DataSourceDefectClassification As List(Of DefectClassificationModel)
        Get
            Return INDGcDefectClassification.DataSource
        End Get
        Set(value As List(Of DefectClassificationModel))
            INDGcDefectClassification.DataSource = value
            INDGcDefectClassification.RefreshDataSource()
        End Set
    End Property

    Private Shadows _modelQualityControl As New MDashboardQualityControl(Tag)

    Public ReadOnly Property DateTechnicalConcept As DateTime
        Get
            Return DateTime.Now
        End Get
    End Property

    Public WriteOnly Property DosageName As String
        Set(value As String)
            INDTeTypeDose.Text = value
        End Set
    End Property

    Public WriteOnly Property Readjustment As Boolean
        Set(value As Boolean)
            INDCBReadJusment.Text = If(value, "SI", "NO")
        End Set
    End Property

    Private _dateExpired As DateTime?
    Public Property DateExpired As DateTime?
        Get
            Return INDdeExpiratedDate.EditValue
        End Get
        Set(value As DateTime?)
            INDdeExpiratedDate.EditValue = value
        End Set
    End Property

    Private _temperature As String
    Public Property Temperature As String
        Get
            Return INDteTemperature.Text
        End Get
        Set(value As String)
            INDteTemperature.Text = value
        End Set
    End Property

    Private _batchCode As String
    Public WriteOnly Property BatchCode As String
        Set(value As String)
            _batchCode = value
        End Set
    End Property

    Private _technicalConcept As String
    Public Property TechnicalConcept As String
        Get
            Return INDmeTechnicalConcept.Text
        End Get
        Set(value As String)
            INDmeTechnicalConcept.Text = value
        End Set
    End Property

#End Region

#Region "Variables"
    ''' <summary>
    ''' control de la parte superior 
    ''' </summary>
    Dim _CtrGeneric2Labels As CtrGeneric2Labels

    ''' <summary>
    ''' Me permite saber si el paquete puede realizar readecuaciones
    ''' </summary>
    Private allowedReadjustment As Boolean = False
#End Region

#Region "Enums"
    Public Enum eStatus
        Pending
        Readjustment
        Delete
    End Enum
#End Region

#Region "Handles"
#Region "Load"
    Private Sub FrmPopupTechnicalConceptReadjusments_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeForm()
    End Sub
#End Region
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub
#End Region

#Region "Methods and Functions"
    Private Async Sub InitializeForm()
        Try
            AsyncLoader(True)
            _CtrGeneric2Labels.Label1.Text = "Lote verificado:"
            _CtrGeneric2Labels.Label2.Text = $"{Me._batchCode}"
            Me.DataSourceDefectClassification = Await _modelQualityControl.GetRequestPackageDetailStatusDefectClassification({RequestPackageDetailStatusId}.ToList, unitDoseClass)

            BarraBotones.PrepareToolbar(Presentation.Controls.eAction.SaveOrDelete)
            INDDeDateTechnicalConcept.EditValue = Me.DateTechnicalConcept
            Using model As New MReadjustments(Tag)
                Dim objReadjustments = model.GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId, True).FirstOrDefault
                Dim maxReadjustments As Integer
                If objReadjustments.RequestPackageDetailStatus.PackagePersonalizedId IsNot Nothing Then
                    maxReadjustments = objReadjustments.RequestPackageDetailStatus.PackagePersonalized.Package.Readjustments
                Else
                    maxReadjustments = objReadjustments.RequestPackageDetailStatus.Package.Readjustments
                End If
                If maxReadjustments = 0 Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
                    Mensaje(EeventViewerImages.Advertencia) = "La adecuación seleccionada no tiene autorizado realizar readecuaciones"
                Else
                    Me.allowedReadjustment = True
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            Dim messageException As String = String.Empty
            If ex.Message IsNot Nothing Then
                messageException = ex.Message
            ElseIf ex.InnerException.Message IsNot Nothing Then
                messageException = ex.InnerException.Message
            End If
            Mensaje(EeventViewerImages.Advertencia) = messageException
            AsyncLoader(False)
            Me.Close()
            Throw ex
            AsyncLoader(False)
        End Try

    End Sub

    Async Sub Guardar()
        If ValidateControls() = False Then
            Exit Sub
        End If
        If Me.allowedReadjustment Then
            Try
                AsyncLoader(True)
                Dim ObjReadjustments = AssignValuesToEntity(Me.ViewReadjustment)
                Using model As New MReadjustments(Tag)
                    Dim result = Await model.SaveReadjustmentsByTechnicalConcept(ObjReadjustments)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = "Verificación de estado guardada exitosamente"
                        Me.Close()
                    Else
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
                        Mensaje(EeventViewerImages.Advertencia) = String.Join("-", result.MessageResult)
                    End If
                End Using
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.MensajeError) = ex.Message
            End Try
        End If
    End Sub

    Async Sub Eliminar()
        Try
            AsyncLoader(True)
            Dim listReadjustmentIds = Me.ViewReadjustment.Id
            Using model As New MReadjustments(Tag)
                Dim result = Await model.GenerateInventoryAjustmenByReadjusments({listReadjustmentIds}.ToList(), OperatingUnitId)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Concat("No se pudo generar el ajuste de inventario por: ", String.Join("-", result.MessageResult))
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    Private Function AssignValuesToEntity(viewSimpleReadjustments As ViewReadjustmentsXpo) As Readjustments
        Dim Newreadjustment As New Readjustments()
        With Newreadjustment
            .EntityId = viewSimpleReadjustments.EntityId
            .EntityName = viewSimpleReadjustments.EntityName
            .RequestPackageDetailStatusId = viewSimpleReadjustments.RequestPackageDetailStatusId
            .BatchCode = viewSimpleReadjustments.BatchCode
            .SendTo = viewSimpleReadjustments.SendTo
            .Status = eStatus.Readjustment
            .TechnicalConceptDate = DateTechnicalConcept
            .ExpiratedDate = DateExpired
            .Temperature = Temperature
            .TechnicalConcept = TechnicalConcept
            .IsReadjustment = True
        End With
        Return Newreadjustment
    End Function

#End Region

End Class