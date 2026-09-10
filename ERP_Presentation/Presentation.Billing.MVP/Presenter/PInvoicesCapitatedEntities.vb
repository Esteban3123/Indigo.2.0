'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 20-12-2014
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
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class PInvoicesCapitatedEntities
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IInvoicesCapitatedEntities

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IInvoicesCapitatedEntities)
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
        Using modelCommmonTreasury As New MBlockRecordAndSequense(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista las categorias por estado y usuario
    ''' </summary>
    ''' <param name="status"></param>
    ''' <remarks></remarks>
    Public Sub GetCategoriesByStatusAndUser(status As Boolean)
        View.CategoryXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.GetCategoriesByStatusAndUser(status, Indigo.UserIndigoId)
    End Sub

    ''' <summary>
    ''' Obtiene las facturas relacionadas por grupo de atención que no están relacionadas a otras facturas
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    Public Sub GetPreviousRIPSInvoiceByCareGruop(ByVal CareGroupId As Integer)
        View.PreviousRIPSInvoiceXpo = XpoServiceEx.Instance(Me.Indigo.TransactionalContainer).BillingService.ListInvoiceEntityCapitatedToRIPSByCareGroupId(CareGroupId)
    End Sub

    ''' <summary>
    ''' Obtiene el grupo de atención por el Id
    ''' </summary>
    ''' <param name="CareGroupById"></param>
    ''' <remarks></remarks>
    Public Function GetContractCareGroupById(CareGroupById As Integer) As ContractCareGroupXpo
        Dim filtroConsulta As String = "Id = " & CareGroupById
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.GetCollection(Of ContractCareGroupXpo)(Nothing, filtroConsulta).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Lista los agrupadores por grupo de atención dentro del periodo indicado
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListCareGroupsWithGroupers(CareGroupId As Integer, DateInitial As Date, DateEnd As Date) As List(Of ViewCareGroupsWithGroupersXpo)
        Dim filtroConsulta As String = "CareGroupId = " & CareGroupId & " AND InvoiceEntityCapitatedId IS NULL"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewCareGroupsWithGroupersXpo)(Nothing, filtroConsulta)
    End Function

    ''' <summary>
    ''' Lista los agrupadores por Id de la factura de monto fijo
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListInvoiceEntityCapitatedGroupers(InvoiceEntityCapitatedId As Integer) As List(Of ViewCareGroupsWithGroupersXpo)
        Dim filtroConsulta As String = "InvoiceEntityCapitatedId = " & InvoiceEntityCapitatedId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewCareGroupsWithGroupersXpo)(Nothing, filtroConsulta)
    End Function

End Class
