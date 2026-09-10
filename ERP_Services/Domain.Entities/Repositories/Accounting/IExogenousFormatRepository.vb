#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IExogenousFormatRepository
    Inherits IRepository(Of ExogenousFormat)

    ''' <summary>
    ''' Obtiene un formato de exógena por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExogenousFormatById(id As Integer, Optional tracking As Boolean = True) As ExogenousFormat

    ''' <summary>
    ''' Obtiene un formato de exógena por codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetExogenousFormatByCode(code As String, Optional tracking As Boolean = True) As ExogenousFormat

End Interface
