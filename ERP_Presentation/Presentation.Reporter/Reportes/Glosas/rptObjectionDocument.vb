Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraRichEdit.Model
Imports DevExpress.XtraReports.Parameters

Public Class rptObjectionDocument
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Const CNameReport = "Glosas.FrmObjectionsReception"

    Private _INDObjectionId As Integer

    Public Property INDObjectionId As Integer
        Get
            Return Me._INDObjectionId
        End Get
        Set(value As Integer)
            Me._INDObjectionId = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad operativa
    ''' </summary>
    Dim _operatingUnit As OperatingUnit

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Me.DataSource = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).GlosasService.GetCollection(Of Glosas_GlosaMovementGlosa)(Nothing, "InvoiceDetailId.ObjectionsReceptionDId.GlosaObjectionsReceptionCId.Id=" & INDObjectionId)

        If Me._operatingUnit IsNot Nothing Then
            Me.INDlblAddress.Text = If(Me._operatingUnit.Address IsNot Nothing, Me._operatingUnit.Address.Trim() & If(Me._operatingUnit.City IsNot Nothing, If(Me._operatingUnit.City.Name IsNot Nothing, " " & Me._operatingUnit.City.Name.Trim() & If(Me._operatingUnit.City.Department IsNot Nothing, " - " & Me._operatingUnit.City.Department.Name.Trim(), String.Empty), String.Empty), String.Empty), String.Empty)
            Me.INDlblPhoneEmail.Text = If(Me._operatingUnit.Phone IsNot Nothing, Me._operatingUnit.Phone.Trim() & If(Me._operatingUnit.EmailAudit IsNot Nothing, " - " & Me._operatingUnit.EmailAudit.Trim(), String.Empty), String.Empty)
        End If
    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptObjectionDocument.CNameReport
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Public totalGlosa As Double = 0
    Public totalAccepted As Double = 0
    Public totalGlosaGeneral As Double = 0
    Public totalAcceptedGeneral As Double = 0
    Public totalGlosaHeader As Double = 0

    Public Sub SumValGlosa_SummaryRowChanged(sender As Object, e As EventArgs)
        Dim movAux = CType(GetCurrentColumnValue("Movimientos"), Glosas_GlosaMovementGlosa)
        '   If movAux.InvoiceDetailId.ObjectionsReceptionDId.JournalVoucher = "1" Then
        '    totalGlosa += Convert.ToDouble(movAux.ValueGlosado)
        '    totalAccepted += Convert.ToDouble(movAux.ValueAcceptedFirstInstance)
        '   Else
        '    totalGlosa += Convert.ToDouble(movAux.ValueReiterated)
        '    totalAccepted += Convert.ToDouble(movAux.ValueAcceptedSecondInstance)
        '   End If

        If movAux.ValueReiterated > 0 Then
            totalGlosa += Convert.ToDouble(movAux.ValueReiterated)
            totalAccepted += Convert.ToDouble(movAux.ValueAcceptedSecondInstance)

        Else
            If movAux.MainGlosa = True Then
                totalGlosa += Convert.ToDouble(movAux.ValueGlosado)
            End If
            totalAccepted += Convert.ToDouble(movAux.ValueAcceptedFirstInstance)
        End If

    End Sub

    Public Sub SumValGlosa_SummaryReset(sender As Object, e As EventArgs)
        totalGlosa = 0
    End Sub

    Public Sub SumValGlosa_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs)
        e.Result = totalGlosa
        e.Handled = True
    End Sub

    Public Sub SumValAccepted_SummaryReset(sender As Object, e As EventArgs)
        totalAccepted = 0
    End Sub

    Public Sub SumValAccepted_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs)
        e.Result = totalAccepted
        e.Handled = True
    End Sub

    Public Sub SumTotalGlosa_SummaryRowChanged(sender As Object, e As EventArgs)
        Dim movAux = CType(GetCurrentColumnValue("Movimientos"), Glosas_GlosaMovementGlosa)
        '   If movAux.InvoiceDetailId.ObjectionsReceptionDId.JournalVoucher = "1" Then
        '    totalGlosaGeneral += Convert.ToDouble(movAux.ValueGlosado)
        '    totalAcceptedGeneral += Convert.ToDouble(movAux.ValueAcceptedFirstInstance)
        '   Else
        '    totalGlosaGeneral += Convert.ToDouble(movAux.ValueReiterated)
        '    totalAcceptedGeneral += Convert.ToDouble(movAux.ValueAcceptedSecondInstance)
        '   End If
        If movAux.ValueReiterated > 0 Then
            totalGlosaGeneral += Convert.ToDouble(movAux.ValueReiterated)
            totalAcceptedGeneral += Convert.ToDouble(movAux.ValueAcceptedSecondInstance)
        Else
            If movAux.MainGlosa = True Then
                totalGlosaGeneral += Convert.ToDouble(movAux.ValueGlosado)
            End If
            totalAcceptedGeneral += Convert.ToDouble(movAux.ValueAcceptedFirstInstance)
        End If
    End Sub

    Public Sub SumTotalGlosa_SummaryReset(sender As Object, e As EventArgs)
        totalGlosaGeneral = 0
    End Sub

    Public Sub SumTotalGlosa_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs)
        e.Result = totalGlosaGeneral
        e.Handled = True
    End Sub

    Public Sub SumTotalAccepted_SummaryReset(sender As Object, e As EventArgs)
        totalAcceptedGeneral = 0
    End Sub

    Public Sub SumTotalAccepted_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs)
        e.Result = totalAcceptedGeneral
        e.Handled = True
    End Sub

    Public Sub ValueObjectLbl_SummaryGetResult(sender As Object, e As DevExpress.XtraReports.UI.SummaryGetResultEventArgs)
        e.Result = totalGlosaHeader
        e.Handled = True
    End Sub

    Public Sub ValueObjLbl_SummaryReset(sender As Object, e As EventArgs)
        totalGlosaHeader = 0
    End Sub

    Public Sub ValueObjLbl_SummaryRowChanged(sender As Object, e As EventArgs)
        Dim movAux = CType(GetCurrentColumnValue("Movimientos"), Glosas_GlosaMovementGlosa)
        '   If movAux.InvoiceDetailId.ObjectionsReceptionDId.JournalVoucher = "1" Then
        '    totalGlosaHeader += Convert.ToDouble(movAux.ValueGlosado)
        '   Else
        '    totalGlosaHeader += Convert.ToDouble(movAux.ValueReiterated)
        '   End If
        If movAux.ValueReiterated > 0 Then
            totalGlosaHeader += Convert.ToDouble(movAux.ValueReiterated)
        Else
            If movAux.MainGlosa = True Then
                totalGlosaHeader += Convert.ToDouble(movAux.ValueGlosado)
            End If
        End If
    End Sub

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal operatingUnit As OperatingUnit)
        ' This call is required by the designer.
        InitializeComponent()

        Me._operatingUnit = operatingUnit

    End Sub

    Private Sub rptObjectionDocument_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdObjectionDocu").Value}
            CargarDataSource()
        End If
    End Sub
End Class