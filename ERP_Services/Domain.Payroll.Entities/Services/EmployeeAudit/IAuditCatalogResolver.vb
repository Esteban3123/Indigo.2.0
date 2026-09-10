'***********************************************************************
' Assembly         : Domain.Payroll.Entities
' Author           : Jhon Willian Corredor Araujo
' Created          : 04-09-2026
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Catálogos cuyos identificadores se traducen a texto legible al construir la bitácora.
''' </summary>
Public Enum AuditCatalog
    FunctionalUnit
    Group
    ContractType
    Position
    Bank
    OrganizationChartPosition
    ContractModificationReason
    CostCenter
    WorkCenter
    EmployeeType
    PensionaryType
    ContributorSubtype
    ReligiousBeliefs
    EthnicGroups
    City
End Enum

''' <summary>
''' Colecciones de contacto de la persona que se auditan por elemento.
''' </summary>
Public Enum AuditContactKind
    Address
    Phone
    Email
End Enum

''' <summary>
''' Traduce a texto legible los valores que se persisten en la bitácora.
''' El grafo que llega del cliente puebla unas propiedades de navegación y otras no,
''' de modo que el texto se resuelve contra la base y no contra la navegación.
''' </summary>
Public Interface IAuditCatalogResolver

    ''' <summary>
    ''' Nombre del registro del catálogo, o cadena vacía si no existe.
    ''' </summary>
    ''' <param name="catalog">Catálogo a consultar</param>
    ''' <param name="id">Identificador del registro</param>
    Function GetName(catalog As AuditCatalog, id As Integer) As String

    ''' <summary>
    ''' Texto almacenado de un dato de contacto. Se consulta antes de confirmar el
    ''' guardado, de modo que devuelve el valor previo a la modificación.
    ''' </summary>
    ''' <param name="kind">Tipo de dato de contacto</param>
    ''' <param name="id">Identificador del elemento</param>
    Function GetContactText(kind As AuditContactKind, id As Integer) As String

End Interface
