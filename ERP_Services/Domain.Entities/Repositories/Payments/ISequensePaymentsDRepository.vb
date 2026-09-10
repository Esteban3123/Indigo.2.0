'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface ISequensePaymentsDRepository
    Inherits IRepository(Of PaymentsSecuenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As PaymentsSecuenceDetail

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id, validando el IdSequencePaymentsC
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDByIdSequencePaymentsC(ByVal id As Int32) As PaymentsSecuenceDetail
    Function GetSequenseDetailUpdatedById(idSequence As Int32) As PaymentsSecuenceDetail

#End Region

End Interface