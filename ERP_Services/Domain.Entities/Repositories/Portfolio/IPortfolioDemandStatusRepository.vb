'***********************************************************************
' Assembly         : Domain.Entities.Repositories.Portfolio
' Author           : Hector Rodriguez Rubiano
' Created          : 09-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base

Public Interface IPortfolioDemandStatusRepository
    Inherits IRepository(Of DemandStatus)

    ''' <summary>
    ''' Función que obtiene todos los estados de demanda
    ''' </summary>
    ''' <returns></returns>
    Function ListAllDemandStatus() As List(Of DemandStatus)

    ''' <summary>
    ''' Función que obtiene un estado de demanda por Código
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetDemandStatusByCode(Code As String) As DemandStatus

End Interface
