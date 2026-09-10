Imports System.Runtime.Serialization
Imports System.Text
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Class AgreementsC

    ''' <summary>
    ''' Factura
    ''' </summary>
    <DataMember()>
    Public Property InvoiceNumber As String

    ''' <summary>
    ''' Unidad operativa
    ''' </summary>
    <DataMember()>
    Public Property OperativeUnit As String

#Region "Methods"
    Public Function ConvertToXmlAgreement(agreementsC As AgreementsC) As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As StringBuilder = New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        builder.Append(String.Format(formatXml, "ChangeTracker", agreementsC.ChangeTracker.State.ToString()))
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(Me))))
        Next

        Dim AgreementsDIdTmp As Integer = 1
        Dim AgreementsD As TrackableCollection(Of AgreementsD) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("AgreementsD")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If AgreementsD IsNot Nothing AndAlso AgreementsD.Any Then
            AgreementsD.ToList().ForEach(Sub(i)
                                             builder.Append("<" & i.GetType().Name & ">")
                                             builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                             builder.Append(String.Format(formatXml, "AgreementsDIdTmp", AgreementsDIdTmp))

                                             'agrego las propiedades del detalle
                                             For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                 If entityProperty.GetValue(i) Is Nothing Then
                                                     Continue For
                                                 End If
                                                 builder.Append(String.Format(formatXml, entityProperty.Name, Utils.CleanFields(entityProperty.GetValue(i))))
                                             Next

                                             builder.Append("</" & i.GetType().Name & ">")
                                             AgreementsDIdTmp += 1
                                         End Sub)

        End If


        'agrego los detalles que se esten eliminando
        If agreementsC.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("AgreementsD") Then
            For Each i As AgreementsD In agreementsC.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("AgreementsD")
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
