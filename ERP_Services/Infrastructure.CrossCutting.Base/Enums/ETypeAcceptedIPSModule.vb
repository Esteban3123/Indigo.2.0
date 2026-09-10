Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion paera saber el modulo 
''' </summary>
''' <remarks></remarks>
<DataContract>
Public Enum ETypeAcceptedIPSModule As Integer
    ''' <summary>
    ''' Aceptacion de IPS en el proceso Glosa por primera ves
    ''' </summary>
    ''' <remarks></remarks>
    AcceptanceGlossProcessed = 1
    ''' <summary>
    '''  Aceptacion de IPS en el proceso reiteracion o glosa por segunda ves
    ''' </summary>
    ''' <remarks></remarks>
    AcceptanceReiterationProcessed = 2
    ''' <summary>
    ''' Aceptacion de IPS en el proceso de conciliacion
    ''' </summary>
    ''' <remarks></remarks>
    AcceptanceConciliationProcessed = 3
End Enum