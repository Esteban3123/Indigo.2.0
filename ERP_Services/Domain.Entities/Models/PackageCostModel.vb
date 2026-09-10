Public Class PackageCostModel
    Public Property RequestPackageDetailStatusId As Integer
    Public Property PackageId As Integer
    Public Property PackagePersonalizedId As Integer

    Public Property ProductId As Integer

    Public Property Cost As Decimal
    Public Property Percentage As Decimal

#Region "Campos agregados en el ajuste"
    ''' <summary>
    ''' Verificar si ProductPackageId siempre es = a ProductId sino esta propiedad se puede quitar
    ''' </summary>
    ''' <returns></returns>
    Public Property ProductPackageId As Integer
    Public Property BatchSerialGenerated As BatchSerial
#End Region
End Class
