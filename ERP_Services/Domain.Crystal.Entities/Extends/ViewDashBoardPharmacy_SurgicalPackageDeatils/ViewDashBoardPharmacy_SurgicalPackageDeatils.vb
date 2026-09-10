Imports Domain.Entities
Imports System.Runtime.Serialization
Imports System.Text
Imports Infrastructure.CrossCutting.Base

Partial Class ViewDashBoardPharmacy_SurgicalPackageDeatils

    Property Action As Integer?

    Property PattientSupplyOrder As Integer

    Property ApplyProcedureId As Integer?

    Property CodeNameApplyProcedure As String

    Property ListPhysicalInventory As List(Of PhysicalInventory)

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

    <DataMember()>
    Public Property GuardaGastoQX As Boolean

    <DataMember()>
    Public Property IdProgramacionQXPrincipal As Integer

    <DataMember()>
    Public Property PackageId As Integer?

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

    Public Function ToXML() As String
        Dim formatXml As String = "<{0}>{1}</{0}>"
        Dim builder As New StringBuilder()
        builder.Append("<" & Me.GetType().Name & ">")
        For Each entityProperty As System.Reflection.PropertyInfo In Me.GetType().GetProperties().Where(Function(o) Not o.Name.Equals("TimeStamp") AndAlso o.PropertyType.Namespace.Equals("System"))
            If entityProperty.GetValue(Me) Is Nothing Then
                Continue For
            End If
            If entityProperty.PropertyType.Name.Equals("String") Then
                builder.Append(String.Format(formatXml, entityProperty.Name, entityProperty.GetValue(Me).ToString().CleanSpecialChars().Trim()))
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