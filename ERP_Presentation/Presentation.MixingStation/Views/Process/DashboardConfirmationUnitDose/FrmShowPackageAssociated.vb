'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 23/01/2021
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
Imports System.Windows.Forms
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmShowPackageAssociated

#Region "Variables"

    ''' <summary>
    ''' Id del paquete asociado
    ''' </summary>
    Public PackageId As Integer

    ''' <summary>
    ''' Paquete personalizado
    ''' </summary>
    ''' <returns></returns>
    Public Property PackagePersonalizedId As Integer?

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PDashboardConfirmationUnitDose

#End Region

#Region "Properties"

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

#Region "Methods"

    ''' <summary>
    ''' Consulta el paquete
    ''' </summary>
    Private Sub GetPackage()
        AsyncLoader(True)

        If PackagePersonalizedId.HasValue Then
            LoadPersonalizedPackage()
        Else
            LoadPackage()
        End If
    End Sub

    Private Sub LoadPersonalizedPackage()
        Dim packagePersonalized = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MixingStationService _
            .GetXPOObject(Of MixinStationPackagePersonalizedXpo)($"Id={PackagePersonalizedId.Value}")

        INDtxtConcentration.EditValue = packagePersonalized.Concentration
        INDseRefrigeratedTerm.EditValue = packagePersonalized.RefrigeratedTerm
        INDseEnvironmentalTemperatureTerm.EditValue = packagePersonalized.EnvironmentalTemperatureTerm
        AsyncLoader(False)

        INDgcComponents.DataSource = Nothing
        INDviewComponents.ShowLoadingPanel()

        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).MixingStationService _
                                            .GetCollection(Of PackagePersonalizedDetailXpo)(Nothing, $"PackagePersonalizedId={PackagePersonalizedId.Value}")

                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcComponents.SafeInvoke(Sub()
                                                                         INDviewComponents.HideLoadingPanel()
                                                                         INDgcComponents.DataSource = result
                                                                     End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcComponents.SafeInvoke(Sub()
                                                                         INDviewComponents.HideLoadingPanel()
                                                                         Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                     End Sub)
                                      End If
                                  End Try
                              End Sub, tokenAsync.Token)
    End Sub

    Private Sub LoadPackage()
        Dim packageXpo = Presenter.GetPackageAssociatedById(PackageId)
        INDtxtConcentration.EditValue = packageXpo.Concentration
        INDseRefrigeratedTerm.EditValue = packageXpo.StabilityHour
        INDseEnvironmentalTemperatureTerm.EditValue = packageXpo.EnvironmentalTemperatureTerm
        AsyncLoader(False)

        INDgcComponents.DataSource = Nothing
        INDviewComponents.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Try
                                      Dim result = Presenter.ListPackageDetailByPackageId(PackageId)
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcComponents.SafeInvoke(Sub()
                                                                         INDviewComponents.HideLoadingPanel()
                                                                         INDgcComponents.DataSource = result
                                                                     End Sub)
                                      End If
                                  Catch ex As Exception
                                      If Not tokenAsync.IsCancellationRequested Then
                                          INDgcComponents.SafeInvoke(Sub()
                                                                         INDviewComponents.HideLoadingPanel()
                                                                         Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                     End Sub)
                                      End If
                                  End Try
                              End Sub, tokenAsync.Token)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmShowPackageAssociated_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Presenter = New PDashboardConfirmationUnitDose
        GetPackage()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se ejecuta cuando se termine de pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmShowPackageAssociated_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDtxtConcentration.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAssignPackage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmShowPackageAssociated_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#End Region

#End Region

End Class