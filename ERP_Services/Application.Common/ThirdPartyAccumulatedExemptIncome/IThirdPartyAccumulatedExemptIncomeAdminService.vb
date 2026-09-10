Imports Domain.Base.Entities
Imports Domain.Entities

Public Interface IThirdPartyAccumulatedExemptIncomeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene la información de Ingresos exentos acumulados por tercero y por año
    ''' </summary>
    ''' <param name="idThirdParty">Año</param>
    ''' <param name="year">Año</param>
    ''' <returns></returns>
    Function GetThirdPartyYear(ByVal idThirdParty As Integer, ByVal year As String, Optional tracking As Boolean = True) As ThirdpartyAccumulatedExemptIncome

    ''' <summary>
    ''' Guarda o edita la información de Ingresos exentos acumulados por tercero y por año
    ''' </summary>
    ''' <param name="thirdParty">Tercero</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveThirdPartyAccumulatedExemptIncome(thirdParty As ThirdpartyAccumulatedExemptIncome) As ActionMessageResult(Of ThirdpartyAccumulatedExemptIncome)

End Interface
