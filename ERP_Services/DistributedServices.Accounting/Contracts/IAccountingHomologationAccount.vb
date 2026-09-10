#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAccountingHomologationAccount

#Region "Methods"

    ''' <summary>
    ''' Guarda una homologacion de cuentas
    ''' </summary>
    ''' <returns>Resultado de la acción</returns>
    <OperationContract()>
    Function SaveHomologationAccount(ByVal ListHomologationAccount As List(Of HomologationAccount)) As ActionResult

#End Region

End Interface
