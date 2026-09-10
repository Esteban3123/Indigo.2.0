Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Public Class CashReceipts

    <DataMember>
    Property NitNameThirdParty As String

    <DataMember>
    Property CodeNameMainAccount As String

    <DataMember>
    Property CodeNameCostCenter As String

    <DataMember>
    Property CodeNameCashRegister As String

    <DataMember>
    Property CodeNameBankAccount As String

    <DataMember()>
    Property Prefix As String

    <DataMember>
    Property AllowBudgetInterface As Boolean = True

    <DataMember>
    Property RevenueControlDetailId As Integer

    <DataMember>
    Property currencyAbbreviation As String

    <DataMember>
    Property CardName As String

    Public Function ToXML() As String
        Dim CashReceiptDetailsIdTmp As Integer = 1
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
            ElseIf entityProperty.GetValue(Me).GetType() = GetType(String) AndAlso (entityProperty.Name.Equals("Detail") OrElse entityProperty.Name.Equals("PaymentResponsibles")) Then
                builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me).ToString().ConvertToXmlText()))
            Else
                builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(Me))))
            End If


        Next
        Dim cashReceiptDetail As TrackableCollection(Of CashReceiptDetails) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("CashReceiptDetails")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If cashReceiptDetail IsNot Nothing AndAlso cashReceiptDetail.Any Then
            cashReceiptDetail.ToList().ForEach(Sub(i)



                                                   builder.Append("<" & i.GetType().Name & ">")
                                                   builder.Append(String.Format(formatXml, "IdTmp", CashReceiptDetailsIdTmp))
                                                   builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                   Dim portfolioAdvanceAdd As PortfolioAdvance = Nothing
                                                   'valido si el detalle ya esta agregado para saber donde buscar para colocar el id temporal
                                                   If i.Id > 0 Then
                                                       portfolioAdvanceAdd = Me.PortfolioAdvance.Where(Function(x) x.CashReceiptDetailId = i.Id).FirstOrDefault()
                                                   Else
                                                       portfolioAdvanceAdd = Me.PortfolioAdvance.ToList().Find(Function(x) x.CashReceiptDetails IsNot Nothing AndAlso x.CashReceiptDetails.Equals(i))
                                                   End If
                                                   If portfolioAdvanceAdd IsNot Nothing Then
                                                       portfolioAdvanceAdd.CashReceiptDetailIdTmp = CashReceiptDetailsIdTmp
                                                   End If

                                                   'agrego las propiedades del detalle del recibo de caja
                                                   For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                       If entityProperty.GetValue(i) Is Nothing Then
                                                           Continue For
                                                       End If
                                                       builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                   Next


                                                   'agrego los detalles de CashReceiptAccountReceivable que vengan eliminados
                                                   If i.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("CashReceiptAccountReceivable") Then
                                                       For Each item As CashReceiptAccountReceivable In i.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("CashReceiptAccountReceivable")
                                                           builder.Append("<" & item.GetType().Name & ">")
                                                           builder.Append(String.Format(formatXml, "CashReceiptDetailIdTmp", CashReceiptDetailsIdTmp.ToString()))
                                                           builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))
                                                           builder.Append(String.Format(formatXml, "Id", item.Id.ToString()))
                                                           builder.Append(String.Format(formatXml, "CashReceiptDetailId", item.CashReceiptDetailId.ToString()))
                                                           builder.Append(String.Format(formatXml, "AccountReceivableId", item.AccountReceivableId.ToString()))
                                                           builder.Append(String.Format(formatXml, "InvoiceNumber", item.InvoiceNumber.ToString()))
                                                           builder.Append(String.Format(formatXml, "Value", item.Value.ToString().Replace(",", ".")))
                                                           builder.Append("</" & item.GetType().Name & ">")
                                                       Next
                                                   End If


                                                   'obtenemos las entidades del detalle del recibo de caja
                                                   'inicio CashReceiptAccountReceivable

                                                   Dim cashReceiptAccountReceivable As TrackableCollection(Of CashReceiptAccountReceivable) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("CashReceiptAccountReceivable")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                   If cashReceiptAccountReceivable IsNot Nothing AndAlso cashReceiptAccountReceivable.Any Then
                                                       cashReceiptAccountReceivable.ToList().ForEach(Sub(x)
                                                                                                         builder.Append("<" & x.GetType().Name & ">")
                                                                                                         builder.Append(String.Format(formatXml, "CashReceiptDetailIdTmp", CashReceiptDetailsIdTmp))
                                                                                                         builder.Append(String.Format(formatXml, "ChangeTracker", x.ChangeTracker.State.ToString()))
                                                                                                         For Each entityProperty As System.Reflection.PropertyInfo In x.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                             builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(x))))
                                                                                                         Next
                                                                                                         builder.Append("</" & x.GetType().Name & ">")
                                                                                                     End Sub)
                                                   End If
                                                   'fin CashReceiptAccountReceivable

                                                   'inicio CashReceiptAdvancePayment

                                                   'agrego los detalles de CashReceiptAdvancePayment que se eliminaron
                                                   If i.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("CashReceiptAdvancePayment") Then
                                                       For Each item As CashReceiptAdvancePayment In i.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("CashReceiptAdvancePayment")
                                                           builder.Append("<" & item.GetType().Name & ">")
                                                           builder.Append(String.Format(formatXml, "CashReceiptDetailIdTmp", CashReceiptDetailsIdTmp.ToString()))
                                                           builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))
                                                           builder.Append(String.Format(formatXml, "Id", item.Id.ToString()))
                                                           builder.Append(String.Format(formatXml, "CashReceiptDetailId", item.CashReceiptDetailId.ToString()))
                                                           builder.Append(String.Format(formatXml, "AdvancePaymentId", item.AdvancePaymentId.ToString()))
                                                           builder.Append(String.Format(formatXml, "AdvancePaymentCode", item.AdvancePaymentCode.ToString()))
                                                           builder.Append(String.Format(formatXml, "PaymentValue", item.PaymentValue.ToString().Replace(",", ".")))
                                                           builder.Append(String.Format(formatXml, "ValueInCurrencyHeader", item.ValueInCurrencyHeader.ToString().Replace(",", ".")))
                                                           builder.Append("</" & item.GetType().Name & ">")
                                                       Next
                                                   End If

                                                   Dim cashReceiptAdvancePayment As TrackableCollection(Of CashReceiptAdvancePayment) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("CashReceiptAdvancePayment")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                   If cashReceiptAdvancePayment IsNot Nothing AndAlso cashReceiptAdvancePayment.Any Then
                                                       cashReceiptAdvancePayment.ToList().ForEach(Sub(x)
                                                                                                      builder.Append("<" & x.GetType().Name & ">")
                                                                                                      builder.Append(String.Format(formatXml, "CashReceiptDetailIdTmp", CashReceiptDetailsIdTmp))
                                                                                                      builder.Append(String.Format(formatXml, "ChangeTracker", x.ChangeTracker.State.ToString()))
                                                                                                      For Each entityProperty As System.Reflection.PropertyInfo In x.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                          If entityProperty.GetValue(x) Is Nothing Then
                                                                                                              Continue For
                                                                                                          End If
                                                                                                          builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(x))))
                                                                                                      Next
                                                                                                      builder.Append("</" & x.GetType().Name & ">")
                                                                                                  End Sub)
                                                   End If
                                                   'fin CashReceiptAdvancePayment

                                                   'inicio CashReceiptDetailAccountPayable

                                                   'agrego los detalles de CashReceiptDetailAccountPayable que se eliminaron
                                                   If i.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("CashReceiptDetailAccountPayable") Then
                                                       For Each item As CashReceiptDetailAccountPayable In i.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("CashReceiptDetailAccountPayable")
                                                           builder.Append("<" & item.GetType().Name & ">")
                                                           builder.Append(String.Format(formatXml, "CashReceiptDetailIdTmp", CashReceiptDetailsIdTmp.ToString()))
                                                           builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))
                                                           builder.Append(String.Format(formatXml, "Id", item.Id.ToString()))
                                                           builder.Append(String.Format(formatXml, "CashReceiptDetailId", item.CashReceiptDetailId.ToString()))
                                                           builder.Append(String.Format(formatXml, "AccountPayableId", item.AccountPayableId.ToString()))
                                                           builder.Append(String.Format(formatXml, "RefundValue", item.RefundValue.ToString().Replace(",", ".")))
                                                           builder.Append("</" & item.GetType().Name & ">")
                                                       Next
                                                   End If

                                                   Dim cashReceiptDetailAccountPayable As TrackableCollection(Of CashReceiptDetailAccountPayable) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("CashReceiptDetailAccountPayable")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                   If cashReceiptDetailAccountPayable IsNot Nothing AndAlso cashReceiptDetailAccountPayable.Any Then
                                                       cashReceiptDetailAccountPayable.ToList().ForEach(Sub(x)
                                                                                                            builder.Append("<" & x.GetType().Name & ">")
                                                                                                            builder.Append(String.Format(formatXml, "CashReceiptDetailIdTmp", CashReceiptDetailsIdTmp))
                                                                                                            builder.Append(String.Format(formatXml, "ChangeTracker", x.ChangeTracker.State.ToString()))
                                                                                                            For Each entityProperty As System.Reflection.PropertyInfo In x.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                                If entityProperty.GetValue(x) Is Nothing Then
                                                                                                                    Continue For
                                                                                                                End If
                                                                                                                builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(x))))
                                                                                                            Next
                                                                                                            builder.Append("</" & x.GetType().Name & ">")
                                                                                                        End Sub)
                                                   End If
                                                   'fin CashReceiptDetailAccountPayable

                                                   CashReceiptDetailsIdTmp += 1
                                                   builder.Append("</" & i.GetType().Name & ">")
                                               End Sub)
        End If
        'Inicio PaymentMethods
        Dim paymentMethods As TrackableCollection(Of PaymentMethods) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("PaymentMethods")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If paymentMethods IsNot Nothing AndAlso paymentMethods.Any Then
            paymentMethods.ToList().ForEach(Sub(i)
                                                builder.Append("<" & i.GetType().Name & ">")
                                                builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                'agrego las propiedades de los metodos de pago
                                                For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                    If entityProperty.GetValue(i) Is Nothing Then
                                                        Continue For
                                                    End If
                                                    builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                Next
                                                builder.Append("</" & i.GetType().Name & ">")
                                            End Sub)
        End If
        'Fin PaymentMethods

        'inicio PortfolioAdvance
        Dim portfolioAdvance As TrackableCollection(Of PortfolioAdvance) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("PortfolioAdvance")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If portfolioAdvance IsNot Nothing AndAlso portfolioAdvance.Any Then
            portfolioAdvance.ToList().ForEach(Sub(i)
                                                  builder.Append("<" & i.GetType().Name & ">")
                                                  builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                  'agrego las propiedades de los anticipos
                                                  For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                      If entityProperty.GetValue(i) Is Nothing Then
                                                          Continue For
                                                      End If
                                                      builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))

                                                      
                                                  Next
                                                  builder.Append("</" & i.GetType().Name & ">")
                                              End Sub)
        End If
        'fin PortfolioAdvance
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
