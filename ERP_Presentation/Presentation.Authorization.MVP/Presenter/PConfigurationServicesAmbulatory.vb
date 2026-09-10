'***********************************************************************
' Assembly         : Presentacion.Authorization.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/05/2020
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
Imports Presentation.Base

#End Region

Public Class PConfigurationServicesAmbulatory

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IConfigurationServicesAmbulatory

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
    Public Sub New(ByRef iview As IConfigurationServicesAmbulatory)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Function InitializePortfolio() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.ListAuthorizationPortfolioByStatus(True)
    End Function

    Public Function InitializeCareGroup() As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCareGroupByStatusXpCollection(True)
    End Function

    Public Function ListServices(authorizationPortfolioId As Integer) As List(Of ViewListPortfolioServicesXpo)
        Dim filter As String = "AuthorizationPortfolioId = " & authorizationPortfolioId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListPortfolioServicesXpo)(Nothing, filter).ToList()
    End Function

    Public Function ListProducts(authorizationPortfolioId As Integer) As List(Of ViewListPortfolioProductsXpo)
        Dim filter As String = "AuthorizationPortfolioId = " & authorizationPortfolioId
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ViewListPortfolioProductsXpo)(Nothing, filter).ToList()
    End Function

    Public Function GetConfigurationServicesAmbulatoryById(id As Integer) As ConfigurationServicesAmbulatoryXpo
        Dim filter As String = "Id = " & id
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AuthorizationService.GetCollection(Of ConfigurationServicesAmbulatoryXpo)(Nothing, filter).FirstOrDefault()
    End Function

#End Region

End Class
