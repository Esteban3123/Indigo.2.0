Imports Domain.Entities
Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Class ViewDashboardPharmacyDetail

    Property Action As Integer?

    Property PattientSupplyOrder As Integer

    Property ApplyProcedureId As Integer?

    Property CodeNameApplyProcedure As String

    Property ListPhysicalInventory As List(Of PhysicalInventory)

    Property ListPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)

    ''' <summary>
    ''' bandera para cuando se va a anular solamente un item 
    ''' 1 - se anula el item solamente
    ''' 0 o NULL - se anula toda la solicitud con todos los items
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property AnulateOnly As Integer?

    <DataMember>
    Public Property CodePatient As String

    <DataMember>
    Public Property CareCenterCode As String

    <DataMember>
    Public Property FunctionUnitCode As String

    <DataMember>
    Public Property ConsecutivePescription As Integer

    <DataMember>
    Public Property ConsecutiveInputs As String

    <DataMember>
    Public Property ConsecutivePharmacy As String

    <DataMember>
    Public Property AdmissionNumeber As String

    <DataMember>
    Public Property PattientCode As String

    <DataMember>
    Public Property CUMSource As Integer

    <DataMember>
    Public Property IdProductHeon As Integer

    <DataMember>
    Public Property QuantityAsignedByHEON As Integer

    <DataMember>
    Public Property recetarioOMedica As String

    <DataMember>
    Public Property solicitudDetalle As SolicitudDetalleObject

    <DataMember>
    Property MedicalFormulaDetailId As Integer

    <DataMember>
    Property ListDeferred As List(Of ViewDashboardPharmacyDetailDeferred)

    <DataMember>
    Property IsDeferred As Boolean

    <DataMember>
    Property TypeProduct As String

    <DataMember>
    Property ProductId As Integer?

    <DataMember>
    Property MedicamentCode As String

    <DataMember>
    Property MedicamentName As String

    ''' <summary>
    ''' Id del Motivo general para la anulacion de una solicitud
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property HCMOANULBId As String

    ''' <summary>
    ''' La descripcion de la anulacion de una solictud
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property Description As String

    <DataMember()>
    Public Property DiagnosticCode As String
    <DataMember()>
    Public Property TreatmentDays As Integer
    <DataMember()>
    Public Property IDMipres As String
    <DataMember()>
    Public Property AuthorizationNumber As String

    <DataMember()>
    Public Property TotalDose As Decimal?

    <DataMember()>
    Public Property TotalDoseMeasurement As String

    <DataMember()>
    Public Property FinishedProductCodeNPT As String

    <DataMember>
    Public Property ListDeliveriesByPharmacyProductType As List(Of DeliveriesByPharmacyProductTypeModel)


    Public Function ToXML() As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            If entityProperty.PropertyType.Name.Equals("String") Then
                If entityProperty.Name = "Producto" Then
                    builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me).ToString().CleanSpecialChars(".").Trim()))
                Else
                    builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me).ToString().CleanSpecialChars().Trim()))
                End If
            ElseIf entityProperty.PropertyType.Name.Equals("DateTime") Then
                builder.Append(String.Format(formatXml, entityProperty.Name, CDate(entityProperty.GetValue(Me)).ToString("dd/MM/yyyy hh:mm:ss")))
            Else
                builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me)))
            End If
        Next
        builder.Append("</" & Me.GetType().Name & ">")
        Return builder.ToString()
    End Function

End Class