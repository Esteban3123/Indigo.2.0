Imports Presentation.Controls
Imports Domain.Maintenance.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Maintenance.MVP


Public Class FrmPopUpAddActivities

    Public VarTypeTipoRegimen As String = ""
    Public VarMetricUnit As String = ""

    Public ObjMaintenanceActivity As MaintenanceActivity

    Public IdParts As Integer = 0
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MMaintenancePlan


    Dim _TimeUnitDataSource As List(Of Tuple(Of Byte, String))
    ReadOnly Property TimeUnitDataSource As List(Of Tuple(Of Byte, String))
        Get
            If _TimeUnitDataSource Is Nothing Then
                _TimeUnitDataSource = New List(Of Tuple(Of Byte, String))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(1, "Días"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(2, "Semanas"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(3, "Meses"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(4, "Años"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(5, "Lunes"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(6, "Martes"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(7, "Miércoles"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(8, "Jueves"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(9, "Viernes"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(10, "Sábado"))
                _TimeUnitDataSource.Add(New Tuple(Of Byte, String)(11, "Domingo"))
            End If
            Return _TimeUnitDataSource
        End Get
    End Property

    Dim _PredictiveMaintenanceDataSource As List(Of Tuple(Of Integer, String))
    ReadOnly Property PredictiveMaintenanceDataSource As List(Of Tuple(Of Integer, String))
        Get
            If _PredictiveMaintenanceDataSource Is Nothing Then
                _PredictiveMaintenanceDataSource = New List(Of Tuple(Of Integer, String))
                _PredictiveMaintenanceDataSource.Add(New Tuple(Of Integer, String)(1, "No requiere Medición"))
                _PredictiveMaintenanceDataSource.Add(New Tuple(Of Integer, String)(2, "Controlar solo el Límite Mínimo"))
                _PredictiveMaintenanceDataSource.Add(New Tuple(Of Integer, String)(3, "Controlar solo el Límite Máximo"))
                _PredictiveMaintenanceDataSource.Add(New Tuple(Of Integer, String)(4, "Controlar Límites Mínimo y Máximo"))
            End If
            Return _PredictiveMaintenanceDataSource
        End Get
    End Property

    Dim _MaxTimeUnitDataSource As List(Of Tuple(Of Integer, String))
    ReadOnly Property MaxTimeUnitDataSource As List(Of Tuple(Of Integer, String))
        Get
            If _MaxTimeUnitDataSource Is Nothing Then
                _MaxTimeUnitDataSource = New List(Of Tuple(Of Integer, String))
                _MaxTimeUnitDataSource.Add(New Tuple(Of Integer, String)(1, "Días"))
                _MaxTimeUnitDataSource.Add(New Tuple(Of Integer, String)(2, "Semanas"))
                _MaxTimeUnitDataSource.Add(New Tuple(Of Integer, String)(3, "Meses"))
                _MaxTimeUnitDataSource.Add(New Tuple(Of Integer, String)(4, "Años"))
            End If
            Return _MaxTimeUnitDataSource
        End Get
    End Property

    Dim _PriorityDataSource As List(Of Tuple(Of Integer, String))
    ReadOnly Property PriorityDataSource As List(Of Tuple(Of Integer, String))
        Get
            If _PriorityDataSource Is Nothing Then
                _PriorityDataSource = New List(Of Tuple(Of Integer, String))
                _PriorityDataSource.Add(New Tuple(Of Integer, String)(1, "Alta"))
                _PriorityDataSource.Add(New Tuple(Of Integer, String)(2, "Media"))
                _PriorityDataSource.Add(New Tuple(Of Integer, String)(3, "Baja"))
            End If
            Return _PriorityDataSource
        End Get
    End Property

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        VarTypeTipoRegimen = Nothing
        VarMetricUnit = Nothing
        ObjMaintenanceActivity = Nothing
        IdParts = Nothing
        Model = Nothing
    End Sub

    Private Async Sub FrmPopUpAddActivities_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        IndGlTimeUnit.Properties.DataSource = TimeUnitDataSource
        INDGlPredictiveMaintenance.Properties.DataSource = PredictiveMaintenanceDataSource
        INDGlPriority.Properties.DataSource = PriorityDataSource
        INDGlMaximunTimeUnit.Properties.DataSource = TimeUnitDataSource
        INDglUnitMetricId.Properties.DataSource = Await Model.ListAllMeasurementUnit()
        INDTxtActividad.Focus()

        If VarTypeTipoRegimen = "1" Then ' Fechas
            INDLcItemControlTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemMaximum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcItemMaximunValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcItemTimeUnitLectura.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else ' Lecturas
            INDLcItemControlTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemMaximum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemControlTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Dim Label = INDLcItemFrequency.Text
            INDLcItemFrequency.Text = Label + " (" + VarMetricUnit + ")"
        End If

    End Sub

    Private Sub INDGlPredictiveMaintenance_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlPredictiveMaintenance.EditValueChanged
        If INDGlPredictiveMaintenance.EditValue = "1" Then
            INDLcItemPredictiveMetricUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcItemMinimunValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcItemMaximumValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDGlPredictiveMaintenance.EditValue = "2" Then
            INDLcItemPredictiveMetricUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemMinimunValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemMaximumValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        ElseIf INDGlPredictiveMaintenance.EditValue = "3" Then
            INDLcItemPredictiveMetricUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemMinimunValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcItemMaximumValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLcItemPredictiveMetricUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemMinimunValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemMaximumValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub


    Private Sub INDChkRequired_EditValueChanged(sender As Object, e As EventArgs) Handles INDChkRequired.EditValueChanged
        If INDChkRequired.EditValue = True Then
            INDLcItemStopDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLcItemStopDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub


    Private Sub INDChkMaximum_EditValueChanged(sender As Object, e As EventArgs) Handles INDChkMaximum.EditValueChanged
        If INDChkMaximum.EditValue = True Then
            INDLcItemMaximunValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcItemTimeUnitLectura.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLcItemMaximunValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcItemTimeUnitLectura.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Dim _listMaintenancePlanDetail As List(Of Domain.Maintenance.Entities.MaintenancePlanDetail)
    Public ReadOnly Property listMaintenancePlanDetail As List(Of Domain.Maintenance.Entities.MaintenancePlanDetail)
        Get
            Return _listMaintenancePlanDetail
        End Get
    End Property



    Private Function AssignValues() As MaintenanceActivity
        ObjMaintenanceActivity = New MaintenanceActivity()

        With ObjMaintenanceActivity
            .Name = INDTxtActividad.EditValue
            .Frequency = INDSpinFrequency.EditValue

            If VarTypeTipoRegimen = "1" Then ' Fechas
                .TimeUnit = IndGlTimeUnit.EditValue
                .HandledControlTime = False
            Else ' Lectura
                .HandledControlTime = INDChkMaximum.EditValue
                .MaxTime = CInt(IndSpinMaxtime.EditValue)
                .MaxTimeUnit = INDGlMaximunTimeUnit.EditValue
            End If
            .Priority = INDGlPriority.EditValue
            .NumberHours = INDspinPriorityhours.EditValue
            .NumberMinutes = INDSpinPriorityMinutes.EditValue
            .IsShutdown = INDChkRequired.EditValue
            .ShutdownDays = CInt(INDspinShutdwonDays.EditValue)
            .PredictiveMaintenance = INDGlPredictiveMaintenance.EditValue
            .MeasurementUnitId = INDglUnitMetricId.EditValue
            .MinValue = INDTxtEditMinimuValue.EditValue
            .MaxValue = INDTxtMaximunValue.EditValue
            .ActivityProcedure = INDMemoEditProcedure.EditValue

        End With

        Return ObjMaintenanceActivity

    End Function

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Public Function ValidateControls() As Boolean
        ValidateControls = True

        If Object.Equals(INDTxtActividad.EditValue, Nothing) = True Then
            ValidateControls = False
        End If

        If Object.Equals(INDSpinFrequency.EditValue, Nothing) = True Then
            ValidateControls = False
        End If

        If Object.Equals(INDGlPriority.EditValue, Nothing) = True Then
            ValidateControls = False
        End If

        If Object.Equals(INDspinPriorityhours.EditValue, Nothing) = True Then
            ValidateControls = False
        End If

        If Object.Equals(INDSpinPriorityMinutes.EditValue, Nothing) = True Then
            ValidateControls = False
        End If

        If Object.Equals(INDGlPredictiveMaintenance.EditValue, Nothing) = True Then
            ValidateControls = False
        End If

        'If Object.Equals(INDglUnitMetricId.EditValue, Nothing) = True Then
        '    ValidateControls = False
        'End If

        Return ValidateControls
    End Function


End Class