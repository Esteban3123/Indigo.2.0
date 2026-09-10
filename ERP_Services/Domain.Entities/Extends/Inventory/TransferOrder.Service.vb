#Region "Imports"

Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Public Class TransferOrder

#Region "Properties"

    <DataMember()>
    Public Property Prefix As String

    <DataMember()>
    Public Property DescriptionSourceWarehouse As String

    <DataMember()>
    Public Property DescriptionTargetFuntionalUnit As String

    <DataMember()>
    Public Property DescriptionTransitWarehouse As String

    <DataMember()>
    Public Property DescriptionTargetWarehouse As String

    <DataMember()>
    Public Property DescriptionAdjustmentConcept As String

    <DataMember()>
    Public Property DescirptionThirdParty As String

#End Region

#Region "Methods"

    Public Function ToXML() As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        builder.Append(String.Format(formatXml, "ChangeTracker", Me.ChangeTracker.State.ToString()))
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            If Not entityProperty.Name.Equals("Description") Then
                builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(Me))))
            Else
                builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me)))
            End If
        Next

        Dim TransferOrderDetailIdTmp As Integer = 1
        Dim TransferOrderDetail As TrackableCollection(Of TransferOrderDetail) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("TransferOrderDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If TransferOrderDetail IsNot Nothing AndAlso TransferOrderDetail.Any Then
            TransferOrderDetail.ToList().ForEach(Sub(i)
                                                     builder.Append("<" & i.GetType().Name & ">")
                                                     builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                     builder.Append(String.Format(formatXml, "TransferOrderDetailIdTmp", TransferOrderDetailIdTmp))

                                                     'agrego las propiedades del detalle
                                                     For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                         If entityProperty.GetValue(i) Is Nothing Then
                                                             Continue For
                                                         End If
                                                         builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(i))))
                                                     Next

                                                     'agrego los subdetalles
                                                     Dim TransferOrderDetailBatchSerial As TrackableCollection(Of TransferOrderDetailBatchSerial) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("TransferOrderDetailBatchSerial")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                     If TransferOrderDetailBatchSerial IsNot Nothing AndAlso TransferOrderDetailBatchSerial.Any Then
                                                         TransferOrderDetailBatchSerial.ToList().ForEach(Sub(item)
                                                                                                             builder.Append("<" & item.GetType().Name & ">")
                                                                                                             builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))
                                                                                                             builder.Append(String.Format(formatXml, "TransferOrderDetailIdTmp", TransferOrderDetailIdTmp))
                                                                                                             'agrego las propiedades del detalle
                                                                                                             For Each entityProperty As System.Reflection.PropertyInfo In item.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                                 If entityProperty.GetValue(item) Is Nothing Then
                                                                                                                     Continue For
                                                                                                                 End If
                                                                                                                 builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(item))))
                                                                                                             Next
                                                                                                             builder.Append("</" & item.GetType().Name & ">")
                                                                                                         End Sub)
                                                     End If

                                                     'agrego los subdetalles que se esten eliminando
                                                     If i.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("TransferOrderDetailBatchSerial") Then
                                                         For Each item As TransferOrderDetailBatchSerial In i.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("TransferOrderDetailBatchSerial")
                                                             builder.Append("<" & item.GetType().Name & ">")
                                                             builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))
                                                             builder.Append(String.Format(formatXml, "TransferOrderDetailIdTmp", TransferOrderDetailIdTmp))

                                                             'agrego las propiedades del detalle
                                                             For Each entityProperty As System.Reflection.PropertyInfo In item.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                 If entityProperty.GetValue(item) Is Nothing Then
                                                                     Continue For
                                                                 End If
                                                                 builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(item))))
                                                             Next
                                                             builder.Append("</" & item.GetType().Name & ">")
                                                         Next
                                                     End If

                                                     builder.Append("</" & i.GetType().Name & ">")
                                                     TransferOrderDetailIdTmp += 1
                                                 End Sub)
        End If

        'agrego los detalles que se esten eliminando
        If Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("TransferOrderDetail") Then
            For Each i As TransferOrderDetail In Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("TransferOrderDetail")
                builder.Append("<" & i.GetType().Name & ">")
                builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                'agrego las propiedades del detalle
                For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                    If entityProperty.GetValue(i) Is Nothing Then
                        Continue For
                    End If
                    builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(i))))
                Next

                'agrego los subdetalles que se esten eliminando
                If i.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("TransferOrderDetailBatchSerial") Then
                    For Each item As TransferOrderDetailBatchSerial In i.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("TransferOrderDetailBatchSerial")
                        builder.Append("<" & item.GetType().Name & ">")
                        builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))

                        'agrego las propiedades del detalle
                        For Each entityProperty As System.Reflection.PropertyInfo In item.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                            If entityProperty.GetValue(item) Is Nothing Then
                                Continue For
                            End If
                            builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(item))))
                        Next
                        builder.Append("</" & item.GetType().Name & ">")
                    Next
                End If

                builder.Append("</" & i.GetType().Name & ">")
            Next
        End If

        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

#End Region

End Class
