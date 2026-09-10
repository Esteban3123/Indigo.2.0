Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.DB
Imports Presentation.Controls.MVP
Imports DevExpress.Data.Linq
Imports Presentation.Base
Imports Presentation.Treasury.MVP
Imports Presentation.Controls
Imports Infrastructure.Data.Xpo.TreasuryRepository

Public Class Form1
    Implements ICards, ICustomizableForm

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Using Model As New MBusqueda
            Dim a As LinqInstantFeedbackSource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetSchedulePayment)
            Dim b As LinqInstantFeedbackSource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.GetSchedulePayment)
            GridControl1.DataSource = a
            SearchLookUpEdit1.Properties.DataSource = b
        End Using



    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements ICards.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Public Property CashReceiptConceptCommisionXPO As XPCollection(Of CashReceiptConceptXpo) Implements ICards.CashReceiptConceptCommisionXPO

    Public Property CashReceiptConceptIcaXPO As LinqInstantFeedbackSource Implements ICards.CashReceiptConceptIcaXPO

    Public Property CashReceiptConceptRtfXPO As LinqInstantFeedbackSource Implements ICards.CashReceiptConceptRtfXPO

    Public Property Code As String Implements ICards.Code

    Public Property IdCashReceiptConceptCommision As Integer Implements ICards.IdCashReceiptConceptCommision

    Public Property IdCashReceiptConceptICA As Integer Implements ICards.IdCashReceiptConceptICA

    Public Property IdCashReceiptConceptRTF As Integer Implements ICards.IdCashReceiptConceptRTF

    Public Property IdRetentionConceptCommision As Integer Implements ICards.IdRetentionConceptCommision

    Public Property IdRetentionConceptICA As Integer Implements ICards.IdRetentionConceptICA

    Public Property IdRetentionConceptRTF As Integer Implements ICards.IdRetentionConceptRTF

    Public Property IdThirdParty As Integer Implements ICards.IdThirdParty

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICards.MyLayoutControl
        Get

        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements ICards.MyTag
        Get

        End Get
    End Property

    Public Property NameCard As String Implements ICards.NameCard

    Public Property RetentionConceptCommisionXPO As XPInstantFeedbackSource Implements ICards.RetentionConceptCommisionXPO

    Public Property RetentionConceptICAXPO As XPInstantFeedbackSource Implements ICards.RetentionConceptICAXPO

    Public Property RetentionConceptRTFXPO As XPInstantFeedbackSource Implements ICards.RetentionConceptRTFXPO

    Public Property Sequense As Domain.Entities.TreasurySequence Implements ICards.Sequense

    Public Property Status As Boolean Implements ICards.Status

    Public Property ThirdPartyXPO As XPInstantFeedbackSource Implements ICards.ThirdPartyXPO
End Class