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
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Accounting.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class PProductInvoice
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IProductInvoice

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IProductInvoice)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
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
        Using modelCommmonInventoy As New Presentation.Billing.MVP.MBlockRecordAndSequense(View.MyTag)
            Me.View.Sequence = Await modelCommmonInventoy.GetSequense()
        End Using
    End Sub


    ''' <summary>
    ''' Lista las sucursales por tercero
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBranchOfficeByThirdPartyId(ThirdPartyId As Integer)
        View.BranchOfficeXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListBranchOfficeByThirdPartyId(ThirdPartyId, True)
    End Sub

    Public Sub GetRetentionConceptByBranchOfficeId(brachOfficeId As Integer)
        Using model As New MRetentionConcept(Me.View.MyTag)
            Me.View.RetentionConceptBranchTask = model.GetRetentionConceptByIdBrachOfficeIdAsync(brachOfficeId)
        End Using
    End Sub

    ''' <summary>
    ''' Obtiene el tercero por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetThirdPartyById(Id As Integer) As CommonThirdPartyXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCollection(Of CommonThirdPartyXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    Public Sub GetIVARetentionConceptByThirdPartyId(thirdPartyId As Integer)
        Using model As New MRetentionConcept(Me.View.MyTag)
            Me.View.RetentionConceptThirdTask = model.GetIVARetentionConceptByThirdPartyIdAsync(thirdPartyId)
        End Using
    End Sub

    ''' <summary>
    ''' Datasource de las remissiones de salida a importar.
    ''' </summary>
    ''' <param name="WareHouseId"></param>
    ''' <returns></returns>
    Public Function GetRemissionOutput(WareHouseId As Integer, _thirdPartyId As Integer) As List(Of InventoryRemissionOutputDetailPhysicalReportXpo)
        Dim filter As String = $"RemissionOutputDetailId.RemissionOutputId.Status = 2 AND RemissionOutputDetailId.RemissionOutputId.WarehouseId.Id = {WareHouseId} AND RemissionOutputDetailId.RemissionOutputId.CustomerId.ThirdPartyId ={_thirdPartyId} AND OutstandingQuantity >0 "
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of InventoryRemissionOutputDetailPhysicalReportXpo)(Nothing, filter)
    End Function
End Class
