'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/07/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PTransfers

#Region "Construct"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ITransfers

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ITransfers)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Functions"

    ''' <summary>
    ''' Obtiene la secuencia numerica del formulario de traslados
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de centro de costo
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenter()
        'Using model As New MBusqueda
        View.CostCenterXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Inicializa los proveedores con sus lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeSupplier()
        Using model As New MBusqueda
            View.SupplierXpo = CType(model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Supplier), DevExpress.Xpo.XPInstantFeedbackSource)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para consultar los anticipos a proveedores
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAdvancePayments(ByVal IdSupplier As Integer)
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {IdSupplier, 2, 2}
            View.AdvancePaymentsXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAdvancePayments, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para consultar las facturas que tiene el proveedor seleccionado
    ''' </summary>
    ''' <param name="IdSupplier"></param>
    ''' <remarks></remarks>
    Public Sub InitializeAccountPayable(ByVal IdSupplier As Integer, ByVal TransferType As Integer)
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {IdSupplier, 2, TransferType}
            View.AccountPayableSharesDatasource = ModelXpo.ConsultarEntidades(eDataSource.ListSharesWithAccountPayable, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Establece el datasource del concepto de notas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAccountPayableConceptNote()
        View.AccountPayableConceptNoteXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableConceptNoteByStatus(True)
    End Sub

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeMainAccountOtherConcept()
        View.MainAccountOtherConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True)
    End Sub

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeThirdPartyOtherConcept()
        View.ThirdPartyOtherConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetThirdParty()
    End Sub

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCostCenterOtherConcept()
        View.CostCenterOtherConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Sub

    ''' <summary>
    ''' Obtiene el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetAdvancePaymentsById(Id As Integer) As AdvancePaymentsXpo
        Dim filter As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of AdvancePaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene el anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetListAccountPayableSharesById(Id As Integer) As List(Of AccountPayableSharesXpo)
        Dim filter As String = "IdAccountPayable.Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of AccountPayableSharesXpo)(Nothing, filter).ToList()
    End Function

#End Region

End Class
