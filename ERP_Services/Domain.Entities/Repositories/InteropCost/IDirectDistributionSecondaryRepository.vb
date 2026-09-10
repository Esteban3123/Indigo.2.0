'***********************************************************************
' Assembly         : Domain.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

Public Interface IDirectDistributionSecondaryRepository
    Inherits IRepository(Of DirectDistributionSecondary)


    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDirectDistributionSecondary(ByVal code As String) As DirectDistributionSecondary

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetDirectDistributionSecondaryById(id As Integer) As DirectDistributionSecondary

    ''' <summary>
    ''' Obtiene la cantidad de veces que esta el elemento de distribucion secundaria 
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCountByDistributionSecondaryId(idDistributionSecundary As Integer, month As Integer, year As Integer) As Integer

    ''' <summary>
    ''' Actualiza el campo import en la tabla LogisticProductionCenterRecordDetail
    ''' </summary>
    ''' <param name="ObjectXml"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_UpdateFieldImport(ObjectXml As String, Status As Integer) As SP_UpdateFieldImport_Result

End Interface
