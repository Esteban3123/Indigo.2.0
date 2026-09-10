'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class PLiquidator

#Region "Constants"
    ''' <summary>
    ''' Variable que se usa para establecer el tope máximo en UVT de las rentas exentas del 25%
    ''' </summary>
    Const MaxUVTExemptIncome As Integer = 790

    ''' <summary>
    ''' Variable que se usa para establecer el tope máximo en UVT de las rentas exentas y deducciones
    ''' </summary>
    Const MaxUVTExemptIncomeAndDeductions As Integer = 1340

#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el parámetro para identificar cálculo de topes de rentas y deducciones (0 Anual - 1 Mensual)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property WorkIncomeControl As Boolean
#End Region

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ILiquidator

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ILiquidator)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Calcula aportes obligatorios a fondos de pensiones y
    ''' fondo de solidaridad pensional
    ''' </summary>
    ''' <remarks></remarks>
    Public Function CalculateRequiredContributions(ByVal FeeCommissionService As Decimal, ByVal MinimumWage As Decimal, ByVal Percentage As Decimal) As Decimal
        Dim valueFeeCommissionService As Decimal = FeeCommissionService * 40 / 100 * (Percentage / 100)
        Dim valueMinimumWage As Decimal = 25 * MinimumWage * (Percentage / 100)
        If valueFeeCommissionService > valueMinimumWage Then
            Return valueMinimumWage
        Else
            Return valueFeeCommissionService
        End If
    End Function

    ''' <summary>
    ''' Calcula los topes de aportes obligatorios a fondos de pensiones y
    ''' fondo de solidaridad pensional
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateCaps(ByVal valueCompare As Decimal, ByVal MinimumWage As Decimal, ByVal Percentage As Decimal) As List(Of Decimal)
        Dim ListValues As New List(Of Decimal)
        'Tope maximo exento de renta
        Dim valueCapsExent As Decimal = 25 * MinimumWage * (Percentage / 100)
        If valueCompare > valueCapsExent Then
            ListValues.Add(valueCapsExent)
        Else
            ListValues.Add(valueCompare)
        End If
        'El primer item del listado es el tope real, y el segundo item del listado es el tope maximo exento
        ListValues.Add(valueCapsExent)
        Return ListValues
    End Function

    ''' <summary>
    ''' Calcula los topes totales de exentos de renta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateCapsTotalExent(FeeCommissionServiceData As Decimal, TotalIncomeExent As Decimal, UVT As Integer) As List(Of Decimal)
        Dim ListValues As New List(Of Decimal)
        Dim valueTotalPercentageIncome As Decimal = FeeCommissionServiceData * 30 / 100
        Dim valueTotalUVT As Decimal = CDec(UVT * 3800 / 12)
        If TotalIncomeExent < valueTotalPercentageIncome Then
            ListValues.Add(TotalIncomeExent)
        Else
            ListValues.Add(valueTotalPercentageIncome)
        End If

        ListValues.Add(valueTotalUVT)

        If valueTotalUVT > ListValues(0) Then
            ListValues.Add(ListValues(0))
        Else
            ListValues.Add(valueTotalUVT)
        End If
        Return ListValues
    End Function

    ''' <summary>
    ''' Calcula los limites por dependientes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateLimitsDependent(paymenthMonth As Decimal, UVT As Integer, valDependent As Decimal) As List(Of Decimal)
        Dim ListValues As New List(Of Decimal)

        Dim limitPaymentMonth As Decimal = paymenthMonth * 10 / 100
        ListValues.Add(limitPaymentMonth)

        Dim valUvtThirtyTwo As Decimal = CDec(UVT * 32)
        ListValues.Add(valUvtThirtyTwo)

        If valDependent < limitPaymentMonth Then
            ListValues.Add(valDependent)
        Else
            ListValues.Add(limitPaymentMonth)
        End If

        If valDependent < valUvtThirtyTwo Then
            ListValues.Add(valDependent)
        Else
            ListValues.Add(valUvtThirtyTwo)
        End If

        Return ListValues
    End Function

    ''' <summary>
    ''' Escoge el valor a sumar para las deducciones
    ''' </summary>
    ''' <param name="limit"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChooseValue(limit As Decimal, value As Decimal, Optional isLowerLimit As Boolean = False) As Decimal
        If isLowerLimit = False Then
            If limit > value Then
                Return value
            Else
                Return limit
            End If
        Else
            If limit > value Then
                Return limit
            Else
                Return value
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo para validación de topes máximos de UVT
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateLimits(subTotal As Decimal, UVT As Integer, MaxUVT As Decimal) As Decimal
        Dim valUvt As Decimal = MaxUVT * UVT
        If subTotal > valUvt Then
            Return CDec(Utils.RoundValue(valUvt, Utils.RoundLevel.Unit))
        Else
            Return CDec(Utils.RoundValue(subTotal, Utils.RoundLevel.Unit))
        End If
    End Function

    ''' <summary>
    ''' Metodo que calcula menos renta exenta -25% del subtotal
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateLessRentsExentsPopup(subTotal As Decimal, UVT As Integer) As Decimal
        Dim valSubTotal As Decimal = subTotal * 25 / 100
        Dim MaxUVTExemptIncomeLimit = CDec(MaxUVTExemptIncome / If(WorkIncomeControl, 12.0, 1.0))
        Return ValidateLimits(valSubTotal, UVT, MaxUVTExemptIncomeLimit)
    End Function

    ''' <summary>
    ''' Metodo que retorna la retención exenta del 25% del periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateExemptIncomeReal(ExemptIncomePrevious As Decimal, ExemptIncomeCalculated As Decimal, UVT As Integer) As Decimal
        Dim ExemptIncomeTotal As Decimal = ExemptIncomePrevious + ExemptIncomeCalculated
        Dim LessRentsExents = ValidateLimits(ExemptIncomeTotal, UVT, MaxUVTExemptIncome)
        If LessRentsExents > ExemptIncomePrevious Then
            If LessRentsExents = ExemptIncomeTotal Then
                Return ExemptIncomeCalculated
            End If
            Return LessRentsExents - ExemptIncomePrevious
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Metodo que calcula el valor máximo de deducciones y rentas exentas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateMaxDeductionsAndRentExents(subTotal As Decimal, deductionsAndRentExents As Decimal, UVT As Integer) As Decimal
        Dim valSubTotal As Decimal = subTotal * 40 / 100
        If deductionsAndRentExents < valSubTotal Then
            valSubTotal = deductionsAndRentExents
        End If

        Dim MaxUVTExemptIncomeAndDeductionsLimit = CDec(MaxUVTExemptIncomeAndDeductions / If(WorkIncomeControl, 12.0, 1.0))
        Return ValidateLimits(valSubTotal, UVT, MaxUVTExemptIncomeAndDeductionsLimit)
    End Function

    ''' <summary>
    ''' Metodo que calcula la deducción y rentas exentas del periodo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateExemptIncomeAndDeductionsReal(ExemptIncomeAndDeductionsPrevious As Decimal, ExemptIncomeAndDeductionsCalculated As Decimal, UVT As Integer) As Decimal
        Dim ExemptIncomeAndDeductionsTotal As Decimal = ExemptIncomeAndDeductionsPrevious + ExemptIncomeAndDeductionsCalculated
        Dim LessExemptIncomeAndDeductions = ValidateLimits(ExemptIncomeAndDeductionsTotal, UVT, MaxUVTExemptIncomeAndDeductions)
        If LessExemptIncomeAndDeductions > ExemptIncomeAndDeductionsPrevious Then
            If LessExemptIncomeAndDeductions = ExemptIncomeAndDeductionsTotal Then
                Return ExemptIncomeAndDeductionsCalculated
            End If
            Return LessExemptIncomeAndDeductions - ExemptIncomeAndDeductionsPrevious
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Metodo que calcula la base de retencion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidarBaseRetenciones(ByVal Value As Decimal, ByVal SumDeductionsAndRentExents As Decimal, MaxDeductionsAndRentExents As Decimal) As Decimal
        If SumDeductionsAndRentExents > MaxDeductionsAndRentExents Then
            Return Value - MaxDeductionsAndRentExents
        Else
            Return Value - SumDeductionsAndRentExents
        End If
    End Function

    ''' <summary>
    ''' Calcula el valor del uvt para la retencion 383
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateRetention383(taxBase As Decimal, UVT As Decimal, ListRange383 As List(Of Domain.Entities.RetentionConceptRanges)) As Decimal
        Dim UVTTaxBase As Decimal = Decimal.Round(taxBase / UVT, 2)
        Dim ValueReturn As Decimal = 0
        If ListRange383 IsNot Nothing AndAlso ListRange383.Count > 0 Then
            Dim retentionConceptRange As Domain.Entities.RetentionConceptRanges = (From item In ListRange383 Where UVTTaxBase >= item.ValueInitial AndAlso UVTTaxBase < item.ValueFinish Select item).FirstOrDefault
            If retentionConceptRange IsNot Nothing Then
                ValueReturn = Decimal.Round(CDec(((UVTTaxBase - retentionConceptRange.ValueInitial) * (retentionConceptRange.Percentage / 100)) + retentionConceptRange.UVTIncrement), 2)
                ValueReturn = CDec(Utils.RoundValue(ValueReturn * UVT, Utils.RoundLevel.Thousands))
            End If
        End If
        Return ValueReturn
    End Function

    ''' <summary>
    ''' Calcula el valor del uvt para la retencion 384
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculateRetention384(ListRangeRetention384 As List(Of Domain.Entities.RetentionConceptRanges), FeeCommissionService As Decimal, PaymentHealthObligatory As Decimal, RiskWork As Decimal, RequiredContributions As Decimal, SolidarityPension As Decimal, UVT As Decimal) As Decimal
        Dim TaxBase As Decimal = FeeCommissionService - PaymentHealthObligatory - RiskWork - RequiredContributions - SolidarityPension
        Dim UVTTaxBase As Decimal = TaxBase / UVT
        Dim valueFinally As Decimal = 0
        Dim RangeUVT As Decimal = 0
        If ListRangeRetention384 IsNot Nothing AndAlso ListRangeRetention384.Count > 0 Then
            Dim retentionConceptRange As Domain.Entities.RetentionConceptRanges = (From item In ListRangeRetention384 Where UVTTaxBase >= item.ValueInitial AndAlso UVTTaxBase < item.ValueFinish Select item).FirstOrDefault()
            If retentionConceptRange IsNot Nothing Then
                If retentionConceptRange.ValueDeducted > 0 Then
                    RangeUVT = (UVTTaxBase * (retentionConceptRange.Percentage / 100)) - retentionConceptRange.ValueDeducted
                Else
                    RangeUVT = retentionConceptRange.Percentage
                End If
                valueFinally = CDec(Utils.RoundValue(RangeUVT * UVT, Utils.RoundLevel.Thousands))
            End If
        End If
        Return valueFinally
    End Function

    ''' <summary>
    ''' Calcula el porcentaje al valor final
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CalculatePercentageFinallyValue(retentionValue As Decimal, paymentMonth As Decimal) As Decimal
        Dim result As Decimal = 0
        If paymentMonth > 0 Then
            result = Decimal.Round((retentionValue / paymentMonth) * 100, 2)
        End If
        Return result
    End Function

    Public Function GetViewThirdPartyRetentionAccumulated(ThirdPartyId As Integer, Year As Integer) As Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionAccumulatedXpo
        Dim filtroConsulta As String = String.Format("ThirdPartyId = {0} AND Year = {1}", ThirdPartyId, Year)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of Infrastructure.Data.Xpo.PaymentsRepository.ViewThirdPartyRetentionAccumulatedXpo)(Nothing, filtroConsulta).FirstOrDefault
    End Function

    ''' <summary>
    ''' Obtiene los documentos CxP-Notas de retención
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    Public Function GetViewThirdPartyRetentionByFilter(filters As String) As List(Of PaymentsRepository.ViewThirdPartyRetentionXpo)
        Dim data = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsRepository.ViewThirdPartyRetentionXpo)(Nothing, filters)
        If data.Any() Then
            Return data.ToList()
        Else
            Return New List(Of PaymentsRepository.ViewThirdPartyRetentionXpo)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una sumatoria de valores previos de un listado de CxP por fecha y tercero
    ''' </summary>
    ''' <param name="dateDocument"></param>
    Public Sub GetPreviousValues(ThirdPartyId As Integer, dateDocument As Date)
        Dim initialDate As Date = New Date(dateDocument.Year, dateDocument.Month, 1)
        Dim endDate As Date = initialDate.AddMonths(1).AddDays(-1)
        Dim filters As String = String.Format("ThirdPartyId = {0} AND Type IN(1,3) AND DocumentDate >= #{1}# AND DocumentDate <= #{2}#", ThirdPartyId, initialDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"))
        Dim data = Me.GetViewThirdPartyRetentionByFilter(filters)
        If data.Any() Then
            Me.View.AccumulatedIncome = data.Sum(Function(s) s.DebitValue - s.CreditValue)
            Me.View.PreviousRetArt383 = data.Sum(Function(s) s.RetentionValue383)
        End If
    End Sub

#End Region

End Class
