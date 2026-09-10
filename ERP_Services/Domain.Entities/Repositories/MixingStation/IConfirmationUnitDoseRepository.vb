'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IConfirmationUnitDoseRepository
    Inherits IRepository(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetConfirmationUnitDoseById(id As String, Optional tracking As Boolean = True) As ConfirmationUnitDose

    ''' <summary>
    ''' Sp que actualiza la orden medica
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_UpdateMedicalOrder(xml As String, userCode As String) As SP_UpdateMedicalOrder_Result

End Interface
