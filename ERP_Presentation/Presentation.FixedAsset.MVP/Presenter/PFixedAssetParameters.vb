'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 27-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PSettingFixedAsset

#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim View As ISettingFixedAsset
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ISettingFixedAsset)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Function GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer) As PaymentsSettingPaymentsXpo
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Function

    Public Sub InitializeServiceMainAccount()
        View.ServiceMainAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True)
    End Sub

    Public Sub InitializeIVAFreightAccountPayableConcept()
        View.IVAFreightAccountPayableConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableConceptByHandlesRetentionAndConceptType(True, False, 2)
    End Sub

    Public Sub InitializeFreightAccountPayableConcept()
        View.FreightAccountPayableConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableConceptByHandlesRetentionAndConceptType(True, False, 2)
    End Sub

    Public Sub InitializeIVAAccountPayableConcept()
        View.IVAAccountPayableConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableConceptByHandlesRetentionAndConceptType(True, False, 1)
    End Sub

    Public Sub InitializeIVARetentionAccountPayableConcept()
        View.IVARetentionAccountPayableConceptXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.ListAccountPayableConceptByHandlesRetentionAndConceptType(True, True, 2)
    End Sub

    'Concepto de nota de pago para cuando se haga devolucion del comprobante de entrada

    Public Sub InitializeRefundAccountPayableConceptNote()
        Dim filter() As Object = {True, 1}
        Using model As New MBusqueda
            Me.View.RefundAccountPayableConceptNoteXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsNotesByConcepType, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeLegalBook()
        View.LegalBookXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Sub

    ''' <summary>
    ''' Metodo que conecta al modelo para ejecutar el metodo que trae un colección desde el xpo
    ''' </summary>
    Public Sub GetListCurrency()
        Using model As New MSettingFixedAsset("")
            Me.View.ListCurrency = XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.getCollectionCurrency
        End Using
    End Sub

#End Region

End Class
