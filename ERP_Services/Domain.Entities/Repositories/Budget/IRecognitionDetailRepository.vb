'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region
Public Interface IRecognitionDetailRepository
    Inherits IRepository(Of RecognitionDetail)
    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRecognitionDetailById(Id As Integer) As RecognitionDetail
End Interface
