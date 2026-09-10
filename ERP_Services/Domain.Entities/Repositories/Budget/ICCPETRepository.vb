'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 28-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface ICCPETRepository
    Inherits IRepository(Of CCPET)

    ''' <summary>
    ''' Obtiene un CCPET por codigo y por tipo de rubro
    ''' </summary>
    '''<param name="Code">Código del Conceptos CCPET</param>
    ''' <returns></returns>
    Function GetCCPETByCode(Code As String) As CCPET

    ''' <summary>
    ''' consulta un CCPET por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCCPETById(Id As Integer) As CCPET

End Interface
