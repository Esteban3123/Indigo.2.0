'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRawMaterialDevolutionDetailRepository
    Inherits IRepository(Of RawMaterialDevolutionDetail)

    ''' <summary>
    ''' Obtiene los detalles de devolucion de materia prima
    ''' </summary>
    ''' <param name="rawMatertialDevolutionId"></param>
    ''' <returns></returns>
    Function GetRawMaterialDevolutionDetailByRawMaterialDevolutionId(rawMatertialDevolutionId As Integer, Optional tracking As Boolean = True) As List(Of RawMaterialDevolutionDetail)
End Interface
