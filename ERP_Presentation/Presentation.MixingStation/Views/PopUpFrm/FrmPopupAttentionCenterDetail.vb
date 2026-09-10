'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 13/06/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmPopupAttentionCenterDetail
#Region "EVENTS"
#End Region

#Region "Variables"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "MixingStation"

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PCMConfig

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _CMCenterAttentionEntity As CMCenterAttention
    ''' <summary>
    ''' 
    ''' </summary>
    Public _listCMCenterAttentionDetail As Domain.Entities.TrackableCollection(Of CMCenterAttention)
#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece código y el nombre del producto para el detalle del paquete
    ''' </summary>
    Public Property CodeCenterAttention As String
        Get
            Return INDsleCenterAttention.EditValue
        End Get
        Set(value As String)
            INDsleCenterAttention.EditValue = value
        End Set
    End Property
    Property ProductionLineDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleProductionLine.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProductionLine.Properties.DataSource = value
        End Set
    End Property

    Public Property Id_ProductionLine As String
        Get
            Return INDsleProductionLine.EditValue
        End Get
        Set(value As String)
            INDsleProductionLine.EditValue = value
        End Set
    End Property

    Private _ProductionLinesIds As String
    Public Property ProductionLinesIds() As String
        Get
            Return _ProductionLinesIds
        End Get
        Set(ByVal value As String)
            _ProductionLinesIds = value
        End Set
    End Property

    Private _MixingStationId As Integer
    Public Property MixingStationId() As Integer
        Get
            Return _MixingStationId
        End Get
        Set(ByVal value As Integer)
            _MixingStationId = value
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
        CodeCenterAttention = Nothing
        INDtxtCode.Text = Nothing
        INDsleLocation.Properties.NullText = Nothing
        Id_ProductionLine = Nothing
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
    Private Sub FrmPopupAttentionCenterDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        If _presenter Is Nothing Then _presenter = New PCMConfig()
    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
    End Sub
#End Region

#Region "Click"

    Sub Load_ProductionLine()
        'Using Model As New MBusqueda
        '    ProductionLineDatasource = Model.ConsultarEntidades(eDataSource.ListProductionLine)
        'End Using
        If String.IsNullOrEmpty(Me.ProductionLinesIds) Then
            Mensaje(EeventViewerImages.Advertencia) = "Configure líneas de producción de la central de mezcla"
        Else

            INDsleProductionLine.Properties.DataSource = _presenter.GetProductionLineByIds(Me.ProductionLinesIds)
        End If
    End Sub


    Sub Load_CenterAttention()
        Using Model As New MPatient(Me.Tag)
            INDsleCenterAttention.Properties.DataSource = Model.ListCenter
        End Using
    End Sub

    Private Sub INDpceCenterAttention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCenterAttention.EditValueChanged
        If Not String.IsNullOrEmpty(INDsleCenterAttention.EditValue) Then
            Dim centerAttention = DirectCast(INDsleCenterAttention.GetSelectedObject(), Infrastructure.Data.Xpo.CrystalRepository.CentersXpo)
            If centerAttention Is Nothing Then
                centerAttention = _presenter.GetCenterAttentionByCode(INDsleCenterAttention.EditValue.ToString.Trim)
            End If
            INDtxtCode.Text = centerAttention.CODCENATE.Trim
            Dim _CommonCityXpo = _presenter.GetCityByCode(centerAttention.DEPMUNCOD.Trim)
            If _CommonCityXpo Is Nothing Then
                INDsleLocation.Properties.NullText = centerAttention.DEPMUNCOD.Trim
            Else
                INDsleLocation.Properties.NullText = _CommonCityXpo.CodeName

            End If
        Else
            INDtxtCode.Text = String.Empty
            INDsleLocation.Properties.NullText = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Agrega los datos  al detalle de paquete
    ''' </summary>
    Private Sub AddCenterAttention()

        _CMCenterAttentionEntity = New CMCenterAttention
        Dim _productionLine = DirectCast(INDsleProductionLine.GetSelectedObject(), Infrastructure.Data.Xpo.MixingStationRepository.MixingStationProductionLineXpo)
        If _productionLine Is Nothing Then
            _productionLine = _presenter.GetProductionLineById(INDsleProductionLine.EditValue)
        End If

        With _CMCenterAttentionEntity
            .Agregado = True
            .CodeCenterAttention = CodeCenterAttention
            .IdProductionLine = INDsleProductionLine.EditValue
            .CodeNameCenterAttention = String.Format("{0} - {1}", INDtxtCode.Text.Trim, INDsleCenterAttention.Text.Trim)
            .CodeNameProductionLine = String.Format("{0} - {1}", _productionLine.Code, _productionLine.Name)
            .Ubicacion = INDsleLocation.Properties.NullText
            .StateCA = True
            .StatusName = "Activo"
            .ChangeTracker.State = Domain.Base.Entities.ObjectState.Added
        End With
    End Sub

    ''' <summary>
    ''' Agrega el centro de atención a la central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        Try
            If CodeCenterAttention Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un centro de atención"
                Exit Sub
            End If
            If Id_ProductionLine Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una línea de producción"
                Exit Sub
            End If

            If _listCMCenterAttentionDetail.FirstOrDefault(Function(ca) ca.CodeCenterAttention = Me.CodeCenterAttention And ca.IdProductionLine = Id_ProductionLine) IsNot Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "El centro de atención y la línea de producción ya se encuentra agregada"
                Exit Sub
            End If

            Dim productionLine = _presenter.GetProductionLineById(INDsleProductionLine.EditValue)
            Dim unitDoseTypeIds = productionLine.ProductionLineUnitDoseTypeXpo.Select(Function(m) m.Id_UnitDoseType.Id).ToList()

            Dim cmCenterAttentionXpo = _presenter.ValidateCareCenterInOtherCMByUnitDoseType(Me.MixingStationId, CodeCenterAttention, unitDoseTypeIds)
            If cmCenterAttentionXpo IsNot Nothing Then
                Dim unitDoseType = cmCenterAttentionXpo.ProductionLine.ProductionLineUnitDoseTypeXpo.Where(Function(m) unitDoseTypeIds.Contains(m.Id_UnitDoseType.Id)).FirstOrDefault()
                Mensaje(EeventViewerImages.Advertencia) = $"La línea de producción con tipo de dosis unitaria ({unitDoseType.Id_UnitDoseType.CodeDescription}) se encuentra asociada al mismo centro de atención en la central de mezclas ({cmCenterAttentionXpo.IdMixingStation.CodeName})"
                Exit Sub
            Else
                AddCenterAttention()
                _listCMCenterAttentionDetail.Add(_CMCenterAttentionEntity)
            End If

            Me.Close()
        Catch ex As Exception
            INDbtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#End Region

#Region "KwyDown"

    Private Sub INDsleCenterAttention_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCenterAttention.QueryPopUp
        If INDsleCenterAttention.Properties.DataSource Is Nothing Then
            Load_CenterAttention()
        End If
    End Sub

    Private Sub INDsleProductionLine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionLine.QueryPopUp
        If INDsleProductionLine.Properties.DataSource Is Nothing Then
            Load_ProductionLine()
        End If
    End Sub

#End Region

End Class