'************************************************************
' Assembly         : Domain.Entities.Repossitories
' Author           : Felix Camilo Salazar Roldan
' Created          : 2024-13-12'

' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

Public Interface IAccountManagementSequenceDetailRepository
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
