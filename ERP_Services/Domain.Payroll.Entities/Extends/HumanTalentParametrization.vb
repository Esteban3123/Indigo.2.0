Public Class HumanTalentParametrization

    Public Property Tabs As New List(Of Tab)

    Public Property Items As New List(Of Item)

    Public Class Tab
        Property TabName As String
        Property Code As String
    End Class
    Public Class Item
        Property ItemText As String
        Property ItemName As String
        Property LayaoutContolItem As Object
        Property Control As Object
        Property Visible As Boolean
        Property Obligatory As Boolean
        Property Mandatory As Boolean
        Property LcgName As String
    End Class
End Class
