Imports System.IO
Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports System.Security
Imports System.Globalization

Public Class PharmaceuticalDispensing

#Region "Properties"

    <DataMember()>
    Public Property FullNameAdmission As String

#End Region

#Region "Properties DashboardPharmacy"
    <DataMember>
    Public Property CodePatient As String
    <DataMember>
    Public Property PantientName As String
    <DataMember>
    Public Property PantientFirstName As String
    <DataMember>
    Public Property PantientMiddleName As String
    <DataMember>
    Public Property PantientLastName As String
    <DataMember>
    Public Property PantientSecondLastName As String
    <DataMember>
    Public Property CareCenterCode As String
    <DataMember>
    Public Property FunctionUnitCode As String
    <DataMember>
    Public Property FunctionUnitName As String
    <DataMember>
    Public Property ConsecutivePescription As Integer
    <DataMember>
    Public Property ConsecutiveInputs As Integer
    <DataMember>
    Public Property ConsecutivePharmacy As Integer
    <DataMember>
    Public Property ConsecutiveCrystal As Decimal
    <DataMember>
    Public Property HistoryType As String
    <DataMember>
    Public Property CodeNameWarehouse As String

    <DataMember>
    Public Property OfficeType As Integer?
    <DataMember>
    Public Property LogisticOperator As Integer?
    <DataMember>
    Public Property MedicalOrderRecipe As String

    <DataMember>
    Public Property CodeNameUser As String

    <DataMember>
    Public Property Validado As Boolean

    ''' <summary>
    ''' Permite saber si el registro de la pestaña de quimioterapia es de tipo domiciliaria
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Public Property IDHCORDPRON As Integer?

    <DataMember>
    Public Property PerformsHealthProfessionalThirdPartyId As Integer?
    <DataMember>
    Public Property DateFormulation As DateTime?
