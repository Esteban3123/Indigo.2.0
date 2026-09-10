'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/02/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Controls.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Billing.MVP

#End Region

Public Class FrmImportInfoQuotation

#Region "Globals"

    ''' <summary>
    ''' Presentador de cotizaciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PQuotation

#End Region

#Region "Public Event"

    ''' <summary>
    ''' Evento para importar la informacion
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ImportInfoEvent(ByVal e As AddInfoImportQuotation)

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDgcInfo)
        Presenter = New PQuotation()
        INDgcInfo.DataSource = Nothing
        INDgcInfo.DataSource = Presenter.ListQuotationImportInfo()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnImportInfo_Click(sender As Object, e As EventArgs) Handles INDbtnImportInfo.Click
        ImportInfo()
    End Sub

#End Region

#Region "RowClick"

    ''' <summary>
    ''' Metodo que importa la información al form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewInfo_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles viewInfo.RowClick
        If e.Clicks = 2 AndAlso e.RowHandle >= 0 Then
            ImportInfo()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que importa la información al form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImportInfo()
        Dim QuotationId As Integer = viewInfo.GetRowCellValue(viewInfo.FocusedRowHandle, "Id")
        Dim args As New AddInfoImportQuotation With {.QuotationId = QuotationId}
        RaiseEvent ImportInfoEvent(args)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Slide de Mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

End Class

Public Class AddInfoImportQuotation
    Inherits EventArgs

    Property QuotationId As Integer

End Class