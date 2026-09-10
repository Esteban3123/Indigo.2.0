Imports System.Data.Entity.ModelConfiguration

Public Class ContainersConfiguration
    Inherits EntityTypeConfiguration(Of ContainersEntities)

    Public Sub New()
        ToTable("Containers", "Security").HasKey(Function(c) c.Id)

    End Sub

End Class
