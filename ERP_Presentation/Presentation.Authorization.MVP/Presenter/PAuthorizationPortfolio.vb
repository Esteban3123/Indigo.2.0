'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/04/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AuthorizationRepository
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports Presentation.Base

#End Region

Public Class PAuthorizationPortfolio

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IAuthorizationPortfolio

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
    Public Sub New(ByRef iview As IAuthorizationPortfolio)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Function InitializeCUPS() As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatusXpCollection(True)
    End Function

    Public Function InitializeProducts() As XPCollection(Of Infrastructure.Data.Xpo.InventoryRepository.InventoryProductXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListInventoryProductByStatus(True)
    End Function

    Public Function InitializeAuthorizationGroup() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListAuthorizationGroupByStatus(True)
    End Function

    Public Function InitializeCentersHIS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Function

    Public Function CountDescriptions(listCupsIds As List(Of Integer)) As Integer
        Dim stringIds = String.Join(",", listCupsIds.ToArray())
        Dim filter As String = "CUPSEntityId in (" & stringIds & ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewListCupsEntityWithDescriptionsXpo)(Nothing, filter).Count()
    End Function

    Public Function GetListCUPS(Id As Integer) As List(Of AuthorizationPortfolioCUPSEntityXpo)
        Dim filter As String = "AuthorizationPortfolioId.Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of AuthorizationPortfolioCUPSEntityXpo)(Nothing, filter).ToList()
    End Function

    Public Function GetListProducts(Id As Integer) As List(Of AuthorizationPortfolioInventoryProductXpo)
        Dim filter As String = "AuthorizationPortfolioId.Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of AuthorizationPortfolioInventoryProductXpo)(Nothing, filter).ToList()
    End Function

    Public Function GetListCareCenter(Id As Integer) As List(Of AuthorizationPortfolioCareCenterXpo)
        Dim filter As String = "AuthorizationPortfolioId.Id = " & Id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of AuthorizationPortfolioCareCenterXpo)(Nothing, filter).ToList()
    End Function

    Public Function GetCareCenterByCode(code As String) As ADCENATEN
        Dim filter As String = "CODCENATE = '" & code & "'"
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.GetCollection(Of ADCENATEN)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
