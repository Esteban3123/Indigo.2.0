
#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports System.Text
Imports System.Windows.Forms
Imports System.IO
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Class FrmGenerateBankFileIncentivePayment

    Private Async Sub FrmGenerateBankFileIncentivePayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AsyncLoader(True)
        Using model As New MBankFile(Me.Tag)
            INDgleCompany.Properties.DataSource = Await model.ListAllCompany()
        End Using

        Using modelBusqueda As New MBusqueda
            INDsleBank.Properties.DataSource = modelBusqueda.ConsultarEntidades(eDataSource.ListEntityBankReportTreasury)
        End Using

        Using modelIncentive As New MIncentivePayment("597")
            Dim ListDates = Await modelIncentive.GetConfirmIncentivenDates()

            If ListDates IsNot Nothing AndAlso ListDates.Count() > 0 Then
                INDgleLastLiquidationDate.Properties.DataSource = ListDates
            Else
                INDgleLastLiquidationDate.Enabled = False
            End If

        End Using
        AsyncLoader(False)
    End Sub

    Private Async Sub INDButtonFileGenerate_Click(sender As Object, e As EventArgs) Handles INDButtonFileGenerate.Click
        If ValidateControls() = False Then
            Exit Sub
        End If

        Using model = New MIncentivePayment(MIncentivePayment.TAG)
            Dim CompanyId = INDgleCompany.EditValue
            Dim dateLiquidation As Date = CType(INDgleLastLiquidationDate.EditValue, Date).Date

            Dim objetoSeleccion = DirectCast(DirectCast(INDgvSleBank.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, TreasuryEntityBankAccountsXpo)

            AsyncLoader(True)
            Dim BankFileIncentivePayment = Await model.GenerateBankFileAsync(dateLiquidation, objetoSeleccion.IdBank.Id, INDsleBank.EditValue, objetoSeleccion.Type, CompanyId)
            AsyncLoader(False)

            If BankFileIncentivePayment.StateResult = True Then
                DialogGenerateFile(BankFileIncentivePayment.ObjectEmbbeded)
            End If

        End Using

    End Sub

    Function ValidateControls() As Boolean
        If INDsleBank.EditValue IsNot Nothing AndAlso CStr(INDsleBank.Text) <> "" Then
            If CStr(INDsleBank.EditValue.ToString()) = "-1 " Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(GrupoNoTieneEstructuraArch, Eform.ArchivoBanco)
                Return False
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Eform.Incapacidades), INDlyItemBank.Text)
            Return False
        End If

        If Not (INDgleLastLiquidationDate.EditValue IsNot Nothing AndAlso CStr(INDgleLastLiquidationDate.EditValue) <> "") Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Eform.Incapacidades), INDlyItemLastLiquidationDate.Text)
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Evento que se ejecuta al cargar la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As StringBuilder)
        Dim Random As New Random()
        Dim numero As Integer = Random.Next(0, 45)
        Dim save As SaveFileDialog = New SaveFileDialog()
        save.Filter = "Texto|*.txt"
        'save.Title = INDlyGrBankFile.Text 
        Dim dateLiquidation As Date = CType(INDgleLastLiquidationDate.EditValue, Date).Date
        save.FileName = "Primas Mes " & Month(dateLiquidation) & " - " & Year(dateLiquidation) & ".txt"
        If save.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim file = save.OpenFile()
            Dim streamWrite As New StreamWriter(file)
            streamWrite.Write(content)
            streamWrite.Flush()
            streamWrite.Close()
            If MessageIndigo.Show(obtenerRecurso(GuardadoDeseaAbrir, Eform.ArchivoBanco), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Process.Start(save.FileName)
            End If
        End If
    End Sub

    Private Sub INDsleBank_CustomDisplayText(sender As Object, e As DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs) Handles INDsleBank.CustomDisplayText

        If INDgvSleBank.GetFocusedRow IsNot Nothing Then
            Dim objetoSeleccion = DirectCast(DirectCast(INDgvSleBank.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, TreasuryEntityBankAccountsXpo)

            Dim AccountBank As String = objetoSeleccion.Number
            Dim NameBank As String = objetoSeleccion.IdBank.Name
            e.DisplayText = AccountBank + " - " + NameBank
        End If

        

    End Sub
End Class