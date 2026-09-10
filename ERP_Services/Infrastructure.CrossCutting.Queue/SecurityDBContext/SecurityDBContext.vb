Imports System.Configuration
Imports System.Data.Entity

Public Class SecurityDBContext
    Inherits DbContext

    Public Property Containers() As DbSet(Of ContainersEntities)

    Public Property EventsConfiguration() As DbSet(Of EventsConfigurationEntities)

    Public Sub New(ByVal nameOrConnectionString As String)
        MyBase.New(nameOrConnectionString.Replace("""", ""))
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)

        MyBase.Configuration.LazyLoadingEnabled = False
        MyBase.Configuration.ProxyCreationEnabled = False

        modelBuilder.Configurations.Add(New ContainersConfiguration())
        modelBuilder.Configurations.Add(New EventsConfigurationConfiguration())
    End Sub

End Class
