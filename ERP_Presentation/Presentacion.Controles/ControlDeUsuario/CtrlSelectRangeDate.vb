Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class CtrlSelectRangeDate

#Region "Properties"
    ''' <summary>
    ''' Gets or sets the initial date.
    ''' </summary>
    ''' <value>
    ''' The initial date.
    ''' </value>
    Public Property InitialDate As DateTime
        Get
            Return INDDeInitialDate.EditValue
        End Get
        Set(value As DateTime)
            INDDeInitialDate.Properties.MaxValue = value
            INDDeInitialDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets the end date.
    ''' </summary>
    ''' <value>
    ''' The end date.
    ''' </value>
    Public Property EndDate As DateTime
        Get
            Return INDDeEndDate.EditValue
        End Get
        Set(value As DateTime)
            INDDeEndDate.EditValue = value
        End Set
    End Property

    Public Event OnChangingDate(newDate As DateTime)
#End Region

#Region "Hnadler"
    ''' <summary>
    ''' Handles the Load event of the Control control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanging event of the INDDeInitialDate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDDeInitialDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDDeInitialDate.EditValueChanging
        If INDDeEndDate.EditValue IsNot Nothing AndAlso CType(e.NewValue, DateTime) > CType(INDDeEndDate.EditValue, DateTime) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "La Fecha Inicial no debe ser mayor a la Fecha Final"
            e.Cancel = True
        End If
        Dim dateNow As DateTime = Await New Controls.MVP.MformBase().GetDateServerAsync()
        If e.NewValue > dateNow Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "La Fecha Inicial no debe ser mayor a la Fecha Actual"
            e.Cancel = True
        End If
        'RaiseEvent OnChangingDate(e.NewValue)
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanging event of the INDDeEndDate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ChangingEventArgs"/> instance containing the event data.</param>
    Private Sub INDDeEndDate_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDDeEndDate.EditValueChanging
        If INDDeInitialDate.EditValue IsNot Nothing AndAlso CType(e.NewValue, DateTime) < CType(INDDeInitialDate.EditValue, DateTime) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "La Fecha Inicial no debe ser mayor a la Fecha Final"
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Sets the mensaje.
    ''' </summary>
    ''' <value>
    ''' The mensaje.
    ''' </value>
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

    ''' <summary>
    ''' Handles the StyleChanged event of the Control control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    ''' <summary>
    ''' Applies the style skin.
    ''' </summary>
    ''' <param name="skinName">Name of the skin.</param>
    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(Me.INDLcRoot)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(Me.INDLciFechaInicial)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(Me.INDLciFechaFinal)
    End Sub

    ''' <summary>
    ''' Gets the date values.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetDateValues() As Tuple(Of DateTime?, DateTime?)
        Return New Tuple(Of DateTime?, DateTime?)(CType(INDDeInitialDate.EditValue, DateTime?), CType(INDDeEndDate.EditValue, DateTime?))
    End Function
#End Region

End Class
