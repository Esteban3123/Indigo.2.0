Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP

Public Class FrmMedicalFeesCausationModal

    Private Sub FrmMedicalFeesCausationModal_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub FrmMedicalFeesCausationModal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()
    End Sub

    Private Sub LoadData()

    End Sub

    ''' <summary>
    ''' Metodo que actualiza el datasource del repositorio de médico
    ''' dependiendo si el serviceOrderDetail maneja el tipo de liquidación
    ''' especialidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Private Sub RefreshQueryPopup(sender As Object, view As DevExpress.XtraGrid.Views.Grid.GridView)
        Dim controlSearch As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim viewSearch As GridView = controlSearch.Properties.View
        Dim itemCollection = view.GetFocusedRow()
        If itemCollection.LiquidationType = 2 Then
            viewSearch.ActiveFilterString = "CODESPEC1.CODESPECI='" & itemCollection.PerformsProfessionalSpecialty & "' Or CODESPEC2.CODESPECI='" & itemCollection.PerformsProfessionalSpecialty & "' Or CODESPEC3.CODESPECI='" & itemCollection.PerformsProfessionalSpecialty & "'"
        Else
            viewSearch.ActiveFilterString = String.Empty
        End If
        viewSearch.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepSleHealthProfessionalGridNoSurgical_Popup(sender As Object, e As EventArgs) Handles INDrepSleHealthProfessionalGridNoSurgical.Popup
        RefreshQueryPopup(sender, ViewNoSurgical)
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Evento que se dispara al cambiar el valor del repositorio de cambiar el médico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDrepSleHealthProfessionalGridNoSurgical_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepSleHealthProfessionalGridNoSurgical.EditValueChanging
        If e.NewValue IsNot Nothing Then
            Try
                AsyncLoader(True)
                Dim healthProfessional As Infrastructure.Data.Xpo.CrystalRepository.HealthCareProfessionalXpo
                Dim itemCollection As Domain.Entities.ViewListNoSurgical
                Using modelServiceOrder As New MServiceOrder(Me.Tag)
                    Dim healthProfessionalTmp = modelServiceOrder.GetCareProfessionalByCode(e.NewValue.ToString.Trim)
                    healthProfessional = healthProfessionalTmp(0)

                    Using modelThirdParty As New MThirdParty(Me.Tag)
                        Dim thirdParty = modelThirdParty.GetThirdParty(healthProfessional.CODIGONIT.TrimStart("0"))
                        If thirdParty.Id = 0 Then
                            AsyncLoader(False)
                            'si el medico no esta creado como tercero en la BD no continua el proceso
                            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ProfessionalNotThirdParty", "Billing"), healthProfessional.CodeName)
                            e.Cancel = True
                            Exit Sub
                        End If

                        itemCollection = ViewNoSurgical.GetFocusedRow()
                        itemCollection.PerformsHealthProfessionalCode = e.NewValue
                        itemCollection.ThirdPartyId = thirdParty.Id
                        itemCollection.ThirdPartyDescription = healthProfessional.CodeName
                    End Using

                    Dim result As ActionResult(Of ServiceOrderDetail) = Await modelServiceOrder.UpdateFieldsServiceOrderDetail(itemCollection.ServiceOrderDetailId, itemCollection.PerformsHealthProfessionalCode, itemCollection.ThirdPartyId)
                    If result.StateResult = False Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = result.MessageResult.ToString
                        Exit Sub
                    End If
                End Using
                AsyncLoader(False)
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Sub

End Class