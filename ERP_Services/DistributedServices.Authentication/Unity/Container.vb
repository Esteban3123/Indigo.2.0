Imports Application.Autentication.JwtService
Imports Domain.Autentication
Imports Domain.Autentication.Interfaces
Imports Infrastructure.CrossCutting.Autentication.Secrets.AZKeyVault
Imports Infrastructure.CrossCutting.Autentication.Secrets.CacheSecrect
Imports Microsoft.Extensions.Caching.Memory
Imports Microsoft.Practices.Unity

Public Class Container
    Private Shared _currentContainer As IUnityContainer

    Public Shared ReadOnly Property Current() As IUnityContainer
        Get

            If _currentContainer IsNot Nothing Then
                Return _currentContainer
            End If

            ConfigureContainer()

            Return _currentContainer
        End Get
    End Property

    Private Shared Sub ConfigureContainer()
        Dim newContainer = New UnityContainer()

        ' 1. Registrar IMemoryCache (usa la implementación por defecto)
        newContainer.RegisterType(Of IMemoryCache, MemoryCache)(
            New ContainerControlledLifetimeManager(),  ' Singleton
            New InjectionFactory(Function(c) New MemoryCache(New MemoryCacheOptions()))
        )

        ' 2. Registrar KeyVaultSecretProvider
        newContainer.RegisterType(Of KeyVaultSecretProvider)(
            New ContainerControlledLifetimeManager(),
            New InjectionFactory(Function(c)
                                     Dim keyVaultUrl = System.Configuration.ConfigurationManager.AppSettings("KeyVaultUrl")
                                     Dim tenantId = System.Configuration.ConfigurationManager.AppSettings("TenantId")
                                     Dim clientId = System.Configuration.ConfigurationManager.AppSettings("ClientId")
                                     Dim clientSecret = System.Configuration.ConfigurationManager.AppSettings("ClientSecretId")

                                     Return New KeyVaultSecretProvider(keyVaultUrl, tenantId, clientId, clientSecret)
                                 End Function)
        )

        ' 3. Registrar CachedSecretProvider como ISecretProvider
        newContainer.RegisterType(Of ISecretProvider, CachedSecretProvider)(
            New ContainerControlledLifetimeManager(),
            New InjectionFactory(Function(c)
                                     Dim memoryCache = c.Resolve(Of IMemoryCache)()
                                     Dim keyVaultProvider = c.Resolve(Of KeyVaultSecretProvider)()
                                     Dim cacheDuration = TimeSpan.FromMinutes(10)

                                     Return New CachedSecretProvider(keyVaultProvider, memoryCache, cacheDuration)
                                 End Function)
        )

        '4. se regista el Servicio de TokenService 
        newContainer.RegisterType(Of IJwtTokenService, JwtTokenService)(
        New ContainerControlledLifetimeManager(),
        New InjectionFactory(Function(c)
                                 Dim secretProvider = c.Resolve(Of ISecretProvider)()

                                 Dim jwtSettings = New JwtSettings With {
                                    .SecretName = Configuration.ConfigurationManager.AppSettings("MySecretJwt"),
                                    .AudienceSecretName = Configuration.ConfigurationManager.AppSettings("JwtAudience"),
                                    .IssuerSecretName = Configuration.ConfigurationManager.AppSettings("JwtIssuer"),
                                    .ExpiresInMinutes = Configuration.ConfigurationManager.AppSettings("JwtExpiresInMinutes")
                                 }
                                 Return New JwtTokenService(secretProvider, jwtSettings)
                             End Function)
)
        _currentContainer = newContainer
    End Sub

End Class
