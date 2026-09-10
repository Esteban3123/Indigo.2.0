'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/082015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface IRecognitionRepository
    Inherits IRepository(Of Recognition)

    ''' <summary>
    ''' Obtiene un reconocimiento por codigo
    ''' </summary>
    '''<param name="Code">Código del reconocimiento</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetRecognition(Code As String, ItemType As Byte, budgetaryValidityId As Integer) As Recognition

    ''' <summary>
    ''' Obtiene un reconocimiento por id
    ''' </summary>
    '''<param name="Id">Id del reconocimiento</param>
    ''' <returns></returns>
    Function GetRecognitionById(Id As Integer) As Recognition

    ''' <summary>
    ''' ejecuta el store procedure para guardar un reconocimiento
    ''' </summary>
    ''' <param name="recognitionXml"></param>
    ''' <param name="recognitionDetailForDeleteXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveRecognition(recognitionXml As String, recognitionDetailForDeleteXml As String, codeUser As String) As SP_SaveRecognition_Result

End Interface
