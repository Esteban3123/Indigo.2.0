Imports System.ComponentModel
Imports System.Globalization

Public Class CtrMoreInfoDashboardPharmacy

    ''' <summary>
    ''' Delegado de la funcion que establece la informacion
    ''' </summary>
    ''' <returns></returns>
    Public Delegate Function SetInfoDelegate() As Tuple(Of String, String, Date?)
    ''' <summary>
    ''' variable de tipo del delegado
    ''' </summary>
    Private _setInfoDelegate As SetInfoDelegate

    ''' <summary>
    ''' Obtiene o asigna el PopupContainerControl que será lanzado
    ''' </summary>
    ''' <value>PopupContainerControl que se lanzará</value>
    ''' <returns>El PopupContainerControl que se lanzará</returns>
    <BrowsableAttribute(False)>
    Public Property PopupContainerControl As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return Me.PcePopUpEditAdmission.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            Me.PcePopUpEditAdmission.Properties.PopupControl = value
        End Set
    End Property

    WriteOnly Property Patient As String
        Set(value As String)
            INDLblPatient.Text = value
            INDLblPatient.ToolTip = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de nacimiento
    ''' </summary>
    WriteOnly Property BirthDay As Date?
        Set(value As Date?)
            INDLblBirthDay.Text = String.Format("Fecha de nacimiento : {0}", Format(value, "d/MMM/yyyy"))
            INDLblBirthDay.ToolTip = "Fecha de Nacimiento"
        End Set
    End Property

    WriteOnly Property AdmissionNumber As String
        Set(value As String)
            PcePopUpEditAdmission.Text = "Ingreso: " + value
        End Set
    End Property

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Presentation.Resources.ThemeResourceManager.SetStyleThemeOnControl(Me)
    End Sub

    ''' <summary>
    ''' Asigna el delegado que se da al ejecutar para obtener el valor del comprobante
    ''' </summary>
    Public Sub SetInfoFunction(setInfoDelegate As SetInfoDelegate)
        _setInfoDelegate = setInfoDelegate
    End Sub

    ''' <summary>
    ''' Muestra el valor del comprobante de egreso
    ''' </summary>
    Public Sub PrintInfo()
        If _setInfoDelegate IsNot Nothing Then
            Dim value As Tuple(Of String, String, Date?) = _setInfoDelegate()
            Patient = value.Item1
            AdmissionNumber = value.Item2
            BirthDay = value?.Item3
        End If
    End Sub

    Public Event ClosePopupInfo(sender As Object, e As EventArgs)

    Private Sub PcePopUpEditAdmission_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles PcePopUpEditAdmission.Closed
        RaiseEvent ClosePopupInfo(Nothing, EventArgs.Empty)
    End Sub
End Class
