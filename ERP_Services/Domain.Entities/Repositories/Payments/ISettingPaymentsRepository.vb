'***********************************************************************
' Assembly         : Domain.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface ISettingPaymentsRepository
    Inherits IRepository(Of SettingPayments)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingPaymentsByIdOperatingUnit(id As Integer, Optional tracking As Boolean = False) As SettingPayments

End Interface
