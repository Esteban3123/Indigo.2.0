'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 11/04/2019
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IATCEntityRepository

    Inherits IRepository(Of ATCEntity)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetATCEntity(code As String) As ATCEntity

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetATCEntityById(id As Integer) As ATCEntity

End Interface