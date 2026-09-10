#Region "Imports"

Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Public Class TransferOrderDevolution

#Region "Properties"

    <DataMember()>
    Public Property Prefix As String

    <DataMember()>
    Public Property CodeTransferOrder As String

    <DataMember()>
    Public Property DescriptionWarehouse As String

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
            builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(Me))))
        Next

        Dim TransferOrderDevolutionDetail As TrackableCollection(Of TransferOrderDevolutionDetail) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("TransferOrderDevolutionDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If TransferOrderDevolutionDetail IsNot Nothing AndAlso TransferOrderDevolutionDetail.Any Then
            TransferOrderDevolutionDetail.ToList().ForEach(Sub(i)
                                                               builder.Append("<" & i.GetType().Name & ">")
                                                               builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))

                                                               'agrego las propiedades del detalle
                                                               For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                   If entityProperty.GetValue(i) Is Nothing Then
                                                                       Continue For
                                                                   End If
                                                                   builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(i))))
                                                               Next

                                                               builder.Append("</" & i.GetType().Name & ">")
                                                           End Sub)
        End If

        'agrego los detalles que se esten eliminando
        If Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("TransferOrderDevolutionDetail") Then
            For Each item As TransferOrderDevolutionDetail In Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("TransferOrderDevolutionDetail")
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

        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

#End Region

End Class
