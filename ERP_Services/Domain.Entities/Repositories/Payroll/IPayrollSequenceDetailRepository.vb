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

    ''' <summary>
    ''' Incrementa atomicamente el campo Next y retorna el valor PREVIO al incremento.
    ''' Disenado para uso en bucles masivos (confirmacion de nomina, etc).
    ''' NO interactua con el ChangeTracker de EF, por lo que no genera conflictos
    ''' de tracking si la entidad ya esta cargada en el contexto.
    ''' Garantiza atomicidad mediante UPDATE ... OUTPUT a nivel SQL.
    ''' </summary>
    ''' <param name="idSequence">Id del detalle de secuencia</param>
    ''' <returns>Valor PRE-incremento (el consecutivo a usar)</returns>
    Function IncrementSequenceAndGetNext(idSequence As Integer) As Long
End Interface
