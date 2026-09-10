Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Public Class WorkOrder

#Region "Properties"

    <DataMember()>
    Public Property RowId As Integer

    ''' <summary>
    ''' Tipo de Proceso a Realizar:
    ''' 1 - Asignación de Responsable
    ''' 2 - Reasignación de Responsable
    ''' </summary>
    <DataMember()>
    Public Property ProcessType As Byte

    <DataMember()>
    Public Property OperatingUnitId As Integer

    <DataMember()>
    Public Property Company As String

    <DataMember()>
    Public Property BrachOfficeCodeName As String

    <DataMember()>
    Public Property ProtocolCodeName As String

    <DataMember()>
    Public Property PhysicalAssetDescription As String

    <DataMember()>
    Public Property Plate As String

    <DataMember()>
    Public Property Model As String

    <DataMember()>
    Public Property Serie As String

    <DataMember()>
    Public Property PartsName As String

    <DataMember()>
    Public Property Location As String

    <DataMember()>
    Public Property MaintenanceResponsibleCodeName As String

    <DataMember()>
    Public Property ResponsiblePhone As String

#End Region

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

        Dim WorkOrderActivities As TrackableCollection(Of WorkOrderActivities) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("WorkOrderActivities")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If WorkOrderActivities IsNot Nothing AndAlso WorkOrderActivities.Any Then
            WorkOrderActivities.ToList().ForEach(Sub(i)
                                                     builder.Append("<" & i.GetType().Name & ">")
                                                     builder.Append(String.Format(formatXml, "WorkOrderRowId", Me.RowId))

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

        Dim WorkOrderConsumables As TrackableCollection(Of WorkOrderConsumables) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("WorkOrderConsumables")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If WorkOrderConsumables IsNot Nothing AndAlso WorkOrderConsumables.Any Then
            WorkOrderConsumables.ToList().ForEach(Sub(i)
                                                      builder.Append("<" & i.GetType().Name & ">")
                                                      builder.Append(String.Format(formatXml, "WorkOrderRowId", Me.RowId))

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

        Dim WorkOrderTools As TrackableCollection(Of WorkOrderTools) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("WorkOrderTools")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If WorkOrderTools IsNot Nothing AndAlso WorkOrderTools.Any Then
            WorkOrderTools.ToList().ForEach(Sub(i)
                                                builder.Append("<" & i.GetType().Name & ">")
                                                builder.Append(String.Format(formatXml, "WorkOrderRowId", Me.RowId))

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

        Dim WorkOrderSupplies As TrackableCollection(Of WorkOrderSupplies) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("WorkOrderSupplies")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If WorkOrderSupplies IsNot Nothing AndAlso WorkOrderSupplies.Any Then
            WorkOrderSupplies.ToList().ForEach(Sub(i)
                                                   builder.Append("<" & i.GetType().Name & ">")
                                                   builder.Append(String.Format(formatXml, "WorkOrderRowId", Me.RowId))

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

        builder.Append(String.Format(formatXml, "ChangeTracker", Me.ChangeTracker.State.ToString()))
        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

#End Region

End Class
