'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IInteropCostSequenceDetailRepository
    Inherits IRepository(Of InteropCostSecuenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene el detalle de la secuencia
    ''' </summary>
    Function GetSequenseDById(ByVal id As Int32) As InteropCostSecuenceDetail

#End Region

End Interface