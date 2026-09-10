#Region "Imports"

Imports System.ServiceModel
Imports Application.Admissions
Imports Application.Security
Imports DistributedServices.Authentication
Imports Domain.Crystal
Imports Domain.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Security
Imports Infrastructure.Data.CrystalRepository
Imports Infrastructure.Data.ModelRepository
Imports Infrastructure.Data.SecurityRepository
Imports Microsoft.Practices.Unity

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
            Dim container = containerInfo?.container
            Dim hisContainer = containerInfo?.hisContainer

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
        'Inyectamos el contexto
        newContainer.RegisterType(Of IGlobalModelUnitOfWork)(New PerResolveLifetimeManager(), New InjectionFactory(Function(c)
                                                                                                                       Return New GlobalModelUnitOfWork(container)
                                                                                                                   End Function))
        'Inyectamos el servicio WCF
        newContainer.RegisterType(Of IAdmissionsSequenceRepository, AdmissionsSequenceRepository)()
        newContainer.RegisterType(Of IAdmissionsSequenceDetailRepository, AdmissionsSequenceDetailRepository)()
        newContainer.RegisterType(Of IAdmissionsSequenseAdminService, AdmissionsSequenseAdminService)()


        _currentContainer = newContainer

    End Sub

#End Region

End Class