Imports System.Runtime.Serialization
Imports System.Text
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Partial Public Class UploadBankStatements

    ''' <summary>
    ''' Unidad operativa
    ''' </summary>
    <DataMember()>
    Public Property OperativeUnit As String

    Private Function CleanFields(field As Object) As String
        If IsNumeric(field) Then
            Return field.ToString().Replace(",", ".")
        End If
        If IsDate(field) Then
            Return CDate(field).ToString("dd/MM/yyyy hh:mm:ss")
        End If
        Return field.ToString().CleanSpecialChars()
    End Function

    Public Function ToXML() As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        builder.Append(String.Format(formatXml, "ChangeTracker", Me.ChangeTracker.State.ToString()))
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(Me))))
        Next


        Dim UploadBankStatementsDetail As TrackableCollection(Of UploadBankStatementsDetail) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("UploadBankStatementsDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If UploadBankStatementsDetail IsNot Nothing AndAlso UploadBankStatementsDetail.Any Then
            UploadBankStatementsDetail.ToList().ForEach(Sub(i)
                                                            builder.Append("<" & i.GetType().Name & ">")
                                                            builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))


                                                            'agrego las propiedades del detalle 
                                                            For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                If entityProperty.GetValue(i) Is Nothing Then
                                                                    Continue For
                                                                End If
                                                                builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                            Next

                                                            builder.Append("</" & i.GetType().Name & ">")

                                                        End Sub)
        End If



        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function


End Class
