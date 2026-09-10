#Region "Imports"

Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class FrmImportJournalVoucher

#Region "EVENTS"

    ''' <summary>
    ''' Evento para obtener el comprobante de contable seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="journalVoucherId"></param>
    ''' <remarks></remarks>
    Public Event GetJournalVoucher(sender As Object, journalVoucherId As Integer)

#End Region

#Region "GLOBALS"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Accounting"

    ''' <summary>
    ''' Id del Libro Contable
    ''' </summary>
    ''' <remarks></remarks>
    Dim _legalBookId As Integer

    ''' <summary>
    ''' Id del Tipo de Comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Dim _journalVoucherTypeId As Integer

#End Region

#Region "PROPERTIES"

    ''' <summary>
    ''' Propiedad para asignar el Id del libro contable
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property LegalBookId As Integer
        Set(value As Integer)
            _legalBookId = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar el Id del Tipo de Comprobante
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property JournalVoucherTypeId As Integer
        Set(value As Integer)
            _journalVoucherTypeId = value
        End Set
    End Property

#End Region

#Region "HANDLES"

#Region "Load"

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmImportJournalVouchers_Load(sender As Object, e As EventArgs) Handles MyBase.Load        
        IndigoGridControl1.RefreshGrid(INDGcImportJournalVoucher)

        Using model As New MVP.MDocumentAccount(Me.Tag)
            INDGcImportJournalVoucher.DataSource = Nothing
            INDGcImportJournalVoucher.DataSource = model.ListJournalVourchersByLegalBookAndJournalVoucherType(_legalBookId, _journalVoucherTypeId)
            INDGcImportJournalVoucher.RefreshDataSource()
        End Using
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el formulario está siendo eliminado, reinicia las variables y demás objetos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _legalBookId = Nothing
        _journalVoucherTypeId = Nothing
        LegalBookId = Nothing
        JournalVoucherTypeId = Nothing
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se ejecuta al presionar una tecla, verifica si la tecla presionada es "Esc" 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmImportInfo_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se ejecuta cuando se da click en el btn de "Aceptar"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        If INDGvImportJournalVoucher.GetFocusedRow Is Nothing Then
            MessageIndigo.Show("Debe seleccionar un registro", MessageType.Warning, Me.Text)
            Exit Sub
        End If

        Dim journalVoucher = DirectCast(DirectCast(INDGvImportJournalVoucher.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.JournalVouchersXpo)

        Me.Close()
        RaiseEvent GetJournalVoucher(Nothing, journalVoucher.Id)
    End Sub

#End Region

#End Region

End Class