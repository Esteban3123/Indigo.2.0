'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IObligationModificationDetailRepository
    Inherits IRepository(Of ObligationModificationDetail)

    ''' <summary>
    ''' Obtiene un detalle de una modificacion de obligacion por id
    ''' </summary>
    '''<param name="Id"></param>
    ''' <returns></returns>
    Function GetObligationModificationDetailById(Id As Integer) As ObligationModificationDetail

End Interface
