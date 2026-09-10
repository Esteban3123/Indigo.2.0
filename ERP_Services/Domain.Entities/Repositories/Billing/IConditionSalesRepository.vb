'************************************************************
' Assembly         : Domain.Billing
' Author           : Andres Alarcon
' Created          : 14-06-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IConditionSalesRepository
    Inherits IRepository(Of ConditionSales)

    ''' <summary>
    ''' Lista todas las condiciones de venta por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListConditionSalesByUserCode(ByVal userCode As String) As List(Of ConditionSales)

    ''' <summary>
    ''' Obtiene una condicion de venta por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetConditionSalesById(ByVal Id As Integer) As ConditionSales

    ''' <summary>
    ''' Obtiene una condicion de venta por codigo
    ''' </summary>
    ''' <returns></returns>
    Function GetConditionSalesByCode(ByVal code As String) As ConditionSales

End Interface
