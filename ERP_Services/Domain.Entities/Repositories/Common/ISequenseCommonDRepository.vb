'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Maintenance.Entities
Imports Domain.Base
Imports Domain.Entities
#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface ISequenseCommonDRepository
    Inherits IRepository(Of CommonSequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As CommonSequenceDetail
    Function GetSequenseDetailUpdatedById(idSequence As Long) As CommonSequenceDetail

#End Region

End Interface