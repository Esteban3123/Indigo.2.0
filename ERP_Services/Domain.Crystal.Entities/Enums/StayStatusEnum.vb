'***********************************************************************
' Assembly         : Domain.Crystal.Entities
' Author           : Juan F. Tamayo
' Created          : 2015-01-24
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Runtime.Serialization

''' <summary>
''' Representa los estados en los que se puede
''' encontrar una estancia
''' </summary>
<DataContract()>
Public Enum StayStatusEnum

    ''' <summary>
    ''' Estado 1-Activo
    ''' </summary>
    <EnumMember()>
    Active = 1
    ''' <summary>
    ''' Estado 2-Pendiente de liquidar
    ''' </summary>
    <EnumMember()>
    PendingToLiquidate = 2
    ''' <summary>
    ''' Estado 3-Liquidado
    ''' </summary>
    <EnumMember()>
    Liquidated = 3

End Enum