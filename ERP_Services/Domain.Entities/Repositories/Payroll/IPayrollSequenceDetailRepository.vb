'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan Carlos Bermudez
' Created          : 15-07-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IPayrollSequenceDetailRepository
    Inherits IRepository(Of PayrollSequenceDetail)

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As PayrollSequenceDetail
    Function GetSequenseDetailUpdatedById(idSequence As Integer) As PayrollSequenceDetail
End Interface
