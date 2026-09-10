#Region "Imports"

Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class XtraReportBase

#Region "Handlers"

    ''' <summary>
    ''' Aqui se asigna el texto al label de CopyRight
    ''' </summary>
    Private Sub XtraReportBase_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.BeforePrint
        Me.LblCopyRight.Text = String.Format(ResourceManager.GetString("CopyRightReport"), SessionValues.Instance.IndigoCompanyName)
    End Sub

#End Region

#Region "Members"

    ''' <summary>
    ''' Obtiene el texto mostrado en el label de CopyRight
    ''' </summary>
    ''' <returns>Texto mostrador en el label de CopyRight</returns>
    Protected Property CopyRightMessage As String
        Get
            Return Me.LblCopyRight.Text
        End Get
        Set(value As String)
            Me.LblCopyRight.Text = value
        End Set
    End Property

#End Region

End Class