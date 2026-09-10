'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 28-10-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base
Imports Presentation.Controls.MVP
#End Region

Public Class PServiceOrder
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IServiceOrder

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IServiceOrder)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub
    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MBlockRecordAndSequense(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene los servicios de las cotizaciones confirmadas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListQuotationServiceOrderDetail(patientCode As String, listCupsEntityId As List(Of Integer)) As List(Of QuotationServiceOrderDetailXpo)
        Dim filter As String = ""

        If listCupsEntityId Is Nothing Then 'Si el listado viene vacío es porque la importación se realiza desde la cabecera de la orden de servicio
            filter = "QuotationId.Status = 2 and QuotationId.ThirdPartyId.Nit = '" & patientCode & "' and (ServiceOrderDetailXpo is null or ServiceOrderDetailXpo.Count() = 0)"
        Else 'Si el listado viene lleno es porque la importación se realiza desde el detalle de la orden, control cuentas hospitalario o ambulatorio
            Dim joinCupsIds As String = String.Join(",", listCupsEntityId.ToArray())
            filter = "QuotationId.Status = 2 and QuotationId.ThirdPartyId.Nit = '" & patientCode & "' and (ServiceOrderDetailXpo is null or ServiceOrderDetailXpo.Count() = 0) and CUPSEntityId.Id in (" & joinCupsIds & ")"
        End If

        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.GetCollection(Of QuotationServiceOrderDetailXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el parámetro de contrato por unidad operativa
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer) As SettingsContractXpo
        Dim filter As String = "OperatingUnitId = " & operatingUnitId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of SettingsContractXpo)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' carga el valor de la bandera para saber si el sistema es impuesto incluido o no
    ''' </summary>
    Public Async Function LoadFlagTaxInclude() As Task
        Using Model As New MServiceOrder("")
            Dim CompanySettings = Await Model.CompanySettings
            Me.View.FlagTaxInclude = CompanySettings.SalePriceIncludeTax
        End Using
    End Function

End Class
