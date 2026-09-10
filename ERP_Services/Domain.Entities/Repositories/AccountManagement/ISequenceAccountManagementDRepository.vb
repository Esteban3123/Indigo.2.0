'************************************************************
' Assembly         : Domain.Entities
' Author           : Felix Camilo Salazar Rolda
' Created          : 2021-12-13
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface ISequenceAccountManagementRepository
    Inherits IRepository(Of AccountManagementSequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As AccountManagementSequenceDetail

    Function GetSequenseDetailUpdatedById(idSequence As Int32) As AccountManagementSequenceDetail

#End Region

End Interface

