
Imports System.Data.Entity.ModelConfiguration

Public Class EventsConfigurationConfiguration
    Inherits EntityTypeConfiguration(Of EventsConfigurationEntities)

    Public Sub New()
        ToTable("EventsConfiguration", "Security").HasKey(Function(ev) ev.Id).
            HasRequired(Function(ev) ev.Containers).
            WithMany(Function(ev) ev.EventConfigurations).
            HasForeignKey(Function(ev) ev.ContainerId)
    End Sub
End Class
