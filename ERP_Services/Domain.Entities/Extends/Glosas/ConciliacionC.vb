Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text

Partial Public Class ConciliationC
    Inherits Entity(Of ConciliationC)

#Region "Methods"

    Public Function ToXML() As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(Me))))
        Next
        builder.Append(String.Format(formatXml, "ChangeTracker", Me.ChangeTracker.State.ToString()))

        Dim ConciliationParticipants As TrackableCollection(Of ConciliationParticipants) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("ConciliationParticipants")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If ConciliationParticipants IsNot Nothing AndAlso ConciliationParticipants.Any Then
            ConciliationParticipants.ToList().ForEach(Sub(i)
                                                          builder.Append("<" & i.GetType().Name & ">")
                                                          For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                              If entityProperty.GetValue(i) Is Nothing Then
                                                                  Continue For
                                                              End If
                                                              builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(i))))
                                                          Next
                                                          builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                          builder.Append("</" & i.GetType().Name & ">")
                                                      End Sub)
        End If

        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

#End Region

End Class
