'***********************************************************************
' Assembly         : Domain.Accounting
' Author           : Daniel Eduardo Arévalo
' Created          : 26/04/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ISettingsExogenousInformationRepository

    Inherits IRepository(Of SettingsExogenousInformation)

    ''' <summary>
    ''' funcion que lista todas los tipos de poliza
    ''' </summary>
    ''' <returns>Lista de tipos de poliza</returns>
    Function GetSettingsExogenousInformation(Optional tracking As Boolean = True) As List(Of SettingsExogenousInformation)

End Interface
