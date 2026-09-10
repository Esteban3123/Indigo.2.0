'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 18-03-2014
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
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP

#End Region

''' <summary>
''' Presentador del frontal Tarjetas
''' </summary>
Public Class PCards

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ICards

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ICards)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Carga todos los terceros
    ''' </summary>
    Public Sub InitializeThirdPartyXPO()
        Dim modelThirdPartyXPO As New MBusqueda
        Me.View.ThirdPartyXPO = modelThirdPartyXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Initializes the retention concept.
    ''' </summary>
    Public Sub InitializeCashReceiptConceptIcaXPO()
        Dim modelXpo As New MBusqueda
        Dim filter() As Object = {3, True}
        Me.View.CashReceiptConceptIcaXPO = modelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConceptsByRetentionType, filter)
    End Sub

    ''' <summary>
    ''' Initializes the retention concept.
    ''' </summary>
    Public Sub InitializeCashReceiptConceptRtfXPO()
        Dim modelXpo As New MBusqueda
        Dim filter() As Object = {1, True}
        Me.View.CashReceiptConceptRtfXPO = modelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListCashReceiptConceptsByRetentionType, filter)
    End Sub

    ''' <summary>
    ''' Initializes the retention concept.
    ''' </summary>
    Public Sub InitializeCashReceiptConceptCommisionXPO()
        Dim modelXpo As New MBusqueda
        Dim filter() As Object = {False, True}
        Me.View.CashReceiptConceptCommisionXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListCashReceiptConceptCollection(True)
    End Sub

    ''' <summary>
    ''' inicializa la consulta de conceptos de retencion
    ''' </summary>
    Public Sub InitializeRetentionConceptXPO()
        Using model As New MBusqueda
            Dim filter() As Object = {1, True}
            Me.View.RetentionConceptRTFXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConceptByTypeRetention, filter)
            Me.View.RetentionConceptICAXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConceptByTypeRetention, filter)
            Me.View.RetentionConceptCommisionXPO = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConceptByTypeRetention, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

End Class
