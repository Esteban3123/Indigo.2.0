'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/01/2019
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports DevExpress.XtraGrid.Columns
Imports System.Drawing
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports System.Globalization
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Text
Imports Domain.Base.Entities
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Billing.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports DevExpress.XtraSplashScreen
Imports System.ComponentModel

#End Region

Public Class FrmSelectedReport

#Region "Variables"

    ''' <summary>
    ''' Datasource search diferido
    ''' </summary>
    Private ListType As List(Of Tuple(Of Integer, String))

    Public InvoiceId As Integer

    Public AdmissionNumber As String

    Public InvoiceNumber As String

#End Region

#Region "PublicEvents"

    Public Event SetTypeReport(sender As Object, e As SelectReportEventArgs)

#End Region

#Region "Methods"

    Private Sub InitializeTuple()
        ListType = New List(Of Tuple(Of Integer, String))()
        ListType.Add(New Tuple(Of Integer, String)(1, "Tirilla"))
        ListType.Add(New Tuple(Of Integer, String)(2, "Media Carta"))
        INDsleTypeReport.Properties.DataSource = ListType
    End Sub

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

#Region "Hanlders"

#Region "Shown"

    Private Sub FrmSelectedReport_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        InitializeTuple()
        INDsleTypeReport.EditValue = 1
        INDsleTypeReport.Focus()
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAcept_Click(sender As Object, e As EventArgs) Handles INDbtnAcept.Click
        If INDsleTypeReport.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de reporte"
            Exit Sub
        End If

        Dim args As New SelectReportEventArgs
        args.TypeReport = INDsleTypeReport.EditValue
        args.InvoiceId = InvoiceId
        args.AdmissionNumber = AdmissionNumber
        args.InvoiceNumber = InvoiceNumber
        RaiseEvent SetTypeReport(Nothing, args)
        Me.Close()
    End Sub

#End Region

#End Region

End Class

Public Class SelectReportEventArgs
    Inherits EventArgs

    Public TypeReport As Integer

    Public InvoiceId As Integer

    Public AdmissionNumber As String

    Public InvoiceNumber As String

End Class