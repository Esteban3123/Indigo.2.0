Imports Domain.Entities

Public Class CommonVariables : Implements ICommonVariables

    Private container As String
    Private hisContainer As String

    Public Sub New(container As String, hisContainer As String)
        Me.container = container
        Me.hisContainer = hisContainer
    End Sub


    Public Function getContainer() As String Implements ICommonVariables.getContainer
        Return Me.container
    End Function

    Public Function getHisContainer() As String Implements ICommonVariables.getHisContainer
        Return Me.hisContainer
    End Function

End Class
