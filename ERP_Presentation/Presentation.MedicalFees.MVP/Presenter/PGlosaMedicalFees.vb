#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PGlosaMedicalFees

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IGlosaMedicalFees

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IGlosaMedicalFees)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()
        Me.Indigo = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub


    Public Sub ListGlosaMedicalFeesConcepts()
        Using Model As New MGlosaMedicalFees("")
            Me.View.ListGlosaMedicalFeesConcepts = Model.ListGlosaMedicalFeesConcepts()
        End Using
    End Sub

    Public Sub LoadAccountPayableBySupplier(SupplierId As Integer)
        Using Model As New MGlosaMedicalFees("")
            Me.View.ListAccountPayable = Model.ListAccountPayablebysupplier(SupplierId)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource 
    ''' </summary>
    Public Sub InitializeSupplier()
        Dim model As New MBusqueda
        Me.View.SuppliersDistributionLinesXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListSuppliersDistributionLines), DevExpress.Xpo.XPInstantFeedbackSource)
    End Sub


    ''' <summary>
    ''' Funcion para obtener placa por el Id
    ''' </summary>
    ''' <param name="Id">id de la unidad funcional</param>
    ''' <returns></returns>
    Public Function ListAccountPayableById(ByVal Id As Integer) As AccountPayableXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of AccountPayableXpo)(Nothing, filter).FirstOrDefault()
    End Function


#End Region

End Class
