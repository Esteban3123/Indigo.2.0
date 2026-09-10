#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
#End Region

Public Class rptSupportPaymentSuppliers
    Implements IReport
    Implements IReportAsync

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim IndigoSessionValues As SessionValues = SessionValues.Instance

    Private filters As Dictionary(Of String, String)

    ''' <summary>
    ''' Obtiene la lista de los datos a imprimir
    ''' </summary>
    Private result As ActionResult(Of List(Of SP_SupportPaymentSuppliers_Result))


    Public Sub CargarDataSource() Implements IReport.CargarDataSource
    End Sub


    ''' <summary>
    ''' Esta función es una implementación de la interfaz IReportAsync y
    ''' se encarga de cargar los datos necesarios para alimentar un informe de manera asincrónica.
    ''' </summary>
    ''' <returns></returns>
    Public Async Function CargarDataSourceAsync() As Task Implements IReportAsync.CargarDataSourceAsync

        If ParametrosReporte Is Nothing OrElse Not ParametrosReporte?.Any() Then
            Return
        End If

        Dim XmlFilters As String = TryCast(ParametrosReporte?.FirstOrDefault, System.Collections.Generic.IDictionary(Of String, Object))?.Item("XmlFilter")

        result = Await IndigoConecta.Instancia.CurrentCloud.IndigoPayments.GetSupportPaymentSuppliersAsync(XmlFilters)
        If result IsNot Nothing AndAlso result?.ObjectEmbbeded?.Any() Then
            DataSource = result.ObjectEmbbeded
        Else
            DataSource = Nothing
        End If
    End Function

    ''' <summary>
    ''' La función CargarImagenes implementa la interfaz IReport
    ''' pero actualmente no contiene ninguna lógica o código dentro de ella.
    ''' </summary>
    Public Sub CargarImagenes() Implements IReport.CargarImagenes

    End Sub

    ''' <summary>
    ''' La propiedad NameReport implementa la interfaz IReport y proporciona un valor de cadena vacía ("") como resultado.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property NameReport As String Implements IReport.NameReport
        Get
            Return ""
        End Get
    End Property


    ''' <summary>
    ''' La propiedad ParametrosReporte implementa la interfaz IReport y
    ''' permite almacenar y acceder a un conjunto de parámetros relacionados con un informe
    ''' </summary>
    ''' <returns></returns>
    Public Property ParametrosReporte As Object() Implements IReport.ParametrosReporte

    ''' <summary>
    '''  La función principal de este evento es establecer el texto de la etiqueta antes de que se lleve a cabo la impresión
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDLblCompany_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblCompany.BeforePrint
        INDLblCompany.Text = IndigoSessionValues.IndigoCompanyName
    End Sub


    ''' <summary>
    '''  La función principal de este evento es establecer el texto de la etiqueta antes de que se lleve a cabo la impresión
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDLblNitCompany_BeforePrint(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDLblNitCompany.BeforePrint
        INDLblNitCompany.Text = "Nit:" & IndigoSessionValues.IndigoCompanyNit
    End Sub
End Class