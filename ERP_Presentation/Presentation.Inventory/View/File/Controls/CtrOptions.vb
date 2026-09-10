Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo
Imports Domain.Entities

Public Class CtrOptions

#Region "Properties and Variables"
    ''' <summary>
    ''' variable que indica si el popup de diagnosticos se abre por primera vez para llenar el datasource
    ''' </summary>
    Private _stateOpenPopUpDiagnostic As Boolean

    ''' <summary>
    ''' Obtiene o establece el id del diagnostico
    ''' </summary>
    Private Property DiagnosticId As Integer
        Get
            Return CType(INDsleDiagnostic.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleDiagnostic.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de diagnosticos
    ''' </summary>
    Private Property DiagnosticDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleDiagnostic.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDiagnostic.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto plano de la nota
    ''' </summary>
    Public Property TextPlainNote As String
        Get
            Return INDrecNote.Text
        End Get
        Set(value As String)

        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto de la nota en formato html
    ''' </summary>
    Public Property HtmTextNote As String
        Get
            Return INDrecNote.HtmlText
        End Get
        Set(value As String)
            INDrecNote.HtmlText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de los diagnosticos
    ''' </summary>
    Public Property ListDiagnostic As List(Of DiagnosticData)
        Get
            Return CType(INDgcDiagnostisData.DataSource, List(Of DiagnosticData))
        End Get
        Set(value As List(Of DiagnosticData))
            INDgcDiagnostisData.DataSource = value
        End Set
    End Property
#End Region

#Region "Events"
    ''' <summary>
    ''' Handles the Load event of the CtrOptions control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrOptions_Load(sender As Object, e As EventArgs) Handles Me.Load
        IndigoGridControl1.RefreshGrid(INDgcDiagnostisData)
    End Sub

    Public Sub CtrOptions_LoadPublic() Handles Me.Load
        INDgcDiagnostisData.DataSource = ListDiagnostic
        INDgcDiagnostisData.RefreshDataSource()
    End Sub
    ''' <summary>
    ''' Handles the Click event of the INDsbAddDiagnostic control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddDiagnostic_Click(sender As Object, e As EventArgs) Handles INDsbAddDiagnostic.Click
        If DiagnosticId <> 0 Then
            Dim _diagnostic As Object = Nothing
            _diagnostic = CType(CType(INDgvDiagnostic.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.DiagnosticXpo)
            Dim _diacnosticData As New DiagnosticData()
            With _diacnosticData
                .DiagnosticId = DiagnosticId
                .DiagnosticCode = _diagnostic.Code
                .DiagnosticName = _diagnostic.Name
                .Comment = ""
            End With
            If ListDiagnostic Is Nothing Then
                ListDiagnostic = New List(Of DiagnosticData)()
            End If
            ListDiagnostic.Add(_diacnosticData)
            INDgcDiagnostisData.RefreshDataSource()
        End If
    End Sub
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleDiagnostic control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleDiagnostic_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleDiagnostic.QueryPopUp
        If Not _stateOpenPopUpDiagnostic Then
            InitializeDiagnostic()
            _stateOpenPopUpDiagnostic = True
        End If
    End Sub
#End Region

#Region "Methods and Variables"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        _stateOpenPopUpDiagnostic = False
    End Sub

    ''' <summary>
    ''' carga el datasource de diagnosticos
    ''' </summary>
    Private Sub InitializeDiagnostic()
        Using Model As New MBusqueda
            Me.DiagnosticDatasource = Model.ConsultarEntidades(eDataSource.ListDiagnostic)
        End Using
    End Sub
#End Region

End Class

Public Class DiagnosticData

    Property Id As Integer

    Property DiagnosticId As Integer

    Property DiagnosticCode As String

    Property DiagnosticName As String

    Property Comment As String


End Class
