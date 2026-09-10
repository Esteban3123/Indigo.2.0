'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/02/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Common.MVP
Imports System.Threading
Imports DevExpress.XtraGrid.Views.Grid
#End Region

Public Class FrmImportQuotation

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PServiceOrder

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Código del paciente
    ''' </summary>
    Public PatientCode As String

    ''' <summary>
    ''' Listado de Id de los cups, este listado viene lleno cuando la importación se realiza desde 
    ''' los detalles de la orden de servicio, control cuentas hospitalario o ambulatorio, cuando no viene lleno es porque el form
    ''' se abre desde la cabecera de ordenes de servicio
    ''' </summary>
    Public ListCupsEntityId As List(Of Integer) = Nothing

    ''' <summary>
    ''' Campo que se utiliza en control servicios ambulatorios
    ''' </summary>
    Public arg As Object

    ''' <summary>
    ''' Campo que se utiliza en control servicios ambulatorios
    ''' </summary>
    Public senderForm As Object

    ''' <summary>
    ''' Permite saber si este form se abre desde el dashboard cotizaciones
    ''' </summary>
    Public IsDashboardQuoted As Boolean = False

#End Region

#Region "Public Event"

    ''' <summary>
    ''' Evento para importar la informacion
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ImportEvent(ByVal e As AddImportQuotationServiceOrderDetail)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
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

#Region "Methods"

    ''' <summary>
    ''' Ejecuta la consulta para traer los datos
    ''' </summary>
    Private Sub ExecuteGetList()
        INDviewQuotations.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListQuotationServiceOrderDetail(PatientCode, ListCupsEntityId)
                                  If Not tokenAsync.IsCancellationRequested Then
                                      INDgcQuotations.SafeInvoke(Sub()
                                                                     INDviewQuotations.HideLoadingPanel()
                                                                     INDgcQuotations.DataSource = result
                                                                 End Sub)
                                  End If
                              End Sub, tokenAsync.Token)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmImportQuotation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PServiceOrder()
        ExecuteGetList()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnImports_Click(sender As Object, e As EventArgs) Handles INDbtnImports.Click
        If INDviewQuotations.LoadingPanelVisible Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha terminado de cargar la rejilla"
            Exit Sub
        End If

        Dim ListDetails = (From x In DirectCast(INDgcQuotations.FocusedView, GridView).GetSelectedRows() Select CType(DirectCast(INDgcQuotations.FocusedView, GridView).GetRow(x), QuotationServiceOrderDetailXpo)).ToList()

        If ListDetails Is Nothing OrElse ListDetails.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item"
            Exit Sub
        End If

        Dim args As New AddImportQuotationServiceOrderDetail With {.ListQuotationServiceOrderDetailXpo = ListDetails, .arg = arg, .sender = senderForm}
        RaiseEvent ImportEvent(args)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmImportQuotation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmImportQuotation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Me.Close()
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar el check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewQuotations_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewQuotations.SelectionChanged
        'Se realiza la validación de solo poder seleccionar un solo cups siempre y cuando el form no se abra desde la cabecera de la orden de servicio
        If ListCupsEntityId IsNot Nothing AndAlso ListCupsEntityId.Count > 0 AndAlso IsDashboardQuoted = False Then
            Dim infoRowSelected As QuotationServiceOrderDetailXpo = INDviewQuotations.GetFocusedRow()
            Dim rowHandleInfoSelected = INDviewQuotations.FocusedRowHandle()
            Dim listRowHandles = INDviewQuotations.GetSelectedRows()
            If listRowHandles.Length > 0 Then
                For i = 0 To listRowHandles.Count - 1 Step 1
                    If rowHandleInfoSelected <> listRowHandles(i) Then
                        Dim row As QuotationServiceOrderDetailXpo = INDviewQuotations.GetRow(listRowHandles(i))
                        If row IsNot Nothing Then
                            If infoRowSelected.CUPSEntityId.Id = row.CUPSEntityId.Id Then
                                INDviewQuotations.UnselectRow(listRowHandles(i))
                            End If
                        End If
                    End If
                Next
            End If
        ElseIf ListCupsEntityId IsNot Nothing AndAlso ListCupsEntityId.Count > 0 AndAlso IsDashboardQuoted = True Then 'Si se abre desde el dashboard de cotizaciones se valida que solo seleccionen un item
            Dim rowHandleInfoSelected = INDviewQuotations.FocusedRowHandle()
            Dim listRowHandles = INDviewQuotations.GetSelectedRows()

            If listRowHandles.Length > 0 Then
                For i = 0 To listRowHandles.Count - 1 Step 1
                    If rowHandleInfoSelected <> listRowHandles(i) Then
                        INDviewQuotations.UnselectRow(listRowHandles(i))
                    End If
                Next
            End If
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddImportQuotationServiceOrderDetail
    Inherits EventArgs

    ''' <summary>
    ''' Listado de detalles que se seleccionaron
    ''' </summary>
    ''' <returns></returns>
    Property ListQuotationServiceOrderDetailXpo As List(Of QuotationServiceOrderDetailXpo)

    ''' <summary>
    ''' Campo que se utiliza en control servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Property arg As Object

    ''' <summary>
    ''' Campo que se utiliza en control servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Property sender As Object

End Class