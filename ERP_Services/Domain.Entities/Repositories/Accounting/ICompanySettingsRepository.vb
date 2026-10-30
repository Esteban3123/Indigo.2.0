'************************************************************
' Assembly         : Domain.Entities.Contracts
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 2014-10-07
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Contrato de repositorio para la entidad clase contable
''' </summary>
Public Interface ICompanySettingsRepository
    Inherits IRepository(Of CompanySettings)

    ''' <summary>
    ''' Funcion para obtener los parametros de la empres|||1|
    ''' </summary>
    ''' <returns></returns>
    Function GetCompanySettings(Optional ByVal AsNoTracking As Boolean = False) As CompanySettings

    ''' <summary>
    ''' Indica si ya existen cuentas por pagar (el consecutivo de radicación deja de ser editable).
    ''' </summary>
    Function HasAccountPayable() As Boolean

End Interface