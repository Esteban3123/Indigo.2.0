'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Andrea Coqueco
' Created          : 11-05-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Glosas.MVP
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' Popup para asignación del tercero causante de la glosa
''' </summary>
Public Class FrmResponsibleThirdPartyPopUp

#Region "Propiedades"
    ''' <summary>
    ''' Id del Responsable
    ''' </summary>
    Public ResponsibleThirdPartyId As Integer
#End Region

#Region "Builder"
    Sub New()
        ' This call is required by the designer.
        InitializeComponent()
    End Sub
#End Region

#Region "ButtonClick"
    Private Sub INDBtnAddResponsibleThirdParty_Click(sender As Object, e As EventArgs) Handles INDBtnAddResponsibleThirdParty.Click
        If Me.INDSleResponsibleThirdParty.EditValue IsNot Nothing Then
            Me.ResponsibleThirdPartyId = Me.INDSleResponsibleThirdParty.EditValue
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    Private Sub INDSleResponsibleThirdParty_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponsibleThirdParty.QueryPopUp
        Using model As New MBusqueda
            Me.INDSleResponsibleThirdParty.Properties.DataSource = model.ConsultarEntidades(eDataSource.ListAllThirdParty)
        End Using
    End Sub
#End Region

End Class