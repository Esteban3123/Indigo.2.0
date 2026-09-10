Imports System.Text

Public Class RevenueControlDetail

    Private formatXml As String = "<{0}>{1}</{0}>"

    Public Function ConvertToXml(includes() As String)
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me)))
        Next
        Dim distribution As TrackableCollection(Of ServiceOrderDetailDistribution) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("ServiceOrderDetailDistribution")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If distribution IsNot Nothing AndAlso distribution.Any() Then
            distribution.ToList().ForEach(Sub(i)
                                              builder.Append("<" & i.GetType().Name & ">")
                                              For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                  builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i)))
                                              Next
                                              Dim serviceOrderDetail As ServiceOrderDetail = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("ServiceOrderDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                              builder.Append("<" & serviceOrderDetail.GetType().Name & ">")
                                              For Each entityProperty As System.Reflection.PropertyInfo In serviceOrderDetail.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                  builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(serviceOrderDetail)))
                                              Next
                                              builder.Append("</" & serviceOrderDetail.GetType().Name & ">")
                                              builder.Append("</" & i.GetType().Name & ">")
                                          End Sub)
        End If
        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function





    <Obsolete("No esta terminada, se iba a utilizar para dejar el método genérico")> _
    Public Function GetXmlData(entity As Object, includes() As String) As String
        Dim builder As New StringBuilder()
        builder.Append("<" & entity.GetType().Name & ">")
        For Each entityProperty As System.Reflection.PropertyInfo In entity.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(entity)))
        Next
        'Dim newIncludes As New List(Of String)()
        'For Each i In includes
        '    i.Replace(entity.GetType().Name, "")
        '    If i.StartsWith(".") Then
        '        i.Substring(1, i.Length)
        '    End If
        '    If Not i.Equals("") Then
        '        newIncludes.Add(i)
        '    End If
        'Next
        For Each inc In includes
            If inc.Split(".").Length = 1 Then
                Dim currentEntity = CType(entity.GetType().GetProperties().Where(Function(o) o.Name.Equals(inc)).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(entity)
                Dim result As String = GetXmlData(currentEntity, includes)

            End If
        Next
        builder.Append("</" & entity.GetType().Name & ">")
        Return builder.ToString()
    End Function

    <Obsolete("No esta terminada, se iba a utilizar para dejar el método genérico")> _
    Public Function GetXmlData(entity As Object) As String
        Dim builder As New StringBuilder()
        builder.Append("<" & entity.GetType().Name & ">")
        For Each entityProperty As System.Reflection.PropertyInfo In entity.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(entity)))
        Next
        builder.Append("</" & entity.GetType().Name & ">")
        Return builder.ToString()
    End Function


End Class
