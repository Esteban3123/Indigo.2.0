#Region "Imports"

Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

#End Region

Partial Public Class PharmaceuticalDispensingTransfer

#Region "Properties"

    <DataMember>
    Property WarehouseCodeName As String

    <DataMember>
    Public Property Prefix As String

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

        Dim PharmaceuticalDispensingTransferDetail As TrackableCollection(Of PharmaceuticalDispensingTransferDetail) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("PharmaceuticalDispensingTransferDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If PharmaceuticalDispensingTransferDetail IsNot Nothing AndAlso PharmaceuticalDispensingTransferDetail.Any Then
            PharmaceuticalDispensingTransferDetail.ToList().ForEach(Sub(i)
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
        If Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("PharmaceuticalDispensingTransferDetail") Then
            For Each i As PharmaceuticalDispensingTransferDetail In Me.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("PharmaceuticalDispensingTransferDetail")
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
            Next
        End If

        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

#End Region

End Class
