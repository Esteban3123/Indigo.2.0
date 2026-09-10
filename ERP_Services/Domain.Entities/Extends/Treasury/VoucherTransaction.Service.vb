Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Public Class VoucherTransaction

#Region "Properties"
    <DataMember()> _
    Property FullNameThird As String
    <DataMember()> _
    Property FullNameCashRegister As String
    <DataMember()> _
    Property FullNameCostCenter As String
    <DataMember()> _
    Property FullNameEntityBankAccount As String
    <DataMember()> _
    Property FullNameMainAccount As String
    <DataMember()> _
    Property Prefix As String
    <DataMember()> _
    Property IsDispersionFundGenerated As Boolean
    <DataMember()> _
    Property EntityBankAccountsType As Byte?
    <DataMember()>
    Property StatusName As String
    <DataMember()>
    Property UpdateFields As Boolean
    <DataMember()>
    Property IndigoCompanyNit As String
    <DataMember()>
    Property AuthorizationResolutionId As Integer?
    <DataMember()>
    Property AuthorizationResolutionName As String

#End Region


    Public Function ToXML() As String
        Dim VoucherTransactionDetailIdTmp As Integer = 1
        Dim DischargeBillIdTmp As Integer = 1

        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        builder.Append(String.Format(formatXml, "HandlesDocumentSupport", Me.HandlesDocumentSupport))
        'agrego el estado de la entidad
        builder.Append(String.Format(formatXml, "ChangeTracker", Me.ChangeTracker.State.ToString()))
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            If entityProperty.GetValue(Me).GetType() = GetType(DateTime) Then
                builder.Append(String.Format(formatXml, entityProperty.Name, CDate(entityProperty.GetValue(Me)).ToString("dd/MM/yyyy HH:mm:ss")))
            ElseIf entityProperty.GetValue(Me).GetType() = GetType(String) AndAlso (entityProperty.Name.Equals("Detail") OrElse entityProperty.Name.Equals("Beneficiary")) Then
                builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me).ToString().ConvertToXmlText()))
            ElseIf entityProperty.GetValue(Me).GetType() = GetType(String) AndAlso (entityProperty.Name.Equals("Detail") OrElse entityProperty.Name.Equals("FullNameThird")) Then
                builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me).ToString().ConvertToXmlText()))
            Else
                builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(Me).ToString().ConvertToXmlText())))
            End If
        Next
        Dim voucherTransactionDetails As List(Of VoucherTransactionDetails) = CType(CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("VoucherTransactionDetails")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me), TrackableCollection(Of VoucherTransactionDetails)).ToList
        Dim objectList = (From x In Me.ChangeTracker.ObjectsRemovedFromCollectionProperties Where x.Key = "VoucherTransactionDetails" Select x.Value).SingleOrDefault()
        If objectList IsNot Nothing Then
            For Each item As VoucherTransactionDetails In objectList
                voucherTransactionDetails.Add(item)
            Next
        End If

        If voucherTransactionDetails IsNot Nothing AndAlso voucherTransactionDetails.Any Then
            voucherTransactionDetails.ToList().ForEach(Sub(i)
                                                           builder.Append("<" & i.GetType().Name & ">")
                                                           builder.Append(String.Format(formatXml, "IdTmp", VoucherTransactionDetailIdTmp))
                                                           If i.IsDelete = True Then
                                                               builder.Append(String.Format(formatXml, "ChangeTracker", "Deleted"))
                                                           Else
                                                               builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                           End If

                                                           'agrego las propiedades del detalle
                                                           For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                               If entityProperty.GetValue(i) Is Nothing Then
                                                                   Continue For
                                                               End If

                                                               If entityProperty.GetValue(i).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("FullNameThirdParty") Then
                                                                   builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i).ToString().ConvertToXmlText()))
                                                               ElseIf entityProperty.GetValue(i).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("Detail") Then
                                                                   builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i).ToString().ConvertToXmlText()))
                                                               ElseIf entityProperty.GetValue(i).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("AdvanceDetail") Then
                                                                   builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i).ToString().ConvertToXmlText()))
                                                               ElseIf entityProperty.GetValue(i).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("Observation") Then
                                                                   builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i).ToString().ConvertToXmlText()))
                                                               Else
                                                                   builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                               End If
                                                           Next

                                                           Dim VoucherTransactionAdvance As TrackableCollection(Of VoucherTransactionAdvance) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("VoucherTransactionAdvance")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                           Dim objectList1 = (From x In i.ChangeTracker.ObjectsRemovedFromCollectionProperties Where x.Key = "VoucherTransactionAdvance" Select x.Value).SingleOrDefault()
                                                           If objectList1 IsNot Nothing Then
                                                               While objectList1.Count > 0
                                                                   CType(objectList1(0), VoucherTransactionAdvance).MarkAsDeleted()
                                                                   VoucherTransactionAdvance.Add(objectList1(0))
                                                               End While
                                                           End If
                                                           If VoucherTransactionAdvance IsNot Nothing AndAlso VoucherTransactionAdvance.Any Then
                                                               VoucherTransactionAdvance.ToList().ForEach(Sub(x)
                                                                                                              builder.Append("<" & x.GetType().Name & ">")
                                                                                                              builder.Append(String.Format(formatXml, "IdVoucherTransactionDTmp", VoucherTransactionDetailIdTmp))
                                                                                                              If i.IsDelete = True Then
                                                                                                                  builder.Append(String.Format(formatXml, "ChangeTracker", "Deleted"))
                                                                                                              Else
                                                                                                                  builder.Append(String.Format(formatXml, "ChangeTracker", x.ChangeTracker.State.ToString()))
                                                                                                              End If
                                                                                                              For Each entityProperty As System.Reflection.PropertyInfo In x.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                                  If entityProperty.GetValue(x) Is Nothing Then
                                                                                                                      Continue For
                                                                                                                  End If

                                                                                                                  If entityProperty.GetValue(x).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("Detail") Then
                                                                                                                      builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(x).ToString().ConvertToXmlText()))
                                                                                                                  Else
                                                                                                                      builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(x))))
                                                                                                                  End If
                                                                                                              Next
                                                                                                              builder.Append("</" & x.GetType().Name & ">")
                                                                                                          End Sub)


                                                           End If

                                                           Dim DischargeBill As TrackableCollection(Of DischargeBill) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("DischargeBill")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                           Dim objectList2 = (From x In i.ChangeTracker.ObjectsRemovedFromCollectionProperties Where x.Key = "DischargeBill" Select x.Value).SingleOrDefault()
                                                           If objectList2 IsNot Nothing Then
                                                               While objectList2.Count > 0
                                                                   CType(objectList2(0), DischargeBill).MarkAsDeleted()
                                                                   DischargeBill.Add(objectList2(0))
                                                               End While
                                                           End If
                                                           If DischargeBill IsNot Nothing AndAlso DischargeBill.Any Then
                                                               DischargeBill.ToList().ForEach(Sub(x)
                                                                                                  builder.Append("<" & x.GetType().Name & ">")
                                                                                                  builder.Append(String.Format(formatXml, "IdTmp", DischargeBillIdTmp))
                                                                                                  builder.Append(String.Format(formatXml, "IdVoucherTransactionDTmp", VoucherTransactionDetailIdTmp))
                                                                                                  If i.IsDelete = True Then
                                                                                                      builder.Append(String.Format(formatXml, "ChangeTracker", "Deleted"))
                                                                                                  Else
                                                                                                      builder.Append(String.Format(formatXml, "ChangeTracker", x.ChangeTracker.State.ToString()))
                                                                                                  End If
                                                                                                  For Each entityProperty As System.Reflection.PropertyInfo In x.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                      If entityProperty.GetValue(x) Is Nothing Then
                                                                                                          Continue For
                                                                                                      End If

                                                                                                      If entityProperty.GetValue(x).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("Detail") Then
                                                                                                          builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(x).ToString().ConvertToXmlText()))
                                                                                                      ElseIf entityProperty.GetValue(x).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("AccountPayableBillNumber") Then
                                                                                                          builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(x).ToString().ConvertToXmlText()))
                                                                                                      Else
                                                                                                          builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(x))))
                                                                                                      End If
                                                                                                  Next

                                                                                                  If x.DischargeBillBudget IsNot Nothing AndAlso x.DischargeBillBudget.Any() Then
                                                                                                      For Each budget In x.DischargeBillBudget
                                                                                                          builder.Append("<" & budget.GetType().Name & ">")
                                                                                                          builder.Append(String.Format(formatXml, "IdDischargeBillTmp", DischargeBillIdTmp))
                                                                                                          If i.IsDelete = True OrElse x.ChangeTracker.State = ObjectState.Deleted Then
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

                                                                                                  builder.Append("</" & x.GetType().Name & ">")

                                                                                                  DischargeBillIdTmp += 1
                                                                                              End Sub)


                                                           End If

                                                           Dim TreasuryAdvances As TrackableCollection(Of TreasuryAdvances) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("TreasuryAdvances")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                           Dim objectList3 = (From x In i.ChangeTracker.ObjectsRemovedFromCollectionProperties Where x.Key = "TreasuryAdvances" Select x.Value).SingleOrDefault()
                                                           If objectList3 IsNot Nothing Then
                                                               While objectList3.Count > 0
                                                                   CType(objectList3(0), TreasuryAdvances).MarkAsDeleted()
                                                                   TreasuryAdvances.Add(objectList3(0))
                                                               End While
                                                           End If
                                                           If TreasuryAdvances IsNot Nothing AndAlso TreasuryAdvances.Any Then
                                                               TreasuryAdvances.ToList().ForEach(Sub(x)
                                                                                                     builder.Append("<" & x.GetType().Name & ">")
                                                                                                     builder.Append(String.Format(formatXml, "IdVoucherTransactionDTmp", VoucherTransactionDetailIdTmp))
                                                                                                     If i.IsDelete = True Then
                                                                                                         builder.Append(String.Format(formatXml, "ChangeTracker", "Deleted"))
                                                                                                     Else
                                                                                                         builder.Append(String.Format(formatXml, "ChangeTracker", x.ChangeTracker.State.ToString()))
                                                                                                     End If
                                                                                                     For Each entityProperty As System.Reflection.PropertyInfo In x.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                         If entityProperty.GetValue(x) Is Nothing Then
                                                                                                             Continue For
                                                                                                         End If

                                                                                                         If entityProperty.GetValue(x).GetType() = GetType(String) AndAlso entityProperty.Name.Equals("Detail") Then
                                                                                                             builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(x).ToString().ConvertToXmlText()))
                                                                                                         Else
                                                                                                             builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(x))))
                                                                                                         End If
                                                                                                     Next
                                                                                                     builder.Append("</" & x.GetType().Name & ">")
                                                                                                 End Sub)


                                                           End If

                                                           VoucherTransactionDetailIdTmp += 1
                                                           builder.Append("</" & i.GetType().Name & ">")
                                                       End Sub)
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
        'Return field.ToString().CleanSpecialChars("(),.-%/º")
        Return field.ToString()
    End Function
End Class
