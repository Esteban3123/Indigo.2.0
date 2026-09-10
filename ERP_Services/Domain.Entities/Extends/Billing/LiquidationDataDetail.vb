Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Partial Public Class LiquidationDataDetail

    Inherits Entity(Of LiquidationDataDetail)

    <DataMember()>
    Public Property NameCondition As String

    <DataMember()>
    Public Property NameConditionsItems As String


End Class
