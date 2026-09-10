#Region "Imports"

Imports System.ServiceModel
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.Data.ModelRepository
Imports Microsoft.Practices.Unity
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Security
Imports Application.Taxes
Imports DistributedServices.Authentication

#End Region

Public NotInheritable Class Container

#Region "Singleton"

    ''' <summary>
    ''' Unica instancia del contenedor
    ''' </summary>
    Private Shared _currentContainer As IUnityContainer

    ''' <summary>
    ''' Obtiene la unica instancia del contenedor
    ''' </summary>
    ''' <returns>Contenedor configurado</returns>
    Public Shared ReadOnly Property Current() As IUnityContainer
        Get
            Dim containerInfo = JwtFactory.GetContainerFromToken()
            Dim container As String = containerInfo?.container
            Dim hisContainer As String = containerInfo?.hisContainer

            If _currentContainer IsNot Nothing Then
                Dim sessionVariables As ICommonVariables = _currentContainer.Resolve(Of ICommonVariables)()
                If sessionVariables.getContainer().Equals(container) Then
                    Return _currentContainer
                End If
            End If


            ConfigureContainer(container, hisContainer)

            Return _currentContainer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Configura las dependencias en el contenedor
    ''' </summary>
    Private Shared Sub ConfigureContainer(container As String, hisContainer As String)
        Dim newContainer = New UnityContainer()

        newContainer.RegisterType(Of ICommonVariables)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                 Return New CommonVariables(container, hisContainer)
                                                                                                             End Function))
        'Inyectamos el contexto de Billing
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))


        newContainer.RegisterType(Of ITaxesPropertyRepository, TaxesPropertyRepository)()
        newContainer.RegisterType(Of ITaxesPropertyAdminService, TaxesPropertyAdminService)()
        'TaxesLiquidation
        newContainer.RegisterType(Of ITaxesLiquidationRepository, TaxesLiquidationRepository)()
        newContainer.RegisterType(Of ITaxesLiquidationAdminService, TaxesLiquidationAdminService)()

        'LowTaxesLiquidation
        newContainer.RegisterType(Of ILowTaxesLiquidationRepository, LowTaxLiquidationRepository)()
        newContainer.RegisterType(Of ILowTaxLiquidationAdminService, LowTaxLiquidationAdminService)()

        'Fiscalization
        newContainer.RegisterType(Of IFiscalizationRepository, FiscalizationRepository)()
        newContainer.RegisterType(Of IFiscalizationAdminService, FiscalizationAdminService)()

        newContainer.RegisterType(Of IConsecutiveRepository, ConsecutiveRepository)()

        _currentContainer = newContainer

    End Sub

#End Region

End Class