Imports System.Runtime.Serialization

Partial Public Class DistributionManpowerDetail

    ''' <summary>
    ''' para que se puedan generar todos los valores bien la cabecera DistributionManpower ya debe tener los valores totales a distribuir incluidos
    ''' </summary>
    <DataMember()> _
    Public Property SetHoursQuantity As Integer
        Get
            Return Me.HoursQuantity
        End Get
        Set(value As Integer)
            If DistributionManpower IsNot Nothing AndAlso value <> Me.HoursQuantity Then
                Me.HoursQuantity = value
                Me.TotalAccrued = Me.GetTotalValue(value, Me.DistributionManpower.TotalAccrued) + 0
                Me.TotalProvision = Me.GetTotalValue(value, Me.DistributionManpower.TotalProvision)
                Me.TotalEmployerContribution = Me.GetTotalValue(value, Me.DistributionManpower.TotalEmployerContribution)
                Me.TotalParafiscal = Me.GetTotalValue(value, Me.DistributionManpower.TotalParafiscal)

                If DistributionManpower.DistributionManpowerDetail.Sum(Function(x) x.HoursQuantity) = DistributionManpower.HoursWorked Then
                    Dim differenceAccrued As Decimal = Math.Round(DistributionManpower.TotalAccrued - DistributionManpower.DistributionManpowerDetail.Sum(Function(x) x.TotalAccrued), 0)
                    Dim differenceProvision As Decimal = Math.Round(DistributionManpower.TotalProvision - DistributionManpower.DistributionManpowerDetail.Sum(Function(x) x.TotalProvision), 0)
                    Dim differenceEmployerContribution As Decimal = Math.Round(DistributionManpower.TotalEmployerContribution - DistributionManpower.DistributionManpowerDetail.Sum(Function(x) x.TotalEmployerContribution), 0)
                    Dim differenceParafiscal As Decimal = Math.Round(DistributionManpower.TotalParafiscal - DistributionManpower.DistributionManpowerDetail.Sum(Function(x) x.TotalParafiscal), 0)

                    Me.TotalAccrued += differenceAccrued
                    Me.TotalProvision += differenceProvision
                    Me.TotalEmployerContribution += differenceEmployerContribution
                    Me.TotalParafiscal += differenceParafiscal
                End If
            End If
        End Set
    End Property

    <DataMember()> _
    Public Property TotalAccruedEmployerContribution As Decimal
        Get
            Return (Me.TotalAccrued + Me.TotalEmployerContribution)
        End Get
        Set(value As Decimal)

        End Set
    End Property


    Public Function GetTotalValue(totalHours As Integer, totalValue As Decimal) As Decimal
        If totalValue = 0D Then
            Return 0D
        End If
        Return Math.Round(totalHours * (totalValue / Me.DistributionManpower.HoursWorked), 0)
    End Function

    <DataMember()> _
    Property PercentageDistribution As String

End Class
