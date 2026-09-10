Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Partial Class RISGRIMAGE
    Inherits Entity(Of RISGRIMAGE)

    <DataMember()>
    Public ReadOnly Property CodigoNombre As String
        Get
            Return String.Concat(Me.CODIGO, " - ", Me.NOMBRE)
        End Get
    End Property

End Class
