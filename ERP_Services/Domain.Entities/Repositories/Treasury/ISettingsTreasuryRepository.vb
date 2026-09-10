'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ISettingsTreasuryRepository
    Inherits IRepository(Of SettingsTreasury)

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetSettingsTreasuryById(id As Integer) As SettingsTreasury

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative">The identifier unit operative.</param>
    ''' <returns></returns>
    Function GetSettingsTreasuryByIdUnitOperative(IdUnitOperative As Integer) As SettingsTreasury

End Interface
