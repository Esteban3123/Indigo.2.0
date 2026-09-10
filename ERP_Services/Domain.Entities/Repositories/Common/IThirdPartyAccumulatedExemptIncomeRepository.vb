'************************************************************
' Assembly         : Domain.Entities.Common
' Author           : Jorge Eduardo Guerra Rojas
' Created          : 2023-03-02
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IThirdPartyAccumulatedExemptIncomeRepository
    Inherits IRepository(Of ThirdpartyAccumulatedExemptIncome)

    ''' <summary>
    ''' Funcion la cual obtiene la información de los ingresos exentos acumulados por tercero por año y tercero
    ''' </summary>
    ''' <param name="IdThirdParty">Id del tercero</param>
    ''' <param name="Year">año en vigencia</param>
    ''' <returns></returns>
    Function GetThirdpartyYear(ByVal IdThirdParty As Integer, ByVal Year As Integer, Optional tracking As Boolean = True) As ThirdpartyAccumulatedExemptIncome

End Interface
