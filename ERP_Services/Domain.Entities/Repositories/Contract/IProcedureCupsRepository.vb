'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IProcedureCupsRepository
    Inherits IRepository(Of ProcedureCups)

    Function GetCountProcedureCupsByProcedureTemplateIdAndCupsId(procedureTemplateId As Integer, CupsId As Integer, Optional RiasId As Integer? = Nothing, Optional ContractDescriptionId As Integer? = Nothing) As Integer

    ''' <summary>
    ''' Obtiene un detalle plantilla de procedimiento por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProcedureCupsById(id As Integer) As ProcedureCups
    ''' <summary>
    ''' lista los procedimientos cups por platilla de procedimiento y cupsId
    ''' </summary>
    ''' <param name="procedureTemplateId"></param>
    ''' <param name="CupsId"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProcedureProcedureCupsByProcedureTemplateIdAndCUPSId(procedureTemplateId As Integer, CupsId As Integer, Optional tracking As Boolean = True) As List(Of ProcedureCups)

End Interface
