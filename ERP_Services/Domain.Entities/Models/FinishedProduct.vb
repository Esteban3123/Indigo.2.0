Public Class FinishedProduct

    Property Type As EFinishedProductType

    Property Code As String

    Property Name As String

    Property Description As String

    Property AtcId As Integer

    Property UnitDoseType As UnitDoseType

    Property Setting As MixingStationSetting


#Region "Product Values"

    Public Function ProductCode() As String
        If Me.Type = EFinishedProductType.Standard Then
            Return $"PKG-{Me.Code}-STD" 'PKG Abreviatura de paquete + Código del paquete + STD Estandar
        End If

        Return $"PKG-{Me.Code}-CUD"  'PKG Abreviatura de paquete + Código del paquete + CUD CustomUnitDose
    End Function

    Public Function ProductName() As String
        If Me.UnitDoseType.MSClass = Infrastructure.CrossCutting.Base.EUnitDoseTypeClass.ParenteralNutrition Then
            Return Me.Name
        End If

        Return Me.Description
    End Function


    Public Function ProductAbbreviation() As String
        Dim name = Me.ProductName().ToUpper()

        If name.Length > 5 Then
            Return name.Substring(0, 5)
        End If

        Return name
    End Function

#End Region

End Class

Public Enum EFinishedProductType
    Standard = 1
    Custom = 2
End Enum
