Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para determinar si realiza interfacez Nativa o Integracion 
''' </summary>
''' <remarks></remarks>
<DataContract()>
Public Enum EGlossesIntegration As Integer
    ''' <summary>
    ''' representa que el modulo realiza interfaz a Genesis
    ''' </summary>
    ''' <remarks></remarks>
    Native = 1
    ''' <summary>
    ''' representa que el modulo se integra con ERP DGH
    ''' </summary>
    ''' <remarks></remarks>
    Integration = 2
    ''' <summary>
    ''' Integracion con Dinamica FOX
    ''' </summary>
    IntegractionFox = 3
    ''' <summary>
    ''' Integracion Dinamica NET
    ''' </summary>
    IntegrationNet = 4

    ''' <summary>
    ''' 5 - Nativo Integracion (Este tipo corresponde a cuando hay una reparametrizacion en VIE y se requiere seguir glosando facturas que vienen desde el mismo VIE pero de Otra BD)
    ''' </summary>
    IntegrationNATIVE_MIGRATIONS = 5
End Enum