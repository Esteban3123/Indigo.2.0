'************************************************************
' Assembly         : Domain.Billing
' Author           : Giovanny Plazas Lozano
' Created          : 04-03-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region


Public Interface IElectronicsPropertiesRepository
    Inherits IRepository(Of ElectronicsProperties)

    ''' <summary>
    ''' funcion que se encarga de consultar y retornar Flag cuando el origen de la Nota se deba enviar a Rips
    ''' </summary>
    ''' <param name="billingNoteId"></param>
    ''' <returns></returns>
    Function ValidateOriginNoteToRIPS(billingNoteId As Integer) As Boolean
End Interface
