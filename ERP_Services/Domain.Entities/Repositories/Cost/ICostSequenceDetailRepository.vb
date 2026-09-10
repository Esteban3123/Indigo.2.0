'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface ICostSequenceDetailRepository
    Inherits IRepository(Of CostSecuenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene el detalle de la secuencia
    ''' </summary>
    Function GetSequenseDById(ByVal id As Int32) As CostSecuenceDetail

#End Region

End Interface