Imports System.ComponentModel
Imports System.Runtime.Serialization
''' <summary>
''' Enumeracion para establecer los estados de los RIPS
''' </summary>
<DataContract()>
Public Enum EUserCampaingRole

    ''' <summary>
    ''' QF Calidad
    ''' </summary>
    <EnumMember>
    QualityControl = 1
    ''' <summary>
    ''' QF Producción 
    ''' </summary>
    <EnumMember>
    ProductionQuality = 2
    ''' <summary>
    ''' Auxiliar de Central de Mezclas 
    ''' </summary>
    <EnumMember>
    Assistant = 3
    ''' <summary>
    ''' Director técnico
    ''' </summary>
    <EnumMember>
    TechnicalDirector = 4
End Enum
