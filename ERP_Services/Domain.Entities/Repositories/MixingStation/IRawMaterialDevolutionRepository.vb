'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-10
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IRawMaterialDevolutionRepository
    Inherits IRepository(Of RawMaterialDevolution)

    ''' <summary>
    ''' Obtiene una devolucion de materia prima por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetRawMaterialDevolutionByCode(code As String, Optional tracking As Boolean = True) As RawMaterialDevolution
End Interface
