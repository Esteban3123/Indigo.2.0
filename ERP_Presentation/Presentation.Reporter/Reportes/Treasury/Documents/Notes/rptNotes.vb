#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters

#End Region

Public Class rptNotes
    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion.
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    Dim INDIdCostCenter

    Const CNameReport = "Administracion de Efectivo.FrmNotes"

    Private INDValue As Double

    Private INDUser As Object

    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filtroConsulta As String = "TreasuryNoteId.Id = " & ParametrosReporte(0)

        Dim INDList As Object = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryNotesDetailXpo)(Nothing, filtroConsulta)

        If INDList IsNot Nothing AndAlso INDList.Count > 0 Then 'Asi estaba originalmente con solo esta porcion de codigo
            Dim INDCodeUser = CType(INDList(0), TreasuryNotesDetailXpo).TreasuryNoteId.CreationUser.Trim()
            INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
            If INDList.Count > 0 Then
                INDIdCostCenter = CType(INDList(0), TreasuryNotesDetailXpo).TreasuryNoteId.CostCenterId
                INDValue = CType(INDList(0), TreasuryNotesDetailXpo).TreasuryNoteId.Value
            End If
        Else 'Se agrego este else mientras los de medilaser pasan los reportes para saber como hacerlos, mientras se deja asi para que no se reviente al usuario
            filtroConsulta = "Id = " & ParametrosReporte(0)
            INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).TreasuryService.GetCollection(Of TreasuryNotesXpo)(Nothing, filtroConsulta)

            Dim INDCodeUser = CType(INDList(0), TreasuryNotesXpo).CreationUser.Trim()
            INDUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).TreasuryService.GetCollection(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)(Nothing, "UserCode = '" & INDCodeUser & "'")
            If INDList.Count > 0 Then
                INDIdCostCenter = CType(INDList(0), TreasuryNotesXpo).CostCenterId
                INDValue = CType(INDList(0), TreasuryNotesXpo).Value
            End If
        End If


        Me.DataSource = INDList

    End Sub

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return rptNotes.CNameReport
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    Private Sub rptNotes_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.BeforePrint
        'Dim filtroConsulta As String = "Id = " & ParametrosReporte(0)
        If Me.Parameters.Count > 0 And Me.Parameters(0).Value > 0 Then
            Dim ParametrosFilter As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {ParametrosFilter("INDSubIdNotes").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
        Me.INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
        If INDUser.count() > 0 Then
            INDUserCreate.Text = INDUser(0).CodeName
        End If

        INDValue = Math.Round(INDValue, 2)
        Dim parteEntera As Decimal = Math.Truncate(INDValue) 'esto solo toma la parte entera
        Dim parteDecimal As Decimal = (INDValue - parteEntera) * 100 'asi solo sacaria la parte decimal

        If parteDecimal > 0 Then
            Me.XrTableCell12.Text = Utils.Num2Text(parteEntera) & " PESOS CON " & Utils.Num2Text(parteDecimal) & " CENTAVOS M/Cte."
        Else
            Me.XrTableCell12.Text = Utils.Num2Text(INDValue) & " PESOS M/Cte."
        End If

        Dim table As XRTable = CType(XrTable1, XRTable)

        If INDIdCostCenter Is Nothing Then
            If table.Rows("XrTableRow6") IsNot Nothing Then
                table.Rows.Remove(XrTableRow6)
            End If
        End If
    End Sub

End Class