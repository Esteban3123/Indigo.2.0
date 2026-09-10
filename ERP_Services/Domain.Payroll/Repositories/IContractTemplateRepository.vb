'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IContractTemplateRepository
    Inherits IRepository(Of ContractTemplate)

    ''' <summary>
    ''' Lista todas las Plantillas de Contrato
    ''' </summary>
    ''' <returns>Plantillas de Contrato</returns>
    ''' <remarks></remarks>
    Function ListAllContractTemplate() As List(Of ContractTemplate)

    ''' <summary>
    ''' Obtiene una Plantilla de Contrato
    ''' </summary>
    ''' <param name="code">Código de la Plantilla de Contrato</param>
    ''' <returns>Plantilla de Contrato</returns>
    ''' <remarks></remarks>
    Function GetContractTemplate(ByVal code As String, Optional desatach As Boolean = True) As ContractTemplate

End Interface
