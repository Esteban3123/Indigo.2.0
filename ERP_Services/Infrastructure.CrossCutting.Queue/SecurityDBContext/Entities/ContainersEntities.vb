Public Class ContainersEntities

    ' Primary key
    Public Property Id() As Integer
    Public Property Code() As String
    Public Property Name() As String
    Public Property FoundationalContainer() As String
    Public Property TransactionalContainer() As String
    Public Property DocumentalContainer() As String
    Public Property CompanyType() As Byte
    Public Property CompanyNit() As String
    Public Property State() As Boolean

    Public Overridable Property EventConfigurations() As ICollection(Of EventsConfigurationEntities)

End Class