#End Region


    Private Function CleanFields(field As Object) As String
        If IsNumeric(field) Then
            Return field.ToString().Replace(",", ".")
        End If
        If IsDate(field) Then
            Return CDate(field).ToString("yyyy-MM-dd HH:mm:ss")
        End If
        Return field.ToString().CleanSpecialChars()
    End Function

    Public Function ToXML(isPharmaceuticalDispensing As Boolean, Optional DispensingIntegration As Byte = 1, Optional EntityName As String = Nothing) As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim PharmaceuticalDispensingIdTmp As Integer = 1
        Dim PharmaceuticalDispensingDetailIdTmp As Integer = 1

        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        builder.Append(String.Format(formatXml, "IsPharmaceuticalDispensing", IIf(isPharmaceuticalDispensing, 1, 0)))
        builder.Append(String.Format(formatXml, "DispensingIntegration", DispensingIntegration))
        builder.Append(String.Format(formatXml, "PharmaceuticalDispensingIdTmp", PharmaceuticalDispensingDetailIdTmp))
        If EntityName IsNot Nothing Then
            builder.Append(String.Format(formatXml, "EntityName", EntityName))
        End If
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            builder.Append(String.Format(formatXml, entityProperty.Name, CleanFields(entityProperty.GetValue(Me))))
        Next



        Dim pharmaceuticalDispensingDetail As TrackableCollection(Of PharmaceuticalDispensingDetail) = CType(Me.GetType().GetProperties().Where(Function(o) o.Name.Equals("PharmaceuticalDispensingDetail")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(Me)
        If pharmaceuticalDispensingDetail IsNot Nothing AndAlso pharmaceuticalDispensingDetail.Any() Then
            pharmaceuticalDispensingDetail.ToList().ForEach(Sub(i)
                                                                builder.Append("<" & i.GetType().Name & ">")

                                                                builder.Append(String.Format(formatXml, "ChangeTracker", i.ChangeTracker.State.ToString()))
                                                                builder.Append(String.Format(formatXml, "PharmaceuticalDispensingIdTmp", PharmaceuticalDispensingIdTmp))
                                                                builder.Append(String.Format(formatXml, "PharmaceuticalDispensingDetailIdTmp", PharmaceuticalDispensingDetailIdTmp))

                                                                If i.ListDeliveriesByPharmacyProductType?.Any() Then
                                                                    For Each item In i.ListDeliveriesByPharmacyProductType
                                                                        builder.Append("<" & item.GetType().Name & ">")

                                                                        builder.Append(String.Format(formatXml, "PharmaceuticalDispensingDetailIdTmp", PharmaceuticalDispensingDetailIdTmp))
                                                                        builder.Append(String.Format(formatXml, "HCFARMEPDID", i.EntityId.ToString()))
                                                                        builder.Append(String.Format(formatXml, "ProductId", item.ProductId.ToString()))
                                                                        builder.Append(String.Format(formatXml, "ProductType", item.ProductType.ToString()))
                                                                        builder.Append(String.Format(formatXml, "Quantity", item.Quantity.ToString()))
                                                                        builder.Append(String.Format(formatXml, "ConcentrationByUnit", item.ConcentrationByUnit))
                                                                        builder.Append(String.Format(formatXml, "TotalWeight", item.TotalWeight))
                                                                        builder.Append(String.Format(formatXml, "QuantityDeliveryPT", item.QuantityDeliveryPT))

                                                                        builder.Append("</" & item.GetType().Name & ">")
                                                                    Next
                                                                End If

                                                                For Each entityProperty As System.Reflection.PropertyInfo In i.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                    If entityProperty.GetValue(i) Is Nothing Then
                                                                        Continue For
                                                                    End If
                                                                    If entityProperty.PropertyType.Name.Equals("String") Then
                                                                        builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i).ToString().CleanSpecialChars(".Ññ")))
                                                                    Else
                                                                        If entityProperty.PropertyType.Name.Equals("Decimal") Then
                                                                            builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i).ToString().Replace(",", ".")))
                                                                        ElseIf entityProperty.PropertyType.Name.Equals("DateTime") Then
                                                                            builder.Append(String.Format(formatXml, entityProperty.Name, CDate(entityProperty.GetValue(i)).ToString("yyyy-MM-dd HH:mm:ss")))
                                                                        Else
                                                                            builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(i)))
                                                                        End If
                                                                    End If
                                                                Next


                                                                If i.ChangeTracker.ObjectsRemovedFromCollectionProperties.ContainsKey("PharmaceuticalDispensingDetailBatchSerial") Then
                                                                    For Each item As PharmaceuticalDispensingDetailBatchSerial In i.ChangeTracker.ObjectsRemovedFromCollectionProperties.Item("PharmaceuticalDispensingDetailBatchSerial")
                                                                        builder.Append("<" & item.GetType().Name & ">")
                                                                        builder.Append(String.Format(formatXml, "ChangeTracker", item.ChangeTracker.State.ToString()))
                                                                        builder.Append(String.Format(formatXml, "PharmaceuticalDispensingDetailIdTmp", PharmaceuticalDispensingDetailIdTmp))
                                                                        builder.Append(String.Format(formatXml, "Id", item.Id.ToString()))
                                                                        builder.Append(String.Format(formatXml, "PharmaceuticalDispensingDetailId", item.PharmaceuticalDispensingDetailId.ToString()))
                                                                        builder.Append(String.Format(formatXml, "PhysicalInventoryId", item.PhysicalInventoryId.ToString()))
                                                                        builder.Append(String.Format(formatXml, "Quantity", item.Quantity.ToString()))
                                                                        builder.Append(String.Format(formatXml, "OutstandingQuantity", item.OutstandingQuantity.ToString()))
                                                                        If item.PhysicalInventoryCustodyId IsNot Nothing Then
                                                                            builder.Append(String.Format(formatXml, "PhysicalInventoryCustodyId", item.PhysicalInventoryCustodyId.ToString()))
                                                                        End If
                                                                        builder.Append("</" & item.GetType().Name & ">")
                                                                    Next
                                                                End If

                                                                Dim pharmaceuticalDispensingDetailBatchSerial As TrackableCollection(Of PharmaceuticalDispensingDetailBatchSerial) = CType(i.GetType().GetProperties().Where(Function(o) o.Name.Equals("PharmaceuticalDispensingDetailBatchSerial")).FirstOrDefault(), System.Reflection.PropertyInfo).GetValue(i)
                                                                If pharmaceuticalDispensingDetailBatchSerial IsNot Nothing AndAlso pharmaceuticalDispensingDetailBatchSerial.Any() Then
                                                                    pharmaceuticalDispensingDetailBatchSerial.ToList().ForEach(Sub(x)
                                                                                                                                   builder.Append("<" & x.GetType().Name & ">")
                                                                                                                                   builder.Append(String.Format(formatXml, "ChangeTracker", x.ChangeTracker.State.ToString()))
                                                                                                                                   builder.Append(String.Format(formatXml, "PharmaceuticalDispensingDetailIdTmp", PharmaceuticalDispensingDetailIdTmp))
                                                                                                                                   For Each entityProperty As System.Reflection.PropertyInfo In x.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
                                                                                                                                       Dim val = entityProperty.GetValue(x)
                                                                                                                                       If entityProperty.PropertyType.Name.Equals("String") Then
                                                                                                                                           builder.Append(String.Format(formatXml, entityProperty.Name, XmlEscape(If(val, ""))))
                                                                                                                                       Else
                                                                                                                                           builder.Append(String.Format(formatXml, entityProperty.Name, val))
                                                                                                                                       End If
                                                                                                                                   Next
                                                                                                                                   builder.Append("</" & x.GetType().Name & ">")
                                                                                                                               End Sub)

                                                                End If
                                                                PharmaceuticalDispensingDetailIdTmp += 1
                                                                builder.Append("</" & i.GetType().Name & ">")
                                                            End Sub)
        End If
        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

    Private Shared Function XmlEscape(value As String) As String
        If value Is Nothing Then Return String.Empty
        Return SecurityElement.Escape(value)
    End Function
End Class
