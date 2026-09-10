'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/082015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IBudgetTransferRepository
    Inherits IRepository(Of BudgetTransfer)

    ''' <summary>
    ''' Obtiene un traslado por codigo
    ''' </summary>
    '''<param name="Code">Código del traslado</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <param name="yearValidity">Año vigencia</param>
    ''' <returns></returns>
    Function GetBudgetTransfer(Code As String, ItemType As Byte, yearValidity As Integer) As BudgetTransfer

    ''' <summary>
    ''' Obtiene un traslado por id
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    Function GetBudgetTransferById(Id As Integer) As BudgetTransfer

End Interface
