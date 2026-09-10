Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Public Class SchedulePayment

#Region "Properties"
    ''' <summary>
    ''' Este objeto es para identificar desde donde estoy generando el archivo plano
    ''' </summary>
    <DataMember()>
    Public fromVoucherTransaction As VoucherTransaction
#End Region

#Region "Methods"
    Public Function ToXML() As String
        Dim SchedulePaymentDetailIdTmp As Integer = 1
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        'agrego el estado de la entidad
        builder.Append(String.Format(formatXml, "ChangeTracker", Me.ChangeTracker.State.ToString()))
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            If entityProperty.GetValue(Me) <> Nothing AndAlso entityProperty.GetValue(Me).GetType() = GetType(DateTime) Then
                builder.Append(String.Format(formatXml, entityProperty.Name, CDate(entityProperty.GetValue(Me)).ToString("dd/MM/yyyy HH:mm:ss")))
            Else
                builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(Me))))
            End If
        Next

        Dim listSchedulePaymentBankAccount As TrackableCollection(Of SchedulePaymentBankAccount) = TryCast(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("SchedulePaymentBankAccount")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)

        If listSchedulePaymentBankAccount?.Any() Then
            listSchedulePaymentBankAccount.ToList().FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ForEach(Sub(i)
                                                                                                                                  builder.Append($"<{i.GetType().Name}>")
                                                                                                                                  builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                                                                                                  'agrego las propiedades del detalle
                                                                                                                                  For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                                                      If entityProperty.GetValue(i) Is Nothing Then
                                                                                                                                          Continue For
                                                                                                                                      End If
                                                                                                                                      builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                                                                                                  Next
                                                                                                                                  builder.Append($"</{i.GetType().Name}>")
                                                                                                                              End Sub)
        End If

        Dim voucherTransactionDetails As TrackableCollection(Of SchedulePaymentDetail) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("SchedulePaymentDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If voucherTransactionDetails IsNot Nothing AndAlso voucherTransactionDetails.Any Then
            voucherTransactionDetails.ToList().ForEach(Sub(i)
                                                           builder.Append("<" & i.GetType().Name & ">")
                                                           builder.Append(String.Format(formatXml, "IdTmp", SchedulePaymentDetailIdTmp))
                                                           builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))

                                                           'agrego las propiedades del detalle
                                                           For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                               If entityProperty.GetValue(i) Is Nothing Then
                                                                   Continue For
                                                               End If
                                                               builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                           Next

                                                           If i.SchedulePaymentDetailBudget IsNot Nothing AndAlso i.SchedulePaymentDetailBudget.Any() Then
                                                               For Each budget In i.SchedulePaymentDetailBudget
                                                                   builder.Append("<" & budget.GetType().Name & ">")
                                                                   builder.Append(String.Format(formatXml, "IdSchedulePaymentDetailTmp", SchedulePaymentDetailIdTmp))
                                                                   If i.ChangeTracker.State = ObjectState.Deleted Then
                                                                       builder.Append(String.Format(formatXml, "ChangeTracker", "Deleted"))
                                                                   Else
                                                                       builder.Append(String.Format(formatXml, "ChangeTracker", budget.ChangeTracker.State.ToString()))
                                                                   End If

                                                                   For Each entityProperty As System.Reflection.PropertyInfo In budget.GetType().GetProperties().Where(Function(o) o.PropertyType.Namespace.Equals("System"))
                                                                       If entityProperty.GetValue(budget) Is Nothing Then
                                                                           Continue For
                                                                       End If
                                                                       builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(budget))))
                                                                   Next
                                                                   builder.Append("</" & budget.GetType().Name & ">")
                                                               Next
                                                           End If

                                                           builder.Append("</" & i.GetType().Name & ">")

                                                           SchedulePaymentDetailIdTmp += 1
                                                       End Sub)
        End If

        If Me.ChangeTracker.ObjectsRemovedFromCollectionProperties IsNot Nothing AndAlso Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.Any() Then
            Dim objDelete = Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.Where(Function(o) o.Key = "SchedulePaymentDetail").FirstOrDefault()
            If objDelete.Value IsNot Nothing AndAlso objDelete.Value.Any() Then
                For Each detail In objDelete.Value.ToList()
                    builder.Append("<" & objDelete.Key & ">")
                    builder.Append(String.Format(formatXml, "IdTmp", SchedulePaymentDetailIdTmp))
                    builder.Append(String.Format(formatXml, "ChangeTracker", detail.ChangeTracker.State.ToString()))

                    'agrego las propiedades del detalle
                    For Each entityProperty As System.Reflection.PropertyInfo In detail.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                        If entityProperty.GetValue(detail) Is Nothing Then
                            Continue For
                        End If
                        builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(detail))))
                    Next

                    SchedulePaymentDetailIdTmp += 1
                    builder.Append("</" & objDelete.Key & ">")
                Next
            End If
        End If

        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

    Private Function CleanFields(field As Object) As String
        If IsNumeric(field) Then
            Return field.ToString().Replace(",", ".")
        End If
        If IsDate(field) Then
            Return CDate(field).ToString("dd/MM/yyyy hh:mm:ss")
        End If
        Return field.ToString().CleanSpecialChars()
    End Function
End Class
#End Region