Imports System.Runtime.Serialization
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.Text

Partial Public Class PortfolioNote
    Inherits Entity(Of Domain.Entities.PortfolioNote)

#Region "Properties"

    <DataMember>
    Property CodeNameCustomer As String

    <DataMember>
    Property NitNameThirParty As String

    <DataMember>
    Property ThirdPartyId As Integer

    <DataMember>
    Property CodeNameAdvanceDistribution As String

    <DataMember>
    Property PortfolioTransferCode As String

    <DataMember>
    Property PortfolioTransferCodeNameCustomer As String

    <DataMember>
    Property EntityCode As String

    <DataMember>
    Property EntityId As Integer

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
            builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(Me))))
        Next


        Dim portfolioNoteDetail As TrackableCollection(Of PortfolioNoteDetail) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("PortfolioNoteDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If portfolioNoteDetail IsNot Nothing AndAlso portfolioNoteDetail.Any Then
            portfolioNoteDetail.ToList().ForEach(Sub(i)
                                                     builder.Append("<" & i.GetType().Name & ">")
                                                     builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))

                                                     'agrego las propiedades del detalle del recibo de caja
                                                     For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                         If entityProperty.GetValue(i) Is Nothing Then
                                                             Continue For
                                                         End If
                                                         builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                     Next

                                                     builder.Append("</" & i.GetType().Name & ">")
                                                 End Sub)
        End If

        Dim PortfolioNoteAccountReceivableAdvanceIdTmp As Integer = 1
        Dim portfolioNoteAccountReceivableAdvance As TrackableCollection(Of PortfolioNoteAccountReceivableAdvance) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("PortfolioNoteAccountReceivableAdvance")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If portfolioNoteAccountReceivableAdvance IsNot Nothing AndAlso portfolioNoteAccountReceivableAdvance.Any Then
            portfolioNoteAccountReceivableAdvance.ToList().ForEach(Sub(i)
                                                                       builder.Append("<" & i.GetType().Name & ">")
                                                                       builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                                       builder.Append(String.Format(formatXml, "PortfolioNoteAccountReceivableAdvanceIdTmp", PortfolioNoteAccountReceivableAdvanceIdTmp))

                                                                       'agrego las propiedades del detalle del recibo de caja
                                                                       For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                           If entityProperty.GetValue(i) Is Nothing Then
                                                                               Continue For
                                                                           End If
                                                                           builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(i))))
                                                                       Next

                                                                       'agrego los subdetalles
                                                                       Dim PortfolioNoteAccountReceivableDetail As TrackableCollection(Of PortfolioNoteAccountReceivableDetail) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("PortfolioNoteAccountReceivableDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                                       If PortfolioNoteAccountReceivableDetail IsNot Nothing AndAlso PortfolioNoteAccountReceivableDetail.Any Then
                                                                           PortfolioNoteAccountReceivableDetail.ToList().ForEach(Sub(item)
                                                                                                                                     builder.Append("<" & item.GetType().Name & ">")
                                                                                                                                     builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))
                                                                                                                                     builder.Append(String.Format(formatXml, "PortfolioNoteAccountReceivableAdvanceIdTmp", PortfolioNoteAccountReceivableAdvanceIdTmp))
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

                                                                       builder.Append("</" & i.GetType().Name & ">")
                                                                       PortfolioNoteAccountReceivableAdvanceIdTmp += 1
                                                                   End Sub)
        End If

        Dim portfolioNoteDistribution As TrackableCollection(Of PortfolioNoteDistribution) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("PortfolioNoteDistribution")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If portfolioNoteDistribution IsNot Nothing AndAlso portfolioNoteDistribution.Any Then
            portfolioNoteDistribution.ToList().ForEach(Sub(i)
                                                           builder.Append("<" & i.GetType().Name & ">")
                                                           builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))

                                                           'agrego las propiedades del detalle del recibo de caja
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
    
    Private Function CleanFields(field As Object) As String
        If IsNumeric(field) Then
            Return field.ToString().Replace(",", ".")
        End If
        If IsDate(field) Then
            Return CDate(field).ToString("dd/MM/yyyy hh:mm:ss")
        End If
        Return field.ToString().CleanSpecialChars()
    End Function

#End Region

End Class
