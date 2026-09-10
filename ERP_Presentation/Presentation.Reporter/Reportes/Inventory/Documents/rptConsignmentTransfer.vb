'*************************************************************
' Assembly         : Presentation.Reporter
' Author           : Mariana Gonzalez Calderon 
' Created          : 30-10-2025
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Librerias Improtadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Domain.Entities
Imports DevExpress.XtraReports.UI
Imports System.Drawing.Printing
Imports DevExpress.XtraReports.Parameters
Imports Infrastructure.Data.Xpo.SecurityRepository
#End Region

Public Class rptConsignmentTransfer

    Implements IReport

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para almacener lsitado de traslado en cosignacion
    ''' </summary>
    Dim INDList As List(Of ConsignmentTransferXpo)


    ''' <summary>
    ''' Carga el origen de datos (DataSource) para el reporte de transferencias de consignación.
    ''' Obtiene el registro correspondiente al Id indicado en los parámetros del reporte,
    ''' consulta el usuario que creó la transferencia y asigna su nombre al campo visual del reporte.
    ''' </summary>
    Public Sub CargarDataSource() Implements IReport.CargarDataSource
        Dim filters As String = "Id = " & ParametrosReporte(0)
        INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of ConsignmentTransferXpo)(Nothing, filters)
        If INDList.Count > 0 Then
            Dim INDNameUser = CType(INDList(0), ConsignmentTransferXpo).CreationUser.Trim()

            Dim INDListUser = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, "UserCode = '" & INDNameUser & "'")

            If INDListUser IsNot Nothing AndAlso INDListUser.Count > 0 Then
                Dim INDCodName = CType(INDListUser(0), UserXpo).CodeName.Trim
                Me.INDUserCreate.Text = INDCodName
            End If
        End If
        Me.DataSource = INDList
    End Sub

    ''' <summary>
    ''' Carga y devuelve la colección de registros de transferencia de consignación
    ''' según el identificador especificado en los parámetros del reporte.
    ''' Obtiene además el usuario creador del registro y muestra su nombre
    ''' en el control visual del reporte.
    ''' </summary>
    Public Function LoadDatasource() As DevExpress.Xpo.XPCollection(Of ConsignmentTransferXpo)
        Try
            Dim filters As String = "Id = " & ParametrosReporte(0)

            INDList = XpoServiceEx.Instance(IndigoSessionValues.TransactionalContainer).BillingService.GetCollection(Of ConsignmentTransferXpo)(Nothing, filters)
            If INDList.Count > 0 Then
                Dim INDNameUser = CType(INDList(0), ConsignmentTransferXpo).CreationUser.Trim()
                Dim listUsers = XpoServiceEx.Instance(IndigoSessionValues.SecurityContainer).SecurityService.ListAllUserXpCollection(INDNameUser)
                Dim user = Nothing
                If listUsers IsNot Nothing AndAlso listUsers.Count > 0 Then
                    user = listUsers.ToEntityList(Of UserXpo).FirstOrDefault
                End If
                If user IsNot Nothing Then
                    Dim INDCodName = user.CodeName
                    Me.INDUserCreate.Text = INDCodName
                End If
            End If

            Me.DataSource = INDList
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property

    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    ''' Evento que se ejecuta antes de la impresión del reporte de transferencias de consignación.
    ''' Asigna los parámetros del reporte, carga el origen de datos correspondiente y
    ''' establece los valores de encabezado con la información de la compañía y el usuario actual.
    ''' </summary>
    Private Sub rptConsignmentTransfer_BeforePrint(sender As Object, e As PrintEventArgs) Handles MyBase.BeforePrint
        If Me.Parameters.Count > 0 AndAlso Me.Parameters(0).Value > 0 Then
            Dim reportParams As ParameterCollection = Me.Parameters
            ParametrosReporte = New Object() {reportParams("INDIdConsignmentTransfer").Value}
            CargarDataSource()
        End If

        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
        INDLblNitCompany.Text = "Nit: " & IndigoSessionValues.IndigoCompanyNit
        INDUserImp.Text = "Usuario Impresión : " & IndigoSessionValues.UserIndigo & " - " & IndigoSessionValues.UserIndigoName
    End Sub

End Class
