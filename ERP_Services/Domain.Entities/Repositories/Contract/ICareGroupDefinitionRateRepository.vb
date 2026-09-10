'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 18/11/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface ICareGroupDefinitionRateRepository
    Inherits IRepository(Of CareGroupDefinitionRate)
    ''' <summary>
    ''' metodo para retornar la una definicion de tarifa por id del grupo de atencion y fecha
    ''' </summary>
    ''' <param name="careGroupId"></param>
    ''' <param name="serviceDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCareGroupDefinitionRateByCareGroupIdAndDate(careGroupId As Integer, serviceDate As DateTime, Optional tracking As Boolean = True) As CareGroupDefinitionRate

    ''' <summary>
    ''' Se obtiene la primera condición que se encuentre con RIAS
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <returns></returns>
    Function GetConditionRIAS(definitionRateId As Integer) As DefinitionRateDetailCondition

End Interface
