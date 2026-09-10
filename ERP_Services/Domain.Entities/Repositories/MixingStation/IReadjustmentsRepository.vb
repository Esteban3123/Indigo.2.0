'************************************************************
' Assembly         : Domain.MixingStation
' Author           : Giovanny Plazas L
' Created          : 25-08-2022
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del repositorio Consecutivos de lotes
''' </summary>
Public Interface IReadjustmentsRepository
    Inherits IRepository(Of Readjustments)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="RequestPackageDetailStatusId"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Function GetReadjustmentsByRequestPackageStatus(RequestPackageDetailStatusId As Integer, Optional tracking As Boolean = False) As List(Of Readjustments)

    ''' <summary>
    ''' Obtiene unaa readecuación por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function GetReadjustmentsById(Id As Integer) As Readjustments

End Interface
