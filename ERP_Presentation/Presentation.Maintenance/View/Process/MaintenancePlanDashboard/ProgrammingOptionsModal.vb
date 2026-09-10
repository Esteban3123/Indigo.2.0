Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.FixedAsset.MVP
Imports Presentation.Maintenance.MVP

Public Class ProgrammingOptionsModal

#Region "Properties"
    ''' <summary>
    ''' Parámetros de mantenimiento
    ''' </summary>
    Private _maintenanceParameter As Task(Of MaintenanceParameter)
    Public Property TotalFixedAsset As Integer
    Public Property ProtocolCode As String
    Public Property ResponsibleCode As String
    Public Property PermissionsForm As Dictionary(Of Integer, String)
    Public Property TotalFixedAssetPerDay As Integer
        Get
            Return INDSpnTotalPerDay.EditValue
        End Get
        Set(value As Integer)
            INDSpnTotalPerDay.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje
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

#Region "Handlers"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProgrammingOptionsModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadMaintenanceParameters()
        loadInit()
    End Sub

    ''' <summary>
    ''' Click aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAccept_Click(sender As Object, e As EventArgs) Handles INDBtnAccept.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub
#End Region

#Region "Functions"
    ''' <summary>
    ''' Carga los parámetros de mantenimiento
    ''' </summary>
    Private Sub LoadMaintenanceParameters()
        Using mParameter As New MMaintenanceParameter(Me.Tag)
            _maintenanceParameter = mParameter.ListMaintenanceParameterAsync()
        End Using
    End Sub

    ''' <summary>
    ''' Carga los datos iniciales
    ''' </summary>
    Private Async Sub loadInit()
        If _maintenanceParameter Is Nothing OrElse Not _maintenanceParameter.IsCompleted Then
            INDProgress.BringToFront()
        End If
        Dim parameter As MaintenanceParameter = Await _maintenanceParameter
        If parameter Is Nothing OrElse parameter.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron parámetros de Mantenimiento"
            Return
        End If

        INDSpnTotalFixedAsset.EditValue = TotalFixedAsset
        ' si maneja tiempos consultamos los tiempos del responsable y del protocolo
        If parameter.TimeProtocolRequire Then
            Dim responsible As MaintenanceResponsible
            Using model As New MMaintenanceResponsible(Me.Tag)
                responsible = Await model.GetResponsibleAsync(ResponsibleCode)
                If responsible Is Nothing OrElse responsible.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = $"No se encontró responsable de mantenimiento con código {ResponsibleCode}"
                    Return
                End If
                INDSpnDisponibleResponsable.EditValue = IIf(responsible.Capacity Is Nothing, 0, responsible.Capacity)
            End Using
            Using model As New MProtocolMaintenance(Me.Tag)
                Dim protocol = Await model.GetMaintenanceProtocol(ProtocolCode)
                If protocol Is Nothing OrElse protocol.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = $"No se encontró protocolo con código {ProtocolCode}"
                    Return
                End If
                Dim minutos As Integer = protocol.ProtocolActivities.Where(Function(m) m.Unit IsNot Nothing AndAlso m.Unit.Value = 1) _
                .Sum(Function(m) m.Time.Value)
                Dim horas As Integer = protocol.ProtocolActivities.Where(Function(m) m.Unit IsNot Nothing AndAlso m.Unit.Value = 2) _
                    .Sum(Function(m) m.Time.Value)
                INDSpnProtocol.EditValue = Utils.GetStrHours(horas, minutos).Item3
                INDSpnTotalPerDay.EditValue = Me.CalculateTotalFixedAsset(IIf(responsible.Capacity Is Nothing, 0, responsible.Capacity), horas, minutos)
            End Using
        Else
            INDLciTiempoProtocolo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciTiempoResponsable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        INDProgress.SendToBack()

        If PermissionsForm IsNot Nothing AndAlso Me.PermissionsForm.ContainsKey(CInt(PermissionsActionsForm.ModifyQuantityFixedAssetPerDay)) Then
            INDSpnTotalPerDay.ReadOnly = False
        End If
        INDSpnTotalFixedAsset.Focus()
    End Sub

    Public Function CalculateTotalFixedAsset(laboralHours As Integer, hoursProtocol As Integer, minutesProtocol As Integer) As Integer
        Return Math.Floor(laboralHours / (hoursProtocol + (minutesProtocol / 60)))
    End Function
#End Region

End Class